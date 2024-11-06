<%@ Page language="c#" Codebehind="factoryOrders_plan.aspx.cs" AutoEventWireup="false" Inherits="WebAdmin.factoryOrders_plan" %>
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
			document.location.href='factoryOrders.aspx';		
		}

		function changeShipDate()
		{
		}
		
		function validate()
		{
			if (document.thisform.factoryNo.value == "") 
			{
				alert("Du måste välja fabrik.");
				return;
			}
			if (document.thisform.consumerNo.value == "") 
			{
				alert("Du måste välja värmeverk.");
				return;
			}
			if (document.thisform.categoryCode.value == "") 
			{
				alert("Du måste välja kategori.");
				return;
			}
			
			
			document.thisform.command.value = "createPlan";
			document.thisform.action = "factoryOrders_plan.aspx";
			document.thisform.submit();
		}

		function createOrders()
		{				
			document.thisform.command.value = "createOrders";
			document.thisform.action = "factoryOrders_plan.aspx";
			document.thisform.submit();
		}
		
	
		</script>
		
	</HEAD>
	<body MS_POSITIONING="GridLayout" topmargin="10">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" action="orders_view.aspx" method="post" runat="server">
			<input type="hidden" name="command" value="">
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame" style="border-bottom: 0 px">
				<tr>
					<td height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td class="activityName" colspan="2">Planeringsförslag fabriksorder</td>
				</tr>
			</table>
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame" style="border-top: 0 px">
				<tr>
					<td valign="top">
						<% if (mode == 0) { %>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<td class="interaction" valign="top" width="300">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Uppläggningsdatum</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><% WebAdmin.HTMLHelper.createDatePicker("creationDate", DateTime.Today); %>&nbsp;</td>
										</tr>
									</table>
								</td>								

								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Antal orders</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><select name="noOfOrders" class="Textfield">
												<option value="2">2</option>
												<option value="3">3</option>
												<option value="4">4</option>
												<option value="5">5</option>
												<option value="6">6</option>
												<option value="7">7</option>
												<option value="8">8</option>
												<option value="9">9</option>
												<option value="10">10</option>
												<option value="15">15</option>
												<option value="20">20</option>
											</select></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="interaction" valign="top" width="300">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Fabrik</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><select name="factoryNo" class="Textfield">
												<option value="">- Ej vald -</option>
												<%
													int z = 0;
													while (z < factoryDataSet.Tables[0].Rows.Count)
													{
														%><option value="<%= factoryDataSet.Tables[0].Rows[z].ItemArray.GetValue(0).ToString() %>"><%= factoryDataSet.Tables[0].Rows[z].ItemArray.GetValue(1).ToString() %></option><%
														z++;
													}
													
													z = 0;
													while (z < shippingCustomersDataSet.Tables[0].Rows.Count)
													{
														%><option value="<%= shippingCustomersDataSet.Tables[0].Rows[z].ItemArray.GetValue(0).ToString() %>"><%= shippingCustomersDataSet.Tables[0].Rows[z].ItemArray.GetValue(1).ToString() %></option><%
														z++;
													}
													
												%>
												</select></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" width="300">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Värmeverk</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><select name="consumerNo" class="Textfield">
												<option value="">- Ej vald -</option>
												<%
													z = 0;
													while (z < consumerDataSet.Tables[0].Rows.Count)
													{
														%><option value="<%= consumerDataSet.Tables[0].Rows[z].ItemArray.GetValue(0).ToString() %>"><%= consumerDataSet.Tables[0].Rows[z].ItemArray.GetValue(1).ToString() %></option><%
														z++;
													}
												%>
												</select></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Transportör</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><select name="organizationNo" class="Textfield">
												<option value="">- Ej vald -</option>
												<%
													z = 0;
													while (z < organizationDataSet.Tables[0].Rows.Count)
													{
														%><option value="<%= organizationDataSet.Tables[0].Rows[z].ItemArray.GetValue(0).ToString() %>"><%= organizationDataSet.Tables[0].Rows[z].ItemArray.GetValue(1).ToString() %></option><%
														z++;
													}
												%>
												</select></td>
										</tr>
									</table>
								</td>																					
							</tr>
							<tr>
								<td class="interaction" valign="top" width="300">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Kategori</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><select name="categoryCode" class="Textfield">
												<option value="">- Ej vald -</option>
												<%
													z = 0;
													while (z < categoryDataSet.Tables[0].Rows.Count)
													{
														%><option value="<%= categoryDataSet.Tables[0].Rows[z].ItemArray.GetValue(0).ToString() %>"><%= categoryDataSet.Tables[0].Rows[z].ItemArray.GetValue(0).ToString() + ": "+ categoryDataSet.Tables[0].Rows[z].ItemArray.GetValue(1).ToString() %></option><%
														z++;
													}
												%>
												</select></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Antal (ton)</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><input type="text" name="quantity" class="Textfield" size="20" maxlength="10"></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Miniminivå (ton)</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><input type="text" name="minimumQuantity" class="Textfield" size="20" maxlength="10"></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<table cellspacing="1" cellpadding="2" border="0" width="100%">
						<tr>
							<td align="right" height="25"><input type="button" onclick="validate()" value="Skapa förslag" class="Button">&nbsp;<input type="button" onclick="goBack()" value="Avbryt" class="Button"></td>
						</tr>
						</table>
						<% } %>
						<% if (mode == 1) { %>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<td class="interaction" valign="top" width="300">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Uppläggningsdatum</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= templateFactoryOrder.creationDate.ToString("yyyy-MM-dd") %><input type="hidden" name="creationDate" value="<%= templateFactoryOrder.creationDate.ToString("yyyy-MM-dd") %>"></b>&nbsp;</td>
										</tr>
									</table>
								</td>								
								<td class="interaction" valign="top" width="300">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Startdatum och klockslag</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= templateFactoryOrder.shipDate.ToString("yyyy-MM-dd HH:mm") %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Antal orders</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= noOfOrders.ToString() %><input type="hidden" name="noOfOrders" value="<%= noOfOrders.ToString() %>"></b>&nbsp;</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="interaction" valign="top" width="300">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Fabrik</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= templateFactoryOrder.factoryName %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" width="300">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Värmeverk</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= templateFactoryOrder.consumerName %><input type="hidden" name="consumerNo" value="<%= templateFactoryOrder.consumerNo %>"></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Transportör</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= templateFactoryOrder.getOrganizationName(database) %></b></td>
										</tr>
									</table>
								</td>																					
							</tr>
							<tr>
								<td class="interaction" valign="top" width="300">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Kategori</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= templateFactoryOrder.categoryCode+": "+templateFactoryOrder.categoryDescription %></b><input type="hidden" name="categoryCode" value="<%= templateFactoryOrder.categoryCode %>"></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Antal (ton)</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= templateFactoryOrder.quantity %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Miniminivå (ton)</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= templateFactoryOrder.consumerLevel %></b></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<br>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Fabrik</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Transportör</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Hämtdatum och klockslag</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Leveransdatum och klockslag</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Planeringstyp</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Antal (ton)</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">
									Skapa</th>
							</tr>
							<%
								int k = 0;
								while (k < planList.Count)
								{
									Navipro.SantaMonica.Common.FactoryOrder factoryOrder = (Navipro.SantaMonica.Common.FactoryOrder)planList[k];
									
									%>
									<tr>
										<td class="jobDescription" valign="top"><select name="factoryNo_<%= k %>" class="Textfield">
												<%
													int z = 0;
													while (z < factoryDataSet.Tables[0].Rows.Count)
													{
														%><option value="<%= factoryDataSet.Tables[0].Rows[z].ItemArray.GetValue(0).ToString() %>" <% if (factoryOrder.factoryNo == factoryDataSet.Tables[0].Rows[z].ItemArray.GetValue(0).ToString()) Response.Write("selected"); %>><%= factoryDataSet.Tables[0].Rows[z].ItemArray.GetValue(1).ToString() %></option><%
														z++;
													}
													
													z = 0;
													while (z < shippingCustomersDataSet.Tables[0].Rows.Count)
													{
														%><option value="<%= shippingCustomersDataSet.Tables[0].Rows[z].ItemArray.GetValue(0).ToString() %>" <% if (factoryOrder.factoryNo == shippingCustomersDataSet.Tables[0].Rows[z].ItemArray.GetValue(0).ToString()) Response.Write("selected"); %>><%= shippingCustomersDataSet.Tables[0].Rows[z].ItemArray.GetValue(1).ToString() %></option><%
														z++;
													}
													
												%>
												</select></td>
										<td class="jobDescription" valign="top"><select name="organizationNo_<%= k %>" class="Textfield">
												<option value="">- Ej vald -</option>
												<%
													z = 0;
													while (z < organizationDataSet.Tables[0].Rows.Count)
													{
														%><option value="<%= organizationDataSet.Tables[0].Rows[z].ItemArray.GetValue(0).ToString() %>" <% if (factoryOrder.organizationNo == organizationDataSet.Tables[0].Rows[z].ItemArray.GetValue(0).ToString()) Response.Write("selected"); %>><%= organizationDataSet.Tables[0].Rows[z].ItemArray.GetValue(1).ToString() %></option><%
														z++;
													}
												%>
												</select></td>
										<td class="jobDescription" valign="top"><% WebAdmin.HTMLHelper.createDatePicker("shipDate_"+k+"_", factoryOrder.shipDate); %>&nbsp;<select name="shipTimeHour_<%= k %>" class="Textfield">
								<%
									int hour = 0;
									while (hour < 24)
									{
										string hourString = hour.ToString();
										if (hourString.Length == 1) hourString = "0"+hourString;
										
										string selected = "";
										if (hour == factoryOrder.shipTime.Hour) selected = "selected";
										
										%><option value="<%= hour %>" <%= selected %>><%= hourString %></option><%
									
										hour++;
									}									
								%>
								</select>:<select name="shipTimeMinute_<%= k %>" class="Textfield">
								<%
									int minute = 0;
									while (minute < 60)
									{
										string minuteString = minute.ToString();
										if (minuteString.Length == 1) minuteString = "0"+minuteString;
										
										string selected = "";
										if (minute == factoryOrder.shipTime.Minute) selected = "selected";
										
										%><option value="<%= minute %>" <%= selected %>><%= minuteString %></option><%
									
										minute = minute + 15;
									}									
								%>
								</select></td>
										<td class="jobDescription" valign="top"><% WebAdmin.HTMLHelper.createDatePicker("plannedArrivalDate_"+k+"_", factoryOrder.plannedArrivalDateTime); %>&nbsp;<select name="plannedArrivalTimeHour_<%= k %>" class="Textfield">
								<%
									hour = 0;
									while (hour < 24)
									{
										string hourString = hour.ToString();
										if (hourString.Length == 1) hourString = "0"+hourString;
										
										string selected = "";
										if (hour == factoryOrder.plannedArrivalDateTime.Hour) selected = "selected";
										
										%><option value="<%= hour %>" <%= selected %>><%= hourString %></option><%
									
										hour++;
									}									
								%>
								</select>:<select name="plannedArrivalTimeMinute_<%= k %>" class="Textfield">
								<%
									minute = 0;
									while (minute < 60)
									{
										string minuteString = minute.ToString();
										if (minuteString.Length == 1) minuteString = "0"+minuteString;
										
										string selected = "";
										if (minute == factoryOrder.plannedArrivalDateTime.Minute) selected = "selected";
										
										%><option value="<%= minute %>" <%= selected %>><%= minuteString %></option><%
									
										minute = minute + 15;
									}									
								%>
								</select></td>
										<td class="jobDescription" valign="top"><select name="planningType_<%= k %>" class="Textfield">
											<option value="1">Lås planerat leveransdatum</option>
											<option value="2">Lås hämtdatum + planerat leveransdatum</option>
										</select></td>
										<td class="jobDescription" valign="top"><input type="text" name="quantity_<%= k %>" value="<%= factoryOrder.quantity %>" class="Textfield" size="10"></imput></td>
										<td class="jobDescription" valign="top" align="center"><input type="checkbox" name="create_<%= k %>" <% if (factoryOrder.type == 1) Response.Write("checked"); %>></td>
									</tr>
									<%
										
								
									k++;
								}
							
							
							%>
						</table>						
						<table cellspacing="1" cellpadding="2" border="0" width="100%">
						<tr>
							<td align="right" height="25"><input type="button" onclick="createOrders()" value="Skapa order" class="Button">&nbsp;<input type="button" onclick="goBack()" value="Avbryt" class="Button"></td>
						</tr>
						</table>
						<% } %>
					</td>
				</tr>
				</table>
				
			</table>
		</form>
	</body>
</HTML>