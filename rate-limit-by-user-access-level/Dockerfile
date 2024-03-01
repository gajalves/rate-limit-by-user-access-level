#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["rate-limit-by-user-access-level/rate-limit-by-user-access-level.csproj", "rate-limit-by-user-access-level/"]
RUN dotnet restore "rate-limit-by-user-access-level/rate-limit-by-user-access-level.csproj"
COPY . .
WORKDIR "/src/rate-limit-by-user-access-level"
RUN dotnet build "rate-limit-by-user-access-level.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "rate-limit-by-user-access-level.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "rate-limit-by-user-access-level.dll"]