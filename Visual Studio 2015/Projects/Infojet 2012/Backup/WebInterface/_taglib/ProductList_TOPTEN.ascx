<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductList_TOPTEN.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.ProductList_TOPTEN" %>

<script type="text/javascript" src="http://yui.yahooapis.com/combo?2.6.0/build/utilities/utilities.js&2.6.0/build/container/container_core-min.js"></script> 
<script type="text/javascript" src="/_assets/js/carousel/carousel.js"></script>
<script type="text/javascript" src="/_assets/js/carousel/carousel_custom.js"></script>

<link rel="stylesheet" type="text/css" href="/_assets/css/carousel/carousel.css" />
<link rel="stylesheet" type="text/css" href="/_assets/css/carousel/yui.css" />




<!-- Carousel Structure -->
<div id="mycarousel" class="carousel-component">
    <div class="carousel-prev">
        <img id="prev-arrow" class="left-button-image" 
            src="/_assets/img/carousel-left-enabled.gif" alt="Previous Button"/>
    </div>
    <div class="carousel-next">
        <img id="next-arrow" class="right-button-image" 
            src="/_assets/img/carousel-right-enabled.gif" alt="Next Button"/>
    </div>
    <div class="carousel-clip-region">
        <ul class="carousel-list">
        
        <asp:Repeater ID="itemRepeater" runat="server">
            <ItemTemplate>
	            <li id="mycarousel-item-<%#Container.ItemIndex+1 %>"><a href="<%# DataBinder.Eval(Container.DataItem, "link")%>"><img src="<%#DataBinder.Eval(Container.DataItem, "imageUrl")%>" alt="<%#DataBinder.Eval(Container.DataItem, "description")%>"/></a></li>
		    </ItemTemplate>
		</asp:Repeater>
	</ul>
    </div>
</div>


