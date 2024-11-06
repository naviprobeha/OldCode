<%@ Page language="c#" Codebehind="shippingCustomers_schedules_modify.aspx.cs" AutoEventWireup="false" Inherits="WebAdmin.shippingCustomers_schedules_modify" %>
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
	

	<body MS_POSITIONING="GridLayout" topmargin="10">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" name="thisform" method="post" runat="server">
			<input type="hidden" name="command" value="saveSchedule">
			<input type="hidden" name="shippingCustomerNo" value="<%= currentShippingCustomer.no %>">
			<input type="hidden" name="scheduleEntryNo" value="<%= currentShippingCustomerSchedule.entryNo %>">
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame" style="border-bottom: 0 px">
				<tr>
					<td colspan="2" height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td colspan="2" class="activityName"><%

						if (currentShippingCustomerSchedule.entryNo > 0)
						{
							%>Schema för kund <%= currentShippingCustomer.no %><%
						}
						else
						{
							%>Nytt schema för kund <%= currentShippingCustomer.no %><%
						}
						
					%></td>
				</tr>
			</table>
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame" style="border-top: 0 px">
				<tr>
					<td valign="top">
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Typ</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><select name="type" class="Textfield"><option value="1">Fabriksorder</option></select></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Weckor</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><select name="week" class="Textfield"><option value="0">Alla veckor</option><option value="1">Jämna veckor</option><option value="2">Udda veckor</option></select></td>
										</tr>
									</table>
								</td>								
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Klockslag</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><select name="timeHour" class="Textfield">
											<%
												int hour = 0;
												while (hour < 24)
												{
													string hourString = hour.ToString();
													if (hourString.Length == 1) hourString = "0"+hourString;
													
													string selected = "";
													if (hour == currentShippingCustomerSchedule.time.Hour) selected = "selected";
													
													%><option value="<%= hour %>" <%= selected %>><%= hourString %></option><%
												
													hour++;
												}									
											%>
											</select>:<select name="timeMinute" class="Textfield">
											<%
												int minute = 0;
												while (minute < 60)
												{
													string minuteString = minute.ToString();
													if (minuteString.Length == 1) minuteString = "0"+minuteString;
													
													string selected = "";
													if (minute == currentShippingCustomerSchedule.time.Minute) selected = "selected";
													
													%><option value="<%= minute %>" <%= selected %>><%= minuteString %></option><%
												
													minute = minute + 15;
												}									
											%>
											</select></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" colspan="4">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Antal (ton)</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><input type="text" name="quantity" value="<%= currentShippingCustomerSchedule.quantity %>" class="Textfield"></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Måndagar</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><input type="checkbox" name="mondays" <% if (currentShippingCustomerSchedule.mondays) Response.Write("checked"); %>></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Tisdagar</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><input type="checkbox" name="tuesdays" <% if (currentShippingCustomerSchedule.tuesdays) Response.Write("checked"); %>></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Onsdagar</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><input type="checkbox" name="wednesdays" <% if (currentShippingCustomerSchedule.wednesdays) Response.Write("checked"); %>></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Torsdagar</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><input type="checkbox" name="thursdays" <% if (currentShippingCustomerSchedule.thursdays) Response.Write("checked"); %>></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Fredagar</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><input type="checkbox" name="fridays" <% if (currentShippingCustomerSchedule.fridays) Response.Write("checked"); %>></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Lördagar</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><input type="checkbox" name="saturdays" <% if (currentShippingCustomerSchedule.saturdays) Response.Write("checked"); %>></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Söndagar</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><input type="checkbox" name="sundays" <% if (currentShippingCustomerSchedule.sundays) Response.Write("checked"); %>></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<br>
						<input type="button" onclick="document.location.href='shippingCustomers_view.aspx?shippingCustomerNo=<%= currentShippingCustomer.no %>';" class="Button" value="Avbryt">&nbsp;<input type="submit" class="Button" value="Spara">
					</td>
				</tr>
			</table>
		</form>

		
	</body>
</HTML>