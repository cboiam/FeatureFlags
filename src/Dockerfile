FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /source

COPY . .

RUN dotnet publish -c release -o /app /p:EnvironmentName=${ASPNETCORE_ENVIRONMENT}

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build /app .

EXPOSE 80
ENTRYPOINT ["dotnet", "FeatureFlag.Api.dll"]