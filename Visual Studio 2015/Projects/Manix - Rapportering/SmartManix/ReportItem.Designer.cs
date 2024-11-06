namespace Navipro.SmartInventory
{
    partial class ReportItem
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
            this.button1 = new System.Windows.Forms.Button();
            this.descriptionBox = new System.Windows.Forms.TextBox();
            this.qtyLabel = new System.Windows.Forms.Label();
            this.prodOrderLineNoBox = new System.Windows.Forms.TextBox();
            this.itemLabel = new System.Windows.Forms.Label();
            this.prodOrderNoBox = new System.Windows.Forms.TextBox();
            this.scanItemLabel = new System.Windows.Forms.Label();
            this.userBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.itemNoBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.statusBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.logViewList = new System.Windows.Forms.ListBox();
            this.scanBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(4, 261);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 49);
            this.button1.TabIndex = 48;
            this.button1.Text = "Avbryt";
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // descriptionBox
            // 
            this.descriptionBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.descriptionBox.Location = new System.Drawing.Point(4, 177);
            this.descriptionBox.Name = "descriptionBox";
            this.descriptionBox.ReadOnly = true;
            this.descriptionBox.Size = new System.Drawing.Size(232, 23);
            this.descriptionBox.TabIndex = 47;
            // 
            // qtyLabel
            // 
            this.qtyLabel.Location = new System.Drawing.Point(4, 159);
            this.qtyLabel.Name = "qtyLabel";
            this.qtyLabel.Size = new System.Drawing.Size(107, 20);
            this.qtyLabel.Text = "Beskrivning";
            // 
            // prodOrderLineNoBox
            // 
            this.prodOrderLineNoBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.prodOrderLineNoBox.Location = new System.Drawing.Point(122, 133);
            this.prodOrderLineNoBox.Name = "prodOrderLineNoBox";
            this.prodOrderLineNoBox.ReadOnly = true;
            this.prodOrderLineNoBox.Size = new System.Drawing.Size(114, 23);
            this.prodOrderLineNoBox.TabIndex = 45;
            // 
            // itemLabel
            // 
            this.itemLabel.Location = new System.Drawing.Point(122, 116);
            this.itemLabel.Name = "itemLabel";
            this.itemLabel.Size = new System.Drawing.Size(54, 20);
            this.itemLabel.Text = "Radnr";
            // 
            // prodOrderNoBox
            // 
            this.prodOrderNoBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.prodOrderNoBox.Location = new System.Drawing.Point(4, 133);
            this.prodOrderNoBox.Name = "prodOrderNoBox";
            this.prodOrderNoBox.ReadOnly = true;
            this.prodOrderNoBox.Size = new System.Drawing.Size(112, 23);
            this.prodOrderNoBox.TabIndex = 44;
            // 
            // scanItemLabel
            // 
            this.scanItemLabel.Location = new System.Drawing.Point(4, 116);
            this.scanItemLabel.Name = "scanItemLabel";
            this.scanItemLabel.Size = new System.Drawing.Size(107, 20);
            this.scanItemLabel.Text = "Prod. ordernr";
            // 
            // userBox
            // 
            this.userBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.userBox.Location = new System.Drawing.Point(4, 42);
            this.userBox.Name = "userBox";
            this.userBox.ReadOnly = true;
            this.userBox.Size = new System.Drawing.Size(232, 23);
            this.userBox.TabIndex = 42;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(4, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 20);
            this.label4.Text = "Anställningsnr";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 21);
            this.label1.Text = "Återrapportering";
            // 
            // itemNoBox
            // 
            this.itemNoBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.itemNoBox.Location = new System.Drawing.Point(4, 221);
            this.itemNoBox.Name = "itemNoBox";
            this.itemNoBox.ReadOnly = true;
            this.itemNoBox.Size = new System.Drawing.Size(112, 23);
            this.itemNoBox.TabIndex = 56;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(4, 203);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 20);
            this.label3.Text = "Artikelnr";
            // 
            // statusBox
            // 
            this.statusBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.statusBox.Location = new System.Drawing.Point(122, 221);
            this.statusBox.Name = "statusBox";
            this.statusBox.ReadOnly = true;
            this.statusBox.Size = new System.Drawing.Size(114, 23);
            this.statusBox.TabIndex = 59;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(122, 203);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 20);
            this.label5.Text = "Status";
            // 
            // logViewList
            // 
            this.logViewList.Location = new System.Drawing.Point(269, 178);
            this.logViewList.Name = "logViewList";
            this.logViewList.Size = new System.Drawing.Size(100, 98);
            this.logViewList.TabIndex = 68;
            this.logViewList.Visible = false;
            // 
            // scanBox
            // 
            this.scanBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.scanBox.Location = new System.Drawing.Point(4, 88);
            this.scanBox.Name = "scanBox";
            this.scanBox.Size = new System.Drawing.Size(232, 23);
            this.scanBox.TabIndex = 79;
            this.scanBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.scanBox_KeyPress);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(4, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 20);
            this.label6.Text = "ID-nr";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.button2.Location = new System.Drawing.Point(129, 261);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(107, 49);
            this.button2.TabIndex = 88;
            this.button2.Text = "Bokför";
            // 
            // ReportItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(638, 455);
            this.Controls.Add(this.logViewList);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.scanBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.statusBox);
            this.Controls.Add(this.itemNoBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.descriptionBox);
            this.Controls.Add(this.qtyLabel);
            this.Controls.Add(this.prodOrderLineNoBox);
            this.Controls.Add(this.itemLabel);
            this.Controls.Add(this.prodOrderNoBox);
            this.Controls.Add(this.scanItemLabel);
            this.Controls.Add(this.userBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReportItem";
            this.Text = "ReceiptItem";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox descriptionBox;
        private System.Windows.Forms.Label qtyLabel;
        private System.Windows.Forms.TextBox prodOrderLineNoBox;
        private System.Windows.Forms.Label itemLabel;
        private System.Windows.Forms.TextBox prodOrderNoBox;
        private System.Windows.Forms.Label scanItemLabel;
        private System.Windows.Forms.TextBox userBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox itemNoBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox statusBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox logViewList;
        private System.Windows.Forms.TextBox scanBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button2;
    }
}