<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserDefined_SALESID.ascx.cs" Inherits="WebInterface._taglib.UserDefined_SALESID" %>
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

</table>