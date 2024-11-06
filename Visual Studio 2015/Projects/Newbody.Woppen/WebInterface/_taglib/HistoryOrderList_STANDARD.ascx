<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HistoryOrderList_STANDARD.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.HistoryOrderList_STANDARD" %>
<%@ Register TagPrefix="Infojet" TagName="Translate" src="Translate.ascx" %>
<br />
<asp:Repeater runat="server" ID="historyOrderRepeater">
<HeaderTemplate>
    <table class="tableView" width="100%">
    <tr>
        <th><Infojet:Translate runat="server" ID="no" code="NO" /></th>
        <th><Infojet:Translate runat="server" ID="date" code="DATE" /></th>
        <th><Infojet:Translate runat="server" ID="yourRef" code="CONTACT PERSON" /></th>
        <th style="text-align: right;"><Infojet:Translate runat="server" ID="orderDesc" code="ORDER DESC" /></th>
        <th style="text-align: right;"><Infojet:Translate runat="server" ID="amountInclVat" code="AMOUNT" /></th>
        <th>&nbsp;</th>
    </tr>
</HeaderTemplate>

<ItemTemplate>
    <tr>
        <td><%#DataBinder.Eval(Container.DataItem, "no")%></td>
        <td><%#DataBinder.Eval(Container.DataItem, "orderDate")%></td>
        <td><%#DataBinder.Eval(Container.DataItem, "sellToContact")%></td>
        <td style="text-align: right;"><%#DataBinder.Eval(Container.DataItem, "description")%></td>
        <td style="text-align: right;"><%#DataBinder.Eval(Container.DataItem, "amount")%></td>
        <td style="text-align: center;"><a href="<%#DataBinder.Eval(Container.DataItem, "link")%>"><Infojet:Translate runat="server" ID="amountInclVat" code="SHOW" /></a></td>
    </tr>
    
    
    <asp:repeater id="shipments" datasource='<%#DataBinder.Eval(Container.DataItem, "shipments")%>' runat="server"> 

    <ItemTemplate>
        <tr>
            <td><img src="_assets/img/sub.gif" border="0">&nbsp;<%#DataBinder.Eval(Container.DataItem, "no")%></td>
            <td><%#DataBinder.Eval(Container.DataItem, "shipmentDate")%></td>
            <td>Utleverans</td>
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
        <td style="background-color: #f5f5f5;"><%#DataBinder.Eval(Container.DataItem, "orderDate")%></td>
        <td style="background-color: #f5f5f5;"><%#DataBinder.Eval(Container.DataItem, "sellToContact")%></td>
        <td style="background-color: #f5f5f5; text-align: right;"><%#DataBinder.Eval(Container.DataItem, "description")%></td>
        <td style="background-color: #f5f5f5; text-align: right;"><%#DataBinder.Eval(Container.DataItem, "amount")%></td>
        <td style="background-color: #f5f5f5; text-align: center;"><a href="<%#DataBinder.Eval(Container.DataItem, "link")%>"><Infojet:Translate runat="server" ID="amountInclVat" code="SHOW" /></a></td>
    </tr>
    
    
    <asp:repeater id="shipments" datasource='<%#DataBinder.Eval(Container.DataItem, "shipments")%>' runat="server"> 

    <ItemTemplate>
        <tr>
            <td style="background-color: #f5f5f5;"><img src="_assets/img/sub.gif" border="0">&nbsp;<%#DataBinder.Eval(Container.DataItem, "no")%></td>
            <td style="background-color: #f5f5f5;"><%#DataBinder.Eval(Container.DataItem, "shipmentDate")%></td>
            <td style="background-color: #f5f5f5;">Utleverans</td>
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