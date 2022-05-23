#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Precos.API/Precos.API.csproj", "Precos.API/"]
COPY ["Vendas.Domain/Vendas.Domain.csproj", "Vendas.Domain/"]
COPY ["Vendas.Infrastructure.Cross/Vendas.Infrastructure.Cross.csproj", "Vendas.Infrastructure.Cross/"]
COPY ["Vendas.Infrastructure/Vendas.Infrastructure.csproj", "Vendas.Infrastructure/"]
RUN dotnet restore "Precos.API/Precos.API.csproj"
COPY . .
WORKDIR "/src/Precos.API"
RUN dotnet build "Precos.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Precos.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Precos.API.dll"]
