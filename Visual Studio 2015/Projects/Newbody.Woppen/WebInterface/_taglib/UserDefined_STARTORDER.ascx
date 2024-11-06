<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserDefined_STARTORDER.ascx.cs" Inherits="WebInterface._taglib.UserDefined_STARTORDER" %>
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

    
    document.pageForm.submit();

}

</script>

<div class="inputForm" style="margin-right: 2px;">

<div class="pane">
    <div style="width: 100%;">
        <table>
            <tr>
                <td colspan="2"><label class="header"><Infojet:Translate runat="server" ID="contactAddress" code="CONTACT ADDRESS" /></label></td>
            </tr>
            <tr>
                <td style="width: 250px"><label><Infojet:Translate runat="server" ID="Translate1" code="NAME" /> *</label></td>
                <td style="width: 400px"><input type="text" name="name" size="30" maxlength="50" value="<%= infojet.userSession.webUserAccount.billToCompanyName %>" class="Textfield"/></td>
            </tr>
            <tr>
                <td><label><Infojet:Translate runat="server" ID="Translate2" code="ADDRESS" /></label></td>
                <td><input type="text" name="address" size="30" maxlength="50" value="<%= infojet.userSession.webUserAccount.billToAddress %>" class="Textfield" /></td>
            </tr>
            <tr>
                <td><label><Infojet:Translate runat="server" ID="Translate3" code="ADDRESS 2" /> *</label></td>
                <td><input type="text" name="address2" size="30" maxlength="50" value="<%= infojet.userSession.webUserAccount.billToAddress2 %>" class="Textfield" /></td>
            </tr>
            <tr>
                <td><label><Infojet:Translate runat="server" ID="Translate4" code="POST ADDRESS" /> *</label></td>
                <td><input type="text" name="postCode" size="10" maxlength="10" value="<%= infojet.userSession.webUserAccount.billToPostCode %>" class="Textfield" /> <input type="text" name="city" size="30" maxlength="30" value="<%= infojet.userSession.webUserAccount.billToCity %>" class="Textfield" /></td>
            </tr>       
            <tr>
                <td><label><Infojet:Translate runat="server" ID="Translate8" code="PERSON NO" /></label> *</td>
                <td><input type="text" name="personNo" size="30" maxlength="20" value="<%= infojet.userSession.webUserAccount.registrationNo %>" class="Textfield" /></td>
            </tr>
            <tr>
                <td><label><Infojet:Translate runat="server" ID="Translate5" code="CELL PHONE NO" /></label> *</td>
                <td><input type="text" name="phoneNo" size="30" maxlength="20" value="<%= infojet.userSession.webUserAccount.cellPhoneNo %>" class="Textfield" /></td>
            </tr>
            <tr>
                <td><label><Infojet:Translate runat="server" ID="Translate6" code="EMAIL" /></label></td>
                <td><input type="text" name="email" size="30" maxlength="100" value="<%= infojet.userSession.webUserAccount.email %>" class="Textfield" /></td>
            </tr>
            <tr>
                <td colspan="2"><label class="header"><Infojet:Translate runat="server" ID="Translate7" code="UNION SCHOOL" /></label></td>
            </tr>
            <tr>
                <td><label><Infojet:Translate runat="server" ID="Translate9" code="NAME" /> *</label></td>
                <td><input type="text" name="name" size="30" maxlength="50" value="" class="Textfield"/></td>
            </tr>
            <tr>
                <td><label><Infojet:Translate runat="server" ID="Translate15" code="CATEGORY" /> *</label></td>
                <td><select name="category" class="Textfield">
                    <option value="0"><Infojet:Translate runat="server" ID="Translate16" code="SCHOOL" /></option>
                    <option value="1"><Infojet:Translate runat="server" ID="Translate17" code="UNION" /></option>
                    </select></td>
            </tr>
            <tr>
                <td colspan="2"><label class="header"><Infojet:Translate runat="server" ID="Translate10" code="SHIP-TO ADDRESS HEAD" /></label></td>
            </tr>
            <tr>
                <td><label><Infojet:Translate runat="server" ID="Translate11" code="NAME" /> *</label></td>
                <td><input type="text" name="shipToName" size="30" maxlength="50" value="<%= infojet.userSession.webUserAccount.name %>" class="Textfield"/></td>
            </tr>
            <tr>
                <td><label><Infojet:Translate runat="server" ID="Translate12" code="ADDRESS" /></label></td>
                <td><input type="text" name="shipToAddress" size="30" maxlength="50" value="<%= infojet.userSession.webUserAccount.address %>" class="Textfield" /></td>
            </tr>
            <tr>
                <td><label><Infojet:Translate runat="server" ID="Translate13" code="ADDRESS 2" /> *</label></td>
                <td><input type="text" name="shipToAddress2" size="30" maxlength="50" value="<%= infojet.userSession.webUserAccount.address2 %>" class="Textfield" /></td>
            </tr>
            <tr>
                <td><label><Infojet:Translate runat="server" ID="Translate14" code="POST ADDRESS" /> *</label></td>
                <td><input type="text" name="shipToPostCode" size="10" maxlength="10" value="<%= infojet.userSession.webUserAccount.postCode %>" class="Textfield" /> <input type="text" name="shipToCity" size="30" maxlength="30" value="<%= infojet.userSession.webUserAccount.city %>" class="Textfield" /></td>
            </tr>       
            <tr>
                <td colspan="2"><label class="header"><Infojet:Translate runat="server" ID="Translate18" code="STARTORDER HEAD" /></label></td>
            </tr>
            <tr>
                <td><label><Infojet:Translate runat="server" ID="Translate19" code="QTY SALESPERSONS" /> *</label></td>
                <td><input type="text" name="qtySalesPersons" size="3" maxlength="2" value="" class="Textfield"/></td>
            </tr>
            <tr>
                <td><label><Infojet:Translate runat="server" ID="Translate20" code="SO DEL DATE" /> *</label></td>
                <td><%= createDatePicker("soDeliveryDate", DateTime.Today) %></td>
            </tr>
            <tr>
                <td><label><Infojet:Translate runat="server" ID="Translate21" code="SALES CONCEPT" /> *</label></td>
                <td><select name="salesConcept" class="Textfield"></select></td>
            </tr>
            <tr>
                <td><label><Infojet:Translate runat="server" ID="Translate22" code="FO DATE" /> *</label></td>
                <td><%= createDatePicker("foDate", DateTime.Today) %></td>
            </tr>
            <tr>
                <td><label><Infojet:Translate runat="server" ID="Translate23" code="CAMPAINS" /> *</label></td>
                <td><select name="campains" class="Textfield"></select></td>
            </tr>
            <tr>
                <td><label><Infojet:Translate runat="server" ID="Translate25" code="GOAL" /> *</label></td>
                <td><select name="goal" class="Textfield">
                <option value="Annat"><Infojet:Translate runat="server" ID="other" code="OTHER" /></option>
                </select></td>
            </tr>
            <tr>
                <td><label><Infojet:Translate runat="server" ID="Translate26" code="OTHER GOAL" /> *</label></td>
                <td><input type="text" name="otherGoal" size="30" maxlength="100" value="" class="Textfield"/></td>
            </tr>
            <tr>
                <td colspan="2"><label class="header"><Infojet:Translate runat="server" ID="Translate27" code="CONFIRM SO" /></header></td>
            </tr>
            <tr>
                <td colspan="2"><Infojet:Translate runat="server" ID="Translate24" code="CONFIRM SO TEXT" /></td>
            </tr>
            <tr>
                <td><label><Infojet:Translate runat="server" ID="Translate28" code="CONFIRM" /></label></td>
                <td><input type="checkbox" name="confirm" /></td>
            </tr>
        </table>        
    </div>
</div>
<br />&nbsp;<br />
</div>

<div style="float:left; width: 100%;">
<div style="float:left; text-align: left; padding-top: 5px; width: 50%;"><a href="<%= prevWebPageUrl %>"><img src="_assets/img/<%= infojet.translate("IMG BACK BTN") %>" alt="<%= infojet.translate("BACK") %>" /></a></div>
<div style="float:left; text-align: right; padding-top: 5px; width: 50%; color: #ef1c22;"><a href="javascript:submitOrder()"><img src="_assets/img/<%= infojet.translate("IMG NEXT BTN") %>" alt="<%= infojet.translate("NEXT") %>" /></a</div>
</div>