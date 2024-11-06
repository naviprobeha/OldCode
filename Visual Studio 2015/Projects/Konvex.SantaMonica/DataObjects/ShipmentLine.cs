using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Konvex.SmartShipping.DataObjects
{
    public class ShipmentLine
    {
        private int _entryNo;
        private int _originalEntryNo;
        private string _shipmentNo;
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
        private bool _extraPayment;
        private string _agentCode;

        private ShipmentLineIDCollection _shipmentLineIdCollection;

        public ShipmentLine()
        {
            _shipmentLineIdCollection = new ShipmentLineIDCollection();
        }

        public int entryNo { get { return _entryNo; } set { _entryNo = value; } }
        public int originalEntryNo { get { return _originalEntryNo; } set { _originalEntryNo = value; } }
        public string shipmentNo { get { return _shipmentNo; } set { _shipmentNo = value; } }
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
        public bool extraPayment { get { return _extraPayment; } set { _extraPayment = value; } }
        public string agentCode { get { return _agentCode; } set { _agentCode = value; } }
        public ShipmentLineIDCollection shipmentLineIdCollection { get { return _shipmentLineIdCollection; } set { _shipmentLineIdCollection = value; } }


        public JObject toJsonObject()
        {
            JObject jObject = new JObject();

            int qty = (int)quantity;

            jObject.Add("EntryNo", entryNo);
            jObject.Add("ShipmentNo", shipmentNo);
            jObject.Add("ItemNo", itemNo);
            jObject.Add("Description", description);
            jObject.Add("Quantity", qty);
            jObject.Add("QuantityPcs", qty);
            jObject.Add("QuantityWeight", "0");
            jObject.Add("UnitPrice", unitPrice.ToString("N", CultureInfo.InvariantCulture));
            jObject.Add("Amount", amount.ToString("N", CultureInfo.InvariantCulture));

            jObject.Add("DepItemNo", connectionItemNo);
            jObject.Add("DepQuantity", connectionQuantity.ToString());
            jObject.Add("DepUnitPrice", connectionUnitPrice.ToString("N", CultureInfo.InvariantCulture));
            jObject.Add("DepAmount", connectionAmount.ToString("N", CultureInfo.InvariantCulture));
            jObject.Add("SampleQuantity", testQuantity.ToString());
            jObject.Add("TotalAmount", totalAmount.ToString("N", CultureInfo.InvariantCulture));

            if (extraPayment)
            {
                jObject.Add("PenaltyPayment", 1);
            }
            else
            {
                jObject.Add("PenaltyPayment", 0);
            }
            
 

            JArray linesArray = new JArray();

            foreach (ShipmentLineID idLine in shipmentLineIdCollection)
            {
                linesArray.Add(idLine.toJsonObject());
            }

            jObject.Add("ids", linesArray);



            return jObject;
        }
    }


}
