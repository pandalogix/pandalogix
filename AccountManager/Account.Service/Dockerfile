FROM mcr.microsoft.com/dotnet/core/runtime:2.2 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /src
COPY AccountManager/Account.Service/Account.Service.csproj AccountManager/Account.Service/
RUN dotnet restore AccountManager/Account.Service/Account.Service.csproj
COPY . .
WORKDIR /src/AccountManager/Account.Service
RUN dotnet build Account.Service.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Account.Service.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Account.Service.dll"]
