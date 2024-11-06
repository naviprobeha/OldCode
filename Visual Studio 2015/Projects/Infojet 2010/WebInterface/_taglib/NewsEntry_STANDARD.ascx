<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewsEntry_STANDARD.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.NewsEntry_STANDARD" %>

<% webNewsEntry.setMainImageSize(400, 270); %>

<div class="userControl">
<img src="<%= webNewsEntry.getMainImageUrl() %>" alt="<%= webNewsEntry.header %>" style="float: right; margin-left: 5px; margin-bottom: 5px;" />
<h1><%= webNewsEntry.header %></h1>
<p class="ingress"><%= webNewsEntry.ingress %></p>
<p class="body"><%= webNewsEntry.body %></p>
</div>