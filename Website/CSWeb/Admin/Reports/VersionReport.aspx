<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VersionReport.aspx.cs"
    Inherits="CSWeb.Admin.Reports.VersionReport" MasterPageFile="../AdminSite.master" %>

<%@ Register TagPrefix="usercontrols" TagName="RangeDateControl" Src="../usercontrols/RangeDateControlV1.ascx" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" Width="450"
        font-name="verdana, san-serif" Font-Size="7" CssClass="error"></asp:ValidationSummary>
    <table width="100%" cellpadding="2" cellspacing="1" border="0" class="ExampleA">
        <tr>
            <td class="text" width="40%">
                <usercontrols:RangeDateControl ID="rangeDateControlCriteria" LabelStyle="FieldName"
                    runat="server" DisplayDropDown="true" StartDateWidth="115" EndDateWidth="115"
                    LabelStartText="From" LabelEndText="To" />
            </td>
             <td width="15%"><asp:CheckBox AutoPostBack="false" ID="cbArchive" runat="server" Text="IncludeArchiveData" /></td>
            <td>
                <asp:Button ID="lblSearch" runat="server" CommandName="Search" Text="Search" OnClick="btnSubmit_Click" />
            </td>
        </tr>
    </table>
    <br />
    
    <div align="right">
        <asp:Literal ID="FCLiteral" runat="server"></asp:Literal>
        <br />
    </div>

    <asp:DataList runat="server" ID="dlVersionCategoryList" RepeatLayout="Flow" RepeatDirection="Horizontal"
        OnItemDataBound="dlVersionCategoryList_ItemDataBound" DataKeyField="CategoryId">
        <HeaderTemplate>
            <table width="100%" border="0" cellspacing="1" cellpadding="2" border="2">
                <tr>
                    <td class="title" colspan="3">
                        VersionReport
                    </td>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr class="header">
                <td align="left" class="ibn-sectionheader">
                    <b>
                        <asp:Label ID="lblCategory" runat="server" /></b>
                </td>
            </tr>
            <tr class="header">
                <asp:DataList runat="server" ID="dlVersionItemList" RepeatLayout="Flow" RepeatDirection="Horizontal"
                    OnItemDataBound="dlVersionList_ItemDataBound">
                    <HeaderTemplate>
                        <table width="100%" border="0" cellspacing="1" cellpadding="2">
                            <tr>
                                <td width="20%">
                                    <b>Version</b>
                                </td>
                                <td align="center" width="10%">
                                    <b>Unique Visitors</b>
                                </td>
                                <td align="center" width="10%">
                                    <b>Total Orders</b>
                                </td>
                                <td align="center" width="10%">
                                    <b>Conversion %</b>
                                </td>
                                <td align="center" width="10%">
                                    <b>Avg. Order Value</b>
                                </td>
                                <td align="center" width="10%">
                                    <b>Total Revenue</b>
                                </td>
                                <td align="center" width="10%">
                                    <b>Revenue Per Visitor</b>
                                </td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td width="20%">
                                <asp:Label ID="lblTitle" runat="server" />
                            </td>
                            <td align="center" width="10%">
                                N/A
                            </td>
                            <td align="center" width="10%">
                                <asp:Label ID="lblTotalOrder" runat="server" />
                            </td>
                            <td align="center" width="10%">
                                N/A
                            </td>
                            <td align="center" width="10%">
                                <asp:Label ID="lblAvgOrder" runat="server" />
                            </td>
                            <td align="center" width="10%">
                                <asp:Label ID="lblTotalRev" runat="server" />
                            </td>
                            <td align="center" width="10%">
                                N/A
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        <tr>
                            <td width="20%" align="left" class="ibn-sectionheader">
                                <b>Total</b>
                            </td>
                            <td align="center" width="10%">
                                N/A
                            </td>
                            <td align="center" width="10%">
                                <asp:Label ID="lblSumTotalOrder" runat="server" />
                            </td>
                            <td align="center" width="10%">
                                N/A
                            </td>
                            <td align="center" width="10%">
                                <asp:Label ID="lblSumAvgOrder" runat="server" />
                            </td>
                            <td align="center" width="10%">
                                <asp:Label ID="lblSumTotalRev" runat="server" />
                            </td>
                            <td align="center" width="10%">
                                N/A
                            </td>
                        </tr>
                        <tr>
                            <td colspan="7">
                                &#160;
                            </td>
                        </tr>
                    </FooterTemplate>
                </asp:DataList>
            </tr>
        </ItemTemplate>
    </asp:DataList>
</asp:Content>
