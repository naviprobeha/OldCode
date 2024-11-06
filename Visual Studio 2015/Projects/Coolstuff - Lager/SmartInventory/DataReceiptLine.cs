using System;
using System.Data.SqlServerCe;
using System.Data;
using System.Xml;

namespace Navipro.SmartInventory
{
    /// <summary>
    /// Summary description for DataSalesLine.
    /// </summary>
    public class DataReceiptLine : ServiceArgument
    {
        private string _documentNo = "";
        private int _lineNo = 0;
        private string _binCode = "";
        private string _brand = "";
        private string _itemNo = "";
        private string _variantCode = "";
        private string _description = "";
        private string _description2 = "";
        private float _quantity = 0;
        private float _qtyToReceive = 0;

        private float _weight = 0;
        private float _length = 0;
        private float _height = 0;
        private float _width = 0;



        private SmartDatabase smartDatabase;

        public DataReceiptLine(SmartDatabase smartDatabase, SqlCeDataReader dataReader)
        {

            //
            // TODO: Add constructor logic here
            //
            this.smartDatabase = smartDatabase;
            fromDataReader(dataReader);
        }

        public DataReceiptLine(SmartDatabase smartDatabase, DataRow dataRow)
        {

            //
            // TODO: Add constructor logic here
            //
            this.smartDatabase = smartDatabase;
            fromDataRow(dataRow);
        }

        public DataReceiptLine(SmartDatabase smartDatabase)
        {

            //
            // TODO: Add constructor logic here
            //
            this.smartDatabase = smartDatabase;

        }

        public string documentNo { get { return _documentNo; } set { _documentNo = value; } }
        public int lineNo { get { return _lineNo; } set { _lineNo = value; } }
        public string binCode { get { return _binCode; } set { _binCode = value; } }
        public string brand { get { return _brand; } set { _brand = value; } }
        public string itemNo { get { return _itemNo; } set { _itemNo = value; } }
        public string variantCode { get { return _variantCode; } set { _variantCode = value; } }
        public string description { get { return _description; } set { _description = value; } }
        public string description2 { get { return _description2; } set { _description2 = value; } }
        public float quantity { get { return _quantity; } set { _quantity = value; } }
        public float qtyToReceive { get { return _qtyToReceive; } set { _qtyToReceive = value; } }
        public float weight { get { return _weight; } set { _weight = value; } }
        public float length { get { return _length; } set { _length = value; } }
        public float height { get { return _height; } set { _height = value; } }
        public float width { get { return _width; } set { _width = value; } }

