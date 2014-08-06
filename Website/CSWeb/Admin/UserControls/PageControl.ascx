<%@ Control Language="c#" AutoEventWireup="false" Codebehind="PageControl.ascx.cs" Inherits="CSWeb.Admin.UserControls.PageControl" %>

<asp:PlaceHolder runat="server" ID="holderDropdownMode">
	
    <asp:linkbutton id="Prev" runat="server" CommandName="Prev" OnCommand="OnCommand" CssClass="link" CausesValidation="false"/>
    
<asp:dropdownlist id="dl" runat="server" AutoPostBack="True" OnSelectedIndexChanged="OnSelectedIndexChanged"></asp:dropdownlist>

	<asp:linkbutton id="Next" runat="server" CssClass="link" OnCommand="OnCommand" CausesValidation="false"/>
    
</asp:PlaceHolder>


<asp:PlaceHolder runat="server" ID="holderLinksMode">

	<asp:Label ID="lblResults" runat="server" CssClass="badge badge-success pull-left" Visible="false" style="margin-right: 8px; margin-top: 5px"></asp:Label>
    
	<asp:PlaceHolder runat="server" id='holderHideOnOnePage'>
    
    <div class="pagination remove-margin pull-left">

    <ul>
		<asp:PlaceHolder runat="server" ID="holderPrewLinks">
			
            <li><asp:LinkButton ID="firstPageLink" CausesValidation='<%# CausesValidation %>' CommandName="Firts" runat="server" OnClick="OnNavigationClick" ToolTip="First"><i class="icon-double-angle-left"></i></asp:LinkButton>
			</li>
            
            <li>
            <asp:LinkButton ID="previousPageLink" CausesValidation='<%# CausesValidation %>' CommandName="Previous" runat="server" OnClick="OnNavigationClick" ToolTip="Previous"><i class="icon-angle-left"></i></asp:LinkButton>
            </li>
		</asp:PlaceHolder>
        
        
		<asp:Repeater ID="pagingLinksRepeater" runat="server" OnItemDataBound="pagingLinksRepeater_OnItemDataBound" OnItemCommand="pagingLinksRepeater_OnItemCommand">
			<ItemTemplate>
				   <li><asp:LinkButton runat="server" ID="pageSelector" CausesValidation='<%# CausesValidation %>' ></asp:LinkButton>
                   </li>
                   
                   <li class="active">
				<asp:Label runat="server" ID="lblCurrentPage" Visible="false" ></asp:Label></li>
			</ItemTemplate>
		</asp:Repeater>
        
        
		<asp:PlaceHolder runat="server" ID="holderNextLinks">
        
			<li><asp:LinkButton ID="nextPageLink" CausesValidation='<%# CausesValidation %>' CommandName="Next" runat="server" OnClick="OnNavigationClick" ToolTip="Next"><i class="icon-angle-right"></i></asp:LinkButton></li>
            
            <li>
			<asp:LinkButton ID="lastPageLink" CausesValidation='<%# CausesValidation %>' CommandName="Last" runat="server" OnClick="OnNavigationClick" ToolTip="Last"><i class="icon-double-angle-right"></i></asp:LinkButton>
            </li>
		</asp:PlaceHolder>
        
        
	
    </ul>
    </div>
    </asp:PlaceHolder>
    
</asp:PlaceHolder>
