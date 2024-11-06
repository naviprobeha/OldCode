using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Navipro.Cashjet.PaymentModules.CardPaymentBPTI
{
    public class PaymentModule : Navipro.Cashjet.PaymentModules.Interfaces.IPaymentModule, BPTI.ICoBpTiEvents
    {
        private Navipro.Cashjet.PaymentModules.Interfaces.IPaymentHandler paymentHandler;
        private decimal amount;
        private string ipAddress;
        private int currentStatus;
        private bool transactionStarted;
        private int transactionType;
        private string cardProvider;
        private int verificationMethod;
        private bool queryClose;
        private int paymentStatus;
        private decimal vat;
        private decimal cashback;
        private bool waitAndStartTransaction;
        private int reportType;
        private int noOfTransactions;
        private bool closeConnectionAfterTransaction;
        private bool endTransSent;
        private string typeString;

        private BPTI.CoBpTiX1 bpti;
        private UCOMIConnectionPointContainer icpc;
        private UCOMIConnectionPoint icp;
        private int cookie;

        private System.Threading.Thread statusThread;

        public PaymentModule(Navipro.Cashjet.PaymentModules.Interfaces.IPaymentHandler paymentHandler)
        {
            this.paymentHandler = paymentHandler;
            
            readConfiguration();

         
        }
 


        
        #region IPaymentModule Members

        public bool startTransaction(string type, decimal amount, decimal vat, decimal cashback)
        {
            typeString = type;


            if (bpti == null)
            {
                bpti = new BPTI.CoBpTiX1Class();
                Guid IID_ICoBpTiEvents = typeof(BPTI.ICoBpTiEvents).GUID;

                icpc = (UCOMIConnectionPointContainer)bpti;
                icpc.FindConnectionPoint(ref IID_ICoBpTiEvents, out icp);
                icp.Advise(this, out cookie);

                bpti.initLan(ipAddress, 2000);
            }

            transactionType = 0;
            verificationMethod = 0;
            queryClose = false;
            cardProvider = "";
            this.amount = amount;
            this.vat = vat;
            this.cashback = cashback;
            paymentStatus = 0;
            transactionStarted = false;

            if (currentStatus == 4) endTransaction();

            if ((currentStatus == 1) || (currentStatus == 2) || (currentStatus == 5))
            {

                sendStartTransaction();
            }
            else
            {
                waitAndStartTransaction = true;
            }

            return true;
        }

        private void sendStartTransaction()
        {
            endTransSent = false;

            if (transactionType == 0)
            {
                if (amount > 0)
                {

                    bpti.start(4352); //Purchase 

                    
                    if (typeString == "MANUAL")
                    {
                        bpti.sendCardData("", "", "");
                    }
                }
                else
                {
                    bpti.start(4353); //Refund   

                    if (typeString == "MANUAL")
                    {
                        bpti.sendCardData("", "", "");
                    }
                }
                
            }
            if (transactionType == 1)
            {
                bpti.start(4358);
            }

            waitAndStartTransaction = false;
        }

        public void cancel()
        {
            queryClose = true;
            endTransaction();
            endTransSent = true;

            if (((currentStatus == 2) || (currentStatus == 5)) && (paymentStatus == 0))
            {
                paymentHandler.notify("Avbrutet av kassör.");
                updateStatus(1, cardProvider, verificationMethod, "Avbrutet av kassör.", "CANCEL");
               
            }
        }

        public void reversePrevTransaction()
        {
            queryClose = true;
            bpti.cancel();

            
        }

        public void endTransaction()
        {
            bpti.endTransaction();

         }

        public void close()
        {
            bpti.close();
            bpti.disconnect();
            bpti = null;
        }

        public void endOfDay()
        {
            if (bpti == null)
            {
                bpti = new BPTI.CoBpTiX1Class();
                Guid IID_ICoBpTiEvents = typeof(BPTI.ICoBpTiEvents).GUID;

                icpc = (UCOMIConnectionPointContainer)bpti;
                icpc.FindConnectionPoint(ref IID_ICoBpTiEvents, out icp);
                icp.Advise(this, out cookie);

                bpti.initLan(ipAddress, 2000);
            }

            cardProvider = "";
            verificationMethod = 0;

            transactionType = 1;
            queryClose = false;

            if ((currentStatus == 1) || (currentStatus == 2) || (currentStatus == 5))
            {
                sendStartTransaction();
            }
            else
            {
                waitAndStartTransaction = true;
            }

        }

        #endregion

        private void readConfiguration()
        {
            System.Xml.XmlDocument configDoc = new System.Xml.XmlDocument();
            configDoc.Load("C:\\CardPayment\\config.xml");

            System.Xml.XmlElement docElement = configDoc.DocumentElement;
            System.Xml.XmlElement element = (System.Xml.XmlElement)docElement.SelectSingleNode("terminalIpAddress");
            ipAddress = element.FirstChild.Value;

            element = (System.Xml.XmlElement)docElement.SelectSingleNode("closeConnectionAfterTransaction");
            if (element != null)
            {
                if (element.FirstChild.Value.ToUpper() == "TRUE") closeConnectionAfterTransaction = true;
            }
        }

        private void updateStatus(int status, string cardProfider, int verificationMethod, string rejectionReason, string responseCode)
        {
            paymentHandler.setResponseData("PROVIDER", cardProvider);
            paymentHandler.setResponseData("VERIFICATION_METHOD", verificationMethod.ToString());
            paymentHandler.setResponseData("REJECTION_REASON", rejectionReason.ToString());
            paymentHandler.setResponseData("RESPONSE_CODE", responseCode.ToString());

            paymentHandler.setStatus(status);


        }


        #region ICoBpTiEvents Members

        void BPTI.ICoBpTiEvents.cardDataEvent(ref string text, ref string cardNo, ref string expDate, ref string track2)
        {
            throw new NotImplementedException();
        }

        void BPTI.ICoBpTiEvents.exceptionEvent(ref string text, int code)
        {
            paymentHandler.raiseError(text + " (" + code + ")");
        }

        void BPTI.ICoBpTiEvents.infoEvent(ref string text)
        {
            if (currentStatus != 5)
            {
                paymentHandler.setDisplayText(5, text);
            }
        }

        void BPTI.ICoBpTiEvents.lppCmdFailedEvent(int cmd, int code, ref string text)
        {
            if ((cmd == 7428) && (code == -1))
            {
                paymentHandler.raiseError(text);
            }
            else
            {
                if ((cmd == 7431) && (code == 1003))
                {
                    //End-kommando avvisat. OK.
                }
                else
                {

                    paymentHandler.notify(text);
                    updateStatus(1, cardProvider, 0, text, code.ToString());
                }
            }

        }

        void BPTI.ICoBpTiEvents.paymentCodeEvent(ref string text)
        {
            string paymentCodeValue = paymentHandler.getReferalValue(text);
            if (paymentCodeValue != "")
            {
                bpti.sendPaymentCode(paymentCodeValue);
            }
            else
            {
                bpti.sendPaymentCode("");
            }

        }

        void BPTI.ICoBpTiEvents.referralEvent(ref string text)
        {
            string referralValue = paymentHandler.getReferalValue(text);
            if (referralValue != "")
            {
                bpti.sendApprovalCode(referralValue);
            }
            else
            {
                bpti.sendApprovalCode("9999");
            }
        }

        void BPTI.ICoBpTiEvents.resultDataEvent(int resultType, int item, ref string description, ref string Value)
        {
            paymentHandler.setPrintText(resultType.ToString(), item.ToString(), description, Value);
            if ((resultType == 2) && ((item == 22) || (item == 23) || (item == 25) || (item == 26))) verificationMethod = 1;
            if ((resultType == 1) && (item == 19)) cardProvider = getCardProvider(Value);
            if ((resultType == 2) && (item == -1)) updateStatus(paymentStatus, cardProvider, verificationMethod, "", "");
            if ((resultType == 3) && (item == -1)) updateStatus(2, cardProvider, verificationMethod, "", "");
            if ((resultType == 6) && (item == -1)) updateStatus(2, cardProvider, verificationMethod, "", "");
            if ((resultType == 7) && (item == -1)) updateStatus(2, cardProvider, verificationMethod, "", "");
            if ((resultType == 8) && (item == -1)) updateStatus(2, cardProvider, verificationMethod, "", "");
        }

        void BPTI.ICoBpTiEvents.statusChangeEvent(int newStatus)
        {
            if (newStatus != currentStatus)
            {
                if ((currentStatus == 6) && (newStatus == 1)) bpti.open();
                
                currentStatus = newStatus;

                if (currentStatus == 4)
                {
                    if (!transactionStarted)
                    {
                        transactionStarted = true;
                        bpti.sendAmounts((int)(Math.Abs(amount) * 100), (int)(vat), (int)(cashback * 100));
                    }
                }
                if (currentStatus == 5)
                {

                    transactionStarted = false;
                    
                    if ((paymentStatus != 2) && (queryClose))
                    {
                        paymentHandler.notify("Avbrutet av kassör.");
                        updateStatus(1, cardProvider, verificationMethod, "Avbrutet av kassör.", "CANCEL");
                    }
                     
                }
                if ((currentStatus == 1) || (currentStatus == 5))
                {
                    if (waitAndStartTransaction)
                    {                       
                        sendStartTransaction();
                    }
                }
            }
        }

        void BPTI.ICoBpTiEvents.terminalDspEvent(ref string row1, ref string row2, ref string row3, ref string row4)
        {
            if (currentStatus != 5)
            {
                paymentHandler.setDisplayText(1, row1);
                paymentHandler.setDisplayText(2, row2);
                paymentHandler.setDisplayText(3, row3);
                paymentHandler.setDisplayText(4, row4);
            }

        }

        void BPTI.ICoBpTiEvents.terminatedEvent(ref string reason, int reasonCode)
        {
            throw new NotImplementedException();
        }

        void BPTI.ICoBpTiEvents.txnResultEvent(int txnType, int resultCode, ref string text)
        {
            if (txnType == 4354)
            {
                paymentHandler.raiseError("Reset!");
            }
            if (txnType == 4358)
            {
                paymentHandler.notify(text);
            }

            if ((transactionType == 0) || (transactionType == 3))
            {
                if ((resultCode == 0) || (resultCode == 4003))
                {
                    //Godkänt
                    paymentStatus = 2;
                    //updateStatus(2, 0, 0, 0, "", "");
                }
                else
                {
                    //Ej godkänt
                    paymentHandler.notify(text);
                    paymentStatus = 1;
                    //updateStatus(1, 0, 0, 0, "", "");
                }

                bpti.customerReceipt();
                bpti.merchantReceipt();

            }
            if (transactionType == 1)
            {
                bpti.batchReport();
            }

            if (transactionType == 2)
            {
                bpti.batchReport();
            }

        }

        #endregion


        private string getCardProvider(string Value)
        {
            if (Value.Length >= 14) return Value.Substring(10, 3);
            return "Unknown";
        }

        public void transactionLog(int reportType, int noOfTransactions)
        {

            if (bpti == null)
            {
                bpti = new BPTI.CoBpTiX1Class();
                Guid IID_ICoBpTiEvents = typeof(BPTI.ICoBpTiEvents).GUID;

                icpc = (UCOMIConnectionPointContainer)bpti;
                icpc.FindConnectionPoint(ref IID_ICoBpTiEvents, out icp);
                icp.Advise(this, out cookie);

                bpti.initLan(ipAddress, 2000);
            }

            transactionType = 2;
            verificationMethod = 0;
            queryClose = false;
            cardProvider = "";
            paymentStatus = 0;

            this.reportType = reportType;
            this.noOfTransactions = noOfTransactions;

            if (reportType == 0) bpti.transLogByNbr(11, noOfTransactions);
            if (reportType == 1) bpti.transLogByNbr(12, noOfTransactions);
            if (reportType == 2) bpti.unsentTransactions();

            
            bpti.batchReport();            
        }
    }
}
