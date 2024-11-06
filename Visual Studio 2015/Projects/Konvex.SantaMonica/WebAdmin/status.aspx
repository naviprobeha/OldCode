<%@ Page language="c#" Codebehind="status.aspx.cs" AutoEventWireup="false" Inherits="WebAdmin.status" %>
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
		document.thisform.action = "status.aspx";
		document.thisform.command.value = "changeDate";
		document.thisform.submit();
	
	}

	
	function changeWeek()
	{
		document.thisform.action = "status.aspx";
		document.thisform.command.value = "changeDate";
		document.thisform.submit();
	
	}



	function gotoToday()
	{
		document.thisform.action = "status.aspx";	
		document.thisform.currentYear.value = "<%= DateTime.Now.Year %>";
		document.thisform.currentWeek.value = "<%= Navipro.SantaMonica.Common.CalendarHelper.GetWeek(DateTime.Today) %>";
	
		document.thisform.command.value = "changeDate";
		document.thisform.submit();
	
	}
	
	</script>
	
	<body MS_POSITIONING="GridLayout" topmargin="10" onload="setTimeout('changeWeek()', 60000)">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" action="consumerCapacity.aspx" method="post" runat="server">
		<input type="hidden" name="command" value="">
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame">
				<tr>
					<td height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td class="activityName">Status</td>
				</tr>
				<tr>
					<td class="" width="95%">År:&nbsp;<% WebAdmin.HTMLHelper.createYearPicker("currentYear", currentYear); %>&nbsp;Vecka:&nbsp;<% WebAdmin.HTMLHelper.createWeekPicker("currentWeek", currentYear, currentWeek); %>
					&nbsp;<input type="button" value="Idag" onclick="gotoToday()" class="Button"></td>
				</tr>
				<tr>
					<td>
						<table cellspacing="1" cellpadding="2" border="0" width="100%">
						<tr>
							<td valign="top">
								<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
									<tr>
										<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">Fabrik / Transportör</th>
										<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">Ej avslutade rutter</th>
										<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">Ej lastade order</th>
										<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">Ej lastade containers</th>
										<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">Ej lossade containers</th>
										<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">Ej komplett invägda containers</th>
										<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">Containers på golvet</th>
									</tr>
									<%
									
										int i = 0;
										while  (i < activeFactoryDataSet.Tables[0].Rows.Count)
										{
											%>
											<tr>
												<td class="jobDescription"><%= activeFactoryDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString()+", "+activeFactoryDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString() %></td>
												<td class="jobDescription" valign="top" align="center" style="color: red">&nbsp;</td>
												<td class="jobDescription" valign="top" align="center" style="color: red">&nbsp;</td>
												<td class="jobDescription" valign="top" align="center" style="color: red">&nbsp;</td>
												<td class="jobDescription" valign="top" align="center" style="color: red">&nbsp;</td>
												<td class="jobDescription" valign="top" align="center" style="color: red"><%= getNonScaledContainers(activeFactoryDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString()) %></td>
												<td class="jobDescription" valign="top" align="center" style="color: red"><%= getUnScaledContainers(activeFactoryDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString()) %></td>
											</tr>
											<%
											
											i++;
										}

										i = 0;
										while  (i < organizationDataSet.Tables[0].Rows.Count)
										{
											%>
											<tr>
												<td class="jobDescription"><%= organizationDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %>&nbsp;</td>
												<td class="jobDescription" valign="top" align="center"><%= getNonFinishedJournals(organizationDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()) %></td>
												<td class="jobDescription" valign="top" align="center" style="color: red"><%= getNonLoadedOrders(organizationDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()) %></td>
												<td class="jobDescription" valign="top" align="center" style="color: red"><%= getNonLoadedContainers(organizationDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()) %></td>
												<td class="jobDescription" valign="top" align="center" style="color: red"><%= getNonUnLoadedContainers(organizationDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()) %></td>
												<td class="jobDescription" valign="top" align="center" style="color: red">&nbsp;</td>
												<td class="jobDescription" valign="top" align="center" style="color: red">&nbsp;</td>
											</tr>
											<%
											
											i++;
										}
																								
									
									%>
								</table>
							</td>
						</tr>
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

