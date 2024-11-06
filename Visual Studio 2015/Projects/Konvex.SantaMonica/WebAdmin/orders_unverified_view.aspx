<%@ Page language="c#" Codebehind="orders_unverified_view.aspx.cs" AutoEventWireup="false" Inherits="Navipro.SantaMonica.WebAdmin.orders_unverified_view" %>
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
		
		function modifyOrder()
		{
			document.location.href = "orders_modify_full.aspx?shipOrderNo=<%= currentShipOrder.entryNo %>";			
		}

		function modifyOrderLines()
		{
			document.location.href = "orders_items.aspx?shipOrderNo=<%= currentShipOrder.entryNo %>";			
		}
		
		function verifyOrder(organizationNo, customerNo)
		{
			if (confirm("Är du säker på att du vill byta kundnr till "+customerNo+" och transportör till "+organizationNo+"?"))
			{
				document.thisform.action = "orders_unverified_view.aspx?organizationNo="+organizationNo+"&customerNo="+customerNo;
				document.thisform.command.value = "setCustomerNo";
				document.thisform.submit();		
			}
		}

		function goBack()
		{
			document.location.href='orders.aspx';		
		}

	
		<%
			if (notifyUserAboutPayment)
			{
				%>alert("Betalningssätt ändrat till kontant.");<%
			}
		%>

		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" topmargin="10">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" action="orders_unverified_view.aspx" method="post" runat="server">
			<input type="hidden" name="command" value="">
			<input type="hidden" name="shipOrderNo" value="<%= currentShipOrder.entryNo %>"> 
			<input type="hidden" name="mode" value="">		
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame">
				<tr>
					<td height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td class="activityName">Ej verifierad körorder <% if (currentShipOrder.entryNo > 0) Response.Write(currentShipOrder.entryNo); %></td>
				</tr>
				<tr>
					<td align="left" height="25"><input type="button" onclick="modifyOrder()" value="Ändra orderhuvud" class="Button">&nbsp;<input type="button" onclick="modifyOrderLines()" value="Ändra rader / djurslag" class="Button">&nbsp;<input type="button" onclick="goBack()" value="Klar" class="Button"></td>
				</tr>
				<tr>
					<td>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<td class="interaction" valign="top" width="250">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Hämtdatum</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentShipOrder.shipDate.ToString("yyyy-MM-dd") %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" width="250">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Anmälningsdatum</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><% if (currentShipOrder.creationDate.Year != 1753) Response.Write(currentShipOrder.creationDate.ToString("yyyy-MM-dd")); %></b>&nbsp;</td>
										</tr>
									</table>
								</td>								
								<td class="interaction" valign="top" width="250">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Transportör</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentShipOrder.getOrganizationName(database) %></b>&nbsp;</td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Prioritet</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentShipOrder.priority %></b>&nbsp;</td>
										</tr>
									</table>
								</td>								
							</tr>
						</table>
						<br>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<td class="interaction" valign="top" width="250">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Kundnr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentShipOrder.customerNo %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" width="250">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Namn</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentCustomer.name %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Betalsätt</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><% if (currentShipOrder.paymentType == 1) Response.Write("Kontant"); else Response.Write("Faktura"); %></b></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<br>
						<table cellspacing="0" cellpadding="0" border="0" width="100%">
						<tr>
							<td style="padding-right: 2px" valign="top">						
								<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
									<tr>
										<td class="interaction" valign="top" width="250">
											<table cellspacing="0" cellpadding="0" width="100%" border="0">
												<tr>
													<td colspan="2" class="activityAuthor" height="15" valign="top">Faktureras kundnr</td>
												</tr>
												<tr>
													<td class="interaction" height="20"><b><%= currentShipOrder.billToCustomerNo %></b>&nbsp;</td>
													<td class="interaction" width="90%" nowrap>&nbsp;</td>
												</tr>
											</table>
										</td>
										<td class="interaction" valign="top">
											<table cellspacing="0" cellpadding="0" width="100%" border="0">
												<tr>
													<td class="activityAuthor" height="15" valign="top">Produktionsplatsnr</td>
												</tr>
												<tr>
													<td class="interaction" height="20"><b><%= currentCustomer.productionSite %></b></td>
												</tr>
											</table>
										</td>
									</tr>
									<tr>
										<td class="interaction" valign="top" colspan="2">
											<table cellspacing="0" cellpadding="0" width="100%" border="0">
												<tr>
													<td class="activityAuthor" height="15" valign="top">Kundnamn</td>
												</tr>
												<tr>
													<td class="interaction" height="20"><b><%= currentShipOrder.customerName %></b></td>
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
													<td class="interaction" height="20"><b><%= currentShipOrder.address %></b></td>
												</tr>
											</table>
										</td>
										<td class="interaction" valign="top">
											<table cellspacing="0" cellpadding="0" width="100%" border="0">
												<tr>
													<td class="activityAuthor" height="15" valign="top">Adress 2</td>
												</tr>
												<tr>
													<td class="interaction" height="20"><b><%= currentShipOrder.address2 %></b></td>
												</tr>
											</table>
										</td>
									</tr>
									<tr>
										<td class="interaction" valign="top">
											<table cellspacing="0" cellpadding="0" width="100%" border="0">
												<tr>
													<td class="activityAuthor" height="15" valign="top">Postnr</td>
												</tr>
												<tr>
													<td class="interaction" height="20"><b><%= currentShipOrder.postCode %></b></td>
												</tr>
											</table>
										</td>
										<td class="interaction" valign="top">
											<table cellspacing="0" cellpadding="0" width="100%" border="0">
												<tr>
													<td class="activityAuthor" height="15" valign="top">Ort</td>
												</tr>
												<tr>
													<td class="interaction" height="20"><b><%= currentShipOrder.city %></b></td>
												</tr>
											</table>
										</td>
									</tr>
								</table>
							</td>
							<td style="padding-left: 2px" valign="top">
								<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
									<tr>
										<td class="interaction" valign="top" width="250">
											<table cellspacing="0" cellpadding="0" width="100%" border="0">
												<tr>
													<td class="activityAuthor" height="15" valign="top">Hämtadress namn</td>
												</tr>
												<tr>
													<td class="interaction" height="20"><b><%= currentShipOrder.shipName %></b></td>
												</tr>
											</table>
										</td>
										<td class="interaction" valign="top">
											<table cellspacing="0" cellpadding="0" width="100%" border="0">
												<tr>
													<td class="activityAuthor" height="15" valign="top">Produktionsplatsnr</td>
												</tr>
												<tr>
													<td class="interaction" height="20"><b><%= currentShipOrder.productionSite %></b></td>
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
													<td class="interaction" height="20"><b><%= currentShipOrder.shipAddress %></b></td>
												</tr>
											</table>
										</td>
										<td class="interaction" valign="top">
											<table cellspacing="0" cellpadding="0" width="100%" border="0">
												<tr>
													<td class="activityAuthor" height="15" valign="top">Adress 2</td>
												</tr>
												<tr>
													<td class="interaction" height="20"><b><%= currentShipOrder.shipAddress2 %></b></td>
												</tr>
											</table>
										</td>
									</tr>
									<tr>
										<td class="interaction" valign="top">
											<table cellspacing="0" cellpadding="0" width="100%" border="0">
												<tr>
													<td class="activityAuthor" height="15" valign="top">Postnr</td>
												</tr>
												<tr>
													<td class="interaction" height="20"><b><%= currentShipOrder.shipPostCode %></b></td>
												</tr>
											</table>
										</td>
										<td class="interaction" valign="top">
											<table cellspacing="0" cellpadding="0" width="100%" border="0">
												<tr>
													<td class="activityAuthor" height="15" valign="top">Ort</td>
												</tr>
												<tr>
													<td class="interaction" height="20"><b><%= currentShipOrder.shipCity %></b></td>
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
													<td class="interaction" height="20"><b><%= currentShipOrder.phoneNo %></b></td>
												</tr>
											</table>
										</td>
										<td class="interaction" valign="top">
											<table cellspacing="0" cellpadding="0" width="100%" border="0">
												<tr>
													<td class="activityAuthor" height="15" valign="top">Mobiltelefonnr</td>
												</tr>
												<tr>
													<td class="interaction" height="20"><b><%= currentShipOrder.cellPhoneNo %></b></td>
												</tr>
											</table>
										</td>
									</tr>
								</table>
							</td>
						</tr>
						</table>
						<br>
						Matchade kunder
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Kundnr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Transportör</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Namn</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Adress</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Ort</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Telefonnr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Mobiltelefonnr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Prod. nr.</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center" valign="bottom">
									Kontant betalning</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center" valign="bottom">
									Använd kundnr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center" valign="bottom">
									Visa</th>
							</tr>
							<%
								int c = 0;
								while (c < customerList.Count)
								{
									Navipro.SantaMonica.Common.Customer customer = (Navipro.SantaMonica.Common.Customer)customerList[c];
									bool blocked = false;
									string cashPayment = "";
									if (customer.forceCashPayment) cashPayment = "Ja";
									
									string bgStyle = "";
									
									if ((c % 2) > 0)
									{
										bgStyle = " style=\"background-color: #e0e0e0;\"";
									}
									
									%>	
										<tr>
											<td class="jobDescription" <%= bgStyle %> valign="top"><%= customer.no %></td>
											<td class="jobDescription" <%= bgStyle %> valign="top"><%= customer.organizationNo %></td>
											<td class="jobDescription" <%= bgStyle %> valign="top"><%= customer.name %></td>
											<td class="jobDescription" <%= bgStyle %> valign="top"><%= customer.address %></td>
											<td class="jobDescription" <%= bgStyle %> valign="top"><%= customer.city %></td>
											<td class="jobDescription" <%= bgStyle %> valign="top"><%= customer.phoneNo %></td>
											<td class="jobDescription" <%= bgStyle %> valign="top"><%= customer.cellPhoneNo %></td>
											<td class="jobDescription" <%= bgStyle %> valign="top"><%= customer.productionSite %></td>
											<td class="jobDescription" <%= bgStyle %> valign="top" align="center">&nbsp;<%= cashPayment %>&nbsp;</td>
											<td class="jobDescription" <%= bgStyle %> valign="top" align="center">&nbsp;<% if (!blocked) { %><a href="javascript:verifyOrder('<%= customer.organizationNo %>', '<%= customer.no %>')"><img src="images/button_assist.gif" border="0" width="12" height="13"></a><% } %>&nbsp;</td>
											<td class="jobDescription" <%= bgStyle %> valign="top" align="center">&nbsp;<% if (!blocked) { %><a href="customers_view.aspx?organizationNo=<%= customer.organizationNo %>&customerNo=<%= customer.no %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a><% } %>&nbsp;</td>
										</tr>
								
								
									<%
								
									c++;
								}
								if (customerList.Count == 0)
								{
									%>
									<tr>
										<td class="jobDescription" valign="top" colspan="11">Inga matchade kunder funna...</td>
									</tr>
									<%
								}
							%>
						</table>
						<br>			
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<td class="interaction" valign="top" width="100%">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Vägbeskrivning</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentShipOrder.directionComment+currentShipOrder.directionComment2 %></b></td>
										</tr>
									</table>
								</td>
							</tr>							
						</table>
						<br>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Fraktinnehåll</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentShipOrder.details %></b></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Kommentarer</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentShipOrder.comments %></b></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<br>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">Datum</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">Klockslag</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">Källa</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">Beskrivning</th>
							</tr>
							<%
						
								int z = 0;
								while (z < shipOrderLogLineDataSet.Tables[0].Rows.Count)
								{
									Navipro.SantaMonica.Common.ShipOrderLogLine shipOrderLogLine = new Navipro.SantaMonica.Common.ShipOrderLogLine(shipOrderLogLineDataSet.Tables[0].Rows[z]);
						
									%>
									<tr>
										<td class="jobDescription" valign="top"><%= shipOrderLogLine.date.ToString("yyyy-MM-dd") %></td>
										<td class="jobDescription" valign="top"><%= shipOrderLogLine.timeOfDay.ToString("HH:mm:ss") %></td>
										<td class="jobDescription" valign="top"><%= shipOrderLogLine.source %></td>
										<td class="jobDescription" valign="top"><%= shipOrderLogLine.text %></td>
									</tr>
									<%
								
									z++;
								}

								if (z == 0)
								{
									%><tr>
										<td class="jobDescription" colspan="11">Inga rader registrerade.</td>
									</tr><%
								}
							
							%>
						</table>						
						<br>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">Artikelnr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">Beskrivning</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right">Antal</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right">Enhet</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right">Pris</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right">Avlivningar</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right">Pris avlivning</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right">Provtagningar</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right">Totalbelopp</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" colspan="2">ID</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca">Ta bort</th>	
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca">Ändra</th>	
							</tr>
							<%
						
								int i = 0;
								while (i < shipOrderLineDataSet.Tables[0].Rows.Count)
								{
									string price = decimal.Round(decimal.Parse(shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(6).ToString()), 2)+" kr";
									string connectionPrice = decimal.Round(decimal.Parse(shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(8).ToString()), 2)+" kr";
									string totalAmount = decimal.Round(decimal.Parse(shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(9).ToString()), 2)+" kr";

									Navipro.SantaMonica.Common.Item item = items.getEntry(database, shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString());
							
									%>
									<tr>
										<td class="jobDescription" valign="top"><%= shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString() %></td>
										<td class="jobDescription" valign="top"><%= item.description %></td>
										<td class="jobDescription" valign="top" align="right"><%= shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(3).ToString() %></td>
										<td class="jobDescription" valign="top" align="right"><%= item.unitOfMeasure %></td>
										<td class="jobDescription" valign="top" align="right"><%= price %></td>
										<td class="jobDescription" valign="top" align="right"><%= shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() %></td>
										<td class="jobDescription" valign="top" align="right"><%= connectionPrice %></td>
										<td class="jobDescription" valign="top" align="right"><%= shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(11).ToString() %></td>
										<td class="jobDescription" valign="top" align="right"><%= totalAmount %></td>
										<td class="jobDescription" valign="top" nowrap><%
										
											System.Data.DataSet shipOrderLineIdDataSet = shipOrderLineIds.getDataSet(database, currentShipOrder.entryNo, int.Parse(shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()));
											
											int m = 0;
											
											while (m < shipOrderLineIdDataSet.Tables[0].Rows.Count)
											{
												string bseValue = "";
												string postMortemValue = "";
												if (shipOrderLineIdDataSet.Tables[0].Rows[m].ItemArray.GetValue(4).ToString() == "1") bseValue = "(P)";
												if (shipOrderLineIdDataSet.Tables[0].Rows[m].ItemArray.GetValue(5).ToString() == "1") postMortemValue = "(O)";
												
												
												Response.Write(shipOrderLineIdDataSet.Tables[0].Rows[m].ItemArray.GetValue(3).ToString()+bseValue+postMortemValue+"<br>");
												m++;
											}
										
										
										%></td>
										<td class="jobDescription" valign="top" align="center"><a href="orders_id.aspx?shipOrderNo=<%= Request["shipOrderNo"] %>&shipOrderlineNo=<%= shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
										<td class="jobDescription" valign="top" align="center"><a href="orders_view.aspx?command=deleteLine&shipOrderNo=<%= Request["shipOrderNo"] %>&lineNo=<%= shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>"><img src="images/button_delete.gif" border="0" width="12" height="13"></a></td>
										<td class="jobDescription" valign="top" align="center"><a href="orders_items_modify.aspx?shipOrderNo=<%= Request["shipOrderNo"] %>&entryNo=<%= shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
									</tr>
									<%
								
									i++;
								}

								if (i == 0)
								{
									%><tr>
										<td class="jobDescription" colspan="13">Inga rader registrerade.</td>
									</tr><%
								}
							
							%>
						</table>
					</td>
				</tr>
				<tr>
					<td align="right" height="25"><input type="button" onclick="modifyOrder()" value="Ändra orderhuvud" class="Button">&nbsp;<input type="button" onclick="modifyOrderLines()" value="Ändra rader / djurslag" class="Button">&nbsp;<input type="button" onclick="goBack()" value="Klar" class="Button"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
