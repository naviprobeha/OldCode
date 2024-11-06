<%@ Page language="c#" Codebehind="lineorders_view.aspx.cs" AutoEventWireup="false" Inherits="WebAdmin.lineorders_view" %>
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
	
					
			function loadOrder()
			{
				if (confirm("Du kommer att lasta aktuell order. Är du säker?") == 1)
				{
					document.thisform.action = "lineorders_view.aspx";
					document.thisform.command.value = "loadOrder";
					document.thisform.submit();
				}
			}
					
			function setAutoPlan()
			{
				document.location.href = "lineorders_view.aspx?lineOrderNo=<%= currentLineOrder.entryNo %>&command=toggleAutoPlan";			
			}

			function addContainer()
			{
				
				document.thisform.action = "lineorders_view.aspx";
				document.thisform.command.value = "addContainer";
				document.thisform.submit();
			}

			</script>
		
		<% } %>
		
		<script>
		
		function goBack()
		{
			document.location.href='lineorders.aspx';		
		}

		function showDocument()
		{
			document.location.href='shippingForm_print.aspx?sid=<%= Session.SessionID %>&lineOrderEntryNo=<%= currentLineOrder.entryNo %>';		
		}
	
		</script>
		
	</HEAD>
	<body MS_POSITIONING="GridLayout" topmargin="10">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" action="orders_view.aspx" method="post" runat="server">
			<input type="hidden" name="command" value="">
			<input type="hidden" name="lineOrderNo" value="<%= currentLineOrder.entryNo %>"> 
			<input type="hidden" name="mode" value="">		
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame" style="border-bottom: 0 px">
				<tr>
					<td height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td class="activityName" colspan="2">Linjeorder <% if (currentLineOrder.entryNo > 0) Response.Write(currentLineOrder.entryNo); %></td>
				</tr>
				<tr>
					<td align="left" height="25" colspan="2"><% if (currentOrganization.allowLineOrderSupervision) { %><% if (currentLineOrder.status < 7) { %><input type="button" onclick="loadOrder()" value="Lasta containers" class="Button">&nbsp;<input type="button" onclick="setAutoPlan()" value="Planering" class="Button">&nbsp;<% } %><input type="button" onclick="modifyOrder()" value="Ändra" class="Button">&nbsp;<% } %><input type="button" onclick="showDocument()" value="Skriv ut" class="Button">&nbsp;<input type="button" onclick="goBack()" value="Klar" class="Button"></td>
				</tr>
			</table>
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame" style="border-top: 0 px">
				<tr>
					<td width="50" valign="top"><iframe id="mapFrame" width="265" height="100%" frameborder="no" scrolling="no" src="<%= mapServer.getUrl() %>" style="border: 1px #000000 solid;"></iframe></td>
					<td valign="top">
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<td class="interaction" valign="top" width="300">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Hämtdatum</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentLineOrder.shipDate.ToString("yyyy-MM-dd") %>&nbsp;<%= currentLineOrder.shipTime.ToString("HH:mm") %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" width="250">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Uppläggningsdatum</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><% if (currentLineOrder.creationDate.Year != 1753) Response.Write(currentLineOrder.creationDate.ToString("yyyy-MM-dd")); %></b>&nbsp;</td>
										</tr>
									</table>
								</td>								
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Bekräftad till datum och klockslag</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><% if (currentLineOrder.confirmedToDateTime.ToString("yyyy-MM-dd") != "1753-01-01") Response.Write(currentLineOrder.confirmedToDateTime.ToString("yyyy-MM-dd HH:mm")); %>&nbsp;</b></td>
										</tr>
									</table>
								</td>
							</tr>
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
							<tr>
								<td class="interaction" valign="top" width="300">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Typ</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentLineOrder.getType() %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" width="250">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Skapad genom</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentLineOrder.getCreatedByType() %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Skapad av</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentLineOrder.createdByCode %></b></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="interaction" valign="top" width="300">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Chaufför</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentLineOrder.driverName %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" width="250">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Ruttgrupp</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentLineOrder.routeGroupCode %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Rapporterad väntetid</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentLineOrder.loadWaitTime %> minuter</b></td>
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
											<td class="interaction" height="20"><b><%= currentLineOrder.details %></b></td>
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
							<tr>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Vägbeskrivning</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentLineOrder.directionComment+currentLineOrder.directionComment2 %></b></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<br>
						Transaktioner
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">Typ</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">Containernr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">Datum och klockslag</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">Bil</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">Utförd av</th>
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
									%><tr>
										<td class="jobDescription" colspan="5">Inga transaktioner registrerade.</td>
									</tr><%
								}
							
							%>
						</table>
						
						<% if (reasonReportedDataSet.Tables[0].Rows.Count > 0) { %>					
						<br>
						Uppföljning
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">Uppföljningskod</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">Rapporterad datum och klockslag</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">Rapporterad av</th>
							</tr>
							<%
														
								int k = 0;
								while (k < reasonReportedDataSet.Tables[0].Rows.Count)
								{
									Navipro.SantaMonica.Common.ReasonReportedLineOrder reasonReportedLineOrder = new Navipro.SantaMonica.Common.ReasonReportedLineOrder(reasonReportedDataSet.Tables[0].Rows[k]);
						
									
									%>
									<tr>
										<td class="jobDescription" valign="top"><%= reasonReportedLineOrder.reasonCode %></td>
										<td class="jobDescription" valign="top"><%= reasonReportedLineOrder.entryDateTime.ToString("yyyy-MM-dd HH:mm") %></td>
										<td class="jobDescription" valign="top"><%= reasonReportedLineOrder.operatorNo %></td>
									</tr>
									<%
								
									k++;
								}

							
							%>
						</table>
						<% } %>						
						<br>
						Containers
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">Containernr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">Kategori</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">Uppskattad vikt</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">Invägd vikt</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">Vågstatus</th>
								<% if (currentOrganization.allowLineOrderSupervision) { %><th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" width="50">Flytta följesedlar</th><% } %>
								<% if (currentOrganization.allowLineOrderSupervision) { %><th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" width="50">Ta bort</th><% } %>
							</tr>
							<%
						
								int i = 0;
								while (i < lineOrderContainerDataSet.Tables[0].Rows.Count)
								{
									Navipro.SantaMonica.Common.LineOrderContainer lineOrderContainer = new Navipro.SantaMonica.Common.LineOrderContainer(lineOrderContainerDataSet.Tables[0].Rows[i]);

									
									string weight = decimal.Round(decimal.Parse(lineOrderContainer.weight.ToString()), 2)+" kg";
									string realWeight = decimal.Round(decimal.Parse((lineOrderContainer.realWeight * 1000).ToString()), 2)+" kg";
									
									string categoryDesc = "";
									Navipro.SantaMonica.Common.Category category = lineOrderContainer.getCategory(database);
									if (category != null) categoryDesc = category.description;
							
									%>
									<tr>
										<td class="jobDescription" valign="top"><%= lineOrderContainer.containerNo %></td>
										<td class="jobDescription" valign="top"><%= categoryDesc %></td>
										<td class="jobDescription" valign="top" align="left"><%= weight %></td>
										<td class="jobDescription" valign="top" align="left"><%= realWeight %></td>
										<td class="jobDescription" valign="top" align="left"><%= lineOrderContainer.getScalingStatus(database, currentLineOrder) %></td>
										<% if (currentOrganization.allowLineOrderSupervision) { %><td class="jobDescription" valign="top" align="center"><a href="lineorders_move_cont.aspx?lineOrderNo=<%= Request["lineOrderNo"] %>&containerNo=<%= lineOrderContainer.containerNo %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td><% } %>
										<% if (currentOrganization.allowLineOrderSupervision) { %><td class="jobDescription" valign="top" align="center"><a href="lineorders_view.aspx?command=deleteContainer&lineOrderNo=<%= Request["lineOrderNo"] %>&entryNo=<%= lineOrderContainer.entryNo %>"><img src="images/button_delete.gif" border="0" width="12" height="13"></a></td><% } %>
									</tr>
									<%
								
									i++;
								}

								if (i == 0)
								{
									%><tr>
										<td class="jobDescription" colspan="11">Inga containers registrerade.</td>
									</tr><%
								}
							
							%>
						</table>
						
						<% if (currentOrganization.allowLineOrderSupervision) { %>						
							<br>
							<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
								<tr>
									<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">Container</th>
									<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">Kategori</th>
									<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">Uppskattad vikt</th>
									<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" width="100">Val</th>	
								</tr>
								<tr>
									<td class="jobDescription" valign="top"><select name="containerNo" class="Textfield">
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
									<td class="jobDescription" valign="top"><select name="categoryCode" class="Textfield">
										<%
											
											int categoryEntryNo = 0;
											
											while (categoryEntryNo < categoryDataSet.Tables[0].Rows.Count)
											{	
											
												%>
												<option value="<%= categoryDataSet.Tables[0].Rows[categoryEntryNo].ItemArray.GetValue(0).ToString() %>"><%= categoryDataSet.Tables[0].Rows[categoryEntryNo].ItemArray.GetValue(0).ToString() %>, <%= categoryDataSet.Tables[0].Rows[categoryEntryNo].ItemArray.GetValue(1).ToString() %></option>
												<%
													
												categoryEntryNo++;
											}
																																					
										%>
									</select></td>
									<td class="jobDescription" valign="top"><input type="text" name="weight" value="" class="Textfield" size="10"> kg</td>
									<td class="jobDescription" valign="top" align="center"><input type="button" value="Lägg till" class="button" onclick="addContainer()"></td>
								</tr>
								
							</table>
						<% } %>
							<br>
							<table cellspacing="1" cellpadding="2" border="0" width="100%">
							<tr>
								<td align="right" height="25"><% if (currentOrganization.allowLineOrderSupervision) { %><% if (currentLineOrder.status < 7) { %><input type="button" onclick="loadOrder()" value="Lasta containers" class="Button">&nbsp;<input type="button" onclick="setAutoPlan()" value="Planering" class="Button">&nbsp;<% } %><input type="button" onclick="modifyOrder()" value="Ändra" class="Button">&nbsp;<% } %><input type="button" onclick="showDocument()" value="Skriv ut" class="Button">&nbsp;<input type="button" onclick="goBack()" value="Klar" class="Button"></td>
							</tr>
							</table>							
					</td>
				</tr>
				</table>
				
			</table>
		</form>
	</body>
</HTML>