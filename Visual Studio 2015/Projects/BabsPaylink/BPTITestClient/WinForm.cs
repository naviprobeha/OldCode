using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using BPTI;
using System.Runtime.InteropServices.ComTypes;

namespace BpTiSharpClient
{
	/// <summary>
	/// Summary description for WinForm.
    /// 
	/// </summary>
    public class WinForm : System.Windows.Forms.Form, ICoBpTiEvents
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton tcpIp;
        private System.Windows.Forms.RadioButton rs232;
        private System.Windows.Forms.NumericUpDown comPort;
        private System.Windows.Forms.TextBox ipAddress;
        private System.Windows.Forms.Button initButton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox amount;
        private System.Windows.Forms.TextBox VAT;
        private System.Windows.Forms.TextBox cashBack;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button purchaseButton;
        private System.Windows.Forms.Button refundButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button endButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.ListBox eventsList;
        private System.Windows.Forms.TextBox trmDsp;
        private System.Windows.Forms.ListBox receipt;

        #region variables
        CoBpTiClass api;
        bool opened, transactionStarted;
        private TextBox PAN;
        private TextBox ExpDate;
        private TextBox CV2;
        private Label label4;
        private Label label5;
        private Label label6;
        private Button closeBatchButton;
        private Button trmConfigButton;
        private Button detailedButton;
        private Button totalButton;
        private Button openBatchButton;
        private TextBox byNumber;
        private Panel panel4;
        private Label TO;
        private Label label9;
        private Label label8;
        private Label label7;
        private TextBox textBox2;
        private TextBox FROM;
        private Button totalDateButton;
        private Button detailedDateButton;
        private Panel panel5;
        private Button notSentButton;
        private Button testComButton;
        private Panel panelRef;
        private Label check;
        private Button btnRef;
        private TextBox txtRef;
        private Button btnAbortRef;
        private TextBox referralText;
        private Button manualCardButton;
        //int initButtonStatus;
        private int pendingTransType, currentTransType;
        #endregion


        #region BpTiConstants
        //==============================
        //Constanst for start method
        enum TransactionTypes
        {
            LPP_PURCHASE = 4352,
            LPP_REFUND = 4353,
            LPP_REVERSAL = 4354,
            LPP_CLOSEBATCH = 4358
        };
        enum ResultDataTypes
        {
            rdCUSTOMERRECEIPT = 1,
            rdMERCHANTRECEIPT,
            rdCLOSEBATCHRESULT,
            rdTRMCONFIG,
            rdCURRENTBATCH,
            rdTRANSLOGDETAILED,
            rdTRANSLOGTOTALS,
            rdUNSENTTRANS,
            rdUNDEFINED = -1
        };
        enum ReceiptItems
        {
            riName = 1,
            riAddress = 2,
            riPostAddress = 3,
            riPhone = 4,
            riOrgNbr = 5,
            riBank = 6,
            riMerchantNbr = 7,
            riTrmId = 8,
            riTime = 9,
            riAmount = 10,
            riTotal = 11,
            riVAT = 12,
            riCashBack = 13,
            riCardNo = 14,
            riCardNoMasked = 15,
            riExpDate = 16,
            riCardProduct = 17,
            riAccountType = 18,
            riAuthInfo = 19,
            riReferenceNo = 20,
            riAcceptanceText = 21,
            riIdLine = 22,
            riSignatureLine = 23,
            riRefundAcceptanceText = 24,
            riCashierSignatureText = 25,
            riCashierNameText = 26,
            riTxnType = 27,
            riSaveReceipt = 28,
            riCustomerEx = 29,
            riPinUsed = 30,
            riEnd = -1,
        };

        enum ResultDataValues { rdEnd = -1 };
        #endregion

