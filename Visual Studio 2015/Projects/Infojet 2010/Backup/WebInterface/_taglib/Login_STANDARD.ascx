<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Login_STANDARD.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.Login_STANDARD" %>

<div class="userControl">

<asp:LoginView runat="server" ID="loginView">
    <AnonymousTemplate>
        <asp:Login ID="LoginForm" runat="server" onauthenticate="Login1_Authenticate" onloggedin="Login1_LoggedIn">
        <LayoutTemplate>
            <table>
            <tr>
                <td colspan="2" style="padding: 2px;"><asp:Label runat="server" id="userIdLabel" Font-Size="10px"></asp:Label><br /><asp:TextBox ID="UserName" runat="server" CssClass="Textfield" Width="80px" /></td>
            </tr>
            <tr>
                <td style="padding: 2px; width: 50%;"><asp:Label runat="server" id="passwordLabel" Font-Size="10px"></asp:Label><br /><asp:TextBox ID="Password" TextMode="Password" runat="server" Width="80px" CssClass="Textfield" /></td>
                <td style="padding: 2px; width: 50%" valign="bottom"><asp:ImageButton CommandName="Login" id="LoginImageButton" runat="server" ImageUrl="../_assets/img/login_button.gif" /></td>
            </tr>
            <tr>
                <td colspan="3" style="padding: 2px;">
                    <table>
                    <tr>
                        <td><asp:CheckBox ID="RememberMe" runat="server" />&nbsp;</td>
                        <td><asp:Label ID="rememberMeLabel" AssociatedControlID="RememberMe" runat="server" Font-Size="10px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="2"><asp:LinkButton runat="server" ID="forgotPassword" OnClick="forgotPassword_Click" CssClass="small"><asp:Label ID="forgotPasswordLabel" runat="server" Font-Size="10px"></asp:Label></asp:LinkButton></td>
                    </tr>
                    </table>
                </td>
            </tr>
            </table>   
        </LayoutTemplate>
        </asp:Login>        
    </AnonymousTemplate>
    <LoggedInTemplate>
        <div>
            <asp:Label runat="server" ID="customerName" Font-Bold="true" Font-Size="11px"></asp:Label>, <asp:Label runat="server" ID="customerCity" Font-Bold="true" Font-Size="11px"></asp:Label><br />
            <asp:Label runat="server" ID="customerNoLabel" Font-Size="11px"></asp:Label>: <asp:Label runat="server" ID="customerNo" Font-Size="11px"></asp:Label><br />
            <asp:Label runat="server" ID="ipAddressLabel" Font-Size="11px"></asp:Label>: <asp:Label runat="server" ID="ipAddress" Font-Size="11px"></asp:Label><br />
            <asp:HyperLink runat="server" ID="myProfileLink" Font-Size="11px" ForeColor="#ffffff"></asp:HyperLink> | <asp:LinkButton runat="server" ID="logoffLink" Font-Size="11px" ForeColor="#ffffff"></asp:LinkButton> 
        </div>        
    </LoggedInTemplate>
</asp:LoginView>
</div>

