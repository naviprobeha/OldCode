namespace SmartShipment
{
    partial class Smartshipment
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
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.mainMenu2 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem4);
            this.mainMenu1.MenuItems.Add(this.menuItem5);
            this.mainMenu1.MenuItems.Add(this.menuItem6);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 34);
            this.label1.Text = "Huvudmeny";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular);
            this.button1.Location = new System.Drawing.Point(12, 69);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(210, 55);
            this.button1.TabIndex = 1;
            this.button1.Text = "Skapa order";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // mainMenu2
            // 
            this.mainMenu2.MenuItems.Add(this.menuItem1);
            this.mainMenu2.MenuItems.Add(this.menuItem2);
            this.mainMenu2.MenuItems.Add(this.menuItem3);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "Order";
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "Verktyg";
            // 
            // menuItem3
            // 
            this.menuItem3.Text = "Inställningar";
            // 
            // menuItem4
            // 
            this.menuItem4.MenuItems.Add(this.menuItem7);
            this.menuItem4.Text = "Order";
            // 
            // menuItem5
            // 
            this.menuItem5.MenuItems.Add(this.menuItem8);
            this.menuItem5.Text = "Verktyg";
            // 
            // menuItem6
            // 
            this.menuItem6.MenuItems.Add(this.menuItem9);
            this.menuItem6.Text = "Inställningar";
            // 
            // menuItem7
            // 
            this.menuItem7.Text = "Lista";
            this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Text = "Synkronisera";
            this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
            // 
            // menuItem9
            // 
            this.menuItem9.Text = "Synkronisering";
            this.menuItem9.Click += new System.EventHandler(this.menuItem9_Click);
            // 
            // Smartshipment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Menu = this.mainMenu1;
            this.Name = "Smartshipment";
            this.Text = "Start";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.MainMenu mainMenu2;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem menuItem7;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem menuItem8;
        private System.Windows.Forms.MenuItem menuItem6;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem9;

    }
}