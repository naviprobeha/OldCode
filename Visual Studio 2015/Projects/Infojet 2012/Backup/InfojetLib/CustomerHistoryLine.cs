using System;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for CustomerHistoryLine.
	/// </summary>
	public class CustomerHistoryLine
	{
		private string _no;
        private string _description;
        private string _unitOfMeasureCode;
        private string _quantity;
        private string _unitPrice;
        private string _amount;
        private string _amountInclVat;
        private string _shipmentDate;


		public CustomerHistoryLine()
		{
			//
			// TODO: Add constructor logic here
			//
		}

        public string no { get { return _no; } set { _no = value; } }
        public string description { get { return _description; } set { _description = value; } }
        public string unitOfMeasureCode { get { return _unitOfMeasureCode; } set { _unitOfMeasureCode = value; } }
        public string quantity { get { return _quantity; } set { _quantity = value; } }
        public string unitPrice { get { return _unitPrice; } set { _unitPrice = value; } }
        public string amount { get { return _amount; } set { _amount = value; } }
        public string amountInclVat { get { return _amountInclVat; } set { _amountInclVat = value; } }
        public string shipmentDate { get { return _shipmentDate; } set { _shipmentDate = value; } }

	}
}
