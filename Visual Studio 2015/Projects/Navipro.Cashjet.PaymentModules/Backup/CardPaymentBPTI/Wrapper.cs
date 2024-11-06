using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Navipro.Cashjet.PaymentModules.CardPaymentBPTI
{
    public class Wrapper : MarshalByRefObject, BPTI.ICoBpTiEvents
    {
        private BPTI.CoBpTiX1 bpti;
        private UCOMIConnectionPointContainer icpc;
        private UCOMIConnectionPoint icp;
        private int cookie;

        public delegate void ExceptionEventHandler(string text, int code);
        public delegate void InfoEventHandler(string text);
        public delegate void LppCmdFailedEventHandler(int cmd, int code, string text);
        public delegate void PaymentCodeEventHandler(string text);
        public delegate void StatusChangeEventHandler(int newStatus);
        public delegate void ReferralEventHandler(string text);
        public delegate void ResultDataEventHandler(int resultType, int item, string description, string Value);
        public delegate void TerminalDspEventHandler(string row1, string row2, string row3, string row4);
        public delegate void TerminatedEventHandler(string reason, int reasonCode);
        public delegate void TxnResultEventHandler(int txnType, int resultCode, string text);



        public event ExceptionEventHandler onExceptionEvent;
        public event InfoEventHandler onInfoEvent;
        public event LppCmdFailedEventHandler onLppCmdFailedEvent;
        public event PaymentCodeEventHandler onPaymentCodeEvent;
        public event StatusChangeEventHandler onStatusChangeEvent;
        public event ReferralEventHandler onReferralEvent;
        public event ResultDataEventHandler onResultDataEvent;
        public event TerminalDspEventHandler onTerminalDspEvent;
        public event TerminatedEventHandler onTerminatedEvent;
        public event TxnResultEventHandler onTxnResultEvent;

        
        public Wrapper()
        {
            bpti = new BPTI.CoBpTiX1Class();
            icpc = (UCOMIConnectionPointContainer)bpti;
            Guid IID_ICoBpTiEvents = typeof(BPTI.ICoBpTiEvents).GUID;

            icpc.FindConnectionPoint(ref IID_ICoBpTiEvents, out icp);
            icp.Advise(this, out cookie);

        }

        public void initLan(string ipAddress, int port)
        {
            bpti.initLan(ipAddress, port);
        }

        public void open()
        {
            bpti.open();
        }

        public void close()
        {
            bpti.close();
        }


        #region ICoBpTiEvents Members



        public void cardDataEvent(ref string text, ref string cardNo, ref string expDate, ref string track2)
        {
            
        }

        protected virtual void raiseOnExceptionEvent(string text, int code)
        {
            ExceptionEventHandler handler = onExceptionEvent;
            if (handler != null)
            {
                handler(text, code);
            }
        }

        public void exceptionEvent(ref string text, int code)
        {
            raiseOnExceptionEvent(text, code);
        }

        protected virtual void raiseOnInfoEvent(string text)
        {
            InfoEventHandler handler = onInfoEvent;
            if (handler != null)
            {
                handler(text);
            }
        }

        public void infoEvent(ref string text)
        {
            raiseOnInfoEvent(text);
        }

        protected virtual void raiseOnLppCmdFailedEvent(int cmd, int code, string text)
        {
            LppCmdFailedEventHandler handler = onLppCmdFailedEvent;
            if (handler != null)
            {
                handler(cmd, code, text);
            }
        }

        public void lppCmdFailedEvent(int cmd, int code, ref string text)
        {
            raiseOnLppCmdFailedEvent(cmd, code, text);
        }


        protected virtual void raiseOnPaymentCodeEvent(string text)
        {
            PaymentCodeEventHandler handler = onPaymentCodeEvent;
            if (handler != null)
            {
                handler(text);
            }
        }

        public void paymentCodeEvent(ref string text)
        {
            raiseOnPaymentCodeEvent(text);
        }

        protected virtual void raiseOnReferralEvent(string text)
        {
            ReferralEventHandler handler = onReferralEvent;
            if (handler != null)
            {
                handler(text);
            }
        }

        public void referralEvent(ref string text)
        {
            raiseOnReferralEvent(text);
        }

        protected virtual void raiseOnResultDataEvent(int resultType, int item, string description, string Value)
        {
            ResultDataEventHandler handler = onResultDataEvent;
            if (handler != null)
            {
                handler(resultType, item, description, Value);
            }
        }

        public void resultDataEvent(int resultType, int item, ref string description, ref string Value)
        {
            raiseOnResultDataEvent(resultType, item, description, Value);
        }

        protected virtual void raiseOnStatusChangeEvent(int newStatus)
        {
            StatusChangeEventHandler handler = onStatusChangeEvent;
            if (handler != null)
            {
                handler(newStatus);
            }
        }

        public void statusChangeEvent(int newStatus)
        {
            raiseOnStatusChangeEvent(newStatus);
        }

        protected virtual void raiseOnTerminalDspEvent(string row1, string row2, string row3, string row4)
        {            
            TerminalDspEventHandler handler = onTerminalDspEvent;
            if (handler != null)
            {
                handler(row1, row2, row3, row4);
            }
        }
        public void terminalDspEvent(ref string row1, ref string row2, ref string row3, ref string row4)
        {
            raiseOnTerminalDspEvent(row1, row2, row3, row4);
        }

        protected virtual void raiseOnTerminatedEvent(string reason, int reasonCode)
        {
            TerminatedEventHandler handler = onTerminatedEvent;
            if (handler != null)
            {
                handler(reason, reasonCode);
            }
        }

        public void terminatedEvent(ref string reason, int reasonCode)
        {
            raiseOnTerminatedEvent(reason, reasonCode);
        }

        protected virtual void raiseOnTxnResultEvent(int txnType, int resultCode, string text)
        {
            TxnResultEventHandler handler = onTxnResultEvent;
            if (handler != null)
            {
                handler(txnType, resultCode, text);
            }
        }
        public void txnResultEvent(int txnType, int resultCode, ref string text)
        {
            raiseOnTxnResultEvent(txnType, resultCode, text);
        }

        #endregion
    }
}
