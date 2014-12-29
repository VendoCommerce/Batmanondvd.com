<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PathList.aspx.cs" Inherits="CSWeb.Admin.PathList" MasterPageFile="AdminSite.master" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
   <title>Upsell Path List</title>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <span id="pageid" class="upsell"></span>
    <div id="page-content">
<ul id="nav-info" class="clearfix">
<li><a href="main.aspx"><i class="icon-home"></i></a></li>
<li><i class="icon-star"></i> Catalog</li>
<li><i class="icon-gift"></i> Upsell Path List</li>
</ul>
<h3 class="page-header page-header-top">Upsell Path List</h3>

        <ul>
        <li>This page lists all the active upsell paths for all versions.</li>
        <li>You can only update the weight(%) for the selected version.</li>
        <li>If an upsell A/B test needs to be conducted, select the version and enter the weight for each path and hit save.</li>
        <li>To make new path active select version from dropdown which this new path is applied to and hit the save button.</li>
        </ul>

<p>
    <asp:ValidationSummary ID="valErrorSummary" runat="server" CssClass="text-error"
        ValidationGroup="valError" DisplayMode="List" />
 <asp:CustomValidator ID="cvTemplateStep" runat="server" CssClass="text-error" ValidationGroup="valError" ErrorMessage='Make sure that the total of all weight fields is 100%' ></asp:CustomValidator>
 </p>
   

<div class="push">
<asp:HyperLink ID="lbItemAdd" runat="server" CssClass="btn btn-success" NavigateUrl="PathItem.aspx"><i class="icon-plus"></i> Add Upsell Path</asp:HyperLink> <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-primary" NavigateUrl="TemplateList.aspx"><i class="icon-list-alt"></i> Upsell Templates</asp:HyperLink>
<div class="form-inline" style="margin-top: 20px"><label>Select Version:</label> <asp:DropDownList ID="ddlVersion" runat="server" AutoPostBack="true" onselectedindexchanged="ddlVersion_SelectedIndexChanged"></asp:DropDownList>
</div>
</div>
    <table class="table table-bordered table-striped">
		<thead>
        <tr>
            <th class="span1 text-center">
                Active
            </th>
            <th>
                Title
            </th>
            <th>
                Weight
            </th>
            <th>
               Date Created
            </th>
            <th class="span1 text-center">
                Options
            </th>
        </tr>
        </thead>
        <tbody>
        <asp:DataList runat="server" ID="dlPathList" RepeatLayout="Flow" RepeatDirection="Horizontal"
            OnItemCommand="dlPathList_ItemCommand">
            <ItemTemplate>
                <tr>
                    <td class="span1 text-center">
                        <asp:CheckBox ID="cbVisible" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Active") %>' />
                    </td>
                    <td>
                        <%# DataBinder.Eval(Container.DataItem, "Title") %>
                    </td>
                    <td>
                        <asp:TextBox  AutoCompleteType="Disabled" ID="txtWeight" runat="server" MaxLength="4" Text='<%# String.Format("{0:0.##}", DataBinder.Eval(Container.DataItem, "Weight")) %>' />
                    <asp:CompareValidator ID="cmpValorderNo" runat="server" ControlToValidate="txtWeight" Type="Double" ErrorMessage="Weight number must be a double." Operator="DataTypeCheck" ValidationGroup="valError" CssClass="text-error" ValueToCompare="0">*</asp:CompareValidator>
                    </td>
                    <td>
                        <%# DataBinder.Eval(Container.DataItem, "CreateDate")%>
                    </td>
                    <td class="span1 text-center"><div class="btn-group">
                        <asp:LinkButton ID="lbSave" CssClass="btn btn-mini btn-success" runat="server" CausesValidation="True" ToolTip="Edit" CommandName="Edit"><i class="icon-pencil"></i></asp:LinkButton>
                        <asp:LinkButton ID="lbCancel" runat="server" CssClass="btn btn-mini btn-danger" CausesValidation="False" ToolTip="Delete" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this upsell?')"><i class="icon-remove"></i></asp:LinkButton>
                    </div>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:DataList>
      </tbody>
      </table>
      <p>
      <asp:LinkButton Text="Save" CssClass="btn btn-success" ID="imgSave" OnCommand="btnSave_OnClick" CommandName="Save" runat="server" ValidationGroup="valError"><i class="icon-save"></i> Save</asp:LinkButton>
     <%-- <asp:LinkButton Text="Make Selected Path(s) Active" CssClass="btn btn-success" ID="lbActiveSave" OnCommand="btnSave_OnClick" CommandName="Active" runat="server" ValidationGroup="valError"><i class="icon-save"></i> Make Selected Path(s) Active</asp:LinkButton>--%>
</p>
</div>
</asp:Content>
