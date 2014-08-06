Alter PROCEDURE [dbo].[pr_get_order_transaction]                     
@startDate datetime=null,                      
@endDate datetime=null,    
@fieldId int=0,    
@includeArchiveData bit=0         
-- pr_get_order_transaction  '6/1/2011', '11/30/2014', 1, 0                   
AS                          
  
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED        
  
SELECT a.OrderId, b.Title As OrderStatus, AuthorizationCode, TransactionCode, Charge, TransactionDate, OrderStatusId,      
  Total, ISNULL(c.FieldValue,'') AS Affiliate           
FROM [Order] a       
INNER JOIN [orderStatusType] b ON a.OrderStatusId = b.StatusId     
LEFT JOIN OrderCustomField c On c.OrderId = a.OrderId AND c.FieldId = @fieldId           
WHERE CreatedDate>= @startDate AND CreatedDate <=@endDate        
AND a.OrderStatusId <> 1            
ORDER BY CreatedDate DESC       