<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserDefined_SALESIDINFO.ascx.cs" Inherits="WebInterface._taglib.UserDefined_SALESIDINFO" %>
<%@ Register TagPrefix="Infojet" TagName="Translate" src="Translate.ascx" %>

<table id="salesIdInfo" cellspacing="0" cellpadding="2">
<tr>
    <td id="groupName" colspan="2"><%= salesId.description %></td>
</tr>
<tr>
    <td width="100"><Infojet:Translate runat="server" ID="groupId" code="GROUP ID" />:</td>
    <td><%= salesId.code %></td>
</tr>
<tr>
    <td colspan="2">&nbsp;</td>
</tr>
<tr>
    <td width="100"><Infojet:Translate runat="server" ID="Translate1" code="CONTACT PERSON" />:</td>
    <td><%= salesId.getContact().name %></td>
</tr>
<tr>
    <td width="100"><Infojet:Translate runat="server" ID="Translate4" code="CONTACT EMAIL" />:</td>
    <td><a href="mailto:<%= salesId.getContact().email %>"><%= salesId.getContact().email %></a></td>
</tr>
<tr>
    <td width="100"><Infojet:Translate runat="server" ID="Translate2" code="CONTACT PHONENO" />:</td>
    <td><%= salesId.getContact().phoneNo %></td>
</tr>
<tr>
    <td width="100"><Infojet:Translate runat="server" ID="Translate3" code="CONTACT CELL PHONE" />:</td>
    <td><%= salesId.getContact().cellPhoneNo %></td>
</tr>
<tr>
    <td colspan="2">&nbsp;</td>
</tr>
<tr>
    <td width="100"><Infojet:Translate runat="server" ID="salesPerson" code="SALESPERSON" />:</td>
    <td><%= salesPersonWebUserAccount.name %></td>
</tr>
<tr>
    <td><Infojet:Translate runat="server" ID="email" code="EMAIL" />:</td>
    <td><a href="mailto:<%= salesPersonWebUserAccount.email%>"><%= salesPersonWebUserAccount.email %></a></td>
</tr>
<tr>
    <td><Infojet:Translate runat="server" ID="phoneNo" code="PHONE NO" />:</td>
    <td><%= salesPersonWebUserAccount.phoneNo%></td>
</tr>
<tr>
    <td><Infojet:Translate runat="server" ID="Translate5" code="CELL PHONE NO" />:</td>
    <td><%= salesPersonWebUserAccount.cellPhoneNo%></td>
</tr>
<tr>
    <td colspan="2">&nbsp;</td>
</tr>
<tr>
    <td><Infojet:Translate runat="server" ID="Translate6" code="TOTAL QTY PACKAGES" />:</td>
    <td><%= salesId.soldPackages %> st</td>
</tr>
<tr>
    <td><Infojet:Translate runat="server" ID="Translate7" code="RANKING" />:</td>
    <td><%= salesId.getRanking(salesPersonWebUserAccount.no) %>:a</td>
</tr>

</table>
