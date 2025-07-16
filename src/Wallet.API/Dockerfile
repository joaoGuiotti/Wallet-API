FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copie os arquivos de projeto primeiro para otimizar o cache do Docker
COPY src/Wallet.API/Wallet.API.csproj Wallet.API/
COPY src/Wallet.Application/Wallet.Application.csproj Wallet.Application/
COPY src/Wallet.Domain/Wallet.Domain.csproj Wallet.Domain/
COPY src/Wallet.Infrastructure/Wallet.Infrastructure.csproj Wallet.Infrastructure/

RUN dotnet restore Wallet.API/Wallet.API.csproj

# Copie o restante dos arquivos do projeto
COPY src/Wallet.API/. ./Wallet.API/
COPY src/Wallet.Application/. ./Wallet.Application/
COPY src/Wallet.Domain/. ./Wallet.Domain/
COPY src/Wallet.Infrastructure/. ./Wallet.Infrastructure/

RUN dotnet publish Wallet.API/Wallet.API.csproj -c $BUILD_CONFIGURATION -o /app/publish
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
COPY ./scripts/entrypoint.sh .
RUN chmod +x ./entrypoint.sh
# ENTRYPOINT ["./entrypoint.sh"]
ENTRYPOINT ["dotnet", "Wallet.API.dll"]


