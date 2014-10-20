<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserEdit.aspx.cs" Inherits="CSWeb.Admin.UserEdit"  MasterPageFile="AdminSite.master" EnableViewState="true" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <title>Add/Edit Administrator</title>
    </asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

 <span id="pageid" class="admins"></span>
    <div id="page-content">
<ul id="nav-info" class="clearfix">
<li><a href="main.aspx"><i class="icon-home"></i></a></li>
<li><i class="icon-group"></i> Users</li>
<li><a href="users.aspx"><i class="icon-key"></i> Administrators</a></li>
<li><i class="icon-pencil"></i> Add/Edit Administrators</li>
</ul>
<h3 class="page-header page-header-top">Add/Edit Administrators</h3>  

<p>
 <asp:ValidationSummary ID="valErrorSummary" runat="server" CssClass="text-error" 
                 ValidationGroup="valError" DisplayMode="List" />
</p>


    <div class="form-horizontal form-box">


 <h4 class="form-box-header">Administrator Details<div style="margin-left: 10px; display: inline; font-style: normal;">
 <asp:Label ID="lblSuccess" runat="server" Text="Changes Saved!" Visible="false" CssClass="label label-success"></asp:Label>
<asp:Label ID="lblCancel" runat="server" Text="Changes Cancelled!" Visible="false" CssClass="label label-important"></asp:Label></div></h4>

<div class="form-box-content">

 <div class="control-group">
<label class="control-label">First Name</label>
<div class="controls">
      <asp:TextBox ID="txtFirstName" runat="Server" MaxLength="200" CssClass="input-large" />
                        <asp:RequiredFieldValidator runat="server" Display="Dynamic" ControlToValidate="txtFirstName" ID="valReqFirstName" CssClass="text-error" ValidationGroup="valError" ErrorMessage="First Name is a required field." >*</asp:RequiredFieldValidator><span class="help-inline"><code>required</code></span>
                        </div></div>
            
            
             <div class="control-group">
<label class="control-label">Last Name</label>
<div class="controls">
        <asp:TextBox ID="txtLastName" runat="Server" MaxLength="200" CssClass="input-large" />
                        <asp:RequiredFieldValidator runat="server" Display="Dynamic" ControlToValidate="txtLastName" ID="valReqLastName" CssClass="text-error" ValidationGroup="valError" ErrorMessage="Last Name is a required field." >*</asp:RequiredFieldValidator><span class="help-inline"><code>required</code></span>
                  </div></div>
                  
               <div class="control-group">
<label class="control-label">Username</label>
<div class="controls">         
      <asp:TextBox ID="txtUserName" runat="Server" MaxLength="50" CssClass="input-large" />
                        <asp:RequiredFieldValidator runat="server" Display="Dynamic" ControlToValidate="txtUserName" CssClass="text-error" ID="valReqTitle" ValidationGroup="valError" ErrorMessage="Username is a required field." >*</asp:RequiredFieldValidator><span class="help-inline"><code>required</code></span>
             </div></div>
             
          <div class="control-group">
<label class="control-label">Email</label>
<div class="controls">            
      <asp:TextBox ID="txtEmail" runat="Server" MaxLength="100" CssClass="input-large" />
                        <asp:RequiredFieldValidator runat="server" Display="Dynamic" ControlToValidate="txtEmail" ID="RequiredFieldValidator1" CssClass="text-error" ValidationGroup="valError" ErrorMessage="Email is a required field." >*</asp:RequiredFieldValidator><span class="help-inline"><code>required</code></span>
      </div></div>           
                        
                        
                        
      <div class="control-group">
<label class="control-label">Password</label>
<div class="controls">  
      <asp:TextBox ID="txtPassword" runat="Server" MaxLength="20" TextMode="Password" CssClass="input-large" />
                        <asp:RequiredFieldValidator runat="server" Display="Dynamic" ControlToValidate="txtPassword" CssClass="text-error" ID="RequiredFieldValidator2" ValidationGroup="valError" ErrorMessage="Password is required field." >*</asp:RequiredFieldValidator><span class="help-inline"><code>required</code></span>
          </div></div>
          
                  
       <div class="control-group">
<label class="control-label">Admin Type</label>
<div class="controls">                     
        <asp:DropDownList ID="ddlAdminType" runat="Server" AutoPostBack="false" CssClass="input-large" />
     </div></div>                
     
     
     <div class="control-group">
<label class="control-label">Account Disabled</label>
<div class="controls">  
<label class="checkbox"> 
  <asp:CheckBox ID="cbAccount" runat="Server" AutoPostBack="false" />
 </label> 
  </div></div>
                
                
             <div class="form-actions">    
                        <asp:LinkButton runat="server" CssClass="btn btn-danger" ID="btnCancel" CommandName="Cancel" OnCommand="btnAction_Command"><i class="icon-ban-circle"></i> Cancel</asp:LinkButton> <asp:LinkButton runat="server" ID="btnSave" CommandName="Save" CssClass="btn btn-success" OnCommand="btnAction_Command" CausesValidation="true" ValidationGroup="valError"><i class="icon-save"></i> Save</asp:LinkButton>
</div>
</div>
</div>
</div>
    </asp:Content>