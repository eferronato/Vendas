#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Notificacoes.Worker/Notificacoes.Worker.csproj", "Notificacoes.Worker/"]
RUN dotnet restore "Notificacoes.Worker/Notificacoes.Worker.csproj"
COPY . .
WORKDIR "/src/Notificacoes.Worker"
RUN dotnet build "Notificacoes.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Notificacoes.Worker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Notificacoes.Worker.dll"]
