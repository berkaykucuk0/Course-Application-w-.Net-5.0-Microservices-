#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["IdentityServer/Course.IdentityServer/Course.IdentityServer.csproj", "IdentityServer/Course.IdentityServer/"]
COPY ["Course.Shared/Course.Shared.csproj", "Course.Shared/"]
RUN dotnet restore "IdentityServer/Course.IdentityServer/Course.IdentityServer.csproj"
COPY . .
WORKDIR "/src/IdentityServer/Course.IdentityServer"
RUN dotnet build "Course.IdentityServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Course.IdentityServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Course.IdentityServer.dll"]