<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" ValidateRequest="false" Inherits="Navipro.Infojet.WebInterface._Default" %>
<%@ Register TagPrefix="Infojet" TagName="Content" src="_taglib/Content.ascx" %>
<%@ Register TagPrefix="Infojet" TagName="Language" src="_taglib/Language.ascx" %>
<%@ Register TagPrefix="Infojet" TagName="Translate" src="_taglib/Translate.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title><Infojet:Translate ID="title" runat="server" code="TITLE" /></title>
    <link href="_assets/css/design.css" rel="stylesheet" type="text/css" />
    <link href="_assets/css/visuals.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <center>
    <form id="pageForm" runat="server">
    <div id="page">
        
        <div id="header">
            <div id="logo">
            </div>       
        </div>

        <div id="border">
        	<div id="menu"><Infojet:Content id="menu" runat="server" part="menu"/></div>
        </div>
        
        <div id="body">

	        <div id="main"><Infojet:Content id="content1" runat="server" part="body"/></div>
	        <div id="right"><Infojet:Content id="content2" runat="server" part="right"/></div>
        </div>
        
        <div id="bottom">
        	<div id="line">&nbsp;</div>
        	<Infojet:Translate id="footer1" runat="server" code="FOOTER1"/><br/>
        	<Infojet:Translate id="footer2" runat="server" code="FOOTER2"/>
        </div>
        
    </div>
        
    </form>
    </center>    
</body>
</html>
