<%@ Page language="c#" Codebehind="shipments.aspx.cs" AutoEventWireup="false" Inherits="Navipro.SantaMonica.WebAdmin.shipments" %>
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
		//document.thisform.command.value = "changeShipDate";
		//document.thisform.submit();
	
	}

	function submitSearch()
	{
		document.thisform.command.value = "changeShipDate";
		document.thisform.submit();
	
	}
		
	function toggleInfo()
	{
		document.thisform.command.value = "toggleInfo";
		document.thisform.submit();
	
	}

	</script>
	
	<body MS_POSITIONING="GridLayout" topmargin="10" onload="setTimeout('document.thisform.submit()', 90000)">
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
					<td class="activityName">Registrerade följesedlar</td>
				</tr>
				<tr>
					<td class="">Datumintervall:&nbsp;<% WebAdmin.HTMLHelper.createDatePicker("startDate", startDate); %>&nbsp;-&nbsp;<% WebAdmin.HTMLHelper.createDatePicker("endDate", endDate); %>&nbsp;Bil:&nbsp;<select name="agent" class="Textfield">
					<option value="">- Alla -</option>
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
					</select>&nbsp;Container:&nbsp;<select name="container" class="Textfield">
					<option value="">- Alla -</option>
					<%
						int k = 0;
						while (k < activeContainers.Tables[0].Rows.Count)
						{
							
							%>
							<option value="<%= activeContainers.Tables[0].Rows[k].ItemArray.GetValue(0).ToString() %>" <% if (Request["container"] == activeContainers.Tables[0].Rows[k].ItemArray.GetValue(0).ToString()) Response.Write("selected"); %>><%= activeContainers.Tables[0].Rows[k].ItemArray.GetValue(0).ToString() %></option>													
							<%
						
							k++;
						}
					
					
					%>
					</select>&nbsp;
					<%
						if (currentOrganization.callCenterMaster)
						{
							%>
									Transportör: <select name="organizationNo" class="Textfield">
								
									<%
										int t = 0;
										while (t < organizationDataSet.Tables[0].Rows.Count)
										{
											%><option value="<%= organizationDataSet.Tables[0].Rows[t].ItemArray.GetValue(0).ToString() %>" <% if (selectedOrganizationNo == organizationDataSet.Tables[0].Rows[t].ItemArray.GetValue(0).ToString()) Response.Write("selected"); %>><%= organizationDataSet.Tables[0].Rows[t].ItemArray.GetValue(1).ToString() %></option><%
											
											t++;
										}
									%>												
									</select>
							<%
						}
					%>							
					
					
					<input type="button" value="Sök" class="Button" onclick="submitSearch()">&nbsp;&nbsp;<input type="button" value="Avräkning" class="button" onclick="document.location.href='shipments_summary.aspx?startDate=<%= startDate.ToString("yyyy-MM-dd") %>&endDate=<%= endDate.ToString("yyyy-MM-dd") %>&agent=<%= Request["agent"] %>&organizationNo=<%= selectedOrganizationNo %>';">&nbsp;<input type="button" value="<% if (showInfo) Response.Write("Dölj info"); else Response.Write("Visa info"); %>" class="button" onclick="toggleInfo()">
			
					</td>
				</tr>
				<tr>
					<td>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Följesedelsnr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Körordernr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Kundnr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Namn</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Namn (Hämtning)</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Adress (Hämtning)</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Ort (Hämtning)</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Datum</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Linjeordernr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">
									Status</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									&nbsp;</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									&nbsp;</th>
							</tr>
							<%
								int i = 0;
								while (i < activeShipments.Tables[0].Rows.Count)
								{
								
									string bgStyle = "";
									
									if ((i % 2) > 0)
									{
										bgStyle = " style=\"background-color: #e0e0e0;\"";
									}
								
									string status = "";
									string icon = "";
									if (activeShipments.Tables[0].Rows[i].ItemArray.GetValue(8).ToString() == "0")
									{
										status = "";
										icon = "ind_white.gif";
									}
									if (activeShipments.Tables[0].Rows[i].ItemArray.GetValue(8).ToString() == "1")
									{
										status = "Köad";
										icon = "ind_white.gif";
									}
									if (activeShipments.Tables[0].Rows[i].ItemArray.GetValue(8).ToString() == "2")
									{
										status = "Skickad";
										icon = "ind_yellow.gif";
									}
									if (activeShipments.Tables[0].Rows[i].ItemArray.GetValue(8).ToString() == "3") 
									{
										status = "Bekräftad";
										icon = "ind_green.gif";
									}

									string shipOrderNo = "";
									string shipOrderLink = "";
									string lineOrderNo = "";
									string customerNo = "";
									string customerLink = "";

									if (int.Parse(activeShipments.Tables[0].Rows[i].ItemArray.GetValue(9).ToString()) > 0) 
									{
										shipOrderNo = activeShipments.Tables[0].Rows[i].ItemArray.GetValue(9).ToString();
										shipOrderLink = "<a href=\"orders_view.aspx?shipOrderNo="+activeShipments.Tables[0].Rows[i].ItemArray.GetValue(9).ToString() +"\"><img src=\"images/button_assist.gif\" border=\"0\"></a>";
									}

									if (int.Parse(activeShipments.Tables[0].Rows[i].ItemArray.GetValue(13).ToString()) > 0) 
									{
										lineOrderNo = activeShipments.Tables[0].Rows[i].ItemArray.GetValue(13).ToString()+" ("+activeShipments.Tables[0].Rows[i].ItemArray.GetValue(14).ToString()+")";
										//lineOrderLink = "<a href=\"orders_view.aspx?shipOrderNo="+activeShipments.Tables[0].Rows[i].ItemArray.GetValue(9).ToString() +"\"><img src=\"images/button_assist.gif\" border=\"0\"></a>";
									}
									
									if (activeShipments.Tables[0].Rows[i].ItemArray.GetValue(2).ToString() != "") 
									{
										customerNo = activeShipments.Tables[0].Rows[i].ItemArray.GetValue(2).ToString();
										customerLink = "<a href=\"customers_view.aspx?customerNo="+activeShipments.Tables[0].Rows[i].ItemArray.GetValue(2).ToString() +"\"><img src=\"images/button_assist.gif\" border=\"0\"></a>";
									}
							
									if (showInfo)
									{
										%>
										<tr>
											<td class="jobDescription" rowspan="2" valign="top" <%= bgStyle %>><%= activeShipments.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %></td>
											<td class="jobDescription" rowspan="2" valign="top" <%= bgStyle %>><table cellspacing="0" cellpadding="0" border="0"><tr><td class="jobDescription" <%= bgStyle %>><%= shipOrderNo %>&nbsp;</td><td <%= bgStyle %>><%= shipOrderLink %></td></tr></table></td>
											<td class="jobDescription" rowspan="2" valign="top" <%= bgStyle %>><table cellspacing="0" cellpadding="0" border="0"><tr><td class="jobDescription" <%= bgStyle %>><%= customerNo %>&nbsp;</td><td <%= bgStyle %>><%= customerLink %></td></tr></table></td>
											<td class="jobDescription" valign="top" <%= bgStyle %>><%= activeShipments.Tables[0].Rows[i].ItemArray.GetValue(3).ToString() %></td>
											<td class="jobDescription" valign="top" <%= bgStyle %>><%= activeShipments.Tables[0].Rows[i].ItemArray.GetValue(10).ToString() %></td>
											<td class="jobDescription" valign="top" <%= bgStyle %>><%= activeShipments.Tables[0].Rows[i].ItemArray.GetValue(11).ToString() %></td>
											<td class="jobDescription" valign="top" <%= bgStyle %>><%= activeShipments.Tables[0].Rows[i].ItemArray.GetValue(12).ToString() %></td>
											<td class="jobDescription" rowspan="2" valign="top" <%= bgStyle %>><%= activeShipments.Tables[0].Rows[i].ItemArray.GetValue(6).ToString().Substring(0, 10) %></td>
											<td class="jobDescription" rowspan="2" valign="top" align="left" <%= bgStyle %>><%= lineOrderNo %></td>
											<td class="jobDescription" rowspan="2" valign="top" align="center" <%= bgStyle %>><%= status %></td>
											<td class="jobDescription" rowspan="2" valign="top" align="center" <%= bgStyle %>><a href="shipments_view.aspx?no=<%= activeShipments.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
											<td class="jobDescription" rowspan="2" valign="top" align="center" <%= bgStyle %>><img src="images/<%= icon %>" alt="<%= status %>" border="0"></td>
										</tr>
										<tr>
											<td class="jobDescription" colspan="4" <%= bgStyle %>><%= shipmentsClass.getShipmentContent(database, activeShipments.Tables[0].Rows[i].ItemArray.GetValue(1).ToString()).Replace("(R)", "<font style=\"color: red;\"><b>(R)</b></font>") %>&nbsp;</td>
										</tr>
										<%
									}
									else
									{
										%>
										<tr>
											<td class="jobDescription" valign="top" <%= bgStyle %>><%= activeShipments.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %></td>
											<td class="jobDescription" valign="top" <%= bgStyle %>><%= shipOrderNo %></td>
											<td class="jobDescription" valign="top" <%= bgStyle %>><%= activeShipments.Tables[0].Rows[i].ItemArray.GetValue(2).ToString() %></td>
											<td class="jobDescription" valign="top" <%= bgStyle %>><%= activeShipments.Tables[0].Rows[i].ItemArray.GetValue(3).ToString() %></td>
											<td class="jobDescription" valign="top" <%= bgStyle %>><%= activeShipments.Tables[0].Rows[i].ItemArray.GetValue(10).ToString() %></td>
											<td class="jobDescription" valign="top" <%= bgStyle %>><%= activeShipments.Tables[0].Rows[i].ItemArray.GetValue(11).ToString() %></td>
											<td class="jobDescription" valign="top" <%= bgStyle %>><%= activeShipments.Tables[0].Rows[i].ItemArray.GetValue(12).ToString() %></td>
											<td class="jobDescription" valign="top" <%= bgStyle %>><%= activeShipments.Tables[0].Rows[i].ItemArray.GetValue(6).ToString().Substring(0, 10) %></td>
											<td class="jobDescription" valign="top" align="center" <%= bgStyle %>><%= status %></td>
											<td class="jobDescription" valign="top" align="center" <%= bgStyle %>><a href="shipments_view.aspx?no=<%= activeShipments.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
											<td class="jobDescription" valign="top" align="center" <%= bgStyle %>><img src="images/<%= icon %>" alt="<%= status %>" border="0"></td>
										</tr>
										<%
									}									
								
									i++;
								}
							
							
							%>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>