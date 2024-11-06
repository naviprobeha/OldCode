<%@ Page language="c#" Codebehind="lineorders_modify.aspx.cs" AutoEventWireup="false" Inherits="WebAdmin.lineorders_modify" %>
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
		
		function changeShipDate()
		{
		
		}
		
		
		function validate()
		{
			proceed = true;
					
			if ((proceed) && (document.thisform.directionComment.value.length > 500))
			{
				alert("Vägbeskrivningen får max vara 500 tkn");
				proceed = false;
			}

			if ((proceed) && (document.thisform.comments.value.length > 200))
			{
				alert("Kommentaren får max vara 200 tkn");
				proceed = false;
			}
		
			if (proceed)
			{
				document.thisform.action = "lineorders_modify.aspx";
				document.thisform.command.value = "saveOrder";
				document.thisform.submit();
			}
		
		}
		
		function deleteOrder()
		{
			if (confirm("Linjeordern kommer att raderas. Är du säker?") == true)
			{
				document.thisform.action = "lineorders_modify.aspx";			
				document.thisform.command.value = "deleteOrder";
				document.thisform.submit();
			}
		}

		function goBack()
		{
			document.location.href='lineorders.aspx';		
		}

		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" topmargin="10">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" action="orders_modify.aspx" method="post" runat="server">
			<input type="hidden" name="command"> <input type="hidden" name="lineOrderNo" value="<%= currentLineOrder.entryNo %>">
			<input type="hidden" name="positionX" value="<%= currentLineOrder.positionX %>">
			<input type="hidden" name="positionY" value="<%= currentLineOrder.positionY %>">
			<input type="hidden" name="mode">
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame">
				<tr>
					<td height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td class="activityName">Linjeorder</td>
				</tr>
				<tr>
					<td>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center" colspan="2">
									Skapa / ändra linjeorder</th>
							</tr>
							<tr>
								<td class="interaction" width="30%">&nbsp;<b>Kundnr:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<%
									if (currentLineOrder.shippingCustomerNo != "")
									{
										Response.Write(currentLineOrder.shippingCustomerNo+", "+currentLineOrder.shippingCustomerName);
									}
									else
									{
										Response.Write("Ej vald");
									}
									%></td>
							</tr>
							<tr>
								<td class="interaction" width="30%">&nbsp;<b>Namn:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<input type="text" name="shippingCustomerName" class="Textfield" size="30" maxlength="30" value="<%= currentLineOrder.shippingCustomerName %>"></td>
							</tr>
							<tr>
								<td class="interaction" width="30%">&nbsp;<b>Adress:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<input type="text" name="address" class="Textfield" size="30" maxlength="30" value="<%= currentLineOrder.address %>"></td>
							</tr>
							<tr>
								<td class="interaction" width="30%">&nbsp;<b>Adress 2:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<input type="text" name="address2" class="Textfield" size="30" maxlength="30" value="<%= currentLineOrder.address2 %>"></td>
							</tr>
							<tr>
								<td class="interaction" width="30%">&nbsp;<b>Postadress:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<input type="text" name="postCode" class="Textfield" size="10" maxlength="20" value="<%= currentLineOrder.postCode %>">&nbsp;<input type="text" name="city" class="Textfield" size="30" maxlength="30" value="<%= currentLineOrder.city %>"></td>
							</tr>
							<tr>
								<td class="interaction" width="30%">&nbsp;<b>Telefonnr:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<input type="text" name="phoneNo" class="Textfield" size="20" maxlength="20" value="<%= currentLineOrder.phoneNo %>"></td>
							</tr>
							<tr>
								<td class="interaction" width="30%">&nbsp;<b>Mobiltelefonnr:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<input type="text" name="cellPhoneNo" class="Textfield" size="20" maxlength="20" value="<%= currentLineOrder.cellPhoneNo %>"></td>
							</tr>
							<tr>
								<td class="interaction2" width="30%">&nbsp;</td>
								<td class="interaction2" height="20">&nbsp;</td>
							</tr>
							<tr>
								<td class="interaction" width="30%">&nbsp;<b>Anmälningsdatum:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<% WebAdmin.HTMLHelper.createDatePicker("creationDate", currentLineOrder.creationDate); %></td>
							</tr>
							<tr>
								<td class="interaction" width="30%">&nbsp;<b>Hämtdatum:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<% WebAdmin.HTMLHelper.createDatePicker("shipDate", currentLineOrder.shipDate); %></td>
							</tr>
							<tr>
								<td class="interaction" width="30%">&nbsp;<b>Klockslag:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<select name="shipTimeHour" class="Textfield">
								<%
									int hour = 0;
									while (hour < 24)
									{
										string hourString = hour.ToString();
										if (hourString.Length == 1) hourString = "0"+hourString;
										
										string selected = "";
										if (hour == currentLineOrder.shipTime.Hour) selected = "selected";
										
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
										if (minute == currentLineOrder.shipTime.Minute) selected = "selected";
										
										%><option value="<%= minute %>" <%= selected %>><%= minuteString %></option><%
									
										minute = minute + 15;
									}									
								%>
								</select></td>
							</tr>
							<tr>
								<td class="interaction2" width="30%">&nbsp;</td>
								<td class="interaction2" height="20">&nbsp;</td>
							</tr>
							<tr>
								<td class="interaction" width="30%" valign="top">&nbsp;<b>Vägbeskrivning:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<textarea name="directionComment" rows="3" cols="40"><%= currentLineOrder.directionComment+currentLineOrder.directionComment2 %></textarea></td>
							</tr>
							<tr>
								<td class="interaction" width="30%" valign="top">&nbsp;<b>Kommentar:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<textarea name="comments" rows="3" cols="40"><%= currentLineOrder.comments %></textarea></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td align="right" height="25"><% if (currentLineOrder.entryNo > 0) { %><input type="button" onclick="goBack();" value="Tillbaka" class="Button">&nbsp;<input type="button" onclick="deleteOrder()" value="Radera" class="Button">&nbsp;<% } %><input type="button" onclick="validate()" value="Spara" class="Button"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
