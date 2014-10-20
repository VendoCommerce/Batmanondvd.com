<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VersionReport1.aspx.cs" EnableEventValidation="false" Inherits="CSWeb.Admin.VersionReport1" EnableViewState="true" EnableSessionState="true" MasterPageFile="AdminReport.master"  %>

<%@ Register TagPrefix="usercontrols" TagName="RangeDateControl" Src="usercontrols/RangeDateControlReport.ascx" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <title><%=siteName %> - Version Report</title>
    
    <script type="text/javascript">
        function removejscssfile(filename, filetype) {
            var targetelement = (filetype == "js") ? "script" : (filetype == "css") ? "link" : "none" //determine element type to create nodelist from
            var targetattr = (filetype == "js") ? "src" : (filetype == "css") ? "href" : "none" //determine corresponding attribute to test for
            var allsuspects = document.getElementsByTagName(targetelement)
            for (var i = allsuspects.length; i >= 0; i--) { //search backwards within nodelist for matching elements to remove
                if (allsuspects[i] && allsuspects[i].getAttribute(targetattr) != null && allsuspects[i].getAttribute(targetattr).indexOf(filename) != -1)
                    allsuspects[i].parentNode.removeChild(allsuspects[i]) //remove element by calling parentNode.removeChild()
            }
        }
    </script>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
   <span id="pageid" class="versionreport"></span>
      
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
    
<asp:DataList runat="server" ID="dlVersionCategoryList" RepeatLayout="Flow" RepeatDirection="Horizontal" OnItemDataBound="dlVersionCategoryList_ItemDataBound" DataKeyField="CategoryId">
<HeaderTemplate>
<table class="table table-bordered table-report">
      <tr class="header-row"><th colspan="7">Summary</th></tr>
</HeaderTemplate>
<ItemTemplate>
<tr id="CategoryHeaderRow" runat="server" class="categoryrow">
<td colspan="7"><span class="lblcat"><asp:Label ID="lblCategory" runat="server" /></span></td></tr>                 
<asp:DataList runat="server" ID="dlVersionItemList" RepeatLayout="Flow" RepeatDirection="Horizontal" OnItemDataBound="dlVersionList_ItemDataBound">
<HeaderTemplate>
  <tr class="subheader-row">
      <th>Sites</th>
      <th class="text-center">Unique Visitors</th>
      <th class="text-center">Total Orders</th>
      <th class="text-center">Conversion %</th>
      <th class="text-center">Avg. Order Value</th>
      <th class="text-center">Total Revenue</th>
      <th class="text-center">Revenue Per Visitor</th>
  </tr>
</HeaderTemplate>
<ItemTemplate>
    <tr>
        <td><asp:Label ID="lblTitle" runat="server" /></td>
        <td class="text-center"><asp:Label ID="lbHitLinkVisitor" runat="server" /></td>
        <td class="text-center"><asp:Label ID="lblTotalOrder" runat="server" /></td>
        <td class="text-center"><asp:Label ID="lblConversion" runat="server" /></td>
        <td class="text-center"><asp:Label ID="lblAvgOrder" runat="server" /></td>
        <td class="text-center"><asp:Label ID="lblTotalRev" runat="server" /></td>
        <td class="text-center"><asp:Label ID="lblRevenuePerVisit" runat="server" /></td>
    </tr>
 </ItemTemplate>
<FooterTemplate>
     <tr id="versionFooter" runat="server" class="report-totals-version">
        <td>Total:</td>
        <td class="text-center"><asp:Label ID="lblSumHitLinkVisitor" runat="server" /></td>
        <td class="text-center"><asp:Label ID="lblSumTotalOrder" runat="server" /></td>
        <td class="text-center"><asp:Label ID="lblSumTotalConversion" runat="server" /></td>
        <td class="text-center"><asp:Label ID="lblSumAvgOrder" runat="server" /></td>
        <td class="text-center"><asp:Label ID="lblSumTotalRev" runat="server" /></td>
        <td class="text-center"><asp:Label ID="lblSumRevenuePerClick" runat="server" /></td>
    </tr> 
 </FooterTemplate>
</asp:DataList>
                                 
 </ItemTemplate>
<FooterTemplate>
       <tr><td colspan="7">&nbsp;</td></tr>            
    <tr class="success report-totals">
        <td>Total:</td>
        <td class="text-center"><asp:Label ID="lblTotalSumHitLinkVisitor" runat="server" /></td>
        <td class="text-center"><asp:Label ID="lblTotalSumTotalOrder" runat="server" /></td>
        <td class="text-center"><asp:Label ID="lblTotalSumTotalConversion" runat="server" /></td>
        <td class="text-center"><asp:Label ID="lblTotalSumAvgOrder" runat="server" /></td>
        <td class="text-center"><asp:Label ID="lblTotalSumTotalRev" runat="server" /></td>
        <td class="text-center"><asp:Label ID="lblTotalSumRevenuePerClick" runat="server" /></td>
    </tr>
          </table>                
 </FooterTemplate>
</asp:DataList>
             
 
</div>
</div>


</div>        <!-- /page-content-report -->    
             
    </asp:Content>
