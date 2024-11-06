<%@ Page language="c#" Codebehind="scheduled_orders_convert.aspx.cs" AutoEventWireup="false" Inherits="Navipro.SantaMonica.WebAdmin.scheduled_orders_convert" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SmartShipping</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link rel="stylesheet" href="css/webstyle.css">
		<script>

		function goBack()
		{
			document.location.href='scheduled_orders.aspx';		
		}

		function validate()
		{
			document.thisform.action = "scheduled_orders_convert.aspx";
			document.thisform.command.value = "createOrders";
			document.thisform.submit();
		}

		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" topmargin="10">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" action="orders_modify.aspx" method="post" runat="server">
			<input type="hidden" name="command" value="">
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame">
				<tr>
					<td height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td class="activityName">Återkommande order</td>
				</tr>
				<tr>
					<td>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center" colspan="2">
									Skapa körorder</th>
							</tr>
							<tr>
								<td class="interaction" width="30%">&nbsp;<b>Hämtdatum:</b></td>
								<td class="interaction2" height="20">&nbsp;&nbsp;<% createDatePicker("shipDate", System.DateTime.Now); %></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td align="right" height="25"><input type="button" onclick="goBack();" value="Tillbaka" class="Button">&nbsp;<input type="button" onclick="validate()" value="Skapa" class="Button"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
