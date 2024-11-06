<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Menu_LINE.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.Menu_LINE" %>
<asp:Repeater ID="menuRepeater" runat="server">
<HeaderTemplate>
    <table cellspacing="0" cellpadding="0" border="0" width="100%">
    <tr>
</HeaderTemplate>
<ItemTemplate>
    <td width="100"><a href="<%#DataBinder.Eval(Container.DataItem, "link")%>"><%#DataBinder.Eval(Container.DataItem, "text")%></a></td>
</ItemTemplate>

<FooterTemplate>
    </tr>
    </table>
</FooterTemplate>
</asp:Repeater>
