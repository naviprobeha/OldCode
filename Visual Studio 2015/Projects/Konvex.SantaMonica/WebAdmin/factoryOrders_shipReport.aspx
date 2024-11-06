<%@ Page language="c#" Codebehind="factoryOrders_shipReport.aspx.cs" AutoEventWireup="false" Inherits="WebAdmin.factoryOrders_shipReport" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SmartShipping</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link rel="stylesheet" href="css/webstyle.css">
	</HEAD>
	
	<script>
	
	function changeShipDate()
	{
	
	}

	function setDate()
	{
		document.thisform.command.value = "changeShipDate";
		document.thisform.submit();
	
	}

	function addInvoice()
	{
		document.thisform.command.value = "addInvoice";
		document.thisform.submit();
	
	}

    function deleteInvoice(entryNo)
    {
		if (confirm("Fakturan kommer att raderas. Är du säker?"))
		{
			document.thisform.command.value = "deleteInvoice";
			document.thisform.argument.value = entryNo;
			document.thisform.submit();
		}
    }
    
	function gotoToday()
	{
		document.thisform.workDateYear.value = "<%= DateTime.Now.Year %>";
		document.thisform.workDateMonth.value = "<%= DateTime.Now.Month %>";
		document.thisform.workDateDay.value = "<%= DateTime.Now.ToString("dd") %>";
		document.thisform.noOfDaysBack.value = "0";
	
		document.thisform.command.value = "changeShipDate";
		document.thisform.submit();
	
	}
	
	</script>

	<body MS_POSITIONING="GridLayout" topmargin="10" onload="setTimeout('document.thisform.submit()', 60000)">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" method="post" runat="server">
		<input type="hidden" name="command" value="">
		<input type="hidden" name="argument" value="">
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame">
				<tr>
					<td height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td class="activityName">Leveranser värmeverk</td>
				</tr>
				<tr>
					<td class="" width="95%">Datum:&nbsp;<% WebAdmin.HTMLHelper.createDatePicker("fromDate", fromDate); %> - <% WebAdmin.HTMLHelper.createDatePicker("toDate", toDate); %>&nbsp;<input type="button" value="Sök" onclick="setDate()" class="Button"></td>
				</tr>
				<tr>
					<td>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" width="200">
									&nbsp;</th>
								<%
									int i = 0;
									while (i < consumerDataSet.Tables[0].Rows.Count)
									{
										%><th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right" colspan="2" nowrap>&nbsp;<%= consumerDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %>&nbsp;</th><%
										
										i++;
									}
								%>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right" width="30%">
									Totalt</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right" nowrap>
									Förväntat utfall</th>
							</tr>
							<%
								string bgStyle = "";
						
								int totalLines = 0;
								
								double totalShippingCustomerOutFall = 0;
								
								int j = 0;
								while (j < shippingCustomerDataSet.Tables[0].Rows.Count)
								{
									Navipro.SantaMonica.Common.ShippingCustomer shippingCustomer = new Navipro.SantaMonica.Common.ShippingCustomer(shippingCustomerDataSet.Tables[0].Rows[j]);

									bgStyle = "";
									
								
									if ((totalLines % 2) > 0)
									{
										bgStyle = "background-color: #e0e0e0;";
									}
																
									%>
									<tr>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>" nowrap><%= shippingCustomer.name %></td>
										
										<%
											double shippingCustomerTotal = factoryOrders.getFactoryOrderQuantity(database, fromDate, toDate, 1, shippingCustomer.no);
											double shippingCustomerOutFall = shippingCustomerTotal / dayDuration * toDate.Subtract(fromDate).Days;
											totalShippingCustomerOutFall = totalShippingCustomerOutFall + shippingCustomerOutFall;
											
											i = 0;
											while (i < consumerDataSet.Tables[0].Rows.Count)
											{
												double consumerValue = factoryOrders.getFactoryOrderQuantity(database, fromDate, toDate, consumerDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString(), 1, shippingCustomer.no);
												double consumerPercent = 0;
												if (shippingCustomerTotal > 0) consumerPercent = consumerValue / shippingCustomerTotal * 100;
											
												%><td class="jobDescription" valign="top" style="<%= bgStyle %>" align="right">&nbsp;<%= Math.Round(consumerValue, 2) %></td>
												<td class="jobDescription" valign="top" style="<%= bgStyle %>" align="right">&nbsp;<%= Math.Round(consumerPercent, 1) %> %</td><%
												
												i++;
											}
										%>
										
										<td class="jobDescription" valign="top" align="right" style="<%= bgStyle %>">&nbsp;<%= Math.Round(shippingCustomerTotal, 2) %></td>
										<td class="jobDescription" valign="top" align="right" style="<%= bgStyle %>">&nbsp;<%= Math.Round(shippingCustomerOutFall, 2) %></td>
									</tr>
									<%
								
								
									j++;
									totalLines++;
								}
							
								bgStyle = "";								
						
								if ((totalLines % 2) > 0)
								{
									bgStyle = "background-color: #e0e0e0;";
								}
								totalLines++;							
							
							%>
							<tr>
								<td class="jobDescription" valign="top" style="<%= bgStyle %>"><b>Totalt slakterier</b></td>
								
								<%
									double totalShippingCustomer = factoryOrders.getFactoryOrderQuantity(database, fromDate, toDate, 1);
								
									i = 0;
									while (i < consumerDataSet.Tables[0].Rows.Count)
									{
										%><td class="jobDescription" valign="top" style="<%= bgStyle %>">&nbsp;</td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>">&nbsp;</td><%
										
										i++;
									}
								%>
								
								<td class="jobDescription" valign="top" align="right" style="<%= bgStyle %>" align="right">&nbsp;<b><%= Math.Round(totalShippingCustomer, 2) %></b></td>
								<td class="jobDescription" valign="top" align="right" style="<%= bgStyle %>" align="right">&nbsp;<b><%= Math.Round(totalShippingCustomerOutFall, 2) %></b></td>
							</tr>
							<%
								bgStyle = "";								
						
								if ((totalLines % 2) > 0)
								{
									bgStyle = "background-color: #e0e0e0;";
								}
								totalLines++;							
							%>							
							<tr>
								<td class="jobDescription" valign="top" style="<%= bgStyle %>" colspan="<%= i+i+3 %>">&nbsp;</td>
							</tr>							
							<%
								double totalFactoryOutFall = 0;
							
								j = 0;
								while (j < factoryDataSet.Tables[0].Rows.Count)
								{
									Navipro.SantaMonica.Common.Factory factory = new Navipro.SantaMonica.Common.Factory(factoryDataSet.Tables[0].Rows[j]);
									
									bgStyle = "";								
							
									if ((totalLines % 2) > 0)
									{
										bgStyle = "background-color: #e0e0e0;";
									}
										
									double factoryTotal = factoryOrders.getFactoryOrderQuantity(database, fromDate, toDate, 0, factory.no);
									double factoryOutFall = factoryTotal / dayDuration * toDate.Subtract(fromDate).Days;
									totalFactoryOutFall = totalFactoryOutFall + factoryOutFall;
																
									%>
									<tr>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><%= factory.name %></td>
										
										<%
											i = 0;
											while (i < consumerDataSet.Tables[0].Rows.Count)
											{
												double consumerValue = factoryOrders.getFactoryOrderQuantity(database, fromDate, toDate, consumerDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString(), 0, factory.no);
												double consumerPercent = 0;
												if (factoryTotal > 0) consumerPercent = consumerValue / factoryTotal * 100;
											
											
												%><td class="jobDescription" valign="top" style="<%= bgStyle %>" align="right">&nbsp;<%= Math.Round(consumerValue, 2) %></td>
												<td class="jobDescription" valign="top" style="<%= bgStyle %>" align="right">&nbsp;<%= Math.Round(consumerPercent, 1) %> %</td><%
												
												i++;
											}
										%>
										
										<td class="jobDescription" valign="top" align="right" style="<%= bgStyle %>" align="right">&nbsp;<%= Math.Round(factoryTotal, 2) %></td>
										<td class="jobDescription" valign="top" align="right" style="<%= bgStyle %>" align="right">&nbsp;<%= Math.Round(factoryOutFall, 2) %></td>
									</tr>
									<%
								
								
									totalLines++;							
									j++;
								}
													

								bgStyle = "";								
						
								if ((totalLines % 2) > 0)
								{
									bgStyle = "background-color: #e0e0e0;";
								}

								totalLines++;							
							%>
							
							<tr>
								<td class="jobDescription" valign="top" style="<%= bgStyle %>"><b>Totalt fabriker</b></td>
								
								<%
									double totalFactory = factoryOrders.getFactoryOrderQuantity(database, fromDate, toDate, 0);
								
									i = 0;
									while (i < consumerDataSet.Tables[0].Rows.Count)
									{
										%><td class="jobDescription" valign="top" style="<%= bgStyle %>">&nbsp;</td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>">&nbsp;</td><%
										
										i++;
									}
								%>
								
								<td class="jobDescription" valign="top" align="right" style="<%= bgStyle %>">&nbsp;<b><%= Math.Round(totalFactory, 2) %></b></td>
								<td class="jobDescription" valign="top" align="right" style="<%= bgStyle %>">&nbsp;<b><%= Math.Round(totalFactoryOutFall, 2) %></b></td>
							</tr>
							<%
								bgStyle = "";								
						
								if ((totalLines % 2) > 0)
								{
									bgStyle = "background-color: #e0e0e0;";
								}
								
								totalLines++;
							%>							
							<tr>
								<td class="jobDescription" valign="top" style="<%= bgStyle %>" colspan="<%= i+i+3 %>">&nbsp;</td>
							</tr>							
							<%
								bgStyle = "";								
						
								if ((totalLines % 2) > 0)
								{
									bgStyle = "background-color: #e0e0e0;";
								}
								
								totalLines++;
							%>							
							<tr>
								<td class="jobDescription" valign="top" style="<%= bgStyle %>"><b>Totalt</b></td>
								<%
									i = 0;
									while (i < consumerDataSet.Tables[0].Rows.Count)
									{
										double consumerValue = factoryOrders.getFactoryOrderQuantity(database, fromDate, toDate, consumerDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString());
										double consumerPercent = 0;
										if ((totalShippingCustomer + totalFactory) > 0) consumerPercent = consumerValue / (totalShippingCustomer + totalFactory) * 100;
									
										%><td class="jobDescription" valign="top" style="<%= bgStyle %>" align="right">&nbsp;<b><%= Math.Round(consumerValue, 2) %></b></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>" align="right">&nbsp;<b><%= Math.Round(consumerPercent, 2) %> %</b></td><%
										
										i++;
									}
								%>
								
								<td class="jobDescription" valign="top" align="right" style="<%= bgStyle %>">&nbsp;<b><%= Math.Round(totalShippingCustomer + totalFactory, 2) %></b></td>
								<td class="jobDescription" valign="top" align="right" style="<%= bgStyle %>">&nbsp;<b><%= Math.Round(totalShippingCustomerOutFall + totalFactoryOutFall, 2) %></b></td>
								
							</tr>
							<tr>
								<td class="jobDescription" valign="top" style="<%= bgStyle %>">Fakturerat</td>
								<%
									double invoiceTotal = 0;
									i = 0;
									while (i < consumerDataSet.Tables[0].Rows.Count)
									{
										Navipro.SantaMonica.Common.FactoryOrderLedgerEntries factoryOrderLedgerEntries = new Navipro.SantaMonica.Common.FactoryOrderLedgerEntries();
										double invoiceValue = factoryOrderLedgerEntries.getTotals(database, fromDate, toDate, consumerDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString());
										invoiceTotal = invoiceTotal + invoiceValue;
									
										%><td class="jobDescription" valign="top" style="<%= bgStyle %>" align="right">&nbsp;<%= Math.Round(invoiceValue, 2) %></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>" align="right">&nbsp;</td><%
										
										i++;
									}
								%>
								
								<td class="jobDescription" valign="top" align="right" style="<%= bgStyle %>">&nbsp;<%= Math.Round(invoiceTotal, 2) %></td>
								<td class="jobDescription" valign="top" align="right" style="<%= bgStyle %>">&nbsp;</td>
								
							</tr>
						</table>
						<br/>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">Värmeverk</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">Fakturanr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">Period slutdatum</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right">Antal</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right">Belopp</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">Makulera</th>
							</tr>
							<%
														
								j = 0;
								while (j < factoryOrderLedgerDataSet.Tables[0].Rows.Count)
								{
									Navipro.SantaMonica.Common.FactoryOrderLedgerEntry factoryOrderLedgerEntry = new Navipro.SantaMonica.Common.FactoryOrderLedgerEntry(factoryOrderLedgerDataSet.Tables[0].Rows[j]);
						
									
									%>
									<tr>
										<td class="jobDescription" valign="top"><%= factoryOrderLedgerDataSet.Tables[0].Rows[j].ItemArray.GetValue(6).ToString() %></td>
										<td class="jobDescription" valign="top"><%= factoryOrderLedgerEntry.documentNo %></td>
										<td class="jobDescription" valign="top"><%= factoryOrderLedgerEntry.invoiceDate.ToString("yyyy-MM-dd") %></td>
										<td class="jobDescription" valign="top" align="right"><%= factoryOrderLedgerEntry.quantity %></td>
										<td class="jobDescription" valign="top" align="right"><%= factoryOrderLedgerEntry.amount %> kr</td>
										<td class="jobDescription" valign="top" align="center"><a href="javascript:deleteInvoice('<%= factoryOrderLedgerEntry.entryNo %>')"><img src="images/button_delete.gif" border="0" width="12" height="13"></a></td>
									</tr>
									<%
								
									j++;
								}

								if (j == 0)
								{
									%><tr>
										<td class="jobDescription" colspan="6">Inga fakturor registrerade.</td>
									</tr><%
								}
							
							%>
							<tr>
									<td class="jobDescription" valign="top"><select name="consumerNo" class="DropDown">
										<%
											int z = 0;
											while (z < consumerDataSet.Tables[0].Rows.Count)
											{
												%><option value="<%= consumerDataSet.Tables[0].Rows[z].ItemArray.GetValue(0).ToString() %>"><%= consumerDataSet.Tables[0].Rows[z].ItemArray.GetValue(1).ToString() %></option><%
												
												z++;
											}
										%>
									</select></td>
									<td class="jobDescription" valign="top"><input type="text" name="documentNo" size="30" maxlength="20" class="Textfield"></td>
									<td class="jobDescription" valign="top"><% WebAdmin.HTMLHelper.createDatePicker("invoiceDate", toDate); %></td>
									<td class="jobDescription" valign="top" align="right"><input type="text" name="quantity" size="6" maxlength="10" class="Textfield"></td>
									<td class="jobDescription" valign="top" align="right"><input type="text" name="amount" size="6" maxlength="10" class="Textfield"> kr</td>
									<td class="jobDescription" valign="top" align="center"><input type="button" value="Lägg till" onclick="addInvoice()" class="Button"></td>
							</tr>
						</table>												
					</td>
				</tr>
				<tr>
					<td align="right">&nbsp;</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
