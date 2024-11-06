using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Navipro.Infojet.Lib;
using System.Xml;

namespace Navipro.Newbody.Woppen.Library
{
    /// <summary>
    /// Klassen motsvarar en säljare som är medlem i ett försäljnings-ID.
    /// </summary>
    public class SalesIDSalesPerson
    {
        private string _salesId;
        private string _webUserAccountNo;
        private string _name;

        private int _soldPackages;
        private int _soldItems;
        private int _historyPackages;
        private int _historyItems;
        private int _soldGiftCards;
        private int _historyGiftCards;

        private string _pageUrl;

        private Database database;
        private Navipro.Infojet.Lib.Infojet infojet;

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
            this._historyPackages = 0;
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
                //if (infojet == null) infojet = new Navipro.Infojet.Lib.Infojet();
                Hashtable giftCardTable = SalesID.getGiftCardItems(infojet);

                DatabaseQuery databaseQuery2 = database.prepare("SELECT [Item No_], SUM(Quantity) FROM [" + database.getTableName("Web Cart Line") + "] WHERE [Extra 2] = @salesId AND [Web User Account No] = @webUserAccountNo GROUP BY [Item No_]");
                databaseQuery2.addStringParameter("@salesId", this._salesId, 20);
                databaseQuery2.addStringParameter("@webUserAccountNo", this._webUserAccountNo, 20);

                SqlDataReader dataReader = databaseQuery2.executeQuery();

                while (dataReader.Read())
                {
                    _soldItems = _soldItems + (int)float.Parse(dataReader.GetValue(1).ToString());
                    if (giftCardTable.Contains(dataReader.GetValue(0).ToString()))
                    {
                        if (!dataReader.IsDBNull(1)) _soldPackages = _soldPackages + ((int)float.Parse(dataReader.GetValue(1).ToString()) * (int)giftCardTable[dataReader.GetValue(0).ToString()]);
                    }
                    else
                    {
                        if (!dataReader.IsDBNull(1)) _soldPackages = _soldPackages + (int)float.Parse(dataReader.GetValue(1).ToString());
                    }
                    //if (!giftCardTable.Contains(dataReader.GetValue(0).ToString()))
                    //{
                    //    if (!dataReader.IsDBNull(1)) _soldPackages = _soldPackages + (int)float.Parse(dataReader.GetValue(1).ToString());
                    //}

                }

                dataReader.Close();


            }

