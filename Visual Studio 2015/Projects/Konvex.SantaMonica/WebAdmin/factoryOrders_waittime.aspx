<%@ Page language="c#" Codebehind="factoryOrders_waittime.aspx.cs" AutoEventWireup="false" Inherits="WebAdmin.factoryOrders_waittime" %>
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
	

	function changeYear()
	{
		document.thisform.action = "factoryOrders_waittime.aspx";
		document.thisform.command.value = "changeDate";
		document.thisform.submit();
	
	}

	
	function changeWeek()
	{
		document.thisform.action = "factoryOrders_waittime.aspx";
		document.thisform.command.value = "changeDate";
		document.thisform.submit();
	
	}


	function gotoToday()
	{
		document.thisform.action = "factoryOrders_waittime.aspx";	
		document.thisform.currentYear.value = "<%= DateTime.Now.Year %>";
		document.thisform.currentWeek.value = "<%= Navipro.SantaMonica.Common.CalendarHelper.GetWeek(DateTime.Today) %>";
	
		document.thisform.command.value = "changeDate";
		document.thisform.submit();
	
	}
	

	
	function confirmOrder(factoryOrderNo)
	{
		if (confirm("Du kommer att attestera fabriksordern. Är du säker?"))
		{
			document.thisform.command.value = "confirmWaitTime";
			document.thisform.factoryOrderNo.value = factoryOrderNo;
			document.thisform.submit();
		
		}
	
	}	
	</script>
	
	<body MS_POSITIONING="GridLayout" topmargin="10" onload="setTimeout('document.thisform.submit()', 60000)">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" method="post" runat="server">
		<input type="hidden" name="command" value="">
		<input type="hidden" name="factoryOrderNo" value="">		
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame">
				<tr>
					<td height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td class="activityName">Fabriksorder för attest, <%= currentFactory.name %></td>
				</tr>
				<tr>
					<td class="" width="95%">År:&nbsp;<% WebAdmin.HTMLHelper.createYearPicker("currentYear", currentYear); %>&nbsp;Vecka:&nbsp;<% WebAdmin.HTMLHelper.createWeekPicker("currentWeek", currentYear, currentWeek); %>&nbsp;Fabrik:&nbsp;<select name="factory" class="Textfield" onchange="changeWeek()">
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
					</select></td>
				</tr>
				<tr>
					<td>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									&nbsp;</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Nr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Transportör</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Transportdatum</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Bil</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Fabrik</th>									
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Värmeverk</th>									
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Status</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right">
									Väntetid</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Orsak</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">
									Attestera</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">
									Visa</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									&nbsp;</th>
							</tr>
							<%
								int i = 0;
								while (i < factoryOrderDataSet.Tables[0].Rows.Count)
								{
									Navipro.SantaMonica.Common.FactoryOrder factoryOrder = new Navipro.SantaMonica.Common.FactoryOrder(factoryOrderDataSet.Tables[0].Rows[i]);
									
									string statusText = factoryOrder.getStatusText();
									string statusIcon = factoryOrder.getStatusIcon();
								
									string bgStyle = "";
									
									if ((i % 2) > 0)
									{
										bgStyle = "background-color: #e0e0e0;";
									}
									
									string arrivalTime = factoryOrder.arrivalDateTime.ToString("yyyy-MM-dd HH:mm");
									if (factoryOrder.arrivalDateTime.ToString("HH:mm") == "00:00") arrivalTime = "";
								
									%>
									<tr>
										<td class="jobDescription" valign="top" align="center" style="<%= bgStyle %>"><img src="images/<%= statusIcon %>" border="0" alt="<%= statusText %>"></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><%= factoryOrder.entryNo %></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><%= factoryOrder.organizationNo %></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><%= factoryOrder.shipDate.ToString("yyyy-MM-dd") %></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><%= factoryOrder.agentCode %></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><%= factoryOrder.factoryName %></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><%= factoryOrder.consumerName %></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><%= statusText %></td>
										<td class="jobDescription" valign="top" align="right" style="<%= bgStyle %>"><%= factoryOrder.loadWaitDuration %> min</td>
										<td class="jobDescription" valign="top" align="left" style="<%= bgStyle %>"><%= factoryOrder.loadReasonText %> min</td>
										<td class="jobDescription" valign="top" align="center" style="<%= bgStyle %>"><input type="text" name="approvedBy" class="Textfield" size="5">&nbsp;<a href="javascript:confirmOrder('<%= factoryOrder.entryNo.ToString() %>')"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
										<td class="jobDescription" valign="top" align="center" style="<%= bgStyle %>"><a href="factoryOrders_view.aspx?factoryOrderNo=<%= factoryOrder.entryNo.ToString() %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
										<td class="jobDescription" valign="top" align="center" style="<%= bgStyle %>"><img src="images/<%= statusIcon %>" border="0" alt="<%= statusText %>"></td>
									</tr>
									<%
								
								
									i++;
								}
							
								if (i == 0)
								{
									%>
									<tr>
										<td class="jobDescription" colspan="13">Inga fabriksorder registrerade för attest.</td>
									</tr>
									<%
								}
							
							%>
						</table>						
					</td>
				</tr>
				<tr>
					<td align="right">&nbsp;</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
