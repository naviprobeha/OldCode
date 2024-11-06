using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Navipro.Infojet.Lib;

namespace Navipro.Newbody.Woppen.Library
{
    /// <summary>
    /// Klassen SalesID motsvarar tabellen "FörsäljningsID" i Navision, och håller inkapslad information om säljare i gruppen, antal paket, kollektioner mm. Den information som exponeras som properties cachas upp. Samma databasfrågor ställs alltså aldrig fler än en gång mot databasen så länge man arbetar med de properties som finns.
    /// </summary>
    public class SalesID
    {
        private string _code;
        private string _salesConcept;
        private int _noOfSalesPersons;
        private string _showCase;
        private string _itemSelection;
        private DateTime _orderWeek;
        private DateTime _closingDate;
        private string _description;
        private string _customerNo;
        private string _contactWebUserAccountNo;
        private string _userRegWebUserAccountNo;
        private string _subContWebUserAccountNo;
        private int _nextOrderType;

        private int _soldPackages;
        private int _showCasePackages;
        private int _returnPackages;
        private string _pageUrl;
        private bool _selected;
        private OrderItemCollection _soldShowCaseItems;
        private OrderItemCollection _cartItems;
        private OrderItemCollection _cartLines;
        private OrderItemCollection _historyLines;

        private float _totalQuantity;
        private float _totalAmount;
        private float _totalHistoryQuantity;
        private float _totalHistoryAmount;
        private float _totalSentQuantity;
        private float _totalSentAmount;
        private float _totalSentPackages;
        private float _totalProfitAmount;

        private string _contactName;
        private string _contactUserId;
        private string _contactPassword;

        private bool soldPackagesCalculated;
        private bool showCasePackagesCalculated;
        private bool returnPackagesCalculated;
        private bool historyItemsCalculated;
        private bool historyProfitCalculated;
 

        private int _historyItemsCount;
        private int _historyPackagesCount;
        private int _historyProfit;
        private bool _additionalOrder;

        private int _itemsCount;
        private int _packagesCount;
        private float _profit;
        private string _profitCurrencyCode;
        private bool _mobile;

        private Hashtable _userHistoryPackageTable;
        private Hashtable _userHistoryItemTable;
        private OrderItemCollection _sentLinesCollection;

        private WebUserAccount _contactWebUserAccount;
        private Navipro.Infojet.Lib.Infojet infojetContext;

        /// <summary>
        /// Konstruktor som initierar ett försäljnings-ID. Grundinformationen hämtas från databasen och klassen populeras med data.
        /// </summary>
        /// <param name="infojetContext">En referens till Infojet-klassen.</param>
        /// <param name="code">Nr på försäljnings-ID't.</param>
        public SalesID(Navipro.Infojet.Lib.Infojet infojetContext, string code)
		{
			//
			// TODO: Add constructor logic here
			//
            this.infojetContext = infojetContext;
				 
			this._code = code;

			getFromDatabase();
		}

        /// <summary>
        /// Konstruktor som initierar ett försäljnings-ID utifrån en rad i ett dataset från samma tabell. Ingen information hämtas från databasen.
        /// </summary>
        /// <param name="infojetContext">Referens till Infojet-klassen.</param>
        /// <param name="dataRow">En rad ur ett dataset.</param>
        public SalesID(Navipro.Infojet.Lib.Infojet infojetContext, DataRow dataRow)
		{
            this.infojetContext = infojetContext;

			this._code = dataRow.ItemArray.GetValue(0).ToString();
			this._salesConcept = dataRow.ItemArray.GetValue(1).ToString();
            this._noOfSalesPersons = int.Parse(dataRow.ItemArray.GetValue(2).ToString());
            this._showCase = dataRow.ItemArray.GetValue(3).ToString();
            this._itemSelection = dataRow.ItemArray.GetValue(4).ToString();
            this._orderWeek = DateTime.Parse(dataRow.ItemArray.GetValue(5).ToString());
            this._closingDate = DateTime.Parse(dataRow.ItemArray.GetValue(6).ToString());
            this._description = dataRow.ItemArray.GetValue(7).ToString();
            this._customerNo = dataRow.ItemArray.GetValue(8).ToString();
            this._contactWebUserAccountNo = dataRow.ItemArray.GetValue(9).ToString();
            this._userRegWebUserAccountNo = dataRow.ItemArray.GetValue(10).ToString();
            this._nextOrderType = int.Parse(dataRow.ItemArray.GetValue(11).ToString());
            this._subContWebUserAccountNo = dataRow.ItemArray.GetValue(12).ToString();

            if (dataRow.ItemArray.GetValue(13).ToString() == "1") _additionalOrder = true;

            this._profit = float.Parse(dataRow.ItemArray.GetValue(14).ToString());
            this._profitCurrencyCode = dataRow.ItemArray.GetValue(15).ToString();

            this._mobile = false;
            if (dataRow.ItemArray.GetValue(16).ToString() == "1") this._mobile = true;

            this._soldPackages = 0;
            this._showCasePackages = 0;
            this._returnPackages = 0;


        }

        /// <summary>
        /// Konstruktor som initierar ett försäljnings-ID utifrån en datareader. 
        /// </summary>
        /// <param name="infojetContext">Referens till Infojet-klassen.</param>
        /// <param name="dataReader">En öppen datareader från samma tabell.</param>
        public SalesID(Navipro.Infojet.Lib.Infojet infojetContext, SqlDataReader dataReader)
        {
            this.infojetContext = infojetContext;

            readData(dataReader);
        }

		private void getFromDatabase()
		{
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [FörsäljningsID], [Försäljningskoncept], [Antal säljare], [Visningskasse], [Artikelsortiment], [Ordervecka Slutorder], [Stängningsdatum], [Benämning], [Kund], [Contact Web User Account No_], [User Reg_ Web User Account No_], [Next Ordertype], [Sub Cont Web User Account No_], [Additional order], [Profit], [Profit Currency], [Mobile] FROM [" + infojetContext.systemDatabase.getTableName("FörsäljningsID") + "] WHERE [FörsäljningsID] = @code");

            databaseQuery.addStringParameter("@code", code, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
	
            
            if (dataReader.Read())
			{
                readData(dataReader);
			}

			dataReader.Close();

    	}

        private DataSet getSalesPersonsDataSet()
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [FÖrsäljningsID], [Web User Account] FROM [" + infojetContext.systemDatabase.getTableName("SalesID WebUserAccount") + "] WITH (NOLOCK) WHERE [FÖrsäljningsID] = @code");

            databaseQuery.addStringParameter("@code", code, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            return dataSet;

        }

        private SalesIDSalesPerson getSalesPerson(string webUserAccountNo)
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [FÖrsäljningsID], [Web User Account] FROM [" + infojetContext.systemDatabase.getTableName("SalesID WebUserAccount") + "] WHERE [FÖrsäljningsID] = @code AND [Web User Account] = @webUserAccountNo");

            databaseQuery.addStringParameter("@code", code, 20);
            databaseQuery.addStringParameter("@webUserAccountNo", webUserAccountNo, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();

            SalesIDSalesPerson salesIdSalesPerson = null;

            if (dataReader.Read())
            {
                salesIdSalesPerson = new SalesIDSalesPerson(infojetContext.systemDatabase, dataReader);

            }

            dataReader.Close();

            return salesIdSalesPerson;
        }

        private void readData(SqlDataReader dataReader)
        {
            this._code = dataReader.GetValue(0).ToString();
            this._salesConcept = dataReader.GetValue(1).ToString();
            this._noOfSalesPersons = dataReader.GetInt32(2);
            this._showCase = dataReader.GetValue(3).ToString();
            this._itemSelection = dataReader.GetValue(4).ToString();
            this._orderWeek = dataReader.GetDateTime(5);
            this._closingDate = dataReader.GetDateTime(6);
            this._description = dataReader.GetValue(7).ToString();
            this._customerNo = dataReader.GetValue(8).ToString();
            this._contactWebUserAccountNo = dataReader.GetValue(9).ToString();
            this._userRegWebUserAccountNo = dataReader.GetValue(10).ToString();
            this._nextOrderType = int.Parse(dataReader.GetValue(11).ToString());
            this._subContWebUserAccountNo = dataReader.GetValue(12).ToString();

            this._additionalOrder = false;
            if (dataReader.GetValue(13).ToString() == "1") this._additionalOrder = true;

            this._profit = float.Parse(dataReader.GetValue(14).ToString());
            this._profitCurrencyCode = dataReader.GetValue(15).ToString();

            this._mobile = false;
            if (dataReader.GetValue(16).ToString() == "1") this._mobile = true;

        }

        /// <summary>
        /// Returnerar användarkontot för kontaktpersonen i det aktuella försäljnings-ID't.
        /// </summary>
        /// <returns>WebUserAccount-klassen i Infojet. Representerar ett användarkonto.</returns>
        public WebUserAccount getContact()
        {
            if (_contactWebUserAccount == null)
            {
                _contactWebUserAccount = new WebUserAccount(infojetContext.systemDatabase, this.contactWebUserAccountNo);
            }
            return _contactWebUserAccount;

        }

        /// <summary>
        /// Returnerar ett dataset innehållande produktgrupper ifrån sortimentet assosierad med försäljnings-ID't.
        /// </summary>
        /// <returns>Ett dataset.</returns>
        public DataSet getProductGroups()
        {
            BOMComponents bomComponents = new BOMComponents();
            return bomComponents.getProductGroups(infojetContext.systemDatabase, this.itemSelection);

        }

        /// <summary>
        /// Returnerar en hash-tabell med artikelinformation (lagersaldon och priser) för samtliga artiklar i det aktuella sortimentet.
        /// </summary>
        /// <param name="infojetContext">Referens till Infojet-klassen.</param>
        /// <returns>En hash-tabell. Nykeln i tabellen är artikelnr.</returns>
        public Hashtable getItemInfo(Navipro.Infojet.Lib.Infojet infojetContext)
        {
            BOMComponents bomComponents = new BOMComponents();
            DataSet itemDataSet = bomComponents.getProducts(infojetContext.systemDatabase, this.itemSelection);

            Items items = new Items();
            return items.getItemInfo(itemDataSet, infojetContext, true, true);

        }

        /// <summary>
        /// Returnerar storleken för en given artikel.
        /// </summary>
        /// <param name="itemNo">Artikelnr</param>
        /// <returns>Storlek</returns>
        public string getItemSize(string itemNo)
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Size] FROM [" + infojetContext.systemDatabase.getTableName("Item") + "] WITH (NOLOCK) WHERE [No_] = @no");
            databaseQuery.addStringParameter("@no", itemNo, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();

            string size = "";

            if (dataReader.Read())
            {
                size = dataReader.GetValue(0).ToString();
            }

            dataReader.Close();

            return size;

        }

