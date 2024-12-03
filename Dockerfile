#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ecms.API/core/ecms.API/ecms.API.csproj", "ecms.API/core/ecms.API/"]
COPY ["ecms.API/infrastructure/ecms.Infrastructure/ecms.Infrastructure.csproj", "ecms.API/infrastructure/ecms.Infrastructure/"]
COPY ["ecms.API/core/ecms.Application/ecms.Application.csproj", "ecms.API/core/ecms.Application/"]
COPY ["ecms.API/core/ecms.Domain/ecms.Domain.csproj", "ecms.API/core/ecms.Domain/"]
COPY ["ecms.API/SharedKernal/SharedKernal.csproj", "ecms.API/SharedKernal/"]
RUN dotnet restore "./ecms.API/core/ecms.API/./ecms.API.csproj"
COPY . .
WORKDIR "/src/ecms.API/core/ecms.API"
RUN dotnet build "./ecms.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ecms.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ecms.API.dll"]