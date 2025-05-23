# Etapa de compilación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar los archivos del proyecto y restaurar dependencias
COPY ["UnivCtaCte.Api/*.csproj", "UnivCtaCte.Api/"]
COPY ["UnivCtaCte.Application/*.csproj", "UnivCtaCte.Application/"]
COPY ["UnivCtaCte.Domain/*.csproj", "UnivCtaCte.Domain/"]
COPY ["UnivCtaCte.Infrastructure/*.csproj", "UnivCtaCte.Infrastructure/"]

RUN dotnet restore "UnivCtaCte.Api/UnivCtaCte.Api.csproj"

# Copiar todo y compilar
COPY . .
RUN dotnet publish "UnivCtaCte.Api/UnivCtaCte.Api.csproj" -c Release -o /publish

# Etapa de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "UnivCtaCte.Api.dll"]