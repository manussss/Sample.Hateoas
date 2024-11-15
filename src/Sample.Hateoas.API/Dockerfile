FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Sample.Hateoas.API/Sample.Hateoas.API.csproj", "Sample.Hateoas.API/"]
RUN dotnet restore "./Sample.Hateoas.API/Sample.Hateoas.API.csproj"
COPY . .
WORKDIR "/src/Sample.Hateoas.API"
RUN dotnet build "./Sample.Hateoas.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Sample.Hateoas.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sample.Hateoas.API.dll"]