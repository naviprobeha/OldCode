namespace Navipro.SmartInventory
{
    partial class ReceiptItem
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
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.qtyBox = new System.Windows.Forms.TextBox();
            this.qtyLabel = new System.Windows.Forms.Label();
            this.weightBox = new System.Windows.Forms.TextBox();
            this.itemLabel = new System.Windows.Forms.Label();
            this.descriptionBox = new System.Windows.Forms.TextBox();
            this.scanItemLabel = new System.Windows.Forms.Label();
            this.itemNoBox = new System.Windows.Forms.TextBox();
            this.receiptNoBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lengthBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.widthBox = new System.Windows.Forms.TextBox();
            this.heightBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.qtyToReceiveBox = new System.Windows.Forms.TextBox();
            this.logViewList = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.button2.Location = new System.Drawing.Point(128, 263);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(107, 49);
            this.button2.TabIndex = 49;
            this.button2.Text = "Bekräfta";
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(4, 263);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 49);
            this.button1.TabIndex = 48;
            this.button1.Text = "Avbryt";
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // qtyBox
            // 
            this.qtyBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.qtyBox.Location = new System.Drawing.Point(4, 181);
            this.qtyBox.Name = "qtyBox";
            this.qtyBox.ReadOnly = true;
            this.qtyBox.Size = new System.Drawing.Size(112, 23);
            this.qtyBox.TabIndex = 47;
            // 
            // qtyLabel
            // 
            this.qtyLabel.Location = new System.Drawing.Point(4, 163);
            this.qtyLabel.Name = "qtyLabel";
            this.qtyLabel.Size = new System.Drawing.Size(54, 20);
            this.qtyLabel.Text = "Antal";
            // 
            // weightBox
            // 
            this.weightBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.weightBox.Location = new System.Drawing.Point(4, 127);
            this.weightBox.Name = "weightBox";
            this.weightBox.ReadOnly = true;
            this.weightBox.Size = new System.Drawing.Size(54, 23);
            this.weightBox.TabIndex = 45;
            this.weightBox.GotFocus += new System.EventHandler(this.weightBox_GotFocus);
            // 
            // itemLabel
            // 
            this.itemLabel.Location = new System.Drawing.Point(4, 109);
            this.itemLabel.Name = "itemLabel";
            this.itemLabel.Size = new System.Drawing.Size(54, 20);
            this.itemLabel.Text = "Vikt";
            // 
            // descriptionBox
            // 
            this.descriptionBox.Location = new System.Drawing.Point(4, 83);
            this.descriptionBox.Name = "descriptionBox";
            this.descriptionBox.ReadOnly = true;
            this.descriptionBox.Size = new System.Drawing.Size(232, 23);
            this.descriptionBox.TabIndex = 44;
            // 
            // scanItemLabel
            // 
            this.scanItemLabel.Location = new System.Drawing.Point(4, 66);
            this.scanItemLabel.Name = "scanItemLabel";
            this.scanItemLabel.Size = new System.Drawing.Size(142, 20);
            this.scanItemLabel.Text = "Beskrivning";
            // 
            // itemNoBox
            // 
            this.itemNoBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.itemNoBox.Location = new System.Drawing.Point(122, 42);
            this.itemNoBox.Name = "itemNoBox";
            this.itemNoBox.ReadOnly = true;
            this.itemNoBox.Size = new System.Drawing.Size(114, 23);
            this.itemNoBox.TabIndex = 43;
            this.itemNoBox.GotFocus += new System.EventHandler(this.itemNoBox_GotFocus);
            // 
            // receiptNoBox
            // 
            this.receiptNoBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.receiptNoBox.Location = new System.Drawing.Point(4, 42);
            this.receiptNoBox.Name = "receiptNoBox";
            this.receiptNoBox.ReadOnly = true;
            this.receiptNoBox.Size = new System.Drawing.Size(112, 23);
            this.receiptNoBox.TabIndex = 42;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(122, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.Text = "Artikelnr";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(4, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 20);
            this.label4.Text = "Uppdrag";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 21);
            this.label1.Text = "Inleverans - Artikel";
            // 
            // lengthBox
            // 
            this.lengthBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lengthBox.Location = new System.Drawing.Point(64, 127);
            this.lengthBox.Name = "lengthBox";
            this.lengthBox.ReadOnly = true;
            this.lengthBox.Size = new System.Drawing.Size(52, 23);
            this.lengthBox.TabIndex = 56;
            this.lengthBox.GotFocus += new System.EventHandler(this.lengthBox_GotFocus);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(64, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 20);
            this.label3.Text = "Längd";
            // 
            // widthBox
            // 
            this.widthBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.widthBox.Location = new System.Drawing.Point(122, 127);
            this.widthBox.Name = "widthBox";
            this.widthBox.ReadOnly = true;
            this.widthBox.Size = new System.Drawing.Size(56, 23);
            this.widthBox.TabIndex = 59;
            this.widthBox.GotFocus += new System.EventHandler(this.widthBox_GotFocus);
            // 
            // heightBox
            // 
            this.heightBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.heightBox.Location = new System.Drawing.Point(184, 127);
            this.heightBox.Name = "heightBox";
            this.heightBox.ReadOnly = true;
            this.heightBox.Size = new System.Drawing.Size(52, 23);
            this.heightBox.TabIndex = 60;
            this.heightBox.GotFocus += new System.EventHandler(this.heightBox_GotFocus);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(122, 109);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 20);
            this.label5.Text = "Bredd";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(184, 109);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 20);
            this.label6.Text = "Höjd";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(122, 163);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(112, 20);
            this.label7.Text = "Antal att inlev.";
            // 
            // qtyToReceiveBox
            // 
            this.qtyToReceiveBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.qtyToReceiveBox.Location = new System.Drawing.Point(122, 181);
            this.qtyToReceiveBox.Name = "qtyToReceiveBox";
            this.qtyToReceiveBox.ReadOnly = true;
            this.qtyToReceiveBox.Size = new System.Drawing.Size(114, 23);
            this.qtyToReceiveBox.TabIndex = 67;
            this.qtyToReceiveBox.GotFocus += new System.EventHandler(this.qtyToReceiveBox_GotFocus);
            // 
            // logViewList
            // 
            this.logViewList.Location = new System.Drawing.Point(269, 178);
            this.logViewList.Name = "logViewList";
            this.logViewList.Size = new System.Drawing.Size(100, 98);
            this.logViewList.TabIndex = 68;
            this.logViewList.Visible = false;
            // 
            // ReceiptItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(638, 455);
            this.Controls.Add(this.logViewList);
            this.Controls.Add(this.qtyToReceiveBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.heightBox);
            this.Controls.Add(this.widthBox);
            this.Controls.Add(this.lengthBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.qtyBox);
            this.Controls.Add(this.qtyLabel);
            this.Controls.Add(this.weightBox);
            this.Controls.Add(this.itemLabel);
            this.Controls.Add(this.descriptionBox);
            this.Controls.Add(this.scanItemLabel);
            this.Controls.Add(this.itemNoBox);
            this.Controls.Add(this.receiptNoBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReceiptItem";
            this.Text = "ReceiptItem";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox qtyBox;
        private System.Windows.Forms.Label qtyLabel;
        private System.Windows.Forms.TextBox weightBox;
        private System.Windows.Forms.Label itemLabel;
        private System.Windows.Forms.TextBox descriptionBox;
        private System.Windows.Forms.Label scanItemLabel;
        private System.Windows.Forms.TextBox itemNoBox;
        private System.Windows.Forms.TextBox receiptNoBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox lengthBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox widthBox;
        private System.Windows.Forms.TextBox heightBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox qtyToReceiveBox;
        private System.Windows.Forms.ListBox logViewList;
    }
}