        public WinForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            api = new BPTI.CoBpTiClass();
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.initButton = new System.Windows.Forms.Button();
            this.ipAddress = new System.Windows.Forms.TextBox();
            this.comPort = new System.Windows.Forms.NumericUpDown();
            this.rs232 = new System.Windows.Forms.RadioButton();
            this.tcpIp = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cashBack = new System.Windows.Forms.TextBox();
            this.VAT = new System.Windows.Forms.TextBox();
            this.amount = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.closeButton = new System.Windows.Forms.Button();
            this.endButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.refundButton = new System.Windows.Forms.Button();
            this.purchaseButton = new System.Windows.Forms.Button();
            this.eventsList = new System.Windows.Forms.ListBox();
            this.trmDsp = new System.Windows.Forms.TextBox();
            this.receipt = new System.Windows.Forms.ListBox();
            this.PAN = new System.Windows.Forms.TextBox();
            this.ExpDate = new System.Windows.Forms.TextBox();
            this.CV2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.manualCardButton = new System.Windows.Forms.Button();
            this.closeBatchButton = new System.Windows.Forms.Button();
            this.trmConfigButton = new System.Windows.Forms.Button();
            this.detailedButton = new System.Windows.Forms.Button();
            this.totalButton = new System.Windows.Forms.Button();
            this.openBatchButton = new System.Windows.Forms.Button();
            this.byNumber = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.testComButton = new System.Windows.Forms.Button();
            this.notSentButton = new System.Windows.Forms.Button();
            this.TO = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.FROM = new System.Windows.Forms.TextBox();
            this.totalDateButton = new System.Windows.Forms.Button();
            this.detailedDateButton = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panelRef = new System.Windows.Forms.Panel();
            this.referralText = new System.Windows.Forms.TextBox();
            this.btnAbortRef = new System.Windows.Forms.Button();
            this.btnRef = new System.Windows.Forms.Button();
            this.txtRef = new System.Windows.Forms.TextBox();
            this.check = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comPort)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panelRef.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.initButton);
            this.panel1.Controls.Add(this.ipAddress);
            this.panel1.Controls.Add(this.comPort);
            this.panel1.Controls.Add(this.rs232);
            this.panel1.Controls.Add(this.tcpIp);
            this.panel1.Location = new System.Drawing.Point(16, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(267, 98);
            this.panel1.TabIndex = 0;
            // 
            // initButton
            // 
            this.initButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.initButton.Location = new System.Drawing.Point(117, 59);
            this.initButton.Name = "initButton";
            this.initButton.Size = new System.Drawing.Size(120, 31);
            this.initButton.TabIndex = 5;
            this.initButton.Text = "Init";
            this.initButton.Click += new System.EventHandler(this.initButton_Click);
            // 
            // ipAddress
            // 
            this.ipAddress.Location = new System.Drawing.Point(116, 10);
            this.ipAddress.Name = "ipAddress";
            this.ipAddress.Size = new System.Drawing.Size(107, 20);
            this.ipAddress.TabIndex = 4;
            this.ipAddress.Text = "127.0.0.1";
            this.ipAddress.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // comPort
            // 
            this.comPort.Location = new System.Drawing.Point(117, 38);
            this.comPort.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.comPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.comPort.Name = "comPort";
            this.comPort.Size = new System.Drawing.Size(52, 20);
            this.comPort.TabIndex = 3;
            this.comPort.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // rs232
            // 
            this.rs232.Location = new System.Drawing.Point(16, 38);
            this.rs232.Name = "rs232";
            this.rs232.Size = new System.Drawing.Size(88, 24);
            this.rs232.TabIndex = 1;
            this.rs232.Text = "RS232";
            // 
            // tcpIp
            // 
            this.tcpIp.Location = new System.Drawing.Point(16, 10);
            this.tcpIp.Name = "tcpIp";
            this.tcpIp.Size = new System.Drawing.Size(88, 25);
            this.tcpIp.TabIndex = 0;
            this.tcpIp.Text = "TCP/IP";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.cashBack);
            this.panel2.Controls.Add(this.VAT);
            this.panel2.Controls.Add(this.amount);
            this.panel2.Location = new System.Drawing.Point(17, 118);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(267, 64);
            this.panel2.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(184, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "Kontant";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(96, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "Moms";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "Belopp";
            // 
            // cashBack
            // 
            this.cashBack.Location = new System.Drawing.Point(184, 28);
            this.cashBack.Name = "cashBack";
            this.cashBack.Size = new System.Drawing.Size(76, 20);
            this.cashBack.TabIndex = 2;
            this.cashBack.Text = " 0";
            // 
            // VAT
            // 
            this.VAT.Location = new System.Drawing.Point(96, 28);
            this.VAT.Name = "VAT";
            this.VAT.Size = new System.Drawing.Size(72, 20);
            this.VAT.TabIndex = 1;
            this.VAT.Text = "0";
            // 
            // amount
            // 
            this.amount.Location = new System.Drawing.Point(12, 28);
            this.amount.Name = "amount";
            this.amount.Size = new System.Drawing.Size(68, 20);
            this.amount.TabIndex = 0;
            this.amount.Text = "0";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.closeButton);
            this.panel3.Controls.Add(this.endButton);
            this.panel3.Controls.Add(this.cancelButton);
            this.panel3.Controls.Add(this.refundButton);
            this.panel3.Controls.Add(this.purchaseButton);
            this.panel3.Location = new System.Drawing.Point(17, 184);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(267, 62);
            this.panel3.TabIndex = 2;
            // 
            // closeButton
            // 
            this.closeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeButton.Location = new System.Drawing.Point(87, 35);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 22);
            this.closeButton.TabIndex = 4;
            this.closeButton.Text = "Stäng";
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // endButton
            // 
            this.endButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.endButton.Location = new System.Drawing.Point(7, 35);
            this.endButton.Name = "endButton";
            this.endButton.Size = new System.Drawing.Size(75, 22);
            this.endButton.TabIndex = 3;
            this.endButton.Text = "Avsluta";
            this.endButton.Click += new System.EventHandler(this.endButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.Location = new System.Drawing.Point(167, 7);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Avbryt";
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // refundButton
            // 
            this.refundButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refundButton.Location = new System.Drawing.Point(87, 7);
            this.refundButton.Name = "refundButton";
            this.refundButton.Size = new System.Drawing.Size(75, 23);
            this.refundButton.TabIndex = 1;
            this.refundButton.Text = "Retur";
            this.refundButton.Click += new System.EventHandler(this.refundButton_Click);
            // 
            // purchaseButton
            // 
            this.purchaseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.purchaseButton.Location = new System.Drawing.Point(7, 7);
            this.purchaseButton.Name = "purchaseButton";
            this.purchaseButton.Size = new System.Drawing.Size(75, 23);
            this.purchaseButton.TabIndex = 0;
            this.purchaseButton.Text = "Köp";
            this.purchaseButton.Click += new System.EventHandler(this.purchaseButton_Click);
            // 
            // eventsList
            // 
            this.eventsList.BackColor = System.Drawing.SystemColors.Control;
            this.eventsList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.eventsList.Location = new System.Drawing.Point(287, 14);
            this.eventsList.Name = "eventsList";
            this.eventsList.ScrollAlwaysVisible = true;
            this.eventsList.Size = new System.Drawing.Size(464, 93);
            this.eventsList.TabIndex = 3;
            // 
            // trmDsp
            // 
            this.trmDsp.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.trmDsp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trmDsp.Location = new System.Drawing.Point(287, 118);
            this.trmDsp.Multiline = true;
            this.trmDsp.Name = "trmDsp";
            this.trmDsp.ReadOnly = true;
            this.trmDsp.Size = new System.Drawing.Size(228, 116);
            this.trmDsp.TabIndex = 4;
            // 
            // receipt
            // 
            this.receipt.Location = new System.Drawing.Point(521, 118);
            this.receipt.Name = "receipt";
            this.receipt.Size = new System.Drawing.Size(230, 459);
            this.receipt.TabIndex = 5;
            this.receipt.Visible = false;
            // 
            // PAN
            // 
            this.PAN.Location = new System.Drawing.Point(1, 16);
            this.PAN.Name = "PAN";
            this.PAN.Size = new System.Drawing.Size(118, 20);
            this.PAN.TabIndex = 6;
            this.PAN.Text = "4581090653216002";
            // 
            // ExpDate
            // 
            this.ExpDate.Location = new System.Drawing.Point(149, 16);
            this.ExpDate.Name = "ExpDate";
            this.ExpDate.Size = new System.Drawing.Size(40, 20);
            this.ExpDate.TabIndex = 7;
            this.ExpDate.Text = "1007";
            // 
            // CV2
            // 
            this.CV2.Location = new System.Drawing.Point(210, 16);
            this.CV2.Name = "CV2";
            this.CV2.Size = new System.Drawing.Size(34, 20);
            this.CV2.TabIndex = 8;
            this.CV2.Text = "123";
            this.CV2.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Kortnummer";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(147, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "MMYY";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(211, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "CV2";
            this.label6.Visible = false;
            // 
            // manualCardButton
            // 
            this.manualCardButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.manualCardButton.Location = new System.Drawing.Point(3, 42);
            this.manualCardButton.Name = "manualCardButton";
            this.manualCardButton.Size = new System.Drawing.Size(155, 21);
            this.manualCardButton.TabIndex = 12;
            this.manualCardButton.Text = "Använd manuellt kortnummer";
            this.manualCardButton.UseVisualStyleBackColor = true;
            this.manualCardButton.Click += new System.EventHandler(this.manualCardButton_Click);
            // 
            // closeBatchButton
            // 
            this.closeBatchButton.Location = new System.Drawing.Point(5, 19);
            this.closeBatchButton.Name = "closeBatchButton";
            this.closeBatchButton.Size = new System.Drawing.Size(75, 23);
            this.closeBatchButton.TabIndex = 13;
            this.closeBatchButton.Text = "Dagsavslut";
            this.closeBatchButton.UseVisualStyleBackColor = true;
            this.closeBatchButton.Click += new System.EventHandler(this.closeBatchButton_Click);
            // 
            // trmConfigButton
            // 
            this.trmConfigButton.Location = new System.Drawing.Point(86, 20);
            this.trmConfigButton.Name = "trmConfigButton";
            this.trmConfigButton.Size = new System.Drawing.Size(75, 23);
            this.trmConfigButton.TabIndex = 14;
            this.trmConfigButton.Text = "Trm konfig";
            this.trmConfigButton.UseVisualStyleBackColor = true;
            this.trmConfigButton.Click += new System.EventHandler(this.trmConfigButton_Click);
            // 
            // detailedButton
            // 
            this.detailedButton.Location = new System.Drawing.Point(5, 48);
            this.detailedButton.Name = "detailedButton";
            this.detailedButton.Size = new System.Drawing.Size(75, 23);
            this.detailedButton.TabIndex = 15;
            this.detailedButton.Text = "Detaljerad #";
            this.detailedButton.UseVisualStyleBackColor = true;
            this.detailedButton.Click += new System.EventHandler(this.detailedButton_Click);
            // 
            // totalButton
            // 
            this.totalButton.Location = new System.Drawing.Point(86, 49);
            this.totalButton.Name = "totalButton";
            this.totalButton.Size = new System.Drawing.Size(75, 23);
            this.totalButton.TabIndex = 16;
            this.totalButton.Text = "Total #";
            this.totalButton.UseVisualStyleBackColor = true;
            this.totalButton.Click += new System.EventHandler(this.totalButton_Click);
            // 
            // openBatchButton
            // 
            this.openBatchButton.Location = new System.Drawing.Point(167, 20);
            this.openBatchButton.Name = "openBatchButton";
            this.openBatchButton.Size = new System.Drawing.Size(75, 23);
            this.openBatchButton.TabIndex = 17;
            this.openBatchButton.Text = "Öppen bunt";
            this.openBatchButton.UseVisualStyleBackColor = true;
            this.openBatchButton.Click += new System.EventHandler(this.openBatchButton_Click);
            // 
            // byNumber
            // 
            this.byNumber.Location = new System.Drawing.Point(167, 51);
            this.byNumber.Name = "byNumber";
            this.byNumber.Size = new System.Drawing.Size(76, 20);
            this.byNumber.TabIndex = 18;
            this.byNumber.Text = "10";
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.testComButton);
            this.panel4.Controls.Add(this.notSentButton);
            this.panel4.Controls.Add(this.TO);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.textBox2);
            this.panel4.Controls.Add(this.FROM);
            this.panel4.Controls.Add(this.totalDateButton);
            this.panel4.Controls.Add(this.detailedDateButton);
            this.panel4.Controls.Add(this.byNumber);
            this.panel4.Controls.Add(this.openBatchButton);
            this.panel4.Controls.Add(this.totalButton);
            this.panel4.Controls.Add(this.detailedButton);
            this.panel4.Controls.Add(this.trmConfigButton);
            this.panel4.Controls.Add(this.closeBatchButton);
            this.panel4.Location = new System.Drawing.Point(16, 330);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(266, 231);
            this.panel4.TabIndex = 19;
            // 
            // testComButton
            // 
            this.testComButton.Location = new System.Drawing.Point(88, 194);
            this.testComButton.Name = "testComButton";
            this.testComButton.Size = new System.Drawing.Size(75, 23);
            this.testComButton.TabIndex = 30;
            this.testComButton.Text = "Test komm.";
            this.testComButton.UseVisualStyleBackColor = true;
            this.testComButton.Click += new System.EventHandler(this.testComButton_Click);
            // 
            // notSentButton
            // 
            this.notSentButton.Location = new System.Drawing.Point(6, 194);
            this.notSentButton.Name = "notSentButton";
            this.notSentButton.Size = new System.Drawing.Size(75, 23);
            this.notSentButton.TabIndex = 29;
            this.notSentButton.Text = "Ej skickade";
            this.notSentButton.UseVisualStyleBackColor = true;
            this.notSentButton.Click += new System.EventHandler(this.notSentButton_Click);
            // 
            // TO
            // 
            this.TO.AutoSize = true;
            this.TO.Location = new System.Drawing.Point(97, 150);
            this.TO.Name = "TO";
            this.TO.Size = new System.Drawing.Size(37, 13);
            this.TO.TabIndex = 28;
            this.TO.Text = "T.o.m.";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(97, 106);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(36, 13);
            this.label9.TabIndex = 27;
            this.label9.Text = "Fr.o.m";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(147, 126);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 9);
            this.label8.TabIndex = 26;
            this.label8.Text = "YYMMDDHHMM";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(148, 86);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 9);
            this.label7.TabIndex = 25;
            this.label7.Text = "YYMMDDHHMM";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(145, 147);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 24;
            // 
            // FROM
            // 
            this.FROM.Location = new System.Drawing.Point(145, 103);
            this.FROM.Name = "FROM";
            this.FROM.Size = new System.Drawing.Size(100, 20);
            this.FROM.TabIndex = 23;
            // 
            // totalDateButton
            // 
            this.totalDateButton.Location = new System.Drawing.Point(5, 147);
            this.totalDateButton.Name = "totalDateButton";
            this.totalDateButton.Size = new System.Drawing.Size(75, 23);
            this.totalDateButton.TabIndex = 22;
            this.totalDateButton.Text = "Total";
            this.totalDateButton.UseVisualStyleBackColor = true;
            this.totalDateButton.Click += new System.EventHandler(this.totalDateButton_Click);
            // 
            // detailedDateButton
            // 
            this.detailedDateButton.Location = new System.Drawing.Point(5, 101);
            this.detailedDateButton.Name = "detailedDateButton";
            this.detailedDateButton.Size = new System.Drawing.Size(75, 23);
            this.detailedDateButton.TabIndex = 21;
            this.detailedDateButton.Text = "Detaljerad";
            this.detailedDateButton.UseVisualStyleBackColor = true;
            this.detailedDateButton.Click += new System.EventHandler(this.detailedDateButton_Click);
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.manualCardButton);
            this.panel5.Controls.Add(this.label6);
            this.panel5.Controls.Add(this.label5);
            this.panel5.Controls.Add(this.label4);
            this.panel5.Controls.Add(this.CV2);
            this.panel5.Controls.Add(this.ExpDate);
            this.panel5.Controls.Add(this.PAN);
            this.panel5.Location = new System.Drawing.Point(16, 252);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(267, 72);
            this.panel5.TabIndex = 20;
            // 
            // panelRef
            // 
            this.panelRef.BackColor = System.Drawing.Color.Red;
            this.panelRef.Controls.Add(this.referralText);
            this.panelRef.Controls.Add(this.btnAbortRef);
            this.panelRef.Controls.Add(this.btnRef);
            this.panelRef.Controls.Add(this.txtRef);
            this.panelRef.Controls.Add(this.check);
            this.panelRef.Location = new System.Drawing.Point(288, 269);
            this.panelRef.Name = "panelRef";
            this.panelRef.Size = new System.Drawing.Size(223, 186);
            this.panelRef.TabIndex = 21;
            this.panelRef.Visible = false;
            // 
            // referralText
            // 
            this.referralText.BackColor = System.Drawing.Color.Red;
            this.referralText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.referralText.Location = new System.Drawing.Point(3, 3);
            this.referralText.Multiline = true;
            this.referralText.Name = "referralText";
            this.referralText.Size = new System.Drawing.Size(214, 81);
            this.referralText.TabIndex = 4;
            this.referralText.TextChanged += new System.EventHandler(this.textBox1_TextChanged_1);
            // 
            // btnAbortRef
            // 
            this.btnAbortRef.Location = new System.Drawing.Point(115, 139);
            this.btnAbortRef.Name = "btnAbortRef";
            this.btnAbortRef.Size = new System.Drawing.Size(75, 23);
            this.btnAbortRef.TabIndex = 3;
            this.btnAbortRef.Text = "Avbryt";
            this.btnAbortRef.UseVisualStyleBackColor = true;
            this.btnAbortRef.Click += new System.EventHandler(this.btnAbortRef_Click);
            // 
            // btnRef
            // 
            this.btnRef.Location = new System.Drawing.Point(20, 139);
            this.btnRef.Name = "btnRef";
            this.btnRef.Size = new System.Drawing.Size(75, 23);
            this.btnRef.TabIndex = 2;
            this.btnRef.Text = "OK";
            this.btnRef.UseVisualStyleBackColor = true;
            this.btnRef.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtRef
            // 
            this.txtRef.Location = new System.Drawing.Point(20, 110);
            this.txtRef.Name = "txtRef";
            this.txtRef.Size = new System.Drawing.Size(100, 20);
            this.txtRef.TabIndex = 1;
            // 
            // check
            // 
            this.check.AutoSize = true;
            this.check.Location = new System.Drawing.Point(17, 87);
            this.check.Name = "check";
            this.check.Size = new System.Drawing.Size(106, 13);
            this.check.TabIndex = 0;
            this.check.Text = "Ange kontrollnummer";
            // 
            // WinForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(761, 606);
            this.Controls.Add(this.panelRef);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.receipt);
            this.Controls.Add(this.trmDsp);
            this.Controls.Add(this.eventsList);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "WinForm";
            this.Text = "WinForm";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comPort)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panelRef.ResumeLayout(false);
            this.panelRef.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// Use IConnectionPointContainer to connect BPTI events
        /// Make sure to call Unadvise method to disconnect from BPTI when exiting.
        /// </summary>
        [STAThread]
        static void Main()
        {
            WinForm mainForm = new WinForm();

            int cookie;

            IConnectionPointContainer icpc = (IConnectionPointContainer)mainForm.api;
            IConnectionPoint icp;
            Guid IID_ICoBpTiEvents = typeof(ICoBpTiEvents).GUID;

            
            icpc.FindConnectionPoint(ref IID_ICoBpTiEvents, out icp);
            icp.Advise(mainForm, out cookie);

            Application.Run(mainForm);

            icp.Unadvise(cookie);
            
        }
        /// <summary>
        /// infoEvent - BPTI event that occures to inform cashier with a text.
        /// </summary>
        /// <param name="text">Text to be shown for cashier</param>
        public void infoEvent(ref string text)
        {
            eventsList.Items.Add(text);
        }
        /// <summary>
        ///  lppCmdFailedEvent - BPTI event that occures when terminal reports a failure. 
        /// </summary>
        /// <param name="cmd">LPP tag, and not neccessarily the message, that contains an error code.</param>
        /// <param name="code">LPP error code, Most imortant is 1011, meaning; do close batch.</param>
        /// <param name="text">Text description och LPP error code.</param>
        public void lppCmdFailedEvent(int cmd, int code, ref string text)
        {
            eventsList.Items.Add("Failure cmd: " + cmd.ToString() +       // Show text to cashier
                " code: " + code.ToString() + " text: " + text);

            int xcode = code;

            if (code > 2000 && code < 3000)             // Card complaint. Give customer a new chance.
                xcode = 2000;
            if (code > 3000 && code < 4000)
                xcode = 3000;

            switch (xcode)
            {
                case 1011:
                    MessageBox.Show(this, text, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
                case 2000:             // Give customer a new chance by doing nothing.
                    break;
                case 3000:             // Some kind of amount failure, Restart transaction.
                    api.end();
                    api.start(currentTransType);
                    break;
            }

        }
        /// <summary>
        /// exceptionEvent - BPTI event that occures to inform cashier that an exception has occured in BPTI.
        /// </summary>
        /// <param name="text">A describing text of the exception.</param>
        /// <param name="code">A code for the exception.</param>
        public void exceptionEvent(ref string text, int code)
        {
            eventsList.Items.Add(text + " code: " + code.ToString());
        }
        /// <summary>
        /// referralEvent - BPTI event that occures when cashier is supposed to make a call for authorisation.
        /// Show text and enable cashier to enter an authorisation code.
        /// If cashier chooses to authorize sale without calling, let program enter
        /// "9999".
        /// This event must be followed by a call to sendApprovalCode() or 
        /// end()/endTransaction().
        /// </summary>
        /// <param name="text">Text to show. Includes the phone number to call for authorisation.</param>
        public void referralEvent(ref string text)
        {
            referralText.Text = text;           // Show text to cashier.
            panelRef.Visible = true;
            txtRef.Focus();
            eventsList.Items.Add(text);
        }
        //-----------------------------------------------------------------------
        // Occurs only if bonus is activated and if track 3 is present on a 
        // magstripe card or track 1 is present in a chip card.
        // NOTE! Track 2 or its cardnumber is never sent to sales application.
        //-----------------------------------------------------------------------
        public void cardDataEvent(ref string text, ref string cardNo, ref string expDate,
            ref string track2)
        {
            api.bonusValidation(2001, "");
        }
        //-----------------------------------------------------------------------
        //
        //-----------------------------------------------------------------------
        public void terminatedEvent(ref string reason, int code)
        {
            eventsList.Items.Add("Terminated " + reason + " code: " + code.ToString());
        }
        /// <summary>
        /// statusChangeEvent(int newStatus) - BPTI event that occures to indicate a new status.
        /// </summary>
        /// <param name="newStatus"></param>
        /// <param>0 = Terminal disconnected.</param>
        /// <param>1 = Terminal connected and ready for commands.</param>
        /// <param>2 = Terminal is open, "NY KUND".</param>
        /// <param>3 = Terminale is closed, "TERMINAL STÄNGD".</param>
        /// <param>4 = Transaction has been started.</param>
        /// <param>5 = Transaction has been ended or aborted.</param>
        /// <param>6 = Terminal connected socket wise but not ready for use.</param>
        /// <param>7 = Communiction with terminal no longer exists.</param>
        public void statusChangeEvent(int newStatus)
        {
            switch (newStatus)
            {
                case 0:
                    eventsList.Items.Add("Terminalen frånkopplad.");
                    closeButton.Text = "Öppna";
                    initButton.Text = "Återanslut";
                    break;

                case 1:
                    eventsList.Items.Add("Terminal ansluten och klar att användas.");
                    initButton.Text = "Koppla ifrån";
                    break;

                case 2:
                    eventsList.Items.Add("Status öppen.");
                    opened = true;
                    closeButton.Text = "Stäng";
                    break;

                case 3:
                    eventsList.Items.Add("Status stängd.");
                    opened = false;
                    closeButton.Text = "Öppna";
                    transactionStarted = false;
                    currentTransType = -1;
                    break;

                case 4:
                    eventsList.Items.Add("Transaktion startad.");
                    transactionStarted = true;
                    currentTransType = pendingTransType;
                    break;

                case 5:
                    eventsList.Items.Add("Transaktion avslutad.");
                    transactionStarted = false;
                    currentTransType = -1;
                    break;

                case 6:
                    eventsList.Items.Add("statusChangeEvent med värde=" + new string('0', newStatus));
                    break;

                case 7:
                    eventsList.Items.Add("Kommunikation med terminalen bruten. Anropa connect()?");
                    transactionStarted = false;
                    currentTransType = -1;
                    opened = false;
                    closeButton.Text = "Öppna";
                    initButton.Text = "Återanslut";
                    break;

            }

        }
        /// <summary>
        /// terminalDspEvent - BPTI event that occures when terminal display changes. Must be shown to cashier to
        /// enable assistance for cardholder.
        /// </summary>
        /// <param name="row1">First line</param>
        /// <param name="row2">Second line</param>
        /// <param name="row3">Third line</param>
        /// <param name="row4">Fourth line</param>
        public void terminalDspEvent(ref string row1, ref string row2,
                                                ref string row3, ref string row4)
        {
            trmDsp.Clear();
            trmDsp.AppendText(row1 + Environment.NewLine);
            trmDsp.AppendText(row2 + Environment.NewLine);
            trmDsp.AppendText(row3 + Environment.NewLine);
            trmDsp.AppendText(row4 + Environment.NewLine);
        }
        /// <summary>
        /// txnResultEvent - BPTI event that occurs when a transaction result is ready in BPTI.
        /// The event occures for purchase, refund, reversal, closebatch and test host connection.
        /// </summary>
        /// <param name="txnType">Type of transaction the reply applies for.</param>
        /// <param name="resultCode">0=transaction successful, 
        /// 4003=Close batch indicates a difference between host and terminal.
        /// All other values indicates the transaction was unsuccessful.
        /// </param>
        /// <param name="text">Text to be shown for cashier.</param>
        public void txnResultEvent(int txnType, int resultCode, ref string text)
        {
            eventsList.Items.Add(text);
            receipt.Visible = true;
            receipt.Items.Clear();

            if (txnType == (int)TransactionTypes.LPP_PURCHASE ||
                                txnType == (int)TransactionTypes.LPP_REFUND ||
                                txnType == (int)TransactionTypes.LPP_REVERSAL)
                api.merchantReceipt();
            else if (txnType == (int)TransactionTypes.LPP_CLOSEBATCH)
                api.batchReport();
        }
        //-------------------------------------------------------------------
        //
        //-------------------------------------------------------------------
        public void resultDataEvent(int resultType, int item, ref string description,
                                                                ref string value)
        {
            switch ((ResultDataTypes)resultType)
            {
                case ResultDataTypes.rdCUSTOMERRECEIPT:
                case ResultDataTypes.rdMERCHANTRECEIPT:
                    receiptData(resultType, item, description, value);
                    break;
                case ResultDataTypes.rdCLOSEBATCHRESULT:
                case ResultDataTypes.rdCURRENTBATCH:
                    closeBatchData(item, description, value);
                    break;
                case ResultDataTypes.rdTRMCONFIG:
                case ResultDataTypes.rdTRANSLOGDETAILED:
                case ResultDataTypes.rdTRANSLOGTOTALS:
                case ResultDataTypes.rdUNSENTTRANS:
                    trmConfig(item, description, value);
                    break;
            }
        }
        //-------------------------------------------------------------------
        //
        //-------------------------------------------------------------------
        private void receiptData(int resultType, int item, string text, string data)
        {
            string receiptLine;
            bool written;
            written = false;

            if (text == "")
                receiptLine = data;  // Default use text as is
            else
            {
                // May need formatting depending on paperwith and so on.
                receiptLine = text + data;
            }

            // Certain items should maybe be bold and may need extra space or linefeeds.
            // Just make sure they come in the order the event occures, the rest is design.
            switch ((ReceiptItems)item)
            {
                case ReceiptItems.riTxnType:
                    receipt.Items.Add(" ");
                    receipt.Items.Add(data);
                    receipt.Items.Add(" ");
                    written = true;
                    break;
                case ReceiptItems.riIdLine:
                    receipt.Items.Add(" ");
                    receipt.Items.Add(data + "........................");
                    receipt.Items.Add(" ");
                    written = true;
                    break;
                case ReceiptItems.riSignatureLine:
                    receipt.Items.Add(" ");
                    receipt.Items.Add(data + "........................");
                    receipt.Items.Add(" ");
                    written = true;
                    break;

                case ReceiptItems.riCashierSignatureText:
                    receipt.Items.Add(" ");
                    receipt.Items.Add("........................");
                    break;
                case ReceiptItems.riCashierNameText:
                    receipt.Items.Add(" ");
                    receipt.Items.Add("........................");
                    break;

            }

            receipt.Visible = true;
            if (written == false)
                receipt.Items.Add(receiptLine);


            switch ((ResultDataTypes)resultType)
            {
                case ResultDataTypes.rdMERCHANTRECEIPT:
                    if (item == (int)ReceiptItems.riEnd)
                    {
                        receipt.Items.Add("---- end of receipt ----");
                        api.customerReceipt();
                    }
                    break;

                case ResultDataTypes.rdCUSTOMERRECEIPT:
                    if (item == (int)ReceiptItems.riEnd)
                    {
                        receipt.Items.Add(" ---- end of receipt ----");
                        api.endTransaction();
                    }
                    break;

            }
        }
        //--------------------------------------------------------------------------
        // paymentCodeEvent() - Let BPTI handle payment code. Dummy implementation
        //--------------------------------------------------------------------------
        public void paymentCodeEvent(ref string text) { }
        //-------------------------------------------------------------------
        //
        //-------------------------------------------------------------------
        private void textBox1_TextChanged(object sender, System.EventArgs e)
        {
            tcpIp.Checked = true;
        }
        /// <summary>
        /// initButton_Click - initLan/initRs232 is only made once per instance unless
        /// the arguments in call are wrong. 
        /// If terminal connection is lost after successfull call to any of the
        /// init functions, connect() may be called.
        /// </summary>
        private void initButton_Click(object sender, System.EventArgs e)
        {
            if (initButton.Text == "Init")
            {
                if (tcpIp.Checked)
                    api.initLan(ipAddress.Text, 2000);
                else if (rs232.Checked)
                    api.initRs232(Int32.Parse(comPort.Text), "");
            }
            else if (initButton.Text == "Återanslut")
                api.connect();
            else
                api.disconnect();

        }
        /// <summary>
        /// purchaseButton_Click - Start a purchase transaction or just send the amount values
        /// if transaction has already been started.
        /// If you just want to set terminal in "DRA KORT", clear amount or set to less than 100.
        /// </summary>
        private void purchaseButton_Click(object sender, System.EventArgs e)
        {
            startTransaction(TransactionTypes.LPP_PURCHASE);
            sendAmounts();
        }
        /// <summary>
        /// refundButton_Click - Start a refund transaction or just send the amount values if 
        /// transaction has already been started.
        /// If you just want to set terminal in "DRA KORT", clear amount or set to less than 100.
        /// </summary>
        private void refundButton_Click(object sender, System.EventArgs e)
        {
            startTransaction(TransactionTypes.LPP_REFUND);
            sendAmounts();
        }

        //-------------------------------------------------------------------
        //
        //-------------------------------------------------------------------
        private void startTransaction(TransactionTypes type)
        {
            if (!transactionStarted)
            {
                pendingTransType = (int)type;
                api.start((int)type);
            }
        }
        /// <summary>
        /// sendAmounts() - Send amount values to terminal if amount is greater than 99. 
        /// The values represents öre which is 0.01 of SEK.
        /// </summary>
        private void sendAmounts()
        {
            if (amount.Text.Length > 0 && Int32.Parse(amount.Text) > 99)
            {
                if (VAT.Text.Length == 0)
                    VAT.Text = "0";
                if (cashBack.Text.Length == 0)
                    cashBack.Text = "0";

                api.sendAmounts(Int32.Parse(amount.Text),
                            Int32.Parse(VAT.Text), Int32.Parse(cashBack.Text));
            }
        }
        /// <summary>
        /// endButton_Click() - calls end()/endTransaction() to end and ongoing transaction. Call end() to
        /// abort an ongoing transaction. End()/endTransaction() will reverse a transaction if customerReceipt() 
        /// or merchantReceipt() haven't been called.
        /// </summary>
        private void endButton_Click(object sender, System.EventArgs e)
        {
            if (transactionStarted)
                api.endTransaction();
        }
        /// <summary>
        ///  closeButton_Click - Opens or closes the terminal. Call to open() will set terminal in 
        ///  "NY KUND" state to accept transactions.
        ///  Call to close() will set the terminal in "TERMINAL STÄNGD" state but the terminal is still
        ///  connected.
        /// </summary>
        private void closeButton_Click(object sender, System.EventArgs e)
        {
            if (opened)
            {
                api.close();
            }
            else
            {
                api.open();
            }
        }
        /// <summary>
        /// cancelButton_Click() - Use cancel() to reverse a transaction that just was accepted. I all other
        /// cases just call end()/endTransaction() which may abort an ongoing transaction.
        /// </summary>
        private void cancelButton_Click(object sender, System.EventArgs e)
        {
            api.cancel();
        }
        /// <summary>
        /// closeBatchButton_Click - starts a close batch transaction for reconcilliation. 
        /// Expect a txnResultEvent.
        /// </summary>
        private void closeBatchButton_Click(object sender, EventArgs e)
        {
            api.start((int)TransactionTypes.LPP_CLOSEBATCH);
            receipt.Visible = false;
        }
        /// <summary>
        /// trmConfigButton_Click - 
        /// </summary>
        private void trmConfigButton_Click(object sender, EventArgs e)
        {
            api.terminalConfig();
            receipt.Visible = false;
        }

        private void openBatchButton_Click(object sender, EventArgs e)
        {
            api.currentBatch();
            receipt.Visible = false;
        }

        private void detailedButton_Click(object sender, EventArgs e)
        {
            api.transLogByNbr(11, Convert.ToInt16(byNumber.Text));
            receipt.Visible = false;
        }

        private void totalButton_Click(object sender, EventArgs e)
        {
            api.transLogByNbr(12, Convert.ToInt16(byNumber.Text));
            receipt.Visible = false;
        }

        private void manualCardButton_Click(object sender, EventArgs e)
        {
            api.sendCardData(PAN.Text, ExpDate.Text, CV2.Text);
        }

        private void detailedDateButton_Click(object sender, EventArgs e)
        {
            api.transLogByPeriod(21, FROM.Text, TO.Text);
            receipt.Visible = false;
        }

        private void testComButton_Click(object sender, EventArgs e)
        {
            api.testHostConnection();
        }

        private void notSentButton_Click(object sender, EventArgs e)
        {
            api.unsentTransactions();
            receipt.Visible = false;
        }

        private void totalDateButton_Click(object sender, EventArgs e)
        {
            api.transLogByPeriod(22, FROM.Text, TO.Text);
            receipt.Visible = false;
        }

        private void trmConfig(int item, string text, string data)
        {
            string reportRow;

            if (text.Length == 0)
                reportRow = data;  // Default use text as is
            else
            {
                // May need formatting depending on paperwith and so on.
                reportRow = text + data;
            }

            receipt.Visible = true;

            switch (item)
            {
                case (int)ResultDataValues.rdEnd:
                    reportRow = "SLUT";
                    break;
            }
            receipt.Items.Add(reportRow);
        }

        private void closeBatchData(int item, String text, String data)
        {
            String reportRow;

            if (text.Length == 0)
                reportRow = data;  // Default use text as is
            else
            {
                // May need formatting depending on paperwith and so on.
                reportRow = text + data;
            }

            receipt.Visible = true;

            switch (item)
            {
                case (int)ResultDataValues.rdEnd:
                    reportRow = "SLUT";        // Not what you think...
                    break;
            }

            receipt.Items.Add(reportRow);



        }
        // Send approval code to terminal to authorize the transaction. 
        // If nothing entered supply "9999" to indicate cashier authorized
        // transaction without calling.
        private void button1_Click(object sender, EventArgs e)
        {
            if (txtRef.Text.Length == 0)
                api.sendApprovalCode("9999");
            else
                api.sendApprovalCode(txtRef.Text);
            panelRef.Visible = false;

        }
        //
        // Referral but not accepted. Abort transaction by calling end()
        //
        private void btnAbortRef_Click(object sender, EventArgs e)
        {
            api.end();
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void WinForm_Load(object sender, EventArgs e)
        {
        }
    }
}

