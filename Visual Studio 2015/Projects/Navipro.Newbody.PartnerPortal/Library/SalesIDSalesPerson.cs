using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Navipro.Infojet.Lib;
using System.Xml;

namespace Navipro.Newbody.PartnerPortal.Library
{
    /// <summary>
    /// Klassen motsvarar en säljare som är medlem i ett försäljnings-ID.
    /// </summary>
    public class SalesIDSalesPerson : ServiceArgument
    {
        private string _salesId;
        private string _webUserAccountNo;
        private bool _paymentReceived;
        private WebUserAccount _webUserAccount;

        private int _soldPackages;
        private int _soldPackagesInclGiftCards;
        private float _soldAmount;

        private int _historyPackages;
        private int _historyPackagesInclGiftCards;
        private float _historyAmount;

        private int _status;


        private Database database;
        private Navipro.Infojet.Lib.Infojet infojet;

        public SalesIDSalesPerson()
        {
        }

        /// <summary>
        /// Konstruktor som initierar en säljare utifrån försäljnings-ID samt användarkonto.
        /// </summary>
        /// <param name="database">Referens till databas-objektet i Infojet-klassen.</param>
        /// <param name="salesId">Försäljnings-ID</param>
        /// <param name="webUserAccountNo">Användarkontonr</param>
        public SalesIDSalesPerson(Database database, string salesId, string webUserAccountNo)
        {
            this.database = database;

            this._salesId = salesId;
            this._webUserAccountNo = webUserAccountNo;
            this._soldPackages = 0;
            this._soldAmount = 0;
            this._historyPackages = 0;
        }

        public SalesIDSalesPerson(Database database, string salesId, WebUserAccount webUserAccount)
        {
            this.database = database;

            this._salesId = salesId;
            this._webUserAccountNo = webUserAccount.no;
            this._soldPackages = 0;
            this._soldAmount = 0;
            this._historyPackages = 0;
            this._webUserAccount = webUserAccount;
        }

        /// <summary>
        /// Konstruktor som initierar en säljare utifrån en rad i ett dataset.
        /// </summary>
        /// <param name="database">Referens till databas-objektet i Infojet-klassen.</param>
        /// <param name="dataRow">En rad i ett dataset.</param>
        public SalesIDSalesPerson(Database database, DataRow dataRow)
        {
            this.database = database;

            this._salesId = dataRow.ItemArray.GetValue(0).ToString();
            this._webUserAccountNo = dataRow.ItemArray.GetValue(1).ToString();
            this._soldPackages = 0;
            this._soldAmount = 0;
            this._historyPackages = 0;
        }

        /// <summary>
        /// Konstruktor som initierar en säljare utifrån en öppen datareader.
        /// </summary>
        /// <param name="database">Referens till databas-objektet i Infojet-klassen.</param>
        /// <param name="dataReader">En öppen datareader.</param>
        public SalesIDSalesPerson(Database database, SqlDataReader dataReader)
        {
            this.database = database;

            readData(dataReader);
        }

        private void readData(SqlDataReader dataReader)
        {
            this._salesId = dataReader.GetValue(0).ToString();
            this._webUserAccountNo = dataReader.GetValue(1).ToString();
        }

        private int getSoldPackages()
        {
            if (this._soldPackages == 0)
            {
                DatabaseQuery databaseQuery2 = database.prepare("SELECT SUM(Quantity) FROM [" + database.getTableName("Web Cart Line") + "] WHERE [Extra 2] = @salesId AND [Web User Account No] = @webUserAccountNo");
                databaseQuery2.addStringParameter("@salesId", this._salesId, 20);
                databaseQuery2.addStringParameter("@webUserAccountNo", this._webUserAccountNo, 20);

                SqlDataReader dataReader = databaseQuery2.executeQuery();

                if (dataReader.Read())
                {
                    if (!dataReader.IsDBNull(0)) _soldPackages = (int)float.Parse(dataReader.GetValue(0).ToString());
                }

                dataReader.Close();


            }

            return this._soldPackages;


        }


