<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductList_PEAR.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.ProductList_PEAR" %>
<%@ Register TagPrefix="Infojet" TagName="Translate" src="Translate.ascx" %>


<%
	
	Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();

	if (listView.ImageUrl == "None")
	{
		listView.ImageUrl = infojet.getProperty("350 LIST BTN SEL");
		gridView.ImageUrl = infojet.getProperty("350 GRID BTN");
		tableView.ImageUrl = infojet.getProperty("350 LIST BTN");
	}

	if (!IsPostBack)
	{
		
                filterBox.Text = infojet.translate("FILTER DESCRIPTION");
                filterBox.Attributes.Add("onfocus", "this.value=''");
		
	}

%>


<div id="addToCartOpacity">
</div>
<div id="addToCartPopup">
	<div class="center">
		<img id="lightbox" src="/_assets/img/bigrotation2.gif" alt=""/><br/>
		<Infojet:Translate ID="addingToCart" runat="server" code="ADDING TO CART" />
	</div>
</div>



<script language="javascript">

function showPopup()
{
	document.getElementById("addToCartOpacity").style.display = "inline";
	document.getElementById("addToCartPopup").style.display = "inline";

	setTimeout("hidePopup()", 3000);
}

function hidePopup()
{
	document.getElementById("addToCartOpacity").style.display = "none";
	document.getElementById("addToCartPopup").style.display = "none";

}

</script>

<asp:panel runat="server" ID="itemFilterForm" CssClass="itemFilterForm" defaultbutton="filterButton">
    <div class="header">
        <div style="float: left; padding: 5px; margin-top: 10px; margin-left: 10px; height: 50px;">
            <asp:label runat="server" ID="sorting1Label" Width="60px" Height="20"></asp:label> 
            <asp:DropDownList runat="server" ID="sorting1List" CssClass="DropDown" 
                AutoPostBack="true" onselectedindexchanged="updateFilter"></asp:DropDownList> <asp:DropDownList runat="server" ID="sorting2List" CssClass="DropDown" AutoPostBack="true" OnSelectedIndexChanged="updateFilter"></asp:DropDownList>
        </div>
        <div style="float: left; padding: 5px; margin-left: 20px; height: 50px; margin-top: 8px;">               
            <asp:label runat="server" ID="filterLabel" Width="40px" Height="20" Visible="false"></asp:label> <asp:TextBox runat="server" ID="filterBox" CssClass="Textfield"/> <asp:Button runat="server" ID="filterButton" CssClass="Button" OnClick="updateFilter"/>
        </div>
        <div style="float: left; padding: 5px; margin-left: 20px; margin-top: 10px; height: 50px;">           
            <div style="float: left; padding-top: 2px; padding-right: 2px;"><asp:CheckBox runat="server" ID="showInventoryOnly" AutoPostBack="true" OnCheckedChanged="updateFilter"/></div> <div style="float: left;"><asp:Label runat="server" ID="showInventoryOnlyLabel" Height="20"></asp:Label></div>
        </div>
        <div style="float: left; padding: 5px; margin-left: 40px; margin-top: 10px; height: 50px;">           
            <div style="float: left; padding-top: 2px; padding-left: 2px;"><asp:ImageButton ID="listView" runat="server" ImageUrl="None" OnClick="viewChange_Click" CommandArgument="LIST" /></div>
            <div style="float: left; padding-top: 2px; padding-left: 20px;"><asp:ImageButton ID="gridView" runat="server" ImageUrl="None" OnClick="viewChange_Click" CommandArgument="GRID" /></div>
            <div style="float: left; padding-top: 2px; padding-left: 20px;"><asp:ImageButton ID="tableView" runat="server" ImageUrl="None" OnClick="viewChange_Click" CommandArgument="TABLE" /></div>
        </div>
        <div style="float: left; padding: 5px; margin-top: 10px; margin-left: 10px; height: 50px;">
            <asp:label runat="server" ID="noOfItemsPerPageLabel" Width="60px" Height="20"></asp:label> 
            <asp:DropDownList runat="server" ID="noOfItemsPerPage" CssClass="DropDown" 
                AutoPostBack="true" onselectedindexchanged="updateFilter">
                <asp:ListItem Value="25" Text="25"></asp:ListItem>    
                <asp:ListItem Value="50" Text="50"></asp:ListItem>    
                <asp:ListItem Value="75" Text="75"></asp:ListItem>    
                <asp:ListItem Value="100" Text="100"></asp:ListItem>    
            </asp:DropDownList>
        </div>
       
    </div>
    <br />&nbsp;
    <asp:Repeater ID="itemAttributeFilterRepeater" runat="server">
        <HeaderTemplate>
            <div class="itemListHeader" style="float: left; width: 790px;">        
        </HeaderTemplate>
        <ItemTemplate>
            <div style="float: left; padding: 5px; width: 150px;">
                <%#DataBinder.Eval(Container.DataItem, "text")%><br />
                <asp:DropDownList runat="server" ID="attributeFilter" CssClass="DropDown" AutoPostBack="true" onSelectedIndexChanged="attributeFilterList_SelectedIndexChanged" DataSource='<%#DataBinder.Eval(Container.DataItem, "itemAttributeValueCollection")%>' DataValueField="itemValue" DataTextField="text">
                </asp:DropDownList>
            </div>
        </ItemTemplate>
        
        <FooterTemplate>
            </div>
        </FooterTemplate>
    </asp:Repeater>
