using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Navipro.Cashjet.PaymentModules.CardPaymentHandler
{
    public partial class PaymentWindow : Form
    {
        private Navipro.Cashjet.PaymentModules.Interfaces.IPaymentHandler paymentHandler;
        delegate void SetDisplayTextCallback(int lineNo, string text);
        delegate void SetStatusCallback(int status);
        private bool systemInitiatedCloseWindow;
        

        public PaymentWindow(Navipro.Cashjet.PaymentModules.Interfaces.IPaymentHandler paymentHandler)
        {
            this.paymentHandler = paymentHandler;

            InitializeComponent();

            assignDisplayText(0, "");
            assignDisplayText(1, "");
            assignDisplayText(2, "");
            assignDisplayText(3, "");
            assignDisplayText(4, "");
            assignDisplayText(5, "");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            paymentHandler.cancelTransaction();   
        }


        public void assignDisplayText(int lineNo, string text)
        {
            //throw new Exception(lineNo+", "+text);
            if (lineNo == 0) label1.Text = text;
            if (lineNo == 1) label2.Text = text;
            if (lineNo == 2) label3.Text = text;
            if (lineNo == 3) label4.Text = text;
            if (lineNo == 4) label5.Text = text;
            if (lineNo == 5) label6.Text = text;

            this.Activate();
            
        }

        #region IPaymentHandler Members

 




 
        public void setDisplayText(int lineNo, string text)
        {
            if (this.Visible) InvokeSetDisplayText(lineNo, text);
        }

        #endregion

        private void InvokeSetDisplayText(int lineNo, string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.InvokeRequired)
            {
                //throw new Exception("Display text: " + text);
                SetDisplayTextCallback d = new SetDisplayTextCallback(assignDisplayText);
                Invoke(d, new object[] { lineNo, text });
            }
            else
            {
                this.assignDisplayText(lineNo, text);
            }
        }

        private void PaymentWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!systemInitiatedCloseWindow)
            {
                paymentHandler.closePaymentModule();
            }

        }

        public void closeWindow()
        {
            systemInitiatedCloseWindow = true;
            Close();
            
        }
 
 

 
 

    }
}
