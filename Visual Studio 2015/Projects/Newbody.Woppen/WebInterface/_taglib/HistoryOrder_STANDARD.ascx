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
        <%= customerHistoryOrder.shippingAgentName %><br />
        <label><Infojet:Translate runat="server" ID="shippingAgentServ" code="SHIPPING AGENT SERV" /></label><br />
        <%= customerHistoryOrder.shippingAgentServiceName %><br />
        <label><Infojet:Translate runat="server" ID="yourReference" code="CONTACT PERSON" /></label><br />
        <%= customerHistoryOrder.sellToContact %><br />
   </div>
    <div style="width: 400px">
        <label><Infojet:Translate runat="server" ID="Translate2" code="ORDER DATE" /></label><br />
        <%= customerHistoryOrder.orderDate %><br />
        <label><Infojet:Translate runat="server" ID="Translate1" code="AMOUNT" /></label><br />
        <%= customerHistoryOrder.amount %><br />
        <label><Infojet:Translate runat="server" ID="documents" code="PDF" /></label><br />
        <a href="<%= orderConfirmationPdfUrl %>" target="_blank"><Infojet:Translate runat="server" ID="Translate5" code="GET ORDER CONF" /></a> | <a href="<%= pickListPdfUrl %>" target="_blank"><Infojet:Translate runat="server" ID="Translate6" code="GET PICK LIST" /></a><br />
    </div>       
</div>
<br />&nbsp;<br />
<asp:Repeater runat="server" ID="orderLineRepeater">
<HeaderTemplate>
    <table class="tableView" width="100%">
    <tr>
        <th><Infojet:Translate runat="server" ID="no" code="NO" /></th>
        <th><Infojet:Translate runat="server" ID="description" code="DESCRIPTION" /></th>
        <th style="text-align: right;"><Infojet:Translate runat="server" ID="unitPrice" code="UNIT PRICE" /></th>
        <th style="text-align: right;"><Infojet:Translate runat="server" ID="quantity" code="QUANTITY" /></th>
        <th><Infojet:Translate runat="server" ID="shipmentDate" code="SHIPMENT DATE" /></th>
        <th style="text-align: right;"><Infojet:Translate runat="server" ID="amount" code="AMOUNT" /></th>
    </tr>
</HeaderTemplate>

<ItemTemplate>
        <tr>
            <td><%#DataBinder.Eval(Container.DataItem, "no")%></td>
            <td><%#DataBinder.Eval(Container.DataItem, "description")%></td>
            <td style="text-align: right;"><%#DataBinder.Eval(Container.DataItem, "unitPrice")%></td>
            <td style="text-align: right;"><%#DataBinder.Eval(Container.DataItem, "quantity")%> <%#DataBinder.Eval(Container.DataItem, "unitOfMeasureCode")%></td>
            <td><%#DataBinder.Eval(Container.DataItem, "shipmentDate")%></td>
            <td style="text-align: right;"><%#DataBinder.Eval(Container.DataItem, "amount")%></td>
        </tr>    

</ItemTemplate>

<FooterTemplate>
    </table>
</FooterTemplate>
</asp:Repeater>

</div>