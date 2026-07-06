FROM ://microsoft.com AS build
WORKDIR /src
COPY ["IglesiaZaorAPI.csproj", "./"]
RUN dotnet restore "IglesiaZaorAPI.csproj"
COPY . .
RUN dotnet publish "IglesiaZaorAPI.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000
ENTRYPOINT ["dotnet", "IglesiaZaorAPI.dll"]
