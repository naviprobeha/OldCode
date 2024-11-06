using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Linq;
using System.Text;
using System.Collections;

namespace Navipro.BabsPaylink
{
    public class BPTIHandler : BPTI.ICoBpTiEvents
    {
        private BPTI.CoBpTiX1 api;
        private int cookie;
        private IConnectionPointContainer icpc;
        private IConnectionPoint icp;

        private ArrayList infoEventList;
        private ArrayList txnResultEventList;
        private ArrayList exceptionEventList;
        private ArrayList lppCmdFailedEventList;
        private ArrayList referralEventList;
        private ArrayList resultDataEventList;
        private ArrayList cardDataEventList;

        private int currentStatus = 0;

        private string displayRow1;
        private string displayRow2;
        private string displayRow3;
        private string displayRow4;

        public BPTIHandler()
        {
            api = new BPTI.CoBpTiX1Class();
            this.infoEventList = new ArrayList();
            this.txnResultEventList = new ArrayList();
            this.exceptionEventList = new ArrayList();
            this.lppCmdFailedEventList = new ArrayList();
            this.referralEventList = new ArrayList();
            this.resultDataEventList = new ArrayList();
            this.cardDataEventList = new ArrayList();

            icpc = (IConnectionPointContainer)api;
            Guid IID_ICoBpTiEvents = typeof(BPTI.ICoBpTiEvents).GUID;

            icpc.FindConnectionPoint(ref IID_ICoBpTiEvents, out icp);
            icp.Advise(this, out cookie);

        }

        public void initLan(string ipAddress)
        {
            api.initLan(ipAddress, 2000);
        }

        public void initRs232(int comPort)
        {
            api.initRs232(comPort, "");
        }

        public void close()
        {

            api.disconnect();

            icp.Unadvise(cookie);

        }

        public int getCurrentStatus()
        {
            return this.currentStatus;
        }

        public string getDisplayRow(int rowNo)
        {
            if (rowNo == 1) return this.displayRow1;
            if (rowNo == 2) return this.displayRow2;
            if (rowNo == 3) return this.displayRow3;
            if (rowNo == 4) return this.displayRow4;
            return "";
        }

        public bool infoEventExists()
        {
            if (infoEventList.Count > 0) return true;
            return false;
        }

        public string fetchInfoEvent()
        {
            if (infoEventList.Count > 0)
            {
                string text = infoEventList[0].ToString();
                infoEventList.RemoveAt(0);

                return text;
            }

            return "";
        }

        public bool txnResultEventExists()
        {
            if (txnResultEventList.Count > 0) return true;
            return false;
        }

        public void fetchTxnResultEvent(ref int txnType, ref int resultCode, ref string text)
        {
            if (txnResultEventList.Count > 0)
            {
                TxnResult txnResult = (TxnResult)txnResultEventList[0];
                txnType = txnResult.type;
                resultCode = txnResult.resultCode;
                text = txnResult.text;

                txnResultEventList.RemoveAt(0);
            }
        }

        public bool exceptionEventExists()
        {
            if (exceptionEventList.Count > 0) return true;
            return false;
        }

        public string fetchExceptionEvent()
        {
            if (exceptionEventList.Count > 0)
            {
                string text = exceptionEventList[0].ToString();
                exceptionEventList.RemoveAt(0);

                return text;
            }

            return "";
        }

        public bool lppCmdFailedEventExists()
        {
            if (lppCmdFailedEventList.Count > 0) return true;
            return false;
        }

        public void fetchLppCmdFailedEvent(ref int cmd, ref int code, ref string text)
        {
            if (lppCmdFailedEventList.Count > 0)
            {
                LppCmd lppCmd = (LppCmd)lppCmdFailedEventList[0];
                cmd = lppCmd.cmd;
                code = lppCmd.code;
                text = lppCmd.text;

                lppCmdFailedEventList.RemoveAt(0);
            }
        }

        public bool referralEventExists()
        {
            if (referralEventList.Count > 0) return true;
            return false;
        }

        public string fetchReferralEvent()
        {
            if (referralEventList.Count > 0)
            {
                string text = referralEventList[0].ToString();
                referralEventList.RemoveAt(0);

                return text;
            }

            return "";
        }

        public bool resultDataEventExists()
        {
            if (resultDataEventList.Count > 0) return true;
            return false;
        }

        public void fetchResultDataEvent(ref int type, ref int item, ref string description, ref string value)
        {
            if (resultDataEventList.Count > 0)
            {
                ResultData resultData = (ResultData)resultDataEventList[0];
                type = resultData.type;
                item = resultData.item;
                description = resultData.description;
                value = resultData.value;

                resultDataEventList.RemoveAt(0);
            }
        }

        public bool cardDataEventExists()
        {
            if (cardDataEventList.Count > 0) return true;
            return false;
        }

