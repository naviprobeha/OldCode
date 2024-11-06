using System;
using System.Data.SqlServerCe;
using System.Data;
using System.Xml;

namespace Navipro.SmartInventory
{
    /// <summary>
    /// Summary description for DataSalesLine.
    /// </summary>
    public class DataPurchaseOrder : ServiceArgument
    {
        private string _itemNo = "";
        private string _variantCode = "";
        private string _documentNo = "";
        private string _description = "";
        private string _vendorName = "";
        private float _quantity = 0;
        private float _outstandingQty = 0;

        private string whseDocNo = "";
        private SmartDatabase smartDatabase;

        public DataPurchaseOrder(SmartDatabase smartDatabase, SqlCeDataReader dataReader)
        {

            //
            // TODO: Add constructor logic here
            //
            this.smartDatabase = smartDatabase;
            fromDataReader(dataReader);
        }

        public DataPurchaseOrder(SmartDatabase smartDatabase, DataRow dataRow)
        {

            //
            // TODO: Add constructor logic here
            //
            this.smartDatabase = smartDatabase;
            fromDataRow(dataRow);
        }

        public DataPurchaseOrder(SmartDatabase smartDatabase)
        {

            //
            // TODO: Add constructor logic here
            //
            this.smartDatabase = smartDatabase;

        }

        public string itemNo { get { return _itemNo; } set { _itemNo = value; } }
        public string variantCode { get { return _variantCode; } set { _variantCode = value; } }
        public string documentNo { get { return _documentNo; } set { _documentNo = value; } }
        public string description { get { return _description; } set { _description = value; } }
        public string vendorName { get { return _vendorName; } set { _vendorName = value; } }
        public float quantity { get { return _quantity; } set { _quantity = value; } }
        public float outstandingQty { get { return _outstandingQty; } set { _outstandingQty = value; } }

        public void save()
        {

            try
            {
                SqlCeDataReader dataReader;

                dataReader = smartDatabase.query("SELECT documentNo FROM purchaseOrder WHERE itemNo = '" + _itemNo + "' AND variantCode = '" + _variantCode + "' AND documentNo = '" + _documentNo + "'");

                if (!dataReader.Read())
                {
                    smartDatabase.nonQuery("INSERT INTO purchaseOrder (itemNo, variantCode, documentNo, description, vendorName, quantity, outstandingQty) VALUES ('" + _itemNo + "', '" + _variantCode + "', '" + _documentNo + "', '" + _description + "', '" + _vendorName + "', '" + _quantity + "', '" + _outstandingQty + "')");
                }
                else
                {
                    smartDatabase.nonQuery("UPDATE purchaseOrder SET itemNo = '" + _itemNo + "', variantCode = '" + _variantCode + "', documentNo = '" + _documentNo + "', description = '" + _description + "', vendorName = '" + _vendorName + "', quantity = '" + _quantity + "', outstandingQty = '" + _outstandingQty + "' WHERE itemNo = '" + _itemNo + "' AND variantCode = '" + _variantCode + "' AND documentNo = '" + _documentNo + "'");
                }
                dataReader.Close();
                dataReader.Dispose();

            }
            catch (SqlCeException e)
            {
                smartDatabase.ShowErrors(e);
            }
        }

        public void delete()
        {
            try
            {
                smartDatabase.nonQuery("DELETE FROM purchaseOrder WHERE itemNo = '" + _itemNo + "' AND variantCode = '" + _variantCode + "' AND _documentNo = '" + _documentNo + "'");

            }
            catch (SqlCeException e)
            {
                smartDatabase.ShowErrors(e);
            }

        }

        public static void deleteAll(SmartDatabase smartDatabase)
        {
            smartDatabase.nonQuery("DELETE FROM purchaseOrder");

        }


        private void fromDataReader(SqlCeDataReader dataReader)
        {
            this._itemNo = (string)dataReader.GetValue(0);
            this._variantCode = (string)dataReader.GetValue(1);
            this._documentNo = (string)dataReader.GetValue(2);
            this._description = (string)dataReader.GetValue(3);
            this._vendorName = (string)dataReader.GetValue(4);
            this._quantity = float.Parse(dataReader.GetValue(5).ToString());
            this._outstandingQty = float.Parse(dataReader.GetValue(6).ToString());

        }