</asp:panel>
&nbsp;<br />


<asp:UpdatePanel runat="server" id="singleQtyUpdatePanel" updatemode="Always">
<ContentTemplate>


<asp:Repeater ID="productListRepeater" runat="server" EnableViewState="true" OnItemCommand="repeater_itemCommand">
  <ItemTemplate>
        <asp:HiddenField ID="itemNo" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "no")%>' />
  
        <div class="itemListProduct">            
            <div class="leftPart">
                <div class="image"><div style="float: left; width: 170px; height: 170px;"><a href="<%#DataBinder.Eval(Container.DataItem, "link")%>" class="inherit"><img src="<%#DataBinder.Eval(Container.DataItem, "imageUrl")%>" alt="" /></a></div></div>
                <div class="details">
                    <a name="<%#DataBinder.Eval(Container.DataItem, "no")%>"><a href="<%#DataBinder.Eval(Container.DataItem, "link")%>" class="inherit"><h2><%#DataBinder.Eval(Container.DataItem, "description")%></h2></a></a>
                    <p class="productNo"><%#DataBinder.Eval(Container.DataItem, "no")%></p>
                    <p><%# ((Navipro.Infojet.Lib.ProductItem)Container.DataItem).extendedText %></p>
                </div>
                <%
                	string hidePrice = "";
                	if ((!webItemList.itemListFilterForm.showUnitPrice) && (!webItemList.itemListFilterForm.showUnitListPrice)) hidePrice = "style=\"visibility: hidden;\"";
                %>
                <div class="moreInfo">
                    <p class="attributes">
		     <asp:repeater runat="server" ID="itemAttributeRepeater" datasource='<%#DataBinder.Eval(Container.DataItem, "itemAttributes")%>'>
			<ItemTemplate>
				<%#DataBinder.Eval(Container.DataItem, "itemValue")%>
			</ItemTemplate>
		     </asp:repeater>
		    </p>
                    <div class="price" <%= hidePrice %>><%# formatUnitPrice(DataBinder.Eval(Container.DataItem, "formatedUnitPrice").ToString())%></div>
                    <div class="info"><a href="<%#DataBinder.Eval(Container.DataItem, "link")%>" class="inherit">
                    	<asp:panel id="readMoreText1" runat="server" Visible='<%# ((Navipro.Infojet.Lib.ProductItem)Container.DataItem).webItemSetting.calendarBooking %>'>
                    		<Infojet:Translate ID="readMoreBooking" runat="server" code="READ MORE BOOKING" />
                    	</asp:panel>
                    	<asp:panel id="readMoreText2" runat="server" Visible='<%# !((Navipro.Infojet.Lib.ProductItem)Container.DataItem).webItemSetting.calendarBooking %>'>
                    		<Infojet:Translate ID="readMore" runat="server" code="READ MORE" />
                    	</asp:panel>                    	
                    	</a>
                    </div>
                </div>
            </div>

            <div class="rightPart">
            
                <asp:Panel ID="buyPanel" runat="server" CssClass="buyPanel" visible='<%#(((Navipro.Infojet.Lib.ProductItem)Container.DataItem).isBuyable && (!((Navipro.Infojet.Lib.ProductItem)Container.DataItem).webItemSetting.calendarBooking))%>' >
               
                    <asp:Panel ID="singleQty" runat="server" CssClass="singleQty" Visible='<%# (((Navipro.Infojet.Lib.ProductItem)Container.DataItem).webItemSetting.type == 0) %>'>
                                       
                        <table>
                            <tr>
                                <td height="30"><Infojet:Translate ID="inStock" runat="server" code="INVENTORY STATUS" /></td>
                                <td width="50" align="right" nowrap="nowrap"><%#DataBinder.Eval(Container.DataItem, "inventoryText")%></td>
                            </tr>
                            <tr>
                                <td><span class="qtyLabel"><Infojet:Translate ID="quantity" runat="server" code="QTY" /></span></td>
                                <td align="right"><asp:TextBox ID="quantityBox" runat="server" CssClass="Textfield" Width="40px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td colspan="2" height="30"><asp:Label ID="errorMessage" runat="server" Visible="false" ForeColor="Red"></asp:Label></td>
                            </tr>
                            <tr>
                                <td colspan="2" height="30" valign="bottom" align="right"><asp:LinkButton ID="buy" EnableViewState="true" runat="server" CommandName="addToCart" CommandArgument="0" OnClientClick="javascript:showPopup();"><div class="buyButton"><Infojet:Translate ID="buyButton2" runat="server" code="BUY" /></div></asp:LinkButton></td>
                            </tr>
                        </table>           
                
               
                    </asp:Panel>
                    <asp:Panel ID="variantQty" runat="server" CssClass="variantQty" Visible='<%# (((Navipro.Infojet.Lib.ProductItem)Container.DataItem).webItemSetting.type == 1) %>'>
                        <div class="variantHead"><asp:panel id="inventoryStatusPanel" runat="server" Visible='<%# (((Navipro.Infojet.Lib.ProductItem)Container.DataItem).webItemSetting.visibility == 1) %>'><Infojet:Translate ID="Translate3" runat="server" code="INVENTORY STATUS" /></asp:panel></div>
                        <div class="variantQty">
                            <div class="column">
                                <table>
                   
                                <asp:Repeater ID="vertCollection" runat="server" DataSource='<%#DataBinder.Eval(Container.DataItem, "horizDimension")%>'>                        
                                            <ItemTemplate>
                                                        <tr>
                                                            <td width="30" height="25" style="font-size: 12px; font-weight: bold;"><%#DataBinder.Eval(Container.DataItem, "code")%></td>
                                                            <td width="30" align="right" style="font-size: 10px;"><asp:label ID="inventoryLabel" runat="server"><%#DataBinder.Eval(Container.DataItem, "inventoryText")%></asp:label></td>
                                                            <td width="50" align="right"><asp:textbox ID="matrixQuantityBox" runat="server" CssClass="Textfield" Width="40px"></asp:textbox><asp:HiddenField ID="itemVariantNo" Value='<%#DataBinder.Eval(Container.DataItem, "itemNo")%>' runat="server"></asp:HiddenField><asp:HiddenField ID="itemVariantCode" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "code")%>'></asp:HiddenField><asp:HiddenField ID="inventory" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "inventory")%>'></asp:HiddenField></td>
                                                        </tr>
                                                        <asp:Literal ID="separator" runat="server" Visible='<%# (((Container.ItemIndex +1) % 4) == 0)%>'>
                                                            </table></div><div class="column"><table>
                                                        </asp:Literal>
                                            </ItemTemplate>                    
                                </asp:Repeater>
                                </table>           
                            </div>
                        </div>
                        <div class="variantFoot">
                            <div style="float: left; width: 170px;"><asp:Label ID="errorMessageVariant" runat="server" Visible="false" ForeColor="Red"></asp:Label></div>
                            <div style="float: right;"><asp:LinkButton ID="buyVar" runat="server" EnableViewState="true" CommandName="addToCart" CommandArgument="1"><div class="buyButton"><Infojet:Translate ID="Translate1" runat="server" code="BUY" /></div></asp:LinkButton></div>
                        </div>  
                    </asp:Panel>
                    
                	<asp:panel id="doneMessagePanel" runat="server" Visible="false">
                	<script language="javascript">
                		shiftOpacity("doneMessage_<%#DataBinder.Eval(Container.DataItem, "no")%>", 500);
                		
                		setTimeout("shiftOpacity('doneMessage_<%#DataBinder.Eval(Container.DataItem, "no")%>', 500)", 3000);
                	
                	</script>
                	</asp:panel>
                    
                </asp:Panel>
		<asp:panel id="bookPtnPanel" runat="server" Visible='<%# ((Navipro.Infojet.Lib.ProductItem)Container.DataItem).webItemSetting.calendarBooking %>'>
                        <table style="width: 310px; height: 175px;">
                            <tr>
                                <td height="30" valign="bottom" align="right"><a href="<%#DataBinder.Eval(Container.DataItem, "link")%>" class="inherit"><div class="buyButton"><Infojet:Translate ID="buyButton3" runat="server" code="BOOK" /></div></a></td>
                            </tr>
                        </table>           		
		</asp:panel>                
            </div>
        
        </div>
  </ItemTemplate>            
