<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Country.aspx.cs" Inherits="CSWeb.Admin.Country"
    MasterPageFile="AdminSite.master" EnableSessionState="True" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <title>Add Country</title>
 
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
       <span id="pageid" class="country"></span>
    <div id="page-content">
<ul id="nav-info" class="clearfix">
<li><a href="main.aspx"><i class="icon-home"></i></a></li>
<li><i class="icon-star"></i> Catalog</li>
<li><a href="countrylist.aspx"><i class="icon-globe"></i> Country</a></li>
<li><i class="icon-plus"></i> Add Country</li>
</ul>
<h3 class="page-header page-header-top">
   Add Country</h3> 

    <p>
     <asp:ValidationSummary ID="valErrorSummary" runat="server" CssClass="text-error"
        ValidationGroup="valError" DisplayMode="List" />
       </p>
       

   <div class="form-horizontal form-box">
 <h4 class="form-box-header">Select Country <div style="margin-left: 10px; display: inline; font-style: normal;"><asp:Label ID="lblSuccess" runat="server" Text="Changes Saved!" Visible="false" CssClass="label label-success"></asp:Label>
<asp:Label ID="lblCancel" runat="server" Text="Changes Cancelled!" Visible="false" CssClass="label label-important"></asp:Label></div></h4>
<div class="form-box-content">

<div class="control-group">
<div class="controls">
<asp:RequiredFieldValidator runat="server" Display="None" ControlToValidate="ddlProducts" ID="valReqTitle" ValidationGroup="valError" ErrorMessage="Country is a required field.">*</asp:RequiredFieldValidator>
 <asp:DropDownList ID="ddlProducts" runat="server"></asp:DropDownList>
 <asp:Button ID="btnAddCountry" runat="server" Text="Add Country" OnClick="btn_AddCountry" CausesValidation="true" ValidationGroup="valError" CssClass="btn btn-success" />
    </div></div>
    </div></div>
    
    
    <asp:Repeater ID="rptItems" runat="server" OnItemCommand="dlrepeater_ItemCommand">
        <HeaderTemplate>
            <table class="table table-striped table-bordered">
            <thead>
                <tr>
                    <th id="holderOrderHeader" class="span1 text-center">
                        Remove
                    </th>
                    <th>
                        Country
                    </th>
                </tr>
                </thead>
                <tbody>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td class="span1 text-center">
                    <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="False" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "CountryId") %>' CssClass="btn btn-danger btn-mini"><i class="icon-remove"></i></asp:LinkButton>
                </td>
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "Name") %>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
        </tbody>
            </table>
            </FooterTemplate>
    </asp:Repeater>

 <p>
      <asp:LinkButton runat="server" ID="btnCancel" CommandName="Cancel" OnCommand="btnSave_Command" CausesValidation="false" CssClass="btn btn-danger" ValidationGroup="valError"><i class="icon-ban-circle"></i> Cancel</asp:LinkButton><asp:LinkButton runat="server" ID="btnSave" CssClass="btn btn-success" CommandName="Save" OnCommand="btnSave_Command"><i class="icon-save"></i> Save</asp:LinkButton>
    </p>     
    </div>
         
</asp:Content>