        private void fromDataRow(DataRow dataRow)
        {
            this._itemNo = (string)dataRow.ItemArray.GetValue(0);
            this._variantCode = (string)dataRow.ItemArray.GetValue(1);
            this._documentNo = (string)dataRow.ItemArray.GetValue(2);
            this._description = (string)dataRow.ItemArray.GetValue(3);
            this._vendorName = (string)dataRow.ItemArray.GetValue(4);
            this._quantity = float.Parse(dataRow.ItemArray.GetValue(5).ToString());
            this._outstandingQty = float.Parse(dataRow.ItemArray.GetValue(6).ToString());


        }

        public static DataPurchaseOrder fromDOM(XmlElement purchaseOrdersElement, SmartDatabase smartDatabase)
        {
            XmlNodeList purchaseOrderNodeList = purchaseOrdersElement.SelectNodes("purchaseOrder");
            DataPurchaseOrder firstPurchaseOrder = null;

            int i = 0;
            while (i < purchaseOrderNodeList.Count)
            {
                XmlElement purchaseOrderElement = (XmlElement)purchaseOrderNodeList[i];

                DataPurchaseOrder purchaseOrder = new DataPurchaseOrder(smartDatabase);
                purchaseOrder.itemNo = purchaseOrderElement.GetAttribute("itemNo");
                purchaseOrder.variantCode = purchaseOrderElement.GetAttribute("variantCode");
                purchaseOrder.documentNo = purchaseOrderElement.GetAttribute("documentNo");

                if (purchaseOrderElement.SelectSingleNode("description").FirstChild != null)
                {
                    purchaseOrder.description = purchaseOrderElement.SelectSingleNode("description").FirstChild.Value;
                }
                if (purchaseOrderElement.SelectSingleNode("vendorName").FirstChild != null)
                {
                    purchaseOrder.vendorName = purchaseOrderElement.SelectSingleNode("vendorName").FirstChild.Value;
                }
                if (purchaseOrderElement.SelectSingleNode("quantity").FirstChild != null)
                {
                    purchaseOrder.quantity = float.Parse(purchaseOrderElement.SelectSingleNode("quantity").FirstChild.Value);
                }
                if (purchaseOrderElement.SelectSingleNode("outstandingQty").FirstChild != null)
                {
                    purchaseOrder.outstandingQty = float.Parse(purchaseOrderElement.SelectSingleNode("outstandingQty").FirstChild.Value);
                }

                purchaseOrder.save();

                if (firstPurchaseOrder == null) firstPurchaseOrder = purchaseOrder;

                i++;
            }

            return firstPurchaseOrder;

        }

        public static DataSet getDataSet(SmartDatabase smartDatabase, string itemNo, string variantCode)
        {
            SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT itemNo, variantCode, documentNo, description, vendorName, quantity, outstandingQty FROM purchaseOrder WHERE itemNo = '" + itemNo + "' AND variantCode = '" + variantCode + "' ORDER BY documentNo");

            DataSet purchaseLineDataSet = new DataSet();
            adapter.Fill(purchaseLineDataSet, "purchaseOrder");
            adapter.Dispose();

            return purchaseLineDataSet;
        }


        #region ServiceArgument Members

        public XmlElement toDOM(XmlDocument xmlDocumentContext)
        {
            XmlElement docElement = xmlDocumentContext.CreateElement("document");

            XmlElement documentNoElement = xmlDocumentContext.CreateElement("documentNo");
            XmlText documentNoText = xmlDocumentContext.CreateTextNode(_documentNo);
            documentNoElement.AppendChild(documentNoText);

            docElement.AppendChild(documentNoElement);

            XmlElement whseDocNoElement = xmlDocumentContext.CreateElement("whseDocNo");
            XmlText whseDocNoText = xmlDocumentContext.CreateTextNode(whseDocNo);
            whseDocNoElement.AppendChild(whseDocNoText);

            docElement.AppendChild(whseDocNoElement);


            return docElement;
        }

        #endregion


        public void setWhseDocNo(string whseDocNo)
        {
            this.whseDocNo = whseDocNo;

        }


    }

}
