<%@ Page language="c#" Codebehind="lineorders_assign.aspx.cs" AutoEventWireup="false" Inherits="WebAdmin.lineorders_assign" %>
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
		function validate()
		{
			proceed = true;
			
		
			if (proceed)
			{
				document.thisform.command.value = "assignOrder";
				document.thisform.submit();
			}
		
		}
		
		function changeShipDate()
		{
		
		}
	
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" topmargin="10">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" method="post" runat="server">
			<input type="hidden" name="command" value="">
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame" style="border-bottom: 0 px">
				<tr>
					<td height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td class="activityName">Linjeorder
						<%= currentLineOrder.entryNo %>
					</td>
				</tr>
			</table>
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame" style="border-top: 0 px">
				<tr>
					<td width="50" valign="top"><iframe id="mapFrame" width="265" height="600" frameborder="no" scrolling="no" src="<%= mapServer.getUrl() %>" style="border: 1px #000000 solid;"></iframe></td>
					<td valign="top">			
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Kundnamn</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentLineOrder.shippingCustomerName %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Kundnr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentLineOrder.shippingCustomerNo %></b></td>
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
											<td class="interaction" height="20"><b><%= currentLineOrder.address %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Adress 2</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentLineOrder.address2 %></b></td>
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
											<td class="interaction" height="20"><b><%= currentLineOrder.postCode %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Ort</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentLineOrder.city %></b></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Telefonnr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentLineOrder.phoneNo %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Mobiltelefonnr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentLineOrder.cellPhoneNo %></b></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<br>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Fraktinnehåll</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentLineOrder.details %></b></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Kommentarer</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentLineOrder.comments %></b></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Vägbeskrivning</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentLineOrder.directionComment+currentLineOrder.directionComment2 %></b></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<br>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<td class="interaction" valign="top" colspan="2">
									<%
									
										if ((Request["type"] == null) || (Request["type"] == "assign"))
										{
											%>									
											<table cellspacing="0" cellpadding="0" width="100%" border="0">
												<tr>
													<td class="activityAuthor" height="15" valign="top" width="100">Typ</td>
													<td class="activityAuthor" height="15" valign="top" width="200">Bil</td>
													<td class="activityAuthor" height="15" valign="top">Datum</td>
													<td class="activityAuthor" height="15" valign="top">Automat planering</td>
													<td class="activityAuthor" height="15" valign="top">Skapa ny rutt</td>
													<td class="activityAuthor" height="15" valign="top">&nbsp;</td>
												</tr>
												<tr>
													<td class="interaction" height="20"><select name="type" class="Textfield" onchange="document.thisform.submit()">
														<option value="assign" <% if (Request["type"] == "assign") Response.Write("selected"); %>>Automatisk</option>
														<option value="move" <% if (Request["type"] == "move") Response.Write("selected"); %>>Manuell</option>
													</select></td>
													<td class="interaction" height="20"><select name="agentCode" class="Textfield">
													<option value="">- Ej tilldelad -</option>
													<%
														string agentCode = "";
														if (currentLineOrderJournal != null) agentCode = currentLineOrderJournal.agentCode;
														DateTime shipDate = currentLineOrder.shipDate;
														if (currentLineOrderJournal != null) shipDate = currentLineOrderJournal.shipDate;
														
														int i = 0;
														while (i < activeAgents.Tables[0].Rows.Count)
														{
																										
															%>
															<option value="<%= activeAgents.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>" <% if (agentCode == activeAgents.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()) Response.Write("selected"); %>><%= activeAgents.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()+" "+activeAgents.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %></option>													
															<%
														
															i++;
														}
													
													
													%>
													
													</select></td>
													<td class="interaction" nowrap><% WebAdmin.HTMLHelper.createDatePicker("shipDate", shipDate); %></td>
													<td class="interaction"><input type="checkbox" name="autoPlan" <% if (currentLineOrder.enableAutoPlan) Response.Write("checked"); %>></td>
													<td class="interaction"><input type="checkbox" name="createNewJournal"></td>													
													<td class="interaction"><input type="button" onclick="validate()" value="Tilldela" class="Button"></td>													
												</tr>
											</table>
											<%
										}
										else
										{
										
											%>
											<table cellspacing="0" cellpadding="0" width="100%" border="0">
												<tr>
													<td class="activityAuthor" height="15" valign="top" width="100">Typ</td>
													<td class="activityAuthor" height="15" valign="top" width="200">Rutt</td>
												</tr>
												<tr>
													<td class="interaction" height="20"><select name="type" class="Textfield" onchange="document.thisform.submit()">
														<option value="assign" <% if (Request["type"] == "assign") Response.Write("selected"); %>>Automatisk</option>
														<option value="move" <% if (Request["type"] == "move") Response.Write("selected"); %>>Manuell</option>
													</select></td>
													<td class="interaction" height="20"><select name="lineJournalEntryNo" class="Textfield">
													<option value="0">- Ej tilldelad -</option>
													<%
														
														int i = 0;
														while (i < lineJournalDataSet.Tables[0].Rows.Count)
														{
																										
															%>
															<option value="<%= lineJournalDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>" <% if (currentLineOrder.lineJournalEntryNo == int.Parse(lineJournalDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString())) Response.Write("selected"); %>><%= DateTime.Parse(lineJournalDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString()).ToString("yyyy-MM-dd")+"-"+lineJournalDataSet.Tables[0].Rows[i].ItemArray.GetValue(3).ToString()+" ("+lineJournalDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()+")" %></option>													
															<%
														
															i++;
														}
													
													
													%>
													
													</select>&nbsp;<input type="button" onclick="validate()" value="Tilldela" class="Button"></td>
													
												</tr>
											</table>
											<%
										}
									%>
								</td>
							</tr>
						</table>									
					</td>
				</tr>
			</table>
		</form>
		
	
		
	</body>
</HTML>
