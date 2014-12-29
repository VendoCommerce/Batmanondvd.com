<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Attributes.ascx.cs" Inherits="CSWeb.Admin.UserControls.Attributes" %>

<script type="text/javascript">

    var attrFieldChanged = function (addchkId) {
        document.getElementById(addchkId).checked = true;
    }

    var deleteCheckChanged = function (chkId, txtCtlId, ddlCtlId) {
        var checked = document.getElementById(chkId).checked;

        if (document.getElementById(txtCtlId) != null)
            document.getElementById(txtCtlId).disabled = checked;

        if (document.getElementById(ddlCtlId) != null)
            document.getElementById(ddlCtlId).disabled = checked;
    }

</script>

<asp:Repeater ID="rptAttributes" runat="server" OnItemDataBound="rptAttributes_ItemDataBound">
    <HeaderTemplate>
        <table class="table" style="width: <%= WidthTotal.ToString() %>px;">
        <thead>
            <tr>
                <th>Name</th>
                <th>Value</th>    
                <th>Delete</th>
                <th>Add</th>       
            </tr>
            </thead>    <tbody>
    </HeaderTemplate>
    <ItemTemplate>

        <tr>
            <td>
                <asp:Label ID="lblAttributeName" runat="server" />
                <asp:HiddenField ID="hidSqlDbType" runat="server" />
            </td>
            <td>
                <asp:TextBox  AutoCompleteType="Disabled" ID="txtAttributeValue" runat="server" Visible="false" />

                <asp:DropDownList ID="ddlYesNo" runat="server" Visible="false">
                    <asp:ListItem Value="" Selected="True">Select One</asp:ListItem>
                    <asp:ListItem Value="true">Yes</asp:ListItem>
                    <asp:ListItem Value="false">No</asp:ListItem>
                </asp:DropDownList>

            </td>
            <td class="text-center">
                <asp:CheckBox ID="chkDelete" runat="server" Visible="false" />
            </td>
            <td class="text-center">
                <asp:CheckBox ID="chkAdd" runat="server" Visible="false" />
            </td>
        </tr>

    </ItemTemplate>
    <FooterTemplate>
      </tbody>  </table>
    </FooterTemplate>
</asp:Repeater>

