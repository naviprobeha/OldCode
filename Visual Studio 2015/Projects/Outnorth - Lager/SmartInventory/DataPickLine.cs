using System;
using System.Data.SqlServerCe;
using System.Data;
using System.Xml;

namespace Navipro.SmartInventory
{
    /// <summary>
    /// Summary description for DataSalesLine.
    /// </summary>
    public class DataPickLine : ServiceArgument
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
        private int _action = 0;
        private int _inventory = 0;
        private bool _setQuantity = false;

        private string _usedEanCode = "";

        private SmartDatabase smartDatabase;

        public DataPickLine(SmartDatabase smartDatabase, SqlCeDataReader dataReader)
        {

            //
            // TODO: Add constructor logic here
            //
            this.smartDatabase = smartDatabase;
            fromDataReader(dataReader);
        }

        public DataPickLine(SmartDatabase smartDatabase, DataRow dataRow)
        {

            //
            // TODO: Add constructor logic here
            //
            this.smartDatabase = smartDatabase;
            fromDataRow(dataRow);
        }

        public DataPickLine(SmartDatabase smartDatabase)
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
        public int action { get { return _action; } set { _action = value; } }
        public int inventory { get { return _inventory; } set { _inventory = value; } }
        public string usedEanCode { get { return _usedEanCode; } set { _usedEanCode = value; } }


