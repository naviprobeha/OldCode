<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword_STANDARD.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.ForgotPassword_STANDARD" %>
<%@ Register TagPrefix="Infojet" TagName="Translate" src="Translate.ascx" %>
<br />
<table>
<tr>
    <td style="width: 50px;"><asp:Label runat="server" id="emailLabel" AssociatedControlID="emailBox" Width="200px"><Infojet:Translate runat="server" ID="email" code="EMAIL" /></asp:Label></td>
    <td><asp:TextBox runat="server" ID="emailBox" CssClass="Textfield" Width="200px"></asp:TextBox></td>
    <td><asp:Label runat="server" ID="emailValidation" ForeColor="Red"></asp:Label></td>
</tr>
</table>
<asp:Button runat="server" ID="submitBtn" CssClass="Button" />

