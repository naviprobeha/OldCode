<%@ Page language="c#" Codebehind="factoryInventory.aspx.cs" AutoEventWireup="false" Inherits="WebAdmin.factoryInventory" %>
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

	function changeYear()
	{
		document.thisform.action = "factoryInventory.aspx";
		document.thisform.command.value = "changeDate";
		document.thisform.submit();
	
	}

	
	function changeWeek()
	{
		document.thisform.action = "factoryInventory.aspx";
		document.thisform.command.value = "changeDate";
		document.thisform.submit();
	
	}



	function gotoToday()
	{
		document.thisform.action = "factoryInventory.aspx";	
		document.thisform.currentYear.value = "<%= DateTime.Now.Year %>";
		document.thisform.currentWeek.value = "<%= Navipro.SantaMonica.Common.CalendarHelper.GetWeek(DateTime.Today) %>";
	
		document.thisform.command.value = "changeDate";
		document.thisform.submit();
	
	}
	
	</script>
	
	<body MS_POSITIONING="GridLayout" topmargin="10" onload="setTimeout('changeWeek()', 600000)">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" action="consumerCapacity.aspx" method="post" runat="server">
		<input type="hidden" name="command" value="">
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame">
				<tr>
					<td height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td class="activityName">Lager Fabrik</td>
				</tr>
				<tr>
					<td class="" width="95%">År:&nbsp;<% WebAdmin.HTMLHelper.createYearPicker("currentYear", currentYear); %>&nbsp;Vecka:&nbsp;<% WebAdmin.HTMLHelper.createWeekPicker("currentWeek", currentYear, currentWeek); %>&nbsp;
					Fabrik:&nbsp;<select name="factoryNo" class="Textfield" onchange="changeWeek()">
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
					</select>&nbsp;<input type="button" value="Idag" onclick="gotoToday()" class="Button"></td>
				</tr>
				<tr>
					<td>
						<table cellspacing="1" cellpadding="2" border="0" width="100%">
						<tr>
							<td valign="top">
							<%
							
							
								int weekDay = 0;
								while (weekDay < 7)
								{
							
									%>		
										<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
											<tr>
												<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" colspan="13">
													<%= firstDay.AddDays(weekDay).ToString("yyyy-MM-dd")+" "+Navipro.SantaMonica.Common.CalendarHelper.GetDayOfWeek(firstDay.AddDays(weekDay)) %></th>
											</tr>
											<tr>
												<td class="jobDescription" style="font-weight: bold;" rowspan="2" valign="bottom">Timme</td>
												<td class="jobDescription" style="font-weight: bold; font-size: 12px" colspan="6" align="left">Inkommande netto</td>
												<td class="jobDescription" style="font-weight: bold; font-size: 12px" colspan="4" align="left">Utgående netto</td>
												<td class="jobDescription" style="font-weight: bold;" rowspan="2" valign="bottom" align="right">Totalt lager</td>
												<td class="jobDescription" style="font-weight: bold;" rowspan="2" valign="bottom" align="right">Procent</td>
											</tr>
											<tr>
												<td class="jobDescription" style="font-weight: bold;" align="left">Container</td>
												<td class="jobDescription" style="font-weight: bold;" align="left">Rutt</td>
												<td class="jobDescription" style="font-weight: bold;" align="left">Linjeorder</td>
												<td class="jobDescription" style="font-weight: bold;" align="left">Kund</td>
												<td class="jobDescription" style="font-weight: bold;" align="left">Typ</td>
												<td class="jobDescription" style="font-weight: bold;" align="right">Vikt</td>												
												<td class="jobDescription" style="font-weight: bold;" align="left">Fabriksorder</td>
												<td class="jobDescription" style="font-weight: bold;" align="left">Mottagare</td>
												<td class="jobDescription" style="font-weight: bold;" align="left">Typ</td>
												<td class="jobDescription" style="font-weight: bold;" align="right">Vikt</td>
											</tr>
											<%
												int hour = 0;
												while (hour < 24)
												{
													System.DateTime hourDateTime = new System.DateTime(firstDay.AddDays(weekDay).Year, firstDay.AddDays(weekDay).Month, firstDay.AddDays(weekDay).Day, hour, 0, 0);
													
													System.Collections.ArrayList inventoryList  = (System.Collections.ArrayList)inventoryTable[hourDateTime];
																								
													Navipro.SantaMonica.Common.FactoryInventory factoryInventory = (Navipro.SantaMonica.Common.FactoryInventory)totalInventoryTable[hourDateTime];
													string totalInventory = "";
													string totalProcent = "";
													if (factoryInventory != null)
													{
														totalInventory = Math.Round(factoryInventory.inventory, 0) + " ton";
														totalProcent = factoryInventory.percent+"%";																									
													}
														
													int z = 0;
													if (inventoryList != null)
													{
														
														while (z < inventoryList.Count)
														{
															%><tr><%
															
															if (z == 0)
															{
																%><td class="jobDescription" valign="top" rowspan="<%= inventoryList.Count %>"><%= hourDateTime.ToString("HH:mm") %></td><%
															}
															
															Navipro.SantaMonica.Common.FactoryInventoryEntry factoryInventoryEntry = (Navipro.SantaMonica.Common.FactoryInventoryEntry)inventoryList[z];
														
															if (factoryInventoryEntry.type == 0)
															{
																														
																string colorName = "";
																if (factoryInventoryEntry.status == 0) colorName = "red";
																if (factoryInventoryEntry.status == 1) colorName = "black";

																string lineJournalNo = "";
																string shippingCustomerName = "";
																Navipro.SantaMonica.Common.LineOrder lineOrder = factoryInventoryEntry.getLineOrder(database);
																if (lineOrder != null)
																{
																	lineJournalNo = lineOrder.lineJournalEntryNo.ToString();
																	shippingCustomerName = lineOrder.shippingCustomerName;
																}
																
																%>
																<td class="jobDescription" style="color: <%= colorName %>;"><%= factoryInventoryEntry.containerNo %></td>
																<td class="jobDescription" style="color: <%= colorName %>;"><%= lineJournalNo %>&nbsp;<a href="lineJournals_view.aspx?lineJournalNo=<%= lineJournalNo %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
																<td class="jobDescription" style="color: <%= colorName %>;""><%= factoryInventoryEntry.lineOrderEntryNo %>&nbsp;<a href="lineOrders_view.aspx?lineOrderNo=<%= factoryInventoryEntry.lineOrderEntryNo %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
																<td class="jobDescription" style="color: <%= colorName %>;"><%= shippingCustomerName %>&nbsp;</td>
																<td class="jobDescription" style="color: <%= colorName %>;"><%= factoryInventoryEntry.getStatus() %></td>
																<td class="jobDescription" style="color: <%= colorName %>;" align="right"><%= factoryInventoryEntry.weight %> kg</td>
																<td class="jobDescription" colspan="4">&nbsp;</td>
																<%
															
															}
															
															if (factoryInventoryEntry.type == 1)
															{
																														
																string colorName = "";
																if (factoryInventoryEntry.status == 0) colorName = "red";
																if (factoryInventoryEntry.status == 1) colorName = "black";

																%>
																<td class="jobDescription" colspan="6">&nbsp;</td>
																<td class="jobDescription" style="color: <%= colorName %>;">&nbsp;</td>
																<td class="jobDescription" style="color: <%= colorName %>;">&nbsp;</td>
																<td class="jobDescription" style="color: <%= colorName %>;"><%= factoryInventoryEntry.getStatus() %></td>
																<td class="jobDescription" style="color: <%= colorName %>;" align="right"><%= factoryInventoryEntry.weight * - 1 %> kg</td>
																<%
															
															}
															
															z++;

																
															
															%>
																<td class="jobDescription" align="right" valign="bottom">&nbsp;<%= totalInventory %></td>
																<td class="jobDescription" align="right" valign="bottom">&nbsp;<%= totalProcent %></td>
															</tr><%
															
														}
													}
													else
													{
														%><tr>
															<td class="jobDescription" valign="top"><%= hourDateTime.ToString("HH:mm") %></td>
															<td class="jobDescription" colspan="6">&nbsp;</td>
															<td class="jobDescription" colspan="4">&nbsp;</td>
															<td class="jobDescription" align="right" valign="bottom">&nbsp;<%= totalInventory %></td>
															<td class="jobDescription" align="right" valign="bottom">&nbsp;<%= totalProcent %></td>
														</tr><%
													}
																		

												
													hour++;
												}
											
											
											
											%>
										</table>
										<br>					
									<%
									
									weekDay++;
									
								}
							%>
							</td>
						</tr>
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
