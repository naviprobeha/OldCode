<%@ Page language="c#" Codebehind="consumerCapacity.aspx.cs" AutoEventWireup="false" Inherits="WebAdmin.consumerCapacity" %>
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
		document.thisform.action = "consumerCapacity.aspx";
		document.thisform.command.value = "changeDate";
		document.thisform.submit();
	
	}

	
	function changeWeek()
	{
		document.thisform.action = "consumerCapacity.aspx";
		document.thisform.command.value = "changeDate";
		document.thisform.submit();
	
	}

	function modify()
	{
		document.location.href = "consumerCapacity_modify.aspx?firstDay=<%= firstDay.ToString("yyyy-MM-dd") %>&consumerNo=<%= currentConsumer.no %>";
	
	}

	function generate()
	{
		document.location.href = "consumerCapacity_generate.aspx?consumerNo=<%= currentConsumer.no %>";
	
	}


	function gotoToday()
	{
		document.thisform.action = "consumerCapacity.aspx";	
		document.thisform.currentYear.value = "<%= DateTime.Now.Year %>";
		document.thisform.currentWeek.value = "<%= Navipro.SantaMonica.Common.CalendarHelper.GetWeek(DateTime.Today) %>";
	
		document.thisform.command.value = "changeDate";
		document.thisform.submit();
	
	}
	
	</script>
	
	<body MS_POSITIONING="GridLayout" topmargin="10" onload="setTimeout('changeWeek()', 600000)">
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
					<td class="activityName">Kapacitet Värmeverk</td>
				</tr>
				<tr>
					<td class="" width="95%">År:&nbsp;<% WebAdmin.HTMLHelper.createYearPicker("currentYear", currentYear); %>&nbsp;Vecka:&nbsp;<% WebAdmin.HTMLHelper.createWeekPicker("currentWeek", currentYear, currentWeek); %>&nbsp;
					Värmeverk:&nbsp;<select name="consumerNo" class="Textfield" onchange="changeWeek()">
						<%
							int j = 0;
							while (j < activeConsumers.Tables[0].Rows.Count)
							{
								
								%>
								<option value="<%= activeConsumers.Tables[0].Rows[j].ItemArray.GetValue(0).ToString() %>" <% if (currentConsumer.no == activeConsumers.Tables[0].Rows[j].ItemArray.GetValue(0).ToString()) Response.Write("selected"); %>><%= activeConsumers.Tables[0].Rows[j].ItemArray.GetValue(1).ToString() %></option>													
								<%
							
								j++;
							}
						
						
						%>									
					</select>&nbsp;<input type="button" value="Idag" onclick="gotoToday()" class="Button">&nbsp;<input type="button" value="Ändra" onclick="modify()" class="Button">&nbsp;<input type="button" value="Grundplanering" onclick="generate()" class="Button"></td>
				</tr>
				<tr>
					<td>
						<table cellspacing="1" cellpadding="2" border="0" width="100%">
						<tr>
							<%
							
							
								int weekDay = 0;
								while (weekDay < 7)
								{
							
									%>		
									<td>
										<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
											<tr>
												<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" colspan="3">
													<%= firstDay.AddDays(weekDay).ToString("yyyy-MM-dd")+" "+Navipro.SantaMonica.Common.CalendarHelper.GetDayOfWeek(firstDay.AddDays(weekDay)) %></th>
											</tr>
											<tr>
												<td class="jobDescription" style="font-weight: bold;">Timme</td>
												<td class="jobDescription" style="font-weight: bold;" align="right">Planerad</td>
												<td class="jobDescription" style="font-weight: bold;" align="right">Verklig</td>
											</tr>
											<%
												int hour = 0;
												while (hour < 24)
												{
													System.DateTime hourDateTime = new System.DateTime(firstDay.AddDays(weekDay).Year, firstDay.AddDays(weekDay).Month, firstDay.AddDays(weekDay).Day, hour, 0, 0);
													
													Navipro.SantaMonica.Common.ConsumerCapacity consumerCapacity = (Navipro.SantaMonica.Common.ConsumerCapacity)capacityTable[hourDateTime];
													
													string plannedCapacity = "";
													string actualCapacity = "";
													
													if (consumerCapacity != null)
													{
														if (consumerCapacity.plannedCapacity > 0) plannedCapacity = consumerCapacity.plannedCapacity.ToString()+" ton/h";
														if (consumerCapacity.actualCapacity > 0) actualCapacity = consumerCapacity.actualCapacity.ToString()+" ton/h";
													
														
													}
													
													%>
													<tr>
														<td class="jobDescription"><%= hourDateTime.ToString("HH:mm") %></td>
														<td class="jobDescription" align="right">&nbsp;<%= plannedCapacity %></td>
														<td class="jobDescription" align="right">&nbsp;<%= actualCapacity %></td>
													</tr>
													<%
													
													hour++;
												}
											
											
											
											%>
										</table>					
									</td>
									<%
									
									weekDay++;
									
								}
							%>
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
