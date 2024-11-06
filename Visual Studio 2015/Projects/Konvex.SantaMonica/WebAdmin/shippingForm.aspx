<%@ Page language="c#" Codebehind="shippingForm.aspx.cs" AutoEventWireup="false" Inherits="WebAdmin.shippingForm" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>KONVEX - Fraktsedel</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link rel="stylesheet" href="css/webstyle.css">
	</HEAD>
	
	<script>
	
	function validate()
	{
			noOfContainers = 0;
			<%
				int z = 0;
				while (z < containerDataSet.Tables[0].Rows.Count)
				{
			
					%>
					if (document.thisform.category_<%= containerDataSet.Tables[0].Rows[z].ItemArray.GetValue(0).ToString() %>.value != "")
					{
						document.thisform.weight_<%= containerDataSet.Tables[0].Rows[z].ItemArray.GetValue(0).ToString() %>.value = parseInt(document.thisform.weight_<%= containerDataSet.Tables[0].Rows[z].ItemArray.GetValue(0).ToString() %>.value);
						if (document.thisform.weight_<%= containerDataSet.Tables[0].Rows[z].ItemArray.GetValue(0).ToString() %>.value == "NaN") document.thisform.weight_<%= containerDataSet.Tables[0].Rows[z].ItemArray.GetValue(0).ToString() %>.value = "";
						
						if (document.thisform.weight_<%= containerDataSet.Tables[0].Rows[z].ItemArray.GetValue(0).ToString() %>.value == "")
						{
							alert("Vikt m�ste anges f�r container <%= containerDataSet.Tables[0].Rows[z].ItemArray.GetValue(0).ToString() %>.");						
							return;
						}
						
						noOfContainers++;
					}					
					<%	
								
					z++;
				}
			%>							
			
			if (noOfContainers == 0)
			{
				alert("Ingen container �r rapporterad.");
				return;
			}

			if (!document.thisform.confirmation.checked)
			{
				alert("Uppgifternas riktighet �r inte bekr�ftad. Kryssa i rutan i v�nsterkanten.");
				return;
			}
			
			document.thisform.command.value = "createLineOrder";
			document.thisform.submit();
	}
	
	</script>
	
	<body MS_POSITIONING="GridLayout" topmargin="10">
		<form name="thisform" method="post">
		<input type="hidden" name="command" value="">
			<table cellspacing="0" cellpadding="0" width="640" border="0" style="BORDER-RIGHT: #006699 2px solid; BORDER-TOP: #006699 2px solid; BORDER-LEFT: #006699 2px solid; BORDER-BOTTOM: #006699 2px solid; BACKGROUND-COLOR: #ffffff">
				<tr>
					<td rowspan="2" width="50%" style="border-bottom: 1px #006699 solid; border-right: 1px #006699 solid" valign="top">
						<table cellspacing="0" cellpadding="5" width="100%" border="0">
							<tr>
								<td style="font-size: 10px">Godsmottagare</td>
							</tr>
							<tr>
								<td style="font-size: 46px"><b>KONVEX AB</b></td>
							</tr>
							<tr>
								<td style="font-size: 12px">Box 734, Esplanaden 24<br>531 17 LIDK�PING<br>Tel: 0510-868 50</td>
							</tr>
						</table>
					</td>
					<td valign="top" style="border-bottom: 1px #006699 solid; border-right: 1px #006699 solid">
						<table cellspacing="0" cellpadding="5" width="100%" border="0">
							<tr>
								<td width="50%" style="font-size: 10px">H�mtningsdatum</td>
							</tr>
							<tr>
								<td><% WebAdmin.HTMLHelper.createDatePicker("shipDate", DateTime.Now); %><br><br><select name="shipTimeHour" class="Textfield">
								<%
									int hour = 0;
									while (hour < 24)
									{
										string hourString = hour.ToString();
										if (hourString.Length == 1) hourString = "0"+hourString;
										
										string selected = "";
										if (hour == 16) selected = "selected";
										
										%><option value="<%= hour %>" <%= selected %>><%= hourString %></option><%
									
										hour++;
									}									
								%>
								</select>:<select name="shipTimeMinute" class="Textfield">
								<%
									int minute = 0;
									while (minute < 60)
									{
										string minuteString = minute.ToString();
										if (minuteString.Length == 1) minuteString = "0"+minuteString;
										
										string selected = "";
										if (minute == 0) selected = "selected";
										
										%><option value="<%= minute %>" <%= selected %>><%= minuteString %></option><%
									
										minute = minute + 15;
									}									
								%>
								</select></td>
							</tr>
						</table>
					</td>
					<td valign="top" style="border-bottom: 1px #006699 solid;">
						<table cellspacing="0" cellpadding="5" width="100%" border="0">
							<tr>
								<td style="font-size: 10px">Fraktsedelsnr</td>
							</tr>
							<tr>
								<td>&nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="2" height="100%" valign="top" style="border-bottom: 1px #006699 solid;">
						<table cellspacing="0" cellpadding="5" width="100%" border="0">
							<tr>
								<td style="font-size: 10px">Godsavs�ndare (Ursprungsort)</td>
							</tr>
							<tr>
								<td><%= currentShippingCustomer.name %><br><%= currentShippingCustomer.address %><br><%= currentShippingCustomer.postCode %>&nbsp;<%= currentShippingCustomer.city %></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td rowspan="2" style="border-right: 1px #006699 solid">
						<table cellspacing="0" cellpadding="5" width="100%" border="0">
							<tr>
								<td style="FONT-SIZE: 12px">DETTA UPPDRAG UTF�RS I ENLIGHET MED TRANSPORTF�RETAGETS 
									VID VARJE TIDPUNKT G�LLANDE ANSVARSBEST�MMELSER.<br><br>BEH�LLAREN �R M�RKT ENLIGT NEDAN BEROENDE P� INNEH�LL.<br><br>
									Kat. 1, Endast f�r bortskaffande.<br>
									Kat. 2, F�r inte anv�ndas som foder.<br>
									Kat. 3, F�r inte anv�ndas som livsmedel.</td>
							</tr>
						</table>
					</td>
					<td colspan="2" valign="top" style="border-bottom: 1px #006699 solid">
						<table cellspacing="0" cellpadding="5" width="100%" border="0">
							<tr>
								<td style="font-size: 10px">Slakterinummer / Godk�nnandenr</td>
							</tr>
							<tr>
								<td>&nbsp;</td>
							</tr>
						</table>
					</td>					
				</tr>
				<tr>
					<td colspan="2" valign="top">
						<table cellspacing="0" cellpadding="5" width="100%" border="0">
							<tr>
								<td style="FONT-SIZE: 10px">Mottagande anl�ggning / Godk�nnandenr</td>
							</tr>
							<tr>
								<td>&nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<table cellspacing="0" cellpadding="0" width="640" border="0" style="BORDER-RIGHT: #006699 2px solid; BORDER-LEFT: #006699 2px solid; BORDER-BOTTOM: #006699 1px solid; BACKGROUND-COLOR: #ffffff">
				<tr>
					<td>
						<table cellspacing="0" cellpadding="5" width="100%" border="0">
							<tr>
								<td rowspan="2" valign="bottom" style="BORDER-BOTTOM: #006699 2px solid; font-size: 12px;"><b>Containernr</b></td>
								<td rowspan="2" valign="bottom" style="BORDER-BOTTOM: #006699 2px solid; BORDER-LEFT: #006699 2px solid; font-size: 12px;"><b>Typ</b></td>
								<td rowspan="2" valign="bottom" style="BORDER-BOTTOM: #006699 2px solid; BORDER-LEFT: #006699 2px solid; font-size: 12px;"><b>Inneh�ll</b></td>
								<td style="BORDER-BOTTOM: #006699 2px solid; BORDER-LEFT: #006699 2px solid;" valign="top">
									<table cellspacing="0" cellpadding="5" width="100%" border="0">
										<tr>
											<td style="font-size: 10px;">Konvex leverant�rsnummer</td>
										</tr>
										<tr>
											<td><%= currentShippingCustomer.no %></td>
										</tr>
									</table>
								</td>			
							</tr>
							<tr>
								<td style="BORDER-BOTTOM: #006699 2px solid; font-size: 12px; BORDER-LEFT: #006699 2px solid;" valign="bottom"><b>Leverant�rens uppskattade nettovikt (KG)</b></td>			
							</tr>
							<%
								int i = 0;
								while (i < containerDataSet.Tables[0].Rows.Count)
								{
									Navipro.SantaMonica.Common.LineOrders lineOrders = new Navipro.SantaMonica.Common.LineOrders();
									Navipro.SantaMonica.Common.LineOrder lineOrder = lineOrders.getContainerLineOrder(database, containerDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString());
							
									%>
									<tr>
										<td style="BORDER-BOTTOM: #006699 1px solid; font-size: 12px;"><%= containerDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %></td>
										<td style="BORDER-LEFT: #006699 2px solid; BORDER-BOTTOM: #006699 1px solid; font-size: 12px;"><%= containerDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString() %></td>
										<td style="BORDER-LEFT: #006699 2px solid; BORDER-BOTTOM: #006699 1px solid; font-size: 12px;"><%
																				
											if (lineOrder == null)
											{
												%>
												<select name="category_<%= containerDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>" class="Textfield">
												<option value=""></option>
												<%
													int j = 0;
													while (j < categoryDataSet.Tables[0].Rows.Count)
													{
												
														%><option value="<%= categoryDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString() %>"><%= categoryDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString() %>, <%= categoryDataSet.Tables[0].Rows[j].ItemArray.GetValue(1).ToString() %></option><%										
													
														j++;
													}
												%>										
												</select>
												<%
											}
											else
											{
												%>Anm�ld, ordernr: <%= lineOrder.entryNo %><input type="hidden" name="category_<%= containerDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>" value=""><%
											}
											
										%></td>
										<td style="BORDER-LEFT: #006699 2px solid; BORDER-BOTTOM: #006699 1px solid;"><% if (lineOrder == null) { %><input type="text" name="weight_<%= containerDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>" class="Textfield"><% } %>&nbsp;</td>
									</tr>
									
									<%	
								
								
									i++;
								}
							%>							
						</table>
					</td>
				</tr>
			</table>
			<table cellspacing="0" cellpadding="0" width="640" border="0" style="BORDER-RIGHT: #006699 2px solid; BORDER-LEFT: #006699 2px solid; BORDER-BOTTOM: #006699 2px solid; BACKGROUND-COLOR: #ffffff">
				<tr>
					<td>
						<table cellspacing="0" cellpadding="5" width="100%" border="0">
							<tr>
								<td style="font-size: 10px;">�vriga meddelanden</td>
							</tr>
							<tr>
								<td><textarea name="comments" rows="4" cols="60"></textarea></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>			
			<table cellspacing="0" cellpadding="0" width="640" border="0" style="BORDER-RIGHT: #006699 2px solid; BORDER-LEFT: #006699 2px solid; BORDER-BOTTOM: #006699 2px solid; BACKGROUND-COLOR: #ffffff">
				<tr>
					<td rowspan="3" valign="top" width="33%" style="BORDER-RIGHT: #006699 1px solid;">
						<table cellspacing="0" cellpadding="5" width="100%" border="0">
							<tr>
								<td style="font-size: 10px;">Ovan l�mnade uppgifters riktighet bekr�ftas h�rmed.</td>
							</tr>
							<tr>
								<td><input type="checkbox" name="confirmation"></td>
							</tr>
						</table>
					</td>
					<td rowspan="2" valign="top" width="33%" style="BORDER-RIGHT: #006699 1px solid; BORDER-BOTTOM: #006699 1px solid;">
						<table cellspacing="0" cellpadding="5" width="100%" border="0">
							<tr>
								<td style="font-size: 10px;">Transportf�retag</td>
							</tr>
							<tr>
								<td>&nbsp;</td>
							</tr>
						</table>
					</td>
					<td width="33%" style="BORDER-BOTTOM: #006699 1px solid;">
						<table cellspacing="0" cellpadding="5" width="100%" border="0">
							<tr>
								<td style="font-size: 10px;">Bil</td>
							</tr>
							<tr>
								<td>&nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td valign="top" style="BORDER-BOTTOM: #006699 1px solid;">
						<table cellspacing="0" cellpadding="5" width="100%" border="0">
							<tr>
								<td style="font-size: 10px;">Transportsedel</td>
							</tr>
							<tr>
								<td>&nbsp;</td>
							</tr>
						</table>
					</td>								
				</tr>
				<tr>
					<td valign="top" style="BORDER-RIGHT: #006699 1px solid;">
						<table cellspacing="0" cellpadding="5" width="100%" border="0">
							<tr>
								<td style="font-size: 10px;">F�rare</td>
							</tr>
							<tr>
								<td>&nbsp;</td>
							</tr>
						</table>
					</td>
					<td valign="top">
						<table cellspacing="0" cellpadding="5" width="100%" border="0">
							<tr>
								<td style="font-size: 10px;">Inv�gt av</td>
							</tr>
							<tr>
								<td>&nbsp;</td>
							</tr>
						</table>
					</td>				
				</tr>
			</table>
			<table cellspacing="0" cellpadding="5" border="0" width="640">
				<tr>
					<td align="right"><input type="button" value="Tillbaka" class="Button" onclick="document.location.href='shippingCustomerLineOrders.aspx';">&nbsp;<input type="button" value="Skicka" class="Button" onclick="validate()"></td>
				</tr>
			</table>			
		</form>
	</body>
</HTML>
