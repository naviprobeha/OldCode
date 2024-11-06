<%@ Page language="c#" Codebehind="orders_modify.aspx.cs" AutoEventWireup="false" Inherits="Navipro.SantaMonica.WebAdmin.orders_modify" %>
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
				document.thisform.action = "orders_modify.aspx";
				document.thisform.command.value = "saveOrder";
				document.thisform.submit();
			}
		
		}
		
		function deleteOrder()
		{
			if (confirm("Körordern kommer att raderas. Är du säker?") == true)
			{
				document.thisform.action = "orders_modify.aspx";			
				document.thisform.command.value = "deleteOrder";
				document.thisform.submit();
			}
		}

		function markOrder()
		{
			document.thisform.action = "orders_modify.aspx";			
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
			document.thisform.action = "orders_modify.aspx";
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
					<td>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center" colspan="2">
									Skapa / ändra körorder</th>
							</tr>
							<tr>
								<td class="interaction" width="30%">&nbsp;<b>Kundnr:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<%
									if (currentShipOrder.customerNo != "")
									{
										Response.Write(currentShipOrder.customerNo+", "+sellToCustomer.name);
									}
									else
									{
										Response.Write("Ej vald");
									}
									%>&nbsp;&nbsp;<input type="button" value="Hämta kund" class="Button" onclick="searchCustomer(1);"></td>
							</tr>
							<tr>
								<td class="interaction" width="30%">&nbsp;<b>Faktureras kundnr:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<%
									if (currentShipOrder.billToCustomerNo != "")
									{
										Response.Write(currentShipOrder.billToCustomerNo+", "+billToCustomer.name);
									}
									else
									{
										Response.Write("Ej vald");
									}
									 %>&nbsp;&nbsp;<input type="button" value="Hämta kund" class="Button" onclick="searchCustomer(2);"></td>
							</tr>
							<tr>
								<td class="interaction" width="30%">&nbsp;<b>Namn:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<input type="text" name="customerName" class="Textfield" size="30" maxlength="30" value="<%= currentShipOrder.customerName %>"></td>
							</tr>
							<tr>
								<td class="interaction" width="30%">&nbsp;<b>Adress:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<input type="text" name="address" class="Textfield" size="30" maxlength="30" value="<%= currentShipOrder.address %>"></td>
							</tr>
							<tr>
								<td class="interaction" width="30%">&nbsp;<b>Adress 2:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<input type="text" name="address2" class="Textfield" size="30" maxlength="30" value="<%= currentShipOrder.address2 %>"></td>
							</tr>
							<tr>
								<td class="interaction" width="30%">&nbsp;<b>Postadress:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<input type="text" name="postCode" class="Textfield" size="10" maxlength="20" value="<%= currentShipOrder.postCode %>">&nbsp;<input type="text" name="city" class="Textfield" size="30" maxlength="30" value="<%= currentShipOrder.city %>"></td>
							</tr>
							<tr>
								<td class="interaction" width="30%">&nbsp;<b>Telefonnr:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<input type="text" name="phoneNo" class="Textfield" size="20" maxlength="20" value="<%= currentShipOrder.phoneNo %>"></td>
							</tr>
							<tr>
								<td class="interaction" width="30%">&nbsp;<b>Mobiltelefonnr:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<input type="text" name="cellPhoneNo" class="Textfield" size="20" maxlength="20" value="<%= currentShipOrder.cellPhoneNo %>"></td>
							</tr>
							<tr>
								<td class="interaction" width="30%">&nbsp;<b>Produktionsplatsnr:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<input type="text" name="productionSite" class="Textfield" size="30" maxlength="30" value="<%= sellToCustomer.productionSite %>"></td>
							</tr>
							<tr>
								<td class="interaction2" width="30%">&nbsp;</td>
								<td class="interaction2" height="20">&nbsp;</td>
							</tr>
							<tr>
								<td class="interaction" width="30%">&nbsp;<b>Hämtadress:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<select name="customerShipAddressNo" class="Textfield" onchange="setShipAddress()">
								<option value="">-- Samma som kundadress --</option>
								<%
									int i3 = 0;
									while (i3 < customerShipAddressDataSet.Tables[0].Rows.Count)
									{
										%><option value="<%= customerShipAddressDataSet.Tables[0].Rows[i3].ItemArray.GetValue(2).ToString() %>" <% if (currentShipOrder.customerShipAddressNo == customerShipAddressDataSet.Tables[0].Rows[i3].ItemArray.GetValue(2).ToString()) Response.Write("selected"); %>><%= customerShipAddressDataSet.Tables[0].Rows[i3].ItemArray.GetValue(3).ToString()+", "+customerShipAddressDataSet.Tables[0].Rows[i3].ItemArray.GetValue(7).ToString() %></option><%
								
										i3++;	
									}
									
								%>
								</select>
								</td>
							</tr>
							<tr>
								<td class="interaction" width="30%">&nbsp;<b>Namn:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<input type="text" name="shipName" class="Textfield" size="30" maxlength="30" value="<%= currentShipOrder.shipName %>">&nbsp;<% if ((sellToCustomer != null) && (!sellToCustomer.editable)) { %><input type="button" value="Spara adress" onclick="saveAddress()" class="button"><% } %></td>
							</tr>
							<tr>
								<td class="interaction" width="30%">&nbsp;<b>Adress:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<input type="text" name="shipAddress" class="Textfield" size="30" maxlength="30" value="<%= currentShipOrder.shipAddress %>"></td>
							</tr>
							<tr>
								<td class="interaction" width="30%">&nbsp;<b>Adress 2:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<input type="text" name="shipAddress2" class="Textfield" size="30" maxlength="30" value="<%= currentShipOrder.shipAddress2 %>"></td>
							</tr>
							<tr>
								<td class="interaction" width="30%">&nbsp;<b>Postadress:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<input type="text" name="shipPostCode" class="Textfield" size="10" maxlength="20" value="<%= currentShipOrder.shipPostCode %>">&nbsp;<input type="text" name="shipCity" class="Textfield" size="30" maxlength="30" value="<%= currentShipOrder.shipCity %>"></td>
							</tr>
							<tr>
								<td class="interaction2" width="30%">&nbsp;</td>
								<td class="interaction2" height="20">&nbsp;</td>
							</tr>
							<tr>
								<td class="interaction" width="30%">&nbsp;<b>Anmälningsdatum:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<% createDatePicker("creationDate", currentShipOrder.creationDate); %></td>
							</tr>
							<tr>
								<td class="interaction" width="30%">&nbsp;<b>Hämtdatum:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<% createDatePicker("shipDate", currentShipOrder.shipDate); %></td>
							</tr>
							<tr>
								<td class="interaction" width="30%">&nbsp;<b>Betalsätt:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<select name="paymentType" class="Textfield">
										<option value="0" <% if (currentShipOrder.paymentType == 0) Response.Write("selected"); %>>Faktura</option>
										<option value="1" <% if (currentShipOrder.paymentType == 1) Response.Write("selected"); %>>Kontant</option>
									</select></td>
							</tr>
							<tr>
								<td class="interaction" width="30%">&nbsp;<b>Prioritet / Ordning:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<select name="priority" class="Textfield">
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
							<tr>
								<td class="interaction" width="30%" valign="top">&nbsp;<b>Vägbeskrivning:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<textarea name="directionComment" rows="3" cols="40"><%= currentShipOrder.directionComment+currentShipOrder.directionComment2 %></textarea></td>
							</tr>
							<tr>
								<td class="interaction" width="30%" valign="top">&nbsp;<b>Kommentar:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<textarea name="comments" rows="3" cols="40"><%= currentShipOrder.comments %></textarea></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td align="right" height="25"><% if (currentShipOrder.entryNo > 0) { %><input type="button" onclick="goBack();" value="Tillbaka" class="Button">&nbsp;<input type="button" onclick="markOrder();" value="Makulera" class="Button">&nbsp;<input type="button" onclick="deleteOrder()" value="Radera" class="Button">&nbsp;<% } %><input type="button" onclick="validate()" value="Spara" class="Button"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
