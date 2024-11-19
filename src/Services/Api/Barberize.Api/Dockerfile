#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Api/Barberize.Api/Barberize.Api.csproj", "Services/Api/Barberize.Api/"]
COPY ["Modules/Settings/Barberize.Settings/Barberize.Settings.csproj", "Modules/Settings/Barberize.Settings/"]
COPY ["Shared/Barberize.Shared/Barberize.Shared.csproj", "Shared/Barberize.Shared/"]
COPY ["Shared/Barberize.Shared.Contracts/Barberize.Shared.Contracts.csproj", "Shared/Barberize.Shared.Contracts/"]
RUN dotnet restore "./Services/Api/Barberize.Api/Barberize.Api.csproj"
COPY . .
WORKDIR "/src/Services/Api/Barberize.Api"
RUN dotnet build "./Barberize.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Barberize.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Barberize.Api.dll"]