<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductDetailsModel_CHERRY.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.ProductDetailsModel_CHERRY" %>
<%@ Register TagPrefix="Infojet" TagName="Translate" src="Translate.ascx" %>

<link href="/_assets/js/magiczoom/magiczoom.css" rel="stylesheet" type="text/css" media="screen"/>
<script src="/_assets/js/magiczoom/magiczoom.js" type="text/javascript"></script>



<div class="userControl">
    <div id="productDetails">
        <div id="image">
            <div id="mainImage">
                <asp:Panel runat="server" ID="productImage">
                    <a href="<%= productWebImage.getUrl(1000, 1000) %>" id="Zoomer" rel="zoom-width:405px;zoom-height:400px;zoom-distance:6px" class="MagicZoom"><img src="<%= productWebImage.getUrl(400, 400) %>" alt=""/></a>                
                </asp:Panel>                
                <asp:Image runat="server" id="noProductImage" ImageUrl="../_assets/img/no_image.jpg" Visible="false" />           
            </div>
            <div id="additionalImages">
                <asp:Repeater runat="server" ID="productImageRepeater">
                    <ItemTemplate>                    
                	     <div class="image"><a href="<%#((Navipro.Infojet.Lib.ProductImage)Container.DataItem).getUrlFromSize(1000, 1000) %>" rel="zoom-id:Zoomer;" rev="<%#((Navipro.Infojet.Lib.ProductImage)Container.DataItem).getUrlFromSize(400, 400) %>" ><img src="<%#((Navipro.Infojet.Lib.ProductImage)Container.DataItem).getUrlFromSize(75, 75) %>" alt=""/></a></div>
                    </ItemTemplate>
                </asp:Repeater>                   
            </div>
        </div>            
        <div id="detailBody">
             <h2><asp:Label runat="server" ID="productHeader"></asp:Label></h2>
             <p id="productNo"><asp:label runat="server" ID="productNo"/></p>
             <p id="description"><asp:Label runat="server" ID="productDescription"></asp:Label>
                    <asp:label runat="server" ID="productNoLabel" Font-Bold="true" Visible="false"/>
                    <asp:label runat="server" ID="inventoryLabel" Font-Bold="true" Visible="false"/> <asp:label runat="server" ID="inventory" Visible="false"/>            
                    <asp:label runat="server" ID="unitListPriceLabel" Font-Bold="true" Visible="false"/> <asp:label runat="server" ID="unitListPrice" Visible="false"/>
                    <asp:label runat="server" ID="manufacturerLabel" Font-Bold="true" Visible="false"/> <asp:label runat="server" ID="manufacturer"/>
                    <asp:label runat="server" ID="leadTimeLabel"  Font-Bold="true" Visible="false"/> <asp:label runat="server" ID="leadTime"/>
                    <asp:label runat="server" ID="nextDeliveryDateLabel" Font-Bold="true" Visible="false"/> <asp:label runat="server" ID="nextDeliveryDate"/>
             
                 <asp:repeater runat="server" ID="itemAttributeRepeater">
                    <HeaderTemplate><br /></HeaderTemplate>
                    <ItemTemplate>
                    <asp:label runat="server" ID="attributeLabel" Width="150" Height="20" Font-Bold="true"><%#DataBinder.Eval(Container.DataItem, "text")%></asp:label> <%#DataBinder.Eval(Container.DataItem, "itemValue")%><br />
                    </ItemTemplate>
                 </asp:repeater>
             </p>
             
             <%
			string hidePrice = "";
            if ((!webItemList.itemListFilterForm.showUnitPrice) && (!webItemList.itemListFilterForm.showUnitListPrice)) hidePrice = "style=\"visibility: hidden;\"";
	     %>
             
             <p id="price" <%= hidePrice %>><%= formatUnitPrice(productItem.formatedUnitPrice.ToString()) %><asp:label runat="server" ID="unitPrice" Width="150" Visible="false"/></p>            
            
             <asp:Panel runat="server" ID="buyPanel" Visible="false">
                
                 <asp:Panel runat="server" ID="singleItemPanel">
                        <table>
                            
                            <% 
                                Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();
                                if (webItemList.checkShowInventory(infojet)) { %>
                            <tr>
                                <td height="30"><span class="inventoryLabel"><Infojet:Translate ID="inStock" runat="server" code="INVENTORY STATUS" /></span></td>
                                <td width="50" align="right"><%= productItem.inventoryText %></td>
                                <td>&nbsp;</td>
                            </tr>
                            <% } %>
                            <tr>
                                <td width="50"><span class="qtyLabel"><asp:label runat="server" ID="quantityLabel" Visible="false"/><Infojet:Translate ID="qtyLabel" runat="server" code="QTY" /></span></td>
                                <td align="right" width="40"><asp:TextBox runat="server" ID="quantityBox" CssClass="Textfield" Width="30px"></asp:TextBox></td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="3" height="20"><asp:Label ID="errorMessageSingle" runat="server" Visible="false" ForeColor="Red"></asp:Label>&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="2" height="30" valign="bottom" align="left"><asp:LinkButton runat="server" ID="buyButton" OnClick="buyButton_Click"><div class="buyButton">
                                <% 
                                	Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();
                                	if (productItem.webItemSetting.calendarBooking) 
                                		Response.Write(infojet.translate("BOOK")); 
                                	else 
                                		Response.Write(infojet.translate("BUY")); 
                                		
                                %></div></asp:LinkButton></td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>           
                    
                 </asp:Panel>                    
                    
                 <asp:Panel ID="matrixPanel" runat="server" Visible="false" Width="300px">
                        <%
                       
                            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();     
                        %>
                        <div class="variantHead"><% if ((productItem.webItemSetting.visibility == 1) && (webItemList.checkShowInventory(infojet))) { %><Infojet:Translate ID="Translate3" runat="server" code="INVENTORY STATUS" /><% } %></div>
                        <div class="variantQty">
                            <div class="column">
                                <table>
                   
                                <asp:repeater runat="server" ID="matrixHeaderRepeater">
                                </asp:repeater>

                  	                        
                                <asp:repeater runat="server" ID="matrixBodyRepeater">
                                    <ItemTemplate>
                                        <asp:Repeater ID="matrixCellRepeater" runat="server" DataSource='<%#DataBinder.Eval(Container.DataItem, "subLevelValues")%>'>
                                            <ItemTemplate>
                                                        <tr>
                                                            <td width="30" height="25" style="font-size: 12px; font-weight: bold;"><%#DataBinder.Eval(Container.DataItem, "code")%></td>
                                                            <td width="30" align="right" style="font-size: 10px;"><asp:label ID="inventoryLabel" runat="server"><%#DataBinder.Eval(Container.DataItem, "inventoryText")%></asp:label><asp:HiddenField ID="inventory" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "inventory")%>'></asp:HiddenField></td>
                                                            <td width="50" align="right"><asp:HiddenField ID="itemNo" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "itemNo")%>' /><asp:HiddenField ID="itemVariantCode" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "code")%>' /><asp:TextBox ID="matrixQuantityBox" runat="server" Width="40px" MaxLength="5" CssClass="Textfield"></asp:TextBox></td>
                                                        </tr>
                                                        <asp:Literal ID="separator" runat="server" Visible='<%# (((Container.ItemIndex +1) % 4) == 0)%>'>
                                                            </table></div><div class="column"><table>
                                                        </asp:Literal>
                                            </ItemTemplate>                    
                                        </asp:Repeater>
                                    </ItemTemplate>
                                </asp:Repeater>
                                </table>           
                            </div>
                        </div>
                        <div class="variantFoot">
                            <div style="float: left; width: 340px;"><asp:Label ID="errorMessage" runat="server" Visible="false" ForeColor="Red"></asp:Label></div>
                            <div style="float: left;"><asp:LinkButton ID="buyMatrixButton" runat="server" OnClick="buyButton_Click"><div class="buyButton">
                            <% 
                                	Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();
                                	if (productItem.webItemSetting.calendarBooking) 
                                		Response.Write(infojet.translate("BOOK")); 
                                	else 
                                		Response.Write(infojet.translate("BUY")); 
                                		
                                %></div></asp:LinkButton></div>
                        </div>  
                    </asp:Panel>
                 

             </asp:Panel>
             <div style="float: left; margin-top: 20px; width: 300px;">
                <div class="addthis_toolbox addthis_default_style">
                    <!-- <a class="addthis_button_preferred_1"></a> -->
                    <!-- <a class="addthis_button_preferred_2"></a> -->
                    <!-- <a class="addthis_button_preferred_3"></a> -->
                    <!-- <a class="addthis_button_preferred_4"></a> -->
                    <!-- <a class="addthis_button_compact"></a> -->
                    <!-- <a class="addthis_counter addthis_bubble_style"></a> -->
                    <a class="addthis_button_email"></a> 
                    <a class="addthis_button_print"></a>                    
                </div>
                <br />
                <script type="text/javascript">                    var addthis_config = { "data_track_clickback": true, ui_language: "en" }; </script>
                <script type="text/javascript" src="http://s7.addthis.com/js/250/addthis_widget.js#pubid=ra-4dd2126c30db0166"></script>
             </div>
             
            
        </div>

             <asp:Panel runat="server" ID="calendarPanel" Visible="true">
            
                <div style="float: left; width: 790px; background-color: #ffffff; height: 350px; border-top: solid 1px #b5b5b5;">                
                <div style="float: left; width: 750px; margin-top: 10px; margin-bottom: 10px; padding-left: 10px;"><Infojet:Translate ID="calendarLabel1" runat="server" code="CALENDAR LABEL1" />&nbsp;<Infojet:Translate ID="calendarLabel2" runat="server" code="CALENDAR LABEL2" /></div> 
                <div style="float: left; width: 250px; margin-top: 10px; margin-bottom: 10px; padding-left: 10px;"><b><Infojet:Translate ID="fromDateLabel" runat="server" code="FROM DATE CAL LABEL" /></b> <span>
                    <asp:DropDownList ID="fromTime" runat="server" CssClass="DropDown">
                        <asp:ListItem Value="00:00:00" Text="00:00"></asp:ListItem>
                        <asp:ListItem Value="01:00:00" Text="01:00"></asp:ListItem>
                        <asp:ListItem Value="02:00:00" Text="02:00"></asp:ListItem>
                        <asp:ListItem Value="03:00:00" Text="03:00"></asp:ListItem>
                        <asp:ListItem Value="04:00:00" Text="04:00"></asp:ListItem>
                        <asp:ListItem Value="05:00:00" Text="05:00"></asp:ListItem>
                        <asp:ListItem Value="06:00:00" Text="06:00"></asp:ListItem>
                        <asp:ListItem Value="07:00:00" Text="07:00"></asp:ListItem>
                        <asp:ListItem Value="08:00:00" Text="08:00"></asp:ListItem>
                        <asp:ListItem Value="09:00:00" Text="09:00"></asp:ListItem>
                        <asp:ListItem Value="10:00:00" Text="10:00"></asp:ListItem>
                        <asp:ListItem Value="11:00:00" Text="11:00"></asp:ListItem>
                        <asp:ListItem Value="12:00:00" Text="12:00"></asp:ListItem>
                        <asp:ListItem Value="13:00:00" Text="13:00"></asp:ListItem>
                        <asp:ListItem Value="14:00:00" Text="14:00"></asp:ListItem>
                        <asp:ListItem Value="15:00:00" Text="15:00"></asp:ListItem>
                        <asp:ListItem Value="16:00:00" Text="16:00"></asp:ListItem>
                        <asp:ListItem Value="17:00:00" Text="17:00"></asp:ListItem>
                        <asp:ListItem Value="18:00:00" Text="18:00"></asp:ListItem>
                        <asp:ListItem Value="19:00:00" Text="19:00"></asp:ListItem>
                        <asp:ListItem Value="20:00:00" Text="20:00"></asp:ListItem>
                        <asp:ListItem Value="21:00:00" Text="21:00"></asp:ListItem>
                        <asp:ListItem Value="22:00:00" Text="22:00"></asp:ListItem>
                        <asp:ListItem Value="23:00:00" Text="23:00"></asp:ListItem>
                    </asp:DropDownList></span>
                    <asp:Calendar ID="fromDate" OnDayRender="Calendar_DayRender" runat="server" Width="225px" TitleStyle-ForeColor="#ffffff" ShowGridLines="true" NextPrevStyle-ForeColor="#ffffff" TitleStyle-BackColor="#cccccc" OtherMonthDayStyle-ForeColor="#cccccc" BorderColor="#ffffff" DayStyle-BorderColor="#e5e5e5"></asp:Calendar>
                </div>
                <div style="float: left; margin-top: 10px; margin-bottom: 10px;"><b><Infojet:Translate ID="toDateLabel" runat="server" code="TO DATE CAL LABEL" /></b> <span>                    
                    <asp:DropDownList ID="toTime" runat="server" CssClass="DropDown">
                        <asp:ListItem Value="00:00:00" Text="00:00"></asp:ListItem>
                        <asp:ListItem Value="01:00:00" Text="01:00"></asp:ListItem>
                        <asp:ListItem Value="02:00:00" Text="02:00"></asp:ListItem>
                        <asp:ListItem Value="03:00:00" Text="03:00"></asp:ListItem>
                        <asp:ListItem Value="04:00:00" Text="04:00"></asp:ListItem>
                        <asp:ListItem Value="05:00:00" Text="05:00"></asp:ListItem>
                        <asp:ListItem Value="06:00:00" Text="06:00"></asp:ListItem>
                        <asp:ListItem Value="07:00:00" Text="07:00"></asp:ListItem>
                        <asp:ListItem Value="08:00:00" Text="08:00"></asp:ListItem>
                        <asp:ListItem Value="09:00:00" Text="09:00"></asp:ListItem>
                        <asp:ListItem Value="10:00:00" Text="10:00"></asp:ListItem>
                        <asp:ListItem Value="11:00:00" Text="11:00"></asp:ListItem>
                        <asp:ListItem Value="12:00:00" Text="12:00"></asp:ListItem>
                        <asp:ListItem Value="13:00:00" Text="13:00"></asp:ListItem>
                        <asp:ListItem Value="14:00:00" Text="14:00"></asp:ListItem>
                        <asp:ListItem Value="15:00:00" Text="15:00"></asp:ListItem>
                        <asp:ListItem Value="16:00:00" Text="16:00"></asp:ListItem>
                        <asp:ListItem Value="17:00:00" Text="17:00"></asp:ListItem>
                        <asp:ListItem Value="18:00:00" Text="18:00"></asp:ListItem>
                        <asp:ListItem Value="19:00:00" Text="19:00"></asp:ListItem>
                        <asp:ListItem Value="20:00:00" Text="20:00"></asp:ListItem>
                        <asp:ListItem Value="21:00:00" Text="21:00"></asp:ListItem>
                        <asp:ListItem Value="22:00:00" Text="22:00"></asp:ListItem>
                        <asp:ListItem Value="23:00:00" Text="23:00"></asp:ListItem>
                    </asp:DropDownList></span>
                    <asp:Calendar ID="toDate" OnDayRender="Calendar_DayRender" runat="server" Width="225px" TitleStyle-ForeColor="#ffffff" ShowGridLines="true" NextPrevStyle-ForeColor="#ffffff" TitleStyle-BackColor="#cccccc" OtherMonthDayStyle-ForeColor="#cccccc" BorderColor="#ffffff" DayStyle-BorderColor="#e5e5e5"></asp:Calendar>
                </div>
                </div>
            </asp:Panel>
  
    </div>
</div>

<script runat="server">

    public string formatUnitPrice(string formatedUnitPrice)
    {
        Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();
        Navipro.Infojet.Lib.GeneralLedgerSetup glSetup = new Navipro.Infojet.Lib.GeneralLedgerSetup(infojet.systemDatabase);

        string currencyCode = infojet.currencyCode;
        if (infojet.currencyCode == "") currencyCode = glSetup.lcyCode;

        formatedUnitPrice = formatedUnitPrice.Replace(",", ".");
        //formatedUnitPrice = formatedUnitPrice.Replace(".00", "");

        if (formatedUnitPrice.Contains("."))
        {
            formatedUnitPrice = formatedUnitPrice.Replace(" ", "</span>&nbsp;");
            formatedUnitPrice = formatedUnitPrice.Replace(".", ".<span style=\"font-size: 21px; vertical-align: 11px;\">");
        }

        formatedUnitPrice = formatedUnitPrice.Replace(currencyCode, "<span style=\"font-size: 21px;\">" + currencyCode + "</span><span style=\"font-size: 21px; vertical-align: 11px;\">&nbsp;</span>");

        return formatedUnitPrice;
    }    
    
</script>