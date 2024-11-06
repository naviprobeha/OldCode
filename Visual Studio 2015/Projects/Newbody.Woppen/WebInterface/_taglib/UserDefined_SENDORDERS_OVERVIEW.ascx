<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserDefined_SENDORDERS_OVERVIEW.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.UserDefined_SENDORDERS_OVERVIEW" %>
<%@ Register TagPrefix="Infojet" TagName="Translate" src="Translate.ascx" %>
<br />
<asp:Label runat="server" ID="errorMessageLabel" ForeColor="Red"></asp:Label>
<br />
<script type="text/javascript">

function confirmOrder()
{
    <% if (grandTotalQuantity > 0) { %>
        if (confirm("<Infojet:Translate runat="server" ID="confirm" code="CONFIRM ORDER"/>"))
        {
            document.location.href="<%= sendUrl %>";
        }
    <% } else { %>
        alert("<Infojet:Translate runat="server" ID="zeroOrder" code="ZERO ORDER"/>");
    <% } %>
}

</script>

<div class="orderHistory">

<div class="pane">
    <div>
        <label style="font-size: 9px;"><Infojet:Translate runat="server" ID="shipToAddress" code="SHIP-TO ADDRESS HEAD" /></label><br />
        <%= webCartHeader.shipToName%><br />
        <%= webCartHeader.shipToAddress%><br />
        <%= webCartHeader.shipToAddress2%><br />
        <%= webCartHeader.shipToPostCode%>&nbsp; <%= webCartHeader.shipToCity%>        
    </div>       
    <div>
        <label><Infojet:Translate runat="server" ID="shipmentMethod" code="SHIPMENT METHOD" /></label><br />
        <%= webCartHeader.shipmentMethodCode%><br />
        <label><Infojet:Translate runat="server" ID="ahippingAgent" code="SHIPPING AGENT" /></label><br />
        <%= webCartHeader.shippingAgentCode%><br />
        <label><Infojet:Translate runat="server" ID="ahippingAgentServ" code="SHIPPING AGENT SERV" /></label><br />
        <%= webCartHeader.shippingAgentServiceDescription%><br />        
   </div>      
    <div>
        <label><Infojet:Translate runat="server" ID="Translate1" code="TOTAL PACKAGES" /></label><br />
        <%= grandTotalQuantity %><br />
        <label><Infojet:Translate runat="server" ID="Translate2" code="TOTAL AMOUNT" /></label><br />
        <%= grandTotalAmount.ToString("N2") %><br />
        <label><Infojet:Translate runat="server" ID="Translate3" code="SHIPPER PHONE NO" /></label><br />
        <%= infojet.translate("SHIPPER PHONE VAL") %><br />
   </div>      
</div>

</div>

<br />&nbsp;<br />



<table class="tableView" style="width: 100%">
    <tr>
        <th><Infojet:Translate runat="server" ID="itemNo" code="ITEM NO"/></th>
        <th><Infojet:Translate runat="server" ID="description" code="DESCRIPTION"/></th>
        <th style="text-align: right;"><Infojet:Translate runat="server" ID="unitPrice" code="UNIT PRICE"/></th>            
        <th style="text-align: right;"><Infojet:Translate runat="server" ID="quantity" code="QUANTITY"/></th>
        <th style="text-align: right;"><Infojet:Translate runat="server" ID="quantityShowCase" code="QUANTITY SHOWCASE"/></th>
        <th style="text-align: right;"><Infojet:Translate runat="server" ID="quantityToOrder" code="QUANTITY TO ORDER"/></th>
        <th style="text-align: right;"><Infojet:Translate runat="server" ID="amount" code="AMOUNT"/></th>
    </tr>

<asp:Repeater runat="server" ID="salesIdRepeater">

<ItemTemplate>
    <tr>
        <td>&nbsp;</td>
        <td align="left"><b><%#DataBinder.Eval(Container.DataItem, "description")%></b></td>
        <td align="right">&nbsp;</td>
        <td align="right">&nbsp;</td>
        <td align="right">&nbsp;</td>
        <td align="right">&nbsp;</td>
        <td align="right">&nbsp;</td>
    </tr>


    <asp:Repeater runat="server" ID="cartItemRepeater" datasource='<%#DataBinder.Eval(Container.DataItem, "cartLines")%>'>
    <ItemTemplate>
        <tr>
            <td><%#DataBinder.Eval(Container.DataItem, "itemNo")%></td>
            <td align="left"><%#DataBinder.Eval(Container.DataItem, "description")%></td>
            <td align="right"><%#DataBinder.Eval(Container.DataItem, "unitPrice", "{0:n2}")%></td>
            <td align="right"><%#DataBinder.Eval(Container.DataItem, "quantity")%></td>
            <td align="right"><%#DataBinder.Eval(Container.DataItem, "formatedQuantityShowCase")%></td>
            <td align="right"><%#DataBinder.Eval(Container.DataItem, "formatedQuantityToOrder")%></td>
            <td align="right"><%#DataBinder.Eval(Container.DataItem, "amount", "{0:n2}")%></td>
        </tr>
    </ItemTemplate>
    </asp:Repeater>
    
</ItemTemplate>

</asp:Repeater>
<tr>
    <td>&nbsp;</td>
    <td align="left"><Infojet:Translate runat="server" ID="Translate4" code="FREIGHT"/></td>
    <td align="right">&nbsp;</td>
    <td align="right">&nbsp;</td>
    <td align="right">&nbsp;</td>
    <td align="right">&nbsp;</td>
    <td align="right"><%= webCartHeader.freightFee.ToString("N2") %></td>
</tr>

<tr>
    <td>&nbsp;</td>
    <td align="left"><b><Infojet:Translate runat="server" ID="total" code="TOTAL"/></b></td>
    <td align="right">&nbsp;</td>
    <td align="right">&nbsp;</td>
    <td align="right">&nbsp;</td>
    <td align="right"><b><%= grandTotalQuantity %></b></td>
    <td align="right"><b><%= grandTotalAmount.ToString("N2") %></b></td>
</tr>   
</table>

   
<div style="float:left; width: 100%;">
<div style="float:left; text-align: left; padding-top: 5px; width: 50%;"><% if (prevWebPageUrl != "") { %><a href="<%= prevWebPageUrl %>"><img src="_assets/img/<%= infojet.translate("IMG BACK BTN") %>" alt="<%= infojet.translate("BACK") %>" /></a><% } %></div>
<div style="float:left; text-align: right; padding-top: 5px; width: 50%; color: #ef1c22;"><a href="javascript:confirmOrder()"><img src="_assets/img/<%= infojet.translate("IMG ORDER SEND BTN") %>" alt="<%= infojet.translate("SEND ORDER") %>" /></a></div>
</div>
