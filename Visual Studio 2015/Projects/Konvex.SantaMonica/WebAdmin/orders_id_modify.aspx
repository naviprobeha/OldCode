<%@ Page language="c#" Codebehind="orders_id_modify.aspx.cs" AutoEventWireup="false" Inherits="Navipro.SantaMonica.WebAdmin.orders_id_modify" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SmartShipping</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link rel="stylesheet" href="css/webstyle.css">
		<script>
		
		function goBack()
		{
			document.location.href='orders_id.aspx?shipOrderNo=<%= currentShipOrder.entryNo %>&shipOrderLineNo=<%= currentShipOrderLineId.shipOrderLineEntryNo %>';		
		}

		
		function save()
		{
			document.thisform.submit();
		}
		

		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" topmargin="10" onload="document.thisform.id.focus()">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" action="orders_id_modify.aspx" method="post" runat="server">
			<input type="hidden" name="command" value="save">
			<input type="hidden" name="shipOrderNo" value="<%= currentShipOrder.entryNo %>"> 
			<input type="hidden" name="shipOrderLineNo" value="<%= currentShipOrderLine.entryNo %>"> 
			<input type="hidden" name="entryNo" value="<%= currentShipOrderLineId.entryNo %>"> 
			<input type="hidden" name="mode" value="">		
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame">
				<tr>
					<td height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td class="activityName">Körorder <% if (currentShipOrder.entryNo > 0) Response.Write(currentShipOrder.entryNo); %></td>
				</tr>
				<tr>
					<td>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<td class="interaction" valign="top" width="300">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Hämtdatum</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentShipOrder.shipDate.ToString("yyyy-MM-dd") %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Anmälningsdatum</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><% if (currentShipOrder.creationDate.Year != 1753) Response.Write(currentShipOrder.creationDate.ToString("yyyy-MM-dd")); %></b>&nbsp;</td>
										</tr>
									</table>
								</td>								
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Produktionsplatsnr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentCustomer.productionSite %></b></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td colspan="2" class="activityAuthor" height="15" valign="top">Kundnr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentShipOrder.customerNo %></b>&nbsp;</td>
											<td class="interaction" width="90%" nowrap><a href="customers_view.aspx?customerNo=<%= currentShipOrder.customerNo %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Kundnamn</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentShipOrder.customerName %></b></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>				
				<tr>
					<td>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">Artikelnr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">Beskrivning</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right">Antal</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right">A-pris</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right">Antal avlivningar</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right">A-pris avlivning</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right">Totalbelopp</th>
							</tr>
							<tr>
								<td class="jobDescription" valign="top"><%= currentShipOrderLine.itemNo %></td>
								<td class="jobDescription" valign="top"><%= item.description %></td>
								<td class="jobDescription" valign="top" align="right"><%= currentShipOrderLine.quantity %></td>
								<td class="jobDescription" valign="top" align="right"><%= currentShipOrderLine.unitPrice %></td>
								<td class="jobDescription" valign="top" align="right"><%= currentShipOrderLine.connectionQuantity %></td>
								<td class="jobDescription" valign="top" align="right"><%= currentShipOrderLine.connectionUnitPrice %></td>
								<td class="jobDescription" valign="top" align="right"><%= currentShipOrderLine.totalAmount %></td>
							</tr>
						</table>
						<br>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">ID-nr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">Provt.</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">Obd.</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca">Ta bort</th>	
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca">Ändra</th>	
							</tr>
							<%
							
									System.Data.DataSet shipOrderLineIdDataSet = shipOrderLineIds.getDataSet(database, currentShipOrder.entryNo, currentShipOrderLine.entryNo);
									
									int m = 0;
									
									while (m < shipOrderLineIdDataSet.Tables[0].Rows.Count)
									{
										string bseTestingValue = "";
										string postMortemValue = "";
										if (shipOrderLineIdDataSet.Tables[0].Rows[m].ItemArray.GetValue(4).ToString() == "1") bseTestingValue = "Ja";
										if (shipOrderLineIdDataSet.Tables[0].Rows[m].ItemArray.GetValue(5).ToString() == "1") postMortemValue = "Ja";
										
										if (currentShipOrderLineId.entryNo.ToString() == shipOrderLineIdDataSet.Tables[0].Rows[m].ItemArray.GetValue(0).ToString())
										{
											if (bseTestingValue == "Ja") bseTestingValue = "checked";
											if (postMortemValue == "Ja") postMortemValue = "checked";
											
											%><tr>											
												<td class="jobDescription" valign="top" align="left"><input type="text" name="id" value="<%= shipOrderLineIdDataSet.Tables[0].Rows[m].ItemArray.GetValue(3).ToString() %>" class="Textfield" size="40" maxlength="100">&nbsp;<input type="button" class="Button" name="se" value="SE-nr" onclick="document.thisform.id.value='SE <%= currentCustomer.productionSite %>-'+document.thisform.id.value;"></td>
												<td class="jobDescription" valign="top" align="center"><input type="checkbox" name="bse" <%= bseTestingValue %>></td>
												<td class="jobDescription" valign="top" align="center"><input type="checkbox" name="obd" <%= postMortemValue %>></td>
												<td class="jobDescription" valign="top" align="center">&nbsp;</td>
												<td class="jobDescription" valign="top" align="center"><input type="button" value="Spara" class="Button" onclick="save()"></td>
											</tr><%
										}
										else
										{
											%><tr>
												<td class="jobDescription" valign="top" align="left"><%= shipOrderLineIdDataSet.Tables[0].Rows[m].ItemArray.GetValue(3).ToString() %></td>
												<td class="jobDescription" valign="top" align="center"><%= bseTestingValue %></td>
												<td class="jobDescription" valign="top" align="center"><%= postMortemValue %></td>
												<td class="jobDescription" valign="top" align="center"><a href="orders_id.aspx?command=deleteId&shipOrderNo=<%= Request["shipOrderNo"] %>&shipOrderlineNo=<%= currentShipOrderLine.entryNo %>&idNo=<%= shipOrderLineIdDataSet.Tables[0].Rows[m].ItemArray.GetValue(0).ToString() %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
												<td class="jobDescription" valign="top" align="center"><a href="orders_id_modify.aspx?shipOrderNo=<%= Request["shipOrderNo"] %>&shipOrderlineNo=<%= currentShipOrderLine.entryNo %>&idNo=<%= shipOrderLineIdDataSet.Tables[0].Rows[m].ItemArray.GetValue(0).ToString() %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
											</tr><%
										}
										
										m++;
									}
							
							
							%>							
							
						</table>
					</td>
				</tr>
				<tr>
					<td align="right" height="25"><input type="button" onclick="goBack()" value="Avbryt" class="Button"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>