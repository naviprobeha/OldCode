<%@ Page language="c#" Codebehind="scheduled_orders_modify.aspx.cs" AutoEventWireup="false" Inherits="Navipro.SantaMonica.WebAdmin.scheduled_orders_modify" %>
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
					
			if ((proceed) && (document.thisform.phoneNo.value == ""))
			{
				alert("Du måste ange telefonnr.");
				proceed = false;
			}

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
				document.thisform.action = "scheduled_orders_modify.aspx";
				document.thisform.command.value = "saveOrder";
				document.thisform.submit();
			}
		
		}
		
		function deleteOrder()
		{
			if (confirm("Körordern kommer att raderas. Är du säker?") == true)
			{
				document.thisform.action = "scheduled_orders_modify.aspx";			
				document.thisform.command.value = "deleteOrder";
				document.thisform.submit();
			}
		}

		function goBack()
		{
			document.location.href='scheduled_orders.aspx';		
		}

		function setCustomer()
		{
			document.thisform.command.value = "setCustomer";
			document.thisform.submit();
		}

		function setShipAddress()
		{
			document.thisform.action = "scheduled_orders_modify.aspx";
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
					<td class="activityName"><% if (currentShipOrder.entryNo > 0) Response.Write("Ändra återkommande körorder "+currentShipOrder.organizationNo+currentShipOrder.entryNo); else Response.Write("Ny återkommande körorder"); %></td>
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
								<td class="interaction2" height="20">&nbsp;&nbsp;<%= currentShipOrder.customerName %></td>
							</tr>
							<tr>
								<td class="interaction" width="30%">&nbsp;<b>Adress:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<%= currentShipOrder.address %></td>
							</tr>
							<tr>
								<td class="interaction" width="30%">&nbsp;<b>Adress 2:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<%= currentShipOrder.address2 %></td>
							</tr>
							<tr>
								<td class="interaction" width="30%">&nbsp;<b>Postadress:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<%= currentShipOrder.postCode %>&nbsp;<%= currentShipOrder.city %></td>
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
								<td class="interaction2" height="20">&nbsp;&nbsp;<input type="text" name="shipName" class="Textfield" size="30" maxlength="30" value="<%= currentShipOrder.shipName %>">&nbsp;<input type="button" value="Spara adress" onclick="saveAddress()" class="button"></td>
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
								<td class="interaction" width="30%">&nbsp;<b>Betalsätt:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<select name="paymentType" class="Textfield">
								        <option value="0" <% if (currentShipOrder.paymentType == 0) Response.Write("selected"); %>>Faktura</option>
										<option value="1" <% if (currentShipOrder.paymentType == 1) Response.Write("selected"); %>>Kontant</option>
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
							<tr>
								<td class="interaction" width="30%" valign="top">&nbsp;<b>Dagar då order skall skapas:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<input type="checkbox" name="monday" <% if (currentShipOrder.monday) Response.Write("checked"); %>>&nbsp;Måndag<br>&nbsp;&nbsp;<input type="checkbox" name="tuesday" <% if (currentShipOrder.tuesday) Response.Write("checked"); %>>&nbsp;Tisdag<br>&nbsp;&nbsp;<input type="checkbox" name="wednesday" <% if (currentShipOrder.wednesday) Response.Write("checked"); %>>&nbsp;Onsdag<br>&nbsp;&nbsp;<input type="checkbox" name="thursday" <% if (currentShipOrder.thursday) Response.Write("checked"); %>>&nbsp;Torsdag<br>&nbsp;&nbsp;<input type="checkbox" name="friday" <% if (currentShipOrder.friday) Response.Write("checked"); %>>&nbsp;Fredag<br>&nbsp;&nbsp;<input type="checkbox" name="saturday" <% if (currentShipOrder.saturday) Response.Write("checked"); %>>&nbsp;Lördag<br>&nbsp;&nbsp;<input type="checkbox" name="sunday" <% if (currentShipOrder.sunday) Response.Write("checked"); %>>&nbsp;Söndag</td>
							</tr>
							<tr>
								<td class="interaction" width="30%" valign="top">&nbsp;<b>Veckor då order skall skapas:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<select name="weekType" class="Textfield">
								        <option value="0" <% if (currentShipOrder.weekType == 0) Response.Write("selected"); %>>Varje vecka</option>
										<option value="1" <% if (currentShipOrder.weekType == 1) Response.Write("selected"); %>>Jämna veckor</option>
										<option value="2" <% if (currentShipOrder.weekType == 2) Response.Write("selected"); %>>Udda veckor</option>
									</select></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td align="right" height="25"><% if (currentShipOrder.entryNo > 0) { %><input type="button" onclick="goBack();" value="Tillbaka" class="Button">&nbsp;<input type="button" onclick="deleteOrder()" value="Radera" class="Button">&nbsp;<% } %><input type="button" onclick="validate()" value="Spara" class="Button"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
