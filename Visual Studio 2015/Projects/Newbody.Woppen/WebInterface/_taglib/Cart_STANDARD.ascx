<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Cart_STANDARD.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.Cart_STANDARD" %>

<asp:Panel runat="server" id="cartPanel">
    <div class="userControl">
        <asp:Label runat="server" ID="cartLabel" Font-Size="11px" Font-Bold="true"></asp:Label><br />
        <select name="cartDropDown" class="DropDown" style="width: 170px">    
            <asp:Repeater runat="server" ID="cartList">
                <ItemTemplate>
                    <option value=""><%#DataBinder.Eval(Container.DataItem, "quantity")%> x <%#DataBinder.Eval(Container.DataItem, "description")%> (<%#DataBinder.Eval(Container.DataItem, "formatedAmount")%>)</option>
                </ItemTemplate>
            </asp:Repeater>
        </select><br />
        <asp:Label runat="server" ID="totalLabel" Font-Size="11px"></asp:Label>: <asp:Label runat="server" ID="total" Font-Size="11px" Font-Bold="true"></asp:Label><br />
        <asp:Button runat="server" ID="clearButton" CssClass="Button" 
            onclick="clearButton_Click" /> <asp:Button runat="server" ID="checkOutButton" CssClass="Button" /><br />
    </div>
    
</asp:Panel>         
