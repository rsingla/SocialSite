# SocialSite

Step 1: To build a new project use

dotnet new console -o SocialSite -f net7.0

Step 2: Using Azure Data studio on Mac for connecting to local database


To Start a new Database on your local machine - Use below instruction

1. Install Docker:
MacOS: https://docs.docker.com/desktop/install/mac-install/
Linux: https://docs.docker.com/desktop/install/linux-install/

2. Increase RAM allocation for Docker

--I would suggest increasing to 4GB

3. Create and Run a container for the Image: (Change your username and password as you like)
docker run -e "ACCEPT_EULA=1" -e "MSSQL_USER=SA" -e "MSSQL_SA_PASSWORD=SQLConnect1" -e "MSSQL_PID=Developer" -p 1433:1433 -d --name=sql_connect mcr.microsoft.com/azure-sql-edge

4. Check if the container is running
docker container ls -a

5. Stop and Start Container
docker stop sql_connect
docker start sql_connect


Step 3: Connecting the code to SQL

dotnet add package Dapper
dotnet add package microsoft.data.sqlclient
dotnet add package microsoft.entityframeworkcore
dotnet add package microsoft.entityframeworkcore.sqlserver
dotnet add package microsoft.entityframeworkcore.relational
dotnet add package microsoft.Extensions.Configuration
dotnet add package microsoft.Extensions.Configuration.JSON
dotnet add package Newtonsoft.Json


dotnet restore


To Start a webapplication 

dotnet new webapi --name SocialSite

To test run 
dotnet run 

Take the url in the 
http://localhost:5288/swagger/index.html

curl -X 'GET' \
  'http://localhost:5288/WeatherForecast' \
  -H 'accept: text/plain'