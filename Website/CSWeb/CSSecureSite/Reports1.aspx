<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reports1.aspx.cs" Inherits="CSWeb.Admin.ClientReport1"  EnableViewState="true" EnableSessionState="true" EnableEventValidation="false" MasterPageFile="AdminReport.master" %>
<%@ Register TagPrefix="usercontrols" TagName="RangeDateControl" Src="usercontrols/RangeDateControlReport.ascx" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <title><%=siteName %> - Standard Report</title>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
      <span id="pageid" class="standardreport"></span>
      
<aside class="report_sidebar">
<table class="table table-bordered table-report">
<tr class="header-row"><th><i class="icon-user"></i> Client</th></tr>
<td class="text-center noborder"><asp:Image ID="imgLogo" runat="server" /></td>
</table>

<table class="table table-bordered table-report">
<tr class="header-row"><th><i class="icon-filter"></i> Report Filters</th></tr>
<td class="noborder pad0">
<div class="report-filters form-horizontal form-box">

<div class="control-group">
<label class="text-center">Select Date Range:</label>
<usercontrols:RangeDateControl ID="rangeDateControlCriteria" runat="server" DisplayDropDown="true"  LabelStartText="From:" LabelEndText="To:" PostbackFunction="ctl00$MainContent$btnSubmit" />
</div>

<div class="control-group">
<label class="text-center">Select Version:</label>
<div class="controls text-center" style="margin-left: 0">
 <asp:DropDownList ID="ddlVersion" runat="server" CssClass="input-medium"></asp:DropDownList>
</div>
</div>

<div class="control-group">
<p class="text-center"><asp:Literal ID="litError" runat="server" />
 <asp:LinkButton ID="btnSubmit" CssClass="btn btn-success" runat="server" OnClick="btnSubmit_Click"><i class="icon-double-angle-right"></i> Submit</asp:LinkButton>
</div>
</div>
</td>
</table>
</aside>
    
    
 <div id="page-content-report">
<!-- row -->
<div class="row-fluid">
<div class="span6" id="summary">
<table class="table table-bordered table-report">
<asp:Repeater ID="rptTotals" runat="server">
 <HeaderTemplate>
<tr class="header-row"><th colspan="3" class="text-center">Summary</th></tr>
<tr class="subheader-row"><th></th><th class="span2 text-center">Total</th><th class="span1 text-center">%</th></tr>
</HeaderTemplate>
  <ItemTemplate>
<tr>
<td><%# DataBinder.Eval(Container.DataItem, "Item1") %></td>
<td class="text-right"><%# DataBinder.Eval(Container.DataItem, "Item2")%></td>
<td class="text-right" style="white-space:nowrap"><%# String.Format("{0:0.##}",DataBinder.Eval(Container.DataItem, "Item3"))%></td>
</tr>
  </ItemTemplate>
</asp:Repeater>
</table>
</div>


<div class="span6" id="items_sold">
<table class="table table-bordered table-report">
<asp:Repeater ID="rptTotalsItem" runat="server" OnItemDataBound="rptTotalsItem_ItemDataBound">
<HeaderTemplate>
<tr class="header-row">
<th colspan="4" class="text-center">Top Items Sold</th>
</tr>
<tr class="subheader-row">
<th><asp:Label ID="LinkButton4" Text="Item" runat="server" CommandName="right_item"></asp:Label></th>
<th class="text-center"><asp:Label ID="LinkButton5" Text="Qty" runat="server" CommandName="right_qty"></asp:Label></th>
<th class="text-center"><asp:Label ID="LinkButton6" Text="Revenue" runat="server" CommandName="right_revenueClick"></asp:Label></th>
<th class="text-center"><asp:Label ID="LinkButton1" Text="%" runat="server" CommandName="right_revenueClick"></asp:Label></th>
</tr>
   </HeaderTemplate>
<ItemTemplate>
<tr><td><%# DataBinder.Eval(Container.DataItem, "Title")%></td>
<td class="text-right"><%# DataBinder.Eval(Container.DataItem, "Qty") %></td>
<td class="text-right"><%# String.Format("{0:C}", DataBinder.Eval(Container.DataItem, "TotalPrice")) %></td>
<td class="text-right"><asp:Label ID="lblPercentage" runat="server" /></td>
</tr>
 </ItemTemplate>
  <FooterTemplate>
  
   <asp:Literal ID="litTotal" runat="server" />

 </FooterTemplate>
 </asp:Repeater>
                                               
</table>
</div>
</div>

</div> <!-- /page-content-report -->
</asp:Content>