        private int getHistoryPackages()
        {
            if (this._historyPackages == 0)
            {
                DatabaseQuery databaseQuery2 = database.prepare("SELECT SUM([Ordered Qty]) FROM [" + database.getTableName("Web Order Line") + "] WHERE [FörsäljningsID] = @salesId AND [Web User Account No_] = @webUserAccountNo AND [Förpackningsmaterial] = 0");
                databaseQuery2.addStringParameter("@salesId", this._salesId, 20);
                databaseQuery2.addStringParameter("@webUserAccountNo", this._webUserAccountNo, 20);

                SqlDataReader dataReader = databaseQuery2.executeQuery();

                if (dataReader.Read())
                {
                    if (!dataReader.IsDBNull(0)) _historyPackages = (int)float.Parse(dataReader.GetValue(0).ToString());
                }

                dataReader.Close();


                databaseQuery2 = database.prepare("SELECT SUM([Quantity]) FROM [" + database.getTableName("Web Cart Line") + "] WITH (NOLOCK) WHERE [Extra 2] = @salesId AND [Web User Account No] = @webUserAccountNo");
                databaseQuery2.addStringParameter("@salesId", this._salesId, 20);
                databaseQuery2.addStringParameter("@webUserAccountNo", this._webUserAccountNo, 20);

                dataReader = databaseQuery2.executeQuery();
                if (dataReader.Read())
                {
                    if (!dataReader.IsDBNull(0)) _historyPackages = _historyPackages + (int)float.Parse(dataReader.GetValue(0).ToString());
                }
                dataReader.Close();


            }

            return this._historyPackages;


        }

        /// <summary>
        /// Kontrollerar samtliga orderrader och returnerar en status 0, 1 eller 2 beroende på om raderna är klarmarkerade eller inte.
        /// </summary>
        /// <returns>0: Försäljning pågår, 1: Försäljning ej påbörjad, 2: Klarmarkerad</returns>
        public int getStatus()
        {
            DatabaseQuery databaseQuery = database.prepare("SELECT COUNT(*) FROM [" + database.getTableName("Web Cart Line") + "] WHERE [Extra 2] = @salesId AND [Web User Account No] = @webUserAccountNo");
            databaseQuery.addStringParameter("@salesId", this._salesId, 20);
            databaseQuery.addStringParameter("@webUserAccountNo", this._webUserAccountNo, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();

            int count = 0;
            if (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0)) count = int.Parse(dataReader.GetValue(0).ToString());
            }

            dataReader.Close();

            DatabaseQuery databaseQuery2 = database.prepare("SELECT COUNT(*) FROM [" + database.getTableName("Web Cart Line") + "] WHERE [Extra 2] = @salesId AND [Web User Account No] = @webUserAccountNo AND [Extra 3] = '1'");
            databaseQuery2.addStringParameter("@salesId", this._salesId, 20);
            databaseQuery2.addStringParameter("@webUserAccountNo", this._webUserAccountNo, 20);

            dataReader = databaseQuery2.executeQuery();

