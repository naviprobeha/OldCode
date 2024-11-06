<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Language_STANDARD.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.Language_STANDARD" %>

<asp:Repeater ID="languageRepeater" runat="server">
<ItemTemplate><a href="<%#DataBinder.Eval(Container.DataItem, "changeUrl")%>"><%#DataBinder.Eval(Container.DataItem, "languageText")%></a></ItemTemplate>
<SeparatorTemplate> | </SeparatorTemplate>
</asp:Repeater>
