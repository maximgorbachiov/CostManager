#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Web.API.CategoryService/Web.API.CategoryService.csproj", "Web.API.CategoryService/"]
RUN dotnet restore "Web.API.CategoryService/Web.API.CategoryService.csproj"
COPY . .
WORKDIR "/src/Web.API.CategoryService"
RUN dotnet build "Web.API.CategoryService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Web.API.CategoryService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.API.CategoryService.dll"]