        private int getSoldPackages()
        {
            if (!soldPackagesCalculated)
            {
                DatabaseQuery databaseQuery2 = infojetContext.systemDatabase.prepare("SELECT SUM(Quantity) FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] WHERE [Extra 2] = @salesId");
                databaseQuery2.addStringParameter("@salesId", this.code, 20);

                SqlDataReader dataReader = databaseQuery2.executeQuery();

                if (dataReader.Read())
                {
                    if (!dataReader.IsDBNull(0)) _soldPackages = (int)float.Parse(dataReader.GetValue(0).ToString());
                }

                dataReader.Close();

                soldPackagesCalculated = true;
            }

            return this._soldPackages;


        }

        public int getSoldPackagesInclGiftCards()
        {
            Hashtable giftCardTable = SalesID.getGiftCardItems(infojetContext);

            DatabaseQuery databaseQuery2 = infojetContext.systemDatabase.prepare("SELECT [Item No_], SUM(Quantity) FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] WHERE [Extra 2] = @salesId GROUP BY [Item No_]");
            databaseQuery2.addStringParameter("@salesId", this.code, 20);

            SqlDataReader dataReader = databaseQuery2.executeQuery();

            int noOfPackages = 0;


            while (dataReader.Read())
            {

                if (!dataReader.IsDBNull(1))
                {
                    if (giftCardTable.Contains(dataReader.GetValue(0).ToString()))
                    {
                        noOfPackages = noOfPackages + (int)float.Parse(dataReader.GetValue(1).ToString()) * (int)giftCardTable[dataReader.GetValue(0).ToString()];
                    }
                    else
                    {
                        noOfPackages = noOfPackages + (int)float.Parse(dataReader.GetValue(1).ToString());
                    }
                }


            }

            dataReader.Close();

            return noOfPackages;


        }


        private int getSoldPackages(int itemStatus)
        {
            DatabaseQuery databaseQuery2 = infojetContext.systemDatabase.prepare("SELECT SUM(c.Quantity) FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] c, [" + infojetContext.systemDatabase.getTableName("Item") + "] i WITH (NOLOCK) WHERE c.[Extra 2] = @salesId AND c.[Item No_] = i.[No_] AND i.[Artikelstatus] = @itemStatus");
            databaseQuery2.addStringParameter("@salesId", this.code, 20);
            databaseQuery2.addStringParameter("@itemStatus", itemStatus.ToString(), 20);

            SqlDataReader dataReader = databaseQuery2.executeQuery();

            int itemStatusSoldPackages = 0;

            if (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0)) itemStatusSoldPackages = (int)float.Parse(dataReader.GetValue(0).ToString());
            }

            dataReader.Close();




