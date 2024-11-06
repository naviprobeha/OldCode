using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Navipro.Infojet.Lib
{
    public class WebItemVarDim
    {
        private string _code;
        private string _description;
        private string _textConstantCode;

        private Infojet infojetContext;


        public WebItemVarDim(Infojet infojetContext, string code)
        {
            this._code = code;

            this.infojetContext = infojetContext;

            getFromDatabase();
        }

        public WebItemVarDim(Infojet infojetContext, DataRow dataRow)
        {
            this.infojetContext = infojetContext;

            _code = dataRow.ItemArray.GetValue(0).ToString();
            _description = dataRow.ItemArray.GetValue(1).ToString();
            _textConstantCode = dataRow.ItemArray.GetValue(2).ToString();

        }


        private void getFromDatabase()
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Code], [Description], [Text Constant Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Var Dim") + "] WHERE [Code] = @code ");
            databaseQuery.addStringParameter("code", _code, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                _code = dataReader.GetValue(0).ToString();
                _description = dataReader.GetValue(1).ToString();
                _textConstantCode = dataReader.GetValue(2).ToString();

            }
            dataReader.Close();

            string translation = infojetContext.translate(_textConstantCode);
            if (translation != "") _description = translation;

        }

        public WebModelDimValueCollection getDimValues(string webModelNo)
        {
            WebModelDimValueCollection webModelDimValueCollection = new WebModelDimValueCollection();

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT DISTINCT v.[Web Item Var Dim Value Code], dv.[Sort Order] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Variant Value") + "] v, [" + infojetContext.systemDatabase.getTableName("Web Item Var Dim Value") + "] dv WHERE v.[Web Model No_] = @webModelNo AND v.[Web Item Var Dim Code] = @code AND dv.[Web Item Var Dim Code] = @code AND v.[Web Item Var Dim Value Code] = dv.[Code] ORDER BY dv.[Sort Order]");
            databaseQuery.addStringParameter("webModelNo", webModelNo, 20);
            databaseQuery.addStringParameter("code", _code, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                WebItemVarDimValue webItemVarDimValue = new WebItemVarDimValue(infojetContext, this.code, dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString());
                WebModelDimValue webModelDimValue = new WebModelDimValue(webItemVarDimValue);
                webModelDimValueCollection.Add(webModelDimValue);


                i++;
            }

            return webModelDimValueCollection;
        }

        public string code { get { return _code; } }
        public string description { get { return _description; } }
        public string textConstantCode { get { return _textConstantCode; } }

    }
}
