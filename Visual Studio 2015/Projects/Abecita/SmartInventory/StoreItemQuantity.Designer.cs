namespace SmartInventory
{
    partial class StoreItemQuantity
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
            this.button2 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.handleUnitBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.quantityBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.scanPackageBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular);
            this.button2.Location = new System.Drawing.Point(16, 194);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(208, 64);
            this.button2.TabIndex = 4;
            this.button2.Text = "Huvudmeny";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(8, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(200, 20);
            this.label6.Text = "Sätt antal";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.handleUnitBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.quantityBox);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Location = new System.Drawing.Point(0, 90);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 96);
            // 
            // handleUnitBox
            // 
            this.handleUnitBox.Location = new System.Drawing.Point(16, 24);
            this.handleUnitBox.Name = "handleUnitBox";
            this.handleUnitBox.ReadOnly = true;
            this.handleUnitBox.Size = new System.Drawing.Size(208, 21);
            this.handleUnitBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.Text = "Kolli ID:";
            // 
            // quantityBox
            // 
            this.quantityBox.Location = new System.Drawing.Point(16, 64);
            this.quantityBox.Name = "quantityBox";
            this.quantityBox.Size = new System.Drawing.Size(208, 21);
            this.quantityBox.TabIndex = 2;
            this.quantityBox.GotFocus += new System.EventHandler(this.quantityBox_GotFocus);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(16, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 20);
            this.label7.Text = "Antal:";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.scanPackageBox);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Location = new System.Drawing.Point(0, 34);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(240, 56);
            // 
            // scanPackageBox
            // 
            this.scanPackageBox.Location = new System.Drawing.Point(16, 29);
            this.scanPackageBox.Name = "scanPackageBox";
            this.scanPackageBox.Size = new System.Drawing.Size(208, 21);
            this.scanPackageBox.TabIndex = 0;
            this.scanPackageBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.scanPackageBox_KeyPress);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(16, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 20);
            this.label4.Text = "Scanna kolli ID:";
            // 
            // StoreItemQuantity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Menu = this.mainMenu1;
            this.Name = "StoreItemQuantity";
            this.Text = "Sätt antal";
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox handleUnitBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox quantityBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox scanPackageBox;
        private System.Windows.Forms.Label label4;
    }
}