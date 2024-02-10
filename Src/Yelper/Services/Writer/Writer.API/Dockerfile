#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Services/Writer/Writer.API/Writer.API.csproj", "Services/Writer/Writer.API/"]
COPY ["Common/EventBus/EventBus/EventBus.csproj", "Common/EventBus/EventBus/"]
COPY ["Common/EventBus/RabbitMQEventBus/RabbitMQEventBus.csproj", "Common/EventBus/RabbitMQEventBus/"]
COPY ["Services/Writer/Writer.Contracts/Writer.Contracts/Writer.Contracts.csproj", "Services/Writer/Writer.Contracts/Writer.Contracts/"]
RUN dotnet restore "Services/Writer/Writer.API/Writer.API.csproj"
COPY . .
WORKDIR "/src/Services/Writer/Writer.API"
RUN dotnet build "Writer.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Writer.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Writer.API.dll"]