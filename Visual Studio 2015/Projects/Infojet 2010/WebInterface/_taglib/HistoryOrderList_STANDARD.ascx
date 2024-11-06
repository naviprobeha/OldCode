<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HistoryOrderList_STANDARD.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.HistoryOrderList_STANDARD" %>
<%@ Register TagPrefix="Infojet" TagName="Translate" src="Translate.ascx" %>
<br />
<asp:Repeater runat="server" ID="historyOrderRepeater">
<HeaderTemplate>
    <table class="tableView" width="100%">
    <tr>
        <th class="itemListHeader"><Infojet:Translate runat="server" ID="no" code="NO_" /></th>
        <th class="itemListHeader"><Infojet:Translate runat="server" ID="date" code="DATE" /></th>
        <th class="itemListHeader"><Infojet:Translate runat="server" ID="yourRef" code="CONTACT NAME" /></th>
        <th class="itemListHeader" style="text-align: right;"><Infojet:Translate runat="server" ID="amountInclVat" code="AMOUNT INCLUDING VAT" /></th>
        <th class="itemListHeader">&nbsp;</th>
    </tr>
</HeaderTemplate>

<ItemTemplate>
    <tr>
        <td class="itemListAltLine"><%#DataBinder.Eval(Container.DataItem, "no")%>&nbsp;</td>
        <td class="itemListAltLine"><%#DataBinder.Eval(Container.DataItem, "orderDate")%>&nbsp;</td>
        <td class="itemListAltLine"><%#DataBinder.Eval(Container.DataItem, "sellToContact")%>&nbsp;</td>
        <td class="itemListAltLine" style="text-align: right;"><%#DataBinder.Eval(Container.DataItem, "amountIncludingVat")%></td>
        <td class="itemListAltLine" style="text-align: center;"><a href="<%#DataBinder.Eval(Container.DataItem, "link")%>"><Infojet:Translate runat="server" ID="amountInclVat" code="SHOW" /></a></td>
    </tr>
    
    
    <asp:repeater id="shipments" datasource='<%#DataBinder.Eval(Container.DataItem, "shipments")%>' runat="server"> 

    <ItemTemplate>
        <tr>
            <td class="itemListAltLine"><img src="_assets/img/sub.gif" border="0">&nbsp;<%#DataBinder.Eval(Container.DataItem, "no")%>&nbsp;</td>
            <td class="itemListAltLine"><%#DataBinder.Eval(Container.DataItem, "shipmentDate")%>&nbsp;</td>
            <td class="itemListAltLine"><Infojet:Translate runat="server" ID="shipment" code="SHIPMENT" /></td>
            <td class="itemListAltLine">&nbsp;</td>
            <td class="itemListAltLine" style="text-align: center;"><a href="<%#DataBinder.Eval(Container.DataItem, "link")%>"><Infojet:Translate runat="server" ID="amountInclVat" code="SHOW" /></a></td>
        </tr>    
    </ItemTemplate>    
    
    </asp:repeater>

    
</ItemTemplate>

<AlternatingItemTemplate>

    <tr>
        <td class="itemListLine"><%#DataBinder.Eval(Container.DataItem, "no")%>&nbsp;</td>
        <td class="itemListLine"><%#DataBinder.Eval(Container.DataItem, "orderDate")%>&nbsp;</td>
        <td class="itemListLine"><%#DataBinder.Eval(Container.DataItem, "sellToContact")%>&nbsp;</td>
        <td class="itemListLine" style="text-align: right;"><%#DataBinder.Eval(Container.DataItem, "amountIncludingVat")%></td>
        <td class="itemListLine" style="text-align: center;"><a href="<%#DataBinder.Eval(Container.DataItem, "link")%>"><Infojet:Translate runat="server" ID="amountInclVat" code="SHOW" /></a></td>
    </tr>
    
    
    <asp:repeater id="shipments" datasource='<%#DataBinder.Eval(Container.DataItem, "shipments")%>' runat="server"> 

    <ItemTemplate>
        <tr>
            <td class="itemListLine"><img src="_assets/img/sub.gif" border="0">&nbsp;<%#DataBinder.Eval(Container.DataItem, "no")%>&nbsp;</td>
            <td class="itemListLine"><%#DataBinder.Eval(Container.DataItem, "shipmentDate")%>&nbsp;</td>
            <td class="itemListLine"><Infojet:Translate runat="server" ID="shipment" code="SHIPMENT" /></td>
            <td class="itemListLine">&nbsp;</td>
            <td class="itemListLine" style="text-align: center;"><a href="<%#DataBinder.Eval(Container.DataItem, "link")%>"><Infojet:Translate runat="server" ID="amountInclVat" code="SHOW" /></a></td>
        </tr>    
    </ItemTemplate>    
    
    </asp:repeater>

</AlternatingItemTemplate>


<FooterTemplate>
    </table>
</FooterTemplate>

</asp:Repeater>

<div id="pageNav" style="text-align: right; width: 100%;"><asp:Label runat="server" ID="pageNav"></asp:Label></div>