<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Navipro.Cashjet.Dashboard._Default" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
		<meta content="yes" name="apple-mobile-web-app-capable" />
		<meta content="text/html; charset=iso-8859-1" http-equiv="Content-Type" />
		<meta content="minimum-scale=1.0, width=device-width, maximum-scale=0.6667, user-scalable=no" name="viewport" />
		<link href="_assets/css/style_full.css" rel="stylesheet" type="text/css" />
		<script src="_assets/js/functions.js" type="text/javascript"></script>
		<link rel="apple-touch-icon" href="_assets/img/homescreen.png"/>
		<link href="_assets/img/startup.png" rel="apple-touch-startup-image" />	
		<title>Cashjet Dashboard</title>
</head>
<body>
    <form id="form1" runat="server">
    	<div id="topbar">
		<div id="title">Cashjet Dashboard</div>
		<div id="logo"><img src="_assets/img/nav_small_logo.jpg" alt="" /></div>
	</div>
	<div id="filters"> 
	    <div id="pane">
		    <span class="caption">Rapport</span>
		    <asp:DropDownList ID="cashSiteBox" runat="server" AutoPostBack="true" DataValueField="code" DataTextField="description" CssClass="DropDown"></asp:DropDownList>
		    <span class="arrow">&nbsp;&nbsp;</span>

		    <span class="caption">Datum</span>
		    <asp:DropDownList ID="yearBox" runat="server" AutoPostBack="true" CssClass="DropDown"></asp:DropDownList>
		    <span class="arrow">&nbsp;&nbsp;</span>
		    <asp:DropDownList ID="monthBox" runat="server" AutoPostBack="true" CssClass="DropDown"></asp:DropDownList>
		    <span class="arrow">&nbsp;&nbsp;</span>
		    <asp:DropDownList ID="dayBox" runat="server" AutoPostBack="true" CssClass="DropDown"></asp:DropDownList>
		    <span class="arrow">&nbsp;&nbsp;</span>

		    <span class="caption">Intervall</span>
		    <asp:DropDownList ID="intervalBox" runat="server" AutoPostBack="true" CssClass="DropDown"></asp:DropDownList>
		    <span class="arrow">&nbsp;&nbsp;</span>

            <asp:Panel runat="server" id="intervalDate" Visible="false" style="display:inline;">
		        <span class="caption">Datum</span>
		        <asp:DropDownList ID="year2Box" runat="server" AutoPostBack="true" CssClass="DropDown"></asp:DropDownList>
		        <span class="arrow">&nbsp;&nbsp;</span>
		        <asp:DropDownList ID="month2Box" runat="server" AutoPostBack="true" CssClass="DropDown"></asp:DropDownList>
		        <span class="arrow">&nbsp;&nbsp;</span>
		        <asp:DropDownList ID="day2Box" runat="server" AutoPostBack="true" CssClass="DropDown"></asp:DropDownList>
		        <span class="arrow">&nbsp;&nbsp;</span>
            </asp:Panel>            

		    <span class="graytitle">Priser</span>
		    <asp:DropDownList ID="pricesInclVatBox" runat="server" AutoPostBack="true" CssClass="DropDown">
				<asp:ListItem Value="0">Inkl. moms</asp:ListItem>
				<asp:ListItem Value="1" Selected>Exkl. moms</asp:ListItem>
		    </asp:DropDownList>
		    <span class="arrow">&nbsp;&nbsp;</span>

		</div>		    
		<div style="float: right; margin-top: 13px; margin-right: 10px;"><asp:LinkButton id="xmlExport" runat="server" CssClass="small" OnClick="xmlExport_onClick">XML-export</asp:LinkButton></div>
		
	</div>
	<div id="data">
     
		<div class="dataItem" style="width: 100%; height: auto; text-align: center; padding: 0px;">
			<asp:chart ID="turnoverChart" runat="server" ImageStorageMode="UseImageLocation" ImageLocation="~/_assets/charts/overview" Width="800" ImageType="Png">
                        	<Series>
                            		<asp:Series Name="currentYear"></asp:Series>
                                	<asp:Series Name="lastYear"></asp:Series>
                                	<asp:Series Name="budget"></asp:Series>
                            	</Series>
                            	<ChartAreas>
                            		<asp:ChartArea Name="Default"></asp:ChartArea>
                        	</ChartAreas>
                    	</asp:chart>
		</div>
		
            	<asp:Repeater ID="dataEntries" runat="server">
                	<ItemTemplate>
			        <div class="dataItem">
			        
			            <div>
			                <div style="float: left; font-weight: bold;"><%#DataBinder.Eval(Container.DataItem, "title") %></div>
			                <div style="float: left; width: 100%;">
			                    <div style="float: left; width: 20px;"><img src="_assets/img/<%#DataBinder.Eval(Container.DataItem, "turnOverImg")%>" alt="" width="12" height="12"/></div>
			                    <div style="float: left;">Omsättning</div>
			                    <div style="float: right;"><%#DataBinder.Eval(Container.DataItem, "turnOver", "{0:c}")%></div>
			                </div>
			                <div style="float: left; width: 100%;">
			                    <div style="float: left; width: 20px;">&nbsp;</div>
			                    <div style="float: left;">Budget</div>
			                    <div style="float: right;"><%#DataBinder.Eval(Container.DataItem, "budget", "{0:c}")%></div>
			                </div>
			                <div style="float: left; width: 100%;">
			                    <div style="float: left; width: 20px;">&nbsp;</div>
			                    <div style="float: left;">Omsättning f. år</div>
			                    <div style="float: right;"><%#DataBinder.Eval(Container.DataItem, "turnOverLastYear", "{0:c}")%></div>
			                </div>
			                <div style="float: left; width: 100%;">
			                    <div style="float: left; width: 20px;">&nbsp;</div>
			                    <div style="float: left;">Antal besökare</div>
			                    <div style="float: right;"><%#DataBinder.Eval(Container.DataItem, "noOfVisitors") %></div>
			                </div>
			                <div style="float: left; width: 100%;">
			                    <div style="float: left; width: 20px;"><img src="_assets/img/<%#DataBinder.Eval(Container.DataItem, "noOfReceiptsImg")%>" alt="" width="12" height="12"/></div>
			                    <div style="float: left;">Antal trans.</div>
			                    <div style="float: right;"><%#DataBinder.Eval(Container.DataItem, "noOfReceipts") %></div>
			                </div>
			                <div style="float: left; width: 100%;">
			                    <div style="float: left; width: 20px;">&nbsp;</div>
			                    <div style="float: left;">Antal trans. f.år</div>
			                    <div style="float: right;"><%#DataBinder.Eval(Container.DataItem, "noOfReceiptsLastYear") %></div>
			                </div>
			                <div style="float: left; width: 100%;">
			                    <div style="float: left; width: 20px;"><img src="_assets/img/<%#DataBinder.Eval(Container.DataItem, "averageReceiptAmountImg")%>" alt="" width="12" height="12" /></div>
			                    <div style="float: left;">Snittköp</div>
			                    <div style="float: right;"><%#DataBinder.Eval(Container.DataItem, "averageReceiptAmount", "{0:c}")%></div>
			                </div>
			                <div style="float: left; width: 100%;">
			                    <div style="float: left; width: 20px;">&nbsp;</div>
			                    <div style="float: left;">Snittköp f. år</div>
			                    <div style="float: right;"><%#DataBinder.Eval(Container.DataItem, "averageReceiptAmountLastYear", "{0:c}")%></div>
			                </div>
			                <div style="float: left; width: 100%;">
			                    <div style="float: left; width: 20px;"><img src="_assets/img/<%#DataBinder.Eval(Container.DataItem, "noOfItemsImg")%>" alt="" width="12" height="12" /></div>
			                    <div style="float: left;">Antal sålda artiklar</div>
			                    <div style="float: right;"><%#DataBinder.Eval(Container.DataItem, "noOfItems", "{0:0}")%></div>
			                </div>
			                <div style="float: left; width: 100%;">
			                    <div style="float: left; width: 20px;">&nbsp;</div>
			                    <div style="float: left;">Antal sålda artiklar f. år</div>
			                    <div style="float: right;"><%#DataBinder.Eval(Container.DataItem, "noOfItemsLastYear", "{0:0}")%></div>
			                </div>
			                <div style="float: left; width: 100%;">
			                    <div style="float: left; width: 20px;"><img src="_assets/img/<%#DataBinder.Eval(Container.DataItem, "noOfItemsImg")%>" alt="" width="12" height="12" /></div>
			                    <div style="float: left;">Antal returnerade artiklar</div>
			                    <div style="float: right;"><%#DataBinder.Eval(Container.DataItem, "noOfReturnItems", "{0:0}")%></div>
			                </div>
			                <div style="float: left; width: 100%;">
			                    <div style="float: left; width: 20px;">&nbsp;</div>
			                    <div style="float: left;">Antal returnerade artiklar f. år</div>
			                    <div style="float: right;"><%#DataBinder.Eval(Container.DataItem, "noOfReturnItemsLastYear", "{0:0}")%></div>
			                </div>
			                <div style="float: left; width: 100%;">
			                    <div style="float: left; width: 20px;"><img src="_assets/img/<%#DataBinder.Eval(Container.DataItem, "averageReceiptQuantityImg")%>" alt="" width="12" height="12" /></div>
			                    <div style="float: left;">Antal artiklar per köp</div>
			                    <div style="float: right;"><%#DataBinder.Eval(Container.DataItem, "averageReceiptQuantity", "{0:F2}")%></div>
			                </div>
			                <div style="float: left; width: 100%;">
				                <div style="float: left; width: 20px;">&nbsp;</div>
		                        	<div style="float: left;">Antal artiklar per köp f. år</div>
			                    <div style="float: right;"><%#DataBinder.Eval(Container.DataItem, "averageReceiptQuantityLastYear", "{0:F2}")%></div>
			                </div>
			                <div style="float: left; width: 100%;">
			                    <div style="float: left; width: 20px;">&nbsp;</div>
			                    <div style="float: left;">Totalt rabattbelopp</div>
			                    <div style="float: right;"><%#DataBinder.Eval(Container.DataItem, "totalDiscountAmount", "{0:c}")%></div>
			                </div>
			                <div style="float: left; width: 100%;">
			                    <div style="float: left; width: 20px;">&nbsp;</div>
			                    <div style="float: left;">TG</div>
			                    <div style="float: right;"><%#DataBinder.Eval(Container.DataItem, "profitPercent", "{0:0}")%>%</div>
			                </div>
			                <div style="float: left; width: 100%;">
			                    <div style="float: left; width: 20px;">&nbsp;</div>
			                    <div style="float: left;">TB (ex. moms)</div>
			                    <div style="float: right;"><%#DataBinder.Eval(Container.DataItem, "profitAmount", "{0:c}")%></div>
			                </div>
			                <div style="float: left; width: 100%;">
			                    <div style="float: left; width: 20px;">&nbsp;</div>
			                    <div style="float: left;">TB per köp (ex. moms)</div>
			                    <div style="float: right;"><%#DataBinder.Eval(Container.DataItem, "profitAmountPerReceipt", "{0:c}")%></div>
			                </div>	
			                <div style="float: left; width: 100%;">
			                    <div style="float: left; width: 20px;">&nbsp;</div>
			                    <div style="float: left;">Senaste bokförda avslut</div>
			                    <div style="float: right;"><%#DataBinder.Eval(Container.DataItem, "lastPostedJournalEndDate", "{0:yyyy-MM-dd}")%></div>
			                </div>			                
			            </div>
                        
                            <asp:chart ID="hourChart" runat="server" ImageStorageMode="UseImageLocation" ImageType="Png">
                                <Series>
                                    <asp:Series Name="hours">
                                    </asp:Series>
                                </Series>
                                
                                <ChartAreas>
                                    <asp:ChartArea Name="Default"></asp:ChartArea>
                                </ChartAreas>
                            </asp:chart>
                            
                        <div>
	                        <div style="float: left; width: 100%;">
	                            <div style="float: left;">Produktgrupper Top 10 (Antal)</div>
	                        </div>                        

                            <asp:Repeater ID="productGroupRepeater" runat="server" DataSource='<%#DataBinder.Eval(Container.DataItem, "dataProductGroupCollection") %>'>
                                <ItemTemplate>
			                        <div style="float: left; width: 100%;">
			                            <div style="font-size: 10px; float: left; width: 80%; overflow: hidden; height: 15px;"><%#DataBinder.Eval(Container.DataItem, "description") %></div>
			                            <div style="font-size: 10px; float: right;"><%#DataBinder.Eval(Container.DataItem, "quantity")%></div>
			                        </div>
                                </ItemTemplate>
                            
                            </asp:Repeater>                        
                       </div>
                       <br/><br/>
                       <div>
	                        <div style="float: left; width: 100%;">
	                            <div style="float: left;">Produkter Top 10 (Antal)</div>
	                        </div>                        

                            <asp:Repeater ID="productRepeater" runat="server" DataSource='<%#DataBinder.Eval(Container.DataItem, "dataProductCollection") %>'>
                                <ItemTemplate>
			                        <div style="float: left; width: 100%;">
			                            <div style="font-size: 10px; float: left; width: 80%; overflow: hidden; height: 15px;"><%#DataBinder.Eval(Container.DataItem, "description") %></div>
			                            <div style="font-size: 10px; float: right;"><%#DataBinder.Eval(Container.DataItem, "quantity")%></div>
			                        </div>
                                </ItemTemplate>
                            
                            </asp:Repeater>                        
                       </div>                         
			<br/><br/>
                     
                        <div>
	                        <div style="float: left; width: 100%;">
	                            <div style="float: left;">Produktgrupper Top 10 (Omsättning)</div>
	                        </div>                        

                            <asp:Repeater ID="Repeater1" runat="server" DataSource='<%#DataBinder.Eval(Container.DataItem, "dataProductGroupTurnOverCollection") %>'>
                                <ItemTemplate>
			                        <div style="float: left; width: 100%;">
			                            <div style="font-size: 10px; float: left; width: 80%; overflow: hidden; height: 15px;"><%#DataBinder.Eval(Container.DataItem, "description") %></div>
			                            <div style="font-size: 10px; float: right;"><%#DataBinder.Eval(Container.DataItem, "quantity")%></div>
			                        </div>
                                </ItemTemplate>
                            
                            </asp:Repeater>                        
                       </div>
                       <br/><br/>                       
                       <div>
	                        <div style="float: left; width: 100%;">
	                            <div style="float: left;">Produkter Top 10 (Omsättning)</div>
	                        </div>                        

                            <asp:Repeater ID="Repeater2" runat="server" DataSource='<%#DataBinder.Eval(Container.DataItem, "dataProductTurnOverCollection") %>'>
                                <ItemTemplate>
			                        <div style="float: left; width: 100%;">
			                            <div style="font-size: 10px; float: left; width: 80%; overflow: hidden; height: 15px;"><%#DataBinder.Eval(Container.DataItem, "description") %></div>
			                            <div style="font-size: 10px; float: right;"><%#DataBinder.Eval(Container.DataItem, "quantity")%></div>
			                        </div>
                                </ItemTemplate>
                            
                            </asp:Repeater>                        
                       </div>                       
		</div>			        
                </ItemTemplate>			    
            </asp:Repeater>                        
            </div>
        </div> 
        <br/>&nbsp;
    </form>
</body>
</html>


<script runat="server">

void xmlExport_onClick(Object sender, EventArgs eventArgs)
{
	string dateString = yearBox.Text+"-"+monthBox.Text+"-"+dayBox.Text;
    string dateString2 = year2Box.Text + "-" + month2Box.Text + "-" + day2Box.Text;
    Response.Redirect("xmlData.aspx?store=" + cashSiteBox.Text + "&date=" + dateString + "&interval=" + intervalBox.Text + "&date2=" + dateString2 + "&vat=" + pricesInclVatBox.Text);	

}

</script>