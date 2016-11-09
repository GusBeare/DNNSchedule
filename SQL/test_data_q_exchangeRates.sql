DELETE FROM [dbo].[q_ExchangeRates]
go

INSERT INTO [dbo].[q_ExchangeRates]
           ([CurrencyCode]
           ,[Rate]
           ,[LastUpdated])
     VALUES
           ('ARS', 9.1730,GETDATE())
GO

INSERT INTO [dbo].[q_ExchangeRates]
           ([CurrencyCode]
           ,[Rate]
           ,[LastUpdated])
     VALUES
           ('MXN',16.2659,GETDATE())
GO

INSERT INTO [dbo].[q_ExchangeRates]
           ([CurrencyCode]
           ,[Rate]
           ,[LastUpdated])
     VALUES
           ('PEN',3.1850,GETDATE())
GO
INSERT INTO [dbo].[q_ExchangeRates]
           ([CurrencyCode]
           ,[Rate]
           ,[LastUpdated])
     VALUES
           ('BRL',3.3572,GETDATE())
GO

INSERT INTO [dbo].[q_ExchangeRates]
           ([CurrencyCode]
           ,[Rate]
           ,[LastUpdated])
     VALUES
           ('CLP',665.8327,GETDATE())
GO

INSERT INTO [dbo].[q_ExchangeRates]
           ([CurrencyCode]
           ,[Rate]
           ,[LastUpdated])
     VALUES
           ('COP',2854.1366,GETDATE())
GO

INSERT INTO [dbo].[q_ExchangeRates]
           ([CurrencyCode]
           ,[Rate]
           ,[LastUpdated])
     VALUES
           ('EUR',1.10,GETDATE())
GO
