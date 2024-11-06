<%@ Page language="c#" Codebehind="reportViewer.aspx.cs" AutoEventWireup="false" Inherits="WebAdmin.reportViewer" %>
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
			document.thisform.submit();
	}

	
	</script>

	<body MS_POSITIONING="GridLayout" topmargin="10" onload="setTimeout('document.thisform.submit()', 90000)">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" method="post" runat="server">
			<input type="hidden" name="command" value="update">
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame">
				<tr>
					<td height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td class="activityName">Rapporter</td>
				</tr>
				<tr>
					<td class="">Rapport: <select name="reportName" class="Textfield" onchange="document.thisform.submit();">
						<%
							int i = 0;
							while (i < reportDataSet.Tables[0].Rows.Count)
							{
								Navipro.SantaMonica.Common.DataReport dataReport = new Navipro.SantaMonica.Common.DataReport(reportDataSet.Tables[0].Rows[i]);
								
								%><option value="<%= dataReport.reportName %>" <% if (Request["reportName"] == dataReport.reportName) Response.Write("selected"); %>><%= dataReport.description %></option><%	
								
								
								i++;
							}
						%>
					</select>&nbsp;Datum:&nbsp;<% WebAdmin.HTMLHelper.createDatePicker("fromDate", fromDate); %>&nbsp;--&nbsp;<% WebAdmin.HTMLHelper.createDatePicker("toDate", toDate); %></td>
				</tr>
				<tr>
					<td>
						<%= currentReport.renderReport() %>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>