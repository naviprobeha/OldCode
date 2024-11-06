using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Konvex.SmartShipping.DataObjects
{
    public class OrderLine
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

        private OrderLineIDCollection _orderLineIdCollection;

        public OrderLine()
        { }

        public OrderLine(Navipro.SantaMonica.Common.ShipOrderLine shipOrderLine)
        {
            _orderLineIdCollection = new OrderLineIDCollection();

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
        public OrderLineIDCollection orderLineIdCollection { get { return _orderLineIdCollection; } set { _orderLineIdCollection = value; } }

    }


}
