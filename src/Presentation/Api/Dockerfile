FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Presentation/Api/Api.csproj", "src/Presentation/Api/"]
COPY ["src/Persistence/Persistence.Postgres.Migrations/Persistence.Postgres.Migrations.csproj", "src/Persistence/Persistence.Postgres.Migrations/"]
COPY ["src/Persistence/Persistence.Postgres/Persistence.Postgres.csproj", "src/Persistence/Persistence.Postgres/"]
COPY ["src/Persistence/Persistence.Abstractions/Persistence.Abstractions.csproj", "src/Persistence/Persistence.Abstractions/"]
COPY ["src/Application/Domain/Domain.csproj", "src/Application/Domain/"]
COPY ["src/Application/SharedKernel/SharedKernel.csproj", "src/Application/SharedKernel/"]
COPY ["src/Application/Application/Application.csproj", "src/Application/Application/"]
COPY ["src/Services/Services.Auth/Services.Auth.csproj", "src/Services/Services.Auth/"]
RUN dotnet restore "src/Presentation/Api/Api.csproj"
COPY . .
WORKDIR "src/Presentation/Api"
RUN dotnet build "Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "app/publish/Api.dll"]
