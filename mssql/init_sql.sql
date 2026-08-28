/**
 * Create new user for example.
 */

CREATE LOGIN dotnet2025
WITH PASSWORD = '3dsfeFe#0$a3ff3f';
GO

/**
 * Create and set database.
 */

CREATE DATABASE TestDB;
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
  text VARCHAR(45) NOT NULL,
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

INSERT INTO Example (id, text, more_text) VALUES (0, "text", "more text"), (1, "text", "more text"), (2, "text", "more text"), (3, "text", "more text");
GO

SELECT * FROM Example;
GO