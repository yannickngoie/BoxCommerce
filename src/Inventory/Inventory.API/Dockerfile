#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Inventory/Inventory.API/Inventory.API.csproj", "src/Inventory/Inventory.API/"]
COPY ["src/Common.Utilities/Common.Utilities.csproj", "src/Common.Utilities/"]
COPY ["src/Interchange/EventBus.Messages/EventBus.Messages.csproj", "src/Interchange/EventBus.Messages/"]
COPY ["src/Interchange/Common.Logging/Common.Logging.csproj", "src/Interchange/Common.Logging/"]
COPY ["../Common.Extensions/Common.Extensions.csproj", "../Common.Extensions/"]
RUN dotnet restore "src/Inventory/Inventory.API/Inventory.API.csproj"
COPY . .
WORKDIR "/src/src/Inventory/Inventory.API"
RUN dotnet build "Inventory.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Inventory.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Inventory.API.dll"]
