FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Iduff.csproj", "./"]
RUN dotnet restore "Iduff.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "Iduff.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Iduff.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# Copiando o banco de dados SQLite para o diretório de trabalho do contêiner
COPY --chown=$APP_UID:$APP_UID iduff.db ./iduff.db

ENTRYPOINT ["dotnet", "Iduff.dll"]
