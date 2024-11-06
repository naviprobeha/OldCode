<%@ Page language="c#" Codebehind="containers.aspx.cs" AutoEventWireup="false" Inherits="Navipro.SantaMonica.WebAdmin.containers" %>
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
					<td class="activityName">Containers</td>
				</tr>
				<tr>
					<td class=""><input type="button" value="Ny container" class="button" onclick="document.location.href='containers_modify.aspx';"></td>
				</tr>
				<tr>
					<td>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
						<tr>
							<td class="interaction" valign="top">
								<table cellspacing="0" cellpadding="0" width="100%" border="0">
								<tr>
									<td class="activityAuthor" height="15" valign="top">Containernr</td>
								</tr>
								<tr>
									<td class="interaction" height="20"><input type="text" size="10" name="searchContainerNo" class="Textfield" value="<%= Request["searchContainerNo"] %>"></td>
								</tr>
								</table>
							</td>
							<td class="interaction" valign="top">
								<table cellspacing="0" cellpadding="0" width="100%" border="0">
								<tr>
									<td class="activityAuthor" height="15" valign="top">Containertyp</td>
								</tr>
								<tr>
									<td class="interaction" height="20"><select name="searchContainerTypeCode" class="Textfield">
										<option value="">Välj containertyp</option>
										<%
											int containerTypeIndex = 0;
											while(containerTypeIndex < containerTypeDataSet.Tables[0].Rows.Count)
											{
												%><option value="<%= containerTypeDataSet.Tables[0].Rows[containerTypeIndex].ItemArray.GetValue(0).ToString() %>" <% if (containerTypeDataSet.Tables[0].Rows[containerTypeIndex].ItemArray.GetValue(0).ToString() == Request["searchContainerTypeCode"]) Response.Write("selected"); %>><%= containerTypeDataSet.Tables[0].Rows[containerTypeIndex].ItemArray.GetValue(1).ToString() %></option><%
										
												containerTypeIndex++;	
											}
										%>
									</select></td>
								</tr>
								</table>
							</td>
							<td class="interaction" valign="top">
								<table cellspacing="0" cellpadding="0" width="100%" border="0">
								<tr>
									<td class="activityAuthor" height="15" valign="top">Positionstyp</td>
								</tr>
								<tr>
									<td class="interaction" height="20"><select name="searchLocationType" class="Textfield" onchange="document.thisform.submit()">
										<option value="">Välj positionstyp</option>
										<option value="0" <% if (Request["searchLocationType"] == "0") Response.Write("selected"); %>>Bil</option>
										<option value="1" <% if (Request["searchLocationType"] == "1") Response.Write("selected"); %>>Kund</option>
										<option value="2" <% if (Request["searchLocationType"] == "2") Response.Write("selected"); %>>Fabrik</option>
									</select></td>
								</tr>
								</table>
							</td>
							<td class="interaction" valign="top"  width="400">
								<table cellspacing="0" cellpadding="0" width="100%" border="0">
								<tr>
									<td class="activityAuthor" height="15" valign="top">Positionsnr</td>
								</tr>
								<tr>
									<td class="interaction" height="20"><select name="searchLocationCode" class="Textfield">
										<option value="">Välj positionsnr</option>
										<%
											if (Request["searchLocationType"] == "0")
											{
												int i = 0;
												while (i < agentDataSet.Tables[0].Rows.Count)
												{
													string selected = "";
													if (agentDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() == Request["searchLocationCode"]) selected = "selected";
													
													%><option value="<%= agentDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>" <%= selected %>><%= agentDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>&nbsp;<%= agentDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %></option><%
													i++;												
												}
											}
										
											if (Request["searchLocationType"] == "1")
											{
												int i = 0;
												while (i < shippingCustomerDataSet.Tables[0].Rows.Count)
												{
													string selected = "";
													if (shippingCustomerDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() == Request["searchLocationCode"]) selected = "selected";

													%><option value="<%= shippingCustomerDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>" <%= selected %>><%= shippingCustomerDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %> <%= shippingCustomerDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %>, <%= shippingCustomerDataSet.Tables[0].Rows[i].ItemArray.GetValue(5).ToString() %></option><%
													i++;												
												}
											}
										
											if (Request["searchLocationType"] == "2")
											{
												int i = 0;
												while (i < factoryDataSet.Tables[0].Rows.Count)
												{
													string selected = "";
													if (factoryDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() == Request["searchLocationCode"]) selected = "selected";

													%><option value="<%= factoryDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>" <%= selected %>><%= factoryDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %></option><%
													i++;												
												}
											}
										
										%>
									</select></td>
								</tr>
								</table>
							</td>
							<td class="interaction" valign="top" colspan="2" align="right">
								<table cellspacing="0" cellpadding="0" width="100%" border="0">
								<tr>
									<td class="activityAuthor" height="15" valign="top">&nbsp;</td>
								</tr>
								<tr>
									<td class="interaction" height="20" align="center"><input type="submit" value="Sök" class="Button"></td>
								</tr>
								</table>
							</td>
							
						</tr>											
						</table>
						<br>	
						<% if (Request["command"] == "searchContainer") { %>				
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Containernr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Typ</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right" valign="bottom">
									Volym</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right" valign="bottom">
									Vikt</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Nuvarande position typ</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Nuvarande position</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" valign="bottom">
									Service</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center" valign="bottom">
									Visa</th>
							</tr>
							<%
								int i = 0;
								while (i < containerDataSet.Tables[0].Rows.Count)
								{		
									Navipro.SantaMonica.Common.Container container = new Navipro.SantaMonica.Common.Container(containerDataSet.Tables[0].Rows[i]);
									
									string bgStyle = "";
									
									if ((i % 2) > 0)
									{
										bgStyle = " style=\"background-color: #e0e0e0;\"";
									}
									
									string positionType = "";
									string positionName = containerDataSet.Tables[0].Rows[i].ItemArray.GetValue(6).ToString();
									
									if (containerDataSet.Tables[0].Rows[i].ItemArray.GetValue(5).ToString() == "0") positionType = "Bil";
									if (containerDataSet.Tables[0].Rows[i].ItemArray.GetValue(5).ToString() == "1") positionType = "Kund";
									if (containerDataSet.Tables[0].Rows[i].ItemArray.GetValue(5).ToString() == "2") positionType = "Fabrik";
									
									if (containerDataSet.Tables[0].Rows[i].ItemArray.GetValue(5).ToString() == "1")
									{
										Navipro.SantaMonica.Common.ShippingCustomers shippingCustomers = new Navipro.SantaMonica.Common.ShippingCustomers();
										Navipro.SantaMonica.Common.ShippingCustomer shippingCustomer = shippingCustomers.getEntry(database, containerDataSet.Tables[0].Rows[i].ItemArray.GetValue(6).ToString());
									
										if (shippingCustomer != null)
										{
											positionName = shippingCustomer.name+", "+shippingCustomer.city;
										}
									}
									
									string serviceStatus = "";
									if (container.isServiceReported(database))
									{
										serviceStatus = "Reported";
										
										bgStyle = bgStyle + " style=\"color: red;\"";
									}
													
									%>
									<tr>
										<td class="jobDescription" <%= bgStyle %> valign="top" style="font-weight: bold;"><%= containerDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top" style="font-weight: bold;"><%= containerDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString() %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top" style="font-weight: bold;" align="right"><%= float.Parse(containerDataSet.Tables[0].Rows[i].ItemArray.GetValue(3).ToString()).ToString("0") %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top" style="font-weight: bold;" align="right"><%= float.Parse(containerDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString()).ToString("0") %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top" style="font-weight: bold;"><%= positionType %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top" style="font-weight: bold;"><%= positionName %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top" style="font-weight: bold;"><%= serviceStatus %>&nbsp;</td>
										<td class="jobDescription" <%= bgStyle %> valign="top" align="center"><a href="containers_view.aspx?containerNo=<%= containerDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
									</tr>
									<%
									
									
									i++;
								}
							
							
							%>
						</table>
						<% } %>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>