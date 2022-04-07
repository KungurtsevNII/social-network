FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY ["./src", "./"]

RUN dotnet publish "Presentation/Api/Api.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app/
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Api.dll"]