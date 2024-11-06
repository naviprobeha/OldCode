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

        private rti.RTI_1 rti;
        private UCOMIConnectionPointContainer icpc;
        private UCOMIConnectionPoint icp;
        private int cookie;

        private System.Threading.Thread statusThread;

        public PaymentModule(Navipro.Cashjet.PaymentModules.Interfaces.IPaymentHandler paymentHandler)
        {
            this.paymentHandler = paymentHandler;

            readConfiguration();


            rti = new rti.RTI_1Class();
            Guid IID_ICoBpTiEvents = typeof(rti.IRTI_1Events).GUID;

            icpc = (UCOMIConnectionPointContainer)bpti;
            icpc.FindConnectionPoint(ref IID_ICoBpTiEvents, out icp);
            icp.Advise(this, out cookie);

            rti.init("");

        }


        #region IPaymentModule Members

        public bool startTransaction(decimal amount, decimal vat, decimal cashback)
        {

            return true;
        }

        public void cancel()
        {
        }

        public void reversePrevTransaction()
        {

        }

        public void endTransaction()
        {
        }

        public void close()
        {
            rti.stop();

        }

        public void endOfDay()
        {


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

        #region ICoBpTiEvents Members

        void rti.IRTI_1Events.requestEvent(string xmlData)
        {
            throw new NotImplementedException();
        }

 
        #endregion


     }
}
