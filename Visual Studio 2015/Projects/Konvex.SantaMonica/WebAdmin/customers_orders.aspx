<%@ Page language="c#" Codebehind="customers_orders.aspx.cs" AutoEventWireup="false" Inherits="Navipro.SantaMonica.WebAdmin.customers_orders" %>
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
	

	function toggleInfo()
	{
		document.thisform.command.value = "toggleInfo";
		document.thisform.submit();
	
	}

	
	</script>
	
	<body MS_POSITIONING="GridLayout" topmargin="10" onload="setTimeout('document.thisform.submit()', 30000)">
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
					<td class="activityName">Körorder för kund: <%= Request["customerNo"] %></td>
				</tr>
				<tr>
					<td class="" width="95%">Bil:&nbsp;<select name="agent" class="Textfield" onchange="document.thisform.submit()">
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
					</select>&nbsp;<input type="button" value="<% if (showInfo) Response.Write("Dölj info"); else Response.Write("Visa info"); %>" class="button" onclick="toggleInfo()"></td>
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
									Hämtdatum</th>
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
									string statusIcon = getStatusIcon((int)activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(8));;
								
									string bgStyle = "";
									
									if ((i % 2) > 0)
									{
										bgStyle = " style=\"background-color: #e0e0e0;\"";
									}
								
									if (showInfo)
									{
										%>
										<tr>
											<td class="jobDescription" rowspan="2" valign="top" align="center" <%= bgStyle %>><img src="images/<%= statusIcon %>" border="0" alt="<%= statusText %>"></td>
											<td class="jobDescription" rowspan="2" valign="top" <%= bgStyle %>><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() + activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %></td>
											<td class="jobDescription" rowspan="2" valign="top" <%= bgStyle %>><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(11).ToString().Substring(0, 10) %></td>
											<td class="jobDescription" rowspan="2" valign="top" <%= bgStyle %>><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(2).ToString() %></td>
											<td class="jobDescription" valign="top" <%= bgStyle %>><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(3).ToString() %></td>
											<td class="jobDescription" valign="top" <%= bgStyle %>><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() %></td>
											<td class="jobDescription" valign="top" <%= bgStyle %>><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(6).ToString() %></td>
											<td class="jobDescription" valign="top" <%= bgStyle %>><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(7).ToString() %></td>
											<td class="jobDescription" valign="top" <%= bgStyle %>><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(10).ToString() %></td>
											<td class="jobDescription" rowspan="2" valign="top" <%= bgStyle %>><%= statusText %></td>
											<td class="jobDescription" valign="top" rowspan="2" <%= bgStyle %>><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(9).ToString() %></td>
											<td class="jobDescription" rowspan="2" valign="top" align="center" <%= bgStyle %>><a href="orders_view.aspx?shipOrderNo=<%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
											<td class="jobDescription" rowspan="2" valign="top" align="center" <%= bgStyle %>><img src="images/<%= statusIcon %>" border="0" alt="<%= statusText %>"></td>
										</tr>
										<tr>
											<td class="jobDescription" colspan="3" valign="top" <%= bgStyle %>><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(12).ToString() %></td>
											<td class="jobDescription" colspan="2" valign="top" <%= bgStyle %>><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(13).ToString() %></td>
										</tr>
										<%
									}
									else
									{
										%>
										<tr>
											<td class="jobDescription" valign="top" align="center" <%= bgStyle %>><img src="images/<%= statusIcon %>" border="0" alt="<%= statusText %>"></td>
											<td class="jobDescription" valign="top" <%= bgStyle %>><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() + activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %></td>
											<td class="jobDescription" valign="top" <%= bgStyle %>><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(11).ToString().Substring(0, 10) %></td>
											<td class="jobDescription" valign="top" <%= bgStyle %>><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(2).ToString() %></td>
											<td class="jobDescription" valign="top" <%= bgStyle %>><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(3).ToString() %></td>
											<td class="jobDescription" valign="top" <%= bgStyle %>><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() %></td>
											<td class="jobDescription" valign="top" <%= bgStyle %>><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(6).ToString() %></td>
											<td class="jobDescription" valign="top" <%= bgStyle %>><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(7).ToString() %></td>
											<td class="jobDescription" valign="top" <%= bgStyle %>><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(10).ToString() %></td>
											<td class="jobDescription" valign="top" <%= bgStyle %>><%= statusText %></td>
											<td class="jobDescription" valign="top" <%= bgStyle %>><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(9).ToString() %></td>
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
