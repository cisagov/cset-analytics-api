#========================================
# BUILD DOTNET
#========================================
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-dotnet

WORKDIR /app

COPY ./src/ .

RUN dotnet publish -c Release -o out

RUN wget https://s3.amazonaws.com/rds-downloads/rds-combined-ca-bundle.pem

#========================================
# RUNTIME
#========================================
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime

WORKDIR /app

COPY --from=build-dotnet /app/out .
COPY --from=build-dotnet /app/rds-combined-ca-bundle.pem .

COPY ./etc/entrypoint.sh /app/entrypoint.sh
RUN chmod a+x /app/entrypoint.sh

# ENTRYPOINT ["dotnet", "CsetAnalytics.Api.dll"]

ENTRYPOINT ["/app/entrypoint.sh"]
