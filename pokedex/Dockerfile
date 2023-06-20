FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["pokedex.csproj", "./"]
RUN dotnet restore "pokedex.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "pokedex.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "pokedex.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "pokedex.dll"]
