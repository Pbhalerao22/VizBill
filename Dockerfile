# Build stage
FROM :/mcr.microsoft.com AS build
WORKDIR /src
COPY ["VizBille.csproj", "./"]
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o /app

# Run stage
FROM :/mcr.microsoft.com
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "VizBille.dll"]
