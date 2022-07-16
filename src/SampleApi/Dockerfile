FROM mcr.microsoft.com/dotnet/sdk:6.0 as publish
WORKDIR app
COPY . .

RUN dotnet publish *.csproj -c Release -o ./published

FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime
WORKDIR app
COPY --from=publish ./app/published .
ENTRYPOINT dotnet SampleApi.dll
