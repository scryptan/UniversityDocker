FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["UniversityDocker/UniversityDocker.csproj", "UniversityDocker/"]
RUN dotnet restore "UniversityDocker/UniversityDocker.csproj"
COPY . .
WORKDIR "/src/UniversityDocker"
RUN dotnet build "UniversityDocker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UniversityDocker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UniversityDocker.dll"]
