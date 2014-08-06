<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorLog.aspx.cs" Inherits="CSWeb.Admin.ErrorLog" MasterPageFile="AdminSite.master" EnableViewState="True" %>

<%@ Register TagPrefix="usercontrols1" TagName="RangeDateControl" Src="UserControls/RangeDateControlv1.ascx" %>
<%@ Register TagPrefix="usercontrols1" TagName="paging" Src="UserControls/PageControl.ascx" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
   <title>Error Log</title>
   </asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
     <span id="pageid" class="errorlog"></span>
    <div id="page-content">
<ul id="nav-info" class="clearfix">
<li><a href="main.aspx"><i class="icon-home"></i></a></li>
<li><i class="icon-star"></i> Catalog</li>
<li><i class="icon-warning-sign"></i> Error Log</li>
</ul>
<h3 class="page-header page-header-top">Error Log</h3>
               
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
                    
    <div class="row-fluid" style="margin-bottom: 12px">    
    <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" 
       CssClass="text-error"></asp:ValidationSummary>
      </div>
      <div class="row-fluid">
      <div class="well push">
      <div class="form-inline" style="margin-bottom: 20px;">
  <usercontrols1:RangeDateControl ID="rangeDateControlCriteria" LabelStyle="FieldName" runat="server" DisplayDropDown="true" LabelStartText="From:" LabelEndText="To:" />
           &nbsp;&nbsp;
<asp:LinkButton ID="lblSearch" runat="server" CommandName="Search" OnClick="lblOrder_Search" CssClass="btn btn-primary"><i class="icon-search"></i> Search</asp:LinkButton>
</div>
           </div>
 </div>
 
 <div class="row-fluid"><div class="span12">
 <div class="pull-right">
        <asp:UpdatePanel ID="updPg" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <usercontrols1:paging ID="pg" OnPageChanged="OnPaging" Mode="Links" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
        </div>
    </div>
    </div>
    <asp:UpdatePanel ID="updList" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table class="table table-bordered table-striped">
              <thead>
                <tr>
                    <th class="span3">
                        Error Date
                    </th>
                    <th class="span4">
                        URL
                    </th>
                    <th>
                        Error Message
                    </th>
                </tr>
                </thead>
                <tbody>
                <asp:DataList runat="server" ID="dlErrorList" RepeatLayout="Flow" RepeatDirection="Horizontal"> <ItemTemplate>
                        <tr>
                            <td class="span3">
                                <%# DataBinder.Eval(Container.DataItem, "EventDate")%>
                            </td>
                            <td class="span4">
                                <%# DataBinder.Eval(Container.DataItem, "URL")%>
                            </td>
                            <td>
                                $<%#  DataBinder.Eval(Container.DataItem, "Message")%>
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
