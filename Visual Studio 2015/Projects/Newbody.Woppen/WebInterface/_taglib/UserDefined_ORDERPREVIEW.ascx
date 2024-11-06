<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserDefined_ORDERPREVIEW.ascx.cs" Inherits="WebInterface._taglib.UserDefined_ORDERPREVIEW" %>
<%@ Register TagPrefix="Infojet" TagName="Translate" src="Translate.ascx" %>
<br />
<asp:Repeater runat="server" ID="cartItemRepeater">
<HeaderTemplate>
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
</HeaderTemplate>

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
<FooterTemplate>
    <tr>
        <td>&nbsp;</td>
        <td align="left"><b><Infojet:Translate runat="server" ID="total" code="TOTAL"/></b></td>
        <td align="right">&nbsp;</td>
        <td align="right">&nbsp;</td>
        <td align="right">&nbsp;</td>
        <td align="right"><b><% = totalQuantity %></b></td>
        <td align="right"><b><% = totalAmount.ToString("N2") %></b></td>
    </tr>   
    </table>
</FooterTemplate>
</asp:Repeater>
<div style="float:left; width: 100%;">
<div style="float:left; text-align: left; padding-top: 5px; width: 50%;"><% if (prevWebPageUrl != "") { %><a href="<%= prevWebPageUrl %>"><img src="_assets/img/<%= infojet.translate("IMG BACK BTN") %>" alt="<%= infojet.translate("BACK") %>" /></a><% } %></div>
<div style="float:left; text-align: right; padding-top: 5px; width: 50%; color: #ef1c22;">&nbsp;</div>
</div>