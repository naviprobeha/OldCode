<%@ Page language="c#" Codebehind="factoryOperation_view.aspx.cs" AutoEventWireup="false" Inherits="WebAdmin.factoryOperation_view" %>
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

		function goBack()
		{
			document.location.href='factoryOperation.aspx?factory=<%= currentLineJournal.departureFactoryCode %>';		
		}


		</script>
		
	</HEAD>
	<body MS_POSITIONING="GridLayout" topmargin="10">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" action="factoryOperation_view.aspx" method="post" runat="server">
			<input type="hidden" name="command" value="">
			<input type="hidden" name="lineJournalNo" value="<%= currentLineJournal.entryNo %>"> 
			<input type="hidden" name="mode" value="">		
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame">
				<tr>
					<td height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td class="activityName">Rutt <% if (currentLineJournal.entryNo > 0) Response.Write(currentLineJournal.entryNo); %></td>
				</tr>
				<tr>
					<td>
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
											<td class="interaction" height="20"><b><% if (currentLineJournal.forcedAssignment) Response.Write("Tvingad"); else Response.Write("Automatisk"); %></b>&nbsp;</td>
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
									Provtagn.</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Service</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Rutt</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Status</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">
									Skriv ut</th>									
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
										<td class="jobDescription" valign="top" align="center" style="<%= bgStyle %>"><img src="images/<%= statusIcon %>" border="0" alt="<%= statusText %>"></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><%= lineOrder.entryNo.ToString() %></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><%= lineOrder.getType() %></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><%= lineOrder.organizationNo %></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><%= lineOrder.shipDate.ToString("yyyy-MM-dd") %></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><%= lineOrder.shippingCustomerName %></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><%= lineOrder.city %></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><%
										
											System.Data.DataSet containerDataSet = lineOrder.getContainers(database);
											int k = 0;
											while (k < containerDataSet.Tables[0].Rows.Count)
											{
												Navipro.SantaMonica.Common.LineOrderContainer lineOrderContainer = new Navipro.SantaMonica.Common.LineOrderContainer(containerDataSet.Tables[0].Rows[k]);
												Navipro.SantaMonica.Common.Category category = lineOrderContainer.getCategory(database);
												string categoryDesc = "";
												if (category != null) categoryDesc = category.description;
												
												
												if (lineOrderContainer.isServiceReported(database))
												{
													Response.Write("<span style=\"color: red;\">"+lineOrderContainer.containerNo+": "+categoryDesc+" ("+lineOrderContainer.weight.ToString("0")+" kg)</span><br>");
												}
												else
												{
													Response.Write(lineOrderContainer.containerNo+": "+categoryDesc+" ("+lineOrderContainer.weight.ToString("0")+" kg)<br>");
												}
																							
												k++;											
											}
										
										%></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><table cellspacing="0" cellpadding="0" border="0" width="100%"><%
										
											System.Data.DataSet containerTestDataSet = lineOrder.getContainers(database);
											
											k = 0;
											while (k < containerTestDataSet.Tables[0].Rows.Count)
											{
												Navipro.SantaMonica.Common.LineOrderContainer lineOrderContainer = new Navipro.SantaMonica.Common.LineOrderContainer(containerTestDataSet.Tables[0].Rows[k]);
												
												int testQty = lineOrderContainer.countTestings(database);
												if (testQty > 0) 
												{
													%><tr><td class="jobDescription" style="<%= bgStyle %>"><%= lineOrderContainer.containerNo+" ("+testQty+" st)" %></td><td><a href="factoryOperation_container.aspx?lineOrderNo=<%= lineOrder.entryNo %>&containerNo=<%= lineOrderContainer.containerNo %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td></tr><%
												}
												k++;											
											}
										
										%></table></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><table cellspacing="0" cellpadding="0" border="0" width="100%"><%
										
										
											k = 0;
											while (k < containerDataSet.Tables[0].Rows.Count)
											{
												Navipro.SantaMonica.Common.LineOrderContainer lineOrderContainer = new Navipro.SantaMonica.Common.LineOrderContainer(containerDataSet.Tables[0].Rows[k]);
												
												if (lineOrderContainer.isServiceReported(database))
												{
													Response.Write("<span style=\"color: red;\">&nbsp;</span><br>");
												}
												else
												{
													Response.Write("<a style=\"color: red; font-size: 10px;\" href=\"factoryOperation_view.aspx?parentPage=linejournals&lineJournalNo="+Request["lineJournalNo"]+"&command=reportService&containerNo="+lineOrderContainer.containerNo+"\">Rapportera</a><br>");
												}
												k++;											
											}
										
										%></table></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><%= routeName %></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><%= statusText %></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"align="center"><a href="shippingForm_print.aspx?sid=<%= Session.SessionID %>&lineOrderEntryNo=<%= lineOrder.entryNo.ToString() %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
										<td class="jobDescription" valign="top" align="center" style="<%= bgStyle %>"><img src="images/<%= statusIcon %>" border="0" alt="<%= statusText %>"></td>
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
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">Typ</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">Containernr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">Datum och klockslag</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">Bil</th>
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
									</tr>
									<%
								
									j++;
								}

								if (j == 0)
								{
									%><tr>
										<td class="jobDescription" colspan="4">Inga transaktioner registrerade.</td>
									</tr><%
								}
							
							%>
						</table>
											
					</td>
				</tr>
				<tr>
					<td align="right" height="25"><input type="button" onclick="goBack()" value="Klar" class="Button"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>