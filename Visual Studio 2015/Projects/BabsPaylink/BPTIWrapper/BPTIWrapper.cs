using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;


namespace Navipro.BabsPaylink
{

    class Guids
    {
        public const string coclsguid = "E03D715B-A13F-4cff-92F1-123456781010";
        public const string intfguid = "D030D214-C984-496a-87E7-123456781011";
        public const string eventguid = "D030D214-C984-496a-87E7-123456781012";
        
        public static readonly System.Guid idcoclass;
        public static readonly System.Guid idintf;
        public static readonly System.Guid idevent;

        static Guids()
        {
            idcoclass = new System.Guid(coclsguid);
            idintf = new System.Guid(intfguid);
            idevent = new System.Guid(eventguid);
        }
    }

    [Guid(Guids.intfguid), InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IBPTIWrapper
    {
        [DispId(1)]
        void initLan(string ipAddress);

        [DispId(2)]
        void initRs232(int comPort);

        [DispId(3)]
        void close();

        [DispId(4)]
        bool infoEventExists();

        [DispId(5)]
        string fetchInfoEvent();

        [DispId(6)]
        bool txnResultEventExists();

        [DispId(7)]
        void fetchTxnResultEvent(ref int txnType, ref int resultCode, ref string text);

        [DispId(8)]
        int getCurrentStatus();

        [DispId(9)]
        string getDisplayRow(int rowNo);

        [DispId(10)]
        void start(int type);

        [DispId(11)]
        void sendAmounts(int amount, int vat, int cashBack);

        [DispId(12)]
        bool exceptionEventExists();

        [DispId(13)]
        string fetchExceptionEvent();

        [DispId(14)]
        bool lppCmdFailedEventExists();

        [DispId(15)]
        void fetchLppCmdFailedEvent(ref int cmd, ref int code, ref string text);

        [DispId(16)]
        bool referralEventExists();

        [DispId(17)]
        string fetchReferralEvent();

        [DispId(18)]
        bool resultDataEventExists();

        [DispId(19)]
        void fetchResultDataEvent(ref int type, ref int item, ref string description, ref string value);

        [DispId(20)]
        bool cardDataEventExists();

        [DispId(21)]
        void fetchCardDataEvent(ref string text, ref string cardNo, ref string expDate, ref string track);

        [DispId(22)]
        void endTransaction();

        [DispId(23)]
        void cancel();

        [DispId(24)]
        void customerReceipt();

        [DispId(25)]
        void merchantReceipt();

        [DispId(26)]
        void batchReport();

        [DispId(27)]
        void terminalConfig();

        [DispId(28)]
        void transLogByNbr(int reportType, int noOfEntries);

        [DispId(29)]
        void transLogByPeriod(int reportType, string startDate, string endDate);

        [DispId(30)]
        void unsentTransactions();

        [DispId(31)]
        void testHostConnection();

        [DispId(32)]
        void sendApprovalCode(string approvalCode);
    }
	


    [Guid(Guids.coclsguid), ProgId("Navipro.BPTIWrapper"), ClassInterface(ClassInterfaceType.None)]
    public class BPTIWrapper : IBPTIWrapper
    {
        private BPTIHandler bptiHandler;

        public BPTIWrapper()
        {
            bptiHandler = new BPTIHandler();
        }

        public void initLan(string ipAddress)
        {
            bptiHandler.initLan(ipAddress);
        }

        public void initRs232(int comPort)
        {
            bptiHandler.initRs232(comPort);
        }

        public void close()
        {
            bptiHandler.close();
        }

        public bool infoEventExists()
        {
            return bptiHandler.infoEventExists();
        }
        
        public string fetchInfoEvent()
        {
            return bptiHandler.fetchInfoEvent();
        }

        public bool txnResultEventExists()
        {
            return bptiHandler.txnResultEventExists();
        }

        public void fetchTxnResultEvent(ref int txnType, ref int resultCode, ref string text)
        {
            bptiHandler.fetchTxnResultEvent(ref txnType, ref resultCode, ref text);
        }

        public bool exceptionEventExists()
        {
            return bptiHandler.exceptionEventExists();
        }

        public string fetchExceptionEvent()
        {
            return bptiHandler.fetchExceptionEvent();
        }

        public bool lppCmdFailedEventExists()
        {
            return bptiHandler.lppCmdFailedEventExists();
        }

        public void fetchLppCmdFailedEvent(ref int cmd, ref int code, ref string text)
        {
            bptiHandler.fetchLppCmdFailedEvent(ref cmd, ref code, ref text);
        }

        public bool resultDataEventExists()
        {
            return bptiHandler.resultDataEventExists();
        }

        public void fetchResultDataEvent(ref int type, ref int item, ref string description, ref string value)
        {
            bptiHandler.fetchResultDataEvent(ref type, ref item, ref description, ref value);
        }

        public bool referralEventExists()
        {
            return bptiHandler.referralEventExists();
        }

        public string fetchReferralEvent()
        {
            return bptiHandler.fetchReferralEvent();
        }

        public bool cardDataEventExists()
        {
            return bptiHandler.cardDataEventExists();
        }

        public void fetchCardDataEvent(ref string text, ref string cardNo, ref string expDate, ref string track)
        {
            bptiHandler.fetchCardDataEvent(ref text, ref cardNo, ref expDate, ref track);
        }
        
        public int getCurrentStatus()
        {
            return bptiHandler.getCurrentStatus();
        }

        public string getDisplayRow(int rowNo)
        {
            return bptiHandler.getDisplayRow(rowNo);
        }

        public void start(int type)
        {
            bptiHandler.start(type);
        }

        public void sendAmounts(int amount, int vat, int cashBack)
        {
            bptiHandler.sendAmounts(amount, vat, cashBack);
        }

        public void endTransaction()
        {
            bptiHandler.endTransaction();
        }

        public void cancel()
        {
            bptiHandler.cancel();
        }

        public void sendApprovalCode(string approvalCode)
        {
            bptiHandler.sendApprovalCode(approvalCode);
        }

        public void customerReceipt()
        {
            bptiHandler.customerReceipt();
        }

        public void merchantReceipt()
        {
            bptiHandler.merchantReceipt();
        }

        public void batchReport()
        {
            bptiHandler.batchReport();
        }

        public void terminalConfig()
        {
            bptiHandler.terminalConfig();
        }

        public void transLogByNbr(int reportType, int noOfEntries)
        {
            bptiHandler.transLogByNbr(reportType, noOfEntries);
        }

        public void transLogByPeriod(int reportType, string startDate, string endDate)
        {
            bptiHandler.transLogByPeriod(reportType, startDate, endDate);
        }

        public void unsentTransactions()
        {
            bptiHandler.unsentTransactions();
        }

        public void testHostConnection()
        {
            bptiHandler.testHostConnection();
        }

    }
}
