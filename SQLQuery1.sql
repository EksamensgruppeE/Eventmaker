CREATE DATABASE EventDB

CREATE TABLE Event
(
	Id INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
	DateTime DATETIME NOT NULL,
	Name NVARCHAR(50) NOT NULL,
	Place NVARCHAR(150) NOT NULL,
	Description NVARCHAR(350) NOT NULL
	
)

INSERT INTO dbo.Event
(
    
    DateTime,
    Name,
    Place,
    Description
)
VALUES
(   
    '2019-03-20', -- DateTime - datetime
    N'Fodbold turnering',       -- Name - nvarchar(50)
    N'I hallen',       -- Place - nvarchar(150)
    N'Kæmpe turnering, vær der før klokken 13'        -- Description - nvarchar(350)
    )