        public void save()
        {
            int pickedVal = 0;
            if (picked) pickedVal = 1;

            int placedVal = 0;
            if (placed) placedVal = 1;


            try
            {
                SqlCeDataReader dataReader;

                dataReader = smartDatabase.query("SELECT documentNo FROM pickLine WHERE documentNo = '" + _documentNo + "' AND docLineNo = '"+_lineNo+"'");

                if (!dataReader.Read())
                {
                    smartDatabase.nonQuery("INSERT INTO pickLine (documentNo, docLineNo, binCode, brand, itemNo, variantCode, description, description2, totalQty, quantity, pickedQty, placeBinCode, picked, placed, totalCount, action, inventory) VALUES ('" + _documentNo + "', '" + _lineNo + "', '" + _binCode + "', '" + _brand + "', '" + _itemNo + "', '" + _variantCode + "', '" + _description + "', '" + _description2 + "',  '" + totalQty + "', '" + quantity + "', '" + pickedQty + "', '" + _placeBinCode + "', '" + pickedVal + "', '" + placedVal + "', '"+_count+"', '"+_action+"', '"+_inventory+"')");
                }
                else
                {
                    smartDatabase.nonQuery("UPDATE pickLine SET binCode = '" + _binCode + "', brand = '" + _brand + "', itemNo = '" + _itemNo + "', variantCode = '" + _variantCode + "', description = '" + _description + "', description2 = '" + _description2 + "', totalQty = '" + totalQty + "', quantity = '" + _quantity + "', pickedQty = '" + _pickedQty + "', placeBinCode = '" + _placeBinCode + "', picked = '" + pickedVal + "', placed = '" + placedVal + "', totalCount = '"+_count+"', action = '"+_action+"', inventory = '"+_inventory+"' WHERE documentNo = '" + _documentNo + "' AND docLineNo = '" + _lineNo + "'");
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
                smartDatabase.nonQuery("DELETE FROM pickLine WHERE documentNo = '" + _documentNo + "' AND lineNo = '"+_lineNo+"'");

            }
            catch (SqlCeException e)
            {
                smartDatabase.ShowErrors(e);
            }

        }


        public string getEanCode()
        {
            DataItemCrossReference dataItemCrossRef = DataItemCrossReference.getEan(smartDatabase, this.documentNo, this.itemNo, this.variantCode);
            if (dataItemCrossRef != null)
            {
                return dataItemCrossRef.crossReferenceCode;
            }

            return "";

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
            this._action = dataReader.GetInt32(15);
            this._inventory = dataReader.GetInt32(16);
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
            this._action = int.Parse(dataRow.ItemArray.GetValue(15).ToString());
            this._inventory = int.Parse(dataRow.ItemArray.GetValue(16).ToString());

        }

        public static void fromDOM(XmlElement pickLinesElement, SmartDatabase smartDatabase)
        {
            smartDatabase.nonQuery("DELETE FROM pickLine");

            XmlNodeList pickLineNodeList = pickLinesElement.SelectNodes("pickLine");
            int i = 0;
            while (i < pickLineNodeList.Count)
            {
                XmlElement pickLineElement = (XmlElement)pickLineNodeList[i];

                DataPickLine pickLine = new DataPickLine(smartDatabase);
                pickLine.documentNo = pickLineElement.GetAttribute("documentNo");
                pickLine.lineNo = int.Parse(pickLineElement.GetAttribute("lineNo"));
                pickLine.count = int.Parse(pickLineElement.GetAttribute("count"));

                if (pickLineElement.SelectSingleNode("binCode").FirstChild != null) pickLine.binCode = pickLineElement.SelectSingleNode("binCode").FirstChild.Value;
                if (pickLineElement.SelectSingleNode("brand").FirstChild != null) pickLine.brand = pickLineElement.SelectSingleNode("brand").FirstChild.Value;
                if (pickLineElement.SelectSingleNode("itemNo").FirstChild != null) pickLine.itemNo = pickLineElement.SelectSingleNode("itemNo").FirstChild.Value;
                if (pickLineElement.SelectSingleNode("variantCode").FirstChild != null) pickLine.variantCode = pickLineElement.SelectSingleNode("variantCode").FirstChild.Value;
                if (pickLineElement.SelectSingleNode("description").FirstChild != null) pickLine.description = pickLineElement.SelectSingleNode("description").FirstChild.Value;
                if (pickLineElement.SelectSingleNode("description2").FirstChild != null) pickLine.description2 = pickLineElement.SelectSingleNode("description2").FirstChild.Value;
                if (pickLineElement.SelectSingleNode("totalQty").FirstChild != null) pickLine.totalQty = float.Parse(pickLineElement.SelectSingleNode("totalQty").FirstChild.Value);
                if (pickLineElement.SelectSingleNode("quantity").FirstChild != null) pickLine.quantity = float.Parse(pickLineElement.SelectSingleNode("quantity").FirstChild.Value);
                if (pickLineElement.SelectSingleNode("placeBinCode").FirstChild != null) pickLine.placeBinCode = pickLineElement.SelectSingleNode("placeBinCode").FirstChild.Value;
                if (pickLineElement.SelectSingleNode("inventory").FirstChild != null) pickLine.inventory = int.Parse(pickLineElement.SelectSingleNode("inventory").FirstChild.Value);

                string action = pickLineElement.SelectSingleNode("action").FirstChild.Value;

                if (action == "toPick")
                {
                    pickLine.action = 0;
                }
                if (action == "undo")
                {
                    pickLine.action = 1;
                }

                pickLine.save();

                DataItemCrossReference.fromDOM(pickLineElement, smartDatabase, pickLine);

                i++;
            }

        }

        public static DataSet getDataSet(SmartDatabase smartDatabase, string documentNo)
        {
            SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT documentNo, docLineNo, binCode, brand, itemNo, variantCode, description, description2, totalQty, quantity, pickedQty, placeBinCode, picked, placed, totalCount, action, inventory FROM pickLine WHERE documentNo = '"+documentNo+"' ORDER BY binCode");

            DataSet purchaseLineDataSet = new DataSet();
            adapter.Fill(purchaseLineDataSet, "pickLine");
            adapter.Dispose();

            return purchaseLineDataSet;
        }

        public static DataPickLine getFirstLine(SmartDatabase smartDatabase, string documentNo)
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT documentNo, docLineNo, binCode, brand, itemNo, variantCode, description, description2, totalQty, quantity, pickedQty, placeBinCode, picked, placed, totalCount, action, inventory FROM pickLine WHERE documentNo = '" + documentNo + "' AND picked = 0 ORDER BY binCode");

            DataPickLine dataPickLine = null;

            if (dataReader.Read())
            {
                dataPickLine = new DataPickLine(smartDatabase, dataReader);
            }
            dataReader.Close();

            return dataPickLine;
        }

        public static int countUnpickedLines(SmartDatabase smartDatabase, string documentNo)
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT COUNT(*) FROM pickLine WHERE documentNo = '" + documentNo + "' AND picked = 0");

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


            XmlElement lineElement = xmlDocumentContext.CreateElement("pickLine");
            lineElement.SetAttribute("lineNo", _lineNo.ToString());

            if (picked == true)
            {
                lineElement.SetAttribute("pickedQty", _pickedQty.ToString());
                lineElement.SetAttribute("action", _action.ToString());
            }

            docElement.AppendChild(lineElement);

            return docElement;
        }

        #endregion
    }

}
