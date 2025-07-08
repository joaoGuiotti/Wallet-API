FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
COPY . .
RUN dotnet restore "Wallet.API/Wallet.API.csproj"
RUN dotnet publish "Wallet.API/Wallet.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
COPY ./scripts/entrypoint.sh .
RUN chmod +x ./entrypoint.sh
# ENTRYPOINT ["./entrypoint.sh"]
ENTRYPOINT ["dotnet", "Wallet.API.dll"]


