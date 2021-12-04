FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["ForceTestingService.WEB/ForceTestingService.WEB.csproj", "ForceTestingService.WEB/"]
COPY ["ForceTestingService.ApplicationCore/ForceTestingService.ApplicationCore.csproj", "ForceTestingService.ApplicationCore/"]
COPY ["ForceTestingService.Infrastructure/ForceTestingService.Infrastructure.csproj", "ForceTestingService.Infrastructure/"]
RUN dotnet restore "ForceTestingService.WEB/ForceTestingService.WEB.csproj"
COPY . .
WORKDIR "/src/ForceTestingService.WEB"
RUN dotnet build "ForceTestingService.WEB.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ForceTestingService.WEB.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
EXPOSE 80
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ForceTestingService.WEB.dll"]

