FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-alpine3.9 AS base
WORKDIR /app
EXPOSE 80


FROM mcr.microsoft.com/dotnet/core/sdk:2.2-alpine3.9 AS build
WORKDIR /src
#setup node
ENV NODE_VERSION 10.16.0
ENV NODE_DOWNLOAD_SHA 2e2cddf805112bd0b5769290bf2d1bc4bdd55ee44327e826fa94c459835a9d9a
ARG NPMAUTHTOKEN=''
ENV FONTAWESOME_NPM_AUTH_TOKEN=${NPMAUTHTOKEN}

RUN apk --no-cache add nodejs npm


COPY Web/WebSPA/WebSPA.csproj Web/WebSPA/
RUN dotnet restore Web/WebSPA/WebSPA.csproj
COPY . .
# WORKDIR /src/Web/WebSPA/ClientApp
# RUN  npm install && npm run build
WORKDIR /src/Web/WebSPA
#update npmrc file

RUN dotnet build WebSPA.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish WebSPA.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebSPA.dll"]
