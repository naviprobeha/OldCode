namespace Navipro.SmartInventory
{
    partial class PhysInventory
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
            this.eanBox = new System.Windows.Forms.TextBox();
            this.itemNoLabel = new System.Windows.Forms.Label();
            this.itemNoBox = new System.Windows.Forms.TextBox();
            this.variantCodeBox = new System.Windows.Forms.TextBox();
            this.variantCodeLabel = new System.Windows.Forms.Label();
            this.descLabel = new System.Windows.Forms.Label();
            this.qtyBox = new System.Windows.Forms.TextBox();
            this.quantityLabel = new System.Windows.Forms.Label();
            this.descBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.logViewBox = new System.Windows.Forms.ListBox();
            this.unitOfMeasureLabel = new System.Windows.Forms.Label();
            this.unitOfMeasureBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(4, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(233, 20);
            this.label2.Text = "Scanna varje artikels EAN-kod.";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 20);
            this.label1.Text = "EAN:";
            // 
            // eanBox
            // 
            this.eanBox.Location = new System.Drawing.Point(90, 28);
            this.eanBox.Name = "eanBox";
            this.eanBox.Size = new System.Drawing.Size(147, 21);
            this.eanBox.TabIndex = 3;
            this.eanBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.eanBox_KeyPress);
            // 
            // itemNoLabel
            // 
            this.itemNoLabel.Location = new System.Drawing.Point(3, 77);
            this.itemNoLabel.Name = "itemNoLabel";
            this.itemNoLabel.Size = new System.Drawing.Size(119, 20);
            this.itemNoLabel.Text = "Artikelnr";
            // 
            // itemNoBox
            // 
            this.itemNoBox.Location = new System.Drawing.Point(4, 93);
            this.itemNoBox.Name = "itemNoBox";
            this.itemNoBox.ReadOnly = true;
            this.itemNoBox.Size = new System.Drawing.Size(118, 21);
            this.itemNoBox.TabIndex = 6;
            // 
            // variantCodeBox
            // 
            this.variantCodeBox.Location = new System.Drawing.Point(128, 93);
            this.variantCodeBox.Name = "variantCodeBox";
            this.variantCodeBox.ReadOnly = true;
            this.variantCodeBox.Size = new System.Drawing.Size(109, 21);
            this.variantCodeBox.TabIndex = 8;
            // 
            // variantCodeLabel
            // 
            this.variantCodeLabel.Location = new System.Drawing.Point(127, 77);
            this.variantCodeLabel.Name = "variantCodeLabel";
            this.variantCodeLabel.Size = new System.Drawing.Size(110, 20);
            this.variantCodeLabel.Text = "Variantkod";
            // 
            // descLabel
            // 
            this.descLabel.Location = new System.Drawing.Point(4, 117);
            this.descLabel.Name = "descLabel";
            this.descLabel.Size = new System.Drawing.Size(233, 20);
            this.descLabel.Text = "Beskrivning";
            // 
            // qtyBox
            // 
            this.qtyBox.Location = new System.Drawing.Point(4, 175);
            this.qtyBox.Name = "qtyBox";
            this.qtyBox.ReadOnly = true;
            this.qtyBox.Size = new System.Drawing.Size(118, 21);
            this.qtyBox.TabIndex = 12;
            // 
            // quantityLabel
            // 
            this.quantityLabel.Location = new System.Drawing.Point(3, 159);
            this.quantityLabel.Name = "quantityLabel";
            this.quantityLabel.Size = new System.Drawing.Size(119, 20);
            this.quantityLabel.Text = "Antal";
            // 
            // descBox
            // 
            this.descBox.Location = new System.Drawing.Point(4, 133);
            this.descBox.Name = "descBox";
            this.descBox.ReadOnly = true;
            this.descBox.Size = new System.Drawing.Size(233, 21);
            this.descBox.TabIndex = 15;
            this.descBox.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(156, 220);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 45);
            this.button1.TabIndex = 24;
            this.button1.Text = "Stäng";
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // logViewBox
            // 
            this.logViewBox.Location = new System.Drawing.Point(-3, 72);
            this.logViewBox.Name = "logViewBox";
            this.logViewBox.Size = new System.Drawing.Size(243, 184);
            this.logViewBox.TabIndex = 27;
            this.logViewBox.Visible = false;
            // 
            // unitOfMeasureLabel
            // 
            this.unitOfMeasureLabel.Location = new System.Drawing.Point(126, 159);
            this.unitOfMeasureLabel.Name = "unitOfMeasureLabel";
            this.unitOfMeasureLabel.Size = new System.Drawing.Size(110, 20);
            this.unitOfMeasureLabel.Text = "Enhetskod";
            // 
            // unitOfMeasureBox
            // 
            this.unitOfMeasureBox.Location = new System.Drawing.Point(126, 175);
            this.unitOfMeasureBox.Name = "unitOfMeasureBox";
            this.unitOfMeasureBox.ReadOnly = true;
            this.unitOfMeasureBox.Size = new System.Drawing.Size(109, 21);
            this.unitOfMeasureBox.TabIndex = 30;
            // 
            // PhysInventory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.logViewBox);
            this.Controls.Add(this.unitOfMeasureBox);
            this.Controls.Add(this.unitOfMeasureLabel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.descBox);
            this.Controls.Add(this.qtyBox);
            this.Controls.Add(this.descLabel);
            this.Controls.Add(this.variantCodeBox);
            this.Controls.Add(this.variantCodeLabel);
            this.Controls.Add(this.itemNoBox);
            this.Controls.Add(this.itemNoLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.eanBox);
            this.Controls.Add(this.quantityLabel);
            this.Menu = this.mainMenu1;
            this.Name = "PhysInventory";
            this.Text = "Inventering";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox eanBox;
        private System.Windows.Forms.Label itemNoLabel;
        private System.Windows.Forms.TextBox itemNoBox;
        private System.Windows.Forms.TextBox variantCodeBox;
        private System.Windows.Forms.Label variantCodeLabel;
        private System.Windows.Forms.Label descLabel;
        private System.Windows.Forms.TextBox qtyBox;
        private System.Windows.Forms.Label quantityLabel;
        private System.Windows.Forms.TextBox descBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox logViewBox;
        private System.Windows.Forms.Label unitOfMeasureLabel;
        private System.Windows.Forms.TextBox unitOfMeasureBox;
    }
}