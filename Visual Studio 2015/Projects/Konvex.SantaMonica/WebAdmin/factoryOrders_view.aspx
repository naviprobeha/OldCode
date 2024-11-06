<%@ Page language="c#" Codebehind="factoryOrders_view.aspx.cs" AutoEventWireup="false" Inherits="WebAdmin.factoryorders_view" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SmartShipping</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link rel="stylesheet" href="css/webstyle.css">
		<% if (currentOrganization.allowLineOrderSupervision) { %>
		<script>

			function modifyOrder()
			{
				document.location.href = "factoryOrders_modify.aspx?factoryOrderNo=<%= currentFactoryOrder.entryNo %>";			
			}
	
			function calcInventory()
			{
				document.location.href = "factoryOrders_view.aspx?factoryOrderNo=<%= currentFactoryOrder.entryNo %>&command=calcInventory";			
			}

			function setTransportInvoiceStatus()
			{
				document.location.href = "factoryOrders_view.aspx?factoryOrderNo=<%= currentFactoryOrder.entryNo %>&command=setTransportInvoiceStatus";			
			}
					
		</script>
		<% } %>
		<script>
		
		function goBack()
		{
			document.location.href='factoryOrders.aspx';		
		}

		function showDocument()
		{
			document.location.href='shippingForm_factory_print.aspx?sid=<%= Session.SessionID %>&factoryOrderEntryNo=<%= currentFactoryOrder.entryNo %>';		
		}
	
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" topmargin="10">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" action="orders_view.aspx" method="post" runat="server">
			<input type="hidden" name="command"> <input type="hidden" name="factoryOrderNo" value="<%= currentFactoryOrder.entryNo %>">
			<input type="hidden" name="mode">
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame" style="BORDER-BOTTOM-WIDTH: 0px">
				<tr>
					<td height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td class="activityName" colspan="2">Fabriksorder
						<% if (currentFactoryOrder.entryNo > 0) Response.Write(currentFactoryOrder.entryNo); %>
					</td>
				</tr>
				<tr>
					<td align="left" height="25"><% if ((currentOrganization.allowLineOrderSupervision) && (currentOperator.systemRoleCode == "SUPER")) { %><INPUT class="Button" onclick="setTransportInvoiceStatus()" type="button" value="Ändra fakturastatus">&nbsp;&nbsp;<input type="button" onclick="modifyOrder()" value="Ändra" class="Button">&nbsp;<% } %><input type="button" onclick="showDocument()" value="Skriv ut" class="Button">&nbsp;&nbsp;<input type="button" onclick="goBack()" value="Klar" class="Button"></td>
				</tr>
			</table>
			<TABLE class="frame" style="BORDER-TOP-WIDTH: 0px" cellSpacing="2" cellPadding="2" width="100%"
				border="0">
				<TR>
					<td width="50" valign="top"><iframe id="mapFrame" width="265" height="100%" frameborder="no" scrolling="no" src="<%= mapServer.getUrl() %>" style="BORDER-BOTTOM: #000000 1px solid; BORDER-LEFT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-RIGHT: #000000 1px solid"></iframe>
					</td>
					<td valign="top">
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<td class="interaction" valign="top" width="300">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Planeringsmetod</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.getPlanningType() %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Uppläggningsdatum</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><% if (currentFactoryOrder.creationDate.Year != 1753) Response.Write(currentFactoryOrder.creationDate.ToString("yyyy-MM-dd")); %></b>&nbsp;</td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Attesterad av fabriken</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.getFactoryApprovalStatus() %>&nbsp;</b></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="interaction" valign="top" width="300">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Hämtdatum och klockslag</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.shipDate.ToString("yyyy-MM-dd") %>&nbsp;<%= currentFactoryOrder.shipTime.ToString("HH:mm") %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Planerat leveransdatum och 
												klockslag</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><% if (currentFactoryOrder.plannedArrivalDateTime.Year != 1753) Response.Write(currentFactoryOrder.plannedArrivalDateTime.ToString("yyyy-MM-dd HH:mm")); %>&nbsp;</b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Transportfaktura mottagen</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.getTransportInvoiceStatus() %>&nbsp;</b></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<br>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<td class="interaction" valign="top" width="300">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Transportör</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.getOrganizationName(database) %></b>&nbsp;</td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" width="250">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Bil</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.agentCode %></b>&nbsp;</td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Status</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.getStatusText() %>&nbsp;</b></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="interaction" valign="top" width="300">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Chaufför lastning</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.driverName %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Chaufför lossning</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.dropDriverName %></b></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<br>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<td class="interaction" valign="top" width="300">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Fabrik</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.factoryName %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td colspan="2" class="activityAuthor" height="15" valign="top">Nr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.factoryNo %></b>&nbsp;</td>
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
											<td class="interaction" height="20"><b><%= currentFactoryOrder.factoryAddress %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Adress 2</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.factoryAddress2 %></b></td>
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
											<td class="interaction" height="20"><b><%= currentFactoryOrder.factoryPostCode %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" width="250">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Ort</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.factoryCity %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Telefonnr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.factoryPhoneNo %></b></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="interaction" valign="top" colspan="3">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Övriga meddelanden</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.comments %></b></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<br>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<td class="interaction" valign="top" width="300">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Värmeverk</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.consumerName %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td colspan="2" class="activityAuthor" height="15" valign="top">Nr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.consumerNo %></b>&nbsp;</td>
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
											<td class="interaction" height="20"><b><%= currentFactoryOrder.consumerAddress %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Adress 2</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.consumerAddress2 %></b></td>
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
											<td class="interaction" height="20"><b><%= currentFactoryOrder.consumerPostCode %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" width="250">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Ort</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.consumerCity %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Telefonnr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.consumerPhoneNo %></b></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<br>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<td class="interaction" valign="top" width="300">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">PH-värde (Avsändare)</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.phValueFactory %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">PH-värde (Transportör)</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.phValueShipping %></b></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<br>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<td class="interaction" valign="top" width="300">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Kategori</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.categoryCode %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Beskrivning</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.categoryDescription %></b></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Antal</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.quantity.ToString() %>
													ton</b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" width="250">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Lossat antal</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.realQuantity.ToString() %>
													ton</b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Nivå efter lossning</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.consumerLevel.ToString() %>
													<%= currentFactoryOrder.getConsumerUnit() %>
												</b>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<br>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<td class="interaction" valign="top" width="300">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Klockslag lastning</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.getPickupDateTime() %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" width="250">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Lastningstid</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><% if (currentFactoryOrder.loadDuration > 0) Response.Write(currentFactoryOrder.loadDuration+" minuter"); %>
												</b>
											</td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Väntetid</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><% if (currentFactoryOrder.loadWaitDuration > 0) Response.Write(currentFactoryOrder.loadWaitDuration+" minuter ("+currentFactoryOrder.loadReasonText+")"); %>
												</b>
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="interaction" valign="top" width="300">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Klockslag lossning</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.getArrivalDateTime() %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" width="250">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Lossningstid</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><% if (currentFactoryOrder.dropDuration > 0) Response.Write(currentFactoryOrder.dropDuration+" minuter"); %>
												</b>
											</td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Väntetid</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><% if (currentFactoryOrder.dropWaitDuration > 0) Response.Write(currentFactoryOrder.dropWaitDuration+" minuter ("+currentFactoryOrder.dropReasonText+")"); %>
												</b>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<br>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<td class="interaction" valign="top" width="300">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Extra mil</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.extraDist %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Extra tid</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.extraTime %>
													minuter</b></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<br>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<td class="interaction" valign="top" width="300">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top" nowrap>Städningsstatus 
												rapporterad från bilen</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.getAgentCleaningStatus() %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Kommentar från bilen</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.agentCleaningComment %>
												</b>
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top" nowrap>Städningsstatus 
												rapporterad från värmeverket</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.getConsumerCleaningStatus() %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Kommentar från värmeverket</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.consumerCleaningComment %>
												</b>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<table cellspacing="1" cellpadding="2" border="0" width="100%">
							<tr>
								<td align="right" height="25"><% if ((currentOrganization.allowLineOrderSupervision) && (currentOperator.systemRoleCode == "SUPER")) { %><INPUT class="Button" onclick="setTransportInvoiceStatus()" type="button" value="Ändra fakturastatus">&nbsp;&nbsp;<input type="button" onclick="modifyOrder()" value="Ändra" class="Button">&nbsp;<% } %><input type="button" onclick="showDocument()" value="Skriv ut" class="Button">&nbsp;&nbsp;<input type="button" onclick="goBack()" value="Klar" class="Button"></td>
							</tr>
						</table>
					</td>
				</TR>
			</TABLE>
			</TABLE>
		</form>
	</body>
</HTML>
