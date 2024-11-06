using System;
using System.Xml;
using System.Data;
using System.Data.SqlServerCe;

namespace SmartOrder
{
    /// <summary>
    /// Summary description for DataItemSizes.
    /// </summary>
    public class DataItemCrossReferences : DataCollection
    {
        private SmartDatabase smartDatabase;
        private DataItem dataItem;

        private bool multipleMatchesValue;

        public DataItemCrossReferences(SmartDatabase smartDatabase)
        {
            this.smartDatabase = smartDatabase;
            //
            // TODO: Add constructor logic here
            //
        }

        public DataItemCrossReferences(XmlElement tableElement, SmartDatabase smartDatabase)
        {
            fromDOM(tableElement, smartDatabase);
        }

        public bool multipleMatches
        {
            get
            {
                return multipleMatchesValue;
            }
        }

        public void fromDOM(XmlElement tableElement, SmartDatabase smartDatabase)
        {
            XmlNodeList records = tableElement.GetElementsByTagName("R");
            int i = 0;
            while (i < records.Count)
            {
                XmlElement record = (XmlElement)records.Item(i);

                DataItemCrossReference dataItemCrossReference = new DataItemCrossReference(record, smartDatabase, true);

                i++;
            }
        }

        public DataItemCrossReference getEanCode(string referenceNo)
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT r.itemNo, r.variantDimCode, r.unitCode, r.type, r.no, r.referenceNo, r.description, i.description, i.baseUnit, i.price, i.defaultQuantity, i.itemDiscountGroup FROM itemCrossReference r, item i WHERE r.type = 3 AND r.referenceNo = '" + referenceNo + "' AND r.itemNo = i.no");

            if (dataReader.Read())
            {
                try
                {
                    DataItemCrossReference dataItemCrossReference = new DataItemCrossReference();
                    dataItemCrossReference.itemNo = (string)dataReader.GetValue(0);
                    dataItemCrossReference.variantDimCode = (string)dataReader.GetValue(1);
                    dataItemCrossReference.unitCode = (string)dataReader.GetValue(2);
                    dataItemCrossReference.type = dataReader.GetInt32(3);
                    dataItemCrossReference.no = (string)dataReader.GetValue(4);
                    dataItemCrossReference.referenceNo = referenceNo;
                    dataItemCrossReference.description = (string)dataReader.GetValue(6);

                    dataItemCrossReference.item = new DataItem(dataItemCrossReference.itemNo);
                    dataItemCrossReference.item.description = (string)dataReader.GetValue(7);
                    dataItemCrossReference.item.baseUnit = (string)dataReader.GetValue(8);
                    dataItemCrossReference.item.price = dataReader.GetFloat(9);
                    dataItemCrossReference.item.defaultQuantity = dataReader.GetFloat(10);
                    dataItemCrossReference.item.itemDiscountGroup = (string)dataReader.GetValue(11);
                    dataItemCrossReference.item.setSmartDatabase(smartDatabase);

                    if (dataReader.Read()) multipleMatchesValue = true;

                    dataReader.Dispose();
                    return dataItemCrossReference;
                }
                catch (SqlCeException e)
                {
                    smartDatabase.ShowErrors(e);
                }
            }
            dataReader.Dispose();
            return null;

        }

        public XmlElement toDOM(XmlDocument xmlDocumentContext)
        {
            return null;
        }
    }
}
