<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword_STANDARD.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.ChangePassword_STANDARD" %>
<%@ Register TagPrefix="Infojet" TagName="Translate" src="Translate.ascx" %>

<table>
<tr>
    <td style="width: 150px;"><asp:Label runat="server" id="oldPasswordLabel" AssociatedControlID="oldPasswordBox" Width="200px"><Infojet:Translate runat="server" ID="oldPassword" code="OLD PASSWORD" /></asp:Label></td>
    <td><asp:TextBox runat="server" ID="oldPasswordBox" TextMode="Password" CssClass="Textfield" Width="200px"></asp:TextBox></td>
    <td><asp:Label runat="server" ID="oldPasswordValidation" ForeColor="Red"></asp:Label></td>
</tr>
<tr>
    <td><asp:Label runat="server" id="newPasswordLabel" AssociatedControlID="newPasswordBox" Width="200px"><Infojet:Translate runat="server" ID="newPassword" code="NEW PASSWORD" /></asp:Label></td>
    <td><asp:TextBox runat="server" ID="newPasswordBox" TextMode="Password" CssClass="Textfield" Width="200px"></asp:TextBox></td>
    <td><asp:Label runat="server" ID="newPasswordValidation" ForeColor="Red"></asp:Label></td>
</tr>
<tr>
    <td><asp:Label runat="server" id="retypePasswordLabel" AssociatedControlID="retypePasswordBox" Width="200px"><Infojet:Translate runat="server" ID="retypePassword" code="RETYPE PASSWORD" /></asp:Label></td>
    <td><asp:TextBox runat="server" ID="retypePasswordBox" TextMode="Password" CssClass="Textfield" Width="200px"></asp:TextBox></td>
    <td><asp:Label runat="server" ID="retypePasswordValidation" ForeColor="Red"></asp:Label></td>
</tr>
</table>
<asp:Button runat="server" ID="submitBtn" CssClass="Button" /> <asp:Button runat="server" ID="cancelBtn" CssClass="Button" />

