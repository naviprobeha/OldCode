using System;

namespace Navipro.Infojet.Lib
{
    /// <summary>
    /// Summary description for CustomerHistoryLine.
    /// </summary>
    public class CustomerHistoryLedger
    {
        private int _documentType;
        private string _documentTypeDescription;
        private string _documentNo;
        private string _description;
        private string _amount;
        private string _remainingAmount;
        private bool _open;
        private string _openText;


        public CustomerHistoryLedger()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int documentType { get { return _documentType; } set { _documentType = value; } }
        public string documentTypeDescription { get { return _documentTypeDescription; } set { _documentTypeDescription = value; } }
        public string documentNo { get { return _documentNo; } set { _documentNo = value; } }
        public string description { get { return _description; } set { _description = value; } }
        public string amount { get { return _amount; } set { _amount = value; } }
        public string remainingAmount { get { return _remainingAmount; } set { _remainingAmount = value; } }
        public bool open { get { return _open; } set { _open = value; } }
        public string openText { get { return _openText; } set { _openText = value; } }

    }
}
