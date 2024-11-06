<%@ Page language="c#" Codebehind="containers_modify.aspx.cs" AutoEventWireup="false" Inherits="WebAdmin.containers_modify" %>
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
	
	function goBack()
	{
		<%
			if ((Request["containerNo"] != "") && (Request["containerNo"] != null))
			{
				%>document.location.href="containers_view.aspx?containerNo=<%= currentContainer.no %>";<%
			}
			else
			{
				%>document.location.href="containers.aspx";<%
			}
		%>			
	
	}

	function remove()
	{
		if (confirm("Vill du radera container <%= Request["containerNo"] %>?"))
		{	
			document.thisform.command.value = "deleteContainer";
			document.thisform.submit();
		}
	}

	function validate()
	{
		document.thisform.command.value = "saveContainer";
		document.thisform.submit();
	}
	
	</script>

	<body MS_POSITIONING="GridLayout" topmargin="10">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" name="thisform" method="post" runat="server">
			<input type="hidden" name="command" value="">
			<input type="hidden" name="containerNo" value="<%= Request["containerNo"] %>">
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame" style="border-bottom: 0 px">
				<tr>
					<td colspan="2" height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td colspan="2" class="activityName"><% if ((Request["containerNo"] == "") || (Request["containerNo"] == null)) Response.Write("Ny container"); else Response.Write("Ändra container "+currentContainer.no); %></td>
				</tr>
			</table>
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame" style="border-top: 0 px">
				<tr>
					<td width="50" valign="top"><iframe id="mapFrame" width="265" height="600" frameborder="no" scrolling="no" src="<%= mapServer.getUrl() %>" style="border: 1px #000000 solid;"></iframe></td>
					<td valign="top">
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<%
								if ((Request["containerNo"] == "") || (Request["containerNo"] == null))
								{	
									%>
									<tr>
										<td class="interaction" valign="top" colspan="2">
											<table cellspacing="0" cellpadding="0" width="100%" border="0">
												<tr>
													<td class="activityAuthor" height="15" valign="top">Nr</td>
												</tr>
												<tr>
													<td class="interaction" height="20"><input type="text" name="no" maxlength="20" value="<%= currentContainer.no %>" class="Textfield">&nbsp;<font style="color: red;"><%= errorMessage %></font></td>
												</tr>
											</table>
										</td>
									</tr>
									<%
								}
							%>
							<tr>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Beskrivning</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><input type="text" name="description" maxlength="30" value="<%= currentContainer.description %>" class="Textfield"></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Containertyp</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><select name="containerTypeCode" class="Textfield">
											<%
												int j = 0;
												while (j < containerTypeDataSet.Tables[0].Rows.Count)
												{
													string selected = "";
													if (containerTypeDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString() == currentContainer.containerTypeCode) selected = "selected";
													
													%><option value="<%= containerTypeDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString() %>" <%= selected %>><%= containerTypeDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString() %></option><%
													j++;												
												}
											%>
											</select></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Vikt</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentContainerType.weight.ToString("0") %> kg</b></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Volym</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><b><%= currentContainerType.volume.ToString("0") %> kubikmeter</b></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Nuvarande positionstyp</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><select name="currentLocationType" class="Textfield" onchange="document.thisform.submit()">
												<option value="0" <% if (currentLocationType == 0) Response.Write("selected"); %>>Bil</option>
												<option value="1" <% if (currentLocationType == 1) Response.Write("selected"); %>>Kund</option>
												<option value="2" <% if (currentLocationType == 2) Response.Write("selected"); %>>Fabrik</option>
											</select></td>
										</tr>
									</table>
								</td>
								<td class="interaction" valign="top">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td class="activityAuthor" height="15" valign="top">Nuvarande position</td>
										</tr>
										<tr>
											<td class="interaction" height="20"><select name="currentLocationCode" class="Textfield"><%
											
											if (currentLocationType == 0)
											{
												int i = 0;
												while (i < agentDataSet.Tables[0].Rows.Count)
												{
													string selected = "";
													if (agentDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() == currentContainer.currentLocationCode) selected = "selected";
													
													%><option value="<%= agentDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>" <%= selected %>><%= agentDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>&nbsp;<%= agentDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %></option><%
													i++;												
												}
											}
											if (currentLocationType == 1)
											{
												int i = 0;
												while (i < shippingCustomerDataSet.Tables[0].Rows.Count)
												{
													string selected = "";
													if (shippingCustomerDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() == currentContainer.currentLocationCode) selected = "selected";

													%><option value="<%= shippingCustomerDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>" <%= selected %>><%= shippingCustomerDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %>, <%= shippingCustomerDataSet.Tables[0].Rows[i].ItemArray.GetValue(5).ToString() %> (<%= shippingCustomerDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>)</option><%
													i++;												
												}
											}
											if (currentLocationType == 2)
											{
												int i = 0;
												while (i < factoryDataSet.Tables[0].Rows.Count)
												{
													string selected = "";
													if (factoryDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() == currentContainer.currentLocationCode) selected = "selected";

													%><option value="<%= factoryDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>" <%= selected %>><%= factoryDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %></option><%
													i++;												
												}
											}
											
											
											
											%></select></td>
										</tr>
									</table>
								</td>
							</tr>													
						</table>
						<table cellspacing="0" cellpadding="0" border="0" width="100%">
							<tr>
								<td align="right"><input type="button" value="Avbryt" onclick="goBack()" class="Button"><% if ((Request["containerNo"] != "") && (Request["containerNo"] != null)) { %>&nbsp;<input type="button" value="Ta bort" class="button" onclick="remove()"><% } %>&nbsp;<input type="button" value="Spara" onclick="validate()" class="Button"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	
	</body>
</HTML>