<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HistoryOrderList_STANDARD.ascx.cs" Inherits="Navipro.Infojet.WebInterface._taglib.HistoryOrderList_STANDARD" %>
<%@ Register TagPrefix="Infojet" TagName="Translate" src="Translate.ascx" %>
<br />

<script>

	function changeGroup(groupId)
	{
		document.pageForm.groupId.value = groupId;
		document.pageForm.submit();
	}

	function changeDate()
	{
		document.pageForm.groupId.value = "";
		document.pageForm.submit();
	}


</script>



<div id="infoLeft">
<infojet:translate id="startDate" runat="server" code="START DATE"/><br/>



<select name="startDate" class="redSelector" onchange="changeDate()">
	<%
		DateTime currentStartDate = DateTime.MinValue;
		string currentGroup = null;

		if (Request["startDate"] != null) currentStartDate = DateTime.Parse(Request["startDate"]);
		if (Request["groupId"] != null) currentGroup = Request["groupId"];

            	Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();
		System.Data.SqlClient.SqlDataReader dataReader = infojet.systemDatabase.query("SELECT DISTINCT [Leveransdatum startorder] FROM ["+infojet.systemDatabase.getTableName("FörsäljningsID")+"] WHERE [Kund] = '"+infojet.userSession.customer.no+"' ORDER BY [Leveransdatum startorder] DESC");
	
		while (dataReader.Read())
		{
			if (currentStartDate == DateTime.MinValue) currentStartDate = dataReader.GetDateTime(0);
			%><option value="<%= dataReader.GetDateTime(0).ToString("yyyy-MM-dd") %>" <% if (currentStartDate == dataReader.GetDateTime(0)) Response.Write("selected"); %>><%= dataReader.GetDateTime(0).ToString("yyyy-MM-dd") %></option><%
		}

		dataReader.Close();

	%>
</select>

<div id="table">
	<input type="hidden" name="groupId" value="<%= currentGroup %>"/>

	<table cellspacing="0" cellpadding="5" width="100%" border="0" style="padding: 5px;">
	<tr>
		<td style="background: url(_assets/img/table_bg_3.jpg);">&nbsp;</td>
		<td style="background: url(_assets/img/table_bg_5.jpg); background-position: right top;">&nbsp;</td>
	</tr>

	<%
		System.Collections.ArrayList groupList = new System.Collections.ArrayList();

 		System.Data.SqlClient.SqlDataReader dataReader2 = infojet.systemDatabase.query("SELECT [FörsäljningsID], [Benämning] FROM ["+infojet.systemDatabase.getTableName("FörsäljningsID")+"] WHERE [Kund] = '"+infojet.userSession.customer.no+"' AND [Leveransdatum startorder] = '"+currentStartDate.ToString("yyyy-MM-dd")+"' ORDER BY [Benämning]");
	
		while (dataReader2.Read())
		{
			if ((currentGroup == null) || (currentGroup == "")) currentGroup = dataReader2.GetValue(0).ToString();


				%>
				<tr>
					<td colspan="2" style="border-bottom: 1px solid #ffffff; padding-left: 10px;"><a href="javascript:changeGroup('<%= dataReader2.GetValue(0).ToString() %>');"><%= dataReader2.GetValue(1).ToString() %></a>&nbsp;</td>
				</tr>
				<%

			groupList.Add(dataReader2.GetValue(0).ToString());

		}

		dataReader2.Close();

		if (groupList.Count > 1)
		{
			%>
			<tr>
				<td colspan="2" style="border-bottom: 1px solid #ffffff; padding-left: 10px;"><a href="javascript:changeGroup('ALL');"><infojet:translate id="all" runat="server" code="ALL"/></a>&nbsp;</td>
			</tr>
			<%
		}
	%>
	<tr>
		<td style="background: url(_assets/img/table_bg_4.jpg); background-position: left bottom;">&nbsp;</td>
		<td style="background: url(_assets/img/table_bg_6.jpg); background-position: right bottom;">&nbsp;</td>
	</tr>
	

	</table>

</div>

</div>

