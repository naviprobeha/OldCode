<%@ Page language="c#" Codebehind="factoryOrders.aspx.cs" AutoEventWireup="false" Inherits="WebAdmin.factoryorders" %>
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

	function createOrders()
	{
		document.thisform.command.value = "createOrders";
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
					<td class="activityName">Fabriksorder</td>
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
					</select>&nbsp;<input type="button" value="Idag" onclick="gotoToday()" class="Button">&nbsp;<% if ((currentOrganization.allowLineOrderSupervision) && (currentOperator.systemRoleCode == "SUPER")) { %>&nbsp;<input type="button" value="Ny order" class="button" onclick="document.location.href='factoryOrders_modify.aspx';">&nbsp;<input type="button" value="Planeringsförslag" class="button" onclick="document.location.href='factoryOrders_plan.aspx';"><% } %></td>
				</tr>
				<tr>
					<td>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									&nbsp;</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center" valign="bottom">
									Visa</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Ordernr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Typ</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Transportdatum</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Klockslag</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Fabrik</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Värmeverk</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Kategori</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right" valign="bottom">
									Antal</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Transportör</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Bil</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Status</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Väntetid</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center" valign="bottom">
									Faktura mottagen</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center" valign="bottom">
									Visa</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									&nbsp;</th>
							</tr>
							<%
								int i = 0;
								while (i < activeFactoryOrders.Tables[0].Rows.Count)
								{
									Navipro.SantaMonica.Common.FactoryOrder factoryOrder = new Navipro.SantaMonica.Common.FactoryOrder(activeFactoryOrders.Tables[0].Rows[i]);
									
									string statusText = factoryOrder.getStatusText();
									string statusIcon = factoryOrder.getStatusIcon();
								
									string bgStyle = "";
									
									if ((i % 2) > 0)
									{
										bgStyle = "background-color: #e0e0e0;";
									}
									
									string shipTime = "";
									if (factoryOrder.shipTime.ToString("HH:mm:ss") != "00:00:00") shipTime = factoryOrder.shipTime.ToString("HH:mm");
								
									%>
									<tr>
										<td class="jobDescription" valign="top" align="center" style="<%= bgStyle %>"><img src="images/<%= statusIcon %>" border="0" alt="<%= statusText %>"></td>
										<td class="jobDescription" valign="top" align="center" style="<%= bgStyle %>"><a href="factoryOrders_view.aspx?factoryOrderNo=<%= factoryOrder.entryNo.ToString() %>"><img src="images/button_assist.gif" border="0"></a></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><%= factoryOrder.entryNo.ToString() %></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><%= factoryOrder.getType() %></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><%= factoryOrder.shipDate.ToString("yyyy-MM-dd") %></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><%= shipTime %></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><%= factoryOrder.factoryName+", "+factoryOrder.factoryCity %></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><%= factoryOrder.consumerName+", "+factoryOrder.consumerCity %></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><%= factoryOrder.categoryCode+": "+factoryOrder.categoryDescription %></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>" align="right"><%= factoryOrder.getQuantity().ToString() %> ton</td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><%= factoryOrder.organizationNo %></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>">
											<table cellspacing="0" cellpadding="0" width="100%" border="0">
												<TR>
													<td class="jobDescription" style="<%= bgStyle %>"><%= factoryOrder.agentCode %> <% if (factoryOrder.driverName != "") Response.Write("("+factoryOrder.driverName+")"); %><% if (factoryOrder.dropDriverName != "") Response.Write("+("+factoryOrder.dropDriverName+")"); %></td>
													<% if ((currentOperator.systemRoleCode == "SUPER") && (factoryOrder.status < 4)) { %><td align="right" style="<%= bgStyle %>"><a href="factoryOrders_assign.aspx?factoryOrderNo=<%= factoryOrder.entryNo.ToString() %>"><img src="images/button_assist.gif" border="0"></a></td><% } %>
												</TR>
											</table>
										</td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><%= statusText %></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>" align="center"><% if ((factoryOrder.loadWaitDuration > 0) || (factoryOrder.dropWaitDuration > 0)) Response.Write(factoryOrder.loadWaitDuration+ " / "+ factoryOrder.dropWaitDuration); %></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>" align="center">&nbsp;<%= factoryOrder.getTransportInvoiceStatus() %>&nbsp;</td>
										<td class="jobDescription" valign="top" align="center" style="<%= bgStyle %>"><a href="factoryOrders_view.aspx?factoryOrderNo=<%= factoryOrder.entryNo.ToString() %>"><img src="images/button_assist.gif" border="0"></a></td>
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
