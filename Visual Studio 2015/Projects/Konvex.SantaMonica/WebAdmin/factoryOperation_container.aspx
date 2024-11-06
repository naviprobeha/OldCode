<%@ Page language="c#" Codebehind="factoryOperation_container.aspx.cs" AutoEventWireup="false" Inherits="WebAdmin.factoryOperation_container" %>
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
	<body MS_POSITIONING="GridLayout" topmargin="10">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" action="orders_view.aspx" method="post" runat="server">
			<input type="hidden" name="command"> <input type="hidden" name="lineOrderNo" value="<%= currentLineOrder.entryNo %>">
			<input type="hidden" name="mode">
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame">
				<tr>
					<td height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td class="activityName">Container
						<%= Request["containerNo"] %>
					</td>
				</tr>
				<tr>
					<td>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									&nbsp;</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Artikelnr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Beskrivning</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Antal</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Enhet</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									ID</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Reservmärkning</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Provtagning</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Obduktion</th>
								<th class="headerFrame" align="center" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									&nbsp;</th>
							</tr>
							<%
								int i = 0;
								while (i < shipmentLineDataSet.Tables[0].Rows.Count)
								{
								
									string bgStyle = "";
									
									if ((i % 2) > 0)
									{
										bgStyle = "background-color: #e0e0e0;";
									}
									
									string unitOfMeasure = "";
									
									Navipro.SantaMonica.Common.Items items = new Navipro.SantaMonica.Common.Items();
									Navipro.SantaMonica.Common.Item item = items.getEntry(database, shipmentLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(5).ToString());

									if (item != null)
									{
										unitOfMeasure = item.unitOfMeasure;
									}
									
									string testQuantity = shipmentLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(8).ToString();
									if (testQuantity == "0") testQuantity = "";

									Navipro.SantaMonica.Common.ShipmentLineIds shipmentLineIds = new Navipro.SantaMonica.Common.ShipmentLineIds(database);
									System.Data.DataSet idDataSet = shipmentLineIds.getShipmentLineIdDataSet(shipmentLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(3).ToString(), int.Parse(shipmentLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString()));

								
									%>
							<tr>
								<td class="jobDescription" valign="top">&nbsp;</td>
								<td class="jobDescription" valign="top"><%= shipmentLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(5).ToString() %>&nbsp;</td>
								<td class="jobDescription" valign="top"><%= shipmentLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(6).ToString() %>&nbsp;</td>
								<td class="jobDescription" valign="top"><%= shipmentLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(7).ToString() %>&nbsp;</td>
								<td class="jobDescription" valign="top"><%= unitOfMeasure %>&nbsp;</td>
								<td class="jobDescription" valign="top"><%
											
											
											int l = 0;
											string idString = "";
											
											while(l < idDataSet.Tables[0].Rows.Count)
											{
												if (l > 0) idString = idString + "<br>";
												idString = idString + idDataSet.Tables[0].Rows[l].ItemArray.GetValue(3).ToString();
											
												l++;
											}
											
											Response.Write(idString);
										
										%>&nbsp;</td>
								<td class="jobDescription" valign="top"><%
											
											
											l = 0;
											string reservString = "";
											
											while(l < idDataSet.Tables[0].Rows.Count)
											{
												if (l > 0) reservString = reservString + "<br>";
												reservString = reservString + idDataSet.Tables[0].Rows[l].ItemArray.GetValue(5).ToString();
											
												l++;
											}
											
											Response.Write(reservString);
										
										%>&nbsp;</td>
								<td class="jobDescription" valign="top"><%
										
											l = 0;
											string testString = "";
											
											while(l < idDataSet.Tables[0].Rows.Count)
											{
												if (l > 0) testString = testString + "<br>";
												if (idDataSet.Tables[0].Rows[l].ItemArray.GetValue(6).ToString() == "1")
												{
													testString = testString + "Ja";
												}
												
												l++;
											}
											
											Response.Write(testString);
										
										%></td>
								<td class="jobDescription" valign="top"><%
										
											l = 0;
											string obdString = "";
											
											while(l < idDataSet.Tables[0].Rows.Count)
											{
												if (l > 0) obdString = obdString + "<br>";
												if (idDataSet.Tables[0].Rows[l].ItemArray.GetValue(7).ToString() == "1")
												{
													obdString = obdString + "Ja";
												}
												
												l++;
											}
											
											Response.Write(obdString);
										
										%></td>
								<td class="jobDescription" valign="top" align="center">&nbsp;</td>
							</tr>
							<%
								
								
									i++;
								}
							
							
							%>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
