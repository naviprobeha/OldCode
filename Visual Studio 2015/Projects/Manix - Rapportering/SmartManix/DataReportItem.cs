using System;
using System.Data.SqlServerCe;
using System.Data;
using System.Xml;

namespace Navipro.SmartInventory
{
    /// <summary>
    /// Summary description for DataSalesLine.
    /// </summary>
    public class DataReportItem : ServiceArgument
    {
        private string _id = "";
        private string _userCode = "";
        private string _prodOrderNo = "";
        private int _prodOrderLineNo = 0;
        private string _itemNo = "";
        private string _description = "";
        private string _status = "";

        public DataReportItem()
        {

            //
            // TODO: Add constructor logic here
            //
        }

        public string id { get { return _id; } set { _id = value; } }
        public string userCode { get { return _userCode; } set { _userCode = value; } }
        public string prodOrderNo { get { return _prodOrderNo; } set { _prodOrderNo = value; } }
        public int prodOrderLineNo { get { return _prodOrderLineNo; } set { _prodOrderLineNo = value; } }
        public string itemNo { get { return _itemNo; } set { _itemNo = value; } }
        public string description { get { return _description; } set { _description = value; } }
        public string status { get { return _status; } set { _status = value; } }


        public static DataReportItem fromDOM(XmlElement reportItemElement)
        {
            DataReportItem dataReportItem = new DataReportItem();

            dataReportItem.userCode = reportItemElement.GetAttribute("userCode");
            dataReportItem.id = reportItemElement.GetAttribute("id");
            dataReportItem.prodOrderNo = reportItemElement.GetAttribute("prodOrderNo");
            dataReportItem.prodOrderLineNo = int.Parse(reportItemElement.GetAttribute("prodOrderLineNo"));
            dataReportItem.itemNo = reportItemElement.GetAttribute("itemNo");
            dataReportItem.description = reportItemElement.GetAttribute("description");
            dataReportItem.status = reportItemElement.GetAttribute("routingStatus");

            return dataReportItem;
        }

        #region ServiceArgument Members

        public XmlElement toDOM(XmlDocument xmlDocumentContext)
        {
            XmlElement docElement = xmlDocumentContext.CreateElement("reportItem");

            docElement.SetAttribute("userCode", userCode);
            docElement.SetAttribute("id", id);

            return docElement;
        }

        #endregion
    }

}
