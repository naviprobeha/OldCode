using System;
using System.Data.SqlServerCe;
using System.Data;
using System.Xml;

namespace Navipro.SmartInventory
{
    /// <summary>
    /// Summary description for DataSalesLine.
    /// </summary>
    public class DataSalesHeader
    {
        private int _entryNo;
        private int _documentType;
        private string _no;
        private string _customerNo = "";
        private string _customerName = "";
        private string _address = "";
        private string _city = "";
        private string _countryCode;
        private DateTime _orderDate;
        private float _totalQty;


        private SmartDatabase smartDatabase;

        public DataSalesHeader(SmartDatabase smartDatabase, SqlCeDataReader dataReader)
        {

            //
            // TODO: Add constructor logic here
            //
            this.smartDatabase = smartDatabase;
            fromDataReader(dataReader);
        }

        public DataSalesHeader(SmartDatabase smartDatabase, DataRow dataRow)
        {

            //
            // TODO: Add constructor logic here
            //
            this.smartDatabase = smartDatabase;
            fromDataRow(dataRow);
        }

        public DataSalesHeader(SmartDatabase smartDatabase)
        {

            //
            // TODO: Add constructor logic here
            //
            this.smartDatabase = smartDatabase;

        }

        public int entryNo { get { return _entryNo; } set { _entryNo = value; } }
        public int documentType { get { return _documentType; } set { _documentType = value; } }
        public string no { get { return _no; } set { _no = value; } }
        public string customerNo { get { return _customerNo; } set { _customerNo = value; } }
        public string customerName { get { return _customerName; } set { _customerName = value; } }
        public string address { get { return _address; } set { _address = value; } }
        public string city { get { return _city; } set { _city = value; } }
        public string countryCode { get { return _countryCode; } set { _countryCode = value; } }
        public DateTime orderDate { get { return _orderDate; } set { _orderDate = value; } }
        public float totalQty { get { return _totalQty; } set { _totalQty = value; } }

        public void save()
        {
            _customerName = _customerName.Replace("'", "");
            _address = _address.Replace("'", "");
            _city = _city.Replace("'", "");
           

            try
            {
                SqlCeDataReader dataReader;

                dataReader = smartDatabase.query("SELECT entryNo FROM salesHeader WHERE entryNo = '" + _entryNo + "'");

                if (!dataReader.Read())
                {
                    smartDatabase.nonQuery("INSERT INTO salesHeader (documentType, no, customerNo, customerName, address, city, countryCode, orderDate, totalQty) VALUES ('" + _documentType + "', '" + _no + "', '" + _customerNo + "', '" + _customerName + "', '" + _address + "', '" + _city + "', '" + _countryCode + "', '" + _orderDate.ToString("yyyy-MM-dd") + "', '"+_totalQty.ToString().Replace(",", ".")+"')");
                }
                else
                {
                    smartDatabase.nonQuery("UPDATE salesHeader SET documentType = '" + _documentType + "', no = '" + _no + "', customerNo = '" + _customerNo + "', customerName = '" + _customerName + "', address = '" + _address + "', city = '" + _city + "', countryCode = '" + _countryCode + "', orderDate = '" + _orderDate.ToString("yyyy-MM-dd") + "', totalQty = '" + _totalQty.ToString().Replace(",", ".") + "' WHERE entryNo = '" + _entryNo + "'");
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
                smartDatabase.nonQuery("DELETE FROM salesHeader WHERE entryNo = '" + _entryNo + "'");

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
            this._no = (string)dataReader.GetValue(2);
            this._customerNo = (string)dataReader.GetValue(3);
            this._customerName = (string)dataReader.GetValue(4);
            this._address = (string)dataReader.GetValue(5);
            this._city = (string)dataReader.GetValue(6);
            this._countryCode = (string)dataReader.GetValue(7);
            this._orderDate = DateTime.Parse(dataReader.GetValue(8).ToString());
            this._totalQty = float.Parse(dataReader.GetValue(9).ToString());
        }

        private void fromDataRow(DataRow dataRow)
        {
            this._entryNo = int.Parse(dataRow.ItemArray.GetValue(0).ToString());
            this._documentType = int.Parse(dataRow.ItemArray.GetValue(1).ToString());
            this._no = dataRow.ItemArray.GetValue(2).ToString();
            this._customerNo = dataRow.ItemArray.GetValue(3).ToString();
            this._customerName = dataRow.ItemArray.GetValue(4).ToString();
            this._address = dataRow.ItemArray.GetValue(5).ToString();
            this._city = dataRow.ItemArray.GetValue(6).ToString();
            this._countryCode = dataRow.ItemArray.GetValue(7).ToString();
            this._orderDate = DateTime.Parse(dataRow.ItemArray.GetValue(8).ToString());
            this._totalQty = float.Parse(dataRow.ItemArray.GetValue(9).ToString());
        }

        public static void fromDOM(XmlElement ordersElement, SmartDatabase smartDatabase)
        {
            deleteSalesHeaders(smartDatabase);


            XmlNodeList orderNodeList = ordersElement.SelectNodes("order");
            int i = 0;
            while (i < orderNodeList.Count)
            {
                XmlElement orderElement = (XmlElement)orderNodeList[i];
                int documentType = int.Parse(orderElement.GetAttribute("documentType"));
                string no = orderElement.GetAttribute("no");

                DataSalesHeader dataSalesHeader = new DataSalesHeader(smartDatabase);
                dataSalesHeader._documentType = documentType;
                dataSalesHeader.no = no;

                if (orderElement.SelectSingleNode("customerNo").FirstChild != null) dataSalesHeader.customerNo = orderElement.SelectSingleNode("customerNo").FirstChild.Value;
                if (orderElement.SelectSingleNode("customerName").FirstChild != null) dataSalesHeader.customerName = orderElement.SelectSingleNode("customerName").FirstChild.Value;
                if (orderElement.SelectSingleNode("address").FirstChild != null) dataSalesHeader.address = orderElement.SelectSingleNode("address").FirstChild.Value;
                if (orderElement.SelectSingleNode("city").FirstChild != null) dataSalesHeader.city = orderElement.SelectSingleNode("city").FirstChild.Value;
                if (orderElement.SelectSingleNode("countryCode").FirstChild != null) dataSalesHeader.countryCode = orderElement.SelectSingleNode("countryCode").FirstChild.Value;
                if (orderElement.SelectSingleNode("orderDate").FirstChild != null) dataSalesHeader.orderDate = DateTime.Parse(orderElement.SelectSingleNode("orderDate").FirstChild.Value);
                if (orderElement.SelectSingleNode("totalQty").FirstChild != null) dataSalesHeader.totalQty = float.Parse(orderElement.SelectSingleNode("totalQty").FirstChild.Value);

                dataSalesHeader.save();



                i++;
            }
        }

        public static DataSet getDataSet(SmartDatabase smartDatabase)
        {
            SqlCeDataAdapter purchaseLineAdapter = smartDatabase.dataAdapterQuery("SELECT entryNo, documentType, no, customerNo, customerName, address, city, countryCode, orderDate, totalQty FROM salesHeader ORDER BY documentType, no");

            DataSet purchaseLineDataSet = new DataSet();
            purchaseLineAdapter.Fill(purchaseLineDataSet, "salesHeader");
            purchaseLineAdapter.Dispose();

            return purchaseLineDataSet;
        }


        public static void deleteSalesHeaders(SmartDatabase smartDatabase)
        {
            smartDatabase.nonQuery("DELETE FROM salesHeader");
        }

    }

}
