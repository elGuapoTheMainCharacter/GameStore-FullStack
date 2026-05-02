FROM ://microsoft.com AS build-env
WORKDIR /App

# Copy everything from the inner GameStore.Api folder
COPY . ./

# Restore and Publish
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Build runtime image
FROM ://microsoft.com
WORKDIR /App
COPY --from=build-env /App/out .

# This must match your .csproj filename exactly
ENTRYPOINT ["dotnet", "GameStore.Api.dll"]
