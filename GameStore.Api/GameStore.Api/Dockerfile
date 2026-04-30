FROM ://microsoft.com AS build-env
WORKDIR /App

# Copy everything from the current folder
COPY . ./

# Restore and Publish
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Final image
FROM ://microsoft.com
WORKDIR /App
COPY --from=build-env /App/out .

# Use the exact name of your project file
ENTRYPOINT ["dotnet", "GameStore.Api.dll"]
