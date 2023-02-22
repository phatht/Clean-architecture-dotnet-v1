#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["mcr-service-user/mcr-service-user.csproj", "mcr-service-user/"]
COPY ["Kernels.Infrastructure/Kernels.Infrastructure.csproj", "Kernels.Infrastructure/"]
COPY ["eve-bus/EventBusRabbitMQ/EventBusRabbitMQ.csproj", "eve-bus/EventBusRabbitMQ/"]
COPY ["eve-bus/EventBus/EventBus.csproj", "eve-bus/EventBus/"]
RUN dotnet restore "mcr-service-user/mcr-service-user.csproj"
COPY . .
WORKDIR "/src/mcr-service-user"
RUN dotnet build "mcr-service-user.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "mcr-service-user.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "mcr-service-user.dll"]