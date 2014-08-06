/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Coupon ADD
	IncludeShipping bit NULL
GO
ALTER TABLE dbo.Coupon SET (LOCK_ESCALATION = TABLE)
GO
COMMIT




ALTER PROCEDURE [dbo].[pr_get_coupon]      
@couponId int     
--pr_get_coupon 0                     
AS                      
                      
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED                      
          
SELECT CouponId, SkuId, RelatedSkuId, DiscountType,  DiscountAmount           
FROM SkuCoupon  a (NOLOCK)       
WHERE (@couponId=0 OR CouponId=  @couponId)      
                
SELECT CouponId, Title, Discount, Active, DiscountType,  CreateDate, TotalAmount,IncludeShipping           
FROM Coupon  a (NOLOCK)       
WHERE (@couponId=0 OR CouponId=  @couponId)               
ORDER BY Title   
  


ALTER PROCEDURE [dbo].[pr_update_coupon]  
@couponId int,   
@title varchar(100),  
@discount smallmoney,  
@total smallmoney,  
@discountType int,  
@skuId int,  
@relatedskuId int,   
@itemdiscounttype int,  
@itemdiscount smallmoney,
@includeShipping bit
-- pr_update_coupon 7, 'test1', 0, 0, 4, 25, 26, 2, 10  
AS  
  
IF @couponId = 0  
BEGIN  
 INSERT INTO Coupon(Title, Discount, Active, CreateDate, DiscountType, TotalAmount,IncludeShipping)  
 VALUES(@title, @discount, 1, getdate(), @discountType, @total,@includeShipping)  
   
 SET @couponId = SCOPE_IDENTITY()  
   
 IF @discountType =4  
 BEGIN  
  INSERT INTO SkuCoupon(CouponId, SkuId, RelatedSkuId, DiscountType, DiscountAmount)  
  VALUES(@couponId, @skuId, @relatedskuId, @itemdiscounttype, @itemdiscount)  
 END  
  
END  
ELSE  
BEGIN  
 UPDATE Coupon  
 SET title = @title, Discount = @discount, DiscountType = @discountType, TotalAmount = @total,IncludeShipping=@includeShipping  
 WHERE couponId = @couponId  
   
 IF @discountType =4  
 BEGIN  
  IF NOT EXISTS(SELECT TOP 1 1 FROM SkuCoupon WHERE CouponId = @couponId)  
   INSERT INTO SkuCoupon(CouponId, SkuId, RelatedSkuId, DiscountType, DiscountAmount)  
   VALUES(@couponId, @skuId, @relatedskuId, @itemdiscounttype, @itemdiscount)  
  ELSE  
   UPDATE SkuCoupon  
   SET SkuId = @skuId, RelatedSkuId = @relatedskuId, DiscountType = @itemdiscounttype, DiscountAmount = @itemdiscount  
   WHERE couponId = @couponId  
 END  
  
END  

