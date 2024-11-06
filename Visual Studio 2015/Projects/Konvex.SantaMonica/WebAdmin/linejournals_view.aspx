<%@ Page language="c#" Codebehind="linejournals_view.aspx.cs" AutoEventWireup="false" Inherits="WebAdmin.linejournals_view" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SmartShipping</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link rel="stylesheet" href="css/webstyle.css">
		<%
			string parentPage = "linejournals";
			if (currentLineJournal.status >= 8) parentPage = "linejournals_reported";
			if ((Request["parentPage"] != "") && (Request["parentPage"] != null)) parentPage = Request["parentPage"];
		%>
		<script>
				
		function modifyJournal()
		{
			document.location.href = "linejournals_modify.aspx?lineJournalNo=<%= currentLineJournal.entryNo %>";			
		}

		function recalcTime()
		{
			document.location.href = "linejournals_view.aspx?lineJournalNo=<%= currentLineJournal.entryNo %>&command=recalcTime";			
		}

		function updateArrivalTime()
		{
			document.location.href = "linejournals_view.aspx?lineJournalNo=<%= currentLineJournal.entryNo %>&command=updateArrivalTime";			
		}

		</script>
		<% if (currentOrganization.allowLineOrderSupervision) { %>
		<script>
					
			
			function toggleForcedAssignment()
			{
				document.location.href = "linejournals_view.aspx?lineJournalNo=<%= currentLineJournal.entryNo %>&command=toggleForcedAssignment";			
			}

			function setInvoiceStatus()
			{
				document.location.href = "linejournals_view.aspx?lineJournalNo=<%= currentLineJournal.entryNo %>&command=setInvoiceStatus";			
			}

		</script>
		<% } %>
		<script>

		function goBack()
		{
			
			document.location.href='<%= parentPage %>.aspx';		
		}

		function loadOrders()
		{
			if (confirm("Du kommer att lasta samtliga containers. Är du säker?") == 1)
			{
				document.thisform.command.value = "loadOrders";
				document.thisform.submit();
			}
		}

		function unloadOrders()
		{
			if (confirm("Du kommer att lossa samtliga containers. Är du säker?") == 1)
			{
				document.thisform.command.value = "unloadOrders";
				document.thisform.submit();
			}
		}

		function moveToDay()
		{
			if (confirm("Du kommer att flytta rutten till dagens datum. Är du säker?") == 1)
			{
				document.thisform.command.value = "moveToDay";
				document.thisform.submit();
			}
		}

		function reportJournal()
		{
			if (confirm("Du kommer att slutföra rutten. Är du säker?") == 1)
			{
				document.thisform.command.value = "reportJournal";
				document.thisform.submit();
			}
		}

		function openJournal()
		{
			if (confirm("Du kommer att öppna rutten. Är du säker?") == 1)
			{
				if (confirm("Skall rutten skickas ut till bilen igen?") == 1)
				{
					document.thisform.sendJournal.value = 1;
				}
				document.thisform.command.value = "openJournal";
				document.thisform.submit();
			}
		}
	
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" topmargin="10">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" action="linejournals_view.aspx" method="post" runat="server">
			<input type="hidden" name="command"> <input type="hidden" name="sendJournal"> <input type="hidden" name="lineJournalNo" value="<%= currentLineJournal.entryNo %>">
			<input type="hidden" name="mode">
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame">
				<tr>
					<td height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td class="activityName">Rutt
						<% if (currentLineJournal.entryNo > 0) Response.Write(currentLineJournal.entryNo); %>
					</td>
				</tr>			
				<tr>
					<td align="left" height="25"><% if (currentLineJournal.status < 8) { %><% if (currentOrganization.allowLineOrderSupervision) { %><% if (currentLineJournal.shipDate < DateTime.Today) { %><input type="button" onclick="moveToDay()" value="Flytta till idag" class="Button">&nbsp;<% } %><% if ((currentLineJournal.allOrdersLoaded(database)) && (currentLineJournal.status < 7)) { %><input type="button" onclick="unloadOrders()" value="Lossa containers" class="Button">&nbsp;<% } %><% if (currentLineJournal.status == 7) { %><input type="button" onclick="reportJournal()" value="Återrapportera rutt" class="Button">&nbsp;<% } %><input type="button" onclick="toggleForcedAssignment()" value="Tilldelning" class="Button">&nbsp;<% } %><input type="button" onclick="modifyJournal()" value="Ändra" class="Button"><% } %><% if (currentOrganization.allowLineOrderSupervision) { %><% if (currentLineJournal.status == 8) { %><input type="button" onclick="openJournal()" value="Återför rutt" class="Button">&nbsp;<input type="button" onclick="setInvoiceStatus()" value="Ändra fakturastatus" class="Button">&nbsp;<% } %><% } %>&nbsp;<input type="button" onclick="goBack()" value="Klar" class="Button"></td>
				</tr>
				
				<tr>
					<td>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<td class="interaction" valign="top" width="400">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Transportör</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentLineJournal.organizationNo %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Status</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentLineJournal.getStatusText(database) %></b>&nbsp;</td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Bil</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentLineJournal.agentCode %></b>&nbsp;</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="interaction" valign="top" width="400">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Hämtdatum</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentLineJournal.shipDate.ToString("yyyy-MM-dd") %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Utgår ifrån / Slutstation</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentLineJournal.departureFactoryCode %>
													/
													<%= currentLineJournal.arrivalFactoryCode %>
												</b>
											</td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Lastningsgrupp</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentLineJournal.getAgentStorageGroupDescription(database) %></b>&nbsp;</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Beräknad avfärdstid</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><% if (currentLineJournal.departureDateTime.ToString("yyyy") != "1753") Response.Write(currentLineJournal.departureDateTime.ToString("yyyy-MM-dd HH:mm")); %></b>&nbsp;</td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Beräknad ankomsttid</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><% if (currentLineJournal.departureDateTime.ToString("yyyy") != "1753") Response.Write(currentLineJournal.arrivalDateTime.ToString("yyyy-MM-dd HH:mm")); %></b>&nbsp;</td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Tilldelning</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><% if (currentLineJournal.forcedAssignment) Response.Write("Låst"); else Response.Write("Automatisk"); %></b>&nbsp;</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="interaction" valign="top" width="400">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Beräknad sträcka</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentLineJournal.calculatedDistance.ToString("0") %>
													km</b>&nbsp;</td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Körd / mätt sträcka</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentLineJournal.measuredDistance.ToString("0") %>
													km</b>&nbsp;</td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Rapporterad sträcka</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentLineJournal.reportedDistance.ToString("0") %>
													km</b>&nbsp;</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="interaction" valign="top" width="400">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Rapporterad sträcka (singel)</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentLineJournal.reportedDistanceSingle.ToString("0") %>
													km</b>&nbsp;</td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Rapporterad sträcka (bil+släp)</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentLineJournal.reportedDistanceTrailer.ToString("0") %>
													km</b>&nbsp;</td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Väntetid lossning</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentLineJournal.dropWaitTime %> min</b></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Status vågsystemet</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentLineJournal.getScaleStatus() %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Faktura mottagen</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentLineJournal.getInvoiceStatus() %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Attesterad av fabriken</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentLineJournal.getFactoryConfirmedWaitTime() %></b></td>
										</tr>
									</table>
								</td>
							</tr>							
						</table>
						<br>
						Linjeorder
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									&nbsp;</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Ordernr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Typ</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Transportör</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Transportdatum</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Namn</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Ort</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Containers</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Bekräftad till</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Rutt</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Status</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">
									Visa</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									&nbsp;</th>
							</tr>
							<%
								int i = 0;
								while (i < lineOrderDataSet.Tables[0].Rows.Count)
								{
									Navipro.SantaMonica.Common.LineOrder lineOrder = new Navipro.SantaMonica.Common.LineOrder(lineOrderDataSet.Tables[0].Rows[i]);
									
									string routeName = lineOrder.getRouteName(database);									
									string statusText = lineOrder.getStatusText();
									string statusIcon = lineOrder.getStatusIcon();
								
									string bgStyle = "";
									
									if ((i % 2) > 0)
									{
										bgStyle = "background-color: #e0e0e0;";
									}
								
									%>
							<tr>
								<td class="jobDescription" valign="top" align="center"><img src="images/<%= statusIcon %>" border="0" alt="<%= statusText %>"></td>
								<td class="jobDescription" valign="top"><%= lineOrder.entryNo.ToString() %></td>
								<td class="jobDescription" valign="top"><%= lineOrder.getType() %></td>
								<td class="jobDescription" valign="top"><%= lineOrder.organizationNo %></td>
								<td class="jobDescription" valign="top"><%= lineOrder.shipDate.ToString("yyyy-MM-dd") %></td>
								<td class="jobDescription" valign="top"><%= lineOrder.shippingCustomerName %></td>
								<td class="jobDescription" valign="top"><%= lineOrder.city %></td>
								<td class="jobDescription" valign="top"><%
										
											System.Data.DataSet containerDataSet = lineOrder.getContainers(database);
											int k = 0;
											while (k < containerDataSet.Tables[0].Rows.Count)
											{
												Navipro.SantaMonica.Common.LineOrderContainer lineOrderContainer = new Navipro.SantaMonica.Common.LineOrderContainer(containerDataSet.Tables[0].Rows[k]);
												Navipro.SantaMonica.Common.Category category = lineOrderContainer.getCategory(database);
												string categoryDesc = "";
												if (category != null) categoryDesc = category.description;
												
												Response.Write(lineOrderContainer.containerNo+": "+categoryDesc+" ("+lineOrderContainer.weight.ToString("0")+" kg)<br>");
											
												k++;											
											}
										
										%></td>
								<td class="jobDescription" valign="top"><% if (lineOrder.confirmedToDateTime.Year > 1753) Response.Write(lineOrder.confirmedToDateTime.ToString("yyyy-MM-dd HH:mm")); %></td>
								<td class="jobDescription" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<TR>
											<td class="jobDescription" valign="top"><%= routeName %></td>
											<% if (currentOrganization.allowLineOrderSupervision) { %>
											<td align="right" valign="top"><a href="lineorders_assign.aspx?lineOrderNo=<%= lineOrder.entryNo %>"><img src="images/button_assist.gif" border="0" alt="Tilldela rutt" width="12" height="13"></a></td>
											<% } %>
										</TR>
									</table>
								</td>
								<td class="jobDescription" valign="top"><%= statusText %></td>
								<td class="jobDescription" valign="top" align="center"><a href="lineorders_view.aspx?lineOrderNo=<%= lineOrder.entryNo.ToString() %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
								<td class="jobDescription" valign="top" align="center"><img src="images/<%= statusIcon %>" border="0" alt="<%= statusText %>"></td>
							</tr>
							<%
								
								
									i++;
								}
							
							
							%>
						</table>
						<br>
						Transaktioner
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Typ</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Containernr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Datum och klockslag</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Bil</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Utförd av</th>
							</tr>
							<%
														
								int j = 0;
								while (j < containerEntryDataSet.Tables[0].Rows.Count)
								{
									Navipro.SantaMonica.Common.ContainerEntry containerEntry = new Navipro.SantaMonica.Common.ContainerEntry(containerEntryDataSet.Tables[0].Rows[j]);
						
									
									%>
							<tr>
								<td class="jobDescription" valign="top"><%= containerEntry.getType() %></td>
								<td class="jobDescription" valign="top"><%= containerEntry.containerNo %></td>
								<td class="jobDescription" valign="top"><%= containerEntry.entryDateTime.ToString("yyyy-MM-dd HH:mm") %></td>
								<td class="jobDescription" valign="top"><%= containerEntry.sourceCode %></td>
								<td class="jobDescription" valign="top"><%= containerEntry.creatorNo %></td>
							</tr>
							<%
								
									j++;
								}

								if (j == 0)
								{
									%>
							<tr>
								<td class="jobDescription" colspan="5">Inga transaktioner registrerade.</td>
							</tr>
							<%
								}
							
							%>
						</table>
					</td>
				</tr>
				<tr>
					<td align="right" height="25"><% if (currentLineJournal.status < 8) { %><% if (currentOrganization.allowLineOrderSupervision) { %><% if (currentLineJournal.shipDate < DateTime.Today) { %><input type="button" onclick="moveToDay()" value="Flytta till idag" class="Button">&nbsp;<% } %><% if ((currentLineJournal.allOrdersLoaded(database)) && (currentLineJournal.status < 7)) { %><input type="button" onclick="unloadOrders()" value="Lossa containers" class="Button">&nbsp;<% } %><% if (currentLineJournal.status == 7) { %><input type="button" onclick="reportJournal()" value="Återrapportera rutt" class="Button">&nbsp;<% } %><input type="button" onclick="toggleForcedAssignment()" value="Tilldelning" class="Button">&nbsp;<% } %><input type="button" onclick="modifyJournal()" value="Ändra" class="Button"><% } %><% if (currentOrganization.allowLineOrderSupervision) { %><% if (currentLineJournal.status == 8) { %><input type="button" onclick="openJournal()" value="Återför rutt" class="Button">&nbsp;<input type="button" onclick="setInvoiceStatus()" value="Ändra fakturastatus" class="Button">&nbsp;<% } %><% } %>&nbsp;<input type="button" onclick="goBack()" value="Klar" class="Button"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
