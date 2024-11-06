using System;
using System.Data.SqlServerCe;
using System.Data;

namespace Navipro.SmartInventory
{
    /// <summary>
    /// Summary description for DataSalesLine.
    /// </summary>
    public class DataSalesLineCarton
    {
        private int _entryNo;
        private int _documentType;
        private string _documentNo;
        private int _lineNo;
        private string _cartonNo = "";
        private float _splitOnQuantity;

        private SmartDatabase smartDatabase;

        public DataSalesLineCarton(SmartDatabase smartDatabase, SqlCeDataReader dataReader)
        {

            //
            // TODO: Add constructor logic here
            //
            this.smartDatabase = smartDatabase;
            readData(dataReader);
        }

        public DataSalesLineCarton(SmartDatabase smartDatabase)
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
        public string cartonNo { get { return _cartonNo; } set { _cartonNo = value; } }
        public float splitOnQuantity { get { return _splitOnQuantity; } set { _splitOnQuantity = value; } }




        public void save()
        {
            try
            {
                SqlCeDataReader dataReader;

                dataReader = smartDatabase.query("SELECT entryNo FROM salesLineCarton WHERE entryNo = '" + _entryNo + "'");

                if (!dataReader.Read())
                {
                    smartDatabase.nonQuery("INSERT INTO salesLineCarton (documentType, documentNo, orderLineNo, cartonNo, splitOnQuantity) VALUES ('" + _documentType + "', '" + _documentNo + "', '" + _lineNo + "', '" + _cartonNo + "', '" + _splitOnQuantity.ToString().Replace(",", ".") + "')");
                }
                else
                {
                    smartDatabase.nonQuery("UPDATE salesLineCarton SET documentType = '" + _documentType + "', documentNo = '" + _documentNo + "', orderLineNo = '" + _lineNo + "', cartonNo = '" + _cartonNo + "', splitOnQuantity = '" + _splitOnQuantity.ToString().Replace(",", ".") + "' WHERE entryNo = '" + _entryNo + "'");
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
                smartDatabase.nonQuery("DELETE FROM salesLineCarton WHERE entryNo = '" + _entryNo + "'");

            }
            catch (SqlCeException e)
            {
                smartDatabase.ShowErrors(e);
            }

        }


        private void readData(SqlCeDataReader dataReader)
        {
            this._entryNo = dataReader.GetInt32(0);
            this._documentType = dataReader.GetInt32(1);
            this._documentNo = (string)dataReader.GetValue(2);
            this._lineNo = dataReader.GetInt32(3);
            this._cartonNo = (string)dataReader.GetValue(4);
            this._splitOnQuantity = float.Parse(dataReader.GetValue(5).ToString());
        }

        public static DataScanLine getSalesLineCarton(SmartDatabase smartDatabase, int documentType, string documentNo, int lineNo)
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT entryNo, type, documentType, documentNo, orderLineNo, quantity, cartonNo FROM scanLine WHERE documentType = '" + documentType + "' AND documentNo = '" + documentNo + "' AND orderLineNo = '" + lineNo + "'");

            DataScanLine dataScanLine = null;

            if (dataReader.Read())
            {
                dataScanLine = new DataScanLine(smartDatabase, dataReader);
            }
            dataReader.Close();
            dataReader.Dispose();

            return dataScanLine;
        }

        public static void deleteDocument(SmartDatabase smartDatabase, int documentType, string documentNo)
        {
            smartDatabase.nonQuery("DELETE FROM salesLineCarton WHERE documentType = '" + documentType + "' AND documentNo = '" + documentNo + "'");
        }

        public static void deleteEntry(SmartDatabase smartDatabase, int entryNo)
        {
            smartDatabase.nonQuery("DELETE FROM salesLineCarton WHERE entryNo = '" + entryNo + "'");
        }


        public static void updateCartonNo(SmartDatabase smartDatabase, int entryNo, string cartonNo)
        {
            smartDatabase.nonQuery("UPDATE salesLineCarton SET cartonNo = '"+cartonNo+"' WHERE entryNo = '" + entryNo + "'");
        }

        public static DataSet getDataSet(SmartDatabase smartDatabase, int documentType, string documentNo, int lineNo)
        {
            SqlCeDataAdapter purchaseLineAdapter = smartDatabase.dataAdapterQuery("SELECT entryNo, documentType, documentNo, orderLineNo, cartonNo, splitOnQuantity FROM salesLineCarton WHERE documentType = '" + documentType + "' AND documentNo = '" + documentNo + "' AND orderLineNo = '"+lineNo+"' ORDER BY cartonNo");

            DataSet purchaseLineDataSet = new DataSet();
            purchaseLineAdapter.Fill(purchaseLineDataSet, "salesLineCarton");
            purchaseLineAdapter.Dispose();

            return purchaseLineDataSet;
        }

        public static void createCarton(SmartDatabase smartDatabase, int documentType, string documentNo, int lineNo, string cartonNo, float splitOnQuantity)
        {
            DataSalesLineCarton dataSalesLineCarton = new DataSalesLineCarton(smartDatabase);
            dataSalesLineCarton.documentType = documentType;
            dataSalesLineCarton.documentNo = documentNo;
            dataSalesLineCarton.lineNo = lineNo;
            dataSalesLineCarton.cartonNo = cartonNo;
            dataSalesLineCarton.splitOnQuantity = splitOnQuantity;
            dataSalesLineCarton.save();
        }

        public static DataSalesLineCarton getCurrentCarton(SmartDatabase smartDatabase, int documentType, string documentNo, int lineNo)
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT entryNo, documentType, documentNo, orderLineNo, cartonNo, splitOnQuantity FROM salesLineCarton WHERE documentType = '" + documentType + "' AND documentNo = '" + documentNo + "' AND orderLineNo = '" + lineNo + "' ORDER BY CAST(cartonNo as int) DESC");

            DataSalesLineCarton dataSalesLineCarton = null;

            if (dataReader.Read())
            {
                dataSalesLineCarton = new DataSalesLineCarton(smartDatabase, dataReader);
            }
            dataReader.Close();
            dataReader.Dispose();

            return dataSalesLineCarton;
        }

        public static DataSalesLineCarton getLastCarton(SmartDatabase smartDatabase, int documentType, string documentNo)
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT entryNo, documentType, documentNo, orderLineNo, cartonNo, splitOnQuantity FROM salesLineCarton WHERE documentType = '" + documentType + "' AND documentNo = '" + documentNo + "' ORDER BY CAST(cartonNo as int) DESC");

            DataSalesLineCarton dataSalesLineCarton = null;

            if (dataReader.Read())
            {
                dataSalesLineCarton = new DataSalesLineCarton(smartDatabase, dataReader);
            }
            dataReader.Close();
            dataReader.Dispose();

            return dataSalesLineCarton;
        }

        public static bool cartonExists(SmartDatabase smartDatabase, int documentType, string documentNo, int lineNo, string cartonNo)
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT entryNo FROM salesLineCarton WHERE documentType = '" + documentType + "' AND documentNo = '" + documentNo + "' AND orderLineNo = '" + lineNo + "' AND cartonNo = '"+cartonNo+"'");

            bool found = false;

            if (dataReader.Read())
            {
                found = true;
            }

            return found;
        }

        public static bool cartonsExists(SmartDatabase smartDatabase, int documentType, string documentNo)
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT entryNo FROM salesLineCarton WHERE documentType = '" + documentType + "' AND documentNo = '" + documentNo + "'");

            bool found = false;

            if (dataReader.Read())
            {
                found = true;
            }

            return found;
        }
    }

}
