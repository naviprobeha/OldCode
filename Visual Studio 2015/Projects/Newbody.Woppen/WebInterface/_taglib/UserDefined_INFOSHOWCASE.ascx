<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserDefined_INFOSHOWCASE.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.UserDefined_INFOSHOWCASE" %>
<%@ Register TagPrefix="Infojet" TagName="Translate" src="Translate.ascx" %>
<br />
<asp:Label runat="server" ID="errorMessageLabel" ForeColor="Red"></asp:Label>
<br />


<table class="tableView" style="width: 100%">
    <tr>
            <th><Infojet:Translate runat="server" ID="itemNo" code="ITEM NO"/></th>
            <th><Infojet:Translate runat="server" ID="description" code="DESCRIPTION"/></th>
            <th style="text-align: right;"><Infojet:Translate runat="server" ID="quantity" code="QTY"/></th>
            <th style="text-align: right;"><Infojet:Translate runat="server" ID="quantityShowCase" code="SOLD QUANTITY"/></th>
            <th style="text-align: right;"><Infojet:Translate runat="server" ID="quantityToOrder" code="REMAINING QUANTITY"/></th>
    </tr>

<asp:Repeater runat="server" ID="salesIdRepeater">

<ItemTemplate>
    <tr>
        <td>&nbsp;</td>
        <td align="left"><b><%#DataBinder.Eval(Container.DataItem, "description")%></b></td>
        <td align="right">&nbsp;</td>
        <td align="right">&nbsp;</td>
        <td align="right">&nbsp;</td>
    </tr>




    <asp:Repeater runat="server" ID="cartItemRepeater" datasource='<%#DataBinder.Eval(Container.DataItem, "notSoldHistoryShowCaseItems")%>'>
    <ItemTemplate>

	    <tr>
	         <td><%#DataBinder.Eval(Container.DataItem, "itemNo")%></td>
        	 <td align="left"><%#DataBinder.Eval(Container.DataItem, "description")%></td>
	         <td align="right"><%#DataBinder.Eval(Container.DataItem, "quantity")%></td>
  		     <td align="right"><%#DataBinder.Eval(Container.DataItem, "formatedSoldQuantity")%></td>
       		 <td align="right"><%#DataBinder.Eval(Container.DataItem, "remainingQuantity")%></td>
	    </tr>

    </ItemTemplate>
    </asp:Repeater>
    
</ItemTemplate>

</asp:Repeater>

</table>

   
