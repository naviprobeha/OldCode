<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserDefined_SHOWCASE.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.UserDefined_SHOWCASE" %>
<%@ Register TagPrefix="Infojet" TagName="Translate" src="Translate.ascx" %>
<br />
<asp:Label runat="server" ID="errorMessageLabel" ForeColor="Red"></asp:Label>

<script type="text/javascript">

var changed = false;

function goBack()
{
    if (changed)
    {
        if (confirm("<%= infojet.translate("CANCEL SHOWCASE") %>"))
        {
            document.location.href = "<%= prevWebPageUrl %>";
        }
    }
    else
    {
            document.location.href = "<%= prevWebPageUrl %>";
    }    
}

function checkQuantities()
{
    <asp:Repeater runat="server" ID="quantityRepeater">    
        <ItemTemplate>
            if (document.pageForm.realQuantity<%#DataBinder.Eval(Container.DataItem, "itemNo")%>.value > <%#DataBinder.Eval(Container.DataItem, "quantity")%>)
            {
                alert("<%= infojet.translate("SHOWCASE QUANTITY") %> <%#DataBinder.Eval(Container.DataItem, "quantity")%> <%= infojet.translate("SHOWCASE QUANTITY 2") %> <%#DataBinder.Eval(Container.DataItem, "itemNo")%>");
                return false;
            }
        </ItemTemplate>
    </asp:Repeater>
    
    return true;
}


function submitShowCase()
{
    chosen = "";
    len = document.pageForm.showCaseCalculationMethod.length;

    for (i = 0; i <len; i++) 
    {
        if (document.pageForm.showCaseCalculationMethod[i].checked) 
        {
            chosen = document.pageForm.showCaseCalculationMethod[i].value
        }
    }

    if (chosen == "") 
    {
        alert("<%= infojet.translate("NO METHOD CHOOSEN") %>");
        return;
    }
     
    if (checkQuantities())
    {
        document.pageForm.action = "<%= sendUrl %>";    
        document.pageForm.submit();
    }
}

function setChanged()
{
    changed = true;
}

</script>


<asp:Repeater runat="server" ID="cartItemRepeater">
<HeaderTemplate>
    <table class="tableView" style="width: 100%">
        <tr>
            <th><Infojet:Translate runat="server" ID="itemNo" code="ITEM NO"/></th>
            <th><Infojet:Translate runat="server" ID="description" code="DESCRIPTION"/></th>
            <th style="text-align: right;" title="<%= infojet.translate("DELIVERED QTY TEXT") %>"><Infojet:Translate runat="server" ID="quantity" code="DELIVERED QUANTITY"/></th>
            <th style="text-align: right;" title="<%= infojet.translate("SALESPERSON QTY TEXT") %>"><Infojet:Translate runat="server" ID="realQuantity" code="SALESPERSON QUANTITY"/></th>
            <th style="text-align: right;" title="<%= infojet.translate("QTY PACK MTRL TEXT") %>"><Infojet:Translate runat="server" ID="qtyPackMtrl" code="QTY PACK MTRL"/></th>
            <th style="text-align: right;" title="<%= infojet.translate("QTY PACK SLIPS TEXT") %>"><Infojet:Translate runat="server" ID="qtyPackSlips" code="QTY PACK SLIPS"/></th>
        </tr>
</HeaderTemplate>

<ItemTemplate>
    <tr>
        <td><%#DataBinder.Eval(Container.DataItem, "itemNo")%></td>
        <td align="left"><%#DataBinder.Eval(Container.DataItem, "description")%></td>
        <td align="right"><%#DataBinder.Eval(Container.DataItem, "quantity")%></td>
        <td align="right"><input type="text" name="realQuantity<%#DataBinder.Eval(Container.DataItem, "itemNo")%>" value="<%#DataBinder.Eval(Container.DataItem, "remainingQuantity")%>" class="Textfield" size="3" maxlength="3" onchange="setChanged()" /></td>
        <td align="right"><input type="text" name="qtyPackMtrl<%#DataBinder.Eval(Container.DataItem, "itemNo")%>" value="<%#DataBinder.Eval(Container.DataItem, "quantity2")%>" class="Textfield" size="3" maxlength="3" onchange="setChanged()"/></td>
        <td align="right"><input type="text" name="qtyPackSlips<%#DataBinder.Eval(Container.DataItem, "itemNo")%>" value="<%#DataBinder.Eval(Container.DataItem, "quantity3")%>" class="Textfield" size="3" maxlength="3" onchange="setChanged()"/></td>
    </tr>
</ItemTemplate>

<FooterTemplate>
    </table>
</FooterTemplate>
</asp:Repeater>

<div class="inputForm" style="margin-right: 2px;">
<br />
<div class="pane">
    <div style="width: 100%;">
        <table style="width: 500px">
            <tr>
                <td colspan="2"><label class="header"><Infojet:Translate runat="server" ID="showCaseCalcMethodHeader" code="SHOWCASE CALC METHOD" /></label></td>
            </tr>
            <tr>
                <td colspan="2"><label><Infojet:Translate runat="server" ID="Translate3" code="SHOWCASE CALC TEXT 1" />&nbsp;<Infojet:Translate runat="server" ID="Translate4" code="SHOWCASE CALC TEXT 2" /></label></td>
            </tr>
            <tr>
                <td style="width: 10px"><input type="radio" name="showCaseCalculationMethod" value="1" <%= method1Check %> onclick="setChanged()"/></td>
                <td><label><Infojet:Translate ID="Translate1" runat="server" code="SHOWCASE CALC 1" /></label></td>
            </tr>
            <tr>
                <td style="width: 10px"><input type="radio" name="showCaseCalculationMethod" value="2" <%= method2Check %> onclick="setChanged()"/></td>
                <td><label><Infojet:Translate ID="Translate2" runat="server" code="SHOWCASE CALC 2" /></label></td>
            </tr>
        </table>        
    </div>
</div>
<br />&nbsp;<br />
</div>

<div style="float:left; width: 100%;">
<div style="float:left; text-align: left; padding-top: 5px; width: 50%;"><a href="javascript:goBack()"><img src="_assets/img/<%= infojet.translate("IMG BACK BTN") %>" alt="<%= infojet.translate("BACK") %>" /></a></div>
<div style="float:left; text-align: right; padding-top: 5px; width: 50%;"><% if (sendUrl != "") { %><a href="javascript:submitShowCase()"><img src="_assets/img/<%= infojet.translate(nextBtn) %>" alt="" /></a><% } %></div>
</div>