<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Checkout_2_STANDARD.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.Checkout_2_STANDARD" %>
<%@ Register TagPrefix="Infojet" TagName="Translate" src="Translate.ascx" %>

<script type="text/javascript">

var lastEntryNo = 0;

function changeQuantity(entryNo)
{
    document.getElementById('cartRef_'+entryNo).style.visibility = "hidden";
    document.getElementById('changeCartRef_'+entryNo).style.visibility = "visible";
    document.getElementById('cartQuantity_'+entryNo).style.visibility = "hidden";
    document.getElementById('changeCartQuantity_'+entryNo).style.visibility = "visible";
    document.getElementById('changeButton_'+entryNo).style.visibility = "visible";
    document.getElementById('lineButtons_'+entryNo).style.visibility = "hidden";
    
    if (lastEntryNo > 0)
    {
        document.getElementById('cartRef_'+lastEntryNo).style.visibility = "visible";
        document.getElementById('changeCartRef_'+lastEntryNo).style.visibility = "hidden";
        document.getElementById('cartQuantity_'+lastEntryNo).style.visibility = "visible";
        document.getElementById('changeCartQuantity_'+lastEntryNo).style.visibility = "hidden";
        document.getElementById('changeButton_'+lastEntryNo).style.visibility = "hidden";
        document.getElementById('lineButtons_'+lastEntryNo).style.visibility = "visible";
    }
    
    lastEntryNo = entryNo;
}

</script>


<div class="orderHistory">

<div class="pane">
    <div>
        <label style="font-size: 9px;"><Infojet:Translate runat="server" ID="shipToAddress" code="SHIP-TO ADDRESS HEAD" /></label><br />
        <%= webCartHeader.shipToName%><br />
        <%= webCartHeader.shipToAddress%><br />
        <%= webCartHeader.shipToAddress2%><br />
        <%= webCartHeader.shipToPostCode%>&nbsp; <%= webCartHeader.shipToCity%><br />      
        <%= webCartHeader.shipToCountryCode%><br />
   </div>       
    <div>
        <label style="font-size: 9px;"><Infojet:Translate runat="server" ID="Translate1" code="BILL-TO ADDRESS HEAD" /></label><br />
        <%= webCartHeader.billToName%><br />
        <%= webCartHeader.billToAddress%><br />
        <%= webCartHeader.billToAddress2%><br />
        <%= webCartHeader.billToPostCode%>&nbsp; <%= webCartHeader.shipToCity%> <br />       
        <%= webCartHeader.billToCountryCode%><br />
    </div>       
   
</div>
<br />&nbsp;<br />
<div class="pane"> 
    <div>
        <label><Infojet:Translate runat="server" ID="Translate2" code="CONTACT NAME" /></label><br />
        <%= webCartHeader.contactName%><br />
        <label><Infojet:Translate runat="server" ID="Translate3" code="PHONE NO" /></label><br />
        <%= webCartHeader.phoneNo%><br />
        <label><Infojet:Translate runat="server" ID="Translate5" code="EMAIL" /></label><br />
        <%= webCartHeader.email%><br />
   </div>      
    <div>
        <label><Infojet:Translate runat="server" ID="Translate7" code="CUSTOMER ORDER NO" /></label><br />
        <%= webCartHeader.customerOrderNo%><br />
        <label><Infojet:Translate runat="server" ID="Translate6" code="NOTE OF GOODS" /></label><br />
        <%= webCartHeader.noteOfGoods%><br />
        <label><Infojet:Translate runat="server" ID="Translate15" code="SHIPMENT DATE" /></label><br />
        <%= webCartHeader.shipmentDate.ToString("yyyy-MM-dd") %><br />
   </div>      
</div>

<br />&nbsp;<br />
<div class="pane"> 
    <div>
        <label><Infojet:Translate runat="server" ID="Translate4" code="SHIPMENT METHOD" /></label><br />
        <asp:DropDownList ID="shipmentMethodList" runat="server" CssClass="DropDown" AutoPostBack="true" DataTextField="description" DataValueField="code" OnSelectedIndexChanged="shipmentMethodList_SelectedIndexChanged"></asp:DropDownList><br />
        <asp:Label ID="shipmentMethodText" runat="server" Font-Size="11px"></asp:Label><br />
    </div>      
    <div>
        <label><Infojet:Translate runat="server" ID="Translate11" code="PAYMENT METHOD" /></label><br />
        <asp:DropDownList ID="paymentMethodList" runat="server" CssClass="DropDown" AutoPostBack="true" DataTextField="description" DataValueField="code" OnSelectedIndexChanged="paymentMethodList_SelectedIndexChanged"></asp:DropDownList><br />
        <asp:Label ID="paymentMethodText" runat="server" Font-Size="11px"></asp:Label><br />
    </div>
</div>

