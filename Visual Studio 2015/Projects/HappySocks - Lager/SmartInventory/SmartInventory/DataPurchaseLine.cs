using System;
using System.Data.SqlServerCe;
using System.Data;
using System.Xml;

namespace Navipro.SmartInventory
{
	/// <summary>
	/// Summary description for DataSalesLine.
	/// </summary>
	public class DataPurchaseLine
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
		private float _quantityReceived;
		private float _quantityToReceive;
        private string _ean;


		private SmartDatabase smartDatabase;

		public DataPurchaseLine(SmartDatabase smartDatabase, SqlCeDataReader dataReader)
		{

			//
			// TODO: Add constructor logic here
			//
			this.smartDatabase = smartDatabase;
            fromDataReader(dataReader);
		}

        public DataPurchaseLine(SmartDatabase smartDatabase, DataRow dataRow)
        {

            //
            // TODO: Add constructor logic here
            //
            this.smartDatabase = smartDatabase;
            fromDataRow(dataRow);
        }

        public DataPurchaseLine(SmartDatabase smartDatabase)
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
        public float quantityReceived { get { return _quantityReceived; } set { _quantityReceived = value; } }
        public float quantityToReceive { get { return _quantityToReceive; } set { _quantityToReceive = value; } }
        public string ean { get { return _ean; } set { _ean = value; } }




		public void save()
		{
			try
			{
				SqlCeDataReader dataReader;

				dataReader = smartDatabase.query("SELECT entryNo FROM purchaseLine WHERE entryNo = '"+_entryNo+"'");

				if (!dataReader.Read())
				{
                    smartDatabase.nonQuery("INSERT INTO purchaseLine (documentType, documentNo, orderLineNo, itemNo, variantCode, description, unitOfMeasure, quantity, quantityReceived, quantityToReceive, ean) VALUES ('"+_documentType+"', '" + _documentNo + "', '" + _lineNo + "', '" + _itemNo + "', '" + _variantCode + "', '" + _description + "', '" + _unitOfMeasure + "', '" + _quantity.ToString().Replace(",", ".") + "', '" + _quantityReceived.ToString().Replace(",", ".") + "', '" + _quantityToReceive.ToString().Replace(",", ".") + "', '"+_ean+"')");
				}
				else
				{
                    smartDatabase.nonQuery("UPDATE purchaseLine SET documentType = '"+documentType+"', documentNo = '" + _documentNo + "', orderLineNo = '" + _lineNo + "', itemNo = '" + _itemNo + "', variantCode = '" + _variantCode + "', description = '" + _description + "', unitOfMeasure = '" + _unitOfMeasure + "', quantity = '" + _quantity.ToString().Replace(",", ".") + "', quantityReceived = '" + _quantityReceived.ToString().Replace(",", ".") + "', quantityToReceive = '" + _quantityToReceive.ToString().Replace(",", ".") + "', ean = '" + _ean + "' WHERE entryNo = '" + _entryNo + "'");
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
				smartDatabase.nonQuery("DELETE FROM purchaseLine WHERE entryNo = '"+_entryNo+"'");

			}
			catch (SqlCeException e) 
			{
				smartDatabase.ShowErrors(e);
			}

		}


        private void fromDataReader(SqlCeDataReader dataReader)
        {
            this._entryNo = dataReader.GetInt32(0);
            this._documentType = dataReader.GetInt32(1);
            this._documentNo = (string)dataReader.GetValue(2);
            this._lineNo = dataReader.GetInt32(3);
            this._itemNo = (string)dataReader.GetValue(4);
            this._variantCode = (string)dataReader.GetValue(5);
            this._description = (string)dataReader.GetValue(6);
            this._unitOfMeasure = (string)dataReader.GetValue(7);
            this._quantity = float.Parse(dataReader.GetValue(8).ToString());
            this._quantityReceived = float.Parse(dataReader.GetValue(9).ToString());
            this._quantityToReceive = float.Parse(dataReader.GetValue(10).ToString());
            this._ean = (string)dataReader.GetValue(11);
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
            this._quantityReceived = float.Parse(dataRow.ItemArray.GetValue(9).ToString());
            this._quantityToReceive = float.Parse(dataRow.ItemArray.GetValue(10).ToString());
            this._ean = dataRow.ItemArray.GetValue(11).ToString();
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

                string itemNo = lineElement.SelectSingleNode("no").FirstChild.Value;
                string variantCode = lineElement.SelectSingleNode("variantCode").FirstChild.Value;
                string description = lineElement.SelectSingleNode("description").FirstChild.Value;
                string unitOfMeasure = lineElement.SelectSingleNode("unitOfMeasureCode").FirstChild.Value;
                float quantity = float.Parse(lineElement.SelectSingleNode("quantity").FirstChild.Value.Replace(",", "."));
                float quantityReceived = float.Parse(lineElement.SelectSingleNode("quantityReceived").FirstChild.Value.Replace(",", "."));
                float quantityToReceive = float.Parse(lineElement.SelectSingleNode("quantityToReceive").FirstChild.Value.Replace(",", "."));
                string ean = lineElement.SelectSingleNode("ean").FirstChild.Value;

                
                DataPurchaseLine dataPurchaseLine = new DataPurchaseLine(smartDatabase);
                dataPurchaseLine.documentType = documentType;
                dataPurchaseLine.documentNo = documentNo;
                dataPurchaseLine.lineNo = lineNo;
                dataPurchaseLine.itemNo = itemNo;
                dataPurchaseLine.variantCode = variantCode;
                dataPurchaseLine.description = description;
                dataPurchaseLine.unitOfMeasure = unitOfMeasure;
                dataPurchaseLine.quantity = quantity;
                dataPurchaseLine.quantityReceived = quantityReceived;
                dataPurchaseLine.quantityToReceive = 0;
                dataPurchaseLine.ean = ean;

                DataScanLine dataScanLine = DataScanLine.getLineFromOrderLine(smartDatabase, 0, documentType, documentNo, lineNo);
                if (dataScanLine != null)
                {
                    dataPurchaseLine.quantityToReceive = dataScanLine.quantity;
                }
                
                dataPurchaseLine.save();



                i++;
            }
        }

