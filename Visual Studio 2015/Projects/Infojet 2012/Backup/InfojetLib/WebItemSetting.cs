using System;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
    /// <summary>
    /// Summary description for Item.
    /// </summary>
    public class WebItemSetting
    {
        private Infojet infojetContext;

        private int _type;
        private string _no;
        private int _availability = 0;
        private int _visibility = 0;
        private int _leadTimeDays = 0;
        private DateTime _preOrderDeliveryDate;
        private bool _calendarBooking;
        private int _safetyInventoryLevel = 0;
        private int _fixedInventoryValue = 0;
        private string _webConfigModelNo = "";
        private bool _requireConfiguration = false;


        public WebItemSetting(Infojet infojetContext, int type, string no)
        {
            //
            // TODO: Add constructor logic here
            //
            this.infojetContext = infojetContext;

            this._type = type;
            this._no = no;

            getFromDatabase();

        }

        public WebItemSetting(Infojet infojetContext, SqlDataReader dataReader)
        {
            //
            // TODO: Add constructor logic here
            //
            this.infojetContext = infojetContext;

            readData(dataReader);

            if (_availability == 0) _availability = infojetContext.webSite.availability + 1;
            if (_visibility == 0) _visibility = infojetContext.webSite.visibility + 1;
            if (_safetyInventoryLevel == 0) _safetyInventoryLevel = (int)infojetContext.webSite.zeroInventoryValue;

        }

 
        private void getFromDatabase()
        {
            if (!isCached(_type, _no))
            {
                DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Type], [No_], [Availability], [Visibility], [Lead Time Days], [Pre-Order Delivery Date], [Calendar Booking], [Safety Inventory Level], [Fixed Inventory Value], [Web Config Model No_], [Require Configuration] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Setting") + "] WHERE [Type] = @type AND [No_] = @no");
                databaseQuery.addIntParameter("type", _type);
                databaseQuery.addStringParameter("no", _no, 20);

                SqlDataReader dataReader = databaseQuery.executeQuery();
                if (dataReader.Read())
                {

                    readData(dataReader);
                }

                dataReader.Close();

                if (_availability == 0) _availability = infojetContext.webSite.availability + 1;
                if (_visibility == 0) _visibility = infojetContext.webSite.visibility + 1;
                if (_safetyInventoryLevel == 0) _safetyInventoryLevel = (int)infojetContext.webSite.zeroInventoryValue;

                WebItemSetting.cache(this);

            }
        }

        private void readData(SqlDataReader dataReader)
        {

            _type = dataReader.GetInt32(0);
            _no = dataReader.GetValue(1).ToString();
            _availability = dataReader.GetInt32(2);
            _visibility = dataReader.GetInt32(3);
            _leadTimeDays = dataReader.GetInt32(4);
            _preOrderDeliveryDate = dataReader.GetDateTime(5);

            _calendarBooking = false;
            if (dataReader.GetValue(6).ToString() == "1") _calendarBooking = true;

            _safetyInventoryLevel = dataReader.GetInt32(7);
            _fixedInventoryValue = dataReader.GetInt32(8);

            _webConfigModelNo = dataReader.GetValue(9).ToString();

            _requireConfiguration = false;
            if (dataReader.GetValue(10).ToString() == "1") _requireConfiguration = true;


        }

        public void setViewOnly()
        {
            this._availability = 4;
        }

        public int type { get { return _type; } }
        public string no { get { return _no; } }
        public int availability { get { return _availability; } }
        public int visibility { get { return _visibility; } }
        public int leadTimeDays { get { return _leadTimeDays; } }
        public DateTime preOrderDeliveryDate { get { return _preOrderDeliveryDate; } }
        public bool calendarBooking { get { return _calendarBooking; } }
        public int safetyInventoryLevel { get { return _safetyInventoryLevel; } }
        public int fixedInventoryValue { get { return _fixedInventoryValue; } }
        public string webConfigModelNo { get { return _webConfigModelNo; } }
        public bool requireConfiguration { get { return _requireConfiguration; } }

        private bool isCached(int type, string no)
        {
            Hashtable webItemSettingHashTable = (Hashtable)System.Web.HttpContext.Current.Session["webItemSettingCollection"];
            if (webItemSettingHashTable == null) webItemSettingHashTable = new Hashtable();

            if (webItemSettingHashTable.Contains(type+";"+no))
            {   
                WebItemSetting webItemSetting = (WebItemSetting)webItemSettingHashTable[type+";"+no];
                this._availability = webItemSetting._availability;
                this._calendarBooking = webItemSetting._calendarBooking;
                this._fixedInventoryValue = webItemSetting._fixedInventoryValue;
                this._leadTimeDays = webItemSetting._leadTimeDays;
                this._preOrderDeliveryDate = webItemSetting._preOrderDeliveryDate;
                this._requireConfiguration = webItemSetting._requireConfiguration;
                this._safetyInventoryLevel = webItemSetting._safetyInventoryLevel;
                this._webConfigModelNo = webItemSetting._webConfigModelNo;
                this._visibility = webItemSetting._visibility;
                return true;
            }

            return false;
        }

        public static void cache(WebItemSetting webItemSetting)
        {
            Hashtable webItemSettingHashTable = (Hashtable)System.Web.HttpContext.Current.Session["webItemSettingCollection"];
            if (webItemSettingHashTable == null) webItemSettingHashTable = new Hashtable();

            if (webItemSettingHashTable.Contains(webItemSetting.type + ";" + webItemSetting.no))
            {
                webItemSettingHashTable[webItemSetting.type + ";" + webItemSetting.no] = webItemSetting;
            }
            else
            {
                webItemSettingHashTable.Add(webItemSetting.type + ";" + webItemSetting.no, webItemSetting);
            }
        }

        public static void preCacheItemList(Infojet infojetContext, DataSet productDataSet)
        {
            string query = "";
            string modelQuery = "";
            int i = 0;
            while (i < productDataSet.Tables[0].Rows.Count)
            {
                int type = int.Parse(productDataSet.Tables[0].Rows[i].ItemArray.GetValue(10).ToString());
                if (type == 1)
                {
                    string no = productDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();
                    if (query != "") query = query + " OR";
                    query = query + " ([Type] = '0' AND [No_] = '" + no + "')";
                }
                if (type == 2)
                {
                    string no = productDataSet.Tables[0].Rows[i].ItemArray.GetValue(11).ToString();
                    if (query != "") query = query + " OR";
                    if (modelQuery != "") modelQuery = modelQuery + " OR";
                    query = query + " ([Type] = '1' AND [No_] = '" + no + "')";
                    modelQuery = modelQuery + " (mv.[Web Model No_] = '" + no + "')";

                } 
               
                i++;
            }


            if (query != "")
            {
            i = 0;
                DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Type], [No_], [Availability], [Visibility], [Lead Time Days], [Pre-Order Delivery Date], [Calendar Booking], [Safety Inventory Level], [Fixed Inventory Value], [Web Config Model No_], [Require Configuration] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Setting") + "] WHERE " + query);

                SqlDataReader dataReader = databaseQuery.executeQuery();
                while (dataReader.Read())
                {
                    WebItemSetting webItemSetting = new WebItemSetting(infojetContext, dataReader);
                    cache(webItemSetting);
                    i++;
                }

                dataReader.Close();
            }
            if (modelQuery != "")
            {
                i = 0;
                DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT s.[Type], s.[No_], s.[Availability], s.[Visibility], s.[Lead Time Days], s.[Pre-Order Delivery Date], s.[Calendar Booking], s.[Safety Inventory Level], s.[Fixed Inventory Value], s.[Web Config Model No_], s.[Require Configuration] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Setting") + "] s, [" + infojetContext.systemDatabase.getTableName("Web Model Variant") + "] mv WHERE s.[Type] = 0 AND s.[No_] = mv.[Item No_] AND (" + modelQuery+")");

                SqlDataReader dataReader = databaseQuery.executeQuery();
                while (dataReader.Read())
                {
                    WebItemSetting webItemSetting = new WebItemSetting(infojetContext, dataReader);
                    cache(webItemSetting);
                    i++;
                }

                dataReader.Close();
            }
        }

    }
}
