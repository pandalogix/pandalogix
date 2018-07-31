
FROM microsoft/dotnet:2.1-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY . .
RUN dotnet restore 
WORKDIR /src/Engine/Engine.Service/
RUN dotnet build Engine.Service.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Engine.Service.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Engine/Engine.Service/Engine.Service.dll"]
