#!/bin/bash
set -e

echo "Waiting for SQL Server to be up..."
/wait-for-it.sh sqlserver:1433 -t 60

echo "Applying EF Core migrations..."
dotnet ef database update --project "./Infrastructure/Infrastructure.csproj" \
                         --startup-project "./Presentation/Presentation.csproj"

echo "Starting the app..."
exec dotnet Presentation.dll 