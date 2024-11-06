<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductDetailsModel_STANDARD.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.ProductDetailsModel_STANDARD" %>

<link href="_assets/js/magiczoom/magiczoom.css" rel="stylesheet" type="text/css" media="screen"/>
<script src="_assets/js/magiczoom/magiczoom.js" type="text/javascript"></script>

<div class="userControl">
    <h1><asp:Label runat="server" ID="productHeader"></asp:Label></h1>
    <div style="float: left; padding-top: 5px;">
        <div style="float: left; padding-right: 5px; width: 250px;">
            <div style="height: 250px;">
                <asp:Panel runat="server" ID="productImage">
                    <a href="<%= productWebImage.getUrl(500, 500) %>" rel="zoom-width:405px;zoom-height:200px;zoom-distance:6px" class="MagicZoom"><img src="<%= productWebImage.getUrl(250, 250) %>" /></a>                
                </asp:Panel>                
                <asp:Image runat="server" id="noProductImage" ImageUrl="../_assets/img/no_image.jpg" Visible="false" /><br /><br />
            </div>
            <asp:Repeater runat="server" ID="productImageRepeater">
                <ItemTemplate>
                    <a href="<%#DataBinder.Eval(Container.DataItem, "changeUrl")%>"><img src="<%#DataBinder.Eval(Container.DataItem, "url")%>" alt="<%#DataBinder.Eval(Container.DataItem, "description")%>" /></a>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div style="padding-left: 5px; float: left; background-color: #f5f5f5; width: 400px; height: 300px; border-top: dashed 1px #777777; border-bottom: dashed 1px #777777;">
            <asp:label runat="server" ID="productNoLabel" Width="150" Font-Bold="true"/> <asp:label runat="server" ID="productNo"/><br />
            <asp:label runat="server" ID="manufacturerLabel" Width="150" Font-Bold="true"/> <asp:label runat="server" ID="manufacturer"/><br />
            <asp:label runat="server" ID="inventoryLabel" Width="150" Font-Bold="true"/> <asp:label runat="server" ID="inventory"/><br />            
            <asp:label runat="server" ID="leadTimeLabel" Width="150" Font-Bold="true"/> <asp:label runat="server" ID="leadTime"/><br />
            <asp:label runat="server" ID="unitListPriceLabel" Width="150" Font-Bold="true"/> <asp:label runat="server" ID="unitListPrice"/><br />
            <asp:repeater runat="server" ID="itemAttributeRepeater">
                <ItemTemplate>
                <asp:label runat="server" ID="attributeLabel" Width="150" Height="20" Font-Bold="true"><%#DataBinder.Eval(Container.DataItem, "text")%></asp:label> <%#DataBinder.Eval(Container.DataItem, "itemValue")%><br />
                </ItemTemplate>
            </asp:repeater>
            <asp:repeater runat="server" ID="dimensionRepeater">
                <ItemTemplate>
                <asp:label runat="server" ID="dimensionLabel" Width="150" Height="20" Font-Bold="true"><%#DataBinder.Eval(Container.DataItem, "description")%></asp:label><asp:HiddenField ID="code" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "code")%>' /> <asp:DropDownList ID="dimensionDropDown" runat="server" DataSource='<%#DataBinder.Eval(Container.DataItem, "values")%>' DataValueField="code" DataTextField="description" AutoPostBack="true" OnSelectedIndexChanged="dimensionDropDown_selectedIndexChanged" CssClass="DropDown"></asp:DropDownList><br />
                </ItemTemplate>
            </asp:repeater>
            <br />
            <asp:label runat="server" ID="unitPrice" Width="150" Font-Bold="true" Font-Size="16px" ForeColor="Red"/><br />
            <br />
            <asp:Panel runat="server" ID="buyPanel" Visible="false">
                <asp:label runat="server" ID="quantityLabel" Font-Bold="true"/> <asp:TextBox runat="server" ID="quantityBox" CssClass="Textfield" Width="30px"></asp:TextBox> 
                <asp:Button runat="server" ID="buyButton" CssClass="Button" 
                    onclick="buyButton_Click" />
            </asp:Panel>
            <br />
            
            <div class="addthis_toolbox addthis_default_style ">
                <a class="addthis_button_preferred_1"></a>
                <a class="addthis_button_preferred_2"></a>
                <a class="addthis_button_preferred_3"></a>
                <a class="addthis_button_preferred_4"></a>
                <a class="addthis_button_compact"></a>
                <a class="addthis_counter addthis_bubble_style"></a>
            </div>
            <script type="text/javascript">    var addthis_config = { "data_track_clickback": true };</script>
            <script type="text/javascript" src="http://s7.addthis.com/js/250/addthis_widget.js#pubid=ra-4dd2126c30db0166"></script>
            
        </div>
    </div>
    <div style="float: left; width: 400px;">
        <asp:Label runat="server" ID="productDescription"></asp:Label>    
    </div>    
</div>