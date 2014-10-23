<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerOrders.aspx.cs"
    Inherits="CSWeb.Admin.CustomerOrders" MasterPageFile="AdminSite.master" EnableViewState="True" %>

<%@ Register TagPrefix="usercontrols1" TagName="RangeDateControl" Src="usercontrols/RangeDateControl.ascx" %>
<%@ Register TagPrefix="usercontrols1" TagName="paging" Src="UserControls/PageControl.ascx" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
       <asp:ValidationSummary ID="valErrorSummary" runat="server" CssClass="failureNotification"
        ValidationGroup="valError" DisplayMode="List" />

    <table width="600" cellpadding="2" cellspacing="1" border="0" class="ibn-alternating">
        <tr>
            <td class="text" width="150">
                First Name:
            </td>
            <td class="text" width="150">
                Last Name:
            </td>
            <td class="text" width="150">
                Email:
            </td>
        </tr>
        <tr class="FieldValue">
            <td>
                <asp:TextBox ID="txtFirstName" runat="server" Columns="20" MaxLength="50" EnableViewState="True"
                    CssClass="FieldValue" />
                <asp:RequiredFieldValidator runat="server" Display="Dynamic" ControlToValidate="txtFirstName"
                    ID="RequiredFieldValidator4" ValidationGroup="valError" ErrorMessage="* FirstName is required field.">*</asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:TextBox ID="txtLastName" runat="server" Columns="20" MaxLength="50" EnableViewState="True"
                    CssClass="FieldValue" />
                <asp:RequiredFieldValidator runat="server" Display="Dynamic" ControlToValidate="txtLastName"
                    ID="RequiredFieldValidator1" ValidationGroup="valError" ErrorMessage="* LastName is required field.">*</asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server" Columns="20" MaxLength="50" EnableViewState="True"
                    CssClass="FieldValue" />
                <asp:RequiredFieldValidator runat="server" Display="Dynamic" ControlToValidate="txtEmail"
                    ID="RequiredFieldValidator2" ValidationGroup="valError" ErrorMessage="* Email is required field.">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Button ID="lblSearch" runat="server" CommandName="Search" Text="Search" OnClick="lblOrder_Search"
                    ValidationGroup="valError" CausesValidation="true" />
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" border="0" cellspacing="1" cellpadding="2" border="2">
     <tr>
            <td align="right" colspan="3">
                <asp:UpdatePanel ID="updPg" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <usercontrols1:paging ID="pg" OnPageChanged="OnPaging" Mode="Links" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="context-menu" colspan="3">
                Orders
            </td>
        </tr>
        <tr class="context-menu">
            <td align="left" width="25%" class="ibn-sectionheader">
                FirstName
            </td>
            <td align="center" width="25%" class="ibn-sectionheader">
                LastName
            </td>
            <td align="center" width="25%" class="ibn-sectionheader">
                Email
            </td>
            <td align="center" width="10%" class="ibn-sectionheader">
                Total Amount
            </td>
            <td align="center" width="15%" class="ibn-sectionheader">
                Total orders
            </td>
            <td align="center" width="20%" class="ibn-sectionheader">
                Options
            </td>
        </tr>
        <asp:DataList runat="server" ID="dlordersList" RepeatLayout="Flow" RepeatDirection="Horizontal" OnItemDataBound="dlordersList_ItemDataBound">
            <ItemTemplate>
                <tr>
                    <td class="body" align="left" width="25%">
                        <%# DataBinder.Eval(Container.DataItem, "FirstName")%>
                    </td>
                    <td width="25%" align="center">
                        <%# DataBinder.Eval(Container.DataItem, "LastName")%>
                    </td>
                    <td width="25%" align="center">
                        <%# DataBinder.Eval(Container.DataItem, "Email")%>
                    </td>
                    <td width="10%" align="center">
                        <%# String.Format("${0:0.##}", Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "Password")))%>
                    </td>
                    <td width="15%" align="center">
                        <%# DataBinder.Eval(Container.DataItem, "UserTypeId")%>
                    </td>
                    <td nowrap align="center" width="10%">
                        <asp:HyperLink ID="hlView" runat="Server"><img title='Add States' src='/Content/images/cssecuresite/edit.gif' alt="View Details" border="0"></asp:HyperLink>&#160;
                    </td>
                </tr>
            </ItemTemplate>
        </asp:DataList>
        <br />
</asp:Content>
