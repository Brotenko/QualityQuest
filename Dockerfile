FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY ServerLogic/ ./
RUN dotnet publish ./ServerLogic.sln -c Release -o build --self-contained=false
ENTRYPOINT ["dotnet", "./build/ServerLogic.dll"]
