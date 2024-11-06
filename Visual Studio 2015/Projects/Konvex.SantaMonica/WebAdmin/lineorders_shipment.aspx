<%@ Page language="c#" Codebehind="lineorders_shipment.aspx.cs" AutoEventWireup="false" Inherits="WebAdmin.lineOrders_shipment" %>
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
		document.thisform.action = "lineorders_shipment.aspx";
		document.thisform.submit();
	
	}

	function gotoToday()
	{
		document.thisform.workDateYear.value = "<%= DateTime.Now.Year %>";
		document.thisform.workDateMonth.value = "<%= DateTime.Now.Month %>";
		document.thisform.workDateDay.value = "<%= DateTime.Now.ToString("dd") %>";
		document.thisform.noOfDaysBack.value = "0";
	
		document.thisform.command.value = "changeShipDate";
		document.thisform.action = "lineorders_shipment.aspx";
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
					<td class="activityName">Linjeorder</td>
				</tr>
				<tr>
					<td class="" width="95%">Datum:&nbsp;<% WebAdmin.HTMLHelper.createDatePicker("workDate", toDate); %>&nbsp;Bakåt i tiden:&nbsp;<select name="noOfDaysBack" class="Textfield" onchange="document.thisform.submit()">
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
					</select>&nbsp;<input type="button" value="Idag" onclick="gotoToday()" class="Button">&nbsp;<% if (currentOrganization.allowLineOrderSupervision) { %>&nbsp;<input type="button" value="Ny order" class="button" onclick="document.location.href='shippingCustomers.aspx';"><% } %></td>
				</tr>
				<tr>
					<td>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									&nbsp;</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Ordernr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Typ</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Tr. datum</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Tid</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Namn</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Ort</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Containers</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Transportör</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Rutt</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									&nbsp;</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Status</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">
									Visa</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									&nbsp;</th>
							</tr>
							<%
								int i = 0;
								while (i < activeLineOrders.Tables[0].Rows.Count)
								{
									Navipro.SantaMonica.Common.LineOrder lineOrder = new Navipro.SantaMonica.Common.LineOrder(activeLineOrders.Tables[0].Rows[i]);
									
									string routeName = lineOrder.getRouteName(database);									
									string statusText = lineOrder.getStatusText();
									string statusIcon = lineOrder.getStatusIcon();
								
									string bgStyle = "";
									
									if ((i % 2) > 0)
									{
										bgStyle = "background-color: #e0e0e0;";
									}
									
									string shipTime = "";
									if (lineOrder.shipTime.ToString("HH:mm:ss") != "00:00:00") shipTime = lineOrder.shipTime.ToString("HH:mm");
								
									string commentBooleanImg = "";
									if (lineOrder.comments != "") commentBooleanImg = "<img src=\"images/info.gif\" border=\"0\" alt=\""+lineOrder.comments+"\">";
								
								
									%>
									<tr>
										<td class="jobDescription" valign="top" align="center" style="<%= bgStyle %>"><img src="images/<%= statusIcon %>" border="0" alt="<%= statusText %>"></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><%= lineOrder.entryNo.ToString() %></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><%= lineOrder.getType() %></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><%= lineOrder.shipDate.ToString("yyyy-MM-dd") %></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><%= shipTime %></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><%= lineOrder.shippingCustomerName %></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><%= lineOrder.city %></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>" nowrap>
											<table cellspacing="0" cellpadding="0" width="100%" border="0">
											<%
										
											System.Data.DataSet containerDataSet = lineOrder.getContainers(database);
											int k = 0;
											while (k < containerDataSet.Tables[0].Rows.Count)
											{
												Navipro.SantaMonica.Common.LineOrderContainer lineOrderContainer = new Navipro.SantaMonica.Common.LineOrderContainer(containerDataSet.Tables[0].Rows[k]);
												Navipro.SantaMonica.Common.Category category = lineOrderContainer.getCategory(database);
												string categoryDesc = "";
												if (category != null) categoryDesc = category.description;

												string serviceStyle = "";
												Navipro.SantaMonica.Common.ContainerEntries containerEntries = new Navipro.SantaMonica.Common.ContainerEntries();
												System.Data.DataSet serviceDataSet = containerEntries.getServiceDataSet(database, lineOrderContainer.containerNo);
												if (serviceDataSet.Tables[0].Rows.Count > 0) serviceStyle = "color: red; font-weight: bold;";

												string testingImg = "";
												string postMortemImg = "";
												if (bseTestingList.Contains(lineOrder.entryNo.ToString()))
												{
													int testingCount = lineOrderContainer.countTestings(database);
													if (testingCount > 0) testingImg = "<img src=\"images/ind_wide_red.gif\" border=\"0\" alt=\"Antal för provtagning: "+testingCount+"\">";
												}
												if (postMortemList.Contains(lineOrder.entryNo.ToString())) 
												{
													if (lineOrderContainer.containsPostMortem(database)) postMortemImg = "<img src=\"images/ind_wide_yellow.gif\" border=\"0\" alt=\"Innehåller obduktioner\">";
												}
												
												%><tr>
												<td class="jobDescription" style="<%= bgStyle+serviceStyle %>"><%= lineOrderContainer.containerNo+": "+categoryDesc %></td>
												<td class="jobDescription" style="<%= bgStyle %>" align="right"><%= postMortemImg %>&nbsp;<%= testingImg %>&nbsp;</td>
												</tr><%
											
												k++;											
											}
										
											%></table>
										</td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><%= lineOrder.organizationNo %></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>">
											<table cellspacing="0" cellpadding="0" width="100%" border="0">
												<TR>
													<td class="jobDescription" style="<%= bgStyle %>" valign="top" nowrap><%= routeName %></td>
													<% if (currentOrganization.allowLineOrderSupervision) { %><td align="right" valign="top"><a href="lineorders_assign.aspx?lineOrderNo=<%= lineOrder.entryNo %>"><img src="images/button_assist.gif" border="0" alt="Tilldela rutt" width="12" height="13"></a></td><% } %>
												</TR>
											</table>
										</td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>" align="center"><%= commentBooleanImg %></td>									
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><%= statusText %></td>
										<td class="jobDescription" valign="top" align="center" style="<%= bgStyle %>"><a href="shippingForm_print.aspx?sid=<%= Session.SessionID %>&lineOrderEntryNo=<%= lineOrder.entryNo.ToString() %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
										<td class="jobDescription" valign="top" align="center" style="<%= bgStyle %>"><img src="images/<%= statusIcon %>" border="0" alt="<%= statusText %>"></td>
									</tr>
									<%
								
								
									i++;
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
