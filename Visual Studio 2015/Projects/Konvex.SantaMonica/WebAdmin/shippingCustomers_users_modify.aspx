<%@ Page language="c#" Codebehind="shippingCustomers_users_modify.aspx.cs" AutoEventWireup="false" Inherits="WebAdmin.shippingCustomers_users_modify" %>
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
			<input type="hidden" name="command" value="saveUser">
			<input type="hidden" name="shippingCustomerNo" value="<%= currentShippingCustomer.no %>">
			<input type="hidden" name="userId" value="<%= currentShippingCustomerUser.userId %>">
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame" style="border-bottom: 0 px">
				<tr>
					<td colspan="2" height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td colspan="2" class="activityName"><%

						if (currentShippingCustomerUser.userId != "")
						{
							%>Användare <%= currentShippingCustomerUser.userId %><%
						}
						else
						{
							%>Ny användare för kund <%= currentShippingCustomer.no %><%
						}
						
					%></td>
				</tr>
			</table>
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame" style="border-top: 0 px">
				<tr>
					<td valign="top">
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Användar-ID</td>
										</tr>
										<tr>
											<td class="interaction" height="20">
											<%
												if (currentShippingCustomerUser.userId != "")
												{
													%><%= currentShippingCustomerUser.userId %><input type="hidden" name="newUserId" value="<%= currentShippingCustomerUser.userId %>"><%
												}
												else
												{
													%><input type="text" name="newUserId" class="Textfield" size="20" maxlength="20" value="<%= Request["newUserId"] %>"><%
												}
												
											%>&nbsp;<font style="color: red;"><%= message %></font></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Namn</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><input type="text" name="name" class="Textfield" size="40" maxlength="29" value="<%= currentShippingCustomerUser.name %>"></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Lösenord</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><input type="text" name="password" class="Textfield" size="40" maxlength="29" value="<%= currentShippingCustomerUser.password %>"></td>
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