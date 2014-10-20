<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PathItem.aspx.cs" Inherits="CSWeb.Admin.PathItem" MasterPageFile="AdminSite.master" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
   <title>Add/Edit Upsell Details</title>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
  <span id="pageid" class="upsell"></span>
    <div id="page-content">
<ul id="nav-info" class="clearfix">
<li><a href="main.aspx"><i class="icon-home"></i></a></li>
<li><i class="icon-star"></i> Catalog</li>
<li><a href="pathlist.aspx"><i class="icon-gift"></i> Upsell Path List</a></li>
<li><i class="icon-pencil"></i> Add/Edit Upsell Details</li>
</ul>
<h3 class="page-header page-header-top">Add/Edit Upsell Details
<div style="margin-left: 10px; display: inline; font-style: normal;"><asp:Label ID="Label1" runat="server" Text="Changes Saved!" Visible="false" CssClass="label label-success"></asp:Label>
<asp:Label ID="lblCancel" runat="server" Text="Changes Cancelled!" Visible="false" CssClass="label label-important"></asp:Label></div>
</h3>

    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" />
        
 <p><asp:ValidationSummary ID="valErrorSummary" runat="server" CssClass="text-error" ValidationGroup="valError" DisplayMode="List" /></p>

     <div class="form-horizontal form-box">  
      <h4 class="form-box-header">Upsell Details<div style="margin-left: 10px; display: inline; font-style: normal;"><asp:Label ID="lblSuccess" runat="server" Text="Changes Saved!" Visible="false" CssClass="label label-success"></asp:Label></div></h4>

            <div class="form-box-content">
       
<div class="control-group">
<label class="control-label">Title</label>
<div class="controls">
       <asp:TextBox ID="txtTitle" runat="Server" MaxLength="100" CssClass="input-xlarge" />
                <asp:RequiredFieldValidator runat="server" Display="None" ControlToValidate="txtTitle" ID="valReqTitle" CssClass="text-error" ValidationGroup="valError" ErrorMessage="Title is a required field.">*</asp:RequiredFieldValidator><span class="help-inline"><code>required</code></span>
           </div></div>
              
                
   <div class="control-group">
<label class="control-label">Weight</label>
<div class="controls">  
     <asp:TextBox ID="txtWeight" runat="Server" MaxLength="4" />
                <asp:RequiredFieldValidator runat="server" Display="None" ControlToValidate="txtWeight"  ID="valReqCode" ValidationGroup="valError" CssClass="text-error" ErrorMessage="Weight is a required field.">*</asp:RequiredFieldValidator>
       <asp:CompareValidator ID="cmpValorderNo" runat="server" ControlToValidate="txtWeight" Type="Double" ErrorMessage="Weight amount must be a double." Operator="DataTypeCheck" ValidationGroup="valError" CssClass="text-error" ValueToCompare="0">*</asp:CompareValidator><span class="help-inline"><code>required</code></span>
      </div></div>
      
                      
   <div class="control-group">
<label class="control-label">Version</label>
<div class="controls">  
<asp:ListBox ID="lstVersion" runat="server" DataValueField="SkuId" SelectionMode="multiple" TabIndex="9"  />
          </div></div>
          
          <div class="control-group">
<label class="control-label">Templates</label>
<div class="controls">  
  <table class="table table-bordered table-striped">
                    <thead>
                    <tr>
                        <th class="span1 text-center">
                            Active
                        </th>
                        <th>
                            Title
                        </th>
                        <th class="span1 text-center">
                            Order
                        </th>
                    </tr>
                    </thead>
                    <tbody>
                    <asp:DataList runat="server" ID="dlTemplateList" RepeatLayout="Flow" RepeatDirection="Horizontal" OnItemDataBound="dlTemplateList_ItemDataBound">
                        <ItemTemplate>
                            <tr id="holderExpireDate" runat="server">
                                <td class="span1 text-center">
                                    <asp:CheckBox ID="cbVisible" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Active") %>' />
                                </td>
                                <td>
                                    <%# DataBinder.Eval(Container.DataItem, "Title") %>
                                </td>
                                <td class="span1 text-center">
                                    <asp:TextBox runat="server" ID="txtOrderNo" MaxLength="2" Text='<%# DataBinder.Eval(Container.DataItem, "OrderNo") %>' CssClass="input-mini text-center"></asp:TextBox>
                                    <asp:CompareValidator ID="cmpValorderNo" runat="server" ControlToValidate="txtOrderNo" Type="Integer" ErrorMessage="Order number must be an integer." Operator="DataTypeCheck" ValidationGroup="valError" Display="Dynamic" CssClass="text-error" ValueToCompare="0">*</asp:CompareValidator>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:DataList>
          </tbody>
          </table>
          </div></div>
    
 <div class="form-actions">
      <asp:LinkButton runat="server" ID="btnCancel" CommandName="Cancel" CausesValidation="false" OnCommand="btnAction_Command" CssClass="btn btn-danger"><i class="icon-ban-circle"></i> Cancel</asp:LinkButton> <asp:LinkButton runat="server" ID="btnSave" CommandName="Save" OnCommand="btnAction_Command" CssClass="btn btn-success" CausesValidation="true" ValidationGroup="valError"><i class="icon-save"></i> Save</asp:LinkButton>
</div>

    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>

 </div>
    </div>
</div>

</asp:Content>
