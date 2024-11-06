<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductDetailsModel_STANDARD.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.ProductDetailsModel_STANDARD" %>
<%@ Register TagPrefix="Infojet" TagName="Translate" src="Translate.ascx" %>

<link href="/_assets/js/magiczoom/magiczoom.css" rel="stylesheet" type="text/css" media="screen"/>
<script src="/_assets/js/magiczoom/magiczoom.js" type="text/javascript"></script>

<%
    string totalHeight = "";
    if (calendarPanel.Visible == false) totalHeight = "height: 250px;";

    string detailsHeight = "height: 130px;";
    if (matrixPanel.Visible == true) detailsHeight = "height: 51px;";
%>

<div class="userControl">
    <h1><asp:Label runat="server" ID="productHeader"></asp:Label></h1>
    <div style="float: left; padding-top: 5px; margin-top: 10px;">
        <div style="float: left; padding-right: 5px; width: 250px;">
            <div style="height: 250px;">
                <asp:Panel runat="server" ID="productImage">
                    <a href="<%= productWebImage.getUrl(1000, 1000) %>" id="Zoomer" rel="zoom-width:405px;zoom-height:400px;zoom-distance:6px" class="MagicZoom" style="border: 1px solid #b5b5b5;"><img src="<%= productWebImage.getUrl(250, 250) %>" style="border: 1px solid #b5b5b5;" /></a>                
                </asp:Panel>                
                <asp:Image runat="server" id="noProductImage" ImageUrl="../_assets/img/no_image.jpg" Visible="false" /><br /><br />
            </div>
            <br/>
            <asp:Repeater runat="server" ID="productImageRepeater">
                <ItemTemplate>                    
                	 <a href="<%#((Navipro.Infojet.Lib.ProductImage)Container.DataItem).getUrlFromSize(1000, 1000) %>" rel="zoom-id:Zoomer;" rev="<%#((Navipro.Infojet.Lib.ProductImage)Container.DataItem).getUrlFromSize(250, 250) %>" ><img src="<%#((Navipro.Infojet.Lib.ProductImage)Container.DataItem).getUrlFromSize(50, 50) %>" style="border: 1px solid #b5b5b5;" /></a>
                </ItemTemplate>
            </asp:Repeater>            
        </div>
        <div style="margin-left: 20px; float: left; background-color: #f5f5f5; width: 500px; <%= totalHeight %> border: solid 1px #b5b5b5;">            
            <div style="float: left; width: 500px;">
                <div style="float: left; width: 250px; <%= detailsHeight %> padding-top: 16px; padding-left: 16px; padding-bottom: 16px;">
                    <asp:label runat="server" ID="productNoLabel" Width="150" Font-Bold="true"/> <asp:label runat="server" ID="productNo"/><br />
                    <asp:label runat="server" ID="inventoryLabel" Width="150" Font-Bold="true"/> <asp:label runat="server" ID="inventory"/><br />            
                    <asp:label runat="server" ID="unitListPriceLabel" Width="150" Font-Bold="true"/> <asp:label runat="server" ID="unitListPrice"/><br />
                    <asp:label runat="server" ID="manufacturerLabel" Width="150" Font-Bold="true"/> <asp:label runat="server" ID="manufacturer"/><br />
                    <asp:label runat="server" ID="leadTimeLabel" Width="150" Font-Bold="true"/> <asp:label runat="server" ID="leadTime"/><br />
                    <asp:label runat="server" ID="nextDeliveryDateLabel" Width="150" Font-Bold="true"/> <asp:label runat="server" ID="nextDeliveryDate"/><br />
                    
                    <asp:repeater runat="server" ID="itemAttributeRepeater">
                        <ItemTemplate>
                        <asp:label runat="server" ID="attributeLabel" Width="150" Height="20" Font-Bold="true"><%#DataBinder.Eval(Container.DataItem, "text")%></asp:label> <%#DataBinder.Eval(Container.DataItem, "itemValue")%><br />
                        </ItemTemplate>
                    </asp:repeater>
                    <asp:label runat="server" ID="unitPrice" Width="150" Font-Bold="true" Font-Size="16px" ForeColor="Red"/>
                                       
                 </div>
                 <asp:Panel runat="server" ID="buyPanel" Visible="false">
                    
                     <asp:Panel runat="server" ID="singleItemPanel">
                        <div style="float: left; width: 500px;  border-top: solid 1px #b5b5b5; background-color: #ffffff;">  
                            <div style="float: left; padding-left: 16px; padding-top: 10px; padding-bottom: 10px;">    
                                <asp:label runat="server" ID="quantityLabel" Font-Bold="true"/> <asp:TextBox runat="server" ID="quantityBox" CssClass="Textfield" Width="30px"></asp:TextBox> 
                                <asp:Button runat="server" ID="buyButton" CssClass="Button" onclick="buyButton_Click" /><br />
                                <asp:Label ID="errorMessageSingle" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                            </div>
                        </div>
                     </asp:Panel>                    
                        
                     <asp:Panel ID="matrixPanel" runat="server" Visible="false">
                     
                         <div style="float: left; width: 200px; height: 200px; border-left: solid 1px #b5b5b5; border-right: solid 1px #b5b5b5; padding-right: 10px; padding-left: 10px; background-color: #ffffff;">
                        
	                        <table id="matrix" style="padding-top: 16px;">
		                    <asp:repeater runat="server" ID="matrixHeaderRepeater">
		                    </asp:repeater>

              	                        
	                        <asp:repeater runat="server" ID="matrixBodyRepeater">
		                    <ItemTemplate>
                                <tr>
	                                <td width="60" align="left" style="font-weight: bold;" colspan="2"><%#DataBinder.Eval(Container.DataItem, "description")%></td>
	                                <td width="100" align="right" style="font-weight: bold;"><Infojet:Translate ID="inventory" runat="server" code="IN STOCK" /></td>
                                </tr>
     		                    
		                        <asp:repeater runat="server" ID="matrixCellRepeater" dataSource='<%#DataBinder.Eval(Container.DataItem, "subLevelValues")%>'>
			                    <ItemTemplate>
		                            <tr>
			                            <td width="60" align="left"><%#DataBinder.Eval(Container.DataItem, "description")%></td>
			                            <td width="60" align="left"><asp:HiddenField ID="itemNo" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "itemNo")%>' /><asp:HiddenField ID="itemVariantCode" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "itemVariantCode")%>' /><asp:TextBox ID="matrixQuantityBox" runat="server" Width="40px" MaxLength="5" CssClass="Textfield"></asp:TextBox></td>
			                            <td width="60" align="right"><asp:HiddenField ID="inventory" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "inventory")%>' /><asp:label ID="inventoryLabel" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "inventory")%>'></asp:label></td>
		                            </tr>
			                    </ItemTemplate>
		                        </asp:repeater>                   

		                    </ItemTemplate>
	                        </asp:repeater>            
	                        </table>
                            <asp:Label ID="errorMessage" runat="server" Visible="false" ForeColor="Red"></asp:Label><br /><br />
                            <asp:Button runat="server" ID="buyMatrixButton" CssClass="Button" onclick="buyButton_Click" />
                 
                            <br />
                         </div>
                       
                        </asp:Panel>

                 </asp:Panel>
                    
               </div>
            <asp:Panel runat="server" ID="calendarPanel" Visible="true">
            
                <div style="float: left; width: 100%; background-color: #ffffff; height: 350px; border-top: solid 1px #b5b5b5;">                
                <div style="float: left; width: 480px; margin-top: 10px; margin-bottom: 10px; padding-left: 12px;"><Infojet:Translate ID="calendarLabel1" runat="server" code="CALENDAR LABEL1" />&nbsp;<Infojet:Translate ID="calendarLabel2" runat="server" code="CALENDAR LABEL2" /></div> 
                <div style="float: left; width: 250px; margin-top: 10px; margin-bottom: 10px; padding-left: 12px;"><b><Infojet:Translate ID="fromDateLabel" runat="server" code="FROM DATE CAL LABEL" /></b> <span>
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
            <br />
            <div style="float: left; width: 484px; border-top: solid 1px #b5b5b5; padding-left: 16px; padding-top: 15px;">
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
                <script type="text/javascript"> var addthis_config = { "data_track_clickback": true, ui_language: "en" }; </script>
                <script type="text/javascript" src="http://s7.addthis.com/js/250/addthis_widget.js#pubid=ra-4dd2126c30db0166"></script>
            </div>
        </div>
    </div>
    <div style="float: left; width: 400px;">
        <asp:Label runat="server" ID="productDescription"></asp:Label>    
    </div>    
</div>