using Api.Library;
using Api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Xml;

[RoutePrefix("api/inventory")]
public class InventoryController : ApiController
{
    // POST api/items
    [HeaderAuthorization]
    public async Task<bool> Post()
    {
        string xml = await Request.Content.ReadAsStringAsync();

        List<Inventory> inventoryList = new List<Inventory>();

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xml);


        XmlElement docElement = xmlDoc.DocumentElement;

        XmlNodeList locationNodeList = docElement.SelectNodes("location");
        foreach(XmlNode locationNode in locationNodeList)
        {
 
            XmlElement locationElement = (XmlElement)locationNode;
            string plant = locationElement.GetAttribute("plant");
            string locationCode = "";

            if (plant == "5000") locationCode = "CL";
            if (plant == "5060") locationCode = "VÄXJÖ";
            if (plant == "5080") locationCode = "KALMAR";


            XmlNodeList itemNodeList = locationElement.SelectNodes("item");
            foreach (XmlNode itemNode in itemNodeList)
            {
                XmlElement itemElement = (XmlElement)itemNode;
                string sku = itemElement.GetAttribute("sku");
                string itemNo = sku;
                string variantCode = "";
                if (sku.IndexOf(" ") > 0)
                {
                    itemNo = sku.Substring(0, sku.IndexOf(" "));
                    variantCode = sku.Substring(sku.IndexOf(" ") + 1);
                }

                string quantityOnStockStr = XmlHelper.GetNodeValue(itemElement, "quantityOnStock");
                string availableQuantityStr = XmlHelper.GetNodeValue(itemElement, "availableQuantity");
                decimal quantityOnStock = 0;
                decimal availableQuantity = 0;
                decimal.TryParse(availableQuantityStr, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out quantityOnStock);
                decimal.TryParse(availableQuantityStr, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out availableQuantity);


                string dateStr = "";
                decimal incomingQty = 0;
                DateTime nextReceiptDate = DateTime.Parse("2001-01-01");

                XmlElement incomingElement = (XmlElement)itemElement.SelectSingleNode("incoming");
                if (incomingElement != null)
                {
                    string incomingQtyStr = "";
                    dateStr = incomingElement.GetAttribute("date");
                    incomingQtyStr = incomingElement.GetAttribute("availableQuantity");
                    decimal.TryParse(incomingQtyStr, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out incomingQty);
                    nextReceiptDate = DateTime.Parse(dateStr);
                }

                Inventory inv = new Inventory();
                inv.id = sku + "-" + locationCode;
                inv.sku = sku;
                inv.itemNo = itemNo;
                inv.variantCode = variantCode;
                inv.locationCode = locationCode;
                inv.quantity = availableQuantity;
                inv.nextReceiptDate = nextReceiptDate;
                inv.nextReceiptQty = incomingQty;
                inventoryList.Add(inv);

            }

        }
        
        if (inventoryList.Count > 0)
        {
            RequestHandler.PushInventories(inventoryList);
        }

        return true;
    }
}
 