        public void fetchCardDataEvent(ref string text, ref string cardNo, ref string expDate, ref string track)
        {
            if (cardDataEventList.Count > 0)
            {
                CardData cardData = (CardData)cardDataEventList[0];
                text = cardData.text;
                cardNo = cardData.cardNo;
                expDate = cardData.expDate;
                track = cardData.track;

                cardDataEventList.RemoveAt(0);
            }
        }

        public void start(int type)
        {
            this.resultDataEventList.Clear();
            this.referralEventList.Clear();
            this.infoEventList.Clear();
            this.exceptionEventList.Clear();
            this.cardDataEventList.Clear();
            this.lppCmdFailedEventList.Clear();
            this.txnResultEventList.Clear();
         
            api.start(type);            
        }

        public void sendAmounts(int amount, int vat, int cashBack)
        {
            api.sendAmounts(amount, vat, cashBack);
        }

        public void endTransaction()
        {
            api.endTransaction();
        }

        public void cancel()
        {
            api.cancel();
        }

        public void sendApprovalCode(string approvalCode)
        {
            api.sendApprovalCode(approvalCode);
        }

        public void customerReceipt()
        {
            api.customerReceipt();
        }

        public void merchantReceipt()
        {
            api.merchantReceipt();
        }

        public void batchReport()
        {
            api.batchReport();
        }

        public void terminalConfig()
        {
            this.resultDataEventList.Clear();
            this.referralEventList.Clear();
            this.infoEventList.Clear();
            this.exceptionEventList.Clear();
            this.cardDataEventList.Clear();
            this.lppCmdFailedEventList.Clear();
            this.txnResultEventList.Clear();

            api.terminalConfig();
        }

        #region ICoBpTiEvents Members

        public void cardDataEvent(ref string text, ref string cardNo, ref string expDate, ref string track2)
        {
            CardData cardData = new CardData();
            cardData.text = text;
            cardData.cardNo = cardNo;
            cardData.expDate = expDate;
            cardData.track = track2;

            this.cardDataEventList.Add(cardData);
        }

        public void exceptionEvent(ref string text, int code)
        {
            exceptionEventList.Add(text + " (" + code.ToString() + ")");
        }

        public void infoEvent(ref string text)
        {
            infoEventList.Add(text);
        }

        public void lppCmdFailedEvent(int cmd, int code, ref string text)
        {
            LppCmd lppCmd = new LppCmd();
            lppCmd.cmd = cmd;
            lppCmd.code = code;
            lppCmd.text = text;

            this.lppCmdFailedEventList.Add(lppCmd);
        }

        public void paymentCodeEvent(ref string text)
        {

        }

        public void referralEvent(ref string text)
        {
            this.referralEventList.Add(text);
        }

        public void resultDataEvent(int resultType, int item, ref string description, ref string Value)
        {
            ResultData resultData = new ResultData();
            resultData.type = resultType;
            resultData.item = item;
            resultData.description = description;
            resultData.value = Value;

            this.resultDataEventList.Add(resultData);
        }

        public void statusChangeEvent(int newStatus)
        {
            this.currentStatus = newStatus;
        }

        public void terminalDspEvent(ref string row1, ref string row2, ref string row3, ref string row4)
        {
            this.displayRow1 = row1;
            this.displayRow2 = row2;
            this.displayRow3 = row3;
            this.displayRow4 = row4;
        }

        public void terminatedEvent(ref string reason, int reasonCode)
        {
        }

        public void txnResultEvent(int txnType, int resultCode, ref string text)
        {
            TxnResult txnResult = new TxnResult();
            txnResult.type = txnType;
            txnResult.resultCode = resultCode;
            txnResult.text = text;

            txnResultEventList.Add(txnResult);
        }

        public void transLogByNbr(int reportType, int noOfEntries)
        {
            this.resultDataEventList.Clear();
            this.referralEventList.Clear();
            this.infoEventList.Clear();
            this.exceptionEventList.Clear();
            this.cardDataEventList.Clear();
            this.lppCmdFailedEventList.Clear();
            this.txnResultEventList.Clear();

            api.transLogByNbr(reportType, noOfEntries);
        }

        public void transLogByPeriod(int reportType, string startDate, string endDate)
        {
            this.resultDataEventList.Clear();
            this.referralEventList.Clear();
            this.infoEventList.Clear();
            this.exceptionEventList.Clear();
            this.cardDataEventList.Clear();
            this.lppCmdFailedEventList.Clear();
            this.txnResultEventList.Clear();

            api.transLogByPeriod(reportType, startDate, endDate);
        }

        public void unsentTransactions()
        {
            this.resultDataEventList.Clear();
            this.referralEventList.Clear();
            this.infoEventList.Clear();
            this.exceptionEventList.Clear();
            this.cardDataEventList.Clear();
            this.lppCmdFailedEventList.Clear();
            this.txnResultEventList.Clear();

            api.unsentTransactions();
        }

        public void testHostConnection()
        {
            api.testHostConnection();
        }

        #endregion
    }
}
