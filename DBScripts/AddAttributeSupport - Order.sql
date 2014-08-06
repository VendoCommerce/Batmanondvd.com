/*

Dev: jzaman
Date: 7/11/2012
Description: Add attributes support to orders.

*/

/* Step: Change database. */ 
USE [CSBaseECommerce]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/* Step: Add the attribute values table. */ 

/* Drop if already exists */
IF EXISTS (SELECT 1 FROM sys.objects WHERE [type] = 'U' AND [name] = 'ObjectAttributeValues_Order') BEGIN /* Step: Table name change. */
	DROP TABLE [dbo].[ObjectAttributeValues_Order] /* Step: Table name change. */
END

GO

CREATE TABLE [dbo].[ObjectAttributeValues_Order] ( /* Step: Table name change. */
	[ObjectAttributeValueID] [int] IDENTITY(1,1) NOT NULL,
	[ObjectAttributeID] [int] NOT NULL,
	[OrderId] [int] NOT NULL, /* -- Step: Column name change */
	[Value] nvarchar(max) NULL,	
	[CreateDate] [datetime] NOT NULL DEFAULT(GETDATE())
		
CONSTRAINT [PK_ObjectAttributeValues_Order] PRIMARY KEY CLUSTERED  /*Step: Primary key change */
(
	[ObjectAttributeValueID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/* Step: Table name change. */
ALTER TABLE [dbo].[ObjectAttributeValues_Order]  /* Step: Table name change. */
WITH CHECK ADD  CONSTRAINT [ObjectAttributeValues_ObjectAttributes_Order_FK1] FOREIGN KEY([ObjectAttributeID]) /*Step: Foreign key change */
REFERENCES [dbo].[ObjectAttributes] ([ObjectAttributeID])

GO

ALTER TABLE [dbo].[ObjectAttributeValues_Order]  /* Step: Table name change. */
WITH CHECK ADD  CONSTRAINT [ObjectAttributeValues_ObjectAttributes_Order_FK2] FOREIGN KEY([OrderId]) /* Step: Foreign key and Column name change. */
REFERENCES [dbo].[Order] ([OrderId]) /* Step: Table and column name change. */

GO

CREATE UNIQUE INDEX IX_ObjectAttributeValue_Order /*Step: Index name change*/
ON [dbo].[ObjectAttributeValues_Order] (ObjectAttributeID, OrderId) /* Step: Table and column name change. */
    
GO

/* Step: Populate Objects table with the attribute values table information. */ 
INSERT INTO dbo.[Objects] (Name, ValuesTableName, PrimaryKeyColName) VALUES ('Order', 'ObjectAttributeValues_Order', 'OrderId')