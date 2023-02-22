#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["mcr-service-webhooks/mcr-service-webhooks.csproj", "mcr-service-webhooks/"]
RUN dotnet restore "mcr-service-webhooks/mcr-service-webhooks.csproj"
COPY . .
WORKDIR "/src/mcr-service-webhooks"
RUN dotnet build "mcr-service-webhooks.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "mcr-service-webhooks.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "mcr-service-webhooks.dll"]