</asp:Repeater>

</ContentTemplate>
</asp:UpdatePanel>


<asp:Repeater ID="productGridRepeater" runat="server" Visible="false">
  <HeaderTemplate>
    <div class="productGrid">
  </HeaderTemplate>
  <ItemTemplate>
        <div class="itemGridProduct">
        
          <div class="image"><div style="float: left; width: 170px; height: 170px;"><a href="<%#DataBinder.Eval(Container.DataItem, "link")%>" class="inherit"><img src="<%#DataBinder.Eval(Container.DataItem, "imageUrl")%>" alt="" /></a></div></div>
          <div class="details">
            <div class="header"><a href="<%#DataBinder.Eval(Container.DataItem, "link")%>" class="inherit"><h2><%#DataBinder.Eval(Container.DataItem, "description")%></h2></a></div>
            <div class="description">
                <p><%#DataBinder.Eval(Container.DataItem, "no")%></p>
            </div>
          </div>
          <%
		string hidePrice = "";
               	if ((!webItemList.itemListFilterForm.showUnitPrice) && (!webItemList.itemListFilterForm.showUnitListPrice)) hidePrice = "style=\"visibility: hidden;\"";
          %>
          <div class="attributes">
		     <asp:repeater runat="server" ID="itemAttributeRepeater" datasource='<%#DataBinder.Eval(Container.DataItem, "itemAttributes")%>'>
			<ItemTemplate>
				<%#DataBinder.Eval(Container.DataItem, "itemValue")%>
			</ItemTemplate>
		     </asp:repeater>
          </div>
          <div class="price" <%= hidePrice %>><%#formatUnitPrice(DataBinder.Eval(Container.DataItem, "formatedUnitPrice").ToString())%>
          </div>
          <div class="moreInfo">         
            <div class="info">
                <table>
                    <tr>
                        <td colspan="2"><a href="<%#DataBinder.Eval(Container.DataItem, "link")%>"><div class="buyButton"><Infojet:Translate ID="readMore" runat="server" code="READ MORE" /></div></a></td>
                    </tr>
                 </table>
           </div>
          </div>
        </div>
        
  </ItemTemplate>
  <FooterTemplate>
    </div>
  </FooterTemplate>
