<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Language.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.WebUserControl2" %>
<asp:Repeater ID="languageRepeater" runat="server">
<ItemTemplate><a href="<%#DataBinder.Eval(Container.DataItem, "changeUrl")%>"><%#DataBinder.Eval(Container.DataItem, "languageText")%></a></ItemTemplate>
<SeparatorTemplate> | </SeparatorTemplate>
</asp:Repeater>
