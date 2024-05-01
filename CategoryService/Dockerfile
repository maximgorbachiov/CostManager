#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

# DOCKER FILE FOR GITHUB ACTIONS BUILD
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ./Abstraction.Repositories ./Abstraction.Repositories
COPY ./Business.Data ./Business.Data
COPY ./Business.Services.Abstraction ./Business.Services.Abstraction
COPY ./BusinessLogic.CategoryService ./BusinessLogic.CategoryService
COPY ./Data.Models ./Data.Models
COPY ./Implementation.Repository.InMemory ./Implementation.Repository.InMemory
COPY ./Implementation.Repository.Postgres ./Implementation.Repository.Postgres
COPY ./Mappers ./Mappers
COPY ./Web.API.CategoryService ./Web.API.CategoryService
COPY ./Web.Data.Models ./Web.Data.Models
RUN dotnet build "Web.API.CategoryService/CategoryService.csproj" -c Release -o /app/build
WORKDIR /src/Web.API.CategoryService

FROM build AS publish
RUN dotnet publish "CategoryService.csproj" -c Release -o /app/publish --self-contained --runtime linux-x64

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CategoryService.dll"]