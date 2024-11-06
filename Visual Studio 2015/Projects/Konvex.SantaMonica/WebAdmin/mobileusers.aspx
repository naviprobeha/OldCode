<%@ Page language="c#" Codebehind="mobileusers.aspx.cs" AutoEventWireup="false" Inherits="Navipro.SantaMonica.WebAdmin.mobileusers" %>
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
	<body MS_POSITIONING="GridLayout" topmargin="10" onload="setTimeout('document.thisform.submit()', 30000)">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" method="post" runat="server">
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame">
				<tr>
					<td height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td class="activityName">Mobilanvändare</td>
				</tr>
				<tr>
					<td><input type="button" value="Ny användare" class="button" onclick="document.location.href='mobileusers_modify.aspx';"></td>
				</tr>
				<tr>
					<td>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" width="90%">
									Namn</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">
									Ändra</th>
							</tr>
							<%
								int i = 0;
								while (i < mobileUserDataSet.Tables[0].Rows.Count)
								{	
									string bgStyle = "";
									
									if ((i % 2) > 0)
									{
										bgStyle = " style=\"background-color: #e0e0e0;\"";
									}
								
														
									%>
									<tr>
										<td class="jobDescription" <%= bgStyle %> valign="top"><%= mobileUserDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString() %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top" align="center"><a href="mobileusers_modify.aspx?entryNo=<%= mobileUserDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
									</tr>
									<%
										
									i++;
								}
								if (i == 0)
								{
									%>
									<tr>
										<td class="jobDescription" valign="top" colspan="2">Inga användare registrerade</td>
									</tr>
									<%
								
								}
																			
							%>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
