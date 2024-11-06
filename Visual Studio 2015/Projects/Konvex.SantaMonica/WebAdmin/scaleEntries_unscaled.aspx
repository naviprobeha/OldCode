<%@ Page language="c#" Codebehind="scaleEntries_unscaled.aspx.cs" AutoEventWireup="false" Inherits="WebAdmin.scaleEntries_unscaled" %>
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
		function deleteEntry(entryNo)
		{
	
			if (confirm("Du kommer att ta bort containern på litan över ej invägda containers. Är du säker?") == 1)
			{
				document.thisform.command.value = "deleteEntry";
				document.thisform.entryNo.value = entryNo;
				document.thisform.submit();
			}
		}
	</script>
	
	<body MS_POSITIONING="GridLayout" topmargin="10" onload="setTimeout('document.thisform.submit()', 90000)">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" method="post" runat="server">
			<input type="hidden" name="command" value="searchContainer">
			<input type="hidden" name="entryNo" value="">
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame">
				<tr>
					<td height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td class="activityName">Ej invägda containers</td>
				</tr>
				<tr>
					<td class="">Fabrik:&nbsp;<select name="factory" class="Textfield" onchange="document.thisform.submit()">
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
									Lossad datum och klockslag</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Containernr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Kategori</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right" valign="bottom">
									Uppskattad vikt (kg)</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Linjeorder</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Kundnr</th>
								<% if (supervisor) { %><th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center" valign="bottom">
									Ta bort</th><% } %>
							</tr>
							<%
							
								int i = 0;
								while (i < lineOrderContainerDataSet.Tables[0].Rows.Count)
								{		
									Navipro.SantaMonica.Common.LineOrderContainer lineOrderContainer = new Navipro.SantaMonica.Common.LineOrderContainer(lineOrderContainerDataSet.Tables[0].Rows[i]);
									
									string bgStyle = "";
									
									if ((i % 2) > 0)
									{
										bgStyle = "background-color: #e0e0e0;";
									}
									
									string factoryCode = lineOrderContainerDataSet.Tables[0].Rows[i].ItemArray.GetValue(8).ToString();
									string lineOrderEntryNo = lineOrderContainerDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString();
									string shippingCustomerNo = lineOrderContainerDataSet.Tables[0].Rows[i].ItemArray.GetValue(9).ToString();
									string lineJournalEntryNo = lineOrderContainerDataSet.Tables[0].Rows[i].ItemArray.GetValue(10).ToString();
									DateTime entryDate = DateTime.Parse(lineOrderContainerDataSet.Tables[0].Rows[i].ItemArray.GetValue(11).ToString());
									DateTime entryTime = DateTime.Parse(lineOrderContainerDataSet.Tables[0].Rows[i].ItemArray.GetValue(12).ToString());
									string categoryText = lineOrderContainer.categoryCode;

									Navipro.SantaMonica.Common.Categories categories = new Navipro.SantaMonica.Common.Categories();
									Navipro.SantaMonica.Common.Category category = categories.getEntry(database, lineOrderContainer.categoryCode);
												
									string serviceStyle = "";
									Navipro.SantaMonica.Common.ContainerEntries containerEntries = new Navipro.SantaMonica.Common.ContainerEntries();
									System.Data.DataSet serviceDataSet = containerEntries.getServiceDataSet(database, lineOrderContainer.containerNo);
									if (serviceDataSet.Tables[0].Rows.Count > 0) serviceStyle = "color: red; font-weight: bold;";
												
																						
									if (category != null) categoryText = categoryText+", "+category.description;							
									%>
									<tr>
										<td class="jobDescription" style="<%= bgStyle %><%= serviceStyle %>" valign="top"><%= factoryCode %></td>
										<td class="jobDescription" style="<%= bgStyle %><%= serviceStyle %>" valign="top"><%= entryDate.ToString("yyyy-MM-dd")+" "+entryTime.ToString("HH:mm:ss") %></td>
										<td class="jobDescription" style="<%= bgStyle %><%= serviceStyle %>" valign="top"><%= lineOrderContainer.containerNo %></td>
										<td class="jobDescription" style="<%= bgStyle %><%= serviceStyle %>" valign="top"><%= categoryText %></td>
										<td class="jobDescription" style="<%= bgStyle %><%= serviceStyle %>" valign="top" align="right"><%= lineOrderContainer.weight.ToString("0") %></td>
										<td class="jobDescription" style="<%= bgStyle %><%= serviceStyle %>" valign="top"><a href="lineOrders_view.aspx?lineOrderNo=<%= lineOrderEntryNo %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a>&nbsp;<%= lineOrderEntryNo %></td>
										<td class="jobDescription" style="<%= bgStyle %><%= serviceStyle %>" valign="top"><a href="shippingCustomers_view.aspx?shippingCustomerNo=<%= shippingCustomerNo %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a>&nbsp;<%= shippingCustomerNo %>&nbsp;</td>
										<% if (supervisor) { %><td class="jobDescription" style="<%= bgStyle %><%= serviceStyle %>" valign="top" align="center"><a href="javascript:deleteEntry('<%= lineOrderContainer.entryNo %>')"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td><% } %>
									</tr>
									<%									
									
									i++;
								}
								
								if (i == 0)
								{
									%>
									<tr>
									<td class="jobDescription" colspan="8">Inga containers i listan.</td>
									</tr>
									<%
								
								}
														
							%>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>