<%
	int soldPackages = 0;
	float profit = 0;
	float balance = 0;

	if (currentGroup == "ALL")
	{
		int i = 0;
		while (i < groupList.Count)
		{
			Navipro.Newbody.Woppen.Library.SalesID salesId = new Navipro.Newbody.Woppen.Library.SalesID(infojet, groupList[i].ToString());
			soldPackages = soldPackages + salesId.historyPackages;
			profit = profit + salesId.historyProfit;
	
			i++;
		}
			

	}
	else
	{
		Navipro.Newbody.Woppen.Library.SalesID salesId = new Navipro.Newbody.Woppen.Library.SalesID(infojet, currentGroup);
		soldPackages = salesId.historyPackages;
		profit = salesId.historyProfit;
		
	}



	System.Data.SqlClient.SqlDataReader dataReader3 = infojet.systemDatabase.query("SELECT SUM(Amount) FROM ["+infojet.systemDatabase.getTableName("Detailed Cust_ Ledg_ Entry")+"] WHERE [Customer No_] = '"+infojet.userSession.customer.no+"' AND [Currency Code] = '"+infojet.userSession.customer.currencyCode+"'");
	
	while (dataReader3.Read())
	{
		balance = float.Parse(dataReader3.GetValue(0).ToString());
	}

	dataReader3.Close();

	Navipro.Infojet.Lib.WebPage detailsPage = new Navipro.Infojet.Lib.WebPage(infojet, infojet.webSite.code, "KP_INFODETAILS");
	Navipro.Infojet.Lib.WebPage spPage = new Navipro.Infojet.Lib.WebPage(infojet, infojet.webSite.code, "KP_INFOSP");
	Navipro.Infojet.Lib.WebPage scPage = new Navipro.Infojet.Lib.WebPage(infojet, infojet.webSite.code, "KP_INFOSC");
	Navipro.Infojet.Lib.WebPage balancePage = new Navipro.Infojet.Lib.WebPage(infojet, infojet.webSite.code, "KP_CUSTOMERLEDGER");
	Navipro.Infojet.Lib.WebPage invoiceHistoryPage = new Navipro.Infojet.Lib.WebPage(infojet, infojet.webSite.code, "KP_INVOICEHISTORY");
	Navipro.Infojet.Lib.WebPage orderHistoryPage = new Navipro.Infojet.Lib.WebPage(infojet, infojet.webSite.code, "KP_ORDERHISTORY");

	string invoicePageUrl = invoiceHistoryPage.getUrl();
	string orderPageUrl = orderHistoryPage.getUrl();

	string balanceStr = infojet.systemDatabase.formatCurrency(balance);
	if (balance < 0)
	{
		balance = balance * -1;
		balanceStr = "-"+infojet.systemDatabase.formatCurrency(balance);	
	}

%>

