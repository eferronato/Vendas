#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Cadastros.API/Cadastros.API.csproj", "Cadastros.API/"]
COPY ["Vendas.Infrastructure.Cross/Vendas.Infrastructure.Cross.csproj", "Vendas.Infrastructure.Cross/"]
COPY ["Vendas.Infrastructure/Vendas.Infrastructure.csproj", "Vendas.Infrastructure/"]
COPY ["Vendas.Domain/Vendas.Domain.csproj", "Vendas.Domain/"]
RUN dotnet restore "Cadastros.API/Cadastros.API.csproj"
COPY . .
WORKDIR "/src/Cadastros.API"
RUN dotnet build "Cadastros.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cadastros.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cadastros.API.dll"]
