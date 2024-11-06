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
        <label><Infojet:Translate runat="server" ID="yourReference" code="CONTACT PERSON" /></label><br />
        <%= customerHistoryInvoice.sellToContact%><br />
        <label><Infojet:Translate runat="server" ID="Translate9" code="EXTERNAL DOC NO" /></label><br />
        <%= customerHistoryInvoice.externalDocumentNo%><br />
        <label><Infojet:Translate runat="server" ID="Translate3" code="AMOUNT INCLUDING VAT" /></label><br />
        <%= customerHistoryInvoice.amountIncludingVat%><br />
        <label><Infojet:Translate runat="server" ID="Translate10" code="PAYED" /></label><br />
        <%= customerHistoryInvoice.payed%><br />
   </div>
    <div style="width: 450px">
        <label><Infojet:Translate runat="server" ID="Translate2" code="INVOICE DATE" /></label><br />
        <%= customerHistoryInvoice.documentDate%><br />
        <label><Infojet:Translate runat="server" ID="Translate6" code="ORDER DATE" /></label><br />
        <%= customerHistoryInvoice.orderDate%><br />
        <label><Infojet:Translate runat="server" ID="Translate8" code="DUE DATE" /></label><br />
        <%= customerHistoryInvoice.dueDate%><br />
        <label><Infojet:Translate runat="server" ID="documents" code="PDF" /></label><br />
        <a href="<%= orderConfirmationPdfUrl %>" target="_blank"><Infojet:Translate runat="server" ID="Translate11" code="GET ORDER CONF" /></a> | <a href="<%= pickListPdfUrl %>" target="_blank"><Infojet:Translate runat="server" ID="Translate12" code="GET PICK LIST" /></a> | <a href="<%= customerHistoryInvoice.pdfLink %>" target="_blank"><Infojet:Translate runat="server" ID="Translate13" code="GET INVOICE PDF" /></a><br />                
    </div>       
</div>
<br />&nbsp;<br />
<asp:Repeater runat="server" ID="invoiceLineRepeater">
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
            <td style="text-align: right;"><%#DataBinder.Eval(Container.DataItem, "amountInclVat")%></td>
        </tr>    

</ItemTemplate>

<FooterTemplate>
    </table>
</FooterTemplate>
</asp:Repeater>

</div>