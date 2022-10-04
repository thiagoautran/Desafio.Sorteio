#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DoorPrize.MVC/DoorPrize.MVC.csproj", "DoorPrize.MVC/"]
COPY ["DoorPrize.ApplicationCore/DoorPrize.ApplicationCore.csproj", "DoorPrize.ApplicationCore/"]
COPY ["DoorPrize.Infrastructure/DoorPrize.Infrastructure.csproj", "DoorPrize.Infrastructure/"]
RUN dotnet restore "DoorPrize.MVC/DoorPrize.MVC.csproj"
COPY . .
WORKDIR "/src/DoorPrize.MVC"
RUN dotnet build "DoorPrize.MVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DoorPrize.MVC.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DoorPrize.MVC.dll"]