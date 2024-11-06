<table cellspacing="0" cellpadding="2" width="100%" border="0">
<tr>

	<%

	Navipro.SantaMonica.Common.ShippingCustomerOrganizations shippingCustomerOrganizations = new Navipro.SantaMonica.Common.ShippingCustomerOrganizations();
	Navipro.SantaMonica.Common.Consumers consumers = new Navipro.SantaMonica.Common.Consumers();

	if (shippingCustomerOrganizations.checkType(database, currentShippingCustomer.no, 0))
	{
		%>
		<td class="menuBar"><a href="shippingCustomerLineOrders.aspx" class="menu"><img src="images/button_overview.gif" border="0"></a></td>
		<td class="menuBar" nowrap><a href="shippingCustomerLineOrders.aspx" class="menu">Containerorder</a>&nbsp;&nbsp;&nbsp;</td>
		<%

	}

	if (shippingCustomerOrganizations.checkType(database, currentShippingCustomer.no, 1))
	{
		%>
		<td class="menuBar"><a href="shippingCustomerFactoryOrders.aspx" class="menu"><img src="images/button_overview.gif" border="0"></a></td>
		<td class="menuBar" nowrap><a href="shippingCustomerFactoryOrders.aspx" class="menu">Biomalorder</a>&nbsp;&nbsp;&nbsp;</td>
		<%

	}

	if (consumers.checkShippingCustomer(database, currentShippingCustomer.no))
	{
		%>
		<td class="menuBar"><a href="consumerFactoryOrders.aspx" class="menu"><img src="images/button_overview.gif" border="0"></a></td>
		<td class="menuBar" nowrap><a href="consumerFactoryOrders.aspx" class="menu">Biomalorder</a>&nbsp;&nbsp;&nbsp;</td>
		<td class="menuBar"><a href="consumerInventoryView.aspx" class="menu"><img src="images/button_overview.gif" border="0"></a></td>
		<td class="menuBar" nowrap><a href="consumerInventoryView.aspx" class="menu">Lager</a>&nbsp;&nbsp;&nbsp;</td>
		<td class="menuBar"><a href="map_consumer.aspx" class="menu"><img src="images/button_overview.gif" border="0"></a></td>
		<td class="menuBar" nowrap><a href="map_consumer.aspx" class="menu">Karta</a>&nbsp;&nbsp;&nbsp;</td>
		<%

	}

	%>
	<td width="100%">&nbsp;</td>
</tr>
</table>