</asp:Repeater>

<asp:Repeater ID="productTableRepeater" runat="server" Visible="false">
  <HeaderTemplate>
    <table id="productTable">
    <tr>
    	<th>&nbsp;</th>
    	<th>Artikelnr</th>
    	<th>Beskrivning</th>
    	<th>Pris</th>
    	<th>&nbsp;</th>
    </tr>	
  </HeaderTemplate>
  <ItemTemplate>
    <tr>
    	<td>&nbsp;</td>
    	<td><%#DataBinder.Eval(Container.DataItem, "no")%></td>
    	<td><%#DataBinder.Eval(Container.DataItem, "description")%></td>
    	<td><%#DataBinder.Eval(Container.DataItem, "formatedUnitPrice")%></td>
    	<td><a href="<%#DataBinder.Eval(Container.DataItem, "link")%>"><Infojet:Translate ID="readMore" runat="server" code="READ MORE" /></a></td>
    </tr>        
  </ItemTemplate>
  <FooterTemplate>
    </table>
  </FooterTemplate>
</asp:Repeater>

<div id="pageNavigation">
<asp:repeater runat="server" id="pageNavigationRepeater">
<ItemTemplate>
	<asp:panel runat="server" CssClass="left" id="selected" visible='<%# (((Navipro.Infojet.Lib.NavigationItem)Container.DataItem).selected) %>'><b><%#DataBinder.Eval(Container.DataItem, "description")%></b></asp:panel>
	<asp:panel runat="server" CssClass="left" id="link" visible='<%# (!((Navipro.Infojet.Lib.NavigationItem)Container.DataItem).selected) %>'><asp:linkbutton runat="server" id="pageNavItem" OnClick="pageNavigation_Click" CommandName="navigate" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "description2")%>' Text='<%#DataBinder.Eval(Container.DataItem, "description")%>'/></asp:panel>
