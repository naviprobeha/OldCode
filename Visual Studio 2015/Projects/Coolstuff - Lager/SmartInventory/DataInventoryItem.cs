using System;
using System.Data.SqlServerCe;
using System.Data;
using System.Xml;

namespace Navipro.SmartInventory
{
    /// <summary>
    /// Summary description for DataSalesLine.
    /// </summary>
    public class DataInventoryItem : ServiceArgument
    {
        private string _binCode = "";
        private string _eanCode = "";
        private string _brand = "";
        private string _itemNo = "";
        private string _variantCode = "";
        private string _description = "";
        private string _description2 = "";
        private float _quantity = 0;
        private bool _setQuantity = false;
        private string _wagonCode = "";
        private string _userNo = "";

        public DataInventoryItem(SmartDatabase smartDatabase)
        {
            //
            // TODO: Add constructor logic here
            //

        }

        public string binCode { get { return _binCode; } set { _binCode = value; } }
        public string eanCode { get { return _eanCode; } set { _eanCode = value; } }
        public string brand { get { return _brand; } set { _brand = value; } }
        public string userNo { get { return _userNo; } set { _userNo = value; } }
        public string itemNo { get { return _itemNo; } set { _itemNo = value; } }
        public string variantCode { get { return _variantCode; } set { _variantCode = value; } }
        public string description { get { return _description; } set { _description = value; } }
        public string description2 { get { return _description2; } set { _description2 = value; } }
        public string wagonCode { get { return _wagonCode; } set { _wagonCode = value; } }
        public float quantity { get { return _quantity; } set { _quantity = value; } }
        public bool setQuantity { get { return _setQuantity; } set { _setQuantity = value; } }



        public static void fromDOM(XmlElement pickLinesElement, SmartDatabase smartDatabase)
        {
            XmlElement pickLineElement = (XmlElement)pickLinesElement.SelectSingleNode("inventoryLine");

            string binCode = pickLineElement.SelectSingleNode("binCode").FirstChild.Value;
            string brand = pickLineElement.SelectSingleNode("brand").FirstChild.Value;
            string itemNo = pickLineElement.SelectSingleNode("itemNo").FirstChild.Value;
            string variantCode = pickLineElement.SelectSingleNode("variantCode").FirstChild.Value;
            string description = pickLineElement.SelectSingleNode("description").FirstChild.Value;
            string description2 = pickLineElement.SelectSingleNode("description2").FirstChild.Value;
            float quantity = float.Parse(pickLineElement.SelectSingleNode("quantity").FirstChild.Value);

            DataPickLine pickLine = new DataPickLine(smartDatabase);
            pickLine.binCode = binCode;
            pickLine.brand = brand;
            pickLine.itemNo = itemNo;
            pickLine.variantCode = variantCode;
            pickLine.description = description;
            pickLine.description2 = description2;
            pickLine.quantity = quantity;


        }

        #region ServiceArgument Members

        public XmlElement toDOM(XmlDocument xmlDocumentContext)
        {
            XmlElement docElement = xmlDocumentContext.CreateElement("inventoryItem");

            docElement.SetAttribute("binCode", _binCode);
            docElement.SetAttribute("eanCode", _eanCode);
            docElement.SetAttribute("quantity", _quantity.ToString());
            docElement.SetAttribute("wagonCode", _wagonCode);
            docElement.SetAttribute("userNo", _userNo);
            if (setQuantity) docElement.SetAttribute("setQuantity", "true");
            if (!setQuantity) docElement.SetAttribute("setQuantity", "false");

            return docElement;
        }

        #endregion
    }

}
