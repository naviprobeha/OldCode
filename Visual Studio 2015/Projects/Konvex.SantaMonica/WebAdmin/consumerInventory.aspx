<%@ Page language="c#" Codebehind="consumerInventory.aspx.cs" AutoEventWireup="false" Inherits="WebAdmin.consumerInventory" %>
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
		document.thisform.action = "consumerInventory.aspx";
		document.thisform.command.value = "changeDate";
		document.thisform.submit();
	
	}

	
	function changeWeek()
	{
		document.thisform.action = "consumerInventory.aspx";
		document.thisform.command.value = "changeDate";
		document.thisform.submit();
	
	}


	function gotoToday()
	{
		document.thisform.action = "consumerInventory.aspx";	
		document.thisform.currentYear.value = "<%= DateTime.Now.Year %>";
		document.thisform.currentWeek.value = "<%= Navipro.SantaMonica.Common.CalendarHelper.GetWeek(DateTime.Today) %>";
	
		document.thisform.command.value = "changeDate";
		document.thisform.submit();
	
	}
	
	</script>
	
	<body MS_POSITIONING="GridLayout" topmargin="10" onload="setTimeout('changeWeek()', 600000)">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" action="consumerInventory.aspx" method="post" runat="server">
		<input type="hidden" name="command" value="">
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame">
				<tr>
					<td height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td class="activityName">Lager Värmeverk</td>
				</tr>
				<tr>
					<td class="" width="95%">År:&nbsp;<% WebAdmin.HTMLHelper.createYearPicker("currentYear", currentYear); %>&nbsp;Vecka:&nbsp;<% WebAdmin.HTMLHelper.createWeekPicker("currentWeek", currentYear, currentWeek); %>&nbsp;
					Värmeverk:&nbsp;<select name="consumerNo" class="Textfield" onchange="changeWeek()">
						<%
							int j = 0;
							while (j < activeConsumers.Tables[0].Rows.Count)
							{
								
								%>
								<option value="<%= activeConsumers.Tables[0].Rows[j].ItemArray.GetValue(0).ToString() %>" <% if (currentConsumer.no == activeConsumers.Tables[0].Rows[j].ItemArray.GetValue(0).ToString()) Response.Write("selected"); %>><%= activeConsumers.Tables[0].Rows[j].ItemArray.GetValue(1).ToString() %></option>													
								<%
							
								j++;
							}
						
						
						%>									
					</select>&nbsp;<input type="button" value="Idag" onclick="gotoToday()" class="Button">&nbsp;</td>
				</tr>
				<tr>
					<td>
						<table cellspacing="1" cellpadding="2" border="0" width="100%">
						<tr>
							<%
							
							
								int weekDay = 0;
								while (weekDay < 7)
								{
							
									%>		
										<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
											<tr>
												<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" colspan="9">
													<%= firstDay.AddDays(weekDay).ToString("yyyy-MM-dd")+" "+Navipro.SantaMonica.Common.CalendarHelper.GetDayOfWeek(firstDay.AddDays(weekDay)) %></th>
											</tr>
											<tr>
												<td class="jobDescription" width="40" style="font-weight: bold;" width="80">Timme</td>
												<td class="jobDescription" width="150" style="font-weight: bold;">Fabriksorder</td>
												<td class="jobDescription" width="150" style="font-weight: bold;">Status</td>
												<td class="jobDescription" style="font-weight: bold;">Fabrik / Slakteri</td>
												<td class="jobDescription" width="120" style="font-weight: bold;" align="right">Påfylld kvantitet</td>
												<td class="jobDescription" width="100" style="font-weight: bold;">Typ</td>											
												<td class="jobDescription" width="100" style="font-weight: bold;" align="right">Nivå</td>
												<td class="jobDescription" width="100" style="font-weight: bold;" align="right">Kapacitet</td>
												<td class="jobDescription" style="font-weight: bold;" align="center" width="50">Ändra</td>
											</tr>
											<%
												int hour = 0;
												while (hour < 24)
												{
													System.DateTime hourDateTime = new System.DateTime(firstDay.AddDays(weekDay).Year, firstDay.AddDays(weekDay).Month, firstDay.AddDays(weekDay).Day, hour, 0, 0);
													
													Navipro.SantaMonica.Common.ConsumerInventory consumerInventory = (Navipro.SantaMonica.Common.ConsumerInventory)inventoryTable[hourDateTime];
													Navipro.SantaMonica.Common.ConsumerCapacity consumerCapacity = (Navipro.SantaMonica.Common.ConsumerCapacity)capacityTable[hourDateTime];
													
													string level = "";
													string capacity = "";
													string typeStr = "";
													
													if (consumerInventory != null)
													{
														level = Math.Round(consumerInventory.inventory, 2).ToString()+" ton";
														if (consumerInventory.type == 0) typeStr = "Avläst";
														if (consumerInventory.type == 1) typeStr = "Beräknad";
													}
													
													if (consumerCapacity != null)
													{
														capacity = consumerCapacity.plannedCapacity.ToString()+" ton/h";
														if (consumerCapacity.actualCapacity > 0) capacity = consumerCapacity.actualCapacity.ToString()+" ton/h (Verklig)";
																												
													}

													Navipro.SantaMonica.Common.ConsumerInventoryOrders consumerInventoryOrders = new Navipro.SantaMonica.Common.ConsumerInventoryOrders();
													System.Collections.ArrayList unloadedFactoryOrderList = consumerInventoryOrders.getFactoryOrders(database, currentConsumer.no, hourDateTime);																									
													System.Collections.ArrayList plannedFactoryOrderList = factoryOrders.getConsumerList(database, currentConsumer.no, hourDateTime);

													System.Collections.ArrayList factoryOrderList = WebAdmin.HTMLHelper.combineLists(unloadedFactoryOrderList, plannedFactoryOrderList);
													
													int loop = factoryOrderList.Count;
													if (loop == 0) loop = 1;
													
													int z = 0;
													while (z < loop)
													{
														string factoryOrderEntryNo = "";
														string status = "";
														string factoryName = "";
														string quantity = "";
														
														if (factoryOrderList.Count > z)
														{
															if (factoryOrderList[z] != null)
															{
																factoryOrderEntryNo = ((Navipro.SantaMonica.Common.FactoryOrder)factoryOrderList[z]).entryNo.ToString();
																factoryName = ((Navipro.SantaMonica.Common.FactoryOrder)factoryOrderList[z]).factoryName;
																status = "Planerad";
																quantity = ((Navipro.SantaMonica.Common.FactoryOrder)factoryOrderList[z]).quantity+" ton";
																if (((Navipro.SantaMonica.Common.FactoryOrder)factoryOrderList[z]).status == 3) 
																{
																	status = "På väg";
																}
																if (((Navipro.SantaMonica.Common.FactoryOrder)factoryOrderList[z]).status == 4) 
																{
																	status = "Lossad";
																	quantity = ((Navipro.SantaMonica.Common.FactoryOrder)factoryOrderList[z]).realQuantity+" ton";																	
																}
															}
														}

														%><tr><%
																												
														if (z == 0)
														{
															%>
															<td class="jobDescription" rowspan="<%= loop %>"><%= hourDateTime.ToString("HH:mm") %></td>
															<%
														}
																													
														%>
														<td class="jobDescription">
															<table cellspacing="0" cellpadding="0" width="100%" border="0">
															<tr>
																<td class="jobDescription"><%= factoryOrderEntryNo %></td>
																<td align="right">&nbsp;<% if ((factoryOrderEntryNo != "") && (currentOrganization.allowLineOrderSupervision)) Response.Write("<a href=\"factoryOrders_view.aspx?factoryOrderNo="+factoryOrderEntryNo+"\"><img src=\"images/button_assist.gif\" border=\"0\"></a>"); %></td>																
															</tr>
															</table>
														</td>
														<td class="jobDescription"><%= status %>&nbsp;</td>
														<td class="jobDescription"><%= factoryName %>&nbsp;</td>
														<td class="jobDescription" align="right">&nbsp;<%= quantity %></td>
														<%
														
														if (z == 0)
														{
															%>
															<td class="jobDescription" rowspan="<%= loop %>"><%= typeStr %></td>
															<td class="jobDescription" rowspan="<%= loop %>" align="right">&nbsp;<%= level %></td>
															<td class="jobDescription" rowspan="<%= loop %>" align="right">&nbsp;<%= capacity %></td>
															<td class="jobDescription" rowspan="<%= loop %>" valign="top" align="center">&nbsp;<% if (currentOrganization.allowLineOrderSupervision) Response.Write("<a href=\"consumerInventory_modify.aspx?consumerNo="+ currentConsumer.no+"&date="+firstDay.AddDays(weekDay).ToString("yyyy-MM-dd")+"&hour="+ hourDateTime.ToString("HH")+"\"><img src=\"images/button_assist.gif\" border=\"0\"></a>"); %>&nbsp;</td>													
															<%
														}												

														%></tr><%
																																	
														z++;
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