<div id="infoMain">

	<table cellspacing="0" width="100%" border="0">
	<tr>
		<td style="background: url(_assets/img/table_bg_1.jpg);" onmouseover="this.style.cursor='hand'" onmouseout="this.style.cursor='auto'" onclick="document.location.href='<%= detailsPage.getUrl()+"&startDate="+currentStartDate.ToString("yyyy-MM-dd")+"&salesId="+currentGroup %>'" style="text-align: right; background-color: #ef1c29; height: 35px; border-top-left-radius: 10px; border-bottom-left-radius: 10px; -moz-border-top-left-radius: 10px; -moz-border-bottom-left-radius: 10px;"><div style="padding-right: 3px; margin: 2px; border-right: 1px solid #ffffff;"><i><Infojet:Translate id="packageQty" runat="server" code="INFO SOLD PACK"/></i><br/><span style="font-size: 16px; font-weight: bold;"><%= soldPackages %></span></div></td>
		<td onmouseover="this.style.cursor='hand'" onmouseout="this.style.cursor='auto'" onclick="document.location.href='<%= detailsPage.getUrl()+"&startDate="+currentStartDate.ToString("yyyy-MM-dd")+"&salesId="+currentGroup %>'" style="text-align: right; background-color: #ef1c29;"><div style="padding-right: 3px; margin: 2px; border-right: 1px solid #ffffff;"><i><Infojet:Translate id="profit" runat="server" code="INFO PROFIT"/></i><br/><span style="font-size: 16px; font-weight: bold;"><%= infojet.systemDatabase.formatCurrency(profit) %></span></div></td>
		<td onmouseover="this.style.cursor='hand'" onmouseout="this.style.cursor='auto'" onclick="document.location.href='<%= spPage.getUrl()+"&startDate="+currentStartDate.ToString("yyyy-MM-dd")+"&salesId="+currentGroup %>'" style="text-align: left; background-color: #ef1c29;"><div style="padding-right: 3px; margin: 2px; border-right: 1px solid #ffffff;"><b><Infojet:Translate id="salesPersonList" runat="server" code="INFO SP LIST"/></b></div></td>
		<td onmouseover="this.style.cursor='hand'" onmouseout="this.style.cursor='auto'" onclick="document.location.href='<%= scPage.getUrl()+"&startDate="+currentStartDate.ToString("yyyy-MM-dd")+"&salesId="+currentGroup %>'" style="text-align: left; background-color: #ef1c29;"><div style="padding-right: 3px; margin: 2px;"><b><Infojet:Translate id="showCase" runat="server" code="INFO SHOWCASE"/></b></div></td>
		<td style="background: url(_assets/img/table_bg_2.jpg); background-position: right;" onmouseover="this.style.cursor='hand'" onmouseout="this.style.cursor='auto'" onclick="document.location.href='<%= balancePage.getUrl() %>'" style="text-align: left; background-color: #000000; border-top-right-radius: 10px; border-bottom-right-radius: 10px; -moz-border-top-right-radius: 10px; -moz-border-bottom-right-radius: 10px;"><div style="padding-right: 3px; margin: 2px;"><i><Infojet:Translate id="balance" runat="server" code="INFO BALANCE"/></i><br/><span style="font-size: 16px; font-weight: bold;"><%= balanceStr %></span></div></td>
	</tr>
	</table>
	<br/>


	<div style="width: 100%; background-color: #f5f5f5; color: #000000; border-top-left-radius: 10px; border-bottom-left-radius: 10px; -moz-border-top-left-radius: 10px; -moz-border-bottom-left-radius: 10px; border-top-right-radius: 10px; border-bottom-right-radius: 10px; -moz-border-top-right-radius: 10px; -moz-border-bottom-right-radius: 10px;">


	<table cellspacing="0" cellpadding="5" width="100%" border="0" style="padding: 5px;">
	<tr>
		<td style="background: url(_assets/img/table_bg_3.jpg);">&nbsp;</td>
		<td>&nbsp;</td>
		<td>&nbsp;</td>
		<td>&nbsp;</td>
		<td>&nbsp;</td>
		<td style="background: url(_assets/img/table_bg_5.jpg); background-position: right top;">&nbsp;</td>

	</tr>
	<tr>
		<td style="border-bottom: 1px solid #ffffff; padding-left: 10px;"><b><i><infojet:translate id="date" runat="server" code="DATE"/></i></b></td>
		<td style="border-bottom: 1px solid #ffffff;">&nbsp;</td>
		<td style="border-bottom: 1px solid #ffffff;">&nbsp;</td>
		<td style="border-bottom: 1px solid #ffffff;">&nbsp;</td>
		<td style="border-bottom: 1px solid #ffffff;">&nbsp;</td>
		<td style="border-bottom: 1px solid #ffffff;">&nbsp;</td>

	</tr>

	<%
		string groupSql = "([FörsäljningsID] = '"+currentGroup+"')";
		if (currentGroup == "ALL")
		{
			groupSql = "(";
			int i = 0;
			while (i < groupList.Count)
			{
				if (groupSql != "(") groupSql = groupSql + " OR ";
 				groupSql = groupSql + "[FörsäljningsID] = '"+groupList[i]+"'";
	
				i++;
			}
			groupSql = groupSql + ")";
		}


		System.Data.SqlClient.SqlDataAdapter dataAdapter = infojet.systemDatabase.dataAdapterQuery("SELECT [No_], [Order Date] FROM ["+infojet.systemDatabase.getTableName("Sales Header")+"] WHERE [Document Type] = 1 AND [Sell-to Customer No_] = '"+infojet.userSession.customer.no+"' AND "+groupSql);
		System.Data.DataSet orderDataSet = new System.Data.DataSet();
		dataAdapter.Fill(orderDataSet);	

		int k = 0;
		while (k < orderDataSet.Tables[0].Rows.Count)
		{
			string orderNo = orderDataSet.Tables[0].Rows[k].ItemArray.GetValue(0).ToString();
			DateTime orderDateTime = DateTime.Parse(orderDataSet.Tables[0].Rows[k].ItemArray.GetValue(1).ToString());

			string orderDetailsUrl = orderHistoryPage.getUrl()+"&docType=0&docNo="+orderNo;
			string orderPdfUrl = orderHistoryPage.getUrl()+"&docType=0&docNo="+orderNo+"&pdf=true";
			string packingSlipPdfUrl = orderHistoryPage.getUrl()+"&docType=4&docNo="+orderNo+"&pdf=true";

			string packageTrackingNo = "";
			string shippingAgentCode = "";
			string shippingAgentUrl = "";
			System.Data.SqlClient.SqlDataReader dataReader4 = infojet.systemDatabase.query("SELECT [Package Tracking No_], [Shipping Agent Code] FROM ["+infojet.systemDatabase.getTableName("Sales Shipment Header")+"] WHERE [Sell-to Customer No_] = '"+infojet.userSession.customer.no+"' AND [Order No_] = '"+orderNo+"'");
	
			if (dataReader4.Read())
			{
				packageTrackingNo = dataReader4.GetValue(0).ToString();
				shippingAgentCode = dataReader4.GetValue(1).ToString();
			}
	
			dataReader4.Close();

			if ((shippingAgentCode != "") && (packageTrackingNo != ""))
			{
				Navipro.Infojet.Lib.ShippingAgent shippingAgent = new Navipro.Infojet.Lib.ShippingAgent(infojet.systemDatabase, shippingAgentCode);

				shippingAgentUrl = shippingAgent.internetAddress.Replace("%1", packageTrackingNo);	
			}

			%>
			<tr>
				<td style="border-bottom: 1px solid #ffffff; padding-left: 10px;"><a style="color: #000000; text-decoration: none;" href="<%= orderDetailsUrl %>"><%= orderDateTime.ToString("yyyy-MM-dd") %></a></td>
				<td style="border-bottom: 1px solid #ffffff;"><a style="color: #000000; text-decoration: none;" href="<%= orderDetailsUrl %>"><infojet:translate id="order" runat="server" code="ORDER"/> <%= orderNo %></a></td>
				<td style="border-bottom: 1px solid #ffffff;"><a style="color: #000000; text-decoration: none;" href="<%= orderPdfUrl %>" target="_blank"><infojet:translate id="orderPdf" runat="server" code="ORDER PDF"/></a></td>
				<td style="border-bottom: 1px solid #ffffff;"><a style="color: #000000; text-decoration: none;" href="<%= orderPdfUrl %>" target="_blank"><infojet:translate id="shipmentSlip" runat="server" code="SHIPMENT SLIP"/></a></td>
				<td style="border-bottom: 1px solid #ffffff;"><a style="color: #000000; text-decoration: none;" href="<%= packingSlipPdfUrl %>" target="_blank"><infojet:translate id="packingSlip" runat="server" code="PACKING SLIP"/></a></td>
				<td style="border-bottom: 1px solid #ffffff;"><% if (shippingAgentUrl != "") { %><a style="color: #000000; text-decoration: none;" href="<%= shippingAgentUrl %>" target="_blank"><infojet:translate id="trackShipment" runat="server" code="TRACK SHIPMENT"/></a><% } %>&nbsp;</td>
			</tr>
			<%

			k++;
		}

		System.Data.SqlClient.SqlDataAdapter dataAdapter2 = infojet.systemDatabase.dataAdapterQuery("SELECT [No_], [Order No_], [Order Date] FROM ["+infojet.systemDatabase.getTableName("Sales Invoice Header")+"] WHERE [Sell-to Customer No_] = '"+infojet.userSession.customer.no+"' AND "+groupSql);
		System.Data.DataSet invoiceDataSet = new System.Data.DataSet();
		dataAdapter2.Fill(invoiceDataSet);

		k = 0;
		while (k < invoiceDataSet.Tables[0].Rows.Count)
		{
			string no = invoiceDataSet.Tables[0].Rows[k].ItemArray.GetValue(0).ToString();
			string orderNo = invoiceDataSet.Tables[0].Rows[k].ItemArray.GetValue(1).ToString();
			DateTime orderDateTime = DateTime.Parse(invoiceDataSet.Tables[0].Rows[k].ItemArray.GetValue(2).ToString());

			string invoiceDetailsUrl = orderHistoryPage.getUrl()+"&docType=2&docNo="+no;
			string invoicePdfUrl = orderHistoryPage.getUrl()+"&docType=2&docNo="+no+"&pdf=true";
			string orderPdfUrl = orderHistoryPage.getUrl()+"&docType=0&docNo="+orderNo+"&pdf=true";
			string packingSlipPdfUrl = orderHistoryPage.getUrl()+"&docType=4&docNo="+orderNo+"&pdf=true";

			string packageTrackingNo = "";
			string shippingAgentCode = "";
			string shippingAgentUrl = "";
			System.Data.SqlClient.SqlDataReader dataReader4 = infojet.systemDatabase.query("SELECT [Package Tracking No_], [Shipping Agent Code] FROM ["+infojet.systemDatabase.getTableName("Sales Shipment Header")+"] WHERE [Sell-to Customer No_] = '"+infojet.userSession.customer.no+"' AND [Order No_] = '"+orderNo+"'");
	
			if (dataReader4.Read())
			{
				packageTrackingNo = dataReader4.GetValue(0).ToString();
				shippingAgentCode = dataReader4.GetValue(1).ToString();
			}
	
			dataReader4.Close();

			if ((shippingAgentCode != "") && (packageTrackingNo != ""))
			{
				Navipro.Infojet.Lib.ShippingAgent shippingAgent = new Navipro.Infojet.Lib.ShippingAgent(infojet.systemDatabase, shippingAgentCode);

				shippingAgentUrl = shippingAgent.internetAddress.Replace("%1", packageTrackingNo);	
			}

			%>
			<tr>
				<td style="border-bottom: 1px solid #ffffff; padding-left: 10px;"><a style="color: #000000; text-decoration: none;" href="<%= invoiceDetailsUrl %>"><%= orderDateTime.ToString("yyyy-MM-dd") %></a></td>
				<td style="border-bottom: 1px solid #ffffff;"><a style="color: #000000; text-decoration: none;" href="<%= invoiceDetailsUrl %>"><infojet:translate id="invoice" runat="server" code="INVOICE"/> <%= no %></a></td>
				<td style="border-bottom: 1px solid #ffffff;"><a style="color: #000000; text-decoration: none;" href="<%= invoicePdfUrl %>" target="_blank"><infojet:translate id="orderPdf2" runat="server" code="INVOICE PDF"/></a></td>
				<td style="border-bottom: 1px solid #ffffff;"><a style="color: #000000; text-decoration: none;" href="<%= orderPdfUrl %>" target="_blank"><infojet:translate id="shipmentSlip2" runat="server" code="SHIPMENT SLIP"/></a></td>
				<td style="border-bottom: 1px solid #ffffff;"><a style="color: #000000; text-decoration: none;" href="<%= packingSlipPdfUrl %>" target="_blank"><infojet:translate id="packingSlip2" runat="server" code="PACKING SLIP"/></a></td>
				<td style="border-bottom: 1px solid #ffffff;"><% if (shippingAgentUrl != "") { %><a style="color: #000000; text-decoration: none;" href="<%= shippingAgentUrl %>" target="_blank"><infojet:translate id="trackShipment2" runat="server" code="TRACK SHIPMENT"/></a><% } %>&nbsp;</td>
			</tr>
			<%

			k++;
		}

	%>

	<tr>
		<td style="background: url(_assets/img/table_bg_4.jpg); background-position: left bottom;">&nbsp;</td>
		<td>&nbsp;</td>
		<td>&nbsp;</td>
		<td>&nbsp;</td>
		<td>&nbsp;</td>
		<td style="background: url(_assets/img/table_bg_6.jpg); background-position: right bottom;">&nbsp;</td>

	</tr>

	</table>


	</div>

