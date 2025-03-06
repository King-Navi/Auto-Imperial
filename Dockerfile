FROM mcr.microsoft.com/dotnet/sdk:9.0

WORKDIR /app

COPY . .

RUN dotnet restore

RUN dotnet build --configuration Release --no-restore

CMD ["dotnet", "test", "--configuration", "Release", "--no-build", "/p:CollectCoverage=true", "/p:CoverletOutputFormat=opencover"]
