#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/Production/Production.API/Production.API.csproj", "src/Production/Production.API/"]
RUN dotnet restore "src/Production/Production.API/Production.API.csproj"
COPY . .
WORKDIR "/src/src/Production/Production.API"
RUN dotnet build "Production.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Production.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Production.API.dll"]
