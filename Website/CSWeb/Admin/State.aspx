<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="State.aspx.cs" Inherits="CSWeb.Admin.State"
    MasterPageFile="AdminSite.master" %>

<%@ Import Namespace="CSBusiness" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <title>Edit State Information</title>

</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
       <span id="pageid" class="country"></span>
    <div id="page-content">
<ul id="nav-info" class="clearfix">
<li><a href="main.aspx"><i class="icon-home"></i></a></li>
<li><i class="icon-star"></i> Catalog</li>
<li><a href="countrylist.aspx"><i class="icon-globe"></i> Country</a></li>
<li><i class="icon-pencil"></i> Edit State Information</li>
</ul>
<h3 class="page-header page-header-top">
   Edit State Information</h3>
   
   <p><asp:Label ID="lblSuccess" runat="server" Text="Changes Saved!" Visible="false" CssClass="label label-success"></asp:Label>
<asp:Label ID="lblCancel" runat="server" Text="Changes Cancelled!" Visible="false" CssClass="label label-important"></asp:Label></p>
   
   
   <div class="form-inline">
<label for="checkSelectAll" class="checkbox"><input type="checkbox" name='checkSelectAll' id='checkSelectAll' /> Select All/Unselect All</label>
    </div>
    <table class="table table-bordered table-striped">
       <thead>
        <tr>
            <th class="span1 text-center">
                Active
            </th>
            <th class="span1 text-center">
                Order
            </th>
            <th>
                State
            </th>
            <th>
                Code
            </th>
            
        </tr>
        </thead>
        <tbody>
        <asp:DataList runat="server" ID="dlStateList" RepeatLayout="Flow" RepeatDirection="Horizontal">
            <ItemTemplate>
                <tr>
                    <td class='stateCheckboxHolder span1 text-center'>
                        <asp:CheckBox ID="cbVisible" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Visible") %>' />
                    </td>
                     <td class="span1 text-center">
                        <asp:TextBox runat="server" ID='txtOrderNo' MaxLength="2" CssClass="input-mini" Text='<%# DataBinder.Eval(Container.DataItem, "DisplayOrder") %>'></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblTitle" runat="Server" Text='<%# DataBinder.Eval(Container.DataItem, "Name") %>' />
                    </td>
                    <td>
                        <asp:Label ID="lblCode" runat="Server" Text='<%# DataBinder.Eval(Container.DataItem, "Abbreviation") %>' />
                    </td>
                   
                </tr>
            </ItemTemplate>
        </asp:DataList>
        </tbody>
          </table>
          
          <p>
                <asp:LinkButton runat="server" ID="btnCancel" CssClass="btn btn-danger" CommandName="Cancel" OnCommand="btnSave_Command"><i class="icon-ban-circle"></i> Cancel</asp:LinkButton>
                <asp:LinkButton runat="server" ID="btnSave" CommandName="Save" CssClass="btn btn-success" OnCommand="btnSave_Command"><i class="icon-save"></i> Save</asp:LinkButton>
           </p>
 
    

    </div>
</asp:Content>
