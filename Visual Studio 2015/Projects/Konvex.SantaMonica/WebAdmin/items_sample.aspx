<%@ Page language="c#" Codebehind="items_sample.aspx.cs" AutoEventWireup="false" Inherits="Navipro.SantaMonica.WebAdmin.items_sample" %>
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
		//document.thisform.command.value = "changeShipDate";
		//document.thisform.submit();
	
	}

	function submitSearch()
	{
		document.thisform.command.value = "changeShipDate";
		document.thisform.submit();
	
	}
		
	</script>
	
	
	<body MS_POSITIONING="GridLayout" topmargin="10" onload="setTimeout('document.thisform.submit()', 30000)">
		<!-- #INCLUDE FILE="header.asp" -->
		<form id="thisform" method="post" runat="server">
		<input type="hidden" name="command">
			<table cellspacing="2" cellpadding="2" border="0" width="100%" class="frame">
				<tr>
					<td height="28">
						<!-- #INCLUDE FILE="menu.asp" -->
					</td>
				</tr>
				<tr>
					<td class="activityName">Antal provtagningar</td>
				</tr>
				<tr>
					<td class="">Datumintervall:&nbsp;<% WebAdmin.HTMLHelper.createDatePicker("startDate", startDate); %>&nbsp;-&nbsp;<% WebAdmin.HTMLHelper.createDatePicker("endDate", endDate); %>&nbsp;Fabrik:&nbsp;<select name="factory" class="Textfield">
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
					</select>&nbsp;<input type="button" value="Sök" class="Button" onclick="submitSearch()"></td>
				</tr>
				<tr>
					<td>
						<table cellspacing="1" cellpadding="2" border="0" width="100%" class="innerFrame">
							<tr>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left" width=80">
									Artikelnr</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Beskrivning</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="left">
									Enhet</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right" width="150">
									Antal provtagningar</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="right" width="150">
									Antal obduktioner</th>
							</tr>
							<%
								int z = 0;
								
								int i = 0;
								while (i < itemDataSet.Tables[0].Rows.Count)
								{	
								
									if ((bseTestingList.Contains(itemDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString())) || (postMortemList.Contains(itemDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString())))
									{
										string bgStyle = "";
										
										if ((z % 2) > 0)
										{
											bgStyle = " style=\"background-color: #e0e0e0;\"";
										}
									
																			
										string bseTestings = "";
										string postMortems = "";
										
										if (bseTestingList.Contains(itemDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString())) bseTestings = bseTestingList[itemDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()].ToString()+" st";
										if (postMortemList.Contains(itemDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString())) postMortems = postMortemList[itemDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()].ToString()+" st";
																									
										%>
										<tr>
											<td class="jobDescription" <%= bgStyle %> valign="top"><%= itemDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %></td>
											<td class="jobDescription" <%= bgStyle %> valign="top"><%= itemDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %></td>
											<td class="jobDescription" <%= bgStyle %> valign="top"><%= itemDataSet.Tables[0].Rows[i].ItemArray.GetValue(8).ToString() %></td>
											<td class="jobDescription" <%= bgStyle %> valign="top" align="right"><%= bseTestings %></td>
											<td class="jobDescription" <%= bgStyle %> valign="top" align="right"><%= postMortems %></td>
										</tr>
										<%
											
										z++;
									}
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
