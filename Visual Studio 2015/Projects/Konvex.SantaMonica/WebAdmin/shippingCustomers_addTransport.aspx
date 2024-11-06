<%@ Page language="c#" Codebehind="shippingCustomers_addTransport.aspx.cs" AutoEventWireup="false" Inherits="WebAdmin.shippingCustomers_addTransport" %>
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
	

	function validate()
	{
		document.thisform.command.value = "saveTransport";
		document.thisform.action = "shippingCustomers_addTransport.aspx";
		document.thisform.submit();
	}
	
	</script>

	<body MS_POSITIONING="GridLayout" topmargin="10">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" name="thisform" method="post" runat="server">
			<input type="hidden" name="command" value="">
			<input type="hidden" name="shippingCustomerNo" value="<%= currentShippingCustomer.no %>">
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame" style="border-bottom: 0 px">
				<tr>
					<td colspan="2" height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td colspan="2" class="activityName">Lägg till transportör för automatplanering</td>
				</tr>
			</table>
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame" style="border-top: 0 px">
				<tr>
					<td valign="top">
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<td class="interaction" valign="top" width="100">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Kundnr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><%= currentShippingCustomer.no %></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Namn</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><%= currentShippingCustomer.name %></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<br>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<td class="interaction" valign="top" width="100">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Ordertyp</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><select name="orderType" class="Textfield"><option value="0" <% if (Request["orderType"] == "0") Response.Write("selected"); %>>Linjeorder</option><option value="1" <% if (Request["orderType"] == "1") Response.Write("selected"); %>>Fabriksorder</option></select></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" width="100">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Typ</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><select name="type" class="Textfield" onchange="document.thisform.submit()"><option value="0" <% if (selectedType == 0) Response.Write("selected"); %>>Transportör</option><option value="1" <% if (selectedType == 1) Response.Write("selected"); %>>Bil</option></select></td>
										</tr>
									</table>
								</td>
								<%
									if (selectedType == 0)
									{
										%>
										<td class="interaction" valign="top">
											<table cellspacing="0" cellpadding="0" width="100%" border="0">
												<tr>
													<td class="activityAuthor" height="15" valign="top">Transportör</td>
												</tr>
												<tr>
													<td class="interaction" height="20"><select name="code" class="Textfield">
													<%
														int organizationI = 0;
														while (organizationI < organizationDataSet.Tables[0].Rows.Count)
														{
															%><option value="<%= organizationDataSet.Tables[0].Rows[organizationI].ItemArray.GetValue(0).ToString() %>"><%= organizationDataSet.Tables[0].Rows[organizationI].ItemArray.GetValue(1).ToString() %></option><%
			
															organizationI++;													
														}
													%>													
													</select></td>
												</tr>
											</table>
										</td>
										<%
									}
									else
									{
										%>
										<td class="interaction" valign="top">
											<table cellspacing="0" cellpadding="0" width="100%" border="0">
												<tr>
													<td class="activityAuthor" height="15" valign="top">Bil</td>
												</tr>
												<tr>
													<td class="interaction" height="20"><select name="code" class="Textfield">
													<%
														int agentI = 0;
														while (agentI < agentDataSet.Tables[0].Rows.Count)
														{
															%><option value="<%= agentDataSet.Tables[0].Rows[agentI].ItemArray.GetValue(0).ToString() %>"><%= agentDataSet.Tables[0].Rows[agentI].ItemArray.GetValue(0).ToString() %>, <%= agentDataSet.Tables[0].Rows[agentI].ItemArray.GetValue(1).ToString() %></option><%
			
															agentI++;													
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
											<td class="activityAuthor" height="15" valign="top">Ordning</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><select name="sortOrder" class="Textfield">
												<option value="1" <% if (Request["sortOrder"] == "1") Response.Write("selected"); %>>1</option>
												<option value="2" <% if (Request["sortOrder"] == "2") Response.Write("selected"); %>>2</option>
												<option value="3" <% if (Request["sortOrder"] == "3") Response.Write("selected"); %>>3</option>
												<option value="4" <% if (Request["sortOrder"] == "4") Response.Write("selected"); %>>4</option>
												<option value="5" <% if (Request["sortOrder"] == "5") Response.Write("selected"); %>>5</option>
												<option value="6" <% if (Request["sortOrder"] == "6") Response.Write("selected"); %>>6</option>
												<option value="7" <% if (Request["sortOrder"] == "7") Response.Write("selected"); %>>7</option>
												<option value="8" <% if (Request["sortOrder"] == "8") Response.Write("selected"); %>>8</option>
												<option value="9" <% if (Request["sortOrder"] == "9") Response.Write("selected"); %>>9</option>
											</select></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<br>
						<table cellspacing="1" cellpadding="2" width="100%">
							<tr>
								<td align="right"><input type="button" class="Button" value="Spara" onclick="validate()"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>

		
	</body>
</HTML>