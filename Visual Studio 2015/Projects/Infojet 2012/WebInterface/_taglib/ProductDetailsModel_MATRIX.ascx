<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductDetailsModel_MATRIX.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.ProductDetailsModel_MATRIX" %>

<div class="userControl">
    <asp:Panel ID="productMatrixPanel" runat="server">
    
    <h1><asp:Label runat="server" ID="productHeader"></asp:Label></h1>
    <div style="float: left; padding-top: 5px;">
        <div style="float: left; padding-right: 5px; width: 250px;">
            <div style="height: 250px;">
                <asp:Panel runat="server" ID="productImage">
                    <a href="<%= productWebImage.getUrl(1000, 1000) %>" id="Zoomer" rel="zoom-width:450px;zoom-height:350px;zoom-distance:6px" class="MagicZoom"><img src="<%= productWebImage.getUrl(400, 400) %>" /></a>
                </asp:Panel>
                <asp:Image runat="server" id="noProductImage" ImageUrl="../_assets/img/no_image.jpg" Visible="false" /><br /><br />
            </div>
            <asp:Repeater runat="server" ID="productImageRepeater">
                <ItemTemplate>
                    <a href="<%#DataBinder.Eval(Container.DataItem, "changeUrl")%>"><img src="<%#DataBinder.Eval(Container.DataItem, "url")%>" alt="<%#DataBinder.Eval(Container.DataItem, "description")%>" /></a>
                </ItemTemplate>
            </asp:Repeater>
	    <div style="float: left;">
		<asp:Label runat="server" ID="productDescription"></asp:Label>    
	    </div>               
        </div>
	<div style="padding-left: 5px; float: left; background-color: #f5f5f5; width: 350px; height: 450px; border-top: dashed 1px #777777; border-bottom: dashed 1px #777777;">
            <asp:label runat="server" ID="productNoLabel" Width="150" Font-Bold="true"/> <asp:label runat="server" ID="productNo"/><br />
            <asp:label runat="server" ID="manufacturerLabel" Width="150" Font-Bold="true"/> <asp:label runat="server" ID="manufacturer"/><br />
            <asp:label runat="server" ID="inventoryLabel" Width="150" Font-Bold="true"/> <asp:label runat="server" ID="inventory" Visible="false"/><span id="inventoryPanel"></span><br />
            <asp:label runat="server" ID="leadTimeLabel" Width="150" Font-Bold="true"/> <asp:label runat="server" ID="leadTime"/><span id="leadTimePanel"></span><br />
            <asp:label runat="server" ID="leadTimeLabel2" Text="&nbsp;" Width="150" Font-Bold="true"/> <asp:label runat="server" ID="leadTime2"/><span id="leadTime2Panel"></span><br />
            <asp:label runat="server" ID="unitListPriceLabel" Width="150" Font-Bold="true"/> <asp:label runat="server" ID="unitListPrice"/><br />
            <asp:repeater runat="server" ID="itemAttributeRepeater">
                <ItemTemplate>
                <asp:label runat="server" ID="attributeLabel" Width="150" Height="20" Font-Bold="true"><%#DataBinder.Eval(Container.DataItem, "text")%></asp:label> <%#DataBinder.Eval(Container.DataItem, "itemValue")%><br />
                </ItemTemplate>
            </asp:repeater>
            <br />
            
            <asp:Panel runat="server" ID="buyPanel" Visible="false">                

		    <asp:label runat="server" ID="unitPrice" Width="150" Font-Bold="true" Font-Size="16px" ForeColor="Red"/><br />
		    <br />
		    <asp:label runat="server" ID="quantityLabel" Font-Bold="true" Visible="false"/>

		    <script type="text/javascript">

		    <asp:Repeater id="itemInfoRepeater" runat="server">
		    <HeaderTemplate>
			function showInventory(itemNo)
			{

		    </HeaderTemplate>
		    <ItemTemplate>
				if (itemNo == '<%#DataBinder.Eval(Container.DataItem, "no")%>')
				{
					document.getElementById("inventoryPanel").innerHTML = '<%#DataBinder.Eval(Container.DataItem, "inventoryText")%>';
					document.getElementById("leadTimePanel").innerHTML = '<%#DataBinder.Eval(Container.DataItem, "nextPlannedReceipt")%>';
					document.getElementById("leadTime2Panel").innerHTML = '<%#DataBinder.Eval(Container.DataItem, "secondPlannedReceipt")%>';
				}
		    </ItemTemplate>    
		    <FooterTemplate>
			}
		    </FooterTemplate>

		    </asp:Repeater>

		    </script>

            
		    <table id="matrix">
		    <asp:repeater runat="server" ID="matrixHeaderRepeater">
			<HeaderTemplate>
			    <tr>
			    <td>&nbsp;</td>
			</HeaderTemplate>

			<ItemTemplate>
			    <td align="center"><%#DataBinder.Eval(Container.DataItem, "description")%></td>
			</ItemTemplate>

			<FooterTemplate>
			    </tr>
			</FooterTemplate>
		    </asp:repeater>
		    <asp:repeater runat="server" ID="matrixBodyRepeater">
			<ItemTemplate>
			    <tr>
			    <td><%#DataBinder.Eval(Container.DataItem, "description")%></td>

			    <asp:repeater runat="server" ID="matrixCellRepeater" dataSource='<%#DataBinder.Eval(Container.DataItem, "subLevelValues")%>' OnItemCreated="matrixCellRepeater_ItemCreated">
				<ItemTemplate>
				    <td align="center"><asp:HiddenField ID="itemNo" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "itemNo")%>' /><asp:HiddenField ID="variantCode" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "variantCode")%>' /><asp:TextBox ID="matrixQuantityBox" runat="server" Width="20px" MaxLength="5" CssClass="Textfield"></asp:TextBox></td>
				</ItemTemplate>
			    </asp:repeater>                   

			    </tr>
			</ItemTemplate>
		    </asp:repeater>            
		    </table>
		    <br />
                <asp:Button runat="server" ID="buyButton" CssClass="Button" 
                    onclick="buyButton_Click" />
            </asp:Panel>
            <br />
        </div>
    </div>
    </asp:Panel>    
</div>

