#========================================
# BUILD DOTNET
#========================================
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-dotnet

WORKDIR /app

COPY ./src/ .

RUN dotnet publish -c Release -o out

#========================================
# RUNTIME
#========================================
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime

WORKDIR /app

COPY --from=build-dotnet /app/out .

COPY ./etc/entrypoint.sh /app/entrypoint.sh
RUN chmod a+x /app/entrypoint.sh

# ENTRYPOINT ["dotnet", "CsetAnalytics.Api.dll"]

ENTRYPOINT ["/app/entrypoint.sh"]