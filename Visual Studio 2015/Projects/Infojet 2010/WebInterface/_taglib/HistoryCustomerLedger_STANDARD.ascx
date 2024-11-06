<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HistoryCustomerLedger_STANDARD.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.HistoryCustomerLedger_STANDARD" %>
<%@ Register TagPrefix="Infojet" TagName="Translate" src="Translate.ascx" %>
<br />

<asp:Repeater runat="server" ID="historyCustomerLedger">
<HeaderTemplate>
    <table class="tableView" width="100%">
    <tr>
        <th class="itemListHeader"><Infojet:Translate runat="server" ID="documentType" code="DOCUMENT TYPE" /></th>
        <th class="itemListHeader"><Infojet:Translate runat="server" ID="documentNo" code="DOCUMENT NO" /></th>
        <th class="itemListHeader"><Infojet:Translate runat="server" ID="description" code="DESCRIPTION" /></th>
        <th class="itemListHeader" style="text-align: right;"><Infojet:Translate runat="server" ID="amount" code="AMOUNT" /></th>
        <th class="itemListHeader" style="text-align: right;"><Infojet:Translate runat="server" ID="remainingAmount" code="REMAINING AMOUNT" /></th>
        <th class="itemListHeader">&nbsp;</th>
    </tr>
</HeaderTemplate>

<ItemTemplate>
    <tr>
        <td class="itemListAltLine"><%#DataBinder.Eval(Container.DataItem, "documentTypeDescription")%>&nbsp;</td>
        <td class="itemListAltLine"><%#DataBinder.Eval(Container.DataItem, "documentNo")%>&nbsp;</td>
        <td class="itemListAltLine"><%#DataBinder.Eval(Container.DataItem, "description")%>&nbsp;</td>
        <td class="itemListAltLine" style="text-align: right;">&nbsp;<%#DataBinder.Eval(Container.DataItem, "amount")%></td>
        <td class="itemListAltLine" style="text-align: right;">&nbsp;<%#DataBinder.Eval(Container.DataItem, "remainingAmount")%></td>
        <td class="itemListAltLine" style="text-align: center;">&nbsp;</td>
    </tr>
    
 
</ItemTemplate>

<AlternatingItemTemplate>

    <tr>
        <td class="itemListLine"><%#DataBinder.Eval(Container.DataItem, "documentTypeDescription")%>&nbsp;</td>
        <td class="itemListLine"><%#DataBinder.Eval(Container.DataItem, "documentNo")%>&nbsp;</td>
        <td class="itemListLine"><%#DataBinder.Eval(Container.DataItem, "description")%>&nbsp;</td>
        <td class="itemListLine" style="text-align: right;">&nbsp;<%#DataBinder.Eval(Container.DataItem, "amount")%></td>
        <td class="itemListLine" style="text-align: right;">&nbsp;<%#DataBinder.Eval(Container.DataItem, "remainingAmount")%></td>
        <td class="itemListLine" style="text-align: center;">&nbsp;</td>
    </tr>

</AlternatingItemTemplate>

<FooterTemplate>
    </table>
</FooterTemplate>

</asp:Repeater>
