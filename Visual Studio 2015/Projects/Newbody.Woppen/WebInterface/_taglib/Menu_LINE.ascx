<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Menu_LINE.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.Menu_LINE" %>
<asp:Repeater ID="menuRepeater" runat="server">
<HeaderTemplate>
    <table cellspacing="0" cellpadding="0" border="0">
    <tr>
</HeaderTemplate>
<ItemTemplate>
    <td style="width: 100px;"><span style="white-space: nowrap;">&nbsp;<a href="<%#DataBinder.Eval(Container.DataItem, "link")%>" title="<%#DataBinder.Eval(Container.DataItem, "helpText")%>" target="<%#DataBinder.Eval(Container.DataItem, "target")%>"><%#DataBinder.Eval(Container.DataItem, "text")%></a>&nbsp;</span></td>
</ItemTemplate>
<FooterTemplate>
    </tr>
    </table>
</FooterTemplate>
</asp:Repeater>
