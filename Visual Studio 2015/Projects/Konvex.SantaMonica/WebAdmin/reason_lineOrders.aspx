<%@ Page language="c#" Codebehind="reason_lineOrders.aspx.cs" AutoEventWireup="false" Inherits="WebAdmin.reason_lineOrders" %>
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
	
	function updateGrid()
	{
		document.thisform.action = "reason_lineOrders.aspx";
		document.thisform.submit();
	}
	
	function report(lineOrderEntryNo)
	{
		if (confirm("Du kommer att återrapportera linjeorder "+lineOrderEntryNo+" från <%= reasonCode %>-listan. Är du säker?"))
		{
			document.location.href="reason_lineOrders.aspx?command=report&reasonCode=<%= reasonCode %>&lineOrderEntryNo="+lineOrderEntryNo;
		}
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
					<td class="" width="95%">Uppföljning på:&nbsp;<select name="reasonCode" class="Textfield" onchange="updateGrid()">
					<%
						int z = 0;
						while (z < reasonDataSet.Tables[0].Rows.Count)
						{
							%><option value="<%= reasonDataSet.Tables[0].Rows[z].ItemArray.GetValue(0).ToString() %>" <% if (reasonCode == reasonDataSet.Tables[0].Rows[z].ItemArray.GetValue(0).ToString()) Response.Write("selected"); %>><%= reasonDataSet.Tables[0].Rows[z].ItemArray.GetValue(1).ToString() %></option><%
							z++;
						}
					%>
					</select>&nbsp;</td>
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
									Återrapportera</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">
									Visa</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									&nbsp;</th>
							</tr>
							<%
								int i = 0;
								while (i < reasonLineOrders.Tables[0].Rows.Count)
								{
									Navipro.SantaMonica.Common.LineOrder lineOrder = new Navipro.SantaMonica.Common.LineOrder(reasonLineOrders.Tables[0].Rows[i]);
									
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

												
												%><tr>
												<td class="jobDescription" style="<%= bgStyle+serviceStyle %>"><%= lineOrderContainer.containerNo+": "+categoryDesc %></td>
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
										<td class="jobDescription" valign="top" align="center" style="<%= bgStyle %>"><a href="javascript:report('<%= lineOrder.entryNo.ToString() %>')"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
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
