<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentProviderList.aspx.cs"
    Inherits="CSWeb.Admin.PaymentProviderList" MasterPageFile="AdminSite.master"
    EnableViewState="true" %>

<%@ Register TagPrefix="Cs" Namespace="CSCore.Common" Assembly="CSCore" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
   <title>Payment Providers</title>
   </asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
 <span id="pageid" class="paymentprovider"></span>
    <div id="page-content">
<ul id="nav-info" class="clearfix">
<li><a href="main.aspx"><i class="icon-home"></i></a></li>
<li><i class="icon-star"></i> Catalog</li>
<li><i class="icon-road"></i> Payment Provider</li>
</ul>
<h3 class="page-header page-header-top">Payment Provider
<div style="margin-left: 10px; display: inline; font-style: normal;">
<asp:Label ID="lblSuccess" runat="server" Text="Changes Saved!" Visible="false" CssClass="label label-success"></asp:Label>
<asp:Label ID="lblCancel" runat="server" Text="Changes Cancelled!" Visible="false" CssClass="label label-important"></asp:Label>
</div>
</h3>

     <div class="row-fluid" style="margin-bottom: 12px">
<asp:ValidationSummary ID="valErrorSummary" runat="server" CssClass="text-error"
        ValidationGroup="valError" DisplayMode="List" />
 </div>
   <div class="push">
 <asp:LinkButton ID="lbItemAdd" runat="server" CssClass="btn btn-success" OnCommand="btnAction_Command" CommandName="AddNew"><i class="icon-plus"></i> Add Payment Provider</asp:LinkButton>
      </div>
      
          
    <table class="table table-bordered table-striped">
      <thead>
        <tr>
            <th class="span1 text-center">
                Active
            </th>
            <th class="span3">
                Title
            </th>
            <th>
                Configuration
            </th>
            <th class="span1 text-center">
                Default
            </th>
            <th class="span1 text-center">
                Options
            </th>
        </tr>
        </thead>
        <tbody>
        <asp:DataList runat="server" ID="dlProviderList" RepeatLayout="Flow" RepeatDirection="Horizontal" OnItemCommand="dlProviderList_ItemCommand"  OnItemDataBound="dlProviderList_ItemDataBound">
            <ItemTemplate>
                <tr>
                    <td class="span1 text-center">
                        <asp:CheckBox ID="cbVisible" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Active") %>' />
                    </td>
                    <td class="span3">
                        <%# DataBinder.Eval(Container.DataItem, "Title") %>
                    </td>
                    <td>
                        <asp:TextBox  AutoCompleteType="Disabled" ID="txtConfig" runat="server" TextMode="MultiLine" CssClass="textarea-large" Rows="5" ReadOnly="true" Text='<%#  DataBinder.Eval(Container.DataItem, "ProviderXML")%>' />
                    </td>
                    <td class="span1 text-center">
                        <Cs:GroupRadioButton ID="rbAlign" runat="server" GroupName="Alignment" onclick='selectRow(this);' />
                    </td>
                    <td class="span1 text-center"><div class="btn-group">
                        <asp:LinkButton CssClass="btn btn-mini btn-success" ID="lbEdit" runat="server" CausesValidation="False" CommandName="Edit" ToolTip="Edit"><i class="icon-pencil"></i></asp:LinkButton>
                        <asp:LinkButton ID="lbRemove" runat="server" CausesValidation="False" CommandName="Delete" CssClass="btn btn-mini btn-danger" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this payment gateway?')"><i class="icon-remove"></i></asp:LinkButton>
                        </div>
                    </td>
                </tr>
            </ItemTemplate>
            <EditItemTemplate>
                <tr>
                    <td class="span1 text-center"></td>
                    <td class="span3">
                        <asp:TextBox  AutoCompleteType="Disabled" ID="txtEditTitle" runat="server" MaxLength="100" Text='<%#DataBinder.Eval(Container.DataItem,"Title")%>' />
                        <asp:RequiredFieldValidator ID="valEditName" runat="server" Display="Dynamic" ErrorMessage="Title is a required field." ControlToValidate="txtEditTitle">*</asp:RequiredFieldValidator>
               
                    </td>
                    <td>
                        <asp:TextBox  AutoCompleteType="Disabled" ID="txtEditConfig" runat="server" TextMode="MultiLine"  CssClass="textarea-large" Rows="5" Text='<%#  DataBinder.Eval(Container.DataItem, "ProviderXML")%>' /><asp:RequiredFieldValidator ID="rfvEditConfig" runat="server" Display="Dynamic" ErrorMessage="Configuration is a required field." ControlToValidate="txtEditConfig">*</asp:RequiredFieldValidator>
                    </td>
                    <td class="span1 text-center"></td>
                    <td class="span1 text-center">
                       <div class="btn-group">
                        <asp:LinkButton CssClass="btn btn-mini btn-success" ID="lbSave" runat="server" CausesValidation="True" CommandName="Update" ToolTip="Save"><i class="icon-save"></i></asp:LinkButton>
                        <asp:LinkButton  CssClass="btn btn-mini btn-danger" ID="lbCancel" runat="server" CausesValidation="False" CommandName="Cancel" ToolTip="Cancel"><i class="icon-ban-circle"></i></asp:LinkButton>
                        </div>
                    </td>
                </tr>
            </EditItemTemplate>
        </asp:DataList>
        <asp:PlaceHolder ID="pnlAddCategory" runat="server" Visible="False">
            <tr>
                <td class="span1 text-center">
                </td>
                <td class="span3">
                    <asp:RequiredFieldValidator ID="valAddName" runat="server" Display="Dynamic" ErrorMessage="Title is a required field." ControlToValidate="txtTitle">*</asp:RequiredFieldValidator>
                    <asp:TextBox  AutoCompleteType="Disabled" ID="txtTitle" runat="server" MaxLength="100" />
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ErrorMessage="Configuration is a required field." ControlToValidate="txtConfig">*</asp:RequiredFieldValidator>
                    <asp:TextBox  AutoCompleteType="Disabled" ID="txtConfig" runat="server" TextMode="MultiLine" CssClass="textarea-large" Rows="5" />
                </td>
                <td class="span1 text-center">
                   
                </td>
                <td class="span1 text-center"><div class="btn-group">
                    <asp:LinkButton ID="lbSave" CssClass="btn btn-mini btn-success" runat="server" CommandName="Add" OnCommand="btnAction_Command" CausesValidation="True" ToolTip="Save"><i class="icon-save"></i></asp:LinkButton>
                    <asp:LinkButton ID="lbCancel" CssClass="btn btn-mini btn-danger" runat="server" CommandName="Cancel" OnCommand="btnAction_Command" CausesValidation="False" ToolTip="Cancel"><i class="icon-ban-circle"></i></asp:LinkButton>
                    </div>
                </td>
            </tr>
        </asp:PlaceHolder>
        </tbody>
         </table>
         
         <p>
           <asp:LinkButton ID="imgSave" CssClass="btn btn-success" OnCommand="btnSave_OnClick" CommandName="Save" runat="server" ValidationGroup="valError"><i class="icon-save"></i> Save</asp:LinkButton>
          </p>
  
</div>
</asp:Content>