            return itemStatusSoldPackages;


        }


        private int getShowCasePackages()
        {
            if (nextOrderType > 2)
            {
                _showCasePackages = 0;
                return _showCasePackages;
            }

            if (!showCasePackagesCalculated)
            {
                //this._showCasePackages = (int)countShowCaseItems();
                //BOMComponents bomComponents = new BOMComponents();
                if (this.showCase != "")
                {
                    this._showCasePackages = this.countShowCaseDeliveredProducts("");
                    showCasePackagesCalculated = true;
                }
            }

            return this._showCasePackages;
        }

        private int getReturnPackages()
        {
            if (nextOrderType > 2)
            {
                _returnPackages = 0;
                return _returnPackages;
            }

            if (!returnPackagesCalculated)
            {
                if (showCase != "")
                {
                    if (!showCasePackagesCalculated) getShowCasePackages();
                    this._returnPackages = this._showCasePackages - (int)countShowCaseItems();
                    returnPackagesCalculated = true;
                }
            }

            return this._returnPackages;
        }

        private int getTotalPackages()
        {
            if (!soldPackagesCalculated) getSoldPackages();
            if (!showCasePackagesCalculated) getShowCasePackages();
            if (!returnPackagesCalculated) getReturnPackages();
            return this._soldPackages - this._showCasePackages + this._returnPackages;
        }

        /// <summary>
        /// Returnerar samtliga säljare i det aktuella försäljnings-ID't. 
        /// </summary>
        /// <param name="infojet">Referens till Infojet-klassen.</param>
        /// <returns>En hårt typad lista som består av SalesIDSalesPerson-objekt.</returns>
        public SalesIDSalesPersonCollection getSalesPersons(Navipro.Infojet.Lib.Infojet infojet)
        {
            SalesIDSalesPersonCollection salesIdSalesPersonCollection = new SalesIDSalesPersonCollection();

            DataSet dataSet = getSalesPersonsDataSet();
            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                SalesIDSalesPerson salesIdSalesPerson = new SalesIDSalesPerson(infojetContext.systemDatabase, dataSet.Tables[0].Rows[i]);
                salesIdSalesPerson.setTranslationHelper(infojet);
                if (this.isPrimaryContactPerson(salesIdSalesPerson.webUserAccountNo)) salesIdSalesPerson.name = salesIdSalesPerson.name + " (" + infojet.translate("CONTACT PERSON") + ")";
                if (this.isSubContactPerson(salesIdSalesPerson.webUserAccountNo)) salesIdSalesPerson.name = salesIdSalesPerson.name + " (" + infojet.translate("SUB CONTACT PERSON") + ")";
                salesIdSalesPersonCollection.Add(salesIdSalesPerson);

                i++;
            }

            return salesIdSalesPersonCollection;

        }

        /// <summary>
        /// Returnerar endast aktiva säljare i det aktuella försäljnings-ID't. 
        /// </summary>
        /// <param name="infojet">Referens till Infojet-klassen.</param>
        /// <returns>En hårt typad lista som består av SalesIDSalesPerson-objekt.</returns>       
        public SalesIDSalesPersonCollection getActiveSalesPersons(Navipro.Infojet.Lib.Infojet infojet)
        {
            SalesIDSalesPersonCollection salesIdSalesPersonCollection = new SalesIDSalesPersonCollection();

            DataSet dataSet = getSalesPersonsDataSet();
            int i = 0;
            int count = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                SalesIDSalesPerson salesIdSalesPerson = new SalesIDSalesPerson(infojetContext.systemDatabase, dataSet.Tables[0].Rows[i]);
                WebUserAccount webUserAccount = new WebUserAccount(infojet.systemDatabase, salesIdSalesPerson.webUserAccountNo);
                count++;
                if (webUserAccount.companyRole != "X")
                {
                    salesIdSalesPerson.setTranslationHelper(infojet);
                    if (this.isPrimaryContactPerson(salesIdSalesPerson.webUserAccountNo)) salesIdSalesPerson.name = salesIdSalesPerson.name + " (" + infojet.translate("CONTACT PERSON") + ")";
                    if (this.isSubContactPerson(salesIdSalesPerson.webUserAccountNo)) salesIdSalesPerson.name = salesIdSalesPerson.name + " (" + infojet.translate("SUB CONTACT PERSON") + ")";
                    salesIdSalesPersonCollection.Add(salesIdSalesPerson);
                }
                i++;
            }

            if (userHistoryPackageTable != null) salesIdSalesPersonCollection.applySalesPersonHistory(userHistoryPackageTable, userHistoryItemTable);
            return salesIdSalesPersonCollection;

        }

        /// <summary>
        /// Returnerar första bästa aktiva säljare i det aktuella försäljnings-ID't.
        /// </summary>
        /// <param name="infojet">Referens till Infojet-klassen.</param>
        /// <returns>Säljaren i form av ett SalesIDSalesPerson-objekt.</returns>
        public SalesIDSalesPerson getFirstAvailableSalesPerson(Navipro.Infojet.Lib.Infojet infojet)
        {

            DataSet dataSet = getSalesPersonsDataSet();
            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                SalesIDSalesPerson salesIdSalesPerson = new SalesIDSalesPerson(infojetContext.systemDatabase, dataSet.Tables[0].Rows[i]);
                WebUserAccount webUserAccount = new WebUserAccount(infojet.systemDatabase, salesIdSalesPerson.webUserAccountNo);
                if (webUserAccount.companyRole == "X")
                {
                    return salesIdSalesPerson;
                }
                i++;
            }

            return null;

        }

        /// <summary>
        /// Kontrollerar om varukorgen för en given säljare är klarmarkerad.
        /// </summary>
        /// <param name="webUserAccountNo">Användarkontonr för säljaren.</param>
        /// <returns>True eller False</returns>
        public bool checkReleasedCart(string webUserAccountNo)
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web User Account No] FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] WHERE [Web User Account No] = @webUserAccountNo AND [Extra 2] = @salesId AND [Extra 3] = '1'");
            databaseQuery.addStringParameter("@webUserAccountNo", webUserAccountNo, 20);
            databaseQuery.addStringParameter("@salesId", this.code, 20);

            bool found = false;

            SqlDataReader sqlDataReader = databaseQuery.executeQuery();
            if (sqlDataReader.Read())
            {
                found = true;
            }

            sqlDataReader.Close();

            return found;

        }

        /// <summary>
        /// Kalkylerar rankningen för en given säljare i försäljnings-ID't. 
        /// </summary>
        /// <param name="webUserAccountNo">Användarkontonr för säljaren.</param>
        /// <returns>Ett heltal där 1 har sålt flest paket.</returns>
        public int getRanking(SalesIDSalesPersonCollection salesPersonCollection, string webUserAccountNo, Hashtable soldPackagesTable)
        {
            int i = 0;

            int ranking = salesPersonCollection.Count;

            SalesIDSalesPerson salesPerson = new SalesIDSalesPerson(infojetContext.systemDatabase, this.code, webUserAccountNo);

            while (i < salesPersonCollection.Count)
            {
                SalesIDSalesPerson salesIdSalesPerson = salesPersonCollection[i];
                if (salesPerson.webUserAccountNo != salesIdSalesPerson.webUserAccountNo)
                {
                    int soldPackages1 = 0;
                    if (soldPackagesTable.Contains(salesPerson.webUserAccountNo)) soldPackages1 = (int)soldPackagesTable[salesPerson.webUserAccountNo];

                    int soldPackages2 = 0;
                    if (soldPackagesTable.Contains(salesIdSalesPerson.webUserAccountNo)) soldPackages2 = (int)soldPackagesTable[salesIdSalesPerson.webUserAccountNo];

                    if (soldPackages1 >= soldPackages2) ranking--;
                }
                i++;
            }

            return ranking;
        }

        /// <summary>
        /// Returnerar en lista innehållande samtliga orderrader för försäljnings-ID't. Räknar även ut totalt antal sålda paket samt motsvarande orderbelopp. Visningspaket är ej avräknade.
        /// </summary>
        /// <param name="totalQuantity">Totalt antal sålda paket.</param>
        /// <param name="totalAmount">Orderbelopp för de sålda paketen.</param>
        /// <returns>En hårt typad lista bestående av OrderItem-objekt.</returns>
        public OrderItemCollection getSalesIdCartLines(out float totalQuantity, out float totalAmount)
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();
            Hashtable giftCardTable = SalesID.getGiftCardItems(infojet);

            OrderItemCollection orderItemCollection = new OrderItemCollection();

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Item No_], SUM([Quantity]) FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] WHERE [Extra 2] = @salesId GROUP BY [Item No_]");
            databaseQuery.addStringParameter("@salesId", this.code, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            totalAmount = 0;
            totalQuantity = 0;

            Items items = new Items();
            Hashtable itemInfoTable = items.getItemInfo(dataSet, infojetContext, true, false);


            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                Item item = new Item(infojetContext.systemDatabase, dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString());

                WebCartLine webCartLine = new WebCartLine(null, null, item);

                webCartLine.quantity = float.Parse(dataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString());
                if (itemInfoTable.Contains(item.no))
                {
                    webCartLine.unitPrice = ((ItemInfo)itemInfoTable[item.no]).unitPrice;
                }


                OrderItem orderItem = new OrderItem(webCartLine);
                orderItem.description = item.description;

                if (nextOrderType < 3)
                {
                    SalesIdShowCase salesIdShowCase = new SalesIdShowCase(infojetContext.systemDatabase, code, orderItem.itemNo);
                    if (salesIdShowCase.quantityReceived > 0)
                    {
                        orderItem.quantityShowCase = salesIdShowCase.quantityReceived;
                    }
                    else
                    {
                        //BOMComponents bomComponents = new BOMComponents();
                        //orderItem.quantityShowCase = bomComponents.getItemQuantity(infojetContext.systemDatabase, this.showCase, orderItem.itemNo);
                        orderItem.quantityShowCase = this.countShowCaseDeliveredProducts(orderItem.itemNo);
                    }
                }

                orderItem.quantityToOrder = orderItem.quantity - orderItem.quantityShowCase;
                if (orderItem.quantityToOrder < 0) orderItem.quantityToOrder = 0;

                orderItem.amount = orderItem.unitPrice * orderItem.quantityToOrder;
                //orderItem.amount = orderItem.amount * item.getVatFactor(infojetContext.userSession.customer);
                //orderItem.unitPrice = orderItem.unitPrice * item.getVatFactor(infojetContext.userSession.customer);

                if (giftCardTable.Contains(webCartLine.itemNo))
                {
                    orderItem.quantityPackages = orderItem.quantityPackages + webCartLine.quantity * (int)giftCardTable[webCartLine.itemNo];
                }
                else
                {
                    orderItem.quantityPackages = orderItem.quantityPackages + webCartLine.quantity;
                }


                orderItemCollection.Add(orderItem);

                totalQuantity = totalQuantity + orderItem.quantityToOrder;
                totalAmount = totalAmount + orderItem.amount;

                i++;
            }

            return orderItemCollection;
        }

        /// <summary>
        /// Returnerar en lista innehållande samtliga inskickade orderrader för aktuellt försäljnings-ID. Räknar även ut totalt antal sålda paket samt motsvarande orderbelopp. Visningspaket avräknas ej.
        /// </summary>
        /// <param name="totalQuantity">Totalt antal sålda paket.</param>
        /// <param name="totalAmount">Orderbelopp för de sålda paketen.</param>
        /// <returns>En hårt typad lista bestående av OrderItem-objekt.</returns>
        public OrderItemCollection getSalesIdSentCartLines(out float totalQuantity, out float totalAmount)
        {
            OrderItemCollection orderItemCollection = new OrderItemCollection();

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Item No_], SUM([Ordered Qty]) FROM [" + infojetContext.systemDatabase.getTableName("Web Order Line") + "] WHERE [FörsäljningsID] = @salesId AND [Förpackningsmaterial] = 0 GROUP BY [Item No_] ORDER BY [Item No_]");
            databaseQuery.addStringParameter("@salesId", this.code, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            totalAmount = 0;
            totalQuantity = 0;

            Items items = new Items();
            Hashtable itemInfoTable = items.getItemInfo(dataSet, infojetContext, true, false);


            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                Item item = new Item(infojetContext.systemDatabase, dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString());

                WebCartLine webCartLine = new WebCartLine(null, null, item);

                webCartLine.quantity = float.Parse(dataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString());
                if (itemInfoTable.Contains(item.no))
                {
                    webCartLine.unitPrice = ((ItemInfo)itemInfoTable[item.no]).unitPrice;
                }


                OrderItem orderItem = new OrderItem(webCartLine);
                orderItem.description = item.description;

                orderItem.amount = orderItem.unitPrice * orderItem.quantity;
                //orderItem.amount = orderItem.amount * item.getVatFactor(infojetContext.userSession.customer);
                //orderItem.unitPrice = orderItem.unitPrice * item.getVatFactor(infojetContext.userSession.customer);

                orderItemCollection.Add(orderItem);

                totalQuantity = totalQuantity + orderItem.quantity;
                totalAmount = totalAmount + orderItem.amount;

                i++;
            }

            databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Item No_], SUM([Quantity]) FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] WHERE [Extra 2] = @salesId GROUP BY [Item No_] ORDER BY [Item No_]");
            databaseQuery.addStringParameter("@salesId", this.code, 20);

            sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            itemInfoTable = items.getItemInfo(dataSet, infojetContext, true, false);


            i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                Item item = new Item(infojetContext.systemDatabase, dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString());

                WebCartLine webCartLine = new WebCartLine(null, null, item);

                webCartLine.quantity = float.Parse(dataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString());
                if (itemInfoTable.Contains(item.no))
                {
                    webCartLine.unitPrice = ((ItemInfo)itemInfoTable[item.no]).unitPrice;
                }


                OrderItem orderItem = new OrderItem(webCartLine);
                orderItem.description = item.description;

                if (orderItem.quantityToOrder < 0) orderItem.quantityToOrder = 0;

                orderItem.amount = orderItem.unitPrice * orderItem.quantity;
                //orderItem.amount = orderItem.amount * item.getVatFactor(infojetContext.userSession.customer);
                //orderItem.unitPrice = orderItem.unitPrice * item.getVatFactor(infojetContext.userSession.customer);

                orderItemCollection.Add(orderItem);

                totalQuantity = totalQuantity + orderItem.quantity;
                totalAmount = totalAmount + orderItem.amount;

                i++;
            }




            return orderItemCollection;
        }

        /// <summary>
        /// Returnerar en lista på fakturerade orderrader för aktuellt försäljnings-ID. Totalt antal paket samt orderbelopp beräknas också. Visningspaket är avräknade. 
        /// </summary>
        /// <param name="totalQuantity">Totalt antal fakturerade paket.</param>
        /// <param name="totalAmount">Fakturerat belopp.</param>
        /// <returns>En hårt typad lista bestående av OrderItem-objekt.</returns>
        public OrderItemCollection getSalesIdHistoryLines(out float totalQuantity, out float totalAmount, out float totalProfit)
        {
            OrderItemCollection orderItemCollection = new OrderItemCollection();
            Hashtable giftCardTable = SalesID.getGiftCardItems(infojetContext);


            totalAmount = 0;
            totalQuantity = 0;
            totalProfit = 0;

            SqlDataAdapter dataAdapter = infojetContext.systemDatabase.dataAdapterQuery("SELECT [No_] FROM [" + infojetContext.systemDatabase.getTableName("Sales Invoice Header") + "] WITH (NOLOCK) WHERE [FörsäljningsID] = '" + this.code + "'");

            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            Items items = new Items();


            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                string docNo = dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();

                SqlDataAdapter lineAdapter = infojetContext.systemDatabase.dataAdapterQuery("SELECT l.[No_], SUM(l.[Quantity]) FROM [" + infojetContext.systemDatabase.getTableName("Sales Invoice Line") + "] l, [" + infojetContext.systemDatabase.getTableName("Item") + "] i WITH (NOLOCK) WHERE l.[Document No_] = '" + docNo + "' AND l.[Type] = 2  AND l.[Unit of Measure Code] = 'PKT' AND l.[No_] = i.[No_] GROUP BY l.[No_]");
                DataSet lineDataSet = new DataSet();
                lineAdapter.Fill(lineDataSet);

                Hashtable itemInfoTable = items.getItemInfo(lineDataSet, infojetContext, true, false);

                int j = 0;
                while (j < lineDataSet.Tables[0].Rows.Count)
                {

                    Item item = new Item(infojetContext.systemDatabase, lineDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString());

                    WebCartLine webCartLine = new WebCartLine(null, null, item);

                    webCartLine.quantity = float.Parse(lineDataSet.Tables[0].Rows[j].ItemArray.GetValue(1).ToString());
                    if (itemInfoTable.Contains(item.no))
                    {
                        webCartLine.unitPrice = ((ItemInfo)itemInfoTable[item.no]).unitPrice;
                    }


                    OrderItem orderItem = new OrderItem(webCartLine);
                    orderItem.description = item.description;

                    orderItem.amount = orderItem.unitPrice * orderItem.quantity;

                    if (giftCardTable.Contains(webCartLine.itemNo))
                    {
                        orderItem.quantityPackages = orderItem.quantityPackages + webCartLine.quantity * (int)giftCardTable[webCartLine.itemNo];
                        totalProfit = totalProfit + ((int)giftCardTable[webCartLine.itemNo] * webCartLine.quantity * profit);
                    }
                    else
                    {
                        orderItem.quantityPackages = orderItem.quantityPackages + webCartLine.quantity;
                        totalProfit = totalProfit + (webCartLine.quantity * profit);
                    }

                    orderItemCollection.Add(orderItem);

                    totalQuantity = totalQuantity + orderItem.quantity;
                    totalAmount = totalAmount + orderItem.amount;

                    j++;
                }

                i++;
            }

            return orderItemCollection;
        }

        /// <summary>
        /// Uppdaterar samtliga orderrader i varukorgen med aktuellt sessions-id inför sändnings av ordern.
        /// </summary>
        /// <param name="sessionId">Aktuellt sessions-id.</param>
        public void applySessionIdToSalesIdCart(string sessionId)
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("UPDATE [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] SET [Session ID] = '' WHERE [Session ID] = @sessionId");
            databaseQuery.addStringParameter("@sessionId", sessionId, 250);

            databaseQuery = infojetContext.systemDatabase.prepare("UPDATE [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] SET [Session ID] = @sessionId WHERE [Extra 2] = @salesId");
            databaseQuery.addStringParameter("@salesId", this.code, 20);
            databaseQuery.addStringParameter("@sessionId", sessionId, 250);

            databaseQuery.execute();

        }

        /// <summary>
        /// Raderar samtliga orderrader i varukorgen för aktuellt försäljnings-ID.
        /// </summary>
        public void deleteCartLines()
        {
            infojetContext.systemDatabase.nonQuery("DELETE FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] WHERE [Extra 2] = '"+code+"'");
        }

        /// <summary>
        /// Returnerar en lista på visningspaket assosierade med aktuellt försäljnings-ID.
        /// </summary>
        /// <param name="includeReceivedQty">Anger om hänsyn skall tas till inrapporterade visningspaket från kontaktpersonen.</param>
        /// <returns>En hårt typad lista bestående av OrderItem-objekt.</returns>
        public OrderItemCollection getShowCaseItems(bool includeReceivedQty)
        {
            OrderItemCollection showCaseItemCollection = new OrderItemCollection();

            if (nextOrderType >= 3) return showCaseItemCollection;

            //BOMComponents bomComponents = new BOMComponents();
            //DataSet showCaseDataSet = bomComponents.getProducts(infojetContext.systemDatabase, this.showCase);

            OrderItemCollection orderItemCollection = getShowCaseCollection();

            int i = 0;
            //while (i < showCaseDataSet.Tables[0].Rows.Count)
            while (i < orderItemCollection.Count) 
            {
                OrderItem orderItem = orderItemCollection[i];
                //string itemNo = showCaseDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();
                //string description = showCaseDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString();
                //float quantity = float.Parse(showCaseDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString());
                //float quantity = this.countShowCaseOriginalProducts(itemNo);

                DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT SUM([Quantity]) FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] WHERE [Extra 2] = @salesId AND [Item No_] = @itemNo");
                databaseQuery.addStringParameter("@salesId", this.code, 20);
                databaseQuery.addStringParameter("@itemNo", orderItem.itemNo, 20);

                float soldQuantity = 0;

                SqlDataReader dataReader = databaseQuery.executeQuery();
                if (dataReader.Read())
                {
                    if (!dataReader.IsDBNull(0)) soldQuantity = float.Parse(dataReader.GetValue(0).ToString());
                }

                dataReader.Close();
              

                //OrderItem orderItem = new OrderItem();
                //orderItem.itemNo = itemNo;
                //orderItem.description = description;
                //orderItem.quantity = quantity;
                orderItem.soldQuantity = soldQuantity;
                orderItem.remainingQuantity = this.countShowCaseDeliveredProducts(orderItem.itemNo) - orderItem.soldQuantity;
                if (orderItem.remainingQuantity < 0) orderItem.remainingQuantity = 0;

                if (includeReceivedQty)
                {
                    SalesIdShowCase salesIdShowCase = new SalesIdShowCase(infojetContext.systemDatabase, code, orderItem.itemNo);
                    if (salesIdShowCase.isEntered)
                    {
                        orderItem.remainingQuantity = salesIdShowCase.quantityReceived;
                        orderItem.quantity2 = salesIdShowCase.qtyPackingMaterial;
                        orderItem.quantity3 = salesIdShowCase.qtyPackingSlips;
                        orderItem.method = salesIdShowCase.method;

                        //if (orderItem.remainingQuantity == 0) orderItem.remainingQuantity = orderItem.quantity;

                    }
                }

                showCaseItemCollection.Add(orderItem);


                i++;
            }

            return showCaseItemCollection;
        }

        /// <summary>
        /// Returnerar en lista på ej sålda visningspaket för aktuellt försäljnings-I baserat på insänd order.
        /// </summary>
        /// <returns>En hårt typad lista bestående av OrderItem-objekt.</returns>
        public OrderItemCollection getHistoryShowCaseItems()
        {
            OrderItemCollection showCaseItemCollection = new OrderItemCollection();

            OrderItemCollection orderItemCollection = getShowCaseCollection();
            Hashtable soldQuantityTable = getSoldQuantities();
            Hashtable deliveredTable = getShowCaseDeliveredProductList();

            int i = 0;
            while (i < orderItemCollection.Count)
            {
                OrderItem orderItem = orderItemCollection[i];



                if (soldQuantityTable[orderItem.itemNo] != null) orderItem.soldQuantity = (float)soldQuantityTable[orderItem.itemNo];
                int deliveredQty = 0;
                if (deliveredTable[orderItem.itemNo] != null) deliveredQty = (int)deliveredTable[orderItem.itemNo];

                orderItem.remainingQuantity = deliveredQty - orderItem.soldQuantity;
                if (orderItem.remainingQuantity < 0) orderItem.remainingQuantity = 0;


                showCaseItemCollection.Add(orderItem);


                i++;
            }

            return showCaseItemCollection;
        }

        /// <summary>
        /// Returnerar en lista med samtliga levererade visningspaket kompletterad med information som KP rapporterat. Listan används som underlag för KP att rapportera in gruppens visningspaket.
        /// </summary>
        /// <returns>En hårt typad lista i form av OrderItem-objekt.</returns>
        public OrderItemCollection getShowCaseItemForReporting()
        {

            OrderItemCollection showCaseItemCollection = new OrderItemCollection();

            if (nextOrderType >= 3) return showCaseItemCollection;

            //BOMComponents bomComponents = new BOMComponents();
            //DataSet showCaseDataSet = bomComponents.getProducts(infojetContext.systemDatabase, this.showCase);
            OrderItemCollection orderItemCollection = getShowCaseCollection();

            int i = 0;
            //while (i < showCaseDataSet.Tables[0].Rows.Count)
            while (i < orderItemCollection.Count)
            {
                OrderItem orderItem = orderItemCollection[i];

                //string itemNo = showCaseDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();
                //string description = showCaseDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString();
                //float quantity = float.Parse(showCaseDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString());
                //float quantity = this.countShowCaseOriginalProducts(itemNo);

                DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT SUM([Quantity]) FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] WHERE [Extra 2] = @salesId AND [Item No_] = @itemNo");
                databaseQuery.addStringParameter("@salesId", this.code, 20);
                databaseQuery.addStringParameter("@itemNo", orderItem.itemNo, 20);

                float soldQuantity = 0;

                SqlDataReader dataReader = databaseQuery.executeQuery();
                if (dataReader.Read())
                {
                    if (!dataReader.IsDBNull(0)) soldQuantity = float.Parse(dataReader.GetValue(0).ToString());
                }

                dataReader.Close();


                //OrderItem orderItem = new OrderItem();
                //orderItem.itemNo = itemNo;
                //orderItem.description = description;
                //orderItem.quantity = quantity;
                orderItem.soldQuantity = soldQuantity;
                orderItem.remainingQuantity = this.countShowCaseOriginalProducts(orderItem.itemNo);

                SalesIdShowCase salesIdShowCase = new SalesIdShowCase(infojetContext.systemDatabase, code, orderItem.itemNo);
                if (salesIdShowCase.isEntered)
                {
                    orderItem.remainingQuantity = salesIdShowCase.quantityReceived;
                    orderItem.quantity2 = salesIdShowCase.qtyPackingMaterial;
                    orderItem.quantity3 = salesIdShowCase.qtyPackingSlips;
                    orderItem.method = salesIdShowCase.method;

                    //if (orderItem.remainingQuantity == 0) orderItem.remainingQuantity = orderItem.quantity;

                }

                showCaseItemCollection.Add(orderItem);


                i++;
            }

            return showCaseItemCollection;
        }

        /// <summary>
        /// Returnerar en lista på sålda visningspaket.
        /// </summary>
        /// <returns>En hårt typad lista bestående av OrderItem-objekt.</returns>
        public OrderItemCollection getSoldShowCaseItems()
        {
            OrderItemCollection showCaseItemCollection = new OrderItemCollection();

            //BOMComponents bomComponents = new BOMComponents();
            //DataSet showCaseDataSet = bomComponents.getProducts(infojetContext.systemDatabase, this.showCase);

            OrderItemCollection orderItemCollection = getShowCaseCollection();

            int i = 0;
            //while (i < showCaseDataSet.Tables[0].Rows.Count)
            while (i < orderItemCollection.Count)
            {
                OrderItem orderItem = orderItemCollection[i];

                //string itemNo = showCaseDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();
                //string description = showCaseDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString();
                //float quantity = float.Parse(showCaseDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString());
                orderItem.quantity = this.countShowCaseDeliveredProducts(orderItem.itemNo);

                DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT SUM([Quantity]) FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] WHERE [Extra 2] = @salesId AND [Item No_] = @itemNo");
                databaseQuery.addStringParameter("@salesId", this.code, 20);
                databaseQuery.addStringParameter("@itemNo", orderItem.itemNo, 20);

                float soldQuantity = 0;

                SqlDataReader dataReader = databaseQuery.executeQuery();
                if (dataReader.Read())
                {
                    if (!dataReader.IsDBNull(0)) soldQuantity = float.Parse(dataReader.GetValue(0).ToString());
                }

                dataReader.Close();



                //OrderItem orderItem = new OrderItem();
                orderItem.salesId = code;
                //orderItem.itemNo = itemNo;
                //orderItem.description = description;
                //orderItem.quantity = quantity;
                orderItem.soldQuantity = soldQuantity;
                orderItem.remainingQuantity = orderItem.quantity - orderItem.soldQuantity;
                if (orderItem.remainingQuantity < 0) orderItem.remainingQuantity = 0;

                if (orderItem.remainingQuantity == 0)
                {
                    SalesIdShowCase salesIdShowCase = new SalesIdShowCase(infojetContext.systemDatabase, code, orderItem.itemNo);
                    orderItem.remainingQuantity = salesIdShowCase.quantityReceived;
                    orderItem.quantity2 = salesIdShowCase.qtyPackingMaterial;
                    orderItem.quantity3 = salesIdShowCase.qtyPackingSlips;
                    orderItem.method = salesIdShowCase.method;

                    if (orderItem.remainingQuantity == 0) orderItem.remainingQuantity = orderItem.quantity;
                    showCaseItemCollection.Add(orderItem);
                }


                i++;
            }

            return showCaseItemCollection;
        }

        /// <summary>
        /// Beräknar totalt antal levererade visningspaket.
        /// </summary>
        /// <returns>Ett heltal i form av ett flyttal.</returns>
        public float countShowCaseItems()
        {
            float showCaseItems = 0;

            OrderItemCollection orderItemCollection = new OrderItemCollection();


            //BOMComponents bomComponents = new BOMComponents();
            //DataSet showCaseDataSet = bomComponents.getProducts(infojetContext.systemDatabase, this.showCase);
            OrderItemCollection showCaseItemCollection = getShowCaseCollection();

            int i = 0;
            //while (i < showCaseDataSet.Tables[0].Rows.Count)
            while (i < showCaseItemCollection.Count)
            {
                //string itemNo = showCaseDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();
                //string description = showCaseDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString();
                //float quantity = float.Parse(showCaseDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString());

                OrderItem orderItem = showCaseItemCollection[i];
                orderItem.quantity = this.countShowCaseDeliveredProducts(orderItem.itemNo);

                DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT SUM([Quantity]) FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] WHERE [Extra 2] = @salesId AND [Item No_] = @itemNo");
                databaseQuery.addStringParameter("@salesId", this.code, 20);
                databaseQuery.addStringParameter("@itemNo", orderItem.itemNo, 20);

                float soldQuantity = 0;

                SqlDataReader dataReader = databaseQuery.executeQuery();
                if (dataReader.Read())
                {
                    if (!dataReader.IsDBNull(0)) soldQuantity = float.Parse(dataReader.GetValue(0).ToString());
                }

                dataReader.Close();


                float soldShowCaseItem = soldQuantity;
                if (soldShowCaseItem > orderItem.quantity) soldShowCaseItem = orderItem.quantity;

                showCaseItems = showCaseItems + soldShowCaseItem;    


                i++;
            }

            return showCaseItems;
        }

        /// <summary>
        /// Beräknar totalt antal fakturerade paket för en viss artikelstatus..
        /// </summary>
        /// <param name="itemStatus">Artikelstatus, heltal.</param>
        /// <returns>Ett heltal.</returns>
        public int countHistoryItems(int itemStatus)
        {
            int quantity = 0;

            SqlDataAdapter dataAdapter = infojetContext.systemDatabase.dataAdapterQuery("SELECT [No_] FROM [" + infojetContext.systemDatabase.getTableName("Sales Invoice Header") + "] WITH (NOLOCK) WHERE [FörsäljningsID] = '" + this.code + "'");

            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                string docNo = dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();

                SqlDataReader sqlDataReader = infojetContext.systemDatabase.query("SELECT SUM(l.[Quantity]) FROM [" + infojetContext.systemDatabase.getTableName("Sales Invoice Line") + "] l WITH (NOLOCK), [" + infojetContext.systemDatabase.getTableName("Item") + "] i WITH (NOLOCK) WHERE l.[Document No_] = '" + docNo + "' AND l.[Type] = 2  AND l.[Unit of Measure Code] = 'PKT' AND l.[No_] = i.[No_] AND i.[Artikelstatus] = '" + itemStatus + "'");
                if (sqlDataReader.Read())
                {
                    if (!sqlDataReader.IsDBNull(0))
                    {
                        quantity = quantity + (int)float.Parse(sqlDataReader.GetValue(0).ToString());
                    }
                }
                sqlDataReader.Close();

                i++;
            }

            return quantity;


        }

        /// <summary>
        /// Returnerar summan av totalt antal sålda paket för aktuellt försäljnings-ID, inkl. ej inskickade order.
        /// </summary>
        /// <returns>Heltal.</returns>
        public void countHistoryItems()
        {

            /*
             
            SqlDataReader sqlDataReader = infojetContext.systemDatabase.query("SELECT SUM([Ordered Qty]) FROM [" + infojetContext.systemDatabase.getTableName("Web Order Line") + "] WITH (NOLOCK) WHERE [FörsäljningsID] = '" + this.code + "' AND [Förpackningsmaterial] = 0");
            if (sqlDataReader.Read())
            {
                if (!sqlDataReader.IsDBNull(0)) quantity = quantity + (int)float.Parse(sqlDataReader.GetValue(0).ToString());
            }
            sqlDataReader.Close();

            sqlDataReader = infojetContext.systemDatabase.query("SELECT SUM([Quantity]) FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] WITH (NOLOCK) WHERE [Extra 2] = '" + this.code + "'");
            if (sqlDataReader.Read())
            {
                if (!sqlDataReader.IsDBNull(0)) quantity = quantity + (int)float.Parse(sqlDataReader.GetValue(0).ToString());
            }
            sqlDataReader.Close();

             * */

            Hashtable giftCardTable = SalesID.getGiftCardItems(infojetContext);
            _historyItemsCount = 0;
            _historyPackagesCount = 0;
            _itemsCount = 0;
            _packagesCount = 0;
            

            SqlDataReader sqlDataReader = infojetContext.systemDatabase.query("SELECT [Item No_], SUM([Ordered Qty]) FROM [" + infojetContext.systemDatabase.getTableName("Web Order Line") + "] WITH (NOLOCK) WHERE [FörsäljningsID] = '" + this.code + "' AND [Förpackningsmaterial] = 0 GROUP BY [Item No_]");
            while (sqlDataReader.Read())
            {
                _historyItemsCount = _historyItemsCount + (int)float.Parse(sqlDataReader.GetValue(1).ToString());
                if (giftCardTable.Contains(sqlDataReader.GetValue(0).ToString()))
                {
                    if (!sqlDataReader.IsDBNull(1)) _historyPackagesCount = _historyPackagesCount + (((int)float.Parse(sqlDataReader.GetValue(1).ToString())) * (int)giftCardTable[sqlDataReader.GetValue(0).ToString()]);
                }
                else
                {
                    if (!sqlDataReader.IsDBNull(1)) _historyPackagesCount = _historyPackagesCount + (int)float.Parse(sqlDataReader.GetValue(1).ToString());
                }
            }
            sqlDataReader.Close();

            sqlDataReader = infojetContext.systemDatabase.query("SELECT [Item No_], SUM([Quantity]) FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] WITH (NOLOCK) WHERE [Extra 2] = '" + this.code + "' GROUP BY [Item No_]");
            while (sqlDataReader.Read())
            {
                _historyItemsCount = _historyItemsCount + (int)float.Parse(sqlDataReader.GetValue(1).ToString());
                _itemsCount = _itemsCount + (int)float.Parse(sqlDataReader.GetValue(1).ToString());

                if (giftCardTable.Contains(sqlDataReader.GetValue(0).ToString()))
                {
                    if (!sqlDataReader.IsDBNull(1))
                    {
                        _historyPackagesCount = _historyPackagesCount + (((int)float.Parse(sqlDataReader.GetValue(1).ToString())) * (int)giftCardTable[sqlDataReader.GetValue(0).ToString()]);
                        _packagesCount = _packagesCount + (((int)float.Parse(sqlDataReader.GetValue(1).ToString())) * (int)giftCardTable[sqlDataReader.GetValue(0).ToString()]);
                    }
                }
                else
                {
                    if (!sqlDataReader.IsDBNull(1))
                    {
                        _historyPackagesCount = _historyPackagesCount + (int)float.Parse(sqlDataReader.GetValue(1).ToString());
                        _packagesCount = _packagesCount + (int)float.Parse(sqlDataReader.GetValue(1).ToString());
                    }
                }
            }
            sqlDataReader.Close();



        }


        public void cleanUpSalesPersons()
        {
            /*
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT DISTINCT [Web User Account No] FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] WHERE [Extra 2] = @salesId");
            databaseQuery.addStringParameter("@salesId", code, 20);
            
            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);


            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                if (this.getSalesPerson(dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()) == null)
                {
                    DatabaseQuery databaseQuery2 = infojetContext.systemDatabase.prepare("UPDATE [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] SET [Extra 2] = @recycleSalesId WHERE [Extra 2] = @salesId AND [Web User Account No] = @webUserAccountNo");
                    databaseQuery2.addStringParameter("@recycleSalesId", code+"-O", 20);
                    databaseQuery2.addStringParameter("@salesId", code, 20);
                    databaseQuery2.addStringParameter("@webUserAccountNo", dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString(), 20);
                    databaseQuery2.execute();
                }

                i++;
            }
             * */
        }

        /// <summary>
        /// Kontrollerar om samtliga säljare klarmarkerat deras orderrader.
        /// </summary>
        /// <param name="infojetContext">Referens till Infojet-klassen.</param>
        /// <returns>True eller False</returns>
        public bool checkAllReleased(Navipro.Infojet.Lib.Infojet infojetContext)
        {
            SalesIDSalesPersonCollection salesPersonCollection = this.getSalesPersons(infojetContext);
            int i = 0;
            while (i < salesPersonCollection.Count)
            {
                if (salesPersonCollection[i].getStatus() == 0) return false;

                i++;
            }

            return true;
        }

        /// <summary>
        /// Returnerar antalet orderrader som är klarmarkerade för aktuellt försäljnings-ID.
        /// </summary>
        /// <param name="infojetContext">Referens till Infojet-klassen.</param>
        /// <returns>Heltal.</returns>
        public int countReleased(Navipro.Infojet.Lib.Infojet infojetContext)
        {
            SalesIDSalesPersonCollection salesPersonCollection = this.getSalesPersons(infojetContext);
            int i = 0;
            int count = 0;
            while (i < salesPersonCollection.Count)
            {
                if (salesPersonCollection[i].getStatus() == 2) count++;

                i++;
            }

            return count;
        }

        /// <summary>
        /// Markerar aktuellt försäljnings-ID.
        /// </summary>
        /// <param name="selected">True eller False.</param>
        public void setSelected(bool selected)
        {
            this._selected = selected;
        }

        /// <summary>
        /// Nr för aktuellt försäljnins-ID.
        /// </summary>
        public string code { set { _code = value; } get { return _code; } }
        
        /// <summary>
        /// Försäljningskoncept.
        /// </summary>
        public string salesConcept { set { _salesConcept = value; } get { return _salesConcept; } }
        
        /// <summary>
        /// Antal allokerade säljare i gruppen.
        /// </summary>
        public int noOfSalesPersons { set { _noOfSalesPersons = value; } get { return _noOfSalesPersons; } }
        
        /// <summary>
        /// Visningspaketsortiment.
        /// </summary>
        public string showCase { set { _showCase = value; } get { return _showCase; } }
        
        /// <summary>
        /// Artikelsortiment.
        /// </summary>
        public string itemSelection { set { _itemSelection = value; } get { return _itemSelection; } }
        
        /// <summary>
        /// Ordervecka då slutordern förväntas.
        /// </summary>
        public DateTime orderWeek { set { _orderWeek = value; } get { return _orderWeek; } }
        
        /// <summary>
        /// Stängningsdatum.
        /// </summary>
        public DateTime closingDate { set { _closingDate = value; } get { return _closingDate; } }
        
        /// <summary>
        /// Beskrivning av gruppen.
        /// </summary>
        public string description { set { _description = value; } get { return _description; } }
        
        /// <summary>
        /// Kundnr.
        /// </summary>
        public string customerNo { set { _customerNo = value; } get { return _customerNo; } }
        
        /// <summary>
        /// Avändarkontonr för kontaktpersonen.
        /// </summary>
        public string contactWebUserAccountNo { set { _contactWebUserAccountNo = value; } get { return _contactWebUserAccountNo; } }
        
        /// <summary>
        /// Användarkontonr för kontot som används då säljare registrerar sig.
        /// </summary>
        public string userRegWebUserAccountNo { set { _userRegWebUserAccountNo = value; } get { return _userRegWebUserAccountNo; } }
        
        /// <summary>
        /// Antal sålda, ej inskickade, paket i gruppen.
        /// </summary>
        public int soldPackages { get { return getSoldPackages(); } }
        
        /// <summary>
        /// Antal visningspaket.
        /// </summary>
        public int showCasePackages { get { return getShowCasePackages(); } }
        
        /// <summary>
        /// Antal ej sålda visningspaket.
        /// </summary>
        public int returnPackages { get { return getReturnPackages(); } }
        
        /// <summary>
        /// Antal paket att skicka in vid nästa orderläggning.
        /// </summary>
        public int packagesToSend { get { return getTotalPackages(); } }
        public string pageUrl { set { _pageUrl = value; } get { return _pageUrl; } }        
        public string selected { get { if (_selected) return "checked=\"checked\""; return ""; } }
        public Hashtable userHistoryPackageTable { get { return _userHistoryPackageTable; } set { _userHistoryPackageTable = value; } }
        public Hashtable userHistoryItemTable { get { return _userHistoryItemTable; } set { _userHistoryItemTable = value; } }
        public OrderItemCollection sentLinesCollection { get { return _sentLinesCollection; } set { _sentLinesCollection = value; } }
        public bool mobile { get { return _mobile; } }

        /// <summary>
        /// Lista över sålda visningspaket.
        /// </summary>
        public OrderItemCollection soldShowCaseItems { get { return this.getSoldShowCaseItems(); } }
        
        /// <summary>
        /// Totalt antal sålda paket på aktuell order.
        /// </summary>
        public float totalQuantity { get { return _totalQuantity; } }

        
        /// <summary>
        /// Totalt orderbelopp.
        /// </summary>
        public float totalAmount { get { return _totalAmount; } }
        
        /// <summary>
        /// Totalt antal paket på fakturerade order.
        /// </summary>
        public float totalHistoryQuantity { get { return _totalHistoryQuantity; } }
        
        /// <summary>
        /// Totalt orderbelopp på fakturerade order.
        /// </summary>
        public float totalHistoryAmount { get { return _totalHistoryAmount; } }
        
        /// <summary>
        /// Totalt antal paket på insända order.
        /// </summary>
        public float totalSentQuantity { get { return _totalSentQuantity; } set { _totalSentQuantity = value; } }

        public float totalSentPackages { get { return _totalSentPackages; } set { _totalSentPackages = value; } }
        
        /// <summary>
        /// Totalt orderbelopp på insända order.
        /// </summary>
        public float totalSentAmount { get { return _totalSentAmount; } set { _totalSentAmount = value; } }

        public float totalProfitAmount { get { return _totalProfitAmount; } set { _totalProfitAmount = value; } }
       

        /// <summary>
        /// Nästa förväntade ordertyp.
        /// </summary>
        public int nextOrderType { get { return _nextOrderType; } }
        
        /// <summary>
        /// Användarkontonr för grupp-kontaktperson.
        /// </summary>
        public string subContWebUserAccountNo { get { return _subContWebUserAccountNo; } }
        
        /// <summary>
        /// Lista över ej sålda visningspaket på aktuell order. Gäller endast slutorder.
        /// </summary>
        public OrderItemCollection notSoldShowCaseItems { get { return this.getShowCaseItems(false); } }
        
        /// <summary>
        /// Lista över ej sålda visningspaket på insända order.
        /// </summary>
        public OrderItemCollection notSoldHistoryShowCaseItems { get { return this.getHistoryShowCaseItems(); } }

        /// <summary>
        /// Kontaktpersonens namn.
        /// </summary>
        public string contactName { set { _contactName = value; } get { return _contactName; } }
        
        /// <summary>
        /// Kontaktpersonens användar-ID.
        /// </summary>
        public string contactUserId { set { _contactUserId = value; } get { return _contactUserId; } }
        
        /// <summary>
        /// Kontaktpersonens lösenord.
        /// </summary>
        public string contactPassword { set { _contactPassword = value; } get { return _contactPassword; } }
        
        /// <summary>
        /// 
        /// </summary>
        public bool additionalOrder { get { return _additionalOrder; } }

        /// <summary>
        /// Lista över aktiva säljare.
        /// </summary>
        public SalesIDSalesPersonCollection activeSalesPersons { get { return getActiveSalesPersons(infojetContext); } }
        
        /// <summary>
        /// Lista över samtliga säljare.
        /// </summary>
        public SalesIDSalesPersonCollection allSalesPersons { get { return getSalesPersons(infojetContext); } }

        /// <summary>
        /// Antal fakturerade paket.
        /// </summary>
        public int historyPackages 
        {
            get
            {
                if (!this.historyItemsCalculated) countHistoryItems();
                this.historyItemsCalculated = true;

                return this._historyPackagesCount;
            }
        }

        public int historyItems
        {
            get
            {
                if (!this.historyItemsCalculated) countHistoryItems();
                this.historyItemsCalculated = true;

                return this._historyItemsCount;
            }
        }

        public int packages
        {
            get
            {
                if (!this.historyItemsCalculated) countHistoryItems();
                this.historyItemsCalculated = true;

                return this._packagesCount;
            }
        }

        public int items
        {
            get
            {
                if (!this.historyItemsCalculated) countHistoryItems();
                this.historyItemsCalculated = true;

                return this._itemsCount;
            }
        }

        /// <summary>
        /// Vinst baserat på fakturerade paket.
        /// </summary>
        public float historyProfit
        {
            get
            {
                if (!this.historyItemsCalculated) countHistoryItems();
                this.historyItemsCalculated = true;

                SalesIDSetup salesIdSetup = new SalesIDSetup(infojetContext.systemDatabase);
                //return (salesIdSetup.unitProfitInclVat * qtyHistoryNormalPackages) + (salesIdSetup.unitProfitInclVatRea * qtyHistoryReaPackages);
                if (_profit == 0) _profit = salesIdSetup.unitProfitInclVat;
                return (_profit * _historyPackagesCount);
            }
        }

        /// <summary>
        /// Orderrader i varukorgen för aktuell order.
        /// </summary>
        public OrderItemCollection cartLines
        {
            get
            {
                if (_cartLines == null)
                {
                    _cartLines = this.getSalesIdCartLines(out _totalQuantity, out _totalAmount);
                }
                return _cartLines;
            }
        }

        /// <summary>
        /// Insända orderrader.
        /// </summary>
        public OrderItemCollection sentLines
        {
            get
            {
                if (sentLinesCollection != null)
                {

                    return sentLinesCollection;
                }
                return this.getSalesIdSentCartLines(out _totalSentQuantity, out _totalSentAmount);
            }
        }

        /// <summary>
        /// Fakturerade orderrader.
        /// </summary>
        public OrderItemCollection historyLines
        {
            get
            {
                if (_historyLines == null)
                {
                    _historyLines = this.getSalesIdHistoryLines(out _totalHistoryQuantity, out _totalHistoryAmount, out _totalProfitAmount);
                }
                return _historyLines;
            }
        }

        /// <summary>
        /// Kontrollerar om kontaktpersonen har valt avräkningsmetod för visningspaketen. Returnerar True eller False.
        /// </summary>
        /// <returns></returns>
        public bool checkShowCaseCalculationMethod()
        {
            if (showCase == "") return true;

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Sales ID], [Item No_], [Quantity Received], [Qty Packing Material], [Qty Packing Slips], [Method] FROM [" + infojetContext.systemDatabase.getTableName("Sales ID ShowCase") + "] WHERE [Sales ID] = @salesId");
            databaseQuery.addStringParameter("@salesId", code, 20);

            bool found = false;

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                found = true;
            }
            dataReader.Close();

            return found;
        }

        /// <summary>
        /// Vinst baserad på varukorgen på aktuell order.
        /// </summary>
        public float profit
        {
            get
            {
                countHistoryItems();
                
                //int qtySoldPackages = getSoldPackages();
                //int qtySoldReaPackages = getSoldPackages(3); //Rea
                //int qtySoldNormalPackages = qtySoldPackages - qtySoldReaPackages;

                //SalesIDSetup salesIdSetup = new SalesIDSetup(infojetContext.systemDatabase);
                //return (salesIdSetup.unitProfitInclVat * qtySoldNormalPackages) + (salesIdSetup.unitProfitInclVatRea * qtySoldReaPackages);
                return (_profit * _packagesCount);
            }
        }

        /// <summary>
        /// Returnerar en 10-i-topp-lista i form av en lista. Endast aktuell säljare presenteras med namn.
        /// </summary>
        /// <param name="webUserAccountNo">Användarkontonr för aktuell säljare</param>
        /// <returns>En hårt typad lista i form av TopListItem-objekt.</returns>
        public TopListCollection getTopList(string webUserAccountNo)
        {
            SalesIDSalesPersonCollection salesPersonCollection = this.getSalesPersons(infojetContext);
            Hashtable soldPackagesTable = SalesIDSalesPersonCollection.getSoldPackages(infojetContext.systemDatabase, this._code);

            TopListCollection topListCollection = new TopListCollection();

            int i = 0;
            while (i < salesPersonCollection.Count)
            {
                SalesIDSalesPerson salesIdSalesPerson = salesPersonCollection[i];

                TopListItem topListItem = new TopListItem();
                if (salesIdSalesPerson.webUserAccountNo == webUserAccountNo)
                {
                    topListItem.name = salesIdSalesPerson.name;
                }
                else
                {
                    topListItem.name = infojetContext.translate("SALESPERSON");
                }

                topListItem.rank = getRanking(salesPersonCollection, salesIdSalesPerson.webUserAccountNo, soldPackagesTable);
                topListItem.soldPackages = 0;
                if (soldPackagesTable.Contains(salesIdSalesPerson.webUserAccountNo)) topListItem.soldPackages = (int)soldPackagesTable[salesIdSalesPerson.webUserAccountNo.ToString()];

                int j = 0;
                bool inserted = false;
                while ((j < topListCollection.Count) && (!inserted))
                {
                    if (topListItem.rank < topListCollection[j].rank)
                    {
                        topListCollection.Insert(j, topListItem);
                        inserted = true;
                    }

                    j++;
                }

                if (!inserted)
                {
                    topListCollection.Add(topListItem);
                }

                i++;
            }

            return topListCollection;
        }

        /*
        public bool checkContactAllowNextOrder()
        {
            string fieldValue = "";

            SqlDataReader dataReader = database.query("SELECT [Value] FROM [" + database.getTableName("Web User Account Profile Line") + "] l, [" + database.getTableName("Web User Account Profile Entry") + "] h WHERE h.[Web User Account No_] = '" + this.contactWebUserAccountNo + "' AND l.[Web User Account No_] = h.[Web User Account No_] AND l.[Web User Acc Profile Entry No_] = h.[Entry No_] AND h.[Current Profile] = 1 AND l.[Field Code] = 'ALLOW NEXT ORDER'");
            if (dataReader.Read())
            {
                fieldValue = dataReader.GetValue(0).ToString();
            }

            dataReader.Close();


            return fieldValue;
        }
        */

        private int countShowCaseDeliveredProducts(string itemNo)
        {
            int quantity = 0;

            SqlDataAdapter dataAdapter = infojetContext.systemDatabase.dataAdapterQuery("SELECT TOP 1 * FROM [" + infojetContext.systemDatabase.getTableName("Sales ID ShowCase") + "] WHERE [Sales ID] = '" + this.code + "'");
            DataSet salesIdShowCaseDataSet = new DataSet();
            dataAdapter.Fill(salesIdShowCaseDataSet);

            if (salesIdShowCaseDataSet.Tables[0].Rows.Count > 0)
            {
                string itemNoFilter = "";
                if (itemNo != "") itemNoFilter = "AND [Item No_] = '" + itemNo + "'";

                SqlDataReader sqlDataReader = infojetContext.systemDatabase.query("SELECT SUM([Quantity Received]) FROM [" + infojetContext.systemDatabase.getTableName("Sales ID ShowCase") + "] WHERE [Sales ID] = '" + this.code + "' " + itemNoFilter);

                if (sqlDataReader.Read())
                {
                    if (!sqlDataReader.IsDBNull(0))
                    {
                        quantity = (int)float.Parse(sqlDataReader.GetValue(0).ToString());
                    }
                }

                sqlDataReader.Close();
            }

            else
            {
                quantity = countShowCaseOriginalProducts(itemNo);

            }
            return quantity;
        }

        private Hashtable getShowCaseDeliveredProductList()
        {            
            Hashtable deliveredProductsTable = new Hashtable();
            if (this.showCase == "") return deliveredProductsTable;

            SqlDataReader dataReader = infojetContext.systemDatabase.query("SELECT [Item No_], SUM([Quantity Received]) FROM [" + infojetContext.systemDatabase.getTableName("Sales ID ShowCase") + "] WHERE [Sales ID] = '" + this.code + "' GROUP BY [Item No_]");
            
            bool read = false;
            while (dataReader.Read())
            {
                read = true;
                int quantity = 0;
                    if (!dataReader.IsDBNull(1))
                    {
                        quantity = (int)float.Parse(dataReader.GetValue(1).ToString());
                    }
                    deliveredProductsTable.Add(dataReader.GetValue(0).ToString(), quantity);
            }
            dataReader.Close();

            if (!read)
            {
                SqlDataReader dataReader2 = infojetContext.systemDatabase.query("SELECT l.[No_], SUM(l.[Quantity]) FROM [" + infojetContext.systemDatabase.getTableName("Sales Shipment Header") + "] h WITH (NOLOCK), [" + infojetContext.systemDatabase.getTableName("Sales Shipment Line") + "] l WITH (NOLOCK), [" + infojetContext.systemDatabase.getTableName("BOM Component") + "] c WITH (NOLOCK) WHERE h.[FörsäljningsID] = '" + this.code + "' AND h.[Odertyp] = 1 AND l.[Document No_] = h.[No_] AND l.[Type] = 2 AND l.[No_] = c.[No_] AND c.[Parent Item No_] = '" + this.showCase + "' GROUP BY l.[No_]");

                while (dataReader2.Read())
                {
                    int quantity = 0;
                    if (!dataReader2.IsDBNull(0))
                    {
                        quantity = quantity + (int)float.Parse(dataReader2.GetValue(1).ToString());
                    }
                    deliveredProductsTable.Add(dataReader2.GetValue(0).ToString(), quantity);
                }

                dataReader2.Close();

                
                SqlDataReader dataReader3 = infojetContext.systemDatabase.query("SELECT l.[No_], SUM(l.[Quantity]) FROM [" + infojetContext.systemDatabase.getTableName("Sales Cr_Memo Header") + "] h WITH (NOLOCK), [" + infojetContext.systemDatabase.getTableName("Sales Cr_Memo Line") + "] l WITH (NOLOCK), [" + infojetContext.systemDatabase.getTableName("BOM Component") + "] c WITH (NOLOCK), [" + infojetContext.systemDatabase.getTableName("Sales Invoice Header") + "] ih WHERE h.[FörsäljningsID] = '" + this.code + "' AND l.[Document No_] = h.[No_] AND l.[Type] = 2 AND l.[No_] = c.[No_] AND c.[Parent Item No_] = '" + this.showCase + "' AND h.[Applies-to Doc_ No_] = ih.[No_] AND ih.[Odertyp] = 1 GROUP BY l.[No_]");

                while (dataReader3.Read())
                {
                    int quantity = 0;
                    if (!dataReader3.IsDBNull(0))
                    {
                        quantity = quantity + (int)float.Parse(dataReader3.GetValue(1).ToString());
                    }
                    if (deliveredProductsTable[dataReader3.GetValue(0).ToString()] != null)
                    {
                        deliveredProductsTable[dataReader3.GetValue(0).ToString()] = ((int)deliveredProductsTable[dataReader3.GetValue(0).ToString()]) - quantity;
                        if (((int)deliveredProductsTable[dataReader3.GetValue(0).ToString()]) < 0) deliveredProductsTable[dataReader3.GetValue(0).ToString()] = 0;
                    }
                }

                dataReader3.Close();
                

            }
            return deliveredProductsTable;
        }


        private int countShowCaseOriginalProducts(string itemNo)
        {
            int quantity = 0;

            SqlDataAdapter dataAdapter = infojetContext.systemDatabase.dataAdapterQuery("SELECT [No_] FROM [" + infojetContext.systemDatabase.getTableName("Sales Shipment Header") + "] WITH (NOLOCK) WHERE [FörsäljningsID] = '" + this.code + "' AND [Odertyp] = 1");
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                string docNo = dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();
                string itemNoFilter = "";
                if (itemNo != "") itemNoFilter = "AND l.[No_] = '" + itemNo + "'";

                SqlDataReader sqlDataReader = infojetContext.systemDatabase.query("SELECT SUM(l.[Quantity]) FROM [" + infojetContext.systemDatabase.getTableName("Sales Shipment Line") + "] l, [" + infojetContext.systemDatabase.getTableName("BOM Component") + "] c WHERE l.[Document No_] = '" + docNo + "' AND l.[Type] = 2 AND l.[No_] = c.[No_] AND c.[Parent Item No_] = '" + this.showCase + "' " + itemNoFilter);
                if (sqlDataReader.Read())
                {
                    if (!sqlDataReader.IsDBNull(0))
                    {
                        quantity = quantity + (int)float.Parse(sqlDataReader.GetValue(0).ToString());
                    }
                }
                sqlDataReader.Close();

                i++;
            }

            return quantity;
        }

        private OrderItemCollection getShowCaseCollection()
        {
            Hashtable hashTable = new Hashtable();
            ArrayList keyList = new ArrayList();
            OrderItemCollection orderItemCollection = new OrderItemCollection();

            SqlDataReader sqlDataReader = infojetContext.systemDatabase.query("SELECT h.[No_], l.[No_], l.[Description], l.[Quantity] FROM [" + infojetContext.systemDatabase.getTableName("Sales Shipment Header") + "] h WITH (NOLOCK), [" + infojetContext.systemDatabase.getTableName("Sales Shipment Line") + "] l WITH (NOLOCK) WHERE h.[FörsäljningsID] = '" + this.code + "' AND h.[Odertyp] = 1 AND l.[Document No_] = h.[No_] AND l.[Type] = 2 AND l.[Unit of Measure Code] = 'PKT' AND l.[Quantity] <> 0");

            while (sqlDataReader.Read())
            {
                string itemNo = sqlDataReader.GetValue(1).ToString();
                if (!hashTable.ContainsKey(itemNo))
                {
                    OrderItem orderItem = new OrderItem();
                    orderItem.itemNo = itemNo;
                    orderItem.description = sqlDataReader.GetValue(2).ToString();

                    hashTable.Add(itemNo, orderItem);
                    keyList.Add(itemNo);
                }

                if (!sqlDataReader.IsDBNull(1))
                {
                    ((OrderItem)hashTable[itemNo]).quantity = ((OrderItem)hashTable[itemNo]).quantity + (int)float.Parse(sqlDataReader.GetValue(3).ToString());
                }

            }
            sqlDataReader.Close();

            int i = 0;
            while (i < keyList.Count)
            {
                OrderItem orderItem = ((OrderItem)hashTable[keyList[i].ToString()]);
                orderItemCollection.Add(orderItem);

                i++;
            }

            return orderItemCollection;
        }

        /// <summary>
        /// Räknar antalet registrerade/aktiva säljare.
        /// </summary>
        /// <returns>Ett heltal.</returns>
        public int countRegisteredSalesPersons()
        {
            SqlDataReader dataReader = infojetContext.systemDatabase.query("SELECT COUNT(*) FROM [" + infojetContext.systemDatabase.getTableName("SalesID WebUserAccount") + "] w, [" + infojetContext.systemDatabase.getTableName("Web User Account") + "] a WHERE w.[FÖrsäljningsID] = '" + code + "' AND w.[Web User Account] = a.[No_] AND a.[Type] = 1 AND [Company Role] = 'X'");

            int count = 0;

            if (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0)) count = int.Parse(dataReader.GetValue(0).ToString());
            }
            dataReader.Close();

            return count;
        }

        /// <summary>
        /// Returnerar en given säljares varukorg i form av ett dataset.
        /// </summary>
        /// <param name="webUserAccountNo">Användarkontonr för säljaren.</param>
        /// <returns>Ett dataset.</returns>
        public DataSet getCartLines(string webUserAccountNo)
        {
            SqlDataAdapter sqlDataAdapter = infojetContext.systemDatabase.dataAdapterQuery("SELECT [Session ID], [Entry No_], [Item No_], [Unit Of Measure Code], [Unit Price], [Quantity], [Amount], [Extra 1], [Extra 2], [Extra 3], [Extra 4], [Extra 5], [Web User Account No], [Reference No_] FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] WHERE [Web User Account No] = '" + webUserAccountNo + "' AND [Extra 2] = '" + code + "' ORDER BY [Item No_]");
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            return dataSet;

        }

        /// <summary>
        /// Returnerar en given säljares insända orderrader i form av ett dataset.
        /// </summary>
        /// <param name="webUserAccountNo">Användarkontonr för säljaren.</param>
        /// <returns>Ett dataset.</returns>
        public DataSet getSentCartLines(string webUserAccountNo)
        {
            SqlDataAdapter sqlDataAdapter = infojetContext.systemDatabase.dataAdapterQuery("SELECT [Document No_], [Line No_], [Item No_], [Unit Of Measure Code], [Unit Price], [Quantity], [Amount], [Extra 1], [Extra 2], [Extra 3], [Extra 4], [Extra 5], [Web User Account No_], [Reference No_] FROM [" + infojetContext.systemDatabase.getTableName("Web Sales Line") + "] WHERE [Web User Account No_] = '" + webUserAccountNo + "' AND [Extra 2] = '" + code + "' ORDER BY [Item No_]");
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            return dataSet;

        }

        /// <summary>
        /// Kontrollerar om ett givet användarkontonr är kontaktperson eller grupp-kontaktperson för aktuellt försäljnings-ID.
        /// </summary>
        /// <param name="webUserAccountNo">Användarkontonr.</param>
        /// <returns>True eller False.</returns>
        public bool isContactPerson(string webUserAccountNo)
        {
            if (webUserAccountNo == this.contactWebUserAccountNo) return true;
            if (webUserAccountNo == this.subContWebUserAccountNo) return true;
            return false;
        }

        /// <summary>
        /// Kontrollerar om ett givet användarkontonr är grupp-kontaktperson för det aktuella försäljnings-ID't.
        /// </summary>
        /// <param name="webUserAccountNo">Användarkontonr.</param>
        /// <returns>True eller False.</returns>
        public bool isSubContactPerson(string webUserAccountNo)
        {
            if (webUserAccountNo == this.subContWebUserAccountNo) return true;
            return false;
        }

        /// <summary>
        /// Kontrollerar om ett givet användarkontonr är huvud-kontaktperson för det aktuella försäljnings-ID't.
        /// </summary>
        /// <param name="webUserAccountNo">Användarkontonr.</param>
        /// <returns>True eller False.</returns>
        public bool isPrimaryContactPerson(string webUserAccountNo)
        {
            if (webUserAccountNo == this.contactWebUserAccountNo) return true;
            return false;
        }

        /// <summary>
        /// Genomför kontroller mot ett antal kriterier om gruppen är berättigad till fri frakt eller inte.
        /// </summary>
        /// <returns>True eller False.</returns>
        public bool checkReorderingFreeFreight()
        {
            SalesIDSetup salesIdSetup = new SalesIDSetup(infojetContext.systemDatabase);

            if (nextOrderType != 3) return false;

            SqlDataReader dataReader = infojetContext.systemDatabase.query("SELECT [No_] FROM [" + infojetContext.systemDatabase.getTableName("Sales Header") + "] WITH (NOLOCK) WHERE [FörsäljningsID] =  '"+this.code+"' AND [Ordertyp] = 3");
            if (dataReader.Read())
            {
                dataReader.Close();
                return false;
            }
            dataReader.Close();

            dataReader = infojetContext.systemDatabase.query("SELECT [No_] FROM [" + infojetContext.systemDatabase.getTableName("Sales Invoice Header") + "] WITH (NOLOCK) WHERE [FörsäljningsID] =  '" + this.code + "' AND [Odertyp] = 3");
            if (dataReader.Read())
            {
                dataReader.Close();
                return false;
            }
            dataReader.Close();

            int noOfPackages = 0;

            dataReader = infojetContext.systemDatabase.query("SELECT SUM(l.[Quantity]) FROM [" + infojetContext.systemDatabase.getTableName("Sales Line") + "] l WITH (NOLOCK), [" + infojetContext.systemDatabase.getTableName("Sales Header") + "] h WITH (NOLOCK) WHERE l.[Document Type] = h.[Document Type] AND l.[Document No_] = h.[No_] AND h.[FörsäljningsID] =  '" + this.code + "' AND l.[Unit of Measure Code] = 'PKT'");
            if (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0)) noOfPackages = noOfPackages + (int)dataReader.GetDecimal(0);
            }
            dataReader.Close();

            dataReader = infojetContext.systemDatabase.query("SELECT SUM(l.[Quantity]) FROM [" + infojetContext.systemDatabase.getTableName("Sales Invoice Line") + "] l WITH (NOLOCK), [" + infojetContext.systemDatabase.getTableName("Sales Invoice Header") + "] h WITH (NOLOCK) WHERE l.[Document No_] = h.[No_] AND h.[FörsäljningsID] =  '" + this.code + "' AND l.[Unit of Measure Code] = 'PKT'");
            if (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0)) noOfPackages = noOfPackages + (int)dataReader.GetDecimal(0);
            }
            dataReader.Close();

            if (noOfPackages < salesIdSetup.freeFreightPackageLimit) return false;

            dataReader = infojetContext.systemDatabase.query("SELECT [Order Date] FROM [" + infojetContext.systemDatabase.getTableName("Sales Invoice Header") + "] WITH (NOLOCK) WHERE [FörsäljningsID] =  '" + this.code + "' AND [Odertyp] = 2");
            if (dataReader.Read())
            {
                DateTime orderDate = dataReader.GetDateTime(0);
                if (orderDate.AddDays(salesIdSetup.freeFreightDaysCount) < DateTime.Today)
                {
                    dataReader.Close();
                    return false;
                }
            }
            dataReader.Close();


            return true;
        }

        /// <summary>
        /// Kontrollerar om artikelsortimentet för det aktuella försäljnings-ID't är ett primärt sortiment eller inte.
        /// </summary>
        /// <returns>True eller False</returns>
        public bool checkPreliminaryCatalog()
        {
            bool prel = false;

            SqlDataReader dataReader = infojetContext.systemDatabase.query("SELECT [Preliminärt sortiment] FROM [" + infojetContext.systemDatabase.getTableName("Item") + "] WITH (NOLOCK) WHERE [No_] = '" + this._itemSelection + "'");
            if (dataReader.Read())
            {
                if (dataReader.GetValue(0).ToString() == "1") prel = true;
            }
            dataReader.Close();

            return prel;
        }

        private Hashtable getSoldQuantities()
        {
            Hashtable soldQuantitiesTable = new Hashtable();
            Hashtable giftCardTable = new Hashtable(); //Hashtable giftCardTable = SalesID.getGiftCardItems(infojetContext);

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Item No_], SUM([Ordered Qty]) FROM [" + infojetContext.systemDatabase.getTableName("Web Order Line") + "] WHERE [FörsäljningsID] = @salesId GROUP BY [Item No_]");
            databaseQuery.addStringParameter("@salesId", this.code, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            while (dataReader.Read())
            {

                float soldQuantity = 0;
                if (giftCardTable.Contains(dataReader.GetValue(0).ToString()))
                {
                    if (!dataReader.IsDBNull(1)) soldQuantity = (float.Parse(dataReader.GetValue(1).ToString()) * (int)giftCardTable[dataReader.GetValue(0).ToString()]);
                }
                else
                {
                    if (!dataReader.IsDBNull(1)) soldQuantity = float.Parse(dataReader.GetValue(1).ToString());
                }
                soldQuantitiesTable.Add(dataReader.GetValue(0).ToString(), soldQuantity);
            }

            dataReader.Close();

            databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Item No_], SUM([Quantity]) FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] WHERE [Extra 2] = @salesId GROUP BY [Item No_]");
            databaseQuery.addStringParameter("@salesId", this.code, 20);

            dataReader = databaseQuery.executeQuery();
            while (dataReader.Read())
            {
                float soldQuantity = 0;
                if (giftCardTable.Contains(dataReader.GetValue(0).ToString()))
                {
                    if (!dataReader.IsDBNull(1)) soldQuantity = (float.Parse(dataReader.GetValue(1).ToString()) * (int)giftCardTable[dataReader.GetValue(0).ToString()]);
                }
                else
                {
                    if (!dataReader.IsDBNull(1)) soldQuantity = float.Parse(dataReader.GetValue(1).ToString());
                }

                if (soldQuantitiesTable[dataReader.GetValue(0).ToString()] == null)
                {
                    soldQuantitiesTable.Add(dataReader.GetValue(0).ToString(), soldQuantity);
                }
                else
                {
                    soldQuantitiesTable[dataReader.GetValue(0).ToString()] = (((float)soldQuantitiesTable[dataReader.GetValue(0).ToString()]) + soldQuantity);
                }
            }

            dataReader.Close();


            return soldQuantitiesTable;
        }


        /// <summary>
        /// Hämtar en lista med samtliga presentkort samt antalet motsvarande paket.
        /// </summary>
        /// <returns>Hashtable</returns>
        public static Hashtable getGiftCardItems(Navipro.Infojet.Lib.Infojet infojetContext)
        {
            Hashtable giftCardTable = new Hashtable();

            SqlDataReader dataReader = infojetContext.systemDatabase.query("SELECT [No_], [Gift Card Package Qty_] FROM [" + infojetContext.systemDatabase.getTableName("Item") + "] WITH (NOLOCK) WHERE [Gift Voucher Item] = 1");
            while (dataReader.Read())
            {
                int qty = 1;
                if (dataReader.GetInt32(1) > 0) qty = dataReader.GetInt32(1);
                giftCardTable.Add(dataReader.GetValue(0).ToString(), qty);
            }

            dataReader.Close();


            return giftCardTable;
        }
    }
}
