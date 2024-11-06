using System;
using System.Data.SqlServerCe;
using System.Data;
using System.Xml;

namespace SmartOrder
{
    /// <summary>
    /// Summary description for DataSalesLine.
    /// </summary>
    public class DataInventoryItem : ServiceArgument
    {
        private string _eanCode = "";
        private string _itemNo = "";
        private string _description = "";
        private string _description2 = "";
        private float _quantity = 0;
        private bool _setQuantity = false;

        public DataInventoryItem(SmartDatabase smartDatabase)
        {
            //
            // TODO: Add constructor logic here
            //

        }

        public string eanCode { get { return _eanCode; } set { _eanCode = value; } }
        public string itemNo { get { return _itemNo; } set { _itemNo = value; } }
        public string description { get { return _description; } set { _description = value; } }
        public string description2 { get { return _description2; } set { _description2 = value; } }
        public float quantity { get { return _quantity; } set { _quantity = value; } }
        public bool setQuantity { get { return _setQuantity; } set { _setQuantity = value; } }



        public static void fromDOM(XmlElement pickLinesElement, SmartDatabase smartDatabase)
        {
            XmlElement pickLineElement = (XmlElement)pickLinesElement.SelectSingleNode("inventoryLine");

            string itemNo = pickLineElement.SelectSingleNode("itemNo").FirstChild.Value;
            string description = pickLineElement.SelectSingleNode("description").FirstChild.Value;
            string description2 = pickLineElement.SelectSingleNode("description2").FirstChild.Value;
            float quantity = float.Parse(pickLineElement.SelectSingleNode("quantity").FirstChild.Value);

        }

        #region ServiceArgument Members

        public XmlElement toDOM(XmlDocument xmlDocumentContext)
        {
            XmlElement docElement = xmlDocumentContext.CreateElement("inventoryItem");

            docElement.SetAttribute("eanCode", _eanCode);
            docElement.SetAttribute("quantity", _quantity.ToString());
            if (setQuantity) docElement.SetAttribute("setQuantity", "true");
            if (!setQuantity) docElement.SetAttribute("setQuantity", "false");

            return docElement;
        }

        #endregion
    }

}
