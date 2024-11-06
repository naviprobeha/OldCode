namespace SmartOrder
{
    partial class Setup
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
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
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.hostBox = new System.Windows.Forms.TextBox();
            this.receiverBox = new System.Windows.Forms.TextBox();
            this.agentIdBox = new System.Windows.Forms.TextBox();
            this.userIdBox = new System.Windows.Forms.TextBox();
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.inputPanel1 = new Microsoft.WindowsCE.Forms.InputPanel(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.spiraEnabled = new System.Windows.Forms.CheckBox();
            this.scannerEnabled = new System.Windows.Forms.CheckBox();
            this.delimiterBox = new System.Windows.Forms.TextBox();
            this.deliveryDateToday = new System.Windows.Forms.CheckBox();
            this.askSynchronization = new System.Windows.Forms.CheckBox();
            this.askPostingMethod = new System.Windows.Forms.CheckBox();
            this.itemSearchMethod = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.showItemNo = new System.Windows.Forms.CheckBox();
            this.showVariant = new System.Windows.Forms.CheckBox();
            this.showBaseUnit = new System.Windows.Forms.CheckBox();
            this.showDeliveryDate = new System.Windows.Forms.CheckBox();
            this.useDynPrices = new System.Windows.Forms.CheckBox();
            this.label20 = new System.Windows.Forms.Label();
            this.printOnLocalPrinter = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(241, 258);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.passwordBox);
            this.tabPage1.Controls.Add(this.userIdBox);
            this.tabPage1.Controls.Add(this.agentIdBox);
            this.tabPage1.Controls.Add(this.receiverBox);
            this.tabPage1.Controls.Add(this.hostBox);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(233, 277);
            this.tabPage1.Text = "Synk";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.itemSearchMethod);
            this.tabPage2.Controls.Add(this.askPostingMethod);
            this.tabPage2.Controls.Add(this.askSynchronization);
            this.tabPage2.Controls.Add(this.deliveryDateToday);
            this.tabPage2.Controls.Add(this.delimiterBox);
            this.tabPage2.Controls.Add(this.scannerEnabled);
            this.tabPage2.Controls.Add(this.spiraEnabled);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(233, 229);
            this.tabPage2.Text = "Order";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.useDynPrices);
            this.tabPage3.Controls.Add(this.showDeliveryDate);
            this.tabPage3.Controls.Add(this.showBaseUnit);
            this.tabPage3.Controls.Add(this.showVariant);
            this.tabPage3.Controls.Add(this.showItemNo);
            this.tabPage3.Controls.Add(this.label19);
            this.tabPage3.Controls.Add(this.label18);
            this.tabPage3.Controls.Add(this.label17);
            this.tabPage3.Controls.Add(this.label16);
            this.tabPage3.Controls.Add(this.label15);
            this.tabPage3.Controls.Add(this.label14);
            this.tabPage3.Controls.Add(this.label13);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(233, 277);
            this.tabPage3.Text = "Visa";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.printOnLocalPrinter);
            this.tabPage4.Controls.Add(this.label20);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(233, 277);
            this.tabPage4.Text = "Allmänt";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(4, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 20);
            this.label1.Text = "Värd";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(4, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 18);
            this.label2.Text = "Mottagare ID";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(4, 149);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 20);
            this.label3.Text = "Agent ID";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(4, 173);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 20);
            this.label4.Text = "Användar-ID";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(4, 197);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 20);
            this.label5.Text = "Lösenord";
            // 
            // hostBox
            // 
            this.hostBox.Location = new System.Drawing.Point(91, 9);
            this.hostBox.Name = "hostBox";
            this.hostBox.Size = new System.Drawing.Size(139, 23);
            this.hostBox.TabIndex = 5;
            this.hostBox.GotFocus += new System.EventHandler(this.hostBox_GotFocus);
            // 
            // receiverBox
            // 
            this.receiverBox.Location = new System.Drawing.Point(91, 33);
            this.receiverBox.Name = "receiverBox";
            this.receiverBox.Size = new System.Drawing.Size(139, 23);
            this.receiverBox.TabIndex = 6;
            this.receiverBox.GotFocus += new System.EventHandler(this.receiverBox_GotFocus);
            // 
            // agentIdBox
            // 
            this.agentIdBox.Location = new System.Drawing.Point(91, 147);
            this.agentIdBox.Name = "agentIdBox";
            this.agentIdBox.Size = new System.Drawing.Size(139, 23);
            this.agentIdBox.TabIndex = 7;
            this.agentIdBox.TextChanged += new System.EventHandler(this.agentIdBox_TextChanged);
            this.agentIdBox.GotFocus += new System.EventHandler(this.agentIdBox_GotFocus);
            // 
            // userIdBox
            // 
            this.userIdBox.Location = new System.Drawing.Point(91, 171);
            this.userIdBox.Name = "userIdBox";
            this.userIdBox.Size = new System.Drawing.Size(139, 23);
            this.userIdBox.TabIndex = 8;
            this.userIdBox.GotFocus += new System.EventHandler(this.userIdBox_GotFocus);
            // 
            // passwordBox
            // 
            this.passwordBox.Location = new System.Drawing.Point(91, 195);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.Size = new System.Drawing.Size(139, 23);
            this.passwordBox.TabIndex = 9;
            this.passwordBox.GotFocus += new System.EventHandler(this.passwordBox_GotFocus);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(3, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 20);
            this.label6.Text = "Spira Fashion";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(4, 33);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 20);
            this.label7.Text = "Fältavskiljare";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(4, 57);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 20);
            this.label8.Text = "Scanner";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(4, 81);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 20);
            this.label9.Text = "Leveransdatum";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(4, 105);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 20);
            this.label10.Text = "Art. sökmetod";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(4, 129);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(100, 20);
            this.label11.Text = "Föreslå synk.";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(4, 153);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(100, 20);
            this.label12.Text = "Föreslå bokf.";
            // 
            // spiraEnabled
            // 
            this.spiraEnabled.Location = new System.Drawing.Point(110, 9);
            this.spiraEnabled.Name = "spiraEnabled";
            this.spiraEnabled.Size = new System.Drawing.Size(100, 20);
            this.spiraEnabled.TabIndex = 7;
            this.spiraEnabled.Text = "Aktiverad";
            // 
            // scannerEnabled
            // 
            this.scannerEnabled.Location = new System.Drawing.Point(111, 57);
            this.scannerEnabled.Name = "scannerEnabled";
            this.scannerEnabled.Size = new System.Drawing.Size(100, 20);
            this.scannerEnabled.TabIndex = 8;
            this.scannerEnabled.Text = "Aktiverad";
            // 
            // delimiterBox
            // 
            this.delimiterBox.Location = new System.Drawing.Point(111, 31);
            this.delimiterBox.Name = "delimiterBox";
            this.delimiterBox.Size = new System.Drawing.Size(31, 23);
            this.delimiterBox.TabIndex = 9;
            this.delimiterBox.GotFocus += new System.EventHandler(this.delimiterBox_GotFocus);
            // 
            // deliveryDateToday
            // 
            this.deliveryDateToday.Location = new System.Drawing.Point(111, 81);
            this.deliveryDateToday.Name = "deliveryDateToday";
            this.deliveryDateToday.Size = new System.Drawing.Size(120, 20);
            this.deliveryDateToday.TabIndex = 10;
            this.deliveryDateToday.Text = "Föreslå dagens";
            // 
            // askSynchronization
            // 
            this.askSynchronization.Location = new System.Drawing.Point(111, 128);
            this.askSynchronization.Name = "askSynchronization";
            this.askSynchronization.Size = new System.Drawing.Size(100, 20);
            this.askSynchronization.TabIndex = 11;
            this.askSynchronization.Text = "Ja";
            // 
            // askPostingMethod
            // 
            this.askPostingMethod.Location = new System.Drawing.Point(111, 152);
            this.askPostingMethod.Name = "askPostingMethod";
            this.askPostingMethod.Size = new System.Drawing.Size(100, 20);
            this.askPostingMethod.TabIndex = 12;
            this.askPostingMethod.Text = "Ja";
            // 
            // itemSearchMethod
            // 
            this.itemSearchMethod.Items.Add("Artikelnr");
            this.itemSearchMethod.Items.Add("Beskrivning");
            this.itemSearchMethod.Items.Add("Säsong");
            this.itemSearchMethod.Items.Add("Sökvy");
            this.itemSearchMethod.Location = new System.Drawing.Point(111, 102);
            this.itemSearchMethod.Name = "itemSearchMethod";
            this.itemSearchMethod.Size = new System.Drawing.Size(100, 23);
            this.itemSearchMethod.TabIndex = 13;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label13.Location = new System.Drawing.Point(4, 4);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(126, 20);
            this.label13.Text = "Order, artikellista";
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(4, 28);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(100, 20);
            this.label14.Text = "Artikelnr";
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(4, 52);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(100, 20);
            this.label15.Text = "Variant / Färg";
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(4, 76);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(100, 20);
            this.label16.Text = "Basenhet";
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(4, 100);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(100, 20);
            this.label17.Text = "Leveransdag";
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label18.Location = new System.Drawing.Point(4, 145);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(100, 20);
            this.label18.Text = "Prishantering";
            // 
            // label19
            // 
            this.label19.Location = new System.Drawing.Point(4, 169);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(117, 20);
            this.label19.Text = "Dynamiska priser";
            // 
            // showItemNo
            // 
            this.showItemNo.Location = new System.Drawing.Point(130, 28);
            this.showItemNo.Name = "showItemNo";
            this.showItemNo.Size = new System.Drawing.Size(100, 20);
            this.showItemNo.TabIndex = 7;
            // 
            // showVariant
            // 
            this.showVariant.Location = new System.Drawing.Point(130, 52);
            this.showVariant.Name = "showVariant";
            this.showVariant.Size = new System.Drawing.Size(100, 20);
            this.showVariant.TabIndex = 8;
            // 
            // showBaseUnit
            // 
            this.showBaseUnit.Location = new System.Drawing.Point(130, 76);
            this.showBaseUnit.Name = "showBaseUnit";
            this.showBaseUnit.Size = new System.Drawing.Size(100, 20);
            this.showBaseUnit.TabIndex = 9;
            // 
            // showDeliveryDate
            // 
            this.showDeliveryDate.Location = new System.Drawing.Point(130, 100);
            this.showDeliveryDate.Name = "showDeliveryDate";
            this.showDeliveryDate.Size = new System.Drawing.Size(100, 20);
            this.showDeliveryDate.TabIndex = 10;
            // 
            // useDynPrices
            // 
            this.useDynPrices.Location = new System.Drawing.Point(130, 169);
            this.useDynPrices.Name = "useDynPrices";
            this.useDynPrices.Size = new System.Drawing.Size(100, 20);
            this.useDynPrices.TabIndex = 11;
            // 
            // label20
            // 
            this.label20.Location = new System.Drawing.Point(4, 4);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(160, 20);
            this.label20.Text = "Skriv ut på lokal skrivare";
            // 
            // printOnLocalPrinter
            // 
            this.printOnLocalPrinter.Location = new System.Drawing.Point(161, 4);
            this.printOnLocalPrinter.Name = "printOnLocalPrinter";
            this.printOnLocalPrinter.Size = new System.Drawing.Size(54, 20);
            this.printOnLocalPrinter.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(142, 267);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 33);
            this.button1.TabIndex = 5;
            this.button1.Text = "OK";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Setup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(247, 312);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Setup";
            this.Text = "Setup";
            this.Load += new System.EventHandler(this.Setup_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Setup_Closing);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TextBox passwordBox;
        private System.Windows.Forms.TextBox userIdBox;
        private System.Windows.Forms.TextBox agentIdBox;
        private System.Windows.Forms.TextBox receiverBox;
        private System.Windows.Forms.TextBox hostBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private Microsoft.WindowsCE.Forms.InputPanel inputPanel1;
        private System.Windows.Forms.CheckBox spiraEnabled;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox itemSearchMethod;
        private System.Windows.Forms.CheckBox askPostingMethod;
        private System.Windows.Forms.CheckBox askSynchronization;
        private System.Windows.Forms.CheckBox deliveryDateToday;
        private System.Windows.Forms.TextBox delimiterBox;
        private System.Windows.Forms.CheckBox scannerEnabled;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox useDynPrices;
        private System.Windows.Forms.CheckBox showDeliveryDate;
        private System.Windows.Forms.CheckBox showBaseUnit;
        private System.Windows.Forms.CheckBox showVariant;
        private System.Windows.Forms.CheckBox showItemNo;
        private System.Windows.Forms.CheckBox printOnLocalPrinter;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button button1;
    }
}