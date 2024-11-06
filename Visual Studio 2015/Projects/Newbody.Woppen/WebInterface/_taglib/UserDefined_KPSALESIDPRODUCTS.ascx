<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserDefined_KPSALESIDPRODUCTS.ascx.cs" Inherits="WebInterface._taglib.UserDefined_KPSALESIDPRODUCTS" %>
<%@ Register TagPrefix="Infojet" TagName="Translate" src="Translate.ascx" %>

<script type="text/javascript">

var positionX = 0;
var move = false;
var speed = 1;

function goBack()
{
    leave = true;
    
    if (!window.frames.productIframe.checkQuantity())
    {
        if (!confirm("<%= infojet.translate("UNHANDLED QTYS") %>"))
        {
            leave = false;
        }
    }
    
    if (leave)
    {
        document.location.href = "<%= prevWebPageUrl %>";
    }
    
}

function refresh()
{
    //document.location.href = "<%= infojet.webPage.getUrl()+"&salesPersonWebUserAccountNo="+Request["salesPersonWebUserAccountNo"] %>";
    document.pageForm.action = "<%= infojet.webPage.getUrl() %>";
    document.pageForm.submit();    
}

function moveLeft()
{
    totalLength = window.frames.productIframe.getTotalLength();
    
    if (positionX > 0)
    {
        speed = speed + 1;
        if (speed > 30) speed = 30;
        
        positionX = positionX - speed;
        if (positionX < 0) positionX = 0;
        
	    window.frames.productIframe.scrollTo(positionX, 0);
       	    
    	if (move) setTimeout("moveLeft()", 0);	    
	}		
}

function moveRight()
{
    totalLength = window.frames.productIframe.getTotalLength();

    if (positionX + totalLength >= 0)
    {
        speed = speed + 1;
        if (speed > 30) speed = 30;
        
        positionX = positionX + speed;        
            
        if (positionX > totalLength) positionX = totalLength ;

	    window.frames.productIframe.scrollTo(positionX, 0);
                      
    	if (move) setTimeout("moveRight()", 0);        
    }	
}

function startMoveRight()
{
    //window.frames.productIframe.startMoveRight();
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
    <% if (salesPersonWebUserAccount.companyRole != "X")
    {
        %>window.frames.productIframe.submitItems();<%
    }
    else
    {
        %>alert("<%= infojet.translate("UNREGED USER") %>");<%
    }
    %>
}

function deleteCartLine(entryNo)
{
    document.pageForm.cart_command.value = "deleteCartLine";
    document.pageForm.cart_entryNo.value = entryNo;
    document.pageForm.submit();    
}

function updateCartLine(entryNo)
{
    document.pageForm.cart_command.value = "updateCartLine";
    document.pageForm.cart_entryNo.value = entryNo;
    document.pageForm.submit();    

}

function releaseCart(entryNo)
{
    if (confirm("<%= infojet.translate("RELEASE QUESTION KP") %>"))
    {
        document.pageForm.cart_command.value = "releaseCart";
        document.pageForm.submit();    
    }
}

function openCart(entryNo)
{
    if (confirm("<%= infojet.translate("OPEN QUESTION KP") %>"))
    {
        document.pageForm.cart_command.value = "openCart";
        document.pageForm.submit();    
    }
}


function changeCartLine(entryNo)
{
    <%
        int z = 0;
        while (z < cartDataSet.Tables[0].Rows.Count)
        {
            Navipro.Infojet.Lib.WebCartLine webCartLine = new Navipro.Infojet.Lib.WebCartLine(infojet, cartDataSet.Tables[0].Rows[z]);
                       
            %>
            if (entryNo == <%= webCartLine.entryNo %>)
            {
                document.getElementById('cartEntry_'+entryNo).style.visibility = "hidden";
                document.getElementById('changeCartEntry_'+entryNo).style.visibility = "visible";
                document.getElementById('cartButtons_'+entryNo).innerHTML = "<a style='font-size: 10px' href='javascript:updateCartLine("+entryNo+")'><%= infojet.translate("SAVE") %></a>";
            }
            else
            {
                document.getElementById('cartEntry_<%= webCartLine.entryNo %>').style.visibility = "visible";
                document.getElementById('changeCartEntry_<%= webCartLine.entryNo %>').style.visibility = "hidden";
                document.getElementById('cartButtons_<%= webCartLine.entryNo %>').innerHTML = "<a style='font-size: 10px' href='javascript:changeCartLine(<%= webCartLine.entryNo %>)'><%= infojet.translate("CHANGE") %></a> | <a style='font-size: 10px' href='javascript:deleteCartLine(<%= webCartLine.entryNo %>)'><%= infojet.translate("REMOVE") %></a>";
            }
            <%
            
            z++;
        }
    %>

}

</script>

<div style="position: absolute; width: 100%; left: 0; top: 250px;">
    <div style="margin-left: auto; margin-right: auto; width: 490px;">
        <div id="leftArrow" style="float: left;"><a href="#" onmousedown="startMoveLeft()" onmouseup="stopMove()" onmouseout="stopMove()"><img src="_assets/img/arrow_left.gif" alt="<%= infojet.translate("MOVE LEFT") %>" /></a></div>
        <div id="rightArrow" style="float: right;"><a href="#" onmousedown="startMoveRight()" onmouseup="stopMove()" onmouseout="stopMove()"><img src="_assets/img/arrow_right.gif" alt="<%= infojet.translate("MOVE RIGHT") %>" /></a></div>
    </div>
