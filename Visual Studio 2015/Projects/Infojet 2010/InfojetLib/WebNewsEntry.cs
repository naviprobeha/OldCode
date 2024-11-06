using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
    /// <summary>
    /// Summary description for Item.
    /// </summary>
    public class WebNewsEntry
    {
        private Infojet infojetContext;

        private string _no;
        private string _description;
        private DateTime _creationDate;
        private DateTime _publishedFromDate;
        private DateTime _publishedToDate;
        private string _introImageCode;
        private string _mainImageCode;
        private bool _commonHeader;

        private WebImage introImage;
        private WebImage mainImage;

        public WebNewsEntry(Infojet infojetContext, string no)
        {
            //
            // TODO: Add constructor logic here
            //
            this.infojetContext = infojetContext;

            this._no = no;

            getFromDatabase();
        }


        public WebNewsEntry(Infojet infojetContext, DataRow dataRow)
        {
            this.infojetContext = infojetContext;

            this._no = dataRow.ItemArray.GetValue(0).ToString();
            this._description = dataRow.ItemArray.GetValue(1).ToString();
            this._creationDate = DateTime.Parse(dataRow.ItemArray.GetValue(2).ToString());
            this._publishedFromDate = DateTime.Parse(dataRow.ItemArray.GetValue(3).ToString());
            this._publishedToDate = DateTime.Parse(dataRow.ItemArray.GetValue(4).ToString());
            this._introImageCode = dataRow.ItemArray.GetValue(5).ToString();
            this._mainImageCode = dataRow.ItemArray.GetValue(6).ToString();
            _commonHeader = false;
            if (dataRow.ItemArray.GetValue(7).ToString() == "1") _commonHeader = true;
        }

        public WebNewsEntry(Infojet infojetContext, SqlDataReader dataReader)
        {
            this.infojetContext = infojetContext;
            readData(dataReader);
        }

        private void readData(SqlDataReader dataReader)
        {
            _no = dataReader.GetValue(0).ToString();
            _description = dataReader.GetValue(1).ToString();
            _creationDate = dataReader.GetDateTime(2);
            _publishedFromDate = dataReader.GetDateTime(3);
            _publishedToDate = dataReader.GetDateTime(4);
            _introImageCode = dataReader.GetValue(5).ToString();
            _mainImageCode = dataReader.GetValue(6).ToString();
            _commonHeader = false;
            if (dataReader.GetValue(7).ToString() == "1") _commonHeader = true; ;
        }

        private void getFromDatabase()
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [No_], [Description], [Creation Date], [Published From Date], [Published To Date], [Intro Image Code], [Main Image Code], [Common Header] FROM [" + infojetContext.systemDatabase.getTableName("Web News Entry") + "] WHERE [No_] = @no");
            databaseQuery.addStringParameter("no", no, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                readData(dataReader);

            }

            dataReader.Close();


        }

        private string getHeader()
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Header] FROM [" + infojetContext.systemDatabase.getTableName("Web News Header") + "] WHERE [Web News Entry No_] = @webNewsEntryNo AND [Language Code] = @languageCode");
            databaseQuery.addStringParameter("webNewsEntryNo", no, 20);
            databaseQuery.addStringParameter("languageCode", infojetContext.languageCode, 20);

            string header = "";

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {

                header = dataReader.GetValue(0).ToString();

            }

            dataReader.Close();

            return header;
        }

        private string getIntroHeader()
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Introduction Header] FROM [" + infojetContext.systemDatabase.getTableName("Web News Header") + "] WHERE [Web News Entry No_] = @webNewsEntryNo AND [Language Code] = @languageCode");
            databaseQuery.addStringParameter("webNewsEntryNo", no, 20);
            databaseQuery.addStringParameter("languageCode", infojetContext.languageCode, 20);

            string header = "";

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {

                header = dataReader.GetValue(0).ToString();

            }

            dataReader.Close();

            return header;
        }


        private string getIngress()
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Text] FROM [" + infojetContext.systemDatabase.getTableName("Web News Line") + "] WHERE [Web News Entry No_] = @webNewsEntryNo AND [Language Code] = @languageCode AND [Type] = 0 ORDER BY [Line No_]");
            databaseQuery.addStringParameter("webNewsEntryNo", no, 20);
            databaseQuery.addStringParameter("languageCode", infojetContext.languageCode, 20);

            string text = "";

            SqlDataReader dataReader = databaseQuery.executeQuery();
            while (dataReader.Read())
            {

                text = text + " " + dataReader.GetValue(0).ToString();
                if (dataReader.GetValue(0).ToString() == "") text = text + "<br/>";

            }

            dataReader.Close();

            return text;
        }

        private string getBody()
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Text] FROM [" + infojetContext.systemDatabase.getTableName("Web News Line") + "] WHERE [Web News Entry No_] = @webNewsEntryNo AND [Language Code] = @languageCode AND [Type] = 1 ORDER BY [Line No_]");
            databaseQuery.addStringParameter("webNewsEntryNo", no, 20);
            databaseQuery.addStringParameter("languageCode", infojetContext.languageCode, 20);

            string text = "";

            SqlDataReader dataReader = databaseQuery.executeQuery();
            while (dataReader.Read())
            {

                text = text + " " + dataReader.GetValue(0).ToString();
                if (dataReader.GetValue(0).ToString() == "") text = text + "<br/>";


            }

            dataReader.Close();

            return text;
        }

        private string getUrl()
        {
            WebPage webPage = infojetContext.webSite.getWebPageByCategory(infojetContext.webSite.newsPageCategoryCode, infojetContext.userSession);
            if (webPage != null)
            {
                return webPage.getUrl() + "&newsEntryNo=" + no;

            }

            return "";
        }

        public void setIntroImageSize(float width, float height)
        {
            if (this.introImageCode != "")
            {
                if (introImage == null) introImage = new WebImage(infojetContext.systemDatabase, this.introImageCode);
                introImage.setSize(width, height);
            }
        }

        public void setMainImageSize(float width, float height)
        {
            if (this.mainImageCode != "")
            {
                if (mainImage == null) mainImage = new WebImage(infojetContext.systemDatabase, this.mainImageCode);
                mainImage.setSize(width, height);
            }
        }

        public string getMainImageUrl()
        {
            if (this.mainImageCode != "")
            {
                if (mainImage == null) mainImage = new WebImage(infojetContext.systemDatabase, this.mainImageCode);
                return mainImage.getUrl();
            }
            return "";
        }

        public string getIntroImageUrl()
        {
            if (this.introImageCode != "")
            {
                if (introImage == null) introImage = new WebImage(infojetContext.systemDatabase, this.introImageCode);
                return introImage.getUrl();
            }
            return "";
        }

        public string no { get { return _no; } }
        public string description { get { return _description; } }
        public DateTime creationDate { get { return _creationDate; } }
        public DateTime publishedFromDate { get { return _publishedFromDate; } }
        public DateTime publishedToDate { get { return _publishedToDate; } }
        public string introImageCode { get { return _introImageCode; } }
        public string mainImageCode { get { return _mainImageCode; } }
        public bool commonHeader { get { return _commonHeader; } }

        public string header { get { return getHeader(); } }
        public string introHeader { get { return getIntroHeader(); } }
        public string ingress { get { return getIngress(); } }
        public string body { get { return getBody(); } }
        public string link { get { return getUrl(); } }
        public string introImageUrl { get { return getIntroImageUrl(); } }
        public string mainImageUrl { get { return getMainImageUrl(); } }

    }
}
