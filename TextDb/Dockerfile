FROM unnamed22090/dotnet:sdk-3.1 AS Build
WORKDIR /app

COPY TextDb/TextDb.csproj TextDb/TextDb.csproj
COPY TextDb.sln TextDb.sln
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o /out ./TextDb/TextDb.csproj

FROM unnamed22090/dotnet:aspnet-3.1 AS Runtime
WORKDIR /app
COPY --from=Build /out  .
ENTRYPOINT ["dotnet", "TextDb.dll"]