</div>










<asp:Repeater runat="server" ID="historyOrderRepeater" visible="false">
<HeaderTemplate>
    <table class="tableView" width="100%">
    <tr>
        <th><Infojet:Translate runat="server" ID="no" code="NO" /></th>
        <th><Infojet:Translate runat="server" ID="date" code="DATE" /></th>
        <th><Infojet:Translate runat="server" ID="yourRef" code="CONTACT PERSON" /></th>
        <th style="text-align: right;"><Infojet:Translate runat="server" ID="orderDesc" code="ORDER DESC" /></th>
        <th style="text-align: right;"><Infojet:Translate runat="server" ID="amountInclVat" code="AMOUNT" /></th>
        <th>&nbsp;</th>
    </tr>
</HeaderTemplate>

<ItemTemplate>
    <tr>
        <td><%#DataBinder.Eval(Container.DataItem, "no")%></td>
        <td><%#DataBinder.Eval(Container.DataItem, "orderDate")%></td>
        <td><%#DataBinder.Eval(Container.DataItem, "sellToContact")%></td>
        <td style="text-align: right;"><%#DataBinder.Eval(Container.DataItem, "description")%></td>
        <td style="text-align: right;"><%#DataBinder.Eval(Container.DataItem, "amount")%></td>
        <td style="text-align: center;"><a href="<%#DataBinder.Eval(Container.DataItem, "link")%>"><Infojet:Translate runat="server" ID="amountInclVat" code="SHOW" /></a></td>
    </tr>
    
    
    <asp:repeater id="shipments" datasource='<%#DataBinder.Eval(Container.DataItem, "shipments")%>' runat="server"> 

    <ItemTemplate>
        <tr>
            <td><img src="_assets/img/sub.gif" border="0">&nbsp;<%#DataBinder.Eval(Container.DataItem, "no")%></td>
            <td><%#DataBinder.Eval(Container.DataItem, "shipmentDate")%></td>
            <td>Utleverans</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td style="text-align: center;"><a href="<%#DataBinder.Eval(Container.DataItem, "link")%>"><Infojet:Translate runat="server" ID="amountInclVat" code="SHOW" /></a></td>
        </tr>    
    </ItemTemplate>    
    
    </asp:repeater>

    
