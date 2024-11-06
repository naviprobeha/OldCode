namespace SmartShipment
{
    partial class QuantityForm
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
            this.grossPriceBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.discountBox = new System.Windows.Forms.TextBox();
            this.totalBox = new System.Windows.Forms.TextBox();
            this.unitPriceBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.quantityBox = new System.Windows.Forms.TextBox();
            this.button12 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.descriptionBox = new System.Windows.Forms.TextBox();
            this.itemNoBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button14 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // grossPriceBox
            // 
            this.grossPriceBox.Location = new System.Drawing.Point(80, 56);
            this.grossPriceBox.Name = "grossPriceBox";
            this.grossPriceBox.ReadOnly = true;
            this.grossPriceBox.Size = new System.Drawing.Size(64, 21);
            this.grossPriceBox.TabIndex = 28;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(80, 42);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 16);
            this.label7.Text = "A-pris:";
            // 
            // discountBox
            // 
            this.discountBox.Location = new System.Drawing.Point(152, 98);
            this.discountBox.Name = "discountBox";
            this.discountBox.ReadOnly = true;
            this.discountBox.Size = new System.Drawing.Size(80, 21);
            this.discountBox.TabIndex = 30;
            this.discountBox.GotFocus += new System.EventHandler(this.discountBox_GotFocus);
            // 
            // totalBox
            // 
            this.totalBox.Location = new System.Drawing.Point(152, 138);
            this.totalBox.Name = "totalBox";
            this.totalBox.ReadOnly = true;
            this.totalBox.Size = new System.Drawing.Size(80, 21);
            this.totalBox.TabIndex = 31;
            this.totalBox.GotFocus += new System.EventHandler(this.totalBox_GotFocus);
            // 
            // unitPriceBox
            // 
            this.unitPriceBox.Location = new System.Drawing.Point(152, 56);
            this.unitPriceBox.Name = "unitPriceBox";
            this.unitPriceBox.ReadOnly = true;
            this.unitPriceBox.Size = new System.Drawing.Size(80, 21);
            this.unitPriceBox.TabIndex = 32;
            this.unitPriceBox.GotFocus += new System.EventHandler(this.unitPriceBox_GotFocus);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(152, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.Text = "A-pris Netto:";
            // 
            // quantityBox
            // 
            this.quantityBox.Location = new System.Drawing.Point(8, 56);
            this.quantityBox.Name = "quantityBox";
            this.quantityBox.ReadOnly = true;
            this.quantityBox.Size = new System.Drawing.Size(64, 21);
            this.quantityBox.TabIndex = 34;
            this.quantityBox.TextChanged += new System.EventHandler(this.quantityBox_TextChanged_1);
            this.quantityBox.GotFocus += new System.EventHandler(this.quantityBox_GotFocus);
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(152, 162);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(80, 24);
            this.button12.TabIndex = 35;
            this.button12.Text = "CL";
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(8, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 16);
            this.label6.Text = "Antal:";
            // 
            // descriptionBox
            // 
            this.descriptionBox.Location = new System.Drawing.Point(80, 18);
            this.descriptionBox.Name = "descriptionBox";
            this.descriptionBox.ReadOnly = true;
            this.descriptionBox.Size = new System.Drawing.Size(152, 21);
            this.descriptionBox.TabIndex = 37;
            // 
            // itemNoBox
            // 
            this.itemNoBox.Location = new System.Drawing.Point(8, 18);
            this.itemNoBox.Name = "itemNoBox";
            this.itemNoBox.ReadOnly = true;
            this.itemNoBox.Size = new System.Drawing.Size(64, 21);
            this.itemNoBox.TabIndex = 38;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(80, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 16);
            this.label2.Text = "Beskrivning:";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.Text = "Artikelnr:";
            // 
            // button14
            // 
            this.button14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular);
            this.button14.Location = new System.Drawing.Point(104, 226);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(40, 40);
            this.button14.TabIndex = 41;
            this.button14.Text = ",";
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // button13
            // 
            this.button13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular);
            this.button13.Location = new System.Drawing.Point(56, 226);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(40, 40);
            this.button13.TabIndex = 42;
            this.button13.Text = "0";
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(152, 194);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(80, 24);
            this.button11.TabIndex = 43;
            this.button11.Text = "Avbryt";
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(152, 226);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(80, 40);
            this.button10.TabIndex = 44;
            this.button10.Text = "OK";
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button9
            // 
            this.button9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular);
            this.button9.Location = new System.Drawing.Point(104, 82);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(40, 40);
            this.button9.TabIndex = 45;
            this.button9.Text = "9";
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button8
            // 
            this.button8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular);
            this.button8.Location = new System.Drawing.Point(56, 82);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(40, 40);
            this.button8.TabIndex = 46;
            this.button8.Text = "8";
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button7
            // 
            this.button7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular);
            this.button7.Location = new System.Drawing.Point(8, 82);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(40, 40);
            this.button7.TabIndex = 47;
            this.button7.Text = "7";
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button6
            // 
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular);
            this.button6.Location = new System.Drawing.Point(104, 130);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(40, 40);
            this.button6.TabIndex = 48;
            this.button6.Text = "6";
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular);
            this.button5.Location = new System.Drawing.Point(56, 130);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(40, 40);
            this.button5.TabIndex = 49;
            this.button5.Text = "5";
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular);
            this.button4.Location = new System.Drawing.Point(8, 130);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(40, 40);
            this.button4.TabIndex = 50;
            this.button4.Text = "4";
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular);
            this.button3.Location = new System.Drawing.Point(104, 178);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(40, 40);
            this.button3.TabIndex = 51;
            this.button3.Text = "3";
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular);
            this.button2.Location = new System.Drawing.Point(56, 178);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(40, 40);
            this.button2.TabIndex = 52;
            this.button2.Text = "2";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular);
            this.button1.Location = new System.Drawing.Point(8, 178);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(40, 40);
            this.button1.TabIndex = 53;
            this.button1.Text = "1";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(152, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 16);
            this.label4.Text = "Totalt:";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(152, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 16);
            this.label5.Text = "Rabatt (%):";
            // 
            // QuantityForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.grossPriceBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.discountBox);
            this.Controls.Add(this.totalBox);
            this.Controls.Add(this.unitPriceBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.quantityBox);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.descriptionBox);
            this.Controls.Add(this.itemNoBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button14);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Menu = this.mainMenu1;
            this.Name = "QuantityForm";
            this.Text = "Antal";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox grossPriceBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox discountBox;
        private System.Windows.Forms.TextBox totalBox;
        private System.Windows.Forms.TextBox unitPriceBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox quantityBox;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox descriptionBox;
        private System.Windows.Forms.TextBox itemNoBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}