</div>


   
<iframe id="productIframe" name="productIframe" frameborder="0" width="100%" height="265" src="product_slide.aspx?webPage=<%= webPageLine.webPageCode %>&salesId=<%= Request["salesId"] %>&salesPersonWebUserAccountNo=<%= Request["salesPersonWebUserAccountNo"] %>" scrolling="auto"></iframe>

<input type="hidden" name="cart_command" value=""/>
<input type="hidden" name="cart_entryNo" value=""/>
<input type="hidden" name="salesPersonWebUserAccountNo" value="<%= Request["salesPersonWebUserAccountNo"] %>"/>
<input type="hidden" name="salesId" value="<%= Request["salesId"] %>"/>

<div style="width: 100%; margin: 3px">
    <div style="float:left;"><% if (!cartIsReleased)
                                                                             { %><a href="javascript:releaseCart()"><img src="_assets/img/<%= infojet.translate("IMG RELEASE BTN") %>" alt="<%= infojet.translate("RELEASE CART KP") %>" /></a><% }
                                                                             else
                                                                             { %><a href="javascript:openCart()"><img src="_assets/img/<%= infojet.translate("IMG OPEN BTN") %>" alt="<%= infojet.translate("OPEN CART KP") %>" /></a><% } %></div>
    <div id="Div1" style="float: right;"><a href="javascript:submitItems()"><img src="_assets/img/<%= infojet.translate("IMG UPDATE CART BTN") %>" alt="<%= infojet.translate("UPDATE CART") %>" /></a>&nbsp;</div>
</div>

<br /><br />

<table cellspacing="2" width="100%" class="salesPersonCart">
<tr>
    <th style="width: 100px;"><Infojet:Translate runat="server" ID="itemNo" code="ITEM NO" /></th>
    <th><Infojet:Translate runat="server" ID="description" code="PRODUCT NAME" /></th>
    <th style="width: 80px; text-align: center;"><Infojet:Translate runat="server" ID="sizeCode" code="SIZE" /></th>    
    <th style="width: 60px; text-align: right;"><Infojet:Translate runat="server" ID="quantity" code="QUANTITY" /></th>        
    <th style="width: 100px; text-align: center;">&nbsp;</th>            
</tr>
<%
    float totalPackages = 0;
    int i = 0;
    while (i < cartDataSet.Tables[0].Rows.Count)
    {
        Navipro.Infojet.Lib.WebCartLine webCartLine = new Navipro.Infojet.Lib.WebCartLine(infojet, cartDataSet.Tables[0].Rows[i]);
        totalPackages = totalPackages + webCartLine.quantity;
       
        
        %>
        <tr>
            <td><%= webCartLine.itemNo %></td>
            <td><%= webCartLine.getItem().description %></td>
            <td align="center"><%= webCartLine.extra1 %></td>
            <td align="right"><div id="cartEntry_<%= webCartLine.entryNo %>" style="float: right; text-align: right;"><%= webCartLine.quantity %></div><div id="changeCartEntry_<%= webCartLine.entryNo %>" style="visibility: hidden; float: right;"><input type="text" size="4" name="cartQuantity_<%= webCartLine.entryNo %>" value="<%= webCartLine.quantity %>" class="Textfield" /></div></td>
            <td align="center" style="color: #ef1c22;"><div id="cartButtons_<%= webCartLine.entryNo %>"><a style="font-size: 10px;" href="javascript:changeCartLine(<%= webCartLine.entryNo %>)"><Infojet:Translate runat="server" ID="Translate2" code="CHANGE" /></a> | <a style="font-size: 10px;" href="javascript:deleteCartLine(<%= webCartLine.entryNo %>)"><Infojet:Translate runat="server" ID="Translate3" code="REMOVE" /></a></div></td>
        </tr>      
        
        <%

        i++;
    }

    if (i == 0)
    {
        %>
        <tr>
            <td colspan="5"><Infojet:Translate runat="server" id="cartEmpty" code="CART EMPTY" /></td>
        </tr>
        <%
    }
%>
<tr>
    <td colspan="3" align="right"><b><Infojet:Translate runat="server" id="Translate1" code="TOTAL PACKAGES" />:</b></td>
    <td align="right"><b><%= totalPackages.ToString() %></b></td>
    <td>&nbsp;</td>    
</tr>

</table>
<div style="float:left; width: 100%;">
<div style="float:left; text-align: left; padding-top: 5px; width: 50%;"><% if (prevWebPageUrl != "") { %><a href="javascript:goBack()"><img src="_assets/img/<%= infojet.translate("IMG BACK BTN") %>" alt="<%= infojet.translate("BACK") %>" /></a><% } %></div>
<div style="float:left; text-align: right; padding-top: 5px; width: 50%; color: #ef1c22;"><% if (!cartIsReleased)
                                                                             { %><a href="javascript:releaseCart()"><img src="_assets/img/<%= infojet.translate("IMG RELEASE BTN") %>" alt="<%= infojet.translate("RELEASE CART KP") %>" /></a><% }
                                                                             else
                                                                             { %><a href="javascript:openCart()"><img src="_assets/img/<%= infojet.translate("IMG OPEN BTN") %>" alt="<%= infojet.translate("OPEN CART KP") %>" /></a><% } %></div>
</div> 