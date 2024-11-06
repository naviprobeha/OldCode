using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Navipro.Cashjet.PaymentModules.CardPaymentVerifone
{
    public class PaymentModule : Navipro.Cashjet.PaymentModules.Interfaces.IPaymentModule
    {
        private Navipro.Cashjet.PaymentModules.Interfaces.IPaymentHandler paymentHandler;
        private PAYPOINTAPILib.paypointClass paypoint;
        private decimal amount;

        public PaymentModule(Navipro.Cashjet.PaymentModules.Interfaces.IPaymentHandler paymentHandler)
        {
            this.paymentHandler = paymentHandler;

            paypoint = new PAYPOINTAPILib.paypointClass();

            paypoint.OnStatus += new PAYPOINTAPILib.IpaypointEvents_OnStatusEventHandler(paypoint_OnStatus);
            paypoint.OnReturn += new PAYPOINTAPILib.IpaypointEvents_OnReturnEventHandler(paypoint_OnReturn);

            paypoint.setHomeDirectory("D:\\Betaltjänster\\Verifone\\Paypoint");

            int result = paypoint.open("", 0, "000010", 3, 1);
            if (result != 0) paymentHandler.raiseError("Failed to open connection to terminal: "+result);

            //result = paypoint.startTestCom();
            //if (result != 0) paymentHandler.raiseError("Failed to test communication: " + result);
        }

        void paypoint_OnReturn()
        {
            //throw new Exception("OnReturn... ");
            //paymentHandler.setPrintText(0, paypoint.printNormal);
            //paymentHandler.setPrintText(0, paypoint.printSignature);
        }

        void paypoint_OnStatus(short statusType)
        {
            throw new Exception("Status: " + statusType);
            if (statusType == 1) ;
            if (statusType == 2) ;
            if (statusType == 3) ;
        }

        public void reversePrevTransaction()
        {

        }

        public void transactionLog(int reportType, int noOfTransactions)
        {

        }

        #region IPaymentModule Members

        public bool startTransaction(string type, decimal amount, decimal vat, decimal cashback)
        {
            this.amount = amount;

            short transactionType = 0x30;
            if (amount < 0) transactionType = 0x31;

            if (paypoint.isInBankMode == 1) throw new Exception("In bank mode");

            int result = paypoint.startTransaction(transactionType, (int)(amount * 100), 0, 0x30, 0, 0);

                if (result != 0)
                {
                    throw new Exception("Could not start transaction: " + result);
                    paymentHandler.raiseError("Start transaction failed: " + result);
                }

                return true;
        }


        public void cancel()
        {
            int result = paypoint.cancelRequest();
            if (result != 0)
            {
                paymentHandler.raiseError("Failed to cancel transaction: "+result);
            }
        }

        public void endTransaction()
        {
            throw new NotImplementedException();
        }

        public void close()
        {
            int result = paypoint.close();
            if (result != 0) paymentHandler.raiseError("Failed to close connection to terminal: "+result);
        }

        public void endOfDay()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
