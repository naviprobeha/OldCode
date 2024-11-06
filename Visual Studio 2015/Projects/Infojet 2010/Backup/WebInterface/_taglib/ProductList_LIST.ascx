<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductList_LIST.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.ProductList_LIST" %>

<script type="text/javascript">

function OnHover(This, normalCssName)
{  
   This.Class = normalCssName;
   This.className = "itemListLineHover";
                    
}  

function OffHover(This)
{
   This.className = This.Class;

}

function Click(This, link)
{
    document.location.href = link;
}

</script>

<asp:panel runat="server" ID="itemFilterForm" CssClass="itemFilterForm">
    <div class="itemListHeader" style="float: left; width: 790px;">
        <div style="float: left; padding: 5px; width: 250px;" class="middle">
            <asp:CheckBox runat="server" ID="showImages" AutoPostBack="true"/> <asp:Label runat="server" ID="showImagesLabel" Height="20"></asp:Label><br />
            <asp:CheckBox runat="server" ID="showItemNo" AutoPostBack="true"/> <asp:Label runat="server" ID="showItemNoLabel" Height="20"></asp:Label><br />
            <asp:CheckBox runat="server" ID="showInventoryOnly" AutoPostBack="true"/> <asp:Label runat="server" ID="showInventoryOnlyLabel" Height="20"></asp:Label>
        </div>
        <div style="float: left; padding: 5px; width: 300px;">
            <asp:label runat="server" ID="sorting1Label" Width="80px" Height="20"></asp:label> 
            <asp:DropDownList runat="server" ID="sorting1List" CssClass="DropDown" 
                AutoPostBack="true" onselectedindexchanged="sorting1List_SelectedIndexChanged"></asp:DropDownList> <asp:DropDownList runat="server" ID="sorting2List" CssClass="DropDown" AutoPostBack="true"></asp:DropDownList><br />
            <asp:label runat="server" ID="filterLabel" Width="80px" Height="20"></asp:label> <asp:TextBox runat="server" ID="filterBox" CssClass="Textfield"/> <asp:Button runat="server" ID="filterButton" CssClass="Button"/><br />
        </div>
    </div>
    <br />&nbsp;
    <asp:Repeater ID="itemAttributeFilterRepeater" runat="server">
        <HeaderTemplate>
            <div class="itemListHeader" style="float: left; width: 790px;">        
        </HeaderTemplate>
        <ItemTemplate>
            <div style="float: left; padding: 5px; width: 150px;">
                <%#DataBinder.Eval(Container.DataItem, "text")%><br />
                <asp:DropDownList runat="server" ID="attributeFilter" CssClass="DropDown" AutoPostBack="true" onSelectedIndexChanged="attributeFilterList_SelectedIndexChanged" DataSource='<%#DataBinder.Eval(Container.DataItem, "itemAttributeValueCollection")%>' DataValueField="itemValue" DataTextField="text">
                </asp:DropDownList>
            </div>
        </ItemTemplate>
        
        <FooterTemplate>
            </div>
        </FooterTemplate>
    </asp:Repeater>
</asp:panel>
&nbsp;<br />
<asp:GridView ID="itemListGrid" runat="server" AutoGenerateColumns="false" 
    Width="790px" BorderWidth="0px" onrowdatabound="itemListGrid_RowDataBound">
    <HeaderStyle CssClass="itemListHeader" />
    <RowStyle CssClass="itemListAltLine"/>
    <AlternatingRowStyle CssClass="itemListLine"/> 
 
</asp:GridView>
