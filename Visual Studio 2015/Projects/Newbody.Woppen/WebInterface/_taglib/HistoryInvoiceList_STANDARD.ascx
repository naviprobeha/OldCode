<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HistoryInvoiceList_STANDARD.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.HistoryInvoiceList_STANDARD" %>
<%@ Register TagPrefix="Infojet" TagName="Translate" src="Translate.ascx" %>
<br />

<asp:Repeater runat="server" ID="historyInvoiceRepeater">
<HeaderTemplate>
    <table class="tableView" width="100%">
    <tr>
        <th><Infojet:Translate runat="server" ID="no" code="NO" /></th>
        <th><Infojet:Translate runat="server" ID="orderNo" code="ORDER NO" /></th>
        <th><Infojet:Translate runat="server" ID="yourRef" code="CONTACT PERSON" /></th>
        <th><Infojet:Translate runat="server" ID="dueDate" code="DUE DATE" /></th>
        <th style="text-align: right;"><Infojet:Translate runat="server" ID="invoiceDesc" code="INVOICE DESC" /></th>
        <th style="text-align: right;"><Infojet:Translate runat="server" ID="amountInclVat" code="AMOUNT INCLUDING VAT" /></th>
        <th style="text-align: center;"><Infojet:Translate runat="server" ID="payed" code="PAYED" /></th>
        <th>&nbsp;</th>
        <th>&nbsp;</th>
    </tr>
</HeaderTemplate>

<ItemTemplate>
    <tr>
        <td><%#DataBinder.Eval(Container.DataItem, "no")%></td>
        <td><%#DataBinder.Eval(Container.DataItem, "orderNo")%></td>
        <td><%#DataBinder.Eval(Container.DataItem, "sellToContact")%></td>
        <td><%#DataBinder.Eval(Container.DataItem, "dueDate")%></td>
        <td style="text-align: right;"><%#DataBinder.Eval(Container.DataItem, "description")%></td>
        <td style="text-align: right;"><%#DataBinder.Eval(Container.DataItem, "amountIncludingVat")%></td>
        <td style="text-align: center;"><%#DataBinder.Eval(Container.DataItem, "payed")%></td>
        <td style="text-align: center;"><a target="_blank" href="<%#DataBinder.Eval(Container.DataItem, "pdfLink")%>"><Infojet:Translate runat="server" ID="Translate1" code="PDF" /></a></td>
        <td style="text-align: center;"><a href="<%#DataBinder.Eval(Container.DataItem, "link")%>"><Infojet:Translate runat="server" ID="view" code="SHOW" /></a></td>
    </tr>
    
    <asp:repeater id="shipments" datasource='<%#DataBinder.Eval(Container.DataItem, "shipments")%>' runat="server"> 

    <ItemTemplate>
        <tr>
            <td><img src="_assets/img/sub.gif" border="0">&nbsp;<%#DataBinder.Eval(Container.DataItem, "no")%></td>
            <td>&nbsp;</td>
            <td>Utleverans</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td style="text-align: center;"><a href="<%#DataBinder.Eval(Container.DataItem, "link")%>"><Infojet:Translate runat="server" ID="amountInclVat" code="SHOW" /></a></td>
        </tr>    
    </ItemTemplate>    
    
    </asp:repeater>    
</ItemTemplate>

<AlternatingItemTemplate>

    <tr>
        <td style="background-color: #f5f5f5;"><%#DataBinder.Eval(Container.DataItem, "no")%></td>
        <td style="background-color: #f5f5f5;"><%#DataBinder.Eval(Container.DataItem, "orderNo")%></td>
        <td style="background-color: #f5f5f5;"><%#DataBinder.Eval(Container.DataItem, "yourReference")%></td>
        <td style="background-color: #f5f5f5;"><%#DataBinder.Eval(Container.DataItem, "dueDate")%></td>
        <td style="background-color: #f5f5f5; text-align: right;"><%#DataBinder.Eval(Container.DataItem, "description")%></td>
        <td style="background-color: #f5f5f5; text-align: right;"><%#DataBinder.Eval(Container.DataItem, "amountIncludingVat")%></td>
        <td style="background-color: #f5f5f5; text-align: center;"><%#DataBinder.Eval(Container.DataItem, "payed")%></td>
        <td style="background-color: #f5f5f5; text-align: center;"><a target="_blank" href="<%#DataBinder.Eval(Container.DataItem, "pdfLink")%>"><Infojet:Translate runat="server" ID="Translate1" code="PDF" /></a></td>
        <td style="background-color: #f5f5f5; text-align: center;"><a href="<%#DataBinder.Eval(Container.DataItem, "link")%>"><Infojet:Translate runat="server" ID="view" code="SHOW" /></a></td>
    </tr>
    
    <asp:repeater id="shipments" datasource='<%#DataBinder.Eval(Container.DataItem, "shipments")%>' runat="server"> 

    <ItemTemplate>
        <tr>
            <td style="background-color: #f5f5f5;"><img src="_assets/img/sub.gif" border="0">&nbsp;<%#DataBinder.Eval(Container.DataItem, "no")%></td>
            <td style="background-color: #f5f5f5;">&nbsp;</td>
            <td style="background-color: #f5f5f5;">Utleverans</td>
            <td style="background-color: #f5f5f5;">&nbsp;</td>
            <td style="background-color: #f5f5f5;">&nbsp;</td>
            <td style="background-color: #f5f5f5;">&nbsp;</td>
            <td style="background-color: #f5f5f5;">&nbsp;</td>
            <td style="background-color: #f5f5f5;">&nbsp;</td>
            <td style="background-color: #f5f5f5; text-align: center;"><a href="<%#DataBinder.Eval(Container.DataItem, "link")%>"><Infojet:Translate runat="server" ID="amountInclVat" code="SHOW" /></a></td>
        </tr>    
    </ItemTemplate>    
    
    </asp:repeater>    

</AlternatingItemTemplate>

<FooterTemplate>
    </table>
</FooterTemplate>

</asp:Repeater>

<div style="text-align: right; width: 100%;"><asp:Label runat="server" ID="pageNav"></asp:Label></div>