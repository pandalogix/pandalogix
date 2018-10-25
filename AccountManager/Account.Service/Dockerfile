FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 7794
EXPOSE 44388

FROM microsoft/dotnet:2.1-sdk AS build
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