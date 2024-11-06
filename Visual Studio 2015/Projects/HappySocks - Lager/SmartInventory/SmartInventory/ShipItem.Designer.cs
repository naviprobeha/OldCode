namespace Navipro.SmartInventory
{
    partial class ShipItem
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.itemNoBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.variantCodeBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.descriptionBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.quantityBox = new System.Windows.Forms.TextBox();
            this.pickedQtyBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.eanBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.cartonBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(4, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(233, 20);
            this.label2.Text = "Plocka artikeln nedan.";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(4, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.Text = "Artikelnr";
            // 
            // itemNoBox
            // 
            this.itemNoBox.Location = new System.Drawing.Point(4, 46);
            this.itemNoBox.Name = "itemNoBox";
            this.itemNoBox.ReadOnly = true;
            this.itemNoBox.Size = new System.Drawing.Size(127, 21);
            this.itemNoBox.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(137, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 20);
            this.label3.Text = "Variantkod";
            // 
            // variantCodeBox
            // 
            this.variantCodeBox.Location = new System.Drawing.Point(137, 46);
            this.variantCodeBox.Name = "variantCodeBox";
            this.variantCodeBox.ReadOnly = true;
            this.variantCodeBox.Size = new System.Drawing.Size(100, 21);
            this.variantCodeBox.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(4, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 20);
            this.label4.Text = "Beskrivning";
            // 
            // descriptionBox
            // 
            this.descriptionBox.Location = new System.Drawing.Point(4, 90);
            this.descriptionBox.Name = "descriptionBox";
            this.descriptionBox.ReadOnly = true;
            this.descriptionBox.Size = new System.Drawing.Size(127, 21);
            this.descriptionBox.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(4, 164);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 20);
            this.label5.Text = "Totalt att plocka";
            // 
            // quantityBox
            // 
            this.quantityBox.Location = new System.Drawing.Point(4, 181);
            this.quantityBox.Name = "quantityBox";
            this.quantityBox.ReadOnly = true;
            this.quantityBox.Size = new System.Drawing.Size(127, 21);
            this.quantityBox.TabIndex = 11;
            // 
            // pickedQtyBox
            // 
            this.pickedQtyBox.Location = new System.Drawing.Point(137, 181);
            this.pickedQtyBox.Name = "pickedQtyBox";
            this.pickedQtyBox.ReadOnly = true;
            this.pickedQtyBox.Size = new System.Drawing.Size(100, 21);
            this.pickedQtyBox.TabIndex = 12;
            this.pickedQtyBox.TextChanged += new System.EventHandler(this.pickedQtyBox_TextChanged);
            this.pickedQtyBox.GotFocus += new System.EventHandler(this.pickedQtyBox_GotFocus);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(137, 164);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 20);
            this.label6.Text = "Plockat antal";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(4, 116);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 20);
            this.label7.Text = "EAN:";
            // 
            // eanBox
            // 
            this.eanBox.Location = new System.Drawing.Point(4, 133);
            this.eanBox.Name = "eanBox";
            this.eanBox.Size = new System.Drawing.Size(127, 21);
            this.eanBox.TabIndex = 37;
            this.eanBox.GotFocus += new System.EventHandler(this.eanBox_GotFocus);
            this.eanBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox6_KeyPress);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 220);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 45);
            this.button1.TabIndex = 39;
            this.button1.Text = "Föregående ";
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(160, 220);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(80, 45);
            this.button11.TabIndex = 38;
            this.button11.Text = "Nästa";
            this.button11.Click += new System.EventHandler(this.button11_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(76, 220);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(88, 45);
            this.button2.TabIndex = 40;
            this.button2.Text = "Avbryt";
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // cartonBox
            // 
            this.cartonBox.Location = new System.Drawing.Point(138, 90);
            this.cartonBox.Name = "cartonBox";
            this.cartonBox.ReadOnly = true;
            this.cartonBox.Size = new System.Drawing.Size(100, 21);
            this.cartonBox.TabIndex = 48;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(137, 72);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 20);
            this.label8.Text = "Kartong";
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.button3.Location = new System.Drawing.Point(138, 116);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(99, 44);
            this.button3.TabIndex = 50;
            this.button3.Text = "Kartonger";
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // ShipItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.cartonBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.pickedQtyBox);
            this.Controls.Add(this.eanBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.quantityBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.descriptionBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.variantCodeBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.itemNoBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label8);
            this.Menu = this.mainMenu1;
            this.Name = "ShipItem";
            this.Text = "ShipItem";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox itemNoBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox variantCodeBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox descriptionBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox quantityBox;
        private System.Windows.Forms.TextBox pickedQtyBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox eanBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox cartonBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button3;
    }
}