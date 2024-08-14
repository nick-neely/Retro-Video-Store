# Stage 1: Build the project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["RetroVideoStore.csproj", "./"]
RUN dotnet restore "./RetroVideoStore.csproj"

# Copy the rest of the source code and build the project
COPY . .
RUN dotnet publish "RetroVideoStore.csproj" -c Release -o /app/publish

# Stage 2: Apply migrations and run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Apply migrations
RUN dotnet ef database update --no-build

# Run the application
ENTRYPOINT ["dotnet", "RetroVideoStore.dll"]
