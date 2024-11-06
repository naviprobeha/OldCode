<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductTree_TREE.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.ProductTree_TREE" %>

<div class="userControl">
<asp:TreeView ID="productTree" runat="server" ontreenodedatabound="productTree_TreeNodeDataBound" ExpandImageUrl="../_assets/img/expand.gif" CollapseImageUrl="../_assets/img/collapse.gif" NoExpandImageUrl="../_assets/img/menu_item_sub.gif">
    <HoverNodeStyle ForeColor="#e5e5e5" />
    <LevelStyles>
        <asp:TreeNodeStyle ForeColor="#ffffff" Font-Size="10px" HorizontalPadding="5px" />
        <asp:TreeNodeStyle ForeColor="#ffffff" Font-Size="10px" HorizontalPadding="5px" />
        <asp:TreeNodeStyle ForeColor="#ffffff" Font-Size="10px" HorizontalPadding="5px" />
    </LevelStyles>    
    
    <DataBindings>
        <asp:TreeNodeBinding DataMember="Navipro.Infojet.Lib.NavigationItem" TextField="text" NavigateUrlField="link" Depth="0" />
        <asp:TreeNodeBinding DataMember="Navipro.Infojet.Lib.NavigationItem" TextField="text" NavigateUrlField="link" Depth="1" />        
        <asp:TreeNodeBinding DataMember="Navipro.Infojet.Lib.NavigationItem" TextField="text" NavigateUrlField="link" Depth="2" />        
    </DataBindings>
</asp:TreeView>
</div>

