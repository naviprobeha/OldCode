<%@ Page language="c#" Codebehind="shipments_special1.aspx.cs" AutoEventWireup="false" Inherits="Navipro.SantaMonica.WebAdmin.shipments_special1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SmartShipping</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link rel="stylesheet" href="css/webstyle.css">
		<script>
	
	function submitSearch()
	{
		document.thisform.command.value = "changeShipDate";
		document.thisform.submit();
	
	}		
	
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" topmargin="10">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" method="post" runat="server">
			<input type="hidden" name="command">
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame">
				<tr>
					<td height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td class="activityName">Underlag ommärkningar, <%= currentOrganization.name %></td>
				</tr>
				<tr>
					<td class="">Datumintervall:&nbsp;<% createDatePicker("startDate", startDate); %>&nbsp;-&nbsp;<% createDatePicker("endDate", endDate); %>&nbsp;Reservmärkning:&nbsp;<input type="text" size=30" maxlength="30" name="searchReMarkUnitId" class="Textfield">&nbsp;<input type="button" value="Sök" class="Button" onclick="submitSearch()"></td>
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
									Produktionsplatsnr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Datum</th>
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
								string prevShipmentNo = "";
								
								Navipro.SantaMonica.Common.Items items = new Navipro.SantaMonica.Common.Items();
								
								while (i < activeShipments.Tables[0].Rows.Count)
								{
								
									if (activeShipments.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() != prevShipmentNo)
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
																	
										
								
										%>
							<tr>
								<td class="jobDescription" valign="top"><%= activeShipments.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %></td>
								<td class="jobDescription" valign="top"><% if (activeShipments.Tables[0].Rows[i].ItemArray.GetValue(9).ToString() != "0") Response.Write(activeShipments.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()+activeShipments.Tables[0].Rows[i].ItemArray.GetValue(9).ToString()); %></td>
								<td class="jobDescription" valign="top"><%= activeShipments.Tables[0].Rows[i].ItemArray.GetValue(10).ToString() %></td>
								<td class="jobDescription" valign="top" nowrap><%= System.DateTime.Parse(activeShipments.Tables[0].Rows[i].ItemArray.GetValue(6).ToString()).ToString("yyyy-MM-dd") %></td>
								<td class="jobDescription" valign="top"><%= activeShipments.Tables[0].Rows[i].ItemArray.GetValue(3).ToString() %><br><%= activeShipments.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() %>, <%= activeShipments.Tables[0].Rows[i].ItemArray.GetValue(5).ToString() %></td>
								<td class="jobDescription" valign="top" align="right">&nbsp;</td>
								<td class="jobDescription" valign="top">&nbsp;</td>
								<td class="jobDescription" valign="top" align="right">&nbsp;</td>
								<td class="jobDescription" valign="top" align="right">&nbsp;</td>
								<td class="jobDescription" valign="top" align="right">&nbsp;</td>
								<td class="jobDescription" valign="top" align="center"><%= status %></td>
								<td class="jobDescription" valign="top" align="center"><img src="images/<%= icon %>" alt="<%= status %>" border="0"></td>
							</tr>
							<%
										Navipro.SantaMonica.Common.ShipmentLineIds shipmentLineIds = new Navipro.SantaMonica.Common.ShipmentLineIds(database);
									
										System.Data.DataSet shipmentLinesDataSet = shipmentLines.getShipmentLinesDataSet(activeShipments.Tables[0].Rows[i].ItemArray.GetValue(1).ToString());
															
										int j = 0;
										while (j < shipmentLinesDataSet.Tables[0].Rows.Count)
										{
											System.Data.DataSet markedShipmentLineIdsDataSet = shipmentLineIds.getMarkedShipmentLineIdDataSet(activeShipments.Tables[0].Rows[i].ItemArray.GetValue(1).ToString(), int.Parse(shipmentLinesDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString()));
											if (markedShipmentLineIdsDataSet.Tables[0].Rows.Count > 0)
											{
											
											
										  
													Navipro.SantaMonica.Common.Item item = items.getEntry(database, shipmentLinesDataSet.Tables[0].Rows[j].ItemArray.GetValue(2).ToString());
													
												
													decimal unitPrice = decimal.Parse(shipmentLinesDataSet.Tables[0].Rows[j].ItemArray.GetValue(6).ToString());																														
													decimal totalPrice = decimal.Parse(shipmentLinesDataSet.Tables[0].Rows[j].ItemArray.GetValue(7).ToString());	
													decimal totalPriceInclVat = decimal.Multiply(totalPrice, new decimal(1.25));
													
																				
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
													
											
													System.Data.DataSet idDataSet = shipmentLineIds.getShipmentLineIdDataSet(activeShipments.Tables[0].Rows[i].ItemArray.GetValue(1).ToString(), int.Parse(shipmentLinesDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString()));
													
													int k = 0;
													while (k < idDataSet.Tables[0].Rows.Count)
													{	
														string remark = "";
														if (idDataSet.Tables[0].Rows[k].ItemArray.GetValue(4).ToString() == "1") remark = " (Ommärkning)";
													
														%>
														<tr>
															<td class="jobDescription" valign="top">&nbsp;</td>
															<td class="jobDescription" valign="top">&nbsp;</td>
															<td class="jobDescription" valign="top">&nbsp;</td>
															<td class="jobDescription" valign="top">&nbsp;</td>
															<td class="jobDescription">&nbsp;&nbsp;&nbsp;<%= idDataSet.Tables[0].Rows[k].ItemArray.GetValue(3).ToString() + " ("+ idDataSet.Tables[0].Rows[k].ItemArray.GetValue(5).ToString()+")" %></td>
															<td class="jobDescription" valign="top">&nbsp;</td>
															<td class="jobDescription" valign="top">&nbsp;</td>
															<td class="jobDescription" valign="top">&nbsp;</td>
															<td class="jobDescription" valign="top">&nbsp;</td>
															<td class="jobDescription" valign="top">&nbsp;</td>
															<td class="jobDescription" valign="top">&nbsp;</td>
															<td class="jobDescription" valign="top">&nbsp;</td>
														</tr>
														<%
														
														k++;
													}
														
																								

											}		
											j++;
											
											%>
											<tr>
												<td class="jobDescription" valign="top" colspan="12">&nbsp;</td>
											</tr>
											<%										
										}

										prevShipmentNo = activeShipments.Tables[0].Rows[i].ItemArray.GetValue(1).ToString();
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
