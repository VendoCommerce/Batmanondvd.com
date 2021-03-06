USE [CSBaseECommerce]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*

CS Base Cart Changes - Version 1.3.0

*/

SET NOCOUNT ON

/************************************************************************************************************/
/************************************************************************************************************/
/* Create db object back-up table */
/************************************************************************************************************/
/************************************************************************************************************/


/* Drop if already exists */
IF EXISTS (SELECT 1 FROM sys.objects WHERE [type] = 'U' AND [name] = '_BackupObjects') BEGIN
	DROP TABLE [dbo].[_BackupObjects]
END

GO

/*

Table Name: _BackupObjects 

Description: Stores objects' body/content for tracking changes.

History:
Date		User		Change
2/8/2013	jzaman		Creation

Notes:

Object types & details: http://msdn.microsoft.com/en-us/library/ms190324.aspx

*/

CREATE TABLE [dbo].[_BackupObjects] (
	[BackupObjectId] [int] IDENTITY(1,1) NOT NULL,	
	[Name] nvarchar(100) NOT NULL,
	[Type] nvarchar(2) NOT NULL,
	[Script] nvarchar(max) NOT NULL,	
	[PreVersion] nvarchar(50) NULL,
	[Comments] nvarchar(1024) NULL,
	[CreateDate] [datetime] NOT NULL DEFAULT(GETDATE()),
	[ModifyDate] [datetime] NULL	
CONSTRAINT [PK_BackupObjectId] PRIMARY KEY CLUSTERED 
(
	[BackupObjectId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


/************************************************************************************************************/
/************************************************************************************************************/
/* Back-up current objects to file */
/************************************************************************************************************/
/************************************************************************************************************/


DECLARE @Date DATE
DECLARE @ScriptFileLocation VARCHAR(100)
DECLARE @ObjectsTable TABLE (name nvarchar(500), [type] nvarchar(2))
DECLARE @ObjectName nvarchar(500)
DECLARE @ObjectType nvarchar(2)
DECLARE @ObjectDef TABLE(line VARCHAR(MAX))
DECLARE @ObjectText VARCHAR(MAX)
DECLARE @CSCartVersion varchar(10)

SET @CSCartVersion = '1.3.0'

-- Enter names of objects (tables, procs) here. Type 'P' for procs. Supports only procs at this time.
INSERT INTO @ObjectsTable (name, [type]) VALUES ('pr_remove_shippingRegion', 'P')
INSERT INTO @ObjectsTable (name, [type]) VALUES ('pr_get_all_orders', 'P')
INSERT INTO @ObjectsTable (name, [type]) VALUES ('pr_get_order', 'P')
INSERT INTO @ObjectsTable (name, [type]) VALUES ('pr_get_order_batchprocess', 'P')
INSERT INTO @ObjectsTable (name, [type]) VALUES ('pr_get_order_detail', 'P')
INSERT INTO @ObjectsTable (name, [type]) VALUES ('pr_update_order', 'P')
INSERT INTO @ObjectsTable (name, [type]) VALUES ('pr_update_ordertotal', 'P')
INSERT INTO @ObjectsTable (name, [type]) VALUES ('pr_get_order_batch', 'P')

DECLARE ObjectsCursor CURSOR FOR SELECT name, [type] FROM @ObjectsTable
OPEN ObjectsCursor
FETCH NEXT FROM ObjectsCursor INTO @ObjectName, @ObjectType
WHILE @@FETCH_STATUS = 0
BEGIN
	
	IF EXISTS (SELECT 1 FROM sys.objects WHERE name = @ObjectName AND [type] = @ObjectType)
	BEGIN
		DELETE FROM @ObjectDef
		SET @ObjectText = ''
	
		INSERT INTO @ObjectDef EXEC sp_helptext @ObjectName
		
		SELECT  @ObjectText = COALESCE(@ObjectText + ' ' + line, line)
		FROM    @ObjectDef	
		
		INSERT INTO dbo.[_BackupObjects] (Name, [type], Script, PreVersion) VALUES (@ObjectName, @ObjectType, @ObjectText, @CSCartVersion)
	END
	ELSE	
	BEGIN
		DECLARE @ErrorMessage NVARCHAR(4000)
		SELECT @ErrorMessage = 'Object name and type not found in sys.objects table: ' + @ObjectName + ', ' + @ObjectType
		RAISERROR (@ErrorMessage, 16, 1);
		
	END
	
	FETCH NEXT FROM ObjectsCursor INTO @ObjectName, @ObjectType
END

CLOSE ObjectsCursor
DEALLOCATE ObjectsCursor
	
/************************************************************************************************************/
/************************************************************************************************************/
/* 
Alter SitePref table to add CSCartVersion column
*/
/************************************************************************************************************/
/************************************************************************************************************/

if exists(select * from sys.columns 
            where Name = 'CSCartVersion' and Object_ID = Object_ID('SitePref'))    
begin
	ALTER TABLE dbo.[SitePref]
	DROP COLUMN [CSCartVersion] 
end

ALTER TABLE dbo.[SitePref]
ADD [CSCartVersion] nvarchar(50) NULL

GO

/************************************************************************************************************/
/************************************************************************************************************/
/* Alter pr_remove_shippingRegion */
/************************************************************************************************************/
/************************************************************************************************************/

/* Drop if already exists */
IF EXISTS (SELECT 1 FROM sys.objects WHERE [type] = 'P' AND [name] = 'pr_remove_shippingRegion') BEGIN
	DROP PROCEDURE [dbo].[pr_remove_shippingRegion]
END

GO

/****** Object:  StoredProcedure [dbo].[pr_remove_shippingRegion]    Script Date: 02/04/2013 15:19:37 ******/
CREATE PROCEDURE [dbo].[pr_remove_shippingRegion]  
@prefId int  
AS  
  
DELETE FROM SkuShipping WHERE PrefId=@prefId

DELETE FROM ShippingOrderValue WHERE PrefId=@prefId

DELETE FROM ShippingRegion WHERE PrefId=@prefId

DELETE FROM ShippingPref WHERE PrefId=@prefId

GO

/************************************************************************************************************/
/************************************************************************************************************/
/* Create [ShippingCharges] */
/************************************************************************************************************/
/************************************************************************************************************/

/* Drop if already exists */
IF EXISTS (SELECT 1 FROM sys.objects WHERE [type] = 'U' AND [name] = 'ShippingCharges') BEGIN
	DROP TABLE [dbo].[ShippingCharges]
END

GO

/*

Table Name: ShippingCharges 

Description: Stores additional shipping charge info.

History:
Date		User		Change
1/28/2013	jzaman		Creation

*/

CREATE TABLE [dbo].[ShippingCharges] (
	[ShippingChargeId] [int] IDENTITY(1,1) NOT NULL,	
	[PrefId] [int] NOT NULL,
	[Key] nvarchar(32) NOT NULL,
	[Cost] [money] NOT NULL,	
	[FriendlyLabel] nvarchar(50) NULL,
	[CreateDate] [datetime] NOT NULL DEFAULT(GETDATE())
	
CONSTRAINT [PK_ShippingChargeId] PRIMARY KEY CLUSTERED 
(
	[ShippingChargeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ShippingCharges]  WITH CHECK ADD  CONSTRAINT [ShippingCharges_ShippingPref_FK1] FOREIGN KEY([PrefId])
REFERENCES [dbo].[shippingpref] ([PrefId])

GO

CREATE UNIQUE NONCLUSTERED INDEX IX_Key ON dbo.[ShippingCharges] ([PrefId], [Key]) ON [PRIMARY]

GO

/************************************************************************************************************/
/************************************************************************************************************/
/* Create pr_remove_shipping_charge */
/************************************************************************************************************/
/************************************************************************************************************/

/* Drop if already exists */
IF EXISTS (SELECT 1 FROM sys.objects WHERE [type] = 'P' AND [name] = 'pr_remove_shipping_charge') BEGIN
	DROP PROCEDURE [dbo].[pr_remove_shipping_charge]
END

GO

/*

Name: pr_remove_shipping_charge

Description: Deletes shipping charge.
 
History:
Date		User		Change
2/4/2013	jzaman		Creation

exec pr_remove_shipping_charge 2
*/

CREATE PROCEDURE [dbo].[pr_remove_shipping_charge]
	@ShippingChargeId int
AS
BEGIN

SET NOCOUNT ON;
		
	DELETE FROM dbo.[ShippingCharges]
	WHERE ShippingChargeId = @ShippingChargeId

END

GO

/************************************************************************************************************/
/************************************************************************************************************/
/* 
Add "AdditionaShippingCharge" column to order and orderarchive table 
and updated related procs
*/
/************************************************************************************************************/
/************************************************************************************************************/
ALTER TABLE dbo.[Order]
DROP COLUMN [Total] 

GO

-- Add column

if exists(select * from sys.columns 
            where Name = 'AdditionalShippingCharge' and Object_ID = Object_ID('Order'))    
begin
	ALTER TABLE dbo.[Order]
	DROP COLUMN [AdditionalShippingCharge] 
end

ALTER TABLE dbo.[Order]
ADD AdditionalShippingCharge MONEY NULL

GO

if exists(select * from sys.columns 
            where Name = 'AdditionalShippingCharge' and Object_ID = Object_ID('OrderArchive'))    
begin
	ALTER TABLE dbo.[OrderArchive]
	DROP COLUMN [AdditionalShippingCharge] 
end

ALTER TABLE dbo.[OrderArchive]
ADD AdditionalShippingCharge MONEY NULL

GO

-- Alter order table's Total column


ALTER TABLE dbo.[Order]
ADD [Total]  AS (((([SubTotal]+[ShippingCost])+[RushShippingCost]+ISNULL([AdditionalShippingCharge], 0))+[Tax])-[DiscountAmount]) PERSISTED

GO

-- Alter [pr_get_all_orders]

ALTER PROCEDURE [dbo].[pr_get_all_orders]             
@startDate datetime=null,              
@endDate datetime=null,   
@includeArchivedata bit =0,     
@startRec int,          
@endRec int,

@firstName nvarchar(100)='',       
@lastName nvarchar(100)='',      
@email nvarchar(250)='',  

@totalCount int = 0 OUTPUT     
AS                  

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED          

CREATE TABLE #temp(
seq int IDENTITY(1,1), 
OrderId INT, 
customerId INT, 
FullPriceSubTotal MONEY, 
SubTotal MONEY, 
Tax MONEY,
ShippingCost MONEY, 
FirstName NVARCHAR(100),
LastName NVARCHAR(100),
Email NVARCHAR(600), 
Total MONEY, 
CreatedDate SMALLDATETIME, 
FullPriceTax MONEY,
AdditionalShippingCharge MONEY)

INSERT INTO #temp(OrderId, CustomerId, FullPriceSubTotal, SubTotal,Tax, ShippingCost, FirstName, LastName, Email, Total, CreatedDate,FullPriceTax, AdditionalShippingCharge)                  
SELECT OrderId, o.CustomerId, FullPriceSubTotal, SubTotal,Tax, ShippingCost, c.FirstName, c.LastName, o.Email, Total, o.CreatedDate, FullPriceTax, AdditionalShippingCharge
FROM [Order] o 
LEFT JOIN [Customer] c 
ON o.CustomerId = c.CustomerId
WHERE o.CreatedDate >= @startDate AND o.CreatedDate <= @endDate
AND (@firstName='' OR (c.FirstName LIKE '%' + @firstName + '%'))
AND (@lastName='' OR (c.LastName LIKE '%' + @lastName + '%'))
AND (@email='' OR (o.Email LIKE '%' + @email + '%')) 
ORDER BY o.CreatedDate DESC   

IF @includeArchivedata = 1      

BEGIN  
	INSERT INTO #temp(OrderId, CustomerId, FullPriceSubTotal, SubTotal,Tax, ShippingCost, FirstName, LastName, Email, Total, CreatedDate,FullPriceTax, AdditionalShippingCharge)                     
	SELECT OrderId, o.CustomerId, FullPriceSubTotal, SubTotal,Tax, ShippingCost, c.FirstName, c.LastName, o.Email, Total, o.CreatedDate, FullPriceTax, AdditionalShippingCharge
	FROM [OrderArchive] o 
	LEFT JOIN [Customer] c
	ON o.CustomerId = c.CustomerId
	WHERE o.CreatedDate >= @startDate AND o.CreatedDate <= @endDate
	AND (@firstName='' OR (c.FirstName LIKE '%' + @firstName + '%'))
	AND (@lastName='' OR (c.LastName LIKE '%' + @lastName + '%'))
	AND (@email='' OR (o.Email LIKE '%' + @email + '%'))    
 ORDER BY o.CreatedDate DESC   
END  


SELECT @totalCount = COUNT(1) FROM   #temp    

SELECT OrderId, CustomerId, FullPriceSubTotal, SubTotal,Tax, ShippingCost, FirstName, LastName, Email, Total, CreatedDate ,FullPriceTax, AdditionalShippingCharge    
FROM #temp      
WHERE seq BETWEEN @startRec AND @endRec    


GO

-- Alter pr_pet_order

ALTER PROCEDURE [dbo].[pr_get_order]         
@orderId int            
AS              
              
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED              

SELECT OrderId, CustomerId, FullPriceSubTotal, SubTotal, Tax, ShippingCost, RushShippingCost, Email, Total, CreatedDate, FullPriceTax, AdditionalShippingCharge           
FROM [Order] (NOLOCK)         
WHERE OrderId =  @orderId

GO

-- Alter [pr_get_order_batchprocess]

-- Add additional shipping charge to proc return manually
  
 -- Alter [pr_get_order_detail]
 
 ALTER PROCEDURE [dbo].[pr_get_order_detail]                
@orderId int        
-- [pr_get_order_detail] 184              
AS                      
                      
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED                      
                      
SELECT OrderId, CustomerId, FullPriceSubTotal, SubTotal, Tax, ShippingCost, RushShippingCost,   
Email, Total, CreatedDate, CreditCardType, CreditCardName, CreditCardNumber, CreditCardCSC,  
CreditCardExpired, ISNULL(IpAddress, '') AS   IpAddress, DisCountCode, DiscountAmount, AuthorizationCode, TransactionCode, v.Title AS 'Version', FullPriceTax
, OrderStatusId, ISNULL(o.AdditionalShippingCharge,0) as AdditionalShippingCharge
FROM [Order] o (NOLOCK)
LEFT JOIN dbo.[Version] v (NOLOCK)
ON o.VersionId = v.VersionID
WHERE OrderId =  @orderId        
      
SELECT a.OrderId, a.SkuId, Quantity, InitialAmount, TaxAmount, c.LongDescription, c.Title,c.SkuCode, c.FullPrice, c.OfferCode, ISNULL(b.AdditionalShippingCharge,0) as AdditionalShippingCharge
FROM [orderSku] a (NOLOCK)        
INNER JOIN [order] b (NOLOCK) ON a.OrderId = b.OrderId      
INNER JOIN [Sku] c(NOLOCK) ON c.SkuId = a.SkuId      
WHERE a.OrderId =  @orderId   
  
SELECT a.FieldId, a.FieldValue, b.FieldName   
FROM OrderCustomField a (NOLOCK)     
INNER JOIN CustomField b   (NOLOCK) ON a.FieldId = b.FieldId  
WHERE a.OrderId =  @orderId   
  
GO
  
-- Alter [pr_update_order]

/*

Name: pr_update_order

Description: Insert or updates order.
 
History:
Date		User		Change
?			?			Creation
7/11/2012	jzaman		Added attributes support

*/

ALTER PROC [dbo].[pr_update_order]   
 @orderId int =0,                        
 @orderxml xml,                
 @requestParam ntext      
 -- [pr_update_order] 0, '', ''               
AS                    

BEGIN
  
SET XACT_ABORT ON;

BEGIN TRY
	BEGIN TRANSACTION
		  
		DECLARE @ObjectName NVARCHAR(50)
		DECLARE @AttributeValuesXml XML
		
		IF @orderId = 0  
		BEGIN             
		  
		  INSERT INTO [Order](CustomerId, Email, TaxSubTotal, FullPriceSubTotal, SubTotal, Tax, ShippingCost, RushShippingCost, AdditionalShippingCharge, DiscountAmount, DiscountCode,           
		  OrderStatusId, CreditCardType, CreditCardName, CreditCardNumber, CreditCardExpired, CreditCardCSC, VersionId, RequestParam, CreatedDate, ModifyDate,    
		  IpAddress,FullPriceTax)                      
		  SELECT    x.n.value('@CustomerId', 'int'),                        
					x.n.value('@Email', 'nvarchar(100)'),             
					x.n.value('@TaxSubTotal', 'money'),              
					x.n.value('@FullPriceSubTotal', 'money'),                       
					x.n.value('@SubTotal', 'money'),                        
					x.n.value('@Tax', 'money'),                     
					x.n.value('@ShippingCost', 'money'),                  
					x.n.value('@RushShippingCost', 'money'),   
					x.n.value('@AdditionalShippingCharge', 'money'),
					x.n.value('@DiscountAmount', 'money'), 
					x.n.value('@DiscountCode', 'varchar(100)'),
					x.n.value('@OrderStatusId', 'int'),                    
					x.n.value('@CreditCardType', 'int'),                       
					x.n.value('@CreditCardName', 'nvarchar(50)'),                  
					x.n.value('@CreditCardNumber', 'nvarchar(100)'),                        
					x.n.value('@CreditCardExpired', 'nvarchar(20)'),                    
					x.n.value('@CreditCardCSC', 'int'),             
					x.n.value('@VersionId', 'int'),                    
					@requestParam,                      
					GETDATE(),                  
					GETDATE(),    
		  x.n.value('@IpAddress', 'nvarchar(100)'),
		  x.n.value('@FullPriceTax', 'money')                
			FROM @orderxml.nodes('//order') as x(n)                     
		  
		 SELECT @orderId = SCOPE_IDENTITY()                    
		  
		  INSERT INTO [OrderSku](OrderId, SkuId, Quantity, InitialAmount, FullAmount, TaxAmount, IsUpSell)                      
		  SELECT  @orderId,                        
							x.n.value('@SkuId', 'int'),                      
							x.n.value('@Quantity', 'int'),                        
							x.n.value('@InitialAmount', 'money'),                
							x.n.value('@FullAmount', 'money'),            
							x.n.value('@TaxAmount', 'money'),      
							x.n.value('@IsUpsell', 'bit')                     
			FROM @orderxml.nodes('//Items') as x(n)                     
		  
		  INSERT INTO OrderCustomField(OrderId, FieldId, FieldValue)                      
		  SELECT  @orderId,                        
			x.n.value('@filedId', 'int'),                      
			x.n.value('@fieldValue', 'nvarchar(1000)')                  
			FROM @orderxml.nodes('//customfield') as x(n)   
		  
		END  
		ELSE  
		BEGIN  
		  
		 --Sri  Quick: we may need to extend if the cart allows to modify the sku info  
		 UPDATE [order]  
			SET Email =  x.n.value('@Email', 'nvarchar(100)'),       
				TaxSubTotal = x.n.value('@TaxSubTotal', 'money'),     
				FullPriceSubTotal = x.n.value('@FullPriceSubTotal', 'money'),       
				SubTotal =  x.n.value('@SubTotal', 'money'),        
				Tax = x.n.value('@Tax', 'money'),    
				ShippingCost =  x.n.value('@ShippingCost', 'money'),   
				RushShippingCost = x.n.value('@RushShippingCost', 'money'),    
				AdditionalShippingCharge = x.n.value('@AdditionalShippingCharge', 'money'),    
				CreditCardType =  x.n.value('@CreditCardType', 'int'),   
				CreditCardName = x.n.value('@CreditCardName', 'nvarchar(50)'),   
				CreditCardNumber =  x.n.value('@CreditCardNumber', 'nvarchar(100)'),    
				CreditCardExpired =  x.n.value('@CreditCardExpired', 'nvarchar(20)'),   
				CreditCardCSC = x.n.value('@CreditCardCSC', 'int'),   
				DiscountAmount = x.n.value('@DiscountAmount', 'money'), 
				DiscountCode = 	x.n.value('@DiscountCode', 'varchar(100)'),
				ModifyDate  = GETDATE(),               
				FullPriceTax = x.n.value('@FullPriceTax', 'money')		
			FROM @orderxml.nodes('//order') as x(n)  
		 WHERE orderId = @orderId  
		  
		END                    
		
		-- insert attributes

		SELECT @ObjectName = (SELECT x.n.value('.', 'nvarchar(50)') FROM @orderxml.nodes('//order/@ObjectName') as x(n))
		SELECT @AttributeValuesXml = (SELECT @orderxml.query('//AttributeValues'))
		EXEC dbo.[pr_save_attribute_values] @ObjectName, @OrderId, @AttributeValuesXml

		SELECT @orderId as OrderId

	COMMIT TRANSACTION
END TRY
BEGIN CATCH	
	
	IF (XACT_STATE()) <> 0 OR @@TRANCOUNT > 0 
		 ROLLBACK TRANSACTION;
		
	DECLARE @ErrorMessage NVARCHAR(4000);
    DECLARE @ErrorSeverity INT;
    DECLARE @ErrorState INT;
    SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();    
	RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
	
END CATCH

END

GO

-- Alter [pr_update_ordertotal]

Alter PROC [dbo].[pr_update_ordertotal]    
@orderId int,                   
 @orderxml xml           
AS              
              
              
UPDATE [order]    
SET TaxSubTotal =   x.n.value('@TaxSubTotal', 'money'), 
FullPriceSubTotal =  x.n.value('@FullPriceSubTotal', 'money'),     
SubTotal= x.n.value('@SubTotal', 'money'), 
Tax = x.n.value('@Tax', 'money'),    
 ShippingCost =  x.n.value('@ShippingCost', 'money'), 
 RushShippingCost = x.n.value('@RushShippingCost', 'money'), 
 AdditionalShippingCharge = x.n.value('@AdditionalShippingCharge', 'money'),
 FullPriceTax =  x.n.value('@FullPriceTax', 'money')  
FROM @orderxml.nodes('//order') as x(n)      
WHERE OrderId = @orderId    
              
DELETE FROM  [OrderSku] WHERE OrderId = @orderId     
              
 INSERT INTO [OrderSku](OrderId, SkuId, Quantity, InitialAmount, FullAmount, TaxAmount, IsUpSell)                
 SELECT  @orderId,                  
      x.n.value('@SkuId', 'int'),                
   x.n.value('@Quantity', 'int'),                  
   x.n.value('@InitialAmount', 'money'),          
    x.n.value('@FullAmount', 'money'),      
     x.n.value('@TaxAmount', 'money'),  
      x.n.value('@IsUpsell', 'bit')                
   FROM @orderxml.nodes('//Items') as x(n)               

GO

-- Alter [pr_get_order_batch]

ALTER PROCEDURE [dbo].[pr_get_order_batch]                          
@orderId int      
      
AS                                
      
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED                                
      
SELECT OrderId, o.CustomerId, FullPriceSubTotal, SubTotal, Tax, ShippingCost, RushShippingCost,             
o.Email, Total, o.CreatedDate, CreditCardType, CreditCardName, CreditCardNumber, CreditCardCSC,            
CreditCardExpired, ISNULL(IpAddress, '') AS   IpAddress, v.Title  as Version,o.AuthorizationCode,o.TransactionCode, ISNULL(o.AdditionalShippingCharge,0) as AdditionalShippingCharge,
FullPriceTax,      
bill.Company AS BillingCompany,        
bill.FirstName AS BillingFirstName,        
bill.LastName AS BillingLastName,        
bill.Address1 AS BillingAddress1,        
bill.Address2 AS BillingAddress2,        
bill.City AS BillingCity,        
bill.ZipPostalCode AS BillingZipPostalCode,        
bill.PhoneNumber AS BillingPhoneNumber,        
bill.FaxNumber AS BillingFaxNumber,        
bill.StateProvince AS BillingStateProvince,        
bill.CountryId AS BillingCountryId,       
ship.Company AS ShippingCompany,        
ship.FirstName AS ShippingFirstName,        
ship.LastName AS ShippingLastName,        
ship.Address1 AS ShippingAddress1,        
ship.Address2 AS ShippingAddress2,        
ship.City AS ShippingCity,        
ship.ZipPostalCode AS ShippingZipPostalCode,        
ship.PhoneNumber AS ShippingPhoneNumber,        
ship.FaxNumber AS ShippingFaxNumber,        
ship.StateProvince AS ShippingStateProvince,        
ship.CountryId AS ShippingCountryId      
FROM [Order] o        
inner join Customer c on o.CustomerId = c.CustomerId        
left join Version v on o.VersionId = v.VersionID        
left join Address bill on c.BillingAddressId = bill.AddressId        
left join Address ship on c.ShippingAddressId = ship.AddressId        
WHERE OrderId = @orderId                          
      
      
SELECT a.OrderId, a.SkuId, Quantity, InitialAmount, TaxAmount, c.LongDescription, c.Title,c.SkuCode, c.FullPrice, c.OfferCode, ISNULL(b.AdditionalShippingCharge,0) as AdditionalShippingCharge
FROM [orderSku] a (NOLOCK)                  
INNER JOIN [order] b (NOLOCK) ON a.OrderId = b.OrderId                
INNER JOIN [Sku] c(NOLOCK) ON c.SkuId = a.SkuId              
WHERE a.OrderId = @orderId 


SELECT OCF.FieldId,OCF.FieldValue,CF.FieldName 
FROM OrderCustomField (NOLOCK) ocf 
INNER JOIN [order] o (NOLOCK) ON o.OrderId = ocf.OrderId                
INNER JOIN CustomField cf on ocf.FieldId=cf.FieldId
WHERE ocf.OrderId = @orderId 

GO

/************************************************************************************************************/
/************************************************************************************************************/
/* Set cart version */
/************************************************************************************************************/
/************************************************************************************************************/

DECLARE @CSCartVersion varchar(10)

SET @CSCartVersion = '1.3.0'

UPDATE dbo.[SitePref]
SET [CSCartVersion] = @CSCartVersion
WHERE [PrefId] = 1

GO

/*

End

*/