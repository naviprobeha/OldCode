<%@ Page language="c#" Codebehind="items_view.aspx.cs" AutoEventWireup="false" Inherits="Navipro.SantaMonica.WebAdmin.items_view" %>
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
	
	function search()
	{
		document.thisform.command.value = "searchCustomer";
		document.thisform.submit();
	}
	
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" topmargin="10">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" method="post" runat="server">
			<input type="hidden" name="command">
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame">
				<tr>
					<td height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td class="activityName">Artikel
						<%= currentItem.no %>
					</td>
				</tr>
				<tr>
					<td>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Artikelnr</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><%= currentItem.no %></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Beskrivning</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><%= currentItem.description %></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Enhet</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><%= currentItem.unitOfMeasure %></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top" colspan="2">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">A-pris</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><%= decimal.Round(currentItem.unitPrice, 2) %></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<br>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Pristyp</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Kod</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Startdatum</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Slutdatum</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Minsta antal</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									A-pris</th>
							</tr>
							<%
								int i = 0;
								while (i < itemPriceDataSet.Tables[0].Rows.Count)
								{	
									string bgStyle = "";
									
									if ((i % 2) > 0)
									{
										bgStyle = " style=\"background-color: #e0e0e0;\"";
									}
								
									string salesType = "";
									if (itemPriceDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() == "0") salesType = "Kund";
									if (itemPriceDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() == "1") salesType = "Kundprisgrupp";
									if (itemPriceDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() == "2") salesType = "Alla kunder";
									
									string unitPrice =	decimal.Round(decimal.Parse(itemPriceDataSet.Tables[0].Rows[i].ItemArray.GetValue(6).ToString()), 2)+" kr";
									
									string startingDate = itemPriceDataSet.Tables[0].Rows[i].ItemArray.GetValue(3).ToString().Substring(0,10);
									if (startingDate == "1753-01-01") startingDate = "";

									string endingDate = itemPriceDataSet.Tables[0].Rows[i].ItemArray.GetValue(5).ToString().Substring(0,10);
									if (endingDate == "1753-01-01") endingDate = "";
									
									%>
							<tr>
								<td class="jobDescription" valign="top" <%= bgStyle %>><%= salesType %></td>
								<td class="jobDescription" valign="top" <%= bgStyle %>><%= itemPriceDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString() %></td>
								<td class="jobDescription" valign="top" <%= bgStyle %>><%= startingDate %></td>
								<td class="jobDescription" valign="top" <%= bgStyle %>><%= endingDate %></td>
								<td class="jobDescription" valign="top" <%= bgStyle %>><%= itemPriceDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() %></td>
								<td class="jobDescription" valign="top" <%= bgStyle %>><%= unitPrice %></td>
							</tr>
							<%
										
									i++;
								}
							
							
							%>
						</table>
						<br>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Kundprisgrupp</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Startdatum</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Slutdatum</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Enhet</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Antal från</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Antal till</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Belopp</th>
							</tr>
							<%
								i = 0;
								while (i < itemPriceExtendedDataSet.Tables[0].Rows.Count)
								{	
								
									string bgStyle = "";
									
									if ((i % 2) > 0)
									{
										bgStyle = " style=\"background-color: #e0e0e0;\"";
									}
									
									string amount =	decimal.Round(decimal.Parse(itemPriceExtendedDataSet.Tables[0].Rows[i].ItemArray.GetValue(7).ToString()), 2)+" kr";
									
									string startingDate = itemPriceExtendedDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString().Substring(0,10);
									if (startingDate == "1753-01-01") startingDate = "";

									string endingDate = itemPriceExtendedDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString().Substring(0,10);
									if (endingDate == "1753-01-01") endingDate = "";
									
									%>
							<tr>
								<td class="jobDescription" valign="top" <%= bgStyle %>><%= itemPriceExtendedDataSet.Tables[0].Rows[i].ItemArray.GetValue(3).ToString() %></td>
								<td class="jobDescription" valign="top" <%= bgStyle %>><%= startingDate %></td>
								<td class="jobDescription" valign="top" <%= bgStyle %>><%= endingDate %></td>
								<td class="jobDescription" valign="top" <%= bgStyle %>><%= itemPriceExtendedDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() %></td>
								<td class="jobDescription" valign="top" <%= bgStyle %>><%= decimal.Truncate(decimal.Parse(itemPriceExtendedDataSet.Tables[0].Rows[i].ItemArray.GetValue(5).ToString())) %></td>
								<td class="jobDescription" valign="top" <%= bgStyle %>><%= decimal.Truncate(decimal.Parse(itemPriceExtendedDataSet.Tables[0].Rows[i].ItemArray.GetValue(6).ToString())) %></td>
								<td class="jobDescription" valign="top" <%= bgStyle %>><%= amount %></td>
							</tr>
							<%
										
									i++;
								}
							
							
							%>
						</table>
					</td>
				</tr>
				<tr>
					<td align="right"><input type="button" value="Tillbaka" onclick="document.location.href='items.aspx';" class="Button"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
