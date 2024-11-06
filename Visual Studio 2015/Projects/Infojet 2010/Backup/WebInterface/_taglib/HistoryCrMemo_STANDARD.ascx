<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HistoryCrMemo_STANDARD.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.HistoryCrMemo_STANDARD" %>
<%@ Register TagPrefix="Infojet" TagName="Translate" src="Translate.ascx" %>
<br />
<div class="orderHistory">

<div class="pane">
    <div>
        <label><Infojet:Translate runat="server" ID="Translate4" code="INVOICE NO" /></label><br />
        <%= customerHistoryCrMemo.no%>
   </div>
    <div>
        <label style="font-size: 9px;"><Infojet:Translate runat="server" ID="Translate7" code="CUSTOMER NO" /></label><br />
        <%= customerHistoryCrMemo.customerNo%><br />
    </div>       
</div>
<br />&nbsp;<br />
<div class="pane">
    <div>
        <label><Infojet:Translate runat="server" ID="billToAddress" code="BILL-TO ADDRESS HEAD" /></label><br />
        <%= customerHistoryCrMemo.customerName%><br />
        <%= customerHistoryCrMemo.address%><br />
        <%= customerHistoryCrMemo.address2%><br />
        <%= customerHistoryCrMemo.postCode%>&nbsp; <%= customerHistoryCrMemo.city%>        
    </div>
    <div>
        <label style="font-size: 9px;"><Infojet:Translate runat="server" ID="shipToAddress" code="SHIP-TO ADDRESS HEAD" /></label><br />
        <%= customerHistoryCrMemo.shipToName%><br />
        <%= customerHistoryCrMemo.shipToAddress%><br />
        <%= customerHistoryCrMemo.shipToAddress2%><br />
        <%= customerHistoryCrMemo.shipToPostCode%>&nbsp; <%= customerHistoryCrMemo.shipToCity%>        
    </div>       
</div>
<br />&nbsp;<br />
<div class="pane">
    <div>
        <label><Infojet:Translate runat="server" ID="yourReference" code="YOUR REFERENCE" /></label><br />
        <%= customerHistoryCrMemo.yourReference%><br />
        <label><Infojet:Translate runat="server" ID="Translate9" code="EXTERNAL DOC NO" /></label><br />
        <%= customerHistoryCrMemo.externalDocumentNo%><br />
        <label><Infojet:Translate runat="server" ID="Translate1" code="AMOUNT" /></label><br />
        <%= customerHistoryCrMemo.amount%><br />
        <label><Infojet:Translate runat="server" ID="Translate3" code="AMOUNT INCLUDING VAT" /></label><br />
        <%= customerHistoryCrMemo.amountIncludingVat%><br />
        <label><Infojet:Translate runat="server" ID="Translate10" code="PAYED" /></label><br />
        <%= customerHistoryCrMemo.payed%><br />
   </div>
    <div>
        <label><Infojet:Translate runat="server" ID="Translate2" code="INVOICE DATE" /></label><br />
        <%= customerHistoryCrMemo.documentDate%><br />
    </div>       
</div>
<br />&nbsp;<br />
<asp:Repeater runat="server" ID="crMemoLineRepeater">
<HeaderTemplate>
    <table class="tableView" width="100%">
    <tr>
        <th><Infojet:Translate runat="server" ID="no" code="NO" /></th>
        <th><Infojet:Translate runat="server" ID="description" code="DESCRIPTION" /></th>
        <th style="text-align: right;"><Infojet:Translate runat="server" ID="unitPrice" code="UNIT PRICE" /></th>
        <th style="text-align: right;"><Infojet:Translate runat="server" ID="quantity" code="QUANTITY" /></th>
        <th><Infojet:Translate runat="server" ID="shipmentDate" code="SHIPMENT DATE" /></th>
        <th style="text-align: right;"><Infojet:Translate runat="server" ID="amount" code="AMOUNT" /></th>
        <th style="text-align: right;"><Infojet:Translate runat="server" ID="amountInclVat" code="AMOUNT INCLUDING VAT" /></th>
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
            <td style="text-align: right;"><%#DataBinder.Eval(Container.DataItem, "amountInclVat")%></td>
        </tr>    

</ItemTemplate>

<FooterTemplate>
    </table>
</FooterTemplate>
</asp:Repeater>

</div>