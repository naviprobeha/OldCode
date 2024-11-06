using System;
using System.Data.SqlServerCe;
using System.Data;
using System.Xml;

namespace Navipro.SmartInventory
{
    /// <summary>
    /// Summary description for DataSalesLine.
    /// </summary>
    public class DataPickConfig : ServiceArgument
    {
        private int _countPickOrders;
        private int _countBulkOrders;
        private int _countOrderLines;
        private string _userNo;
        private string _fromBin;
        private string _toBin;
        private int _maxOrderCount;
        private string _wagonNo;
        private bool _createPickHeaders;
        private string _createdPickListNo = "";
        private string _shipmentNo = "";

        private DataUserCollection _userCollection;
        private DataWagonCollection _wagonCollection;
        private DataShipmentHeaderCollection _shipmentHeaderCollection;


        public DataPickConfig()
        {

            //
            // TODO: Add constructor logic here
            //
            _userCollection = new DataUserCollection();
            _wagonCollection = new DataWagonCollection();
            _shipmentHeaderCollection = new DataShipmentHeaderCollection();
        }

        public int countPickOrders { get { return _countPickOrders; } set { _countPickOrders = value; } }
        public int countBulkOrders { get { return _countBulkOrders; } set { _countBulkOrders = value; } }
        public int countOrderLines { get { return _countOrderLines; } set { _countOrderLines = value; } }
        public string userNo { get { return _userNo; } set { _userNo = value; } }
        public string fromBin { get { return _fromBin; } set { _fromBin = value; } }
        public string toBin { get { return _toBin; } set { _toBin = value; } }
        public int maxOrderCount { get { return _maxOrderCount; } set { _maxOrderCount = value; } }
        public bool createPickHeaders { get { return _createPickHeaders; } set { _createPickHeaders = value; } }
        public DataUserCollection userCollection { get { return _userCollection; } }
        public DataWagonCollection wagonCollection { get { return _wagonCollection; } }
        public DataShipmentHeaderCollection shipmentHeaderCollection { get { return _shipmentHeaderCollection; } }
        public string createdPickListNo { get { return _createdPickListNo; } set { _createdPickListNo = value; } }
        public string wagonNo { get { return _wagonNo; } set { _wagonNo = value; } }
        public string shipmentNo { get { return _shipmentNo; } set { _shipmentNo = value; } }

        public string getUserNoFromName(string name)
        {
            int i = 0;
            while (i < userCollection.Count)
            {
                if (userCollection[i].name == name) return userCollection[i].code;
                i++;
            }
            return "";
        }

        public string getShipmentNoFromName(string name)
        {
            int i = 0;
            while (i < shipmentHeaderCollection.Count)
            {
                if (shipmentHeaderCollection[i].name == name) return shipmentHeaderCollection[i].no;
                i++;
            }
            return "";
        }

        public static DataPickConfig fromDOM(XmlElement pickConfigElement)
        {
            DataPickConfig dataPickConfig = new DataPickConfig();


            dataPickConfig.userNo = pickConfigElement.GetAttribute("userNo");
            dataPickConfig.fromBin = pickConfigElement.GetAttribute("fromBin");
            dataPickConfig.toBin = pickConfigElement.GetAttribute("toBin");
            dataPickConfig.maxOrderCount = int.Parse(pickConfigElement.GetAttribute("maxOrderCount"));

            XmlElement statusElement = (XmlElement)pickConfigElement.SelectSingleNode("counts");
            dataPickConfig.countBulkOrders = int.Parse(statusElement.GetAttribute("countBulkOrders"));
            dataPickConfig.countPickOrders = int.Parse(statusElement.GetAttribute("countPickOrders"));
            dataPickConfig.countOrderLines = int.Parse(statusElement.GetAttribute("countOrderLines"));

            XmlElement createdPickListNoElement = (XmlElement)pickConfigElement.SelectSingleNode("createdPickListNo");
            if (createdPickListNoElement != null)
            {
                if (createdPickListNoElement.FirstChild != null)
                {
                    dataPickConfig.createdPickListNo = createdPickListNoElement.FirstChild.Value;
                }
            }

            XmlNodeList usersNodeList = pickConfigElement.SelectNodes("users/user");
            int i = 0;
            while (i < usersNodeList.Count)
            {
                XmlElement userElement = (XmlElement)usersNodeList[i];

                DataUser dataUser = new DataUser(userElement.GetAttribute("code"), userElement.FirstChild.Value);
                dataPickConfig.userCollection.Add(dataUser);                

                i++;
            }

            XmlNodeList wagonsNodeList = pickConfigElement.SelectNodes("wagons/wagon");
            
            i = 0;
            while (i < wagonsNodeList.Count)
            {
                XmlElement wagonElement = (XmlElement)wagonsNodeList[i];

                DataWagon dataWagon = new DataWagon(wagonElement.FirstChild.Value, int.Parse(wagonElement.GetAttribute("noOfOrders")));
                dataPickConfig.wagonCollection.Add(dataWagon);

                i++;
            }

            XmlNodeList shipmentHeaderNodeList = pickConfigElement.SelectNodes("shipmentHeaders/shipmentHeader");

            i = 0;
            while (i < shipmentHeaderNodeList.Count)
            {
                XmlElement shipmentHeaderElement = (XmlElement)shipmentHeaderNodeList[i];

                DataShipmentHeader dataShipmentHeader = new DataShipmentHeader(shipmentHeaderElement);
                
                dataPickConfig.shipmentHeaderCollection.Add(dataShipmentHeader);

                i++;
            }

            return dataPickConfig;
        }


        #region ServiceArgument Members

        public XmlElement toDOM(XmlDocument xmlDocumentContext)
        {
            XmlElement docElement = xmlDocumentContext.CreateElement("pickConfig");

            docElement.SetAttribute("userNo", _userNo);
            docElement.SetAttribute("fromBin", _fromBin);
            docElement.SetAttribute("toBin", _toBin);
            docElement.SetAttribute("maxOrderCount", _maxOrderCount.ToString());
            docElement.SetAttribute("createPickHeaders", _createPickHeaders.ToString());
            docElement.SetAttribute("wagonNo", _wagonNo);
            docElement.SetAttribute("shipmentNo", _shipmentNo);


            return docElement;

        }

        #endregion
    }

}
