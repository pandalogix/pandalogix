FROM microsoft/mssql-server-linux
COPY ./create-db.sql  ./create-account.sql ./create-pad.sql  ./setup.sh ./entrypoint.sh ./
RUN chmod +x ./setup.sh
RUN chmod +x ./entrypoint.sh
EXPOSE 1433
ENV SA_PASSWORD=yourStrong!Password
ENV ACCEPT_EULA=Y
RUN (/opt/mssql/bin/sqlservr &)  | grep -q "Service Broker manager has started" \
 && /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 'yourStrong!Password' -d master -i create-db.sql \
 && /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 'yourStrong!Password'  -i create-account.sql \
 && /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 'yourStrong!Password' -i create-pad.sql