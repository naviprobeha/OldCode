<%@ Page language="c#" Codebehind="customers_shipments.aspx.cs" AutoEventWireup="false" Inherits="Navipro.SantaMonica.WebAdmin.customers_shipments" %>
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
					<td class="activityName">Följesedlar för kund: <%= Request["customerNo"] %></td>
				</tr>
				<tr>
					<td class=""><input type="button" value="<% if (showInfo) Response.Write("Dölj info"); else Response.Write("Visa info"); %>" class="button" onclick="toggleInfo()"></td>
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
									Adress</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Ort</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Datum</th>
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
								
									if (showInfo)
									{
										%>
										<tr>
											<td class="jobDescription" rowspan="2" valign="top" <%= bgStyle %>><%= activeShipments.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %></td>
											<td class="jobDescription" rowspan="2" valign="top" <%= bgStyle %>><%= activeShipments.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %><%= activeShipments.Tables[0].Rows[i].ItemArray.GetValue(9).ToString() %></td>
											<td class="jobDescription" rowspan="2" valign="top" <%= bgStyle %>><%= activeShipments.Tables[0].Rows[i].ItemArray.GetValue(2).ToString() %></td>
											<td class="jobDescription" valign="top" <%= bgStyle %>><%= activeShipments.Tables[0].Rows[i].ItemArray.GetValue(3).ToString() %></td>
											<td class="jobDescription" valign="top" <%= bgStyle %>><%= activeShipments.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() %></td>
											<td class="jobDescription" valign="top" <%= bgStyle %>><%= activeShipments.Tables[0].Rows[i].ItemArray.GetValue(5).ToString() %></td>
											<td class="jobDescription" rowspan="2" valign="top" <%= bgStyle %>><%= activeShipments.Tables[0].Rows[i].ItemArray.GetValue(6).ToString().Substring(0, 10) %></td>
											<td class="jobDescription" rowspan="2" valign="top" align="center" <%= bgStyle %>><%= status %></td>
											<td class="jobDescription" rowspan="2" valign="top" align="center" <%= bgStyle %>><a href="shipments_view.aspx?no=<%= activeShipments.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
											<td class="jobDescription" rowspan="2" valign="top" align="center" <%= bgStyle %>><img src="images/<%= icon %>" alt="<%= status %>" border="0"></td>
										</tr>
										<tr>
											<td class="jobDescription" colspan="3" <%= bgStyle %>><%= shipmentsClass.getShipmentContent(database, activeShipments.Tables[0].Rows[i].ItemArray.GetValue(1).ToString()) %>&nbsp;</td>
										</tr>
										<%

									}
									else
									{
										%>
										<tr>
											<td class="jobDescription" valign="top" <%= bgStyle %>><%= activeShipments.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %></td>
											<td class="jobDescription" valign="top" <%= bgStyle %>><%= activeShipments.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %><%= activeShipments.Tables[0].Rows[i].ItemArray.GetValue(9).ToString() %></td>
											<td class="jobDescription" valign="top" <%= bgStyle %>><%= activeShipments.Tables[0].Rows[i].ItemArray.GetValue(2).ToString() %></td>
											<td class="jobDescription" valign="top" <%= bgStyle %>><%= activeShipments.Tables[0].Rows[i].ItemArray.GetValue(3).ToString() %></td>
											<td class="jobDescription" valign="top" <%= bgStyle %>><%= activeShipments.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() %></td>
											<td class="jobDescription" valign="top" <%= bgStyle %>><%= activeShipments.Tables[0].Rows[i].ItemArray.GetValue(5).ToString() %></td>
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