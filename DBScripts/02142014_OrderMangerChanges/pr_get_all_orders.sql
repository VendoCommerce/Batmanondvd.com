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
AdditionalShippingCharge MONEY,
OrderStatus NVARCHAR(100),
orderStatusId INT,)      
      
INSERT INTO #temp(OrderId, CustomerId, FullPriceSubTotal, SubTotal,Tax, ShippingCost, FirstName, LastName, Email, Total, CreatedDate,FullPriceTax, AdditionalShippingCharge, OrderStatus,orderStatusId)                        
SELECT OrderId, o.CustomerId, FullPriceSubTotal, SubTotal,Tax, ShippingCost, c.FirstName, c.LastName, o.Email, Total, o.CreatedDate, FullPriceTax, AdditionalShippingCharge, b.Title As OrderStatus, o.OrderStatusId      
FROM [Order] o
INNER JOIN [orderStatusType] b ON o.OrderStatusId = b.StatusId           
LEFT JOIN [Customer] c       
ON o.CustomerId = c.CustomerId      
WHERE o.CreatedDate >= @startDate AND o.CreatedDate <= @endDate      
AND (@firstName='' OR (c.FirstName LIKE '%' + @firstName + '%'))      
AND (@lastName='' OR (c.LastName LIKE '%' + @lastName + '%'))      
AND (@email='' OR (o.Email LIKE '%' + @email + '%'))       
ORDER BY o.CreatedDate DESC         
      
IF @includeArchivedata = 1            
      
BEGIN        
 INSERT INTO #temp(OrderId, CustomerId, FullPriceSubTotal, SubTotal,Tax, ShippingCost, FirstName, LastName, Email, Total, CreatedDate,FullPriceTax, AdditionalShippingCharge, OrderStatus,orderStatusId)                            
 SELECT OrderId, o.CustomerId, FullPriceSubTotal, SubTotal,Tax, ShippingCost, c.FirstName, c.LastName, o.Email, Total, o.CreatedDate, FullPriceTax, AdditionalShippingCharge , b.Title As OrderStatus, o.OrderStatusId      
 FROM [OrderArchive] o 
 INNER JOIN [orderStatusType] b ON o.OrderStatusId = b.StatusId                 
 LEFT JOIN [Customer] c      
 ON o.CustomerId = c.CustomerId      
 WHERE o.CreatedDate >= @startDate AND o.CreatedDate <= @endDate      
 AND (@firstName='' OR (c.FirstName LIKE '%' + @firstName + '%'))      
 AND (@lastName='' OR (c.LastName LIKE '%' + @lastName + '%'))      
 AND (@email='' OR (o.Email LIKE '%' + @email + '%'))          
 ORDER BY o.CreatedDate DESC         
END        
      
      
SELECT @totalCount = COUNT(1) FROM   #temp          
      
SELECT OrderId, CustomerId, FullPriceSubTotal, SubTotal,Tax, ShippingCost, FirstName, LastName, Email, Total, CreatedDate ,FullPriceTax, AdditionalShippingCharge , OrderStatus, orderStatusId         
FROM #temp            
WHERE seq BETWEEN @startRec AND @endRec          