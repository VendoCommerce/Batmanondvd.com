  
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
 DiscountAmount = x.n.value('@DiscountAmount', 'money'), 
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
  