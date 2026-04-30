FROM ://microsoft.com AS build-env
WORKDIR /App

# Copy all files from the folder this Dockerfile is in
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM ://microsoft.com
WORKDIR /App
COPY --from=build-env /App/out .
# Double-check that your file is named GameStore.Api.csproj
ENTRYPOINT ["dotnet", "GameStore.Api.dll"]
