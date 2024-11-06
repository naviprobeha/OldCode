<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebAdmin._Default" %>
<%@ Register TagPrefix="Infojet" TagName="Ribbon" src="_taglib/Ribbon.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Infojet CMS</title>
    <link href="_assets/css/cms.css" rel="stylesheet" type="text/css" />    
</head>


<body>
    <form id="pageForm" runat="server">   
    <table id="frame" cellspacing="0" cellpadding="0">
    <tr>
        <td id="top" colspan="3"><Infojet:Ribbon id="ribbon" runat="server" /></td>
    </tr>
    <tr>
        <td id="topLeft"></td>
        <td id="top"></td>
        <td id="topRight"></td>
    </tr>
     <tr>
        <td id="left"></td>
        <td id="main">vcvc&nbsp;</td>
        <td id="right"></td>
    </tr>
    <tr>
        <td id="bottomLeft"></td>
        <td id="bottom"></td>
        <td></td>
    </tr>

    </table>
    </form>
</body>
</html>
