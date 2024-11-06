namespace Navipro.SmartInventory
{
    partial class InventoryOption
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
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(242, 203);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 103);
            this.label3.TabIndex = 0;
            this.label3.Text = "Avsedd för total inventering av hela lagret. Justering sker först efter bokförd i" +
                "nv. journal.";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(242, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 72);
            this.label2.TabIndex = 1;
            this.label2.Text = "Den här metoden justerar artikelns lagersaldo direkt. En artikel i taget.";
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.button3.Location = new System.Drawing.Point(3, 362);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(345, 128);
            this.button3.TabIndex = 10;
            this.button3.Text = "Avbryt";
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.button2.Location = new System.Drawing.Point(3, 203);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(233, 144);
            this.button2.TabIndex = 9;
            this.button2.Text = "Inv. journal";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 20);
            this.label1.TabIndex = 11;
            this.label1.Text = "Klicka på resp. val nedan.";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(3, 39);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(233, 147);
            this.button1.TabIndex = 6;
            this.button1.Text = "Snabb inv.";
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // InventoryOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(392, 513);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InventoryOption";
            this.Text = "InventoryChoose";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}