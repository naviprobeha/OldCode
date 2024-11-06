using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Konvex.SmartShipping.Goldfinger
{
    public class ShipmentLineID
    {
        private int _entryNo;
        private int _originalEntryNo;
        private string _shipmentNo;
        private int _shipmentLineEntryNo;
        private string _unitId;
        private int _type;
        private string _reMarkUnitId;
        private bool _bseTesting;
        private bool _postMortem;
        private string _agentCode;

        public int entryNo { get { return _entryNo; } set { _entryNo = value; } }
        public int originalEntryNo { get { return _originalEntryNo; } set { _originalEntryNo = value; } }
        public string shipmentNo { get { return _shipmentNo; } set { _shipmentNo = value; } }
        public int shipmentLineEntryNo { get { return _shipmentLineEntryNo; } set { _shipmentLineEntryNo = value; } }
        public string unitId { get { return _unitId; } set { _unitId = value; } }
        public int type { get { return _type; } set { _type = value; } }
        public string reMarkUnitId { get { return _reMarkUnitId; } set { _reMarkUnitId = value; } }
        public bool bseTesting { get { return _bseTesting; } set { _bseTesting = value; } }
        public bool postMortem { get { return _postMortem; } set { _postMortem = value; } }
        public string agentCode { get { return _agentCode; } set { _agentCode = value; } }

    }
}
