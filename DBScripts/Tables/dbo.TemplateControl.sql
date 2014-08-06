USE [CSBaseECommerce]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/* Drop if already exists */
IF EXISTS (SELECT 1 FROM sys.objects WHERE [type] = 'U' AND [name] = 'TemplateControl') BEGIN
	DROP TABLE [dbo].[TemplateControl]
END

GO

/*

Table Name: TemplateControl 

Description: Stores template control information.

History:
Date		User		Change
1/8/2013	jzaman		Creation

*/

CREATE TABLE [dbo].[TemplateControl] (
	[TemplateControlId] [int] IDENTITY(1,1) NOT NULL,
	[TemplateId] [int] NOT NULL,
	[StateId] [int] NULL,
	[DisableTemplate] bit NULL,	
	[CreateDate] [datetime] NOT NULL DEFAULT(GETDATE())
	
CONSTRAINT [PK_TemplateControl] PRIMARY KEY CLUSTERED 
(
	[TemplateControlId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[TemplateControl]  WITH CHECK ADD  CONSTRAINT [TemplateControl_Template_FK1] FOREIGN KEY([TemplateId])
REFERENCES [dbo].[Template] ([TemplateId])

GO
