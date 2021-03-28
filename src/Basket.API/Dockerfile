#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Basket.API/Basket.API.csproj", "src/Basket.API/"]
COPY ["src/Interchange/EventBus.Messages/EventBus.Messages.csproj", "src/Interchange/EventBus.Messages/"]
COPY ["src/Interchange/Common.Logging/Common.Logging.csproj", "src/Interchange/Common.Logging/"]
RUN dotnet restore "src/Basket.API/Basket.API.csproj"
COPY . .
WORKDIR "/src/src/Basket.API"
RUN dotnet build "Basket.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Basket.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Basket.API.dll"]
