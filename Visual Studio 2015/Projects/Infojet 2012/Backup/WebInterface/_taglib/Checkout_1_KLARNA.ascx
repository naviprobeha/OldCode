<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Checkout_1_KLARNA.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.Checkout_1_KLARNA" %>
<%@ Register TagPrefix="Infojet" TagName="Translate" src="Translate.ascx" %>


<div class="orderHistory">

<br/>&nbsp;<br/>
<asp:Label ID="errorMessage" runat="server" ForeColor="Red" Visible="False"></asp:Label>
<br />&nbsp;<br />



<table class="tableView" style="width: 100%">
    <tr>
        <th class="itemListHeader"><Infojet:Translate runat="server" ID="itemNo" code="ITEM NO"/></th>
        <th class="itemListHeader"><Infojet:Translate runat="server" ID="description" code="DESCRIPTION"/></th>
        <th class="itemListHeader"><Infojet:Translate runat="server" ID="Translate14" code="REFERENCE"/></th>
        <th class="itemListHeader" style="text-align: right;"><Infojet:Translate runat="server" ID="unitPrice" code="UNIT PRICE"/></th>            
        <th class="itemListHeader" style="text-align: right;"><Infojet:Translate runat="server" ID="quantity" code="QUANTITY"/></th>
        <th class="itemListHeader" style="text-align: right;"><Infojet:Translate runat="server" ID="amount" code="AMOUNT"/></th>
        <th class="itemListHeader" style="text-align: right;">&nbsp;</th>
    </tr>


    <asp:Repeater runat="server" ID="cartItemRepeater" OnItemCommand="updateCart_ItemCommand">
    <ItemTemplate>
        <tr>
            <td class="itemListAltLine" valign="top"><%#DataBinder.Eval(Container.DataItem, "itemNo")%></td>
            <td class="itemListAltLine" valign="top" align="left"><%#DataBinder.Eval(Container.DataItem, "description")%><asp:panel id="inventoryText" runat="server" Visible='<%# ((((Navipro.Infojet.Lib.CartItem)Container.DataItem).checkInventory == true) && (((Navipro.Infojet.Lib.CartItem)Container.DataItem).quantity > ((Navipro.Infojet.Lib.CartItem)Container.DataItem).inventory)) %>'><span style="color: red;"><Infojet:Translate runat="server" id="invTooLow" code="INV TOO LOW" /><br/>&nbsp;</span></asp:panel><br/><%#DataBinder.Eval(Container.DataItem, "fromDateText")%> - <%#DataBinder.Eval(Container.DataItem, "toDateText")%></td>
            <td class="itemListAltLine" valign="top" align="right"><div id="cartRef_<%#DataBinder.Eval(Container.DataItem, "lineNo")%>" style="float: left; text-align: right;"><%#DataBinder.Eval(Container.DataItem, "referenceNo")%></div><div id="changeCartRef_<%#DataBinder.Eval(Container.DataItem, "lineNo")%>" style="visibility: hidden; float: left;"><asp:textBox Id="referenceBox" Runat="server" Width="100px" Text='<%#DataBinder.Eval(Container.DataItem, "referenceNo")%>' CssClass="Textfield" /></div></td>
            <td class="itemListAltLine" valign="top" align="right"><%#DataBinder.Eval(Container.DataItem, "formatedUnitPrice")%></td>
            <td class="itemListAltLine" valign="top" align="right"><div id="cartQuantity_<%#DataBinder.Eval(Container.DataItem, "lineNo")%>" style="float: right; text-align: right;"><%#DataBinder.Eval(Container.DataItem, "quantity")%></div><div id="changeCartQuantity_<%#DataBinder.Eval(Container.DataItem, "lineNo")%>" style="visibility: hidden; float: right;"><asp:textBox Id="quantityBox" Runat="server" Width="40px" Text='<%#DataBinder.Eval(Container.DataItem, "quantity")%>' CssClass="Textfield" /></div></td>
            <td class="itemListAltLine" valign="top" align="right"><%#DataBinder.Eval(Container.DataItem, "formatedAmount")%></td>
            <td class="itemListAltLine" valign="top" align="center"><div id="lineButtons_<%#DataBinder.Eval(Container.DataItem, "lineNo")%>"><a href="javascript:changeQuantity(<%#DataBinder.Eval(Container.DataItem, "lineNo")%>)"><Infojet:Translate runat="server" ID="Translate10" code="CHANGE" /></a> | <asp:LinkButton ID="removeLink" runat="server" CommandName="remove" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "lineNo")%>'><Infojet:Translate runat="server" ID="Translate3" code="REMOVE" /></asp:LinkButton></div><div id="changeButton_<%#DataBinder.Eval(Container.DataItem, "lineNo")%>" style="visibility: hidden;"><asp:LinkButton ID="updateQuantity" runat="server" CommandName="update" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "lineNo")%>'><Infojet:Translate ID="update" runat="server" code="SAVE" /></asp:LinkButton></div></td>
        </tr>
    </ItemTemplate>

    <AlternatingItemTemplate>
        <tr>
            <td class="itemListLine" valign="top"><%#DataBinder.Eval(Container.DataItem, "itemNo")%></td>
            <td class="itemListLine" valign="top" align="left"><%#DataBinder.Eval(Container.DataItem, "description")%><asp:panel id="inventoryText" runat="server" Visible='<%# ((((Navipro.Infojet.Lib.CartItem)Container.DataItem).checkInventory == true) && (((Navipro.Infojet.Lib.CartItem)Container.DataItem).quantity > ((Navipro.Infojet.Lib.CartItem)Container.DataItem).inventory)) %>'><span style="color: red;"><Infojet:Translate runat="server" id="invTooLow" code="INV TOO LOW" /><br/>&nbsp;</span></asp:panel><br/><%#DataBinder.Eval(Container.DataItem, "fromDateText")%> - <%#DataBinder.Eval(Container.DataItem, "toDateText")%></td>
            <td class="itemListLine" valign="top" align="right"><div id="cartRef_<%#DataBinder.Eval(Container.DataItem, "lineNo")%>" style="float: left; text-align: right;"><%#DataBinder.Eval(Container.DataItem, "referenceNo")%></div><div id="changeCartRef_<%#DataBinder.Eval(Container.DataItem, "lineNo")%>" style="visibility: hidden; float: left;"><asp:textBox Id="referenceBox" Runat="server" Width="100px" Text='<%#DataBinder.Eval(Container.DataItem, "referenceNo")%>' CssClass="Textfield" /></div></td>
            <td class="itemListLine" valign="top" align="right"><%#DataBinder.Eval(Container.DataItem, "formatedUnitPrice")%></td>
            <td class="itemListLine" valign="top" align="right"><div id="cartQuantity_<%#DataBinder.Eval(Container.DataItem, "lineNo")%>" style="float: right; text-align: right;"><%#DataBinder.Eval(Container.DataItem, "quantity")%></div><div id="changeCartQuantity_<%#DataBinder.Eval(Container.DataItem, "lineNo")%>" style="visibility: hidden; float: right;"><asp:textBox Id="quantityBox" Runat="server" Width="40px" Text='<%#DataBinder.Eval(Container.DataItem, "quantity")%>' CssClass="Textfield" /></div></td>
            <td class="itemListLine" valign="top" align="right"><%#DataBinder.Eval(Container.DataItem, "formatedAmount")%></td>
            <td class="itemListLine" valign="top" align="center"><div id="lineButtons_<%#DataBinder.Eval(Container.DataItem, "lineNo")%>"><a href="javascript:changeQuantity(<%#DataBinder.Eval(Container.DataItem, "lineNo")%>)"><Infojet:Translate runat="server" ID="Translate10" code="CHANGE" /></a> | <asp:LinkButton ID="removeLink" runat="server" CommandName="remove" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "lineNo")%>'><Infojet:Translate runat="server" ID="Translate3" code="REMOVE" /></asp:LinkButton></div><div id="changeButton_<%#DataBinder.Eval(Container.DataItem, "lineNo")%>" style="visibility: hidden;"><asp:LinkButton ID="updateQuantity" runat="server" CommandName="update" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "lineNo")%>'><Infojet:Translate ID="update" runat="server" code="SAVE" /></asp:LinkButton></div></td>
        </tr>
    </AlternatingItemTemplate>    
    
    </asp:Repeater>

    <% if (!webCheckout.checkMinQuantities()) { %>
    <tr>
        <td class="itemListAltLine">&nbsp;</td>
        <td class="itemListAltLine" align="left" style="color: red;"><Infojet:Translate runat="server" ID="minOrderQtyLabel" code="MIN ORDER QTY"/></td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right"><b>&nbsp;</b></td>
    </tr>
    <% } %>
    
    <% if (webCartHeader.freightFee != 0) { %>
    <tr>
        <td class="itemListAltLine">&nbsp;</td>
        <td class="itemListAltLine" align="left"><Infojet:Translate runat="server" ID="Translate12" code="FREIGHT"/></td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right"><asp:Label ID="freightAmountLabel" runat="server"></asp:Label></td>
        <td class="itemListAltLine" align="right"><b>&nbsp;</b></td>
    </tr>
    <% } %>
    <% if (webCartHeader.adminFee != 0) { %>
    <tr>
        <td class="itemListAltLine">&nbsp;</td>
        <td class="itemListAltLine" align="left"><Infojet:Translate runat="server" ID="Translate13" code="ADMIN FEE"/></td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right"><asp:Label ID="adminAmountLabel" runat="server"></asp:Label></td>
        <td class="itemListAltLine" align="right"><b>&nbsp;</b></td>
    </tr>
    <% } %>
    <% if (!webCartHeader.pricesInclVat) { %>
    <tr>
        <td class="itemListAltLine">&nbsp;</td>
        <td class="itemListAltLine" align="left"><b><Infojet:Translate runat="server" ID="total" code="SUBTOTAL"/></b></td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right"><b>&nbsp;</b></td>
        <td class="itemListAltLine" align="right"><b><asp:Label ID="totalLabel" runat="server"></asp:Label></b></td>
        <td class="itemListAltLine" align="right"><b>&nbsp;</b></td>
    </tr>  
    <tr>
        <td class="itemListAltLine">&nbsp;</td>
        <td class="itemListAltLine" align="left"><Infojet:Translate runat="server" ID="Translate8" code="VAT AMOUNT"/></td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right"><b>&nbsp;</b></td>
        <td class="itemListAltLine" align="right"><asp:Label ID="vatAmountLabel" runat="server"></asp:Label></td>
        <td class="itemListAltLine" align="right"><b>&nbsp;</b></td>
    </tr> 
    <tr>
        <td class="itemListAltLine">&nbsp;</td>
        <td class="itemListAltLine" align="left"><b><Infojet:Translate runat="server" ID="Translate9" code="TOTAL INCL VAT"/></b></td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right"><b>&nbsp;</b></td>
        <td class="itemListAltLine" align="right"><b><asp:Label ID="totalInclVatLabel" runat="server"></asp:Label></b></td>
        <td class="itemListAltLine" align="right"><b>&nbsp;</b></td>
    </tr>        
    <% } %>
    <% if (webCartHeader.pricesInclVat) { %>
    <tr>
        <td class="itemListAltLine">&nbsp;</td>
        <td class="itemListAltLine" align="left"><b><Infojet:Translate runat="server" ID="Translate22" code="TOTAL INCL VAT"/></b></td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right"><b>&nbsp;</b></td>
        <td class="itemListAltLine" align="right"><b><%= infojet.systemDatabase.formatCurrency(webCheckout.getTotalAmountInclVat(), infojet.currencyCode) %></b></td>
        <td class="itemListAltLine" align="right"><b>&nbsp;</b></td>
    </tr>           
    <tr>
        <td class="itemListAltLine">&nbsp;</td>
        <td class="itemListAltLine" align="left"><Infojet:Translate runat="server" ID="Translate23" code="VAT AMOUNT"/></td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right"><b>&nbsp;</b></td>
        <td class="itemListAltLine" align="right"><%= infojet.systemDatabase.formatCurrency(webCheckout.getTotalVatAmount(), infojet.currencyCode) %></td>
        <td class="itemListAltLine" align="right"><b>&nbsp;</b></td>
    </tr> 
    <% } %>
</table>
  
  
  

<asp:panel id="discountPanel" runat="server"> 
<br />&nbsp;<br />
<div class="pane">
    <%
    	Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();
    	applyDiscountButton.Text = infojet.translate("APPLY DISCOUNT");
    %>
    <div >
        <label><Infojet:Translate runat="server" ID="discountCode" code="DISCOUNT CODE" /></label><br />
        <asp:TextBox ID="discountCodeBox" runat="server" CssClass="Textfield"></asp:TextBox>&nbsp;<asp:Button ID="applyDiscountButton" runat="server" CssClass="Button"/>
    </div>
</div>

</asp:panel>

</div>

<div class="inputForm">

<div class="pane">
    <asp:Panel runat="server" ID="checkoutForm" BackColor="#f5f5f5" Font-Size="11px"></asp:Panel>
</div>

</div>
