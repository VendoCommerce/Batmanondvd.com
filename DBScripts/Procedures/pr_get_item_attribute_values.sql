USE [CSBaseECommerce]
GO
/****** Object:  StoredProcedure [dbo].[pr_get_item_attribute_values]    Script Date: 01/02/2013 18:45:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* Drop if already exists */
IF EXISTS (SELECT 1 FROM sys.objects WHERE [type] = 'P' AND [name] = 'pr_get_item_attribute_values') BEGIN
	DROP PROCEDURE [dbo].[pr_get_item_attribute_values]
END

GO

/*

Name: pr_get_item_attribute_values

Description: Gets all attribute values of given object and item.
 
History:
Date		User		Change
7/2/2012	jzaman		Creation
1/2/2013	jzaman		Added nolock

*/

CREATE PROCEDURE [dbo].[pr_get_item_attribute_values]
	@ObjectName nvarchar(50),
	@ObjectItemID [int]
AS
BEGIN

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED 
SET NOCOUNT ON;
		
	DECLARE @Sql NVARCHAR(500)
	DECLARE @TableName NVARCHAR(50)
	DECLARE @ColName NVARCHAR(50)
	
	SELECT TOP 1 @TableName = ValuesTableName, @ColName = PrimaryKeyColName FROM dbo.[Objects] WHERE Name = @ObjectName
		
	SET @Sql = N'		
	SELECT a.Name AS ''AttributeName'', oav.Value 
	FROM dbo.[' + @TableName + N'] oav WITH(NOLOCK)
	INNER JOIN dbo.[ObjectAttributes] oa 
	ON oav.ObjectAttributeID = oa.ObjectAttributeID	
    INNER JOIN dbo.[Attributes] a
    ON oa.AttributeID = a.AttributeID
    INNER JOIN dbo.[Objects] o
    ON oa.ObjectID = o.ObjectID
    INNER JOIN dbo.[ValueTypes] vt
    ON a.DefaultValueTypeID = vt.ValueTypeID    
    WHERE o.Name = @p1  
    AND oav.' + @ColName + N' = @p2'
    
    EXECUTE sp_executesql @Sql, N'@p1 nvarchar(50), @p2 INT', @p1 = @ObjectName, @p2 = @ObjectItemID;
END
