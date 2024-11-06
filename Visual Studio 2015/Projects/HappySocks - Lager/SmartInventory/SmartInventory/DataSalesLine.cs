using System;
using System.Data.SqlServerCe;
using System.Data;
using System.Xml;

namespace Navipro.SmartInventory
{
    /// <summary>
    /// Summary description for DataSalesLine.
    /// </summary>
    public class DataSalesLine
    {
        private int _entryNo;
        private int _documentType;
        private string _documentNo;
        private int _lineNo;
        private string _itemNo;
        private string _variantCode;
        private string _description;
        private string _unitOfMeasure;
        private float _quantity;
        private float _quantityShipped;
        private float _quantityToShip;
        private string _ean;
        private string _carton = "";
        private int _originalLineNo;

        private SmartDatabase smartDatabase;

        public DataSalesLine(SmartDatabase smartDatabase, SqlCeDataReader dataReader)
        {

            //
            // TODO: Add constructor logic here
            //
            this.smartDatabase = smartDatabase;
            fromDataReader(dataReader);
        }

        public DataSalesLine(SmartDatabase smartDatabase, DataRow dataRow)
        {

            //
            // TODO: Add constructor logic here
            //
            this.smartDatabase = smartDatabase;
            fromDataRow(dataRow);
        }

        public DataSalesLine(SmartDatabase smartDatabase)
        {

            //
            // TODO: Add constructor logic here
            //
            this.smartDatabase = smartDatabase;

        }

        public int entryNo { get { return _entryNo; } set { _entryNo = value; } }
        public int documentType { get { return _documentType; } set { _documentType = value; } }
        public string documentNo { get { return _documentNo; } set { _documentNo = value; } }
        public int lineNo { get { return _lineNo; } set { _lineNo = value; } }
        public string itemNo { get { return _itemNo; } set { _itemNo = value; } }
        public string variantCode { get { return _variantCode; } set { _variantCode = value; } }
        public string description { get { return _description; } set { _description = value; } }
        public string unitOfMeasure { get { return _unitOfMeasure; } set { _unitOfMeasure = value; } }
        public float quantity { get { return _quantity; } set { _quantity = value; } }
        public float quantityShipped { get { return _quantityShipped; } set { _quantityShipped = value; } }
        public float quantityToShip { get { return _quantityToShip; } set { _quantityToShip = value; } }
        public string ean { get { return _ean; } set { _ean = value; } }
        public string carton { get { return _carton; } set { _carton = value; } }
        public int originalLineNo { get { return _originalLineNo; } set { _originalLineNo = value; } }



