using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Konvex.SmartShipping.Goldfinger
{
    public class OrderHeader
    {
        private string _organizationNo;
        private int _entryNo;
        private DateTime _shipDate;
        private string _customerNo;
        private string _customerName;
        private string _address;
        private string _address2;
        private string _postCode;
        private string _city;
        private string _countryCode;
        private string _phoneNo;
        private string _cellPhoneNo;
        private string _details;
        private string _comments;
        private int _priority;
        private string _billToCustomerNo;
        private int _paymentType;
        private string _customerShipAddressNo;
        private string _shipName;
        private string _shipAddress;
        private string _shipAddress2;
        private string _shipPostCode;
        private string _shipCity;
        private string _directionComment;
        private string _directionComment2;
        private int _positionX;
        private int _positionY;
        private string _agentCode;
        private int _status;
        private DateTime _closedDate;
        private DateTime _shipTime;
        private DateTime _creationDate;
        private int _createdBy;
        private string _productionSite;

        public OrderHeader()
        {
        }

        public OrderHeader(DataSet dataset)
        {
            this.organizationNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            this.entryNo = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());
            this.shipDate = DateTime.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(2).ToString());
            this.customerNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
            this.customerName = dataset.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
            this.address = dataset.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
            this.address2 = dataset.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
            this.postCode = dataset.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
            this.city = dataset.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
            this.phoneNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
            this.cellPhoneNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
            this.details = dataset.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
            this.comments = dataset.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
            this.priority = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(13).ToString());
            this.status = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(14).ToString());
            this.positionX = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(15).ToString());
            this.positionY = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(16).ToString());
            this.billToCustomerNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(17).ToString();
            this.customerShipAddressNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(18).ToString();
            this.shipName = dataset.Tables[0].Rows[0].ItemArray.GetValue(19).ToString();
            this.shipAddress = dataset.Tables[0].Rows[0].ItemArray.GetValue(20).ToString();
            this.shipAddress2 = dataset.Tables[0].Rows[0].ItemArray.GetValue(21).ToString();
            this.shipPostCode = dataset.Tables[0].Rows[0].ItemArray.GetValue(22).ToString();
            this.shipCity = dataset.Tables[0].Rows[0].ItemArray.GetValue(23).ToString();
            this.directionComment = dataset.Tables[0].Rows[0].ItemArray.GetValue(24).ToString();
            this.directionComment2 = dataset.Tables[0].Rows[0].ItemArray.GetValue(25).ToString();
            this.paymentType = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(26).ToString());
            this.creationDate = DateTime.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(27).ToString());
            this.productionSite = dataset.Tables[0].Rows[0].ItemArray.GetValue(28).ToString();

        }


        public string organizationNo { get { return _organizationNo; } set { _organizationNo = value; } }
        public int entryNo { get { return _entryNo; } set { _entryNo = value; } }
        public DateTime shipDate { get { return _shipDate; } set { _shipDate = value; } }
        public string customerNo { get { return _customerNo; } set { _customerNo = value; } }
        public string customerName { get { return _customerName; } set { _customerName = value; } }
        public string address { get { return _address; } set { _address = value; } }
        public string address2 { get { return _address2; } set { _address2 = value; } }
        public string postCode { get { return _postCode; } set { _postCode = value; } }
        public string city { get { return _city; } set { _city = value; } }
        public string countryCode { get { return _countryCode; } set { _countryCode = value; } }
        public string phoneNo { get { return _phoneNo; } set { _phoneNo = value; } }
        public string cellPhoneNo { get { return _cellPhoneNo; } set { _cellPhoneNo = value; } }
        public string details { get { return _details; } set { _details = value; } }
        public string comments { get { return _comments; } set { _comments = value; } }
        public int priority { get { return _priority; } set { _priority = value; } }
        public string billToCustomerNo { get { return _billToCustomerNo; } set { _billToCustomerNo = value; } }
        public int paymentType { get { return _paymentType; } set { _paymentType = value; } }
        public string customerShipAddressNo { get { return _customerShipAddressNo; } set { _customerShipAddressNo = value; } }
        public string shipName { get { return _shipName; } set { _shipName = value; } }
        public string shipAddress { get { return _shipAddress; } set { _shipAddress = value; } }
        public string shipAddress2 { get { return _shipAddress2; } set { _shipAddress2 = value; } }
        public string shipPostCode { get { return _shipPostCode; } set { _shipPostCode = value; } }
        public string shipCity { get { return _shipCity; } set { _shipCity = value; } }
        public string directionComment { get { return _directionComment; } set { _directionComment = value; } }
        public string directionComment2 { get { return _directionComment2; } set { _directionComment2 = value; } }
        public int positionX { get { return _positionX; } set { _positionX = value; } }
        public int positionY { get { return _positionY; } set { _positionY = value; } }
        public string agentCode { get { return _agentCode; } set { _agentCode = value; } }
        public int status { get { return _status; } set { _status = value; } }
        public DateTime closedDate { get { return _closedDate; } set { _closedDate = value; } }
        public DateTime shipTime { get { return _shipTime; } set { _shipTime = value; } }
        public DateTime creationDate { get { return _creationDate; } set { _creationDate = value; } }
        public int createdBy { get { return _createdBy; } set { _createdBy = value; } }
        public string productionSite { get { return _productionSite; } set { _productionSite = value; } }

    }
}
