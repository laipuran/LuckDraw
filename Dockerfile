# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY Web/*.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY Web ./Web
COPY Shared ./Shared
WORKDIR /app/Web
RUN pwd
RUN ls
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/Web/out .
ENTRYPOINT ["dotnet", "LuckDrawWeb.dll"]