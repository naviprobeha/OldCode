<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserDefined_INFODETAILS.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.UserDefined_INFODETAILS" %>
<%@ Register TagPrefix="Infojet" TagName="Translate" src="Translate.ascx" %>
<br />
<asp:Label runat="server" ID="errorMessageLabel" ForeColor="Red"></asp:Label>
<br />


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


    <asp:Repeater runat="server" ID="cartItemRepeater" datasource='<%#DataBinder.Eval(Container.DataItem, "sentLines")%>'>
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
    <td align="left"><b><Infojet:Translate runat="server" ID="total" code="TOTAL"/></b></td>
    <td align="right">&nbsp;</td>
    <td align="right">&nbsp;</td>
    <td align="right">&nbsp;</td>
    <td align="right"><b><%= grandTotalQuantity %></b></td>
    <td align="right"><b><%= grandTotalAmount.ToString("N2") %></b></td>
</tr>  
<tr>
    <td>&nbsp;</td>
    <td align="left"><b><Infojet:Translate runat="server" ID="profit" code="PROFIT"/></b></td>
    <td align="right">&nbsp;</td>
    <td align="right">&nbsp;</td>
    <td align="right">&nbsp;</td>
    <td align="right">&nbsp;</td>
    <td align="right"><b><%= totalProfit.ToString("N2") %></b></td>
</tr>   
</table>

   
