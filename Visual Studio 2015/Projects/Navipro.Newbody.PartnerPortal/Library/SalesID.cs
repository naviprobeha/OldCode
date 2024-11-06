using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Navipro.Infojet.Lib;

namespace Navipro.Newbody.PartnerPortal.Library
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
        private DateTime _startDate;
        private DateTime _startOrderDelivery;
        private DateTime _orderWeek;
        private DateTime _closingDate;
        private string _description;
        private string _customerNo;
        private string _contactWebUserAccountNo;
        private string _userRegWebUserAccountNo;
        private string _subContWebUserAccountNo;
        private int _nextOrderType;
        private string _controlNo;
        private SalesPerson _salesPerson;

        private int _soldPackages;
        private float _soldAmount;
        private int _soldPackagesInclGiftCards;
        private int _historyPackages;
        private float _historyAmount;
        private int _historyPackagesInclGiftCards;
        private string _pageUrl;
        private bool _selected;
        private string _status;
        private string _nbSalesPersonCode;


        private string _contactName;
        private string _contactUserId;
        private string _contactPassword;

        private string _customerName = "";


        private bool _additionalOrder;
        private bool _primaryOrderExists;

        private float _profit;
        private string _profitCurrencyCode;

        private OrderItemCollection _sentLinesCollection;

        private WebUserAccount _contactWebUserAccount;
        private Navipro.Infojet.Lib.Infojet infojetContext;

        public SalesID()
        {
        }

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
        /// Konstruktor som initierar ett försäljnings-ID utifrån en datareader. 
        /// </summary>
        /// <param name="infojetContext">Referens till Infojet-klassen.</param>
        /// <param name="dataReader">En öppen datareader från samma tabell.</param>
        public SalesID(Navipro.Infojet.Lib.Infojet infojetContext, SqlDataReader dataReader)
        {
            this.infojetContext = infojetContext;

            readData(dataReader);
        }

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

            this._additionalOrder = false;
            if (dataRow.ItemArray.GetValue(13).ToString() == "1") this._additionalOrder = true;

            this._profit = float.Parse(dataRow.ItemArray.GetValue(14).ToString());
            this._profitCurrencyCode = dataRow.ItemArray.GetValue(15).ToString();

            this._startDate = DateTime.Parse(dataRow.ItemArray.GetValue(16).ToString());
            this._startOrderDelivery = DateTime.Parse(dataRow.ItemArray.GetValue(17).ToString());
            this._controlNo = dataRow.ItemArray.GetValue(18).ToString();

            this._customerName = dataRow.ItemArray.GetValue(19).ToString();

            if (_startDate.Year < 1900) _startDate = DateTime.Parse("2000-01-01 00:00:00");
            if (_orderWeek.Year < 1900) _orderWeek = _startDate;
            if (_closingDate.Year < 1900) _closingDate = _startDate;
            if (startOrderDelivery.Year < 1900) _startOrderDelivery = _startDate;

        }

        private void getFromDatabase()
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [FörsäljningsID], [Försäljningskoncept], [Antal säljare], [Visningskasse], [Artikelsortiment], [Ordervecka Slutorder], [Stängningsdatum], [Benämning], [Kund], [Contact Web User Account No_], [User Reg_ Web User Account No_], [Next Ordertype], [Sub Cont Web User Account No_], [Additional order], [Profit], [Profit Currency], [Försäljningsstartdatum], [Leveransdatum startorder], [Control No_], [NBSäljare] FROM [" + infojetContext.systemDatabase.getTableName("FörsäljningsID") + "] WHERE [FörsäljningsID] = @code");

            databaseQuery.addStringParameter("@code", code, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();


            if (dataReader.Read())
            {
                readData(dataReader);
            }

            dataReader.Close();

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

            this._startDate = dataReader.GetDateTime(16);
            this._startOrderDelivery = dataReader.GetDateTime(17);

            this._controlNo = dataReader.GetValue(18).ToString();

            if (_startDate.Year < 1900) _startDate = DateTime.Parse("2000-01-01 00:00:00");
            if (_orderWeek.Year < 1900) _orderWeek = _startDate;
            if (_closingDate.Year < 1900) _closingDate = _startDate;
            if (startOrderDelivery.Year < 1900) _startOrderDelivery = _startDate;

            this._nbSalesPersonCode = dataReader.GetValue(19).ToString();
        }

        /// <summary>
        /// Returnerar användarkontot för kontaktpersonen i det aktuella försäljnings-ID't.
        /// </summary>
        /// <returns>WebUserAccount-klassen i Infojet. Representerar ett användarkonto.</returns>
        public WebUserAccount contactWebUserAccount  
        {
            get
            {
                if (_contactWebUserAccount == null)
                {
                    _contactWebUserAccount = new WebUserAccount(infojetContext.systemDatabase, this.contactWebUserAccountNo);
                }
                return _contactWebUserAccount;
            }
            set
            { }
        }



        /// <summary>
        /// Returnerar ett dataset innehållande produktgrupper ifrån sortimentet assosierad med försäljnings-ID't.
        /// </summary>
        /// <returns>Ett dataset.</returns>
        public DataSet getProductGroups()
        {
            return BOMComponent.getProductGroups(infojetContext.systemDatabase, this.itemSelection);

        }

        /// <summary>
        /// Returnerar ett dataset innehållande produktgrupper ifrån sortimentet assosierad med försäljnings-ID't.
        /// </summary>
        /// <returns>Ett dataset.</returns>
        public Product[] getProductArray(Infojet.Lib.Infojet infojetContext, string languageCode)
        {
            //DataSet totalProductDataSet = BOMComponent.getProducts(infojetContext.systemDatabase, this.itemSelection);
            //Items items = new Items();
            //Hashtable itemInfoTable = items.getItemInfo(totalProductDataSet, infojetContext, true, false);

            Hashtable itemTextTable = ExtendedTextHeader.getItemTexts(infojetContext, languageCode, true);
            
            string lastCategory = "";
            int productCount = 0;
            ItemCategory itemCategory = null;

            //SalesIDSetup salesIdSetup = new SalesIDSetup(infojetContext.systemDatabase);

            DataSet productGroupDataSet = BOMComponent.getProductGroups(infojetContext.systemDatabase, this.itemSelection);

            Product[] productArray = new Product[productGroupDataSet.Tables[0].Rows.Count];

            int i = 0;
            int z = 0;
            while (i < productGroupDataSet.Tables[0].Rows.Count)
            {

                ProductGroup productGroup = new ProductGroup(infojetContext.systemDatabase, productGroupDataSet.Tables[0].Rows[i]);

                if (lastCategory != productGroup.code)
                {
                    
                    Product product = new Product();
                    product.modelNo = productGroup.itemCategoryCode+"-"+productGroup.code;
                    product.description = productGroup.description;
                    product.unitPrice = 150;
                    product.itemCategoryCode = productGroup.itemCategoryCode;
                    product.itemCategoryDescription = productGroup.itemCategoryDescription;

                    productCount++;
                    if (itemCategory == null) itemCategory = productGroup.getItemCategory();
                    if (itemCategory.code != productGroup.itemCategoryCode) itemCategory = productGroup.getItemCategory();

                    System.Data.DataSet productDataSet = productGroup.getProducts(infojetContext.systemDatabase, itemSelection);
                    int noOfProducts = productDataSet.Tables[0].Rows.Count;

                    Navipro.Infojet.Lib.WebItemImages webItemImages = new Navipro.Infojet.Lib.WebItemImages(infojetContext);
                    Navipro.Infojet.Lib.WebItemImage webItemImage = webItemImages.getItemProductImage(productDataSet.Tables[0].Rows[0].ItemArray.GetValue(0).ToString(), infojetContext.webSite.code);
                    if (webItemImage != null)
                    {
                        product.setImage(webItemImage.image);
                    }

                    product.productSkuArray = new ProductSku[productDataSet.Tables[0].Rows.Count];

                    int j = 0;
                    while (j < productDataSet.Tables[0].Rows.Count)
                    {
                        string sizeCode = productDataSet.Tables[0].Rows[j].ItemArray.GetValue(9).ToString();
                        string itemNo = productDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString();
                        string composition = productDataSet.Tables[0].Rows[j].ItemArray.GetValue(10).ToString();
                        int packageQty = int.Parse(productDataSet.Tables[0].Rows[j].ItemArray.GetValue(11).ToString());

                        Item item = new Item(infojetContext, productDataSet.Tables[0].Rows[j]);

                        ProductSku productSku = new ProductSku();
                        productSku.itemNo = itemNo;
                        productSku.size = sizeCode;
                        product.productSkuArray[j] = productSku;
                        product.composition = composition;
                        product.qtyInPackage = packageQty;

                        if (product.text == "")
                        {
                            if (itemTextTable.Contains(itemNo)) product.text = (string)itemTextTable[itemNo];
                        }


                        if (product.unitOfMeasure == "")
                        {
                            product.unitOfMeasure = item.salesUnitOfMeasure;
                        }

                        j++;
                    }

                    productArray[z] = product;
                    lastCategory = productGroup.code;
                    z++;

                }
                i++;
            }

            Product[] newProductArray = new Product[z];
            i = 0;
            while (i < z)
            {
                newProductArray[i] = productArray[i];
                i++;
            }



            return newProductArray;
        }


        /// <summary>
        /// Returnerar en hash-tabell med artikelinformation (lagersaldon och priser) för samtliga artiklar i det aktuella sortimentet.
        /// </summary>
        /// <param name="infojetContext">Referens till Infojet-klassen.</param>
        /// <returns>En hash-tabell. Nykeln i tabellen är artikelnr.</returns>
        public Hashtable getItemInfo(Navipro.Infojet.Lib.Infojet infojetContext)
        {
            DataSet itemDataSet = BOMComponent.getProducts(infojetContext.systemDatabase, this.itemSelection);

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
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Size] FROM [" + infojetContext.systemDatabase.getTableName("Item") + "] WHERE [No_] = @no");
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

 
        /// <summary>
        /// Returnerar samtliga säljare i det aktuella försäljnings-ID't. 
        /// </summary>
        /// <param name="infojet">Referens till Infojet-klassen.</param>
        /// <returns>En hårt typad lista som består av SalesIDSalesPerson-objekt.</returns>
        public SalesIDSalesPersonCollection getSalesPersons(Navipro.Infojet.Lib.Infojet infojet)
        {
            return SalesIDSalesPerson.getSalesPersons(infojet, code);

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
        public int getRanking(string webUserAccountNo)
        {
            DataSet salesPersonDataSet = SalesIDSalesPerson.getSalesPersonsDataSet(infojetContext, code);
            int i = 0;

            int ranking = salesPersonDataSet.Tables[0].Rows.Count;

            SalesIDSalesPerson salesPerson = new SalesIDSalesPerson(infojetContext.systemDatabase, this.code, webUserAccountNo);

            while (i < salesPersonDataSet.Tables[0].Rows.Count)
            {
                SalesIDSalesPerson salesIdSalesPerson = new SalesIDSalesPerson(infojetContext.systemDatabase, salesPersonDataSet.Tables[0].Rows[i]);
                if (salesPerson.webUserAccountNo != salesIdSalesPerson.webUserAccountNo)
                {
                    //int soldPackages = salesIdSalesPerson.soldPackages;
                    //if (salesPerson.soldPackages >= soldPackages) ranking--;
                }
                i++;
            }

            return ranking;
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
                Item item = new Item(infojetContext, dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString());

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
                Item item = new Item(infojetContext, dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString());

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
        public OrderItemCollection getSalesIdHistoryLines(out float totalQuantity, out float totalAmount)
        {
            OrderItemCollection orderItemCollection = new OrderItemCollection();

            totalAmount = 0;
            totalQuantity = 0;

            SqlDataAdapter dataAdapter = infojetContext.systemDatabase.dataAdapterQuery("SELECT [No_] FROM [" + infojetContext.systemDatabase.getTableName("Sales Invoice Header") + "] WITH (NOLOCK) WHERE [FörsäljningsID] = '" + this.code + "'");

            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            Items items = new Items();


            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                string docNo = dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();

                SqlDataAdapter lineAdapter = infojetContext.systemDatabase.dataAdapterQuery("SELECT l.[No_], SUM(l.[Quantity]) FROM [" + infojetContext.systemDatabase.getTableName("Sales Invoice Line") + "] l, [" + infojetContext.systemDatabase.getTableName("Item") + "] i WHERE l.[Document No_] = '" + docNo + "' AND l.[Type] = 2  AND l.[Unit of Measure Code] = 'PKT' AND l.[No_] = i.[No_] GROUP BY l.[No_]");
                DataSet lineDataSet = new DataSet();
                lineAdapter.Fill(lineDataSet);

                Hashtable itemInfoTable = items.getItemInfo(lineDataSet, infojetContext, true, false);

                int j = 0;
                while (j < lineDataSet.Tables[0].Rows.Count)
                {

                    Item item = new Item(infojetContext, lineDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString());

                    WebCartLine webCartLine = new WebCartLine(null, null, item);

                    webCartLine.quantity = float.Parse(lineDataSet.Tables[0].Rows[j].ItemArray.GetValue(1).ToString());
                    if (itemInfoTable.Contains(item.no))
                    {
                        webCartLine.unitPrice = ((ItemInfo)itemInfoTable[item.no]).unitPrice;
                    }


                    OrderItem orderItem = new OrderItem(webCartLine);
                    orderItem.description = item.description;

                    orderItem.amount = orderItem.unitPrice * orderItem.quantity;

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
            infojetContext.systemDatabase.nonQuery("DELETE FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] WHERE [Extra 2] = '" + code + "'");
        }



        /// <summary>
        /// Kontrollerar om det finns en inskickad order.
        /// </summary>
        public bool checkOrderSent()
        {
            bool exists = false;

            SqlDataReader sqlDataReader = infojetContext.systemDatabase.query("SELECT TOP 1 [Item No_] FROM [" + infojetContext.systemDatabase.getTableName("Web Sales Line") + "] WITH (NOLOCK) WHERE [Extra 2] = '" + this.code + "'");
            if (sqlDataReader.Read())
            {
                exists = true;
            }
            sqlDataReader.Close();

            return exists;
        }


        /// <summary>
        /// Aktiverar efterbeställning.
        /// </summary>
        public void setAdditionalOrderMode(bool active)
        {
            int activeInt = 0;
            if (active) activeInt = 1;

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("UPDATE [" + infojetContext.systemDatabase.getTableName("FörsäljningsID") + "] SET [Additional order] = @active WHERE [FörsäljningsID] = '" + this.code + "'");
            databaseQuery.addIntParameter("@active", activeInt);
            databaseQuery.execute();

        }

        /// <summary>
        /// Ändrar kontaktperson på gruppnivå.
        /// </summary>
        public void setGroupContactPerson(string webUserAccountNo)
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("UPDATE [" + infojetContext.systemDatabase.getTableName("FörsäljningsID") + "] SET [Sub Cont Web User Account No_] = @webUserAccountNo WHERE [FörsäljningsID] = '" + this.code + "'");
            databaseQuery.addStringParameter("@webUserAccountNo", webUserAccountNo, 20);
            databaseQuery.execute();

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

                SqlDataReader sqlDataReader = infojetContext.systemDatabase.query("SELECT SUM(l.[Quantity]) FROM [" + infojetContext.systemDatabase.getTableName("Sales Invoice Line") + "] l, [" + infojetContext.systemDatabase.getTableName("Item") + "] i WHERE l.[Document No_] = '" + docNo + "' AND l.[Type] = 2  AND l.[Unit of Measure Code] = 'PKT' AND l.[No_] = i.[No_] AND i.[Artikelstatus] = '" + itemStatus + "'");
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
        public int countHistoryItems()
        {
            int quantity = 0;

            SqlDataReader sqlDataReader = infojetContext.systemDatabase.query("SELECT SUM([Quantity]) FROM [" + infojetContext.systemDatabase.getTableName("Web Sales Line") + "] WITH (NOLOCK) WHERE [Extra 2] = '" + this.code + "'");
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

            return quantity;


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
        /// Sammanställer statusen ifrån säljarna.
        /// </summary>
        /// <param name="infojetContext">Referens till Infojet-klassen.</param>
        /// <returns>True eller False</returns>
        public int getStatus(Navipro.Infojet.Lib.Infojet infojetContext)
        {
            //SalesIDSalesPersonCollection salesPersonCollection = this.getSalesPersons(infojetContext);
            DataSet dataSet = SalesIDSalesPerson.getSalesPersonsDataSet(infojetContext, this._code);
            int i = 0;
            int countReleased = 0;
            int countNotStarted = 0;

            if ((checkOrderSent()) && (!this.additionalOrder)) return 3;

            Hashtable statusTable = getSalesPersonStatuses();

            while (i < dataSet.Tables[0].Rows.Count)
            {
                string webUserAccountNo = dataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString();

                if (!statusTable.Contains(webUserAccountNo))
                    countNotStarted++;
                else
                {
                    if ((int)statusTable[webUserAccountNo] == 2) countReleased++;
                }
                //if (salesPersonCollection[i].getStatus() == 1) countNotStarted++;
                //if (salesPersonCollection[i].getStatus() == 2) countReleased++;

                i++;
            }

            //if (this._code == "FID035734") throw new Exception(dataSet.Tables[0].Rows.Count.ToString() + ", " + countNotStarted);
            if ((countReleased > 0) && ((countReleased+countNotStarted) == dataSet.Tables[0].Rows.Count)) return 2;
            if (countNotStarted == dataSet.Tables[0].Rows.Count) return 1;
            return 0;

        }

        private Hashtable getSalesPersonStatuses()
        {
            Database database = infojetContext.systemDatabase;

            DatabaseQuery databaseQuery = database.prepare("SELECT [Web User Account No], COUNT(*) FROM [" + database.getTableName("Web Cart Line") + "] WHERE [Extra 2] = @salesId GROUP BY [Web User Account No]");
            databaseQuery.addStringParameter("@salesId", this._code, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();

            Hashtable countTable = new Hashtable();
            while (dataReader.Read())
            {
                int count = 0;
                if (!dataReader.IsDBNull(1)) count = int.Parse(dataReader.GetValue(1).ToString());
                countTable.Add(dataReader.GetValue(0).ToString(), count);
            }

            dataReader.Close();

            DatabaseQuery databaseQuery2 = database.prepare("SELECT [Web User Account No], COUNT(*) FROM [" + database.getTableName("Web Cart Line") + "] WHERE [Extra 2] = @salesId AND [Extra 3] = '1' GROUP BY [Web User Account No]");
            databaseQuery2.addStringParameter("@salesId", this._code, 20);

            dataReader = databaseQuery2.executeQuery();

            Hashtable countReleasedTable = new Hashtable();

            while (dataReader.Read())
            {
                int countReleased = 0;
                if (!dataReader.IsDBNull(1)) countReleased = int.Parse(dataReader.GetValue(1).ToString());
                countReleasedTable.Add(dataReader.GetValue(0).ToString(), countReleased);

            }

            dataReader.Close();

            Hashtable statusTable = new Hashtable();

            foreach (DictionaryEntry entry in countTable)
            {
                if ((int)entry.Value == 0)
                    statusTable.Add(entry.Key, 1);
                else
                    statusTable.Add(entry.Key, 0);                
            }

            foreach (DictionaryEntry entry in countReleasedTable)
            {
                if ((int)countTable[entry.Key] == (int)entry.Value) statusTable[entry.Key] = 2;
            }

            return statusTable;
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
        /// Kontrollnr.
        /// </summary>
        public string controlNo { set { _controlNo = value; } get { return _controlNo; } }

        /// <summary>
        /// Startdatum.
        /// </summary>
        public DateTime startDate { set { _startDate = value; } get { return _startDate; } }

        /// <summary>
        /// Leveransdatum startorder.
        /// </summary>
        public DateTime startOrderDelivery { set { _startOrderDelivery = value; } get { return _startOrderDelivery; } }

        /// <summary>
        /// Orderdatum.
        /// </summary>
        //public DateTime orderDate { set { } get { return _startDate.AddDays(29); } }
        public DateTime orderDate { set { } get { return _orderWeek; } }

        /// <summary>
        /// Leveransdatum slutorder.
        /// </summary>
        public DateTime orderDeliveryDate { set { } get { return _startDate.AddDays(38); } }

        /// <summary>
        /// Efterbeställning öppnas.
        /// </summary>
        public DateTime supplementaryOrderStartDate { set { } get { return _startDate.AddDays(39); } }

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
        public int soldPackages { get { return _soldPackages; } set { _soldPackages = value; } }

        /// <summary>
        /// Belopp sålda, ej inskickade, paket i gruppen.
        /// </summary>
        public float soldAmount { get { return _soldAmount; } set { _soldAmount = value; } }

        /// <summary>
        /// Antal sålda, ej inskickade, paket i gruppen.
        /// </summary>
        public int soldPackagesInclGiftCards { get { return _soldPackagesInclGiftCards; } set { _soldPackagesInclGiftCards = value; } }


        /// <summary>
        /// Vinst sålda, ej inskickade, paket i gruppen.
        /// </summary>
        public float soldProfitAmount { get { return soldPackagesInclGiftCards * _profit; } set { } }

        /// <summary>
        /// Antal sålda, inskickade, paket i gruppen.
        /// </summary>
        public int historyPackages { get { return _historyPackages; } set { _historyPackages = value; } }

        /// <summary>
        /// Belopp sålda, inskickade, paket i gruppen.
        /// </summary>
        public float historyAmount { get { return _historyAmount; } set { _historyAmount = value; } }

        /// <summary>
        /// Antal sålda, inskickade, paket i gruppen.
        /// </summary>
        public int historyPackagesInclGiftCards { get { return _historyPackagesInclGiftCards; } set { _historyPackagesInclGiftCards = value; } }

        /// <summary>
        /// Vinst sålda, inskickade, paket i gruppen.
        /// </summary>
        public float historyProfitAmount { get { return historyPackagesInclGiftCards * _profit; } set { } }
 
 
        /// <summary>
        /// Antal paket att skicka in vid nästa orderläggning.
        /// </summary>
        public string pageUrl { set { _pageUrl = value; } get { return _pageUrl; } }
        public string selected { get { if (_selected) return "checked=\"checked\""; return ""; } }
        //public Hashtable userHistoryPackageTable { get { return _userHistoryPackageTable; } set { _userHistoryPackageTable = value; } }
        public OrderItemCollection sentLinesCollection { get { return _sentLinesCollection; } set { _sentLinesCollection = value; } }
        public SalesPerson salesPerson { get { return _salesPerson; } set { _salesPerson = value; } }


        /// <summary>
        /// Nästa förväntade ordertyp.
        /// </summary>
        public int nextOrderType { get { return _nextOrderType; } }

        /// <summary>
        /// Användarkontonr för grupp-kontaktperson.
        /// </summary>
        public string subContWebUserAccountNo { get { return _subContWebUserAccountNo; } set { _subContWebUserAccountNo = value; } }


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
        public bool additionalOrder { get { return _additionalOrder; } set { } }

        /// <summary>
        /// 
        /// </summary>
        public bool primaryOrderExists { get { return _primaryOrderExists; } set { _primaryOrderExists = value; } }

        /// <summary>
        /// Kundnamn
        /// </summary>
        public string customerName { get { return _customerName; } set { _customerName = value; } }


 

        /// <summary>
        /// Vinst per paket.
        /// </summary>
        public float profit { get { return _profit; } }


        /// <summary>
        /// Status.
        /// </summary>
        public int status
        {
            get
            {
                if ((_status == null) || (_status == "")) _status = getStatus(infojetContext).ToString();
                return int.Parse(_status);
            }
            set { }
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

            SqlDataReader dataReader = infojetContext.systemDatabase.query("SELECT [No_] FROM [" + infojetContext.systemDatabase.getTableName("Sales Header") + "] WHERE [FörsäljningsID] =  '" + this.code + "' AND [Ordertyp] = 3");
            if (dataReader.Read())
            {
                dataReader.Close();
                return false;
            }
            dataReader.Close();

            dataReader = infojetContext.systemDatabase.query("SELECT [No_] FROM [" + infojetContext.systemDatabase.getTableName("Sales Invoice Header") + "] WHERE [FörsäljningsID] =  '" + this.code + "' AND [Odertyp] = 3");
            if (dataReader.Read())
            {
                dataReader.Close();
                return false;
            }
            dataReader.Close();

            int noOfPackages = 0;

            dataReader = infojetContext.systemDatabase.query("SELECT SUM(l.[Quantity]) FROM [" + infojetContext.systemDatabase.getTableName("Sales Line") + "] l, [" + infojetContext.systemDatabase.getTableName("Sales Header") + "] h WHERE l.[Document Type] = h.[Document Type] AND l.[Document No_] = h.[No_] AND h.[FörsäljningsID] =  '" + this.code + "' AND l.[Unit of Measure Code] = 'PKT'");
            if (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0)) noOfPackages = noOfPackages + (int)dataReader.GetDecimal(0);
            }
            dataReader.Close();

            dataReader = infojetContext.systemDatabase.query("SELECT SUM(l.[Quantity]) FROM [" + infojetContext.systemDatabase.getTableName("Sales Invoice Line") + "] l, [" + infojetContext.systemDatabase.getTableName("Sales Invoice Header") + "] h WHERE l.[Document No_] = h.[No_] AND h.[FörsäljningsID] =  '" + this.code + "' AND l.[Unit of Measure Code] = 'PKT'");
            if (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0)) noOfPackages = noOfPackages + (int)dataReader.GetDecimal(0);
            }
            dataReader.Close();

            if (noOfPackages < salesIdSetup.freeFreightPackageLimit) return false;

            dataReader = infojetContext.systemDatabase.query("SELECT [Order Date] FROM [" + infojetContext.systemDatabase.getTableName("Sales Invoice Header") + "] WHERE [FörsäljningsID] =  '" + this.code + "' AND [Odertyp] = 2");
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

            SqlDataReader dataReader = infojetContext.systemDatabase.query("SELECT [Preliminärt sortiment] FROM [" + infojetContext.systemDatabase.getTableName("Item") + "] WHERE [No_] = '" + this._itemSelection + "'");
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

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Item No_], SUM([Ordered Qty]) FROM [" + infojetContext.systemDatabase.getTableName("Web Order Line") + "] WHERE [FörsäljningsID] = @salesId GROUP BY [Item No_]");
            databaseQuery.addStringParameter("@salesId", this.code, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            while (dataReader.Read())
            {
                float soldQuantity = 0;
                if (!dataReader.IsDBNull(1)) soldQuantity = float.Parse(dataReader.GetValue(1).ToString());

                soldQuantitiesTable.Add(dataReader.GetValue(0).ToString(), soldQuantity);
            }

            dataReader.Close();

            databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Item No_], SUM([Quantity]) FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] WHERE [Extra 2] = @salesId GROUP BY [Item No_]");
            databaseQuery.addStringParameter("@salesId", this.code, 20);

            dataReader = databaseQuery.executeQuery();
            while (dataReader.Read())
            {
                float soldQuantity = 0;
                if (!dataReader.IsDBNull(1)) soldQuantity = float.Parse(dataReader.GetValue(1).ToString());

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

        public static SalesIDCollection getSalesIds(Navipro.Infojet.Lib.Infojet infojetContext, string webUserAccountNo)
        {
            Database database = infojetContext.systemDatabase;

            Hashtable soldPackagesTable = getSoldPackagesGroup(infojetContext, webUserAccountNo);
            Hashtable historyPackagesTable = getHistorySoldPackagesGroup(infojetContext, webUserAccountNo);


            //Hashtable soldPackagesTable = new Hashtable();
            //Hashtable historyPackagesTable = new Hashtable();

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT s.[FörsäljningsID], [Försäljningskoncept], [Antal säljare], [Visningskasse], [Artikelsortiment], [Ordervecka Slutorder], [Stängningsdatum], [Benämning], [Kund], [Contact Web User Account No_], [User Reg_ Web User Account No_], [Next Ordertype], [Sub Cont Web User Account No_], [Additional order], [Profit], [Profit Currency], [Försäljningsstartdatum], [Leveransdatum startorder], [Control No_], c.[Name] FROM [" + infojetContext.systemDatabase.getTableName("FörsäljningsID") + "] s LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Customer") + "] c ON s.[Kund] = c.[No_], [" + infojetContext.systemDatabase.getTableName("SalesID WebUserAccount") + "] w WHERE s.[FörsäljningsID] = w.[FÖrsäljningsID] AND w.[Web User Account] = @webUserAccountNo AND [Stängningsdatum] > GETDATE()");
            databaseQuery.addStringParameter("@webUserAccountNo", webUserAccountNo, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();

            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            SalesIDCollection salesIdCollection = new SalesIDCollection();
            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                SalesID salesID = new SalesID(infojetContext, dataSet.Tables[0].Rows[i]);
                
                
                WebUserAccount webUserAccount = salesID.contactWebUserAccount;               
                
                SalesID soldPackages = (SalesID)soldPackagesTable[salesID.code];
                if (soldPackages != null)
                {
                    salesID.soldPackages = soldPackages.soldPackages;
                    salesID.soldPackagesInclGiftCards = soldPackages.soldPackagesInclGiftCards;
                    salesID.soldAmount = soldPackages.soldAmount; // + (salesID.profit * soldPackages.soldPackages);
                }


                SalesID historyPackages = (SalesID)historyPackagesTable[salesID.code];
                if (historyPackages != null)
                {
                    salesID.historyPackages = historyPackages.historyPackages;
                    salesID.historyPackagesInclGiftCards = historyPackages.historyPackagesInclGiftCards;
                    salesID.historyAmount = historyPackages.historyAmount;// +(salesID.profit * historyPackages.historyPackages);

                }

                
                salesID.primaryOrderExists = salesID.checkOrderSent();

                int status = salesID.status;
                
                salesIdCollection.Add(salesID);
                i++;
            }

            return salesIdCollection;
        }

        public static SalesIDCollection getAllSalesIds(Navipro.Infojet.Lib.Infojet infojetContext, string customerNo)
        {
            Database database = infojetContext.systemDatabase;

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT s.[FörsäljningsID], [Försäljningskoncept], [Antal säljare], [Visningskasse], [Artikelsortiment], [Ordervecka Slutorder], [Stängningsdatum], [Benämning], [Kund], [Contact Web User Account No_], [User Reg_ Web User Account No_], [Next Ordertype], [Sub Cont Web User Account No_], [Additional order], [Profit], [Profit Currency], [Försäljningsstartdatum], [Leveransdatum startorder], [Control No_], c.[Name] FROM [" + infojetContext.systemDatabase.getTableName("FörsäljningsID") + "] s LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Customer") + "] c ON s.[Kund] = c.[No_] WHERE s.[Kund] = @customerNo");
            databaseQuery.addStringParameter("@customerNo", customerNo, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();

            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            SalesIDCollection salesIdCollection = new SalesIDCollection();
            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                SalesID salesID = new SalesID(infojetContext, dataSet.Tables[0].Rows[i]);
                WebUserAccount webUserAccount = salesID.contactWebUserAccount;
                int status = salesID.status;
                salesIdCollection.Add(salesID);
                i++;
            }

            return salesIdCollection;
        }


        public static SalesIDCollection getSalesIds(Navipro.Infojet.Lib.Infojet infojetContext, string customerNo, DateTime startDate)
        {
            Database database = infojetContext.systemDatabase;

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT s.[FörsäljningsID], [Försäljningskoncept], [Antal säljare], [Visningskasse], [Artikelsortiment], [Ordervecka Slutorder], [Stängningsdatum], [Benämning], [Kund], [Contact Web User Account No_], [User Reg_ Web User Account No_], [Next Ordertype], [Sub Cont Web User Account No_], [Additional order], [Profit], [Profit Currency], [Försäljningsstartdatum], [Leveransdatum startorder], [Control No_], c.[Name] FROM [" + infojetContext.systemDatabase.getTableName("FörsäljningsID") + "] s LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Customer") + "] c ON s.[Kund] = c.[No_] WHERE s.[Försäljningsstartdatum] = @startDate AND s.[Kund] = @customerNo AND [Stängningsdatum] > GETDATE()");
            databaseQuery.addStringParameter("@customerNo", customerNo, 20);
            databaseQuery.addDateTimeParameter("@startDate", startDate);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();

            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            SalesIDCollection salesIdCollection = new SalesIDCollection();
            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                SalesID salesID = new SalesID(infojetContext, dataSet.Tables[0].Rows[i]);
                WebUserAccount webUserAccount = salesID.contactWebUserAccount;
                int status = salesID.status;
                salesIdCollection.Add(salesID);
                i++;
            }

            return salesIdCollection;
        }

        public static bool unConfirmedAgreementsExists(Navipro.Infojet.Lib.Infojet infojetContext, string webUserAccountNo)
        {
            Database database = infojetContext.systemDatabase;

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT COUNT(*) FROM [" + infojetContext.systemDatabase.getTableName("FörsäljningsID") + "] WHERE [Contact Web User Account No_] = @webUserAccountNo AND [Agreement Approved Date] < '1900-01-01' AND [Stängningsdatum] > GETDATE()");
            databaseQuery.addStringParameter("@webUserAccountNo", webUserAccountNo, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();

            int count = 0;

            if (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0)) count = int.Parse(dataReader.GetValue(0).ToString());
            }
            dataReader.Close();

            if (count > 0) return true;
            return false;
        }

        public void updateSoldPackages()
        {
            updateSoldPackages("");
        }

        public void updateSoldPackages(string webUserAccountNo)
        {
            Hashtable giftCardTable = SalesID.getGiftCardItems(infojetContext);

            Customer customer = new Customer(infojetContext, customerNo);
            float vatFactor = customer.getVatFactor("25");
            float amount = 0;

            string webUserAccountNoQuery = "";
            if (webUserAccountNo != "") webUserAccountNoQuery = "AND [Web User Account No] = @webUserAccountNo";

            DatabaseQuery databaseQuery2 = infojetContext.systemDatabase.prepare("SELECT [Item No_], SUM(Quantity), SUM([Quantity] * [Unit Price]) FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] WHERE [Extra 2] = @salesId "+webUserAccountNoQuery+" GROUP BY [Item No_]");
            databaseQuery2.addStringParameter("@salesId", code, 20);
            databaseQuery2.addStringParameter("@webUserAccountNo", webUserAccountNo, 20);

            SqlDataReader dataReader = databaseQuery2.executeQuery();

            while (dataReader.Read())
            {

                if (!dataReader.IsDBNull(1))
                {
                    _soldPackages = _soldPackages + (int)float.Parse(dataReader.GetValue(1).ToString());

                    if (giftCardTable.Contains(dataReader.GetValue(0).ToString()))
                    {
                        _soldPackagesInclGiftCards = _soldPackagesInclGiftCards + (int)float.Parse(dataReader.GetValue(1).ToString()) * (int)giftCardTable[dataReader.GetValue(0).ToString()];
                    }
                    else
                    {
                        _soldPackagesInclGiftCards = _soldPackagesInclGiftCards + (int)float.Parse(dataReader.GetValue(1).ToString());
                    }
                }
                if (!dataReader.IsDBNull(2))
                {
                    amount = amount + float.Parse(dataReader.GetValue(2).ToString());
                }
            }
            _soldAmount = ((vatFactor * amount) + (_soldPackages * profit));

            dataReader.Close();

        }

        public SalesIDAgreementCollection getAgreement(Navipro.Infojet.Lib.Infojet infojetContext, string languageCode)
        {
            SalesIDAgreementCollection salesIdAgreementCollection = new SalesIDAgreementCollection();

            System.Data.SqlClient.SqlDataReader dataReader = infojetContext.systemDatabase.query("SELECT [Text], [Headline], [Bold] FROM [" + infojetContext.systemDatabase.getTableName("SalesID Agreement") + "] WHERE [Language Code] = '" + languageCode + "' AND [SalesID] = '" + code + "'");
            while (dataReader.Read())
            {
                SalesIDAgreementLine line = new SalesIDAgreementLine();
                line.text = dataReader.GetValue(0).ToString();

                if (dataReader.GetValue(1).ToString() == "1") line.header = true;
                if (dataReader.GetValue(2).ToString() == "1") line.bold = true;

                salesIdAgreementCollection.Add(line);

            }
            dataReader.Close();

            return salesIdAgreementCollection;
        }

        /// <summary>
        /// Kontrollerar om det finns utrymme för att skapa fler säljare.
        /// </summary>
        /// <param name="webUserAccountNo">Användarkontonr.</param>
        /// <returns>True eller False.</returns>
        public int checkAvailableSalesPersonQuota()
        {
            int noOfCreatedSalesPersons = SalesIDSalesPerson.countSalesPersons(infojetContext, code);

            return noOfSalesPersons - noOfCreatedSalesPersons;

        }


        /// <summary>
        /// Kontrollerar om ett givet användar-ID är kontaktperson i NÅGOT av försäljnings-ID:n
        /// </summary>
        /// <param name="webUserAccountNo">Användarkontonr.</param>
        /// <returns>True eller False.</returns>
        public static bool checkIsContactPerson(Navipro.Infojet.Lib.Infojet infojetContext, string webUserAccountNo)
        {
            Database database = infojetContext.systemDatabase;

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Contact Web User Account No_] FROM [" + infojetContext.systemDatabase.getTableName("FörsäljningsID") + "] WITH (NOLOCK) WHERE [Contact Web User Account No_] = @webUserAccountNo AND [Stängningsdatum] > GETDATE()");
            databaseQuery.addStringParameter("@webUserAccountNo", webUserAccountNo, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            bool found = dataReader.Read();

            dataReader.Close();

            return found;
        }

        public static bool checkIsSubContactPerson(Navipro.Infojet.Lib.Infojet infojetContext, string webUserAccountNo)
        {
            Database database = infojetContext.systemDatabase;

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Sub Cont Web User Account No_] FROM [" + infojetContext.systemDatabase.getTableName("FörsäljningsID") + "] WITH (NOLOCK) WHERE [Sub Cont Web User Account No_] = @webUserAccountNo AND [Stängningsdatum] > GETDATE()");
            databaseQuery.addStringParameter("@webUserAccountNo", webUserAccountNo, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            bool found = dataReader.Read();

            dataReader.Close();

            return found;
        }

        public static float calcCurrentBalance(Navipro.Infojet.Lib.Infojet infojetContext, string customerNo)
        {
            SqlDataReader dataReader = infojetContext.systemDatabase.query("SELECT SUM(d.Amount) FROM [" + infojetContext.systemDatabase.getTableName("Detailed Cust_ Ledg_ Entry") + "] d, [" + infojetContext.systemDatabase.getTableName("Customer") + "] c WHERE d.[Customer No_] = '" + customerNo + "' AND c.[No_] = d.[Customer No_] AND d.[Currency Code] = c.[Currency Code]");

            float balance = 0;

            while (dataReader.Read())
            {
                try
                {
                    balance = float.Parse(dataReader.GetValue(0).ToString());
                }
                catch (Exception)
                { }
            }

            dataReader.Close();

            return balance;
        }

        /// <summary>
        /// Beräknar totalt antal fakturerade/ej fakturerade paket.
        /// </summary>
        /// <param name="itemStatus">Artikelstatus, heltal.</param>
        /// <returns>Ett heltal.</returns>
        public int countHistoryItems(bool includeInvoice, bool includeOrder, bool includePreOrder, bool includeCart)
        {
            int quantity = 0;

            if (includeInvoice)
            {
                SqlDataAdapter dataAdapter = infojetContext.systemDatabase.dataAdapterQuery("SELECT [No_] FROM [" + infojetContext.systemDatabase.getTableName("Sales Invoice Header") + "] WITH (NOLOCK) WHERE [FörsäljningsID] = '" + this.code + "'");

                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);

                int i = 0;
                while (i < dataSet.Tables[0].Rows.Count)
                {
                    string docNo = dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();

                    SqlDataReader sqlDataReader = infojetContext.systemDatabase.query("SELECT SUM(l.[Quantity]) FROM [" + infojetContext.systemDatabase.getTableName("Sales Invoice Line") + "] l WHERE l.[Document No_] = '" + docNo + "' AND l.[Type] = 2  AND l.[Unit of Measure Code] = 'PKT'");
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

                dataAdapter = infojetContext.systemDatabase.dataAdapterQuery("SELECT [No_] FROM [" + infojetContext.systemDatabase.getTableName("Sales Cr_Memo Header") + "] WITH (NOLOCK) WHERE [FörsäljningsID] = '" + this.code + "'");

                dataSet = new DataSet();
                dataAdapter.Fill(dataSet);

                i = 0;
                while (i < dataSet.Tables[0].Rows.Count)
                {
                    string docNo = dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();

                    SqlDataReader sqlDataReader = infojetContext.systemDatabase.query("SELECT SUM(l.[Quantity]) FROM [" + infojetContext.systemDatabase.getTableName("Sales Cr_Memo Line") + "] l WHERE l.[Document No_] = '" + docNo + "' AND l.[Type] = 2  AND l.[Unit of Measure Code] = 'PKT'");
                    if (sqlDataReader.Read())
                    {
                        if (!sqlDataReader.IsDBNull(0))
                        {
                            quantity = quantity - (int)float.Parse(sqlDataReader.GetValue(0).ToString());
                        }
                    }
                    sqlDataReader.Close();

                    i++;
                }

            }

            if (includeOrder)
            {
                SqlDataAdapter dataAdapter = infojetContext.systemDatabase.dataAdapterQuery("SELECT [No_] FROM [" + infojetContext.systemDatabase.getTableName("Sales Header") + "] WITH (NOLOCK) WHERE [Document Type] = 1 AND [FörsäljningsID] = '" + this.code + "'");

                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);

                int i = 0;
                while (i < dataSet.Tables[0].Rows.Count)
                {
                    string docNo = dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();

                    SqlDataReader sqlDataReader = infojetContext.systemDatabase.query("SELECT SUM(l.[Quantity]) FROM [" + infojetContext.systemDatabase.getTableName("Sales Line") + "] l WHERE l.[Document Type] = 1 AND l.[Document No_] = '" + docNo + "' AND l.[Type] = 2  AND l.[Unit of Measure Code] = 'PKT'");
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
            }

            if (includePreOrder)
            {
                SqlDataReader sqlDataReader = infojetContext.systemDatabase.query("SELECT SUM(l.[Quantity]) FROM [" + infojetContext.systemDatabase.getTableName("Web Sales Line") + "] l, [" + infojetContext.systemDatabase.getTableName("Web Sales Header") + "] h WHERE l.[Document Type] = h.[Document Type] AND l.[Document No_] = h.[No_] AND h.[Transfered] = 0 AND l.[Extra 2] = '" + this.code + "' AND l.[Unit Of Measure Code] = 'PKT'");
                if (sqlDataReader.Read())
                {
                    if (!sqlDataReader.IsDBNull(0))
                    {
                        quantity = quantity + (int)float.Parse(sqlDataReader.GetValue(0).ToString());
                    }
                }
                sqlDataReader.Close();

            }

            if (includeCart)
            {
                SqlDataReader sqlDataReader = infojetContext.systemDatabase.query("SELECT SUM(l.[Quantity]) FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] l WHERE l.[Extra 2] = '" + this.code + "' AND l.[Unit Of Measure Code] = 'PKT'");
                if (sqlDataReader.Read())
                {
                    if (!sqlDataReader.IsDBNull(0))
                    {
                        quantity = quantity + (int)float.Parse(sqlDataReader.GetValue(0).ToString());
                    }
                }
                sqlDataReader.Close();

            }

            return quantity;


        }


        public static SalesIDStatistics getStatistics(Navipro.Infojet.Lib.Infojet infojetContext, string customerNo, DateTime startDate)
        {
            SalesIDStatistics salesIdStats = new SalesIDStatistics();
            salesIdStats.balance = calcCurrentBalance(infojetContext, customerNo);

            SalesIDCollection salesIdCollection = getSalesIds(infojetContext, customerNo, startDate);

            int i = 0;
            while (i < salesIdCollection.Count)
            {
                int packages = salesIdCollection[i].countHistoryItems();
                salesIdStats.soldPackages = salesIdStats.soldPackages + packages;
                salesIdStats.profitAmount = salesIdStats.profitAmount + (salesIdStats.soldPackages * salesIdCollection[i].profit);    

                i++;
            }

            return salesIdStats;

        }

        public static Hashtable getSoldPackagesGroup(Navipro.Infojet.Lib.Infojet infojetContext, string webUserAccountNo)
        {
            Hashtable salesIdTable = new Hashtable();
            Hashtable giftCardTable = getGiftCardItems(infojetContext);

            DatabaseQuery databaseQuery2 = infojetContext.systemDatabase.prepare("SELECT [Extra 2], [Item No_], SUM(Quantity), SUM([Quantity] * [Unit Price]), SUM([Quantity] * [Profit]) FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] l, [" + infojetContext.systemDatabase.getTableName("SalesID WebUserAccount") + "] w, [" + infojetContext.systemDatabase.getTableName("FörsäljningsID") + "] s WHERE w.[Web User Account] = @webUserAccountNo AND w.[FÖrsäljningsID] = l.[Extra 2] AND s.[FörsäljningsID] = l.[Extra 2] GROUP BY [Extra 2], [Item No_]");
            databaseQuery2.addStringParameter("@webUserAccountNo", webUserAccountNo, 20);

            SqlDataReader dataReader = databaseQuery2.executeQuery();
            
            string debugger = "";

            while (dataReader.Read())
            {
                SalesID salesID = new SalesID();
                salesID.code = dataReader.GetValue(0).ToString();
                if (salesIdTable.Contains(salesID.code)) salesID = (SalesID)salesIdTable[salesID.code];


                if (!dataReader.IsDBNull(2))
                {
                    salesID.soldPackages = salesID.soldPackages + (int)float.Parse(dataReader.GetValue(2).ToString());
                    int giftCardQty = 0;
                    if (giftCardTable.Contains(dataReader.GetValue(1).ToString()))
                    {
                        salesID.soldPackagesInclGiftCards = salesID.soldPackagesInclGiftCards + ((int)float.Parse(dataReader.GetValue(2).ToString()) * (int)giftCardTable[dataReader.GetValue(1).ToString()]);
                        giftCardQty = (int)giftCardTable[dataReader.GetValue(1).ToString()];
                    }
                    else
                    {
                        salesID.soldPackagesInclGiftCards = salesID.soldPackagesInclGiftCards + (int)float.Parse(dataReader.GetValue(2).ToString());
                    }

                    if (!dataReader.IsDBNull(3))
                    {
                        salesID.soldAmount = salesID.soldAmount + float.Parse(dataReader.GetValue(3).ToString());
                    }
                    if (!dataReader.IsDBNull(4))
                    {
                        if (giftCardTable.Contains(dataReader.GetValue(1).ToString()))
                        {
                            float profitPerQty = float.Parse(dataReader.GetValue(4).ToString()) / float.Parse(dataReader.GetValue(2).ToString());
                            salesID.soldAmount = salesID.soldAmount + (profitPerQty * (int)giftCardTable[dataReader.GetValue(1).ToString()] * float.Parse(dataReader.GetValue(2).ToString()));


                        }
                        else
                        {
                            salesID.soldAmount = salesID.soldAmount + float.Parse(dataReader.GetValue(4).ToString());
                        }

                        
                    }

                    debugger = debugger + salesID.code + ";" + dataReader.GetValue(2).ToString() + ";" + giftCardQty + ";" + dataReader.GetValue(3).ToString() + ";" + dataReader.GetValue(4).ToString() + ";" + salesID.soldAmount + "\n";

                }


                if (salesIdTable.Contains(salesID.code))
                    salesIdTable[salesID.code] = salesID;
                else
                    salesIdTable.Add(salesID.code, salesID);
            }

            dataReader.Close();

            //System.Diagnostics.EventLog.WriteEntry("PARTNER", debugger);


            return salesIdTable;

        }

        public static Hashtable getHistorySoldPackagesGroup(Navipro.Infojet.Lib.Infojet infojetContext, string webUserAccountNo)
        {
            Hashtable salesIdTable = new Hashtable();
            Hashtable giftCardTable = getGiftCardItems(infojetContext);


            DatabaseQuery databaseQuery2 = infojetContext.systemDatabase.prepare("SELECT [Extra 2], [Item No_], SUM(Quantity), SUM([Quantity] * [Unit Price]), SUM([Quantity] * [Profit]) FROM [" + infojetContext.systemDatabase.getTableName("Web Sales Line") + "] l, [" + infojetContext.systemDatabase.getTableName("SalesID WebUserAccount") + "] w, [" + infojetContext.systemDatabase.getTableName("FörsäljningsID") + "] s WHERE w.[Web User Account] = @webUserAccountNo AND w.[FÖrsäljningsID] = l.[Extra 2] AND s.[FörsäljningsID] = l.[Extra 2] GROUP BY [Extra 2], [Item No_]");
            databaseQuery2.addStringParameter("@webUserAccountNo", webUserAccountNo, 20);

            SqlDataReader dataReader = databaseQuery2.executeQuery();

            while (dataReader.Read())
            {
                SalesID salesID = new SalesID();
                salesID.code = dataReader.GetValue(0).ToString();
                if (salesIdTable.Contains(salesID.code)) salesID = (SalesID)salesIdTable[salesID.code];

                if (!dataReader.IsDBNull(2))
                {
                    salesID.historyPackages = salesID.historyPackages + (int)float.Parse(dataReader.GetValue(2).ToString());

                    if (giftCardTable.Contains(dataReader.GetValue(1).ToString()))
                    {
                        salesID.historyPackagesInclGiftCards = salesID.historyPackagesInclGiftCards + ((int)float.Parse(dataReader.GetValue(2).ToString()) * (int)giftCardTable[dataReader.GetValue(1).ToString()]);
                    }
                    else
                    {
                        salesID.historyPackagesInclGiftCards = salesID.historyPackagesInclGiftCards + (int)float.Parse(dataReader.GetValue(2).ToString());
                    }

                    if (!dataReader.IsDBNull(3))
                    {
                        salesID.historyAmount = salesID.historyAmount + float.Parse(dataReader.GetValue(3).ToString());
                    }
                    if (!dataReader.IsDBNull(4))
                    {

                        if (giftCardTable.Contains(dataReader.GetValue(1).ToString()))
                        {
                            float profitPerQty = float.Parse(dataReader.GetValue(4).ToString()) / float.Parse(dataReader.GetValue(2).ToString());
                            salesID.historyAmount = salesID.historyAmount + (profitPerQty * (int)giftCardTable[dataReader.GetValue(1).ToString()] * float.Parse(dataReader.GetValue(2).ToString()));

                        }
                        else
                        {
                            salesID.historyAmount = salesID.historyAmount + float.Parse(dataReader.GetValue(4).ToString());
                        }
                    }

                }


                if (salesIdTable.Contains(salesID.code))
                    salesIdTable[salesID.code] = salesID;
                else
                    salesIdTable.Add(salesID.code, salesID);
            }

            dataReader.Close();


            return salesIdTable;

        }


        public static Hashtable getSoldPackages(Navipro.Infojet.Lib.Infojet infojetContext, string salesId)
        {
            Hashtable salesPersonTable = new Hashtable();
            
            Hashtable giftCardTable = getGiftCardItems(infojetContext);

            SalesID currentSalesID = new SalesID(infojetContext, salesId);
            Customer customer = new Customer(infojetContext, currentSalesID.customerNo);
            float vatFactor = customer.getVatFactor("25");

            DatabaseQuery databaseQuery2 = infojetContext.systemDatabase.prepare("SELECT [Web User Account No], [Item No_], SUM(Quantity), SUM([Quantity] * [Unit Price]) FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] WHERE [Extra 2] = @salesId GROUP BY [Web User Account No], [Item No_]");
            databaseQuery2.addStringParameter("@salesId", salesId, 20);

            SqlDataReader dataReader = databaseQuery2.executeQuery();

            while (dataReader.Read())
            {
                SalesIDSalesPerson salesIdSalesPerson = new SalesIDSalesPerson(infojetContext.systemDatabase, salesId, dataReader.GetValue(0).ToString());
                if (salesPersonTable.Contains(salesIdSalesPerson.webUserAccountNo)) salesIdSalesPerson = (SalesIDSalesPerson)salesPersonTable[salesIdSalesPerson.webUserAccountNo];

                int packagesInclGiftCards = 0;
                if (!dataReader.IsDBNull(2))
                {
                    if (giftCardTable.Contains(dataReader.GetValue(1).ToString()))
                    {
                        packagesInclGiftCards = ((int)float.Parse(dataReader.GetValue(2).ToString()) * (int)giftCardTable[dataReader.GetValue(1).ToString()]);
                    }
                    else
                    {
                        packagesInclGiftCards = (int)float.Parse(dataReader.GetValue(2).ToString());
                    }
                    salesIdSalesPerson.soldPackages = salesIdSalesPerson.soldPackages + (int)float.Parse(dataReader.GetValue(2).ToString());
                    salesIdSalesPerson.soldPackagesInclGiftCards = salesIdSalesPerson.soldPackagesInclGiftCards + packagesInclGiftCards;
                }
                if (!dataReader.IsDBNull(3))
                {
                    if (giftCardTable.Contains(dataReader.GetValue(1).ToString()))
                    {
                        salesIdSalesPerson.soldAmount = salesIdSalesPerson.soldAmount + (float.Parse(dataReader.GetValue(3).ToString()) + (currentSalesID.profit * (int)float.Parse(dataReader.GetValue(2).ToString()) * (int)giftCardTable[dataReader.GetValue(1).ToString()]));
                    }
                    else
                    {
                        salesIdSalesPerson.soldAmount = salesIdSalesPerson.soldAmount + (float.Parse(dataReader.GetValue(3).ToString()) + (currentSalesID.profit * (int)float.Parse(dataReader.GetValue(2).ToString())));
                    }
                }

                if (salesPersonTable.Contains(salesIdSalesPerson.webUserAccountNo))
                    salesPersonTable[salesIdSalesPerson.webUserAccountNo] = salesIdSalesPerson;
                else
                    salesPersonTable.Add(salesIdSalesPerson.webUserAccountNo, salesIdSalesPerson);
            }

            dataReader.Close();


            return salesPersonTable;

        }

        public static Hashtable getHistorySoldPackages(Navipro.Infojet.Lib.Infojet infojetContext, string salesId)
        {
            Hashtable salesPersonTable = new Hashtable();
            
            //Empty giftcard table
            //Hashtable giftCardTable = getGiftCardItems(infojetContext);
            Hashtable giftCardTable = new Hashtable();


            SalesID currentSalesID = new SalesID(infojetContext, salesId);
            Customer customer = new Customer(infojetContext, currentSalesID.customerNo);
            float vatFactor = customer.getVatFactor("25");

            DatabaseQuery databaseQuery2 = infojetContext.systemDatabase.prepare("SELECT [Web User Account No_], [Item No_], SUM(Quantity), SUM([Quantity] * [Unit Price]) FROM [" + infojetContext.systemDatabase.getTableName("Web Sales Line") + "] WHERE [Extra 2] = @salesId GROUP BY [Web User Account No_], [Item No_]");
            databaseQuery2.addStringParameter("@salesId", salesId, 20);

            SqlDataReader dataReader = databaseQuery2.executeQuery();

            while (dataReader.Read())
            {
                SalesIDSalesPerson salesIdSalesPerson = new SalesIDSalesPerson(infojetContext.systemDatabase, salesId, dataReader.GetValue(0).ToString());
                if (salesPersonTable.Contains(salesIdSalesPerson.webUserAccountNo)) salesIdSalesPerson = (SalesIDSalesPerson)salesPersonTable[salesIdSalesPerson.webUserAccountNo];

                int packagesInclGiftCards = 0;
                if (!dataReader.IsDBNull(2))
                {
                    if (giftCardTable.Contains(dataReader.GetValue(1).ToString()))
                    {
                        packagesInclGiftCards = ((int)float.Parse(dataReader.GetValue(2).ToString()) * (int)giftCardTable[dataReader.GetValue(1).ToString()]);
                    }
                    else
                    {
                        packagesInclGiftCards = (int)float.Parse(dataReader.GetValue(2).ToString());
                    }
                    salesIdSalesPerson.historyPackages = salesIdSalesPerson.historyPackages + (int)float.Parse(dataReader.GetValue(2).ToString());
                    salesIdSalesPerson.historyPackagesInclGiftCards = salesIdSalesPerson.historyPackagesInclGiftCards + packagesInclGiftCards;
                }
                if (!dataReader.IsDBNull(3))
                {
                    if (giftCardTable.Contains(dataReader.GetValue(1).ToString()))
                    {
                        salesIdSalesPerson.historyAmount = salesIdSalesPerson.historyAmount + (float.Parse(dataReader.GetValue(3).ToString()) + (currentSalesID.profit * (int)float.Parse(dataReader.GetValue(2).ToString()) * (int)giftCardTable[dataReader.GetValue(1).ToString()]));
                    }
                    else
                    {
                        salesIdSalesPerson.historyAmount = salesIdSalesPerson.historyAmount + (float.Parse(dataReader.GetValue(3).ToString()) + (currentSalesID.profit * (int)float.Parse(dataReader.GetValue(2).ToString())));
                    }
                }

                if (salesPersonTable.Contains(salesIdSalesPerson.webUserAccountNo))
                    salesPersonTable[salesIdSalesPerson.webUserAccountNo] = salesIdSalesPerson;
                else
                    salesPersonTable.Add(salesIdSalesPerson.webUserAccountNo, salesIdSalesPerson);
            }

            dataReader.Close();


            return salesPersonTable;

        }
        

        public void inviteSalesPerson(string name, string email, string phoneNo, string languageCode)
        {
            string templateCode = "INVITE";
            string fromAddress = "";
            string subject = "";

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [From Address], [Subject] FROM [" + infojetContext.systemDatabase.getTableName("Web E-Mail Template") + "] WHERE [Code] = @webMailTemplateCode");
            databaseQuery.addStringParameter("@webMailTemplateCode", templateCode, 20);
            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                fromAddress = dataReader.GetValue(0).ToString();
                subject = dataReader.GetValue(1).ToString();
            }
            dataReader.Close();


            int entryNo = 0;

            databaseQuery = infojetContext.systemDatabase.prepare("SELECT TOP 1 [Entry No_] FROM [" + infojetContext.systemDatabase.getTableName("Web Notification Log Entry") + "] ORDER BY [Entry No_] DESC");
            dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                entryNo = int.Parse(dataReader.GetValue(0).ToString());
            }
            dataReader.Close();

            entryNo++;

            databaseQuery = infojetContext.systemDatabase.prepare("INSERT INTO [" + infojetContext.systemDatabase.getTableName("Web Notification Log Entry") + "] ([Entry No_], [Type], [To Address], [From Address], [Subject], [Attachment Type], [Report ID], [Attachment No_], [Queued Date Time], [Sent Date Time], [Status], [Error Message]) VALUES (@entryNo, 0, @toAddress, @fromAddress, @subject, 0, 0, '', GETDATE(), '1753-01-01 00:00:00', 0, '')");
            databaseQuery.addIntParameter("@entryNo", entryNo);
            databaseQuery.addStringParameter("@toAddress", email, 100);
            databaseQuery.addStringParameter("@fromAddress", fromAddress, 100);
            databaseQuery.addStringParameter("@subject", subject, 50);
            databaseQuery.execute();


            databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Text] FROM [" + infojetContext.systemDatabase.getTableName("Web E-Mail Template Line") + "] WHERE [Web Mail Template Code] = @webMailTemplateCode AND [Language Code] = @languageCode ORDER BY [Line No_]");
            databaseQuery.addStringParameter("@webMailTemplateCode", templateCode, 20);
            databaseQuery.addStringParameter("@languageCode", languageCode, 20);

            SqlDataAdapter dataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);


            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                string text = dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();
                text = text.Replace("$[SALESID]", this.code);
                text = text.Replace("$[DESCRIPTION]", this.description);
                text = text.Replace("$[CONTROLNO]", this.controlNo);
                text = text.Replace("$[NAME]", name);
                text = text.Replace("$[EMAIL]", email);
                text = text.Replace("$[PHONENO]", phoneNo);

                databaseQuery = infojetContext.systemDatabase.prepare("INSERT INTO [" + infojetContext.systemDatabase.getTableName("Web Notification Log Line") + "] ([Queue Entry No_], [Line No_], [Text]) VALUES (@queueEntryNo, @lineNo, @text)");
                databaseQuery.addIntParameter("@queueEntryNo", entryNo);
                databaseQuery.addIntParameter("@lineNo", i*10000);
                databaseQuery.addStringParameter("@text", text, 250);
                databaseQuery.execute();
                

                i++;
            }

            databaseQuery = infojetContext.systemDatabase.prepare("UPDATE [" + infojetContext.systemDatabase.getTableName("Web Notification Log Entry") + "] SET [Status] = 1 WHERE [Entry No_] = @entryNo");
            databaseQuery.addIntParameter("@entryNo", entryNo);
            databaseQuery.execute();


        }


        public static SalesID getSalesIdFromControlNo(Navipro.Infojet.Lib.Infojet infojetContext, string controlNo)
        {
            string code = "";
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [FörsäljningsID] FROM [" + infojetContext.systemDatabase.getTableName("FörsäljningsID") + "] WHERE [Control No_] = @controlNo");
            databaseQuery.addStringParameter("@controlNo", controlNo, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                code = dataReader.GetValue(0).ToString();
                dataReader.Close();

                return new SalesID(infojetContext, code);
                
            }
            dataReader.Close();


            return null;

        }


        public static bool checkShipmentMethod(Navipro.Infojet.Lib.Infojet infojetContext, WebShipmentMethod webShipmentMethod, string postCode)
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Daytime Delivery], [Nighttime Delivery] FROM [" + infojetContext.systemDatabase.getTableName("Web Shipment Method") + "] WHERE [Web Site Code] = @webSiteCode AND [Code] = @webShipmentMethodCode");
            databaseQuery.addStringParameter("@webSiteCode", webShipmentMethod.webSiteCode, 20);
            databaseQuery.addStringParameter("@webShipmentMethodCode", webShipmentMethod.code, 20);

            SqlDataAdapter dataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            int i = 0;
            bool dayTime = false;
            bool nightTime = false;

            while (i < dataSet.Tables[0].Rows.Count)
            {
                if (dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() == "1") dayTime = true;
                if (dataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() == "1") nightTime = true;

                i++;
            }

            if ((dayTime == false) && (nightTime == false)) return true;

            databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Day], [Night] FROM [" + infojetContext.systemDatabase.getTableName("Web ShipAgent Postcode Rel") + "] WHERE [AgentId] = @shippingAgentCode AND [PostCode] = @postCode");
            databaseQuery.addStringParameter("@shippingAgentCode", webShipmentMethod.shippingAgentCode, 20);
            databaseQuery.addStringParameter("@postCode", postCode, 20);

            dataAdapter = databaseQuery.executeDataAdapterQuery();
            dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            bool success = false;

            i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                if ((dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() == "1") && (dayTime)) success = true;
                if ((dataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() == "1") && (nightTime)) success = true;

                i++;
            }

            //throw new Exception("Metod: " + webShipmentMethod.code + ", Agent: "+webShipmentMethod.shippingAgentCode+", Postcode: "+postCode+", Dagtid: " + dayTime + ", Kvällstid: " + nightTime+", Success: "+success);


            return success;
        }

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

        public static DataSet getCartLines(Navipro.Infojet.Lib.Infojet infojetContext, string[] salesIdArray, string webSiteCode)
        {
            DataSet dataSet = new DataSet(); 
            string salesIdText = "";

            foreach (string salesIdCode in salesIdArray)
            {
                if (salesIdText != "") salesIdText = salesIdText + " OR ";
                salesIdText = salesIdText + "[Extra 2] = '" + salesIdCode + "'";
            }
            if (salesIdText == "") return dataSet;

            salesIdText = "(" + salesIdText + ")";

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT l.[Session ID], l.[Entry No_], l.[Item No_], l.[Unit Of Measure Code], l.[Unit Price], l.[Quantity], l.[Amount], l.[Extra 1], l.[Extra 2], l.[Extra 3], l.[Extra 4], l.[Extra 5], l.[Web User Account No], l.[Reference No_], l.[From Date], l.[To Date], l.[Web Site Code], i.[Description], t.[Description] FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] l, [" + infojetContext.systemDatabase.getTableName("Item") + "] i LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Item Translation") + "] t ON t.[Item No_] = i.[No_] AND t.[Language Code] = @languageCode WHERE l.[Web Site Code] = @webSiteCode AND l.[Item No_] = i.[No_] AND "+salesIdText+" ORDER BY l.[Entry No_]");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("languageCode", infojetContext.languageCode, 20);


            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            sqlDataAdapter.Fill(dataSet);

            return dataSet;

        }

        public void updateSalesPerson()
        {
            salesPerson = new SalesPerson(infojetContext, this._nbSalesPersonCode);
        }

        public DocumentCollection getDocuments()
        {
            DocumentCollection docCollection = new DocumentCollection();

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [No_], [Order Date] FROM [" + infojetContext.systemDatabase.getTableName("Sales Header") + "] WHERE [FörsäljningsID] = @salesId");
            databaseQuery.addStringParameter("salesId", code, 20);
            SqlDataReader dataReader = databaseQuery.executeQuery();

            while (dataReader.Read())
            {
                Document document = new Document();
                document.documentType = 0;
                document.documentNo = dataReader.GetValue(0).ToString();
                document.orderNo = dataReader.GetValue(0).ToString();
                document.orderDate = dataReader.GetDateTime(1);

                docCollection.Add(document);
            }
            dataReader.Close();

            databaseQuery = infojetContext.systemDatabase.prepare("SELECT [No_], [Order Date], [Order No_], [Posting Date] FROM [" + infojetContext.systemDatabase.getTableName("Sales Invoice Header") + "] WHERE [FörsäljningsID] = @salesId");
            databaseQuery.addStringParameter("salesId", code, 20);
            dataReader = databaseQuery.executeQuery();

            while (dataReader.Read())
            {
                Document document = new Document();
                document.documentType = 1;
                document.documentNo = dataReader.GetValue(0).ToString();
                document.orderNo = dataReader.GetValue(2).ToString();
                document.orderDate = dataReader.GetDateTime(3);
                if (document.orderNo == null) document.orderNo = "";

                docCollection.Add(document);
            }
            dataReader.Close();

            databaseQuery = infojetContext.systemDatabase.prepare("SELECT [No_], [Posting Date] FROM [" + infojetContext.systemDatabase.getTableName("Sales Cr_Memo Header") + "] WHERE [FörsäljningsID] = @salesId");
            databaseQuery.addStringParameter("salesId", code, 20);
            dataReader = databaseQuery.executeQuery();

            while (dataReader.Read())
            {
                Document document = new Document();
                document.documentType = 2;
                document.documentNo = dataReader.GetValue(0).ToString();
                document.orderDate = dataReader.GetDateTime(1);

                docCollection.Add(document);
            }
            dataReader.Close();

            databaseQuery = infojetContext.systemDatabase.prepare("SELECT [No_], [Order Date], [Order No_], [Shipping Agent Code], [Package Tracking No_] FROM [" + infojetContext.systemDatabase.getTableName("Sales Shipment Header") + "] WHERE [FörsäljningsID] = @salesId");
            databaseQuery.addStringParameter("salesId", code, 20);
            dataReader = databaseQuery.executeQuery();

            int i = 0;

            Hashtable shipmentTable = new Hashtable();
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(2))
                {
                    if (!shipmentTable.Contains(dataReader.GetValue(2).ToString()))
                    {
                        Document shipment = new Document();
                        shipment.documentNo = dataReader.GetValue(0).ToString();
                        shipment.shippingAgentCode = dataReader.GetValue(3).ToString();
                        shipment.packageTrackingNo = dataReader.GetValue(4).ToString();
                        shipmentTable.Add(dataReader.GetValue(2).ToString(), shipment);
                    }
                }
            }
            dataReader.Close();

            string whereStr = "";

           
            i = 0;
            while (i < docCollection.Count)
            {
                if (whereStr != "") whereStr = whereStr + " OR ";
                whereStr = whereStr + "[Document No_] = '" + docCollection[i].documentNo + "'";
                whereStr = whereStr + "OR [Document No_] = '" + docCollection[i].orderNo + "'";
                whereStr = whereStr + "OR [Document No_] = '" + docCollection[i].shipmentNo + "'";

                i++;
            }

            if (whereStr != "")
            {
                Hashtable documentTable = new Hashtable();

                databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Document Type], [Document No_] FROM [" + infojetContext.systemDatabase.getTableName("Web Requested Document") + "] WHERE (" + whereStr+") AND [Document] IS NOT NULL");
                dataReader = databaseQuery.executeQuery();

                while (dataReader.Read())
                {
                    documentTable.Add(dataReader.GetValue(0).ToString() + ";" + dataReader.GetValue(1).ToString(), "1");
                }
                dataReader.Close();


                i = 0;
                while (i < docCollection.Count)
                {                   
                    if (shipmentTable.Contains(docCollection[i].orderNo))
                    {
                        Document shipment = (Document)shipmentTable[docCollection[i].orderNo];
                        docCollection[i].shipmentNo = shipment.documentNo;
                        docCollection[i].shippingAgentCode = shipment.shippingAgentCode;
                        docCollection[i].packageTrackingNo = shipment.packageTrackingNo;

                        ShippingAgent shippingAgent = new ShippingAgent(infojetContext.systemDatabase, shipment.shippingAgentCode);
                        if (shippingAgent.internetAddress != null)
                        {
                            docCollection[i].trackingUrl = shippingAgent.internetAddress.Replace("%1", docCollection[i].packageTrackingNo);
                        }
                    }
                    
                    if (docCollection[i].documentType == 1)
                    {
                        //if ((string)documentTable["2;" + docCollection[i].documentNo] == "1") docCollection[i].invoicePdfExists = true;
                        docCollection[i].invoicePdfExists = true;
                    }
                    if (docCollection[i].documentType == 2)
                    {
                        //if ((string)documentTable["3;" + docCollection[i].documentNo] == "1") docCollection[i].invoicePdfExists = true;
                        docCollection[i].invoicePdfExists = true;
                    }
                    if ((string)documentTable["0;" + docCollection[i].orderNo] == "1") docCollection[i].shipmentPdfExists = true;
                    if ((string)documentTable["4;" + docCollection[i].orderNo] == "1") docCollection[i].packingSlipPdfExists = true;

                    
                    i++;
                }

            }
            return docCollection;
        }
    }
}
