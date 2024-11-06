using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Konvex.SmartShipping.DataObjects
{
    public class LineOrderContainer
    {
        private int _entryNo;
        private int _lineOrderEntryNo;
        private string _containerNo;
        private string _categoryCode;
        private float _weight;


        public LineOrderContainer()
        {
        }
        
        public LineOrderContainer(Navipro.SantaMonica.Common.LineOrderContainer lineOrderContainer)
        {
            this._entryNo = lineOrderContainer.entryNo;
            this._lineOrderEntryNo = lineOrderContainer.lineOrderEntryNo;
            this._containerNo = lineOrderContainer.containerNo;
            this._categoryCode = lineOrderContainer.categoryCode;
            this._weight = lineOrderContainer.weight;

        }

        public int entryNo { get { return _entryNo; } set { _entryNo = value; } }
        public int lineOrderEntryNo { get { return _lineOrderEntryNo; } set { _lineOrderEntryNo = value; } }
        public string containerNo { get { return _containerNo; } set { _containerNo = value; } }
        public string categoryCode { get { return _categoryCode; } set { _categoryCode = value; } }
        public float weight { get { return _weight; } set { _weight = value; } }



    }
}
