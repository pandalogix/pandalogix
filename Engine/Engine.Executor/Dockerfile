FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine3.14  AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine3.14  AS build
WORKDIR /src
COPY Engine/Engine.Executor/Engine.Executor.csproj Engine/Engine.Executor/
COPY Common/EventBus/EventBus.csproj Common/EventBus/
COPY Engine/Engine.Contract/Engine.Contract.csproj Engine/Engine.Contract/
COPY Engine/Engine.Core/Engine.Core.csproj Engine/Engine.Core/
RUN dotnet restore Engine/Engine.Executor/Engine.Executor.csproj
COPY . .
WORKDIR /src/Engine/Engine.Executor
RUN dotnet build Engine.Executor.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Engine.Executor.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Engine.Executor.dll"]
