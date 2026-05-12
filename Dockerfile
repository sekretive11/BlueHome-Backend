# =========================
# BUILD STAGE
# =========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# копируем solution и csproj
COPY *.sln .
COPY BlueHome.Server.API/*.csproj BlueHome.Server.API/
COPY BlueHome.Server.Application/*.csproj BlueHome.Server.Application/
COPY BlueHome.Server.Domain/*.csproj BlueHome.Server.Domain/
COPY BlueHome.Server.Infrastructure/*.csproj BlueHome.Server.Infrastructure/

RUN dotnet restore

# копируем остальной код
COPY . .

RUN dotnet publish BlueHome.Server.API/BlueHome.Server.API.csproj \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

# =========================
# RUNTIME STAGE
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "BlueHome.Server.API.dll"]