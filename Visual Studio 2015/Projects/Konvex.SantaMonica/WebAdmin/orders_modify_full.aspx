<%@ Page language="c#" Codebehind="orders_modify_full.aspx.cs" AutoEventWireup="false" Inherits="Navipro.SantaMonica.WebAdmin.orders_modify_full" %>
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
				//if (confirm("Vill du uppdatera kundkortet med berörd information?"))
				//{
					document.thisform.action = "orders_modify_full.aspx?update=true";
				//}
				//else
				//{
					document.thisform.action = "orders_modify_full.aspx?update=true";
				//}
				document.thisform.command.value = "saveOrder";
				document.thisform.submit();
			}
		
		}
		
		function deleteOrder()
		{
			if (confirm("Körordern kommer att raderas. Är du säker?") == true)
			{
				document.thisform.action = "orders_modify_full.aspx";			
				document.thisform.command.value = "deleteOrder";
				document.thisform.submit();
			}
		}

		function clearAdminFee()
		{
			if (confirm("Den administrativa avgiften kommer att sättas till 0 kr. Är du säker?") == true)
			{
				document.thisform.action = "orders_modify_full.aspx";			
				document.thisform.command.value = "clearAdminFee";
				document.thisform.submit();
			}
		}

		function markOrder()
		{
			document.thisform.action = "orders_modify_full.aspx";			
			document.thisform.command.value = "markOrder";
			document.thisform.submit();
		}

		function goBack()
		{
			document.location.href='orders.aspx';		
		}

		function setCustomer()
		{
			document.thisform.command.value = "setCustomer";
			document.thisform.submit();
		}

		function setShipAddress()
		{
			document.thisform.action = "orders_modify_full.aspx";
			document.thisform.command.value = "setShipAddress";
			document.thisform.submit();
		}

		function saveAddress()
		{
			document.thisform.command.value = "saveAddress";
			document.thisform.submit();
		}

		function searchCustomer(mode)
		{
			document.thisform.command.value = "searchCustomer";
			document.thisform.mode.value = mode;
			document.thisform.submit();
	
		}
		
		function addCallMsg()
		{
			if (document.thisform.comments.value.indexOf("Ring innan.") == -1) 
			{
				document.thisform.comments.value = document.thisform.comments.value + " Ring innan.";
			}
			if (document.thisform.comments.value[0] != "X") 
			{
				document.thisform.comments.value = "X"+document.thisform.comments.value;
			}
		}

		function addItem()
		{
			if (document.thisform.quantity.value == "")
			{
				alert("Antal måste anges.");
				return;
			}
			
			document.thisform.action = "orders_modify_full.aspx#lines";
			document.thisform.command.value = "addItem";
			document.thisform.submit();
		}
		
		function deleteItem(lineNo)
		{
			if (confirm("Du kommer att radera vald rad. Är du säker?"))
			{
				document.thisform.action = "orders_modify_full.aspx?lineNo="+lineNo;		
				document.thisform.command.value = "deleteLine";
				document.thisform.submit();		
			}
		
		}
		
		<%
			if (notifyUserAboutPayment)
			{
				%>alert("Betalningssätt ändrat till kontant.");<%
			}
		%>


		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" topmargin="10">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" action="orders_modify.aspx" method="post" runat="server">
			<input type="hidden" name="command" value="">
			<input type="hidden" name="shipOrderNo" value="<%= currentShipOrder.entryNo %>"> 
			<input type="hidden" name="positionX" value="<%= currentShipOrder.positionX %>">
			<input type="hidden" name="positionY" value="<%= currentShipOrder.positionY %>">
			<input type="hidden" name="customerNo" value="<%= currentShipOrder.customerNo %>">
			<input type="hidden" name="billToCustomerNo" value="<%= currentShipOrder.billToCustomerNo %>">	
			<input type="hidden" name="mode" value="">	
			<% 
			System.Collections.ArrayList orgRelations = (System.Collections.ArrayList)Session["current.user.relations"];
			if (orgRelations.Count == 1) { %><input type="hidden" name="organizationNo" value="<%= currentShipOrder.organizationNo %>"><% } %>
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame">
				<tr>
					<td height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td class="activityName"><% if (currentShipOrder.entryNo > 0) Response.Write("Ändra körorder "+currentShipOrder.entryNo); else Response.Write("Ny körorder"); %></td>
				</tr>
				<tr>
					<td align="left" height="25"><% if (currentShipOrder.entryNo > 0) { %><input type="button" onclick="goBack();" value="Tillbaka" class="Button">&nbsp;<input type="button" onclick="markOrder();" value="Makulera" class="Button">&nbsp;<input type="button" onclick="deleteOrder()" value="Radera" class="Button">&nbsp;<% } %><input type="button" onclick="validate()" value="Spara" class="Button"></td>
				</tr>
				<tr>
					<td>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<td class="interaction" valign="top" width="250">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Hämtdatum</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><% createDatePicker("shipDate", currentShipOrder.shipDate); %></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" width="250">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Anmälningsdatum</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><% createDatePicker("creationDate", currentShipOrder.creationDate); %></td>
										</tr>
									</table>
								</td>								
								<%
									if (orgRelations.Count > 1)
									{
										%>						
										<td class="interaction" valign="top" width="250">
											<table cellspacing="0" cellpadding="0" width="100%" border="0">
												<tr>
													<td class="activityAuthor" height="15" valign="top">Transportör</td>
												</tr>
												<tr>
													<td class="interaction" height="20"><select name="organizationNo" class="Textfield">
														<%
															
															Navipro.SantaMonica.Common.Organizations organizations = new Navipro.SantaMonica.Common.Organizations();
														
															int l = 0;
															while(l < orgRelations.Count)
															{
																Navipro.SantaMonica.Common.Organization organization = organizations.getOrganization(database, ((Navipro.SantaMonica.Common.OrganizationOperator)orgRelations[l]).organizationNo, false);

																%><option value="<%= organization.no %>" <% if (organization.no == currentShipOrder.organizationNo) Response.Write("selected"); %>><%= organization.name %></option><%

																l++;
															}
														
														%>
													</select></td>
												</tr>
											</table>
										</td>								
										<%
									}
								%>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Prioritet</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><select name="priority" class="Textfield">
												<option value="8" <% if (currentShipOrder.priority == 8) Response.Write("selected"); %>>Normal</option>
												<option value="1" <% if (currentShipOrder.priority == 1) Response.Write("selected"); %>>1</option>
												<option value="2" <% if (currentShipOrder.priority == 2) Response.Write("selected"); %>>2</option>
												<option value="3" <% if (currentShipOrder.priority == 3) Response.Write("selected"); %>>3</option>
												<option value="4" <% if (currentShipOrder.priority == 4) Response.Write("selected"); %>>4</option>
												<option value="5" <% if (currentShipOrder.priority == 5) Response.Write("selected"); %>>5</option>
												<option value="6" <% if (currentShipOrder.priority == 6) Response.Write("selected"); %>>6</option>
												<option value="7" <% if (currentShipOrder.priority == 7) Response.Write("selected"); %>>7</option>
												<option value="8" <% if (currentShipOrder.priority == 8) Response.Write("selected"); %>>8</option>
												<option value="9" <% if (currentShipOrder.priority == 9) Response.Write("selected"); %>>9</option>
											</select></td>
										</tr>
									</table>
								</td>		
							</tr>
						</table>
						<br>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<td class="interaction" valign="top" width="250">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Kundnr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%
														if (currentShipOrder.customerNo != "")
														{
															Response.Write(currentShipOrder.customerNo);
														}
														else
														{
															Response.Write("Ej vald");
														}
														%></b>&nbsp;&nbsp;<input type="button" value="Hämta kund" class="Button" onclick="searchCustomer(1);"></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" width="250">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Namn</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= sellToCustomer.name %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Betalsätt</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><select name="paymentType" class="Textfield">
										<option value="0" <% if (currentShipOrder.paymentType == 0) Response.Write("selected"); %>>Faktura</option>
										<option value="1" <% if (currentShipOrder.paymentType == 1) Response.Write("selected"); %>>Kontant</option>
									</select></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<br>					
						<table cellspacing="0" cellpadding="0" border="0" width="100%">
						<tr>
							<td style="padding-right: 2px" valign="top" width="512">						
								<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
									<tr>
										<td class="interaction" valign="top">
											<table cellspacing="0" cellpadding="0" width="100%" border="0">
												<tr>
													<td colspan="2" class="activityAuthor" height="15" valign="top">Faktureras kundnr</td>
												</tr>
												<tr>
													<td class="interaction" height="20"><b><%
												if (currentShipOrder.billToCustomerNo != "")
												{
													Response.Write(currentShipOrder.billToCustomerNo);
												}
												else
												{
													Response.Write("Ej vald");
												}
												%></b>&nbsp;&nbsp;<input type="button" value="Hämta kund" class="Button" onclick="searchCustomer(2);"></td>
												</tr>
											</table>
										</td>
										<td class="interaction" valign="top">
											<table cellspacing="0" cellpadding="0" width="100%" border="0">
												<tr>
													<td class="activityAuthor" height="15" valign="top">Produktionsplatsnr</td>
												</tr>
												<tr>
													<td class="interaction" height="20"><b><%= billToCustomer.productionSite %></b></td>
												</tr>
											</table>
										</td>
									</tr>
									<tr>
										<td class="interaction" valign="top" colspan="2">
											<table cellspacing="0" cellpadding="0" width="100%" border="0">
												<tr>
													<td class="activityAuthor" height="15" valign="top">Kundnamn</td>
												</tr>
												<tr>
													<td class="interaction" height="20"><input type="text" name="customerName" class="Textfield" size="30" maxlength="30" value="<%= currentShipOrder.customerName %>" onchange="if (document.thisform.customerShipAddressNo.value == '') document.thisform.shipName.value = document.thisform.customerName.value;"></td>
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
													<td class="interaction" height="20"><input type="text" name="address" class="Textfield" size="30" maxlength="30" value="<%= currentShipOrder.address %>" onchange="if (document.thisform.customerShipAddressNo.value == '') document.thisform.shipAddress.value = document.thisform.address.value;"></td>
												</tr>
											</table>
										</td>
										<td class="interaction" valign="top">
											<table cellspacing="0" cellpadding="0" width="100%" border="0">
												<tr>
													<td class="activityAuthor" height="15" valign="top">Adress 2</td>
												</tr>
												<tr>
													<td class="interaction" height="20"><input type="text" name="address2" class="Textfield" size="30" maxlength="30" value="<%= currentShipOrder.address2 %>" onchange="if (document.thisform.customerShipAddressNo.value == '') document.thisform.shipAddress2.value = document.thisform.address2.value;"></td>
												</tr>
											</table>
										</td>
									</tr>
									<tr>
										<td class="interaction" valign="top">
											<table cellspacing="0" cellpadding="0" width="100%" border="0">
												<tr>
													<td class="activityAuthor" height="15" valign="top">Postnr</td>
												</tr>
												<tr>
													<td class="interaction" height="20"><input type="text" name="postCode" class="Textfield" size="10" maxlength="20" value="<%= currentShipOrder.postCode %>" onchange="if (document.thisform.customerShipAddressNo.value == '') document.thisform.shipPostCode.value = document.thisform.postCode.value;"></td>
												</tr>
											</table>
										</td>
										<td class="interaction" valign="top">
											<table cellspacing="0" cellpadding="0" width="100%" border="0">
												<tr>
													<td class="activityAuthor" height="15" valign="top">Ort</td>
												</tr>
												<tr>
													<td class="interaction" height="20"><input type="text" name="city" class="Textfield" size="30" maxlength="30" value="<%= currentShipOrder.city %>" onchange="if (document.thisform.customerShipAddressNo.value == '') document.thisform.shipCity.value = document.thisform.city.value;"></td>
												</tr>
											</table>
										</td>
									</tr>
									<tr>
										<td class="interaction" valign="top">
											<table cellspacing="0" cellpadding="0" width="100%" border="0">
												<tr>
													<td class="activityAuthor" height="15" valign="top">Telefonnr</td>
												</tr>
												<tr>
													<td class="interaction" height="20"><b><%= billToCustomer.phoneNo %></b></td>
												</tr>
											</table>
										</td>
										<td class="interaction" valign="top">
											<table cellspacing="0" cellpadding="0" width="100%" border="0">
												<tr>
													<td class="activityAuthor" height="15" valign="top">Mobiltelefonnr</td>
												</tr>
												<tr>
													<td class="interaction" height="20"><b><%= billToCustomer.cellPhoneNo %></b></td>
												</tr>
											</table>
										</td>
									</tr>
								</table>
							</td>
							
							
							
							<td style="padding-left: 2px" valign="top">
								<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
									<tr>
										<td class="interaction" valign="top">
											<table cellspacing="0" cellpadding="0" width="100%" border="0">
												<tr>
													<td class="activityAuthor" height="15" valign="top">Hämtadress</td>
												</tr>
												<tr>
													<td class="interaction" height="20"><select name="customerShipAddressNo" class="Textfield" onchange="setShipAddress()" <% if (customerShipAddressDataSet.Tables[0].Rows.Count > 0) Response.Write("style=\"background-color: FFCCFF;\""); %>>
														<option value="">-- Samma som kundadress --</option>
														<%
															int i3 = 0;
															while (i3 < customerShipAddressDataSet.Tables[0].Rows.Count)
															{
																%><option value="<%= customerShipAddressDataSet.Tables[0].Rows[i3].ItemArray.GetValue(2).ToString() %>" <% if (currentShipOrder.customerShipAddressNo == customerShipAddressDataSet.Tables[0].Rows[i3].ItemArray.GetValue(2).ToString()) Response.Write("selected"); %>><%= customerShipAddressDataSet.Tables[0].Rows[i3].ItemArray.GetValue(4).ToString()+", "+customerShipAddressDataSet.Tables[0].Rows[i3].ItemArray.GetValue(7).ToString() %></option><%
														
																i3++;	
															}
															
														%>
														</select></td>
												</tr>
											</table>
										</td>
										<td class="interaction" valign="top">
											<table cellspacing="0" cellpadding="0" width="100%" border="0">
												<tr>
													<td class="activityAuthor" height="15" valign="top">Produktionsplatsnr</td>
												</tr>
												<tr>
												<%
													if (currentShipOrder.productionSite == "") { %>
														<td class="interaction" height="20"><input type="text" name="productionSite" class="Textfield" size="30" maxlength="30" style="background-color: FFCCFF;" value="<%= currentShipOrder.productionSite %>"><% if (productionSiteErrorMessage != "") { %><br/><span style="color: red; font-size: 10px;"><%= productionSiteErrorMessage %></span><% } %></td>
													<% } else { %>
														<td class="interaction" height="20"><input type="text" name="productionSite" class="Textfield" size="30" maxlength="30" value="<%= currentShipOrder.productionSite %>"><% if (productionSiteErrorMessage != "") { %><br/><span style="color: red; font-size: 10px;"><%= productionSiteErrorMessage %></span><% } %></td>
													<% } %>
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
													<td class="interaction" height="20"><input type="text" name="shipName" class="Textfield" size="30" maxlength="30" value="<%= currentShipOrder.shipName %>"></td>
												</tr>
											</table>
										</td>
										<td class="interaction" valign="bottom">&nbsp;<% if ((sellToCustomer != null) && (!sellToCustomer.editable)) { %><input type="button" value="Spara adress" onclick="saveAddress()" class="button"><% } %></td>
									</tr>
									<tr>
										<td class="interaction" valign="top">
											<table cellspacing="0" cellpadding="0" width="100%" border="0">
												<tr>
													<td class="activityAuthor" height="15" valign="top">Adress</td>
												</tr>
												<tr>
													<td class="interaction" height="20"><input type="text" name="shipAddress" class="Textfield" size="30" maxlength="30" value="<%= currentShipOrder.shipAddress %>"></td>
												</tr>
											</table>
										</td>
										<td class="interaction" valign="top">
											<table cellspacing="0" cellpadding="0" width="100%" border="0">
												<tr>
													<td class="activityAuthor" height="15" valign="top">Adress 2</td>
												</tr>
												<tr>
													<td class="interaction" height="20"><input type="text" name="shipAddress2" class="Textfield" size="30" maxlength="30" value="<%= currentShipOrder.shipAddress2 %>"></td>
												</tr>
											</table>
										</td>
									</tr>
									<tr>
										<td class="interaction" valign="top">
											<table cellspacing="0" cellpadding="0" width="100%" border="0">
												<tr>
													<td class="activityAuthor" height="15" valign="top">Postnr</td>
												</tr>
												<tr>
													<td class="interaction" height="20"><input type="text" name="shipPostCode" class="Textfield" size="10" maxlength="20" value="<%= currentShipOrder.shipPostCode %>"></td>
												</tr>
											</table>
										</td>
										<td class="interaction" valign="top">
											<table cellspacing="0" cellpadding="0" width="100%" border="0">
												<tr>
													<td class="activityAuthor" height="15" valign="top">Ort</td>
												</tr>
												<tr>
													<td class="interaction" height="20"><input type="text" name="shipCity" class="Textfield" size="30" maxlength="30" value="<%= currentShipOrder.shipCity %>"></td>
												</tr>
											</table>
										</td>
									</tr>
									<tr>
										<td class="interaction" valign="top">
											<table cellspacing="0" cellpadding="0" width="100%" border="0">
												<tr>
													<td class="activityAuthor" height="15" valign="top">Telefonnr</td>
												</tr>
												<tr>
												<%
													if (currentShipOrder.phoneNo == "") { %>
														<td class="interaction" height="20"><input type="text" name="phoneNo" class="Textfield" size="20" maxlength="20" style="background-color: FFCCFF;" value="<%= currentShipOrder.phoneNo %>"></td>
													<% } else { %>
														<td class="interaction" height="20"><input type="text" name="phoneNo" class="Textfield" size="20" maxlength="20" value="<%= currentShipOrder.phoneNo %>"></td>
													<% } %>
												</tr>
											</table>
										</td>
										<td class="interaction" valign="top">
											<table cellspacing="0" cellpadding="0" width="100%" border="0">
												<tr>
													<td class="activityAuthor" height="15" valign="top">Mobiltelefonnr</td>
												</tr>
												<tr>
												<%
													if (currentShipOrder.cellPhoneNo == "") { %>
														<td class="interaction" height="20"><input type="text" name="cellPhoneNo" class="Textfield" size="20" maxlength="20" style="background-color: FFCCFF;" value="<%= currentShipOrder.cellPhoneNo %>"></td>
													<% } else { %>
														<td class="interaction" height="20"><input type="text" name="cellPhoneNo" class="Textfield" size="20" maxlength="20" value="<%= currentShipOrder.cellPhoneNo %>"></td>
													<% } %>
												</tr>
											</table>
										</td>
									</tr>
								</table>
							</td>
						</tr>
						</table>
						<br>							
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<td class="interaction" valign="top" width="503">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Vägbeskrivning</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><textarea name="directionComment" rows="3" cols="40"><%= currentShipOrder.directionComment+currentShipOrder.directionComment2 %></textarea></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Kommentar</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><textarea name="comments" rows="3" cols="40"><%= currentShipOrder.comments %></textarea>&nbsp;<input type="button" value="Ring innan" class="Button" style="vertical-align: 7px;" onclick="addCallMsg()"/></td>
										</tr>
									</table>
								</td>
							</tr>							
						</table>
						<br>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Produktionsplatsnr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentShipOrder.productionSite %></b></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<br>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Fraktinnehåll</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentShipOrder.details %></b></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<br>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">Artikelnr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">Beskrivning</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right">Antal</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right">Enhet</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right">Pris</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right">Avlivningar</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right">Pris avlivning</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right">Provtagningar</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right">Totalbelopp</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" colspan="2">ID</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca">Ta bort</th>	
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca">Ändra</th>	
							</tr>
							<%
						
								int i = 0;
								while (i < shipOrderLineDataSet.Tables[0].Rows.Count)
								{
									string price = decimal.Round(decimal.Parse(shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(6).ToString()), 2)+" kr";
									string connectionPrice = decimal.Round(decimal.Parse(shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(8).ToString()), 2)+" kr";
									string totalAmount = decimal.Round(decimal.Parse(shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(9).ToString()), 2)+" kr";

									Navipro.SantaMonica.Common.Item item = items.getEntry(database, shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString());
							
									%>
									<tr>
										<td class="jobDescription" valign="top"><%= shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString() %></td>
										<td class="jobDescription" valign="top"><%= item.description %></td>
										<td class="jobDescription" valign="top" align="right"><%= shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(3).ToString() %></td>
										<td class="jobDescription" valign="top" align="right"><%= item.unitOfMeasure %></td>
										<td class="jobDescription" valign="top" align="right"><%= price %></td>
										<td class="jobDescription" valign="top" align="right"><%= shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() %></td>
										<td class="jobDescription" valign="top" align="right"><%= connectionPrice %></td>
										<td class="jobDescription" valign="top" align="right"><%= shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(11).ToString() %></td>
										<td class="jobDescription" valign="top" align="right"><%= totalAmount %></td>
										<td class="jobDescription" valign="top" nowrap><%
										
											System.Data.DataSet shipOrderLineIdDataSet = shipOrderLineIds.getDataSet(database, currentShipOrder.entryNo, int.Parse(shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()));
											
											int m = 0;
											
											while (m < shipOrderLineIdDataSet.Tables[0].Rows.Count)
											{
												string bseValue = "";
												string postMortemValue = "";
												if (shipOrderLineIdDataSet.Tables[0].Rows[m].ItemArray.GetValue(4).ToString() == "1") bseValue = "(P)";
												if (shipOrderLineIdDataSet.Tables[0].Rows[m].ItemArray.GetValue(5).ToString() == "1") postMortemValue = "(O)";
												
												
												Response.Write(shipOrderLineIdDataSet.Tables[0].Rows[m].ItemArray.GetValue(3).ToString()+bseValue+postMortemValue+"<br>");
												m++;
											}
										
										
										%></td>
										<td class="jobDescription" valign="top" align="center"><a href="orders_id.aspx?shipOrderNo=<%= Request["shipOrderNo"] %>&shipOrderlineNo=<%= shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
										<td class="jobDescription" valign="top" align="center"><a href="javascript:deleteItem('<%= shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>');"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
										<td class="jobDescription" valign="top" align="center"><a href="orders_items_modify.aspx?shipOrderNo=<%= Request["shipOrderNo"] %>&entryNo=<%= shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
									</tr>
									<%
								
									i++;
								}

								if (i == 0)
								{
									%><tr>
										<td class="jobDescription" colspan="13">Inga rader registrerade.</td>
									</tr><%
								}
							
							%>
						</table>
						<br><a name="lines"></a>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">Artikel</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right">Antal</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right">Avlivningar</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">ID</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">Provt.</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">Obd.</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca">Val</th>	
							</tr>
							<tr>
								<td class="jobDescription" valign="top"><select name="itemNo" class="Textfield">
									<%
										int o = 0;
										
										while (o < itemDataSet1.Tables[0].Rows.Count)
										{	
											string cashPayment = "";
											if (itemDataSet1.Tables[0].Rows[o].ItemArray.GetValue(11).ToString() == "1") cashPayment = "[Kontant]";
											
											%>
											<option value="<%= itemDataSet1.Tables[0].Rows[o].ItemArray.GetValue(0).ToString() %>"><%= itemDataSet1.Tables[0].Rows[o].ItemArray.GetValue(0).ToString() %>, <%= itemDataSet1.Tables[0].Rows[o].ItemArray.GetValue(1).ToString() %> [<%= itemDataSet1.Tables[0].Rows[o].ItemArray.GetValue(8).ToString() %>] <%= cashPayment %></option>
											<%
												
											o++;
										}
									
									
										o = 0;
										while (o < itemDataSet2.Tables[0].Rows.Count)
										{							
											string cashPayment = "";
											if (itemDataSet2.Tables[0].Rows[o].ItemArray.GetValue(11).ToString() == "1") cashPayment = "[Kontant]";

											%>
											<option value="<%= itemDataSet2.Tables[0].Rows[o].ItemArray.GetValue(0).ToString() %>"><%= itemDataSet2.Tables[0].Rows[o].ItemArray.GetValue(0).ToString() %>, <%= itemDataSet2.Tables[0].Rows[o].ItemArray.GetValue(1).ToString() %> [<%= itemDataSet2.Tables[0].Rows[o].ItemArray.GetValue(8).ToString() %>] <%= cashPayment %></option>
											<%
												
											o++;
										}

										o = 0;
										while (o < itemDataSet3.Tables[0].Rows.Count)
										{							
											string cashPayment = "";
											if (itemDataSet3.Tables[0].Rows[o].ItemArray.GetValue(11).ToString() == "1") cashPayment = "[Kontant]";

											%>
											<option value="<%= itemDataSet3.Tables[0].Rows[o].ItemArray.GetValue(0).ToString() %>"><%= itemDataSet3.Tables[0].Rows[o].ItemArray.GetValue(0).ToString() %>, <%= itemDataSet3.Tables[0].Rows[o].ItemArray.GetValue(1).ToString() %> [<%= itemDataSet3.Tables[0].Rows[o].ItemArray.GetValue(8).ToString() %>] <%= cashPayment %></option>
											<%
												
											o++;
										}

										o = 0;
										while (o < itemDataSet4.Tables[0].Rows.Count)
										{							
											string cashPayment = "";
											if (itemDataSet4.Tables[0].Rows[o].ItemArray.GetValue(11).ToString() == "1") cashPayment = "[Kontant]";

											%>
											<option value="<%= itemDataSet4.Tables[0].Rows[o].ItemArray.GetValue(0).ToString() %>"><%= itemDataSet4.Tables[0].Rows[o].ItemArray.GetValue(0).ToString() %>, <%= itemDataSet4.Tables[0].Rows[o].ItemArray.GetValue(1).ToString() %> [<%= itemDataSet4.Tables[0].Rows[o].ItemArray.GetValue(8).ToString() %>] <%= cashPayment %></option>
											<%
												
											i++;
										}
										
									
									%>
								</select></td>
								<td class="jobDescription" valign="top" align="right"><input type="text" name="quantity" class="Textfield" size="6" maxlength="5"></td>
								<td class="jobDescription" valign="top" align="right"><input type="text" name="connectionQuantity" class="Textfield" size="6" maxlength="5"></td>
								<td class="jobDescription" valign="top" align="left"><input type="text" name="id1" class="Textfield" maxlength="20">&nbsp;<input type="button" class="Button" name="se1" value="SE-nr" onclick="document.thisform.id1.value='SE <%= currentShipOrder.productionSite %>-'+document.thisform.id1.value"><br><input type="text" name="id2" class="Textfield" maxlength="20">&nbsp;<input type="button" class="Button" name="se2" value="SE-nr" onclick="document.thisform.id2.value='SE <%= currentShipOrder.productionSite %>-'+document.thisform.id2.value"><br><input type="text" name="id3" class="Textfield" maxlength="20">&nbsp;<input type="button" class="Button" name="se3" value="SE-nr" onclick="document.thisform.id3.value='SE <%= currentShipOrder.productionSite %>-'+document.thisform.id3.value"><br><input type="text" name="id4" class="Textfield" maxlength="20">&nbsp;<input type="button" class="Button" name="se4" value="SE-nr" onclick="document.thisform.id4.value='SE <%= currentShipOrder.productionSite %>-'+document.thisform.id4.value"><br><input type="text" name="id5" class="Textfield" maxlength="20">&nbsp;<input type="button" class="Button" name="se5" value="SE-nr" onclick="document.thisform.id5.value='SE <%= currentShipOrder.productionSite %>-'+document.thisform.id5.value"></td>
								<td class="jobDescription" valign="top" align="center"><input type="checkbox" name="bse1"><br><input type="checkbox" name="bse2"><br><input type="checkbox" name="bse3"><br><input type="checkbox" name="bse4"><br><input type="checkbox" name="bse5"></td>
								<td class="jobDescription" valign="top" align="center"><input type="checkbox" name="obd1"><br><input type="checkbox" name="obd2"><br><input type="checkbox" name="obd3"><br><input type="checkbox" name="bd4"><br><input type="checkbox" name="obd5"></td>
								<td class="jobDescription" valign="top" align="center"><input type="button" value="Lägg till" class="button" onclick="addItem()"></td>
							</tr>
							
						</table>
						
					</td>
				</tr>
				<tr>
					<td align="right" height="25"><% if (currentShipOrder.entryNo > 0) { %><input type="button" onclick="goBack();" value="Tillbaka" class="Button">&nbsp;<% if (currentOrganization.callCenterMaster) { %><input type="button" onclick="clearAdminFee()" value="Nolla administrativ avgift" class="Button">&nbsp;<% } %><input type="button" onclick="markOrder();" value="Makulera" class="Button">&nbsp;<input type="button" onclick="deleteOrder()" value="Radera" class="Button">&nbsp;<% } %><input type="button" onclick="validate()" value="Spara" class="Button"></td>
				</tr>
			</table>			
		</form>
	</body>
</HTML>
