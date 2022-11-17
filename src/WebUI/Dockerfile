FROM node:latest AS node_base

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base

WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
COPY --from=node_base . .
WORKDIR /app
COPY . .
WORKDIR /app/src/WebUI
RUN dotnet restore
RUN dotnet build "./WebUI.csproj" --no-restore -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./WebUI.csproj" --no-restore -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "WebUI.dll" ]