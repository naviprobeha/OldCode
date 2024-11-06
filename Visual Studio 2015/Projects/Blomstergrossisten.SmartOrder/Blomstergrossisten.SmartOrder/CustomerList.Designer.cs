namespace SmartOrder
{
    partial class CustomerList
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.searchCustomer = new System.Windows.Forms.TextBox();
            this.customerGrid = new System.Windows.Forms.DataGrid();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.inputPanel1 = new Microsoft.WindowsCE.Forms.InputPanel(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 20);
            this.label1.Text = "Sök kund:";
            // 
            // searchCustomer
            // 
            this.searchCustomer.Location = new System.Drawing.Point(79, 11);
            this.searchCustomer.Name = "searchCustomer";
            this.searchCustomer.Size = new System.Drawing.Size(135, 23);
            this.searchCustomer.TabIndex = 1;
            this.searchCustomer.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.searchCustomer.GotFocus += new System.EventHandler(this.textBox1_GotFocus);
            this.searchCustomer.LostFocus += new System.EventHandler(this.textBox1_LostFocus);
            // 
            // customerGrid
            // 
            this.customerGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.customerGrid.Location = new System.Drawing.Point(0, 49);
            this.customerGrid.Name = "customerGrid";
            this.customerGrid.Size = new System.Drawing.Size(247, 200);
            this.customerGrid.TabIndex = 2;
            this.customerGrid.Click += new System.EventHandler(this.customerGrid_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(3, 255);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(96, 33);
            this.button2.TabIndex = 9;
            this.button2.Text = "Avbryt";
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(138, 255);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 33);
            this.button1.TabIndex = 8;
            this.button1.Text = "Ny order";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CustomerList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(247, 312);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.customerGrid);
            this.Controls.Add(this.searchCustomer);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CustomerList";
            this.Text = "searchCustomer";
            this.Load += new System.EventHandler(this.CustomerList_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.CustomerList_Closing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox searchCustomer;
        private System.Windows.Forms.DataGrid customerGrid;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private Microsoft.WindowsCE.Forms.InputPanel inputPanel1;
    }
}