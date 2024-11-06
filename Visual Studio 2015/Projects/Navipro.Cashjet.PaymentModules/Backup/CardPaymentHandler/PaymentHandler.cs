using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Collections;

namespace Navipro.Cashjet.PaymentModules.CardPaymentHandler
{
    public class PaymentHandler : Navipro.Cashjet.PaymentModules.Interfaces.IPaymentHandler
    {
        private PaymentWindow paymentWindow;
        private Navipro.Cashjet.PaymentModules.Interfaces.IPaymentModule paymentModule;
        private string paymentModuleName;
        private int currentStatus = 0;
        private System.Collections.Hashtable responseDataTable;
        private int status;
        private System.Collections.ArrayList printTextArray;
        delegate void HideWindow();
        private string type;

        public PaymentHandler(string paymentModuleName)
        {
            this.paymentModuleName = paymentModuleName;
        }

        private void init()
        {
            if (paymentModule == null)
            {
                //if (paymentModuleName == "NETS") paymentModule = new Navipro.Cashjet.PaymentModules.CardPaymentNets.PaymentModule(this);
                //if (paymentModuleName == "PAYPOINT") paymentModule = new Navipro.Cashjet.PaymentModules.CardPaymentVerifone.PaymentModule(this);
                if (paymentModuleName == "BPTI") paymentModule = new Navipro.Cashjet.PaymentModules.CardPaymentBPTI.PaymentModule(this);
            }
        }

        public int performTransaction(string type, string receiptNo, int entryNo, decimal amount, decimal vat, decimal cashback)
        {
            currentStatus = 0;
            checkPaths();
            createInitialReceiptStatus(receiptNo, entryNo, amount, vat, cashback);

            paymentWindow = new PaymentWindow(this);

            startTransaction(type, amount, vat, cashback);
            paymentWindow.ShowDialog();
            paymentWindow.Dispose();
            createReceiptStatus(receiptNo, entryNo, amount, vat, cashback);

            return currentStatus;
        }

        public void transactionLog(string reportNo, int entryNo, int reportType, int noOfTransactions)
        {
            printTextArray = new System.Collections.ArrayList();
            responseDataTable = new System.Collections.Hashtable();

            init();
            paymentModule.transactionLog(reportType, noOfTransactions);

            paymentWindow = new PaymentWindow(this);
            paymentWindow.ShowDialog();

            createReceiptStatus(reportNo, entryNo, 0, 0, 0);

        }


        public System.Collections.ArrayList getPrintedTextArray()
        {
            return printTextArray;
        }

        public string getResponseData(string code)
        {
            if (this.responseDataTable.Contains(code))
            {
                return ((ResponseDataEntry)responseDataTable[code.ToString()]).value;
            }

            return "";
        }

        public void endOfDay(string reportNo, int entryNo)
        {

            printTextArray = new System.Collections.ArrayList();
            responseDataTable = new System.Collections.Hashtable();

            init();

            paymentModule.endOfDay();

            paymentWindow = new PaymentWindow(this);
            paymentWindow.ShowDialog();

            createReceiptStatus(reportNo, entryNo, 0, 0, 0);

        }

        public void reversePrevTransaction(string receiptNo, int entryNo)
        {

            printTextArray = new System.Collections.ArrayList();
            responseDataTable = new System.Collections.Hashtable();

            init();

            paymentModule.reversePrevTransaction();

            paymentWindow = new PaymentWindow(this);
            paymentWindow.ShowDialog();

            createReceiptStatus(receiptNo, entryNo, 0, 0, 0);

        }

        public void close()
        {
            paymentModule.close();
        }

        private void checkPaths()
        {
            if (!System.IO.Directory.Exists("C:\\cardpayment\\receipts")) System.IO.Directory.CreateDirectory("C:\\cardpayment\\receipts");
            if (!System.IO.Directory.Exists("C:\\cardpayment\\endofday")) System.IO.Directory.CreateDirectory("C:\\cardpayment\\endofday");

        }

        private void createInitialReceiptStatus(string receiptNo, int entryNo, decimal amount, decimal vat, decimal cashback)
        {
            
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<cardPaymentStatus/>");

            XmlElement docElement = xmlDoc.DocumentElement;

            XmlElement receiptElement = xmlDoc.CreateElement("receipt");
            receiptElement.SetAttribute("no", receiptNo);
            receiptElement.SetAttribute("entryNo", entryNo.ToString());
            receiptElement.SetAttribute("amount", amount.ToString());
            receiptElement.SetAttribute("vat", vat.ToString());
            receiptElement.SetAttribute("cashback", cashback.ToString());

            xmlDoc.Save("C:\\cardpayment\\receipts\\" + receiptNo + "_" + entryNo + ".xml");
        }

