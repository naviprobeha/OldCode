using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Navipro.Cashjet.PaymentModules.CardPaymentNets
{
    public class PaymentModule : Navipro.Cashjet.PaymentModules.Interfaces.IPaymentModule
    {
        private decimal amount;
        private BBS.BAXI.BaxiCtrl baxiCtrl;
        private bool readyToSendAmount;
        private Navipro.Cashjet.PaymentModules.Interfaces.IPaymentHandler paymentHandler;
        

        public PaymentModule(Navipro.Cashjet.PaymentModules.Interfaces.IPaymentHandler paymentHandler)
        {
            this.paymentHandler = paymentHandler;

            baxiCtrl = new BBS.BAXI.BaxiCtrl();
            baxiCtrl.OnError += new BBS.BAXI.BaxiErrorEventHandler(baxiCtrl_OnError);
            baxiCtrl.OnStdRsp += new BBS.BAXI.StdRspReceivedHandler(baxiCtrl_OnStdRsp);
            baxiCtrl.OnTerminalReady += new BBS.BAXI.TerminalReadyEventHandler(baxiCtrl_OnTerminalReady);
            baxiCtrl.OnLocalMode += new BBS.BAXI.LocalModeEventHandler(baxiCtrl_OnLocalMode);
            baxiCtrl.OnDisplayText += new BBS.BAXI.DisplayTextEventHandler(baxiCtrl_OnDisplayText);
            baxiCtrl.OnPrintText += new BBS.BAXI.PrintTextEventHandler(baxiCtrl_OnPrintText);

            baxiCtrl.Open();
        }

        void baxiCtrl_OnPrintText(object sender, BBS.BAXI.PrintTextEventArgs args)
        {
            string[] printText = args.PrintText.Split(new char[] { (char)10 });
            
            int i = 0;
            while (i < printText.Length)
            {
                
                paymentHandler.setPrintText("0", "0", printText[i], "");
                i++;
            }
        }

        void baxiCtrl_OnDisplayText(object sender, BBS.BAXI.DisplayTextEventArgs e)
        {
            paymentHandler.setDisplayText(e.DisplaytextID, e.DisplayText);
            
            
        }

        

        void baxiCtrl_OnLocalMode(object sender, BBS.BAXI.LocalModeEventArgs args)
        {
            if (args.Result == 1) readyToSendAmount = true;
            if (args.Result == 0) updateStatus(1, args.IssuerId, args.VerificationMethod, args.RejectionSource, args.RejectionReason, args.ResponseCode);
            if (args.Result == 2) updateStatus(2, args.IssuerId, args.VerificationMethod, args.RejectionSource, args.RejectionReason, args.ResponseCode);
            if (args.Result == 99) updateStatus(2, args.IssuerId, args.VerificationMethod, args.RejectionSource, args.RejectionReason, args.ResponseCode);
        }

        public void endOfDay()
        {
            BBS.BAXI.AdministrationArgs admArgs = new BBS.BAXI.AdministrationArgs(0x3137, "0000");
            baxiCtrl.Administration(admArgs);

        }


        void baxiCtrl_OnTerminalReady(object sender, BBS.BAXI.TerminalReadyEventArgs args)
        {
            throw new Exception(args.ToString());
        }

        void baxiCtrl_OnStdRsp(object sender, BBS.BAXI.StdRspReceivedArgs args)
        {
            
        }

        void baxiCtrl_OnError(object sender, BBS.BAXI.BaxiErrorEventArgs args)
        {
            paymentHandler.raiseError(args.ErrorString + " (" + args.ErrorCode + ")");   
        }


        #region IPaymentModule Members

        public bool startTransaction(string type, decimal amount, decimal vat, decimal cashback)
        {
            this.amount = amount;

            if (readyToSendAmount)
            {
                sendAmounts();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void cancel()
        {
            BBS.BAXI.AdministrationArgs admArgs = new BBS.BAXI.AdministrationArgs(0x3132, "0000");
            baxiCtrl.Administration(admArgs);
        }

        public void endTransaction()
        {
            
        }

        public void close()
        {
            baxiCtrl.Close();
        }


        #endregion

        private void sendAmounts()
        {
            BBS.BAXI.TransferAmountArgs amountArgs = new BBS.BAXI.TransferAmountArgs();

            amountArgs.OperID = "0000";
            amountArgs.Type1 = 0x30;
            amountArgs.Type2 = 0x30;
            amountArgs.Type3 = 0x30;

            if (amount < 0) amountArgs.Type1 = 0x31;

            amountArgs.Amount1 = (int)(Math.Abs(amount) * 100);

            int result = baxiCtrl.TransferAmount(amountArgs);
            if (result == 0)
            {
                paymentHandler.raiseError("Transfer amount failed: " + baxiCtrl.MethodRejectCode + ", " + baxiCtrl.MethodRejectInfo);

            }
        }

        private void updateStatus(int status, int issuerId, int verificationMethod, int rejectionSource, string rejectionReason, string responseCode)
        {
            paymentHandler.setResponseData("ISSUER_ID", issuerId.ToString());
            paymentHandler.setResponseData("VERIFICATION_METHOD", verificationMethod.ToString());
            paymentHandler.setResponseData("REJECTION_SOURCE", rejectionSource.ToString());
            paymentHandler.setResponseData("REJECTION_REASON", rejectionReason.ToString());
            paymentHandler.setResponseData("RESPONSE_CODE", responseCode.ToString());

            paymentHandler.setStatus(status);

 
        }

        public void reversePrevTransaction()
        {

        }

        public void transactionLog(int reportType, int noOfTransactions)
        {
        }


    }
}
