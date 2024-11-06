<%@ Page language="c#" Codebehind="customers_view.aspx.cs" AutoEventWireup="false" Inherits="WebAdmin.customers_view" %>
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
	
	function setPosition()
	{
		<% 
			if (Request["command"] == "setPosition")
			{
				%>
					positionX = window.event.x+document.body.scrollLeft-17;
					positionY = window.event.y+document.body.scrollTop-160;
					
					document.thisform.command.value = "savePosition";
					document.thisform.positionX.value = positionX;
					document.thisform.positionY.value = positionY;
					document.thisform.submit();			
				
				<%
			}
		%>
	}

	function clearPosition()
	{
					
		document.thisform.command.value = "clearPosition";
		document.thisform.submit();			
				
	}
	
	function deleteShipAddress(entryNo)
	{
		if (confirm("Du kommer att radera vald hämtadress. Är du säker?") == 1)
		{
			document.thisform.command.value = "deleteShipAddress";
			document.thisform.customerShipAddressEntryNo.value = entryNo;
			document.thisform.submit();
		}
	}

	function modifyDirectionComment()
	{
		document.location.href="customers_modify.aspx?customerNo=<%= Request["customerNo"] %>&organizationNo=<%= currentCustomer.organizationNo %>";
	}

	function showShipOrders()
	{
		document.location.href="customers_orders.aspx?customerNo=<%= Request["customerNo"] %>&organizationNo=<%= currentCustomer.organizationNo %>";
	}

	function showShipments()
	{
		document.location.href="customers_shipments.aspx?customerNo=<%= Request["customerNo"] %>&organizationNo=<%= currentCustomer.organizationNo %>";
	}
	
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" topmargin="10">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" name="thisform" method="post" runat="server">
			<input type="hidden" name="command"> <input type="hidden" name="positionX"> <input type="hidden" name="positionY">
			<input type="hidden" name="customerShipAddressEntryNo"> <input type="hidden" name="customerNo" value="<%= currentCustomer.no %>">
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
					<td width="50" valign="top"><iframe id="mapFrame" width="265" height="600" frameborder="no" scrolling="no" src="<%= mapServer.getUrl() %>" style="BORDER-BOTTOM: #000000 1px solid; BORDER-LEFT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-RIGHT: #000000 1px solid"></iframe>
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
											<td class="interaction" height="20"><b><%= currentCustomer.productionSite %></b></td>
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
											<td class="interaction" height="20"><b><%= currentCustomer.name %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Organisationsnr / Personnr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentCustomer.registrationNo %></b></td>
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
											<td class="interaction" height="20"><b><%= currentCustomer.address %></b></td>
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
											<td class="interaction" height="20"><b><%= currentCustomer.address2 %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Telefonnr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentCustomer.phoneNo %></b></td>
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
											<td class="interaction" height="20"><b><%= currentCustomer.postCode + " " + currentCustomer.city %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Mobiltelefonnr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentCustomer.cellPhoneNo %></b></td>
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
											<td class="interaction" height="20"><b><%= currentCustomer.dairyCode %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Mejerinr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentCustomer.dairyNo %></b></td>
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
											<td class="interaction" height="20"><b><%= currentCustomer.directionComment+currentCustomer.directionComment2 %></b></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<br>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Namn</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Adress</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Adress 2</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Postadress</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">
									Ändra</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">
									Ta bort</th>
							</tr>
							<%
							int j=0;
							while(j < customerShipAddressDataSet.Tables[0].Rows.Count)
							{
								%>
							<tr>
								<td class="jobDescription"><%= customerShipAddressDataSet.Tables[0].Rows[j].ItemArray.GetValue(3).ToString() %></td>
								<td class="jobDescription"><%= customerShipAddressDataSet.Tables[0].Rows[j].ItemArray.GetValue(4).ToString() %></td>
								<td class="jobDescription"><%= customerShipAddressDataSet.Tables[0].Rows[j].ItemArray.GetValue(5).ToString() %></td>
								<td class="jobDescription"><%= customerShipAddressDataSet.Tables[0].Rows[j].ItemArray.GetValue(6).ToString()+" "+customerShipAddressDataSet.Tables[0].Rows[j].ItemArray.GetValue(7).ToString() %></td>
								<td class="jobDescription" align="center"><a href="customers_shipAddress_modify.aspx?customerNo=<%= currentCustomer.no %>&amp;organizationNo=<%= currentCustomer.organizationNo %>&amp;customerShipAddressNo=<%= customerShipAddressDataSet.Tables[0].Rows[j].ItemArray.GetValue(2).ToString() %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
								<td class="jobDescription" align="center"><a href="javascript:deleteShipAddress('<%= customerShipAddressDataSet.Tables[0].Rows[j].ItemArray.GetValue(2).ToString() %>')"><img src="images/button_delete.gif" border="0" width="12" height="13"></a></td>
							</tr>
							<%
								
								j++;
							}
							
							if (j == 0)
							{
								%>
							<tr>
								<td colspan="6" class="jobDescription">Inga hämtadresser registrerade.</td>
							</tr>
							<%
								
							}
						%>
						</table>
						<br>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="interaction" height="20"><input type="button" value="Visa körorder" onclick="showShipOrders()" class="Button">&nbsp;<input type="button" value="Visa registrerade följesedlar" onclick="showShipments()" class="Button">&nbsp;<input type="button" value="Ändra kundinformation" onclick="modifyDirectionComment()" class="Button">&nbsp;<input type="button" value="Radera position" onclick="clearPosition()" class="Button">&nbsp;<% if (currentOrganization.callCenterMaster) { %><input type="button" value="Visa i Navision" onclick="document.location.href='navision://client/run?target=Form 50017&amp;view=SORTING(Field1)&amp;position=Field1=0(<%= currentCustomer.no %>)';" class="Button"><% } %></td>
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
