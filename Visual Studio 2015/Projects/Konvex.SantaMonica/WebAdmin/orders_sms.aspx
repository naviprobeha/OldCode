<%@ Page language="c#" Codebehind="orders_sms.aspx.cs" AutoEventWireup="false" Inherits="Navipro.SantaMonica.WebAdmin.orders_sms" %>
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
	<body MS_POSITIONING="GridLayout" topmargin="10" onload="setTimeout('document.thisform.submit()', 30000)">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" method="post" runat="server">
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame">
				<tr>
					<td height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td class="activityName">Ej verifierade SMS</td>
				</tr>
				<tr>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Löpnr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Typ</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Telefonnr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Kundnr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Kundnamn</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Meddelande</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Mottagen datum och klockslag</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">
									Skapa order</th>
							</tr>
							<%
								int i = 0;
								while (i < smsDataSet.Tables[0].Rows.Count)
								{	
									Navipro.SantaMonica.Common.SMSMessage smsMessage = new Navipro.SantaMonica.Common.SMSMessage(smsDataSet.Tables[0].Rows[i]);
									string bgStyle = "";
									
									if ((i % 2) > 0)
									{
										bgStyle = " style=\"background-color: #e0e0e0;\"";
									}

									string phoneNo = "0"+smsMessage.phoneNo.Substring(3);
									string phoneNo1 = phoneNo.Substring(0, 4)+"-"+phoneNo.Substring(4);
									string phoneNo2 = phoneNo.Substring(0, 3)+"-"+phoneNo.Substring(3);
									
									string customerNo = "";
									string customerName = "";

									Navipro.SantaMonica.Common.Customers customers = new Navipro.SantaMonica.Common.Customers();
									Navipro.SantaMonica.Common.Customer customer = customers.findFirstCustomerByPhoneNo(database, phoneNo1);
									if (customer != null)
									{
										customerNo = customer.no;
										customerName = customer.name;
									}
									else
									{
										customer = customers.findFirstCustomerByPhoneNo(database, phoneNo2);
										if (customer != null)
										{
											customerNo = customer.no;
											customerName = customer.name;
										}
									}
														
									%>
									<tr>
										<td class="jobDescription" <%= bgStyle %> valign="top"><%= smsMessage.entryNo %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top"><%= smsMessage.type %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top"><%= smsMessage.phoneNo %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top"><%= customerNo %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top"><%= customerName %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top"><%= smsMessage.message %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top"><%= smsMessage.receivedDateTime.ToString("yyyy-MM-dd HH:mm:ss") %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top" align="center"><a href="orders_sms.aspx?command=createOrder&entryNo=<%= smsMessage.entryNo %>&customerNo=<%= customerNo %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
									</tr>
									<%
										
									i++;
								}
								if (i == 0)
								{
									%>
									<tr>
										<td class="jobDescription" valign="top" colspan="8">Inga nya inkomna ej verifierade sms.</td>
									</tr>
									<%
								
								}
																			
							%>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
