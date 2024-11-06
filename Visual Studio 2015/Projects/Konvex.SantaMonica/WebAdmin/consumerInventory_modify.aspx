<%@ Page language="c#" Codebehind="consumerInventory_modify.aspx.cs" AutoEventWireup="false" Inherits="WebAdmin.consumerInventory_generate" %>
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
			
			if (document.thisform.inventory.value == "")
			{
				alert("Du måste ange lagernivå.");
				proceed = false;
			}		
		
			if (proceed)
			{
				document.thisform.command.value = "modify";
				document.thisform.submit();
			}
		
		}

		function calcInventory()
		{
			document.location.href = "consumerInventory_modify.aspx?date=<%= currentDateTime.ToString("yyyy-MM-dd") %>&hour=<%= currentDateTime.ToString("HH") %>&consumerNo=<%= currentConsumer.no %>&command=calcInventory";			
		}


		function goBack()
		{
			document.location.href='consumerInventory.aspx?consumerNo=<%= currentConsumer.no %>';		
		}

		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" topmargin="10">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" action="consumerInventory_modify.aspx" method="post" runat="server">
			<input type="hidden" name="command" value="">
			<input type="hidden" name="consumerNo" value="<%= currentConsumer.no %>">			
			<input type="hidden" name="date" value="<%= currentDateTime.ToString("yyyy-MM-dd") %>">			
			<input type="hidden" name="hour" value="<%= currentDateTime.ToString("HH") %>">			
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
					<td>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center" colspan="2">
									Planera kapacitet</th>
							</tr>
							<tr>
								<td class="interaction" width="30%">&nbsp;<b>Värmeverk:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<%= currentConsumer.name %></td>
							</tr>
							<tr>
								<td class="interaction" width="30%">&nbsp;<b>Datum:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<%= currentDateTime.ToString("yyyy-MM-dd") %></td>
							</tr>
							<tr>
								<td class="interaction" width="30%">&nbsp;<b>Klockslag:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<%= currentDateTime.ToString("HH:mm") %></td>
							</tr>
							<tr>
								<td class="interaction" width="30%">&nbsp;<b>Nivå:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<input type="text" name="inventory" class="Textfield" size="5" maxlength="5" value="<%= currentConsumerInventory.inventory %>"> ton</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td align="right" height="25"><input type="button" onclick="goBack();" value="Avbryt" class="Button">&nbsp;<input type="button" onclick="calcInventory()" value="Uppdatera lager" class="Button">&nbsp;<input type="button" onclick="validate()" value="Spara" class="Button"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
