FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine
WORKDIR /app
EXPOSE 7777
EXPOSE 8181
COPY ServerLogic/ ./
RUN dotnet publish ./ServerLogic.sln -c Release -o build --self-contained=false
CMD ["dotnet", "./build/ServerLogic.dll", "!Password123#", "7777"]


##############################
# The dot '.' means that the Dockerfile is in the current directory. Otherwise specify the path to it instead. 
# 'docker build -t qqserver .'
# --rm is optional, to remove the container after it stopped
# 'docker run --rm -it -p 7777:7777 qqserver'
# bzw. 'docker run --rm -it -p 7777:80 qqserver'