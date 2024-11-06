<%@ Page language="c#" Codebehind="factoryCapacity_modify.aspx.cs" AutoEventWireup="false" Inherits="WebAdmin.factoryCapacity_modify" %>
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

	function setDefaultValues()
	{
		<%
			int i = 0;
			while (i < 7)
			{
				int h = 0;
				while (h < 24)
				{
					%>
					document.thisform.capacity_<%= i+"_"+h %>.value = document.thisform.defaultValue.value;
					<%						
					h++;
				}
				
				i++;				
			}
		
		
		%>	
	}
	
	function save()
	{
		document.thisform.command.value = "save";
		document.thisform.submit();
	
	}
	
	function goBack()
	{
		document.location.href='factoryCapacity.aspx?factoryNo=<%= currentFactory.no %>&firstDay=<%= firstDay.ToString("yyyy-MM-dd") %>';		
	}

	</script>
	
	<body MS_POSITIONING="GridLayout" topmargin="10">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" action="factoryCapacity.aspx" method="post" runat="server">
		<input type="hidden" name="factoryNo" value="<%= currentFactory.no %>">
		<input type="hidden" name="firstDay" value="<%= firstDay.ToString("yyyy-MM-dd") %>">		
		<input type="hidden" name="command" value="">
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame">
				<tr>
					<td height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td class="activityName">Kapacitet Fabrik</td>
				</tr>
				<tr>
					<td class="" width="95%">År:&nbsp;<b><%= currentYear %></b>&nbsp;Vecka:&nbsp;<b><%= currentWeek %></b>&nbsp;Fabrik:&nbsp;<b><%= currentFactory.name %></b></td>
				</tr>
				<tr>
					<td class="" width="95%">Förvalt värde: <input type="text" name="defaultValue" class="Textfield" size="5">&nbsp;<input type="button" value="Fyll i" class="Button" onclick="setDefaultValues()">&nbsp;<input type="button" value="Spara" onclick="save()" class="Button">&nbsp;<input type="button" value="Avbryt" onclick="goBack()" class="Button"></td>
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
													
													Navipro.SantaMonica.Common.FactoryCapacity factoryCapacity = (Navipro.SantaMonica.Common.FactoryCapacity)capacityTable[hourDateTime];
													
													string plannedCapacity = "";
													string actualCapacity = "";
													
													if (factoryCapacity != null)
													{
														float actCapacity = factoryCapacity.calcActualCapacity(database);
														if (factoryCapacity.plannedCapacity > 0) plannedCapacity = factoryCapacity.plannedCapacity.ToString();
														if (actCapacity > 0) actualCapacity = actCapacity.ToString()+" st/h";
													
														
													}
													
													%>
													<tr>
														<td class="jobDescription"><%= hourDateTime.ToString("HH:mm") %></td>
														<td class="jobDescription" align="right"><input type="text" name="capacity_<%= weekDay+"_"+hour %>" value="<%= plannedCapacity %>" class="Textfield" size="6" maxlength="5"></td>
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
