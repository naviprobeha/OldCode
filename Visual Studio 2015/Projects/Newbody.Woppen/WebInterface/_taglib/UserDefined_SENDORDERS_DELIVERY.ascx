<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserDefined_SENDORDERS_DELIVERY.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.UserDefined_SENDORDERS_DELIVERY" %>
<%@ Register TagPrefix="Infojet" TagName="Translate" src="Translate.ascx" %>
<br />

<script type="text/javascript">

function submitDelivery()
{
    if (document.pageForm.shipToName.value == "")
    {
        alert("<%= infojet.translate("SHOP MSG 5") %>");
        return;
    }

    if (document.pageForm.shipToAddress2.value == "")
    {
        alert("<%= infojet.translate("SHOP MSG 6") %>");
        return;
    }

    if (document.pageForm.shipToPostCode.value == "")
    {
        alert("<%= infojet.translate("SHOP MSG 7") %>");
        return;
    }

    if (document.pageForm.shipToCity.value == "")
    {
        alert("<%= infojet.translate("SHOP MSG 8") %>");
        return;
    }

    if (document.pageForm.phoneNo.value == "")
    {
        alert("<%= infojet.translate("SHOP MSG 10") %>");
        return;
    }

    if (getCheckedValue(document.pageForm.webShipmentMethodCode) == "")
    {
        alert("<%= infojet.translate("SHOP MSG 14") %>");
        return;
    }
    
    document.pageForm.action = "<%= sendUrl %>";
    document.pageForm.submit();

}

function getCheckedValue(radioObj) {
	if(!radioObj)
		return "";
	var radioLength = radioObj.length;
	if(radioLength == undefined)
		if(radioObj.checked)
			return radioObj.value;
		else
			return "";
	for(var i = 0; i < radioLength; i++) {
		if(radioObj[i].checked) {
			return radioObj[i].value;
		}
	}
	return "";
}


</script>

<div class="inputForm" style="margin-right: 2px;">

<div class="pane">
    <div style="width: 400px;">
        <table>
            <tr>
                <td colspan="2"><label class="header"><Infojet:Translate runat="server" ID="shipToAddress" code="SHIP-TO ADDRESS HEAD" /></label></td>
            </tr>
            <tr>
                <td style="width: 100px"><label><Infojet:Translate runat="server" ID="Translate1" code="SHIP-TO NAME" /> *</label></td>
                <td style="width: 300px"><input type="text" name="shipToName" size="30" maxlength="50" value="<%= webCartHeader.shipToName %>" class="Textfield"/></td>
            </tr>
            <tr>
                <td><label><Infojet:Translate runat="server" ID="Translate2" code="SHIP-TO ADDRESS" /></label></td>
                <td><input type="text" name="shipToAddress" size="30" maxlength="50" value="<%= webCartHeader.shipToAddress %>" class="Textfield" /></td>
            </tr>
            <tr>
                <td><label><Infojet:Translate runat="server" ID="Translate3" code="SHIP-TO ADDRESS 2" /> *</label></td>
                <td><input type="text" name="shipToAddress2" size="30" maxlength="50" value="<%= webCartHeader.shipToAddress2 %>" class="Textfield" /></td>
            </tr>
            <tr>
                <td><label><Infojet:Translate runat="server" ID="Translate4" code="SHIP-TO POST ADDRESS" /> *</label></td>
                <td><input type="text" name="shipToPostCode" size="10" maxlength="10" value="<%= webCartHeader.shipToPostCode %>" class="Textfield" /> <input type="text" name="shipToCity" size="30" maxlength="30" value="<%= webCartHeader.shipToCity %>" class="Textfield" /></td>
            </tr>       
            <tr>
                <td><label><Infojet:Translate runat="server" ID="Translate5" code="CELL PHONE NO" /></label> *</td>
                <td><input type="text" name="phoneNo" size="30" maxlength="20" value="<%= webCartHeader.phoneNo %>" class="Textfield" /></td>
            </tr>
            <tr>
                <td><label><Infojet:Translate runat="server" ID="Translate6" code="EMAIL" /></label></td>
                <td><input type="text" name="email" size="30" maxlength="100" value="<%= webCartHeader.email %>" class="Textfield" /></td>
            </tr>
            <tr>
                <td colspan="2"><label style="color: red"><Infojet:Translate runat="server" ID="Translate7" code="CHECK ADDRESS" /></label></td>
            </tr>
        </table>        
    </div>
    <div style="width: 300px;">
        <table>
            <tr>
                <td><label class="header"><Infojet:Translate runat="server" ID="Translate8" code="MESSAGE" /></label></td>
            </tr>
            <tr>
                <td><textarea style="width: 250px; height: 100px;" class="Textfield" name="message" onkeypress="return (this.value.length <= 250);"><%= webCartHeader.message %></textarea></td>
            </tr>
        </table>        
    </div>
</div>
<br />&nbsp;<br />
</div>


<asp:Repeater runat="server" ID="shipmentMethodRepeater">
<HeaderTemplate>
    <table class="tableView" style="width: 100%">
</HeaderTemplate>

<ItemTemplate>
    <tr>
        <td align="center" style="width: 20px;"><input type="radio" name="webShipmentMethodCode" value="<%#DataBinder.Eval(Container.DataItem, "code")%>" /></td>
        <td><b><%#DataBinder.Eval(Container.DataItem, "description")%></b><br /><%#DataBinder.Eval(Container.DataItem, "text")%><br />&nbsp;</td>
        <td align="right" valign="top"><%#DataBinder.Eval(Container.DataItem, "formatedAmount")%><input type="hidden" name="<%#DataBinder.Eval(Container.DataItem, "code")%>" value="<%#DataBinder.Eval(Container.DataItem, "amount")%>" /></td>
    </tr>
</ItemTemplate>
<FooterTemplate>
    </table>
</FooterTemplate>
</asp:Repeater>


<div style="float:left; width: 100%;">
<div style="float:left; text-align: left; padding-top: 5px; width: 50%;"><% if (prevWebPageUrl != "") { %><a href="<%= prevWebPageUrl %>"><img src="_assets/img/<%= infojet.translate("IMG BACK BTN") %>" alt="<%= infojet.translate("BACK") %>" /></a><% } %></div>
<div style="float:left; text-align: right; padding-top: 5px; width: 50%; color: #ef1c22;"><% if (nextWebPageUrl != "")
                                                                                             { %><a href="javascript:submitDelivery()"><img src="_assets/img/<%= infojet.translate("IMG NEXT BTN") %>" alt="<%= infojet.translate("NEXT") %>" /></a><% } %></div>
</div>