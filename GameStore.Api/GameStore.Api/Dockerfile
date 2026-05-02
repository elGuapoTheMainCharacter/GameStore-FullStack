FROM ://microsoft.com AS build-env
WORKDIR /App

# Copy all files from your inner GameStore.Api folder
COPY . ./

# Restore and Publish
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Final image - using the lighter runtime image
FROM ://microsoft.com
WORKDIR /App
COPY --from=build-env /App/out .

# This must match your .csproj name exactly
ENTRYPOINT ["dotnet", "GameStore.Api.dll"]
