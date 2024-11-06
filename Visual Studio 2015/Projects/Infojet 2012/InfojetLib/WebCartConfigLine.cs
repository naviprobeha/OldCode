using System;
using System.IO;
using System.Xml;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
    /// <summary>
    /// Summary description for Item.
    /// </summary>
    public class WebCartConfigLine
    {
        private Infojet infojetContext;

        private int _entryNo;
        private int _cartEntryNo;
        private string _webConfigModelNo = "";
        private string _optionCode = "";
        private string _description = "";
        private int _type;
        private string _value = "";
        private int _sortOrder;
        private bool _visible;
        private string _configDescription = "";
        private string _valueDescription = "";
        private float _lineAmount = 0;

        public WebCartConfigLine()
        {
        }

        public WebCartConfigLine(Infojet infojetContext, WebItemConfigLine webItemConfigLine, int cartEntryNo)
        {
            this.infojetContext = infojetContext;

            this._cartEntryNo = cartEntryNo;
            this._webConfigModelNo = webItemConfigLine.webConfigModelNo;
            this._optionCode = webItemConfigLine.optionCode;
            this._description = webItemConfigLine.description;
            this._type = webItemConfigLine.type;
            this._value = webItemConfigLine.optionValue;
            this._sortOrder = webItemConfigLine.sortOrder;
            this._valueDescription = webItemConfigLine.valueDescription;
            this._visible = webItemConfigLine.visible;
            this._lineAmount = webItemConfigLine.lineAmount;

        }


        public WebCartConfigLine(Infojet infojetContext, int entryNo)
        {
            //
            // TODO: Add constructor logic here
            //
            this.infojetContext = infojetContext;

            this._entryNo = entryNo;

            getFromDatabase();
        }

        public WebCartConfigLine(Infojet infojetContext, DataRow dataRow)
        {
            this.infojetContext = infojetContext;

            this._entryNo = int.Parse(dataRow.ItemArray.GetValue(0).ToString());
            this._cartEntryNo = int.Parse(dataRow.ItemArray.GetValue(1).ToString());
            this._webConfigModelNo = dataRow.ItemArray.GetValue(2).ToString();
            this._optionCode = dataRow.ItemArray.GetValue(3).ToString();
            this._description = dataRow.ItemArray.GetValue(4).ToString();
            this._type = int.Parse(dataRow.ItemArray.GetValue(5).ToString());
            this._value = dataRow.ItemArray.GetValue(6).ToString();
            this._sortOrder = int.Parse(dataRow.ItemArray.GetValue(7).ToString());
            this._configDescription = dataRow.ItemArray.GetValue(8).ToString();
            this._valueDescription = dataRow.ItemArray.GetValue(9).ToString();
            this._visible = false;
            if (dataRow.ItemArray.GetValue(10).ToString() == "1") this._visible = true;
            this._lineAmount = float.Parse(dataRow.ItemArray.GetValue(11).ToString());
        }

        private void getFromDatabase()
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Entry No_], [Cart Entry No_], [Web Config Model No_], [Option Code], [Description], [Type], [Value], [Sort Order], [Config Description], [Value Description], [Visible], [Line Amount] FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Config Line") + "] WHERE [Entry No_] = @entryNo");
            databaseQuery.addIntParameter("entryNo", _entryNo);


            SqlDataReader dataReader = databaseQuery.executeQuery();

            if (dataReader.Read())
            {

                _entryNo = dataReader.GetInt32(0);
                _cartEntryNo = dataReader.GetInt32(1);
                _webConfigModelNo = dataReader.GetValue(2).ToString();
                _optionCode = dataReader.GetValue(3).ToString();
                _description = dataReader.GetValue(4).ToString();
                _type = dataReader.GetInt32(5);
                _value = dataReader.GetValue(6).ToString();
                _sortOrder = dataReader.GetInt32(7);
                _configDescription = dataReader.GetValue(8).ToString();
                _valueDescription = dataReader.GetValue(9).ToString();
                _visible = false;
                if (dataReader.GetValue(10).ToString() == "1") _visible = true;
                _lineAmount = dataReader.GetFloat(11);
            }

            dataReader.Close();

        }

        public void save()
        {

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Entry No_] FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Config Line") + "] WHERE [Entry No_] = @entryNo");
            databaseQuery.addIntParameter("entryNo", _entryNo);

            SqlDataReader dataReader = databaseQuery.executeQuery();

            bool exists = dataReader.Read();
            dataReader.Close();

            int visibleVal = 0;
            if (_visible) visibleVal = 1;

            if (exists)
            {
                databaseQuery = infojetContext.systemDatabase.prepare("UPDATE [" + infojetContext.systemDatabase.getTableName("Web Cart Config Line") + "] SET [Cart Entry No_] = @cartEntryNo, [Web Config Model No_] = @webConfigModelNo, [Option Code] = @optionCode, [Description] = @description, [Type] = @type, [Value] = @value, [Sort Order] = @sortOrder, [Config Description] = @configDescription, [Value Description] = @valueDescription, [Visible] = @visible, [Line Amount] = @lineAmount WHERE [Entry No_] = @entryNo");
                databaseQuery.addIntParameter("@entryNo", _entryNo);
                databaseQuery.addIntParameter("@cartEntryNo", _cartEntryNo);
                databaseQuery.addStringParameter("@webConfigModelNo", _webConfigModelNo, 20);
                databaseQuery.addStringParameter("@optionCode", _optionCode, 20);
                databaseQuery.addStringParameter("@description", _description, 30);
                databaseQuery.addIntParameter("@type", _type);
                databaseQuery.addStringParameter("@value", _value, 40);
                databaseQuery.addIntParameter("@sortOrder", _sortOrder);
                databaseQuery.addStringParameter("@configDescription", _configDescription, 250);
                databaseQuery.addStringParameter("@valueDescription", _valueDescription, 250);
                databaseQuery.addSmallIntParameter("@visible", visibleVal);
                databaseQuery.addDecimalParameter("@lineAmount", _lineAmount);

                databaseQuery.execute();

            }
            else
            {

                databaseQuery = infojetContext.systemDatabase.prepare("INSERT INTO [" + infojetContext.systemDatabase.getTableName("Web Cart Config Line") + "] ([Cart Entry No_], [Web Config Model No_], [Option Code], [Description], [Type], [Value], [Sort Order], [Config Description], [Value Description], [Visible], [Line Amount]) VALUES (@cartEntryNo, @webConfigModelNo, @optionCode, @description, @type, @value, @sortOrder, @configDescription, @valueDescription, @visible, @lineAmount)");

                databaseQuery.addIntParameter("@cartEntryNo", _cartEntryNo);
                databaseQuery.addStringParameter("@webConfigModelNo", _webConfigModelNo, 20);
                databaseQuery.addStringParameter("@optionCode", _optionCode, 20);
                databaseQuery.addStringParameter("@description", _description, 30);
                databaseQuery.addIntParameter("@type", _type);
                databaseQuery.addStringParameter("@value", _value, 40);
                databaseQuery.addIntParameter("@sortOrder", _sortOrder);
                databaseQuery.addStringParameter("@configDescription", _configDescription, 250);
                databaseQuery.addStringParameter("@valueDescription", _valueDescription, 250);
                databaseQuery.addSmallIntParameter("@visible", visibleVal);
                databaseQuery.addDecimalParameter("@lineAmount", _lineAmount);

                databaseQuery.execute();

                this._entryNo = (int)infojetContext.systemDatabase.getInsertedSeqNo();
            }


        }


        public void delete()
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("DELETE FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Config Line") + "] WHERE [Entry No_] = @entryNo");
            databaseQuery.addIntParameter("entryNo", _entryNo);
            databaseQuery.execute();
        }

        public int entryNo { get { return _entryNo; } set { _entryNo = value; } }
        public int cartEntryNo { get { return _cartEntryNo; } set { _cartEntryNo = value; } }
        public string webConfigModelNo { get { return _webConfigModelNo; } set { _webConfigModelNo = value; } }
        public string optionCode { get { return _optionCode; } set { _optionCode = value; } }
        public string description { get { return _description; } set { _description = value; } }
        public int type { get { return _type; } set { _type = value; } }
        public string value { get { return _value; } set { _value = value; } }
        public int sortOrder { get { return _sortOrder; } set { _sortOrder = value; } }
        public string configDescription { get { return _configDescription; } set { _configDescription = value; } }
        public string valueDescription { get { return _valueDescription; } set { _valueDescription = value; } }
        public bool visible { get { return _visible; } set { _visible = value; } }

        public float lineAmount { get { return _lineAmount; } set { _lineAmount = value; } }


        public static DataSet getCartConfigLinesDataSet(Infojet infojetContext, int cartEntryNo)
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Entry No_], [Cart Entry No_], [Web Config Model No_], [Option Code], [Description], [Type], [Value], [Sort Order], [Config Description], [Value Description], [Visible], [Line Amount] FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Config Line") + "] WHERE [Cart Entry No_] = @cartEntryNo");
            databaseQuery.addIntParameter("@cartEntryNo", cartEntryNo);


            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            return dataSet;

        }

        public static WebCartConfigLineCollection getCartConfigLines(Infojet infojetContext, int cartEntryNo, bool onlyVisible)
        {
            WebCartConfigLineCollection webCartConfigLineCollection = new WebCartConfigLineCollection();

            DataSet dataSet = getCartConfigLinesDataSet(infojetContext, cartEntryNo);
            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                WebCartConfigLine webCartConfigLine = new WebCartConfigLine(infojetContext, dataSet.Tables[0].Rows[i]);
                
                if ((onlyVisible) && (webCartConfigLine.visible)) webCartConfigLineCollection.Add(webCartConfigLine);
                if (!onlyVisible) webCartConfigLineCollection.Add(webCartConfigLine);

                i++;
            }

            return webCartConfigLineCollection;

        }

    }
}
