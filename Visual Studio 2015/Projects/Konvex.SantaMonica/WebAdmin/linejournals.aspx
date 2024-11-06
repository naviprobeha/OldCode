<%@ Page language="c#" Codebehind="linejournals.aspx.cs" AutoEventWireup="false" Inherits="WebAdmin.linejournals" %>
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
		document.thisform.action = "linejournals.aspx";
		document.thisform.submit();
	
	}

	function gotoToday()
	{
		document.thisform.workDateYear.value = "<%= DateTime.Now.Year %>";
		document.thisform.workDateMonth.value = "<%= DateTime.Now.Month %>";
		document.thisform.workDateDay.value = "<%= DateTime.Now.ToString("dd") %>";
		document.thisform.noOfDaysBack.value = "0";
	
		document.thisform.command.value = "changeShipDate";
		document.thisform.action = "linejournals.aspx";
		document.thisform.submit();
	
	}
	
	</script>
	
	<body MS_POSITIONING="GridLayout" topmargin="10" onload="setTimeout('document.thisform.submit()', 60000)">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" method="post" runat="server" action="linejournals.aspx">
		<input type="hidden" name="command" value="">
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame">
				<tr>
					<td height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td class="activityName">Rutter</td>
				</tr>
				<tr>
					<td class="" width="95%"><% if (currentOrganization.allowLineOrderSupervision) { %>Transportör:&nbsp;<select name="organizationCode" class="Textfield" onchange="changeShipDate()">
					<option value="-">- Alla -</option>
					<%
						int z = 0;
						while (z < organizationDataSet.Tables[0].Rows.Count)
						{
							%><option value="<%= organizationDataSet.Tables[0].Rows[z].ItemArray.GetValue(0).ToString() %>" <% if (organizationCode == organizationDataSet.Tables[0].Rows[z].ItemArray.GetValue(0).ToString()) Response.Write("selected"); %>><%= organizationDataSet.Tables[0].Rows[z].ItemArray.GetValue(1).ToString() %></option><%
							z++;
						}
					%>
					</select>&nbsp;<% } %>Datum:&nbsp;<% WebAdmin.HTMLHelper.createDatePicker("workDate", toDate); %>&nbsp;Bakåt i tiden:&nbsp;<select name="noOfDaysBack" class="Textfield" onchange="document.thisform.submit()">
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
						<option value="">- Alla -</option>
						<%
							int j = 0;
							while (j < activeFactories.Tables[0].Rows.Count)
							{
								
								%>
								<option value="<%= activeFactories.Tables[0].Rows[j].ItemArray.GetValue(0).ToString() %>" <% if (Request["factory"] == activeFactories.Tables[0].Rows[j].ItemArray.GetValue(0).ToString()) Response.Write("selected"); %>><%= activeFactories.Tables[0].Rows[j].ItemArray.GetValue(1).ToString() %></option>													
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
									Transportör</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Transportdatum</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Bil</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Avgående</th>									
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Ankommande</th>									
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Ankommer</th>									
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Låst</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Order</th>
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
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><b><% if (lineJournal.forcedAssignment) Response.Write("Låst"); %>&nbsp;</b></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><b><%= lineJournal.getOrderCount(database) %></b></td>
										<td class="jobDescription" rowspan="2" valign="top" style="<%= bgStyle %>"><b><%= statusText %></b></td>
										<td class="jobDescription" rowspan="2" valign="top" align="center" style="<%= bgStyle %>"><a href="linejournals_view.aspx?parentPage=linejournals&lineJournalNo=<%= lineJournal.entryNo.ToString() %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
										<td class="jobDescription" rowspan="2" valign="top" align="center" style="<%= bgStyle %>"><img src="images/<%= statusIcon %>" border="0" alt="<%= statusText %>"></td>
									</tr>
									<%
											System.Data.DataSet containerDataSet = lineJournal.getContainers(database);
											System.Data.DataSet nonContainerDataSet = lineJournal.getNonContainerOrders(database);
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
												<td height="15" class="jobDescription" style="<%= bgStyle %>"><%= customerName %></td>
												</tr><%
											
												k++;											
											}

											k = 0;
											while (k < nonContainerDataSet.Tables[0].Rows.Count)
											{
												Navipro.SantaMonica.Common.LineOrder lineOrder = new Navipro.SantaMonica.Common.LineOrder(nonContainerDataSet.Tables[0].Rows[k]);
												
												string customerName = lineOrder.shippingCustomerName;
												if (customerName.Length > 20) customerName = customerName.Substring(0, 20)+"...";
												%><tr>
												<td height="15" class="jobDescription" style="<%= bgStyle %>"><%= customerName %></td>
												</tr><%
											
												k++;											
											}
											
										%></table>
										</td>
										<td class="jobDescription" colspan="5" valign="top" style="<%= bgStyle %>">
											<table cellspacing="0" cellpadding="0" width="100%" border="0">
											<%
										
											k = 0;
											while (k < containerDataSet.Tables[0].Rows.Count)
											{
												Navipro.SantaMonica.Common.LineOrderContainer lineOrderContainer = new Navipro.SantaMonica.Common.LineOrderContainer(containerDataSet.Tables[0].Rows[k]);
												Navipro.SantaMonica.Common.LineOrders lineOrders = new Navipro.SantaMonica.Common.LineOrders();
												Navipro.SantaMonica.Common.LineOrder lineOrder = lineOrders.getEntry(database, lineOrderContainer.lineOrderEntryNo.ToString());
												
												Navipro.SantaMonica.Common.Category category = lineOrderContainer.getCategory(database);
												string categoryDesc = "";
												if (category != null) categoryDesc = category.description;

												string serviceStyle = "";
												string postMortemImg = "";
												string bseTestingImg = "";
												
												Navipro.SantaMonica.Common.ContainerEntries containerEntries = new Navipro.SantaMonica.Common.ContainerEntries();
												System.Data.DataSet serviceDataSet = containerEntries.getServiceDataSet(database, lineOrderContainer.containerNo);
												if (serviceDataSet.Tables[0].Rows.Count > 0) serviceStyle = "color: red; font-weight: bold;";

												if (bseTestingList.Contains(lineOrderContainer.lineOrderEntryNo.ToString())) bseTestingImg = "<img src=\"images/ind_wide_red.gif\" border=\"0\" alt=\"Innehåller provtagningar\">";
												if (postMortemList.Contains(lineOrderContainer.lineOrderEntryNo.ToString())) postMortemImg = "<img src=\"images/ind_wide_yellow.gif\" border=\"0\" alt=\"Innehåller obduktioner\">";

												DateTime confirmedToDate = DateTime.Parse(containerDataSet.Tables[0].Rows[k].ItemArray.GetValue(10).ToString());
												DateTime confirmedToTime = DateTime.Parse(containerDataSet.Tables[0].Rows[k].ItemArray.GetValue(11).ToString());

												%><tr>
												
												<td height="15" class="jobDescription" style="<%= bgStyle+serviceStyle %>" width="250"><%= lineOrderContainer.containerNo+": "+lineOrderContainer.categoryCode+", "+categoryDesc %></td>
												<td class="jobDescription" style="<%= bgStyle %>" width="90"><%= lineOrderContainer.weight.ToString("0")+" kg" %></td>
												<td class="jobDescription" style="<%= bgStyle %>" width="150"><% if (confirmedToDate.Year > 1753) Response.Write(confirmedToDate.ToString("yyyy-MM-dd") + " " + confirmedToTime.ToString("HH:mm")); %></td>
												<td class="jobDescription" style="<%= bgStyle %>" nowrap><%= lineOrder.getStatusText() %></td>
												<td class="jobDescription" style="<%= bgStyle %>" align="right"><%= postMortemImg %>&nbsp;<%= bseTestingImg %>&nbsp;</td>
												<td class="jobDescription" style="<%= bgStyle %>" align="right" width="15"><a href="lineorders_view.aspx?lineOrderNo=<%= lineOrder.entryNo %>"><img src="images/button_assist.gif" border="0" alt="Visa linjeorder" width="12" height="13"></a></td>
											
												</tr><%
												
												k++;											
											}
											
											k = 0;
											while (k < nonContainerDataSet.Tables[0].Rows.Count)
											{
												Navipro.SantaMonica.Common.LineOrder lineOrder = new Navipro.SantaMonica.Common.LineOrder(nonContainerDataSet.Tables[0].Rows[k]);
												
												%><tr>
												
												<td height="15" class="jobDescription" style="<%= bgStyle %>" colspan="5" width="250"><%= lineOrder.comments %></td>											
												<td class="jobDescription" style="<%= bgStyle %>" align="right" width="15"><a href="lineorders_view.aspx?lineOrderNo=<%= lineOrder.entryNo %>"><img src="images/button_assist.gif" border="0" alt="Visa linjeorder" width="12" height="13"></a></td>
												</tr><%
												
												k++;											
											}											
											
											%></table>
										</td>
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
