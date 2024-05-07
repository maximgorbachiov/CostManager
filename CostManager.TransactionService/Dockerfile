#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ./CostManager.TransactionService.Abstracts ./CostManager.TransactionService.Abstracts
COPY ./CostManager.TransactionService.API ./CostManager.TransactionService.API
COPY ./CostManager.TransactionService.DB ./CostManager.TransactionService.DB
RUN dotnet build "CostManager.TransactionService.API/CostManager.TransactionService.API.csproj" -c Release -o /app/build
WORKDIR /src/CostManager.TransactionService.API

FROM build AS publish
RUN dotnet publish "CostManager.TransactionService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CostManager.TransactionService.API.dll"]