using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Konvex.SmartShipping.DataObjects
{
    public class LineOrder
    {
        private int _entryNo;
        private string _organizationNo;
        private int _lineJournalEntryNo;
        private DateTime _shipDate;
        private string _shippingCustomerNo;
        private string _shippingCustomerName;
        private string _address;
        private string _address2;
        private string _postCode;
        private string _city;
        private string _countryCode;
        private string _phoneNo;
        private string _cellPhoneNo;
        private string _details;
        private string _comments;
        private string _directionComment;
        private string _directionComment2;
        private int _optimizingSortOrder;
        private int _travelDistance;
        private int _travelTime;
        private int _positionX;
        private int _positionY;
        private int _type;
        private int _status;
        private DateTime _shipTime;
        private DateTime _creationDate;
        private int _createdByType;
        private string _createdByCode;
        private DateTime _confirmedToDateTime;
        private int _priority;
        private string _planningAgentCode;
        private string _driverName;
        private string _arrivalFactoryCode;

        private LineOrderContainerCollection _containers;

        public LineOrder()
        {
        }
        
        public LineOrder(Navipro.SantaMonica.Common.LineOrder lineOrder)
        {
            this._entryNo = lineOrder.entryNo;
            this._organizationNo = lineOrder.organizationNo;
            this._lineJournalEntryNo = lineOrder.lineJournalEntryNo;
            this._shipDate = lineOrder.shipDate;
            this._shippingCustomerNo = lineOrder.shippingCustomerNo;
            this._shippingCustomerName = lineOrder.shippingCustomerName;
            this._address = lineOrder.address;
            this._address2 = lineOrder.address2;
            this._postCode = lineOrder.postCode;
            this._city = lineOrder.city;
            this._countryCode = lineOrder.countryCode;
            this._phoneNo = lineOrder.phoneNo;
            this._cellPhoneNo = lineOrder.cellPhoneNo;
            this._details = lineOrder.details;
            this._comments = lineOrder.comments;
            this._directionComment = lineOrder.directionComment;
            this._directionComment2 = lineOrder.directionComment2;
            this._optimizingSortOrder = lineOrder.optimizingSortOrder;
            this._travelDistance = lineOrder.travelDistance;
            this._travelTime = lineOrder.travelTime;
            this._positionX = lineOrder.positionX;
            this._positionY = lineOrder.positionY;
            this._type = lineOrder.type;
            this._status = lineOrder.status;
            this._shipTime = lineOrder.shipTime;
            this._creationDate = lineOrder.creationDate;
            this._createdByType = lineOrder.createdByType;
            this._createdByCode = lineOrder.createdByCode;
            this._confirmedToDateTime = lineOrder.confirmedToDateTime;
            this._priority = lineOrder.priority;
            this._planningAgentCode = lineOrder.planningAgentCode;
            this._driverName = lineOrder.driverName;
            this._arrivalFactoryCode = lineOrder.arrivalFactoryCode;
        }

        public void applyLines(Navipro.SantaMonica.Common.Database database)
        {
            _containers = new LineOrderContainerCollection();

            Navipro.SantaMonica.Common.LineOrderContainers lineOrderContainers = new Navipro.SantaMonica.Common.LineOrderContainers();
            DataSet lineOrderContainerDataSet = lineOrderContainers.getDataSet(database, this.entryNo);


            int i = 0;
            while (i < lineOrderContainerDataSet.Tables[0].Rows.Count)
            {
                Navipro.SantaMonica.Common.LineOrderContainer lineOrderContainerData = new Navipro.SantaMonica.Common.LineOrderContainer(lineOrderContainerDataSet.Tables[0].Rows[i]);
                LineOrderContainer lineOrderContainer = new LineOrderContainer(lineOrderContainerData);


                _containers.Add(lineOrderContainer);

                i++;
            }

        }


        public int entryNo { get { return _entryNo; } set { _entryNo = value; } }
        public string organizationNo { get { return _organizationNo; } set { _organizationNo = value; } }
        public int lineJournalEntryNo { get { return _lineJournalEntryNo; } set { _lineJournalEntryNo = value; } }
        public DateTime shipDate { get { return _shipDate; } set { _shipDate = value; } }
        public string shippingCustomerNo { get { return _shippingCustomerNo; } set { _shippingCustomerNo = value; } }
        public string shippingCustomerName { get { return _shippingCustomerName; } set { _shippingCustomerName = value; } }
        public string address { get { return _address; } set { _address = value; } }
        public string address2 { get { return _address2; } set { _address2 = value; } }
        public string postCode { get { return _postCode; } set { _postCode = value; } }
        public string city { get { return _city; } set { _city = value; } }
        public string countryCode { get { return _countryCode; } set { _countryCode = value; } }
        public string phoneNo { get { return _phoneNo; } set { _phoneNo = value; } }
        public string cellPhoneNo { get { return _cellPhoneNo; } set { _cellPhoneNo = value; } }
        public string details { get { return _details; } set { _details = value; } }
        public string comments { get { return _comments; } set { _comments = value; } }
        public string directionComment { get { return _directionComment; } set { _directionComment = value; } }
        public string directionComment2 { get { return _directionComment2; } set { _directionComment2 = value; } }
        public int optimizingSortOrder { get { return _optimizingSortOrder; } set { _optimizingSortOrder = value; } }
        public int travelDistance { get { return _travelDistance; } set { _travelDistance = value; } }
        public int travelTime { get { return _travelTime; } set { _travelTime = value; } }
        public int positionX { get { return _positionX; } set { _positionX = value; } }
        public int positionY { get { return _positionY; } set { _positionY = value; } }
        public int type { get { return _type; } set { _type = value; } }
        public int status { get { return _status; } set { _status = value; } }
        public DateTime shipTime { get { return _shipTime; } set { _shipTime = value; } }
        public DateTime creationDate { get { return _creationDate; } set { _creationDate = value; } }
        public int createdByType { get { return _createdByType; } set { _createdByType = value; } }
        public string createdByCode { get { return _createdByCode; } set { _createdByCode = value; } }
        public DateTime confirmedToDateTime { get { return _confirmedToDateTime; } set { _confirmedToDateTime = value; } }
        public int priority { get { return _priority; } set { _priority = value; } }
        public string planningAgentCode { get { return _planningAgentCode; } set { _planningAgentCode = value; } }
        public string driverName { get { return _driverName; } set { _driverName = value; } }
        public string arrivalFactoryCode { get { return _arrivalFactoryCode; } set { _arrivalFactoryCode = value; } }
        public LineOrderContainerCollection containers { get { return _containers; } set { _containers = value; } }


    }
}
