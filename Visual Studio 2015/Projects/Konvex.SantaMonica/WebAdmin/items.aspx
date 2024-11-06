<%@ Page language="c#" Codebehind="items.aspx.cs" AutoEventWireup="false" Inherits="Navipro.SantaMonica.WebAdmin.Items" %>
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
					<td class="activityName">Artiklar</td>
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
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">
									Lägg till STOPP-avgift</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">
									Avlivningsartikel</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">
									Tillgänglig i bilen</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center">
									Kräv ID</th>
								<th class="headerFrame" style="BACKGROUND-COLOR: #a3b7ca" align="center" width="50">
									Priser</th>
							</tr>
							<%
								int z = 0;
								
								int i = 0;
								while (i < itemDataSet1.Tables[0].Rows.Count)
								{	
									string bgStyle = "";
									
									if ((z % 2) > 0)
									{
										bgStyle = " style=\"background-color: #e0e0e0;\"";
									}
								
														
									%>
									<tr>
										<td class="jobDescription" <%= bgStyle %> valign="top"><%= itemDataSet1.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top"><%= itemDataSet1.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top"><%= itemDataSet1.Tables[0].Rows[i].ItemArray.GetValue(8).ToString() %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top" align="center"><%
											if (itemDataSet1.Tables[0].Rows[i].ItemArray.GetValue(3).ToString() == "1")
											{
												%><img src="images/checked.gif" border="0" width="13" height="13"><%
											}	
										 %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top" align="center"><%
											if (itemDataSet1.Tables[0].Rows[i].ItemArray.GetValue(9).ToString() == "1")
											{
												%><img src="images/checked.gif" border="0" width="13" height="13"><%
											}
										 %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top" align="center"><%
											if (itemDataSet1.Tables[0].Rows[i].ItemArray.GetValue(10).ToString() == "1")
											{
												%><img src="images/checked.gif" border="0" width="13" height="13"><%
											}
										 %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top" align="center"><%
											if (itemDataSet1.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() == "1")
											{
												%><img src="images/checked.gif" border="0" width="13" height="13"><%
											}
										 %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top" align="center"><a href="items_view.aspx?itemNo=<%= itemDataSet1.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
									</tr>
									<%
										
									i++;
									z++;
								}
							
							
								i = 0;
								while (i < itemDataSet2.Tables[0].Rows.Count)
								{							
									string bgStyle = "";
									
									if ((z % 2) > 0)
									{
										bgStyle = " style=\"background-color: #e0e0e0;\"";
									}

									%>
									<tr>
										<td class="jobDescription" <%= bgStyle %> valign="top"><%= itemDataSet2.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top"><%= itemDataSet2.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top"><%= itemDataSet2.Tables[0].Rows[i].ItemArray.GetValue(8).ToString() %></td>										
										<td class="jobDescription" <%= bgStyle %> valign="top" align="center"><%
											if (itemDataSet2.Tables[0].Rows[i].ItemArray.GetValue(3).ToString() == "1")
											{
												%><img src="images/checked.gif" border="0" width="13" height="13"><%
											}										
										 %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top" align="center"><%
											if (itemDataSet2.Tables[0].Rows[i].ItemArray.GetValue(9).ToString() == "1")
											{
												%><img src="images/checked.gif" border="0" width="13" height="13"><%
											}
										 %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top" align="center"><%
											if (itemDataSet2.Tables[0].Rows[i].ItemArray.GetValue(10).ToString() == "1")
											{
												%><img src="images/checked.gif" border="0" width="13" height="13"><%
											}
										 %></td>									 
										<td class="jobDescription" <%= bgStyle %> valign="top"><%
											if (itemDataSet2.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() == "1")
											{
												%><img src="images/checked.gif" border="0" width="13" height="13"><%
											}										
										 %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top" align="center"><a href="items_view.aspx?itemNo=<%= itemDataSet2.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>										 
									</tr>
									<%
										
									i++;
									z++;
								}

								i = 0;
								while (i < itemDataSet3.Tables[0].Rows.Count)
								{							
									string bgStyle = "";
									
									if ((z % 2) > 0)
									{
										bgStyle = " style=\"background-color: #e0e0e0;\"";
									}

									%>
									<tr>
										<td class="jobDescription" <%= bgStyle %> valign="top"><%= itemDataSet3.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top"><%= itemDataSet3.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top"><%= itemDataSet3.Tables[0].Rows[i].ItemArray.GetValue(8).ToString() %></td>										
										<td class="jobDescription" <%= bgStyle %> valign="top" align="center"><%
											if (itemDataSet3.Tables[0].Rows[i].ItemArray.GetValue(3).ToString() == "1")
											{
												%><img src="images/checked.gif" border="0" width="13" height="13"><%
											}										
										 %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top" align="center"><%
											if (itemDataSet3.Tables[0].Rows[i].ItemArray.GetValue(9).ToString() == "1")
											{
												%><img src="images/checked.gif" border="0" width="13" height="13"><%
											}
										 %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top" align="center"><%
											if (itemDataSet3.Tables[0].Rows[i].ItemArray.GetValue(10).ToString() == "1")
											{
												%><img src="images/checked.gif" border="0" width="13" height="13"><%
											}
										 %></td>										 
										<td class="jobDescription" <%= bgStyle %> valign="top" align="center"><%
											if (itemDataSet3.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() == "1")
											{
												%><img src="images/checked.gif" border="0" width="13" height="13"><%
											}										
										 %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top" align="center"><a href="items_view.aspx?itemNo=<%= itemDataSet3.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
									</tr>
									<%
										
									i++;
									z++;
								}

								i = 0;
								while (i < itemDataSet4.Tables[0].Rows.Count)
								{							
									string bgStyle = "";
									
									if ((z % 2) > 0)
									{
										bgStyle = " style=\"background-color: #e0e0e0;\"";
									}

									%>
									<tr>
										<td class="jobDescription" <%= bgStyle %> valign="top"><%= itemDataSet4.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top"><%= itemDataSet4.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top"><%= itemDataSet4.Tables[0].Rows[i].ItemArray.GetValue(8).ToString() %></td>										
										<td class="jobDescription" <%= bgStyle %> valign="top" align="center"><%
											if (itemDataSet4.Tables[0].Rows[i].ItemArray.GetValue(3).ToString() == "1")
											{
												%><img src="images/checked.gif" border="0" width="13" height="13"><%
											}										
										 %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top" align="center"><%
											if (itemDataSet4.Tables[0].Rows[i].ItemArray.GetValue(9).ToString() == "1")
											{
												%><img src="images/checked.gif" border="0" width="13" height="13"><%
											}
										 %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top" align="center"><%
											if (itemDataSet4.Tables[0].Rows[i].ItemArray.GetValue(10).ToString() == "1")
											{
												%><img src="images/checked.gif" border="0" width="13" height="13"><%
											}
										 %></td>										 
										<td class="jobDescription" <%= bgStyle %> valign="top" align="center"><%
											if (itemDataSet4.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() == "1")
											{
												%><img src="images/checked.gif" border="0" width="13" height="13"><%
											}										
										 %></td>
										<td class="jobDescription" <%= bgStyle %> valign="top" align="center"><a href="items_view.aspx?itemNo=<%= itemDataSet4.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() %>"><img src="images/button_assist.gif" border="0" width="12" height="13"></a></td>
									</tr>
									<%
										
									i++;
									z++;
								}
							
							
							%>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
