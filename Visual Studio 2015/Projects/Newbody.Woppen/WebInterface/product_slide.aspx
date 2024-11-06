<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="product_slide.aspx.cs" Inherits="WebInterface.product_slide" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Product Slide</title>
    <link href="_assets/css/design_intra.css" rel="stylesheet" type="text/css" />
    <link href="_assets/css/visuals.css" rel="stylesheet" type="text/css" />    
</head>

<script type="text/javascript">

var totalLength = <% if (categoryDataSet != null) Response.Write(categoryDataSet.Tables[0].Rows.Count * 50); else Response.Write("100"); %>
var positionX = 0;
var move = false;
var speed = 1;

function checkQuantity()
{
    <%
        if (categoryDataSet != null)
        {
            int i = 0;
            while (i < categoryDataSet.Tables[0].Rows.Count)
            {
                
                Navipro.Newbody.Woppen.Library.ProductGroup productGroup = new Navipro.Newbody.Woppen.Library.ProductGroup(infojet.systemDatabase, categoryDataSet.Tables[0].Rows[i]);
                System.Data.DataSet productDataSet = productGroup.getProducts(infojet.systemDatabase, salesId.itemSelection);

                int j = 0;
                while (j < productDataSet.Tables[0].Rows.Count)
                {
                    Navipro.Infojet.Lib.Item item = new Navipro.Infojet.Lib.Item(infojet.systemDatabase, productDataSet.Tables[0].Rows[j]);
 
                    %>if ((document.thisform.qty<%= item.no %>box.value != "0") && (document.thisform.qty<%= item.no %>box.value != "")) return false;<%
                    j++;
                }
                
                i++;
            }
    
        }
    %>

    return true;

}

function getTotalLength()
{
    return totalLength;
}

function moveLeft()
{
    if (positionX < 0)
    {
        speed = speed + 1;
        if (speed > 30) speed = 30;
        
        //if (speed > 2)
        //{        
            positionX = positionX + speed;
        //}
        //else
        //{
        //    positionX = positionX + 120;
        //}
        if (positionX > 0) positionX = 0;
        
	    document.getElementById('productTable').style.left = positionX + "px";	    
	    
       	    
    	if (move) setTimeout("moveLeft()", 0);	    
	}		
}

function moveRight()
{
    if (positionX + totalLength >= 0)
    {
        speed = speed + 1;
        if (speed > 30) speed = 30;
        
        //if (speed > 2)
        //{        
            positionX = positionX - speed;        
        //}
        //else
        //{
        //    positionX = positionX - 120;
        //}
            
        if (positionX + totalLength < 0) positionX = 0 - totalLength;

        document.getElementById('productTable').style.left = positionX + "px"; 
                      
    	if (move) setTimeout("moveRight()", 0);        
    }	
}

function startMoveRight()
{
    move = true;
    speed = 1;
    setTimeout("moveRight()", 0);
}

function stopMove()
{
    move = false;    
}

function startMoveLeft()
{
    move = true;
    speed = 1;
    setTimeout("moveLeft()", 0);
}

function submitItems()
{
    document.thisform.command.value = "addItems";    
    document.thisform.submit();
}


<%
    if (reloadPage)
    {
        %>
            if (top.location.href != '<%= infojet.webPage.getUrl() %>')
            {
                top.location.href = '<%= infojet.webPage.getUrl() %>';
            }
            else
            {
                parent.refresh();
            }
        <%
    }
 %>
 

</script>

<body>
    <form id="thisform" runat="server">
    <input type="hidden" name="command" value=""/>
       
    <table id="productTable" style="position: absolute; <%= backgroundClass %>">
    <tr>
    <%
        Navipro.Newbody.Woppen.Library.ItemCategory itemCategory = null;
        
        int i = 0;
        int productCount = 0;
        string lastCategory = "";
        if (categoryDataSet != null)
        {
            while (i < categoryDataSet.Tables[0].Rows.Count)
            {
                
                Navipro.Newbody.Woppen.Library.ProductGroup productGroup = new Navipro.Newbody.Woppen.Library.ProductGroup(infojet.systemDatabase, categoryDataSet.Tables[0].Rows[i]);

                if (lastCategory != productGroup.code)
                {
                    productCount++;
                    if (itemCategory == null) itemCategory = productGroup.getItemCategory();
                    if (itemCategory.code != productGroup.itemCategoryCode) itemCategory = productGroup.getItemCategory();

                    System.Data.DataSet productDataSet = productGroup.getProducts(infojet.systemDatabase, salesId.itemSelection);
                    int noOfProducts = productDataSet.Tables[0].Rows.Count;

                    string imageUrl = "_assets/img/no_image_small.jpg";
                    Navipro.Infojet.Lib.WebItemImages webItemImages = new Navipro.Infojet.Lib.WebItemImages(infojet.systemDatabase);
                    Navipro.Infojet.Lib.WebItemImage webItemImage = webItemImages.getItemProductImage(productDataSet.Tables[0].Rows[0].ItemArray.GetValue(0).ToString(), infojet.webSite.code);
                    if (webItemImage != null)
                    {
                        imageUrl = webItemImage.image.getUrl(140, 140);
                    }
                           
                    %><td style="height: 90px;">
                        <table>
                            <tr>
                                <td style="text-align: center"><img src="<%= imageUrl %>" alt="" /></td>
                            </tr>
                            <tr>
                                <td><img src="_assets/img/box_width.gif" alt="" /></td>
                            </tr>                
                            <tr>
                                <td style="border: 1px solid #cecfc6; vertical-align: top; background: url(_assets/img/box_bg.jpg)">
                                    <table style="width: 100%;" cellspacing="2" cellpadding="2">
                                        <tr>
                                            <td style="font-size: 11px;" colspan="<%= noOfProducts %>"><%= productGroup.description.ToUpper()%></td>
                                        </tr>                
                                        <tr>
                                            <% 

                int j = 0;
                while (j < productDataSet.Tables[0].Rows.Count)
                {
                    string sizeCode = productDataSet.Tables[0].Rows[j].ItemArray.GetValue(9).ToString();
                    string itemNo = productDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString();

                                                    %><td align="center" style="font-size: 10px;"><%= itemNo%><br /><%= sizeCode%></td><%
                                                    
                j++;
                }
            

                                                %></tr><tr><%
                                                        
                j = 0;
                while (j < productDataSet.Tables[0].Rows.Count)
                {
                    Navipro.Infojet.Lib.Item item = new Navipro.Infojet.Lib.Item(infojet.systemDatabase, productDataSet.Tables[0].Rows[j]);

                                                                                    
                                                    %><td align="center" valign="top" style="height: 25px;"><input type="text" size="3" maxlength="3" name="qty<%= item.no %>box" value="0" class="Textfield" onclick="document.thisform.qty<%= item.no %>box.select();"/></td><%


                                                j++;
                                                }
                                            %>   
                                        </tr>                
                                    </table>                                   
                                </td>
                            </tr>
                        </table>
                    
                    </td><%
                }
                lastCategory = productGroup.code;
                
            i++;
            }
        }                                                                                                              
    %>
        
    </tr>
    </table>
  
    </form>
</body>
</html>
