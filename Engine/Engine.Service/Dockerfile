FROM mcr.microsoft.com/dotnet/core/runtime:2.2 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /src
COPY Engine/Engine.Service/Engine.Service.csproj Engine/Engine.Service/
COPY Common/EventBus/EventBus.csproj Common/EventBus/
COPY Engine/Engine.Contract/Engine.Contract.csproj Engine/Engine.Contract/
COPY Engine/Engine.Core/Engine.Core.csproj Engine/Engine.Core/
RUN dotnet restore Engine/Engine.Service/Engine.Service.csproj
COPY . .
WORKDIR /src/Engine/Engine.Service
RUN dotnet build Engine.Service.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Engine.Service.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Engine.Service.dll"]
