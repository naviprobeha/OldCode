<%@ Page language="c#" Codebehind="containers_service.aspx.cs" AutoEventWireup="false" Inherits="Navipro.SantaMonica.WebAdmin.containers_service" %>
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
	
	function performService(containerNo, entryNo)
	{
	
		if (confirm("Är du säker på att du vill åtgärdsrapportera container "+containerNo+"?"))
		{
			document.thisform.command.value = "report";
			document.thisform.containerNo.value = containerNo;
			document.thisform.entryNo.value = entryNo;
			document.thisform.submit();	
		}	
	
	}

	function performDestruction(containerNo)
	{
		if (confirm("Är du säker på att du vill skrota container "+containerNo+"?"))
		{
			document.thisform.command.value = "destruct";
			document.thisform.containerNo.value = containerNo;
			document.thisform.submit();	
		}	
	
	}

	function undo(containerNo)
	{
		if (confirm("Är du säker på att du vill ångra container "+containerNo+"?"))
		{
			document.thisform.command.value = "undo";
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
			<input type="hidden" name="entryNo" value="">
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame">
				<tr>
					<td height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td class="activityName">Servicerapporterade containers</td>
				</tr>
				<tr>
					<td>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Containernr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Typ</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Rapporterad av</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Datum och klockslag</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Nuvarande position typ</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Nuvarande position</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center" valign="bottom">
									Visa</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center" valign="bottom">
									Åtgärdat</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center" valign="bottom">
									Skrota</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center" valign="bottom">
									Ångra</th>

							</tr>
							<%
								int i = 0;
								while (i < containerServiceDataSet.Tables[0].Rows.Count)
								{		
									Navipro.SantaMonica.Common.ContainerEntry containerEntry = new Navipro.SantaMonica.Common.ContainerEntry(containerServiceDataSet.Tables[0].Rows[i]);
									Navipro.SantaMonica.Common.Container container = containerEntry.getContainer(database);
									
									string bgStyle = "";
									
									if ((i % 2) > 0)
									{
										bgStyle = " style=\"background-color: #e0e0e0;\"";
									}
									
									string containerTypeCode = "";
									string locationType = "";
									string locationName = "";
									if (container != null)
									{
										containerTypeCode = container.containerTypeCode;
										locationType = container.getLocationType();
										locationName = container.getLocationName(database);
									}
													
									%>
									<tr>
										<td class="jobDescription" <%= bgStyle %> valign="top"><%= containerEntry.containerNo %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top"><%= containerTypeCode %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top"><%= containerEntry.sourceCode %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top"><%= containerEntry.entryDateTime.ToString("yyyy-MM-dd HH:mm:ss") %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top"><%= locationType %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top"><%= locationName %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top" align="center"><% if (container != null) { %><a href="containers_view.aspx?containerNo=<%= containerEntry.containerNo %>"><img src="images/button_assist.gif" border="0" width="12" height="13"><% } %></a></td>
										<td class="jobDescription" <%= bgStyle %> valign="top" align="left"><select name="serviceType_<%= containerEntry.entryNo %>" class="Textfield">
						<%
							int j = 0;
							while (j < activeServiceTypes.Tables[0].Rows.Count)
							{
								
								%>
								<option value="<%= activeServiceTypes.Tables[0].Rows[j].ItemArray.GetValue(0).ToString() %>" ><%= activeServiceTypes.Tables[0].Rows[j].ItemArray.GetValue(1).ToString() %></option>													
								<%
							
								j++;
							}
						
						
						%>	
						</select><br><select name="serviceType2_<%= containerEntry.entryNo %>" class="Textfield">
						
						<option value=""></option><%
							int k = 0;
							while (k < activeServiceTypes.Tables[0].Rows.Count)
							{
								
								%>
								<option value="<%= activeServiceTypes.Tables[0].Rows[k].ItemArray.GetValue(0).ToString() %>" ><%= activeServiceTypes.Tables[0].Rows[k].ItemArray.GetValue(1).ToString() %></option>													
								<%
							
								k++;
							}
						
						
						%>	
						</select>&nbsp;<a href="javascript:performService('<%= containerEntry.containerNo %>', '<%= containerEntry.entryNo %>')"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
										<td class="jobDescription" <%= bgStyle %> valign="top" align="center"><a href="javascript:performDestruction('<%= containerEntry.containerNo %>')"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
										<td class="jobDescription" <%= bgStyle %> valign="top" align="center"><a href="javascript:undo('<%= containerEntry.containerNo %>')"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
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