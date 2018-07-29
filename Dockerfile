
FROM microsoft/dotnet:2.1-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY Engine/Engine.Service/Engine.Service.csproj Engine/Engine.Service/Engine.Service/
RUN dotnet restore Engine/Engine.Service/Engine.Service.csproj
WORKDIR /src/Engine/Engine.Service/Engine.Service
COPY . .
RUN dotnet build Engine.Service.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Engine.Service.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Engine/Engine.Service/Engine.Service.dll"]
