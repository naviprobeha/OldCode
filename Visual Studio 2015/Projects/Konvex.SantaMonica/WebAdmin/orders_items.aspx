<%@ Page language="c#" Codebehind="orders_items.aspx.cs" AutoEventWireup="false" Inherits="Navipro.SantaMonica.WebAdmin.orders_items" %>
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
		
		function modifyOrder()
		{
			document.location.href = "orders_modify.aspx?shipOrderNo=<%= currentShipOrder.entryNo %>";			
		}

		function goBack()
		{
			document.location.href='orders_view.aspx?shipOrderNo=<%= currentShipOrder.entryNo %>';		
		}

		function addItem()
		{
			if (document.thisform.quantity.value == "")
			{
				alert("Antal måste anges.");
				return;
			}
			
			document.thisform.action = "orders_items.aspx";
			document.thisform.command.value = "addItem";
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
					<td align="left" height="25"><input type="button" onclick="goBack()" value="Klar" class="Button"></td>
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
											<td class="interaction" height="20"><b><%= currentShipOrder.productionSite %></b></td>
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
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">Artikelnr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">Beskrivning</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right">Antal</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right">Enhet</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right">Pris</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right">Avlivningar</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right">Pris avlivning</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right">Provtagningar</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right">Totalbelopp</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" colspan="2">ID</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca">Ta bort</th>	
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca">Ändra</th>	
							</tr>
							<%
						
								int i = 0;
								while (i < shipOrderLineDataSet.Tables[0].Rows.Count)
								{
									string price = decimal.Round(decimal.Parse(shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(6).ToString()), 2)+" kr";
									string connectionPrice = decimal.Round(decimal.Parse(shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(8).ToString()), 2)+" kr";
									string totalAmount = decimal.Round(decimal.Parse(shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(9).ToString()), 2)+" kr";

									Navipro.SantaMonica.Common.Item item = items.getEntry(database, shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString());
							
									%>
									<tr>
										<td class="jobDescription" valign="top"><%= shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString() %></td>
										<td class="jobDescription" valign="top"><%= item.description %></td>
										<td class="jobDescription" valign="top" align="right"><%= shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(3).ToString() %></td>
										<td class="jobDescription" valign="top" align="right"><%= item.unitOfMeasure %></td>
										<td class="jobDescription" valign="top" align="right"><%= price %></td>
										<td class="jobDescription" valign="top" align="right"><%= shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() %></td>
										<td class="jobDescription" valign="top" align="right"><%= connectionPrice %></td>
										<td class="jobDescription" valign="top" align="right"><%= shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(11).ToString() %></td>
										<td class="jobDescription" valign="top" align="right"><%= totalAmount %></td>
										<td class="jobDescription" valign="top" nowrap><%
										
											System.Data.DataSet shipOrderLineIdDataSet = shipOrderLineIds.getDataSet(database, currentShipOrder.entryNo, int.Parse(shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()));
											
											int m = 0;
											
											while (m < shipOrderLineIdDataSet.Tables[0].Rows.Count)
											{
												string bseValue = "";
												string postMortemValue = "";
												if (shipOrderLineIdDataSet.Tables[0].Rows[m].ItemArray.GetValue(4).ToString() == "1") bseValue = "(P)";
												if (shipOrderLineIdDataSet.Tables[0].Rows[m].ItemArray.GetValue(5).ToString() == "1") postMortemValue = "(O)";
												
												
												Response.Write(shipOrderLineIdDataSet.Tables[0].Rows[m].ItemArray.GetValue(3).ToString()+bseValue+postMortemValue+"<br>");
												m++;
											}
										
										
										%></td>
										<td class="jobDescription" valign="top" align="center"><a href="orders_id.aspx?shipOrderNo=<%= Request["shipOrderNo"] %>&shipOrderlineNo=<%= shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
										<td class="jobDescription" valign="top" align="center"><a href="orders_items.aspx?command=deleteLine&shipOrderNo=<%= Request["shipOrderNo"] %>&lineNo=<%= shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
										<td class="jobDescription" valign="top" align="center"><a href="orders_items_modify.aspx?shipOrderNo=<%= Request["shipOrderNo"] %>&entryNo=<%= shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
									</tr>
									<%
								
									i++;
								}

								if (i == 0)
								{
									%><tr>
										<td class="jobDescription" colspan="13">Inga rader registrerade.</td>
									</tr><%
								}
							
							%>
						</table>
						<br>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">Artikel</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right">Antal</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right">Avlivningar</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">ID</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">Provt.</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">Obd.</th>
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
											
											%>
											<option value="<%= itemDataSet1.Tables[0].Rows[o].ItemArray.GetValue(0).ToString() %>"><%= itemDataSet1.Tables[0].Rows[o].ItemArray.GetValue(0).ToString() %>, <%= itemDataSet1.Tables[0].Rows[o].ItemArray.GetValue(1).ToString() %> [<%= itemDataSet1.Tables[0].Rows[o].ItemArray.GetValue(8).ToString() %>] <%= cashPayment %></option>
											<%
												
											o++;
										}
									
									
										o = 0;
										while (o < itemDataSet2.Tables[0].Rows.Count)
										{							
											string cashPayment = "";
											if (itemDataSet2.Tables[0].Rows[o].ItemArray.GetValue(11).ToString() == "1") cashPayment = "[Kontant]";

											%>
											<option value="<%= itemDataSet2.Tables[0].Rows[o].ItemArray.GetValue(0).ToString() %>"><%= itemDataSet2.Tables[0].Rows[o].ItemArray.GetValue(0).ToString() %>, <%= itemDataSet2.Tables[0].Rows[o].ItemArray.GetValue(1).ToString() %> [<%= itemDataSet2.Tables[0].Rows[o].ItemArray.GetValue(8).ToString() %>] <%= cashPayment %></option>
											<%
												
											o++;
										}

										o = 0;
										while (o < itemDataSet3.Tables[0].Rows.Count)
										{							
											string cashPayment = "";
											if (itemDataSet3.Tables[0].Rows[o].ItemArray.GetValue(11).ToString() == "1") cashPayment = "[Kontant]";

											%>
											<option value="<%= itemDataSet3.Tables[0].Rows[o].ItemArray.GetValue(0).ToString() %>"><%= itemDataSet3.Tables[0].Rows[o].ItemArray.GetValue(0).ToString() %>, <%= itemDataSet3.Tables[0].Rows[o].ItemArray.GetValue(1).ToString() %> [<%= itemDataSet3.Tables[0].Rows[o].ItemArray.GetValue(8).ToString() %>] <%= cashPayment %></option>
											<%
												
											o++;
										}

										o = 0;
										while (o < itemDataSet4.Tables[0].Rows.Count)
										{							
											string cashPayment = "";
											if (itemDataSet4.Tables[0].Rows[o].ItemArray.GetValue(11).ToString() == "1") cashPayment = "[Kontant]";

											%>
											<option value="<%= itemDataSet4.Tables[0].Rows[o].ItemArray.GetValue(0).ToString() %>"><%= itemDataSet4.Tables[0].Rows[o].ItemArray.GetValue(0).ToString() %>, <%= itemDataSet4.Tables[0].Rows[o].ItemArray.GetValue(1).ToString() %> [<%= itemDataSet4.Tables[0].Rows[o].ItemArray.GetValue(8).ToString() %>] <%= cashPayment %></option>
											<%
												
											i++;
										}
										
									
									%>
								</select></td>
								<td class="jobDescription" valign="top" align="right"><input type="text" name="quantity" class="Textfield" size="6" maxlength="5"></td>
								<td class="jobDescription" valign="top" align="right"><input type="text" name="connectionQuantity" class="Textfield" size="6" maxlength="5"></td>
								<td class="jobDescription" valign="top" align="left"><input type="text" name="id1" class="Textfield" maxlength="20">&nbsp;<input type="button" class="Button" name="se1" value="SE-nr" onclick="document.thisform.id1.value='SE <%= currentShipOrder.productionSite %>-'+document.thisform.id1.value"><br><input type="text" name="id2" class="Textfield" maxlength="20">&nbsp;<input type="button" class="Button" name="se2" value="SE-nr" onclick="document.thisform.id2.value='SE <%= currentShipOrder.productionSite %>-'+document.thisform.id2.value"><br><input type="text" name="id3" class="Textfield" maxlength="20">&nbsp;<input type="button" class="Button" name="se3" value="SE-nr" onclick="document.thisform.id3.value='SE <%= currentShipOrder.productionSite %>-'+document.thisform.id3.value"><br><input type="text" name="id4" class="Textfield" maxlength="20">&nbsp;<input type="button" class="Button" name="se4" value="SE-nr" onclick="document.thisform.id4.value='SE <%= currentShipOrder.productionSite %>-'+document.thisform.id4.value"><br><input type="text" name="id5" class="Textfield" maxlength="20">&nbsp;<input type="button" class="Button" name="se5" value="SE-nr" onclick="document.thisform.id5.value='SE <%= currentShipOrder.productionSite %>-'+document.thisform.id5.value"></td>
								<td class="jobDescription" valign="top" align="center"><input type="checkbox" name="bse1"><br><input type="checkbox" name="bse2"><br><input type="checkbox" name="bse3"><br><input type="checkbox" name="bse4"><br><input type="checkbox" name="bse5"></td>
								<td class="jobDescription" valign="top" align="center"><input type="checkbox" name="obd1"><br><input type="checkbox" name="obd2"><br><input type="checkbox" name="obd3"><br><input type="checkbox" name="bd4"><br><input type="checkbox" name="obd5"></td>
								<td class="jobDescription" valign="top" align="center"><input type="button" value="Lägg till" class="button" onclick="addItem()"></td>
							</tr>
							
						</table>
					</td>
				</tr>
				<tr>
					<td align="right" height="25"><input type="button" onclick="goBack()" value="Klar" class="Button"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