<asp:panel id="discountPanel" runat="server"> 
<br />&nbsp;<br />
<div class="pane">
    <%
    	Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();
    	applyDiscountButton.Text = infojet.translate("APPLY DISCOUNT");
    %>
    <div >
        <label><Infojet:Translate runat="server" ID="discountCode" code="DISCOUNT CODE" /></label><br />
        <asp:TextBox ID="discountCodeBox" runat="server" CssClass="Textfield"></asp:TextBox>&nbsp;<asp:Button ID="applyDiscountButton" runat="server" CssClass="Button"/>
    </div>
</div>
</asp:panel>

</div>
<br/>&nbsp;<br/>
<asp:Label ID="errorMessage" runat="server" ForeColor="Red" Visible="False"></asp:Label>
<br />&nbsp;<br />

<table class="tableView" style="width: 100%">
    <tr>
        <th class="itemListHeader"><Infojet:Translate runat="server" ID="itemNo" code="ITEM NO"/></th>
        <th class="itemListHeader"><Infojet:Translate runat="server" ID="description" code="DESCRIPTION"/></th>
        <th class="itemListHeader"><Infojet:Translate runat="server" ID="Translate14" code="REFERENCE"/></th>
        <th class="itemListHeader" style="text-align: right;"><Infojet:Translate runat="server" ID="unitPrice" code="UNIT PRICE"/></th>            
        <th class="itemListHeader" style="text-align: right;"><Infojet:Translate runat="server" ID="quantity" code="QUANTITY"/></th>
        <th class="itemListHeader" style="text-align: right;"><Infojet:Translate runat="server" ID="amount" code="AMOUNT"/></th>
        <th class="itemListHeader" style="text-align: right;">&nbsp;</th>
    </tr>


    <asp:Repeater runat="server" ID="cartItemRepeater" OnItemCommand="updateCart_ItemCommand">
    <ItemTemplate>
        <tr>
            <td class="itemListAltLine" valign="top"><%#DataBinder.Eval(Container.DataItem, "itemNo")%></td>
            <td class="itemListAltLine" valign="top" align="left"><%#DataBinder.Eval(Container.DataItem, "description")%></td>
            <td class="itemListAltLine" valign="top" align="right"><div id="cartRef_<%#DataBinder.Eval(Container.DataItem, "lineNo")%>" style="float: left; text-align: right;"><%#DataBinder.Eval(Container.DataItem, "referenceNo")%></div><div id="changeCartRef_<%#DataBinder.Eval(Container.DataItem, "lineNo")%>" style="visibility: hidden; float: left;"><asp:textBox Id="referenceBox" Runat="server" Width="100px" Text='<%#DataBinder.Eval(Container.DataItem, "referenceNo")%>' CssClass="Textfield" /></div></td>
            <td class="itemListAltLine" valign="top" align="right"><%#DataBinder.Eval(Container.DataItem, "unitPrice", "{0:n2}")%></td>
            <td class="itemListAltLine" valign="top" align="right"><div id="cartQuantity_<%#DataBinder.Eval(Container.DataItem, "lineNo")%>" style="float: right; text-align: right;"><%#DataBinder.Eval(Container.DataItem, "quantity")%></div><div id="changeCartQuantity_<%#DataBinder.Eval(Container.DataItem, "lineNo")%>" style="visibility: hidden; float: right;"><asp:textBox Id="quantityBox" Runat="server" Width="40px" Text='<%#DataBinder.Eval(Container.DataItem, "quantity")%>' CssClass="Textfield" /></div></td>
            <td class="itemListAltLine" valign="top" align="right"><%#DataBinder.Eval(Container.DataItem, "formatedAmount")%></td>
            <td class="itemListAltLine" valign="top" align="center"><div id="lineButtons_<%#DataBinder.Eval(Container.DataItem, "lineNo")%>"><a href="javascript:changeQuantity(<%#DataBinder.Eval(Container.DataItem, "lineNo")%>)"><Infojet:Translate runat="server" ID="Translate10" code="CHANGE" /></a> | <asp:LinkButton ID="removeLink" runat="server" CommandName="remove" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "lineNo")%>'><Infojet:Translate runat="server" ID="Translate3" code="REMOVE" /></asp:LinkButton></div><div id="changeButton_<%#DataBinder.Eval(Container.DataItem, "lineNo")%>" style="visibility: hidden;"><asp:LinkButton ID="updateQuantity" runat="server" CommandName="update" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "lineNo")%>'><Infojet:Translate ID="update" runat="server" code="SAVE" /></asp:LinkButton></div></td>
        </tr>
    </ItemTemplate>

    <AlternatingItemTemplate>
        <tr>
            <td class="itemListLine" valign="top"><%#DataBinder.Eval(Container.DataItem, "itemNo")%></td>
            <td class="itemListLine" valign="top" align="left"><%#DataBinder.Eval(Container.DataItem, "description")%></td>
            <td class="itemListLine" valign="top" align="right"><div id="cartRef_<%#DataBinder.Eval(Container.DataItem, "lineNo")%>" style="float: left; text-align: right;"><%#DataBinder.Eval(Container.DataItem, "referenceNo")%></div><div id="changeCartRef_<%#DataBinder.Eval(Container.DataItem, "lineNo")%>" style="visibility: hidden; float: left;"><asp:textBox Id="referenceBox" Runat="server" Width="100px" Text='<%#DataBinder.Eval(Container.DataItem, "referenceNo")%>' CssClass="Textfield" /></div></td>
            <td class="itemListLine" valign="top" align="right"><%#DataBinder.Eval(Container.DataItem, "unitPrice", "{0:n2}")%></td>
            <td class="itemListLine" valign="top" align="right"><div id="cartQuantity_<%#DataBinder.Eval(Container.DataItem, "lineNo")%>" style="float: right; text-align: right;"><%#DataBinder.Eval(Container.DataItem, "quantity")%></div><div id="changeCartQuantity_<%#DataBinder.Eval(Container.DataItem, "lineNo")%>" style="visibility: hidden; float: right;"><asp:textBox Id="quantityBox" Runat="server" Width="40px" Text='<%#DataBinder.Eval(Container.DataItem, "quantity")%>' CssClass="Textfield" /></div></td>
            <td class="itemListLine" valign="top" align="right"><%#DataBinder.Eval(Container.DataItem, "formatedAmount")%></td>
            <td class="itemListLine" valign="top" align="center"><div id="lineButtons_<%#DataBinder.Eval(Container.DataItem, "lineNo")%>"><a href="javascript:changeQuantity(<%#DataBinder.Eval(Container.DataItem, "lineNo")%>)"><Infojet:Translate runat="server" ID="Translate10" code="CHANGE" /></a> | <asp:LinkButton ID="removeLink" runat="server" CommandName="remove" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "lineNo")%>'><Infojet:Translate runat="server" ID="Translate3" code="REMOVE" /></asp:LinkButton></div><div id="changeButton_<%#DataBinder.Eval(Container.DataItem, "lineNo")%>" style="visibility: hidden;"><asp:LinkButton ID="updateQuantity" runat="server" CommandName="update" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "lineNo")%>'><Infojet:Translate ID="update" runat="server" code="SAVE" /></asp:LinkButton></div></td>
        </tr>
    </AlternatingItemTemplate>    
    
    </asp:Repeater>

    <tr>
        <td class="itemListAltLine">&nbsp;</td>
        <td class="itemListAltLine" align="left"><Infojet:Translate runat="server" ID="Translate12" code="FREIGHT"/></td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right"><asp:Label ID="freightAmountLabel" runat="server"></asp:Label></td>
        <td class="itemListAltLine" align="right"><b>&nbsp;</b></td>
    </tr>
    <tr>
        <td class="itemListAltLine">&nbsp;</td>
        <td class="itemListAltLine" align="left"><Infojet:Translate runat="server" ID="Translate13" code="ADMIN FEE"/></td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right"><asp:Label ID="adminAmountLabel" runat="server"></asp:Label></td>
        <td class="itemListAltLine" align="right"><b>&nbsp;</b></td>
    </tr>
    <tr>
        <td class="itemListAltLine">&nbsp;</td>
        <td class="itemListAltLine" align="left"><b><Infojet:Translate runat="server" ID="total" code="SUBTOTAL"/></b></td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right"><b>&nbsp;</b></td>
        <td class="itemListAltLine" align="right"><b><asp:Label ID="totalLabel" runat="server"></asp:Label></b></td>
        <td class="itemListAltLine" align="right"><b>&nbsp;</b></td>
    </tr>  
   
    <tr>
        <td class="itemListAltLine">&nbsp;</td>
        <td class="itemListAltLine" align="left"><Infojet:Translate runat="server" ID="Translate8" code="VAT AMOUNT"/></td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right"><b>&nbsp;</b></td>
        <td class="itemListAltLine" align="right"><asp:Label ID="vatAmountLabel" runat="server"></asp:Label></td>
        <td class="itemListAltLine" align="right"><b>&nbsp;</b></td>
    </tr> 
    <tr>
        <td class="itemListAltLine">&nbsp;</td>
        <td class="itemListAltLine" align="left"><b><Infojet:Translate runat="server" ID="Translate9" code="TOTAL INCL VAT"/></b></td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right">&nbsp;</td>
        <td class="itemListAltLine" align="right"><b>&nbsp;</b></td>
        <td class="itemListAltLine" align="right"><b><asp:Label ID="totalInclVatLabel" runat="server"></asp:Label></b></td>
        <td class="itemListAltLine" align="right"><b>&nbsp;</b></td>
    </tr>        
</table>

   
<div style="float:left; width: 100%;"><asp:Button ID="goBackButton" runat="server" CssClass="Button" />&nbsp;<asp:button ID="nextButton" runat="server" CssClass="Button" /></div>