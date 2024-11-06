<%@ Page language="c#" Codebehind="callLog.aspx.cs" AutoEventWireup="false" Inherits="WebAdmin.callLog" %>
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

	function createOrderQuestion(url, organizationNo, customerNo)
	{
		if (confirm("Det finns redan körorder för vald kund. Vill du se en lista på dessa?"))
		{
			document.location.href = "customers_orders.aspx?customerNo="+customerNo+"&organizationNo="+organizationNo+"&ongoing=1";
		}
		else
		{
			document.location.href = url;
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
					<td class="activityName">Samtalslogg</td>
				</tr>
				<tr>
					<td class="" width="95%">Typ:&nbsp;<select name="callType" class="Textfield" onchange="document.thisform.submit()">
					<option value="">- Alla -</option>
					<option value="2" <% if (Request["callType"] == "2") Response.Write("selected"); %>>Pågående samtal</option>
					<option value="3" <% if (Request["callType"] == "3") Response.Write("selected"); %>>Vidarekopplade samtal</option>
					<option value="5" <% if (Request["callType"] == "5") Response.Write("selected"); %>>Mottagna samtal</option>
					<option value="4" <% if (Request["callType"] == "4") Response.Write("selected"); %>>Missade samtal</option>
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
					</select>&nbsp;<input type="button" value="Idag" onclick="gotoToday()" class="Button">&nbsp;</td>
				</tr>
				<tr>
					<td>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									&nbsp;</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Datum & klockslag</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Samtalstyp</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Nummer</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Status</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Svarat</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Avslutat</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Typ</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Namn</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Ort</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">
									Skapa order</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">
									Visa</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									&nbsp;</th>
							</tr>
							<%
								int i = 0;
								while (i < phoneLogDataSet.Tables[0].Rows.Count)
								{
									Navipro.SantaMonica.Common.PhoneLogEntry phoneLogEntry = new Navipro.SantaMonica.Common.PhoneLogEntry(phoneLogDataSet.Tables[0].Rows[i]);
									
									phoneLogEntry.applyCustomerInformation(database);
									
									string bgStyle = "";
									
									if ((i % 2) > 0)
									{
										bgStyle = "background-color: #e0e0e0;";
									}
			
									string pickedUpDateTime = ""; 
									string finishedDateTime = "";
									if (phoneLogEntry.pickedUpDateTime.Year > 1753) pickedUpDateTime = phoneLogEntry.pickedUpDateTime.ToString("yyyy-MM-dd HH:mm:ss");
									if (phoneLogEntry.finishedDateTime.Year > 1753) finishedDateTime = phoneLogEntry.finishedDateTime.ToString("yyyy-MM-dd HH:mm:ss");
								
									%>
									<tr>
										<td class="jobDescription" valign="top" align="center" style="<%= bgStyle %>"><img src="images/<%= phoneLogEntry.getStatusIcon() %>" border="0" alt="<%= phoneLogEntry.getStatusText() %>"></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><%= phoneLogEntry.receivedDateTime.ToString("yyyy-MM-dd HH:mm:ss") %></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><%= phoneLogEntry.getDirection() %></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><%= phoneLogEntry.remoteNumber %></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><%= phoneLogEntry.getStatusText() %></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><%
											if (pickedUpDateTime != "") 
											{
												Response.Write(pickedUpDateTime);
											}
											else
											{
												Navipro.SantaMonica.Common.PhoneLogEntry altPhoneLogEntry = phoneLogEntry.getAlternativeLogEntry(database);
												if (altPhoneLogEntry != null)
												{
													Response.Write(altPhoneLogEntry.userId+" => "+altPhoneLogEntry.pickedUpDateTime.ToString("HH:mm:ss"));													
												}
											}
											
											 %>&nbsp;</td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><%= finishedDateTime %></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><%= phoneLogEntry.getType() %></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><%= phoneLogEntry.name %></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><%= phoneLogEntry.city %></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>" align="center"><%
										
											System.Data.DataSet dataSet = phoneLogEntry.getCustomerOrganizations(database);
											if (dataSet != null)
											{
												%><table cellspacing="0" cellpadding="0" width="100%" border="0"><%
												int z= 0;
												while (z < dataSet.Tables[0].Rows.Count)
												{
													%><tr>
														<td class="jobDescription" style="<%= bgStyle %>"><%= dataSet.Tables[0].Rows[z].ItemArray.GetValue(0).ToString() %></td>
													<%
													
													Navipro.SantaMonica.Common.ShipOrders shipOrders = new Navipro.SantaMonica.Common.ShipOrders();
													if (shipOrders.checkCustomerShipOrderExists(database, dataSet.Tables[0].Rows[z].ItemArray.GetValue(0).ToString(), phoneLogEntry.originNo, ""))
													{
														%><td class="jobDescription" style="<%= bgStyle %>"><a href="javascript:createOrderQuestion('<%= phoneLogEntry.getCreateOrderUrl(database, dataSet.Tables[0].Rows[z].ItemArray.GetValue(0).ToString(), currentOrganization.no) %>', '<%= dataSet.Tables[0].Rows[z].ItemArray.GetValue(0).ToString() %>', '<%= phoneLogEntry.originNo %>')"><img src="images/button_assist.gif" border="0" width="12" height="13"></a><%
													}
													else
													{
														%><td class="jobDescription" style="<%= bgStyle %>"><a href="<%= phoneLogEntry.getCreateOrderUrl(database, dataSet.Tables[0].Rows[z].ItemArray.GetValue(0).ToString(), currentOrganization.no) %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a><%
													}
													
													%></tr><%
													z++;
												}
												%></table><%
												
											}
											else
											{
												if ((phoneLogEntry.originType == 1) || (phoneLogEntry.originType == 2))
												{
													%><a href="<%= phoneLogEntry.getCreateOrderUrl(database) %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a><%
												}
											}
										%></td>
										<td class="jobDescription" valign="top" align="center" style="<%= bgStyle %>">&nbsp;
											<%
												if ((phoneLogEntry.originType == 1) || (phoneLogEntry.originType == 2))
												{
													%><a href="<%= phoneLogEntry.getViewUrl() %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a><%
												}
											%>
										&nbsp;</td>
										<td class="jobDescription" valign="top" align="center" style="<%= bgStyle %>"><img src="images/<%= phoneLogEntry.getStatusIcon() %>" border="0" alt="<%= phoneLogEntry.getStatusText() %>"></td>
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
