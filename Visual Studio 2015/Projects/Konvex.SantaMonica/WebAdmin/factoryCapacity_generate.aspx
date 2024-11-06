<%@ Page language="c#" Codebehind="factoryCapacity_generate.aspx.cs" AutoEventWireup="false" Inherits="WebAdmin.factoryCapacity_generate" %>
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
			
			if (document.thisform.capacity.value == "")
			{
				alert("Du måste ange kapacitet.");
				proceed = false;
			}		
		
			if (proceed)
			{
				document.thisform.command.value = "generate";
				document.thisform.submit();
			}
		
		}

		function changeShipDate()
		{
		}		

		function goBack()
		{
			document.location.href='factoryCapacity.aspx?factoryNo=<%= currentFactory.no %>';		
		}

		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" topmargin="10">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" action="orders_modify.aspx" method="post" runat="server">
			<input type="hidden" name="command" value="">
			<input type="hidden" name="factoryNo" value="<%= currentFactory.no %>">			
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame">
				<tr>
					<td height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td class="activityName">Kapacitet Fabrik</td>
				</tr>
				<tr>
					<td>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center" colspan="2">
									Planera kapacitet</th>
							</tr>
							<tr>
								<td class="interaction" width="30%">&nbsp;<b>Fabrik:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<%= currentFactory.name %></td>
							</tr>
							<tr>
								<td class="interaction" width="30%">&nbsp;<b>Från och med datum:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<% WebAdmin.HTMLHelper.createDatePicker("fromDate", System.DateTime.Today); %></td>
							</tr>
							<tr>
								<td class="interaction" width="30%">&nbsp;<b>Till och med datum:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<% WebAdmin.HTMLHelper.createDatePicker("toDate", System.DateTime.Today); %></td>
							</tr>
							<tr>
								<td class="interaction" width="30%">&nbsp;<b>Från och med klockslag:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<select name="fromHour" class="Textfield">
								<%
									int hour = 0;
									while (hour < 24)
									{
										string hourString = hour.ToString();
										if (hourString.Length == 1) hourString = "0"+hourString;
										
										string selected = "";
										if (hour == 0) selected = "selected";
										
										%><option value="<%= hour %>" <%= selected %>><%= hourString %>:00</option><%
									
										hour++;
									}									
								%>
								</select></td>
							</tr>
							<tr>
								<td class="interaction" width="30%">&nbsp;<b>Till och med klockslag:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<select name="toHour" class="Textfield">
								<%
									hour = 0;
									while (hour < 24)
									{
										string hourString = hour.ToString();
										if (hourString.Length == 1) hourString = "0"+hourString;
										
										string selected = "";
										if (hour == 23) selected = "selected";
										
										%><option value="<%= hour %>" <%= selected %>><%= hourString %>:00</option><%
									
										hour++;
									}									
								%>
								</select></td>
							</tr>
							<tr>
								<td class="interaction" width="30%">&nbsp;<b>Måndag:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<input type="checkbox" name="monday" checked></td>
							</tr>
							<tr>
								<td class="interaction" width="30%">&nbsp;<b>Tisdag:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<input type="checkbox" name="tuesday" checked></td>
							</tr>
							<tr>
								<td class="interaction" width="30%">&nbsp;<b>Onsdag:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<input type="checkbox" name="wednesday" checked></td>
							</tr>
							<tr>
								<td class="interaction" width="30%">&nbsp;<b>Torsdag:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<input type="checkbox" name="thursday" checked></td>
							</tr>
							<tr>
								<td class="interaction" width="30%">&nbsp;<b>Fredag:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<input type="checkbox" name="friday" checked></td>
							</tr>
							<tr>
								<td class="interaction" width="30%">&nbsp;<b>Lördag:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<input type="checkbox" name="saturday" checked></td>
							</tr>
							<tr>
								<td class="interaction" width="30%">&nbsp;<b>Söndag:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<input type="checkbox" name="sunday" checked></td>
							</tr>
							<tr>
								<td class="interaction" width="30%">&nbsp;<b>Planerad kapacitet:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<input type="text" name="capacity" class="Textfield" size="5" maxlength="5" value=""> containers/h</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td align="right" height="25"><input type="button" onclick="goBack();" value="Avbryt" class="Button">&nbsp;<input type="button" onclick="validate()" value="Skapa kapacitet" class="Button"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
