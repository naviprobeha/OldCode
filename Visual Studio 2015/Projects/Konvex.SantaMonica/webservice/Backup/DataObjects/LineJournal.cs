using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Konvex.SmartShipping.DataObjects
{
    public class LineJournal
    {
        private int _entryNo;
        private string _organizationNo;
        private DateTime _shipDate;
        private string _agentCode;
        private int _status;
        private string _departureFactoryCode;
        private string _arrivalFactoryCode;
        private DateTime _arrivalDateTime;
        private decimal _calculatedDistance;
        private decimal _measuredDistance;
        private decimal _reportedDistance;
        private decimal _reportedDistanceSingle;
        private decimal _reportedDistanceTrailer;
        private int _endingTravelDistance;
        private int _endingTravelTime;
        private int _totalDistance;
        private int _totalTime;
        private DateTime _departureDateTime;
        private int _dropWaitTime;
        private string _dropWaitReason;

        public LineJournal()
        {
        }
        
        public LineJournal(Navipro.SantaMonica.Common.LineJournal lineJournal)
        {
            this._entryNo = lineJournal.entryNo;
            this._organizationNo = lineJournal.organizationNo;
            this._shipDate = lineJournal.shipDate;
            this._agentCode = lineJournal.agentCode;
            this._status = lineJournal.status;
            this._departureFactoryCode = lineJournal.departureFactoryCode;
            this._arrivalFactoryCode = lineJournal.arrivalFactoryCode;
            this._arrivalDateTime = lineJournal.arrivalDateTime;
            this._calculatedDistance = lineJournal.calculatedDistance;
            this._measuredDistance = lineJournal.measuredDistance;
            this._reportedDistance = lineJournal.reportedDistance;
            this._reportedDistanceSingle = lineJournal.reportedDistanceSingle;
            this._reportedDistanceTrailer = lineJournal.reportedDistanceTrailer;
            this._endingTravelDistance = lineJournal.endingTravelDistance;
            this._endingTravelTime = lineJournal.endingTravelTime;
            this._totalDistance = lineJournal.totalDistance;
            this._totalTime = lineJournal.totalTime;
            this._departureDateTime = lineJournal.departureDateTime;
            this._dropWaitTime = lineJournal.dropWaitTime;
        }


        public int entryNo { get { return _entryNo; } set { _entryNo = value; } }
        public string organizationNo { get { return _organizationNo; } set { _organizationNo = value; } }
        public DateTime shipDate { get { return _shipDate; } set { _shipDate = value; } }
        public string agentCode { get { return _agentCode; } set { _agentCode = value; } }
        public int status { get { return _status; } set { _status = value; } }
        public string departureFactoryCode { get { return _departureFactoryCode; } set { _departureFactoryCode = value; } }
        public string arrivalFactoryCode { get { return _arrivalFactoryCode; } set { _arrivalFactoryCode = value; } }
        public DateTime arrivalDateTime { get { return _arrivalDateTime; } set { _arrivalDateTime = value; } }
        public decimal calculatedDistance { get { return _calculatedDistance; } set { _calculatedDistance = value; } }
        public decimal measuredDistance { get { return _measuredDistance; } set { _measuredDistance = value; } }
        public decimal reportedDistance { get { return _reportedDistance; } set { _reportedDistance = value; } }
        public decimal reportedDistanceSingle { get { return _reportedDistanceSingle; } set { _reportedDistanceSingle = value; } }
        public decimal reportedDistanceTrailer { get { return _reportedDistanceTrailer; } set { _reportedDistanceTrailer = value; } }
        public int endingTravelDistance { get { return _endingTravelDistance; } set { _endingTravelDistance = value; } }
        public int endingTravelTime { get { return _endingTravelTime; } set { _endingTravelTime = value; } }
        public int totalDistance { get { return _totalDistance; } set { _totalDistance = value; } }
        public int totalTime { get { return _totalTime; } set { _totalTime = value; } }
        public DateTime departureDateTime { get { return _departureDateTime; } set { _departureDateTime = value; } }
        public int dropWaitTime { get { return _dropWaitTime; } set { _dropWaitTime = value; } }
        public string dropWaitReason { get { return _dropWaitReason; } set { _dropWaitReason = value; } }

    }
}
