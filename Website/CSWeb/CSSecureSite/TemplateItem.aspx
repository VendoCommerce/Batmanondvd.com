<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TemplateItem.aspx.cs" Inherits="CSWeb.Admin.TemplateItem" EnableViewState="true" MasterPageFile="AdminSite.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="HTMLEditor" %>
<%@ Register TagPrefix="usercontrols1" TagName="DateControl" Src="usercontrols/DateControl.ascx" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <title>Add/Edit Upsell Templates</title>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager" runat="server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" />
     <span id="pageid" class="upsell"></span>
    <div id="page-content">
<ul id="nav-info" class="clearfix">
<li><a href="main.aspx"><i class="icon-home"></i></a></li>
<li><i class="icon-star"></i> Catalog</li>
<li><a href="pathlist.aspx"><i class="icon-gift"></i> Upsell Path List</a></li>
<li><a href="templatelist.aspx"><i class="icon-list-alt"></i> Upsell Templates</a></li>
<li><i class="icon-pencil"></i> Add/Edit Upsell Templates</li>
</ul>
<h3 class="page-header page-header-top">Upsell Templates
<div style="margin-left: 10px; display: inline; font-style: normal;"><asp:Label ID="Label1" runat="server" Text="Changes Saved!" Visible="false" CssClass="label label-success"></asp:Label>
<asp:Label ID="lblCancel" runat="server" Text="Changes Cancelled!" Visible="false" CssClass="label label-important"></asp:Label></div>
</h3>      
  <p>
<asp:ValidationSummary ID="valErrorSummary" runat="server" CssClass="text-error" ValidationGroup="valError" DisplayMode="List" />
</p>

     <div class="form-horizontal form-box">  
      <h4 class="form-box-header">Upsell Template Details<div style="margin-left: 10px; display: inline; font-style: normal;"><asp:Label ID="lblSuccess" runat="server" Text="Changes Saved!" Visible="false" CssClass="label label-success"></asp:Label></div></h4>

            <div class="form-box-content">
       
<div class="control-group">
<label class="control-label"><asp:Label ID="lblTitle" runat="Server" AssociatedControlID="txtName" Text="Name" /></label>
<div class="controls">
 <asp:TextBox ID="txtName" runat="server" MaxLength="200" TabIndex="2" CssClass="input-xlarge"/>
                <asp:RequiredFieldValidator ID="rfvName" runat="server" Text="*" ErrorMessage="Name is a required field." ControlToValidate="txtName" ValidationGroup="valError"/><span class="help-inline"><code>required</code></span>
                </div></div>
           
            <div class="control-group">
<label class="control-label">SKU(s)</label>
<div class="controls">    
      <asp:ListBox ID="lstSku" runat="server" DataTextField="Name" DataValueField="SkuId" SelectionMode="multiple" TabIndex="1" CssClass="input-xlarge" />
                <asp:RequiredFieldValidator ID="rfvSku" runat="server" Text="*" ErrorMessage="SKU(s) is a required field." ControlToValidate="lstSku" InitialValue="" ValidationGroup="valError" /><span class="help-inline"><code>required</code></span>
        </div></div>
        
                   
    <div class="control-group">
<label class="control-label">Description</label>
<div class="controls">   
<HTMLEditor:Editor ID="ftbShortDesc" runat="server" 
							Height="400px" 
							Width="700"
							AutoFocus="true"
					/>
<asp:RequiredFieldValidator runat="server" Display="None" ControlToValidate="ftbShortDesc" ID="valReqCartDesc" ValidationGroup="valError" ErrorMessage="Description is a required field.">*</asp:RequiredFieldValidator><span class="help-inline"><code>required</code></span>
      </div></div>
      
      <div class="control-group">
<label class="control-label">Script</label>
<div class="controls">     
<asp:TextBox TextMode="MultiLine" ID="txtScript" runat="server" CssClass="input-xlarge" />
			</div></div>
            
            
                        
    <div class="control-group">
<label class="control-label">Tags</label>
<div class="controls">               
          
                <asp:TextBox ID="txtTag" runat="server" TextMode="MultiLine" TabIndex="4" CssClass="input-xlarge" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Text="*" ErrorMessage="Tags is a required field." ControlToValidate="txtName" ValidationGroup="valError" /><span class="help-inline"><code>required</code></span>
        </div></div>
        
        
          <div class="control-group">
<label class="control-label">Expiration Date</label>
<div class="controls">   
<usercontrols1:DateControl ID="dateControlStart" runat="server" />
</div></div>

                 <div class="control-group">
<label class="control-label">Trigger Item(s)</label>
<div class="controls">   
<asp:ListBox ID="lstTriggerItem" runat="server" CssClass="input-xlarge" DataValueField="SkuId" SelectionMode="multiple" TabIndex="6" />
</div></div>
          
                   <div class="control-group">
<label class="control-label">Suppress Item(s)</label>
<div class="controls">         
<asp:ListBox ID="lstSuppressItem" runat="server" CssClass="input-xlarge" DataValueField="SkuId" SelectionMode="multiple" TabIndex="7"  />
       </div></div>
       
               <div class="control-group">
<label class="control-label">Remove Item(s)</label>
<div class="controls">      
  <asp:ListBox ID="lstRemoveItems" runat="server" CssClass="input-xlarge" DataValueField="SkuId" SelectionMode="multiple" TabIndex="8"  />
     </div></div>  
                
                
                   <div class="control-group">
<label class="control-label">Add Item(s)</label>
<div class="controls">    
<asp:ListBox ID="lstAddItems" runat="server" CssClass="input-xlarge" DataValueField="SkuId" SelectionMode="multiple" TabIndex="9"  />
  </div></div>
  
                   <div class="control-group">
<label class="control-label">Disable Shipping States</label>
<div class="controls">  
 <asp:ListBox ID="lstDisableStates" runat="server" CssClass="input-xlarge" SelectionMode="multiple" />   
 </div></div>
 
        <div class="control-group">
<label class="control-label">URI Label</label>
<div class="controls">  
<asp:TextBox ID="txtURILabel" runat="server" TabIndex="10" MaxLength="200" CssClass="input-xlarge" />
</div></div>
          <div class="form-actions">
                <asp:LinkButton runat="server" CssClass="btn btn-danger" ID="btnCancel" CommandName="Cancel" CausesValidation="false" OnCommand="btnAction_Command" TabIndex="10"><i class="icon-ban-circle"></i> Cancel</asp:LinkButton> <asp:LinkButton runat="server" ID="btnSave" CssClass="btn btn-success" CommandName="Save" OnCommand="btnAction_Command" CausesValidation="true" ValidationGroup="valError" TabIndex="11"><i class="icon-save"></i> Save</asp:LinkButton>
          </div>
           </div>
    </div>
</div>  
</asp:Content>
