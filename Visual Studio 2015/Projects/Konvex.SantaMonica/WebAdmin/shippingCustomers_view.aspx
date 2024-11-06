<%@ Page language="c#" Codebehind="shippingCustomers_view.aspx.cs" AutoEventWireup="false" Inherits="WebAdmin.shippingCustomer_view" %>
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
	
	<script>
	
	function deleteUser(userId)
	{
		if (confirm("Du kommer att radera användare "+userId+". Är du säker?") == 1)
		{
			document.thisform.command.value = "deleteUser";
			document.thisform.userId.value = userId;
			document.thisform.submit();
		}
	}

	function showLineOrders()
	{
		document.location.href="shippingCustomers_lineOrders.aspx?shippingCustomerNo=<%= Request["shippingCustomerNo"] %>";
	}

	function modify()
	{
		document.location.href="shippingCustomers_modify.aspx?shippingCustomerNo=<%= Request["shippingCustomerNo"] %>";
	}

	function clearPosition()
	{
					
		document.thisform.command.value = "clearPosition";
		document.thisform.submit();			
				
	}

	function addTransport()
	{
		document.location.href="shippingCustomers_addTransport.aspx?shippingCustomerNo=<%= Request["shippingCustomerNo"] %>";
	}

	function addUser()
	{
		document.location.href="shippingCustomers_users_modify.aspx?shippingCustomerNo=<%= Request["shippingCustomerNo"] %>";
	}

	function addSchedule()
	{
		document.location.href="shippingCustomers_schedules_modify.aspx?shippingCustomerNo=<%= Request["shippingCustomerNo"] %>";
	}

	</script>

	<body MS_POSITIONING="GridLayout" topmargin="10">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" name="thisform" method="post" runat="server">
			<input type="hidden" name="command" value="">
			<input type="hidden" name="positionX" value="">
			<input type="hidden" name="positionY" value="">
			<input type="hidden" name="shippingCustomerNo" value="<%= currentShippingCustomer.no %>">
			<input type="hidden" name="userId" value="">
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame" style="border-bottom: 0 px">
				<tr>
					<td colspan="2" height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td colspan="2" class="activityName">Kund <%= currentShippingCustomer.name %></td>
				</tr>
			</table>
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame" style="border-top: 0 px">
				<tr>
					<td width="50" valign="top" height="100%"><iframe id="mapFrame" width="265" height="100%" frameborder="no" scrolling="no" src="<%= mapServer.getUrl() %>" style="border: 1px #000000 solid;"></iframe></td>
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
											<td class="interaction" height="20"><b><%= currentShippingCustomer.productionSite %></b></td>
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
											<td class="interaction" height="20"><b><%= currentShippingCustomer.name %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Organisationsnr / Personnr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentShippingCustomer.registrationNo %></b></td>
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
											<td class="interaction" height="20"><b><%= currentShippingCustomer.address %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Uppföljningskod&nbsp;</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentShippingCustomer.reasonCode %></b></td>
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
											<td class="interaction" height="20"><b><%= currentShippingCustomer.address2 %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Telefonnr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentShippingCustomer.phoneNo %></b></td>
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
											<td class="interaction" height="20"><b><%= currentShippingCustomer.postCode + " " + currentShippingCustomer.city %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Mobiltelefonnr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentShippingCustomer.cellPhoneNo %></b></td>
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
											<td class="interaction" height="20"><b><%= currentShippingCustomer.contactName %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Faxnr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentShippingCustomer.faxNo %></b></td>
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
											<td class="interaction" height="20"><b><a href="mailto:<%= currentShippingCustomer.email %>"><%= currentShippingCustomer.email %></a></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Mottagaranläggning</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentShippingCustomer.getPreferedFactoryName(database) %></b></td>
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
									<td class="interaction" height="20"><b><%= currentShippingCustomer.directionComment+currentShippingCustomer.directionComment2 %></b></td>
								</tr>
								</table>
							</td>
						</tr>											
						</table>
						<br>Användarkonton
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
						<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Användar-ID</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Namn</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Lösenord</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">
									Ta bort</th>					
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">
									Ändra</th>					
						</tr>
						<%
							int k=0;
							while(k < shippingCustomerUserDataSet.Tables[0].Rows.Count)
							{
							
									string bgStyle = "";
									
									if ((k % 2) > 0)
									{
										bgStyle = " style=\"background-color: #e0e0e0;\"";
									}
									
							
								%><tr>
									<td class="jobDescription" <%= bgStyle %>><%= shippingCustomerUserDataSet.Tables[0].Rows[k].ItemArray.GetValue(0).ToString() %></td>
									<td class="jobDescription" <%= bgStyle %>><%= shippingCustomerUserDataSet.Tables[0].Rows[k].ItemArray.GetValue(3).ToString() %></td>
									<td class="jobDescription" <%= bgStyle %>><%= shippingCustomerUserDataSet.Tables[0].Rows[k].ItemArray.GetValue(2).ToString() %></td>
									<td class="jobDescription" <%= bgStyle %> align="center"><a href="javascript:deleteUser('<%= shippingCustomerUserDataSet.Tables[0].Rows[k].ItemArray.GetValue(0).ToString() %>')"><img src="images/button_delete.gif" border="0" width="12" height="13"></a></td>
									<td class="jobDescription" <%= bgStyle %> align="center"><a href="shippingCustomers_users_modify.aspx?shippingCustomerNo=<%= currentShippingCustomer.no %>&userId=<%= shippingCustomerUserDataSet.Tables[0].Rows[k].ItemArray.GetValue(0).ToString() %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
								</tr><%
								
								k++;
							}
							
							if (k == 0)
							{
								%><tr>
									<td colspan="5" class="jobDescription">Inga användare registrerade.</td>
								</tr><%
								
							}
						%>
						</table>
						<br>Containers
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Containernr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Typ</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right" valign="bottom">
									Volym</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right" valign="bottom">
									Vikt</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center" valign="bottom">
									Visa</th>
							</tr>
							<%
								int i = 0;
								while (i < containerDataSet.Tables[0].Rows.Count)
								{		
									
									
									string bgStyle = "";
									
									if ((i % 2) > 0)
									{
										bgStyle = " style=\"background-color: #e0e0e0;\"";
									}
									
																					
									%>
									<tr>
										<td class="jobDescription" <%= bgStyle %> valign="top"><%= containerDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top"><%= containerDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString() %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top" align="right"><%= float.Parse(containerDataSet.Tables[0].Rows[i].ItemArray.GetValue(3).ToString()).ToString("0") %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top" align="right"><%= float.Parse(containerDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString()).ToString("0") %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top" align="center"><a href="containers_view.aspx?containerNo=<%= containerDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
									</tr>
									<%
									
									
									i++;
								}
							
							
							%>
						</table>												
						<br>Anslutna transportörer för automatplanering<br>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
						<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Ordertyp</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Typ</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Transportör / Bil</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Ordning</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">
									Ta bort</th>					
						</tr>
						<%
						
							int j=0;
							while(j < shippingCustomerOrganizationDataSet.Tables[0].Rows.Count)
							{
								Navipro.SantaMonica.Common.ShippingCustomerOrganization shippingCustomerOrganization = new Navipro.SantaMonica.Common.ShippingCustomerOrganization(shippingCustomerOrganizationDataSet.Tables[0].Rows[j]);
								
								string description = "";
								if (shippingCustomerOrganization.type == 0)
								{
									Navipro.SantaMonica.Common.Organization organization = shippingCustomerOrganization.getOrganization(database);
									if (organization != null) description = organization.name;
								}
								if (shippingCustomerOrganization.type == 1)
								{
									Navipro.SantaMonica.Common.Agent agent = shippingCustomerOrganization.getAgent(database);
									if (agent != null) description = agent.code+", "+agent.description;
								}
								
								string bgStyle = "";
								
								if ((j % 2) > 0)
								{
									bgStyle = " style=\"background-color: #e0e0e0;\"";
								}
									
								
								
								%>
								<tr>
									<td class="jobDescription" <%= bgStyle %>><%= shippingCustomerOrganization.getOrderType() %></td>
									<td class="jobDescription" <%= bgStyle %>><%= shippingCustomerOrganization.getType() %></td>
									<td class="jobDescription" <%= bgStyle %>><%= description %></td>
									<td class="jobDescription" <%= bgStyle %>><%= shippingCustomerOrganization.sortOrder %></td>
									<td class="jobDescription" <%= bgStyle %> align="center"><a href="shippingCustomers_view.aspx?command=deleteTransport&shippingCustomerNo=<%= currentShippingCustomer.no %>&type=<%= shippingCustomerOrganization.type %>&code=<%= shippingCustomerOrganization.code %>&orderType=<%= shippingCustomerOrganization.orderType %>"><img src="images/button_delete.gif" border="0" width="12" height="13"></a></td>
								</tr>
								<%
								
								j++;
							}
							
							if (j == 0)
							{
								%><tr>
									<td colspan="4" class="jobDescription">Inga transportörer anslutna.</td>
								</tr><%
								
							}
						%>
						</table>
						<br>Scheman<br>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Typ</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">
									Måndag</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">
									Tisdag</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">
									Onsdag</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">
									Torsdag</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">
									Fredag</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">
									Lördag</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">
									Söndag</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Vecka</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Tid</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right">
									Antal</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">
									Ta bort</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">
									Ändra</th>
							</tr>
							<%
								i = 0;
								while (i < shippingCustomerScheduleDataSet.Tables[0].Rows.Count)
								{
									Navipro.SantaMonica.Common.ShippingCustomerSchedule shippingCustomerSchedule = new Navipro.SantaMonica.Common.ShippingCustomerSchedule(shippingCustomerScheduleDataSet.Tables[0].Rows[i]);
								
									string bgStyle = "";
									
									if ((i % 2) > 0)
									{
										bgStyle = " style=\"background-color: #e0e0e0;\"";
									}
									
									string monday = "";
									string tuesday = "";
									string wednesday = "";
									string thursday = "";
									string friday = "";
									string saturday = "";
									string sunday = "";
									
									if (shippingCustomerSchedule.mondays) monday = "<img src=\"images/checked.gif\" border=\"0\">";
									if (shippingCustomerSchedule.tuesdays) tuesday = "<img src=\"images/checked.gif\" border=\"0\">";
									if (shippingCustomerSchedule.wednesdays) wednesday = "<img src=\"images/checked.gif\" border=\"0\">";
									if (shippingCustomerSchedule.thursdays) thursday = "<img src=\"images/checked.gif\" border=\"0\">";
									if (shippingCustomerSchedule.fridays) friday = "<img src=\"images/checked.gif\" border=\"0\">";
									if (shippingCustomerSchedule.saturdays) saturday = "<img src=\"images/checked.gif\" border=\"0\">";
									if (shippingCustomerSchedule.sundays) sunday = "<img src=\"images/checked.gif\" border=\"0\">";
								
									%>
									<tr>
										<td class="jobDescription" valign="top" <%= bgStyle %>><%= shippingCustomerSchedule.getType() %></td>
										<td class="jobDescription" valign="top" <%= bgStyle %> align="center"><%= monday %></td>
										<td class="jobDescription" valign="top" <%= bgStyle %> align="center"><%= tuesday %></td>
										<td class="jobDescription" valign="top" <%= bgStyle %> align="center"><%= wednesday %></td>
										<td class="jobDescription" valign="top" <%= bgStyle %> align="center"><%= thursday %></td>
										<td class="jobDescription" valign="top" <%= bgStyle %> align="center"><%= friday %></td>
										<td class="jobDescription" valign="top" <%= bgStyle %> align="center"><%= saturday %></td>
										<td class="jobDescription" valign="top" <%= bgStyle %> align="center"><%= sunday %></td>
										<td class="jobDescription" valign="top" <%= bgStyle %>><%= shippingCustomerSchedule.getWeek() %></td>
										<td class="jobDescription" valign="top" <%= bgStyle %>><%= shippingCustomerSchedule.time.ToString("HH:mm") %></td>
										<td class="jobDescription" valign="top" <%= bgStyle %> align="right"><%= shippingCustomerSchedule.quantity.ToString() %> ton</td>
										<td class="jobDescription" valign="top" align="center" <%= bgStyle %>><a href="shippingCustomers_view.aspx?shippingCustomerNo=<%= currentShippingCustomer.no %>&scheduleEntryNo=<%= shippingCustomerSchedule.entryNo %>&command=deleteSchedule"><img src="images/button_delete.gif" border="0" alt="Ändra" width="12" height="13"></a></td>
										<td class="jobDescription" valign="top" align="center" <%= bgStyle %>><a href="shippingCustomers_schedules_modify.aspx?shippingCustomerNo=<%= currentShippingCustomer.no %>&scheduleEntryNo=<%= shippingCustomerSchedule.entryNo %>"><img src="images/button_assist.gif" border="0" alt="Ändra" width="12" height="13"></a></td>
									</tr>
									<%
								
								
									i++;

								}
							
								if (i == 0)
								{
									%>
									<tr>
										<td class="jobDescription" colspan="13">Inga scheman definierade.</td>
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
									<td class="interaction" height="20"><input type="button" value="Visa linjeorder" onclick="showLineOrders()" class="Button">&nbsp;<input type="button" value="Ändra kundinformation" onclick="modify()" class="Button">&nbsp;<input type="button" value="Lägg till transportör" onclick="addTransport()" class="Button">&nbsp;<input type="button" value="Lägg till användare" onclick="addUser()" class="Button">&nbsp;<input type="button" value="Lägg till schema" onclick="addSchedule()" class="Button">&nbsp;<input type="button" value="Radera position" onclick="clearPosition()" class="Button"></td>
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