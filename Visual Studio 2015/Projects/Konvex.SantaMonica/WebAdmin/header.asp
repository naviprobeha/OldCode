<%
	string logo = "konvex_logo.jpg";

	Navipro.SantaMonica.Common.Organization headerOrg = (Navipro.SantaMonica.Common.Organization)Session["current.user.organization"];

	if (headerOrg != null)
	{
		if (headerOrg.no == "SSAB") logo = "slt_logo.jpg";
		if (headerOrg.no == "UD") logo = "slt_logo.jpg";
		if (headerOrg.no == "LIT") logo = "slt_logo.jpg";
		if (headerOrg.no == "BD") logo = "slt_logo.jpg";
		if (headerOrg.no == "EF") logo = "slt_logo.jpg";
		if (headerOrg.no == "KADT") logo = "slt_logo.jpg";
		if (headerOrg.no == "KDT") logo = "slt_logo.jpg";
		if (headerOrg.no == "SLT") logo = "slt_logo.jpg";
		if (headerOrg.no == "AVL") logo = "slt_logo.jpg";
		if (headerOrg.no == "GEOL") logo = "slt_logo.jpg";
		if (headerOrg.no == "KALLIN") logo = "slt_logo.jpg";
	}
%>

<table cellspacing="0" cellpadding="0" border="0" width="100%" class="frame">
	<tr height="55">
		<td background="images/logo_bg.jpg" height="55" align="left"><img src="images/<%= logo %>" border="0"></td>
		<td background="images/logo_bg.jpg" height="55" align="right">
			<table cellspacing="0" cellpadding="2" width="99" border="0">
			<tr>
				<td height="55" background="images/nav_small_logo.jpg" valign="top">
					<table cellspacing="0" cellpadding="0" border="0" width="100%">
					<tr>
						<td align="right" style="font-size: 11px; color: #000000;"><a href="logoff.aspx" class="logoff">Logga ut</a>&nbsp;</td>
						<td><a href="logoff.aspx"><img src="images/logout.gif" border="0"></a></td>
					</tr>
					</table>
				</td>
			</tr>
			</table>
		</td>
	</tr>
</table>
