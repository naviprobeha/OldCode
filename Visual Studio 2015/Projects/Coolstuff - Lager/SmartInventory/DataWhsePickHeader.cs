using System;
using System.Data.SqlServerCe;
using System.Data;
using System.Xml;

namespace Navipro.SmartInventory
{
    /// <summary>
    /// Summary description for DataSalesLine.
    /// </summary>
    public class DataWhsePickHeader
    {
        private string _no;
        private string _assignedTo = "";
        private int _noOfLines = 0;

        private SmartDatabase smartDatabase;

        public DataWhsePickHeader(SmartDatabase smartDatabase, SqlCeDataReader dataReader)
        {

            //
            // TODO: Add constructor logic here
            //
            this.smartDatabase = smartDatabase;
            fromDataReader(dataReader);
        }

        public DataWhsePickHeader(SmartDatabase smartDatabase, DataRow dataRow)
        {

            //
            // TODO: Add constructor logic here
            //
            this.smartDatabase = smartDatabase;
            fromDataRow(dataRow);
        }

        public DataWhsePickHeader(SmartDatabase smartDatabase)
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

                dataReader = smartDatabase.query("SELECT no FROM whsePickHeader WHERE no = '" + _no + "'");

                if (!dataReader.Read())
                {
                    smartDatabase.nonQuery("INSERT INTO whsePickHeader (no, assignedTo, noOfLines) VALUES ('" + _no + "', '" + _assignedTo + "', '" + _noOfLines + "')");
                }
                else
                {
                    smartDatabase.nonQuery("UPDATE whsePickHeader SET assignedTo = '" + _assignedTo + "', noOfLines = '" + _noOfLines + "' WHERE no = '" + _no + "'");
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
                smartDatabase.nonQuery("DELETE FROM whsePickHeader WHERE no = '" + _no + "'");

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
            deletePickLists(smartDatabase);


            XmlNodeList activityNodeList = activitiesElement.SelectNodes("whseActivityHeader");
            int i = 0;
            while (i < activityNodeList.Count)
            {
                XmlElement activityElement = (XmlElement)activityNodeList[i];
                string no = activityElement.GetAttribute("no");
                string assignedTo = activityElement.GetAttribute("assignedTo");
                int noOfLines = int.Parse(activityElement.GetAttribute("assignedTo"));

                DataWhsePickHeader dataWhsePickHeader = new DataWhsePickHeader(smartDatabase);
                dataWhsePickHeader.no = no;
                dataWhsePickHeader.assignedTo = assignedTo;
                dataWhsePickHeader.noOfLines = noOfLines;

                dataWhsePickHeader.save();

                i++;
            }
        }

        public static DataSet getDataSet(SmartDatabase smartDatabase)
        {
            SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT no, assignedTo, noOfLines FROM whsePickHeader ORDER BY no");

            DataSet purchaseLineDataSet = new DataSet();
            adapter.Fill(purchaseLineDataSet, "whsePickHeader");
            adapter.Dispose();

            return purchaseLineDataSet;
        }


        public static void deletePickLists(SmartDatabase smartDatabase)
        {
            smartDatabase.nonQuery("DELETE FROM whsePickHeader");
            smartDatabase.nonQuery("DELETE FROM whsePickLine");
        }

    }

}
