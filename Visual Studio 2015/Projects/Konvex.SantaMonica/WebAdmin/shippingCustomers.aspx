<%@ Page language="c#" Codebehind="shippingCustomers.aspx.cs" AutoEventWireup="false" Inherits="Navipro.SantaMonica.WebAdmin.ShippingCustomers" %>
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
		document.thisform.command.value = "searchShippingCustomer";
		document.thisform.submit();
	}

	function createOrder(shippingCustomerNo)
	{
		document.thisform.command.value = "createOrder";
		document.thisform.shippingCustomerNo.value = shippingCustomerNo;
		document.thisform.submit();
	
	}
	
	</script>
	
	<body MS_POSITIONING="GridLayout" topmargin="10">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" method="post" runat="server">
			<input type="hidden" name="command" value="">
			<input type="hidden" name="shippingCustomerNo" value="">
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame">
				<tr>
					<td height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td class="activityName">Kunder Konvex</td>
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
									<td class="interaction" height="20"><input type="text" size="10" name="searchShippingCustomerNo" class="Textfield" value="<%= Request["searchShippingCustomerNo"] %>"></td>
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
									<td class="activityAuthor" height="15" valign="top">Organizationsnr</td>
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
									<td class="activityAuthor" height="15" valign="top">Telefonnr</td>
								</tr>
								<tr>
									<td class="interaction" height="20"><input type="text" size="20" name="searchPhoneNo" class="Textfield" value="<%= Request["searchPhoneNo"] %>"></td>
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
						<% if (Request["command"] == "searchShippingCustomer") { %>				
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
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center" valign="bottom">
									Skapa linjeorder</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center" valign="bottom">
									Visa</th>
							</tr>
							<%
								int i = 0;
								while (i < shippingCustomerDataSet.Tables[0].Rows.Count)
								{		
									string redLine = "";
									bool blocked = false;
									
									if (shippingCustomerDataSet.Tables[0].Rows[i].ItemArray.GetValue(16).ToString() != "0")
									{
										blocked = true;
										redLine = "style='color: red;'";
									}

									
									string bgStyle = "";
									
									if ((i % 2) > 0)
									{
										bgStyle = " style=\"background-color: #e0e0e0;\"";
									}
													
									%>
									<tr>
										<td class="jobDescription" <%= redLine %> <%= bgStyle %> valign="top" style="font-weight: bold;"><%= shippingCustomerDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %></td>
										<td class="jobDescription" <%= redLine %> <%= bgStyle %> valign="top" style="font-weight: bold;"><%= shippingCustomerDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %></td>
										<td class="jobDescription" <%= redLine %> <%= bgStyle %> valign="top" style="font-weight: bold;"><%= shippingCustomerDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString() %></td>
										<td class="jobDescription" <%= redLine %> <%= bgStyle %> valign="top" style="font-weight: bold;"><%= shippingCustomerDataSet.Tables[0].Rows[i].ItemArray.GetValue(5).ToString() %></td>
										<td class="jobDescription" <%= redLine %> <%= bgStyle %> valign="top" style="font-weight: bold;"><%= shippingCustomerDataSet.Tables[0].Rows[i].ItemArray.GetValue(7).ToString() %></td>
										<td class="jobDescription" <%= redLine %> <%= bgStyle %> valign="top" align="center"><% if (!blocked) { %><a href="javascript:createOrder('<%= shippingCustomerDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>','')"><img src="images/button_assist.gif" border="0" width="12" height="13"></a><% } %>&nbsp;</td>
										<td class="jobDescription" <%= redLine %> <%= bgStyle %> valign="top" align="center"><a href="shippingCustomers_view.aspx?shippingCustomerNo=<%= shippingCustomerDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
									</tr>
									<%
									
								
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
