#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Web.API.CategoryService/CategoryService.csproj", "CategoryService/"]
RUN dotnet restore "./CategoryService/CategoryService.csproj"
COPY . .
WORKDIR "/src/CategoryService"
RUN dotnet build "CategoryService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CategoryService.csproj" -c Release -o /app/publish /p:UseAppHost=false --self-contained --runtime linux-64

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CategoryService.dll"]