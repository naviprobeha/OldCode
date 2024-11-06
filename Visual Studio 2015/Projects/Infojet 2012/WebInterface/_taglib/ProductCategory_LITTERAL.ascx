<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductCategory_LITTERAL.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.ProductCategory_LITTERAL" %>

<div class="userControl">
    <h1><%= webItemCategory.getTranslation().description %></h1>
    <div style="float: left; padding-top: 5px; padding-bottom: 10px;">
        <div style="float: left;"><asp:image runat="server" ID="categoryImage" /></div>
        <div style="float: left; width: 500px"><%= webItemCategory.getDescription() %></div>
    </div>    
</div>