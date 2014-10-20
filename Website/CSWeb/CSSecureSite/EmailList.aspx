<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmailList.aspx.cs" Inherits="CSWeb.Admin.EmailList" MasterPageFile="AdminSite.master"%>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
   <title>Email Manager</title>
   </asp:Content>
   
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<span id="pageid" class="emailmanager"></span>
    <div id="page-content">
<ul id="nav-info" class="clearfix">
<li><a href="main.aspx"><i class="icon-home"></i></a></li>
<li><i class="icon-star"></i> Catalog</li>
<li><i class="icon-envelope-alt"></i> Email Manager</li>
</ul>
<h3 class="page-header page-header-top">Email Template Manager</h3>
      <div class="push">
 <asp:HyperLink ID="hlAddEmail" runat="server" CssClass="btn btn-success" NavigateUrl="EmailItem.aspx"><i class="icon-plus"></i> Add Email Template</asp:HyperLink>
        </div>
    <table class="table table-striped table-bordered">
        <thead>
        <tr>
            <th>
                Title
            </th>
            <th>
                Subject
            </th>
            <th>
                From Address
            </th>
            <th>
                Body
            </th>            
            <th>
                Options
            </th>
        </tr>
        </thead>
        <tbody>
        
        <asp:DataList runat="server" ID="dlEmailList" RepeatLayout="Flow" RepeatDirection="Horizontal" OnItemCommand="dlEmailList_ItemCommand"  OnItemDataBound="dlEmailList_ItemDataBound">
            <ItemTemplate>
                <tr>
                 
                    <td>
                        <%# DataBinder.Eval(Container.DataItem, "Title")%>
                    </td>
                    <td>
                        <%# DataBinder.Eval(Container.DataItem, "Subject")%>
                    </td>
                    <td>
                         <%# DataBinder.Eval(Container.DataItem, "FromAddress")%>
                    </td>
                    <td>
                         <asp:Label ID="lblBody" runat="server" />
                    </td>
                    
                    <td>
                          <div class="btn-group">
                        <asp:LinkButton CssClass="btn btn-mini btn-success" ID="lbSave" runat="server" CausesValidation="True" CommandName="Edit" ToolTip="Edit"><i class="icon-pencil"></i></asp:LinkButton>
                        <asp:LinkButton ID="lbRemove" runat="server" CausesValidation="False" CommandName="Delete" CssClass="btn btn-mini btn-danger" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this email template?')"><i class="icon-remove"></i></asp:LinkButton>
                        </div>
                    </td>
                </tr>
            </ItemTemplate>
           
        </asp:DataList>
 </tbody>      
      
    </table>
 </div>
</asp:Content>
