using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
    public class WebModelVariants
    {
        private Infojet infojetContext;

        public WebModelVariants(Infojet infojetContext)
        {
            this.infojetContext = infojetContext;
        }

        public DataSet getDataSet(string webModelNo)
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Model No_], [Item No_], [Item Variant Code], [Variant Dimension 1 Code], [Variant Dimension 2 Code], [Variant Dimension 3 Code], [Variant Dimension 4 Code], [Variant Dim 1 Value], [Variant Dim 2 Value], [Variant Dim 3 Value], [Variant Dim 4 Value] FROM [" + infojetContext.systemDatabase.getTableName("Web Model Variant") + "] WHERE [Web Model No_] = @webModelNo");
            databaseQuery.addStringParameter("webModelNo", webModelNo, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            return dataSet;

        }

        public Hashtable getTable(string webModelNo)
        {
            Hashtable table = new Hashtable();

            DataSet dataSet = getDataSet(webModelNo);

            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                WebModelVariant webModelVariant = new WebModelVariant(infojetContext, dataSet.Tables[0].Rows[i]);
                if (table[webModelVariant.variantDim1Value + "-" + webModelVariant.variantDim2Value + "-" + webModelVariant.variantDim3Value + "-" + webModelVariant.variantDim4Value] == null)
                {
                    table.Add((webModelVariant.variantDim1Value + "-" + webModelVariant.variantDim2Value + "-" + webModelVariant.variantDim3Value + "-" + webModelVariant.variantDim4Value).ToString(), webModelVariant);
                }
                i++;
            }

            return table;

        }


        public WebModelVariant getVariant(string webModelNo, string variantDim1Value, string variantDim2Value, string variantDim3Value, string variantDim4Value)
        {
            WebModelVariant webModelVariant = null;

            string variantQuery = "";
            if (variantDim1Value != "") variantQuery = variantQuery + " AND [Variant Dim 1 Value] = @variantDim1Value";
            if (variantDim2Value != "") variantQuery = variantQuery + " AND [Variant Dim 2 Value] = @variantDim2Value";
            if (variantDim3Value != "") variantQuery = variantQuery + " AND [Variant Dim 3 Value] = @variantDim3Value";
            if (variantDim4Value != "") variantQuery = variantQuery + " AND [Variant Dim 4 Value] = @variantDim4Value";

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Model No_], [Item No_], [Item Variant Code], [Variant Dimension 1 Code], [Variant Dimension 2 Code], [Variant Dimension 3 Code], [Variant Dimension 4 Code], [Variant Dim 1 Value], [Variant Dim 2 Value], [Variant Dim 3 Value], [Variant Dim 4 Value] FROM [" + infojetContext.systemDatabase.getTableName("Web Model Variant") + "] WHERE [Web Model No_] = @webModelNo "+variantQuery);
            databaseQuery.addStringParameter("webModelNo", webModelNo, 20);
            databaseQuery.addStringParameter("variantDim1Value", variantDim1Value, 20);
            databaseQuery.addStringParameter("variantDim2Value", variantDim2Value, 20);
            databaseQuery.addStringParameter("variantDim3Value", variantDim3Value, 20);
            databaseQuery.addStringParameter("variantDim4Value", variantDim4Value, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                webModelVariant = new WebModelVariant(infojetContext, dataReader);

            }

            dataReader.Close();

            return webModelVariant;
        }
    }
}
