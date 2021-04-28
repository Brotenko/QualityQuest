import os
import xml.etree.ElementTree as ET

settingsPath = "ServerLogic/ServerLogic/Properties/Settings.settings"


print("Hello, this is the QualityQuest-Server Installer-skript.\nThis script uses some Docker commands, so make sure Docker is installed and running.\nWhen rebuilding images, this script also removes dangling images, so if you use Docker for other things and use names like 'qqserver' you should back up the corresponding images.\nDo you:\n\t1: Want to (re)build image and start a fresh server?\n\t   (An unbiased recommendation: Do this exactly once.)\n\t2: Simply start the server with the previous parameters?\n\t   (Only works with an already existing image!)")
try:
    option = int(input())
except ValueError:
    print("Please only enter the respective number of the desired parameter.")
    exit()
ET.register_namespace('','http://schemas.microsoft.com/VisualStudio/2004/01/settings')
tree = ET.parse(settingsPath)
root = tree.getroot()
# 0 Logoutputtype
# 1 LogLevel
# 2 LogFilePath
# 3 ServerUrl
# 4 WebPageport
# 5 MCPort
# 6 CertFilePath
# 7 PW-Hash
# 8 Salt
# 9 CertPW
# 10 DockerURL
if int(option) == 1:
    print("Please enter the URL under which the website can be reached externally (without 'https://www.':")
    root[1][3][0].text = input()
    print("Please enter the port for the website (e.g. 443):")
    root[1][4][0].text = input()
    print("Please enter the port for the ModeratorClient to connect to:")
    root[1][5][0].text = input()
    print("Please enter the relative path to the certificate (must be .pfx):")
    root[1][6][0].text = input()
    print("Please enter the access-password for the certificate:")
    root[1][9][0].text = input()
    os.system("docker system prune -f")
    os.system("docker build -t qqserverimage .")
elif int(option) == 2:
    print("Loading params from Settings-File")
else:
    print("Please enter only the respective number of the desired parameter.")
    exit()
serverUrl = root[1][3][0].text
webPagePort = root[1][4][0].text
backendPort = root[1][5][0].text
certFilePath = root[1][6][0].text
certPW = root[1][9][0].text
root[1][10][0].text = "//0.0.0.0:"
tree.write(settingsPath, encoding='utf-8', xml_declaration=True)
os.system("echo webPort: "+webPagePort+" backendport: "+backendPort+" certFilePath: "+certFilePath+ " certPw: "+certPW)
# todo log&settings export from docker is missing right now
os.system("docker run --rm -it -p "+webPagePort+":7777 -p "+backendPort+":80 -e ASPNETCORE_URLS=\"https://+\" -e ASPNETCORE_HTTPS_PORT="+webPagePort+" -e ASPNETCORE_Kestrel__Certificates__Default__Password=\""+certPW+"\" -e ASPNETCORE_Kestrel__Certificates__Default__Path=./"+certFilePath+" --name=qqservercontainer qqserverimage")