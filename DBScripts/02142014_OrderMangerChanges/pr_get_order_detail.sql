 Alter PROCEDURE [dbo].[pr_get_order_detail]                  
@orderId int          
-- [pr_get_order_detail] 184                
AS                        
                        
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED                        
                        
SELECT OrderId, CustomerId, FullPriceSubTotal, SubTotal, Tax, ShippingCost, RushShippingCost,     
Email, Total, CreatedDate, CreditCardType, CreditCardName, CreditCardNumber, CreditCardCSC,    
CreditCardExpired, ISNULL(IpAddress, '') AS   IpAddress, DisCountCode, DiscountAmount, AuthorizationCode, TransactionCode, v.Title AS 'Version', FullPriceTax  
, OrderStatusId, ISNULL(o.AdditionalShippingCharge,0) as AdditionalShippingCharge, b.Title As OrderStatus  
FROM [Order] o (NOLOCK)
INNER JOIN [orderStatusType] b ON o.OrderStatusId = b.StatusId   
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




   
    