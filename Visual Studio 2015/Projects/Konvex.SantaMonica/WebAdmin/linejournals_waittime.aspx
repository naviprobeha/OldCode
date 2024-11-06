<%@ Page language="c#" Codebehind="linejournals_waittime.aspx.cs" AutoEventWireup="false" Inherits="WebAdmin.lineJournals_waittime" %>
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
		document.thisform.action = "linejournals_waittime.aspx";
		document.thisform.command.value = "changeDate";
		document.thisform.submit();
	
	}

	
	function changeWeek()
	{
		document.thisform.action = "linejournals_waittime.aspx";
		document.thisform.command.value = "changeDate";
		document.thisform.submit();
	
	}


	function gotoToday()
	{
		document.thisform.action = "linejournals_waittime.aspx";	
		document.thisform.currentYear.value = "<%= DateTime.Now.Year %>";
		document.thisform.currentWeek.value = "<%= Navipro.SantaMonica.Common.CalendarHelper.GetWeek(DateTime.Today) %>";
	
		document.thisform.command.value = "changeDate";
		document.thisform.submit();
	
	}
	

	
	function confirmJournal(lineJournalEntryNo)
	{
		if (confirm("Du kommer att attestera rutten. Är du säker?"))
		{
			document.thisform.command.value = "confirmWaitTime";
			document.thisform.lineJournalEntryNo.value = lineJournalEntryNo;
			document.thisform.submit();
		
		}
	
	}	
	</script>
	
	<body MS_POSITIONING="GridLayout" topmargin="10" onload="setTimeout('document.thisform.submit()', 60000)">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" method="post" runat="server">
		<input type="hidden" name="command" value="">
		<input type="hidden" name="lineJournalEntryNo" value="">		
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame">
				<tr>
					<td height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td class="activityName">Rutter för attest, <%= currentFactory.name %></td>
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
									Avgående</th>									
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Ankommande</th>									
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Ankommer</th>									
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Status</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right">
									Väntetid</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">
									Attestera</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">
									Visa</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									&nbsp;</th>
							</tr>
							<%
								int i = 0;
								while (i < reportedLineJournals.Tables[0].Rows.Count)
								{
									Navipro.SantaMonica.Common.LineJournal lineJournal = new Navipro.SantaMonica.Common.LineJournal(reportedLineJournals.Tables[0].Rows[i]);
									
									string statusText = lineJournal.getStatusText(database);
									string statusIcon = lineJournal.getStatusIcon();
								
									string bgStyle = "";
									
									if ((i % 2) > 0)
									{
										bgStyle = "background-color: #e0e0e0;";
									}
									
									string arrivalTime = lineJournal.arrivalDateTime.ToString("yyyy-MM-dd HH:mm");
									if (lineJournal.arrivalDateTime.ToString("HH:mm") == "00:00") arrivalTime = "";
								
									%>
									<tr>
										<td class="jobDescription" rowspan="2" valign="top" align="center" style="<%= bgStyle %>"><img src="images/<%= statusIcon %>" border="0" alt="<%= statusText %>"></td>
										<td class="jobDescription" rowspan="2" valign="top" style="<%= bgStyle %>"><b><%= lineJournal.entryNo %></b></td>
										<td class="jobDescription" rowspan="2" valign="top" style="<%= bgStyle %>"><b><%= lineJournal.organizationNo %></b></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><b><%= lineJournal.shipDate.ToString("yyyy-MM-dd") %></b></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><b><%= lineJournal.agentCode %></b></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><b><%= lineJournal.departureFactoryCode %></b></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>"><b><%= lineJournal.arrivalFactoryCode %></b></td>
										<td class="jobDescription" valign="top" style="<%= bgStyle %>" nowrap><b><%= arrivalTime %></b></td>
										<td class="jobDescription" rowspan="2" valign="top" style="<%= bgStyle %>"><b><%= statusText %></b></td>
										<td class="jobDescription" rowspan="2" valign="top" align="right" style="<%= bgStyle %>"><b><%= lineJournal.dropWaitTime %> min</b></td>
										<td class="jobDescription" rowspan="2" valign="top" align="center" style="<%= bgStyle %>"><a href="javascript:confirmJournal('<%= lineJournal.entryNo.ToString() %>')"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
										<td class="jobDescription" rowspan="2" valign="top" align="center" style="<%= bgStyle %>"><a href="factoryOperation_view.aspx?parentPage=linejournals&lineJournalNo=<%= lineJournal.entryNo.ToString() %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
										<td class="jobDescription" rowspan="2" valign="top" align="center" style="<%= bgStyle %>"><img src="images/<%= statusIcon %>" border="0" alt="<%= statusText %>"></td>
									</tr>
									<%
																	
										System.Data.DataSet containerDataSet = lineJournal.getContainers(database);
									%>
									<tr>
										<td class="jobDescription" colspan="2" valign="top" style="<%= bgStyle %>">
											<table cellspacing="0" cellpadding="0" width="100%" border="0">
											<%
										
											int k = 0;
											while (k < containerDataSet.Tables[0].Rows.Count)
											{
												%><tr>
												<td class="jobDescription" style="<%= bgStyle %>" height="15"><%= containerDataSet.Tables[0].Rows[k].ItemArray.GetValue(9).ToString() %></td>
												</tr><%
											
												k++;											
											}
											
										%></table>
										</td>
										<td class="jobDescription" colspan="3" valign="top" style="<%= bgStyle %>"><table cellspacing="0" cellpadding="0" width="100%" border="0"><%
										
											k = 0;
											while (k < containerDataSet.Tables[0].Rows.Count)
											{
												Navipro.SantaMonica.Common.LineOrderContainer lineOrderContainer = new Navipro.SantaMonica.Common.LineOrderContainer(containerDataSet.Tables[0].Rows[k]);
												Navipro.SantaMonica.Common.Category category = lineOrderContainer.getCategory(database);
												string categoryDesc = "";
												if (category != null) categoryDesc = category.description;
												int testQuantity = lineOrderContainer.countTestings(database);
												string testing = "";
												if (testQuantity > 0) testing = "Provt: "+testQuantity;

												string serviceStyle = "";
												Navipro.SantaMonica.Common.ContainerEntries containerEntries = new Navipro.SantaMonica.Common.ContainerEntries();
												System.Data.DataSet serviceDataSet = containerEntries.getServiceDataSet(database, lineOrderContainer.containerNo);
												if (serviceDataSet.Tables[0].Rows.Count > 0) serviceStyle = "color: red; font-weight: bold;";


												%><tr>
													<td class="jobDescription" height="15" style="<%= bgStyle+serviceStyle %>"><%= lineOrderContainer.containerNo %></td>
													<td class="jobDescription" style="<%= bgStyle+serviceStyle %>"><%= lineOrderContainer.categoryCode+", "+categoryDesc %></td>
													<td class="jobDescription" style="<%= bgStyle %>" width="70"><%= lineOrderContainer.weight.ToString("0")+" kg" %></td>
													<td class="jobDescription" style="<%= bgStyle %>" width="70"><%= testing %></td>
												</tr><%												
												
											
												k++;											
											}
										
										%></table></td>
									</tr>
									<%
								
								
									i++;
								}
							
								if (i == 0)
								{
									%>
									<tr>
										<td class="jobDescription" colspan="13">Inga rutter registrerade för attest.</td>
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
