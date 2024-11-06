<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HistoryShipmentList_STANDARD.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.HistoryShipmentList_STANDARD" %>
<%@ Register TagPrefix="Infojet" TagName="Translate" src="Translate.ascx" %>
<br />

<asp:Repeater runat="server" ID="historyShipmentRepeater">
<HeaderTemplate>
    <table class="tableView" width="100%">
    <tr>
        <th class="itemListHeader"><Infojet:Translate runat="server" ID="no" code="NO_" /></th>
        <th class="itemListHeader"><Infojet:Translate runat="server" ID="orderNo" code="ORDER NO" /></th>
        <th class="itemListHeader"><Infojet:Translate runat="server" ID="yourRef" code="CONTACT NAME" /></th>
        <th class="itemListHeader"><Infojet:Translate runat="server" ID="packageTrackingNo" code="PACKAGE TRACKING NO" /></th>
        <th class="itemListHeader"><Infojet:Translate runat="server" ID="shipmentDate" code="SHIPMENT DATE" /></th>
        <th class="itemListHeader">&nbsp;</th>
    </tr>
</HeaderTemplate>

<ItemTemplate>
    <tr>
        <td class="itemListAltLine"><%#DataBinder.Eval(Container.DataItem, "no")%>&nbsp;</td>
        <td class="itemListAltLine"><%#DataBinder.Eval(Container.DataItem, "orderNo")%>&nbsp;</td>
        <td class="itemListAltLine"><%#DataBinder.Eval(Container.DataItem, "sellToContact")%>&nbsp;</td>
        <td class="itemListAltLine"><a target="_blank" href="<%#DataBinder.Eval(Container.DataItem, "shippingAgentInternetAddress")%>"><%#DataBinder.Eval(Container.DataItem, "packageTrackingNo")%></a>&nbsp;</td>
        <td class="itemListAltLine"><%#DataBinder.Eval(Container.DataItem, "shipmentDate")%>&nbsp;</td>
        <td class="itemListAltLine" style="text-align: center;"><a href="<%#DataBinder.Eval(Container.DataItem, "link")%>"><Infojet:Translate runat="server" ID="view" code="SHOW" /></a></td>
    </tr>
</ItemTemplate>

<AlternatingItemTemplate>

    <tr>
        <td class="itemListLine"><%#DataBinder.Eval(Container.DataItem, "no")%>&nbsp;</td>
        <td class="itemListLine"><%#DataBinder.Eval(Container.DataItem, "orderNo")%>&nbsp;</td>
        <td class="itemListLine"><%#DataBinder.Eval(Container.DataItem, "sellToContact")%>&nbsp;</td>
        <td class="itemListLine"><a target="_blank" href="<%#DataBinder.Eval(Container.DataItem, "shippingAgentInternetAddress")%>"><%#DataBinder.Eval(Container.DataItem, "packageTrackingNo")%></a>&nbsp;</td>
        <td class="itemListLine"><%#DataBinder.Eval(Container.DataItem, "shipmentDate")%>&nbsp;</td>
        <td class="itemListLine" style="text-align: center;"><a href="<%#DataBinder.Eval(Container.DataItem, "link")%>"><Infojet:Translate runat="server" ID="view" code="SHOW" /></a></td>
    </tr>

</AlternatingItemTemplate>


<FooterTemplate>
    </table>
</FooterTemplate>

</asp:Repeater>

<div id="pageNav" style="text-align: right; width: 100%;"><asp:Label runat="server" ID="pageNav"></asp:Label></div>