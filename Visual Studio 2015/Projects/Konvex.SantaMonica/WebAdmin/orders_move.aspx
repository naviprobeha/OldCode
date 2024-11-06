<%@ Page language="c#" Codebehind="orders_move.aspx.cs" AutoEventWireup="false" Inherits="Navipro.SantaMonica.WebAdmin.orders_move" %>
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
	
	function move()
	{
		document.thisform.command.value = "move";
		document.thisform.submit();
	}
	
	function toggleInfo()
	{
		document.thisform.command.value = "toggleInfo";
		document.thisform.submit();
	
	}
	
	
	</script>
	
	<body MS_POSITIONING="GridLayout" topmargin="10">
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
					<td class="activityName">Flytta fram</td>
				</tr>
				<tr>
					<td class="" width="95%">Transportdatum:&nbsp;<% WebAdmin.HTMLHelper.createDatePicker("shipDate", shipDate); %>&nbsp;<input type="button" value="Flytta fram" class="Button" onclick="move()">&nbsp;</td>
				</tr>			
				<tr>
					<td>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									&nbsp;</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Flytta</th>
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
									Status</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Bil</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Flytta</th>
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
											<td class="jobDescription" rowspan="2" <%= bgStyle %> valign="top" align="center"><img src="images/<%= statusIcon %>" border="0" alt="<%= statusText %>"></td>
											<td class="jobDescription" rowspan="2" <%= bgStyle %> valign="top" align="center"><input type="checkbox" name="order<%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %>2" onclick="order<%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %>.checked=order<%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %>2.checked"></td>
											<td class="jobDescription" rowspan="2" <%= bgStyle %> valign="top"><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %></td>
											<td class="jobDescription" rowspan="2" <%= bgStyle %> valign="top"><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(11).ToString().Substring(0, 10) %></td>
											<td class="jobDescription" rowspan="2" <%= bgStyle %> valign="top"><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(2).ToString() %></td>
											<td class="jobDescription" <%= bgStyle %> valign="top"><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(3).ToString() %></td>
											<td class="jobDescription" <%= bgStyle %> valign="top"><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() %></td>
											<td class="jobDescription" <%= bgStyle %> valign="top"><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(6).ToString() %></td>
											<td class="jobDescription" <%= bgStyle %> valign="top"><%= statusText %></td>
											<td class="jobDescription" <%= bgStyle %> valign="top"><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(9).ToString() %></td>
											<td class="jobDescription" rowspan="2" <%= bgStyle %> valign="top" align="center"><input type="checkbox" name="order<%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %>" onclick="order<%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %>2.checked=order<%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %>.checked"></td>
											<td class="jobDescription" rowspan="2" <%= bgStyle %> valign="top" align="center"><img src="images/<%= statusIcon %>" border="0" alt="<%= statusText %>"></td>
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
											<td class="jobDescription" <%= bgStyle %> valign="top" align="center"><img src="images/<%= statusIcon %>" border="0" alt="<%= statusText %>"></td>
											<td class="jobDescription" <%= bgStyle %> align="center"><input type="checkbox" name="order<%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %>2" onclick="order<%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %>.checked=order<%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %>2.checked"></td>
											<td class="jobDescription" <%= bgStyle %> valign="top"><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() + activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %></td>
											<td class="jobDescription" <%= bgStyle %> valign="top"><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(11).ToString().Substring(0, 10) %></td>
											<td class="jobDescription" <%= bgStyle %> valign="top"><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(2).ToString() %></td>
											<td class="jobDescription" <%= bgStyle %> valign="top"><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(3).ToString() %></td>
											<td class="jobDescription" <%= bgStyle %> valign="top"><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() %></td>
											<td class="jobDescription" <%= bgStyle %> valign="top"><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(6).ToString() %></td>
											<td class="jobDescription" <%= bgStyle %> valign="top"><%= statusText %></td>
											<td class="jobDescription" <%= bgStyle %> valign="top"><%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(9).ToString() %></td>
											<td class="jobDescription" <%= bgStyle %> align="center"><input type="checkbox" name="order<%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %>" onclick="order<%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %>2.checked=order<%= activeShipOrders.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %>.checked"></td>
											<td class="jobDescription" <%= bgStyle %> valign="top" align="center"><img src="images/<%= statusIcon %>" border="0" alt="<%= statusText %>"></td>
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