            return this._soldPackages;


        }

        private int getSoldItems()
        {
            if (this._soldItems == 0)
            {
                //if (infojet == null) infojet = new Navipro.Infojet.Lib.Infojet();
                Hashtable giftCardTable = SalesID.getGiftCardItems(infojet);

                DatabaseQuery databaseQuery2 = database.prepare("SELECT [Item No_], SUM(Quantity) FROM [" + database.getTableName("Web Cart Line") + "] WHERE [Extra 2] = @salesId AND [Web User Account No] = @webUserAccountNo GROUP BY [Item No_]");
                databaseQuery2.addStringParameter("@salesId", this._salesId, 20);
                databaseQuery2.addStringParameter("@webUserAccountNo", this._webUserAccountNo, 20);

                SqlDataReader dataReader = databaseQuery2.executeQuery();

                while (dataReader.Read())
                {
                    _soldItems = _soldItems + (int)float.Parse(dataReader.GetValue(1).ToString());
                    if (giftCardTable.Contains(dataReader.GetValue(0).ToString()))
                    {
                        if (!dataReader.IsDBNull(1)) _soldPackages = _soldPackages + ((int)float.Parse(dataReader.GetValue(1).ToString()) * (int)giftCardTable[dataReader.GetValue(0).ToString()]);
                    }
                    else
                    {
                        if (!dataReader.IsDBNull(1)) _soldPackages = _soldPackages + (int)float.Parse(dataReader.GetValue(1).ToString());
                    }
                    //if (!giftCardTable.Contains(dataReader.GetValue(0).ToString()))
                    //{
                    //    if (!dataReader.IsDBNull(1)) _soldPackages = _soldPackages + (int)float.Parse(dataReader.GetValue(1).ToString());
                    //}

                }

                dataReader.Close();


            }

            return this._soldItems;


        }

        private int getSoldGiftCards()
        {
            if (this._soldGiftCards == 0)
            {
                if (infojet == null) infojet = new Navipro.Infojet.Lib.Infojet();

                Hashtable giftCardTable = SalesID.getGiftCardItems(infojet);

                DatabaseQuery databaseQuery2 = database.prepare("SELECT [Item No_], SUM(Quantity) FROM [" + database.getTableName("Web Cart Line") + "] WHERE [Extra 2] = @salesId AND [Web User Account No] = @webUserAccountNo GROUP BY [Item No_]");
                databaseQuery2.addStringParameter("@salesId", this._salesId, 20);
                databaseQuery2.addStringParameter("@webUserAccountNo", this._webUserAccountNo, 20);

                SqlDataReader dataReader = databaseQuery2.executeQuery();

                while (dataReader.Read())
                {
                    if (giftCardTable.Contains(dataReader.GetValue(0).ToString()))
                    {
                        if (!dataReader.IsDBNull(1)) _soldGiftCards = _soldGiftCards + (int)float.Parse(dataReader.GetValue(1).ToString());
                    }

                }

                dataReader.Close();


            }

            return this._soldGiftCards;


        }



        private int getHistoryPackages()
        {
            if (this._historyPackages == 0)
            {
                //if (infojet == null) infojet = new Navipro.Infojet.Lib.Infojet();
                //Hashtable giftCardTable = SalesID.getGiftCardItems(infojet);
                Hashtable giftCardTable = new Hashtable();

                DatabaseQuery databaseQuery2 = database.prepare("SELECT [Item No_], SUM([Ordered Qty]) FROM [" + database.getTableName("Web Order Line") + "] WHERE [FörsäljningsID] = @salesId AND [Web User Account No_] = @webUserAccountNo AND [Förpackningsmaterial] = 0 GROUP BY [Item No_]");
                databaseQuery2.addStringParameter("@salesId", this._salesId, 20);
                databaseQuery2.addStringParameter("@webUserAccountNo", this._webUserAccountNo, 20);

                SqlDataReader dataReader = databaseQuery2.executeQuery();

                while (dataReader.Read())
                {
                    if (giftCardTable.Contains(dataReader.GetValue(0).ToString()))
                    {
                        if (!dataReader.IsDBNull(1)) _historyPackages = _historyPackages + (int.Parse(dataReader.GetValue(1).ToString()) * (int)giftCardTable[dataReader.GetValue(0).ToString()]);
                    }
                    else
                    {
                        if (!dataReader.IsDBNull(1)) _historyPackages = _historyPackages + int.Parse(dataReader.GetValue(1).ToString());
                    }
                    //if (!giftCardTable.Contains(dataReader.GetValue(0).ToString()))
                    //{
                    //    if (!dataReader.IsDBNull(1)) _historyPackages = _historyPackages + int.Parse(dataReader.GetValue(1).ToString());
                    //}

                }

                dataReader.Close();


                databaseQuery2 = database.prepare("SELECT [Item No_], SUM([Quantity]) FROM [" + database.getTableName("Web Cart Line") + "] WITH (NOLOCK) WHERE [Extra 2] = @salesId AND [Web User Account No] = @webUserAccountNo GROUP BY [Item No_]");
                databaseQuery2.addStringParameter("@salesId", this._salesId, 20);
                databaseQuery2.addStringParameter("@webUserAccountNo", this._webUserAccountNo, 20);

                dataReader = databaseQuery2.executeQuery();
                while (dataReader.Read())
                {
                    if (giftCardTable.Contains(dataReader.GetValue(0).ToString()))
                    {
                        if (!dataReader.IsDBNull(1)) _historyPackages = _historyPackages + (int.Parse(dataReader.GetValue(1).ToString()) * (int)giftCardTable[dataReader.GetValue(0).ToString()]);
                    }
                    else
                    {
                        if (!dataReader.IsDBNull(1)) _historyPackages = _historyPackages + int.Parse(dataReader.GetValue(1).ToString());
                    } 
                    //if (!giftCardTable.Contains(dataReader.GetValue(0).ToString()))
                    //{
                    //    if (!dataReader.IsDBNull(1)) _historyPackages = _historyPackages + int.Parse(dataReader.GetValue(1).ToString());
                    //}
                }
                dataReader.Close();


            }

            return this._historyPackages;


        }

        private int getHistoryGiftCards()
        {
            if (this._historyGiftCards == 0)
            {
                if (infojet == null) infojet = new Navipro.Infojet.Lib.Infojet();
                Hashtable giftCardTable = SalesID.getGiftCardItems(infojet);

                DatabaseQuery databaseQuery2 = database.prepare("SELECT [Item No_], SUM([Ordered Qty]) FROM [" + database.getTableName("Web Order Line") + "] WHERE [FörsäljningsID] = @salesId AND [Web User Account No_] = @webUserAccountNo AND [Förpackningsmaterial] = 0 GROUP BY [Item No_]");
                databaseQuery2.addStringParameter("@salesId", this._salesId, 20);
                databaseQuery2.addStringParameter("@webUserAccountNo", this._webUserAccountNo, 20);

                SqlDataReader dataReader = databaseQuery2.executeQuery();

                while (dataReader.Read())
                {
                    if (giftCardTable.Contains(dataReader.GetValue(0).ToString()))
                    {
                        if (!dataReader.IsDBNull(1)) _historyGiftCards = _historyGiftCards + int.Parse(dataReader.GetValue(1).ToString());
                    }

                }

                dataReader.Close();


                databaseQuery2 = database.prepare("SELECT [Item No_], SUM([Quantity]) FROM [" + database.getTableName("Web Cart Line") + "] WITH (NOLOCK) WHERE [Extra 2] = @salesId AND [Web User Account No] = @webUserAccountNo GROUP BY [Item No_]");
                databaseQuery2.addStringParameter("@salesId", this._salesId, 20);
                databaseQuery2.addStringParameter("@webUserAccountNo", this._webUserAccountNo, 20);

                dataReader = databaseQuery2.executeQuery();
                while (dataReader.Read())
                {
                    if (giftCardTable.Contains(dataReader.GetValue(0).ToString()))
                    {
                        if (!dataReader.IsDBNull(1)) _historyGiftCards = _historyGiftCards + int.Parse(dataReader.GetValue(1).ToString());
                    }
                }
                dataReader.Close();


            }

            return this._historyGiftCards;


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


        private string getName()
        {
            if ((_name == null) || (_name == ""))
            {
                _name = _webUserAccountNo;
                WebUserAccount webUserAccount = new WebUserAccount(database, this.webUserAccountNo);
                if (webUserAccount.name != "") _name = webUserAccount.name;

                /*
                DatabaseQuery databaseQuery = database.prepare("SELECT [Name] FROM [" + database.getTableName("SalesID WebUserAccount") + "] WHERE [FÖrsäljningsID] = @salesId AND [Web User Account] = @webUserAccountNo");
                databaseQuery.addStringParameter("@salesId", this._salesId, 20);
                databaseQuery.addStringParameter("@webUserAccountNo", this._webUserAccountNo, 20);

                SqlDataReader dataReader = databaseQuery.executeQuery();
                if (dataReader.Read())
                {
                    name = dataReader.GetValue(0).ToString();
                }
                dataReader.Close();
                */
                
            }
            return _name;
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
        /// Länk till detaljsidan för säljaren, om satt.
        /// </summary>
        public string pageUrl { set { _pageUrl = value; } get { return _pageUrl; } }

        /// <summary>
        /// Säljarens namn.
        /// </summary>
        public string name { get { return getName(); } set { _name = value; } }

        /// <summary>
        /// Översatt textsträng motsvarande aktuell status.
        /// </summary>
        public string released { get { return getStatusText(); } }

        /// <summary>
        /// Antal sålda paket för aktuell säljare.
        /// </summary>
        public int soldPackages { get { return getSoldPackages(); } }
        public int soldItems { get { return getSoldItems(); } }

        /// <summary>
        /// Antal fakturerade paket för aktuell säljare.
        /// </summary>
        public int historyPackages { get { return _historyPackages; } set { _historyPackages = value; } }

        public int historyItems { get { return _historyItems; } set { _historyItems = value; } }

        /// <summary>
        /// Antal sålda presentkort för aktuell säljare.
        /// </summary>
        public int soldGiftCards { get { return getSoldGiftCards(); } }

        /// <summary>
        /// Antal fakturerade presentkort för aktuell säljare.
        /// </summary>
        public int historyGiftCards { get { return _historyGiftCards; } set { _historyGiftCards = value; } }

        /// <summary>
        /// Totalt antal paket (sålda + fakturerade) för aktuell säljare.
        /// </summary>
        public int totalPackages { get { return getHistoryPackages()+getSoldPackages()+getHistoryGiftCards()+getSoldGiftCards(); } }




    }
}
