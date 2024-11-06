  <%@ Page language="c#" Codebehind="containers_view.aspx.cs" AutoEventWireup="false" Inherits="WebAdmin.containers_view" %>
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
	
			function changeContainerInfo()
			{
				document.location.href="containers_modify.aspx?containerNo=<%= currentContainer.no %>";
			
			}

			function changeShipDate()
			{
				document.thisform.submit();
			
			}

	
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" topmargin="10">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" name="thisform" method="post" runat="server">
			<input type="hidden" name="command"> <input type="hidden" name="containerNo" value="<%= currentContainer.no %>">
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame" style="BORDER-BOTTOM-WIDTH: 0px">
				<tr>
					<td colspan="2" height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td colspan="2" class="activityName">Container
						<%= currentContainer.no %>
					</td>
				</tr>
			</table>
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame" style="BORDER-TOP-WIDTH: 0px">
				<tr>
					<td width="50" valign="top"><iframe id="mapFrame" width="265" height="600" frameborder="no" scrolling="no" src="<%= mapServer.getUrl() %>" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"></iframe>
					</td>
					<td valign="top">
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Beskrivning</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentContainer.description %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Containertyp</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentContainer.containerTypeCode %></b></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Vikt</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentContainerType.weight.ToString("0") %>
													kg</b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Volym</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentContainerType.volume.ToString("0") %>
													kubikmeter</b></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Nuvarande positionstyp</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentContainer.getLocationType() %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Nuvarande position</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentContainer.getLocationName(database) %></b></td>
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
											<td class="interaction" height="20"><input type="button" value="Ändra containerinformation" onclick="changeContainerInfo()"
													class="Button"></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<br>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="frame" style="border: 0px">
							<tr>
								<td>Datum: <% WebAdmin.HTMLHelper.createDatePicker("containerDate", containerDate); %>&nbsp;&nbsp;Antal poster bakåt i tiden: <select name="noOfContainerRecords" class="Textfield" onchange="document.thisform.submit()">
									<option value="20" <% if (Request["noOfContainerRecords"] == "20") Response.Write("selected"); %>>20</option>
									<option value="40" <% if (Request["noOfContainerRecords"] == "40") Response.Write("selected"); %>>40</option>
									<option value="60" <% if (Request["noOfContainerRecords"] == "60") Response.Write("selected"); %>>60</option>
									<option value="80" <% if (Request["noOfContainerRecords"] == "80") Response.Write("selected"); %>>80</option>
									<option value="100" <% if (Request["noOfContainerRecords"] == "100") Response.Write("selected"); %>>100</option>
								</select></td>
							</tr>
						</table>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Källtyp</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Källa</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Typ</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Datum och klockslag</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Beräknad ankomst oml.</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Positionstyp</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Position</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Dokumenttyp</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Dokumentnr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Utförd av</th>
							</tr>
							<%
							int j=0;
							while(j < containerEntryDataSet.Tables[0].Rows.Count)
							{
								Navipro.SantaMonica.Common.ContainerEntry containerEntry = new Navipro.SantaMonica.Common.ContainerEntry(containerEntryDataSet.Tables[0].Rows[j]);
								
								%>
							<tr>
								<td class="jobDescription" valign="top"><%= containerEntry.getSourceType() %></td>
								<td class="jobDescription" valign="top"><%= containerEntry.sourceCode %></td>
								<td class="jobDescription" valign="top"><%= containerEntry.getType() %></td>
								<td class="jobDescription" valign="top"><%= containerEntry.entryDateTime.ToString("yyyy-MM-dd HH:mm") %></td>
								<td class="jobDescription" valign="top"><% if (containerEntry.type == 2) Response.Write(containerEntry.estimatedArrivalDateTime.ToString("yyyy-MM-dd HH:mm")); %></td>
								<td class="jobDescription" valign="top"><%= containerEntry.getLocationType() %></td>
								<td class="jobDescription" valign="top"><%= containerEntry.getLocationName(database) %></td>
								<td class="jobDescription" valign="top"><%= containerEntry.getDocumentType() %></td>
								<td class="jobDescription" valign="top">
									<table cellspacing="0" cellpadding="0" border="0" width="100%">
									<tr>
										<td class="jobDescription"><%= containerEntry.documentNo %></td>
										<% 
										
											if ((containerEntry.documentNo != "") && (containerEntry.documentType == 1)) { %><td align="right"><a href="lineOrders_view.aspx?lineOrderNo=<%= containerEntry.documentNo %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td><% } 
											if ((containerEntry.documentNo != "") && (containerEntry.documentType == 2)) { %><td align="right"><a href="lineJournals_view.aspx?lineJournalNo=<%= containerEntry.documentNo %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td><% } 
											
											
											
										%>
									</tr>
									</table>									
								<td class="jobDescription" valign="top"><%= containerEntry.creatorNo %></td>
							</tr>
							<%
								
								j++;
							}
							
							if (j == 0)
							{
								%>
							<tr>
								<td colspan="9" class="jobDescription">Inga transaktioner registrerade.</td>
							</tr>
							<%
								
							}
						%>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
