FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app
EXPOSE 5201
EXPOSE 7201

COPY ["./src", "./"]

RUN dotnet publish "Presentation/Api/Api.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app/
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Api.dll"]