        private void createReceiptStatus(string receiptNo, int entryNo, decimal amount, decimal vat, decimal cashback)
        {
      

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<cardPaymentStatus/>");


            XmlElement docElement = xmlDoc.DocumentElement;

            XmlElement receiptElement = xmlDoc.CreateElement("receipt");
            docElement.AppendChild(receiptElement);

            receiptElement.SetAttribute("no", receiptNo);
            receiptElement.SetAttribute("entryNo", entryNo.ToString());
            receiptElement.SetAttribute("amount", amount.ToString());
            receiptElement.SetAttribute("vat", vat.ToString());
            receiptElement.SetAttribute("cashback", cashback.ToString());
            receiptElement.SetAttribute("status", currentStatus.ToString());
            receiptElement.SetAttribute("provider", getResponseData("PROVIDER"));
            receiptElement.SetAttribute("verificationMethod", getResponseData("VERIFICATION_METHOD"));
            receiptElement.SetAttribute("rejectionReason", getResponseData("REJECTION_REASON"));
            receiptElement.SetAttribute("responseCode", getResponseData("RESPONSE_CODE"));

            XmlElement customerPrintTextElement = xmlDoc.CreateElement("customerPrintText");
            receiptElement.AppendChild(customerPrintTextElement);
            XmlElement merchantPrintTextElement = xmlDoc.CreateElement("merchantPrintText");
            receiptElement.AppendChild(merchantPrintTextElement);
            XmlElement reportPrintTextElement = xmlDoc.CreateElement("reportPrintText");
            receiptElement.AppendChild(reportPrintTextElement);

            ArrayList receiptTexts = getPrintedTextArray();
            int i = 0;
            while (i < receiptTexts.Count)
            {
                
                PrintText printText = (PrintText)receiptTexts[i];

                XmlElement printTextElement = xmlDoc.CreateElement("text");
                printTextElement.SetAttribute("code", printText.code);
                printTextElement.SetAttribute("description", printText.description);
                printTextElement.SetAttribute("value", printText.value);

                if (printText.dataType == "1")
                {
                    customerPrintTextElement.AppendChild(printTextElement);
                }
                if (printText.dataType == "2")
                {
                    merchantPrintTextElement.AppendChild(printTextElement);
                }
                if (printText.dataType == "3")
                {
                    reportPrintTextElement.AppendChild(printTextElement);
                }
                if (printText.dataType == "6")
                {
                    reportPrintTextElement.AppendChild(printTextElement);
                }
                if (printText.dataType == "7")
                {
                    reportPrintTextElement.AppendChild(printTextElement);
                }
                if (printText.dataType == "8")
                {
                    reportPrintTextElement.AppendChild(printTextElement);
                }

                i++;
            }


            xmlDoc.Save("C:\\cardpayment\\receipts\\" + receiptNo + "_" + entryNo + ".xml");
        }

        public string getResultXml(string receiptNo, int entryNo)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("C:\\cardpayment\\receipts\\" + receiptNo + "_" + entryNo + ".xml");

            if (xmlDoc.DocumentElement != null) return xmlDoc.OuterXml;
            return "";
        }



        #region IPaymentHandler Members

        public void startTransaction(string type, decimal amount, decimal vat, decimal cashback)
        {
            printTextArray = new System.Collections.ArrayList();
            responseDataTable = new System.Collections.Hashtable();

            init();

            
            if (!paymentModule.startTransaction(type, amount, vat, cashback))
            {
                throw new Exception("Could not start transaction.");
            }

        }


        public void setDisplayText(int lineNo, string text)
        {
            if (paymentWindow != null) paymentWindow.setDisplayText(lineNo, text);
        }

        public void setPrintText(string dataType, string code, string description, string value)
        {
            PrintText printText = new PrintText(dataType, code, description, value);
            printTextArray.Add(printText);
        }

        public void raiseError(string errorMessage)
        {
            this.notify("Felmeddelande: " + errorMessage);
            try
            {
                paymentModule.close();
            }
            catch (Exception e) { }
            paymentModule = null;
            paymentWindow.closeWindow();
            //paymentWindow.Dispose();
        }

        public void cancelTransaction()
        {
            paymentModule.cancel();
        }

        public void setResponseData(string code, string value)
        {
            ResponseDataEntry responseDataEntry = new ResponseDataEntry(code, value);

            if (responseDataTable[code] != null)
            {
                responseDataTable[code] = responseDataEntry;
            }
            else
            {
                responseDataTable.Add(code, responseDataEntry);
            }
        }

        public void setStatus(int status)
        {
            currentStatus = status;
            
            if (status == 1) InvokeHideWindow();
            paymentModule.endTransaction();
            if (status != 1) InvokeHideWindow();            
            //paymentWindow.Dispose();
        }

        public void closePaymentModule()
        {
            paymentModule.close();
        }

        public void notify(string text)
        {
            Notification notification = new Notification(text);
            notification.ShowDialog();
        }

        public string getReferalValue(string label)
        {
            ReferalInput referalInput = new ReferalInput(label);
            referalInput.ShowDialog();

            return referalInput.getValue();
        }

        #endregion


        private void InvokeHideWindow()
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (paymentWindow.InvokeRequired)
            {
                //throw new Exception("Display text: " + text);
                HideWindow d = new HideWindow(paymentWindow.closeWindow);
                paymentWindow.Invoke(d, null);
            }
            else
            {
                paymentWindow.closeWindow();
            }
        }
    }
}
