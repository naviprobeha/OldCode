<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Checkout_4_STANDARD.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.Checkout_4_STANDARD" %>
<%@ Register TagPrefix="Infojet" TagName="Translate" src="Translate.ascx" %>

<div class="orderHistory">
<div style="color: red"><%= errorMessage %></div>
<div class="pane">
    <div>
        <label style="font-size: 9px;"><Infojet:Translate runat="server" ID="shipToAddress" code="SHIP-TO ADDRESS HEAD" /></label><br />
        <%= webCartHeader.shipToName%><br />
        <%= webCartHeader.shipToAddress%><br />
        <%= webCartHeader.shipToAddress2%><br />
        <%= webCartHeader.shipToPostCode%>&nbsp; <%= webCartHeader.shipToCity%><br />      
        <%= webCartHeader.shipToCountryCode%><br />
   </div>       
    <div>
        <label style="font-size: 9px;"><Infojet:Translate runat="server" ID="Translate1" code="BILL-TO ADDRESS HEAD" /></label><br />
        <%= webCartHeader.billToName%><br />
        <%= webCartHeader.billToAddress%><br />
        <%= webCartHeader.billToAddress2%><br />
        <%= webCartHeader.billToPostCode%>&nbsp; <%= webCartHeader.shipToCity%> <br />       
        <%= webCartHeader.billToCountryCode%><br />
    </div>       
   
</div>
<br />&nbsp;<br />
<div class="pane"> 
    <div>
        <label><Infojet:Translate runat="server" ID="Translate2" code="CONTACT NAME" /></label><br />
        <%= webCartHeader.contactName%><br />
        <label><Infojet:Translate runat="server" ID="Translate3" code="PHONE NO" /></label><br />
        <%= webCartHeader.phoneNo%><br />
        <label><Infojet:Translate runat="server" ID="Translate5" code="EMAIL" /></label><br />
        <%= webCartHeader.email%><br />
   </div>      
    <div>
        <label><Infojet:Translate runat="server" ID="Translate7" code="CUSTOMER ORDER NO" /></label><br />
        <%= webCartHeader.customerOrderNo%><br />
        <label><Infojet:Translate runat="server" ID="Translate6" code="NOTE OF GOODS" /></label><br />
        <%= webCartHeader.noteOfGoods%><br />
        <label><Infojet:Translate runat="server" ID="Translate10" code="SHIPMENT DATE" /></label><br />
        <%= webCartHeader.shipmentDate.ToString("yyyy-MM-dd") %><br />
   </div>      
</div>

<br />&nbsp;<br />
<div class="pane"> 
    <div>
        <label><Infojet:Translate runat="server" ID="Translate4" code="SHIPMENT METHOD" /></label><br />
        <%= webCartHeader.webShipmentMethod.description%><br />        
    </div>      
    <div>
        <label><Infojet:Translate runat="server" ID="Translate11" code="PAYMENT METHOD" /></label><br />
        <%= webCartHeader.webPaymentMethod.description%><br />        
    </div></div>

</div>
<br />&nbsp;<br />

