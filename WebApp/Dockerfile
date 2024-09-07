FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["RentalManager.WebApp/RentalManager.WebApp.csproj", "RentalManager.WebApp/"]
RUN dotnet restore "RentalManager.WebApp/RentalManager.WebApp.csproj"
COPY . .
WORKDIR "/src/RentalManager.WebApp"
RUN dotnet build "RentalManager.WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RentalManager.WebApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RentalManager.WebApp.dll"]
