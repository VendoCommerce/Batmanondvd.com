<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CountryList.aspx.cs" Inherits="CSWeb.Admin.CountryList" MasterPageFile="AdminSite.master" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <title>Country</title>
    </asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
      <span id="pageid" class="country"></span>
    <div id="page-content">
<ul id="nav-info" class="clearfix">
<li><a href="main.aspx"><i class="icon-home"></i></a></li>
<li><i class="icon-star"></i> Catalog</li>
<li><i class="icon-globe"></i> Country</li>
</ul>
<h3 class="page-header page-header-top">
   Country</h3> 
   <p><asp:Label ID="lblSuccess" runat="server" Text="Changes Saved!" Visible="false" CssClass="label label-success"></asp:Label>
<asp:Label ID="lblCancel" runat="server" Text="Changes Cancelled!" Visible="false" CssClass="label label-important"></asp:Label></p>
  <p>
    <asp:ValidationSummary ID="valErrorSummary" runat="server" CssClass="text-error"
        ValidationGroup="valError" DisplayMode="List" />
    </p>
    
    <div class="push">
    <asp:HyperLink ID="lbItemAdd" runat="server" CssClass="btn btn-success" NavigateUrl="Country.aspx"><i class="icon-plus"></i> Add a Country</asp:HyperLink>
         </div>
         
    <table class="table table-bordered table-striped">
     <thead>
        <tr>
            <th>
                Country
            </th>
            <th class="span2 text-center">
                Active
            </th>
            <th class="span2 text-center">
                Order
            </th>
            <th class="span2 text-center">
                Options
            </th>
        </tr>
        </thead>
        <tbody>
        <asp:DataList runat="server" ID="dlCountryList" RepeatLayout="Flow" RepeatDirection="Horizontal"
            OnItemDataBound="dlCountryList_ItemDataBound">
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:Literal runat="server" ID='lblTitle'></asp:Literal>
                    </td>
                    <td class="span2 text-center">
                        <asp:CheckBox ID="cbVisible" runat="server" AutoPostBack="false" />
                    </td>
                    <td class="span2 text-center">
                        <asp:TextBox runat="server" ID='txtOrderNo' MaxLength="2" CssClass="input-mini"></asp:TextBox>
                        <asp:CompareValidator ID="cmpValorderNo" runat="server" ControlToValidate="txtOrderNo" Type="Integer" ErrorMessage="Order must be an integer." Display="Dynamic" Operator="DataTypeCheck" ValidationGroup="valError" CssClass="text-error" ValueToCompare="0">*</asp:CompareValidator>
                    </td>
                    <td class="span2 text-center">
                        <asp:HyperLink ID="hlAddState" runat="Server" CssClass="btn btn-info btn-mini"><i class="icon-pencil"></i> Edit States</asp:HyperLink>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:DataList>
        </tbody>
        </table>
        <p>
      <asp:LinkButton ID="imgSave" OnCommand="btnSave_OnClick" CommandName="Save" runat="server" ValidationGroup="valError" CssClass="btn btn-success"><i class="icon-save"></i> Save</asp:LinkButton>
   </p>
   
</div>
</asp:Content>
