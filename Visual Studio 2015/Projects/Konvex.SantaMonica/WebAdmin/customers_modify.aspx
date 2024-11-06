<%@ Page language="c#" Codebehind="customers_modify.aspx.cs" AutoEventWireup="false" Inherits="WebAdmin.customers_modify" %>
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
		document.thisform.command.value = "saveCustomer";
		document.thisform.action = "customers_modify.aspx";
		document.thisform.submit();
	}
	
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" topmargin="10">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" name="thisform" method="post" runat="server">
			<input type="hidden" name="command"> <input type="hidden" name="positionX"> <input type="hidden" name="positionY">
			<input type="hidden" name="customerShipAddressEntryNo"> <input type="hidden" name="customerNo" value="<%= currentCustomer.no %>">
			<input type="hidden" name="organizationNo" value="<%= currentCustomer.organizationNo %>">
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame" style="BORDER-BOTTOM-WIDTH: 0px">
				<tr>
					<td colspan="2" height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td colspan="2" class="activityName">Kund
						<%= currentCustomer.name %>
					</td>
				</tr>
			</table>
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame" style="BORDER-TOP-WIDTH: 0px">
				<tr>
					<td width="50"><iframe id="mapFrame" width="265" height="600" frameborder="no" scrolling="no" src="<%= mapServer.getUrl() %>" style="BORDER-BOTTOM: #000000 1px solid; BORDER-LEFT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-RIGHT: #000000 1px solid"></iframe>
					</td>
					<td valign="top">
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Kundnr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentCustomer.no %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Produktionsplatsnr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><input class="Textfield" maxlength="30" name="productionSite" value="<%= currentCustomer.productionSite %>"><% if (productionSiteErrorMessage != "") { %><br>
												<span style="COLOR: red; FONT-SIZE: 10px">
													<%= productionSiteErrorMessage %>
												</span>
												<% } %>
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Namn</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><input class="Textfield" size="50" maxlength="50" name="name" value="<%= currentCustomer.name %>"></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Organisationsnr / Personnr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b></b><%= currentCustomer.registrationNo %></B></td>
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
											<td class="interaction" height="20"><input class="Textfield" size="50" maxlength="50" name="address" value="<%= currentCustomer.address %>"></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">&nbsp;</td>
										</tr>
										<tr>
											<td class="interaction" height="20">&nbsp;</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Adress 2</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><input class="Textfield" size="50" maxlength="50" name="address2" value="<%= currentCustomer.address2 %>"></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Telefonnr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><input class="Textfield" maxlength="20" name="phoneNo" value="<%= currentCustomer.phoneNo %>"></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Postadress</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><input class="Textfield" maxlength="20" name="postCode" value="<%= currentCustomer.postCode %>">
												<input class="Textfield" maxlength="50" name="city" value="<%= currentCustomer.city %>"></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Mobiltelefonnr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><input class="Textfield" maxlength="20" name="cellPhoneNo" value="<%= currentCustomer.cellPhoneNo %>"></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Kontaktperson</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentCustomer.contactName %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Faxnr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentCustomer.faxNo %></b></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">E-post</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><a href="mailto:<%= currentCustomer.email %>"><%= currentCustomer.email %></a></b></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Mejerikod</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><input class="Textfield" maxlength="30" name="dairyCode" value="<%= currentCustomer.dairyCode %>"></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Mejerinr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><input class="Textfield" maxlength="30" name="dairyNo" value="<%= currentCustomer.dairyNo %>"></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<br>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Vägbeskrivning</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><textarea name="directionComment" cols="60" rows="8"></textarea><script> document.thisform.directionComment.value = "<%= currentCustomer.directionComment+currentCustomer.directionComment2 %>"; </script></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<br>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="interaction" height="20"><input type="button" value="Spara" onclick="validate()" class="Button"></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
