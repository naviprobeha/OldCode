<%@ Page language="c#" Codebehind="shipments_summary.aspx.cs" AutoEventWireup="false" Inherits="Navipro.SantaMonica.WebAdmin.shipments_summary" %>
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
		document.thisform.command.value = "changeShipDate";
		document.thisform.submit();
	
	}
		
	</script>
	
	<body MS_POSITIONING="GridLayout" topmargin="10">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" method="post" runat="server">
			<input type="hidden" name="command" value="">
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame">
				<tr>
					<td height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td class="activityName">Avräkning</td>
				</tr>
				<tr>
					<td class="">Transportör: <%= selectedOrganizationNo %> Datumintervall:&nbsp;<%= startDate.ToString("yyyy-MM-dd") %>&nbsp;-&nbsp;<%= endDate.ToString("yyyy-MM-dd") %><% if (agent != "") Response.Write(",&nbsp;Bil: "+agent); %></td>
				</tr>
				<tr>
					<td>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Följesedelsnr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Körordernr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Datum</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Betalning</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Beskrivning</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right">
									Antal</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Enhet</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right">
									A-pris</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right">
									Totalt</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right">
									Totalt inkl. moms</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">
									Status</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">
									&nbsp;</th>
							</tr>
							<%
								int i = 0;
								int noOfShipments = 0;
								int noOfDeaths = 0;
								decimal totalAmount = 0;
								
								Navipro.SantaMonica.Common.Items items = new Navipro.SantaMonica.Common.Items();
								
								while (i < activeShipments.Tables[0].Rows.Count)
								{
								
									string status = "";
									string icon = "";
									if (activeShipments.Tables[0].Rows[i].ItemArray.GetValue(8).ToString() == "0")
									{
										status = "";
										icon = "ind_white.gif";
									}
									if (activeShipments.Tables[0].Rows[i].ItemArray.GetValue(8).ToString() == "1")
									{
										status = "Köad";
										icon = "ind_white.gif";
									}
									if (activeShipments.Tables[0].Rows[i].ItemArray.GetValue(8).ToString() == "2")
									{
										status = "Skickad";
										icon = "ind_yellow.gif";
									}
									if (activeShipments.Tables[0].Rows[i].ItemArray.GetValue(8).ToString() == "3") 
									{
										status = "Bekräftad";
										icon = "ind_green.gif";
									}

							
									Navipro.SantaMonica.Common.Item stopItem = items.getEntry(database, selectedOrganization.stopItemNo);
								
									
									
									Navipro.SantaMonica.Common.PurchasePrice purchPrice = new Navipro.SantaMonica.Common.PurchasePrice(database, stopItem, 1, selectedOrganization, System.DateTime.Parse(activeShipments.Tables[0].Rows[i].ItemArray.GetValue(6).ToString()));
									
									decimal stopFee = purchPrice.unitCost; 
									decimal stopFeeInclVat = decimal.Multiply(stopFee, new decimal(1.25));
									
									noOfShipments++;
									totalAmount = totalAmount + stopFeeInclVat;
									
									string paymentType = "FAKTURA";
									if (activeShipments.Tables[0].Rows[i].ItemArray.GetValue(10).ToString() == "1") paymentType = "KONTANT";
									if (activeShipments.Tables[0].Rows[i].ItemArray.GetValue(10).ToString() == "2") paymentType = "KORT";
								
									%>
									<tr>
										<td class="jobDescription" valign="top"><%= activeShipments.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %></td>
										<td class="jobDescription" valign="top"><%= activeShipments.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %><%= activeShipments.Tables[0].Rows[i].ItemArray.GetValue(9).ToString() %></td>
										<td class="jobDescription" valign="top"><%= System.DateTime.Parse(activeShipments.Tables[0].Rows[i].ItemArray.GetValue(6).ToString()).ToString("yyyy-MM-dd") %></td>
										<td class="jobDescription" valign="top"><%= paymentType %></td>
										<td class="jobDescription" valign="top">Stopp</td>
										<td class="jobDescription" valign="top" align="right">1</td>
										<td class="jobDescription" valign="top">ST</td>
										<td class="jobDescription" valign="top" align="right"><%= decimal.Round(stopFee, 2)+" kr" %></td>
										<td class="jobDescription" valign="top" align="right"><%= decimal.Round(stopFee, 2)+" kr" %></td>
										<td class="jobDescription" valign="top" align="right"><%= decimal.Round(stopFeeInclVat, 2)+" kr" %></td>
										<td class="jobDescription" valign="top" align="center"><%= status %></td>
										<td class="jobDescription" valign="top" align="center"><img src="images/<%= icon %>" alt="<%= status %>" border="0"></td>
									</tr>
									<%
								
									System.Data.DataSet shipmentLinesDataSet = shipmentLines.getShipmentLinesDataSet(activeShipments.Tables[0].Rows[i].ItemArray.GetValue(1).ToString());
								
							
									int j = 0;
									while (j < shipmentLinesDataSet.Tables[0].Rows.Count)
									{
										Navipro.SantaMonica.Common.Item item = items.getEntry(database, shipmentLinesDataSet.Tables[0].Rows[j].ItemArray.GetValue(2).ToString());
										
										decimal unitPrice = 0;
										
										Navipro.SantaMonica.Common.PurchasePrice purchPrice1 = new Navipro.SantaMonica.Common.PurchasePrice(database, item, 1, selectedOrganization, System.DateTime.Parse(activeShipments.Tables[0].Rows[i].ItemArray.GetValue(6).ToString()));
										unitPrice = purchPrice1.unitCost;										
										
										if (item.putToDeath) 
										{
											noOfDeaths = noOfDeaths + int.Parse(shipmentLinesDataSet.Tables[0].Rows[j].ItemArray.GetValue(4).ToString());
										}
										
										decimal totalPrice = decimal.Multiply(unitPrice, decimal.Parse(shipmentLinesDataSet.Tables[0].Rows[j].ItemArray.GetValue(4).ToString()));
										decimal totalPriceInclVat = decimal.Multiply(totalPrice, new decimal(1.25));
										
										totalAmount = totalAmount + totalPriceInclVat;
								
										%>
										<tr>
											<td class="jobDescription" valign="top">&nbsp;</td>
											<td class="jobDescription" valign="top">&nbsp;</td>
											<td class="jobDescription" valign="top">&nbsp;</td>
											<td class="jobDescription" valign="top">&nbsp;</td>
											<td class="jobDescription" valign="top"><%= shipmentLinesDataSet.Tables[0].Rows[j].ItemArray.GetValue(3).ToString() %></td>
											<td class="jobDescription" valign="top" align="right"><%= shipmentLinesDataSet.Tables[0].Rows[j].ItemArray.GetValue(4).ToString() %></td>
											<td class="jobDescription" valign="top"><%= item.unitOfMeasure %></td>
											<td class="jobDescription" valign="top" align="right"><%= decimal.Round(unitPrice, 2)+" kr" %></td>
											<td class="jobDescription" valign="top" align="right"><%= decimal.Round(totalPrice, 2)+" kr" %></td>
											<td class="jobDescription" valign="top" align="right"><%= decimal.Round(totalPriceInclVat, 2)+" kr" %></td>
											<td class="jobDescription" valign="top" align="center">&nbsp;</td>
											<td class="jobDescription" valign="top" align="center">&nbsp;</td>
										</tr>
										<%


										if (shipmentLinesDataSet.Tables[0].Rows[j].ItemArray.GetValue(5).ToString() != "0")
										{
											Navipro.SantaMonica.Common.Item connectionItem = items.getEntry(database, shipmentLinesDataSet.Tables[0].Rows[j].ItemArray.GetValue(11).ToString());
																				
											unitPrice = 0;

											Navipro.SantaMonica.Common.PurchasePrice purchPrice2 = new Navipro.SantaMonica.Common.PurchasePrice(database, connectionItem, 1, selectedOrganization, System.DateTime.Parse(activeShipments.Tables[0].Rows[i].ItemArray.GetValue(6).ToString()));
											unitPrice = purchPrice2.unitCost;										
											
											if (connectionItem.putToDeath) 
											{
												noOfDeaths = noOfDeaths + int.Parse(shipmentLinesDataSet.Tables[0].Rows[j].ItemArray.GetValue(5).ToString());
											}
											
											totalPrice = decimal.Multiply(unitPrice, decimal.Parse(shipmentLinesDataSet.Tables[0].Rows[j].ItemArray.GetValue(5).ToString()));
											totalPriceInclVat = decimal.Multiply(totalPrice, new decimal(1.25));
											
											totalAmount = totalAmount + totalPriceInclVat;
											
											%>
											<tr>
												<td class="jobDescription" valign="top">&nbsp;</td>
												<td class="jobDescription" valign="top">&nbsp;</td>
												<td class="jobDescription" valign="top">&nbsp;</td>
												<td class="jobDescription" valign="top">&nbsp;</td>
												<td class="jobDescription" valign="top"><%= connectionItem.description %></td>
												<td class="jobDescription" valign="top" align="right"><%= shipmentLinesDataSet.Tables[0].Rows[j].ItemArray.GetValue(5).ToString() %></td>
												<td class="jobDescription" valign="top"><%= connectionItem.unitOfMeasure %></td>
												<td class="jobDescription" valign="top" align="right"><%= decimal.Round(unitPrice, 2)+" kr" %></td>
												<td class="jobDescription" valign="top" align="right"><%= decimal.Round(totalPrice, 2)+" kr" %></td>
												<td class="jobDescription" valign="top" align="right"><%= decimal.Round(totalPriceInclVat, 2)+" kr" %></td>
												<td class="jobDescription" valign="top" align="center">&nbsp;</td>
												<td class="jobDescription" valign="top" align="center">&nbsp;</td>
											</tr>
											<%
										}
											
										j++;
									}
							
								
									i++;
								}
							
							
							%>
							<tr>
								<td class="jobDescription" colspan="12">&nbsp;</td>
							</tr>
							<%
								i = 0;
								decimal totalCashAmount = 0;
								
								while (i < cashShipments.Tables[0].Rows.Count)
								{
									
									
									System.Data.DataSet shipmentLinesDataSet = shipmentLines.getShipmentLinesDataSet(cashShipments.Tables[0].Rows[i].ItemArray.GetValue(1).ToString());
								
								
									int j = 0;
									decimal cashPrice = 0;
									
									while (j < shipmentLinesDataSet.Tables[0].Rows.Count)
									{
																												
										cashPrice = decimal.Add(cashPrice, decimal.Multiply(decimal.Parse(shipmentLinesDataSet.Tables[0].Rows[j].ItemArray.GetValue(10).ToString()), new decimal(1.25)));
										
										j++;
									}															

									totalCashAmount = totalCashAmount + cashPrice;
								
									%>
									<tr>
										<td class="jobDescription" valign="top"><%= cashShipments.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %></td>
										<td class="jobDescription" valign="top"><%= cashShipments.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %><%= cashShipments.Tables[0].Rows[i].ItemArray.GetValue(9).ToString() %></td>
										<td class="jobDescription" valign="top"><%= System.DateTime.Parse(cashShipments.Tables[0].Rows[i].ItemArray.GetValue(6).ToString()).ToString("yyyy-MM-dd") %></td>
										<td class="jobDescription" valign="top">KONTANT</td>
										<td class="jobDescription" valign="top">Kontant betalning av <%= cashShipments.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %> från kund</td>
										<td class="jobDescription" valign="top" align="right">&nbsp;</td>
										<td class="jobDescription" valign="top">&nbsp;</td>
										<td class="jobDescription" valign="top" align="right">&nbsp;</td>
										<td class="jobDescription" valign="top" align="right">&nbsp;</td>
										<td class="jobDescription" valign="top" align="right">-<%= decimal.Round(cashPrice, 2)+" kr" %></td>
										<td class="jobDescription" valign="top" align="center">&nbsp;</td>
										<td class="jobDescription" valign="top" align="center">&nbsp;</td>
									</tr>
									<%
															
								
									i++;
								}
							
							
							%>						
							<tr>
								<td class="jobDescription" colspan="5">&nbsp;</td>
								<td class="jobDescription" align="right">&nbsp;</td>
								<td class="jobDescription" colspan="3">&nbsp;</td>
								<td class="jobDescription" align="right">&nbsp;</td>
								<td class="jobDescription" colspan="2">&nbsp;</td>
							</tr>
							<tr>
								<td class="jobDescription" colspan="5" align="right">Totalt antal stopp</td>
								<td class="jobDescription" align="right"><%= noOfShipments %></td>
								<td class="jobDescription" colspan="3" align="right">Totalt inkört</td>
								<td class="jobDescription" align="right"><%= decimal.Round(totalAmount, 2)+" kr" %></td>
								<td class="jobDescription" colspan="2">&nbsp;</td>
							</tr>
							<tr>
								<td class="jobDescription" colspan="5" align="right">Totalt antal avlivningar</td>
								<td class="jobDescription" align="right"><%= noOfDeaths %></td>
								<td class="jobDescription" colspan="3" align="right">Varav moms</td>
								<td class="jobDescription" align="right"><%= decimal.Round(decimal.Multiply(totalAmount, new decimal(0.2)), 2)+" kr" %></td>
								<td class="jobDescription" colspan="2">&nbsp;</td>
							</tr>
							<tr>
								<td class="jobDescription" colspan="5">&nbsp;</td>
								<td class="jobDescription">&nbsp;</td>
								<td class="jobDescription" colspan="3" align="right">Summa kontanta betalningar</td>
								<td class="jobDescription" align="right">-<%= decimal.Round(totalCashAmount, 2)+" kr" %></td>
								<td class="jobDescription" colspan="2">&nbsp;</td>
							</tr>
							<tr>
								<td class="jobDescription" colspan="5">&nbsp;</td>
								<td class="jobDescription">&nbsp;</td>
								<td class="jobDescription" colspan="3" align="right">Att utbetala</td>
								<td class="jobDescription" align="right"><%= decimal.Round(totalAmount - totalCashAmount, 2)+" kr" %></td>
								<td class="jobDescription" colspan="2">&nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>