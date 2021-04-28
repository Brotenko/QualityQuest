FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine
WORKDIR /app
EXPOSE 80
EXPOSE 7777
COPY ServerLogic/ ./
RUN dotnet publish ./ServerLogic.sln -c Release -o build --self-contained=false
#CMD ["dotnet", "./build/ServerLogic.dll", "!Password123#", "7777"]
ENTRYPOINT ["dotnet", "./build/ServerLogic.dll"]


############# A small compilation of Docker commands. Will be removed from the Dockerfile in the distant future.#################

# Rebuilding a docker image only needs to be done if there were changes on the code-basis.
# The dot '.' means that the Dockerfile is in the current directory. Otherwise specify the path to it instead. -t <xyz> sets the name of the image.
# 'docker build -t qqserver .'
# --rm is optional, to remove the container after it stopped (might be helpful if you rebuild and test an image several times). -it (or only -i) starts the container in "interactive-mode", which means in case of qq that you can interact with the ServerShell. -p xxxx:yyyy maps the containers port x to the host port y. Finally, specify which image the container should use. 
# ports might be changed, as 443 is needed for https
# 'docker run --rm -it -p 80:7777 -p 443:8181 qqserver'
# if you want to reuse a stopped container, get it's name (wich differs from the name of the image!) by using 
# 'docker ps -a' 
# start with
# 'docker start -i Containername'
# if a container is already running and you want to see/use the ServerShell, enter 
# 'docker container attach Containername'

# Exports logs to local machine
# 'docker run -v $(pwd):/app/Logs --rm -it -p 80:7777 -p 8181:8181 --name=qqserver qqserver'

#
#working
#docker run --rm -it -p 443:7777 -p 8181:8181 -e ASPNETCORE_URLS="https://+;http://+" -e ASPNETCORE_HTTPS_PORT=443 -e ASPNETCORE_Kestrel__Certificates__Default__Password="thisIsForTestingOnly" -e ASPNETCORE_Kestrel__Certificates__Default__Path=./TestCert.pfx --name=qqservercontainer qqserverimage
#old
#docker run --rm -it -p 443:7777 -p 8181:8181 -e ASPNETCORE_URLS="https://+;http://+" -e ASPNETCORE_HTTPS_PORT=443 -e ASPNETCORE_Kestrel__Certificates__Default__Password="thisIsForTestingOnly" -e ASPNETCORE_Kestrel__Certificates__Default__Path=./TestCert.pfx --name=qqserver qqserver
#docker run --rm -it -p 443:7777 -p 8181:8181 -e ASPNETCORE_URLS="https://+;http://+" -e ASPNETCORE_HTTPS_PORT=443 -e ASPNETCORE_Kestrel__Certificates__Default__Password="" -e ASPNETCORE_Kestrel__Certificates__Default__Path=../LEcert.pfx --name=qqserver qqserver