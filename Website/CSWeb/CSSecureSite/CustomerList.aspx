<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerList.aspx.cs" Inherits="CSWeb.Admin.CustomerList" MasterPageFile="AdminSite.master" %>
<%@ Register TagPrefix="usercontrols1" TagName="paging" Src="UserControls/PageControl.ascx" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
   <title>Customers</title>
    <script language="javascript">
        function PrintpopUp(url, width, height) {
            var newWindow;
            newWindow = window.open(url, "Categories", "height=300,width=800,resizable=yes,status=no,scrollbars=yes,dependent=yes,toolbar=no,menubar=yes,location=no,left=0,top=0");
            newWindow.focus();
        }
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
   <span id="pageid" class="customers"></span>
    <div id="page-content">
<ul id="nav-info" class="clearfix">
<li><a href="main.aspx"><i class="icon-home"></i></a></li>
<li><i class="icon-group"></i> Users</li>
<li><i class="icon-user"></i> Customers</li>
</ul>
<h3 class="page-header page-header-top">Customers</h3>  
    
<div class="well push">
    <div class="form-inline">
   
        <asp:TextBox  AutoCompleteType="Disabled" ID="txtFirstName" runat="server" MaxLength="50" EnableViewState="True" CssClass="input-medium" placeholder="First Name" />
        &nbsp;&nbsp;

        
                <asp:TextBox  AutoCompleteType="Disabled" ID="txtLastName" runat="server" MaxLength="50" EnableViewState="True" CssClass="input-medium" placeholder="Last Name" />
                
             &nbsp;&nbsp;
         
                <asp:TextBox  AutoCompleteType="Disabled" ID="txtEmail" runat="server" MaxLength="50" EnableViewState="True" CssClass="input-large" placeholder="Email Address" />
           &nbsp;&nbsp;
 <asp:LinkButton ID="btnSearch" runat="server" CommandName="Search" OnClick="btnSearch_Command" CssClass="btn btn-primary"><i class="icon-search"></i> Search</asp:LinkButton>
    </div>
    </div>
<div class="row-fluid">
<div class="pull-left span7">
        <p><asp:HyperLink ID="hlprint" Text="Print" runat="Server" NavigateUrl="javascript:PrintpopUp('CustomerPrint.aspx?print=1', 550,500)" CssClass="btn"><i class="icon-print text-info"></i> Print List</asp:HyperLink> 
                <asp:HyperLink ID="hlexport" Text="Export to Excel" runat="Server" NavigateUrl="javascript:PrintpopUp('CustomerPrint.aspx?print=2', 550,500)" CssClass="btn"><i class="icon-download text-success"></i> Download to Excel</asp:HyperLink>
      </p>
</div>
<div class="pull-right span5">
 <asp:UpdatePanel ID="updPg" runat="server" UpdateMode="Conditional" class="pull-right">
                    <ContentTemplate>
                        <usercontrols1:paging ID="pg" OnPageChanged="OnPaging" Mode="Links" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>

  </div>
  </div>
  
    <asp:UpdatePanel ID="updList" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
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
                        </tr>
                    </ItemTemplate>
                </asp:DataList>
                
                  </tbody>
    </table>
        </ContentTemplate>
    </asp:UpdatePanel>


    </div>
</asp:Content>
