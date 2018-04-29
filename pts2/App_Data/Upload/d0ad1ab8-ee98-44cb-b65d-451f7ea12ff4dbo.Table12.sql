CREATE TABLE [dbo].[Table]
(
	[Id] BIGINT NOT NULL PRIMARY KEY, 
    [firstname] NVARCHAR(50) NOT NULL, 
    [lastname] NVARCHAR(50) NULL, 
    [companyname] NVARCHAR(50) NOT NULL, 
    [address1] NVARCHAR(50) NOT NULL, 
    [address2] NVARCHAR(50) NULL, 
    [contact1] VARCHAR(50) NOT NULL, 
    [contact2] VARCHAR(50) NULL, 
    [email_id] NVARCHAR(50) NOT NULL, 
    [vat_no] NVARCHAR(50) NULL
)
