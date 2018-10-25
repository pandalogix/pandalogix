FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 29308
EXPOSE 44327

FROM microsoft/dotnet:2.1-sdk AS build
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