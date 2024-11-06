<%@ Page language="c#" Codebehind="messages.aspx.cs" AutoEventWireup="false" Inherits="Navipro.SantaMonica.WebAdmin.messages" %>
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
	<body MS_POSITIONING="GridLayout" topmargin="10" onload="setTimeout('document.thisform.submit()', 30000)">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" method="post" runat="server">
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame">
				<tr>
					<td height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td class="activityName">Meddelanden</td>
				</tr>
				<tr>
					<td align="left"><input type="button" value="Nytt meddelande" class="button" onclick="document.location.href='messages_modify.aspx';"></td>
				</tr>			
				<tr>
					<td>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Nr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Avsändare</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Meddelande</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Mottagare</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">
									Ändra</th>
							</tr>
							<%
								string lastMessageNo = "";
								int i = 0;
								
								while (i < unassignedMessages.Tables[0].Rows.Count)
								{
									if (lastMessageNo != unassignedMessages.Tables[0].Rows[i].ItemArray.GetValue(1).ToString())
									{
								
										%>
										<tr>
											<td class="jobDescription" valign="top"><%= unassignedMessages.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() + unassignedMessages.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %></td>
											<td class="jobDescription" valign="top"><%= unassignedMessages.Tables[0].Rows[i].ItemArray.GetValue(2).ToString() %></td>
											<td class="jobDescription" valign="top"><%= unassignedMessages.Tables[0].Rows[i].ItemArray.GetValue(3).ToString() %></td>
											<td class="jobDescription" valign="top">
												<table cellspacing="0" cellpadding="2" width="100%" border="0">
												<%
													System.Data.DataSet messageAgentDataSet = messageAgents.getDataSet(database, currentOrganization.no, unassignedMessages.Tables[0].Rows[i].ItemArray.GetValue(1).ToString());
													
													int j = 0;
													while (j < messageAgentDataSet.Tables[0].Rows.Count)
													{
														Navipro.SantaMonica.Common.MessageAgent messageAgent = messageAgents.getEntry(database, int.Parse(unassignedMessages.Tables[0].Rows[i].ItemArray.GetValue(1).ToString()), messageAgentDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString());
														
														string statusText = messageAgent.getStatusText();																							
														string statusIcon = messageAgent.getStatusIcon();
														
														%>
														<tr>
															<td class="jobDescription" width="30"><%= messageAgentDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString() %></td>
															<td class="jobDescription" width="50"><%= statusText %></td>
															<td class="jobDescription" width="10" align="center"><img src="images/<%= statusIcon %>" border="0"></td>
															<td class="jobDescription" align="center" width="130"><% if (messageAgent.status == 2) Response.Write(messageAgent.ackDateTime); %></td>
														</tr>
														<%								
									
									
														j++;
													}
													
												%>											
												</table>
											</td>
											<td class="jobDescription" valign="top" align="center"><a href="messages_modify.aspx?messageEntryNo=<%= unassignedMessages.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
										</tr>
										<%


									}
									
									lastMessageNo = unassignedMessages.Tables[0].Rows[i].ItemArray.GetValue(1).ToString();
								
									i++;
								}
							
								i = 0;
								while (i < unreadMessages.Tables[0].Rows.Count)
								{
									if (lastMessageNo != unreadMessages.Tables[0].Rows[i].ItemArray.GetValue(1).ToString())
									{
								
										%>
										<tr>
											<td class="jobDescription" valign="top"><%= unreadMessages.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() + unreadMessages.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %></td>
											<td class="jobDescription" valign="top"><%= unreadMessages.Tables[0].Rows[i].ItemArray.GetValue(2).ToString() %></td>
											<td class="jobDescription" valign="top"><%= unreadMessages.Tables[0].Rows[i].ItemArray.GetValue(3).ToString() %></td>
											<td class="jobDescription" valign="top">
												<table cellspacing="0" cellpadding="2" width="100%" border="0">
												<%
													System.Data.DataSet messageAgentDataSet = messageAgents.getDataSet(database, currentOrganization.no, unreadMessages.Tables[0].Rows[i].ItemArray.GetValue(1).ToString());
													
													int j = 0;
													while (j < messageAgentDataSet.Tables[0].Rows.Count)
													{
														Navipro.SantaMonica.Common.MessageAgent messageAgent = messageAgents.getEntry(database, int.Parse(unreadMessages.Tables[0].Rows[i].ItemArray.GetValue(1).ToString()), messageAgentDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString());
														
														string statusText = messageAgent.getStatusText();																							
														string statusIcon = messageAgent.getStatusIcon();
														
														%>
														<tr>
															<td class="jobDescription" width="30"><%= messageAgentDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString() %></td>
															<td class="jobDescription" width="50"><%= statusText %></td>
															<td class="jobDescription" width="10" align="center"><img src="images/<%= statusIcon %>" border="0"></td>
															<td class="jobDescription" align="center" width="130"><% if (messageAgent.status == 2) Response.Write(messageAgent.ackDateTime); %></td>
														</tr>
														<%								
									
									
														j++;
													}
													
												%>											
												</table>
											</td>
											<td class="jobDescription" valign="top" align="center"><a href="messages_modify.aspx?messageEntryNo=<%= unreadMessages.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
										</tr>
										<%

									}
									
									lastMessageNo = unreadMessages.Tables[0].Rows[i].ItemArray.GetValue(1).ToString();
								
									i++;
								}


								i = 0;
								lastMessageNo = "";
								
								while (i < readMessages.Tables[0].Rows.Count)
								{
									if (lastMessageNo != readMessages.Tables[0].Rows[i].ItemArray.GetValue(1).ToString())
									{
								
										%>
										<tr>
											<td class="jobDescription" valign="top"><%= readMessages.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() + readMessages.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %></td>
											<td class="jobDescription" valign="top"><%= readMessages.Tables[0].Rows[i].ItemArray.GetValue(2).ToString() %></td>
											<td class="jobDescription" valign="top"><%= readMessages.Tables[0].Rows[i].ItemArray.GetValue(3).ToString() %></td>
											<td class="jobDescription" valign="top">
												<table cellspacing="0" cellpadding="2" width="100%" border="0">
												<%
													System.Data.DataSet messageAgentDataSet = messageAgents.getDataSet(database, currentOrganization.no, readMessages.Tables[0].Rows[i].ItemArray.GetValue(1).ToString());
													
													int j = 0;
													while (j < messageAgentDataSet.Tables[0].Rows.Count)
													{
														Navipro.SantaMonica.Common.MessageAgent messageAgent = messageAgents.getEntry(database, int.Parse(readMessages.Tables[0].Rows[i].ItemArray.GetValue(1).ToString()), messageAgentDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString());
														
														string statusText = messageAgent.getStatusText();																							
														string statusIcon = messageAgent.getStatusIcon();
														
														%>
														<tr>
															<td class="jobDescription" width="30"><%= messageAgentDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString() %></td>
															<td class="jobDescription" width="50"><%= statusText %></td>
															<td class="jobDescription" width="10" align="center"><img src="images/<%= statusIcon %>" border="0"></td>
															<td class="jobDescription" align="center" width="130"><% if (messageAgent.status == 2) Response.Write(messageAgent.ackDateTime); %></td>
														</tr>
														<%								
									
									
														j++;
													}
													
												%>											
												</table>
											</td>
											<td class="jobDescription" valign="top" align="center"><a href="messages_modify.aspx?messageEntryNo=<%= readMessages.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
										</tr>
										<%
									}
									
									lastMessageNo = readMessages.Tables[0].Rows[i].ItemArray.GetValue(1).ToString();
									
									i++;
								}
							
							%>
						</table>
					</td>
				</tr>
				<tr>
					<td align="right"><input type="button" value="Nytt meddelande" class="button" onclick="document.location.href='messages_modify.aspx';"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
