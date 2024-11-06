<%@ Page language="c#" Codebehind="messages_modify.aspx.cs" AutoEventWireup="false" Inherits="Navipro.SantaMonica.WebAdmin.messages_modify" %>
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
			
			if (document.thisform.sender.value == "") 
			{
				alert("Du måste ange en avsändare.");
				proceed = false;
			}
				
			if ((proceed) && (document.thisform.message.value == ""))
			{
				alert("Du måste ange ett meddelande.");
				proceed = false;
			}

			if ((proceed) && (document.thisform.message.value.length > 250))
			{
				alert("Meddelandet kan max vara 250 kecken långt.");
				proceed = false;
			}
			
			if (proceed)
			{
				document.thisform.submit();
			}
		
		}
		
		function deleteMessage()
		{
			if (confirm("Meddelandet kommer att raderas. Är du säker?") == true)
			{
				document.thisform.command.value = "deleteMessage";
				document.thisform.submit();
			}
		}
	
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" topmargin="10">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" method="post" runat="server">
			<input type="hidden" name="command" value="saveMessage">
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame">
				<tr>
					<td height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td class="activityName">Meddelande <% if (currentMessage.entryNo > 0) Response.Write(currentMessage.organizationNo+currentMessage.entryNo); %></td>
				</tr>
				<tr>
					<td>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center" colspan="2">
									Skapa / ändra meddelande</th>
							</tr>
							<tr>
								<td class="interaction" width="30%">&nbsp;<b>Avsändare:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<input type="text" name="sender" class="Textfield" size="30" maxlength="30" value="<%= currentMessage.fromName %>"></td>
							</tr>
							<tr>
								<td class="interaction" width="30%" valign="top">&nbsp;<b>Meddelande:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<textarea name="message" rows="6" cols="40"><%= currentMessage.message %></textarea></td>
							</tr>
							<tr>
								<td class="interaction" width="30%" valign="top">&nbsp;<b>Mottagare:</b></td>
								<td class="interaction2" height="20">
									<table cellspacing="0" cellpadding="2" width="100%" border="0">
									<%
										int i = 0;
										while (i < activeAgents.Tables[0].Rows.Count)
										{
											%>
											<tr>
												<td class="interaction2" style="font-size: 11px" width="50">&nbsp;&nbsp;<%= activeAgents.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %></td>
												<td class="interaction2" style="font-size: 11px" width="150"><%= activeAgents.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %></td>
												<td class="interaction2" style="font-size: 11px"><input type="checkbox" name="agent_<%= activeAgents.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>" <% if (messageAgents.getEntry(database, currentMessage.entryNo, activeAgents.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()) != null) Response.Write("checked"); %>></td>
											</tr>
											<%
											i++;
										}
									%>										
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td align="right" height="25"><input type="button" onclick="document.location.href='messages.aspx';" value="Tillbaka" class="Button">&nbsp;<% if (currentMessage.entryNo > 0) { %><input type="button" onclick="deleteMessage()" value="Radera" class="Button">&nbsp;<% } %><input type="button" onclick="validate()" value="Spara" class="Button"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