</ItemTemplate>

<AlternatingItemTemplate>

    <tr>
        <td style="background-color: #f5f5f5;"><%#DataBinder.Eval(Container.DataItem, "no")%></td>
        <td style="background-color: #f5f5f5;"><%#DataBinder.Eval(Container.DataItem, "orderDate")%></td>
        <td style="background-color: #f5f5f5;"><%#DataBinder.Eval(Container.DataItem, "sellToContact")%></td>
        <td style="background-color: #f5f5f5; text-align: right;"><%#DataBinder.Eval(Container.DataItem, "description")%></td>
        <td style="background-color: #f5f5f5; text-align: right;"><%#DataBinder.Eval(Container.DataItem, "amount")%></td>
        <td style="background-color: #f5f5f5; text-align: center;"><a href="<%#DataBinder.Eval(Container.DataItem, "link")%>"><Infojet:Translate runat="server" ID="amountInclVat" code="SHOW" /></a></td>
    </tr>
    
    
    <asp:repeater id="shipments" datasource='<%#DataBinder.Eval(Container.DataItem, "shipments")%>' runat="server"> 

    <ItemTemplate>
        <tr>
            <td style="background-color: #f5f5f5;"><img src="_assets/img/sub.gif" border="0">&nbsp;<%#DataBinder.Eval(Container.DataItem, "no")%></td>
            <td style="background-color: #f5f5f5;"><%#DataBinder.Eval(Container.DataItem, "shipmentDate")%></td>
            <td style="background-color: #f5f5f5;">Utleverans</td>
            <td style="background-color: #f5f5f5;">&nbsp;</td>
            <td style="background-color: #f5f5f5;">&nbsp;</td>
           <td style="background-color: #f5f5f5; text-align: center;"><a href="<%#DataBinder.Eval(Container.DataItem, "link")%>"><Infojet:Translate runat="server" ID="amountInclVat" code="SHOW" /></a></td>
        </tr>    
    </ItemTemplate>    
    
    </asp:repeater>

</AlternatingItemTemplate>


<FooterTemplate>
    </table>

</FooterTemplate>


</asp:Repeater>

<div style="text-align: right; width: 100%;"><asp:Label runat="server" ID="pageNav" visible="false"></asp:Label></div>
