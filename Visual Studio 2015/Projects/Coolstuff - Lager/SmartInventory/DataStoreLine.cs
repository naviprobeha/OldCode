using System;
using System.Data.SqlServerCe;
using System.Data;
using System.Xml;

namespace Navipro.SmartInventory
{
    /// <summary>
    /// Summary description for DataSalesLine.
    /// </summary>
    public class DataStoreLine : ServiceArgument
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
        private float _pickedQty = 0;
        private float _totalQty = 0;
        private float _undoQty = 0;
        private string _placeBinCode = "";
        private int _count;
        private bool _picked = false;
        private bool _placed = false;



        private SmartDatabase smartDatabase;

        public DataStoreLine(SmartDatabase smartDatabase, SqlCeDataReader dataReader)
        {

            //
            // TODO: Add constructor logic here
            //
            this.smartDatabase = smartDatabase;
            fromDataReader(dataReader);
        }

        public DataStoreLine(SmartDatabase smartDatabase, DataRow dataRow)
        {

            //
            // TODO: Add constructor logic here
            //
            this.smartDatabase = smartDatabase;
            fromDataRow(dataRow);
        }

        public DataStoreLine(SmartDatabase smartDatabase)
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
        public float pickedQty { get { return _pickedQty; } set { _pickedQty = value; } }
        public float undoQty { get { return _undoQty; } set { _undoQty = value; } }
        public float totalQty { get { return _totalQty; } set { _totalQty = value; } }
        public string placeBinCode { get { return _placeBinCode; } set { _placeBinCode = value; } }
        public int count { get { return _count; } set { _count = value; } }
        public bool picked { get { return _picked; } set { _picked = value; } }
        public bool placed { get { return _placed; } set { _placed = value; } }

        public void save()
        {
            int pickedVal = 0;
            if (picked) pickedVal = 1;

            int placedVal = 0;
            if (placed) placedVal = 1;

            try
            {
                SqlCeDataReader dataReader;

                dataReader = smartDatabase.query("SELECT documentNo FROM storeLine WHERE documentNo = '" + _documentNo + "' AND docLineNo = '" + _lineNo + "'");

                if (!dataReader.Read())
                {
                    smartDatabase.nonQuery("INSERT INTO storeLine (documentNo, docLineNo, binCode, brand, itemNo, variantCode, description, description2, totalQty, quantity, pickedQty, placeBinCode, picked, placed, totalCount) VALUES ('" + _documentNo + "', '" + _lineNo + "', '" + _binCode + "', '" + _brand + "', '" + _itemNo + "', '" + _variantCode + "', '" + _description + "', '" + _description2 + "',  '" + totalQty + "', '" + quantity + "', '" + pickedQty + "', '" + _placeBinCode + "', '" + pickedVal + "', '" + placedVal + "', '" + _count + "')");
                }
                else
                {
                    smartDatabase.nonQuery("UPDATE storeLine SET binCode = '" + _binCode + "', brand = '" + _brand + "', itemNo = '" + _itemNo + "', variantCode = '" + _variantCode + "', description = '" + _description + "', description2 = '" + _description2 + "', totalQty = '" + totalQty + "', quantity = '" + _quantity + "', pickedQty = '" + _pickedQty + "', placeBinCode = '" + _placeBinCode + "', picked = '" + pickedVal + "', placed = '" + placedVal + "', totalCount = '" + _count + "' WHERE documentNo = '" + _documentNo + "' AND docLineNo = '" + _lineNo + "'");
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
                smartDatabase.nonQuery("DELETE FROM storeLine WHERE documentNo = '" + _documentNo + "' AND docLineNo = '" + _lineNo + "'");

            }
            catch (SqlCeException e)
            {
                smartDatabase.ShowErrors(e);
            }

        }

        public static void deleteAll(SmartDatabase smartDatabase, string documentNo)
        {
            smartDatabase.nonQuery("DELETE FROM storeLine WHERE documentNo = '" + documentNo + "'");

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
            this._totalQty = float.Parse(dataReader.GetValue(8).ToString());
            this._quantity = float.Parse(dataReader.GetValue(9).ToString());
            this._pickedQty = float.Parse(dataReader.GetValue(10).ToString());
            this._placeBinCode = (string)dataReader.GetValue(11);
            if (dataReader.GetValue(12).ToString() == "1") _picked = true;
            if (dataReader.GetValue(13).ToString() == "1") _placed = true;
            this._count = dataReader.GetInt32(14);
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
            this._totalQty = float.Parse(dataRow.ItemArray.GetValue(8).ToString());
            this._quantity = float.Parse(dataRow.ItemArray.GetValue(9).ToString());
            this._pickedQty = float.Parse(dataRow.ItemArray.GetValue(10).ToString());
            this._placeBinCode = (string)dataRow.ItemArray.GetValue(11);
            if (dataRow.ItemArray.GetValue(12).ToString() == "1") _picked = true;
            if (dataRow.ItemArray.GetValue(13).ToString() == "1") _placed = true;
            this._count = int.Parse(dataRow.ItemArray.GetValue(14).ToString());

        }

        public static void fromDOM(XmlElement storeLinesElement, SmartDatabase smartDatabase)
        {
            XmlNodeList storeLineNodeList = storeLinesElement.SelectNodes("storeLine");
            int i = 0;
            while (i < storeLineNodeList.Count)
            {
                XmlElement storeLineElement = (XmlElement)storeLineNodeList[i];

                DataStoreLine storeLine = new DataStoreLine(smartDatabase);
                storeLine.documentNo = storeLineElement.GetAttribute("documentNo");
                storeLine.lineNo = int.Parse(storeLineElement.GetAttribute("lineNo"));

                if (storeLineElement.SelectSingleNode("binCode").FirstChild != null)
                {
                    storeLine.binCode = storeLineElement.SelectSingleNode("binCode").FirstChild.Value;
                }
                if (storeLineElement.SelectSingleNode("brand").FirstChild != null)
                {
                    storeLine.brand = storeLineElement.SelectSingleNode("brand").FirstChild.Value;
                }
                if (storeLineElement.SelectSingleNode("itemNo").FirstChild != null)
                {
                    storeLine.itemNo = storeLineElement.SelectSingleNode("itemNo").FirstChild.Value;
                }
                if (storeLineElement.SelectSingleNode("variantCode").FirstChild != null)
                {
                    storeLine.variantCode = storeLineElement.SelectSingleNode("variantCode").FirstChild.Value;
                }
                if (storeLineElement.SelectSingleNode("description").FirstChild != null)
                {
                    storeLine.description = storeLineElement.SelectSingleNode("description").FirstChild.Value;
                }
                if (storeLineElement.SelectSingleNode("description2").FirstChild != null)
                {
                    storeLine.description2 = storeLineElement.SelectSingleNode("description2").FirstChild.Value;
                }
                storeLine.totalQty = float.Parse(storeLineElement.SelectSingleNode("totalQty").FirstChild.Value);
                storeLine.quantity = float.Parse(storeLineElement.SelectSingleNode("quantity").FirstChild.Value);

                if (storeLineElement.SelectSingleNode("placeBinCode").FirstChild != null)
                {
                    storeLine.placeBinCode = storeLineElement.SelectSingleNode("placeBinCode").FirstChild.Value;
                }
                storeLine.count = int.Parse(storeLineElement.GetAttribute("count"));
                storeLine.save();


                DataItemCrossReference.fromDOM(storeLineElement, smartDatabase, storeLine);

                i++;
            }

        }

        public static DataSet getDataSet(SmartDatabase smartDatabase, string documentNo)
        {
            SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT documentNo, docLineNo, binCode, brand, itemNo, variantCode, description, description2, totalQty, quantity, pickedQty, placeBinCode, picked, placed, totalCount FROM storeLine WHERE documentNo = '" + documentNo + "' ORDER BY binCode DESC");

            DataSet purchaseLineDataSet = new DataSet();
            adapter.Fill(purchaseLineDataSet, "storeLine");
            adapter.Dispose();

            return purchaseLineDataSet;
        }

        public static DataStoreLine getFirstLine(SmartDatabase smartDatabase, string documentNo, string binCode)
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT documentNo, docLineNo, binCode, brand, itemNo, variantCode, description, description2, totalQty, quantity, pickedQty, placeBinCode, picked, placed, totalCount FROM storeLine WHERE documentNo = '" + documentNo + "' AND picked = 0 AND binCode = '"+binCode+"' ORDER BY binCode");

            DataStoreLine dataStoreLine = null;

            if (dataReader.Read())
            {
                dataStoreLine = new DataStoreLine(smartDatabase, dataReader);
            }
            dataReader.Close();

            return dataStoreLine;
        }

