<%@ Page language="c#" Codebehind="mobileusers_modify.aspx.cs" AutoEventWireup="false" Inherits="Navipro.SantaMonica.WebAdmin.mobileusers_modify" %>
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
			
			if (document.thisform.name.value == "") 
			{
				alert("Du måste ange ett namn.");
				proceed = false;
			}
						
			if (proceed)
			{
				document.thisform.action = "mobileusers_modify.aspx";
				document.thisform.command.value = "save";
				document.thisform.submit();
			}
		
		}
		
		function deleteUser()
		{
			if (confirm("Användaren kommer att raderas. Är du säker?") == true)
			{
				document.thisform.command.value = "delete";
				document.thisform.submit();
			}
		}

		function updatePassword()
		{
			<% 
				if (currentMobileUser.entryNo > 0)
				{
					%>
					if (document.thisform.changePassword.checked)
					{
						document.thisform.password.disabled = false;
						document.thisform.password.className = "Textfield";
					}
					else
					{
						document.thisform.password.disabled = true;
						document.thisform.password.className = "TextfieldDisabled";
					}		
					<%
				}
			%>
		}

		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" topmargin="10" onload="updatePassword()">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" action="orders_modify.aspx" method="post" runat="server">
			<input type="hidden" name="command" value="">
			<input type="hidden" name="entryNo" value="<%= currentMobileUser.entryNo %>">
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame">
				<tr>
					<td height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td class="activityName">Mobilanvändare <% if (currentMobileUser.entryNo > 0) Response.Write(currentMobileUser.name); %></td>
				</tr>
				<tr>
					<td>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center" colspan="2">
									Skapa / ändra mobilanvändare</th>
							</tr>
							<tr>
								<td class="interaction" width="30%">&nbsp;<b>Namn:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<input type="text" name="name" class="Textfield" size="30" maxlength="30" value="<%= currentMobileUser.name %>"></td>
							</tr>
							<%
								if (currentMobileUser.entryNo > 0)
								{
									%>
									<tr>
										<td class="interaction" width="30%">&nbsp;<b>Lösenord:</b></td>
										<td class="interaction2" height="20">&nbsp;&nbsp;**********</td>
									</tr>
									<tr>
										<td class="interaction" width="30%">&nbsp;<b>Byt lösenord:</b></td>
										<td class="interaction2" height="20">&nbsp;&nbsp;<input type="checkbox" name="changePassword" onclick="updatePassword()"></td>
									</tr>								
									<tr>
										<td class="interaction" width="30%">&nbsp;<b>Nytt lösenord:</b></td>
										<td class="interaction2" height="20">&nbsp;&nbsp;<input type="password" name="password" class="Textfield" size="30" maxlength="30"></td>
									</tr>
									<%
								}
								else
								{
									%>
									<tr>
										<td class="interaction" width="30%">&nbsp;<b>Lösenord:</b><input type="hidden" name="changePassword" value="on"></td>
										<td class="interaction2" height="20">&nbsp;&nbsp;<input type="password" name="password" class="Textfield" size="30" maxlength="30"></td>
									</tr>
									<%
								}									
							%>
							
						</table>
					</td>
				</tr>
				<tr>
					<td align="right" height="25"><input type="button" onclick="document.location.href='mobileusers.aspx';" value="Tillbaka" class="Button">&nbsp;<% if (currentMobileUser.entryNo > 0) { %><input type="button" onclick="deleteUser()" value="Radera" class="Button">&nbsp;<% } %><input type="button" onclick="validate()" value="Spara" class="Button"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
