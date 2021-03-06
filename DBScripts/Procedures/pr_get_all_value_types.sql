USE [CSBaseECommerce]
GO
/****** Object:  StoredProcedure [dbo].[pr_get_all_value_types]    Script Date: 01/02/2013 17:13:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* Drop if already exists */
IF EXISTS (SELECT 1 FROM sys.objects WHERE [type] = 'P' AND [name] = 'pr_get_all_value_types') BEGIN
	DROP PROCEDURE [dbo].[pr_get_all_value_types]
END

GO

/*

Name: pr_get_all_value_types

Description: Gets all value types.
 
History:
Date		User		Change
1/4/2012	jzaman		Creation.

*/

CREATE PROCEDURE [dbo].[pr_get_all_value_types]	
AS
BEGIN

SET NOCOUNT ON;
		
	SELECT vt.ValueTypeID, vt.Name, vt.SqlDbType, vt.[Description]
	FROM dbo.[ValueTypes] vt	
	ORDER BY vt.Name
END
