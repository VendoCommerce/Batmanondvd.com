USE [CSBaseECommerce]
GO

/****** Object:  StoredProcedure [dbo].[pr_update_template]    Script Date: 07/09/2012 15:45:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER TABLE [dbo].[Template]
ADD [Script] varchar(max) NULL

GO

ALTER TABLE [dbo].[Template]
ADD [URILabel] varchar(max) NULL

GO
