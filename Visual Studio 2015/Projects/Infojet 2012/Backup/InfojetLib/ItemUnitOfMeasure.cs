using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
    public class ItemUnitOfMeasure
    {
        private string _itemNo = "";
        private string _salesUnitOfMeasureCode = "";
        private string _baseUnitOfMeasureCode = "";
        private float _qtyPerUnitOfMeasure = 0;

        public ItemUnitOfMeasure()
        { }

        public ItemUnitOfMeasure(SqlDataReader dataReader)
        {
            _itemNo = dataReader.GetValue(0).ToString();
            _salesUnitOfMeasureCode = dataReader.GetValue(1).ToString();
            _baseUnitOfMeasureCode = dataReader.GetValue(2).ToString();
            _qtyPerUnitOfMeasure = 1;
            if (!dataReader.IsDBNull(3)) _qtyPerUnitOfMeasure = float.Parse(dataReader.GetValue(3).ToString());
        }

        public string itemNo { get { return _itemNo; } set { _itemNo = value; } }
        public string salesUnitOfMeasureCode { get { return _salesUnitOfMeasureCode; } set { _salesUnitOfMeasureCode = value; } }
        public string baseUnitOfMeasureCode { get { return _baseUnitOfMeasureCode; } set { _baseUnitOfMeasureCode = value; } }
        public float qtyPerUnitOfMeasure { get { return _qtyPerUnitOfMeasure; } set { _qtyPerUnitOfMeasure = value; } }

        public static Hashtable getItemSalesUnitTable(Infojet infojetContext, System.Data.DataSet itemDataSet)
        {
            Hashtable itemHashtable = new Hashtable();

            string whereQuery = "";

            int i = 0;
            while (i < itemDataSet.Tables[0].Rows.Count)
            {
                string itemNo = itemDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();
                if (whereQuery != "")
                {
                    whereQuery = whereQuery + " OR ";
                }
                whereQuery = whereQuery + "i.[No_] = '" + itemNo + "'";

                i++;
            }

            SqlDataReader dataReader = infojetContext.systemDatabase.query("SELECT i.[No_], i.[Sales Unit of Measure], i.[Base Unit of Measure], iu.[Qty_ per Unit of Measure] FROM [" + infojetContext.systemDatabase.getTableName("Item") + "] i WITH (NOLOCK) LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Item Unit of Measure") + "] iu ON iu.[Item No_] = i.[No_] AND iu.[Code] = i.[Sales Unit of Measure] WHERE (" + whereQuery + ")");
            while (dataReader.Read())
            {
                ItemUnitOfMeasure itemUnitOfMeasure = new ItemUnitOfMeasure(dataReader);
                itemHashtable.Add(itemUnitOfMeasure.itemNo, itemUnitOfMeasure);
            }

            dataReader.Close();

            return itemHashtable;
        }
    }
}
