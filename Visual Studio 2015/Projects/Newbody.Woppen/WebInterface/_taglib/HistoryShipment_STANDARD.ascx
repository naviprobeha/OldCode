<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HistoryShipment_STANDARD.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.HistoryShipment_STANDARD" %>
<%@ Register TagPrefix="Infojet" TagName="Translate" src="Translate.ascx" %>
<br />
<div class="orderHistory">

<div class="pane">
    <div>
        <label><Infojet:Translate runat="server" ID="shipmentNo" code="SHIPMENT NO" /></label> / <label><Infojet:Translate runat="server" ID="Translate4" code="ORDER NO" /></label><br />
        <%= customerHistoryShipment.no %> / <%= customerHistoryShipment.orderNo %>
   </div>
    <div>
        <label style="font-size: 9px;"><Infojet:Translate runat="server" ID="Translate7" code="CUSTOMER NO" /></label><br />
        <%= customerHistoryShipment.customerNo%><br />
    </div>       
</div>
<br />&nbsp;<br />
<div class="pane">
    <div>
        <label><Infojet:Translate runat="server" ID="billToAddress" code="BILL-TO ADDRESS HEAD" /></label><br />
        <%= customerHistoryShipment.customerName%><br />
        <%= customerHistoryShipment.address%><br />
        <%= customerHistoryShipment.address2%><br />
        <%= customerHistoryShipment.postCode%>&nbsp; <%= customerHistoryShipment.city%>        
    </div>
    <div>
        <label style="font-size: 9px;"><Infojet:Translate runat="server" ID="shipToAddress" code="SHIP-TO ADDRESS HEAD" /></label><br />
        <%= customerHistoryShipment.shipToName%><br />
        <%= customerHistoryShipment.shipToAddress%><br />
        <%= customerHistoryShipment.shipToAddress2%><br />
        <%= customerHistoryShipment.shipToPostCode%>&nbsp; <%= customerHistoryShipment.shipToCity%>        
    </div>       
</div>
<br />&nbsp;<br />
<div class="pane">
    <div>
        <label><Infojet:Translate runat="server" ID="shipmentMethod" code="SHIPMENT METHOD" /></label><br />
        <%= customerHistoryShipment.shipmentMethodCode%><br />
        <label><Infojet:Translate runat="server" ID="ahippingAgent" code="SHIPPING AGENT" /></label><br />
        <%= customerHistoryShipment.shippingAgentName%><br />
        <label><Infojet:Translate runat="server" ID="shippingAgentServ" code="SHIPPING AGENT SERV" /></label><br />
        <%= customerHistoryShipment.shippingAgentServiceName%><br />
        <label><Infojet:Translate runat="server" ID="yourReference" code="CONTACT PERSON" /></label><br />
        <%= customerHistoryShipment.sellToContact%><br />
   </div>
    <div>
        <label><Infojet:Translate runat="server" ID="Translate2" code="ORDER DATE" /></label><br />
        <%= customerHistoryShipment.orderDate%><br />
        <label><Infojet:Translate runat="server" ID="Translate1" code="SHIPMENT DATE" /></label><br />
        <%= customerHistoryShipment.shipmentDate%><br />
        <label><Infojet:Translate runat="server" ID="Translate5" code="EXTERNAL DOC NO" /></label><br />
        <%= customerHistoryShipment.externalDocumentNo%><br />
        <label><Infojet:Translate runat="server" ID="Translate3" code="PACKAGE TRACKING NO" /></label><br />
        
        <%
            Navipro.Infojet.Lib.ShippingAgent shippingAgent = new Navipro.Infojet.Lib.ShippingAgent(infojet.systemDatabase, customerHistoryShipment.shippingAgentCode);
            string[] trackingArray = customerHistoryShipment.packageTrackingNo.Split(new Char[] { ',' });
            int i = 0;
            while (i < trackingArray.Length)
            {
                %><a target="_blank" href="<%= shippingAgent.internetAddress.Replace("%1", trackingArray[i]); %>"><%= trackingArray[i] %></a><br /><%
                i++;
            }
        %>
    </div>       
</div>
<br />&nbsp;<br />
<asp:Repeater runat="server" ID="shipmentLineRepeater">
<HeaderTemplate>
    <table class="tableView" width="100%">
    <tr>
        <th><Infojet:Translate runat="server" ID="no" code="NO" /></th>
        <th><Infojet:Translate runat="server" ID="description" code="DESCRIPTION" /></th>
        <th style="text-align: right;"><Infojet:Translate runat="server" ID="quantity" code="QUANTITY" /></th>
        <th><Infojet:Translate runat="server" ID="shipmentDate" code="SHIPMENT DATE" /></th>
    </tr>
</HeaderTemplate>

<ItemTemplate>
        <tr>
            <td><%#DataBinder.Eval(Container.DataItem, "no")%></td>
            <td><%#DataBinder.Eval(Container.DataItem, "description")%></td>
            <td style="text-align: right;"><%#DataBinder.Eval(Container.DataItem, "quantity")%> <%#DataBinder.Eval(Container.DataItem, "unitOfMeasureCode")%></td>
            <td><%#DataBinder.Eval(Container.DataItem, "shipmentDate")%></td>
        </tr>    

</ItemTemplate>

<FooterTemplate>
    </table>
</FooterTemplate>
</asp:Repeater>

</div>