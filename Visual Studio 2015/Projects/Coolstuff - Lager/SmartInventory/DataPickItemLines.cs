using System;
using System.Data.SqlServerCe;
using System.Data;
using System.Xml;

namespace Navipro.SmartInventory
{
    /// <summary>
    /// Summary description for DataSalesLine.
    /// </summary>
    public class DataPickItemLines : ServiceArgument
    {
        private string _documentNo = "";
        private int _lineNo = 0;
        private string _itemNo = "";
        private string _variantCode = "";
        private string _description = "";
        private string _description2 = "";
        private int _count;

        private SmartDatabase smartDatabase;

        public DataPickItemLines(SmartDatabase smartDatabase, SqlCeDataReader dataReader)
        {

            //
            // TODO: Add constructor logic here
            //
            this.smartDatabase = smartDatabase;
            fromDataReader(dataReader);
        }

        public DataPickItemLines(SmartDatabase smartDatabase, DataRow dataRow)
        {

            //
            // TODO: Add constructor logic here
            //
            this.smartDatabase = smartDatabase;
            fromDataRow(dataRow);
        }

        public DataPickItemLines(SmartDatabase smartDatabase)
        {

            //
            // TODO: Add constructor logic here
            //
            this.smartDatabase = smartDatabase;

        }

        public string documentNo { get { return _documentNo; } set { _documentNo = value; } }
        public int lineNo { get { return _lineNo; } set { _lineNo = value; } }
        public string itemNo { get { return _itemNo; } set { _itemNo = value; } }
        public string variantCode { get { return _variantCode; } set { _variantCode = value; } }
        public string description { get { return _description; } set { _description = value; } }
        public string description2 { get { return _description2; } set { _description2 = value; } }
        public int count { get { return _count; } set { _count = value; } }


