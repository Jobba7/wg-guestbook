#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#FROM mcr.microsoft.com/dotnet/aspnet:7.0.4-bullseye-slim-arm64v8 AS base
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["WG.Guestbook.Web/WG.Guestbook.Web.csproj", "WG.Guestbook.Web/"]
RUN dotnet restore "WG.Guestbook.Web/WG.Guestbook.Web.csproj"
COPY . .
WORKDIR "/src/WG.Guestbook.Web"
RUN dotnet build "WG.Guestbook.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WG.Guestbook.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WG.Guestbook.Web.dll"]