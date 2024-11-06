using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Navipro.Cashjet.PaymentModules.Interfaces
{
    public interface IPaymentModule
    {
        bool startTransaction(string type, decimal amount, decimal vat, decimal cashback);
        void cancel();
        void endTransaction();
        void close();
        void endOfDay();
        void reversePrevTransaction();
        void transactionLog(int reportType, int noOfTransactions);
    }
}
