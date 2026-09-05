FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /source

COPY global.json Directory.Build.props Directory.Packages.props CleanArchitecture.sln ./
COPY src/Domain/CleanArchitecture.Domain.csproj src/Domain/
COPY src/Application/CleanArchitecture.Application.csproj src/Application/
COPY src/Infrastructure/CleanArchitecture.Infrastructure.csproj src/Infrastructure/
COPY src/Api/CleanArchitecture.Api.csproj src/Api/
COPY src/Domain/packages.lock.json src/Domain/
COPY src/Application/packages.lock.json src/Application/
COPY src/Infrastructure/packages.lock.json src/Infrastructure/
COPY src/Api/packages.lock.json src/Api/
RUN dotnet restore src/Api/CleanArchitecture.Api.csproj --locked-mode

COPY src/ src/
RUN dotnet publish src/Api/CleanArchitecture.Api.csproj \
    --configuration Release \
    --no-restore \
    --output /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_HTTP_PORTS=8080
ENV ConnectionStrings__DefaultConnection="Data Source=/data/architecture.db"
EXPOSE 8080
VOLUME ["/data"]

USER $APP_UID
ENTRYPOINT ["dotnet", "CleanArchitecture.Api.dll"]
