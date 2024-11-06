<%@ Page language="c#" Codebehind="shippingCustomers_modify.aspx.cs" AutoEventWireup="false" Inherits="WebAdmin.shippingCustomers_modify" %>
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
		document.thisform.action = "shippingCustomers_modify.aspx";
		document.thisform.submit();
	}
	
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" topmargin="10">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" name="thisform" method="post" runat="server">
			<input type="hidden" name="command"> <input type="hidden" name="shippingCustomerNo" value="<%= currentShippingCustomer.no %>">
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame" style="BORDER-BOTTOM-WIDTH: 0px">
				<tr>
					<td colspan="2" height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td colspan="2" class="activityName">Kund
						<%= currentShippingCustomer.name %>
					</td>
				</tr>
			</table>
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame" style="BORDER-TOP-WIDTH: 0px">
				<tr>
					<td width="50" height="100%"><iframe id="mapFrame" width="265" height="100%" frameborder="no" scrolling="no" src="<%= mapServer.getUrl() %>" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"></iframe></td>
					<td valign="top">
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Kundnr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentShippingCustomer.no %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Produktionsplatsnr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><input type="text" class="Textfield" maxlength="20" name="productionSite" value="<%= currentShippingCustomer.productionSite %>"></td>
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
											<td class="interaction" height="20"><input type="text" class="Textfield" size="30" maxlength="30" name="name" value="<%= currentShippingCustomer.name %>"></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Organisationsnr / Personnr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><input type="text" class="Textfield" maxlength="20" name="registrationNo" value="<%= currentShippingCustomer.registrationNo %>"></td>
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
											<td class="interaction" height="20"><input type="text" class="Textfield" size="30" maxlength="30" name="address" value="<%= currentShippingCustomer.address %>"></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Uppföljningskod</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><select name="reasonCode" class="Textfield">
											<option value=""></option>
											<%
												int l = 0;
												while (l < reasonDataSet.Tables[0].Rows.Count)
												{
													string selected = "";
													if (currentShippingCustomer.reasonCode == reasonDataSet.Tables[0].Rows[l].ItemArray.GetValue(0).ToString()) selected = "selected";
													%><option value="<%= reasonDataSet.Tables[0].Rows[l].ItemArray.GetValue(0).ToString() %>" <%= selected %>><%= reasonDataSet.Tables[0].Rows[l].ItemArray.GetValue(1).ToString() %></option><%
											
													l++;
												}
											%>
											</select></td>
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
											<td class="interaction" height="20"><input type="text" class="Textfield" size="30" maxlength="30" name="address2" value="<%= currentShippingCustomer.address2 %>"></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Telefonnr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><input type="text" class="Textfield" maxlength="20" name="phoneNo" value="<%= currentShippingCustomer.phoneNo %>"></td>
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
											<td class="interaction" height="20"><input type="text" class="Textfield" size="10" maxlength="20" name="postCode" value="<%= currentShippingCustomer.postCode %>">&nbsp;<input type="text" class="Textfield" maxlength="20" name="city" value="<%= currentShippingCustomer.city %>"></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Mobiltelefonnr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><input type="text" class="Textfield" maxlength="20" name="cellPhoneNo" value="<%= currentShippingCustomer.cellPhoneNo %>"></td>
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
											<td class="interaction" height="20"><input type="text" class="Textfield" size="30" maxlength="30" name="contactName" value="<%= currentShippingCustomer.contactName %>"></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Faxnr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><input type="text" class="Textfield" maxlength="20" name="faxNo" value="<%= currentShippingCustomer.faxNo %>"></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">E-post</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><input type="text" class="Textfield" size="30" maxlength="100" name="email" value="<%= currentShippingCustomer.email %>"></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Mottagaranläggning</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><select name="preferedFactoryNo" class="Textfield">
											<option value=""></option>
											<%
												int k = 0;
												while (k < factoryDataSet.Tables[0].Rows.Count)
												{
													string selected = "";
													if (currentShippingCustomer.preferedFactoryNo == factoryDataSet.Tables[0].Rows[k].ItemArray.GetValue(0).ToString()) selected = "selected";
													%><option value="<%= factoryDataSet.Tables[0].Rows[k].ItemArray.GetValue(0).ToString() %>" <%= selected %>><%= factoryDataSet.Tables[0].Rows[k].ItemArray.GetValue(0).ToString() %>, <%= factoryDataSet.Tables[0].Rows[k].ItemArray.GetValue(1).ToString() %></option><%
											
													k++;
												}
											%>
											</select></td>
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
											<td class="interaction" height="20"><textarea name="directionComment" cols="60" rows="8"><%= currentShippingCustomer.directionComment+currentShippingCustomer.directionComment2 %></textarea></td>
										</tr>
										<tr>
											<td class="interaction" height="20"><br>
												<input type="button" value="Spara" onclick="validate()" class="Button"></td>
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
