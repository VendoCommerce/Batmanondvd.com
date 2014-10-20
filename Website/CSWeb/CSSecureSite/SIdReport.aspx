<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SIdReport.aspx.cs" Inherits="CSWeb.Admin.SIdReport" EnableEventValidation="false" EnableViewState="true" EnableSessionState="true" MasterPageFile="AdminReport.master" %>

<%@ Register TagPrefix="usercontrols" TagName="RangeDateControl" Src="usercontrols/RangeDateControlReport.ascx" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <title><%=siteName %> - SID Report</title>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
   <span id="pageid" class="sidreport"></span>
      
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
<div class="span12">

<asp:DataList runat="server" ID="dlVersionList" RepeatLayout="Flow" RepeatDirection="Horizontal" OnItemDataBound="dlVersionList_ItemDataBound">
<HeaderTemplate>
<table class="table table-bordered table-report table-striped">
<tr class="header-row"><th colspan="7">Summary</th></tr>
<tr class="subheader-row">
  <th>SID</th>
  <th class="text-center">Unique Visitors</th>
  <th class="text-center">Total Orders</th>
  <th class="text-center">Conversion %</th>
  <th class="text-center">Total Revenue</th>
</tr>
</HeaderTemplate>
<ItemTemplate>
  <tr>
      <td><asp:Label ID="lblTitle" runat="server" /></td>
      <td class="text-center"><asp:Label ID="lbHitLinkVisitor" runat="server" /></td>
      <td class="text-center"><asp:Label ID="lblTotalOrder" runat="server" /></td>
      <td class="text-center"><asp:Label ID="lblConversion" runat="server" /></td>
      <td class="text-center"><asp:Label ID="lblTotalRev" runat="server" /></td>
  </tr>
  </ItemTemplate>
  <FooterTemplate>
      <tr class="success report-totals">
          <td>Total</td>
          <td class="text-center"><asp:Label ID="lblSumHitLinkVisitor" runat="server" /></td>
          <td class="text-center"><asp:Label ID="lblSumTotalOrder" runat="server" /></td>
          <td class="text-center"><asp:Label ID="lblSumTotalConversion" runat="server" /></td>
          <td class="text-center"><asp:Label ID="lblSumTotalRev" runat="server" /></td>
      </tr>
      </table>
  </FooterTemplate>
</asp:DataList>
    </div>
</div>



</div>        <!-- /page-content-report -->               
 </asp:Content>