        public static DataStoreLine getFirstEmptyLine(SmartDatabase smartDatabase, string documentNo, string itemNo, string variantCode)
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT documentNo, docLineNo, binCode, brand, itemNo, variantCode, description, description2, totalQty, quantity, pickedQty, placeBinCode, picked, placed, totalCount FROM storeLine WHERE documentNo = '" + documentNo + "' AND picked = 0 AND itemNo = '" + itemNo + "' AND variantCode = '"+variantCode+"' AND binCode = '' ORDER BY binCode");

            DataStoreLine dataStoreLine = null;

            if (dataReader.Read())
            {
                dataStoreLine = new DataStoreLine(smartDatabase, dataReader);
            }
            dataReader.Close();

            return dataStoreLine;
        }

        public static DataStoreLine getFirstItemLine(SmartDatabase smartDatabase, string documentNo, string itemNo, string variantCode)
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT documentNo, docLineNo, binCode, brand, itemNo, variantCode, description, description2, totalQty, quantity, pickedQty, placeBinCode, picked, placed, totalCount FROM storeLine WHERE documentNo = '" + documentNo + "' AND picked = 0 AND itemNo = '" + itemNo + "' AND variantCode = '" + variantCode + "' ORDER BY binCode");

            DataStoreLine dataStoreLine = null;

            if (dataReader.Read())
            {
                dataStoreLine = new DataStoreLine(smartDatabase, dataReader);
            }
            dataReader.Close();

            return dataStoreLine;
        }

        public static bool containsEmptyBins(SmartDatabase smartDatabase, string documentNo)
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT documentNo FROM storeLine WHERE documentNo = '" + documentNo + "' AND picked = 0 AND binCode = '' ORDER BY binCode");

            if (dataReader.Read())
            {
                dataReader.Close();
                return true;
            }
            dataReader.Close();

            return false;
        }



        public static int countUnhandlesLines(SmartDatabase smartDatabase, string documentNo)
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT COUNT(*) FROM storeLine WHERE documentNo = '" + documentNo + "' AND picked = 0");

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


            XmlElement lineElement = xmlDocumentContext.CreateElement("storeLine");

            lineElement.SetAttribute("lineNo", _lineNo.ToString());
            lineElement.SetAttribute("pickedQty", _pickedQty.ToString());
            lineElement.SetAttribute("binCode", _binCode);

            docElement.AppendChild(lineElement);

            return docElement;
        }

        #endregion
    }

}
