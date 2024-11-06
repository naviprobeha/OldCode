namespace SmartInventory
{
    partial class StartForm
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
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.menu3 = new System.Windows.Forms.Button();
            this.menu2 = new System.Windows.Forms.Button();
            this.menu1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.menuItem2);
            this.menuItem1.MenuItems.Add(this.menuItem3);
            this.menuItem1.Text = "Verktyg";
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "Inställningar";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Text = "Synkronisera";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(14, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 24);
            this.label2.Text = "Huvudmeny";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(14, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 20);
            this.label1.Text = "Välj funktion i menyn nedan.";
            // 
            // menu3
            // 
            this.menu3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular);
            this.menu3.Location = new System.Drawing.Point(30, 161);
            this.menu3.Name = "menu3";
            this.menu3.Size = new System.Drawing.Size(184, 40);
            this.menu3.TabIndex = 9;
            this.menu3.Text = "Lagervård";
            this.menu3.Click += new System.EventHandler(this.menu3_Click);
            // 
            // menu2
            // 
            this.menu2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular);
            this.menu2.Location = new System.Drawing.Point(30, 113);
            this.menu2.Name = "menu2";
            this.menu2.Size = new System.Drawing.Size(184, 40);
            this.menu2.TabIndex = 10;
            this.menu2.Text = "Påfyllning";
            this.menu2.Click += new System.EventHandler(this.menu2_Click);
            // 
            // menu1
            // 
            this.menu1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular);
            this.menu1.Location = new System.Drawing.Point(30, 67);
            this.menu1.Name = "menu1";
            this.menu1.Size = new System.Drawing.Size(184, 40);
            this.menu1.TabIndex = 11;
            this.menu1.Text = "Inlagring";
            this.menu1.Click += new System.EventHandler(this.menu1_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular);
            this.button3.Location = new System.Drawing.Point(30, 207);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(184, 40);
            this.button3.TabIndex = 12;
            this.button3.Text = "Direktinlagring";
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menu3);
            this.Controls.Add(this.menu2);
            this.Controls.Add(this.menu1);
            this.Menu = this.mainMenu1;
            this.Name = "StartForm";
            this.Text = "SmartInventory";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button menu3;
        private System.Windows.Forms.Button menu2;
        private System.Windows.Forms.Button menu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.Button button3;
    }
}