<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductDetails_STANDARD.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.ProductDetails_STANDARD" %>

<div class="userControl">
    <h1><asp:Label runat="server" ID="productHeader"></asp:Label></h1>
    <div style="float: left; padding-top: 5px;">
        <div style="float: left; padding-right: 5px; width: 250px;">
            <div style="height: 250px;">
                <asp:Image runat="server" id="productImage"/><asp:Image runat="server" id="noProductImage" ImageUrl="/_assets/img/no_image.jpg" Visible="false" /><br /><br />
            </div>
            <asp:Repeater runat="server" ID="productImageRepeater">
                <ItemTemplate>
                    <a href="<%#DataBinder.Eval(Container.DataItem, "changeUrl")%>"><img src="<%#DataBinder.Eval(Container.DataItem, "url")%>" alt="<%#DataBinder.Eval(Container.DataItem, "description")%>" /></a>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div style="padding-left: 5px; float: left; background-color: #f5f5f5; width: 400px; height: 300px; border-top: dashed 1px #777777; border-bottom: dashed 1px #777777;">
            <asp:label runat="server" ID="productNoLabel" Width="150" Font-Bold="true"/> <asp:label runat="server" ID="productNo"/><br />
            <asp:label runat="server" ID="manufacturerLabel" Width="150" Font-Bold="true"/> <asp:label runat="server" ID="manufacturer"/><br />
            <asp:label runat="server" ID="inventoryLabel" Width="150" Font-Bold="true"/> <asp:label runat="server" ID="inventory"/><br />            
            <asp:label runat="server" ID="leadTimeLabel" Width="150" Font-Bold="true"/> <asp:label runat="server" ID="leadTime"/><br />
            <asp:label runat="server" ID="unitListPriceLabel" Width="150" Font-Bold="true"/> <asp:label runat="server" ID="unitListPrice"/><br />
            <asp:repeater runat="server" ID="itemAttributeRepeater">
                <ItemTemplate>
                <asp:label runat="server" ID="unitListPriceLabel" Width="150" Font-Bold="true"><%#DataBinder.Eval(Container.DataItem, "text")%></asp:label> <%#DataBinder.Eval(Container.DataItem, "itemValue")%><br />
                </ItemTemplate>
            </asp:repeater>
            <br />
            <asp:label runat="server" ID="unitPrice" Width="150" Font-Bold="true" Font-Size="16px" ForeColor="Red"/><br />
            <br />
            <asp:Panel runat="server" ID="buyPanel" Visible="false">
                <asp:label runat="server" ID="quantityLabel" Font-Bold="true"/> <asp:TextBox runat="server" ID="quantityBox" CssClass="Textfield" Width="30px"></asp:TextBox> 
                <asp:Button runat="server" ID="buyButton" CssClass="Button" 
                    onclick="buyButton_Click" />
            </asp:Panel>
            <br />
        </div>
    </div>
    <div style="float: left; width: 400px;">
        <asp:Label runat="server" ID="productDescription"></asp:Label>    
    </div>    
</div>