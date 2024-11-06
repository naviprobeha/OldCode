<%@ Page language="c#" Codebehind="shippingCustomers_lineOrders.aspx.cs" AutoEventWireup="false" Inherits="WebAdmin.shippingCustomers_lineOrders" %>
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
					<td class="activityName">Linjeorder för kund: <%= Request["shippingCustomerNo"] %></td>
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
									Transportör</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Transportdatum</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Kundnr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Namn</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Ort</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Telefonnr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Container</th>
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
									Navipro.SantaMonica.Common.LineJournal lineJournal = lineOrder.getJournal(database);
									
									string statusText = lineOrder.getStatusText();
									string statusIcon = lineOrder.getStatusIcon();
								
									string bgStyle = "";
									
									if ((i % 2) > 0)
									{
										bgStyle = " style=\"background-color: #e0e0e0;\"";
									}
								
									%>
									<tr>
										<td class="jobDescription" valign="top" align="center" <%= bgStyle %>><img src="images/<%= statusIcon %>" border="0" alt="<%= statusText %>"></td>
										<td class="jobDescription" valign="top" <%= bgStyle %>><%= lineOrder.entryNo.ToString() %></td>
										<td class="jobDescription" valign="top" <%= bgStyle %>><%= lineOrder.organizationNo %></td>
										<td class="jobDescription" valign="top" <%= bgStyle %>><%= lineOrder.shipDate.ToString("yyyy-MM-dd") %></td>
										<td class="jobDescription" valign="top" <%= bgStyle %>><%= lineOrder.shippingCustomerNo %></td>
										<td class="jobDescription" valign="top" <%= bgStyle %>><%= lineOrder.shippingCustomerName %></td>
										<td class="jobDescription" valign="top" <%= bgStyle %>><%= lineOrder.city %></td>
										<td class="jobDescription" valign="top" <%= bgStyle %>><%= lineOrder.phoneNo %></td>
										<td class="jobDescription" valign="top" <%= bgStyle %>><%
										
											System.Data.DataSet containerDataSet = lineOrder.getContainers(database);
											int k = 0;
											while (k < containerDataSet.Tables[0].Rows.Count)
											{
												Response.Write(containerDataSet.Tables[0].Rows[k].ItemArray.GetValue(2).ToString()+"<br>");
											
												k++;											
											}
										
										%></td>
										<td class="jobDescription" valign="top" <%= bgStyle %>>
											<%
												if (lineJournal != null) 
												{
													%>
													<table cellspacing="0" cellpadding="0" width="100%" border="0">
														<TR>
															<td class="jobDescription" <%= bgStyle %>><%= lineJournal.shipDate.ToString("yyyy-MM-dd") + "-"+lineJournal.agentCode %></td>
															<td align="right"><a href="lineorders_assign.aspx?lineOrderNo=<%= lineOrder.entryNo %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
														</TR>
													</table>
													<%
												}
											%>&nbsp;
										</td>
										<td class="jobDescription" valign="top" <%= bgStyle %>><%= statusText %></td>
										<td class="jobDescription" valign="top" align="center" <%= bgStyle %>><a href="lineorders_view.aspx?lineOrderNo=<%= lineOrder.entryNo.ToString() %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
										<td class="jobDescription" valign="top" align="center" <%= bgStyle %>><img src="images/<%= statusIcon %>" border="0" alt="<%= statusText %>"></td>
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
