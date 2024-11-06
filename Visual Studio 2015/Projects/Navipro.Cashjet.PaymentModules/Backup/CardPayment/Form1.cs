using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CardPayment
{
    public partial class Form1 : Form
    {
        Navipro.Cashjet.PaymentModules.CardPaymentHandler.PaymentHandler paymentHandler;
        
        delegate void AddTextCallback(string text);
        private Navipro.Cashjet.PaymentModules.CardPayment.ComHandler commHandler;

        public Form1()
        {
            InitializeComponent();
            //paymentHandler = new Navipro.Cashjet.PaymentModules.CardPaymentHandler.PaymentHandler("NETS");
            //paymentHandler = new Navipro.Cashjet.PaymentModules.CardPaymentHandler.PaymentHandler("PAYPOINT");
            paymentHandler = new Navipro.Cashjet.PaymentModules.CardPaymentHandler.PaymentHandler("BPTI");

            //tcpClient = new Navipro.Cashjet.TcpClient.TcpClient("127.0.0.1", 2001);
            //tcpClient.onDataReceivedEvent += new Navipro.Cashjet.TcpClient.TcpClient.DataReceivedEventHandler(tcpClient_onDataReceivedEvent);
            commHandler = new Navipro.Cashjet.PaymentModules.CardPayment.ComHandler();
            commHandler.start();
        }

        void tcpClient_onDataReceivedEvent(string text)
        {
            InvokeAddText(text);
            Application.DoEvents();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            int status = paymentHandler.performTransaction("R010001", 1, decimal.Parse(textBox1.Text), 25, 0);

            listBox1.Items.Clear();
            System.Collections.ArrayList printTextArray = paymentHandler.getPrintedTextArray();
            int i = 0;
            while (i < printTextArray.Count)
            {
                Navipro.Cashjet.PaymentModules.CardPaymentHandler.PrintText printText = (Navipro.Cashjet.PaymentModules.CardPaymentHandler.PrintText)printTextArray[i];

                listBox1.Items.Add(new ListViewItem(printText.description+": "+printText.value));
                i++;
            }

            textBox2.Text = paymentHandler.getResponseData("PROVIDER");
            textBox3.Text = paymentHandler.getResponseData("VERIFICATION_METHOD");           
            textBox4.Text = paymentHandler.getResponseData("REJECTION_REASON");
            textBox5.Text = paymentHandler.getResponseData("RESPONSE_CODE");


            System.Windows.Forms.MessageBox.Show("Transaction finished: " + status + " (" + printTextArray.Count+")");
             
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //paymentHandler.close();
            commHandler.close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            paymentHandler.endOfDay("HEPP", 1);

            listBox1.Items.Clear();
            System.Collections.ArrayList printTextArray = paymentHandler.getPrintedTextArray();
            int i = 0;
            while (i < printTextArray.Count)
            {
                Navipro.Cashjet.PaymentModules.CardPaymentHandler.PrintText printText = (Navipro.Cashjet.PaymentModules.CardPaymentHandler.PrintText)printTextArray[i];

                listBox1.Items.Add(new ListViewItem(printText.description+": "+printText.value));
                i++;
            }

            //System.Windows.Forms.MessageBox.Show("Transaction finished: " + status);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //paymentHandler.transactionLog("LOG", 1, 0, 5);

            listBox1.Items.Clear();
            System.Collections.ArrayList printTextArray = paymentHandler.getPrintedTextArray();
            int i = 0;
            while (i < printTextArray.Count)
            {
                Navipro.Cashjet.PaymentModules.CardPaymentHandler.PrintText printText = (Navipro.Cashjet.PaymentModules.CardPaymentHandler.PrintText)printTextArray[i];

                listBox1.Items.Add(new ListViewItem(printText.description + ": " + printText.value));
                i++;
            }


        }

        public void addText(string text)
        {
            listBox1.Items.Add(new ListViewItem(text));
        }

        private void InvokeAddText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.InvokeRequired)
            {
                //throw new Exception("Display text: " + text);
                AddTextCallback d = new AddTextCallback(addText);
                Invoke(d, new object[] { text });
            }
            else
            {
                this.addText(text);
            }
        }

    }
}
