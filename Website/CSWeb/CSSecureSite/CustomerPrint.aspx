<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerPrint.aspx.cs" Inherits="CSWeb.Admin.CustomerPrint" %>

<!doctype html>
<html>
<head runat="server">
    <title>Customer Print List</title>
</head>
<body onload="javascript:window.print();">
    <form id="form1" runat="server">
      <table width="100%" border="0" cellspacing="1" cellpadding="2" style="font-family: arial, sans-serif">
  
        <tr>
            <td class="title" colspan="3">
           <strong>     Customers</strong>
            </td>
        </tr>
         <tr>
            <td>
                <em><strong>Name</strong></em>
            </td>
            <td>
               <em><strong> Email</strong></em>
            </td>
            <td>
               <em><strong> Date Created</strong></em>
            </td>

        </tr>
 
    <asp:DataList runat="server" ID="dlCustomerList" RepeatLayout="Flow" RepeatDirection="Horizontal">
           <ItemTemplate>
            <tr>
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "FullName") %>
                </td>
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "Email") %>
                </td>
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "RegistrationDate") %>
                </td>
            </tr>
        </ItemTemplate>
    </asp:DataList>
    </table>

    </form>
</body>
</html>
