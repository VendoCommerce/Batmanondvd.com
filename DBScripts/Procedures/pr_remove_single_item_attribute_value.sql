USE [CSBaseECommerce]
GO
/****** Object:  StoredProcedure [dbo].[pr_remove_single_item_attribute_value]    Script Date: 12/28/2012 10:57:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* Drop if already exists */
IF EXISTS (SELECT 1 FROM sys.objects WHERE [type] = 'P' AND [name] = 'pr_remove_single_item_attribute_value') BEGIN
	DROP PROCEDURE [dbo].[pr_remove_single_item_attribute_value]
END

GO

/*

Name: pr_remove_single_item_attribute_value

Description: removes attribute values (record entries) of specific object and item from the object's attribute values table.
 
History:
Date		User		Change
12/28/2012	jzaman		Creation

exec pr_remove_single_item_attribute_value 'sku', 30, '<DeleteAttributes><weight/><size/></DeleteAttributes>'
*/

CREATE PROCEDURE [dbo].[pr_remove_single_item_attribute_value]
	@ObjectName nvarchar(50),	
	@ObjectItemID [int],
	@AttributeNamesXml xml
AS
BEGIN

SET NOCOUNT ON;
		
	DECLARE @Sql nvarchar(2000)
	DECLARE @TableName nvarchar(50)
	DECLARE @ColName nvarchar(50)
	DECLARE @ObjectAttributeID INT
	
	SELECT TOP 1 @TableName = ValuesTableName, @ColName = PrimaryKeyColName 
	FROM dbo.[Objects] 
	WHERE Name = @ObjectName
	
	Set @Sql = N'	
DELETE oav
FROM dbo.[' + @TableName + N'] oav
INNER JOIN dbo.[ObjectAttributes] oa
ON oav.ObjectAttributeID = oa.ObjectAttributeID
INNER JOIN dbo.[Attributes] a
ON a.AttributeID = oa.AttributeID
INNER JOIN @x.nodes (''//DeleteAttributes/*'') as x(n)
ON x.n.value(''fn:local-name(.)'', ''nvarchar(50)'') = a.Name
WHERE oav.' + @ColName + N' = @oiID
	'	
	
	EXECUTE sp_executesql @Sql, N'@oiID int, @x xml', @oiID = @ObjectItemID, @x = @AttributeNamesXml;
END
