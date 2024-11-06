using System;
using System.Xml;
using System.Data;
using System.Data.SqlServerCe;

namespace SmartOrder
{
    /// <summary>
    /// Summary description for DataItemSizes.
    /// </summary>
    public class DataItemVariantDims : DataCollection
    {
        private SmartDatabase smartDatabase;
        private DataItem dataItem;

        public DataItemVariantDims(SmartDatabase smartDatabase, DataItem dataItem)
        {
            this.smartDatabase = smartDatabase;
            this.dataItem = dataItem;
            //
            // TODO: Add constructor logic here
            //
        }

        public DataItemVariantDims(XmlElement tableElement, SmartDatabase smartDatabase)
        {
            fromDOM(tableElement, smartDatabase);
        }

        public void fromDOM(XmlElement tableElement, SmartDatabase smartDatabase)
        {
            XmlNodeList records = tableElement.GetElementsByTagName("R");
            int i = 0;
            while (i < records.Count)
            {
                XmlElement record = (XmlElement)records.Item(i);

                DataItemVariantDim dataItemVariantDim = new DataItemVariantDim(record, smartDatabase, true);

                i++;
            }
        }

        public DataSet getDataSet()
        {
            SqlCeDataAdapter variantDimAdapter = smartDatabase.dataAdapterQuery("SELECT * FROM itemVariantDim WHERE itemNo = '" + dataItem.no + "'");

            DataSet variantDataSet = new DataSet();
            variantDimAdapter.Fill(variantDataSet, "variantDim");
            variantDimAdapter.Dispose();

            return variantDataSet;
        }

        public XmlElement toDOM(XmlDocument xmlDocumentContext)
        {
            return null;
        }
    }
}
