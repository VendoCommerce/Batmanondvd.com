<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CouponItem.aspx.cs" Inherits="CSWeb.Admin.CouponItem" MasterPageFile="AdminSite.master" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
   <title>Add/Edit Coupon</title>
   </asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
   <span id="pageid" class="coupon"></span>
    <div id="page-content">
<ul id="nav-info" class="clearfix">
<li><a href="main.aspx"><i class="icon-home"></i></a></li>
<li><i class="icon-star"></i> Catalog</li>
<li><a href="couponlist.aspx"><i class="icon-tag"></i> Coupons</li></a></li>
<li><i class="icon-pencil"></i> Add/Edit Coupon</li>
</ul>
<h3 class="page-header page-header-top">Add/Edit Coupon</h3>

<p>
    <asp:ValidationSummary ID="valErrorSummary" runat="server" CssClass="text-error"
        ValidationGroup="valError" DisplayMode="List" />
   </p>     
   
   
    <div class="form-horizontal form-box">  
        
        
         <h4 class="form-box-header">Coupon Details
<div style="margin-left: 10px; display: inline; font-style: normal;"><asp:Label ID="lblSuccess" runat="server" Text="Changes Saved!" Visible="false" CssClass="label label-success"></asp:Label>
<asp:Label ID="lblCancel" runat="server" Text="Changes Cancelled!" Visible="false" CssClass="label label-important"></asp:Label></div></h4>


 <div class="form-box-content">
       
<div class="control-group">
<label class="control-label">Discount Code</label>
<div class="controls">
   <asp:RequiredFieldValidator ID="rfvTitle" runat="server" Display="Dynamic" ErrorMessage="Discount Code is a required field." ControlToValidate="txtDiscountTitle">*</asp:RequiredFieldValidator>
   <asp:TextBox  AutoCompleteType="Disabled" ID="txtDiscountTitle" runat="server" MaxLength="50" CssClass="input-large" /><span class="help-inline"><code>required</code></span>
   </div></div>
           
     
     <div class="control-group">
<label class="control-label">Discount Type</label>
<div class="controls">
  <asp:RequiredFieldValidator ID="rfqDicountType" runat="server" Display="Dynamic" ErrorMessage="Discount Type is a required field." ControlToValidate="ddlDiscountType">*</asp:RequiredFieldValidator>
  <asp:DropDownList runat="server" ID='ddlDiscountType' AutoPostBack="true" OnSelectedIndexChanged="ddlDiscountType_OnSelectedIndexChanged" CssClass="input-large" /><span class="help-inline"><code>required</code></span>
  </div></div>
      
      
         <div class="control-group">
<label class="control-label">Discount (% or amount)</label>
<div class="controls">      
     <asp:TextBox  AutoCompleteType="Disabled" ID="txtPercentage" runat="Server" MaxLength="7" />
                <asp:RequiredFieldValidator runat="server" Display="None" ControlToValidate="txtPercentage" ID="rfvPercentage" ValidationGroup="valError" ErrorMessage="Discount is a required field.">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cmpPercentage" runat="server" ControlToValidate="txtPercentage" Type="Double" ErrorMessage="Discount must be a double." Operator="DataTypeCheck" ValidationGroup="valError" CssClass="text-error" ValueToCompare="0">*</asp:CompareValidator><span class="help-inline"><code>required</code></span>
         </div></div>
         
            <div class="control-group">
<label class="control-label">Total Amount</label>
<div class="controls">      
  <asp:TextBox  AutoCompleteType="Disabled" ID="txttotalAmount" runat="Server" MaxLength="7" />
     </div></div>
          
          
    <div class="control-group">
<label class="control-label">Include Shipping <br>(For % only)</label>
<div class="controls">    
    <label class="checkbox">
    <asp:CheckBox ID="cbIncludeShipping" runat="server" />                
     </label>
     </div></div>      
           
        <asp:Panel ID="pnlItem" runat="server" Visible="false">
        <asp:DataList runat="server" ID="dlSkuCouponList" OnItemCommand="dlSkuCouponList_ItemCommand" RepeatLayout="Flow" RepeatDirection="Horizontal" OnItemDataBound="dlSkuCouponList_ItemDataBound">
        
            <ItemTemplate>
          
                        <asp:DropDownList ID="ddlSkuList" runat="server">
                        </asp:DropDownList>

                   
                        <asp:DropDownList runat="server" ID='ddlRelatedSkuList' />
                    
                        <asp:DropDownList runat="server" ID='ddlItemDiscountType' />
                  
                        <asp:TextBox  AutoCompleteType="Disabled" ID="txtItemDiscount" runat="Server" MaxLength="7" />
                    
           <asp:LinkButton ID="lbRemove" CssClass="btn btn-danger btn-mini" runat="server" CausesValidation="False" CommandName="Delete"><i class="icon-remove"></i></asp:LinkButton>
                  
            </ItemTemplate>
        </asp:DataList>
        
                    <div class="control-group">
<label class="control-label">Item Promotion</label>
<div class="controls">   

                    <table class="table table-bordered table-striped">
                    <thead>
                        <tr>
                            <th>
                                Item
                            </th>
                            <th>
                                Related Item
                            </th>
                            <th>
                                Discount Type
                            </th>
                            <th>
                                Discount Amount
                            </th>
                        </tr>
                        </thead>
                        <tbody>
                        <tr>
                            <td>
                                <asp:DropDownList runat="server" ID='ddlSkuList' CssClass="input-medium" />
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID='ddlRelatedSkuList' CssClass="input-medium" />
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID='ddlItemDiscountType' CssClass="input-medium" />
                            </td>
                            <td>
                                <asp:TextBox  AutoCompleteType="Disabled" ID="txtItemDiscount" runat="Server" MaxLength="7" CssClass="input-medium" /><asp:RequiredFieldValidator runat="server" Display="Dynamic" ControlToValidate="txtItemDiscount" ID="Requiredfieldvalidator1" ValidationGroup="valError" ErrorMessage="Item Discount Amount is a required field.">*</asp:RequiredFieldValidator>
                           <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtItemDiscount" Type="Double" ErrorMessage="Item Discount Amount must be a double." Operator="DataTypeCheck" ValidationGroup="valError" CssClass="failureNotification" ValueToCompare="0">*</asp:CompareValidator>
                            </td>
                        </tr>
                        </tbody>
                    </table>
                    </div></div>
                
        </asp:Panel>
       
       
       <div class="form-actions">
                <asp:LinkButton runat="server" ID="btnCancel" CssClass="btn btn-danger" CommandName="Cancel" CausesValidation="false" OnCommand="btnAction_Command"><i class="icon-ban-circle"></i> Cancel</asp:LinkButton>
              
                <asp:LinkButton runat="server" ID="btnSave" CssClass="btn btn-success" CommandName="Save" OnCommand="btnAction_Command" CausesValidation="true" ValidationGroup="valError"><i class="icon-save"></i> Save</asp:LinkButton>
                </div>
          
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
   </div>
    </div>
</div>
</asp:Content>
