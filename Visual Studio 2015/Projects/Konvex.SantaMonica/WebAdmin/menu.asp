<table cellspacing="0" cellpadding="2" width="100%" border="0">
<tr>

	<%

	Navipro.SantaMonica.Common.RoleMenuItems roleMenuItems = new Navipro.SantaMonica.Common.RoleMenuItems();
	System.Data.DataSet shortcutMenuItemDataSet = roleMenuItems.getUserMenuShortcutDataSet(database, currentOrganization.no, currentOperator.userId);

	int q = 0;
	while(q < shortcutMenuItemDataSet.Tables[0].Rows.Count)
	{
		%>
		<td class="menuBar"><a href="<%= shortcutMenuItemDataSet.Tables[0].Rows[q].ItemArray.GetValue(2).ToString() %>" class="menu"><img src="images/button_overview.gif" border="0"></a></td>
		<td class="menuBar" nowrap><a href="<%= shortcutMenuItemDataSet.Tables[0].Rows[q].ItemArray.GetValue(2).ToString() %>" class="menu"><%= shortcutMenuItemDataSet.Tables[0].Rows[q].ItemArray.GetValue(1).ToString() %></a>&nbsp;&nbsp;&nbsp;</td>
		<%

		q++;
	}

	%>

	<td class="menuBar" width="100%">&nbsp;</td>
	<td class="menuBar"><select name="menu" class="Textfield" onchange="if (this.value != '') document.location.href=this.value;"><option value="">Meny</option><option value="">------</option><%

	System.Data.DataSet menuItemDataSet = roleMenuItems.getUserMenuItemDataSet(database, currentOrganization.no, currentOperator.userId);

	int n = 0;
	while(n < menuItemDataSet.Tables[0].Rows.Count)
	{
		%><option value="<%= menuItemDataSet.Tables[0].Rows[n].ItemArray.GetValue(2).ToString() %>"><%= menuItemDataSet.Tables[0].Rows[n].ItemArray.GetValue(1).ToString() %></option><%

		n++;
	}

	%></select></td>

	<%

	System.Collections.ArrayList relations = (System.Collections.ArrayList)Session["current.user.relations"];

	if (relations.Count > 1)
	{
		%><td class="menuBar"><select name="organization" class="Textfield" onchange="document.location.href='authorize.aspx?command=changeOrganization&index='+this.value;"><%

		Navipro.SantaMonica.Common.Organizations organizations = new Navipro.SantaMonica.Common.Organizations();

		int r = 0;
		while(r < relations.Count)
		{
			Navipro.SantaMonica.Common.Organization organization = organizations.getOrganization(database, ((Navipro.SantaMonica.Common.OrganizationOperator)relations[r]).organizationNo, false);

			%><option value="<%= r %>" <% if (organization.no == currentOrganization.no) Response.Write("selected"); %>><%= organization.name %></option><%

			r++;
		}

		%></select></td><%
	}

	%>
</tr>
</table>
