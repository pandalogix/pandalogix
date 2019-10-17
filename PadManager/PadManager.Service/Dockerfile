FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-alpine3.9 AS base
WORKDIR /app
EXPOSE 3002

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-alpine3.9 AS build
WORKDIR /src
COPY PadManager/PadManager.Service/PadManager.Service.csproj PadManager/PadManager.Service/
COPY PadManager/PadManager.Core/PadManager.csproj PadManager/PadManager.Core/
RUN dotnet restore PadManager/PadManager.Service/PadManager.Service.csproj
COPY . .
WORKDIR /src/PadManager/PadManager.Service
RUN dotnet build PadManager.Service.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish PadManager.Service.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PadManager.Service.dll"]
