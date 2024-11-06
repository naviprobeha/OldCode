namespace SmartOrder
{
    partial class InventoryJournal
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
            this.label8 = new System.Windows.Forms.Label();
            this.itemNoBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.qtyBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.description2Box = new System.Windows.Forms.TextBox();
            this.descriptionBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.scanBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.logViewList = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(8, 7);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(196, 21);
            this.label8.Text = "Inventering - Journal";
            // 
            // itemNoBox
            // 
            this.itemNoBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.itemNoBox.Location = new System.Drawing.Point(7, 113);
            this.itemNoBox.Name = "itemNoBox";
            this.itemNoBox.ReadOnly = true;
            this.itemNoBox.Size = new System.Drawing.Size(232, 23);
            this.itemNoBox.TabIndex = 71;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(7, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 20);
            this.label2.Text = "Artikelnr";
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.button5.Location = new System.Drawing.Point(125, 259);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(114, 47);
            this.button5.TabIndex = 70;
            this.button5.Text = "Avsluta";
            this.button5.Click += new System.EventHandler(this.button5_Click_1);
            // 
            // qtyBox
            // 
            this.qtyBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.qtyBox.Location = new System.Drawing.Point(125, 230);
            this.qtyBox.Name = "qtyBox";
            this.qtyBox.ReadOnly = true;
            this.qtyBox.Size = new System.Drawing.Size(114, 23);
            this.qtyBox.TabIndex = 69;
            this.qtyBox.GotFocus += new System.EventHandler(this.qtyBox_GotFocus_1);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(125, 210);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(108, 20);
            this.label7.Text = "Inventerat antal";
            // 
            // description2Box
            // 
            this.description2Box.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.description2Box.Location = new System.Drawing.Point(7, 184);
            this.description2Box.Name = "description2Box";
            this.description2Box.ReadOnly = true;
            this.description2Box.Size = new System.Drawing.Size(232, 23);
            this.description2Box.TabIndex = 68;
            // 
            // descriptionBox
            // 
            this.descriptionBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.descriptionBox.Location = new System.Drawing.Point(7, 157);
            this.descriptionBox.Name = "descriptionBox";
            this.descriptionBox.ReadOnly = true;
            this.descriptionBox.Size = new System.Drawing.Size(232, 23);
            this.descriptionBox.TabIndex = 67;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(7, 139);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 20);
            this.label6.Text = "Artikel";
            // 
            // scanBox
            // 
            this.scanBox.Location = new System.Drawing.Point(8, 69);
            this.scanBox.Name = "scanBox";
            this.scanBox.Size = new System.Drawing.Size(231, 23);
            this.scanBox.TabIndex = 65;
            this.scanBox.GotFocus += new System.EventHandler(this.scanBox_GotFocus);
            this.scanBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.scanBox_KeyPress);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 20);
            this.label1.Text = "Scanna artikel";
            // 
            // logViewList
            // 
            this.logViewList.Location = new System.Drawing.Point(8, 224);
            this.logViewList.Name = "logViewList";
            this.logViewList.Size = new System.Drawing.Size(100, 82);
            this.logViewList.TabIndex = 80;
            this.logViewList.Visible = false;
            // 
            // InventoryJournal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(247, 312);
            this.Controls.Add(this.logViewList);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.itemNoBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.qtyBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.description2Box);
            this.Controls.Add(this.descriptionBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.scanBox);
            this.Controls.Add(this.label1);
            this.Name = "InventoryJournal";
            this.Text = "InventoryJournal";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox itemNoBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox qtyBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox description2Box;
        private System.Windows.Forms.TextBox descriptionBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox scanBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox logViewList;
    }
}