<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchEngine_STANDARD.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.SearchEngine_STANDARD" %>
<%@ Register TagPrefix="Infojet" TagName="Translate" src="Translate.ascx" %>

<br />
<asp:Repeater ID="searchResultRepeater" runat="server">
<ItemTemplate>
<div>
<b><%#DataBinder.Eval(Container.DataItem, "description")%></b><br />
<%#DataBinder.Eval(Container.DataItem, "text")%><br />
<br />
<a href="<%#DataBinder.Eval(Container.DataItem, "link")%>"><Infojet:Translate ID="readMore" runat="server" code="READ MORE" /></a><br />
<br />
</div>
</ItemTemplate>


</asp:Repeater>