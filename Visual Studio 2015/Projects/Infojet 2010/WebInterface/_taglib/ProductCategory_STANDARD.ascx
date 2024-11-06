<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductCategory_STANDARD.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.ProductCategory_STANDARD" %>

<div class="userControl">
    <h1><asp:Label runat="server" ID="categoryName"></asp:Label></h1>
    <div style="float: left; padding-top: 5px;">
        <div style="float: left;"><asp:image runat="server" ID="categoryImage" /></div>
        <div style="float: left; width: 500px"><asp:Label runat="server" ID="categoryDescription"></asp:Label></div>
    </div>    
</div>