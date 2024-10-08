# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY CampusCuisine/*.csproj ./CampusCuisine/
COPY CampusCuisineUnitTests/*.csproj ./CampusCuisineUnitTests/
COPY CampusCuisineIntegrationTests/*.csproj ./CampusCuisineIntegrationTests/
RUN dotnet restore

# copy everything else and build app
COPY CampusCuisine/. ./CampusCuisine/
WORKDIR /source/CampusCuisine
RUN dotnet publish -c release -o /app

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "CampusCuisine.dll"]