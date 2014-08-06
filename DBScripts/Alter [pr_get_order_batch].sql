Alter PROCEDURE [dbo].[pr_get_order_batch]                        
@orderId int    
    
AS                              
    
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED                              
    
SELECT OrderId, o.CustomerId, FullPriceSubTotal, SubTotal, Tax, ShippingCost, RushShippingCost,           
o.Email, Total, o.CreatedDate, CreditCardType, CreditCardName, CreditCardNumber, CreditCardCSC,          
CreditCardExpired, ISNULL(IpAddress, '') AS   IpAddress, v.Title  as Version,o.AuthorizationCode,o.TransactionCode,      
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
    
    
SELECT a.OrderId, a.SkuId, Quantity, InitialAmount, TaxAmount, c.LongDescription, c.Title,c.SkuCode, c.FullPrice, c.OfferCode            
FROM [orderSku] a (NOLOCK)                
INNER JOIN [order] b (NOLOCK) ON a.OrderId = b.OrderId              
INNER JOIN [Sku] c(NOLOCK) ON c.SkuId = a.SkuId            
WHERE a.OrderId = @orderId            