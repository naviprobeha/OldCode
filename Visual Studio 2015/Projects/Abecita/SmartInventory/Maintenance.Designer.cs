namespace SmartInventory
{
    partial class Maintenance
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
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.menu2 = new System.Windows.Forms.Button();
            this.menu1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular);
            this.button3.Location = new System.Drawing.Point(19, 201);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(184, 40);
            this.button3.TabIndex = 7;
            this.button3.Text = "Direktinlagring";
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular);
            this.button2.Location = new System.Drawing.Point(19, 153);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(184, 40);
            this.button2.TabIndex = 8;
            this.button2.Text = "Inventering";
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular);
            this.button1.Location = new System.Drawing.Point(19, 249);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(184, 40);
            this.button1.TabIndex = 9;
            this.button1.Text = "Tillbaka";
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(3, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 24);
            this.label2.Text = "Lagervård";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(3, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 20);
            this.label1.Text = "Välj funktion i menyn nedan.";
            // 
            // menu2
            // 
            this.menu2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular);
            this.menu2.Location = new System.Drawing.Point(19, 105);
            this.menu2.Name = "menu2";
            this.menu2.Size = new System.Drawing.Size(184, 40);
            this.menu2.TabIndex = 12;
            this.menu2.Text = "Omflyttning";
            this.menu2.Click += new System.EventHandler(this.menu2_Click_1);
            // 
            // menu1
            // 
            this.menu1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular);
            this.menu1.Location = new System.Drawing.Point(19, 57);
            this.menu1.Name = "menu1";
            this.menu1.Size = new System.Drawing.Size(184, 40);
            this.menu1.TabIndex = 13;
            this.menu1.Text = "Lagerplats info.";
            this.menu1.Click += new System.EventHandler(this.menu1_Click_1);
            // 
            // Maintenance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menu2);
            this.Controls.Add(this.menu1);
            this.Name = "Maintenance";
            this.Text = "Lagervård";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button menu2;
        private System.Windows.Forms.Button menu1;
    }
}