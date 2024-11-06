<%@ Page language="c#" Codebehind="factoryOrders_modify.aspx.cs" AutoEventWireup="false" Inherits="WebAdmin.factoryOrders_modify" %>
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
		
		function saveOrder()
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
			
			
			document.thisform.command.value = "saveOrder";
			document.thisform.action = "factoryOrders_modify.aspx";
			document.thisform.submit();
		}

		function deleteOrder()
		{
			if (confirm("Fabriksordern kommer att raderas. Är du säker?"))
			{
				document.thisform.command.value = "deleteOrder";
				document.thisform.action = "factoryOrders_modify.aspx";
				document.thisform.submit();
			}
		}


		function applyFactory()
		{
			<%
				int i = 0;
				while (i < factoryDataSet.Tables[0].Rows.Count)
				{
					%>
					if (document.thisform.factoryNo.value == '<%= factoryDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>')
					{
						document.thisform.factoryType.value = 0;
						document.thisform.factoryName.value = '<%= factoryDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %>';
						document.thisform.factoryAddress.value = '<%= factoryDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() %>';
						document.thisform.factoryAddress2.value = '<%= factoryDataSet.Tables[0].Rows[i].ItemArray.GetValue(5).ToString() %>';
						document.thisform.factoryPostCode.value = '<%= factoryDataSet.Tables[0].Rows[i].ItemArray.GetValue(6).ToString() %>';
						document.thisform.factoryCity.value = '<%= factoryDataSet.Tables[0].Rows[i].ItemArray.GetValue(7).ToString() %>';
						document.thisform.factoryPhoneNo.value = '<%= factoryDataSet.Tables[0].Rows[i].ItemArray.GetValue(9).ToString() %>';								
					}
					<%	
							
				
					i++;
				}
				
				i = 0;
				while (i < shippingCustomersDataSet.Tables[0].Rows.Count)
				{
					%>
					if (document.thisform.factoryNo.value == '<%= shippingCustomersDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>')
					{
						document.thisform.factoryType.value = 1;
						document.thisform.factoryName.value = '<%= shippingCustomersDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %>';
						document.thisform.factoryAddress.value = '<%= shippingCustomersDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString() %>';
						document.thisform.factoryAddress2.value = '<%= shippingCustomersDataSet.Tables[0].Rows[i].ItemArray.GetValue(3).ToString() %>';
						document.thisform.factoryPostCode.value = '<%= shippingCustomersDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() %>';
						document.thisform.factoryCity.value = '<%= shippingCustomersDataSet.Tables[0].Rows[i].ItemArray.GetValue(5).ToString() %>';
						document.thisform.factoryPhoneNo.value = '<%= shippingCustomersDataSet.Tables[0].Rows[i].ItemArray.GetValue(7).ToString() %>';								
					}
					<%	
							
				
					i++;
				}
				
			%>
		
		}

		function applyConsumer()
		{
			<%
				int j = 0;
				while (j < consumerDataSet.Tables[0].Rows.Count)
				{
					%>
					if (document.thisform.consumerNo.value == '<%= consumerDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString() %>')
					{
						document.thisform.consumerName.value = '<%= consumerDataSet.Tables[0].Rows[j].ItemArray.GetValue(1).ToString() %>';
						document.thisform.consumerAddress.value = '<%= consumerDataSet.Tables[0].Rows[j].ItemArray.GetValue(4).ToString() %>';
						document.thisform.consumerAddress2.value = '<%= consumerDataSet.Tables[0].Rows[j].ItemArray.GetValue(5).ToString() %>';
						document.thisform.consumerPostCode.value = '<%= consumerDataSet.Tables[0].Rows[j].ItemArray.GetValue(6).ToString() %>';
						document.thisform.consumerCity.value = '<%= consumerDataSet.Tables[0].Rows[j].ItemArray.GetValue(7).ToString() %>';
						document.thisform.consumerPhoneNo.value = '<%= consumerDataSet.Tables[0].Rows[j].ItemArray.GetValue(9).ToString() %>';								
					}
					<%	
							
				
					j++;
				}
			%>
		
		}

		function applyCategory()
		{
			<%
				int k = 0;
				while (k < categoryDataSet.Tables[0].Rows.Count)
				{
					%>
					if (document.thisform.categoryCode.value == '<%= categoryDataSet.Tables[0].Rows[k].ItemArray.GetValue(0).ToString() %>')
					{
						document.thisform.categoryDescription.value = '<%= categoryDataSet.Tables[0].Rows[k].ItemArray.GetValue(1).ToString() %>';
					}
					<%	
							
				
					k++;
				}
			%>
		
		}

	
		</script>
		
	</HEAD>
	<body MS_POSITIONING="GridLayout" topmargin="10">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" action="orders_view.aspx" method="post" runat="server">
			<input type="hidden" name="command" value="">
			<input type="hidden" name="factoryOrderNo" value="<%= currentFactoryOrder.entryNo %>"> 
			<input type="hidden" name="mode" value="">		
			<input type="hidden" name="factoryType" value="">		
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame" style="border-bottom: 0 px">
				<tr>
					<td height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td class="activityName" colspan="2">Ändra fabriksorder <% if (currentFactoryOrder.entryNo > 0) Response.Write(currentFactoryOrder.entryNo); %></td>
				</tr>
				<tr>
					<td align="left" height="25" colspan="2"><input type="button" onclick="saveOrder()" value="Spara" class="Button">&nbsp;<% if (currentFactoryOrder.entryNo > 0) { %><input type="button" onclick="deleteOrder()" value="Ta bort" class="Button">&nbsp;<% } %><input type="button" onclick="goBack()" value="Avbryt" class="Button"></td>
				</tr>
			</table>
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame" style="border-top: 0 px">
				<tr>
					<td width="50" valign="top"><iframe id="mapFrame" width="265" height="600" frameborder="no" scrolling="no" src="<%= mapServer.getUrl() %>" style="border: 1px #000000 solid;"></iframe></td>
					<td valign="top">
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<td class="interaction" valign="top" width="300">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Planeringsmetod</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><select name="planningType" class="Textfield">
												<option value="0" <% if (currentFactoryOrder.planningType == 0) Response.Write("selected"); %>>Lås hämtdatum</option>
												<option value="1" <% if (currentFactoryOrder.planningType == 1) Response.Write("selected"); %>>Lås planerat leveransdatum</option>
												<option value="2" <% if (currentFactoryOrder.planningType == 2) Response.Write("selected"); %>>Lås hämtdatum + planerat leveransdatum</option></select></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Uppläggningsdatum</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><% WebAdmin.HTMLHelper.createDatePicker("creationDate", currentFactoryOrder.creationDate); %>&nbsp;</td>
										</tr>
									</table>
								</td>								
							</tr>
							<tr>
								<td class="interaction" valign="top" width="300">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Hämtdatum och klockslag</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><% WebAdmin.HTMLHelper.createDatePicker("shipDate", currentFactoryOrder.shipDate); %>&nbsp;<select name="shipTimeHour" class="Textfield">
								<%
									int hour = 0;
									while (hour < 24)
									{
										string hourString = hour.ToString();
										if (hourString.Length == 1) hourString = "0"+hourString;
										
										string selected = "";
										if (hour == currentFactoryOrder.shipTime.Hour) selected = "selected";
										
										%><option value="<%= hour %>" <%= selected %>><%= hourString %></option><%
									
										hour++;
									}									
								%>
								</select>:<select name="shipTimeMinute" class="Textfield">
								<%
									int minute = 0;
									while (minute < 60)
									{
										string minuteString = minute.ToString();
										if (minuteString.Length == 1) minuteString = "0"+minuteString;
										
										string selected = "";
										if (minute == currentFactoryOrder.shipTime.Minute) selected = "selected";
										
										%><option value="<%= minute %>" <%= selected %>><%= minuteString %></option><%
									
										minute = minute + 15;
									}									
								%>
								</select></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Planerat leveransdatum och klockslag</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><% WebAdmin.HTMLHelper.createDatePicker("plannedArrivalDate", currentFactoryOrder.plannedArrivalDateTime); %>&nbsp;<select name="plannedArrivalTimeHour" class="Textfield">
								<%
									hour = 0;
									while (hour < 24)
									{
										string hourString = hour.ToString();
										if (hourString.Length == 1) hourString = "0"+hourString;
										
										string selected = "";
										if (hour == currentFactoryOrder.plannedArrivalDateTime.Hour) selected = "selected";
										
										%><option value="<%= hour %>" <%= selected %>><%= hourString %></option><%
									
										hour++;
									}									
								%>
								</select>:<select name="plannedArrivalTimeMinute" class="Textfield">
								<%
									minute = 0;
									while (minute < 60)
									{
										string minuteString = minute.ToString();
										if (minuteString.Length == 1) minuteString = "0"+minuteString;
										
										string selected = "";
										if (minute == currentFactoryOrder.plannedArrivalDateTime.Minute) selected = "selected";
										
										%><option value="<%= minute %>" <%= selected %>><%= minuteString %></option><%
									
										minute = minute + 15;
									}									
								%>
								</select></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<br>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<td class="interaction" valign="top" width="300">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Transportör</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><select name="organizationNo" class="Textfield">
												<option value="">- Ej vald -</option>
												<%
													int z = 0;
													while (z < organizationDataSet.Tables[0].Rows.Count)
													{
														%><option value="<%= organizationDataSet.Tables[0].Rows[z].ItemArray.GetValue(0).ToString() %>" <% if (currentFactoryOrder.organizationNo == organizationDataSet.Tables[0].Rows[z].ItemArray.GetValue(0).ToString()) Response.Write("selected"); %>><%= organizationDataSet.Tables[0].Rows[z].ItemArray.GetValue(1).ToString() %></option><%
														z++;
													}
												%>
												</select></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" width="250">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Bil</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.agentCode %></b>&nbsp;</td>
										</tr>
									</table>
								</td>								
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Status</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.getStatusText() %>&nbsp;</b></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<br>						
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">						
							<tr>
								<td class="interaction" valign="top" colspan="3">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Fabrik</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><select name="factoryNo" class="Textfield" onchange="applyFactory()">
												<option value="">- Ej vald -</option>
												<%
													z = 0;
													while (z < factoryDataSet.Tables[0].Rows.Count)
													{
														%><option value="<%= factoryDataSet.Tables[0].Rows[z].ItemArray.GetValue(0).ToString() %>" <% if (currentFactoryOrder.factoryNo == factoryDataSet.Tables[0].Rows[z].ItemArray.GetValue(0).ToString()) Response.Write("selected"); %>><%= factoryDataSet.Tables[0].Rows[z].ItemArray.GetValue(1).ToString() %></option><%
														z++;
													}
													
													z = 0;
													while (z < shippingCustomersDataSet.Tables[0].Rows.Count)
													{
														%><option value="<%= shippingCustomersDataSet.Tables[0].Rows[z].ItemArray.GetValue(0).ToString() %>" <% if (currentFactoryOrder.factoryNo == shippingCustomersDataSet.Tables[0].Rows[z].ItemArray.GetValue(0).ToString()) Response.Write("selected"); %>><%= shippingCustomersDataSet.Tables[0].Rows[z].ItemArray.GetValue(1).ToString() %></option><%
														z++;
													}
													
												%>
												</select><input type="hidden" name="factoryName" value="<%= currentFactoryOrder.factoryName %>"></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="interaction" valign="top" width="300">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Adress</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><input type="text" name="factoryAddress" value="<%= currentFactoryOrder.factoryAddress %>" class="Textfield" size="30" maxlength="30"></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Adress 2</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><input type="text" name="factoryAddress2" value="<%= currentFactoryOrder.factoryAddress2 %>" class="Textfield" size="30" maxlength="30"></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Postnr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><input type="text" name="factoryPostCode" value="<%= currentFactoryOrder.factoryPostCode %>" class="Textfield" size="20" maxlength="20"></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" width="250">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Ort</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><input type="text" name="factoryCity" value="<%= currentFactoryOrder.factoryCity %>" class="Textfield" size="30" maxlength="30"></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Telefonnr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><input type="text" name="factoryPhoneNo" value="<%= currentFactoryOrder.factoryPhoneNo %>" class="Textfield" size="30" maxlength="30"></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<br>						
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">						
							<tr>
								<td class="interaction" valign="top" colspan="3">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Värmeverk</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><select name="consumerNo" class="Textfield" onchange="applyConsumer()">
												<option value="">- Ej vald -</option>
												<%
													z = 0;
													while (z < consumerDataSet.Tables[0].Rows.Count)
													{
														%><option value="<%= consumerDataSet.Tables[0].Rows[z].ItemArray.GetValue(0).ToString() %>" <% if (currentFactoryOrder.consumerNo == consumerDataSet.Tables[0].Rows[z].ItemArray.GetValue(0).ToString()) Response.Write("selected"); %>><%= consumerDataSet.Tables[0].Rows[z].ItemArray.GetValue(1).ToString() %></option><%
														z++;
													}
												%>
												</select><input type="hidden" name="consumerName" value="<%= currentFactoryOrder.consumerName %>"></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="interaction" valign="top" width="300">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Adress</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><input type="text" name="consumerAddress" value="<%= currentFactoryOrder.consumerAddress %>" class="Textfield" size="30" maxlength="30"></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Adress 2</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><input type="text" name="consumerAddress2" value="<%= currentFactoryOrder.consumerAddress2 %>" class="Textfield" size="30" maxlength="30"></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Postnr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><input type="text" name="consumerPostCode" value="<%= currentFactoryOrder.consumerPostCode %>" class="Textfield" size="20" maxlength="20"></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" width="250">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Ort</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><input type="text" name="consumerCity" value="<%= currentFactoryOrder.consumerCity %>" class="Textfield" size="30" maxlength="30"></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Telefonnr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><input type="text" name="consumerPhoneNo" value="<%= currentFactoryOrder.consumerPhoneNo %>" class="Textfield" size="30" maxlength="30"></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<br>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<td class="interaction" valign="top" width="300">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Kategori</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><select name="categoryCode" class="Textfield" onchange="applyCategory()">
												<option value="">- Ej vald -</option>
												<%
													z = 0;
													while (z < categoryDataSet.Tables[0].Rows.Count)
													{
														%><option value="<%= categoryDataSet.Tables[0].Rows[z].ItemArray.GetValue(0).ToString() %>" <% if (currentFactoryOrder.categoryCode == categoryDataSet.Tables[0].Rows[z].ItemArray.GetValue(0).ToString()) Response.Write("selected"); %>><%= categoryDataSet.Tables[0].Rows[z].ItemArray.GetValue(0).ToString() + ": "+ categoryDataSet.Tables[0].Rows[z].ItemArray.GetValue(1).ToString() %></option><%
														z++;
													}
												%>
												</select><input type="hidden" name="categoryDescription" value="<%= currentFactoryOrder.categoryDescription %>"></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" width="250">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Antal (ton)</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><input type="text" name="quantity" value="<%= currentFactoryOrder.quantity %>" class="Textfield" size="20" maxlength="10"></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Lossat antal (ton)</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><input type="text" name="realQuantity" value="<%= currentFactoryOrder.realQuantity %>" class="Textfield" size="20" maxlength="10"></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<br>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<td class="interaction" valign="top" width="300">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">PH-värde (Avsändare)</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><input type="text" name="phValueFactory" value="<%= currentFactoryOrder.phValueFactory %>" class="Textfield" maxlength="4"></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">PH-värde (Transportör)</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><input type="text" name="phValueShipping" value="<%= currentFactoryOrder.phValueShipping%>" class="Textfield" maxlength="4"></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<br>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<td class="interaction" valign="top" width="300">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Städningsstatus rapporterad från bilen</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><%= currentFactoryOrder.getAgentCleaningStatus() %></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Kommentar från bilen</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><input type="text" name="agentCleaningComment" value="<%= currentFactoryOrder.agentCleaningComment%>" class="Textfield" maxlength="250" size="130"></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="interaction" valign="top" width="300">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Städningsstatus rapporterad från värmeverket</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><%= currentFactoryOrder.getConsumerCleaningStatus() %></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Kommentar från värmeverket</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><input type="text" name="consumerCleaningComment" value="<%= currentFactoryOrder.consumerCleaningComment%>" class="Textfield" maxlength="250" size="130"></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<table cellspacing="1" cellpadding="2" border="0" width="100%">
						<tr>
							<td align="right" height="25"><input type="button" onclick="saveOrder()" value="Spara" class="Button">&nbsp;<% if (currentFactoryOrder.entryNo > 0) { %><input type="button" onclick="deleteOrder()" value="Ta bort" class="Button">&nbsp;<% } %><input type="button" onclick="goBack()" value="Avbryt" class="Button"></td>
						</tr>
						</table>							
					</td>
				</tr>
				</table>
				
			</table>
		</form>
	</body>
</HTML>