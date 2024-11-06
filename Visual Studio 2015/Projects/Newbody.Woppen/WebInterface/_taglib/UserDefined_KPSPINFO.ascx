<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserDefined_KPSPINFO.ascx.cs" Inherits="WebInterface._taglib.UserDefined_KPSPINFO" %>
<%@ Register TagPrefix="Infojet" TagName="Translate" src="Translate.ascx" %>

<script type="text/javascript">

function changeSPFullName()
{
    document.getElementById('nameForm').innerHTML = "<input type='text' name='salesPersonName' value='<%= salesPersonWebUserAccount.name %>' class='Textfield' maxlength='50' size='30' />";
    document.getElementById('nameButton').innerHTML = "<a style='font-size: 10px' href='javascript:updateSPName()'><%= infojet.translate("SAVE") %></a>";

}

function showPassword()
{
    document.getElementById('passwordForm').innerHTML = "<%= salesPersonWebUserAccount.password %>";
    document.getElementById('passwordButton').innerHTML = "<a style='font-size: 10px' href='javascript:hidePassword()'><%= infojet.translate("HIDE") %></a>";

}

function hidePassword()
{
    document.getElementById('passwordForm').innerHTML = "********";
    document.getElementById('passwordButton').innerHTML = "<a style='font-size: 10px' href='javascript:showPassword()'><%= infojet.translate("SHOW") %></a>";

}

function updateSPName()
{
    document.pageForm.infoCommand.value = "updateSPName";
    document.pageForm.submit();
}

function deleteSP()
{
    <% if (allowDelete) { %>
    if (confirm("<%= infojet.translate("CONFIRM DELETE SP") %>"))
    {
        document.pageForm.infoCommand.value = "deleteSP";
        document.pageForm.submit();
    }
    <% } else { %>
    alert("<%= infojet.translate("CANNOT DELETE SP") %>");    
    <% } %>   
    
}

function promoteSP()
{
    if (confirm("<%= infojet.translate("CONFIRM PROMOTE SP") %>"))
    {
        document.pageForm.infoCommand.value = "promoteSP";
        document.pageForm.submit();
    }
}

function demoteSP()
{
    if (confirm("<%= infojet.translate("CONFIRM DEMOTE SP") %>"))
    {
        document.pageForm.infoCommand.value = "demoteSP";
        document.pageForm.submit();
    }
}

</script>

<input type="hidden" name="infoCommand" value="" />

<br />
<table style="width: 240px" cellspacing="1" cellpadding="2">
<tr>
    <td colspan="2"><b><Infojet:Translate runat="server" ID="Translate4" code="SP INFO" /></b></td>
</tr>
<tr>
    <td height="25" colspan="2" style="vertical-align: bottom; font-size: 9px;"><Infojet:Translate runat="server" ID="Translate5" code="NAME" /></td>
</tr>        
<tr>
    <td><div id="nameForm"><%= salesPersonWebUserAccount.name %></div></td>
    <td align="right" valign="top"><% if (!salesId.isContactPerson(salesPersonWebUserAccount.no))
                                      { %><div id="nameButton"><a href="javascript:changeSPFullName()" style="font-size: 10px;"><Infojet:Translate runat="server" ID="Translate7" code="CHANGE" /></a></div><% } %></td>
</tr>        
<tr>
    <td height="25" colspan="2" style="vertical-align: bottom; font-size: 9px;"><Infojet:Translate runat="server" ID="Translate1" code="USER ID" /></td>
</tr>        
<tr>
    <td><%= salesPersonWebUserAccount.userId%></td>
    <td align="right">&nbsp;</td>
</tr>        

<tr>
    <td height="25" colspan="2" style="vertical-align: bottom; font-size: 9px;"><Infojet:Translate runat="server" ID="Translate6" code="PASSWORD" /></td>
</tr>        
<tr>
    <td><div id="passwordForm">********</div></td>
    <td align="right"><div id="passwordButton"><a href="javascript:showPassword()" style="font-size: 10px;"><Infojet:Translate runat="server" ID="Translate8" code="SHOW" /></a></div></td>
</tr>        

<tr>
    <td height="25" colspan="2" style="vertical-align: bottom; font-size: 9px;"><Infojet:Translate runat="server" ID="Translate2" code="CELL PHONE NO" /></td>
</tr>        
<tr>
    <td><%= salesPersonWebUserAccount.cellPhoneNo %></td>
</tr>        

<tr>
    <td height="25" colspan="2" style="vertical-align: bottom; font-size: 9px;"><Infojet:Translate runat="server" ID="Translate3" code="EMAIL" /></td>
</tr>        
<tr>
    <td><a href="mailto:<%= salesPersonWebUserAccount.email %>"><%= salesPersonWebUserAccount.email %></a></td>
</tr>        

<% if ((salesId.isPrimaryContactPerson(infojet.userSession.webUserAccount.no)) && (!salesId.isContactPerson(salesPersonWebUserAccount.no)) && (noOfContactSalesIds > 1))
{ %>
    <tr>
        <td align="right" colspan="2"><a href="javascript:promoteSP()" style="font-size: 10px"><Infojet:Translate ID="promoteSP" runat="server" code="PROMOTE SP"/></a></td>
    </tr>        
<% } %>

<% if ((salesId.isPrimaryContactPerson(infojet.userSession.webUserAccount.no)) && (salesId.isSubContactPerson(salesPersonWebUserAccount.no)))
{ %>
    <tr>
        <td align="right" colspan="2"><a href="javascript:demoteSP()" style="font-size: 10px"><Infojet:Translate ID="demoteSP" runat="server" code="DEMOTE SP"/></a></td>
    </tr>        
<% } %>
    
<tr>
    <td align="right" colspan="2"><% if (!salesId.isContactPerson(salesPersonWebUserAccount.no))
                                     { %><a href="javascript:deleteSP()" style="font-size: 10px"><Infojet:Translate ID="deleteSP" runat="server" code="DELETE SP"/></a><% } %></td>
</tr>        

</table>