FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/UserService/UserService/UserService.csproj", "src/UserService/UserService/"]
COPY ["src/UserService/UserService.Application/UserService.Application.csproj", "src/UserService/UserService.Application/"]
COPY ["src/UserService/UserService.Entities/UserService.Entities.csproj", "src/UserService/UserService.Entities/"]
COPY ["src/UserService/UserService.Shared/UserService.Shared.csproj", "src/UserService/UserService.Shared/"]
COPY ["src/UserService/UserService.Contracts/UserService.Contracts.csproj", "src/UserService/UserService.Contracts/"]
COPY ["src/UserService/UserService.Repository/UserService.Repository.csproj", "src/UserService/UserService.Repository/"]
COPY ["src/UserService/UserService.Presentation/UserService.Presentation.csproj", "src/UserService/UserService.Presentation/"]
COPY ["src/UserService/UserService.LoggerService/UserService.LoggerService.csproj", "src/UserService/UserService.LoggerService/"]
RUN dotnet restore "src/UserService/UserService/UserService.csproj"
COPY . .
WORKDIR "/src/src/UserService/UserService"
RUN dotnet build "UserService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "UserService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserService.dll"]
