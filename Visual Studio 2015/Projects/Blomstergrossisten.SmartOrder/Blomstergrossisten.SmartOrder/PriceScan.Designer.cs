namespace SmartOrder
{
    partial class PriceScan
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
            this.scanCode = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.descriptionBox = new System.Windows.Forms.TextBox();
            this.itemNoBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.quantityBox = new System.Windows.Forms.TextBox();
            this.unitPriceBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // scanCode
            // 
            this.scanCode.Location = new System.Drawing.Point(4, 41);
            this.scanCode.Name = "scanCode";
            this.scanCode.Size = new System.Drawing.Size(232, 23);
            this.scanCode.TabIndex = 8;
            this.scanCode.GotFocus += new System.EventHandler(this.scanCode_GotFocus);
            this.scanCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.scanBox_KeyPress);
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(4, 24);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(67, 20);
            this.label15.Text = "Scanna:";
            // 
            // descriptionBox
            // 
            this.descriptionBox.Location = new System.Drawing.Point(79, 93);
            this.descriptionBox.Name = "descriptionBox";
            this.descriptionBox.ReadOnly = true;
            this.descriptionBox.Size = new System.Drawing.Size(157, 23);
            this.descriptionBox.TabIndex = 25;
            // 
            // itemNoBox
            // 
            this.itemNoBox.Location = new System.Drawing.Point(4, 93);
            this.itemNoBox.Name = "itemNoBox";
            this.itemNoBox.ReadOnly = true;
            this.itemNoBox.Size = new System.Drawing.Size(68, 23);
            this.itemNoBox.TabIndex = 24;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(78, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 20);
            this.label2.Text = "Beskrivning:";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(4, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 20);
            this.label1.Text = "Artikelnr:";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(4, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(188, 20);
            this.label3.Text = "Prisfråga";
            // 
            // quantityBox
            // 
            this.quantityBox.Location = new System.Drawing.Point(80, 147);
            this.quantityBox.Name = "quantityBox";
            this.quantityBox.ReadOnly = true;
            this.quantityBox.Size = new System.Drawing.Size(76, 23);
            this.quantityBox.TabIndex = 32;
            this.quantityBox.GotFocus += new System.EventHandler(this.quantityBox_GotFocus);
            // 
            // unitPriceBox
            // 
            this.unitPriceBox.Location = new System.Drawing.Point(5, 147);
            this.unitPriceBox.Name = "unitPriceBox";
            this.unitPriceBox.Size = new System.Drawing.Size(68, 23);
            this.unitPriceBox.TabIndex = 31;
            this.unitPriceBox.GotFocus += new System.EventHandler(this.unitPriceBox_GotFocus);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(79, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 20);
            this.label4.Text = "Antal:";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(5, 130);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 20);
            this.label5.Text = "A-pris:";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(173, 267);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(63, 33);
            this.button4.TabIndex = 35;
            this.button4.Text = "Stäng";
            this.button4.Click += new System.EventHandler(this.button4_Click_1);
            // 
            // PriceScan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(247, 312);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.quantityBox);
            this.Controls.Add(this.unitPriceBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.descriptionBox);
            this.Controls.Add(this.itemNoBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.scanCode);
            this.Controls.Add(this.label15);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PriceScan";
            this.Text = "PriceScan";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox scanCode;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox descriptionBox;
        private System.Windows.Forms.TextBox itemNoBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox quantityBox;
        private System.Windows.Forms.TextBox unitPriceBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button4;
    }
}