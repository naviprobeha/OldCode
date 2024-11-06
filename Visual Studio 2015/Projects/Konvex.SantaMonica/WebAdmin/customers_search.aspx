<%@ Page language="c#" Codebehind="customers_search.aspx.cs" AutoEventWireup="false" Inherits="Navipro.SantaMonica.WebAdmin.customers_search" %>
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
	
	function search()
	{
		document.thisform.command.value = "searchCustomer";
		document.thisform.submit();
	}

	function setCustomer(customerNo, customerShipAddressNo, organizationNo)
	{
		document.thisform.command.value = "setCustomer";
		document.thisform.customerNo.value = customerNo;
		document.thisform.mode.value = "<%= Request["mode"] %>";
		document.thisform.customerShipAddressNo.value = customerShipAddressNo;
		document.thisform.organizationNo.value = organizationNo;
		document.thisform.action = "customers_search.aspx";
		document.thisform.submit();
	
	}
	
	</script>
	
	<body MS_POSITIONING="GridLayout" topmargin="10">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" method="post" runat="server">
			<input type="hidden" name="command" value="">
			<input type="hidden" name="shipOrderNo" value="<%= Request["shipOrderNo"] %>">
			<input type="hidden" name="organizationNo" value="">
			<input type="hidden" name="customerNo" value="">
			<input type="hidden" name="mode" value="<%= Request["mode"] %>">
			<input type="hidden" name="customerShipAddressNo" value="">
			<input type="hidden" name="origin" value="<%= Request["origin"] %>">
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame">
				<tr>
					<td height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td class="activityName">Sök <% if (Request["mode"] == "2") Response.Write("fakturerings"); %>kund för order <%= currentOrganization.no+Request["shipOrderNo"] %></td>
				</tr>
				<tr>
					<td>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
						<tr>
							<td class="interaction" valign="top">
								<table cellspacing="0" cellpadding="0" width="100%" border="0">
								<tr>
									<td class="activityAuthor" height="15" valign="top">Kundnr</td>
								</tr>
								<tr>
									<td class="interaction" height="20"><input type="text" size="10" name="searchCustomerNo" class="Textfield" value="<%= Request["searchCustomerNo"] %>"></td>
								</tr>
								</table>
							</td>
							<td class="interaction" valign="top" colspan="2">
								<table cellspacing="0" cellpadding="0" width="100%" border="0">
								<tr>
									<td class="activityAuthor" height="15" valign="top">Namn</td>
								</tr>
								<tr>
									<td class="interaction" height="20"><input type="text" size="30" name="searchName" class="Textfield" value="<%= Request["searchName"] %>"></td>
								</tr>
								</table>
							</td>							
							<td class="interaction" valign="top" colspan="2">
								<table cellspacing="0" cellpadding="0" width="100%" border="0">
								<tr>
									<td class="activityAuthor" height="15" valign="top">Ort</td>
								</tr>
								<tr>
									<td class="interaction" height="20"><input type="text" size="20" name="searchCity" class="Textfield" value="<%= Request["searchCity"] %>"></td>
								</tr>
								</table>
							</td>
							<td class="interaction" valign="top" colspan="2">
								<table cellspacing="0" cellpadding="0" width="100%" border="0">
								<tr>
									<td class="activityAuthor" height="15" valign="top">Organizationsnr / Personnr</td>
								</tr>
								<tr>
									<td class="interaction" height="20"><input type="text" size="20" name="searchRegNo" class="Textfield" value="<%= Request["searchRegNo"] %>"></td>
								</tr>
								</table>
							</td>
							<td class="interaction" valign="top" colspan="2">
								<table cellspacing="0" cellpadding="0" width="100%" border="0">
								<tr>
									<td class="activityAuthor" height="15" valign="top">Produktionsplatsnr</td>
								</tr>
								<tr>
									<td class="interaction" height="20"><input type="text" size="20" name="searchProdSite" class="Textfield" value="<%= Request["searchProdSite"] %>"></td>
								</tr>
								</table>
							</td>
							<td class="interaction" valign="top" colspan="2">
								<table cellspacing="0" cellpadding="0" width="100%" border="0">
								<tr>
									<td class="activityAuthor" height="15" valign="top">Telefonnr / Mobiltel.</td>
								</tr>
								<tr>
									<td class="interaction" height="20"><input type="text" size="20" name="searchPhoneNo" class="Textfield" value="<%= Request["searchPhoneNo"] %>"></td>
								</tr>
								</table>
							</td>
							<td class="interaction" valign="top" colspan="2">
								<table cellspacing="0" cellpadding="0" width="100%" border="0">
								<tr>
									<td class="activityAuthor" height="15" valign="top">Betalning</td>
								</tr>
								<tr>
									<td class="interaction" height="20"><select name="searchPaymentType" class="Textfield"><option value=""></option><option value="1" <% if (Request["searchPaymentType"] == "1") Response.Write("selected"); %>>Endast kontant</option></select></td>
								</tr>
								</table>
							</td>
							<td class="interaction" valign="top" colspan="2">
								<table cellspacing="0" cellpadding="0" width="100%" border="0">
								<tr>
									<td class="activityAuthor" height="15" valign="top">&nbsp;</td>
								</tr>
								<tr>
									<td class="interaction" height="20"><input type="submit" value="Sök" class="Button" onclick="search()"></td>
								</tr>
								</table>
							</td>
							
						</tr>											
						</table>
						<br>	
						<% if (Request["command"] == "searchCustomer") { %>				
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Kundnr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Namn</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Adress</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Ort</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Telefonnr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Mobiltelefonnr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Faxnr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">
									Välj</th>
							</tr>
							<%
								string lastCustomerNo = "";
								string firstOrganizationNo = "";
								
								int count = 0;
								int i = 0;
								while (i < customerDataSet.Tables[0].Rows.Count)
								{		
									string redLine = "";
									string bgStyle = "";
									bool blocked = false;
									if (customerDataSet.Tables[0].Rows[i].ItemArray.GetValue(12).ToString() != "0")
									{
										blocked = true;
										redLine = "style='color: red;'";
									}
													
									if (lastCustomerNo != customerDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString())
									{												
										if ((currentOrganization.callCenterMaster) && (lastCustomerNo != ""))
										{
											%></td>
												</tr>
											<%
										}
											
										bgStyle = "";						
										if ((count % 2) > 0)
										{
											bgStyle = " style=\"background-color: #e0e0e0;\"";
										}
										count++;
														
										%>
										<tr>
											<td class="jobDescription" <%= redLine %> <%= bgStyle %> valign="top" style="font-weight: bold;"><%= customerDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %></td>
											<td class="jobDescription" <%= redLine %> <%= bgStyle %> valign="top" style="font-weight: bold;"><%= customerDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %></td>
											<td class="jobDescription" <%= redLine %> <%= bgStyle %> valign="top" style="font-weight: bold;"><%= customerDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString() %></td>
											<td class="jobDescription" <%= redLine %> <%= bgStyle %> valign="top" style="font-weight: bold;"><%= customerDataSet.Tables[0].Rows[i].ItemArray.GetValue(5).ToString() %></td>
											<td class="jobDescription" <%= redLine %> <%= bgStyle %> valign="top" style="font-weight: bold;"><%= customerDataSet.Tables[0].Rows[i].ItemArray.GetValue(7).ToString() %></td>
											<td class="jobDescription" <%= redLine %> <%= bgStyle %> valign="top" style="font-weight: bold;"><%= customerDataSet.Tables[0].Rows[i].ItemArray.GetValue(8).ToString() %></td>
											<td class="jobDescription" <%= redLine %> <%= bgStyle %> valign="top" style="font-weight: bold;"><%= customerDataSet.Tables[0].Rows[i].ItemArray.GetValue(15).ToString() %></td>
											<td class="jobDescription" <%= redLine %> <%= bgStyle %> valign="top" align="left"><% if (!blocked) { %>&nbsp;&nbsp;&nbsp;<a href="javascript:setCustomer('<%= customerDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>','','<%= customerDataSet.Tables[0].Rows[i].ItemArray.GetValue(14).ToString() %>')"><img src="images/button_assist.gif" border="0" style="vertical-align: middle;" width="12" height="13"></a> <%= customerDataSet.Tables[0].Rows[i].ItemArray.GetValue(14).ToString() %><% } %><%
											
											firstOrganizationNo = customerDataSet.Tables[0].Rows[i].ItemArray.GetValue(14).ToString();
									}
									else
									{
										if (!blocked)
										{
											%><br>&nbsp;&nbsp;&nbsp;<a href="javascript:setCustomer('<%= customerDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>','','<%= customerDataSet.Tables[0].Rows[i].ItemArray.GetValue(14).ToString() %>')"><img src="images/button_assist.gif" border="0" style="vertical-align: middle;" width="12" height="13"></a> <%= customerDataSet.Tables[0].Rows[i].ItemArray.GetValue(14).ToString() %><%
										}
									}
									
									if (!currentOrganization.callCenterMaster)
									{
										%></td>
											</tr>
										<%
									
										Navipro.SantaMonica.Common.CustomerShipAddresses customerShipAddresses = new Navipro.SantaMonica.Common.CustomerShipAddresses(); 
										System.Data.DataSet customerShipAddressDataSet = customerShipAddresses.getDataSet(database, currentOrganization.no, customerDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString());
										
										int j = 0;
										while (j < customerShipAddressDataSet.Tables[0].Rows.Count)
										{		
															
											%>
											<tr>
												<td class="jobDescription" <%= redLine %> <%= bgStyle %> valign="top">&nbsp;</td>
												<td class="jobDescription" <%= redLine %> <%= bgStyle %> valign="top"><%= customerShipAddressDataSet.Tables[0].Rows[j].ItemArray.GetValue(3).ToString() %></td>
												<td class="jobDescription" <%= redLine %> <%= bgStyle %> valign="top"><%= customerShipAddressDataSet.Tables[0].Rows[j].ItemArray.GetValue(4).ToString() %></td>
												<td class="jobDescription" <%= redLine %> <%= bgStyle %> valign="top"><%= customerShipAddressDataSet.Tables[0].Rows[j].ItemArray.GetValue(7).ToString() %></td>
												<td class="jobDescription" <%= redLine %> <%= bgStyle %> valign="top">&nbsp;</td>
												<td class="jobDescription" <%= redLine %> <%= bgStyle %> valign="top">&nbsp;</td>
												<td class="jobDescription" <%= redLine %> <%= bgStyle %> valign="top"><%= customerShipAddressDataSet.Tables[0].Rows[j].ItemArray.GetValue(15).ToString() %></td>
												<td class="jobDescription" <%= redLine %> <%= bgStyle %> valign="top" align="left">&nbsp;&nbsp;&nbsp;<% if (!blocked) { %><a href="javascript:setCustomer('<%= customerDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>','<%= customerShipAddressDataSet.Tables[0].Rows[j].ItemArray.GetValue(2).ToString() %>','<%= customerDataSet.Tables[0].Rows[i].ItemArray.GetValue(14).ToString() %>')"><img src="images/button_assist.gif" border="0" width="12" height="13"></a> <%= customerDataSet.Tables[0].Rows[i].ItemArray.GetValue(14).ToString() %><% } %></td>
											</tr>
											<%
											
																				
											j++;
										}
									}		
									
									lastCustomerNo = customerDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();

									
									i++;
								}
							
							
							%>
						</table>
						<% } %>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
