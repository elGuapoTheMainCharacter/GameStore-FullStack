# Use the SDK image to build the app
FROM ://microsoft.com AS build-env
WORKDIR /App

# Copy all files from the current folder (where your .csproj is)
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Use the runtime image to run the app
FROM ://microsoft.com
WORKDIR /App
COPY --from=build-env /App/out .

# IMPORTANT: Ensure 'GameStore.Api.dll' matches your .csproj filename exactly
ENTRYPOINT ["dotnet", "GameStore.Api.dll"]
