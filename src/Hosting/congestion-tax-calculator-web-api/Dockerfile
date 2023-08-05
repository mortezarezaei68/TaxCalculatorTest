FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Hosting/congestion-tax-calculator-web-api/congestion-tax-calculator-web-api.csproj", "src/Hosting/congestion-tax-calculator-web-api/"]
COPY ["src/Infrastructure/congestion-tax-calculator-persistence-ef/congestion-tax-calculator-persistence-ef.csproj", "src/Infrastructure/congestion-tax-calculator-persistence-ef/"]
COPY ["src/Domain/congestion-tax-calculator-domain/congestion-tax-calculator-domain.csproj", "src/Domain/congestion-tax-calculator-domain/"]
COPY ["src/Domain/congestion-tax-calculator-shared-domain/congestion-tax-calculator-shared-domain.csproj", "src/Domain/congestion-tax-calculator-shared-domain/"]
COPY ["src/Interface/congestion-tax-calculator-net-core/congestion-tax-calculator-net-core.csproj", "src/Interface/congestion-tax-calculator-net-core/"]
COPY ["src/Infrastructure/congestion-tax-calculator-validation/congestion-tax-calculator-validation.csproj", "src/Infrastructure/congestion-tax-calculator-validation/"]
RUN dotnet restore "src/Hosting/congestion-tax-calculator-web-api/congestion-tax-calculator-web-api.csproj"
COPY . .
WORKDIR "/src/src/Hosting/congestion-tax-calculator-web-api"
RUN dotnet build "congestion-tax-calculator-web-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "congestion-tax-calculator-web-api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "congestion-tax-calculator-web-api.dll"]
