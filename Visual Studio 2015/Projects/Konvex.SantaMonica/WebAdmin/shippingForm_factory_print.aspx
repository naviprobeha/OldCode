<%@ Page language="c#" Codebehind="shippingForm_factory_print.aspx.cs" AutoEventWireup="false" Inherits="WebAdmin.shippingForm_factory_print" %>
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
								<td><%= currentFactoryOrder.shipDate.ToString("yyyy-MM-dd") %></td>
							</tr>
						</table>
					</td>
					<td valign="top" style="BORDER-BOTTOM: #006699 1px solid">
						<table cellspacing="0" cellpadding="5" width="100%" border="0">
							<tr>
								<td style="FONT-SIZE: 10px">Fraktsedel nr</td>
							</tr>
							<tr>
								<td>FO-<%= currentFactoryOrder.entryNo %></td>
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
								<td><%= currentFactoryOrder.factoryName %><br>
									<%= currentFactoryOrder.factoryAddress %>
									<br>
									<%= currentFactoryOrder.factoryPostCode %>
									&nbsp;<%= currentFactoryOrder.factoryCity %></td>
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
									Kat. 1, Endast för bortskaffande.</td>
							</tr>
						</table>
					</td>
					<td colspan="2" valign="top" style="BORDER-BOTTOM: #006699 1px solid">
						<table cellspacing="0" cellpadding="5" width="100%" border="0">
							<tr>
								<td style="FONT-SIZE: 10px">Avsändande anläggnings godkännandenr</td>
							</tr>
							<tr>
								<td><%= productionSite %>&nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="2" valign="top">
						<table cellspacing="0" cellpadding="5" width="100%" border="0">
							<tr>
								<td style="FONT-SIZE: 10px">Mottagande anläggning</td>
							</tr>

							<tr>
								<td><%= currentFactoryOrder.consumerName %><br>
								<%= currentFactoryOrder.consumerAddress %><br>
								<%= currentFactoryOrder.consumerPostCode %>&nbsp;<%= currentFactoryOrder.consumerCity %>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<table cellspacing="0" cellpadding="0" width="640" border="0" style="BORDER-RIGHT: #006699 2px solid; BORDER-LEFT: #006699 2px solid; BORDER-BOTTOM: #006699 2px solid; BACKGROUND-COLOR: #ffffff">
				<tr>
					<td style="FONT-SIZE: 10px; BORDER-BOTTOM: #006699 1px solid; BORDER-RIGHT: #006699 1px solid" valign="top" width="33%">
						<table cellspacing="0" cellpadding="5" border="0" width="100%">
						<tr>
							<td style="FONT-SIZE: 10px;">PH-värde (Avsändare)</td>	
						</tr>
						<tr>
							<td style="FONT-SIZE: 12px;"><%= currentFactoryOrder.phValueFactory %>&nbsp;</td>	
						</tr>
						</table>
					</td>												
					<td style="FONT-SIZE: 10px; BORDER-BOTTOM: #006699 1px solid; BORDER-RIGHT: #006699 1px solid" valign="top" width="33%">
						<table cellspacing="0" cellpadding="5" border="0" width="100%">
						<tr>
							<td style="FONT-SIZE: 10px;">PH-värde (Transportör)</td>	
						</tr>
						<tr>
							<td style="FONT-SIZE: 12px;"><%= currentFactoryOrder.phValueShipping %>&nbsp;</td>	
						</tr>
						</table>
					</td>												
					<td style="FONT-SIZE: 10px; BORDER-BOTTOM: #006699 1px solid; " valign="top" width="33%">
						<table cellspacing="0" cellpadding="5" border="0" width="100%">
						<tr>
							<td style="FONT-SIZE: 10px;">Konvex leverantörsnummer</td>	
						</tr>
						<tr>
							<td style="FONT-SIZE: 12px;"><%= currentFactoryOrder.factoryNo %></td>	
						</tr>
						</table>
					</td>												
				</tr>
				<tr>
					<td style="BORDER-RIGHT: #006699 1px solid">
						<table cellspacing="0" cellpadding="5" border="0">
						<tr>
							<td style="FONT-SIZE: 10px;">Kategori</td>	
						</tr>
						<tr>
							<td style="FONT-SIZE: 12px;"><%= currentFactoryOrder.categoryCode %>&nbsp;<%= currentFactoryOrder.categoryDescription %></td>	
						</tr>
						</table>
					</td>
					<td valign="top" style="BORDER-RIGHT: #006699 1px solid">
						<table cellspacing="0" cellpadding="5" border="0" width="100%">
						<tr>
							<td style="FONT-SIZE: 10px;">Leverantörens uppskattade nettovikt kg</td>	
						</tr>
						<tr>
							<td style="FONT-SIZE: 12px;"><%= (currentFactoryOrder.quantity * 1000).ToString("0") %></td>	
						</tr>
						</table>									
					</td>												
					<td valign="top">
						<table cellspacing="0" cellpadding="5" border="0" width="100%">
						<tr>
							<td style="FONT-SIZE: 10px;">Konvex invägda nettovikt kg</td>	
						</tr>
						<tr>
							<td style="FONT-SIZE: 12px;"><%= (currentFactoryOrder.realQuantity * 1000).ToString("0") %></td>	
						</tr>
						</table>									
					</td>
				</tr>
			</table>
			<table cellspacing="0" cellpadding="0" width="640" border="0" style="BORDER-RIGHT: #006699 2px solid; BORDER-LEFT: #006699 2px solid; BORDER-BOTTOM: #006699 2px solid; BACKGROUND-COLOR: #ffffff">
				<tr>
					<td style="BORDER-RIGHT: #006699 1px solid;BORDER-BOTTOM: #006699 1px solid;" width="33%">
						<table cellspacing="0" cellpadding="5" width="100%" border="0">
							<tr>
								<td style="FONT-SIZE: 10px">Lastning slutförd klockan</td>
							</tr>
							<tr>
								<td style="FONT-SIZE: 12px"><% if (currentFactoryOrder.status > 2) Response.Write(currentFactoryOrder.shipDate.ToString("yyyy-MM-dd") + " " + currentFactoryOrder.shipTime.ToString("HH:mm")); %>&nbsp;</td>
							</tr>
						</table>
					</td>
					<td style="BORDER-RIGHT: #006699 1px solid;BORDER-BOTTOM: #006699 1px solid;" width="33%">
						<table cellspacing="0" cellpadding="5" width="100%" border="0">
							<tr>
								<td style="FONT-SIZE: 10px">Lasttid</td>
							</tr>
							<tr>
								<td style="FONT-SIZE: 12px"><% if (currentFactoryOrder.loadDuration > 0) Response.Write(currentFactoryOrder.loadDuration.ToString() + " minuter"); %>&nbsp;</td>
							</tr>
						</table>
					</td>
					<td style="BORDER-BOTTOM: #006699 1px solid;" width="33%">
						<table cellspacing="0" cellpadding="5" width="100%" border="0">
							<tr>
								<td style="FONT-SIZE: 10px">Väntetid</td>
							</tr>
							<tr>
								<td style="FONT-SIZE: 12px"><% if (currentFactoryOrder.loadWaitDuration > 0) Response.Write(currentFactoryOrder.loadWaitDuration.ToString() + " minuter"); %>&nbsp;</td>
							</tr>
						</table>
					</td>
					
				</tr>
				<tr>
					<td style="BORDER-RIGHT: #006699 1px solid;" width="33%">
						<table cellspacing="0" cellpadding="5" width="100%" border="0">
							<tr>
								<td style="FONT-SIZE: 10px">Lossning slutförd klockan</td>
							</tr>
							<tr>
								<td style="FONT-SIZE: 12px"><% if (currentFactoryOrder.status > 3) Response.Write(currentFactoryOrder.arrivalDateTime.ToString("yyyy-MM-dd HH:mm")); %>&nbsp;</td>
							</tr>
						</table>
					</td>
					<td style="BORDER-RIGHT: #006699 1px solid;" width="33%">
						<table cellspacing="0" cellpadding="5" width="100%" border="0">
							<tr>
								<td style="FONT-SIZE: 10px">Losstid</td>
							</tr>
							<tr>
								<td style="FONT-SIZE: 12px"><% if (currentFactoryOrder.dropDuration > 0) Response.Write(currentFactoryOrder.dropDuration.ToString() + " minuter"); %>&nbsp;</td>
							</tr>
						</table>
					</td>
					<td width="33%">
						<table cellspacing="0" cellpadding="5" width="100%" border="0">
							<tr>
								<td style="FONT-SIZE: 10px">Väntetid</td>
							</tr>
							<tr>
								<td style="FONT-SIZE: 12px"><% if (currentFactoryOrder.dropWaitDuration > 0) Response.Write(currentFactoryOrder.dropWaitDuration.ToString() + " minuter"); %>&nbsp;</td>
							</tr>
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
								<td style="FONT-SIZE: 12px"><%= currentFactoryOrder.comments %>&nbsp;</td>
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
								<td style="FONT-SIZE: 12px" valign="bottom"><%= currentFactoryOrder.getCreatedByName(database) %>&nbsp;</td>
							</tr>
						</table>
					</td>
					<td rowspan="2" valign="top" width="33%" style="BORDER-RIGHT: #006699 1px solid; BORDER-BOTTOM: #006699 1px solid">
						<table cellspacing="0" cellpadding="5" width="100%" border="0">
							<tr>
								<td style="FONT-SIZE: 10px">Transportföretagets namn och adress</td>
							</tr>
							<%
								Navipro.SantaMonica.Common.Organization organization = currentFactoryOrder.getOrganization(database);
								
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
					<td width="33%">
						<table cellspacing="0" cellpadding="5" width="100%" border="0">
							<tr>
								<td style="FONT-SIZE: 10px">Bil</td>
							</tr>
							<tr>
								<td style="FONT-SIZE: 12px"><%= currentFactoryOrder.agentCode %>&nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td valign="top" style="BORDER-BOTTOM: #006699 1px solid">&nbsp;</td>
				</tr>
				<tr>
					<td valign="top" style="BORDER-RIGHT: #006699 1px solid">
						<table cellspacing="0" cellpadding="5" width="100%" border="0">
							<tr>
								<td style="FONT-SIZE: 10px">Förare</td>
							</tr>
							<tr>
								<td style="FONT-SIZE: 12px"><%= currentFactoryOrder.driverName %></td>
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
					<td align="right"><% if (showModify) { %><input type="button" value="Ändra" class="Button" onclick="document.location.href='shippingForm_factory_modify.aspx?factoryOrderEntryNo=<%= currentFactoryOrder.entryNo %>';"><% } %>&nbsp;<input type="button" value="Tillbaka" class="Button" onclick="document.location.href='<%= returnPage %>';"></td>
				</tr>
			</table>
			<% } %>
		</form>
	</body>
</HTML>
