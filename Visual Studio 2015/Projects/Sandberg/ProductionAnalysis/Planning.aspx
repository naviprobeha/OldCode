<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Planning.aspx.cs" Inherits="ProductionAnalysis.Planning" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
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
	        <td><h1>Produktionslista <asp:Label ID="header" runat="server" Text=""></asp:Label></h1></td>
	    </tr>
	    <tr>
            <td>Typ: <asp:DropDownList ID="typeList" runat="server" AutoPostBack="True" 
                    CssClass="DropDown" onselectedindexchanged="typeList_SelectedIndexChanged"></asp:DropDownList> Antal artiklar: <asp:Label ID="noOfItems" runat="server"/> st. Antal körningar: <asp:Label ID="noOfRuns" runat="server"/></td>
	    </tr>
	    <tr>
	        <td>
                <table class="data">
                    <tr>
                        <th>&nbsp;</th>
                        <th style="text-align: center;">Tillverkas vecka</th>
                        <th>Orderdatum</th>
                        <th>Artikelnr</th>
                        <th>Beskrivning</th>
                        <th style="text-align: right;">Antal på order</th>
                        <th style="text-align: right;">Antal intern order</th>
                        <th style="text-align: right;">Lagersaldo</th>
                        <th style="text-align: right;">Disponibelt</th>
                        <th style="text-align: right;">Försäljning antal</th>
                        <th style="text-align: center;">Utgående artikel</th>
                        <th style="text-align: center;">Dubbel körning</th>
                        <th>&nbsp;</th>
                    </tr>
                    
                    <%
                        if (itemJournalLineDataSet != null)
                        {
                            int i = 0;
                            int count = 0;
                            int weekNo = 1;
                            string style = "";
                            string iconImg = "";
                            string colorStyle = "";
                            bool doubleBatch = false;
                            
                            while (i < itemJournalLineDataSet.Tables[0].Rows.Count)
                            {
                                if (count == noOfSlots)
                                {
                                    weekNo++;
                                    count = 0;
                                }

                                
                                style = "";
                                if ((weekNo % 2) == 0) style = "background-color: #e0e0e0;";
                                if (weekNo == 1) iconImg = "ind_green.gif";
                                if (weekNo == 2) iconImg = "ind_yellow.gif";
                                if (weekNo == 3) iconImg = "ind_red.gif";
                                if (weekNo == 4) iconImg = "ind_purple.gif";

                                colorStyle = "";
                                if (weekNo == 1) colorStyle = "background-color: #53d40c;";
                                if (weekNo == 2) colorStyle = "background-color: #fce913;";
                                if (weekNo == 3) colorStyle = "background-color: #fc0000;";
                                if (weekNo == 4) colorStyle = "background-color: #cf22da;";
                                
                                if (!doubleBatch)
                                {
                                    Navipro.Sandberg.Common.ItemJournalLine itemJournalLine = new Navipro.Sandberg.Common.ItemJournalLine(itemJournalLineDataSet.Tables[0].Rows[i]);


                                   
                                    if (itemJournalLine.qtyOnSampleTestBookOrder > 0) style = "background-color: #fce913;";
                                    if (itemJournalLine.qtyOnOrder > itemJournalLine.qtyOnSampleTestBookOrder) style = "background-color: #fc0000;";
                                    
                                    %>
                                    <tr>
                                        <td width="20" style="<%= colorStyle %>">&nbsp;</td>
                                        <td style="<%= style %>" align="center"><%= weekNo%></td>
                                        <td style="<%= style %>"><%= itemJournalLine.documentDate.ToString("yyyy-MM-dd")%>&nbsp;</td>
                                        <td style="<%= style %>"><a href="<%= link+itemJournalLine.itemNo %>" target="_blank"><%= itemJournalLine.itemNo%></a>&nbsp;</td>
                                        <td style="<%= style %>"><%= itemJournalLine.description%>&nbsp;</td>
                                        <td style="<%= style %>" align="right"><%= itemJournalLine.qtyOnOrder.ToString("F2")%>&nbsp;</td>
                                        <td style="<%= style %>" align="right"><%= itemJournalLine.qtyOnSampleTestBookOrder.ToString("F2")%>&nbsp;</td>
                                        <td style="<%= style %>" align="right"><%= itemJournalLine.inventory.ToString("F2")%>&nbsp;</td>
                                        <td style="<%= style %>" align="right"><%= itemJournalLine.disposable.ToString("F2")%>&nbsp;</td>
                                        <td style="<%= style %>" align="right"><%= itemJournalLine.saleQty.ToString("F2")%>&nbsp;</td>
                                        <td style="<%= style %>" align="center"><%= itemJournalLine.getExpiringItem()%>&nbsp;</td>
                                        <td style="<%= style %>" align="center"><%= itemJournalLine.getDoubleBatch()%>&nbsp;</td>
                                        <td width="20" style="<%= colorStyle %>">&nbsp;</td>
                                    </tr>
                                    <%
                                    
                                        if (itemJournalLine.doubleBatch)
                                            doubleBatch = true;
                                        else
                                            i++;
                                }
                                else
                                {                  
                                    %>
                                    <tr>
                                        <td style="<%= colorStyle %>">&nbsp;</td>
                                        <td style="<%= style %>" align="center"><%= weekNo %></td>
                                        <td style="<%= style %>">&nbsp;</td>
                                        <td style="<%= style %>">&nbsp;</td>
                                        <td style="<%= style %>">&nbsp;</td>
                                        <td style="<%= style %>">&nbsp;</td>
                                        <td style="<%= style %>">&nbsp;</td>
                                        <td style="<%= style %>">&nbsp;</td>
                                        <td style="<%= style %>">&nbsp;</td>
                                        <td style="<%= style %>">&nbsp;</td>
                                        <td style="<%= style %>">&nbsp;</td>
                                        <td style="<%= style %>">&nbsp;</td>
                                        <td style="<%= colorStyle %>">&nbsp;</td>
                                    </tr>
                                    <%
                                    doubleBatch = false;
                                    i++; 
                                }

                                count++;
                            }

                            while (count < noOfSlots)
                            {
                                    %>
                                    <tr>
                                        <td style="<%= colorStyle %>">&nbsp;</td>
                                        <td style="<%= style %>" align="center"><%= weekNo %></td>
                                        <td style="<%= style %>">&nbsp;</td>
                                        <td style="<%= style %>">&nbsp;</td>
                                        <td style="<%= style %>">&nbsp;</td>
                                        <td style="<%= style %>">&nbsp;</td>
                                        <td style="<%= style %>">&nbsp;</td>
                                        <td style="<%= style %>">&nbsp;</td>
                                        <td style="<%= style %>">&nbsp;</td>
                                        <td style="<%= style %>">&nbsp;</td>
                                        <td style="<%= style %>">&nbsp;</td>
                                        <td style="<%= style %>">&nbsp;</td>
                                        <td style="<%= colorStyle %>">&nbsp;</td>
                                   </tr>
                                    <%

                                        count++;
                            }
                        }         
                    %>
               
                </table>
	        </td>
	    </tr>
    </table>
      
        
    </form>
</body>
</html>
