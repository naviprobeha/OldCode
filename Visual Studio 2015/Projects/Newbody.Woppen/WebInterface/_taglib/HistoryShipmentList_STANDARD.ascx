<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HistoryShipmentList_STANDARD.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.HistoryShipmentList_STANDARD" %>
<%@ Register TagPrefix="Infojet" TagName="Translate" src="Translate.ascx" %>
<br />

<asp:Repeater runat="server" ID="historyShipmentRepeater">
<HeaderTemplate>
    <table class="tableView" width="100%">
    <tr>
        <th><Infojet:Translate runat="server" ID="no" code="NO" /></th>
        <th><Infojet:Translate runat="server" ID="orderNo" code="ORDER NO" /></th>
        <th><Infojet:Translate runat="server" ID="yourRef" code="CONTACT PERSON" /></th>
        <th style="text-align: right;"><Infojet:Translate runat="server" ID="shipmentDesc" code="SHIPMENT DESC" /></th>
        <th><Infojet:Translate runat="server" ID="packageTrackingNo" code="PACKAGE TRACKING NO" /></th>
        <th><Infojet:Translate runat="server" ID="shipmentDate" code="SHIPMENT DATE" /></th>
        <th>&nbsp;</th>
    </tr>
</HeaderTemplate>

<ItemTemplate>
    <tr>
        <td><%#DataBinder.Eval(Container.DataItem, "no")%></td>
        <td><%#DataBinder.Eval(Container.DataItem, "orderNo")%></td>
        <td><%#DataBinder.Eval(Container.DataItem, "sellToContact")%></td>
        <td style="text-align: right;"><%#DataBinder.Eval(Container.DataItem, "description")%></td>
        <td><a target="_blank" href="<%#DataBinder.Eval(Container.DataItem, "shippingAgentInternetAddress")%>"><%#DataBinder.Eval(Container.DataItem, "packageTrackingNo")%></a></td>
        <td><%#DataBinder.Eval(Container.DataItem, "shipmentDate")%></td>
        <td style="text-align: center;"><a href="<%#DataBinder.Eval(Container.DataItem, "link")%>"><Infojet:Translate runat="server" ID="view" code="SHOW" /></a></td>
    </tr>
</ItemTemplate>

<AlternatingItemTemplate>

    <tr>
        <td style="background-color: #f5f5f5;"><%#DataBinder.Eval(Container.DataItem, "no")%></td>
        <td style="background-color: #f5f5f5;"><%#DataBinder.Eval(Container.DataItem, "orderNo")%></td>
        <td style="background-color: #f5f5f5;"><%#DataBinder.Eval(Container.DataItem, "sellToContact")%></td>
        <td style="background-color: #f5f5f5; text-align: right;"><%#DataBinder.Eval(Container.DataItem, "description")%></td>
        <td style="background-color: #f5f5f5;"><a target="_blank" href="<%#DataBinder.Eval(Container.DataItem, "shippingAgentInternetAddress")%>"><%#DataBinder.Eval(Container.DataItem, "packageTrackingNo")%></a></td>
        <td style="background-color: #f5f5f5;"><%#DataBinder.Eval(Container.DataItem, "shipmentDate")%></td>
        <td style="background-color: #f5f5f5; text-align: center;"><a href="<%#DataBinder.Eval(Container.DataItem, "link")%>"><Infojet:Translate runat="server" ID="view" code="SHOW" /></a></td>
    </tr>

</AlternatingItemTemplate>


<FooterTemplate>
    </table>
</FooterTemplate>

</asp:Repeater>

<div style="text-align: right; width: 100%;"><asp:Label runat="server" ID="pageNav"></asp:Label></div>