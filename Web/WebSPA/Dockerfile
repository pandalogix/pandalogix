FROM mcr.microsoft.com/dotnet/core/runtime:2.2.6 AS base
WORKDIR /app
EXPOSE 80


FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /src
#setup node
ENV NODE_VERSION 8.9.4
ENV NODE_DOWNLOAD_SHA 21fb4690e349f82d708ae766def01d7fec1b085ce1f5ab30d9bda8ee126ca8fc
ARG NPMAUTHTOKEN=''
ENV FONTAWESOME_NPM_AUTH_TOKEN=${NPMAUTHTOKEN}

RUN curl -SL "https://nodejs.org/dist/v${NODE_VERSION}/node-v${NODE_VERSION}-linux-x64.tar.gz" --output nodejs.tar.gz \
    && echo "$NODE_DOWNLOAD_SHA nodejs.tar.gz" | sha256sum -c - \
    && tar -xzf "nodejs.tar.gz" -C /usr/local --strip-components=1 \
    && rm nodejs.tar.gz \
    && ln -s /usr/local/bin/node /usr/local/bin/nodejs

COPY Web/WebSPA/WebSPA.csproj Web/WebSPA/
RUN dotnet restore Web/WebSPA/WebSPA.csproj
COPY . .
WORKDIR /src/Web/WebSPA
#update npmrc file

RUN dotnet build WebSPA.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish WebSPA.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebSPA.dll"]
