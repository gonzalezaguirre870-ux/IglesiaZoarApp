FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["IglesiaZoarAPI.csproj", "./"]
RUN dotnet restore "IglesiaZoarAPI.csproj"
COPY . .
RUN dotnet publish "IglesiaZoarAPI.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000
ENTRYPOINT ["dotnet", "IglesiaZoarAPI.dll"]
