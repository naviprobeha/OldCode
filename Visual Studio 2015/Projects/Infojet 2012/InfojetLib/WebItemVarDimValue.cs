using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Navipro.Infojet.Lib
{
    public class WebItemVarDimValue
    {
        private string _webItemVarDimCode;
        private string _code;
        private string _description;
        private string _textConstantCode;
        private int _sortOrder;

        private Infojet infojetContext;

        public WebItemVarDimValue(Infojet infojetContext)
        {

            this.infojetContext = infojetContext;

        }


        public WebItemVarDimValue(Infojet infojetContext, string webItemVarDimCode, string code)
        {
            this._webItemVarDimCode = webItemVarDimCode;
            this._code = code;

            this.infojetContext = infojetContext;

            getFromDatabase();
        }


        private void getFromDatabase()
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Item Var Dim Code], [Code], [Description], [Text Constant Code], [Sort Order] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Var Dim Value") + "] WHERE [Web Item Var Dim Code] = @webItemVarDimCode AND [Code] = @code");
            databaseQuery.addStringParameter("webItemVarDimCode", _webItemVarDimCode, 20);
            databaseQuery.addStringParameter("code", _code, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                _webItemVarDimCode = dataReader.GetValue(0).ToString();
                _code = dataReader.GetValue(1).ToString();
                _description = dataReader.GetValue(2).ToString();
                _textConstantCode = dataReader.GetValue(3).ToString();
                _sortOrder = dataReader.GetInt32(4);

            }
            dataReader.Close();

            string translation = infojetContext.translate(_textConstantCode);
            if (translation != "") _description = translation;

        }

        public WebModelDimValueCollection getDimValues(string webModelNo)
        {
            WebModelDimValueCollection webModelDimValueCollection = new WebModelDimValueCollection();

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT DISTINCT [Web Item Var Dim Value Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Variant Value") + "] WHERE [Web Model No_] = @no AND [Web Item Var Dim Code] = @code ");
            databaseQuery.addStringParameter("webModelNo", webModelNo, 20);
            databaseQuery.addStringParameter("code", _code, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {



                i++;
            }

            return webModelDimValueCollection;
        }

        public string code { get { return _code; } set { _code = value; } }
        public string description { get { return _description; } set { _description = value; } }
        public string textConstantCode { get { return _textConstantCode; } }

    }
}
