<%@ Page language="c#" Codebehind="shipments_print.aspx.cs" AutoEventWireup="false" Inherits="Navipro.SantaMonica.WebAdmin.shipments_print" %>
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
	<body MS_POSITIONING="GridLayout" topmargin="10" onload="window.print();">
		<form id="thisform" method="post" runat="server">
			<input type="hidden" name="command">
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame">
				<tr>
					<td class="activityName">Följesedel
						<%= currentShipmentHeader.no %>
					</td>
				</tr>
				<tr>
					<td>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Följesedelsnr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentShipmentHeader.no %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Hämtdatum</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentShipmentHeader.shipDate.ToString("yyyy-MM-dd") %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" colspan="2" height="15" valign="top">Kundnr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentShipmentHeader.customerNo %></b>&nbsp;</td>
											<td class="interaction" width="90%" nowrap><a href="customers_view.aspx?customerNo=<%= currentShipmentHeader.customerNo %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Namn</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentShipmentHeader.customerName %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Namn (Hämtning)</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentShipmentHeader.shipName %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Namn (Transportör)</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= shipmentOrganization.name %>&nbsp;</b></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Adress</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentShipmentHeader.address %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Adress (Hämtning)</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentShipmentHeader.shipAddress %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Adress (Transportör)</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= shipmentOrganization.address %>&nbsp;</b></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Adress 2</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentShipmentHeader.address2 %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Adress 2 (Hämtning)</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentShipmentHeader.shipAddress2 %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Adress 2 (Transportör)</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= shipmentOrganization.address2 %>&nbsp;</b></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Postadress</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentShipmentHeader.postCode +" "+ currentShipmentHeader.city %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Postadress (Hämtning)</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentShipmentHeader.shipPostCode +" "+ currentShipmentHeader.shipCity %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Postadress (Transportör)</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= shipmentOrganization.postCode +" "+ shipmentOrganization.city %>&nbsp;</b></td>
										</tr>
									</table>
								</td>

							</tr>
							<tr>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Telefonnr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentShipmentHeader.phoneNo %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Mobiltelefonnr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentShipmentHeader.cellPhoneNo %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Telefonnr (Transportör)</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= shipmentOrganization.phoneNo %>&nbsp;</b></td>
										</tr>
									</table>
								</td>

							</tr>
							<tr>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Registrerad av</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentShipmentHeader.userName %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Referensinfo</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentShipmentHeader.reference %></b></td>
										</tr>
									</table>
								</td>
								<%
									string shipOrderNo = "";
									string shipOrderLink = "";
									
									if (currentShipmentHeader.shipOrderEntryNo != 0) 
									{
										shipOrderNo = currentShipmentHeader.organizationNo+currentShipmentHeader.shipOrderEntryNo;
										shipOrderLink = "<a href=\"orders_view.aspx?shipOrderNo="+currentShipmentHeader.shipOrderEntryNo+"\"><img src=\"images/button_assist.gif\" border=\"0\"></a>";
									}
								%>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top" colspan="2">Körorder</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= shipOrderNo %>&nbsp;</b></td>
											<td class="interaction" height="20" width="90%"><%= shipOrderLink %></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Mejerikod</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentShipmentHeader.dairyCode %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Mejerinr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentShipmentHeader.reference %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Containernr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentShipmentHeader.containerNo %></b></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Produktionsplatsnr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentShipmentHeader.productionSite %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Betalninssätt</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentShipmentHeader.paymentText %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Kontantnotanr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentShipmentHeader.invoiceNo %></b></td>
										</tr>
									</table>
								</td>							
							</tr>							
						</table>
						<br>
						Samtliga nedanstående animaliska biprodukter kat 1; endast för bortskaffande.
						<br>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Artikelnr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Beskrivning</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right">
									Antal/Vikt</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right">
									Antal avlivningar</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right">
									Belopp</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right">
									Avlivningsbelopp</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right">
									Totalbelopp</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right">
									Antal för provtagning</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">
									Dubbel taxa</th>
							</tr>
							<%
								int i = 0;
								while (i < shipmentLinesDataSet.Tables[0].Rows.Count)
								{	
								
									string amount = decimal.Round(decimal.Parse(shipmentLinesDataSet.Tables[0].Rows[i].ItemArray.GetValue(7).ToString()), 2)+" kr";
									string connectionAmount = decimal.Round(decimal.Parse(shipmentLinesDataSet.Tables[0].Rows[i].ItemArray.GetValue(9).ToString()), 2)+" kr";
									string totalAmount = decimal.Round(decimal.Parse(shipmentLinesDataSet.Tables[0].Rows[i].ItemArray.GetValue(10).ToString()), 2)+" kr";
									
									
									%>
							<tr>
								<td class="jobDescription" valign="top"><%= shipmentLinesDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString() %></td>
								<td class="jobDescription" valign="top"><b><%= shipmentLinesDataSet.Tables[0].Rows[i].ItemArray.GetValue(3).ToString() %></b></td>
								<td class="jobDescription" valign="top" align="right"><%= shipmentLinesDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() %></td>
								<td class="jobDescription" valign="top" align="right"><%= shipmentLinesDataSet.Tables[0].Rows[i].ItemArray.GetValue(5).ToString() %></td>
								<td class="jobDescription" valign="top" align="right"><%= amount %></td>
								<td class="jobDescription" valign="top" align="right"><%= connectionAmount %></td>
								<td class="jobDescription" valign="top" align="right"><%= totalAmount %></td>
								<td class="jobDescription" valign="top" align="right"><%= shipmentLinesDataSet.Tables[0].Rows[i].ItemArray.GetValue(13).ToString() %></td>
								<td class="jobDescription" valign="top" align="center"><% if (shipmentLinesDataSet.Tables[0].Rows[i].ItemArray.GetValue(12).ToString() == "1") Response.Write("Ja"); %></td>
							</tr>
							<%
									Navipro.SantaMonica.Common.ShipmentLineIds shipmentLineIds = new Navipro.SantaMonica.Common.ShipmentLineIds(database);
									System.Data.DataSet idDataSet = shipmentLineIds.getShipmentLineIdDataSet(currentShipmentHeader.no, int.Parse(shipmentLinesDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()));
									
									int j = 0;
									while (j < idDataSet.Tables[0].Rows.Count)
									{	
										string reMarkUnitId = "";
										string bseValue = "";
										string postMortemValue = "";
										if (idDataSet.Tables[0].Rows[j].ItemArray.GetValue(5).ToString() != "") reMarkUnitId = " (Reservmärkning: "+idDataSet.Tables[0].Rows[j].ItemArray.GetValue(5).ToString()+") ";
										if (idDataSet.Tables[0].Rows[j].ItemArray.GetValue(6).ToString() == "1") bseValue = "(BSE) ";
										if (idDataSet.Tables[0].Rows[j].ItemArray.GetValue(7).ToString() == "1") postMortemValue = "(Obduktion) ";
									
										%>
										<tr>
											<td class="jobDescription">&nbsp;</td>
											<td class="jobDescription" colspan="7">&nbsp;&nbsp;&nbsp;<%= idDataSet.Tables[0].Rows[j].ItemArray.GetValue(3).ToString() + reMarkUnitId + bseValue + postMortemValue %></td>
											<td class="jobDescription">&nbsp;</td>
										</tr>
							<%
										
										j++;
									}
										
									i++;
								}
							
							
							%>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
