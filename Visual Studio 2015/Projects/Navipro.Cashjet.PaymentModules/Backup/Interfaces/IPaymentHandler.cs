using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Navipro.Cashjet.PaymentModules.Interfaces
{
    public interface IPaymentHandler
    {
        void startTransaction(string type, decimal amount, decimal vat, decimal cashback);
        void setDisplayText(int lineNo, string text);
        void setPrintText(string dataType, string code, string description, string value);
        void raiseError(string errorMessage);
        void cancelTransaction();
        void setResponseData(string code, string value);
        void setStatus(int status);
        void closePaymentModule();
        void notify(string text);
        string getReferalValue(string label);
        void transactionLog(string reportNo, int entryNo, int reportType, int noOfTransactions);
    }
}
