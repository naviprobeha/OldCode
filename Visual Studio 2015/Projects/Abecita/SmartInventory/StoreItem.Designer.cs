namespace SmartInventory
{
    partial class StoreItem
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.binCodeBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.statusBox = new System.Windows.Forms.TextBox();
            this.sumQuantityBox = new System.Windows.Forms.TextBox();
            this.itemNoBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.scanBinBox = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.packageScanBox = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(128, 226);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(96, 32);
            this.button2.TabIndex = 6;
            this.button2.Text = "Huvudmeny";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(16, 226);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 32);
            this.button1.TabIndex = 7;
            this.button1.Text = "Tillbaka";
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(8, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(200, 20);
            this.label6.Text = "Inlagring";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.binCodeBox);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.statusBox);
            this.panel1.Controls.Add(this.sumQuantityBox);
            this.panel1.Controls.Add(this.itemNoBox);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 90);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 80);
            // 
            // binCodeBox
            // 
            this.binCodeBox.Location = new System.Drawing.Point(16, 16);
            this.binCodeBox.Name = "binCodeBox";
            this.binCodeBox.ReadOnly = true;
            this.binCodeBox.Size = new System.Drawing.Size(96, 21);
            this.binCodeBox.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(16, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 20);
            this.label7.Text = "Lagerplats:";
            // 
            // statusBox
            // 
            this.statusBox.Location = new System.Drawing.Point(128, 56);
            this.statusBox.Name = "statusBox";
            this.statusBox.ReadOnly = true;
            this.statusBox.Size = new System.Drawing.Size(96, 21);
            this.statusBox.TabIndex = 2;
            // 
            // sumQuantityBox
            // 
            this.sumQuantityBox.Location = new System.Drawing.Point(16, 56);
            this.sumQuantityBox.Name = "sumQuantityBox";
            this.sumQuantityBox.ReadOnly = true;
            this.sumQuantityBox.Size = new System.Drawing.Size(96, 21);
            this.sumQuantityBox.TabIndex = 3;
            // 
            // itemNoBox
            // 
            this.itemNoBox.Location = new System.Drawing.Point(128, 16);
            this.itemNoBox.Name = "itemNoBox";
            this.itemNoBox.ReadOnly = true;
            this.itemNoBox.Size = new System.Drawing.Size(96, 21);
            this.itemNoBox.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(128, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 20);
            this.label3.Text = "Status lpl:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 20);
            this.label2.Text = "Saldo:";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(128, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 20);
            this.label1.Text = "Artikelnr:";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.scanBinBox);
            this.panel3.Location = new System.Drawing.Point(0, 34);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(240, 56);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(16, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(208, 20);
            this.label5.Text = "Scanna lagerplats:";
            // 
            // scanBinBox
            // 
            this.scanBinBox.Location = new System.Drawing.Point(16, 29);
            this.scanBinBox.Name = "scanBinBox";
            this.scanBinBox.Size = new System.Drawing.Size(208, 21);
            this.scanBinBox.TabIndex = 1;
            this.scanBinBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.scanBinBox_KeyPress);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.packageScanBox);
            this.panel2.Location = new System.Drawing.Point(0, 170);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(240, 56);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(160, 17);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(64, 32);
            this.button3.TabIndex = 0;
            this.button3.Text = "Ändra";
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(16, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 20);
            this.label4.Text = "Scanna kolli ID:";
            // 
            // packageScanBox
            // 
            this.packageScanBox.Location = new System.Drawing.Point(16, 29);
            this.packageScanBox.Name = "packageScanBox";
            this.packageScanBox.Size = new System.Drawing.Size(136, 21);
            this.packageScanBox.TabIndex = 2;
            this.packageScanBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.packageScanBox_KeyPress);
            // 
            // StoreItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Menu = this.mainMenu1;
            this.Name = "StoreItem";
            this.Text = "Inlagring";
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox binCodeBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox statusBox;
        private System.Windows.Forms.TextBox sumQuantityBox;
        private System.Windows.Forms.TextBox itemNoBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox scanBinBox;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox packageScanBox;
    }
}