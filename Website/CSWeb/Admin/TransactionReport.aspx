<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TransactionReport.aspx.cs" Inherits="CSWeb.Admin.TransactionReport"  EnableViewState="true" EnableSessionState="true"%>
<%@ Register TagPrefix="usercontrols" TagName="RangeDateControl" Src="~/usercontrols/RangeDateControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>My-No-No.com - Transaction History Report</title>
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
                <a href="VersionReport.aspx">Version Report </a>| <a href="Reports.aspx">Standard
                    Report</a> | <a href="MIdReport.aspx">MID Report</a>|<span class="current">Transaction History</span> |  <a href="PromoCodeDetail.aspx"> PromoCode Detail Report</a> | <a href="PromoCodeSummary.aspx">PromoCode Summary Report </a>
                
            </div>
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
                             <img src="/Content/Images/nono_logo.png" /></div>
                </div>
                <div id="retrieve_activity">
                    <h2>
                    </h2>
                    <div id="activity_select">
                        <p>
                            Select range:</p>
                        <p>
                            <usercontrols:RangeDateControl ID="rangeDateControlCriteria" LabelStyle="FieldName"
                                runat="server" DisplayDropDown="true" StartDateWidth="115" EndDateWidth="115"
                                LabelStartText="From" LabelEndText="To" />
                        </p>
                        <div class="clear">
                        </div>
                        <p>
                            &nbsp;
                        </p>
                 
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
     
                    <asp:DataList runat="server" ID="dlTransactionList" RepeatLayout="Flow" RepeatDirection="Horizontal"
                        OnItemDataBound="dlTransactionList_ItemDataBound">
                        <HeaderTemplate>
                        <div class="table_pad2">
                            <table class="summary_table2" width="720" cellpadding="0" cellspacing="0">
                                <tr>
                                    <th class="cola">
                                        Order No
                                    </th>
                                    <th class="colb">
                                        Status
                                    </th>
                                    <th class="colc">
                                        Affiliate
                                    </th>
                                    <th class="cold">
                                        Payment Type
                                    </th>
                                    <th class="colf">
                                        Auth Code
                                    </th>
                                    <th class="colf">
                                        Transaction ID
                                    </th>
                                    <th class="colf">
                                        Transaction Date
                                    </th>
                                    <th class="colf">
                                        Total
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="cola">
                                    <asp:Label ID="lblOrderNo" runat="server" />
                                </td>
                                <td class="colb">
                                    <asp:Label ID="lblOrderStatus" runat="server" />
                                </td>
                                <td class="colc">
                                    <asp:Label ID="lblAffiliate" runat="server" />
                                </td>
                                <td class="cold">
                                    <asp:Label ID="lblPayment" runat="server" Text="Credit Card" />
                                </td>
                                <td class="colf">
                                    <asp:Label ID="lblAuthCode" runat="server" />
                                </td>
                                <td class="colf">
                                    <asp:Label ID="lblTransaction" runat="server" />
                                </td>
                                 <td class="colf">
                                    <asp:Label ID="lblTransactionDate" runat="server" />
                                </td>
                                 <td class="colf">
                                    <asp:Label ID="lblAmount" runat="server" />
                                </td>
                            </tr>
                        </ItemTemplate>
     
                    </asp:DataList>
                    </table>
                    <!-- end summary table -->
                </div>
                <!-- end table_pad -->
                <!-- end right -->
                <div class="clear">
                </div>
            </div>
            <!-- end main -->
            <div class="clear">
            </div>
        </div>
        <!-- end outerwrap -->
    </form>
</body>
</html>