</ItemTemplate>
<SeparatorTemplate><div>&nbsp;|&nbsp;</div></SeparatorTemplate>
</asp:repeater>
</div>

<script runat="server">

    public void repeater_itemCommand(object sender, RepeaterCommandEventArgs e)
    {
        Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();

        if (e.CommandName == "addToCart")
        {
            HiddenField itemNoField = (HiddenField)e.Item.FindControl("itemNo");
            
            
            if (e.CommandArgument == "0")
            {
                //Single Qty
                Navipro.Infojet.Lib.Item item = new Navipro.Infojet.Lib.Item(infojet, itemNoField.Value);
                Navipro.Infojet.Lib.ProductItem productItem = item.getProductItem(infojet, webPageLine);
                
                TextBox quantityBox = (TextBox)e.Item.FindControl("quantityBox");

                bool allCheckOk = true;
                float quantity = 0;
                try
                {
                    quantity = float.Parse(quantityBox.Text);
                }
                catch (Exception)
                { }

                if (quantity > 0)
                {
                    //Check inventory
                    if (productItem.webItemSetting.availability == 1)
                    {
                        if (productItem.inventory < quantity)
                        {
                            allCheckOk = false;
                            Label errorMessage = (Label)e.Item.FindControl("errorMessage");
                            errorMessage.Text = infojet.translate("NOT ENOUGH IN STOCK");
                            errorMessage.Visible = true;
                        }
                    }

                    if (allCheckOk)
                    {
                        Navipro.Infojet.Lib.WebUserAccount webUserAccount = null;
                        if (infojet.userSession != null) webUserAccount = infojet.userSession.webUserAccount;

                        infojet.cartHandler.addItemToCart(productItem.no, quantity.ToString(), false, "", "", "", "", "", "", webUserAccount);
                        //infojet.redirect(infojet.webPage.getUrl() + "&category=" + Request["category"]+"#"+itemNoField.Value);
                        
                        
                        //Panel doneMessagePanel = (Panel)e.Item.FindControl("doneMessagePanel");
                        //doneMessagePanel.Visible = true;

                    }
                }                
            }

            if (e.CommandArgument == "1")
            {
                //Variants
            	Navipro.Infojet.Lib.WebModel webModel = new Navipro.Infojet.Lib.WebModel(infojet, itemNoField.Value);
            	Navipro.Infojet.Lib.ProductItem productItem = webModel.getProductItem(infojet, webPageLine);

                ArrayList arrayList = new ArrayList();
                bool allCheckOk = true;
                Repeater matrixBodyRepeater = (Repeater)e.Item.FindControl("vertCollection");

                foreach (RepeaterItem cellItem in matrixBodyRepeater.Items)
                {

                    //Repeater matrixCellRepeater = repeaterItem.FindControl("horizCollection") as Repeater;

                    //foreach (RepeaterItem cellItem in matrixCellRepeater.Items)
                    //{
                        HiddenField variantNoField = cellItem.FindControl("itemVariantNo") as HiddenField;
                        HiddenField variantCodeField = cellItem.FindControl("itemVariantCode") as HiddenField;
                        TextBox matrixQuantityBox = cellItem.FindControl("matrixQuantityBox") as TextBox;
                        HiddenField inventoryField = cellItem.FindControl("inventory") as HiddenField;
                        Label inventoryLabel = cellItem.FindControl("inventoryLabel") as Label;

                        Navipro.Infojet.Lib.ItemInfo itemInfo = new Navipro.Infojet.Lib.ItemInfo();
                        itemInfo.no = variantNoField.Value;
                        itemInfo.variantCode = variantCodeField.Value;
                        try
                        {
                            itemInfo.quantity = float.Parse(matrixQuantityBox.Text);
                        }
                        catch (Exception)
                        { }

                        try
                        {
                            itemInfo.inventory = float.Parse(inventoryField.Value);
                        }
                        catch (Exception)
                        { }


                        arrayList.Add(itemInfo);


                        //Check inventory
                        if (productItem.webItemSetting.availability == 1)
                        {
                            if (itemInfo.quantity > 0)
                            {
                                if (itemInfo.inventory < itemInfo.quantity)
                                {
                                    allCheckOk = false;
                                    matrixQuantityBox.ForeColor = System.Drawing.Color.Red;
                                    Label errorMessage = (Label)e.Item.FindControl("errorMessageVariant");
                                    errorMessage.Text = infojet.translate("NOT ENOUGH IN STOCK");
                                    errorMessage.Visible = true;
                                }
                            }
                        }
                    //}
                }

                if (allCheckOk)
                {
                    string itemNo = "";
                    int i = 0;
                    while (i < arrayList.Count)
                    {
                        Navipro.Infojet.Lib.ItemInfo itemInfo = (Navipro.Infojet.Lib.ItemInfo)arrayList[i];

                        if (itemInfo.quantity > 0)
                        {
                            itemNo = itemInfo.no;
                            infojet.cartHandler.addItemToCart(itemInfo.no, itemInfo.quantity.ToString(), false, itemInfo.variantCode, "", "", "", "");
                        }
                        i++;
                    }

                    //infojet.redirect(infojet.webPage.getUrl() + "&category=" + Request["category"]+"#"+itemNoField.Value);
                                    Label errorMessage = (Label)e.Item.FindControl("errorMessageVariant");
			errorMessage.Text = "Done";
			errorMessage.Visible = true;
                    
                }
            }
        }


   
    }

    public void viewChange_Click(object sender, EventArgs e)
    {
        System.Web.UI.WebControls.ImageButton imgButton = (System.Web.UI.WebControls.ImageButton)sender;
        Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();

	
        if (imgButton.CommandArgument == "LIST")
        {
            productListRepeater.Visible = true;
            productGridRepeater.Visible = false;
            productTableRepeater.Visible = false;
            
            listView.ImageUrl = infojet.getProperty("350 LIST BTN SEL");
            gridView.ImageUrl = infojet.getProperty("350 GRID BTN");
            tableView.ImageUrl = infojet.getProperty("350 LIST BTN");
        }
        if (imgButton.CommandArgument == "GRID")
        {
            productListRepeater.Visible = false;
            productGridRepeater.Visible = true;
            productTableRepeater.Visible = false;
            
            listView.ImageUrl = infojet.getProperty("350 LIST BTN");
            gridView.ImageUrl = infojet.getProperty("350 GRID BTN SEL");
            tableView.ImageUrl = infojet.getProperty("350 LIST BTN");
            
        }
        if (imgButton.CommandArgument == "TABLE")
        {
            productListRepeater.Visible = false;
            productGridRepeater.Visible = false;
            productTableRepeater.Visible = true;
            
            listView.ImageUrl = infojet.getProperty("350 LIST BTN");
            gridView.ImageUrl = infojet.getProperty("350 GRID BTN");
            tableView.ImageUrl = infojet.getProperty("350 LIST BTN SEL");

	
	productTableRepeater.DataSource = productItemCollection;
	productTableRepeater.DataBind();
        throw new Exception(productItemCollection.Count.ToString());    
        }
        
    }

    public string formatUnitPrice(string formatedUnitPrice)
    {
   
    
        Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();
        
        string currencyCode = infojet.currencyCode;
        if (infojet.currencyCode == "") currencyCode = infojet.generalLedgerSetup.lcyCode;

        formatedUnitPrice = formatedUnitPrice.Replace(",", ".");       
        //formatedUnitPrice = formatedUnitPrice.Replace(".00", "");
        
        //if (formatedUnitPrice.Contains("."))
        //{
        //    formatedUnitPrice = formatedUnitPrice.Replace(" ", "</span>&nbsp;");
        //    formatedUnitPrice = formatedUnitPrice.Replace(".", ".<span style=\"font-size: 14px; vertical-align: 9px;\">");
        //}

        formatedUnitPrice = formatedUnitPrice.Replace(currencyCode, "<span style=\"font-size: 14px;\">" + currencyCode + "</span><span style=\"font-size: 21px; vertical-align: 11px;\">&nbsp;</span>");
                
        return formatedUnitPrice;
    }



	    
</script>