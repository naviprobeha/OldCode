<%@ Page language="c#" Codebehind="orders_assign.aspx.cs" AutoEventWireup="false" Inherits="WebAdmin.orders_assign" %>
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
		function validate()
		{
			proceed = true;
			
		
			if (proceed)
			{
				document.thisform.command.value = "assignOrder";
				document.thisform.submit();
			}
		
		}
		
		function validateLastAgent()
		{
			proceed = true;
			
		
			if (proceed)
			{
				document.thisform.command.value = "assignOrderToLastAgent";
				document.thisform.submit();
			}
		
		}
	
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" topmargin="10">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" method="post" runat="server">
			<input type="hidden" name="command" value="">
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame" style="border-bottom: 0 px">
				<tr>
					<td height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td class="activityName">Körorder
						<%= currentShipOrder.entryNo %>
					</td>
				</tr>
			</table>
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame" style="border-top: 0 px">
				<tr>
					<td width="50" valign="top"><iframe id="mapFrame" width="265" height="600" frameborder="no" scrolling="no" src="<%= mapServer.getUrl() %>" style="border: 1px #000000 solid;"></iframe></td>
					<td valign="top">	
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Bilar</td>
											<td class="activityAuthor" height="15" valign="top">Senaste hämtning</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><select name="agentCode" class="Textfield">
												<option value="">- Ej tilldelad -</option>
												<%
													int i = 0;
													while (i < activeAgents.Tables[0].Rows.Count)
													{
														
														%>
														<option value="<%= activeAgents.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>" <% if (currentShipOrder.agentCode == activeAgents.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()) Response.Write("selected"); %>><%= activeAgents.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()+" "+activeAgents.Tables[0].Rows[i].ItemArray.GetValue(1).ToString()+" ("+activeAgents.Tables[0].Rows[i].ItemArray.GetValue(14).ToString()+")" %></option>													
														<%
													
														i++;
													}
												
												
												%>
												
												</select>&nbsp;<input type="button" onclick="validate()" value="Tilldela" class="Button">
											</td>
											<td class="interaction" height="20" width="500"><% if (lastAgent != null) { %><%= lastAgent.lastUpdated.ToString("yyyy-MM-dd") %>, <%= lastAgent.code %>&nbsp;<input type="button" onclick="validateLastAgent()" value="Tilldela <%= lastAgent.code+lastAgent._officeMode %>" class="Button"><% } %>
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
											<td class="activityAuthor" height="15" valign="top">Kundnamn</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentShipOrder.customerName %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Kundnr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentShipOrder.customerNo %></b></td>
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
						<br>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<td class="interaction" valign="top" width="300">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Faktureras kundnr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentShipOrder.billToCustomerNo %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Namn</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= billToCustomer.name %></b></td>
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
											<td class="activityAuthor" height="15" valign="top">Hämtadress namn</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentShipOrder.shipName %></b></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="interaction" valign="top" width="300">
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
							<tr>
								<td class="interaction" valign="top" colspan="2">
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

					</td>
				</tr>
			</table>
		</form>
		
			
	</body>
</HTML>
