import json
import os

pathServerParams = "ServerLogic/ServerLogic/Properties/serverParams.json"
# todo sollte über -v mit docker veränderbar/lesbar sein, im code wird einfach das settingsfile mit den werten befüllt und gespeichert, dadurch unabhängig vom image bau
# TODO dritte option "build from image with new parameters etc"
print(
    "Hello, this is the QualityQuest-Server Installer-skript.\nThis script uses some Docker commands, so make sure Docker is installed and running.\nWhen rebuilding images, this script also removes dangling images, so if you use Docker for other things and use names like 'qqserver' you should back up the corresponding images.\nDo you:\n\t1: (Install) (Re)build image and start a fresh server.\n\t   (Recommendation: Do this exactly once.)\n\t2: (Configurate) Reuse an existing image but change some params.\n\t3: (Continue) Simply start the server with the previous parameters.\n\t   (Only works with an already existing image!)")
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
    print("Removing dangling Docker-images...")
    dic_opts = {"ServerURL": ServerURL, "PAWebPagePort": PAWebPagePort, "MCWebSocketPort": MCWebSocketPort,
                "CertFilePath": CertFilePath, "CertPW": CertPW, "PWHash": "", "Salt": "", "LogLevel": 0,
                "LogOutPutType": 2}
    json_obj = json.dumps(dic_opts, indent=4)
    with open(pathServerParams, "w") as outfile:
        outfile.write(json_obj)
    #with open("Saves/serverParams.json", "w") as outfile:
    #    outfile.write(json_obj)
    os.system("docker system prune -f")
    print("Build new Image...")
    os.system("docker build -t qqserverimage .")
elif int(option) == 2:
    #todo load image from tar? ask for differente params, equal to 1, without building image
    print("Please enter the URL under which the website can be reached (without 'https://www.':")
    ServerURL = input()
    print("Please enter the port for the website (e.g. 443):")
    PAWebPagePort = input()
    print("Please enter the port for the ModeratorClient to connect to:")
    MCWebSocketPort = input()
    print("Please make sure that the certificate (must be .pfx) is inside 'QualityQuest/ServerLogic' and enter the name of it:")
    CertFilePath = input()
    print("Please enter the access-password for the certificate:")
    CertPW = input()
    dic_opts = {"ServerURL": ServerURL, "PAWebPagePort": PAWebPagePort, "MCWebSocketPort": MCWebSocketPort,
                "CertFilePath": CertFilePath, "CertPW": CertPW, "PWHash": "", "Salt": "", "LogLevel": 0,
                "LogOutPutType": 2}
    json_obj = json.dumps(dic_opts, indent=4)
    with open(pathServerParams, "w") as outfile:
        outfile.write(json_obj)
elif int(option) == 3:
    print("Transfering params from Saves to Settings-File ...")
    with open("Saves/serverParams.json") as f:
        OldParamsData = json.load(f)
    old_dic_opts = {"ServerURL": OldParamsData["ServerURL"], "PAWebPagePort": OldParamsData["PAWebPagePort"], "MCWebSocketPort": OldParamsData["MCWebSocketPort"],
                "CertFilePath": OldParamsData["CertFilePath"], "CertPW": OldParamsData["CertPW"], "PWHash": OldParamsData["PWHash"], "Salt": OldParamsData["Salt"], "LogLevel": OldParamsData["LogLevel"],
                "LogOutPutType": OldParamsData["LogOutPutType"]}
    old_json_obj = json.dumps(old_dic_opts, indent=4)
    with open(pathServerParams, "w") as outfile:
        outfile.write(old_json_obj)
    # todo "recycling", reuse older settings file
    # todo volume mount entstprechend anpassen, serverParams.json entsprechend kopieren, os bestimmen
else:
    print("Please enter only the respective number of the desired parameter.")
    exit()
# todo pfad zum eigenen Verzeichnis bestimmen, volume mount entstprechend anpassen, serverParams.json entsprechend kopieren (wenn vorherige werte übernommen werden sollen)
with open(pathServerParams) as f:
    paramsData = json.load(f)
PAWebPagePort = paramsData["PAWebPagePort"]
MCWebSocketPort = paramsData["MCWebSocketPort"]
CertFilePath = paramsData["CertFilePath"]
CertPW = paramsData["CertPW"]
SavesPath = os.path.dirname(os.path.abspath(__file__))+'\Saves'

os.system("docker run --rm -it -p " + str(PAWebPagePort) + ":7777 -p " + MCWebSocketPort + ":80 -e ASPNETCORE_URLS=\"https://+;http://+\" -e ASPNETCORE_HTTPS_PORT=" + str(PAWebPagePort) + " -e ASPNETCORE_Kestrel__Certificates__Default__Password=\"" + CertPW + "\" -e ASPNETCORE_Kestrel__Certificates__Default__Path=./" + CertFilePath + " -v "+SavesPath+":/app/ServerLogic/Properties/Persist --name=qqservercontainer qqserverimage")
