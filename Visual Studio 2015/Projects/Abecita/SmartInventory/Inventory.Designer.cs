namespace SmartInventory
{
    partial class Inventory
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.binDepartment = new System.Windows.Forms.ComboBox();
            this.scanBinBox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.binCodeBox = new System.Windows.Forms.TextBox();
            this.statusBox = new System.Windows.Forms.TextBox();
            this.descriptionBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.heIdBox = new System.Windows.Forms.TextBox();
            this.sumQuantityBox = new System.Windows.Forms.TextBox();
            this.itemNoBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.binLabel = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular);
            this.button1.Location = new System.Drawing.Point(128, 231);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 32);
            this.button1.TabIndex = 5;
            this.button1.Text = "Ändra";
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular);
            this.button2.Location = new System.Drawing.Point(8, 231);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(104, 32);
            this.button2.TabIndex = 6;
            this.button2.Text = "Tillbaka";
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(8, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(200, 20);
            this.label6.Text = "Lagervård - Inventering";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.binDepartment);
            this.panel3.Controls.Add(this.scanBinBox);
            this.panel3.Location = new System.Drawing.Point(0, 39);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(240, 56);
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(8, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(112, 20);
            this.label7.Text = "Scanna lagerplats:";
            // 
            // binDepartment
            // 
            this.binDepartment.Location = new System.Drawing.Point(168, 32);
            this.binDepartment.Name = "binDepartment";
            this.binDepartment.Size = new System.Drawing.Size(56, 22);
            this.binDepartment.TabIndex = 1;
            this.binDepartment.SelectedIndexChanged += new System.EventHandler(this.binDepartment_SelectedIndexChanged);
            // 
            // scanBinBox
            // 
            this.scanBinBox.Location = new System.Drawing.Point(8, 32);
            this.scanBinBox.Name = "scanBinBox";
            this.scanBinBox.Size = new System.Drawing.Size(152, 21);
            this.scanBinBox.TabIndex = 2;
            this.scanBinBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.scanBinBox_KeyPress);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.listBox1);
            this.panel1.Controls.Add(this.binCodeBox);
            this.panel1.Controls.Add(this.statusBox);
            this.panel1.Controls.Add(this.descriptionBox);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.heIdBox);
            this.panel1.Controls.Add(this.sumQuantityBox);
            this.panel1.Controls.Add(this.itemNoBox);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.binLabel);
            this.panel1.Location = new System.Drawing.Point(0, 95);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 136);
            // 
            // listBox1
            // 
            this.listBox1.Location = new System.Drawing.Point(8, 8);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(224, 114);
            this.listBox1.TabIndex = 0;
            this.listBox1.Visible = false;
            // 
            // binCodeBox
            // 
            this.binCodeBox.Location = new System.Drawing.Point(8, 24);
            this.binCodeBox.Name = "binCodeBox";
            this.binCodeBox.ReadOnly = true;
            this.binCodeBox.Size = new System.Drawing.Size(104, 21);
            this.binCodeBox.TabIndex = 1;
            // 
            // statusBox
            // 
            this.statusBox.Location = new System.Drawing.Point(128, 104);
            this.statusBox.Name = "statusBox";
            this.statusBox.ReadOnly = true;
            this.statusBox.Size = new System.Drawing.Size(104, 21);
            this.statusBox.TabIndex = 2;
            // 
            // descriptionBox
            // 
            this.descriptionBox.Location = new System.Drawing.Point(8, 64);
            this.descriptionBox.Name = "descriptionBox";
            this.descriptionBox.ReadOnly = true;
            this.descriptionBox.Size = new System.Drawing.Size(104, 21);
            this.descriptionBox.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(128, 88);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 20);
            this.label8.Text = "Status:";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(8, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 20);
            this.label5.Text = "Beskrivning:";
            // 
            // heIdBox
            // 
            this.heIdBox.Location = new System.Drawing.Point(8, 104);
            this.heIdBox.Name = "heIdBox";
            this.heIdBox.ReadOnly = true;
            this.heIdBox.Size = new System.Drawing.Size(104, 21);
            this.heIdBox.TabIndex = 6;
            // 
            // sumQuantityBox
            // 
            this.sumQuantityBox.Location = new System.Drawing.Point(128, 64);
            this.sumQuantityBox.Name = "sumQuantityBox";
            this.sumQuantityBox.ReadOnly = true;
            this.sumQuantityBox.Size = new System.Drawing.Size(104, 21);
            this.sumQuantityBox.TabIndex = 7;
            // 
            // itemNoBox
            // 
            this.itemNoBox.Location = new System.Drawing.Point(128, 24);
            this.itemNoBox.Name = "itemNoBox";
            this.itemNoBox.ReadOnly = true;
            this.itemNoBox.Size = new System.Drawing.Size(104, 21);
            this.itemNoBox.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(128, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 20);
            this.label3.Text = "Saldo:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(128, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 20);
            this.label2.Text = "Artikel:";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 20);
            this.label1.Text = "HE ID:";
            // 
            // binLabel
            // 
            this.binLabel.Location = new System.Drawing.Point(8, 8);
            this.binLabel.Name = "binLabel";
            this.binLabel.Size = new System.Drawing.Size(100, 20);
            this.binLabel.Text = "Lagerplats:";
            // 
            // Inventory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Menu = this.mainMenu1;
            this.Name = "Inventory";
            this.Text = "Inventering";
            this.Load += new System.EventHandler(this.Inventory_Load);
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox binDepartment;
        private System.Windows.Forms.TextBox scanBinBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox binCodeBox;
        private System.Windows.Forms.TextBox statusBox;
        private System.Windows.Forms.TextBox descriptionBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox heIdBox;
        private System.Windows.Forms.TextBox sumQuantityBox;
        private System.Windows.Forms.TextBox itemNoBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label binLabel;
    }
}