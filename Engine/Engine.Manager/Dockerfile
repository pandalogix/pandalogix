FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine3.14  AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine3.14  AS build
WORKDIR /src
COPY Engine/Engine.Manager/Engine.Manager.csproj Engine/Engine.Manager/
COPY Common/EventBus/EventBus.csproj Common/EventBus/
COPY Engine/Engine.Contract/Engine.Contract.csproj Engine/Engine.Contract/
COPY Engine/Engine.Core/Engine.Core.csproj Engine/Engine.Core/
RUN dotnet restore Engine/Engine.Manager/Engine.Manager.csproj
COPY . .
WORKDIR /src/Engine/Engine.Manager
RUN dotnet build Engine.Manager.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Engine.Manager.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Engine.Manager.dll"]
