USE [MiracleSkinNow.com_m]
GO

/****** Object:  View [dbo].[v_ObjectAttributeValues_Order]    Script Date: 10/02/2012 11:16:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[v_ObjectAttributeValues_Order]
AS
SELECT     TOP (100) PERCENT dbo.ObjectAttributeValues_Order.ObjectAttributeValueID, dbo.[Order].OrderId, dbo.Attributes.Name, dbo.ObjectAttributeValues_Order.Value, 
                      dbo.ObjectAttributeValues_Order.CreateDate
FROM         dbo.Attributes INNER JOIN
                      dbo.ObjectAttributes ON dbo.Attributes.AttributeID = dbo.ObjectAttributes.AttributeID INNER JOIN
                      dbo.ObjectAttributeTypes ON dbo.ObjectAttributes.ObjectAttributeTypeID = dbo.ObjectAttributeTypes.ObjectAttributeTypeID INNER JOIN
                      dbo.ObjectAttributeValues_Order ON dbo.ObjectAttributes.ObjectAttributeID = dbo.ObjectAttributeValues_Order.ObjectAttributeID INNER JOIN
                      dbo.Objects ON dbo.ObjectAttributes.ObjectID = dbo.Objects.ObjectID INNER JOIN
                      dbo.[Order] ON dbo.ObjectAttributeValues_Order.OrderId = dbo.[Order].OrderId INNER JOIN
                      dbo.ValueTypes ON dbo.Attributes.DefaultValueTypeID = dbo.ValueTypes.ValueTypeID
ORDER BY dbo.[Order].OrderId, dbo.ObjectAttributeValues_Order.ObjectAttributeValueID


GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[21] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Attributes"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 125
               Right = 232
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ObjectAttributes"
            Begin Extent = 
               Top = 6
               Left = 270
               Bottom = 125
               Right = 469
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ObjectAttributeTypes"
            Begin Extent = 
               Top = 6
               Left = 507
               Bottom = 125
               Right = 706
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ObjectAttributeValues_Order"
            Begin Extent = 
               Top = 6
               Left = 744
               Bottom = 125
               Right = 945
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Objects"
            Begin Extent = 
               Top = 6
               Left = 983
               Bottom = 125
               Right = 1168
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Order"
            Begin Extent = 
               Top = 6
               Left = 1206
               Bottom = 125
               Right = 1384
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ValueTypes"
            Begin Extent = 
               Top = 126
               Left = 38
               Bottom = 245
               Right = 198
      ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_ObjectAttributeValues_Order'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'      End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 5295
         Alias = 900
         Table = 3960
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_ObjectAttributeValues_Order'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_ObjectAttributeValues_Order'
GO


