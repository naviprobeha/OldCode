<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" ValidateRequest="false" Inherits="Navipro.Infojet.WebInterface._Default" %>
<%@ Register TagPrefix="Infojet" TagName="Content" src="_taglib/Content.ascx" %>
<%@ Register TagPrefix="Infojet" TagName="Language" src="_taglib/Language.ascx" %>
<%@ Register TagPrefix="Infojet" TagName="Translate" src="_taglib/Translate.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title><Infojet:Translate id="title" runat="server" code="TITLE" /></title>
    <link href="_assets/css/design.css" rel="stylesheet" type="text/css" />
    <link href="_assets/css/visuals.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="pageForm" runat="server">
    <div id="page">
        
        <div id="header">
            <div id="logo">
            </div>
		    <div id="info">
		        <div id="languages"><Infojet:Language id="language" runat="server"/>&nbsp;</div>
		        <div id="searchBox"><asp:TextBox runat="server" ID="searchQueryBox" Width="120px" CssClass="Textfield"></asp:TextBox>&nbsp;<asp:Button runat="server" ID="searchButton" CssClass="Button" OnClick="searchButton_Click" />&nbsp;</div>       
		    </div>        
                    
        </div>
        <div id="body">

	        <div id="left"><Infojet:Content id="content1" runat="server" part="left"/></div>
	        <div id="main"><Infojet:Content id="content" runat="server" part="body"/></div>

        </div>
    </div>
    </form>
</body>
</html>