        public void save()
        {
            try
            {
                SqlCeDataReader dataReader;

                dataReader = smartDatabase.query("SELECT entryNo FROM salesLine WHERE entryNo = '" + _entryNo + "'");

                if (!dataReader.Read())
                {
                    smartDatabase.nonQuery("INSERT INTO salesLine (documentType, documentNo, orderLineNo, itemNo, variantCode, description, unitOfMeasure, quantity, quantityShipped, quantityToShip, ean, carton, originalLineNo) VALUES ('"+_documentType+"', '" + _documentNo + "', '" + _lineNo + "', '" + _itemNo + "', '" + _variantCode + "', '" + _description + "', '" + _unitOfMeasure + "', '" + _quantity.ToString().Replace(",", ".") + "', '" + _quantityShipped.ToString().Replace(",", ".") + "', '" + _quantityToShip.ToString().Replace(",", ".") + "', '" + _ean + "', '"+_carton+"', '"+_originalLineNo+"')");
                }
                else
                {
                    smartDatabase.nonQuery("UPDATE salesLine SET documentType = '"+_documentType+"', documentNo = '" + _documentNo + "', orderLineNo = '" + _lineNo + "', itemNo = '" + _itemNo + "', variantCode = '" + _variantCode + "', description = '" + _description + "', unitOfMeasure = '" + _unitOfMeasure + "', quantity = '" + _quantity.ToString().Replace(",", ".") + "', quantityShipped = '" + _quantityShipped.ToString().Replace(",", ".") + "', quantityToShip = '" + _quantityToShip.ToString().Replace(",", ".") + "', ean = '" + _ean + "', carton = '"+_carton+"', originalLineNo = '"+_originalLineNo+"' WHERE entryNo = '" + _entryNo + "'");
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
                smartDatabase.nonQuery("DELETE FROM salesLine WHERE entryNo = '" + _entryNo + "'");

            }
            catch (SqlCeException e)
            {
                smartDatabase.ShowErrors(e);
            }

        }


        private void fromDataReader(SqlCeDataReader dataReader)
        {
            this._entryNo = dataReader.GetInt32(0);
            this.documentType = dataReader.GetInt32(1);
            this._documentNo = (string)dataReader.GetValue(2);
            this._lineNo = dataReader.GetInt32(3);
            this._itemNo = (string)dataReader.GetValue(4);
            this._variantCode = (string)dataReader.GetValue(5);
            this._description = (string)dataReader.GetValue(6);
            this._unitOfMeasure = (string)dataReader.GetValue(7);
            this._quantity = float.Parse(dataReader.GetValue(8).ToString());
            this._quantityShipped = float.Parse(dataReader.GetValue(9).ToString());
            this._quantityToShip = float.Parse(dataReader.GetValue(10).ToString());
            this._ean = (string)dataReader.GetValue(11);
            this._carton = (string)dataReader.GetValue(12);
            this._originalLineNo = dataReader.GetInt32(13);
        }

        private void fromDataRow(DataRow dataRow)
        {
            this._entryNo = int.Parse(dataRow.ItemArray.GetValue(0).ToString());
            this._documentType = int.Parse(dataRow.ItemArray.GetValue(1).ToString());
            this._documentNo = dataRow.ItemArray.GetValue(2).ToString();
            this._lineNo = int.Parse(dataRow.ItemArray.GetValue(3).ToString());
            this._itemNo = dataRow.ItemArray.GetValue(4).ToString();
            this._variantCode = dataRow.ItemArray.GetValue(5).ToString();
            this._description = dataRow.ItemArray.GetValue(6).ToString();
            this._unitOfMeasure = dataRow.ItemArray.GetValue(7).ToString();
            this._quantity = float.Parse(dataRow.ItemArray.GetValue(8).ToString());
            this._quantityShipped = float.Parse(dataRow.ItemArray.GetValue(9).ToString());
            this._quantityToShip = float.Parse(dataRow.ItemArray.GetValue(10).ToString());
            this._ean = dataRow.ItemArray.GetValue(11).ToString();
            this._carton = dataRow.ItemArray.GetValue(12).ToString();
            this._originalLineNo = int.Parse(dataRow.ItemArray.GetValue(13).ToString());
        }

        public static void fromDOM(XmlElement orderElement, SmartDatabase smartDatabase)
        {
            int documentType = int.Parse(orderElement.GetAttribute("documentType"));
            string documentNo = orderElement.GetAttribute("documentNo");
            
            delete(smartDatabase, documentType, documentNo);


            XmlNodeList linesNodeList = orderElement.SelectNodes("lines/line");
            int i = 0;
            while (i < linesNodeList.Count)
            {
                XmlElement lineElement = (XmlElement)linesNodeList[i];
                int lineNo = int.Parse(lineElement.GetAttribute("lineNo"));

                DataSalesLine dataSalesLine = new DataSalesLine(smartDatabase);
                dataSalesLine.documentType = documentType;
                dataSalesLine.documentNo = documentNo;
                dataSalesLine.lineNo = lineNo;

                if (lineElement.SelectSingleNode("no").FirstChild != null) dataSalesLine.itemNo = lineElement.SelectSingleNode("no").FirstChild.Value;
                if (lineElement.SelectSingleNode("variantCode").FirstChild != null) dataSalesLine.variantCode = lineElement.SelectSingleNode("variantCode").FirstChild.Value;
                if (lineElement.SelectSingleNode("description").FirstChild != null) dataSalesLine.description = lineElement.SelectSingleNode("description").FirstChild.Value;
                if (lineElement.SelectSingleNode("unitOfMeasureCode").FirstChild != null) dataSalesLine.unitOfMeasure = lineElement.SelectSingleNode("unitOfMeasureCode").FirstChild.Value;
                if (lineElement.SelectSingleNode("quantity").FirstChild != null) dataSalesLine.quantity = float.Parse(lineElement.SelectSingleNode("quantity").FirstChild.Value.Replace(",", "."));
                if (lineElement.SelectSingleNode("quantityShipped").FirstChild != null) dataSalesLine.quantityShipped = float.Parse(lineElement.SelectSingleNode("quantityShipped").FirstChild.Value.Replace(",", "."));
                if (lineElement.SelectSingleNode("ean").FirstChild != null) dataSalesLine.ean = lineElement.SelectSingleNode("ean").FirstChild.Value;

                if (!DataSalesLineCarton.cartonsExists(smartDatabase, documentType, documentNo))
                {
                    XmlNodeList xmlCartonNodeList = lineElement.SelectNodes("cartons/carton");
                    int j = 0;
                    while (j < xmlCartonNodeList.Count)
                    {
                        XmlElement cartonElement = (XmlElement)xmlCartonNodeList[j];
                        string cartonNo = cartonElement.GetAttribute("no");
                        float splitOnQty = float.Parse(cartonElement.GetAttribute("splitOnQty"));

                        DataSalesLineCarton.createCarton(smartDatabase, documentType, documentNo, lineNo, cartonNo, splitOnQty);

                        j++;
                    }
                }

                dataSalesLine.quantityToShip = 0;

                DataScanLine dataScanLine = DataScanLine.getLineFromOrderLine(smartDatabase, 1, documentType, documentNo, lineNo);
                if (dataScanLine != null)
                {
                    dataSalesLine.quantityToShip = dataScanLine.quantity;
                    dataSalesLine.carton = dataScanLine.cartonNo;
                }

                dataSalesLine.save();



                i++;
            }
        }

        public static DataSet getDataSet(SmartDatabase smartDatabase, int documentType, string documentNo)
        {
            SqlCeDataAdapter purchaseLineAdapter = smartDatabase.dataAdapterQuery("SELECT entryNo, documentType, documentNo, orderLineNo, itemNo, variantCode, description, unitOfMeasure, quantity, quantityShipped, quantityToShip, ean, carton, originalLineNo FROM salesLine WHERE documentType = '"+documentType+"' AND documentNo = '" + documentNo + "' ORDER BY itemNo");

            DataSet purchaseLineDataSet = new DataSet();
            purchaseLineAdapter.Fill(purchaseLineDataSet, "salesLine");
            purchaseLineAdapter.Dispose();

            return purchaseLineDataSet;
        }

        public static bool checkLineExists(SmartDatabase smartDatabase, int documentType, string documentNo, string ean)
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT entryNo, documentType, documentNo, orderLineNo, itemNo, variantCode, description, unitOfMeasure, quantity, quantityShipped, quantityToShip, ean, carton, originalLineNo FROM salesLine WHERE documentType = '" + documentType + "' AND documentNo = '" + documentNo + "' AND ean LIKE '%" + ean + "'%");

            bool exists = false;

            if (dataReader.Read())
            {
                exists = true;
            }
            dataReader.Close();
            dataReader.Dispose();

            return exists;
        }

