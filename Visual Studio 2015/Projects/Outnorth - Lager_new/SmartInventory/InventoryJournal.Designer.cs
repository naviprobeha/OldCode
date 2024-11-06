namespace Navipro.SmartInventory
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
            this.binBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.scanBox = new System.Windows.Forms.TextBox();
            this.description2Box = new System.Windows.Forms.TextBox();
            this.descriptionBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.brandBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.qtyBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.itemNoBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.logViewList = new System.Windows.Forms.ListBox();
            this.variantCodeBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // binBox
            // 
            this.binBox.Location = new System.Drawing.Point(3, 66);
            this.binBox.Name = "binBox";
            this.binBox.Size = new System.Drawing.Size(118, 23);
            this.binBox.TabIndex = 13;
            this.binBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.binBox_KeyPress);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 20);
            this.label3.Text = "Scanna lagerplats";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(127, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 20);
            this.label1.Text = "Scanna artikel";
            // 
            // scanBox
            // 
            this.scanBox.Location = new System.Drawing.Point(127, 66);
            this.scanBox.Name = "scanBox";
            this.scanBox.Size = new System.Drawing.Size(108, 23);
            this.scanBox.TabIndex = 17;
            this.scanBox.GotFocus += new System.EventHandler(this.scanBox_GotFocus_1);
            this.scanBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.scanBox_KeyPress_1);
            // 
            // description2Box
            // 
            this.description2Box.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.description2Box.Location = new System.Drawing.Point(3, 181);
            this.description2Box.Name = "description2Box";
            this.description2Box.ReadOnly = true;
            this.description2Box.Size = new System.Drawing.Size(232, 23);
            this.description2Box.TabIndex = 25;
            // 
            // descriptionBox
            // 
            this.descriptionBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.descriptionBox.Location = new System.Drawing.Point(3, 154);
            this.descriptionBox.Name = "descriptionBox";
            this.descriptionBox.ReadOnly = true;
            this.descriptionBox.Size = new System.Drawing.Size(232, 23);
            this.descriptionBox.TabIndex = 24;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(3, 136);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 20);
            this.label6.Text = "Artikel";
            // 
            // brandBox
            // 
            this.brandBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.brandBox.Location = new System.Drawing.Point(3, 227);
            this.brandBox.Name = "brandBox";
            this.brandBox.ReadOnly = true;
            this.brandBox.Size = new System.Drawing.Size(118, 23);
            this.brandBox.TabIndex = 23;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(3, 204);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 20);
            this.label5.Text = "Varumärke";
            // 
            // qtyBox
            // 
            this.qtyBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.qtyBox.Location = new System.Drawing.Point(127, 227);
            this.qtyBox.Name = "qtyBox";
            this.qtyBox.ReadOnly = true;
            this.qtyBox.Size = new System.Drawing.Size(108, 23);
            this.qtyBox.TabIndex = 31;
            this.qtyBox.GotFocus += new System.EventHandler(this.qtyBox_GotFocus);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(127, 207);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(108, 20);
            this.label7.Text = "Inventerat antal";
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.button5.Location = new System.Drawing.Point(121, 256);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(114, 47);
            this.button5.TabIndex = 37;
            this.button5.Text = "Avsluta";
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // itemNoBox
            // 
            this.itemNoBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.itemNoBox.Location = new System.Drawing.Point(3, 110);
            this.itemNoBox.Name = "itemNoBox";
            this.itemNoBox.ReadOnly = true;
            this.itemNoBox.Size = new System.Drawing.Size(118, 23);
            this.itemNoBox.TabIndex = 39;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 20);
            this.label2.Text = "Artikelnr";
            // 
            // logViewList
            // 
            this.logViewList.Location = new System.Drawing.Point(411, 102);
            this.logViewList.Name = "logViewList";
            this.logViewList.Size = new System.Drawing.Size(100, 98);
            this.logViewList.TabIndex = 46;
            this.logViewList.Visible = false;
            // 
            // variantCodeBox
            // 
            this.variantCodeBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.variantCodeBox.Location = new System.Drawing.Point(127, 110);
            this.variantCodeBox.Name = "variantCodeBox";
            this.variantCodeBox.ReadOnly = true;
            this.variantCodeBox.Size = new System.Drawing.Size(108, 23);
            this.variantCodeBox.TabIndex = 47;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(127, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 20);
            this.label4.Text = "Variantkod";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(3, 256);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(114, 47);
            this.button1.TabIndex = 55;
            this.button1.Text = "Byt lagerplats";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(4, 4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(196, 21);
            this.label8.Text = "Inventering - Journal";
            // 
            // InventoryJournal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(638, 455);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.logViewList);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.variantCodeBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.itemNoBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.qtyBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.description2Box);
            this.Controls.Add(this.descriptionBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.brandBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.scanBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.binBox);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InventoryJournal";
            this.Text = "InventoryJournal";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox binBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox scanBox;
        private System.Windows.Forms.TextBox description2Box;
        private System.Windows.Forms.TextBox descriptionBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox brandBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox qtyBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox itemNoBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox logViewList;
        private System.Windows.Forms.TextBox variantCodeBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label8;
    }
}