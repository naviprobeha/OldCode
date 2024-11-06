<%@ Page language="c#" Codebehind="scaleEntries_missing.aspx.cs" AutoEventWireup="false" Inherits="WebAdmin.scaleEntries_missing" %>
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
	}


	function search()
	{
		document.thisform.command.value = "search";
		document.thisform.submit();
	
	}
	
	</script>

	<body MS_POSITIONING="GridLayout" topmargin="10">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" method="post" runat="server">
			<input type="hidden" name="command" value="searchContainer">
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame">
				<tr>
					<td height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td class="activityName">Saknade vågtransaktioner</td>
				</tr>
				<tr>
					<td class="">Datum:&nbsp;<% WebAdmin.HTMLHelper.createDatePicker("fromDate", fromDate); %>&nbsp;Fabrik:&nbsp;<select name="factory" class="Textfield">
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
					</select>&nbsp;<input type="button" value="Sök" onclick="search()" class="Button"></td>
				</tr>
				<tr>
					<td>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Fabrik</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Trans. nr</th>
							</tr>
							<%
							
								int i = 0;
								while (i < missingTransactionList.Count)
								{		
									
									
									string bgStyle = "";
									
									if ((i % 2) > 0)
									{
										bgStyle = " style=\"background-color: #e0e0e0;\"";
									}
									
																					
									%>
									<tr>
										<td class="jobDescription" <%= bgStyle %> valign="top"><%= currentFactory.no %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top"><%= missingTransactionList[i].ToString() %></td>
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