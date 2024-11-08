<%@ Page language="c#" Codebehind="factoryOperation.aspx.cs" AutoEventWireup="false" Inherits="WebAdmin.factoryOperation" %>
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
	
	<script>
	
	function changeShipDate()
	{
		document.thisform.command.value = "changeShipDate";
		document.thisform.submit();
	
	}

	function gotoToday()
	{
		document.thisform.workDateYear.value = "<%= DateTime.Now.Year %>";
		document.thisform.workDateMonth.value = "<%= DateTime.Now.Month %>";
		document.thisform.workDateDay.value = "<%= DateTime.Now.ToString("dd") %>";
		document.thisform.noOfDaysBack.value = "0";
	
		document.thisform.command.value = "changeShipDate";
		document.thisform.submit();
	
	}
	
	</script>
	
	<body MS_POSITIONING="GridLayout" topmargin="10" onload="setTimeout('document.thisform.submit()', 60000)">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" method="post" runat="server">
		<input type="hidden" name="command" value="">
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame">
				<tr>
					<td height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td class="activityName"><%= currentFactory.name %></td>
				</tr>
				<tr>
					<td class="" width="95%">Datum:&nbsp;<% WebAdmin.HTMLHelper.createDatePicker("workDate", toDate); %>&nbsp;Bak�t i tiden:&nbsp;<select name="noOfDaysBack" class="Textfield" onchange="document.thisform.submit()">
						<option value="0" <% if (Session["noOfDaysBack"].ToString() == "0") Response.Write("selected"); %>>0 dagar</option>
						<option value="1" <% if (Session["noOfDaysBack"].ToString() == "1") Response.Write("selected"); %>>1 dagar</option>
						<option value="2" <% if (Session["noOfDaysBack"].ToString() == "2") Response.Write("selected"); %>>2 dagar</option>
						<option value="3" <% if (Session["noOfDaysBack"].ToString() == "3") Response.Write("selected"); %>>3 dagar</option>
						<option value="4" <% if (Session["noOfDaysBack"].ToString() == "4") Response.Write("selected"); %>>4 dagar</option>
						<option value="5" <% if (Session["noOfDaysBack"].ToString() == "5") Response.Write("selected"); %>>5 dagar</option>
						<option value="6" <% if (Session["noOfDaysBack"].ToString() == "6") Response.Write("selected"); %>>6 dagar</option>
						<option value="7" <% if (Session["noOfDaysBack"].ToString() == "7") Response.Write("selected"); %>>7 dagar</option>
						<option value="8" <% if (Session["noOfDaysBack"].ToString() == "8") Response.Write("selected"); %>>8 dagar</option>
						<option value="9" <% if (Session["noOfDaysBack"].ToString() == "9") Response.Write("selected"); %>>9 dagar</option>
					</select>&nbsp;Fabrik:&nbsp;<select name="factory" class="Textfield" onchange="document.thisform.submit()">
						<%
							int j = 0;
							while (j < activeFactories.Tables[0].Rows.Count)
							{
								
								%>
								<option value="<%= activeFactories.Tables[0].Rows[j].ItemArray.GetValue(1).ToString() %>" <% if (currentFactory.no == activeFactories.Tables[0].Rows[j].ItemArray.GetValue(1).ToString()) Response.Write("selected"); %>><%= activeFactories.Tables[0].Rows[j].ItemArray.GetValue(2).ToString() %></option>													
								<%
							
								j++;
							}
						
						
						%>									
					</select>&nbsp;<input type="button" value="Idag" onclick="gotoToday()" class="Button"></td>
				</tr>
				<tr>
					<td>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									&nbsp;</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Nr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Transport�r</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Transportdatum</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Bil</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Avg�ende</th>									
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Ankommande</th>									
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Ankommer</th>									
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Status</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">
									Visa</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									&nbsp;</th>
							</tr>
							<%
								int i = 0;
								while (i < activeLineJournals.Tables[0].Rows.Count)
								{
									Navipro.SantaMonica.Common.LineJournal lineJournal = new Navipro.SantaMonica.Common.LineJournal(activeLineJournals.Tables[0].Rows[i]);
									
									string statusText = lineJournal.getStatusText(database);
									string statusIcon = lineJournal.getStatusIcon();
								
									string bgStyle = "";
									
									if ((i % 2) > 0)
									{
										bgStyle = "background-color: #e0e0e0;";
									}
									
									string arrivalTime = lineJournal.arrivalDateTime.ToString("yyyy-MM-dd HH:mm");
									if (lineJournal.arrivalDateTime.ToString("HH:mm") == "00:00") arrivalTime = "";
								
									%>
									<tr>
										<td class="jobDescription" rowspan="2" valign="top" align="center" style="<%= bgStyle %>"><img src="images/<%= statusIcon %>" border="0" alt="<%= statusText %>"></td>
										<td class="jobDescription" rowspan="2" valign="top" style="<%= bgStyle %>"><b><%= lineJournal.entryNo %></b></td>
										<td class="jobDescription" rowspan="2" valign="top" style="<%= bgStyle %>"><b><%= lineJournal.organizationNo %></b></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><b><%= lineJournal.shipDate.ToString("yyyy-MM-dd") %></b></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><b><%= lineJournal.agentCode %></b></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><b><%= lineJournal.departureFactoryCode %></b></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><b><%= lineJournal.arrivalFactoryCode %></b></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>" nowrap><b><%= arrivalTime %></b></td>
										<td class="jobDescription" rowspan="2" valign="top" style="<%= bgStyle %>"><b><%= statusText %></b></td>
										<td class="jobDescription" rowspan="2" valign="top" align="center" style="<%= bgStyle %>"><a href="factoryOperation_view.aspx?parentPage=linejournals&lineJournalNo=<%= lineJournal.entryNo.ToString() %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
										<td class="jobDescription" rowspan="2" valign="top" align="center" style="<%= bgStyle %>"><img src="images/<%= statusIcon %>" border="0" alt="<%= statusText %>"></td>
									</tr>
									<%
																	
										System.Data.DataSet containerDataSet = lineJournal.getContainers(database);
									%>
									<tr>
										<td class="jobDescription" colspan="2" valign="top" style="<%= bgStyle %>">
											<table cellspacing="0" cellpadding="0" width="100%" border="0">
											<%
										
											int k = 0;
											while (k < containerDataSet.Tables[0].Rows.Count)
											{
												string customerName = containerDataSet.Tables[0].Rows[k].ItemArray.GetValue(9).ToString();
												if (customerName.Length > 20) customerName = customerName.Substring(0, 20)+"...";

												%><tr>
												<td class="jobDescription" style="<%= bgStyle %>" height="15"><%= customerName %></td>
												</tr><%
											
												k++;											
											}
											
										%></table>
										</td>
										<td class="jobDescription" colspan="3" valign="top" style="<%= bgStyle %>"><table cellspacing="0" cellpadding="0" width="100%" border="0"><%
										
											k = 0;
											while (k < containerDataSet.Tables[0].Rows.Count)
											{
												Navipro.SantaMonica.Common.LineOrderContainer lineOrderContainer = new Navipro.SantaMonica.Common.LineOrderContainer(containerDataSet.Tables[0].Rows[k]);
												Navipro.SantaMonica.Common.Category category = lineOrderContainer.getCategory(database);
												string categoryDesc = "";
												if (category != null) categoryDesc = category.description;
												int testQuantity = lineOrderContainer.countTestings(database);
												string testing = "";
												if (testQuantity > 0) testing = "Provt: "+testQuantity;

												string serviceStyle = "";
												Navipro.SantaMonica.Common.ContainerEntries containerEntries = new Navipro.SantaMonica.Common.ContainerEntries();
												System.Data.DataSet serviceDataSet = containerEntries.getServiceDataSet(database, lineOrderContainer.containerNo);
												if (serviceDataSet.Tables[0].Rows.Count > 0) serviceStyle = "color: red; font-weight: bold;";

												string postMortemImg = "";
												string bseTestingImg = "";

												if (bseTestingList.Contains(lineOrderContainer.lineOrderEntryNo.ToString())) bseTestingImg = "<img src=\"images/ind_wide_red.gif\" border=\"0\" alt=\"Inneh�ller provtagningar\">";
												if (postMortemList.Contains(lineOrderContainer.lineOrderEntryNo.ToString())) postMortemImg = "<img src=\"images/ind_wide_yellow.gif\" border=\"0\" alt=\"Inneh�ller obduktioner\">";
												

												%><tr>
													<td class="jobDescription" height="15" style="<%= bgStyle+serviceStyle %>"><%= lineOrderContainer.containerNo %></td>
													<td class="jobDescription" style="<%= bgStyle+serviceStyle %>"><%= lineOrderContainer.categoryCode+", "+categoryDesc %></td>
													<td class="jobDescription" style="<%= bgStyle %>" align="right"><%
													
														if (lineOrderContainer.isServiceReported(database))
														{
															Response.Write("&nbsp;");
														}
														else
														{
															Response.Write("<a style=\"color: red; font-size: 10px;\" href=\"factoryOperation.aspx?command=reportService&containerNo="+lineOrderContainer.containerNo+"\">Rapportera service</a>&nbsp;&nbsp;&nbsp;");
														}
													
													%></td>
													<td class="jobDescription" style="<%= bgStyle %>" width="70"><%= lineOrderContainer.weight.ToString("0")+" kg" %></td>
													<td class="jobDescription" style="<%= bgStyle %>" width="70"><%= testing %></td>
													<td class="jobDescription" style="<%= bgStyle %>" width="70" align="right"><%= postMortemImg %>&nbsp;<%= bseTestingImg %>&nbsp;</td>
													<td class="jobDescription" style="<%= bgStyle %>" align="right" width="20"><% if ((testing != "") || (postMortemImg != "")) { %><a href="factoryOperation_container.aspx?lineOrderNo=<%= lineOrderContainer.lineOrderEntryNo %>&containerNo=<%= lineOrderContainer.containerNo %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a><% } %>&nbsp;</td>
												</tr><%												
												
											
												k++;											
											}
										
										%></table></td>
									</tr>
									<%
								
								
									i++;
								}
							
								if (i == 0)
								{
									%>
									<tr>
										<td class="jobDescription" colspan="13">Inga rutter registrerade.</td>
									</tr>
									<%
								}
							
							%>
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									&nbsp;</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Nr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Transport�r</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Transportdatum</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Bil</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Avg�ende</th>									
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Ankommande</th>									
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Ankommer</th>									
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Status</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">
									Visa</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									&nbsp;</th>
							</tr>
							<%
								i = 0;
								while (i < reportedLineJournals.Tables[0].Rows.Count)
								{
									Navipro.SantaMonica.Common.LineJournal lineJournal = new Navipro.SantaMonica.Common.LineJournal(reportedLineJournals.Tables[0].Rows[i]);
									
									string statusText = lineJournal.getStatusText(database);
									string statusIcon = lineJournal.getStatusIcon();
								
									string bgStyle = "";
									
									if ((i % 2) > 0)
									{
										bgStyle = "background-color: #e0e0e0;";
									}
									
									string arrivalTime = lineJournal.arrivalDateTime.ToString("yyyy-MM-dd HH:mm");
									if (lineJournal.arrivalDateTime.ToString("HH:mm") == "00:00") arrivalTime = "";
								
									%>
									<tr>
										<td class="jobDescription" rowspan="2" valign="top" align="center" style="<%= bgStyle %>"><img src="images/<%= statusIcon %>" border="0" alt="<%= statusText %>"></td>
										<td class="jobDescription" rowspan="2" valign="top" style="<%= bgStyle %>"><b><%= lineJournal.entryNo %></b></td>
										<td class="jobDescription" rowspan="2" valign="top" style="<%= bgStyle %>"><b><%= lineJournal.organizationNo %></b></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><b><%= lineJournal.shipDate.ToString("yyyy-MM-dd") %></b></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><b><%= lineJournal.agentCode %></b></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><b><%= lineJournal.departureFactoryCode %></b></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><b><%= lineJournal.arrivalFactoryCode %></b></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>" nowrap><b><%= arrivalTime %></b></td>
										<td class="jobDescription" rowspan="2" valign="top" style="<%= bgStyle %>"><b><%= statusText %></b></td>
										<td class="jobDescription" rowspan="2" valign="top" align="center" style="<%= bgStyle %>"><a href="factoryOperation_view.aspx?parentPage=linejournals&lineJournalNo=<%= lineJournal.entryNo.ToString() %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
										<td class="jobDescription" rowspan="2" valign="top" align="center" style="<%= bgStyle %>"><img src="images/<%= statusIcon %>" border="0" alt="<%= statusText %>"></td>
									</tr>
									<%
																	
										System.Data.DataSet containerDataSet = lineJournal.getContainers(database);
									%>
									<tr>
										<td class="jobDescription" colspan="2" valign="top" style="<%= bgStyle %>">
											<table cellspacing="0" cellpadding="0" width="100%" border="0">
											<%
										
											int k = 0;
											while (k < containerDataSet.Tables[0].Rows.Count)
											{
												%><tr>
												<td class="jobDescription" style="<%= bgStyle %>" height="15"><%= containerDataSet.Tables[0].Rows[k].ItemArray.GetValue(9).ToString() %></td>
												</tr><%
											
												k++;											
											}
											
										%></table>
										</td>
										<td class="jobDescription" colspan="3" valign="top" style="<%= bgStyle %>"><table cellspacing="0" cellpadding="0" width="100%" border="0"><%
										
											k = 0;
											while (k < containerDataSet.Tables[0].Rows.Count)
											{
												Navipro.SantaMonica.Common.LineOrderContainer lineOrderContainer = new Navipro.SantaMonica.Common.LineOrderContainer(containerDataSet.Tables[0].Rows[k]);
												Navipro.SantaMonica.Common.Category category = lineOrderContainer.getCategory(database);
												string categoryDesc = "";
												if (category != null) categoryDesc = category.description;
												int testQuantity = lineOrderContainer.countTestings(database);
												string testing = "";
												if (testQuantity > 0) testing = "Provt: "+testQuantity;

												string serviceStyle = "";
												Navipro.SantaMonica.Common.ContainerEntries containerEntries = new Navipro.SantaMonica.Common.ContainerEntries();
												System.Data.DataSet serviceDataSet = containerEntries.getServiceDataSet(database, lineOrderContainer.containerNo);
												if (serviceDataSet.Tables[0].Rows.Count > 0) serviceStyle = "color: red; font-weight: bold;";

												string postMortemImg = "";
												string bseTestingImg = "";

												if (bseTestingList.Contains(lineOrderContainer.lineOrderEntryNo.ToString())) bseTestingImg = "<img src=\"images/ind_wide_red.gif\" border=\"0\" alt=\"Inneh�ller provtagningar\">";
												if (postMortemList.Contains(lineOrderContainer.lineOrderEntryNo.ToString())) postMortemImg = "<img src=\"images/ind_wide_yellow.gif\" border=\"0\" alt=\"Inneh�ller obduktioner\">";


												%><tr>
													<td class="jobDescription" height="15" style="<%= bgStyle+serviceStyle %>"><%= lineOrderContainer.containerNo %></td>
													<td class="jobDescription" style="<%= bgStyle+serviceStyle %>"><%= lineOrderContainer.categoryCode+", "+categoryDesc %></td>
													<td class="jobDescription" style="<%= bgStyle %>" align="right"><%
													
														if (lineOrderContainer.isServiceReported(database))
														{
															Response.Write("&nbsp;");
														}
														else
														{
															Response.Write("<a style=\"color: red; font-size: 10px;\" href=\"factoryOperation.aspx?command=reportService&containerNo="+lineOrderContainer.containerNo+"\">Rapportera service</a>&nbsp;&nbsp;&nbsp;");
														}
													
													%></td>
													<td class="jobDescription" style="<%= bgStyle %>" width="70"><%= (lineOrderContainer.realWeight * 1000).ToString("0")+" kg" %></td>
													<td class="jobDescription" style="<%= bgStyle %>" width="70"><%= testing %></td>
													<td class="jobDescription" style="<%= bgStyle %>" width="70" align="right"><%= postMortemImg %>&nbsp;<%= bseTestingImg %>&nbsp;</td>
													<td class="jobDescription" style="<%= bgStyle %>" align="right" width="20"><% if ((testing != "") || (postMortemImg != "")) { %><a href="factoryOperation_container.aspx?lineOrderNo=<%= lineOrderContainer.lineOrderEntryNo %>&containerNo=<%= lineOrderContainer.containerNo %>"><img src="images/button_assist.gif" border="0" alt="Visa inneh�ll" width="12" height="13"></a><% } %>&nbsp;</td>
												</tr><%												
												
											
												k++;											
											}
										
										%></table></td>
									</tr>
									<%
								
								
									i++;
								}
							
								if (i == 0)
								{
									%>
									<tr>
										<td class="jobDescription" colspan="13">Inga �terrapporterade rutter registrerade.</td>
									</tr>
									<%
								}
							
							%>
						</table>						
					</td>
				</tr>
				<tr>
					<td align="right">&nbsp;</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
