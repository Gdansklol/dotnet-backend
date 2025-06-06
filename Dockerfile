# Base image with .NET SDK
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy and restore
COPY *.sln .
COPY dotnet-backend.csproj ./
RUN dotnet restore dotnet-backend.csproj

# Copy and build
COPY . .
RUN dotnet publish dotnet-backend.csproj -c Release -o /app/publish

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "dotnet-backend.dll"]
