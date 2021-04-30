import json
import os

pathServerParams = "ServerLogic/ServerLogic/Properties/serverParams.json"
# todo sollte über -v mit docker veränderbar/lesbar sein, im code wird einfach das settingsfile mit den werten befüllt und gespeichert, dadurch unabhängig vom image bau
print(
    "Hello, this is the QualityQuest-Server Installer-skript.\nThis script uses some Docker commands, so make sure Docker is installed and running.\nWhen rebuilding images, this script also removes dangling images, so if you use Docker for other things and use names like 'qqserver' you should back up the corresponding images.\nDo you:\n\t1: (Install) (Re)build image and start a fresh server.\n\t   (Recommendation: Do this exactly once.)\n\t2: (Start) Simply start the server with the previous parameters.\n\t   (Only works with an already existing image!)")
try:
    option = int(input())
except ValueError:
    print("Please only enter the respective number of the desired parameter.")
    exit()
if int(option) == 1:
    print("Please enter the URL under which the website can be reached (without 'https://www.':")
    ServerURL = input()
    print("Please enter the port for the website (e.g. 443):")
    PAWebPagePort = input()
    print("Please enter the port for the ModeratorClient to connect to:")
    MCWebSocketPort = input()
    print(
        "Please make sure that the certificate (must be .pfx) is inside 'QualityQuest/ServerLogic' and enter the name of it:")
    CertFilePath = input()
    print("Please enter the access-password for the certificate:")
    CertPW = input()
    print("Removing dangling Docker-images:")

    dic_opts = {"ServerURL": ServerURL, "PAWebPagePort": PAWebPagePort, "MCWebSocketPort":MCWebSocketPort, "CertFilePath":CertFilePath, "CertPW":CertPW}
    json_obj = json.dumps(dic_opts, indent=4)
    with open(pathServerParams, "w") as outfile:
        outfile.write(json_obj)
    os.system("docker system prune -f")
    print("Build new Image:")
    os.system("docker build -t qqserverimage .")
elif int(option) == 2:
    print("Loading params from Settings-File")
else:
    print("Please enter only the respective number of the desired parameter.")
    exit()
with open(pathServerParams) as f:
    paramsData = json.load(f)
PAWebPagePort = paramsData["PAWebPagePort"]
MCWebSocketPort = paramsData["MCWebSocketPort"]
CertFilePath = paramsData["CertFilePath"]
CertPW = paramsData["CertPW"]
os.system("docker run --rm -it -p " + PAWebPagePort + ":7777 -p " + MCWebSocketPort + ":80 -e ASPNETCORE_URLS=\"https://+;http://+\" -e ASPNETCORE_HTTPS_PORT=" + PAWebPagePort + " -e ASPNETCORE_Kestrel__Certificates__Default__Password=\"" + CertPW + "\" -e ASPNETCORE_Kestrel__Certificates__Default__Path=./" + CertFilePath + " --name=qqservercontainer qqserverimage")

#os.system("echo docker run --rm -it -p " + PAWebPagePort + ":7777 -p " + MCWebSocketPort + ":80 -e ASPNETCORE_URLS=\"https://+;http://+\" -e ASPNETCORE_HTTPS_PORT=" + PAWebPagePort + " -e ASPNETCORE_Kestrel__Certificates__Default__Password=\"" + CertPW + "\" -e ASPNETCORE_Kestrel__Certificates__Default__Path=./" + CertFilePath + " --name=qqservercontainer qqserverimage")
