<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="CSWeb.Admin.ClientReport"  EnableViewState="true" EnableSessionState="true" EnableEventValidation="false" %>
<%@ Register TagPrefix="usercontrols" TagName="RangeDateControl" Src="usercontrols/RangeDateControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2" runat="server">
    <title>BatmanOnDvd.com - Standard Report</title>
    <style type="text/css">
        .day_range {
width: 112px;
font-size: 11px;
color: #6b6b85;
}
        select {
  font-family: Verdana,sans-serif;
  font-size: 8pt;
}
        body 
       {
           color: #6B6B85;
        }
        
.or {
text-align: center;
padding: 9px 0 3px 0;
}
        body, table, p, h1, h2
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 11px;
        }
        body, p, h1, h2, h3, h4
        {
            margin: 0;
            padding: 0;
        }
        img
        {
            border: none;
        }
        body
        {
            padding-bottom: 40px;
        }
        h1
        {
            font-size: 14px;
        }
        h2
        {
            border-bottom: 2px #fff solid;
        }
        table
        {
            /*color:#000*/
        }
        #wrapper
        {
            width: 941px;
            margin: auto;
        }
        #container
        {
            float: left;
            background: #e7e7e7 url(/Content/images/admin/rep_bg2.jpg) no-repeat 176px 158px;
            border: 15px #ccc solid;
            border-top: none;
            position: relative;
            padding-bottom: 48px;
        }
        .text_replace
        {
            text-indent: -999em;
            overflow: hidden;
        }
        #header
        {
            background: url(/Content/Images/admin/rep_mainhd2.jpg) no-repeat;
            width: 921px;
            height: 125px;
        }
        #hd_date
        {
            margin-bottom: 3px;
        }
        #hd_date, #time
        {
            text-align: center;
        }
        #hd_client
        {
            background: url(/Content/images/admin/rep_hdclient.jpg) no-repeat;
        }
        #hd_resource
        {
            background: url(/Content/images/admin/rep_hdresource.jpg) no-repeat;
        }
        #hd_activity
        {
            background: url(/Content/images/admin/rep_hdactivity.jpg) no-repeat;
        }
        #hd_summary
        {
            background: url(/Content/images/admin/rep_hdsummary.jpg) no-repeat;
        }
        #hd_wksummary
        {
            background: url(/Content/images/admin/rep_hdwksummary.jpg) no-repeat;
        }
        #hd_mosummary
        {
            background: url(/Content/images/admin/rep_hdmosummary.jpg) no-repeat;
        }
        .hd_items_sold
        {
            background: url(/Content/images/admin/rep_hdtopitems.jpg) no-repeat;
            width: 306px;
            height: 20px;
        }
        #hd_client, #hd_resource, #hd_activity
        {
            width: 152px;
            height: 20px;
        }
        #hd_summary, #hd_wksummary, #hd_mosummary
        {
            width: 299px;
            height: 20px;
        }
        .content
        {
            background-color: #fff;
            border: 1px #e5e5e5 solid;
            margin-bottom: 11px;
        }
        #client .content
        {
            padding: 13px 0 9px 7px;
        }
        #resource .content img
        {
            margin: 3px;
        }
        #activity .content
        {
            padding: 11px 6px;
        }
        .col_a
        {
            float: left;
            width: 152px;
            margin: 8px 0 0 9px;
            display: inline; /*fixes ie6 float margin bug*/
        }
        .col_b
        {
            margin: 5px 0 0 163px;
            padding: 8px 0 0 7px;
        }
        .col_b div
        {
            float: left;
            margin-left: 7px;
            display: inline; /*fixes ie6 float margin bug*/
        }
        .col_b .content
        {
            margin-left: 0;
            padding: 2px;
        }
        #summary, #wk_summary, #mo_summary
        {
            width: 299px;
        }
        #ddlCannedDates
        {
            border: 1px #999 solid;
            margin: 4px 0 0 0;
            font-size: 11px;
            font-family: Arial, Helvetica, sansserif;
            width: 112px;
            color: #6B6B85;
        }
        #txtDateTo_dvControl, #txtDateFrom_dvControl
        {
            white-space: nowrap;
            width: 30px;
        }
        #items_sold, #wk_items_sold, #mo_items_sold
        {
            width: 306px;
        }
        #txtDateFrom_txtDtInt, #txtDateTo_txtDtInt, #txtDateFrom, #txtDateTo
        {
            border: 1px #999 solid;
            width: 50px;
            height: 12px;
            font-size: 5px;
            margin: 0 0 3px 4px;
            font-size: 11px;
            font-family: Arial, Helvetica, sans-serif;
            padding: 2px;
        }
        #activity label
        {
            clear: left;
            float: left;
            width: 35px;
            text-align: right;
            display: block;
            padding-top: 3px;
        }
        #activity input
        {
            /*margin-left:4px;*/
        }
        .clearfix:after
        {
            content: ".";
            display: block;
            height: 0;
            clear: both;
            visibility: hidden;
        }
        td, th
        {
            margin: 0;
            padding: 3px 5px;
        }
        th
        {
            background-color: #f6f6f6;
            color: #000;
            border-top: 1px #e5e5e5 solid;
            border-bottom: 1px #e5e5e5 solid;
        }
        th.cola, th.cold
        {
            border-left: 1px #e5e5e5 solid;
        }
        th.colc, th.colg
        {
            border-right: 1px #e5e5e5 solid;
        }
        .tbl_cola
        {
            width: 175px;
        }
        .tbl_colb
        {
            width: 60px;
        }
        .tbl_colc
        {
            width: 50px;
        }
        .tbl_cold
        {
            width: 200px;
            border-right: 1px solid #E5E5E5;
        }
        .tbl_cole
        {
            width: 30px;
        }
        .tbl_colf
        {
            width: 25px;
        }
        .tbl_colg
        {
            width: 50px;
        }
        td.tbl_colb, td.tbl_cole
        {
            border-left: 1px #e5e5e5 solid;
            border-right: 1px #e5e5e5 solid;
        }
        td.tbl_colf
        {
            border-right: 1px #e5e5e5 solid;
        }
        #lnk_logout
        {
            position: absolute;
            top: 8px;
            right: 8px;
            display: block;
            color: #5e5e5e;
        }
        #resource
        {
            display: none;
        }
        #btnSubmit
        {
            margin-top: 4px;
        }
        #stats_week, #wk_items_sold, #stats_month, #mo_items_sold, .orders_4upsell, .aov_upsell
        {
            display: none;
        }
        #txtDateFrom_tdtxtHiddens, #txtDateTo_tdtxtHiddens
        {
            width: 0;
        }
        #txtDateFrom_tdtxtHiddens, #txtDateFrom_tdtxtDtInt, #txtDateTo_tdtxtHiddens, #txtDateTo_tdtxtDtInt
        {
            margin: 0;
            padding: 0;
        }
        #txtDateFrom_tdtxtDtInt, #txtDateTo_tdtxtDtInt
        {
            padding-top: 3px;
            padding-bottom: 12px;
        }
        #txtDateFrom_txtDtInt, #txtDateTo_txtDtInt
        {
            width: 140px;
            color: #6B6B85;
        }
        .Error
        {
            color: Red;
        }
        #report_nav
        {
            width: 921px;
            height: 19px;
            position: absolute;
            margin: 0;
            padding: 0;
            top: 84px;
            font-weight: bold;
            font-family: 'ITC Avant Garde Std' , 'Avant Garde' , 'Century Gothic' , 'Lucida Grande' ,Geneva,Arial;
            text-align: center;
            color: #000;
	font-size: 11px;
        }
        #report_nav a
        {
            color: #000;
            text-decoration: none;
            margin: 0 6px;
        }
        #report_nav span.current
        {
            color: #657A33;
            text-decoration: none;
            margin: 0 6px;
        }
    </style>
