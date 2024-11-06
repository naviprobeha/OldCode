﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Konvex.SmartShipping.DataObjects
{
    public class ShipOrderLine
    {
        private int _entryNo;
        private int _orderEntryNo;
        private string _itemNo;
        private string _description;
        private int _quantity;
        private int _connectionQuantity;
        private float _unitPrice;
        private float _amount;
        private float _connectionUnitPrice;
        private float _connectionAmount;
        private float _totalAmount;
        private string _connectionItemNo;
        private int _testQuantity;
        private string _agentCode;

        private ShipOrderLineIDCollection _shipOrderLineIdCollection;

        public ShipOrderLine()
        { }

        public ShipOrderLine(Navipro.SantaMonica.Common.ShipOrderLine shipOrderLine)
        {
            _shipOrderLineIdCollection = new ShipOrderLineIDCollection();

            this._entryNo = shipOrderLine.entryNo;
            this._orderEntryNo = shipOrderLine.shipOrderEntryNo;
            this._itemNo = shipOrderLine.itemNo;

            this._quantity = shipOrderLine.quantity;
            this._unitPrice = shipOrderLine.unitPrice;
            this._amount = shipOrderLine.amount;

            this._connectionItemNo = shipOrderLine.connectionItemNo;
            this._connectionQuantity = shipOrderLine.connectionQuantity;
            this._connectionUnitPrice = shipOrderLine.unitPrice;
            this._connectionAmount = shipOrderLine.connectionAmount;

            this._totalAmount = shipOrderLine.totalAmount;

            this._agentCode = "";
            this._description = "";
        }

        public int entryNo { get { return _entryNo; } set { _entryNo = value; } }
        public int orderEntryNo { get { return _orderEntryNo; } set { _orderEntryNo = value; } }
        public string itemNo { get { return _itemNo; } set { _itemNo = value; } }
        public string description { get { return _description; } set { _description = value; } }
        public int quantity { get { return _quantity; } set { _quantity = value; } }
        public int connectionQuantity { get { return _connectionQuantity; } set { _connectionQuantity = value; } }
        public float unitPrice { get { return _unitPrice; } set { _unitPrice = value; } }
        public float amount { get { return _amount; } set { _amount = value; } }
        public float connectionUnitPrice { get { return _connectionUnitPrice; } set { _connectionUnitPrice = value; } }
        public float connectionAmount { get { return _connectionAmount; } set { _connectionAmount = value; } }
        public float totalAmount { get { return _totalAmount; } set { _totalAmount = value; } }
        public string connectionItemNo { get { return _connectionItemNo; } set { _connectionItemNo = value; } }
        public int testQuantity { get { return _testQuantity; } set { _testQuantity = value; } }
        public string agentCode { get { return _agentCode; } set { _agentCode = value; } }
        public ShipOrderLineIDCollection shipOrderLineIdCollection { get { return _shipOrderLineIdCollection; } set { _shipOrderLineIdCollection = value; } }


        public static ShipOrderLine fromJsonObject(JObject jsonObject)
        {
            JToken jsonToken = null;

            

            ShipOrderLine entry = new ShipOrderLine();
            entry.entryNo = (int)jsonObject["entryNo"];
            entry.orderEntryNo = (int)jsonObject["shipOrderEntryNo"];
            entry.itemNo = (string)jsonObject["itemNo"];
            entry.description = (string)jsonObject["description"];
            entry.connectionItemNo = (string)jsonObject["depItemNo"];

            if (entry.description.Length > 30) entry.description = entry.description.Substring(0, 30);

            if (jsonObject.TryGetValue("quantity", out jsonToken))
            {                
                entry.quantity = int.Parse(((JValue)jsonToken).Value.ToString());
            }

            if (jsonObject.TryGetValue("unitPrice", out jsonToken))
            {
                entry.unitPrice = float.Parse(((JValue)jsonToken).Value.ToString());
            }
            if (jsonObject.TryGetValue("amount", out jsonToken))
            {
                entry.amount = float.Parse(((JValue)jsonToken).Value.ToString());
            }

            if (jsonObject.TryGetValue("depQuantity", out jsonToken))
            {
                entry.connectionQuantity = int.Parse(((JValue)jsonToken).Value.ToString());
            }

            if (jsonObject.TryGetValue("depUnitPrice", out jsonToken))
            {
                entry.connectionUnitPrice = float.Parse(((JValue)jsonToken).Value.ToString());
            }
            if (jsonObject.TryGetValue("depAmount", out jsonToken))
            {
                entry.connectionAmount = float.Parse(((JValue)jsonToken).Value.ToString());
            }

            if (jsonObject.TryGetValue("totalAmount", out jsonToken))
            {
                entry.totalAmount = float.Parse(((JValue)jsonToken).Value.ToString());
            }

            if (jsonObject.TryGetValue("sampleQuantity", out jsonToken))
            {
                entry.testQuantity = int.Parse(((JValue)jsonToken).Value.ToString());
            }


            entry.shipOrderLineIdCollection = new ShipOrderLineIDCollection();

            JArray idArray = (JArray)jsonObject["ids"];
            if (idArray != null)
            {
                foreach (JToken jToken in idArray)
                {
                    JObject idObject = (JObject)jToken;

                    ShipOrderLineID id = ShipOrderLineID.fromJsonObject(idObject);
                    if (entry.testQuantity > 0) id.bseTesting = true;

                    entry.shipOrderLineIdCollection.Add(id);
                }
            }

            return entry;

        }


        public JObject toJsonObject()
        {
            JObject jObject = new JObject();

            int qty = (int)quantity;

            if (description.Length > 30) description = description.Substring(0, 30);

            jObject.Add("EntryNo", entryNo);
            jObject.Add("ItemNo", itemNo);
            jObject.Add("Description", description);
            jObject.Add("Quantity", qty);
            jObject.Add("QuantityWeight", "0");
            jObject.Add("UnitPrice", unitPrice.ToString("N", CultureInfo.InvariantCulture));
            jObject.Add("Amount", amount.ToString("N", CultureInfo.InvariantCulture));

            jObject.Add("DepItemNo", connectionItemNo);
            jObject.Add("DepQuantity", connectionQuantity.ToString());
            jObject.Add("DepUnitPrice", connectionUnitPrice.ToString("N", CultureInfo.InvariantCulture));
            jObject.Add("DepAmount", connectionAmount.ToString("N", CultureInfo.InvariantCulture));
            jObject.Add("SampleQuantity", testQuantity.ToString());
            jObject.Add("TotalAmount", totalAmount.ToString("N", CultureInfo.InvariantCulture));

            JArray linesArray = new JArray();

            foreach (ShipOrderLineID idLine in shipOrderLineIdCollection)
            {
                linesArray.Add(idLine.toJsonObject());
            }

            jObject.Add("ids", linesArray);



            return jObject;
        }
    }


}