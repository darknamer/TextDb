FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS Build
WORKDIR /app
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /out ./TextDb/TextDb.csproj

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-bionic AS Runtime
WORKDIR /app
COPY --from=Build /out  .
ENTRYPOINT ["dotnet", "TextDb.dll"]
