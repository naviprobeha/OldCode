<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchEngine_STANDARD.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.SearchEngine_STANDARD" %>
<%@ Register TagPrefix="Infojet" TagName="Translate" src="Translate.ascx" %>

<%
    
    string searchQuery = System.Web.HttpContext.Current.Request["searchQuery"];       
    
    Navipro.Infojet.Lib.DefaultSearchEngine searchEngine = new Navipro.Infojet.Lib.DefaultSearchEngine();

    Navipro.Infojet.Lib.NavigationItemCollection navigationItemCollection = new Navipro.Infojet.Lib.NavigationItemCollection();
    navigationItemCollection = searchEngine.getAllSearchResults(navigationItemCollection, infojet, searchQuery);

    searchResultRepeater.DataSource = navigationItemCollection;
    searchResultRepeater.DataBind();
    
%>


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