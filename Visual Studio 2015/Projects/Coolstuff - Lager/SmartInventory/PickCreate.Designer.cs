namespace Navipro.SmartInventory
{
    partial class PickCreate
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
            this.label1 = new System.Windows.Forms.Label();
            this.qtyBulkBox = new System.Windows.Forms.TextBox();
            this.qtyPickBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.userBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.toBinBox = new System.Windows.Forms.TextBox();
            this.fromBinBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.maxOrderCount = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.wagonBox = new System.Windows.Forms.TextBox();
            this.logViewList = new System.Windows.Forms.ListBox();
            this.regionBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 21);
            this.label1.Text = "Skapa plock";
            // 
            // qtyBulkBox
            // 
            this.qtyBulkBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.qtyBulkBox.Location = new System.Drawing.Point(122, 85);
            this.qtyBulkBox.Name = "qtyBulkBox";
            this.qtyBulkBox.ReadOnly = true;
            this.qtyBulkBox.Size = new System.Drawing.Size(114, 23);
            this.qtyBulkBox.TabIndex = 13;
            // 
            // qtyPickBox
            // 
            this.qtyPickBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.qtyPickBox.Location = new System.Drawing.Point(4, 85);
            this.qtyPickBox.Name = "qtyPickBox";
            this.qtyPickBox.ReadOnly = true;
            this.qtyPickBox.Size = new System.Drawing.Size(112, 23);
            this.qtyPickBox.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(122, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 20);
            this.label2.Text = "Antal bulk-order";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(4, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 20);
            this.label3.Text = "Antal fack-order";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(4, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 20);
            this.label4.Text = "Användare";
            // 
            // userBox
            // 
            this.userBox.Location = new System.Drawing.Point(4, 126);
            this.userBox.Name = "userBox";
            this.userBox.Size = new System.Drawing.Size(232, 23);
            this.userBox.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(4, 149);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 20);
            this.label5.Text = "Plockvagn";
            // 
            // toBinBox
            // 
            this.toBinBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.toBinBox.Location = new System.Drawing.Point(122, 210);
            this.toBinBox.Name = "toBinBox";
            this.toBinBox.Size = new System.Drawing.Size(114, 23);
            this.toBinBox.TabIndex = 25;
            // 
            // fromBinBox
            // 
            this.fromBinBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.fromBinBox.Location = new System.Drawing.Point(4, 210);
            this.fromBinBox.Name = "fromBinBox";
            this.fromBinBox.Size = new System.Drawing.Size(112, 23);
            this.fromBinBox.TabIndex = 24;
            this.fromBinBox.GotFocus += new System.EventHandler(this.fromBinBox_GotFocus);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(121, 190);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(114, 20);
            this.label6.Text = "Gång t.o.m.";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(3, 190);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(112, 20);
            this.label7.Text = "Gång fr.o.m.";
            // 
            // maxOrderCount
            // 
            this.maxOrderCount.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.maxOrderCount.Location = new System.Drawing.Point(122, 166);
            this.maxOrderCount.Name = "maxOrderCount";
            this.maxOrderCount.Size = new System.Drawing.Size(114, 23);
            this.maxOrderCount.TabIndex = 30;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(4, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(114, 20);
            this.label8.Text = "Marknad";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(121, 149);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(112, 20);
            this.label9.Text = "Max antal order";
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.button5.Location = new System.Drawing.Point(122, 267);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(113, 47);
            this.button5.TabIndex = 40;
            this.button5.Text = "Skapa";
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(4, 239);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(231, 22);
            this.button1.TabIndex = 41;
            this.button1.Text = "Uppdatera";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.button2.Location = new System.Drawing.Point(4, 267);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(111, 47);
            this.button2.TabIndex = 42;
            this.button2.Text = "Avbryt";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // wagonBox
            // 
            this.wagonBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.wagonBox.Location = new System.Drawing.Point(4, 166);
            this.wagonBox.Name = "wagonBox";
            this.wagonBox.Size = new System.Drawing.Size(112, 23);
            this.wagonBox.TabIndex = 43;
            this.wagonBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.WagonBox_KeyPress);
            // 
            // logViewList
            // 
            this.logViewList.Location = new System.Drawing.Point(355, 81);
            this.logViewList.Name = "logViewList";
            this.logViewList.Size = new System.Drawing.Size(100, 98);
            this.logViewList.TabIndex = 54;
            // 
            // regionBox
            // 
            this.regionBox.Location = new System.Drawing.Point(4, 45);
            this.regionBox.Name = "regionBox";
            this.regionBox.Size = new System.Drawing.Size(232, 23);
            this.regionBox.TabIndex = 64;
            // 
            // PickCreate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(638, 455);
            this.Controls.Add(this.logViewList);
            this.Controls.Add(this.wagonBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.maxOrderCount);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.toBinBox);
            this.Controls.Add(this.fromBinBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.userBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.qtyBulkBox);
            this.Controls.Add(this.qtyPickBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.regionBox);
            this.Controls.Add(this.label8);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PickCreate";
            this.Text = "PickCreate";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox qtyBulkBox;
        private System.Windows.Forms.TextBox qtyPickBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox userBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox toBinBox;
        private System.Windows.Forms.TextBox fromBinBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox maxOrderCount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox wagonBox;
        private System.Windows.Forms.ListBox logViewList;
        private System.Windows.Forms.ComboBox regionBox;
    }
}