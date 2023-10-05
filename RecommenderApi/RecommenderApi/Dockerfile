FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS final-env
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "RecommenderApi.dll"]