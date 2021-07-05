#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["FrontEnds/Course.Web/Course.Web.csproj", "FrontEnds/Course.Web/"]
COPY ["Course.Shared/Course.Shared.csproj", "Course.Shared/"]
RUN dotnet restore "FrontEnds/Course.Web/Course.Web.csproj"
COPY . .
WORKDIR "/src/FrontEnds/Course.Web"
RUN dotnet build "Course.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Course.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Course.Web.dll"]