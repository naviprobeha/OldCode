<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ProductionAnalysis._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Produktionsanalys</title>
    <link rel="stylesheet" href="css/webstyle.css"/>
</head>
<body>
	<table class="logo">
    	<tr>
		    <td>&nbsp;</td>
    		<td align="right"><img src="images/nav_small_logo.jpg" alt=""/></td>
	    </tr>
    </table>


    <form id="form1" runat="server">
	<br />
	<table class="main">
	    <tr>
	        <td><h1>Produktionsanalys</h1></td>
	    </tr>
	    <tr>
	        <td>
	            <table class="request">
	                <tr>
	                    <td>                
                            <table class="field">
	                            <tr>
	                                <td>Artikelnr:</td>
	                            </tr>
	                            <tr>
						            <td>
                                        <asp:TextBox ID="itemNoBox" CssClass="TextField" runat="server" 
                                            ontextchanged="itemNoBox_TextChanged"></asp:TextBox>
                                    </td>
	                            </tr>
	                        </table>
	                    </td>
	                    <td align="right" valign="bottom">
                            <asp:Button ID="searchBtn" runat="server" CssClass="Button" Text="Sök" 
                                onclick="searchBtn_Click" />
                        </td>
	                </tr>
	            </table>        
	            <br />
                <asp:Panel ID="dataPanel" runat="server">
	            <table class="data">
	                <tr>
	                    <th>Artikelnr</th>
	                    <th>Antal kg material</th>
	                    <th>Färg 1</th>
	                    <th>Färg 2</th>
	                    <th>Färg 3</th>
	                    <th>Färg 4</th>
	                    <th>Färg 5</th>
	                    <th>Färg 6</th>
	                    <th>Färg 7</th>
	                    <th>Färg 8</th>
	                    <th>Prima %</th>
	                    <th>&nbsp;</th>
	                </tr>
	                <tr>
	                    <td>
                            <asp:DropDownList ID="calcItemNo" runat="server" CssClass="DropDown">
                            </asp:DropDownList>
                        </td>
	                    <td><asp:TextBox ID="qtyOfKgMaterial" CssClass="TextField" runat="server"></asp:TextBox></td>
	                    <td><%= color1Value.ToString("F2") %></td>
	                    <td><%= color2Value.ToString("F2") %></td>
	                    <td><%= color3Value.ToString("F2") %></td>
	                    <td><%= color4Value.ToString("F2") %></td>
	                    <td><%= color5Value.ToString("F2") %></td>
	                    <td><%= color6Value.ToString("F2") %></td>
	                    <td><%= color7Value.ToString("F2") %></td>
	                    <td><%= color8Value.ToString("F2") %></td>
	                    <td><%= primaValue.ToString("F2") %> %</td>
	                    <td align="right">
                            <asp:Button ID="calcBtn" runat="server" Text="Beräkna" CssClass="Button" 
                                onclick="calcBtn_Click" />
                        </td>
	                </tr>
	            </table>
	            <br />
                <table class="data">
                    <tr>
                        <th>Datum</th>
                        <th>Artikelnr</th>
                        <th>Parti</th>
                        <th>Artikelnr papper</th>
                        <th>Antal kg papper</th>
                        <th>Antal m. prima</th>
                        <th>Prima %</th>
                        <th colspan="2">Färg 1</th>
                        <th colspan="2">Färg 2</th>
                        <th colspan="2">Färg 3</th>
                        <th colspan="2">Färg 4</th>
                        <th colspan="2">Färg 5</th>
                        <th colspan="2">Färg 6</th>
                        <th colspan="2">Färg 7</th>
                        <th colspan="2">Färg 8</th>
                    </tr>
                    
                    <%
                        if (productionEntryDataSet != null)
                        {
                            int i = 0;
                            while (i < productionEntryDataSet.Tables[0].Rows.Count)
                            {

                                Navipro.Sandberg.Common.ProductionEntry productionEntry = new Navipro.Sandberg.Common.ProductionEntry(productionEntryDataSet.Tables[0].Rows[i]);

                                %>
                                <tr>
                                    <td rowspan="3" nowrap="nowrap"><%= productionEntry.date.ToString("yyyy-MM-dd")%></td>
                                    <td rowspan="3"><%= productionEntry.itemNo%>&nbsp;</td>
                                    <td rowspan="3"><%= productionEntry.variantCode%>&nbsp;</td>
                                    <td><%= productionEntry.paperItemNo%>&nbsp;</td>
                                    <td><%= productionEntry.kgOfPaper.ToString("F2") %>&nbsp;</td>
                                    <td><%= productionEntry.mOfPrima.ToString("F2") %>&nbsp;</td>
                                    <td><%= productionEntry.primaPercent.ToString("F2") %>&nbsp;</td>
                                    <td><%= productionEntry.color1ItemNo %>&nbsp;</td>
                                    <td align="right">&nbsp;<%= productionEntry.color1Kg.ToString("F2") %></td>
                                    <td><%= productionEntry.color2ItemNo %>&nbsp;</td>
                                    <td align="right">&nbsp;<%= productionEntry.color2Kg.ToString("F2") %></td>
                                    <td><%= productionEntry.color3ItemNo %>&nbsp;</td>
                                    <td align="right">&nbsp;<%= productionEntry.color3Kg.ToString("F2") %></td>
                                    <td><%= productionEntry.color4ItemNo %>&nbsp;</td>
                                    <td align="right">&nbsp;<%= productionEntry.color4Kg.ToString("F2") %></td>
                                    <td><%= productionEntry.color5ItemNo %>&nbsp;</td>
                                    <td align="right">&nbsp;<%= productionEntry.color5Kg.ToString("F2") %></td>
                                    <td><%= productionEntry.color6ItemNo %>&nbsp;</td>
                                    <td align="right">&nbsp;<%= productionEntry.color6Kg.ToString("F2") %></td>
                                    <td><%= productionEntry.color7ItemNo %>&nbsp;</td>
                                    <td align="right">&nbsp;<%= productionEntry.color7Kg.ToString("F2") %></td>
                                    <td><%= productionEntry.color8ItemNo %>&nbsp;</td>
                                    <td align="right">&nbsp;<%= productionEntry.color8Kg.ToString("F2") %></td>
                                </tr>
                                <tr>
                                    <td colspan="20"><%= productionEntry.comment %>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="20"><%= commentLines.getComposedComments(database, productionEntry.itemNo, productionEntry.variantCode) %>&nbsp;</td>
                                </tr>
                                <%
                       
                                i++;
                            }
                        }         
                    %>
               
                </table>
                </asp:Panel>          
	        </td>
	    </tr>
    </table>
      
        
    </form>
</body>
</html>
