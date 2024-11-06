<%@ Page language="c#" Codebehind="samples.aspx.cs" AutoEventWireup="false" Inherits="Navipro.SantaMonica.WebAdmin.samples" %>
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
	
	function changeShipDate()
	{
		//document.thisform.command.value = "changeShipDate";
		//document.thisform.submit();
	
	}

	function submitSearch()
	{
		document.thisform.command.value = "changeShipDate";
		document.thisform.submit();
	
	}
	
	function sendSample(entryNo)
	{
		document.thisform.command.value = "sendShipment";
		document.thisform.idEntryNo.value = entryNo;
		document.thisform.submit();		
	}

	function revokeSample(entryNo)
	{
		document.thisform.command.value = "revokeShipment";
		document.thisform.idEntryNo.value = entryNo;
		document.thisform.submit();		
	}
		
	</script>
	
	
	<body MS_POSITIONING="GridLayout" topmargin="10">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" method="post" runat="server">
		<input type="hidden" name="command">
		<input type="hidden" name="idEntryNo">
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame">
				<tr>
					<td height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td class="activityName">Provtagningar</td>
				</tr>
				<tr>
					<td class="">Datumintervall:&nbsp;<% WebAdmin.HTMLHelper.createDatePicker("startDate", startDate); %>&nbsp;-&nbsp;<% WebAdmin.HTMLHelper.createDatePicker("endDate", endDate); %>&nbsp;Fabrik:&nbsp;<select name="factory" class="Textfield">
						<%
							int j = 0;
							while (j < activeFactories.Tables[0].Rows.Count)
							{
								
								%>
								<option value="<%= activeFactories.Tables[0].Rows[j].ItemArray.GetValue(1).ToString() %>" <% if (currentFactory.no == activeFactories.Tables[0].Rows[j].ItemArray.GetValue(1).ToString()) Response.Write("selected"); %>><%= activeFactories.Tables[0].Rows[j].ItemArray.GetValue(2).ToString() %></option>													
								<%
							
								j++;
							}
						
						
						%>									
					</select>&nbsp;ID-nr:&nbsp;<input type="text" name="unitId" size="20" maxlength="20" class="Textfield">&nbsp;<input type="button" value="Sök" class="Button" onclick="submitSearch()"></td>
				</tr>
				<tr>
					<td>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" width=80">
									Följesedelsnr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" width=80">
									Datum</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" width=80">
									Artikelnr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Beskrivning</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									ID-nr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Ommärkningsnr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center" width="150">
									Visa</th>
							</tr>
							<%
							
								int i = 0;
								while (i < shipmentLineIdDataSet.Tables[0].Rows.Count)
								{	
								
									string bgStyle = "";
										
									if ((i % 2) > 0)
									{
										bgStyle = " style=\"background-color: #e0e0e0;\"";
									}
									
	
									string sendLink = "";	
									string comments = "";
									Navipro.SantaMonica.Common.ShipmentSending shipmentSending = shipmentSendings.getId(database, shipmentLineIdDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString());
									if (shipmentSending != null)
									{
										sendLink = sendLink + "<a href=\"javascript:revokeSample('"+shipmentLineIdDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()+"')\">Ångra</a>";
										comments = shipmentSending.comments;
									}
									else
									{
										sendLink = sendLink + "<a href=\"javascript:sendSample('"+shipmentLineIdDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()+"')\">Överför</a>";
									}
																							
									%>
									<tr>
										<td rowspan="2" class="jobDescription" <%= bgStyle %> valign="top"><%= shipmentLineIdDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %></td>
										<td rowspan="2" class="jobDescription" <%= bgStyle %> valign="top"><%= DateTime.Parse(shipmentLineIdDataSet.Tables[0].Rows[i].ItemArray.GetValue(10).ToString()).ToString("yyyy-MM-dd") %></td>
										<td rowspan="2" class="jobDescription" <%= bgStyle %> valign="top"><%= shipmentLineIdDataSet.Tables[0].Rows[i].ItemArray.GetValue(8).ToString() %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top"><%= shipmentLineIdDataSet.Tables[0].Rows[i].ItemArray.GetValue(9).ToString() %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top"><%= shipmentLineIdDataSet.Tables[0].Rows[i].ItemArray.GetValue(3).ToString() %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top"><%= shipmentLineIdDataSet.Tables[0].Rows[i].ItemArray.GetValue(5).ToString() %></td>
										<td rowspan="2" class="jobDescription" <%= bgStyle %> valign="top" align="center"><a href="samples_view.aspx?idEntryNo=<%= shipmentLineIdDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>"><img src="images/button_assist.gif" alt="Visa" border="0" width="12" height="13"></a></td>
									</tr>
									<tr>
										<td colspan="3" class="jobDescription" <%= bgStyle %> valign="top"><%= comments %></td>
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
