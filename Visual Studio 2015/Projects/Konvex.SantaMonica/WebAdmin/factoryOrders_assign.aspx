<%@ Page language="c#" Codebehind="factoryOrders_assign.aspx.cs" AutoEventWireup="false" Inherits="WebAdmin.factoryorders_assign" %>
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

		function validate()
		{
			
			document.thisform.action = "factoryOrders_assign.aspx";
			document.thisform.command.value = "assign";
			document.thisform.submit();
		}

		function updateDriver()
		{
			
			document.thisform.action = "factoryOrders_assign.aspx";
			document.thisform.command.value = "updateDriver";
			document.thisform.submit();
		}
	
		</script>
		
	</HEAD>
	<body MS_POSITIONING="GridLayout" topmargin="10">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" action="orders_view.aspx" method="post" runat="server">
			<input type="hidden" name="command" value="">
			<input type="hidden" name="factoryOrderNo" value="<%= currentFactoryOrder.entryNo %>"> 
			<input type="hidden" name="mode" value="">		
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame" style="border-bottom: 0 px">
				<tr>
					<td height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td class="activityName" colspan="2">Fabriksorder <% if (currentFactoryOrder.entryNo > 0) Response.Write(currentFactoryOrder.entryNo); %></td>
				</tr>
				<tr>
					<td align="left" height="25" colspan="2"><input type="button" onclick="goBack()" value="Tillbaka" class="Button"></td>
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
											<td class="activityAuthor" height="15" valign="top">Hämtdatum och klockslag</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.shipDate.ToString("yyyy-MM-dd") %>&nbsp;<%= currentFactoryOrder.shipTime.ToString("HH:mm") %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" width="250">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Uppläggningsdatum</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><% if (currentFactoryOrder.creationDate.Year != 1753) Response.Write(currentFactoryOrder.creationDate.ToString("yyyy-MM-dd")); %></b>&nbsp;</td>
										</tr>
									</table>
								</td>								
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Levererad datum och klockslag</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><% if (currentFactoryOrder.closedDateTime.ToString("yyyy-MM-dd") != "1753-01-01") Response.Write(currentFactoryOrder.closedDateTime.ToString("yyyy-MM-dd HH:mm")); %>&nbsp;</b></td>
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
											<td class="activityAuthor" height="15" valign="top">Fabrik</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.factoryName %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td colspan="2" class="activityAuthor" height="15" valign="top">Nr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.factoryNo %></b>&nbsp;</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Adress</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.factoryAddress %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Adress 2</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.factoryAddress2 %></b></td>
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
											<td class="interaction" height="20"><b><%= currentFactoryOrder.factoryPostCode %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" width="250">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Ort</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.factoryCity %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Telefonnr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.factoryPhoneNo %></b></td>
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
											<td class="activityAuthor" height="15" valign="top">Värmeverk</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.consumerName %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td colspan="2" class="activityAuthor" height="15" valign="top">Nr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.consumerNo %></b>&nbsp;</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Adress</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.consumerAddress %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Adress 2</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.consumerAddress2 %></b></td>
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
											<td class="interaction" height="20"><b><%= currentFactoryOrder.consumerPostCode %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" width="250">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Ort</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.consumerCity %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Telefonnr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.consumerPhoneNo %></b></td>
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
											<td class="interaction" height="20"><b><%= currentFactoryOrder.categoryCode %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" width="250">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Beskrivning</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.categoryDescription %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Antal</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentFactoryOrder.quantity.ToString() %> ton</b></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<br>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<% if ((mobileUserDataSet != null) && (mobileUserDataSet.Tables[0].Rows.Count > 0)) { %>							
								<td class="interaction" valign="top" width="300">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Chaufför lastning</td>
										</tr>
										<td class="interaction" height="20"><select name="loadDriverName" class="Textfield" onchange="updateDriver()">
										<option value="">- Ej vald -</option>
										<%
											int j = 0;
											while (j < mobileUserDataSet.Tables[0].Rows.Count)
											{													
												%>
												<option value="<%= mobileUserDataSet.Tables[0].Rows[j].ItemArray.GetValue(2).ToString() %>" <% if (currentFactoryOrder.driverName == mobileUserDataSet.Tables[0].Rows[j].ItemArray.GetValue(2).ToString()) Response.Write("selected"); %>><%= mobileUserDataSet.Tables[0].Rows[j].ItemArray.GetValue(2).ToString() %></option>													
												<%
											
												j++;
											}
										
										
										%>
										</select></td>
									</table>
								</td>
								<td class="interaction" valign="top" width="250">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top" width="300">Chaufför lossning</td>
										</tr>
										<td class="interaction" height="20"><select name="dropDriverName" class="Textfield" onchange="updateDriver()">
										<option value="">- Samma som lastning -</option>
										<%
											j = 0;
											while (j < mobileUserDataSet.Tables[0].Rows.Count)
											{													
												%>
												<option value="<%= mobileUserDataSet.Tables[0].Rows[j].ItemArray.GetValue(2).ToString() %>" <% if (currentFactoryOrder.dropDriverName == mobileUserDataSet.Tables[0].Rows[j].ItemArray.GetValue(2).ToString()) Response.Write("selected"); %>><%= mobileUserDataSet.Tables[0].Rows[j].ItemArray.GetValue(2).ToString() %></option>													
												<%
											
												j++;
											}
										
										
										%>
										</select></td>
									</table>										
								</td>
								<% } %>
								
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Bil</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><select name="agentCode" class="Textfield">
											<option value="">- Ej tilldelad -</option>
											<%
												int i = 0;
												while (i < activeAgents.Tables[0].Rows.Count)
												{
													
													%>
													<option value="<%= activeAgents.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>" <% if (currentFactoryOrder.agentCode == activeAgents.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()) Response.Write("selected"); %>><%= activeAgents.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()+" "+activeAgents.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %></option>													
													<%
												
													i++;
												}
											
											
											%>										
											</select>&nbsp;<input type="button" onclick="validate()" value="Tilldela" class="Button"></td>
										</tr>
									</table>
								</td>								
							</tr>
						</table>						
						<table cellspacing="1" cellpadding="2" border="0" width="100%">
						<tr>
							<td align="right" height="25"><input type="button" onclick="goBack()" value="Tillbaka" class="Button"></td>
						</tr>
						</table>							
					</td>
				</tr>
				</table>
				
			</table>
		</form>
	</body>
</HTML>