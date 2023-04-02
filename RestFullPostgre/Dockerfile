#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src
COPY ["RestFullPostgre/RestFullPostgre.csproj", "RestFullPostgre/"]
RUN dotnet restore "RestFullPostgre/RestFullPostgre.csproj"
COPY . .
WORKDIR "/src/RestFullPostgre"
RUN dotnet build "RestFullPostgre.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RestFullPostgre.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RestFullPostgre.dll"]