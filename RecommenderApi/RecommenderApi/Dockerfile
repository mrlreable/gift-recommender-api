FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY .. ./
RUN dotnet restore RecommenderApi/RecommenderApi.Api.csproj
RUN dotnet restore RecommenderApi.Common/RecommenderApi.Common.csproj
RUN cd RecommenderApi
RUN dotnet publish -c Release -o /app/publish RecommenderApi/RecommenderApi.Api.csproj

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS final-env
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "RecommenderApi.Api.dll"]