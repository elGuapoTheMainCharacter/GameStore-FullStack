# Use the official .NET 10 SDK image to build the app
FROM ://microsoft.com AS build-env
WORKDIR /App

# Copy everything from the current folder into the container
COPY . ./

# Restore dependencies and publish the application
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Use the official .NET 10 ASP.NET runtime image to run the app
FROM ://microsoft.com
WORKDIR /App
COPY --from=build-env /App/out .

# This must match your GameStore.Api.csproj filename exactly
ENTRYPOINT ["dotnet", "GameStore.Api.dll"]
