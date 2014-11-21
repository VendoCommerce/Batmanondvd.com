<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SidReport.aspx.cs" EnableEventValidation="false"
    Inherits="CSWeb.Admin.SIdReport" EnableViewState="true" EnableSessionState="true" %>

<%@ Register TagPrefix="usercontrols" TagName="RangeDateControl" Src="usercontrols/RangeDateControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>BatmanOnDvd.com - Sid Report</title>
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
    <link href="/Styles/midstyles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="outerwrap2">
        <div id="header2">
            <div id="logo">
                <img src="/Content/images/conversion_logo.jpg" width="238" height="62" />

            </div>
            <!-- end logo -->
           
            <!-- end log_link -->
            <div id="report_nav">
                                <a href="VersionReport.aspx">
                    Version Report</a> | <a href="sReports.aspx">
                    Standard Report</a>
                 | 
                  <span class="current">  SId Report   </span>
            </div>
            <div id="log_link">
                <a id="lnk_logout" href="Logout.aspx">Logout</a></div>
        </div>
        <!-- end header -->
        <div id="main2">
            <div id="date2">
                <p class="day">
                    <asp:Literal ID="liHeader" runat="server" /></p>
                <p class="time">
                    <asp:Literal ID="liSubHeader" runat="server" /></p>
            </div>
            <div id="left">
                <div id="client">
                    <h2>
                    </h2>
                    <div id="client_logo">
                        <asp:Image ID="imgLogo" style="max-width: 120px" runat="server"  /></div>
                </div>
                <div id="retrieve_activity">
                    <h2>
                    </h2>
                    <div id="activity_select">
                        <p>
                            Select range:</p>
                        <p>
                            <usercontrols:RangeDateControl ID="rangeDateControlCriteria" runat="server" DisplayDropDown="true" StartDateWidth="115" EndDateWidth="115"
                                LabelStartText="From:" LabelEndText="To:" PostbackFunction="btnSubmit" />
                        <div class="clear">
                        </div>
                        
                        <p>
                            &nbsp;
                        </p>
                        <p>
                            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" /></p>
                        <p>
                            &nbsp;
                        </p>
                    </div>
                </div>
            </div>
            <!-- end left -->
            <div id="right2">
                <h2>
                </h2>
                <!-- end summary header -->
                
             
                   <div class="clear">

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

<asp:DataList runat="server" ID="dlVersionList" RepeatLayout="Flow" RepeatDirection="Horizontal" OnItemDataBound="dlVersionList_ItemDataBound">
<HeaderTemplate>
<table class="table table-bordered table-report table-striped">
<tr class="alt"><th colspan="7">Summary</th></tr>
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
            <div class="clear">
            </div>
    </div>
    <!-- end outerwrap -->
     </form>
</body>
</html>
