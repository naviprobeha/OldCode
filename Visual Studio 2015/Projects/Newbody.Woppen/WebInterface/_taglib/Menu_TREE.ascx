<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Menu_TREE.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.Menu_TREE" %>

<div class="userControl">
<asp:TreeView ID="menuTree" runat="server" ontreenodedatabound="menuTree_TreeNodeDataBound" ShowExpandCollapse="false">
    <HoverNodeStyle ForeColor="#e5e5e5" />
    <SelectedNodeStyle ForeColor="#cccccc" />
    <LevelStyles>
        <asp:TreeNodeStyle ForeColor="#ffffff" Font-Size="12px" Font-Bold="true" HorizontalPadding="5px" ImageUrl="../_assets/img/menu_item_sub.gif"/>
        <asp:TreeNodeStyle ForeColor="#ffffff" Font-Size="10px" ImageUrl="../_assets/img/menu_item_sub.gif" />
    </LevelStyles>    
    
    <DataBindings>
        <asp:TreeNodeBinding DataMember="Navipro.Infojet.Lib.NavigationItem" TextField="text" NavigateUrlField="link" Depth="0" />
        <asp:TreeNodeBinding DataMember="Navipro.Infojet.Lib.NavigationItem" TextField="text" NavigateUrlField="link" Depth="1" />        
    </DataBindings>
</asp:TreeView>
</div>



