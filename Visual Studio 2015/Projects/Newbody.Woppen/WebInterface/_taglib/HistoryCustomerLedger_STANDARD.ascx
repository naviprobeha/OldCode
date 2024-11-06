<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HistoryCustomerLedger_STANDARD.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.HistoryCustomerLedger_STANDARD" %>
<%@ Register TagPrefix="Infojet" TagName="Translate" src="Translate.ascx" %>
<br />

<asp:Repeater runat="server" ID="historyCustomerLedger">
<HeaderTemplate>
    <table class="tableView" width="100%">
    <tr>
        <th><Infojet:Translate runat="server" ID="documentType" code="DOCUMENT TYPE" /></th>
        <th><Infojet:Translate runat="server" ID="documentNo" code="DOCUMENT NO" /></th>
        <th><Infojet:Translate runat="server" ID="description" code="DESCRIPTION" /></th>
        <th style="text-align: right;"><Infojet:Translate runat="server" ID="amount" code="AMOUNT" /></th>
        <th style="text-align: right;"><Infojet:Translate runat="server" ID="remainingAmount" code="REMAINING AMOUNT" /></th>
        <th>&nbsp;</th>
    </tr>
</HeaderTemplate>

<ItemTemplate>
    <tr>
        <td><%#DataBinder.Eval(Container.DataItem, "documentTypeDescription")%></td>
        <td><%#DataBinder.Eval(Container.DataItem, "documentNo")%></td>
        <td><%#DataBinder.Eval(Container.DataItem, "description")%></td>
        <td style="text-align: right;"><%#DataBinder.Eval(Container.DataItem, "amount")%></td>
        <td style="text-align: right;"><%#DataBinder.Eval(Container.DataItem, "remainingAmount")%></td>
        <td style="text-align: center;">&nbsp;</td>
    </tr>
    
 
</ItemTemplate>

<FooterTemplate>
    </table>
</FooterTemplate>

</asp:Repeater>
