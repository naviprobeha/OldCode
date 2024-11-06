<%@ Page language="c#" Codebehind="linejournals_modify.aspx.cs" AutoEventWireup="false" Inherits="WebAdmin.linejournals_modify" %>
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
				document.thisform.action = "linejournals_modify.aspx";
				document.thisform.command.value = "saveJournal";
				document.thisform.submit();
			}
		
		}
		function goBack()
		{
			document.location.href='linejournals_view.aspx?lineJournalNo=<%= currentLineJournal.entryNo %>';		
		}

	
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" topmargin="10">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" action="linejournals_modify.aspx" method="post" runat="server">
			<input type="hidden" name="command" value="">
			<input type="hidden" name="lineJournalNo" value="<%= currentLineJournal.entryNo %>"> 
			<input type="hidden" name="mode" value="">		
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame">
				<tr>
					<td height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td class="activityName">Ändra rutt <% if (currentLineJournal.entryNo > 0) Response.Write(currentLineJournal.shipDate.ToString("yyyy-MM-dd")+"-"+currentLineJournal.agentCode); %></td>
				</tr>
				<tr>
					<td>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<td class="interaction" valign="top" width="400">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Transportör</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentLineJournal.organizationNo %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Status</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentLineJournal.getStatusText(database) %></b>&nbsp;</td>
										</tr>
									</table>
								</td>								
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Bil</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><select name="agentCode" class="Textfield">
											<option value="">-- Ej tilldelad --</option>
											<%
												int i = 0;
												while (i < agentDataSet.Tables[0].Rows.Count)
												{
													%><option value="<%= agentDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>" <% if (currentLineJournal.agentCode == agentDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()) Response.Write("selected"); %> ><%= agentDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>, <%= agentDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %></option><%
													i++;	
												}
											
											%>
											</select>&nbsp;</td>
										</tr>
									</table>
								</td>								
							</tr>
							<tr>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Beräknad avfärdstid</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><% if (currentLineJournal.departureDateTime.ToString("yyyy") != "1753") Response.Write(currentLineJournal.departureDateTime.ToString("yyyy-MM-dd HH:mm")); %></b>&nbsp;</td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Beräknad ankomsttid</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><% if (currentLineJournal.departureDateTime.ToString("yyyy") != "1753") Response.Write(currentLineJournal.arrivalDateTime.ToString("yyyy-MM-dd HH:mm")); %></b>&nbsp;</td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" width="400">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Hämtdatum</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentLineJournal.shipDate.ToString("yyyy-MM-dd") %></b></td>
										</tr>
									</table>
								</td>
							</tr>
							<%
								if (currentOrganization.allowLineOrderSupervision)
								{
									%>
									<tr>
										<td class="interaction" valign="top">
											<table cellspacing="0" cellpadding="0" width="100%" border="0">
												<tr>
													<td class="activityAuthor" height="15" valign="top">Utgår ifrån</td>
												</tr>
												<tr>
													<td class="interaction" height="20"><select name="departureFactoryCode" class="Textfield">
													<%
														i = 0;
														while (i < factoryDataSet.Tables[0].Rows.Count)
														{
															%><option value="<%= factoryDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>" <% if (currentLineJournal.departureFactoryCode == factoryDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()) Response.Write("selected"); %> ><%= factoryDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %></option><%
															i++;	
														}
													
													%>
													</select></td>
												</tr>
											</table>
										</td>
										<td class="interaction" valign="top">
											<table cellspacing="0" cellpadding="0" width="100%" border="0">
												<tr>
													<td class="activityAuthor" height="15" valign="top">Slutstation</td>
												</tr>
												<tr>
													<td class="interaction" height="20"><select name="arrivalFactoryCode" class="Textfield">
													<%
														i = 0;
														while (i < factoryDataSet.Tables[0].Rows.Count)
														{
															%><option value="<%= factoryDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>" <% if (currentLineJournal.arrivalFactoryCode == factoryDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()) Response.Write("selected"); %> ><%= factoryDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %></option><%
															i++;	
														}
													
													%>
													</select></td>
												</tr>
											</table>
										</td>
										<td class="interaction" valign="top">
											<table cellspacing="0" cellpadding="0" width="100%" border="0">
												<tr>
													<td class="activityAuthor" height="15" valign="top">Lastningsgrupp</td>
												</tr>
												<tr>
													<td class="interaction" height="20"><b><%= currentLineJournal.getAgentStorageGroupDescription(database) %></b>&nbsp;</td>
												</tr>
											</table>
										</td>								
									</tr>
									<%
								}
								else
								{
									%>
									<tr>
										<td class="interaction" valign="top">
											<table cellspacing="0" cellpadding="0" width="100%" border="0">
												<tr>
													<td class="activityAuthor" height="15" valign="top">Utgår ifrån</td>
												</tr>
												<tr>
													<td class="interaction" height="20"><%= currentLineJournal.departureFactoryCode %><input type="hidden" name="departureFactoryCode" value="<%= currentLineJournal.departureFactoryCode %>"></td>
												</tr>
											</table>
										</td>
										<td class="interaction" valign="top">
											<table cellspacing="0" cellpadding="0" width="100%" border="0">
												<tr>
													<td class="activityAuthor" height="15" valign="top">Slutstation</td>
												</tr>
												<tr>
													<td class="interaction" height="20"><%= currentLineJournal.arrivalFactoryCode %><input type="hidden" name="arrivalFactoryCode" value="<%= currentLineJournal.arrivalFactoryCode %>"></td>
												</tr>
											</table>
										</td>
										<td class="interaction" valign="top">
											<table cellspacing="0" cellpadding="0" width="100%" border="0">
												<tr>
													<td class="activityAuthor" height="15" valign="top">Lastningsgrupp</td>
												</tr>
												<tr>
													<td class="interaction" height="20"><b><%= currentLineJournal.getAgentStorageGroupDescription(database) %></b>&nbsp;</td>
												</tr>
											</table>
										</td>								
									</tr>
									<%
								}
							%>
						</table>
					</td>
				</tr>
				<tr>
					<td align="right" height="25"><input type="button" onclick="goBack()" value="Tillbaka" class="Button">&nbsp;<input type="button" onclick="validate()" value="Spara" class="Button"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>