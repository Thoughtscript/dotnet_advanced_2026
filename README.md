# dotnet_advanced_2026

[![](https://img.shields.io/badge/ASP.NET-10.0-purple.svg)](https://learn.microsoft.com/en-us/aspnet/core/migration/90-to-100?view=aspnetcore-10.0)
[![](https://img.shields.io/badge/MSSQL-2022-blue.svg)](https://hub.docker.com/r/microsoft/mssql-server)
[![](https://img.shields.io/badge/.NET-10.0-blue.svg)](https://dotnet.microsoft.com/en-us/download/dotnet/10.0) 
[![](https://img.shields.io/badge/EntityFrameworkCoreE-10.0-blue.svg)](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/10) 
[![](https://img.shields.io/badge/Docker-blue.svg)](https://www.docker.com/) 

## Set Up and Use

### GitHub CoPilot

*This example used GitHub CoPilot with manual oversight guiding the creation of numerous Dependency Injection, Database Caching, and Docker `entrypoint.sh` examples.*

### Docker

**Docker Compose**:
```bash
docker compose up
```

> *Uses a custom `ENTRYPOINT` that waits for MSSQL readiness and automatically runs `mssql/init_sql.sql` with `sqlcmd`. The script is safe to rerun when the container restarts.* (Thanks AI!)

## Topics

### .NET CLI

```bash
dotnet new mvc --language "C#"
dotnet run

# For less detailed tests
dotnet test
```

### ASP.NET

Views and Endpoints:
* Swagger UI -> https://localhost:5177/swagger
* OpenAPI document -> https://localhost:5177/openapi/v1.json
* Simple String/Text Response -> https://localhost:5177/Example/SimpleString
* Automatic JSON Serialization -> https://localhost:5177/Example/JsonResponse
* Default Home -> https://localhost:5177/
* Prebuilt Context Path Example -> https://localhost:5177/Home/Privacy
* Asynchronous SQL Response -> https://localhost:5177/Example/SqlExamples

Interface and Dependency Injection:
* https://0.0.0.0:5177/Service/SimpleExample
* https://0.0.0.0:5177/Service/ServiceLifetime
* https://0.0.0.0:5177/Service/KeyedServices
* https://0.0.0.0:5177/Service/EnumerableServices
* https://0.0.0.0:5177/Service/ThreadSafeSingleton

### Caching

```bash
dotnet add package Microsoft.Extensions.Caching.Memory
```

Database query caching:
* `ExampleQueryService` keeps the first query result in its scoped instance as a first-level cache.
* `IMemoryCache` stores the result across scopes as a second-level cache for five minutes, with a one-minute sliding expiration.
* https://0.0.0.0:5177/Example/SqlExamples

### Tests

Tests use `asp_entity/tests/asp_entity.runsettings` for detailed per-test console output, while the xUnit configuration enables diagnostic messages and full test method names. Run them with:

```bash
# From the top-level root directory
dotnet test asp_entity/tests/asp_entity.Tests.csproj
```

### EntityFrameworkCore

Already run from within [asp_entity/src](./asp_entity/src):

```bash
# The versions should match
dotnet add package Microsoft.EntityFrameworkCore --version 10.0.11
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 10.0.11
```

If those versions mismatch:
```bash
asp_entity-1  | Using launch settings from /app/src/Properties/launchSettings.json...
asp_entity-1  | Building...
asp_entity-1  | /app/src/asp_entity.csproj : error NU1605: Warning As Error: Detected package downgrade: Microsoft.EntityFrameworkCore from 10.0.11 to 10.0.0. Reference the package directly from the project to select a different version. 
asp_entity-1  | /app/src/asp_entity.csproj : error NU1605:  asp_entity -> Microsoft.EntityFrameworkCore.SqlServer 10.0.11 -> Microsoft.EntityFrameworkCore.Relational 10.0.11 -> Microsoft.EntityFrameworkCore (>= 10.0.11) 
asp_entity-1  | /app/src/asp_entity.csproj : error NU1605:  asp_entity -> Microsoft.EntityFrameworkCore (>= 10.0.0)
asp_entity-1  | 
asp_entity-1  | The build failed. Fix the build errors and run again.
```

### MSSQL

> Exercise in MSSQL admin.

1. `docker-entrypoint-initdb.d` isn't supported within the Docker Container but I've kept the convention for familiarity's sake.
      * This: `/opt/mssql-tools18/bin/sqlcmd -U sa -P FD83wr9DF_*9pke89 -S localhost -No -i docker-entrypoint-initdb.d/init_sql.sql` is now executed using `entrypoint.sh`.
      * Since initialization doesn't happen immediately, I've added `sleep 120` to the [Bash script](./asp_entity/run.sh).
      * Some of the issues identified [here](https://github.com/Thoughtscript/dotnet_2025/blob/main/README.md) can be side-stepped on `macOS 26.6`.

The Docker Container uses Kerberos authentication which should be configured. Use the following to verify the above using an inline query and script:

```bash
/opt/mssql-tools18/bin/sqlcmd -U sa -P FD83wr9DF_*9pke89 -S localhost -No -i docker-entrypoint-initdb.d/init_sql.sql
# Must use dbo syntax inline
/opt/mssql-tools18/bin/sqlcmd -U sa -P FD83wr9DF_*9pke89 -S localhost -Q "SELECT * FROM TestDB.dbo.Example;" -C
/opt/mssql-tools18/bin/sqlcmd -U sa -P FD83wr9DF_*9pke89 -S localhost -No -i docker-entrypoint-initdb.d/verify_sql.sql
```

> https://learn.microsoft.com/en-us/sql/sql-server/?view=sql-server-ver16

## Resources and Links

1. https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection/service-lifetimes
1. https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection/guidelines