        public void save()
        {
            try
            {
                SqlCeDataReader dataReader;

                dataReader = smartDatabase.query("SELECT documentNo FROM pickItemLine WHERE documentNo = '" + _documentNo + "' AND docLineNo = '" + _lineNo + "'");

                if (!dataReader.Read())
                {
                    smartDatabase.nonQuery("INSERT INTO pickItemLine (documentNo, docLineNo, itemNo, variantCode, description, description2) VALUES ('" + _documentNo + "', '" + _lineNo + "', '" + _itemNo + "', '" + _variantCode + "', '" + _description + "', '" + _description2 + "')");
                    //smartDatabase.nonQuery("INSERT INTO pickItemLine (documentNo, itemNo, variantCode, description, description2) VALUES ('" + _documentNo + "', '" + _itemNo + "', '" + _variantCode + "', '" + _description + "', '" + _description2 + "')");
                }
                else
                {
                    smartDatabase.nonQuery("UPDATE pickItemLine SET itemNo = '" + _itemNo + "', variantCode = '" + _variantCode + "', description = '" + _description + "', description2 = '" + _description2 + "' WHERE documentNo = '" + _documentNo + "' AND docLineNo = '" + _lineNo + "'");
                    //smartDatabase.nonQuery("UPDATE pickItemLine SET itemNo = '" + _itemNo + "', variantCode = '" + _variantCode + "', description = '" + _description + "', description2 = '" + _description2 + "' WHERE documentNo = '" + _documentNo + "'");
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
                smartDatabase.nonQuery("DELETE FROM pickItemLine WHERE documentNo = '" + _documentNo + "' AND docLineNo = '" + _lineNo + "'");
                //smartDatabase.nonQuery("DELETE FROM pickItemLine WHERE documentNo = '" + _documentNo + "'");
            }
            catch (SqlCeException e)
            {
                smartDatabase.ShowErrors(e);
            }

        }

        public static void deleteAll(SmartDatabase smartDatabase, string documentNo)
        {
            smartDatabase.nonQuery("DELETE FROM pickItemLine WHERE documentNo = '" + documentNo + "'");

        }

        private void fromDataReader(SqlCeDataReader dataReader)
        {
            this._documentNo = (string)dataReader.GetValue(0);
            this._lineNo = dataReader.GetInt32(1);
            this._itemNo = (string)dataReader.GetValue(2);
            this._variantCode = (string)dataReader.GetValue(3);
            this._description = (string)dataReader.GetValue(4);
            this._description2 = (string)dataReader.GetValue(5);
            this._count = dataReader.GetInt32(6);
        }

        private void fromDataRow(DataRow dataRow)
        {
            this._documentNo = (string)dataRow.ItemArray.GetValue(0);
            this._lineNo = int.Parse(dataRow.ItemArray.GetValue(1).ToString());
            this._itemNo = (string)dataRow.ItemArray.GetValue(2);
            this._variantCode = (string)dataRow.ItemArray.GetValue(3);
            this._description = (string)dataRow.ItemArray.GetValue(4);
            this._description2 = (string)dataRow.ItemArray.GetValue(5);
            this._count = int.Parse(dataRow.ItemArray.GetValue(6).ToString());
        }

        public static void fromDOM(XmlElement pickLinesElement, SmartDatabase smartDatabase)
        {
            deletePickItemLine(smartDatabase);

            XmlNodeList pickLineNodeList = pickLinesElement.SelectNodes("pickListLine");
            int i = 0;            
            while (i < pickLineNodeList.Count)
            {
                XmlElement pickLineElement = (XmlElement)pickLineNodeList[i];

                DataPickItemLines pickItemLine = new DataPickItemLines(smartDatabase);
                pickItemLine.documentNo = pickLineElement.GetAttribute("documentNo");
                pickItemLine.lineNo = int.Parse(pickLineElement.GetAttribute("lineNo"));
                //pickItemLine.lineNo = int.Parse(pickLineElement.SelectSingleNode("lineNo").FirstChild.Value);
                pickItemLine.count = int.Parse(pickLineElement.GetAttribute("count"));

                if (pickLineElement.SelectSingleNode("itemNo").FirstChild != null)
                {
                    pickItemLine.itemNo = pickLineElement.SelectSingleNode("itemNo").FirstChild.Value;                    
                }
                if (pickLineElement.SelectSingleNode("variantCode").FirstChild != null)
                {
                    pickItemLine.variantCode = pickLineElement.SelectSingleNode("variantCode").FirstChild.Value;
                }
                if (pickLineElement.SelectSingleNode("description").FirstChild != null)
                {
                    pickItemLine.description = pickLineElement.SelectSingleNode("description").FirstChild.Value;
                }
                if (pickLineElement.SelectSingleNode("description2").FirstChild != null)
                {
                    pickItemLine.description2 = pickLineElement.SelectSingleNode("description2").FirstChild.Value;
                }

                pickItemLine.save();
                   
                i++;
            }
        }

        public static DataSet getDataSet(SmartDatabase smartDatabase, string documentNo)
        {
            SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT documentNo, docLineNo, itemNo, variantCode, description, description2 FROM pickItemLine WHERE documentNo = '" + documentNo + "' ORDER BY docLineNo");

            DataSet pickItemLineDataSet = new DataSet();
            adapter.Fill(pickItemLineDataSet, "pickItemLine");
            adapter.Dispose();

            return pickItemLineDataSet;
        }

        public static int countUnhandlesLines(SmartDatabase smartDatabase, string documentNo)
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT COUNT(*) FROM pickItemLine WHERE documentNo = '" + documentNo + "'");

            int count = 0;

            if (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0)) count = int.Parse(dataReader.GetValue(0).ToString());
            }
            dataReader.Close();

            return count;
        }

        public static void deletePickItemLine(SmartDatabase smartDatabase)
        {
            smartDatabase.nonQuery("DELETE FROM pickItemLine");
        }

        #region ServiceArgument Members

        XmlElement ServiceArgument.toDOM(XmlDocument xmlDocumentContext)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

}
