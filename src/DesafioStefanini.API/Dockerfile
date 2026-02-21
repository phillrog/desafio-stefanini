FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
EXPOSE 5001

# Estágio de Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# 1. Copia os projetos (Caminhos relativos à RAIZ do repositório)
COPY ["src/DesafioStefanini.API/DesafioStefanini.API.csproj", "DesafioStefanini.API/"]
COPY ["src/DesafioStefanini.Application/DesafioStefanini.Application.csproj", "DesafioStefanini.Application/"]
COPY ["src/DesafioStefanini.Domain/DesafioStefanini.Domain.csproj", "DesafioStefanini.Domain/"]
COPY ["src/DesafioStefanini.Infrastructure/DesafioStefanini.Infrastructure.csproj", "DesafioStefanini.Infrastructure/"]

# 2. Restaura
RUN dotnet restore "DesafioStefanini.API/DesafioStefanini.API.csproj"

# 3. Copia o código todo (O .dockerignore vai filtrar a pasta .front)
COPY . .

# 4. Compila
WORKDIR "/src/src/DesafioStefanini.API"
RUN dotnet build "DesafioStefanini.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Estágio de Publicação
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "DesafioStefanini.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Estágio Final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS=http://+:5001
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "DesafioStefanini.API.dll"]