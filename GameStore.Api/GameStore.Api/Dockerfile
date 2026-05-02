FROM ://microsoft.com AS build-env
WORKDIR /App

# 1. Copy everything from the current directory
COPY . ./

# 2. Restore and Build
RUN dotnet restore
RUN dotnet publish -c Release -o out

# 3. Use the runtime image
FROM ://microsoft.com
WORKDIR /App
COPY --from=build-env /App/out .

# 4. This command tells Docker to run the DLL that matches your project name
# (Replace 'GameStore.Api.dll' ONLY if your .csproj has a different name)
ENTRYPOINT ["dotnet", "GameStore.Api.dll"]
