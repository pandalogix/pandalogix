FROM mcr.microsoft.com/mssql/server:2017-latest-ubuntu
COPY ./create-db.sql  ./create-account.sql ./create-pad.sql ./
EXPOSE 1433
ENV SA_PASSWORD=yourStrong!Password
ENV ACCEPT_EULA=Y
RUN (/opt/mssql/bin/sqlservr &)  | grep -q "Service Broker manager has started" \
 && /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 'yourStrong!Password' -d master -i create-db.sql \
 && /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 'yourStrong!Password'  -i create-account.sql \
 && /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 'yourStrong!Password' -i create-pad.sql
