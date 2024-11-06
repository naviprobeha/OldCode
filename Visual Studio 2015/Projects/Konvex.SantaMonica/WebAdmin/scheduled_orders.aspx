<%@ Page language="c#" Codebehind="scheduled_orders.aspx.cs" AutoEventWireup="false" Inherits="Navipro.SantaMonica.WebAdmin.scheduled_orders" %>
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
					<td class="activityName">Återkommande körorder</td>
				</tr>
				<tr>
					<td class="" width="95%"><input type="button" value="Ny återkommand order" class="button" onclick="document.location.href='scheduled_orders_modify.aspx';">&nbsp;<input type="button" value="Skapa körorder" class="button" onclick="document.location.href='scheduled_orders_convert.aspx';">&nbsp;&nbsp;Sortering: <select name="sorting" class="Textfield" onchange="document.thisform.submit();">
						<option value="Entry No" <% if (Request["sorting"] == "Entry No") Response.Write("selected"); %>>Nr</option>
						<option value="Ship Name" <% if (Request["sorting"] == "Ship Name") Response.Write("selected"); %>>Namn</option>
						<option value="Ship City" <% if (Request["sorting"] == "Ship City") Response.Write("selected"); %>>Ort</option>
						<option value="Comments" <% if (Request["sorting"] == "Comments") Response.Write("selected"); %>>Kommentar</option></select></td>
				</tr>
				<tr>
					<td>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Nr</th>
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
									Kommentar</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">
									Visa</th>
							</tr>
							<%
								int i = 0;
								while (i < scheduledShipOrders.Tables[0].Rows.Count)
								{
									string bgStyle = "";
									
									if ((i % 2) > 0)
									{
										bgStyle = " style=\"background-color: #e0e0e0;\"";
									}
								
									%>
									<tr>
										<td class="jobDescription" valign="top" <%= bgStyle %>><%= scheduledShipOrders.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() + scheduledShipOrders.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %></td>
										<td class="jobDescription" valign="top" <%= bgStyle %>><%= scheduledShipOrders.Tables[0].Rows[i].ItemArray.GetValue(2).ToString() %></td>
										<td class="jobDescription" valign="top" <%= bgStyle %>><%= scheduledShipOrders.Tables[0].Rows[i].ItemArray.GetValue(15).ToString() %></td>
										<td class="jobDescription" valign="top" <%= bgStyle %>><%= scheduledShipOrders.Tables[0].Rows[i].ItemArray.GetValue(16).ToString() %></td>
										<td class="jobDescription" valign="top" <%= bgStyle %>><%= scheduledShipOrders.Tables[0].Rows[i].ItemArray.GetValue(19).ToString() %></td>
										<td class="jobDescription" valign="top" <%= bgStyle %>><%= scheduledShipOrders.Tables[0].Rows[i].ItemArray.GetValue(8).ToString() %></td>
										<td class="jobDescription" valign="top" <%= bgStyle %>><%= scheduledShipOrders.Tables[0].Rows[i].ItemArray.GetValue(10).ToString() %></td>
										<td class="jobDescription" valign="top" align="center" <%= bgStyle %>><a href="scheduled_orders_modify.aspx?shipOrderNo=<%= scheduledShipOrders.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
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
