<%@ Page language="c#" Codebehind="map.aspx.cs" AutoEventWireup="false" Inherits="WebAdmin.map" %>
<HTML>
	<HEAD>
		<title>SmartShipping</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="css/webstyle.css" rel="stylesheet">
		<script>
	
		function updateWindow()
		{
		
			document.thisform.submit();
		}	
		
		</script>
	</HEAD>
	<body topMargin="10" onload="setTimeout('updateWindow()', 600000)" MS_POSITIONING="GridLayout">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" name="thisform" method="post" runat="server">
			<input type="hidden" name="command"> <input type="hidden" name="width">
			<table class="frame" height="90%" cellSpacing="2" cellPadding="2" width="100%" border="0">
				<tr>
					<td colSpan="2" height="28">
						<!-- #INCLUDE FILE="menu.asp" --></td>
				</tr>
				<tr>
					<td class="activityName" colSpan="2">Karta</td>
				</tr>
				<tr>
					<td height="100%"><iframe id=mapFrame 
      style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid" 
      src="<%= mapServer.getUrl() %>" frameBorder=no width="100%" scrolling=no 
      height="100%"></iframe>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
