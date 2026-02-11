# AS cria um "apelido" para a imagem
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base

# Cria um usuário não-root para rodar a aplicação
RUN adduser --disabled-password --uid 1000 appuser

USER appuser
WORKDIR /app
EXPOSE 80

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["HelloDocker/HelloDocker.csproj", "HelloDocker/"]
RUN dotnet restore "./HelloDocker/HelloDocker.csproj"
COPY . .
WORKDIR "/src/HelloDocker"
RUN dotnet build "./HelloDocker.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./HelloDocker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS Code in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HelloDocker.dll"]