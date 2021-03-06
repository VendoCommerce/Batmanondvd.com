USE [CSBaseECommerce]
GO
/****** Object:  StoredProcedure [dbo].[pr_update_template]    Script Date: 01/08/2013 17:12:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* Drop if already exists */
IF EXISTS (SELECT 1 FROM sys.objects WHERE [type] = 'P' AND [name] = 'pr_update_template') BEGIN
	DROP PROCEDURE [dbo].[pr_update_template]
END

GO

/*

Name: pr_update_template

Description: updates template.
 
History:
Date		User		Change
?			?			Creation.
1/8/2013	jzaman		support for template control added.

Example:

*/

CREATE PROC [dbo].[pr_update_template]         
@templateId int,       
@title varchar(200),      
@body varchar(max),      
@script varchar(max),
@tag xml,  
@expireDate  datetime,  
@templateItemXML xml,
@uriLabel varchar(200)
AS  
   
SET QUOTED_IDENTIFIER ON        
        
IF @templateId =0      
BEGIN      
 INSERT INTO Template(Name, Body, Script, Tag, [ExpireDate], Visible, URILabel, CreateDate)      
 SELECT @title, @body, @script, @tag, @expireDate, 1, @uriLabel, GETDATE()
       
 SELECT @templateId = SCOPE_IDENTITY()     
      
END    
BEGIN    
 UPDATE Template    
 SET Name = @title, Body = @body, Script = @script, Tag= @tag, URILabel = @uriLabel, [ExpireDate] = @expireDate    
 WHERE TemplateId = @templateId    
     
 DELETE FROM  TemplateItems WHERE TemplateId = @templateId    
 
 DELETE FROM  TemplateControl WHERE TemplateId = @templateId    
END    
    
INSERT INTO TemplateItems(TemplateId, SkuId, TypeId)        
select  @templateId, x.n.value('@skuId', 'int'),        
x.n.value('@typeId', 'int')       
from @templateItemXML.nodes('//item') as x(n) 
 
INSERT INTO dbo.[TemplateControl] (TemplateId, StateId, DisableTemplate)
SELECT @templateId,
x.n.value('@stateId', 'int'),
x.n.value('@disableTemplate', 'bit')
FROM @templateItemXML.nodes('//controlItem') as x(n) 