<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HistoryOrder_STANDARD.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.HistoryOrder_STANDARD" %>
<%@ Register TagPrefix="Infojet" TagName="Translate" src="Translate.ascx" %>
<br />
<div class="orderHistory">

<div class="pane">
    <div>
        <label><Infojet:Translate runat="server" ID="Translate4" code="ORDER NO" /></label><br />
        <%= customerHistoryOrder.no %>
   </div>
    <div>
        <label style="font-size: 9px;"><Infojet:Translate runat="server" ID="Translate7" code="CUSTOMER NO" /></label><br />
        <%= customerHistoryOrder.customerNo %><br />
    </div>       
</div>
<br />&nbsp;<br />
<div class="pane">
    <div>
        <label><Infojet:Translate runat="server" ID="billToAddress" code="BILL-TO ADDRESS HEAD" /></label><br />
        <%= customerHistoryOrder.customerName %><br />
        <%= customerHistoryOrder.address %><br />
        <%= customerHistoryOrder.address2 %><br />
        <%= customerHistoryOrder.postCode %>&nbsp; <%= customerHistoryOrder.city %>        
    </div>
    <div>
        <label style="font-size: 9px;"><Infojet:Translate runat="server" ID="shipToAddress" code="SHIP-TO ADDRESS HEAD" /></label><br />
        <%= customerHistoryOrder.shipToName %><br />
        <%= customerHistoryOrder.shipToAddress %><br />
        <%= customerHistoryOrder.shipToAddress2 %><br />
        <%= customerHistoryOrder.shipToPostCode %>&nbsp; <%= customerHistoryOrder.shipToCity %>        
    </div>       
</div>
<br />&nbsp;<br />
<div class="pane">
    <div>
        <label><Infojet:Translate runat="server" ID="shipmentMethod" code="SHIPMENT METHOD" /></label><br />
        <%= customerHistoryOrder.shipmentMethodCode %><br />
        <label><Infojet:Translate runat="server" ID="ahippingAgent" code="SHIPPING AGENT" /></label><br />
        <%= customerHistoryOrder.shippingAgentCode %><br />
        <label><Infojet:Translate runat="server" ID="shippingAgentServ" code="SHIPPING AGENT SERV" /></label><br />
        <%= customerHistoryOrder.shippingAgentServiceCode %><br />
        <label><Infojet:Translate runat="server" ID="yourReference" code="CONTACT NAME" /></label><br />
        <%= customerHistoryOrder.sellToContact %><br />
   </div>
    <div>
        <label><Infojet:Translate runat="server" ID="Translate5" code="CUSTOMER ORDER NO" /></label><br />
        <%= customerHistoryOrder.externalDocumentNo %><br />
        <label><Infojet:Translate runat="server" ID="Translate2" code="ORDER DATE" /></label><br />
        <%= customerHistoryOrder.orderDate %><br />
        <label><Infojet:Translate runat="server" ID="Translate1" code="AMOUNT" /></label><br />
        <%= customerHistoryOrder.amount %><br />
        <label><Infojet:Translate runat="server" ID="Translate3" code="AMOUNT INCLUDING VAT" /></label><br />
        <%= customerHistoryOrder.amountIncludingVat %><br />
    </div>       
</div>
<br />&nbsp;<br />
<asp:Repeater runat="server" ID="orderLineRepeater">
<HeaderTemplate>
    <table class="tableView" width="100%">
    <tr>
        <th class="itemListHeader"><Infojet:Translate runat="server" ID="no" code="NO" /></th>
        <th class="itemListHeader"><Infojet:Translate runat="server" ID="description" code="DESCRIPTION" /></th>
        <th class="itemListHeader" style="text-align: right;"><Infojet:Translate runat="server" ID="unitPrice" code="UNIT PRICE" /></th>
        <th class="itemListHeader" style="text-align: right;"><Infojet:Translate runat="server" ID="quantity" code="QUANTITY" /></th>
        <th class="itemListHeader"><Infojet:Translate runat="server" ID="unit" code="UNIT OF MEASURE" /></th>
        <th class="itemListHeader"><Infojet:Translate runat="server" ID="shipmentDate" code="SHIPMENT DATE" /></th>
        <th class="itemListHeader" style="text-align: right;"><Infojet:Translate runat="server" ID="amount" code="AMOUNT" /></th>
        <th class="itemListHeader" style="text-align: right;"><Infojet:Translate runat="server" ID="amountInclVat" code="AMOUNT INCLUDING VAT" /></th>
    </tr>
</HeaderTemplate>

<ItemTemplate>
        <tr>
            <td class="itemListAltLine"><%#DataBinder.Eval(Container.DataItem, "no")%>&nbsp;</td>
            <td class="itemListAltLine"><%#DataBinder.Eval(Container.DataItem, "description")%>&nbsp;</td>
            <td class="itemListAltLine" style="text-align: right;">&nbsp;<%#DataBinder.Eval(Container.DataItem, "unitPrice")%></td>
            <td class="itemListAltLine" style="text-align: right;">&nbsp;<%#DataBinder.Eval(Container.DataItem, "quantity")%></td>
            <td class="itemListAltLine"><%#DataBinder.Eval(Container.DataItem, "unitOfMeasureCode")%>&nbsp;</td>
            <td class="itemListAltLine"><%#DataBinder.Eval(Container.DataItem, "shipmentDate")%>&nbsp;</td>
            <td class="itemListAltLine" style="text-align: right;">&nbsp;<%#DataBinder.Eval(Container.DataItem, "amount")%></td>
            <td class="itemListAltLine" style="text-align: right;">&nbsp;<%#DataBinder.Eval(Container.DataItem, "amountInclVat")%></td>
        </tr>    

</ItemTemplate>

<AlternatingItemTemplate>
        <tr>
            <td class="itemListLine"><%#DataBinder.Eval(Container.DataItem, "no")%>&nbsp;</td>
            <td class="itemListLine"><%#DataBinder.Eval(Container.DataItem, "description")%>&nbsp;</td>
            <td class="itemListLine" style="text-align: right;">&nbsp;<%#DataBinder.Eval(Container.DataItem, "unitPrice")%></td>
            <td class="itemListLine" style="text-align: right;">&nbsp;<%#DataBinder.Eval(Container.DataItem, "quantity")%></td>
            <td class="itemListLine"><%#DataBinder.Eval(Container.DataItem, "unitOfMeasureCode")%>&nbsp;</td>
            <td class="itemListLine"><%#DataBinder.Eval(Container.DataItem, "shipmentDate")%>&nbsp;</td>
            <td class="itemListLine" style="text-align: right;">&nbsp;<%#DataBinder.Eval(Container.DataItem, "amount")%></td>
            <td class="itemListLine" style="text-align: right;">&nbsp;<%#DataBinder.Eval(Container.DataItem, "amountInclVat")%></td>
        </tr>    



</AlternatingItemTemplate>

<FooterTemplate>
    </table>
</FooterTemplate>
</asp:Repeater>

</div>