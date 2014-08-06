<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SKUItem.aspx.cs" Inherits="CSWeb.Admin.SKUItem" EnableViewState="true" MasterPageFile="AdminSite.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="HTMLEditor" %>
<%@ Register TagPrefix="uc" TagName="attributes" Src="UserControls/Attributes.ascx" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <title>Add New Sku</title>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <script type="text/javascript">
        function setVisibilityTaxPanel(control) {
            var panel = document.getElementById('<%= rbListTaxable.ClientID %>');
            if (panel.checked) {
                alert(panel.value);
            }
            for (var i = 0; i < panel.length; i++) {
                if (panel[i].checked) {
                    alert(radio[i].value);
                }
            }

            if (control.value == 'Yes') {
                panel.style.visibility = 'visible';
            }
            else {
                panel.style.visibility = 'hidden';
            }
        }
    </script>
    
  <span id="pageid" class="sku"></span>
    <div id="page-content">
<ul id="nav-info" class="clearfix">
<li><a href="main.aspx"><i class="icon-home"></i></a></li>
<li><i class="icon-star"></i> Catalog</li>
<li><a href="skulist.aspx"><i class="icon-barcode"></i> SKUs</a></li>
<li><i class="icon-pencil"></i> Add/Edit SKU</li>
</ul>
<h3 class="page-header page-header-top">Add/Edit SKU</h3>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" />
        

<p>
    &nbsp;<asp:ValidationSummary ID="valErrorSummary" runat="server" CssClass="text-error"
        ValidationGroup="valError" DisplayMode="List" />
        <p>
    <%-- <asp:UpdatePanel ID="udpInnerUpdatePanel" runat="Server" UpdateMode="Conditional">
        <ContentTemplate> --%>
       <div class="form-horizontal form-box">  
        
        
         <h4 class="form-box-header">SKU Details
<div style="margin-left: 10px; display: inline; font-style: normal;"><asp:Label ID="lblSuccess" runat="server" Text="Changes Saved!" Visible="false" CssClass="label label-success"></asp:Label>
<asp:Label ID="lblCancel" runat="server" Text="Changes Cancelled!" Visible="false" CssClass="label label-important"></asp:Label></div></h4>
       <div class="form-box-content">

<div class="control-group">
<label class="control-label">Title</label>
<div class="controls">
<asp:TextBox ID="txtTitle" runat="Server" MaxLength="500" CssClass="input-large"  /><asp:RequiredFieldValidator runat="server" Display="None" ControlToValidate="txtTitle" ID="valReqTitle" ValidationGroup="valError" CssClass="text-error" ErrorMessage="Title is required field.">*</asp:RequiredFieldValidator><span class="help-inline"><code>required</code></span>
</div></div>
           
<div class="control-group">
<label class="control-label">SKU Code</label>
<div class="controls"> 
<asp:TextBox ID="txtSkuCode" runat="Server" CssClass="input-small" /><asp:RequiredFieldValidator runat="server" Display="None" ControlToValidate="txtSkuCode" ID="valReqCode" ValidationGroup="valError" CssClass="text-error" ErrorMessage="Sku Code is required field.">*</asp:RequiredFieldValidator><span class="help-inline"><code>required</code></span>
</div></div>
           
<div class="control-group">
<label class="control-label">Category</label>
<div class="controls"> 
 <asp:DropDownList ID="ddlCategory" runat="Server" CssClass="input-medium" />
 </div></div>
           
<div class="control-group">
<label class="control-label">Offer Code</label>
<div class="controls">             
<asp:TextBox ID="txtOfferCode" runat="Server" CssClass="input-small" />
</div></div>


<div class="control-group">
<label class="control-label">Full Price</label>
<div class="controls">
<asp:TextBox ID="txtfullprice" runat="Server" MaxLength="7" CssClass="input-small" /><asp:RequiredFieldValidator runat="server" Display="None" ControlToValidate="txtfullprice" ID="Requiredfieldvalidator2" CssClass="text-error" ValidationGroup="valError" ErrorMessage="Full Price is a required field.">*</asp:RequiredFieldValidator><asp:CompareValidator ID="cmpValorderNo" runat="server" ControlToValidate="txtfullprice" Type="Double" CssClass="text-error" ErrorMessage="Full Price must be a double." Operator="DataTypeCheck" ValidationGroup="valError" ValueToCompare="0">*</asp:CompareValidator><span class="help-inline"><code>required</code></span>
</div></div>
           
 <div class="control-group">
