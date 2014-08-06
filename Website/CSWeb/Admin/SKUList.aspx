<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SKUList.aspx.cs" MasterPageFile="AdminSite.master" Inherits="CSWeb.Admin.SKUList" %>

<%@ Register TagPrefix="usercontrols1" TagName="paging" Src="UserControls/PageControl.ascx" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
   <title>SKUs</title>
   
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <span id="pageid" class="sku"></span>
    <div id="page-content">
<ul id="nav-info" class="clearfix">
<li><a href="main.aspx"><i class="icon-home"></i></a></li>
<li><i class="icon-star"></i> Catalog</li>
<li><i class="icon-barcode"></i> SKUs</li>
</ul>
<h3 class="page-header page-header-top">SKUs
<div style="margin-left: 10px; display: inline; font-style: normal;"><asp:Label ID="lblSuccess" runat="server" Text="Sku has been copied!" Visible="false" CssClass="label label-success"></asp:Label>
<asp:Label ID="lblCancel" runat="server" Text="Sku has been deleted!" Visible="false" CssClass="label label-important"></asp:Label></div>
</h3>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" />
   
   <div class="row-fluid" style="margin-bottom: 12px">
    <asp:ValidationSummary CssClass="text-error" ID="valError" runat="server" ShowSummary="True" DisplayMode="List" />
  </div>
  
           <!-- Toolbar -->
<div class="push">
        <asp:HyperLink ID="hlAddSku" runat="server" CssClass="btn btn-success" NavigateUrl="SKUItem.aspx"><i class="icon-plus"></i> Add a SKU</asp:HyperLink>
           
<div class="pull-right">
                <asp:UpdatePanel ID="updPg" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
           <usercontrols1:paging ID="pg" OnPageChanged="OnPaging" Mode="Links" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
</div>
</div>  <!-- END Toolbar -->

    <asp:UpdatePanel ID="updList" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
<table class="table table-striped table-bordered">

 <thead>
<tr>
 <th>SKU Name&nbsp;&nbsp;<button class="btn btn-mini btn-inverse" data-toggle="tooltip" title="Click on SKU Name below to edit SKU details"><i class="icon-info-sign"></i></button></th>
  <th>Initial Price</th>
  <th>Full Price</th>
  <th>Active</th>
  <th class="span1">Options</th>
 </tr>
   </thead><tbody>
                <asp:DataList runat="server" ID="dlSkuList" DataKeyField="SkuId" OnItemCommand="dlSKU_ItemCommand" RepeatLayout="Flow" RepeatDirection="Horizontal" >
                    <ItemTemplate>
       <tr>
          <td>
             <a href="SKUItem.aspx?skuid=<%# DataBinder.Eval(Container.DataItem, "skuid") %>">
                                <%# DataBinder.Eval(Container.DataItem, "title") %> - <%# DataBinder.Eval(Container.DataItem, "skuid") %>
                                </a>
             </td>
           <td>$<%#   String.Format("{0:0.##}", DataBinder.Eval(Container.DataItem, "InitialPrice"))%></td>
                            <td>
                                $<%#   String.Format("{0:0.##}", DataBinder.Eval(Container.DataItem, "FullPrice")) %></td>
                            <td>
                                1
                            </td>
                            <td class="span1"><div class="btn-group">
                              <asp:LinkButton ID="lbCopy" runat="server" ToolTip="Copy this SKU" CausesValidation="True" CommandName="Copy" CssClass="btn btn-mini btn-primary"><i class="icon-copy"></i></asp:LinkButton> <asp:LinkButton ID="lbRemove" runat="server" CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this SKU?')" CssClass="btn btn-mini btn-danger" ToolTip="Remove this SKU"><i class="icon-remove"></i></asp:LinkButton>
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
