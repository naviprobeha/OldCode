namespace SmartInventory
{
    partial class MaintInv
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
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.locationBox = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.heIdBox = new System.Windows.Forms.TextBox();
            this.statusBox = new System.Windows.Forms.TextBox();
            this.sumQuantityBox = new System.Windows.Forms.TextBox();
            this.descriptionBox = new System.Windows.Forms.TextBox();
            this.itemNoBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(8, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(192, 32);
            this.label7.Text = "För att ändra aktuellt antal klicka i saldo-rutan nedan.";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(16, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 23);
            this.label4.Text = "Lagerplats:";
            // 
            // locationBox
            // 
            this.locationBox.Location = new System.Drawing.Point(88, 63);
            this.locationBox.Name = "locationBox";
            this.locationBox.ReadOnly = true;
            this.locationBox.Size = new System.Drawing.Size(136, 21);
            this.locationBox.TabIndex = 8;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular);
            this.button2.Location = new System.Drawing.Point(8, 231);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(224, 32);
            this.button2.TabIndex = 9;
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
            // panel1
            // 
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.heIdBox);
            this.panel1.Controls.Add(this.statusBox);
            this.panel1.Controls.Add(this.sumQuantityBox);
            this.panel1.Controls.Add(this.descriptionBox);
            this.panel1.Controls.Add(this.itemNoBox);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 87);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 128);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(16, 83);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 20);
            this.label8.Text = "Status:";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(16, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 20);
            this.label5.Text = "Beskrivning:";
            // 
            // heIdBox
            // 
            this.heIdBox.Location = new System.Drawing.Point(88, 104);
            this.heIdBox.Name = "heIdBox";
            this.heIdBox.ReadOnly = true;
            this.heIdBox.Size = new System.Drawing.Size(136, 21);
            this.heIdBox.TabIndex = 2;
            // 
            // statusBox
            // 
            this.statusBox.Location = new System.Drawing.Point(88, 80);
            this.statusBox.Name = "statusBox";
            this.statusBox.ReadOnly = true;
            this.statusBox.Size = new System.Drawing.Size(136, 21);
            this.statusBox.TabIndex = 3;
            // 
            // sumQuantityBox
            // 
            this.sumQuantityBox.Location = new System.Drawing.Point(88, 56);
            this.sumQuantityBox.Name = "sumQuantityBox";
            this.sumQuantityBox.Size = new System.Drawing.Size(136, 21);
            this.sumQuantityBox.TabIndex = 4;
            this.sumQuantityBox.GotFocus += new System.EventHandler(this.sumQuantityBox_GotFocus);
            // 
            // descriptionBox
            // 
            this.descriptionBox.Location = new System.Drawing.Point(88, 32);
            this.descriptionBox.Name = "descriptionBox";
            this.descriptionBox.ReadOnly = true;
            this.descriptionBox.Size = new System.Drawing.Size(136, 21);
            this.descriptionBox.TabIndex = 5;
            // 
            // itemNoBox
            // 
            this.itemNoBox.Location = new System.Drawing.Point(88, 8);
            this.itemNoBox.Name = "itemNoBox";
            this.itemNoBox.ReadOnly = true;
            this.itemNoBox.Size = new System.Drawing.Size(136, 21);
            this.itemNoBox.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 20);
            this.label3.Text = "Saldo:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 20);
            this.label2.Text = "Artikel:";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 20);
            this.label1.Text = "HE ID:";
            // 
            // MaintInv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.locationBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.panel1);
            this.Menu = this.mainMenu1;
            this.Name = "MaintInv";
            this.Text = "Lagervård - Information";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox locationBox;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox heIdBox;
        private System.Windows.Forms.TextBox statusBox;
        private System.Windows.Forms.TextBox sumQuantityBox;
        private System.Windows.Forms.TextBox descriptionBox;
        private System.Windows.Forms.TextBox itemNoBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}