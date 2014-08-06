<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmailItem.aspx.cs" Inherits="CSWeb.Admin.EmailItem"  EnableViewState="true" MasterPageFile="AdminSite.master"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="HTMLEditor" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <title>Add/Edit Email Template</title>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
 <span id="pageid" class="emailmanager"></span>
    <div id="page-content">
<ul id="nav-info" class="clearfix">
<li><a href="main.aspx"><i class="icon-home"></i></a></li>
<li><i class="icon-star"></i> Catalog</li>
<li><a href="emaillist.aspx"><i class="icon-envelope-alt"></i> Email Manager</a></li>
<li><i class="icon-pencil"></i> Add/Edit Email Template</li>
</ul>
<h3 class="page-header page-header-top">Add/Edit Email Template</h3>

    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" />
        
  <p>      
    <asp:ValidationSummary ID="valErrorSummary" runat="server" CssClass="text-error"
        ValidationGroup="valError" DisplayMode="List" />
</p>

   <div class="form-horizontal form-box">  
        
        
         <h4 class="form-box-header">Email Template Details
<div style="margin-left: 10px; display: inline; font-style: normal;"><asp:Label ID="lblSuccess" runat="server" Text="Changes Saved!" Visible="false" CssClass="label label-success"></asp:Label>
<asp:Label ID="lblCancel" runat="server" Text="Changes Cancelled!" Visible="false" CssClass="label label-important"></asp:Label></div></h4>

       <div class="form-box-content">
       
<div class="control-group">
<label class="control-label">Name</label>
<div class="controls"><asp:TextBox ID="txtName" runat="Server" MaxLength="100" CssClass="input-large" />
                <asp:RequiredFieldValidator runat="server" Display="None" ControlToValidate="txtName" ID="valReqTitle" CssClass="text-error" ValidationGroup="valError" ErrorMessage="Name is a required field.">*</asp:RequiredFieldValidator><span class="help-inline"><code>required</code></span>
 </div></div>
 
     
  <div class="control-group">
<label class="control-label">From Address</label>
<div class="controls"> <asp:TextBox ID="txtfromAddress" runat="Server" MaxLength="100" CssClass="input-large" />
<asp:RequiredFieldValidator runat="server" Display="None" CssClass="text-error" ControlToValidate="txtfromAddress" ID="RequiredFieldValidator1" ValidationGroup="valError" ErrorMessage="From Address is a required field.">*</asp:RequiredFieldValidator><span class="help-inline"><code>required</code></span>
</div></div>
          
       <div class="control-group">
<label class="control-label">To Address</label>
<div class="controls"><asp:TextBox ID="txtToAddress" runat="Server" MaxLength="100" CssClass="input-large" />

  </div></div>    
                
                
    <div class="control-group">
<label class="control-label">Subject</label>
<div class="controls"> <asp:TextBox ID="txtSubject" runat="Server" MaxLength="200" CssClass="input-large" />
                <asp:RequiredFieldValidator runat="server" Display="None" ControlToValidate="txtSubject" ID="valReqCode" CssClass="text-error" ValidationGroup="valError" ErrorMessage="Subject is a required field.">*</asp:RequiredFieldValidator><span class="help-inline"><code>required</code></span>
     </div></div>     
     
      
         <div class="control-group">
<label class="control-label">Body</label>
<div class="controls"><HTMLEditor:Editor ID="EmailBodyDesc" runat="server"  Height="400px"  Width="700px" AutoFocus="true" /><asp:RequiredFieldValidator runat="server" Display="None" ControlToValidate="EmailBodyDesc" CssClass="text-error" ID="valReqCartDesc" ValidationGroup="valError" ErrorMessage="Body is a required field.">*</asp:RequiredFieldValidator><span class="help-inline"><code>required</code></span>
  </div></div>   
              
         <div class="form-actions">
                <asp:LinkButton runat="server" ID="btnCancel" CssClass="btn btn-danger" CommandName="Cancel" CausesValidation="false" OnCommand="btnAction_Command"><i class="icon-ban-circle"></i> Cancel</asp:LinkButton>
             
                <asp:LinkButton CssClass="btn btn-success" runat="server" ID="btnSave" CommandName="Save" OnCommand="btnAction_Command" CausesValidation="true" ValidationGroup="valError"><i class="icon-save"></i> Save</asp:LinkButton>
</div>

 </div>
    </div>
</div>

</asp:Content>