        public void save()
        {

            try
            {
                SqlCeDataReader dataReader;

                dataReader = smartDatabase.query("SELECT documentNo FROM receiptLine WHERE documentNo = '" + _documentNo + "' AND docLineNo = '" + _lineNo + "'");

                if (!dataReader.Read())
                {
                    smartDatabase.nonQuery("INSERT INTO receiptLine (documentNo, docLineNo, binCode, brand, itemNo, variantCode, description, description2, quantity, qtyToReceive, weight, length, width, height) VALUES ('" + _documentNo + "', '" + _lineNo + "', '" + _binCode + "', '" + _brand + "', '" + _itemNo + "', '" + _variantCode + "', '" + _description + "', '" + _description2 + "', '" + _quantity + "', '" + _qtyToReceive + "', '"+_weight+"', '"+_length+"', '"+_width+"', '"+_height+"')");
                }
                else
                {
                    smartDatabase.nonQuery("UPDATE receiptLine SET binCode = '" + _binCode + "', brand = '" + _brand + "', itemNo = '" + _itemNo + "', variantCode = '" + _variantCode + "', description = '" + _description + "', description2 = '" + _description2 + "', quantity = '" + _quantity + "', qtyToReceive = '" + _qtyToReceive + "', weight = '"+_weight+"', length = '"+_length+"', width = '"+_width+"', height = '"+_height+"' WHERE documentNo = '" + _documentNo + "' AND docLineNo = '" + _lineNo + "'");
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
                smartDatabase.nonQuery("DELETE FROM receiptLine WHERE documentNo = '" + _documentNo + "' AND docLineNo = '" + _lineNo + "'");

            }
            catch (SqlCeException e)
            {
                smartDatabase.ShowErrors(e);
            }

        }

        public static void deleteAll(SmartDatabase smartDatabase, string documentNo)
        {
            smartDatabase.nonQuery("DELETE FROM receiptLine WHERE documentNo = '" + documentNo + "'");

        }


        private void fromDataReader(SqlCeDataReader dataReader)
        {
            this._documentNo = (string)dataReader.GetValue(0);
            this._lineNo = dataReader.GetInt32(1);
            this._binCode = (string)dataReader.GetValue(2);
            this._brand = (string)dataReader.GetValue(3);
            this._itemNo = (string)dataReader.GetValue(4);
            this._variantCode = (string)dataReader.GetValue(5);
            this._description = (string)dataReader.GetValue(6);
            this._description2 = (string)dataReader.GetValue(7);
            this._quantity = float.Parse(dataReader.GetValue(8).ToString());
            this._qtyToReceive = float.Parse(dataReader.GetValue(9).ToString());
            this._weight = float.Parse(dataReader.GetValue(10).ToString());
            this._length = float.Parse(dataReader.GetValue(11).ToString());
            this._width = float.Parse(dataReader.GetValue(12).ToString());
            this._height = float.Parse(dataReader.GetValue(13).ToString());
               
        }

        private void fromDataRow(DataRow dataRow)
        {
            this._documentNo = (string)dataRow.ItemArray.GetValue(0);
            this._lineNo = int.Parse(dataRow.ItemArray.GetValue(1).ToString());
            this._binCode = (string)dataRow.ItemArray.GetValue(2);
            this._brand = (string)dataRow.ItemArray.GetValue(3);
            this._itemNo = (string)dataRow.ItemArray.GetValue(4);
            this._variantCode = (string)dataRow.ItemArray.GetValue(5);
            this._description = (string)dataRow.ItemArray.GetValue(6);
            this._description2 = (string)dataRow.ItemArray.GetValue(7);
            this._quantity = float.Parse(dataRow.ItemArray.GetValue(8).ToString());
            this._qtyToReceive = float.Parse(dataRow.ItemArray.GetValue(9).ToString());
            this._weight = float.Parse(dataRow.ItemArray.GetValue(10).ToString());
            this._length = float.Parse(dataRow.ItemArray.GetValue(11).ToString());
            this._width = float.Parse(dataRow.ItemArray.GetValue(12).ToString());
            this._height = float.Parse(dataRow.ItemArray.GetValue(13).ToString());

        }

        public static void fromDOM(XmlElement storeLinesElement, SmartDatabase smartDatabase)
        {
            XmlNodeList receiptLineNodeList = storeLinesElement.SelectNodes("receiptLine");

            int i = 0;
            while (i < receiptLineNodeList.Count)
            {
                XmlElement receiptLineElement = (XmlElement)receiptLineNodeList[i];

                DataReceiptLine receiptLine = new DataReceiptLine(smartDatabase);
                receiptLine.documentNo = receiptLineElement.GetAttribute("documentNo");
                receiptLine.lineNo = int.Parse(receiptLineElement.GetAttribute("lineNo"));

                if (receiptLineElement.SelectSingleNode("binCode").FirstChild != null)
                {
                    receiptLine.binCode = receiptLineElement.SelectSingleNode("binCode").FirstChild.Value;
                }
                if (receiptLineElement.SelectSingleNode("brand").FirstChild != null)
                {
                    receiptLine.brand = receiptLineElement.SelectSingleNode("brand").FirstChild.Value;
                }
                if (receiptLineElement.SelectSingleNode("itemNo").FirstChild != null)
                {
                    receiptLine.itemNo = receiptLineElement.SelectSingleNode("itemNo").FirstChild.Value;
                }
                if (receiptLineElement.SelectSingleNode("variantCode").FirstChild != null)
                {
                    receiptLine.variantCode = receiptLineElement.SelectSingleNode("variantCode").FirstChild.Value;
                }
                if (receiptLineElement.SelectSingleNode("description").FirstChild != null)
                {
                    receiptLine.description = receiptLineElement.SelectSingleNode("description").FirstChild.Value;
                }
                if (receiptLineElement.SelectSingleNode("description2").FirstChild != null)
                {
                    receiptLine.description2 = receiptLineElement.SelectSingleNode("description2").FirstChild.Value;
                }
                receiptLine.quantity = float.Parse(receiptLineElement.SelectSingleNode("quantity").FirstChild.Value);
                receiptLine.qtyToReceive = float.Parse(receiptLineElement.SelectSingleNode("qtyToReceive").FirstChild.Value);

                receiptLine.weight = float.Parse(receiptLineElement.SelectSingleNode("weight").FirstChild.Value);
                receiptLine.length = float.Parse(receiptLineElement.SelectSingleNode("length").FirstChild.Value);
                receiptLine.height = float.Parse(receiptLineElement.SelectSingleNode("height").FirstChild.Value);
                receiptLine.width = float.Parse(receiptLineElement.SelectSingleNode("width").FirstChild.Value);

                receiptLine.save();


                DataItemCrossReference.fromDOM2(receiptLineElement, smartDatabase, receiptLine);

                i++;
            }

        }

        public static DataSet getDataSet(SmartDatabase smartDatabase, string documentNo)
        {
            SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT documentNo, docLineNo, binCode, brand, itemNo, variantCode, description, description2, quantity, qtyToReceive, weight, length, width, height FROM receiptLine WHERE documentNo = '" + documentNo + "' ORDER BY itemNo");

            DataSet purchaseLineDataSet = new DataSet();
            adapter.Fill(purchaseLineDataSet, "receiptLine");
            adapter.Dispose();

            return purchaseLineDataSet;
        }

        public static DataReceiptLine getFirstLine(SmartDatabase smartDatabase, string documentNo, string binCode)
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT documentNo, docLineNo, binCode, brand, itemNo, variantCode, description, description2, quantity, qtyToReceive, weight, length, width, height FROM receiptLine WHERE documentNo = '" + documentNo + "' AND picked = 0 AND binCode = '" + binCode + "' ORDER BY binCode");

            DataReceiptLine dataReceiptLine = null;

            if (dataReader.Read())
            {
                dataReceiptLine = new DataReceiptLine(smartDatabase, dataReader);
            }
            dataReader.Close();

            return dataReceiptLine;
        }