        public static DataSalesLine getLineFromEan(SmartDatabase smartDatabase, int documentType, string documentNo, string ean)
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT entryNo, documentType, documentNo, orderLineNo, itemNo, variantCode, description, unitOfMeasure, quantity, quantityShipped, quantityToShip, ean, carton, originalLineNo FROM salesLine WHERE documentType = '" + documentType + "' AND documentNo = '" + documentNo + "' AND ean LIKE '%" + ean + "%'");

            DataSalesLine dataSalesLine = null;

            if (dataReader.Read())
            {
                dataSalesLine = new DataSalesLine(smartDatabase, dataReader);
            }
            dataReader.Close();
            dataReader.Dispose();

            return dataSalesLine;
        }

        public static DataSalesLine getFirstLine(SmartDatabase smartDatabase, int documentType, string documentNo)
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT TOP 1 entryNo, documentType, documentNo, orderLineNo, itemNo, variantCode, description, unitOfMeasure, quantity, quantityShipped, quantityToShip, ean, carton, originalLineNo FROM salesLine WHERE documentType = '"+documentType+"' AND documentNo = '"+documentNo+"'");

            DataSalesLine dataSalesLine = null;

            if (dataReader.Read())
            {
                dataSalesLine = new DataSalesLine(smartDatabase, dataReader);
            }
            dataReader.Close();
            dataReader.Dispose();

