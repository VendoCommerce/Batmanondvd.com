<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StandardReport.aspx.cs"
    Inherits="CSWeb.Admin.Reports.StandardReport" MasterPageFile="../AdminSite.master" %>

<%@ Register TagPrefix="usercontrols" TagName="RangeDateControl" Src="../usercontrols/RangeDateControlV1.ascx" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .OR
        {
            margin: 12px 0 6px 0;
            fontweight: bold;
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
            background: url(/Content/images/admin/rep_mainhd2.jpg) no-repeat;
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
            fontsize: 5px;
            margin: 4px 0 0 0;
            fontsize: 11px;
            fontfamily: Arial, Helvetica, sansserif;
            width: 112px;
            color: #6B6B85;
        }
        #txtDateTo_dvControl, #txtDateFrom_dvControl
        {
            whitespace: nowrap;
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
        }
        
        #report_nav a
        {
            color: #000;
            text-decoration: none;
            margin: 0 12px;
        }
        
        #report_nav span.current
        {
            color: #657a33;
            text-decoration: none;
            margin: 0 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" Width="450"
        font-name="verdana, san-serif" Font-Size="7" CssClass="error"></asp:ValidationSummary>
    <table width="100%" cellpadding="2" cellspacing="1" border="0" class="ExampleA">
        <tr>
            <td class="text" width="500">
                <usercontrols:RangeDateControl ID="rangeDateControlCriteria" LabelStyle="FieldName"
                    runat="server" DisplayDropDown="true" StartDateWidth="115" EndDateWidth="115"
                    LabelStartText="From" LabelEndText="To" />
            </td>
            <td>
                Version:
                <asp:DropDownList ID="ddlVersion" runat="Server" CssClass="text-1" />
            </td>
             <td>
                UpSell Paths:
                <asp:DropDownList ID="ddlPaths" runat="Server" CssClass="text-1" />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Button ID="lblSearch" runat="server" CommandName="Search" Text="Search" OnClick="btnSubmit_Click" />
            </td>
        </tr>
    </table>
    <br />
    <br />
    <br />
    <table cellpadding="2" cellspacing="1" border="0">
        <tr>
            <td width="50%" valign="top" class="content">
                <h2 id="hd_summary" class="text_replace">
                    Summary</h2>
                <asp:Repeater ID="rptTotals" runat="server">
                    <HeaderTemplate>
                        <table cellpadding="2" cellspacing="1" border="0">
                            <tr class="alt">
                                <th>&nbsp;
                                    
                                </th>
                                <th style="text-decoration: underline">
                                    Total
                                </th>
                                <th style="text-decoration: underline">
                                    %
                                </th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "Item1") %>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "Item2")%>
                            </td>
                            <td>
                                <%# String.Format("{0:0.##}",DataBinder.Eval(Container.DataItem, "Item3"))%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
    </table>
    </td>
    <td width="10%">&nbsp;
        
    </td>
    <td width="40%" valign="top" class="content">
        <h2 class="hd_items_sold text_replace">
            Items Sold</h2>
        <asp:Repeater ID="rptTotalsItem" runat="server">
            <HeaderTemplate>
                <table cellpadding="2" cellspacing="1" border="0">
                    <tr>
                        <th class="tbl_cold first" style="text-decoration: underline;" align="left">
                            <asp:LinkButton ID="LinkButton1" Text="Item" runat="server" CommandName="right_item"
                                ForeColor="Black"></asp:LinkButton>
                        </th>
                        <th class="tbl_colf" style="text-decoration: underline">
                            <asp:LinkButton ID="LinkButton2" Text="Qty" runat="server" CommandName="right_qty"
                                ForeColor="Black"></asp:LinkButton>
                        </th>
                        <th class="tbl_colg last" style="text-decoration: underline">
                            <asp:LinkButton ID="LinkButton3" Text="Revenue" runat="server" CommandName="right_revenueClick"
                                ForeColor="Black"></asp:LinkButton>
                        </th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td class="tbl_cold">
                        <%# DataBinder.Eval(Container.DataItem, "Title") %>
                    </td>
                    <td class="tbl_colf">
                        <%# DataBinder.Eval(Container.DataItem, "Qty") %>
                    </td>
                    <td class="tbl_colg">
                        <%# String.Format("{0:0.##}", DataBinder.Eval(Container.DataItem, "TotalPrice")) %>
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
                </tr>
            </FooterTemplate>
        </asp:Repeater>
        </table>
    </td>
    </tr> </table>
</asp:Content>
