using System;
using System.Xml;
using System.Data;
using System.Data.SqlServerCe;
using System.Collections;

namespace SmartOrder
{
    /// <summary>
    /// Summary description for DataItems.
    /// </summary>
    public class DataItemVariants : DataCollection
    {
        private SmartDatabase smartDatabase;
        private ArrayList dataX;
        private ArrayList dataY;
        private ArrayList dataValid;
        private DataItem dataItem;
        private DataColor dataColor;


        public DataItemVariants(SmartDatabase smartDatabase)
        {
            this.smartDatabase = smartDatabase;
            dataX = new ArrayList();
            dataY = new ArrayList();
            dataValid = new ArrayList();
        }

        public DataItemVariants(SmartDatabase smartDatabase, DataItem dataItem, DataColor dataColor)
        {
            this.smartDatabase = smartDatabase;
            dataX = new ArrayList();
            dataY = new ArrayList();
            dataValid = new ArrayList();
            this.dataItem = dataItem;
            this.dataColor = dataColor;
        }

        public DataItemVariants(XmlElement tableElement, SmartDatabase smartDatabase)
        {
            fromDOM(tableElement, smartDatabase);
            dataX = new ArrayList();
            dataY = new ArrayList();
            dataValid = new ArrayList();
        }


        public void fromDOM(XmlElement tableElement, SmartDatabase smartDatabase)
        {
            XmlNodeList records = tableElement.GetElementsByTagName("R");
            int i = 0;
            while (i < records.Count)
            {
                XmlElement record = (XmlElement)records.Item(i);

                DataItemVariant dataItemVariant = new DataItemVariant(record, smartDatabase, true);

                i++;
            }
        }

        public DataSet getDataSet()
        {
            SqlCeDataAdapter itemVariantAdapter = smartDatabase.dataAdapterQuery("SELECT * FROM itemVariant");

            DataSet itemVariantDataSet = new DataSet();
            itemVariantAdapter.Fill(itemVariantDataSet, "itemVariant");
            itemVariantAdapter.Dispose();

            return itemVariantDataSet;
        }

        public void produceDataGrids()
        {
            SqlCeDataAdapter itemVariantAdapter = smartDatabase.dataAdapterQuery("SELECT sizeCode, size2Code, valid FROM itemVariant WHERE baseItemNo = '" + dataItem.no + "' AND colorCode = '" + dataColor.code + "'");

            DataSet itemVariantDataSet = new DataSet();
            itemVariantAdapter.Fill(itemVariantDataSet, "itemVariant");
            itemVariantAdapter.Dispose();

            int i = 0;
            while (i < itemVariantDataSet.Tables[0].Rows.Count)
            {
                if (dataY.IndexOf((string)itemVariantDataSet.Tables[0].Rows[i].ItemArray.GetValue(0)) == -1)
                {
                    dataY.Add((string)itemVariantDataSet.Tables[0].Rows[i].ItemArray.GetValue(0));
                }
                if (dataX.IndexOf((string)itemVariantDataSet.Tables[0].Rows[i].ItemArray.GetValue(1)) == -1)
                {
                    dataX.Add((string)itemVariantDataSet.Tables[0].Rows[i].ItemArray.GetValue(1));
                }
                if ((int)itemVariantDataSet.Tables[0].Rows[i].ItemArray.GetValue(2) == 1)
                {
                    string hash = (string)itemVariantDataSet.Tables[0].Rows[i].ItemArray.GetValue(0) + ":" + (string)itemVariantDataSet.Tables[0].Rows[i].ItemArray.GetValue(1);
                    if (dataValid.IndexOf(hash) == -1)
                    {
                        dataValid.Add(hash);
                    }
                }
                i++;
            }


        }

        public ArrayList getDataX()
        {
            return dataX;
        }

        public ArrayList getDataY()
        {
            return dataY;
        }

        public bool checkIfValid(DataSize dataSize1, DataSize2 dataSize2)
        {
            if (dataSize2 != null)
            {
                if (dataValid.IndexOf(dataSize1.code + ":" + dataSize2.code) > -1) return true;
            }
            else
            {
                if (dataValid.IndexOf(dataSize1.code + ":") > -1) return true;
            }
            return false;
        }

        public XmlElement toDOM(XmlDocument xmlDocumentContext)
        {
            return null;
        }
    }
}
