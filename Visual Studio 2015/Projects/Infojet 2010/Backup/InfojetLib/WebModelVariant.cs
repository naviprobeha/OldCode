using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Navipro.Infojet.Lib
{
    public class WebModelVariant
    {
        private string _webModelNo;
        private string _itemNo;
        private string _itemVariantCode;
        private string _variantDimension1Code;
        private string _variantDimension2Code;
        private string _variantDimension3Code;
        private string _variantDimension4Code;
        private string _variantDim1Value;
        private string _variantDim2Value;
        private string _variantDim3Value;
        private string _variantDim4Value;

        private Infojet infojetContext;


        public WebModelVariant(Infojet infojetContext, string webModelNo, string itemNo, string itemVariantCode)
        {
            this._webModelNo = webModelNo;
            this._itemNo = itemNo;
            this._itemVariantCode = itemVariantCode;

            this.infojetContext = infojetContext;

            getFromDatabase();
        }

        public WebModelVariant(Infojet infojetContext, SqlDataReader dataReader)
        {
            this.infojetContext = infojetContext;

            readData(dataReader);
        }

        public WebModelVariant(Infojet infojetContext, DataRow dataRow)
        {
            this.infojetContext = infojetContext;

            _webModelNo = dataRow.ItemArray.GetValue(0).ToString();
            _itemNo = dataRow.ItemArray.GetValue(1).ToString();
            _itemVariantCode = dataRow.ItemArray.GetValue(2).ToString();
            _variantDimension1Code = dataRow.ItemArray.GetValue(3).ToString();
            _variantDimension2Code = dataRow.ItemArray.GetValue(4).ToString();
            _variantDimension3Code = dataRow.ItemArray.GetValue(5).ToString();
            _variantDimension4Code = dataRow.ItemArray.GetValue(6).ToString();
            _variantDim1Value = dataRow.ItemArray.GetValue(7).ToString();
            _variantDim2Value = dataRow.ItemArray.GetValue(8).ToString();
            _variantDim3Value = dataRow.ItemArray.GetValue(9).ToString();
            _variantDim4Value = dataRow.ItemArray.GetValue(10).ToString();
  
        }


        private void getFromDatabase()
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Model No_], [Item No_], [Item Variant Code], [Variant Dimension 1 Code], [Variant Dimension 2 Code], [Variant Dimension 3 Code], [Variant Dimension 4 Code], [Variant Dim 1 Value], [Variant Dim 2 Value], [Variant Dim 3 Value], [Variant Dim 4 Value] FROM [" + infojetContext.systemDatabase.getTableName("Web Model Variant") + "] WHERE [Web Model No_] = @webModelNo AND [Item No_] = @itemNo AND [Item Variant Code] = @itemVariantCode");
            databaseQuery.addStringParameter("webModelNo", _webModelNo, 20);
            databaseQuery.addStringParameter("itemNo", _itemNo, 20);
            databaseQuery.addStringParameter("itemVariantCode", _itemVariantCode, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                readData(dataReader);
            }
            dataReader.Close();


        }

        private void readData(SqlDataReader dataReader)
        {
            _webModelNo = dataReader.GetValue(0).ToString();
            _itemNo = dataReader.GetValue(1).ToString();
            _itemVariantCode = dataReader.GetValue(2).ToString();
            _variantDimension1Code = dataReader.GetValue(3).ToString();
            _variantDimension2Code = dataReader.GetValue(4).ToString();
            _variantDimension3Code = dataReader.GetValue(5).ToString();
            _variantDimension4Code = dataReader.GetValue(6).ToString();
            _variantDim1Value = dataReader.GetValue(7).ToString();
            _variantDim2Value = dataReader.GetValue(8).ToString();
            _variantDim3Value = dataReader.GetValue(9).ToString();
            _variantDim4Value = dataReader.GetValue(10).ToString();
        }

        public string webModelNo { get { return _webModelNo; } }
        public string itemNo { get { return _itemNo; } }
        public string itemVariantCode { get { return _itemVariantCode; } }
        public string variantDimension1Code { get { return _variantDimension1Code; } }
        public string variantDimension2Code { get { return _variantDimension2Code; } }
        public string variantDimension3Code { get { return _variantDimension3Code; } }
        public string variantDimension4Code { get { return _variantDimension4Code; } }
        public string variantDim1Value { get { return _variantDim1Value; } }
        public string variantDim2Value { get { return _variantDim2Value; } }
        public string variantDim3Value { get { return _variantDim3Value; } }
        public string variantDim4Value { get { return _variantDim4Value; } }

    }
}
