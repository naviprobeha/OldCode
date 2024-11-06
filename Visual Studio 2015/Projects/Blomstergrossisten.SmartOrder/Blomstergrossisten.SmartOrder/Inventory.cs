using System;
using System.Xml;

namespace SmartOrder
{
    /// <summary>
    /// Summary description for Inventory.
    /// </summary>
    public class Inventory
    {
        private Logger logger;
        private SmartDatabase smartDatabase;
        private DataItem itemObject;
        private XmlElement xmlInventoryElement;

        public Inventory(SmartDatabase smartDatabase, Logger logger)
        {
            this.smartDatabase = smartDatabase;
            this.logger = logger;
            //
            // TODO: Add constructor logic here
            //
        }

        public Inventory(XmlElement inventoryElement, SmartDatabase smartDatabase, Logger logger)
        {
            this.logger = logger;
            fromDOM(inventoryElement, smartDatabase);
        }

        public DataItem item
        {
            get
            {
                return itemObject;
            }
        }

        public XmlElement xmlInventory
        {
            get
            {
                return xmlInventoryElement;
            }
        }


        public void fromDOM(XmlElement inventoryElement, SmartDatabase smartDatabase)
        {
            this.xmlInventoryElement = inventoryElement;

            XmlNodeList items = inventoryElement.GetElementsByTagName("ITEM");
            if (items.Count > 0)
            {
                itemObject = new DataItem((XmlElement)items.Item(0), smartDatabase);
            }

        }

    }
}
