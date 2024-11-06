using System;
using System.Data.SqlServerCe;
using System.Data;

namespace Navipro.SmartInventory
{
    /// <summary>
    /// Summary description for DataSalesLine.
    /// </summary>
    public class DataScanLine
    {
        private int _entryNo;
        private int _type;
        private int _documentType;
        private string _documentNo;
        private int _lineNo;
        private float _quantity;
        private string _cartonNo = "";


        private SmartDatabase smartDatabase;

        public DataScanLine(SmartDatabase smartDatabase, SqlCeDataReader dataReader)
        {

            //
            // TODO: Add constructor logic here
            //
            this.smartDatabase = smartDatabase;
            readData(dataReader);
        }

        public DataScanLine(SmartDatabase smartDatabase)
        {

            //
            // TODO: Add constructor logic here
            //
            this.smartDatabase = smartDatabase;

        }

        public int entryNo { get { return _entryNo; } set { _entryNo = value; } }
        public int type { get { return _type; } set { _type = value; } }
        public int documentType { get { return _documentType; } set { _documentType = value; } }
        public string documentNo { get { return _documentNo; } set { _documentNo = value; } }
        public int lineNo { get { return _lineNo; } set { _lineNo = value; } }
        public float quantity { get { return _quantity; } set { _quantity = value; } }
        public string cartonNo { get { return _cartonNo; } set { _cartonNo = value; } }




        public void save()
        {
            try
            {
                SqlCeDataReader dataReader;

                dataReader = smartDatabase.query("SELECT entryNo FROM scanLine WHERE entryNo = '" + _entryNo + "'");

                if (!dataReader.Read())
                {
                    smartDatabase.nonQuery("INSERT INTO scanLine (type, documentType, documentNo, orderLineNo, quantity, cartonNo) VALUES ('" + _type + "', '" + _documentType + "', '" + _documentNo + "', '" + _lineNo + "', '" + _quantity.ToString().Replace(",", ".") + "', '"+_cartonNo+"')");
                }
                else
                {
                    smartDatabase.nonQuery("UPDATE scanLine SET type = '"+_type +"', documentType = '"+_documentType+"', documentNo = '" + _documentNo + "', orderLineNo = '" + _lineNo + "', quantity = '" + _quantity.ToString().Replace(",", ".") + "', cartonNo = '"+_cartonNo+"' WHERE entryNo = '" + _entryNo + "'");
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
                smartDatabase.nonQuery("DELETE FROM scanLine WHERE entryNo = '" + _entryNo + "'");

            }
            catch (SqlCeException e)
            {
                smartDatabase.ShowErrors(e);
            }

        }


        private void readData(SqlCeDataReader dataReader)
        {
            this._entryNo = dataReader.GetInt32(0);
            this._type = dataReader.GetInt32(1);
            this._documentType = dataReader.GetInt32(2);
            this._documentNo = (string)dataReader.GetValue(3);
            this._lineNo = dataReader.GetInt32(4);
            this._quantity = float.Parse(dataReader.GetValue(5).ToString());
            this._cartonNo = (string)dataReader.GetValue(6);
        }

        public static DataScanLine getLineFromOrderLine(SmartDatabase smartDatabase, int type, int documentType, string documentNo, int lineNo)
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT entryNo, type, documentType, documentNo, orderLineNo, quantity, cartonNo FROM scanLine WHERE type = '"+type+"' AND documentType = '"+documentType+"' AND documentNo = '" + documentNo + "' AND orderLineNo = '" + lineNo + "'");

            DataScanLine dataScanLine = null;

            if (dataReader.Read())
            {
                dataScanLine = new DataScanLine(smartDatabase, dataReader);
            }
            dataReader.Close();
            dataReader.Dispose();

            return dataScanLine;
        }

        public static void updatePurchase(SmartDatabase smartDatabase, DataPurchaseLine dataPurchaseLine)
        {
            DataScanLine dataScanLine = getLineFromOrderLine(smartDatabase, 0, dataPurchaseLine.documentType, dataPurchaseLine.documentNo, dataPurchaseLine.lineNo);
            if (dataScanLine == null)
            {
                dataScanLine = new DataScanLine(smartDatabase);
                dataScanLine.type = 0;
                dataScanLine.documentType = dataPurchaseLine.documentType;
                dataScanLine.documentNo = dataPurchaseLine.documentNo;
                dataScanLine.lineNo = dataPurchaseLine.lineNo;
            }

            dataScanLine.quantity = dataPurchaseLine.quantityToReceive;
            dataScanLine.save();

        }

        public static void updateSales(SmartDatabase smartDatabase, DataSalesLine dataSalesLine)
        {
            DataScanLine dataScanLine = getLineFromOrderLine(smartDatabase, 1, dataSalesLine.documentType, dataSalesLine.documentNo, dataSalesLine.lineNo);
            if (dataScanLine == null)
            {
                dataScanLine = new DataScanLine(smartDatabase);
                dataScanLine.type = 1;
                dataScanLine.documentType = dataSalesLine.documentType;
                dataScanLine.documentNo = dataSalesLine.documentNo;
                dataScanLine.lineNo = dataSalesLine.lineNo;
                dataScanLine.cartonNo = dataSalesLine.carton;
            }

            dataScanLine.quantity = dataSalesLine.quantityToShip;
            dataScanLine.save();

        }

        public static void deleteDocument(SmartDatabase smartDatabase, int type, int documentType, string documentNo)
        {
            smartDatabase.nonQuery("DELETE FROM scanLine WHERE type = '"+type+"' AND documentType = '"+documentType+"' AND documentNo = '" + documentNo + "'");
        }
    }

}
