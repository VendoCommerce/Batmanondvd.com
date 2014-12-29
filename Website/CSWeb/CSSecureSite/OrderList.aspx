<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderList.aspx.cs" Inherits="CSWeb.Admin.OrderList" MasterPageFile="AdminSite.master" EnableViewState="True" EnableSessionState="True" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
   <title>Orders</title>
</asp:Content>

<%@ Register TagPrefix="usercontrols1" TagName="paging" Src="UserControls/PageControl.ascx" %>

<%@ Register TagPrefix="usercontrols1" TagName="RangeDateControl" Src="UserControls/RangeDateControlv1.ascx" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
  <span id="pageid" class="orders"></span>
    <div id="page-content">
<ul id="nav-info" class="clearfix">
<li><a href="main.aspx"><i class="icon-home"></i></a></li>
<li><i class="icon-shopping-cart"></i> Order Manager</li>
<li><i class="icon-shopping-cart"></i> Orders</li>
</ul>

<h3 class="page-header page-header-top">Orders</h3>
               
   <p>          
<asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" CssClass="text-error"></asp:ValidationSummary>
   </p>
      
<div class="well push">
<div class="form-inline" style="margin-bottom: 20px;">
 <usercontrols1:RangeDateControl ID="rangeDateControlCriteria" LabelStyle="FieldName" runat="server" DisplayDropDown="true" StartDateWidth="115" EndDateWidth="115" LabelStartText="From" LabelEndText="To" />
</div>
<div class="form-inline">
<asp:TextBox  AutoCompleteType="Disabled" ID="txtFirstName" runat="server" MaxLength="50" EnableViewState="True" CssClass="input-medium" placeholder="First Name" />&nbsp;&nbsp;<asp:TextBox  AutoCompleteType="Disabled" ID="txtLastName" runat="server" MaxLength="50" EnableViewState="True" CssClass="input-medium" placeholder="Last Name" />&nbsp;&nbsp;<asp:TextBox  AutoCompleteType="Disabled" ID="txtEmail" runat="server" MaxLength="50" EnableViewState="True" CssClass="input-medium" placeholder="Email Address" />&nbsp;&nbsp;<label class="checkbox"><asp:CheckBox AutoPostBack="false" ID="cbArchive" runat="server"></asp:CheckBox>&nbsp;Include Archive Date</label>&nbsp;&nbsp;<asp:LinkButton CssClass="btn btn-primary" ID="lblSearch" runat="server" CommandName="Search" OnClick="lblOrder_Search"><i class="icon-search"></i> Search</asp:LinkButton>
       
  <asp:Literal ID="FCLiteral" runat="server"></asp:Literal>
</div>
  </div>   
     <div class="row-fluid">
     
     <div class="form-horizontal span6"><label class="checkbox">
       <asp:CheckBox ID="CbInCludeFullAmount" runat="server" AutoPostBack="True" OnCheckedChanged="CbInCludeFullAmount_CheckedChanged"></asp:CheckBox>&nbsp;Include Full Amount
     </label>
     </div>
  
  		<div class="span6">
                <asp:UpdatePanel ID="updPg" runat="server" UpdateMode="Conditional" class="pull-right">
                    <ContentTemplate>
                        <usercontrols1:paging ID="pg" OnPageChanged="OnPaging" Mode="Links" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
         </div>
     </div>
         
    <asp:UpdatePanel ID="updList" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table class="table table-bordered table-striped">
              <thead>
                <tr>
                    <th>Order ID &nbsp;&nbsp;<button class="btn btn-mini btn-inverse" data-toggle="tooltip" title="Click Order ID below to view order details"><i class="icon-info-sign"></i></button></th>
                    <th>Email</th>
                    <th>Order Date</th>
                    <th><asp:Label ID="lblHeader" runat="server" Text="Subtotal" /></th>
                    <th>Shipping</th>
                    <th>Tax</th>
                    <th>Total</th>
                    <th>First Name</th>
                    <th>Last Name</th>
                    <th>Order Status</th>
                    <th class="span1 text-center">Delete</th>
                </tr>
                </thead>
                <tbody>
                <asp:DataList runat="server" ID="dlordersList" RepeatLayout="Flow" RepeatDirection="Horizontal" OnItemDataBound="dlOrderList_ItemDataBound" OnItemCommand="dlOrderList_ItemCommand">
                    <ItemTemplate>
                        <tr><td>
                        <asp:LinkButton ID="hlDetail" runat="server" CommandArgument=<%# DataBinder.Eval(Container.DataItem, "OrderId")%> CommandName="LoadOrder" Text=<%# DataBinder.Eval(Container.DataItem, "OrderId")%>></asp:LinkButton>
                        </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "Email")%>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "CreatedDate")%>
                            </td>
                            <td>
                                <asp:Label ID="lblSubTotal" runat="server" />
                            </td>
                            <td>
                                $<%# String.Format("{0:0.##}", DataBinder.Eval(Container.DataItem, "ShippingCost"))%></td>
                            <td>
                                $<%# String.Format("{0:0.##}", DataBinder.Eval(Container.DataItem, "Tax"))%></td>
                            <td>
                                <asp:Label ID="lblTotal" runat="server" />
                            </td>
                            
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "customerInfo.FirstName")%>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "customerInfo.LastName")%>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "OrderStatus")%>
                            </td>
                          <td class="span1 text-center"><div class="btn-group">
                    <asp:LinkButton ID="lbCancel" CssClass="btn btn-mini btn-danger" runat="server" CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this order?')" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "OrderId") %>'><i class="icon-remove"></i></asp:LinkButton>
                            </div>
                		</td>
                        </tr>
                    </ItemTemplate>
                </asp:DataList>
                </tbody>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
</asp:Content>
