ARG BUILD_ID

# Retrieve and set base image layer
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS base
ARG BUILD_ID
ARG BUILD_VERSION
WORKDIR /app
RUN apk add curl

# Create build image for runtime
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app
COPY ./CASAdapter.sln .
COPY ./api ./api
COPY ./api/api.csproj ./api/Api.csproj
COPY ./client ./client/
COPY ./client/client.csproj ./client/Client.csproj
COPY ./Model ./Model
COPY ./test ./test
COPY ./test/test.csproj ./test/Test.csproj

# Build and publish a release
RUN dotnet publish -c Release -o /app/publish

# Build runtime image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://*:8080
ENTRYPOINT ["dotnet", "Api.dll"]
