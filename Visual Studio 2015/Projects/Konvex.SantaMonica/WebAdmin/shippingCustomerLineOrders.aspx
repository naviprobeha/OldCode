<%@ Page language="c#" Codebehind="shippingCustomerLineOrders.aspx.cs" AutoEventWireup="false" Inherits="WebAdmin.shippingCustomerLineOrders" %>
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
	
	function deleteLineOrder(entryNo)
	{
		if (confirm("Kundordern kommer att tas bort. Är du säker?"))
		{
			document.location.href="shippingForm.aspx?lineOrderEntryNo="+entryNo+"&command=deleteLineOrder";
		}
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
						<!-- #INCLUDE FILE="customermenu.asp" -->
					</td>
				</tr>
				<tr>
					<td class="activityName">Containerorder</td>
				</tr>
				<tr>
					<td class="" width="95%">Datum:&nbsp;<% WebAdmin.HTMLHelper.createDatePicker("workDate", toDate); %>&nbsp;Bakåt i tiden:&nbsp;<select name="noOfDaysBack" class="Textfield" onchange="document.thisform.submit()">
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
					</select>&nbsp;<input type="button" value="Idag" onclick="gotoToday()" class="Button">&nbsp;<input type="button" value="Ny order" class="button" onclick="document.location.href='shippingForm.aspx';"></td>
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
									Transportör</th>
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
									Rutt</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Status</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">
									Ta bort</th>
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
										bgStyle = " style=\"background-color: #e0e0e0;\"";
									}
									
									string shipTime = "";
									if (lineOrder.shipTime.ToString("HH:mm:ss") != "00:00:00") shipTime = lineOrder.shipTime.ToString("HH:mm");
								
									%>
									<tr>
										<td class="jobDescription" valign="top" align="center" <%= bgStyle %>><img src="images/<%= statusIcon %>" border="0" alt="<%= statusText %>"></td>
										<td class="jobDescription" valign="top" <%= bgStyle %>><%= lineOrder.entryNo.ToString() %></td>
										<td class="jobDescription" valign="top" <%= bgStyle %>><%= lineOrder.getType() %></td>
										<td class="jobDescription" valign="top" <%= bgStyle %>><%= lineOrder.organizationNo %></td>
										<td class="jobDescription" valign="top" <%= bgStyle %>><%= lineOrder.shipDate.ToString("yyyy-MM-dd") %></td>
										<td class="jobDescription" valign="top" <%= bgStyle %>><%= shipTime %></td>
										<td class="jobDescription" valign="top" <%= bgStyle %>><%= lineOrder.shippingCustomerName %></td>
										<td class="jobDescription" valign="top" <%= bgStyle %>><%= lineOrder.city %></td>
										<td class="jobDescription" valign="top" <%= bgStyle %>><%
										
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
										<td class="jobDescription" valign="top" <%= bgStyle %>><%= routeName %></td>
										<td class="jobDescription" valign="top" <%= bgStyle %>><%= statusText %></td>
										<td class="jobDescription" valign="top" align="center" <%= bgStyle %>><% if (lineOrder.status < 6) { %><a href="javascript:deleteLineOrder('<%= lineOrder.entryNo.ToString() %>');"><img src="images/button_assist.gif" border="0" width="12" height="13"><% } %></a></td>
										<td class="jobDescription" valign="top" align="center" <%= bgStyle %>><a href="shippingForm_print.aspx?lineOrderEntryNo=<%= lineOrder.entryNo.ToString() %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
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
