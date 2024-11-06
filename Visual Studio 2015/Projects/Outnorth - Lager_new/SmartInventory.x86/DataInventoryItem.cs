using System;
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

        public DataInventoryItem()
        {
            //
            // TODO: Add constructor logic here
            //

        }

        public string binCode { get { return _binCode; } set { _binCode = value; } }
        public string eanCode { get { return _eanCode; } set { _eanCode = value; } }
        public string brand { get { return _brand; } set { _brand = value; } }
        public string itemNo { get { return _itemNo; } set { _itemNo = value; } }
        public string variantCode { get { return _variantCode; } set { _variantCode = value; } }
        public string description { get { return _description; } set { _description = value; } }
        public string description2 { get { return _description2; } set { _description2 = value; } }
        public float quantity { get { return _quantity; } set { _quantity = value; } }
        public bool setQuantity { get { return _setQuantity; } set { _setQuantity = value; } }



        public static void fromDOM(XmlElement pickLinesElement)
        {
            XmlElement invLineElement = (XmlElement)pickLinesElement.SelectSingleNode("inventoryLine");

            string binCode = invLineElement.SelectSingleNode("binCode").FirstChild.Value;
            string brand = invLineElement.SelectSingleNode("brand").FirstChild.Value;
            string itemNo = invLineElement.SelectSingleNode("itemNo").FirstChild.Value;
            string variantCode = invLineElement.SelectSingleNode("variantCode").FirstChild.Value;
            string description = invLineElement.SelectSingleNode("description").FirstChild.Value;
            string description2 = invLineElement.SelectSingleNode("description2").FirstChild.Value;
            float quantity = float.Parse(invLineElement.SelectSingleNode("quantity").FirstChild.Value);

            DataInventoryItem dataInvItem = new DataInventoryItem();
            dataInvItem.binCode = binCode;
            dataInvItem.brand = brand;
            dataInvItem.itemNo = itemNo;
            dataInvItem.variantCode = variantCode;
            dataInvItem.description = description;
            dataInvItem.description2 = description2;
            dataInvItem.quantity = quantity;


        }

        #region ServiceArgument Members

        public XmlElement toDOM(XmlDocument xmlDocumentContext)
        {
            XmlElement docElement = xmlDocumentContext.CreateElement("inventoryItem");

            docElement.SetAttribute("binCode", _binCode);
            docElement.SetAttribute("eanCode", _eanCode);
            docElement.SetAttribute("quantity", _quantity.ToString());
            if (setQuantity) docElement.SetAttribute("setQuantity", "true");
            if (!setQuantity) docElement.SetAttribute("setQuantity", "false");

            return docElement;
        }

        #endregion
    }

}
