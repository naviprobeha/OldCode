<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductTree_LIST.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.ProductTree_LIST" %>

<%
    if (webPageLine.code == "")
    {
        if (Request["category"] != null) webPageLine.code = Request["category"];
    }
    
    Navipro.Infojet.Lib.WebItemCategory webItemCategory = new Navipro.Infojet.Lib.WebItemCategory(infojet, webPageLine.code);

    productTree.DataSource = webItemCategory.getProductCategoryTree();
    productTree.DataBind();
 %>


<div class="userControl">
<div class="productTree" style="width: 800px; float: left;">

<asp:repeater id="productTree" runat="server">

</asp:repeater>

<%

	Navipro.Infojet.Lib.NavigationItemCollection navigationItemCollection = (Navipro.Infojet.Lib.NavigationItemCollection)productTree.DataSource;

	int i = 0;
	while(i < navigationItemCollection.Count)
	{
		Navipro.Infojet.Lib.NavigationItem navigationItem = navigationItemCollection[i];
	
		if (navigationItem.webImage != null)
		{
    			%><div style="float: left; width: 200px; height: 150px; text-align: center;"><a href="<%= navigationItem.link %>"><img src="<%= navigationItem.webImage.getUrl(100,100) %>" alt=""/></a><br/><a href="<%= navigationItem.link %>"><%= navigationItem.text %></a><br/>&nbsp;</div><%
    		}
    		else
    		{
    			%><div style="float: left; width: 200px; height: 150px; text-align: center;"><a href="<%= navigationItem.link %>"><%= navigationItem.text %></a><br/>&nbsp;</div><%
    		}
		
	
		i++;
	}
%>

</table>


</div>
</div>