            return dataSalesLine;
        }

        public static DataSalesLine getFirstUnPickedLine(SmartDatabase smartDatabase, int documentType, string documentNo)
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT entryNo, documentType, documentNo, orderLineNo, itemNo, variantCode, description, unitOfMeasure, quantity, quantityShipped, quantityToShip, ean, carton, originalLineNo FROM salesLine WHERE documentType = '" + documentType + "' AND documentNo = '" + documentNo + "' AND quantityToShip = 0");

            DataSalesLine dataSalesLine = null;

            if (dataReader.Read())
            {
                dataSalesLine = new DataSalesLine(smartDatabase, dataReader);
            }
            dataReader.Close();
            dataReader.Dispose();

            return dataSalesLine;
        }

        public static int getLastCarton(SmartDatabase smartDatabase, int documentType, string documentNo)
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT carton FROM salesLine WHERE documentType = '" + documentType + "' AND documentNo = '" + documentNo + "' ORDER BY carton DESC");

            int carton = 0;

            if (dataReader.Read())
            {
                try
                {
                    carton = int.Parse(dataReader.GetValue(0).ToString());
                }
                catch (Exception) { }
            }
            dataReader.Close();
            dataReader.Dispose();

            return carton;
        }

        public static DataSalesLine getLineFromEntryNo(SmartDatabase smartDatabase, int documentType, string documentNo, int entryNo)
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT entryNo, documentType, documentNo, orderLineNo, itemNo, variantCode, description, unitOfMeasure, quantity, quantityShipped, quantityToShip, ean, carton, originalLineNo FROM salesLine WHERE documentType = '"+documentType+"' AND documentNo = '"+documentNo+"' AND entryNo = '" + entryNo + "'");

            DataSalesLine dataSalesLine = null;

            if (dataReader.Read())
            {
                dataSalesLine = new DataSalesLine(smartDatabase, dataReader);
            }
            dataReader.Close();
            dataReader.Dispose();

            return dataSalesLine;
        }
        public static DataSalesLine getAvailableLineFromEan(SmartDatabase smartDatabase, int documentType, string documentNo, string ean)
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT entryNo, documentType, documentNo, orderLineNo, itemNo, variantCode, description, unitOfMeasure, quantity, quantityShipped, quantityToShip, ean, carton, originalLineNo FROM salesLine WHERE documentType = '" + documentType + "' AND documentNo = '" + documentNo + "' AND ean LIKE '%" + ean + "%' AND ((quantityShipped+quantityToShip) < quantity)");

            DataSalesLine dataSalesLine = null;

            if (dataReader.Read())
            {
                dataSalesLine = new DataSalesLine(smartDatabase, dataReader);
            }
            dataReader.Close();
            dataReader.Dispose();

            return dataSalesLine;
        }

        public static DataSalesLine getNextLine(SmartDatabase smartDatabase, DataSalesLine currentSalesLine)
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT entryNo, documentType, documentNo, orderLineNo, itemNo, variantCode, description, unitOfMeasure, quantity, quantityShipped, quantityToShip, ean, carton, originalLineNo FROM salesLine WHERE documentType = '"+currentSalesLine.documentType+"' AND documentNo = '"+currentSalesLine.documentNo+"' AND entryNo > '"+currentSalesLine.entryNo+"'");

            DataSalesLine dataSalesLine = null;

            if (dataReader.Read())
            {               
                dataSalesLine = new DataSalesLine(smartDatabase, dataReader);
            }

            dataReader.Close();
            dataReader.Dispose();

            return dataSalesLine;
        }

        public static DataSalesLine getPrevLine(SmartDatabase smartDatabase, DataSalesLine currentSalesLine)
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT entryNo, documentType, documentNo, orderLineNo, itemNo, variantCode, description, unitOfMeasure, quantity, quantityShipped, quantityToShip, ean, carton, originalLineNo FROM salesLine WHERE documentType = '" + currentSalesLine.documentType + "' AND documentNo = '" + currentSalesLine.documentNo + "' AND entryNo < '" + currentSalesLine.entryNo + "' ORDER BY entryNo DESC");

            DataSalesLine dataSalesLine = null;

            if (dataReader.Read())
            {
                dataSalesLine = new DataSalesLine(smartDatabase, dataReader);
            }

            dataReader.Close();
            dataReader.Dispose();

            return dataSalesLine;
        }

        public static void delete(SmartDatabase smartDatabase, int documentType, string documentNo)
        {
            smartDatabase.nonQuery("DELETE FROM salesLine WHERE documentType = '" + documentType + "' AND documentNo = '" + documentNo + "'");
        }

        public static int sumQuantity(SmartDatabase smartDatabase, int documentType, string documentNo)
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT SUM(quantity) FROM salesLine WHERE documentType = '" + documentType + "' AND documentNo = '" + documentNo + "'");

            int qty = 0;

            if (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0)) qty = int.Parse(dataReader.GetValue(0).ToString());
            }

            dataReader.Close();
            dataReader.Dispose();

            return qty;
        }

        public static int sumQuantityToShip(SmartDatabase smartDatabase, int documentType, string documentNo)
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT SUM(quantityToShip) FROM salesLine WHERE documentType = '" + documentType + "' AND documentNo = '" + documentNo + "'");

            int qty = 0;

            if (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0)) qty = int.Parse(dataReader.GetValue(0).ToString());
            }

            dataReader.Close();
            dataReader.Dispose();

            return qty;
        }

    }

}
