#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Api/SavePlan.API.csproj", "src/Api/"]
COPY ["src/BuildingBlocks /Application/BuildingBlocks.Application.csproj", "src/BuildingBlocks /Application/"]
COPY ["src/BuildingBlocks /Infrastructure/BuildingBlocks.Infrastructure.csproj", "src/BuildingBlocks /Infrastructure/"]
COPY ["src/Modules/Finances/Infrastructure/Finances.Infrastructure.csproj", "src/Modules/Finances/Infrastructure/"]
COPY ["src/Modules/Finances/Domain/Finances.Domain.csproj", "src/Modules/Finances/Domain/"]
COPY ["src/BuildingBlocks /Domain/BuildingBlocks.Domain.csproj", "src/BuildingBlocks /Domain/"]
COPY ["src/Modules/Finances/Application/Finances.Application.csproj", "src/Modules/Finances/Application/"]
RUN dotnet restore "./src/Api/SavePlan.API.csproj"
COPY . .
WORKDIR "/src/src/Api"
RUN dotnet build "./SavePlan.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SavePlan.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SavePlan.API.dll"]