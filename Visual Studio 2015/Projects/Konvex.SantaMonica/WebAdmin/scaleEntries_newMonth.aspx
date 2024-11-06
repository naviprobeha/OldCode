<%@ Page language="c#" Codebehind="scaleEntries_newMonth.aspx.cs" AutoEventWireup="false" Inherits="WebAdmin.scaleEntries_newMonth" %>
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

	function changeYear()
	{
		document.thisform.command.value = "changeShipDate";
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
					<td class="activityName">Vägningar råvaror hämtade föregående månad</td>
				</tr>
				<tr>
					<td class="">År:&nbsp;<% WebAdmin.HTMLHelper.createYearPicker("workDateYear", toDate.Year); %>&nbsp;Månadsskifte:&nbsp;<select name="workDateMonth" class="Textfield" onchange="document.thisform.submit()">
						<option value="1" <% if (Session["workDateMonth"].ToString() == "1") Response.Write("selected"); %>>December/Januari</option>
						<option value="2" <% if (Session["workDateMonth"].ToString() == "2") Response.Write("selected"); %>>Januari/Februar</option>
						<option value="3" <% if (Session["workDateMonth"].ToString() == "3") Response.Write("selected"); %>>Februari/Mars</option>
						<option value="4" <% if (Session["workDateMonth"].ToString() == "4") Response.Write("selected"); %>>Mars/April</option>
						<option value="5" <% if (Session["workDateMonth"].ToString() == "5") Response.Write("selected"); %>>April/Maj</option>
						<option value="6" <% if (Session["workDateMonth"].ToString() == "6") Response.Write("selected"); %>>Maj/Juni</option>
						<option value="7" <% if (Session["workDateMonth"].ToString() == "7") Response.Write("selected"); %>>Juni/Juli</option>
						<option value="8" <% if (Session["workDateMonth"].ToString() == "8") Response.Write("selected"); %>>Juli/Augusti</option>
						<option value="9" <% if (Session["workDateMonth"].ToString() == "9") Response.Write("selected"); %>>Augusti/September</option>
						<option value="10" <% if (Session["workDateMonth"].ToString() == "10") Response.Write("selected"); %>>September/Oktober</option>
						<option value="11" <% if (Session["workDateMonth"].ToString() == "11") Response.Write("selected"); %>>Oktober/November</option>
						<option value="12" <% if (Session["workDateMonth"].ToString() == "12") Response.Write("selected"); %>>November/December</option>
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
					</select></td>
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
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right" valign="bottom">
									Vikt före månadsskifte (t)</th>
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
							
								decimal totalWeight = 0;
								decimal totalPreWeight = 0;
								
								int i = 0;
								int k = 0;
								
								while (i < scaleEntriesDataSet.Tables[0].Rows.Count)
								{		
									
															
									Navipro.SantaMonica.Common.ScaleEntry scaleEntry = new Navipro.SantaMonica.Common.ScaleEntry(scaleEntriesDataSet.Tables[0].Rows[i]);
									
									if (scaleEntry.hasShipments(database))
									{
										if (scaleEntry.getCreationDateWeight(database) > 0)
										{
											string bgStyle = "";
											
											if ((k % 2) > 0)
											{
												bgStyle = " style=\"background-color: #e0e0e0;\"";
											}
											k++;
										
											string lineOrderEntryNo = "";	
											string containerNo = "";
											if (scaleEntry.lineOrderEntryNo > 0) lineOrderEntryNo = ""+scaleEntry.lineOrderEntryNo;
											if (scaleEntry.containerNo.Length > 1) containerNo = ""+scaleEntry.containerNo;

											decimal weight = decimal.Parse(scaleEntry.weight.ToString());
											decimal preWeight = decimal.Parse((scaleEntry.getCreationDateWeight(database)/1000).ToString());

											if ((scaleEntry.status == 2) || (scaleEntry.status == 9))
											{
												totalWeight = totalWeight + weight;
												totalPreWeight = totalPreWeight + preWeight;
											}													
											if (scaleEntry.status == 8)
											{
												totalWeight = totalWeight - weight;
												totalPreWeight = totalPreWeight + preWeight;
											}	
												
																						
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
												<td class="jobDescription" <%= bgStyle %> valign="top" align="right"><%= decimal.Round(preWeight, 2).ToString("N") %></td>
												<td class="jobDescription" <%= bgStyle %> valign="top"><%= scaleEntry.agentCode %></td>
												<td class="jobDescription" <%= bgStyle %> valign="top"><% if (lineOrderEntryNo != "") { %><a href="lineOrders_view.aspx?lineOrderNo=<%= lineOrderEntryNo %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a>&nbsp;<% } %><%= lineOrderEntryNo %></td>
												<td class="jobDescription" <%= bgStyle %> valign="top"><%= scaleEntry.getStatus() %></td>
												<td class="jobDescription" <%= bgStyle %> valign="top"><%= scaleEntry.getNavisionStatus() %>&nbsp;</td>
											</tr>
											<%									

										}
									}
																											
									i++;
								}
														
							%>
							<tr>
								<td class="jobDescription" colspan="8" align="right"><b>Totalt:</b></td>
								<td class="jobDescription" align="right"><b><%= decimal.Round(totalWeight, 2).ToString("N") %></b></td>
								<td class="jobDescription" align="right"><b><%= decimal.Round(totalPreWeight, 2).ToString("N") %></b></td>
								<td class="jobDescription" colspan="4">&nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>