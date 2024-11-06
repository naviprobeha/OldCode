<%@ Page language="c#" Codebehind="lineorders_future.aspx.cs" AutoEventWireup="false" Inherits="WebAdmin.lineorders_future" %>
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
					<td class="activityName">Kommande linjeorder</td>
				</tr>
				<tr>
					<td class="" width="95%"><% if (currentOrganization.allowLineOrderSupervision) { %>Transportör:&nbsp;<select name="organizationCode" class="Textfield" onchange="document.thisform.submit()">
					<option value="">- Alla -</option>
					<%
						int z = 0;
						while (z < organizationDataSet.Tables[0].Rows.Count)
						{
							%><option value="<%= organizationDataSet.Tables[0].Rows[z].ItemArray.GetValue(0).ToString() %>" <% if (Request["organizationCode"] == organizationDataSet.Tables[0].Rows[z].ItemArray.GetValue(0).ToString()) Response.Write("selected"); %>><%= organizationDataSet.Tables[0].Rows[z].ItemArray.GetValue(1).ToString() %></option><%
							z++;
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
					</select></td>
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
									Transportdatum</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Klockslag</th>
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
								
									%>
									<tr>
										<td class="jobDescription" valign="top" align="center" style="<%= bgStyle %>"><img src="images/<%= statusIcon %>" border="0" alt="<%= statusText %>"></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><%= lineOrder.entryNo.ToString() %></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><%= lineOrder.getType() %></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><%= lineOrder.shipDate.ToString("yyyy-MM-dd") %></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><%= shipTime %></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><%= lineOrder.shippingCustomerName %></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><%= lineOrder.city %></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>" nowrap><%
										
											System.Data.DataSet containerDataSet = lineOrder.getContainers(database);
											int k = 0;
											while (k < containerDataSet.Tables[0].Rows.Count)
											{
												Navipro.SantaMonica.Common.LineOrderContainer lineOrderContainer = new Navipro.SantaMonica.Common.LineOrderContainer(containerDataSet.Tables[0].Rows[k]);
												Navipro.SantaMonica.Common.Category category = lineOrderContainer.getCategory(database);
												string categoryDesc = "";
												if (category != null) categoryDesc = category.description;
												
												Response.Write(lineOrderContainer.containerNo+": "+categoryDesc+"<br>");
											
												k++;											
											}
										
										%></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><%= lineOrder.organizationNo %></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>">
											<table cellspacing="0" cellpadding="0" width="100%" border="0">
												<TR>
													<td class="jobDescription" style="<%= bgStyle %>" valign="top" nowrap><%= routeName %></td>
													<% if (currentOrganization.allowLineOrderSupervision) { %><td align="right" valign="top"><a href="lineorders_assign.aspx?lineOrderNo=<%= lineOrder.entryNo %>"><img src="images/button_assist.gif" border="0" alt="Tilldela rutt" width="12" height="13"></a></td><% } %>
												</TR>
											</table>
										</td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><%= statusText %></td>
										<td class="jobDescription" valign="top" align="center" style="<%= bgStyle %>"><a href="lineorders_view.aspx?lineOrderNo=<%= lineOrder.entryNo.ToString() %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
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
