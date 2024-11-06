<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Stylesheet.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.Stylesheet" %>

<%
    Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();
    Response.Write("<link href=\"_assets/css/"+infojet.webSite.code+"_design.css\" rel=\"stylesheet\" type=\"text/css\" />");
    Response.Write("<link href=\"_assets/css/" + infojet.webSite.code + "_visuals.css\" rel=\"stylesheet\" type=\"text/css\" />");    
    
%>