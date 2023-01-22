FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RentalManager.WebAPI/RentalManager.WebAPI.csproj", "RentalManager.WebAPI/"]
RUN dotnet restore "RentalManager.WebAPI/RentalManager.WebAPI.csproj"
COPY . .
WORKDIR "/src/RentalManager.WebAPI"
RUN dotnet build "RentalManager.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RentalManager.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RentalManager.WebAPI.dll"]