        public static DataSet getDataSet(SmartDatabase smartDatabase, int documentType, string documentNo)
        {
            SqlCeDataAdapter purchaseLineAdapter = smartDatabase.dataAdapterQuery("SELECT entryNo, documentType, documentNo, orderLineNo, itemNo, variantCode, description, unitOfMeasure, quantity, quantityReceived, quantityToReceive, ean FROM purchaseLine WHERE documentType = '"+documentType+"' AND documentNo = '" + documentNo + "' ORDER BY itemNo");

            DataSet purchaseLineDataSet = new DataSet();
            purchaseLineAdapter.Fill(purchaseLineDataSet, "purchaseLine");
            purchaseLineAdapter.Dispose();

            return purchaseLineDataSet;
        }

        public static int getDocumentType(SmartDatabase smartDatabase, string documentNo)
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT documentType FROM purchaseLine WHERE documentNo = '" + documentNo + "'");

            int documentType = 0;

            if (dataReader.Read())
            {
                documentType = dataReader.GetInt32(0);
            }
            dataReader.Close();
            dataReader.Dispose();

            return documentType;
        }

        public static bool checkLineExists(SmartDatabase smartDatabase, int documentType, string documentNo, string ean)
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT entryNo, documentType, documentNo, orderLineNo, itemNo, variantCode, description, unitOfMeasure, quantity, quantityReceived, quantityToReceive, ean FROM purchaseLine WHERE documentType = '" + documentType + "' AND documentNo = '" + documentNo + "' AND ean = '" + ean + "'");

            bool exists = false;

            if (dataReader.Read())
            {
                exists = true;
            }
            dataReader.Close();
            dataReader.Dispose();

            return exists;
        }

        public static DataPurchaseLine getLineFromEan(SmartDatabase smartDatabase, int documentType, string documentNo, string ean)
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT entryNo, documentType, documentNo, orderLineNo, itemNo, variantCode, description, unitOfMeasure, quantity, quantityReceived, quantityToReceive, ean FROM purchaseLine WHERE documentType = '" + documentType + "' AND documentNo = '" + documentNo + "' AND ean = '" + ean + "'");

            DataPurchaseLine dataPurchaseLine = null;

            if (dataReader.Read())
            {
                dataPurchaseLine = new DataPurchaseLine(smartDatabase, dataReader);
            }
            dataReader.Close();
            dataReader.Dispose();

            return dataPurchaseLine;
        }

        public static DataPurchaseLine getLineFromEntryNo(SmartDatabase smartDatabase, int entryNo)
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT entryNo, documentType, documentNo, orderLineNo, itemNo, variantCode, description, unitOfMeasure, quantity, quantityReceived, quantityToReceive, ean FROM purchaseLine WHERE entryNo = '" + entryNo + "'");

            DataPurchaseLine dataPurchaseLine = null;

            if (dataReader.Read())
            {
                dataPurchaseLine = new DataPurchaseLine(smartDatabase, dataReader);
            }
            dataReader.Close();
            dataReader.Dispose();

            return dataPurchaseLine;
        }
        public static DataPurchaseLine getAvailableLineFromEan(SmartDatabase smartDatabase, int documentType, string documentNo, string ean)
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT entryNo, documentType, documentNo, orderLineNo, itemNo, variantCode, description, unitOfMeasure, quantity, quantityReceived, quantityToReceive, ean FROM purchaseLine WHERE documentType = '" + documentType + "' AND documentNo = '" + documentNo + "' AND ean = '" + ean + "' AND ((quantityReceived+quantityToReceive) < quantity)");

            DataPurchaseLine dataPurchaseLine = null;

            if (dataReader.Read())
            {
                dataPurchaseLine = new DataPurchaseLine(smartDatabase, dataReader);
            }
            dataReader.Close();
            dataReader.Dispose();

            return dataPurchaseLine;
        }

        public static void delete(SmartDatabase smartDatabase, int documentType, string documentNo)
        {
            smartDatabase.nonQuery("DELETE FROM purchaseLine WHERE documentType = '"+documentType+"' AND documentNo = '" + documentNo + "'");
        }

        public static int sumQuantity(SmartDatabase smartDatabase, int documentType, string documentNo)
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT SUM(quantity) FROM purchaseLine WHERE documentType = '" + documentType + "' AND documentNo = '" + documentNo + "'");

            int qty = 0;

            if (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0)) qty = int.Parse(dataReader.GetValue(0).ToString());
            }

            dataReader.Close();
            dataReader.Dispose();

            return qty;
        }

        public static int sumQuantityToReceive(SmartDatabase smartDatabase, int documentType, string documentNo)
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT SUM(quantityToReceive) FROM purchaseLine WHERE documentType = '" + documentType + "' AND documentNo = '" + documentNo + "'");

            int qty = 0;

            if (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0)) qty = int.Parse(dataReader.GetValue(0).ToString());
            }

            dataReader.Close();
            dataReader.Dispose();

            return qty;
        }

        public static int sumQuantityReceived(SmartDatabase smartDatabase, int documentType, string documentNo)
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT SUM(quantityReceived) FROM purchaseLine WHERE documentType = '" + documentType + "' AND documentNo = '" + documentNo + "'");

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
