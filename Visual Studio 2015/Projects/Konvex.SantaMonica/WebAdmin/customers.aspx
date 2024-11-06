<%@ Page language="c#" Codebehind="customers.aspx.cs" AutoEventWireup="false" Inherits="Navipro.SantaMonica.WebAdmin.Customers" %>
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

	function createOrder(customerNo, customerShipAddressNo, organizationNo)
	{
		document.thisform.command.value = "createOrder";
		document.thisform.customerNo.value = customerNo;
		document.thisform.orgNo.value = organizationNo;
		document.thisform.customerShipAddressNo.value = customerShipAddressNo;
		document.thisform.submit();
	
	}

	function createOrderQuestion(customerNo, customerShipAddressNo, organizationNo)
	{
		if (confirm("Det finns redan körorder för vald kund. Vill du se en lista på dessa?"))
		{
			document.location.href = "customers_orders.aspx?customerNo="+customerNo+"&organizationNo="+organizationNo+"&ongoing=1";
		}
		else
		{
			createOrder(customerNo, customerShipAddressNo, organizationNo);
		}	
	}
	
	</script>
	
	<body MS_POSITIONING="GridLayout" topmargin="10">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" method="post" runat="server">
			<input type="hidden" name="command" value="">
			<input type="hidden" name="customerNo" value="">
			<input type="hidden" name="customerShipAddressNo" value="">			
			<input type="hidden" name="orgNo" value="">			
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame">
				<tr>
					<td height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td class="activityName">Kunder</td>
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
							<td class="interaction" valign="top">
								<table cellspacing="0" cellpadding="0" width="100%" border="0">
								<tr>
									<td class="activityAuthor" height="15" valign="top">Namn</td>
								</tr>
								<tr>
									<td class="interaction" height="20"><input type="text" size="30" name="searchName" class="Textfield" value="<%= Request["searchName"] %>"></td>
								</tr>
								</table>
							</td>							
							<td class="interaction" valign="top">
								<table cellspacing="0" cellpadding="0" width="100%" border="0">
								<tr>
									<td class="activityAuthor" height="15" valign="top">Ort</td>
								</tr>
								<tr>
									<td class="interaction" height="20"><input type="text" size="20" name="searchCity" class="Textfield" value="<%= Request["searchCity"] %>"></td>
								</tr>
								</table>
							</td>
							<td class="interaction" valign="top">
								<table cellspacing="0" cellpadding="0" width="100%" border="0">
								<tr>
									<td class="activityAuthor" height="15" valign="top">Produktionsplatsnr</td>
								</tr>
								<tr>
									<td class="interaction" height="20"><input type="text" size="20" name="searchProdSite" class="Textfield" value="<%= Request["searchProdSite"] %>"></td>
								</tr>
								</table>
							</td>
							<td class="interaction" valign="top">
								<table cellspacing="0" cellpadding="0" width="100%" border="0">
								<tr>
									<td class="activityAuthor" height="15" valign="top">Telefonnr / Mobiltel.</td>
								</tr>
								<tr>
									<td class="interaction" height="20"><input type="text" size="20" name="searchPhoneNo" class="Textfield" value="<%= Request["searchPhoneNo"] %>"></td>
								</tr>
								</table>
							</td>
							<td class="interaction" valign="top">
								<table cellspacing="0" cellpadding="0" width="100%" border="0">
								<tr>
									<td class="activityAuthor" height="15" valign="top">Betalning</td>
								</tr>
								<tr>
									<td class="interaction" height="20"><select name="searchPaymentType" class="Textfield"><option value=""></option><option value="1" <% if (Request["searchPaymentType"] == "1") Response.Write("selected"); %>>Endast kontant</option></select></td>
								</tr>
								</table>
							</td>
							<%
								if (currentOrganization.callCenterMaster)
								{
									%>
									<td class="interaction" valign="top">
										<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Transportör</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><select name="organizationNo" class="Textfield">
											<option value="">Alla</option>
											
											<%
												int i = 0;
												while (i < organizationDataSet.Tables[0].Rows.Count)
												{
													%><option value="<%= organizationDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>" <% if (Request["organizationNo"] == organizationDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()) Response.Write("selected"); %>><%= organizationDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %></option><%
													
													i++;
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
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Kundnr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Namn</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Adress</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Ort</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Telefonnr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Mobiltelefonnr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Prod. nr.</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center" valign="bottom">
									Kontant betalning</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center" valign="bottom">
									Skapa körorder</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center" valign="bottom">
									Visa</th>
							</tr>
							<%
								string lastCustomerNo = "";
								string firstOrganizationNo = "";
								
								int i = 0;
								int count = 0;
								string bgStyle = "";

								Navipro.SantaMonica.Common.ShipOrders shipOrders = new Navipro.SantaMonica.Common.ShipOrders();
								
								System.Collections.Hashtable customerOrgList = new System.Collections.Hashtable();
								while (i < customerDataSet.Tables[0].Rows.Count)
								{
									System.Collections.ArrayList orgList = (System.Collections.ArrayList)customerOrgList[customerDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()];
									
									if (orgList == null)
									{
										orgList = new System.Collections.ArrayList();
										customerOrgList.Add(customerDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString(), orgList);
									}
									
									orgList.Add(customerDataSet.Tables[0].Rows[i].ItemArray.GetValue(14).ToString());
									customerOrgList[customerDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()] = orgList;
											
									i++;
								}								
								
								i = 0;
								
								while (i < customerDataSet.Tables[0].Rows.Count)
								{		
									string redLine = "";
									bool blocked = false;
									string cashPayment = "";
									
									if (customerDataSet.Tables[0].Rows[i].ItemArray.GetValue(12).ToString() != "0")
									{
										blocked = true;
										redLine = "style='color: red;'";
									}

									if (customerDataSet.Tables[0].Rows[i].ItemArray.GetValue(13).ToString() != "0")
									{
										cashPayment = "<img src=\"images/checked.gif\" border=\"0\">";	
									}
									
	
								
									if (lastCustomerNo != customerDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString())
									{												
											
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
											<td class="jobDescription" <%= redLine %> <%= bgStyle %> valign="top" style="font-weight: bold;" align="center">&nbsp;<%= cashPayment %>&nbsp;</td>
											
											<%
											if (shipOrders.checkCustomerShipOrderExists(database, customerDataSet.Tables[0].Rows[i].ItemArray.GetValue(14).ToString(), customerDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString(), ""))
											{
												%><td class="jobDescription" <%= redLine %> <%= bgStyle %> valign="top" align="left"><% if (!blocked) { 
												
													System.Collections.ArrayList orgList = (System.Collections.ArrayList)customerOrgList[customerDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()];
													int x = 0;
													while (x < orgList.Count)
													{
														%>&nbsp;&nbsp;&nbsp;<a href="javascript:createOrderQuestion('<%= customerDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>','','<%= orgList[x].ToString() %>')"><img src="images/button_assist.gif" border="0" style="vertical-align: middle;margin-top: 2px;" width="12" height="13"></a>&nbsp;<%= orgList[x].ToString() %><br/><%
													
														x++;
													}
													%>	
													</td><% } %><%
											}
											else
											{
												%><td class="jobDescription" <%= redLine %> <%= bgStyle %> valign="top" align="left"><% if (!blocked) { 
												
													System.Collections.ArrayList orgList = (System.Collections.ArrayList)customerOrgList[customerDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()];
													int x = 0;
													while (x < orgList.Count)
													{
														%>&nbsp;&nbsp;&nbsp;<a href="javascript:createOrder('<%= customerDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>','','<%= orgList[x].ToString() %>')"><img src="images/button_assist.gif" border="0" style="vertical-align: middle; margin-top: 2px;" width="12" height="13"></a>&nbsp;<%= orgList[x].ToString() %><br/><%
														
														x++;
													}
													%>
													</td><% } %><%
											}
																					
											
											firstOrganizationNo = customerDataSet.Tables[0].Rows[i].ItemArray.GetValue(14).ToString();
									
										%>
											<td class="jobDescription" <%= redLine %> <%= bgStyle %> valign="top" align="center"><a href="customers_view.aspx?customerNo=<%= customerDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>&organizationNo=<%= customerDataSet.Tables[0].Rows[i].ItemArray.GetValue(14).ToString() %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
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
													<td class="jobDescription" <%= redLine %> <%= bgStyle %> valign="top"><%= customerShipAddressDataSet.Tables[0].Rows[j].ItemArray.GetValue(14).ToString() %></td>
													<td class="jobDescription" <%= redLine %> <%= bgStyle %> valign="top">&nbsp;</td>
													<td class="jobDescription" <%= redLine %> <%= bgStyle %> valign="top"><%= customerShipAddressDataSet.Tables[0].Rows[j].ItemArray.GetValue(15).ToString() %></td>
													<td class="jobDescription" <%= redLine %> <%= bgStyle %> valign="top">&nbsp;</td>
													<%
														if (shipOrders.checkCustomerShipOrderExists(database, customerDataSet.Tables[0].Rows[i].ItemArray.GetValue(14).ToString(), customerDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString(), customerShipAddressDataSet.Tables[0].Rows[j].ItemArray.GetValue(2).ToString()))
														{
															%>
															<td class="jobDescription" <%= redLine %> <%= bgStyle %> valign="top" align="left"><% if (!blocked) { 
															
																System.Collections.ArrayList orgList = (System.Collections.ArrayList)customerOrgList[customerDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()];
																int x = 0;
																while (x < orgList.Count)
																{
															
																	%>&nbsp;&nbsp;&nbsp;<a href="javascript:createOrderQuestion('<%= customerDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>','<%= customerShipAddressDataSet.Tables[0].Rows[j].ItemArray.GetValue(2).ToString() %>','<%= orgList[x].ToString() %>')"><img src="images/button_assist.gif" border="0" width="12" height="13"></a>&nbsp;<%= orgList[x].ToString() %><br/><%
																	
																	x++;
																}
																
															} %></td>
															<%
														}
														else
														{
															%>
															<td class="jobDescription" <%= redLine %> <%= bgStyle %> valign="top" align="left"><% if (!blocked) { 
															
																System.Collections.ArrayList orgList = (System.Collections.ArrayList)customerOrgList[customerDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()];
																int x = 0;
																while (x < orgList.Count)
																{
																	%>&nbsp;&nbsp;&nbsp;<a href="javascript:createOrder('<%= customerDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>','<%= customerShipAddressDataSet.Tables[0].Rows[j].ItemArray.GetValue(2).ToString() %>','<%= orgList[x].ToString() %>')"><img src="images/button_assist.gif" border="0" width="12" height="13"></a>&nbsp;<%= orgList[x].ToString() %><br/><%
																	
																	x++;
																	
																}
																
															 } %></td>
																	
															<%
														}
													%>													
													<td class="jobDescription" <%= redLine %> <%= bgStyle %> valign="top" align="center"><a href="customers_view.aspx?customerNo=<%= customerDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>&organizationNo=<%= customerDataSet.Tables[0].Rows[i].ItemArray.GetValue(14).ToString() %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
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
