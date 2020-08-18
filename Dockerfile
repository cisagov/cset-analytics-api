#========================================
# BUILD DOTNET
#========================================
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-dotnet

# Copy  files to image
COPY ./src/ /app
WORKDIR /app

# Build Application
RUN dotnet publish -c Release -o out

#========================================
# RUNTIME
#========================================
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime

# Copy artifacts from dotnet build
COPY --from=build-dotnet /app/out /app

# Copy artifacts from angular build
COPY --from=build-ng /app/dist/CsetAnalytics /app/wwwroot/

# Start API
WORKDIR /app
ENTRYPOINT dotnet CsetAnalytics.Api.dll