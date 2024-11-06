<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" ValidateRequest="false" Inherits="Navipro.Infojet.WebInterface._Default" %>
<%@ Register TagPrefix="Infojet" TagName="Content" src="_taglib/Content.ascx" %>
<%@ Register TagPrefix="Infojet" TagName="Language" src="_taglib/Language.ascx" %>
<%@ Register TagPrefix="Infojet" TagName="Translate" src="_taglib/Translate.ascx" %>
<%@ Register TagPrefix="Infojet" TagName="Header" src="_taglib/UserDefined_HEADER.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title><Infojet:Translate ID="title" runat="server" code="TITLE" /></title>
    <link href="_assets/css/design_intra.css" rel="stylesheet" type="text/css" />
    <link href="_assets/css/visuals.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <center>
    <form id="pageForm" runat="server">
    <div id="page">
        
        <div id="header">
            <div id="logo">
                <Infojet:Header id="headerPart" runat="server" />
            </div>       
        </div>

        <div id="border">
        	<div id="menu"><Infojet:Content id="menu" runat="server" part="menu"/></div>
        	<div id="submenu"><Infojet:Content id="submenu" runat="server" part="submenu"/></div>
        </div>
        
        <div id="body">
            <div id="bodyTop" style="float:left;">
	            <div id="bodyLeft" style="float:left;"><Infojet:Content id="content2" runat="server" part="topLeft"/></div>
	            <div id="bodyRight" style="float:left;"><Infojet:Content id="content3" runat="server" part="topRight"/></div>
            </div>
	        <div id="main"><Infojet:Content id="content1" runat="server" part="body"/></div>
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
