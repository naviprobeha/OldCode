<%@ Page language="c#" Codebehind="containers_service_log.aspx.cs" AutoEventWireup="false" Inherits="Navipro.SantaMonica.WebAdmin.containers_service_log" %>
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
	
	function performService(containerNo)
	{
		if (confirm("Är du säker på att du vill åtgärdsrapportera container "+containerNo+"?"))
		{
			document.thisform.command.value = "report";
			document.thisform.containerNo.value = containerNo;
			document.thisform.submit();	
		}	
	
	}
	
	</script>

	<body MS_POSITIONING="GridLayout" topmargin="10">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" method="post" runat="server">
			<input type="hidden" name="command" value="">
			<input type="hidden" name="containerNo" value="">
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame">
				<tr>
					<td height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td class="activityName">Containerservice logg</td>
				</tr>
				<tr>
					<td>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Datum och klockslag</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Containernr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Typ</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Service Typ</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Service Typ2</th>			
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Utförd av</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center" valign="bottom">
									Visa</th>
							</tr>
							<%
								int i = 0;
								while (i < containerServiceDataSet.Tables[0].Rows.Count)
								{		
									Navipro.SantaMonica.Common.ContainerServiceEntry containerServiceEntry = new Navipro.SantaMonica.Common.ContainerServiceEntry(containerServiceDataSet.Tables[0].Rows[i]);
									Navipro.SantaMonica.Common.Container container = containerServiceEntry.getContainer(database);
									
									string containerTypeCode = "";
									if (container != null)
									{
										containerTypeCode = container.containerTypeCode;
									}
									
									string bgStyle = "";
									
									if ((i % 2) > 0)
									{
										bgStyle = " style=\"background-color: #e0e0e0;\"";
									}
									
									
													
									%>
									<tr>
										<td class="jobDescription" <%= bgStyle %> valign="top"><%= containerServiceEntry.entryDateTime.ToString("yyyy-MM-dd HH:mm:ss") %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top"><%= containerServiceEntry.containerNo %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top"><%= containerTypeCode %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top"><%= containerServiceEntry.serviceType %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top"><%= containerServiceEntry.serviceType2 %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top"><%= containerServiceEntry.userId %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top" align="center"><a href="containers_view.aspx?containerNo=<%= containerServiceEntry.containerNo %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
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