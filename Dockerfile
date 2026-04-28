# 1. Etapa de compilación
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiamos los archivos de proyecto para restaurar dependencias
COPY ["Alojamiento.Api/Alojamiento.Api.csproj", "Alojamiento.Api/"]
COPY ["Alojamiento.Business/Alojamiento.Business.csproj", "Alojamiento.Business/"]
COPY ["Alojamiento.DataManagement/Alojamiento.DataManagement.csproj", "Alojamiento.DataManagement/"]
COPY ["Alojamiento.Domain/Alojamiento.Domain.csproj", "Alojamiento.Domain/"]

RUN dotnet restore "Alojamiento.Api/Alojamiento.Api.csproj"

# Copiamos el resto del código y compilamos
COPY . .
WORKDIR "/src/Alojamiento.Api"
RUN dotnet build "Alojamiento.Api.csproj" -c Release -o /app/build

# 2. Publicación
FROM build AS publish
RUN dotnet publish "Alojamiento.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 3. Imagen final para correr la app
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=publish /app/publish .

# Configuramos el puerto que Render espera
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "Alojamiento.Api.dll"]