<label class="control-label">Initial Price</label>
<div class="controls"> 
<asp:TextBox ID="txtinitialprice" runat="Server" MaxLength="7" CssClass="input-small" /><asp:RequiredFieldValidator runat="server" Display="None" ControlToValidate="txtinitialprice" ID="Requiredfieldvalidator1" CssClass="text-error" ValidationGroup="valError" ErrorMessage="Initial Price is a required field.">*</asp:RequiredFieldValidator><asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtinitialprice" Type="Double" CssClass="text-error" ErrorMessage="Initial Price must be a double." Operator="DataTypeCheck" ValidationGroup="valError" ValueToCompare="0">*</asp:CompareValidator><span class="help-inline"><code>required</code></span>
</div></div>
          
 <div class="control-group">
<label class="control-label">Weight</label>
<div class="controls">
<asp:TextBox ID="txtWeight" runat="Server" MaxLength="5" CssClass="input-small" />
</div></div>

         
 <div class="control-group">
<label class="control-label">Stock Quantity</label>
<div class="controls">
<asp:TextBox ID="txtStock" runat="Server" MaxLength="7" CssClass="input-small" />
 </div></div> 


 <div class="control-group">
<label class="control-label">Availability</label>
<div class="controls"><label class="checkbox">
<asp:CheckBox ID="cbAvailable" runat="Server" /></label>
                </div>
                </div>
                
          
 <div class="control-group">
<label class="control-label">Is Taxable</label>
<div class="controls">
<label class="radio">
<asp:RadioButtonList ID="rbListTaxable" runat="server" OnSelectedIndexChanged="rbListTaxable_SelectedIndexChanged" AutoPostBack="true" RepeatLayout="Flow" RepeatDirection="Horizontal">
<asp:ListItem>Yes</asp:ListItem>
<asp:ListItem>No</asp:ListItem>
</asp:RadioButtonList>
</label>
</div></div>
                     
<asp:Panel ID="pnlTaxableAmount" runat="server" Visible="false">
 <div class="control-group">
<label class="control-label">Taxable Amount</label>
<div class="controls">
<asp:TextBox ID="txtTaxAmount" runat="Server" MaxLength="7" CssClass="input-small" /><asp:RequiredFieldValidator runat="server" Display="None" ControlToValidate="txtTaxAmount" ID="valReqAmount" ValidationGroup="valError" CssClass="text-error" ErrorMessage="Taxable Amount is a required field.">*</asp:RequiredFieldValidator><asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtTaxAmount" Type="Double" ErrorMessage="Taxable Amount must be a double." Operator="DataTypeCheck" ValidationGroup="valError" CssClass="text-error" ValueToCompare="0">*</asp:CompareValidator><span class="help-inline"><code>required</code></span>
</div></div>
</asp:Panel>

       
 <div class="control-group">
<label class="control-label">Product Image</label>
<div class="controls">
<asp:TextBox ID="txtImagePath" runat="Server" MaxLength="200" CssClass="input-large" /><span class="help-inline"><code>(Example: /content/images/cart_product_pic.jpg)</code></span>
</div></div>

 <div class="control-group">
<label class="control-label">Cart Description</label>
<div class="controls">
 <HTMLEditor:Editor ID="ftbShortDesc" runat="server" Height="200px" Width="500px" AutoFocus="true" /><span class="help-inline"><code>required</code></span>
 <asp:RequiredFieldValidator runat="server" Display="None" ControlToValidate="ftbShortDesc" ID="valReqCartDesc" ValidationGroup="valError" ErrorMessage="Cart Description is a required field." CssClass="text-error">*</asp:RequiredFieldValidator>
 </div></div>
                
 <div class="control-group">
<label class="control-label">Receipt Description</label>
<div class="controls">
<HTMLEditor:Editor ID="ftbLongDesc" runat="server" Height="200px" Width="500px" AutoFocus="true" />
</div></div>

 <div class="control-group">
<label class="control-label">Email Description</label>
<div class="controls">            
<HTMLEditor:Editor ID="ftbEmailDesc" runat="server" Height="200px" Width="500px" AutoFocus="true" />
</div></div>
     
 </div>
     
 <h4 class="form-box-header">Attributes</h4>
<div class="form-box-content">
 <uc:attributes id="ucAttributes" runat="server" WidthTotal="500" />
          
      
          <div class="form-actions">
                <asp:LinkButton runat="server" ID="btnCancel" CommandName="Cancel" CausesValidation="false" CssClass="btn btn-danger" OnCommand="btnAction_Command"><i class="icon-ban-circle"></i> Cancel</asp:LinkButton> 
                
                <asp:LinkButton runat="server" ID="btnSave" CommandName="Save" OnCommand="btnAction_Command" CssClass="btn btn-success" CausesValidation="true" ValidationGroup="valError"><i class="icon-save"></i> Save</asp:LinkButton> 
          </div>
          
          
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
    </div>
    </div>
</div>
    
</asp:Content>
