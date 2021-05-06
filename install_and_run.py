import json
import os
import sys

pathServerParams = "Saves/serverParams.json"
print(
    "Hello, this is the QualityQuest-Server Installer-skript.\nThis script uses some Docker commands, so make sure Docker is installed and running.\nWhen rebuilding images, this script also removes dangling images, so if you use Docker for other things and use names like 'qqserver' you should back up the corresponding images.\nDo you:\n\t1: (Install) (Re)build image and start a fresh server.\n\t   (Recommendation: Do this exactly once.)\n\t2: (Configurate) Reuse an existing image but change some params.\n\t    (This will also reset LogLevel & LogOutputType)\n\t3: (Continue) Simply start the server with the previous parameters.\n\t   (Only works with an already existing image!)")
try:
    option = int(input())
except ValueError:
    print("Please only enter the respective number of the desired parameter.")
    exit()
# get params, build new image, run container with params
if int(option) == 1:
    print("Please enter the URL under which the website can be reached (written like 'qualityquest.com')")
    ServerURL = input()
    print("Please enter the port for the website (e.g. 443):")
    PAWebPagePort = input()
    print("Please enter the port for the ModeratorClient to connect to:")
    MCWebSocketPort = input()
    print("Please make sure that the certificate (must be .pfx) is inside 'QualityQuest/ServerLogic' and enter the name of it:")
    CertFilePath = input()
    if not os.path.isfile(os.path.dirname(os.path.abspath(__file__))+"/ServerLogic/"+CertFilePath) or not CertFilePath.__contains__(".pfx"):
        print("The Cert-File was not found, please make sure that it's inside 'QualityQuest/ServerLogic' and is of .pfx format.")
        exit()
    print("Please enter the access-password for the certificate:")
    CertPW = input()
    print("Removing dangling Docker-images...")
    dic_opts = {"ServerURL": ServerURL, "PAWebPagePort": PAWebPagePort, "MCWebSocketPort": MCWebSocketPort,
                "CertFilePath": CertFilePath, "CertPW": CertPW, "PWHash": "", "Salt": "", "LogLevel": 0,
                "LogOutPutType": 2}
    json_obj = json.dumps(dic_opts, indent=4)
    with open(pathServerParams, "w") as outfile:
        outfile.write(json_obj)
    with open("ServerLogic/ServerLogic/Properties/serverParams.json", "w") as outfile:
        outfile.write(json_obj)
    os.system("docker system prune -f")
    print("Build new Image...")
    os.system("docker build -t qqserverimage .")
# get params and run container on existing image with params
elif int(option) == 2:
    print("Please enter the URL under which the website can be reached (written like 'qualityquest.com')")
    ServerURL = input()
    print("Please enter the port for the website (e.g. 443):")
    PAWebPagePort = input()
    print("Please enter the port for the ModeratorClient to connect to:")
    MCWebSocketPort = input()
    print("Please make sure that the certificate (must be .pfx) is inside 'QualityQuest/ServerLogic' and enter the name of it:")
    CertFilePath = input()
    if not os.path.isfile(os.path.dirname(os.path.abspath(__file__)) + "/ServerLogic/" + CertFilePath) or not CertFilePath.__contains__(".pfx"):
        print("The Cert-File was not found, please make sure that it's inside 'QualityQuest/ServerLogic' and is of .pfx format.")
        exit()
    print("Please enter the access-password for the certificate:")
    CertPW = input()
    dic_opts = {"ServerURL": ServerURL, "PAWebPagePort": PAWebPagePort, "MCWebSocketPort": MCWebSocketPort,
                "CertFilePath": CertFilePath, "CertPW": CertPW, "PWHash": "", "Salt": "", "LogLevel": 0,
                "LogOutPutType": 2}
    json_obj = json.dumps(dic_opts, indent=4)
    with open(pathServerParams, "w") as outfile:
        outfile.write(json_obj)
# run container on existing image with saved params
elif int(option) == 3:
    print("Transfering params from Saves to Settings-File ...")
else:
    print("Please enter only the respective number of the desired parameter.")
    exit()
with open(pathServerParams) as f:
    paramsData = json.load(f)
PAWebPagePort = paramsData["PAWebPagePort"]
MCWebSocketPort = paramsData["MCWebSocketPort"]
CertFilePath = paramsData["CertFilePath"]
CertPW = paramsData["CertPW"]

print("Removing old containers ...")
os.system("docker rm -f qqservercontainer")
if os.name == 'nt':
    #windows
    os.system("cls")
    SavesPath = os.path.dirname(os.path.abspath(__file__)) + '\Saves'
else:
    #linux/...
    os.system("clear")
    SavesPath = os.path.dirname(os.path.abspath(__file__)) + '/Saves'
os.system("docker run --rm -it -p " + str(PAWebPagePort) + ":443 -p " + MCWebSocketPort + ":80 -e ASPNETCORE_URLS=\"https://+;http://+\" -e ASPNETCORE_HTTPS_PORT=443 -e ASPNETCORE_Kestrel__Certificates__Default__Password=\"" + CertPW + "\" -e ASPNETCORE_Kestrel__Certificates__Default__Path=./" + CertFilePath + " -v "+SavesPath+":/app/ServerLogic/Properties/Persist --name=qqservercontainer qqserverimage")
