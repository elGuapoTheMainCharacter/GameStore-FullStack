FROM ://microsoft.com AS build-env
WORKDIR /App

# 1. Copy everything from the current folder (innermost GameStore.Api)
COPY . ./

# 2. Restore and Publish
RUN dotnet restore
RUN dotnet publish -c Release -o out

# 3. Build runtime image
FROM ://microsoft.com
WORKDIR /App
COPY --from=build-env /App/out .

# 4. This MUST match your .csproj filename exactly
ENTRYPOINT ["dotnet", "GameStore.Api.dll"]
