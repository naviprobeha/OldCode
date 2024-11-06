namespace SmartShipment
{
    partial class OrderHeader
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
            this.scanBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.referenceNoBox = new System.Windows.Forms.TextBox();
            this.goodsmarkBox = new System.Windows.Forms.TextBox();
            this.mark = new System.Windows.Forms.Label();
            this.phoneNoBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.contactNameBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.customerNameBox = new System.Windows.Forms.TextBox();
            this.customerNoBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.inputPanel1 = new Microsoft.WindowsCE.Forms.InputPanel();
            this.SuspendLayout();
            // 
            // scanBox
            // 
            this.scanBox.Location = new System.Drawing.Point(6, 42);
            this.scanBox.Name = "scanBox";
            this.scanBox.Size = new System.Drawing.Size(224, 21);
            this.scanBox.TabIndex = 18;
            this.scanBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.scanBox_KeyPress_1);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(6, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 20);
            this.label7.Text = "Scanna";
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular);
            this.button3.Location = new System.Drawing.Point(6, 226);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(104, 40);
            this.button3.TabIndex = 20;
            this.button3.Text = "Avbryt";
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular);
            this.button2.Location = new System.Drawing.Point(126, 226);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(104, 40);
            this.button2.TabIndex = 21;
            this.button2.Text = "Nästa";
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // referenceNoBox
            // 
            this.referenceNoBox.Location = new System.Drawing.Point(134, 202);
            this.referenceNoBox.MaxLength = 30;
            this.referenceNoBox.Name = "referenceNoBox";
            this.referenceNoBox.Size = new System.Drawing.Size(96, 21);
            this.referenceNoBox.TabIndex = 22;
            this.referenceNoBox.GotFocus += new System.EventHandler(this.referenceNoBox_GotFocus_1);
            this.referenceNoBox.LostFocus += new System.EventHandler(this.referenceNoBox_LostFocus_1);
            // 
            // goodsmarkBox
            // 
            this.goodsmarkBox.Location = new System.Drawing.Point(6, 202);
            this.goodsmarkBox.MaxLength = 30;
            this.goodsmarkBox.Name = "goodsmarkBox";
            this.goodsmarkBox.Size = new System.Drawing.Size(120, 21);
            this.goodsmarkBox.TabIndex = 23;
            this.goodsmarkBox.GotFocus += new System.EventHandler(this.goodsmarkBox_GotFocus_1);
            this.goodsmarkBox.LostFocus += new System.EventHandler(this.goodsmarkBox_LostFocus_1);
            // 
            // mark
            // 
            this.mark.Location = new System.Drawing.Point(6, 186);
            this.mark.Name = "mark";
            this.mark.Size = new System.Drawing.Size(100, 20);
            this.mark.Text = "Godsmärke";
            // 
            // phoneNoBox
            // 
            this.phoneNoBox.Location = new System.Drawing.Point(134, 162);
            this.phoneNoBox.MaxLength = 20;
            this.phoneNoBox.Name = "phoneNoBox";
            this.phoneNoBox.Size = new System.Drawing.Size(96, 21);
            this.phoneNoBox.TabIndex = 25;
            this.phoneNoBox.GotFocus += new System.EventHandler(this.phoneNoBox_GotFocus_1);
            this.phoneNoBox.LostFocus += new System.EventHandler(this.phoneNoBox_LostFocus_1);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(134, 146);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 20);
            this.label5.Text = "Telefonnr";
            // 
            // contactNameBox
            // 
            this.contactNameBox.Location = new System.Drawing.Point(6, 162);
            this.contactNameBox.MaxLength = 50;
            this.contactNameBox.Name = "contactNameBox";
            this.contactNameBox.Size = new System.Drawing.Size(120, 21);
            this.contactNameBox.TabIndex = 27;
            this.contactNameBox.GotFocus += new System.EventHandler(this.contactNameBox_GotFocus_1);
            this.contactNameBox.LostFocus += new System.EventHandler(this.contactNameBox_LostFocus_1);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 146);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 20);
            this.label4.Text = "Kontaktperson";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular);
            this.button1.Location = new System.Drawing.Point(6, 106);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(224, 32);
            this.button1.TabIndex = 29;
            this.button1.Text = "Välj kund";
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // customerNameBox
            // 
            this.customerNameBox.Location = new System.Drawing.Point(86, 82);
            this.customerNameBox.Name = "customerNameBox";
            this.customerNameBox.ReadOnly = true;
            this.customerNameBox.Size = new System.Drawing.Size(144, 21);
            this.customerNameBox.TabIndex = 30;
            // 
            // customerNoBox
            // 
            this.customerNoBox.Location = new System.Drawing.Point(6, 82);
            this.customerNoBox.Name = "customerNoBox";
            this.customerNoBox.ReadOnly = true;
            this.customerNoBox.Size = new System.Drawing.Size(72, 21);
            this.customerNoBox.TabIndex = 31;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(86, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 20);
            this.label3.Text = "Kundnamn";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 20);
            this.label2.Text = "Kundnr";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(6, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 24);
            this.label1.Text = "Order";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(134, 186);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 20);
            this.label6.Text = "Referensnr";
            // 
            // OrderHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.scanBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.referenceNoBox);
            this.Controls.Add(this.goodsmarkBox);
            this.Controls.Add(this.mark);
            this.Controls.Add(this.phoneNoBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.contactNameBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.customerNameBox);
            this.Controls.Add(this.customerNoBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label6);
            this.Menu = this.mainMenu1;
            this.Name = "OrderHeader";
            this.Text = "Orderhuvud";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox scanBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox referenceNoBox;
        private System.Windows.Forms.TextBox goodsmarkBox;
        private System.Windows.Forms.Label mark;
        private System.Windows.Forms.TextBox phoneNoBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox contactNameBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox customerNameBox;
        private System.Windows.Forms.TextBox customerNoBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private Microsoft.WindowsCE.Forms.InputPanel inputPanel1;
    }
}