<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserDefined_SENDORDERS_SHOWCASE.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.UserDefined_SENDORDERS_SHOWCASE" %>
<%@ Register TagPrefix="Infojet" TagName="Translate" src="Translate.ascx" %>
<br />
<asp:Label runat="server" ID="errorMessageLabel" ForeColor="Red"></asp:Label>

<script type="text/javascript">

function submitShowCase()
{
    document.pageForm.action = "<%= sendUrl %>";    
    document.pageForm.submit();
}
</script>

<!--
<div style="padding: 2px;">
<label for="showCaseCalculationMethod"><Infojet:Translate ID="showCaseCalculationMethod" runat="server" code="SHOWCASE CALC METHOD" /></label> <select name="showCaseCalculationMethod" id="showCaseCalculationMethod" class="DropDown">
<option value="0"><Infojet:Translate ID="mehtod1" runat="server" code="SHOWCASE CALC 1" /></option>
<option value="1"><Infojet:Translate ID="method2" runat="server" code="SHOWCASE CALC 2" /></option>
</select>
</div>
-->

<asp:Repeater runat="server" ID="salesIdRepeater">
<ItemTemplate>
    <div><b><%#DataBinder.Eval(Container.DataItem, "description")%></b></div>
    
    <asp:Repeater runat="server" ID="cartItemRepeater" datasource='<%#DataBinder.Eval(Container.DataItem, "soldShowCaseItems")%>'>
    <HeaderTemplate>
        <table class="tableView" style="width: 100%">
            <tr>
                <th><Infojet:Translate runat="server" ID="itemNo" code="ITEM NO"/></th>
                <th><Infojet:Translate runat="server" ID="description" code="DESCRIPTION"/></th>
                <th style="text-align: right;"><Infojet:Translate runat="server" ID="quantity" code="QUANTITY"/></th>
                <th style="text-align: right;"><Infojet:Translate runat="server" ID="realQuantity" code="REAL QUANTITY"/></th>
                <th style="text-align: right;"><Infojet:Translate runat="server" ID="qtyPackMtrl" code="QTY PACK MTRL"/></th>
                <th style="text-align: right;"><Infojet:Translate runat="server" ID="qtyPackSlips" code="QTY PACK SLIPS"/></th>
            </tr>
    </HeaderTemplate>

    <ItemTemplate>
        <tr>
            <td><%#DataBinder.Eval(Container.DataItem, "itemNo")%></td>
            <td align="left"><%#DataBinder.Eval(Container.DataItem, "description")%></td>
            <td align="right"><%#DataBinder.Eval(Container.DataItem, "quantity")%></td>
            <td align="right"><input type="text" name="<%#DataBinder.Eval(Container.DataItem, "salesId")%>_realQuantity_<%#DataBinder.Eval(Container.DataItem, "itemNo")%>" value="<%#DataBinder.Eval(Container.DataItem, "remainingQuantity")%>" class="Textfield" size="3" maxlength="3"/></td>
            <td align="right"><input type="text" name="<%#DataBinder.Eval(Container.DataItem, "salesId")%>_qtyPackMtrl_<%#DataBinder.Eval(Container.DataItem, "itemNo")%>" value="<%#DataBinder.Eval(Container.DataItem, "quantity2")%>" class="Textfield" size="3" maxlength="3"/></td>
            <td align="right"><input type="text" name="<%#DataBinder.Eval(Container.DataItem, "salesId")%>_qtyPackSlips_<%#DataBinder.Eval(Container.DataItem, "itemNo")%>" value="<%#DataBinder.Eval(Container.DataItem, "quantity3")%>" class="Textfield" size="3" maxlength="3"/></td>
        </tr>
    </ItemTemplate>

    <FooterTemplate>
        </table>
    </FooterTemplate>
    </asp:Repeater>

    <br />    
</ItemTemplate>
</asp:Repeater>

<div style="float:left; width: 100%;">
<div style="float:left; text-align: left; padding-top: 5px; width: 50%;"><% if (prevWebPageUrl != "") { %><a href="<%= prevWebPageUrl %>"><img src="_assets/img/<%= infojet.translate("IMG BACK BTN") %>" alt="<%= infojet.translate("BACK") %>" /></a><% } %></div>
<div style="float:left; text-align: right; padding-top: 5px; width: 50%;"><% if (sendUrl != "") { %><a href="javascript:submitShowCase()"><img src="_assets/img/<%= infojet.translate("IMG NEXT BTN") %>" alt="<%= infojet.translate("NEXT") %>" /></a><% } %></div>
</div>