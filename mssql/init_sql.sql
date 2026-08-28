/**
 * Create new user for example.
 */

IF NOT EXISTS (SELECT 1 FROM sys.server_principals WHERE name = 'dotnet2025')
BEGIN
  CREATE LOGIN dotnet2025
  WITH PASSWORD = '3dsfeFe#0$a3ff3f';
END;
GO

/**
 * Create and set database.
 */

IF DB_ID('TestDB') IS NULL
BEGIN
  CREATE DATABASE TestDB;
END;
GO

SELECT name FROM sys.databases;
USE TestDB;
GO

/**
 * Create table after check.
 */
DROP TABLE IF EXISTS Example;
GO

CREATE TABLE Example (
  id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
  text VARCHAR(45) NOT NULL
);
GO

/**
 * Modify the table.
 */

ALTER TABLE Example ADD more_text VARCHAR(45);
GO

SET IDENTITY_INSERT Example ON;
GO

/**
 * Insert values into table.
 */

INSERT INTO Example (id, text, more_text) VALUES (0, 'text', 'more text'), (1, 'text', 'more text'), (2, 'text', 'more text'), (3, 'text', 'more text');
GO

SELECT * FROM Example;
GO