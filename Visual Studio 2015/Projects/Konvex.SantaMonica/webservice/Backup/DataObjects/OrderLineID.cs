﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Konvex.SmartShipping.DataObjects
{
    public class OrderLineID
    {
        private int _entryNo;
        private int _orderEntryNo;
        private int _orderLineEntryNo;
        private string _unitId;
        private int _type;
        private string _reMarkUnitId;
        private bool _bseTesting;
        private bool _postMortem;
        private string _agentCode;

        public OrderLineID()
        {
        }

        public OrderLineID(System.Data.DataRow orderLineIdDataSet)
        {
            //[Entry No], [Ship Order Entry No], [Ship Order Line Entry No], [Unit ID], [BSE Testing], [Post Mortem]
            this._entryNo = int.Parse(orderLineIdDataSet.ItemArray.GetValue(0).ToString());
            this._orderEntryNo = int.Parse(orderLineIdDataSet.ItemArray.GetValue(1).ToString());
            this._orderLineEntryNo = int.Parse(orderLineIdDataSet.ItemArray.GetValue(2).ToString());
            this._unitId = orderLineIdDataSet.ItemArray.GetValue(3).ToString();
            if (int.Parse(orderLineIdDataSet.ItemArray.GetValue(4).ToString()) == 1) _bseTesting = true;
            if (int.Parse(orderLineIdDataSet.ItemArray.GetValue(5).ToString()) == 1) _postMortem = true;
            
        }

        public int entryNo { get { return _entryNo; } set { _entryNo = value; } }
        public int orderEntryNo { get { return _orderEntryNo; } set { _orderEntryNo = value; } }
        public int orderLineEntryNo { get { return _orderLineEntryNo; } set { _orderLineEntryNo = value; } }
        public string unitId { get { return _unitId; } set { _unitId = value; } }
        public int type { get { return _type; } set { _type = value; } }
        public string reMarkUnitId { get { return _reMarkUnitId; } set { _reMarkUnitId = value; } }
        public bool bseTesting { get { return _bseTesting; } set { _bseTesting = value; } }
        public bool postMortem { get { return _postMortem; } set { _postMortem = value; } }
        public string agentCode { get { return _agentCode; } set { _agentCode = value; } }

    }
}