            int countReleased = 0;
            if (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0)) countReleased = int.Parse(dataReader.GetValue(0).ToString());
            }

            dataReader.Close();


            if (count == 0) return 1;
            if (countReleased == count) return 2;
            return 0;

        }

        public int getStatus(Hashtable totalCartLines, Hashtable releasedCartLines)
        {
            int count = 0;
            int countReleased = 0;
            if (totalCartLines.Contains(this._webUserAccountNo)) count = (int)totalCartLines[webUserAccountNo];
            if (releasedCartLines.Contains(this._webUserAccountNo)) countReleased = (int)releasedCartLines[webUserAccountNo];


            if (count == 0) return 1;
            if (countReleased == count) return 2;
            return 0;

        }


        /// <summary>
        /// Sätter en referens till Infojet-objektet för att nyttja översättningsfunktionerna i Infojet.
        /// </summary>
        /// <param name="infojet">Referens till Infojet-klassen.</param>
        public void setTranslationHelper(Navipro.Infojet.Lib.Infojet infojet)
        {
            this.infojet = infojet;
        }

        /// <summary>
        /// Returnerar en översatt statustext motsvarande resultatet från getStatus.
        /// </summary>
        /// <returns>En översatt textsträng.</returns>
        public string getStatusText()
        {
            int status = getStatus();
            string statusText = "STARTED";
            if (status == 2) statusText = "RELEASED";
            if (status == 1) statusText = "NOT STARTED";

            if (infojet != null) return infojet.translate(statusText);
            return statusText;

        }


 
        public void releaseCart()
        {
            DatabaseQuery databaseQuery = database.prepare("UPDATE [" + database.getTableName("Web Cart Line") + "] SET [Extra 3] = '1' WHERE [Extra 2] = @salesId AND [Web User Account No] = @webUserAccountNo");
            databaseQuery.addStringParameter("@salesId", this._salesId, 20);
            databaseQuery.addStringParameter("@webUserAccountNo", this._webUserAccountNo, 20);
            databaseQuery.execute();
        }

        public void undoReleaseCart()
        {
            DatabaseQuery databaseQuery = database.prepare("UPDATE [" + database.getTableName("Web Cart Line") + "] SET [Extra 3] = '0' WHERE [Extra 2] = @salesId AND [Web User Account No] = @webUserAccountNo");
            databaseQuery.addStringParameter("@salesId", this._salesId, 20);
            databaseQuery.addStringParameter("@webUserAccountNo", this._webUserAccountNo, 20);
            databaseQuery.execute();
        }


        /// <summary>
        /// Försäljnings-ID.
        /// </summary>
        public string salesId { set { _salesId = value; } get { return _salesId; } }

        /// <summary>
        /// Användarkontonr på säljaren.
        /// </summary>
        public string webUserAccountNo { set { _webUserAccountNo = value; } get { return _webUserAccountNo; } }

        /// <summary>
        /// Användarkonto.
        /// </summary>
        public WebUserAccount webUserAccount { set { _webUserAccount = value; } get { return _webUserAccount; } }

        /// <summary>
        /// Sålda paket.
        /// </summary>
        public int soldPackages { set { _soldPackages = value; } get { return _soldPackages; } }

        /// <summary>
        /// Sålda paket.
        /// </summary>
        public int soldPackagesInclGiftCards { set { _soldPackagesInclGiftCards = value; } get { return _soldPackagesInclGiftCards; } }

        /// <summary>
        /// Sålt belopp.
        /// </summary>
        public float soldAmount { set { _soldAmount = value; } get { return _soldAmount; } }

        /// <summary>
        /// Sålda paket.
        /// </summary>
        public int historyPackages { set { _historyPackages = value; } get { return _historyPackages; } }

        /// <summary>
        /// Sålda paket.
        /// </summary>
        public int historyPackagesInclGiftCards { set { _historyPackagesInclGiftCards = value; } get { return _historyPackagesInclGiftCards; } }

        /// <summary>
        /// Sålt belopp.
        /// </summary>
        public float historyAmount { set { _historyAmount = value; } get { return _historyAmount; } }



        /// <summary>
        /// Status.
        /// </summary>
        public int status { set { _status = value; } get { return _status; } }

        /// <summary>
        /// Översatt textsträng motsvarande aktuell status.
        /// </summary>
        public string released { get { return getStatusText(); } }

        /// <summary>
        /// Betalning överlämnad till KP.
        /// </summary>
        public bool paymentReceived { get { return _paymentReceived; } set { _paymentReceived = value; } }

        /// <summary>
        /// Antal sålda paket för aktuell säljare.
        /// </summary>
        //public int soldPackages { get { return getSoldPackages(); } }

        /// <summary>
        /// Antal fakturerade paket för aktuell säljare.
        /// </summary>
        //public int historyPackages { get { return _historyPackages; } set { _historyPackages = value; } }

        /// <summary>
        /// Totalt antal paket (sålda + fakturerade) för aktuell säljare.
        /// </summary>
        //public int totalPackages { get { return getHistoryPackages() + getSoldPackages(); } }

        public static DataSet getSalesPersonsDataSet(Navipro.Infojet.Lib.Infojet infojetContext, string salesId)
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [FÖrsäljningsID], [Web User Account] FROM [" + infojetContext.systemDatabase.getTableName("SalesID WebUserAccount") + "] WHERE [FÖrsäljningsID] = @code");

            databaseQuery.addStringParameter("@code", salesId, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            return dataSet;

        }

        public static int countSalesPersons(Navipro.Infojet.Lib.Infojet infojetContext, string salesId)
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [FÖrsäljningsID], [Web User Account] FROM [" + infojetContext.systemDatabase.getTableName("SalesID WebUserAccount") + "] WHERE [FÖrsäljningsID] = @code");
            databaseQuery.addStringParameter("@code", salesId, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            return dataSet.Tables[0].Rows.Count;

        }

        public static SalesIDSalesPersonCollection getSalesPersons(Navipro.Infojet.Lib.Infojet infojetContext, string salesId)
        {
            Hashtable salesPersonTable = SalesID.getSoldPackages(infojetContext, salesId);
            Hashtable salesPersonHistoryTable = SalesID.getHistorySoldPackages(infojetContext, salesId);

            Hashtable totalCartLines = new Hashtable();
            Hashtable releasedCartLines = new Hashtable();
            getWebUserAccountCarts(infojetContext, salesId, out totalCartLines, out releasedCartLines);

            SalesIDSalesPersonCollection salesIdSalesPersonCollection = new SalesIDSalesPersonCollection();

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [No_], [User-ID], [Password], [Closed], [Contact No_], [Customer No_], [Company Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Bill-to Company Name], [Bill-to Address], [Bill-to Address 2], [Bill-to Post Code], [Bill-to City], [Bill-to Country Code], [Registration No_], [E-Mail], [Phone No_], [Last Logged On Date], [Last Logged On Time], [History Date], [History Time], [Max Buy Type], [Max Buy Limit _ Order], [Company Role], [Cell Phone No_], w.[Name], [Language Code], [Allow Ordering], sw.[Payment Received] FROM [" + infojetContext.systemDatabase.getTableName("Web User Account") + "] w, [" + infojetContext.systemDatabase.getTableName("SalesID WebUserAccount") + "] sw WHERE w.[No_] = sw.[Web User Account] AND w.[Type] = 1 AND sw.[FÖrsäljningsID] = @code");
            databaseQuery.addStringParameter("@code", salesId, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);
            

            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                WebUserAccount webUserAccount = new WebUserAccount(infojetContext.systemDatabase, dataSet.Tables[0].Rows[i]); 
                SalesIDSalesPerson salesIdSalesPerson = new SalesIDSalesPerson(infojetContext.systemDatabase, salesId, webUserAccount);

                if (salesPersonTable.Contains(webUserAccount.no))
                {
                    salesIdSalesPerson.soldPackages = ((SalesIDSalesPerson)salesPersonTable[webUserAccount.no]).soldPackages;
                    salesIdSalesPerson.soldPackagesInclGiftCards = ((SalesIDSalesPerson)salesPersonTable[webUserAccount.no]).soldPackagesInclGiftCards;
                    salesIdSalesPerson.soldAmount = ((SalesIDSalesPerson)salesPersonTable[webUserAccount.no]).soldAmount;
                }

                if (salesPersonHistoryTable.Contains(webUserAccount.no))
                {
                    salesIdSalesPerson.historyPackages = ((SalesIDSalesPerson)salesPersonHistoryTable[webUserAccount.no]).historyPackages;
                    salesIdSalesPerson.historyPackagesInclGiftCards = ((SalesIDSalesPerson)salesPersonHistoryTable[webUserAccount.no]).historyPackagesInclGiftCards;
                    salesIdSalesPerson.historyAmount = ((SalesIDSalesPerson)salesPersonHistoryTable[webUserAccount.no]).historyAmount;
                }

                salesIdSalesPerson.status = salesIdSalesPerson.getStatus(totalCartLines, releasedCartLines);
                if (dataSet.Tables[0].Rows[i].ItemArray.GetValue(32).ToString() == "1") salesIdSalesPerson.paymentReceived = true;

                salesIdSalesPerson.setTranslationHelper(infojetContext);
                //if (this.isPrimaryContactPerson(salesIdSalesPerson.webUserAccountNo)) salesIdSalesPerson.name = salesIdSalesPerson.name + " (" + infojetContext.translate("CONTACT PERSON") + ")";
                //if (this.isSubContactPerson(salesIdSalesPerson.webUserAccountNo)) salesIdSalesPerson.name = salesIdSalesPerson.name + " (" + infojetContext.translate("SUB CONTACT PERSON") + ")";
                salesIdSalesPersonCollection.Add(salesIdSalesPerson);

                i++;
            }

            return salesIdSalesPersonCollection;

        }

        public static void getWebUserAccountCarts(Navipro.Infojet.Lib.Infojet infojetContext, string salesId, out Hashtable totalCartLines, out Hashtable releasedCartLines)
        {
            totalCartLines = new Hashtable();
            releasedCartLines = new Hashtable();

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web User Account No], COUNT(*) FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] WHERE [Extra 2] = @salesId GROUP BY [Web User Account No]");
            databaseQuery.addStringParameter("@salesId", salesId, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();

            
            while(dataReader.Read())
            {
                int count = 0;
                if (!dataReader.IsDBNull(1)) count = int.Parse(dataReader.GetValue(1).ToString());
                totalCartLines.Add(dataReader.GetValue(0).ToString(), count);
            }

            dataReader.Close();

            DatabaseQuery databaseQuery2 = infojetContext.systemDatabase.prepare("SELECT [Web User Account No], COUNT(*) FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] WHERE [Extra 2] = @salesId AND [Extra 3] = '1' GROUP BY [Web User Account No]");
            databaseQuery2.addStringParameter("@salesId", salesId, 20);


            dataReader = databaseQuery2.executeQuery();

            while(dataReader.Read())
            {
                int count = 0;
                if (!dataReader.IsDBNull(1)) count = int.Parse(dataReader.GetValue(1).ToString());
                releasedCartLines.Add(dataReader.GetValue(0).ToString(), count);
            }

            dataReader.Close();

        }

        public void updateSoldPackages(Navipro.Infojet.Lib.Infojet infojetContext)
        {
            SalesID currentSalesID = new SalesID(infojetContext, salesId);
            Customer customer = new Customer(infojetContext, currentSalesID.customerNo);
            float vatFactor = customer.getVatFactor("25");

            DatabaseQuery databaseQuery2 = database.prepare("SELECT SUM(Quantity), SUM([Quantity] * [Unit Price]) FROM [" + database.getTableName("Web Cart Line") + "] WHERE [Extra 2] = @salesId AND [Web User Account No] = @webUserAccountNo");
            databaseQuery2.addStringParameter("@salesId", salesId, 20);
            databaseQuery2.addStringParameter("@webUserAccountNo", webUserAccountNo, 20);

            SqlDataReader dataReader = databaseQuery2.executeQuery();

            if (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    _soldPackages = (int)float.Parse(dataReader.GetValue(0).ToString());
                }
                if (!dataReader.IsDBNull(1))
                {
                    _soldAmount = float.Parse(dataReader.GetValue(1).ToString());
                    _soldAmount = _soldAmount + (currentSalesID.profit * _soldPackages);

                }
            }

            dataReader.Close();


        }



        public void setPaymentReceived(bool paymentReceived)
        {
            DatabaseQuery databaseQuery = database.prepare("UPDATE [" + database.getTableName("SalesID WebUserAccount") + "] SET [Payment Received] = @paymentReceived WHERE [FÖrsäljningsID] = @salesId AND [Web User Account] = @webUserAccountNo");
            databaseQuery.addStringParameter("@salesId", _salesId, 20);
            databaseQuery.addStringParameter("@webUserAccountNo", _webUserAccountNo, 20);
            if (!paymentReceived) databaseQuery.addSmallIntParameter("@paymentReceived", 0);
            if (paymentReceived) databaseQuery.addSmallIntParameter("@paymentReceived", 1);

            databaseQuery.execute();
        }

        public static void clearAllPaymentReceived(Navipro.Infojet.Lib.Infojet infojetContext, string salesId)
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("UPDATE [" + infojetContext.systemDatabase.getTableName("SalesID WebUserAccount") + "] SET [Payment Received] = 0 WHERE [FÖrsäljningsID] = @salesId");
            databaseQuery.addStringParameter("@salesId", salesId, 20);

            databaseQuery.execute();
        }

        

        #region ServiceArgument Members

        public XmlElement toDOM(XmlDocument xmlDoc)
        {
            XmlElement containerElement = xmlDoc.CreateElement("salesPerson");

            XmlElement fieldElement = null;

            fieldElement = xmlDoc.CreateElement("salesId");
            fieldElement.AppendChild(xmlDoc.CreateTextNode(this.salesId));
            containerElement.AppendChild(fieldElement);

            fieldElement = xmlDoc.CreateElement("webUserAccountNo");
            fieldElement.AppendChild(xmlDoc.CreateTextNode(this.webUserAccountNo));
            containerElement.AppendChild(fieldElement);

            if (this.webUserAccount != null)
            {
                fieldElement = xmlDoc.CreateElement("name");
                fieldElement.AppendChild(xmlDoc.CreateTextNode(this.webUserAccount.name));
                containerElement.AppendChild(fieldElement);

                fieldElement = xmlDoc.CreateElement("userId");
                fieldElement.AppendChild(xmlDoc.CreateTextNode(this.webUserAccount.userId));
                containerElement.AppendChild(fieldElement);

                fieldElement = xmlDoc.CreateElement("email");
                fieldElement.AppendChild(xmlDoc.CreateTextNode(this.webUserAccount.email));
                containerElement.AppendChild(fieldElement);

                fieldElement = xmlDoc.CreateElement("phoneNo");
                fieldElement.AppendChild(xmlDoc.CreateTextNode(this.webUserAccount.phoneNo));
                containerElement.AppendChild(fieldElement);

            }

            return containerElement;
           
        }

        #endregion
    }
}
