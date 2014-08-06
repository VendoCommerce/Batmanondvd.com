Alter PROCEDURE pr_get_resource  
AS  
  
SELECT ResourceId, [Key], Culture, Value  
FROM [resource]  
ORDER BY ResourceId