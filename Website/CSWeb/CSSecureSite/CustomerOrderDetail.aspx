<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerOrderDetail.aspx.cs" Inherits="CSWeb.Admin.CustomerOrderDetail" MasterPageFile="AdminSite.master" EnableViewState="True"%>

<%@ Register TagPrefix="usercontrols" TagName="RangeDateControl" Src="usercontrols/RangeDateControl.ascx" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:ValidationSummary ID="valErrorSummary" runat="server" CssClass="failureNotification"
        ValidationGroup="valError" DisplayMode="List" />
   
    <table width="100%" border="0" cellspacing="1" cellpadding="2" border="2">
        <tr>
            <td class="context-menu" colspan="3">
                Customer Order Details
            </td>
        </tr>
        <tr class="context-menu">
            <td align="left" width="25%" class="ibn-sectionheader">
                OrderId
            </td>
            <td align="center" width="25%" class="ibn-sectionheader">
                Email
            </td>
            <td align="center" width="10%" class="ibn-sectionheader">
               Total Amount
            </td>
            <td align="center" width="10%" class="ibn-sectionheader">
               Status
            </td>
             <td align="center" width="10%" class="ibn-sectionheader">
                 Completed
            </td>
           <td align="center" width="10%" class="ibn-sectionheader">
                 Options
            </td>
        </tr>
        <asp:DataList runat="server" ID="dlordersList" RepeatLayout="Flow" RepeatDirection="Horizontal">
            <ItemTemplate>
                <tr>
                    <td class="body" align="left" width="25%">
                        <%# DataBinder.Eval(Container.DataItem, "orderId")%>
                    </td>
                    <td width="25%" align="center">
                        <%# DataBinder.Eval(Container.DataItem, "Email")%>
                    </td>
              
                    <td width="10%" align="center">
                        $<%# String.Format("{0:0.##}", DataBinder.Eval(Container.DataItem, "Total"))%>
                    </td>
                    <td width="10%" align="center">
                        <%# DataBinder.Eval(Container.DataItem, "Title")%>
                    </td>
                 <td width="10%" align="center">
                        <%# DataBinder.Eval(Container.DataItem, "Completed")%>
                    </td>
                    <td nowrap align="center" width="10%">
                                <asp:HyperLink ID="hlDetail" runat="server" ImageUrl="/Content/images/admin/edit.gif" ToolTip="Details"  NavigateUrl='<%# "OrderDetail.aspx?oId=" + DataBinder.Eval(Container.DataItem, "OrderId")%>' />
                           
                         
                            </td>
                </tr>
            </ItemTemplate>
        </asp:DataList>
        <tr>
            <td colspan="4">
                <asp:Button runat="server" ID="btnCancel" Text="Back" CommandName="Back" OnCommand="btnAction_Command" />
        </tr>
        </table>
        <br />
</asp:Content>

