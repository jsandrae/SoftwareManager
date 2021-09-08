#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["SoftwareManager/Server/SoftwareManager.Server.csproj", "SoftwareManager/Server/"]
COPY ["SoftwareManager/Shared/SoftwareManager.Shared.csproj", "SoftwareManager/Shared/"]
COPY ["SoftwareManager/Client/SoftwareManager.Client.csproj", "SoftwareManager/Client/"]
RUN dotnet restore "SoftwareManager/Server/SoftwareManager.Server.csproj"
COPY . .
WORKDIR "/src/SoftwareManager/Server"
RUN dotnet build "SoftwareManager.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SoftwareManager.Server.csproj" -c Release -o /app/publish

FROM base AS final
RUN apt-get -y update && apt -y install nano
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SoftwareManager.Server.dll"]