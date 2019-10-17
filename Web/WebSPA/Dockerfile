FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-alpine3.9 AS base
WORKDIR /app
EXPOSE 5000


FROM mcr.microsoft.com/dotnet/core/sdk:3.0-alpine3.9 AS build
WORKDIR /src
#setup node
ENV NODE_VERSION 10.16.0
ENV NODE_DOWNLOAD_SHA 2e2cddf805112bd0b5769290bf2d1bc4bdd55ee44327e826fa94c459835a9d9a
ARG NPMAUTHTOKEN=''
ENV FONTAWESOME_NPM_AUTH_TOKEN=${NPMAUTHTOKEN}

RUN apk update && apk upgrade && \
    apk add --no-cache fftw-dev gcc g++ make libc6-compat && \
    apk add --no-cache python && \
    apk add --update --no-cache --repository http://dl-3.alpinelinux.org/alpine/edge/community --repository http://dl-3.alpinelinux.org/alpine/edge/main vips-dev &&\
    apk add --no-cache nodejs npm bash git yarn openssh &&\
    npm install --global gatsby --no-optional gatsby-cli &&\
    rm -rf /var/cache/apk/*

RUN echo $FONTAWESOME_NPM_AUTH_TOKEN

COPY Web/WebSPA/WebSPA.csproj Web/WebSPA/
RUN dotnet restore Web/WebSPA/WebSPA.csproj
COPY . .
# WORKDIR /src/Web/WebSPA/client
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
