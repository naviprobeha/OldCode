<%@ Page language="c#" Codebehind="orders_items_modify.aspx.cs" AutoEventWireup="false" Inherits="Navipro.SantaMonica.WebAdmin.orders_items_modify" %>
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
		
		function goBack()
		{
			document.location.href='orders_items.aspx?shipOrderNo=<%= currentShipOrder.entryNo %>';		
		}

		function save()
		{
			if (document.thisform.quantity.value == "")
			{
				alert("Antal måste anges.");
				return;
			}
			
			document.thisform.action = "orders_items_modify.aspx";
			document.thisform.command.value = "save";
			document.thisform.submit();
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
		<form id="thisform" action="orders_view.aspx" method="post" runat="server">
			<input type="hidden" name="command" value="">
			<input type="hidden" name="shipOrderNo" value="<%= currentShipOrder.entryNo %>"> 
			<input type="hidden" name="entryNo" value="<%= currentShipOrderLine.entryNo %>"> 
			<input type="hidden" name="mode" value="">		
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame">
				<tr>
					<td height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td class="activityName">Körorder <% if (currentShipOrder.entryNo > 0) Response.Write(currentShipOrder.entryNo); %></td>
				</tr>
				<tr>
					<td align="left" height="25"><input type="button" onclick="goBack()" value="Avbryt" class="Button"></td>
				</tr>
				<tr>
					<td>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<td class="interaction" valign="top" width="300">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Hämtdatum</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentShipOrder.shipDate.ToString("yyyy-MM-dd") %></b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Anmälningsdatum</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><% if (currentShipOrder.creationDate.Year != 1753) Response.Write(currentShipOrder.creationDate.ToString("yyyy-MM-dd")); %></b>&nbsp;</td>
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
											<td colspan="2" class="activityAuthor" height="15" valign="top">Kundnr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentShipOrder.customerNo %></b>&nbsp;</td>
											<td class="interaction" width="90%" nowrap><a href="customers_view.aspx?customerNo=<%= currentShipOrder.customerNo %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Kundnamn</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentShipOrder.customerName %></b></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>				
				<tr>
					<td>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">Artikel</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right">Antal</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right">Avlivningar</th>
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
											
											string selected = "";
											if (currentShipOrderLine.itemNo == itemDataSet1.Tables[0].Rows[o].ItemArray.GetValue(0).ToString()) selected = "selected"; 

											%>
											<option value="<%= itemDataSet1.Tables[0].Rows[o].ItemArray.GetValue(0).ToString() %>" <%= selected %>><%= itemDataSet1.Tables[0].Rows[o].ItemArray.GetValue(0).ToString() %>, <%= itemDataSet1.Tables[0].Rows[o].ItemArray.GetValue(1).ToString() %> [<%= itemDataSet1.Tables[0].Rows[o].ItemArray.GetValue(8).ToString() %>] <%= cashPayment %></option>
											<%
												
											o++;
										}
									
									
										o = 0;
										while (o < itemDataSet2.Tables[0].Rows.Count)
										{							
											string cashPayment = "";
											if (itemDataSet2.Tables[0].Rows[o].ItemArray.GetValue(11).ToString() == "1") cashPayment = "[Kontant]";

											string selected = "";
											if (currentShipOrderLine.itemNo == itemDataSet2.Tables[0].Rows[o].ItemArray.GetValue(0).ToString()) selected = "selected"; 

											%>
											<option value="<%= itemDataSet2.Tables[0].Rows[o].ItemArray.GetValue(0).ToString() %>" <%= selected %>><%= itemDataSet2.Tables[0].Rows[o].ItemArray.GetValue(0).ToString() %>, <%= itemDataSet2.Tables[0].Rows[o].ItemArray.GetValue(1).ToString() %> [<%= itemDataSet2.Tables[0].Rows[o].ItemArray.GetValue(8).ToString() %>] <%= cashPayment %></option>
											<%
												
											o++;
										}

										o = 0;
										while (o < itemDataSet3.Tables[0].Rows.Count)
										{							
											string cashPayment = "";
											if (itemDataSet3.Tables[0].Rows[o].ItemArray.GetValue(11).ToString() == "1") cashPayment = "[Kontant]";

											string selected = "";
											if (currentShipOrderLine.itemNo == itemDataSet3.Tables[0].Rows[o].ItemArray.GetValue(0).ToString()) selected = "selected"; 

											%>
											<option value="<%= itemDataSet3.Tables[0].Rows[o].ItemArray.GetValue(0).ToString() %>" <%= selected %>><%= itemDataSet3.Tables[0].Rows[o].ItemArray.GetValue(0).ToString() %>, <%= itemDataSet3.Tables[0].Rows[o].ItemArray.GetValue(1).ToString() %> [<%= itemDataSet3.Tables[0].Rows[o].ItemArray.GetValue(8).ToString() %>] <%= cashPayment %></option>
											<%
												
											o++;
										}

										o = 0;
										while (o < itemDataSet4.Tables[0].Rows.Count)
										{							
											string cashPayment = "";
											if (itemDataSet4.Tables[0].Rows[o].ItemArray.GetValue(11).ToString() == "1") cashPayment = "[Kontant]";

											string selected = "";
											if (currentShipOrderLine.itemNo == itemDataSet4.Tables[0].Rows[o].ItemArray.GetValue(0).ToString()) selected = "selected"; 

											%>
											<option value="<%= itemDataSet4.Tables[0].Rows[o].ItemArray.GetValue(0).ToString() %>" <%= selected %>><%= itemDataSet4.Tables[0].Rows[o].ItemArray.GetValue(0).ToString() %>, <%= itemDataSet4.Tables[0].Rows[o].ItemArray.GetValue(1).ToString() %> [<%= itemDataSet4.Tables[0].Rows[o].ItemArray.GetValue(8).ToString() %>] <%= cashPayment %></option>
											<%
												
											o++;
										}
										
									
									%>
								</select></td>
								<td class="jobDescription" valign="top" align="right"><input type="text" name="quantity" class="Textfield" size="6" maxlength="5" value="<%= currentShipOrderLine.quantity %>"></td>
								<td class="jobDescription" valign="top" align="right"><input type="text" name="connectionQuantity" class="Textfield" size="6" maxlength="5" value="<%= currentShipOrderLine.connectionQuantity %>"></td>
								<td class="jobDescription" valign="top" align="center"><input type="button" value="Spara" class="button" onclick="save()"></td>
							</tr>
							
						</table>
					</td>
				</tr>
				<tr>
					<td align="right" height="25"><input type="button" onclick="goBack()" value="Avbryt" class="Button"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
