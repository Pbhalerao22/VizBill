# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the csproj from the subfolder
COPY ["VizBill/VizBill.csproj", "VizBill/"]
RUN dotnet restore "VizBill/VizBill.csproj"

# Copy everything and build
COPY . .
WORKDIR "/src/VizBill"
RUN dotnet publish "VizBill.csproj" -c Release -o /app

# Run stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "VizBill.dll"]
