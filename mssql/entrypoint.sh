#!/usr/bin/env bash
set -euo pipefail

/opt/mssql/bin/sqlservr &
sqlservr_pid=$!

trap 'kill "$sqlservr_pid" 2>/dev/null || true' SIGINT SIGTERM

echo "Waiting for SQL Server to accept connections..."
until /opt/mssql-tools18/bin/sqlcmd \
    -S localhost \
    -U sa \
    -P "$MSSQL_SA_PASSWORD" \
    -C \
    -Q "SELECT 1" >/dev/null 2>&1; do
    sleep 1
done

echo "SQL Server is ready; applying initialization script."
/opt/mssql-tools18/bin/sqlcmd \
    -S localhost \
    -U sa \
    -P "$MSSQL_SA_PASSWORD" \
    -C \
    -b \
    -i /docker-entrypoint-initdb.d/init_sql.sql

wait "$sqlservr_pid"