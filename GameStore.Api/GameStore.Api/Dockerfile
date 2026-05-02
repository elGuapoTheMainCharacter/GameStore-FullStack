# Build stage (SDK image)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build-env
WORKDIR /App

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Runtime stage (lighter image)
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /App

COPY --from=build-env /App/out .

ENTRYPOINT ["dotnet", "GameStore.Api.dll"]
