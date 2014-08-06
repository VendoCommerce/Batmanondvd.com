  
/*  
  
Name: pr_get_all_attributes  
  
Description: Gets all attributes.  
   
History:  
Date  User  Change  
1/2/2012 jzaman  Creation. 
10/2/2013 rgosai  Update. 
  
*/  
  
Alter PROCEDURE [dbo].[pr_get_all_attributes]   
AS  
BEGIN  
  
SET NOCOUNT ON;  
    
 SELECT a.AttributeId, a.Name, a.[Description], vt.Name AS 'ValueTypeName', vt.ValueTypeId  
 FROM dbo.[Attributes] a  
 INNER JOIN dbo.[ValueTypes] vt  
 ON a.DefaultValueTypeId = vt.ValueTypeId  
 ORDER BY a.AttributeId  
END  