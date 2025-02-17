docker exec -it sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Strong@Passw0rd -Q "SELECT name FROM sys.databases;"      
name



// sql

docker exec -it --user root sqlserver bash
apt-get update && apt-get install -y mssql-tools

echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
source ~/.bashrc




apt-get update
apt-get install -y curl gnupg

<!-- // add Microsoft key -->
curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add -

<!-- // add Microsoft repository -->
curl https://packages.microsoft.com/config/ubuntu/22.04/prod.list > /etc/apt/sources.list.d/mssql-tools.list


apt-get update

<!-- // download the mssql-tools -->
apt-get install -y mssql-tools

<!-- check if sqlsmd was downloaded -->
/opt/mssql-tools/bin/sqlcmd -?

<!-- add sqlcmd to PATH -->
echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
source ~/.bashrc

<!-- check that sqlcmd is enabled -->
sqlcmd -?


<!-- TODO add to Dockerfile next code -->
FROM mcr.microsoft.com/mssql/server:2022-latest

RUN apt-get update && \
    apt-get install -y curl gnupg && \
    curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add - && \
    curl https://packages.microsoft.com/config/ubuntu/22.04/prod.list > /etc/apt/sources.list.d/mssql-tools.list && \
    apt-get update && \
    apt-get install -y mssql-tools

ENV PATH="$PATH:/opt/mssql-tools/bin"






docker exec -it sqlserver bash
/opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P 'Strong@Passw0rd' -N -C
