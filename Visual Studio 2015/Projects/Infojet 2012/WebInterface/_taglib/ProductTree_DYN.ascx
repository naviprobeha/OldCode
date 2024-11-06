<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductTree_DYN.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.ProductTree_DYN" %>

<div class="userControl">

<asp:Repeater ID="productTree" runat="server">
<HeaderTemplate><ul></HeaderTemplate>

<ItemTemplate>
    <li><a href="<%#DataBinder.Eval(Container.DataItem, "link")%>"><%#DataBinder.Eval(Container.DataItem, "text")%></a></li>
    <asp:Repeater ID="productSubTree" runat="server" DataSource='<%#DataBinder.Eval(Container.DataItem, "subNavigationItems")%>'>
        <HeaderTemplate><ul></HeaderTemplate>

        <ItemTemplate>
            <li><a href="<%#DataBinder.Eval(Container.DataItem, "link")%>"><%#DataBinder.Eval(Container.DataItem, "text")%></a></li>
            <asp:Repeater ID="productSubTree" runat="server" DataSource='<%#DataBinder.Eval(Container.DataItem, "subNavigationItems")%>'>
                <HeaderTemplate><ul></HeaderTemplate>
                <ItemTemplate>
                    <li><a href="<%#DataBinder.Eval(Container.DataItem, "link")%>"><%#DataBinder.Eval(Container.DataItem, "text")%></a></li>
                </ItemTemplate>
                <FooterTemplate></ul></FooterTemplate>   
            </asp:Repeater>

        </ItemTemplate>

        <FooterTemplate></ul></FooterTemplate>   
    </asp:Repeater>

</ItemTemplate>

<FooterTemplate></ul></FooterTemplate>
</asp:Repeater>


</div>