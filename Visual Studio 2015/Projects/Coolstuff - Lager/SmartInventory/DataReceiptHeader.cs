using System;
using System.Data.SqlServerCe;
using System.Data;
using System.Xml;

namespace Navipro.SmartInventory
{
    /// <summary>
    /// Summary description for DataSalesLine.
    /// </summary>
    public class DataReceiptHeader : ServiceArgument
    {
        private string _no;
        private string _assignedTo = "";
        private int _noOfLines = 0;

        private SmartDatabase smartDatabase;

        public DataReceiptHeader(SmartDatabase smartDatabase, SqlCeDataReader dataReader)
        {

            //
            // TODO: Add constructor logic here
            //
            this.smartDatabase = smartDatabase;
            fromDataReader(dataReader);
        }

        public DataReceiptHeader(SmartDatabase smartDatabase, DataRow dataRow)
        {

            //
            // TODO: Add constructor logic here
            //
            this.smartDatabase = smartDatabase;
            fromDataRow(dataRow);
        }

        public DataReceiptHeader(SmartDatabase smartDatabase)
        {

            //
            // TODO: Add constructor logic here
            //
            this.smartDatabase = smartDatabase;

        }

        public string no { get { return _no; } set { _no = value; } }
        public string assignedTo { get { return _assignedTo; } set { _assignedTo = value; } }
        public int noOfLines { get { return _noOfLines; } set { _noOfLines = value; } }

        public void save()
        {

            try
            {
                SqlCeDataReader dataReader;

                dataReader = smartDatabase.query("SELECT no FROM receiptHeader WHERE no = '" + _no + "'");

                if (!dataReader.Read())
                {
                    smartDatabase.nonQuery("INSERT INTO receiptHeader (no, assignedTo, noOfLines) VALUES ('" + _no + "', '" + _assignedTo + "', '" + _noOfLines + "')");
                }
                else
                {
                    smartDatabase.nonQuery("UPDATE receiptHeader SET assignedTo = '" + _assignedTo + "', noOfLines = '" + _noOfLines + "' WHERE no = '" + _no + "'");
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
                smartDatabase.nonQuery("DELETE FROM receiptHeader WHERE no = '" + _no + "'");

            }
            catch (SqlCeException e)
            {
                smartDatabase.ShowErrors(e);
            }

        }


        private void fromDataReader(SqlCeDataReader dataReader)
        {
            this._no = (string)dataReader.GetValue(0);
            this._assignedTo = (string)dataReader.GetValue(1);
            this._noOfLines = dataReader.GetInt32(2);
        }

        private void fromDataRow(DataRow dataRow)
        {
            this._no = dataRow.ItemArray.GetValue(0).ToString();
            this._assignedTo = dataRow.ItemArray.GetValue(1).ToString();
            this._noOfLines = int.Parse(dataRow.ItemArray.GetValue(2).ToString());
        }

        public static void fromDOM(XmlElement activitiesElement, SmartDatabase smartDatabase)
        {
            deleteReceiptLists(smartDatabase);

            XmlNodeList activityNodeList = activitiesElement.SelectNodes("receiptList");
            int i = 0;
            while (i < activityNodeList.Count)
            {

                XmlElement activityElement = (XmlElement)activityNodeList[i];
                string no = activityElement.GetAttribute("no");

                string assignedTo = "";
                if (activityElement.SelectSingleNode("assignedTo").FirstChild != null)
                {
                    assignedTo = activityElement.SelectSingleNode("assignedTo").FirstChild.Value;
                }

                int noOfLines = int.Parse(activityElement.SelectSingleNode("noOfLines").FirstChild.Value);

                DataReceiptHeader dataWhseReceiptHeader = new DataReceiptHeader(smartDatabase);
                dataWhseReceiptHeader.no = no;
                dataWhseReceiptHeader.assignedTo = assignedTo;
                dataWhseReceiptHeader.noOfLines = noOfLines;

                dataWhseReceiptHeader.save();

                i++;
            }

        }

        public static DataSet getDataSet(SmartDatabase smartDatabase)
        {
            SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT no, assignedTo, noOfLines FROM receiptHeader ORDER BY no");

            DataSet purchaseLineDataSet = new DataSet();
            adapter.Fill(purchaseLineDataSet, "receiptHeader");
            adapter.Dispose();

            return purchaseLineDataSet;
        }

        public static DataReceiptHeader getReceiptHeader(SmartDatabase smartDatabase, string documentNo)
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT no, assignedTo, noOfLines FROM receiptHeader WHERE no = '" + documentNo + "'");

            DataReceiptHeader dataReceiptHeader = null;

            if (dataReader.Read())
            {
                dataReceiptHeader = new DataReceiptHeader(smartDatabase, dataReader);
            }
            dataReader.Close();

            return dataReceiptHeader;
        }

        public static void deleteReceiptLists(SmartDatabase smartDatabase)
        {
            smartDatabase.nonQuery("DELETE FROM receiptHeader");
            smartDatabase.nonQuery("DELETE FROM receiptLine");
        }


        #region ServiceArgument Members

        public XmlElement toDOM(XmlDocument xmlDocumentContext)
        {
            XmlElement docElement = xmlDocumentContext.CreateElement("receiptHeader");
            /*
            docElement.SetAttribute("no", _no);

            XmlElement linesElement = xmlDocumentContext.CreateElement("lines");

            DataSet dataSet = DataPickLine.getDataSet(smartDatabase, _no);
            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                DataPickLine dataPickLine = new DataPickLine(smartDatabase, dataSet.Tables[0].Rows[i]);
                if (dataPickLine.picked == true)
                {
                    XmlElement lineElement = xmlDocumentContext.CreateElement("line");
                    lineElement.SetAttribute("lineNo", dataPickLine.lineNo.ToString());
                    lineElement.SetAttribute("pickedQty", dataPickLine.pickedQty.ToString());
                    linesElement.AppendChild(lineElement);
                }

                i++;
            }

            docElement.AppendChild(linesElement);
            */
            return docElement;

        }

        #endregion
    }

}
