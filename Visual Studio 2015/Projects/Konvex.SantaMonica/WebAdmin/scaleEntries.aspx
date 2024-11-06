<%@ Page language="c#" Codebehind="scaleEntries.aspx.cs" AutoEventWireup="false" Inherits="WebAdmin.scaleEntries" %>
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

	function searchReference()
	{
		document.thisform.command.value = "searchReference";
		document.thisform.submit();
	
	}
	
	</script>

	<body MS_POSITIONING="GridLayout" topmargin="10" onload="setTimeout('document.thisform.submit()', 90000)">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" method="post" runat="server">
			<input type="hidden" name="command" value="searchContainer">
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame">
				<tr>
					<td height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td class="activityName">Vägningar</td>
				</tr>
				<tr>
					<td class="">Datum:&nbsp;<% WebAdmin.HTMLHelper.createDatePicker("workDate", toDate); %>&nbsp;Bakåt i tiden:&nbsp;<select name="noOfDaysBack" class="Textfield" onchange="document.thisform.submit()">
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
					</select>&nbsp;Fabrik:&nbsp;<select name="factory" class="Textfield" onchange="document.thisform.submit()">
						<%
							int j = 0;
							while (j < activeFactories.Tables[0].Rows.Count)
							{
								
								%>
								<option value="<%= activeFactories.Tables[0].Rows[j].ItemArray.GetValue(1).ToString() %>" <% if (currentFactory.no == activeFactories.Tables[0].Rows[j].ItemArray.GetValue(1).ToString()) Response.Write("selected"); %>><%= activeFactories.Tables[0].Rows[j].ItemArray.GetValue(2).ToString() %></option>													
								<%
							
								j++;
							}
						
						
						%>									
					</select>&nbsp;Typ:&nbsp;<select name="type" class="Textfield" onchange="document.thisform.submit()">
						<option value="">- Alla -</option>
						<option value="0" <% if (Request["type"] == "0") Response.Write("selected"); %>>In</option>
						<option value="1" <% if (Request["type"] == "1") Response.Write("selected"); %>>Ut</option>
					</select>&nbsp;<input type="button" value="Idag" onclick="gotoToday()" class="Button">&nbsp;Fraktsedel: <input type="text" class="Textfield" name="reference">&nbsp;<input type="button" value="Sök" onclick="searchReference()" class="Button"></td>
				</tr>
				<tr>
					<td>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Fabrik</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Trans. nr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Typ</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Fraktsedel</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Containernr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Datum och klockslag</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Kundnr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Kategori</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right" valign="bottom">
									Vikt (t)</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Bil</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Linjeorder</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Status</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									ERP Status</th>
							</tr>
							<%
							
								int i = 0;
								while (i < scaleEntriesDataSet.Tables[0].Rows.Count)
								{		
									
									
									string bgStyle = "";
									
									if ((i % 2) > 0)
									{
										bgStyle = " style=\"background-color: #e0e0e0;\"";
									}
									
									Navipro.SantaMonica.Common.ScaleEntry scaleEntry = new Navipro.SantaMonica.Common.ScaleEntry(scaleEntriesDataSet.Tables[0].Rows[i]);
									
									string lineOrderEntryNo = "";	
									string containerNo = "";
									if (scaleEntry.lineOrderEntryNo > 0) lineOrderEntryNo = ""+scaleEntry.lineOrderEntryNo;
									if (scaleEntry.containerNo.Length > 1) containerNo = ""+scaleEntry.containerNo;

									decimal weight = decimal.Parse(scaleEntry.weight.ToString());
																						
									%>
									<tr>
										<td class="jobDescription" <%= bgStyle %> valign="top"><%= scaleEntry.factoryCode %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top"><%= scaleEntry.entryNo %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top"><%= scaleEntry.getType() %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top"><%= scaleEntry.reference %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top"><%= containerNo %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top"><%= scaleEntry.entryDateTime.ToString("yyyy-MM-dd HH:mm:ss") %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top"><a href="shippingCustomers_view.aspx?shippingCustomerNo=<%= scaleEntry.shippingCustomerNo %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a>&nbsp;<%= scaleEntry.shippingCustomerNo %>&nbsp;</td>
										<td class="jobDescription" <%= bgStyle %> valign="top"><%= scaleEntry.categoryCode+", "+scaleEntry.getCategory(database) %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top" align="right"><%= decimal.Round(weight, 2).ToString("N") %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top"><%= scaleEntry.agentCode %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top"><% if (lineOrderEntryNo != "") { %><a href="lineOrders_view.aspx?lineOrderNo=<%= lineOrderEntryNo %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a>&nbsp;<% } %><%= lineOrderEntryNo %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top"><%= scaleEntry.getStatus() %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top"><%= scaleEntry.getNavisionStatus() %></td>
									</tr>
									<%									
									
									i++;
								}
														
							%>
							<tr>
								<td class="jobDescription" colspan="8" align="right"><b>Totalt:</b></td>
								<td class="jobDescription" align="right"><b><%= decimal.Round(decimal.Parse(scaleSum.ToString()), 2).ToString("N") %></b></td>
								<td class="jobDescription" colspan="4">&nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>