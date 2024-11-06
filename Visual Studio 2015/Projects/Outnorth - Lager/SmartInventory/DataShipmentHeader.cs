using System;
using System.Data.SqlServerCe;
using System.Data;
using System.Xml;

namespace Navipro.SmartInventory
{
    /// <summary>
    /// Summary description for DataSalesLine.
    /// </summary>
    public class DataShipmentHeader : ServiceArgument
    {
        private string _no;
        private string _name = "";
        private int _noOfLines = 0;

        private SmartDatabase smartDatabase;

        public DataShipmentHeader(SmartDatabase smartDatabase, SqlCeDataReader dataReader)
        {

            //
            // TODO: Add constructor logic here
            //
            this.smartDatabase = smartDatabase;
            fromDataReader(dataReader);
        }

        public DataShipmentHeader(SmartDatabase smartDatabase, DataRow dataRow)
        {

            //
            // TODO: Add constructor logic here
            //
            this.smartDatabase = smartDatabase;
            fromDataRow(dataRow);
        }

        public DataShipmentHeader(XmlElement shipmentElement)
        {

            //
            // TODO: Add constructor logic here
            //
            fromDOM(shipmentElement);
        }

        public DataShipmentHeader(SmartDatabase smartDatabase)
        {

            //
            // TODO: Add constructor logic here
            //
            this.smartDatabase = smartDatabase;

        }

        public string no { get { return _no; } set { _no = value; } }
        public string name { get { return _name; } set { _name = value; } }
        public int noOfLines { get { return _noOfLines; } set { _noOfLines = value; } }

        public void save()
        {

            try
            {
                SqlCeDataReader dataReader;

                dataReader = smartDatabase.query("SELECT no FROM shipmentHeader WHERE no = '" + _no + "'");

                if (!dataReader.Read())
                {
                    smartDatabase.nonQuery("INSERT INTO shipmentHeader (no, name, noOfLines) VALUES ('" + _no + "', '" + _name + "', '" + _noOfLines + "')");
                }
                else
                {
                    smartDatabase.nonQuery("UPDATE shipmentHeader SET name = '" + _name + "', noOfLines = '" + _noOfLines + "' WHERE no = '" + _no + "'");
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
                smartDatabase.nonQuery("DELETE FROM shipmentHeader WHERE no = '" + _no + "'");

            }
            catch (SqlCeException e)
            {
                smartDatabase.ShowErrors(e);
            }

        }


        private void fromDataReader(SqlCeDataReader dataReader)
        {
            this._no = (string)dataReader.GetValue(0);
            this._name = (string)dataReader.GetValue(1);
            this._noOfLines = dataReader.GetInt32(2);
        }

        private void fromDataRow(DataRow dataRow)
        {
            this._no = dataRow.ItemArray.GetValue(0).ToString();
            this._name = dataRow.ItemArray.GetValue(1).ToString();
            this._noOfLines = int.Parse(dataRow.ItemArray.GetValue(2).ToString());
        }

        public void fromDOM(XmlElement activityElement)
        {
            no = activityElement.GetAttribute("no");

            name = activityElement.SelectSingleNode("name").FirstChild.Value;
            noOfLines = int.Parse(activityElement.SelectSingleNode("noOfLines").FirstChild.Value);

        }

        public static void fromDOM(XmlElement activitiesElement, SmartDatabase smartDatabase)
        {
            deleteShipmentLists(smartDatabase);

            XmlNodeList activityNodeList = activitiesElement.SelectNodes("shipmentList");
            int i = 0;
            while (i < activityNodeList.Count)
            {
                XmlElement activityElement = (XmlElement)activityNodeList[i];
                string no = activityElement.GetAttribute("no");

                string name = activityElement.SelectSingleNode("name").FirstChild.Value;
                int noOfLines = int.Parse(activityElement.SelectSingleNode("noOfLines").FirstChild.Value);

                DataShipmentHeader dataWhsePickHeader = new DataShipmentHeader(smartDatabase);
                dataWhsePickHeader.no = no;
                dataWhsePickHeader.name = name;
                dataWhsePickHeader.noOfLines = noOfLines;

                dataWhsePickHeader.save();

                i++;
            }
        }

        public static DataSet getDataSet(SmartDatabase smartDatabase)
        {
            SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT no, name, noOfLines FROM shipmentHeader ORDER BY no");

            DataSet purchaseLineDataSet = new DataSet();
            adapter.Fill(purchaseLineDataSet, "shipmentHeader");
            adapter.Dispose();

            return purchaseLineDataSet;
        }

        public static DataShipmentHeader getShipmentHeader(SmartDatabase smartDatabase, string documentNo)
        {
            SqlCeDataReader dataReader = smartDatabase.query("SELECT no, name, noOfLines FROM shipmentHeader WHERE no = '" + documentNo + "'");

            DataShipmentHeader dataShipmentHeader = null;

            if (dataReader.Read())
            {
                dataShipmentHeader = new DataShipmentHeader(smartDatabase, dataReader);
            }
            dataReader.Close();

            return dataShipmentHeader;
        }

        public static void deleteShipmentLists(SmartDatabase smartDatabase)
        {
            smartDatabase.nonQuery("DELETE FROM shipmentHeader");
        }


        #region ServiceArgument Members

        public XmlElement toDOM(XmlDocument xmlDocumentContext)
        {
            XmlElement docElement = xmlDocumentContext.CreateElement("shipmentHeader");

            docElement.SetAttribute("no", _no);


            return docElement;

        }

        #endregion
    }

}
