#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Catalog/Course.Services.Catalog/Course.Services.Catalog.csproj", "Services/Catalog/Course.Services.Catalog/"]
COPY ["Course.Shared/Course.Shared.csproj", "Course.Shared/"]
RUN dotnet restore "Services/Catalog/Course.Services.Catalog/Course.Services.Catalog.csproj"
COPY . .
WORKDIR "/src/Services/Catalog/Course.Services.Catalog"
RUN dotnet build "Course.Services.Catalog.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Course.Services.Catalog.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Course.Services.Catalog.dll"]