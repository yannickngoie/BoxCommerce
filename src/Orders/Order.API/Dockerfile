#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Orders/Order.API/Order.API.csproj", "src/Orders/Order.API/"]
COPY ["src/Interchange/EventBus.Messages/EventBus.Messages.csproj", "src/Interchange/EventBus.Messages/"]
COPY ["src/Orders/Order.Infrastructure/Order.Infrastructure.csproj", "src/Orders/Order.Infrastructure/"]
COPY ["src/Orders/Order.Application/Order.Application.csproj", "src/Orders/Order.Application/"]
COPY ["src/Interchange/Common.Logging/Common.Logging.csproj", "src/Interchange/Common.Logging/"]
COPY ["src/Orders/Order.Domain/Order.Domain.csproj", "src/Orders/Order.Domain/"]
COPY ["../Common.Entities/Common.Models.csproj", "../Common.Entities/"]
RUN dotnet restore "src/Orders/Order.API/Order.API.csproj"
COPY . .
WORKDIR "/src/src/Orders/Order.API"
RUN dotnet build "Order.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Order.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Order.API.dll"]