<table class="tableView" style="width: 100%">
    <tr>
        <th class="itemListHeader"><Infojet:Translate runat="server" ID="itemNo" code="ITEM NO"/></th>
        <th class="itemListHeader"><Infojet:Translate runat="server" ID="description" code="DESCRIPTION"/></th>
        <th class="itemListHeader"><Infojet:Translate runat="server" ID="Translate14" code="REFERENCE"/></th>
        <th class="itemListHeader" style="text-align: right;"><Infojet:Translate runat="server" ID="unitPrice" code="UNIT PRICE"/></th>            
        <th class="itemListHeader" style="text-align: right;"><Infojet:Translate runat="server" ID="orderedQty" code="ORDERED QTY"/></th>
        <th class="itemListHeader" style="text-align: right;"><Infojet:Translate runat="server" ID="qtyToShip" code="QTY TO SHIP"/></th>
        <th class="itemListHeader" style="text-align: right;"><Infojet:Translate runat="server" ID="outstandingQty" code="OUTSTANDING QTY"/></th>
        <th class="itemListHeader" style="text-align: right;"><Infojet:Translate runat="server" ID="nextReceiptDate" code="NEXT RECEIPT DATE"/></th>
        <th class="itemListHeader" style="text-align: right;"><Infojet:Translate runat="server" ID="amount" code="AMOUNT"/></th>
    </tr>


    <asp:Repeater runat="server" ID="cartItemRepeater">
    <ItemTemplate>
        <tr>
            <td class="itemListAltLine"><%#DataBinder.Eval(Container.DataItem, "itemNo")%></td>
            <td class="itemListAltLine" align="left"><%#DataBinder.Eval(Container.DataItem, "description")%></td>
            <td class="itemListAltLine" align="right"><div id="cartRef_<%#DataBinder.Eval(Container.DataItem, "lineNo")%>" style="float: left; text-align: right;"><%#DataBinder.Eval(Container.DataItem, "referenceNo")%></div><div id="changeCartRef_<%#DataBinder.Eval(Container.DataItem, "lineNo")%>" style="visibility: hidden; float: left;"><asp:textBox Id="referenceBox" Runat="server" Width="100px" Text='<%#DataBinder.Eval(Container.DataItem, "referenceNo")%>' CssClass="Textfield" /></div></td>
            <td class="itemListAltLine" align="right"><%#DataBinder.Eval(Container.DataItem, "unitPrice", "{0:n2}")%></td>
            <td class="itemListAltLine" align="right"><%#DataBinder.Eval(Container.DataItem, "quantity")%></td>
            <td class="itemListAltLine" align="right"><%#DataBinder.Eval(Container.DataItem, "qtyToShip")%></td>
            <td class="itemListAltLine" align="right"><%#DataBinder.Eval(Container.DataItem, "outstandingQty")%></td>
            <td class="itemListAltLine" align="right" nowrap="nowrap">&nbsp;<%#DataBinder.Eval(Container.DataItem, "nextReceiptDate")%></td>
            <td class="itemListAltLine" align="right" nowrap="nowrap"><%#DataBinder.Eval(Container.DataItem, "formatedAmount")%></td>
        </tr>
    </ItemTemplate>

    <AlternatingItemTemplate>
        <tr>
            <td class="itemListLine"><%#DataBinder.Eval(Container.DataItem, "itemNo")%></td>
            <td class="itemListLine" align="left"><%#DataBinder.Eval(Container.DataItem, "description")%></td>
            <td class="itemListLine" align="right"><div id="cartRef_<%#DataBinder.Eval(Container.DataItem, "lineNo")%>" style="float: left; text-align: right;"><%#DataBinder.Eval(Container.DataItem, "referenceNo")%></div><div id="changeCartRef_<%#DataBinder.Eval(Container.DataItem, "lineNo")%>" style="visibility: hidden; float: left;"><asp:textBox Id="referenceBox" Runat="server" Width="100px" Text='<%#DataBinder.Eval(Container.DataItem, "referenceNo")%>' CssClass="Textfield" /></div></td>
            <td class="itemListLine" align="right"><%#DataBinder.Eval(Container.DataItem, "unitPrice", "{0:n2}")%></td>
            <td class="itemListLine" align="right"><%#DataBinder.Eval(Container.DataItem, "quantity")%></td>
            <td class="itemListLine" align="right"><%#DataBinder.Eval(Container.DataItem, "qtyToShip")%></td>
            <td class="itemListLine" align="right"><%#DataBinder.Eval(Container.DataItem, "outstandingQty")%></td>
            <td class="itemListLine" align="right" nowrap="nowrap">&nbsp;<%#DataBinder.Eval(Container.DataItem, "nextReceiptDate")%></td>
            <td class="itemListLine" align="right"><%#DataBinder.Eval(Container.DataItem, "formatedAmount")%></td>
        </tr>
    </AlternatingItemTemplate>    
    
    </asp:Repeater>

    <tr>
        <td class="itemListAltLine">&nbsp;</td>
        <td class="itemListAltLine" align="left"><Infojet:Translate runat="server" ID="Translate12" code="FREIGHT"/></td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right" nowrap="nowrap"><asp:Label ID="freightAmountLabel" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td class="itemListAltLine">&nbsp;</td>
        <td class="itemListAltLine" align="left"><Infojet:Translate runat="server" ID="Translate13" code="ADMIN FEE"/></td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right" nowrap="nowrap"><asp:Label ID="adminAmountLabel" runat="server"></asp:Label></td>
    </tr>

    <tr>
        <td class="itemListAltLine">&nbsp;</td>
        <td class="itemListAltLine" align="left"><b><Infojet:Translate runat="server" ID="total" code="SUBTOTAL"/></b></td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right"><b>&nbsp;</b></td>
        <td class="itemListAltLine" align="right" nowrap="nowrap"><b><asp:Label ID="totalLabel" runat="server"></asp:Label></b></td>
    </tr>  
    <tr>
        <td class="itemListAltLine">&nbsp;</td>
        <td class="itemListAltLine" align="left"><Infojet:Translate runat="server" ID="Translate8" code="VAT AMOUNT"/></td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right"><b>&nbsp;</b></td>
        <td class="itemListAltLine" align="right" nowrap="nowrap"><asp:Label ID="vatAmountLabel" runat="server"></asp:Label></td>
    </tr> 
    <tr>
        <td class="itemListAltLine">&nbsp;</td>
        <td class="itemListAltLine" align="left"><b><Infojet:Translate runat="server" ID="Translate9" code="TOTAL INCL VAT"/></b></td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right"><b>&nbsp;</b></td>
        <td class="itemListAltLine" align="right" nowrap="nowrap"><b><asp:Label ID="totalInclVatLabel" runat="server"></asp:Label></b></td>
    </tr>        
</table>

   
<div style="float:left; width: 100%;"><asp:Button ID="goBackButton" runat="server" CssClass="Button" />&nbsp;<asp:button ID="nextButton" runat="server" CssClass="Button" /></div>