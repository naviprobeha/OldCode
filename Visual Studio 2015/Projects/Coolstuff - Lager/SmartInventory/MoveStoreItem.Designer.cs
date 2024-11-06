namespace Navipro.SmartInventory
{
    partial class MoveStoreItem
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
            this.logViewList = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.qtyBox = new System.Windows.Forms.TextBox();
            this.qtyLabel = new System.Windows.Forms.Label();
            this.description2Box = new System.Windows.Forms.TextBox();
            this.descriptionBox = new System.Windows.Forms.TextBox();
            this.itemLabel = new System.Windows.Forms.Label();
            this.scanEanBox = new System.Windows.Forms.TextBox();
            this.scanItemLabel = new System.Windows.Forms.Label();
            this.wagonBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.binBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // logViewList
            // 
            this.logViewList.Location = new System.Drawing.Point(316, 110);
            this.logViewList.Name = "logViewList";
            this.logViewList.Size = new System.Drawing.Size(100, 98);
            this.logViewList.TabIndex = 62;
            this.logViewList.Visible = false;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(4, 263);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 49);
            this.button1.TabIndex = 60;
            this.button1.Text = "Stäng";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // qtyBox
            // 
            this.qtyBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.qtyBox.Location = new System.Drawing.Point(4, 201);
            this.qtyBox.Name = "qtyBox";
            this.qtyBox.ReadOnly = true;
            this.qtyBox.Size = new System.Drawing.Size(90, 23);
            this.qtyBox.TabIndex = 59;
            this.qtyBox.GotFocus += new System.EventHandler(this.qtyBox_GotFocus_1);
            // 
            // qtyLabel
            // 
            this.qtyLabel.Location = new System.Drawing.Point(4, 182);
            this.qtyLabel.Name = "qtyLabel";
            this.qtyLabel.Size = new System.Drawing.Size(54, 20);
            this.qtyLabel.Text = "Antal";
            // 
            // description2Box
            // 
            this.description2Box.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.description2Box.Location = new System.Drawing.Point(4, 156);
            this.description2Box.Name = "description2Box";
            this.description2Box.ReadOnly = true;
            this.description2Box.Size = new System.Drawing.Size(232, 23);
            this.description2Box.TabIndex = 58;
            // 
            // descriptionBox
            // 
            this.descriptionBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.descriptionBox.Location = new System.Drawing.Point(4, 127);
            this.descriptionBox.Name = "descriptionBox";
            this.descriptionBox.ReadOnly = true;
            this.descriptionBox.Size = new System.Drawing.Size(232, 23);
            this.descriptionBox.TabIndex = 57;
            // 
            // itemLabel
            // 
            this.itemLabel.Location = new System.Drawing.Point(4, 109);
            this.itemLabel.Name = "itemLabel";
            this.itemLabel.Size = new System.Drawing.Size(100, 20);
            this.itemLabel.Text = "Artikel";
            // 
            // scanEanBox
            // 
            this.scanEanBox.Location = new System.Drawing.Point(4, 83);
            this.scanEanBox.Name = "scanEanBox";
            this.scanEanBox.Size = new System.Drawing.Size(232, 23);
            this.scanEanBox.TabIndex = 56;
            this.scanEanBox.GotFocus += new System.EventHandler(this.scanBinBox_GotFocus);
            this.scanEanBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.scanEanBox_KeyPress_1);
            // 
            // scanItemLabel
            // 
            this.scanItemLabel.Location = new System.Drawing.Point(4, 66);
            this.scanItemLabel.Name = "scanItemLabel";
            this.scanItemLabel.Size = new System.Drawing.Size(142, 20);
            this.scanItemLabel.Text = "Scanna EAN-kod";
            // 
            // wagonBox
            // 
            this.wagonBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.wagonBox.Location = new System.Drawing.Point(4, 42);
            this.wagonBox.Name = "wagonBox";
            this.wagonBox.ReadOnly = true;
            this.wagonBox.Size = new System.Drawing.Size(100, 23);
            this.wagonBox.TabIndex = 54;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(4, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 20);
            this.label4.Text = "Vagn";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(232, 21);
            this.label1.Text = "Lagerflytt - Inlagring - Artikel";
            // 
            // binBox
            // 
            this.binBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.binBox.Location = new System.Drawing.Point(110, 42);
            this.binBox.Name = "binBox";
            this.binBox.ReadOnly = true;
            this.binBox.Size = new System.Drawing.Size(126, 23);
            this.binBox.TabIndex = 68;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(110, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.Text = "Lagerplats";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.button2.Location = new System.Drawing.Point(128, 263);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(108, 49);
            this.button2.TabIndex = 92;
            this.button2.Text = "Bekräfta";
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // MoveStoreItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(638, 455);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.logViewList);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.qtyBox);
            this.Controls.Add(this.qtyLabel);
            this.Controls.Add(this.description2Box);
            this.Controls.Add(this.descriptionBox);
            this.Controls.Add(this.itemLabel);
            this.Controls.Add(this.scanEanBox);
            this.Controls.Add(this.scanItemLabel);
            this.Controls.Add(this.wagonBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.binBox);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MoveStoreItem";
            this.Text = "MoveStoreItem";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox logViewList;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox qtyBox;
        private System.Windows.Forms.Label qtyLabel;
        private System.Windows.Forms.TextBox description2Box;
        private System.Windows.Forms.TextBox descriptionBox;
        private System.Windows.Forms.Label itemLabel;
        private System.Windows.Forms.TextBox scanEanBox;
        private System.Windows.Forms.Label scanItemLabel;
        private System.Windows.Forms.TextBox wagonBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox binBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
    }
}