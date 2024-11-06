namespace SmartOrder
{
    partial class OrderItem
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
            this.label2 = new System.Windows.Forms.Label();
            this.itemNoBox = new System.Windows.Forms.TextBox();
            this.descriptionBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.quantity = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.unitPriceBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.amountBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.discount = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.year = new System.Windows.Forms.ComboBox();
            this.month = new System.Windows.Forms.ComboBox();
            this.day = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.Text = "Artikelnr";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(87, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.Text = "Beskrivning";
            // 
            // itemNoBox
            // 
            this.itemNoBox.Location = new System.Drawing.Point(4, 21);
            this.itemNoBox.Name = "itemNoBox";
            this.itemNoBox.ReadOnly = true;
            this.itemNoBox.Size = new System.Drawing.Size(77, 23);
            this.itemNoBox.TabIndex = 2;
            this.itemNoBox.GotFocus += new System.EventHandler(this.itemNoBox_GotFocus);
            // 
            // descriptionBox
            // 
            this.descriptionBox.Location = new System.Drawing.Point(88, 21);
            this.descriptionBox.Name = "descriptionBox";
            this.descriptionBox.ReadOnly = true;
            this.descriptionBox.Size = new System.Drawing.Size(148, 23);
            this.descriptionBox.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(4, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 20);
            this.label3.Text = "Antal:";
            // 
            // quantity
            // 
            this.quantity.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.quantity.Location = new System.Drawing.Point(4, 75);
            this.quantity.Name = "quantity";
            this.quantity.Size = new System.Drawing.Size(77, 29);
            this.quantity.TabIndex = 5;
            this.quantity.GotFocus += new System.EventHandler(this.quantity_GotFocus);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(88, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 20);
            this.label4.Text = "A-pris:";
            // 
            // unitPriceBox
            // 
            this.unitPriceBox.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.unitPriceBox.Location = new System.Drawing.Point(88, 75);
            this.unitPriceBox.Name = "unitPriceBox";
            this.unitPriceBox.Size = new System.Drawing.Size(63, 29);
            this.unitPriceBox.TabIndex = 7;
            this.unitPriceBox.GotFocus += new System.EventHandler(this.unitPriceBox_GotFocus);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(158, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 20);
            this.label5.Text = "Belopp:";
            // 
            // amountBox
            // 
            this.amountBox.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.amountBox.Location = new System.Drawing.Point(158, 75);
            this.amountBox.Name = "amountBox";
            this.amountBox.Size = new System.Drawing.Size(78, 29);
            this.amountBox.TabIndex = 9;
            this.amountBox.GotFocus += new System.EventHandler(this.amountBox_GotFocus);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(4, 107);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 20);
            this.label6.Text = "Rabatt:";
            // 
            // discount
            // 
            this.discount.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.discount.Location = new System.Drawing.Point(4, 125);
            this.discount.Name = "discount";
            this.discount.Size = new System.Drawing.Size(77, 29);
            this.discount.TabIndex = 11;
            this.discount.GotFocus += new System.EventHandler(this.discount_GotFocus);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(84, 136);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 20);
            this.label7.Text = "%";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(4, 166);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(115, 20);
            this.label8.Text = "Leveransdatum:";
            // 
            // year
            // 
            this.year.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.year.Location = new System.Drawing.Point(4, 184);
            this.year.Name = "year";
            this.year.Size = new System.Drawing.Size(62, 29);
            this.year.TabIndex = 14;
            this.year.SelectedIndexChanged += new System.EventHandler(this.year_SelectedIndexChanged);
            // 
            // month
            // 
            this.month.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.month.Location = new System.Drawing.Point(73, 184);
            this.month.Name = "month";
            this.month.Size = new System.Drawing.Size(46, 29);
            this.month.TabIndex = 15;
            this.month.SelectedIndexChanged += new System.EventHandler(this.month_SelectedIndexChanged);
            // 
            // day
            // 
            this.day.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.day.Location = new System.Drawing.Point(126, 184);
            this.day.Name = "day";
            this.day.Size = new System.Drawing.Size(46, 29);
            this.day.TabIndex = 16;
            this.day.SelectedIndexChanged += new System.EventHandler(this.day_SelectedIndexChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(38, 266);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(96, 33);
            this.button2.TabIndex = 18;
            this.button2.Text = "Tillbaka";
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(140, 266);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 33);
            this.button1.TabIndex = 17;
            this.button1.Text = "Klar";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // OrderItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(247, 312);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.day);
            this.Controls.Add(this.month);
            this.Controls.Add(this.year);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.discount);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.amountBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.unitPriceBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.quantity);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.descriptionBox);
            this.Controls.Add(this.itemNoBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "OrderItem";
            this.Text = "OrderItem";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox itemNoBox;
        private System.Windows.Forms.TextBox descriptionBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox quantity;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox unitPriceBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox amountBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox discount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox year;
        private System.Windows.Forms.ComboBox month;
        private System.Windows.Forms.ComboBox day;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
    }
}