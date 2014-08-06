<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CouponList.aspx.cs" Inherits="CSWeb.Admin.CouponList"  MasterPageFile="AdminSite.master" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
   <title>Coupons</title>
   </asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
  <span id="pageid" class="coupon"></span>
    <div id="page-content">
<ul id="nav-info" class="clearfix">
<li><a href="main.aspx"><i class="icon-home"></i></a></li>
<li><i class="icon-star"></i> Catalog</li>
<li><i class="icon-tag"></i> Coupons</li>
</ul>
<h3 class="page-header page-header-top">Coupons</h3>


<asp:ValidationSummary CssClass="text-error" ID="valError" runat="server" ShowSummary="True" DisplayMode="List" />
        
 <div class="push">
<asp:HyperLink ID="hlItem" runat="server" CssClass="btn btn-success" NavigateUrl="CouponItem.aspx"><i class="icon-plus"></i> Add Promotion/Coupon</asp:HyperLink>
</div>

         
    <table class="table table-bordered table-striped">
      <thead>
        <tr>
            <th>
                Promotion/Coupon Code
            </th>
            <th>
                Discount
            </th>
            <th>
                Total Amount
            </th>
            <th class="span1 text-center">
                Type
            </th>
            <th class="span1 text-center">
                Active
            </th>
            <th class="span1 text-center">
                Options
            </th>
        </tr>
        </thead>
        <tbody>
        <asp:DataList runat="server" ID="dlCouponList" OnItemCommand="dlCouponList_ItemCommand" RepeatLayout="Flow" RepeatDirection="Horizontal" OnItemDataBound="dlCouponList_ItemDataBound">
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:Literal runat="server" ID='lblTitle'></asp:Literal>
                    </td>
                    <td>
                        <asp:Literal runat="server" ID='lblDiscount'></asp:Literal>
                    </td>
                    <td>
                        <asp:Literal runat="server" ID='lblTotalAmount'></asp:Literal>
                    </td>
                    <td class="span1 text-center">
                        <asp:Literal runat="server" ID='lblDiscountType'></asp:Literal>
                    </td>
                    <td class="span1 text-center">
                        <asp:Literal runat="server" ID='lblStatus'></asp:Literal>
                    </td>
                    <td class="span1 text-center"><div class="btn-group">
                        <asp:HyperLink ID="hlEditLink" runat="server" CausesValidation="False" CommandName="Edit" CssClass="btn btn-mini btn-success"><i class="icon-pencil"></i></asp:HyperLink>
                        
                        <asp:LinkButton ID="lbRemove" runat="server" CausesValidation="False" CommandName="Delete" CssClass="btn btn-mini btn-danger" OnClientClick="return confirm('Are you sure you want to delete this coupon?')"><i class="icon-remove"></i></asp:LinkButton>
                        </div>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:DataList>
     
</tbody>
    </table>

</div>
</asp:Content>
