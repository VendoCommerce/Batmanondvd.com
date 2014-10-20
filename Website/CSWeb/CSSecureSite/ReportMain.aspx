<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportMain.aspx.cs" Inherits="CSWeb.Admin.ReportMain"
    MasterPageFile="AdminSite.master" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <title>Report Admin Page</title>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table cellpadding="2" cellspacing="1" border="0" width="100%">
        <td valign="top">
            <table>
                <tr>
                    
                    <td valign="bottom" class="ibn-sectionheader" width="25%" colspan="2">
                        <b>Report Management</b>
                    </td>
                </tr>
                <tr>
                    <td valign="top" align="right" width="1%">
                        <img hspace="0" src="/Content/images/admin/rect.gif" vspace="2" border="0"><br>
                    </td>
                    <td valign="top" width="100%" width="49%">
                        <a class="link" href="reports/VersionReport.aspx">Version Report</a><br>
                        <span class="body">Version Report Description
                            <br>
                        </span>
                </tr>
                <tr>
                    <td valign="top" align="right" width="1%">
                        <img hspace="0" src="/Content/images/admin/rect.gif" vspace="2" border="0"><br>
                    </td>
                    <td valign="top" width="100%" width="49%">
                        <a class="link" href="reports/StandardReport.aspx">Order Summary Report</a><br>
                        <span class="body">Order Summary Report Description
                            <br>
                        </span>
                </tr>
                  <tr>
                    <td valign="top" align="right" width="1%">
                        <img hspace="0" src="/Content/images/admin/rect.gif" vspace="2" border="0"><br>
                    </td>
                    <td valign="top" width="100%" width="49%">
                        <a class="link" href="Reports.aspx">>Client Custom Reports</a><br>
                        <span class="body">>Client Custom Reports Description
                            <br>
                        </span>
                </tr>
            </table>
        </td>
        </tr>
    </table>
    <br />
</asp:Content>
