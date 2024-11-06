<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchBox_STANDARD.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.SearchBox_STANDARD" %>
<%@ Register TagPrefix="Infojet" TagName="Property" src="Property.ascx" %>

<div id="searchBox">
	<div id="searchPanel"><div id="field"><asp:TextBox runat="server" ID="searchQueryBox" Width="120px" CssClass="Textfield"></asp:TextBox></div><div id="button"><asp:Button runat="server" ID="searchButton" OnClick="searchButton_Click" /></div></div>
</div>
