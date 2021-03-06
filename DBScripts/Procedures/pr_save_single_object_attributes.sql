USE [CSBaseECommerce]
GO
/****** Object:  StoredProcedure [dbo].[pr_save_object_attribute]    Script Date: 12/28/2012 09:17:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* Drop if already exists */
IF EXISTS (SELECT 1 FROM sys.objects WHERE [type] = 'P' AND [name] = 'pr_save_object_attribute') BEGIN
	DROP PROCEDURE [dbo].[pr_save_object_attribute]
END

GO

/*

Name: pr_save_object_attribute

Description: save attribute's association to object.
 
History:
Date		User		Change
1/7/2013	jzaman		Creation

Example:
exec [pr_save_object_attribute] 1, 3, 1, N'new', N'new2'
*/

CREATE PROCEDURE [dbo].[pr_save_object_attribute]	
	@ObjectId int,
	@AttributeId int,
	@ObjectAttributeTypeId int,
	@Description nvarchar(500),
	@DisplayLabel nvarchar(100)
AS
BEGIN

	DECLARE @OAID INT
	
	SELECT @OAID = (SELECT ObjectAttributeId FROM dbo.[ObjectAttributes] WHERE ObjectId = @ObjectId AND AttributeId = @AttributeId)
	
	IF (@OAID IS NULL)
	BEGIN
		INSERT INTO dbo.[ObjectAttributes] (ObjectId, AttributeId, ObjectAttributeTypeId, [Description], DisplayLabel)
		VALUES (@ObjectId, @AttributeId, @ObjectAttributeTypeId, @Description, @DisplayLabel)		
	END
	ELSE
	BEGIN
		UPDATE dbo.[ObjectAttributes]
		SET ObjectAttributeTypeId = @ObjectAttributeTypeId,
			[Description] = @Description,
			DisplayLabel = @DisplayLabel
		WHERE ObjectAttributeId = @OAID
	END
	
END