        public static DataReceiptLine getFirstEmptyLine(SmartDatabase smartDatabase, string documentNo, string itemNo, string variantCode)
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT documentNo, docLineNo, binCode, brand, itemNo, variantCode, description, description2, totalQty, quantity, pickedQty, placeBinCode, picked, placed, totalCount FROM storeLine WHERE documentNo = '" + documentNo + "' AND picked = 0 AND itemNo = '" + itemNo + "' AND variantCode = '" + variantCode + "' AND binCode = '' ORDER BY binCode");

            DataReceiptLine dataReceiptLine = null;

            if (dataReader.Read())
            {
                dataReceiptLine = new DataReceiptLine(smartDatabase, dataReader);
            }
            dataReader.Close();

            return dataReceiptLine;
        }

        public static DataReceiptLine getFirstItemLine(SmartDatabase smartDatabase, string documentNo, string itemNo, string variantCode)
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT documentNo, docLineNo, binCode, brand, itemNo, variantCode, description, description2, quantity, qtyToReceive, weight, length, width, height FROM receiptLine WHERE documentNo = '" + documentNo + "' AND itemNo = '" + itemNo + "' AND variantCode = '" + variantCode + "' ORDER BY docLineNo");

            DataReceiptLine dataReceiptLine = null;

            if (dataReader.Read())
            {
                dataReceiptLine = new DataReceiptLine(smartDatabase, dataReader);
            }
            dataReader.Close();

            return dataReceiptLine;
        }

        public static DataReceiptLine getNextItemLine(SmartDatabase smartDatabase, string documentNo, string itemNo, string variantCode, int prevLineNo)
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT documentNo, docLineNo, binCode, brand, itemNo, variantCode, description, description2, quantity, qtyToReceive, weight, length, width, height FROM receiptLine WHERE documentNo = '" + documentNo + "' AND itemNo = '" + itemNo + "' AND variantCode = '" + variantCode + "' AND docLineNo > '"+prevLineNo+"' ORDER BY docLineNo");

            DataReceiptLine dataReceiptLine = null;

            if (dataReader.Read())
            {
                dataReceiptLine = new DataReceiptLine(smartDatabase, dataReader);
            }
            dataReader.Close();

            return dataReceiptLine;
        }

        public static int countItemLines(SmartDatabase smartDatabase, string documentNo, string itemNo, string variantCode)
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT COUNT(*) FROM receiptLine WHERE documentNo = '" + documentNo + "' AND itemNo = '" + itemNo + "' AND variantCode = '" + variantCode + "'");

            DataReceiptLine dataReceiptLine = null;

            int count = 0;

            if (dataReader.Read())
            {
                count = int.Parse(dataReader.GetValue(0).ToString());
            }
            dataReader.Close();

            return count;
        }

        public static bool containsEmptyBins(SmartDatabase smartDatabase, string documentNo)
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT documentNo FROM receiptLine WHERE documentNo = '" + documentNo + "' AND picked = 0 AND binCode = '' ORDER BY binCode");

            if (dataReader.Read())
            {
                dataReader.Close();
                return true;
            }
            dataReader.Close();

            return false;
        }



        public static int countLines(SmartDatabase smartDatabase, string documentNo)
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT COUNT(*) FROM receiptLine WHERE documentNo = '" + documentNo + "'");

            int count = 0;

            if (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0)) count = int.Parse(dataReader.GetValue(0).ToString());
            }
            dataReader.Close();

            return count;
        }

        #region ServiceArgument Members

        public XmlElement toDOM(XmlDocument xmlDocumentContext)
        {
            XmlElement docElement = xmlDocumentContext.CreateElement("document");

            XmlElement documentNoElement = xmlDocumentContext.CreateElement("documentNo");
            XmlText documentNoText = xmlDocumentContext.CreateTextNode(_documentNo);
            documentNoElement.AppendChild(documentNoText);

            docElement.AppendChild(documentNoElement);


            XmlElement lineElement = xmlDocumentContext.CreateElement("receiptLine");

            lineElement.SetAttribute("lineNo", _lineNo.ToString());
            lineElement.SetAttribute("qtyToReceive", _qtyToReceive.ToString());
            lineElement.SetAttribute("binCode", _binCode);
            lineElement.SetAttribute("weight", _weight.ToString());
            lineElement.SetAttribute("length", _length.ToString());
            lineElement.SetAttribute("width", _width.ToString());
            lineElement.SetAttribute("height", _height.ToString());


            docElement.AppendChild(lineElement);

            return docElement;
        }

        #endregion
    }

}
