<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="CSWeb.Admin.Users" MasterPageFile="AdminSite.master"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
    <asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
<title>Administrators</title>
    </asp:Content>

    

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
  <span id="pageid" class="admins"></span>
    <div id="page-content">
<ul id="nav-info" class="clearfix">
<li><a href="main.aspx"><i class="icon-home"></i></a></li>
<li><i class="icon-group"></i> Users</li>
<li><i class="icon-key"></i> Administrators</li>
</ul>
<h3 class="page-header page-header-top">Administrators</h3>  
       
    
    <asp:Panel ID="pnlSearch" runat="server">
 
      <div class="well push">
    <div class="form-inline">

                <asp:TextBox  AutoCompleteType="Disabled" ID="txtFirstName" runat="server" MaxLength="50" EnableViewState="True" CssClass="input-medium" placeholder="First Name" />
        &nbsp;&nbsp;

        <asp:TextBox  AutoCompleteType="Disabled" ID="txtLastName" runat="server" placeholder="Last Name" MaxLength="50" EnableViewState="True" CssClass="input-medium" />
        &nbsp;&nbsp;
        
                <asp:TextBox  AutoCompleteType="Disabled" ID="txtEmail" runat="server" MaxLength="50" placeholder="Email Address" EnableViewState="True" CssClass="input-large" />
           &nbsp;&nbsp;
           
                <asp:LinkButton ID="lblSearch" runat="server" CommandName="Search" OnClick="lblCustomer_Search" CssClass="btn btn-primary"><i class="icon-search"></i> Search</asp:LinkButton>
            </div>
    </div>
    </asp:Panel>

<div class="push">
    <asp:HyperLink ID="hlAddUser" runat="server" CssClass="btn btn-success" NavigateURL="UserEdit.aspx"><i class="icon-plus"></i> Create New Administrator</asp:HyperLink>
</div>
        <table class="table table-bordered table-striped">
       <thead>
         <tr>
            <th>
                Name
            </th>
            <th>
                Email
            </th>
            <th>
                Date Created
            </th>
              <th class="span2 text-center">
                Options
            </th>
        </tr>
 </thead>
 <tbody>
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
                <td class="span2 text-center"><div class="btn-group">
                     <asp:HyperLink ID="hlEditCust" runat="server" CssClass="btn btn-mini btn-success" CausesValidation="False" NavigateURL='<%# "UserEdit.aspx?CustId=" + DataBinder.Eval(Container.DataItem, "CustomerId") %>' ToolTip="Edit"><i class="icon-pencil"></i></asp:HyperLink> 
                     <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-mini btn-info" CausesValidation="False" NavigateURL="#" data-toggle="tooltip" Title='<%# DataBinder.Eval(Container.DataItem, "Password") %>'><i class="icon-info-sign"></i> View Password</asp:HyperLink>
               
             </div>
                </td>
            </tr>
            
        </ItemTemplate>
    </asp:DataList>
    </tbody>
    </table>
    
    
    </div>
</asp:Content>
