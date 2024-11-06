<%@ Page language="c#" Codebehind="shippingForm_print.aspx.cs" AutoEventWireup="false" Inherits="WebAdmin.shippingForm_print" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>KONVEX - Fraktsedel</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link rel="stylesheet" href="http://konvex.workanywhere.se/css/webstyle.css">
	</HEAD>
	<body MS_POSITIONING="GridLayout" topmargin="10">
		<form name="thisform" method="post">
			<table cellspacing="0" cellpadding="0" width="640" border="0" style="BORDER-RIGHT: #006699 2px solid; BORDER-TOP: #006699 2px solid; BORDER-LEFT: #006699 2px solid; BORDER-BOTTOM: #006699 2px solid; BACKGROUND-COLOR: #ffffff">
				<tr>
					<td rowspan="2" width="50%" style="BORDER-RIGHT: #006699 1px solid; BORDER-BOTTOM: #006699 1px solid"
						valign="top">
						<table cellspacing="0" cellpadding="5" width="100%" border="0">
							<tr>
								<td style="FONT-SIZE: 10px">Godsmottagare</td>
							</tr>
							<tr>
								<td style="FONT-SIZE: 46px"><b>KONVEX AB</b></td>
							</tr>
							<tr>
								<td style="FONT-SIZE: 12px">Box 734, Esplanaden 24<br>
									531 17 LIDKÖPING<br>
									Tel: 0510-868 50</td>
							</tr>
						</table>
					</td>
					<td valign="top" style="BORDER-RIGHT: #006699 1px solid; BORDER-BOTTOM: #006699 1px solid">
						<table cellspacing="0" cellpadding="5" width="100%" border="0">
							<tr>
								<td width="50%" style="FONT-SIZE: 10px">Hämtningsdatum</td>
							</tr>
							<tr>
								<td><%= currentLineOrder.shipDate.ToString("yyyy-MM-dd") %></td>
							</tr>
						</table>
					</td>
					<td valign="top" style="BORDER-BOTTOM: #006699 1px solid">
						<table cellspacing="0" cellpadding="5" width="100%" border="0">
							<tr>
								<td style="FONT-SIZE: 10px">Fraktsedel nr</td>
							</tr>
							<tr>
								<td><%= currentLineOrder.entryNo %></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="2" height="100%" valign="top" style="BORDER-BOTTOM: #006699 1px solid">
						<table cellspacing="0" cellpadding="5" width="100%" border="0">
							<tr>
								<td style="FONT-SIZE: 10px">Godsavsändare (Ursprungsort)</td>
							</tr>
							<tr>
								<td><%= currentLineOrder.shippingCustomerName %><br>
									<%= currentLineOrder.address %>
									<br>
									<%= currentLineOrder.postCode %>
									&nbsp;<%= currentLineOrder.city %></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td rowspan="2" style="BORDER-RIGHT: #006699 1px solid">
						<table cellspacing="0" cellpadding="5" width="100%" border="0">
							<tr>
								<td style="FONT-SIZE: 12px">DETTA UPPDRAG UTFÖRS I ENLIGHET MED TRANSPORTFÖRETAGETS 
									VID VARJE TIDPUNKT GÄLLANDE ANSVARSBESTÄMMELSER.<br><br>BEHÅLLAREN ÄR MÄRKT ENLIGT NEDAN BEROENDE PÅ INNEHÅLL.<br><br>
									Kat. 1, Endast för bortskaffande.<br>
									Kat. 2, Får inte användas som foder.<br>
									Kat. 3, Får inte användas som livsmedel.</td>
							</tr>
						</table>
					</td>
					<td colspan="2" valign="top" style="BORDER-BOTTOM: #006699 1px solid">
						<table cellspacing="0" cellpadding="5" width="100%" border="0">
							<tr>
								<td style="FONT-SIZE: 10px">Slakterinr / Godkännandenr</td>
							</tr>
							<tr>
								<td><%= currentShippingCustomer.productionSite %></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="2" valign="top">
						<table cellspacing="0" cellpadding="5" width="100%" border="0">
							<tr>
								<td style="FONT-SIZE: 10px">Mottagande anläggning / Godkännandenr</td>
							</tr>
							<%
							
								Navipro.SantaMonica.Common.Factory factory = currentLineOrder.getArrivalFactory(database);
								
								if (factory != null)
								{
									%>
							<tr>
								<td><%= factory.name %>
									/
									<%= factory.confirmationIdNo %>
								</td>
							</tr>
							<%
								}
							%>
						</table>
					</td>
				</tr>
			</table>
			<table cellspacing="0" cellpadding="0" width="640" border="0" style="BORDER-RIGHT: #006699 2px solid; BORDER-LEFT: #006699 2px solid; BORDER-BOTTOM: #006699 1px solid; BACKGROUND-COLOR: #ffffff">
				<tr>
					<td>
						<table cellspacing="0" cellpadding="5" width="100%" border="0">
							<tr>
								<td rowspan="2" valign="bottom" style="FONT-SIZE: 12px; BORDER-BOTTOM: #006699 2px solid"><b>Containernr</b></td>
								<td rowspan="2" valign="bottom" style="FONT-SIZE: 12px; BORDER-LEFT: #006699 2px solid; BORDER-BOTTOM: #006699 2px solid"><b>Typ</b></td>
								<td rowspan="2" valign="bottom" width="200" style="FONT-SIZE: 12px; BORDER-LEFT: #006699 2px solid; BORDER-BOTTOM: #006699 2px solid"><b>Innehåll</b></td>
								<td colspan="2" style="BORDER-LEFT: #006699 2px solid; BORDER-BOTTOM: #006699 2px solid"
									valign="top">
									<table cellspacing="0" cellpadding="5" width="100%" border="0">
										<tr>
											<td style="FONT-SIZE: 10px">Konvex leverantörsnummer</td>
										</tr>
										<tr>
											<td><%= currentLineOrder.shippingCustomerNo %></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td style="FONT-SIZE: 12px; BORDER-LEFT: #006699 2px solid; BORDER-BOTTOM: #006699 2px solid"
									valign="bottom" align="right"><b>Leverantörens uppskattade nettovikt (KG)</b></td>
								<td style="FONT-SIZE: 12px; BORDER-LEFT: #006699 2px solid; BORDER-BOTTOM: #006699 2px solid"
									valign="bottom" align="right"><b>Konvex invägda nettovikt (KG)</b></td>
							</tr>
							<%
								int i = 0;
								while (i < containerDataSet.Tables[0].Rows.Count)
								{
									Navipro.SantaMonica.Common.Containers containers = new Navipro.SantaMonica.Common.Containers();
									Navipro.SantaMonica.Common.Container container = containers.getEntry(database, containerDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString());

									Navipro.SantaMonica.Common.Categories categories = new Navipro.SantaMonica.Common.Categories();
									Navipro.SantaMonica.Common.Category category = categories.getEntry(database, containerDataSet.Tables[0].Rows[i].ItemArray.GetValue(3).ToString());
							
									%>
							<tr>
								<td style="FONT-SIZE: 12px; BORDER-BOTTOM: #006699 1px solid"><%= container.description %></td>
								<td style="FONT-SIZE: 12px; BORDER-LEFT: #006699 2px solid; BORDER-BOTTOM: #006699 1px solid"><%= container.containerTypeCode %></td>
								<td style="FONT-SIZE: 12px; BORDER-LEFT: #006699 2px solid; BORDER-BOTTOM: #006699 1px solid"
									nowrap><%= category.description %></td>
								<td style="FONT-SIZE: 12px; BORDER-LEFT: #006699 2px solid; BORDER-BOTTOM: #006699 1px solid"
									align="right"><%= float.Parse(containerDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString()).ToString("0") %></td>
								<td style="FONT-SIZE: 12px; BORDER-LEFT: #006699 2px solid; BORDER-BOTTOM: #006699 1px solid"
									align="right"><%= (float.Parse(containerDataSet.Tables[0].Rows[i].ItemArray.GetValue(5).ToString())*1000).ToString("0") %></td>
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
								<td style="FONT-SIZE: 10px">Övriga meddelanden</td>
							</tr>
							<tr>
								<td style="FONT-SIZE: 12px"><%= currentLineOrder.comments %>&nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<table cellspacing="0" cellpadding="0" width="640" border="0" style="BORDER-RIGHT: #006699 2px solid; BORDER-LEFT: #006699 2px solid; BORDER-BOTTOM: #006699 2px solid; BACKGROUND-COLOR: #ffffff">
				<tr>
					<td rowspan="3" valign="top" width="33%" style="BORDER-RIGHT: #006699 1px solid">
						<table cellspacing="0" cellpadding="5" width="100%" border="0">
							<tr>
								<td style="FONT-SIZE: 10px">Ovan lämnade uppgifters riktighet är bekräftade av</td>
							</tr>
							<tr>
								<td style="FONT-SIZE: 12px" valign="bottom"><%= currentLineOrder.getCreatedByName(database) %>&nbsp;</td>
							</tr>
						</table>
					</td>
					<td rowspan="2" valign="top" width="33%" style="BORDER-RIGHT: #006699 1px solid; BORDER-BOTTOM: #006699 1px solid">
						<table cellspacing="0" cellpadding="5" width="100%" border="0">
							<tr>
								<td style="FONT-SIZE: 10px">Transportföretag</td>
							</tr>
							<%
								Navipro.SantaMonica.Common.Organization organization = currentLineOrder.getOrganization(database);
								
								if (organization != null)
								{
									%>
							<tr>
								<td style="FONT-SIZE: 12px"><%= organization.name %><br>
									<%= organization.address %>
									<br>
									<%= organization.postCode %>
									&nbsp;<%= organization.city %>&nbsp;</td>
							</tr>
							<%
								}
								else
								{
									%>
							<tr>
								<td style="FONT-SIZE: 12px">&nbsp;</td>
							</tr>
							<%								
								}
							%>
						</table>
					</td>
					<td width="33%" style="BORDER-BOTTOM: #006699 1px solid">
						<table cellspacing="0" cellpadding="5" width="100%" border="0">
							<tr>
								<td style="FONT-SIZE: 10px">Bil</td>
							</tr>
							<tr>
								<td style="FONT-SIZE: 12px"><%= currentLineOrder.getAgentName(database) %>&nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td valign="top" style="BORDER-BOTTOM: #006699 1px solid">
						<table cellspacing="0" cellpadding="5" width="100%" border="0">
							<tr>
								<td style="FONT-SIZE: 10px">Transportsedel</td>
							</tr>
							<tr>
								<td style="FONT-SIZE: 12px"><%= currentLineOrder.getRouteName(database) %>&nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td valign="top" style="BORDER-RIGHT: #006699 1px solid">
						<table cellspacing="0" cellpadding="5" width="100%" border="0">
							<tr>
								<td style="FONT-SIZE: 10px">Förare</td>
							</tr>
							<tr>
								<td style="FONT-SIZE: 12px"><%= currentLineOrder.getAgentMobileUser(database) %>&nbsp;</td>
							</tr>
						</table>
					</td>
					<td valign="top">
						<table cellspacing="0" cellpadding="5" width="100%" border="0">
							<tr>
								<td style="FONT-SIZE: 10px">Invägt av</td>
							</tr>
							<tr>
								<td>&nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<% if ((Request["sid"] == "") || (Request["sid"] == null)) { %>
			<table cellspacing="0" cellpadding="5" border="0" width="640">
				<tr>
					<td align="right"><input type="button" value="Tillbaka" class="Button" onclick="document.location.href='shippingCustomerLineOrders.aspx';"></td>
				</tr>
			</table>
			<% } %>
		</form>
	</body>
</HTML>
