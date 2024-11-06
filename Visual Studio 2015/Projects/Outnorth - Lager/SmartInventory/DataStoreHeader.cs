using System;
using System.Data.SqlServerCe;
using System.Data;
using System.Xml;

namespace Navipro.SmartInventory
{
    /// <summary>
    /// Summary description for DataSalesLine.
    /// </summary>
    public class DataStoreHeader : ServiceArgument
    {
        private string _no;
        private string _assignedTo = "";
        private int _noOfLines = 0;

        private SmartDatabase smartDatabase;

        public DataStoreHeader(SmartDatabase smartDatabase, SqlCeDataReader dataReader)
        {

            //
            // TODO: Add constructor logic here
            //
            this.smartDatabase = smartDatabase;
            fromDataReader(dataReader);
        }

        public DataStoreHeader(SmartDatabase smartDatabase, DataRow dataRow)
        {

            //
            // TODO: Add constructor logic here
            //
            this.smartDatabase = smartDatabase;
            fromDataRow(dataRow);
        }

        public DataStoreHeader(SmartDatabase smartDatabase)
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

                dataReader = smartDatabase.query("SELECT no FROM storeHeader WHERE no = '" + _no + "'");

                if (!dataReader.Read())
                {
                    smartDatabase.nonQuery("INSERT INTO storeHeader (no, assignedTo, noOfLines) VALUES ('" + _no + "', '" + _assignedTo + "', '" + _noOfLines + "')");
                }
                else
                {
                    smartDatabase.nonQuery("UPDATE storeHeader SET assignedTo = '" + _assignedTo + "', noOfLines = '" + _noOfLines + "' WHERE no = '" + _no + "'");
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
                smartDatabase.nonQuery("DELETE FROM storeHeader WHERE no = '" + _no + "'");

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
            deleteStoreLists(smartDatabase);

            XmlNodeList activityNodeList = activitiesElement.SelectNodes("storeList");
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
                
                DataStoreHeader dataWhseStoreHeader = new DataStoreHeader(smartDatabase);
                dataWhseStoreHeader.no = no;
                dataWhseStoreHeader.assignedTo = assignedTo;
                dataWhseStoreHeader.noOfLines = noOfLines;

                dataWhseStoreHeader.save();
                
                i++;
            }
        }

        public static DataSet getDataSet(SmartDatabase smartDatabase)
        {
            SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT no, assignedTo, noOfLines FROM storeHeader ORDER BY no");

            DataSet purchaseLineDataSet = new DataSet();
            adapter.Fill(purchaseLineDataSet, "storeHeader");
            adapter.Dispose();

            return purchaseLineDataSet;
        }

        public static DataStoreHeader getPickHeader(SmartDatabase smartDatabase, string documentNo)
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT no, assignedTo, noOfLines FROM storeHeader WHERE no = '" + documentNo + "'");

            DataStoreHeader dataStoreHeader = null;

            if (dataReader.Read())
            {
                dataStoreHeader = new DataStoreHeader(smartDatabase, dataReader);
            }
            dataReader.Close();

            return dataStoreHeader;
        }

        public static void deleteStoreLists(SmartDatabase smartDatabase)
        {
            smartDatabase.nonQuery("DELETE FROM storeHeader");
            smartDatabase.nonQuery("DELETE FROM storeLine");
        }


        #region ServiceArgument Members

        public XmlElement toDOM(XmlDocument xmlDocumentContext)
        {
            XmlElement docElement = xmlDocumentContext.CreateElement("storeHeader");
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
