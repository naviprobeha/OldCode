<%@ Page language="c#" Codebehind="lineorders_move_cont.aspx.cs" AutoEventWireup="false" Inherits="WebAdmin.lineorders_move_cont" %>
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
				document.location.href = "lineorders_modify.aspx?lineOrderNo=<%= currentLineOrder.entryNo %>";			
			}
	
					
			function validate()
			{
				if (confirm("Du kommer att flytta valda följesedlar. Är du säker?") == 1)
				{
					document.thisform.action = "lineorders_move_cont.aspx";
					document.thisform.command.value = "moveShipments";
					document.thisform.submit();
				}
			}
					

			</script>
		
		<% } %>
		
		<script>
		
		function goBack()
		{
			document.location.href='lineorders.aspx';		
		}

	
		</script>
		
	</HEAD>
	<body MS_POSITIONING="GridLayout" topmargin="10">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" action="orders_view.aspx" method="post" runat="server">
			<input type="hidden" name="command" value="">
			<input type="hidden" name="lineOrderNo" value="<%= currentLineOrder.entryNo %>"> 
			<input type="hidden" name="containerNo" value="<%= Request["containerNo"] %>"> 
			<input type="hidden" name="mode" value="">		
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame" style="border-bottom: 0 px">
				<tr>
					<td height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td class="activityName" colspan="2">Flytta innehåll, linjeorder <% if (currentLineOrder.entryNo > 0) Response.Write(currentLineOrder.entryNo); %></td>
				</tr>
			</table>
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame" style="border-top: 0 px">
				<tr>
					<td width="50" valign="top"><iframe id="mapFrame" width="265" height="100%" frameborder="no" scrolling="no" src="<%= mapServer.getUrl() %>" style="border: 1px #000000 solid;"></iframe></td>
					<td valign="top">
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Kundnamn</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentLineOrder.shippingCustomerName %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td colspan="2" class="activityAuthor" height="15" valign="top">Kundnr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentLineOrder.shippingCustomerNo %></b>&nbsp;</td>
											<% if (currentOrganization.allowLineOrderSupervision) { %><td class="interaction" width="90%" nowrap><a href="shippingCustomers_view.aspx?shippingCustomerNo=<%= currentLineOrder.shippingCustomerNo %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td><% } %>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td colspan="2" class="activityAuthor" height="15" valign="top">Status</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentLineOrder.getStatusText() %></b>&nbsp;</td>
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
											<td class="interaction" height="20"><b><%= currentLineOrder.address %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Adress 2</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentLineOrder.address2 %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Telefonnr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentLineOrder.phoneNo %></b></td>
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
											<td class="interaction" height="20"><b><%= currentLineOrder.postCode %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Ort</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentLineOrder.city %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Mobiltelefonnr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentLineOrder.cellPhoneNo %></b></td>
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
											<td class="interaction" height="20"><b><% if (lineOrderOrganization != null) Response.Write(lineOrderOrganization.name); %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" width="250">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Rutt</td>
										</tr>
										<%
											string routeName = currentLineOrder.getRouteName(database);
										%>
										<tr>
											<td class="interaction" height="20">
												<table cellspacing="0" cellpadding="0" width="100%" border="0">
													<TR>
														<td class="interaction" valign="top" nowrap><b><%= routeName %></b></td>
														<% if (currentOrganization.allowLineOrderSupervision) { %>
														<td align="left" valign="top" width="80%">&nbsp;<a href="linejournals_view.aspx?lineJournalNo=<%= currentLineOrder.lineJournalEntryNo %>"><img src="images/button_assist.gif" border="0" alt="Tilldela rutt" width="12" height="13"></a></td>
														<% } %>
													</TR>
												</table>
											</td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Planering</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><% if (currentLineOrder.enableAutoPlan) Response.Write("Automatisk"); else Response.Write("Manuell"); %></b></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<br>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Container</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= Request["containerNo"] %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Flytta till container</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><select name="moveToContainerNo" class="Textfield">
										<%
											int o = 0;
											
											while (o < containersDataSet.Tables[0].Rows.Count)
											{	
											
												%>
												<option value="<%= containersDataSet.Tables[0].Rows[o].ItemArray.GetValue(0).ToString() %>"><%= containersDataSet.Tables[0].Rows[o].ItemArray.GetValue(0).ToString() %></option>
												<%
													
												o++;
											}
																																					
										%>
									</select></td>
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
											<td class="interaction" height="20"><b><%= currentLineOrder.comments %></b></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<br>
						Följesedlar
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">Nr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">Datum</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">Namn</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">Ort</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">Innehåll</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">Flytta</th>
							</tr>
							<%
														
								int j = 0;
								while (j < shipmentDataSet.Tables[0].Rows.Count)
								{
									Navipro.SantaMonica.Common.LineOrderShipment lineOrderShipment = new Navipro.SantaMonica.Common.LineOrderShipment(shipmentDataSet.Tables[0].Rows[j]);
									Navipro.SantaMonica.Common.ShipmentHeaders shipmentHeaders = new Navipro.SantaMonica.Common.ShipmentHeaders();
									Navipro.SantaMonica.Common.ShipmentHeader shipmentHeader = shipmentHeaders.getEntry(database, lineOrderShipment.shipmentNo);
									
									%>
									<tr>
										<td class="jobDescription" valign="top" nowrap><%= lineOrderShipment.shipmentNo %></td>
										<td class="jobDescription" valign="top" nowrap><%= shipmentHeader.shipDate.ToString("yyyy-MM-dd") %>&nbsp;</td>
										<td class="jobDescription" valign="top"><%= shipmentHeader.customerName %>&nbsp;</td>
										<td class="jobDescription" valign="top"><%= shipmentHeader.city %>&nbsp;</td>
										<td class="jobDescription" valign="top"><%= shipmentHeaders.getShipmentContent(database, shipmentHeader.no) %>&nbsp;</td>
										<td class="jobDescription" valign="top" align="center"><input type="checkbox" name="move_<%= shipmentHeader.no %>"></td>
									</tr>
									<%
								
									j++;
								}

								if (j == 0)
								{
									%><tr>
										<td class="jobDescription" colspan="5">Inga transaktioner registrerade.</td>
									</tr><%
								}
							
							%>
						</table>
						
						
							<br>
							<table cellspacing="1" cellpadding="2" border="0" width="100%">
							<tr>
								<td align="right" height="25"><input type="button" onclick="goBack()" value="Avbryt" class="Button">&nbsp;<input type="button" onclick="validate()" value="Spara" class="Button"></td>
							</tr>
							</table>							
					</td>
				</tr>
				</table>
				
			</table>
		</form>
	</body>
</HTML>