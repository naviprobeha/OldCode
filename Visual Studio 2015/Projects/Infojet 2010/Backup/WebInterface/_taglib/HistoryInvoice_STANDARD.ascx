<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HistoryInvoice_STANDARD.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.HistoryInvoice_STANDARD" %>
<%@ Register TagPrefix="Infojet" TagName="Translate" src="Translate.ascx" %>
<br />
<div class="orderHistory">

<div class="pane">
    <div>
        <label><Infojet:Translate runat="server" ID="Translate4" code="INVOICE NO" /> / <Infojet:Translate runat="server" ID="Translate5" code="ORDER NO" /></label><br />
        <%= customerHistoryInvoice.no %> / <%= customerHistoryInvoice.orderNo %>
   </div>
    <div>
        <label style="font-size: 9px;"><Infojet:Translate runat="server" ID="Translate7" code="CUSTOMER NO" /></label><br />
        <%= customerHistoryInvoice.customerNo%><br />
    </div>       
</div>
<br />&nbsp;<br />
<div class="pane">
    <div>
        <label><Infojet:Translate runat="server" ID="billToAddress" code="BILL-TO ADDRESS HEAD" /></label><br />
        <%= customerHistoryInvoice.customerName%><br />
        <%= customerHistoryInvoice.address%><br />
        <%= customerHistoryInvoice.address2%><br />
        <%= customerHistoryInvoice.postCode%>&nbsp; <%= customerHistoryInvoice.city%>        
    </div>
    <div>
        <label style="font-size: 9px;"><Infojet:Translate runat="server" ID="shipToAddress" code="SHIP-TO ADDRESS HEAD" /></label><br />
        <%= customerHistoryInvoice.shipToName%><br />
        <%= customerHistoryInvoice.shipToAddress%><br />
        <%= customerHistoryInvoice.shipToAddress2%><br />
        <%= customerHistoryInvoice.shipToPostCode%>&nbsp; <%= customerHistoryInvoice.shipToCity%>        
    </div>       
</div>
<br />&nbsp;<br />
<div class="pane">
    <div>
        <label><Infojet:Translate runat="server" ID="yourReference" code="CONTACT NAME" /></label><br />
        <%= customerHistoryInvoice.sellToContact%><br />
        <label><Infojet:Translate runat="server" ID="Translate9" code="EXTERNAL DOC NO" /></label><br />
        <%= customerHistoryInvoice.externalDocumentNo%><br />
        <label><Infojet:Translate runat="server" ID="Translate1" code="AMOUNT" /></label><br />
        <%= customerHistoryInvoice.amount%><br />
        <label><Infojet:Translate runat="server" ID="Translate3" code="AMOUNT INCLUDING VAT" /></label><br />
        <%= customerHistoryInvoice.amountIncludingVat%><br />
        <label><Infojet:Translate runat="server" ID="Translate10" code="PAYED" /></label><br />
        <%= customerHistoryInvoice.payed%><br />
   </div>
    <div>
        <label><Infojet:Translate runat="server" ID="Translate2" code="INVOICE DATE" /></label><br />
        <%= customerHistoryInvoice.documentDate%><br />
        <label><Infojet:Translate runat="server" ID="Translate6" code="ORDER DATE" /></label><br />
        <%= customerHistoryInvoice.orderDate%><br />
        <label><Infojet:Translate runat="server" ID="Translate8" code="DUE DATE" /></label><br />
        <%= customerHistoryInvoice.dueDate%><br />
    </div>       
</div>
<br />&nbsp;<br />
<asp:Repeater runat="server" ID="invoiceLineRepeater">
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
            <td class="itemListAltLine"><%#DataBinder.Eval(Container.DataItem, "no")%></td>
            <td class="itemListAltLine"><%#DataBinder.Eval(Container.DataItem, "description")%></td>
            <td class="itemListAltLine" style="text-align: right;"><%#DataBinder.Eval(Container.DataItem, "unitPrice")%></td>
            <td class="itemListAltLine" style="text-align: right;"><%#DataBinder.Eval(Container.DataItem, "quantity")%></td>
            <td class="itemListAltLine"><%#DataBinder.Eval(Container.DataItem, "unitOfMeasureCode")%></td>
            <td class="itemListAltLine"><%#DataBinder.Eval(Container.DataItem, "shipmentDate")%></td>
            <td class="itemListAltLine" style="text-align: right;"><%#DataBinder.Eval(Container.DataItem, "amount")%></td>
            <td class="itemListAltLine" style="text-align: right;"><%#DataBinder.Eval(Container.DataItem, "amountInclVat")%></td>
        </tr>    

</ItemTemplate>

<AlternatingItemTemplate>

        <tr>
            <td class="itemListLine"><%#DataBinder.Eval(Container.DataItem, "no")%></td>
            <td class="itemListLine"><%#DataBinder.Eval(Container.DataItem, "description")%></td>
            <td class="itemListLine" style="text-align: right;"><%#DataBinder.Eval(Container.DataItem, "unitPrice")%></td>
            <td class="itemListLine" style="text-align: right;"><%#DataBinder.Eval(Container.DataItem, "quantity")%></td>
            <td class="itemListLine"><%#DataBinder.Eval(Container.DataItem, "unitOfMeasureCode")%></td>
            <td class="itemListLine"><%#DataBinder.Eval(Container.DataItem, "shipmentDate")%></td>
            <td class="itemListLine" style="text-align: right;"><%#DataBinder.Eval(Container.DataItem, "amount")%></td>
            <td class="itemListLine" style="text-align: right;"><%#DataBinder.Eval(Container.DataItem, "amountInclVat")%></td>
        </tr>    

</AlternatingItemTemplate>

<FooterTemplate>
    </table>
</FooterTemplate>
</asp:Repeater>

</div>