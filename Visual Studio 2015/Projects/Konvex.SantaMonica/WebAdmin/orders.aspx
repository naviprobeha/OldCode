<%@ Page language="c#" Codebehind="orders.aspx.cs" AutoEventWireup="false" Inherits="Navipro.SantaMonica.WebAdmin._default" %>
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

	function retrieveNotLoadedOrders()
	{
		if (confirm("Du kommer att hämta olastade order till dagens datum. Vill du fortsätta?") == 1)
		{
			document.thisform.command.value = "retrieveNotLoadedOrders";
			document.thisform.submit();
		}
	
	}

	function changeOfficeMode()
	{
		document.thisform.command.value = "changeOfficeMode";
		document.thisform.submit();
	
	}

	function toggleInfo()
	{
		document.thisform.command.value = "toggleInfo";
		document.thisform.submit();
	
	}

	function toggleLoadedOrders()
	{
		document.thisform.command.value = "toggleLoadedOrders";
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
					<td class="activityName">Körorder</td>
				</tr>
				<tr>
					<td class="" width="95%"><% if (currentOrganization.callCenterMaster) { %>Transportör: <select name="organizationNo" class="Textfield" onchange="document.thisform.submit();">
											<option value="-">Alla</option>
											
											<%
												int o = 0;
												while (o < organizationDataSet.Tables[0].Rows.Count)
												{
													%><option value="<%= organizationDataSet.Tables[0].Rows[o].ItemArray.GetValue(0).ToString() %>" <% if (currentOrganizationNo == organizationDataSet.Tables[0].Rows[o].ItemArray.GetValue(0).ToString()) Response.Write("selected"); %>><%= organizationDataSet.Tables[0].Rows[o].ItemArray.GetValue(1).ToString() %></option><%
													
													o++;
												}
											%>												
											</select>&nbsp;<% } %>Bil:&nbsp;<select name="agent" class="Textfield" onchange="document.thisform.submit()">
					<option value="">- Alla -</option>
					<option value="-" <% if (Request["agent"] == "-") Response.Write("selected"); %>>- Otilldelade -</option>
					<%
						int j = 0;
						while (j < activeAgents.Tables[0].Rows.Count)
						{
							
							%>
							<option value="<%= activeAgents.Tables[0].Rows[j].ItemArray.GetValue(0).ToString() %>" <% if (Request["agent"] == activeAgents.Tables[0].Rows[j].ItemArray.GetValue(0).ToString()) Response.Write("selected"); %>><%= activeAgents.Tables[0].Rows[j].ItemArray.GetValue(0).ToString()+" "+activeAgents.Tables[0].Rows[j].ItemArray.GetValue(1).ToString() %></option>													
							<%
						
							j++;
						}
					
					
					%>
					</select>&nbsp;Datum:&nbsp;<% WebAdmin.HTMLHelper.createDatePicker("workDate", toDate); %>&nbsp;Bakåt i tiden:&nbsp;<select name="noOfDaysBack" class="Textfield" onchange="document.thisform.submit()">
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
					</select>&nbsp;<input type="button" value="<% if (showLoadedOrders) Response.Write("Dölj lastade"); else Response.Write("Visa lastade"); %>" onclick="toggleLoadedOrders()" class="Button"/>&nbsp;<input type="button" value="Idag" onclick="gotoToday()" class="Button">&nbsp;<input type="button" value="Tilldela flera" class="button" onclick="document.location.href='orders_assign_multi.aspx?fromDate=<%= fromDate.ToString("yyyy-MM-dd") %>&toDate=<%= toDate.ToString("yyyy-MM-dd") %>';">&nbsp;<input type="button" value="Olastade order" class="button" onclick="retrieveNotLoadedOrders();">&nbsp;<input type="button" value="Flytta fram" class="button" onclick="document.location.href='orders_move.aspx?fromDate=<%= fromDate.ToString("yyyy-MM-dd") %>&toDate=<%= toDate.ToString("yyyy-MM-dd") %>';">&nbsp;<input type="button" value="<% if (showInfo) Response.Write("Dölj info"); else Response.Write("Visa info"); %>" class="button" onclick="toggleInfo()">&nbsp;<input type="button" value="Ny order" class="button" onclick="document.location.href='customers.aspx';">&nbsp;<input type="button" value="<% if (currentOrganization.officeMode == 0) Response.Write("Normalläge"); else Response.Write("Kontorsläge"); %>" class="button" onclick="changeOfficeMode();">&nbsp;</td>
				</tr>
				<tr>
					<td>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									&nbsp;</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Ordernr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" nowrap>
									Anmäld</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Kundnr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Namn</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Adress</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Ort</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Telefonnr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Mobiltel</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									&nbsp;</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Status</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Bil</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">
									Visa</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									&nbsp;</th>
							</tr>
							<%
								int i = 0;
								while (i < activeShipOrders.Tables[0].Rows.Count)
								{
									string statusText = getStatusText((int)activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(8));
									string statusIcon = getStatusIcon((int)activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(8));
								
								    Navipro.SantaMonica.Common.ShipOrderLogLines shipOrderLogLines = new Navipro.SantaMonica.Common.ShipOrderLogLines();
								    if ((int)activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(8) == 0)
								    {
										if (shipOrderLogLines.checkAssigned(database, (int)activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(1)))
										{
											statusIcon = getStatusIcon(1);
										}
									}
								
									string bgStyle = "";
									
									if ((i % 2) > 0)
									{
										bgStyle = " style=\"background-color: #e0e0e0;\"";
									}

									string commentBooleanImg = "";
									if (activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(13).ToString() != "") commentBooleanImg = "<img src=\"images/info.gif\" border=\"0\" alt=\""+activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(13).ToString()+"\">";
									
									Navipro.SantaMonica.Common.Organizations organizations = new Navipro.SantaMonica.Common.Organizations();
									Navipro.SantaMonica.Common.Organization organization = organizations.getOrganization(database, activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(0).ToString());
									string organizationName = activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();
									if (organization != null)
									{
										organizationName = organization.name;
										if (organizationName.Length > 15) organizationName = organizationName.Substring(0, 15)+"...";
									}
									
									if (showInfo)
									{
										%>
										<tr>
											<td class="jobDescription" rowspan="2" valign="top" align="center" <%= bgStyle %>><img src="images/<%= statusIcon %>" border="0" alt="<%= statusText %>"></td>
											<td class="jobDescription" rowspan="2" valign="top" <%= bgStyle %>><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %></td>
											<td class="jobDescription" valign="top" <%= bgStyle %> nowrap><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(14).ToString().Substring(0, 10) %></td>
											<td class="jobDescription" valign="top" <%= bgStyle %>><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(2).ToString() %></td>
											<td class="jobDescription" valign="top" <%= bgStyle %>><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(3).ToString() %></td>
											<td class="jobDescription" valign="top" <%= bgStyle %>><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() %></td>
											<td class="jobDescription" valign="top" <%= bgStyle %>><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(6).ToString() %></td>
											<td class="jobDescription" valign="top" <%= bgStyle %>><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(7).ToString() %></td>
											<td class="jobDescription" valign="top" <%= bgStyle %>><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(10).ToString() %></td>
											<td class="jobDescription" valign="top" <%= bgStyle %> align="center"><%= commentBooleanImg %></td>
											<td class="jobDescription" rowspan="2" valign="top" <%= bgStyle %>><%= statusText %></td>
											<td class="jobDescription" rowspan="2" valign="top" <%= bgStyle %>>
												<table cellspacing="0" cellpadding="0" width="100%" border="0">
													<TR>
														<td class="jobDescription" <%= bgStyle %>><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(9).ToString() %></td>
														<td align="right"><a href="orders_assign.aspx?shipOrderNo=<%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
													</TR>
												</table>
											</td>
											<td class="jobDescription" rowspan="2" valign="top" align="center" <%= bgStyle %>><a href="orders_view.aspx?shipOrderNo=<%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
											<td class="jobDescription" rowspan="2" valign="top" align="center" <%= bgStyle %>><img src="images/<%= statusIcon %>" border="0" alt="<%= statusText %>"></td>
										</tr>
										<tr>
											<td class="jobDescription" colspan="2" valign="top" <%= bgStyle %>><%= organizationName %></td>
											<td class="jobDescription" colspan="3" valign="top" <%= bgStyle %>><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(12).ToString() %></td>
											<td class="jobDescription" colspan="3" valign="top" <%= bgStyle %>><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(13).ToString() %></td>
										</tr>
										<%
									}
									else
									{
										%>
										<tr>
											<td class="jobDescription" valign="top" align="center" <%= bgStyle %>><img src="images/<%= statusIcon %>" border="0" alt="<%= statusText %>"></td>
											<td class="jobDescription" valign="top" <%= bgStyle %>><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() + activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %></td>
											<td class="jobDescription" valign="top" <%= bgStyle %> nowrap><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(14).ToString().Substring(0, 10) %></td>
											<td class="jobDescription" valign="top" <%= bgStyle %>><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(2).ToString() %></td>
											<td class="jobDescription" valign="top" <%= bgStyle %>><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(3).ToString() %></td>
											<td class="jobDescription" valign="top" <%= bgStyle %>><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() %></td>
											<td class="jobDescription" valign="top" <%= bgStyle %>><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(6).ToString() %></td>
											<td class="jobDescription" valign="top" <%= bgStyle %>><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(7).ToString() %></td>
											<td class="jobDescription" valign="top" <%= bgStyle %>><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(10).ToString() %></td>
											<td class="jobDescription" valign="top" <%= bgStyle %> align="center"><%= commentBooleanImg %></td>
											<td class="jobDescription" valign="top" <%= bgStyle %>><%= statusText %></td>
											<td class="jobDescription" valign="top" <%= bgStyle %>>
												<table cellspacing="0" cellpadding="0" width="100%" border="0">
													<TR>
														<td class="jobDescription" <%= bgStyle %>><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(9).ToString() %></td>
														<td align="right"><a href="orders_assign.aspx?shipOrderNo=<%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
													</TR>
												</table>
											</td>
											<td class="jobDescription" valign="top" align="center" <%= bgStyle %>><a href="orders_view.aspx?shipOrderNo=<%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
											<td class="jobDescription" valign="top" align="center" <%= bgStyle %>><img src="images/<%= statusIcon %>" border="0" alt="<%= statusText %>"></td>
										</tr>
										<%
									
									}
								
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
