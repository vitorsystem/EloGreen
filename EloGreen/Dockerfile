# Fase base para execução
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Fase de compilação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# 1. Copiando TODOS os projetos da sua arquitetura para o Restore funcionar
COPY ["EloGreen/EloGreen.API.csproj", "EloGreen/"]
COPY ["EloGreen.Application/EloGreen.Application.csproj", "EloGreen.Application/"]
COPY ["EloGreen.Domain/EloGreen.Domain.csproj", "EloGreen.Domain/"]
COPY ["EloGreen.Infrastructure/EloGreen.Infrastructure.csproj", "EloGreen.Infrastructure/"]

# 2. Restaurando os pacotes (agora com o nome correto: EloGreen.API)
RUN dotnet restore "./EloGreen/EloGreen.API.csproj"

# 3. Copiando o resto do código-fonte
COPY . .
WORKDIR "/src/EloGreen"

# 4. Compilando o projeto
RUN dotnet build "./EloGreen.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Fase de publicação
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EloGreen.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Fase final (Imagem de Produção)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# 5. Entrypoint ajustado para a DLL correta
ENTRYPOINT ["dotnet", "EloGreen.API.dll"]