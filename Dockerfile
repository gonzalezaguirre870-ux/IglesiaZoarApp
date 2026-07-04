 # 1. Usar el entorno oficial de .NET para compilar
FROM ://microsoft.com AS build
WORKDIR /src

# Copiar el proyecto y restaurar las dependencias
COPY ["IglesiaZoarAPI.csproj", "./"]
RUN dotnet restore "./IglesiaZoarAPI.csproj"

# Copiar todo el código del motor y compilarlo
COPY . .
RUN dotnet publish "IglesiaZoarAPI.csproj" -c Release -o /app/publish

# 2. Configurar el entorno de ejecución final
FROM ://microsoft.com AS final
WORKDIR /app
COPY --from=build /app/publish .

# Configurar el puerto internacional automático para Render
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "IglesiaZoarAPI.dll"]
