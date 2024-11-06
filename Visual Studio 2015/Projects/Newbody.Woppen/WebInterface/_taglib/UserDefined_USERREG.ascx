<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserDefined_USERREG.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.UserDefined_USERREG" %>
<%@ Register TagPrefix="Infojet" TagName="Translate" src="Translate.ascx" %>
<br />

<div style="color: Red;"><asp:Label ID="errorMessage" runat="server"></asp:Label></div>

<asp:Panel ID="newUserForm" runat="server">

<div class="inputForm" style="margin-right: 2px;">

<div class="pane">
    <asp:Panel runat="server" ID="userRegForm" BackColor="#f5f5f5" Font-Size="11px"></asp:Panel>
</div>
<br />&nbsp;<br />
</div>


<div style="float:left; width: 100%;">
<div style="float:left; text-align: right; padding-top: 5px; width: 100%; color: #ef1c22;">
   <asp:Button runat="server" ID="submitBtn" />
</div>        
</div>
</asp:Panel>

