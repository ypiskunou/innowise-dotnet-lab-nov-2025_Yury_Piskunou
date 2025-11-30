FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/ProductService/ProductService/ProductService.csproj", "src/ProductService/ProductService/"]
COPY ["src/ProductService/ProductService.Entities/ProductService.Entities.csproj", "src/ProductService/ProductService.Entities/"]
COPY ["src/ProductService/ProductService.Repository/ProductService.Repository.csproj", "src/ProductService/ProductService.Repository/"]
COPY ["src/ProductService/ProductService.Contracts/ProductService.Contracts.csproj", "src/ProductService/ProductService.Contracts/"]
COPY ["src/ProductService/ProductService.Shared/ProductService.Shared.csproj", "src/ProductService/ProductService.Shared/"]
COPY ["src/ProductService/ProductService.Presentation/ProductService.Presentation.csproj", "src/ProductService/ProductService.Presentation/"]
COPY ["src/ProductService/ProductService.Application/ProductService.Application.csproj", "src/ProductService/ProductService.Application/"]
COPY ["src/ProductService/ProductService.LoggerService/ProductService.LoggerService.csproj", "src/ProductService/ProductService.LoggerService/"]
RUN dotnet restore "src/ProductService/ProductService/ProductService.csproj"
COPY . .
WORKDIR "/src/src/ProductService/ProductService"
RUN dotnet build "ProductService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ProductService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductService.dll"]
