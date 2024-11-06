<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserDefined_PROGRESS.ascx.cs" Inherits="WebInterface._taglib.UserDefined_PROGRESS" %>

<asp:Panel ID="progressBarPanel" runat="server" Visible="false">

<div style="background-image: url(_assets/img/<%= progressBarImg %>); width: 100%; height: 80px;">
<br />
<div style="float: left; width: <%= width1 %>; color: #ffffff;"><%= header1 %></div>
<div style="float: left; width: <%= separatorWidth %>;">&nbsp;</div>
<div style="float: left; width: <%= width2 %>; color: #ffffff;"><%= header2 %></div>
<div style="float: left; width: <%= separatorWidth %>;">&nbsp;</div>
<div style="float: left; width: <%= width3 %>; color: #ffffff;"><%= header3 %></div>
<div style="float: left; width: <%= separatorWidth %>;">&nbsp;</div>
<div style="float: left; width: <%= width4 %>; color: #ffffff;"><%= header4 %></div>
<div style="float: left; width: <%= separatorWidth %>;">&nbsp;</div>
<div style="float: left; width: <%= width5 %>; color: #ffffff;"><%= header5 %></div>
<div style="float: left; width: <%= separatorWidth %>;">&nbsp;</div>
<div style="float: left; width: <%= width6 %>; color: #ffffff;"><%= header6 %></div>
</div>

</asp:Panel>