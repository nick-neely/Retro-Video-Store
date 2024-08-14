# Stage 1: Build the project
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["RetroVideoStore.csproj", "RetroVideoStore/"]
RUN dotnet restore "RetroVideoStore.csproj"
COPY . .
WORKDIR "/src/RetroVideoStore"
RUN dotnet build "RetroVideoStore.csproj" -c Release -o /app/build

# Stage 2: Publish the project
FROM build AS publish
RUN dotnet publish "RetroVideoStore.csproj" -c Release -o /app/publish

# Stage 3: Set up the runtime environment and run the app
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Apply migrations
ENTRYPOINT ["dotnet", "RetroVideoStore.dll"]
CMD ["dotnet", "ef", "database", "update"]
