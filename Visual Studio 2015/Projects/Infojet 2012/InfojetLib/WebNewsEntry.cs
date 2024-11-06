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

        private string _header;
        private string _introHeader;
        private string _ingress;
        private string _body;

        public WebNewsEntry()
        { }

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
            if (_header != null) return _header;
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Header] FROM [" + infojetContext.systemDatabase.getTableName("Web News Header") + "] WHERE [Web News Entry No_] = @webNewsEntryNo AND [Language Code] = @languageCode");
            databaseQuery.addStringParameter("webNewsEntryNo", no, 20);
            databaseQuery.addStringParameter("languageCode", infojetContext.languageCode, 20);

            _header = "";

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {

                _header = dataReader.GetValue(0).ToString();

            }

            dataReader.Close();

            return _header;
        }

        private string getIntroHeader()
        {
            if (_introHeader != null) return _introHeader;

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Introduction Header] FROM [" + infojetContext.systemDatabase.getTableName("Web News Header") + "] WHERE [Web News Entry No_] = @webNewsEntryNo AND [Language Code] = @languageCode");
            databaseQuery.addStringParameter("webNewsEntryNo", no, 20);
            databaseQuery.addStringParameter("languageCode", infojetContext.languageCode, 20);

            _introHeader = "";

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {

                _introHeader = dataReader.GetValue(0).ToString();

            }

            dataReader.Close();

            return _introHeader;
        }


        private string getIngress()
        {
            if (_ingress != null) return _ingress;

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Text] FROM [" + infojetContext.systemDatabase.getTableName("Web News Line") + "] WHERE [Web News Entry No_] = @webNewsEntryNo AND [Language Code] = @languageCode AND [Type] = 0 ORDER BY [Line No_]");
            databaseQuery.addStringParameter("webNewsEntryNo", no, 20);
            databaseQuery.addStringParameter("languageCode", infojetContext.languageCode, 20);

            _ingress = "";

            SqlDataReader dataReader = databaseQuery.executeQuery();
            while (dataReader.Read())
            {

                _ingress = _ingress + " " + dataReader.GetValue(0).ToString();
                if (dataReader.GetValue(0).ToString() == "") _ingress = _ingress + "<br/>";

            }

            dataReader.Close();

            return _ingress;
        }

        private string getBody()
        {
            if (_body != null) return _body;
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Text] FROM [" + infojetContext.systemDatabase.getTableName("Web News Line") + "] WHERE [Web News Entry No_] = @webNewsEntryNo AND [Language Code] = @languageCode AND [Type] = 1 ORDER BY [Line No_]");
            databaseQuery.addStringParameter("webNewsEntryNo", no, 20);
            databaseQuery.addStringParameter("languageCode", infojetContext.languageCode, 20);

            _body = "";

            SqlDataReader dataReader = databaseQuery.executeQuery();
            while (dataReader.Read())
            {

                _body = _body + " " + dataReader.GetValue(0).ToString();
                if (dataReader.GetValue(0).ToString() == "") _body = _body + "<br/>";


            }

            dataReader.Close();

            return _body;
        }

        private string getUrl()
        {
            WebPage webPage = infojetContext.webSite.getWebPageByCategory(infojetContext.webSite.newsPageCategoryCode, infojetContext.userSession);
            if (webPage != null)
            {
                Link link = webPage.getUrlLink();
                link.setNewsEntry(no, this.header);
                return link.toUrl();

            }

            return "";
        }

        public void setIntroImageSize(float width, float height)
        {
            if (this.introImageCode != "")
            {
                if (introImage == null) introImage = new WebImage(infojetContext, this.introImageCode);
                introImage.setSize(width, height);
            }
        }

        public void setMainImageSize(float width, float height)
        {
            if (this.mainImageCode != "")
            {
                if (mainImage == null) mainImage = new WebImage(infojetContext, this.mainImageCode);
                mainImage.setSize(width, height);
            }
        }

        public string getMainImageUrl()
        {
            if (this.mainImageCode != "")
            {
                if (mainImage == null) mainImage = new WebImage(infojetContext, this.mainImageCode);
                return mainImage.getUrl();
            }
            return "";
        }

        public string getIntroImageUrl()
        {
            if (this.introImageCode != "")
            {
                if (introImage == null) introImage = new WebImage(infojetContext, this.introImageCode);
                return introImage.getUrl();
            }
            return "";
        }

        public string no { get { return _no; } set { _no = value; } }
        public string description { get { return _description; } set { _description = value; } }
        public DateTime creationDate { get { return _creationDate; } set { _creationDate = value; } }
        public DateTime publishedFromDate { get { return _publishedFromDate; } set { _publishedFromDate = value; } }
        public DateTime publishedToDate { get { return _publishedToDate; } set { _publishedToDate = value; } }
        public string introImageCode { get { return _introImageCode; } set { _introImageCode = value; } }
        public string mainImageCode { get { return _mainImageCode; } set { _mainImageCode = value; } }
        public bool commonHeader { get { return _commonHeader; } set { _commonHeader = value; } }

        public string header { get { return getHeader(); } set { } }
        public string introHeader { get { return getIntroHeader(); } set { } }
        public string ingress { get { return getIngress(); } set { } }
        public string body { get { return getBody(); } set { } }
        public string link { get { return getUrl(); } }
        public string introImageUrl { get { return getIntroImageUrl(); } }
        public string mainImageUrl { get { return getMainImageUrl(); } }

    }
}