</head>
<body style="background-color: #e2e2e2;">
    <form id="form3" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="wrapper" style="font-family: Arial">
        <div id="container">
 <a id="lnk_logout" href="Logout.aspx">Logout</a>
            <div id="report_nav">
                 <a href="VersionReport.aspx">
                    Version Report</a> | <span class="current">Standard Report</span> <%--|    <a href="SIdReport.aspx">
                    SId Report</a>--%>
                
            </div>
            <div id="header" class="text_replace">
                CONVERSION SYSTEMS - Maximize Your Online Revenue
            </div>
            <h1 id="hd_date">
                <asp:Literal ID="liHeader" runat="server" />
            </h1>
            <p id="time" class="daytime">
                <asp:Literal ID="liSubHeader" runat="server" />
            </p>
            <div class="col_a">
                <div id="client">
                    <h2 id="hd_client" class="text_replace">
                        Client</h2>
                    <div class="content" style="text-align:center">
                 
                        <asp:Image ID="imgLogo" style="max-width: 120px" runat="server"  />
                    </div>
                </div>
                <div id="resource">
                    <h2 id="hd_resource" class="text_replace">
                        Select Resource</h2>
                    <div class="content">
                        <img src="/Content/images/rep_lnkreports.jpg" width="144" height="29" alt="Reports" />
                        <img src="/Content/images/rep_lnkorders.jpg" width="144" height="29" alt="Orders" />
                    </div>
                </div>
                <div id="activity">
                    <h2 id="hd_activity" class="text_replace">
                        Retrieve Activity</h2>
                    <div class="content">
                        <div align="left">
                             
                            Select range:
                          <usercontrols:RangeDateControl ID="rangeDateControlCriteria" runat="server" DisplayDropDown="true" StartDateWidth="115" EndDateWidth="115"
                                LabelStartText="From:" LabelEndText="To:" PostbackFunction="btnSubmit" />
          


                        </div>
                        <br />
                        <div align="left">
                            Select Version:
                            <br />
                            <asp:DropDownList ID="ddlVersion" runat="server">
                            </asp:DropDownList>
                            </div>
                            <br />
                            <!--div align="left">
                                Upsell Path:
                                <br />
                                <asp:DropDownList Width="138" ID="ddlPaths" runat="server">
                                </asp:DropDownList>
                            </div-->
                            <br />
                            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" />
                            <div align="center" class="Error">
                                <asp:Literal ID="litError" runat="server" />
                            </div>
                        </div>
          
                </div>
            </div> <!-- Col A -->
            <div class="col_b">
                <div id="stats_day">
                    <div id="summary">
                        <h2 id="hd_summary" class="text_replace">
                            Summary</h2>
                        <div class="content">
                            <table cellpadding="0" cellspacing="0">
                                <asp:Repeater ID="rptTotals" runat="server">
                                    <HeaderTemplate>
                                        <tr class="alt">
                                            <th class="tbl_cola">
                                                &nbsp;
                                            </th>
                                            <th class="tbl_colb" style="text-decoration: underline">
                                                Total
                                            </th>
                                            <th class="tbl_colc" style="text-decoration: underline">
                                                %
                                            </th>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td class="tbl_cola">
                                                <%# DataBinder.Eval(Container.DataItem, "Item1") %>
                                            </td>
                                            <td class="tbl_colb">
                                                <%# DataBinder.Eval(Container.DataItem, "Item2")%>
                                            </td>
                                            <td class="tbl_colc">
                                                <%# String.Format("{0:0.##}",DataBinder.Eval(Container.DataItem, "Item3"))%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </div>
                    </div>

                    <div id="items_sold">
                        <h2 class="hd_items_sold text_replace">
                            Items Sold</h2>
                        <div class="content">
                            <table cellpadding="0" cellspacing="0">
                                <asp:Repeater ID="rptTotalsItem" runat="server"  OnItemDataBound="rptTotalsItem_ItemDataBound">
                                    <HeaderTemplate>
                                        <tr>
                                            <th class="tbl_cold first" style="text-decoration: underline">
                                                <asp:LinkButton ID="LinkButton4" Text="Item" runat="server" CommandName="right_item"
                                                    ForeColor="Black"></asp:LinkButton>
                                            </th>
                                            <th class="tbl_colf" style="text-decoration: underline">
                                                <asp:LinkButton ID="LinkButton5" Text="Qty" runat="server" CommandName="right_qty"
                                                    ForeColor="Black"></asp:LinkButton>
                                            </th>
                                            <th class="tbl_colg last" style="text-decoration: underline">
                                                <asp:LinkButton ID="LinkButton6" Text="Revenue" runat="server" CommandName="right_revenueClick"
                                                    ForeColor="Black"></asp:LinkButton>
                                            </th>
                                               <th class="tbl_colg last" style="text-decoration: underline">
                                               <asp:LinkButton ID="LinkButton1" Text="%" runat="server" CommandName="right_revenueClick"
                                                    ForeColor="Black"></asp:LinkButton>
                                            </th>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td class="tbl_cold">
                                                <%# DataBinder.Eval(Container.DataItem, "Title")%>
                                            </td>
                                            <td class="tbl_colf">
                                                <%# DataBinder.Eval(Container.DataItem, "Qty") %>
                                            </td>
                                            <td class="tbl_colg">
                                                <%# String.Format("{0:C}", DataBinder.Eval(Container.DataItem, "TotalPrice")) %>
                                            </td>
                                             <td class="tbl_colg">
                                             <asp:Label ID="lblPercentage" runat="server" />
                                             </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <tr style="display: none">
                                            <th class="tbl_cold">
                                                Totals
                                            </th>
                                            <th class="tbl_colf">
                                                <asp:Literal ID="litTotal" runat="server" />
                                            </th>
                                            <td class="tbl_colg">
                                            </td>
                                            <td class="tbl_colg">
                                            </td>
                                        </tr>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <!--close col_b-->
        </div>
    </div>
    </form>
</body>
</html>
