namespace SmartShipment
{
    partial class CustomerList
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
            this.button1 = new System.Windows.Forms.Button();
            this.customerGrid = new System.Windows.Forms.DataGrid();
            this.customerTable = new System.Windows.Forms.DataGridTableStyle();
            this.customerNoCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.nameCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.addressCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.searchCustomer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.serviceLog = new System.Windows.Forms.ListBox();
            this.inputPanel1 = new Microsoft.WindowsCE.Forms.InputPanel();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular);
            this.button2.Location = new System.Drawing.Point(8, 222);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(104, 40);
            this.button2.TabIndex = 6;
            this.button2.Text = "Avbryt";
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular);
            this.button1.Location = new System.Drawing.Point(128, 222);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 40);
            this.button1.TabIndex = 7;
            this.button1.Text = "Välj";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // customerGrid
            // 
            this.customerGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.customerGrid.Location = new System.Drawing.Point(0, 38);
            this.customerGrid.Name = "customerGrid";
            this.customerGrid.Size = new System.Drawing.Size(240, 176);
            this.customerGrid.TabIndex = 8;
            this.customerGrid.TableStyles.Add(this.customerTable);
            this.customerGrid.Click += new System.EventHandler(this.customerGrid_Click);
            // 
            // customerTable
            // 
            this.customerTable.GridColumnStyles.Add(this.customerNoCol);
            this.customerTable.GridColumnStyles.Add(this.nameCol);
            this.customerTable.GridColumnStyles.Add(this.addressCol);
            this.customerTable.MappingName = "customer";
            // 
            // customerNoCol
            // 
            this.customerNoCol.Format = "";
            this.customerNoCol.FormatInfo = null;
            this.customerNoCol.MappingName = "no";
            this.customerNoCol.NullText = "Kundnr";
            // 
            // nameCol
            // 
            this.nameCol.Format = "";
            this.nameCol.FormatInfo = null;
            this.nameCol.HeaderText = "Namn";
            this.nameCol.MappingName = "name";
            this.nameCol.NullText = "";
            this.nameCol.Width = 100;
            // 
            // addressCol
            // 
            this.addressCol.Format = "";
            this.addressCol.FormatInfo = null;
            this.addressCol.HeaderText = "Adress";
            this.addressCol.MappingName = "address";
            this.addressCol.NullText = "";
            this.addressCol.Width = 100;
            // 
            // searchCustomer
            // 
            this.searchCustomer.Location = new System.Drawing.Point(88, 6);
            this.searchCustomer.Name = "searchCustomer";
            this.searchCustomer.Size = new System.Drawing.Size(104, 21);
            this.searchCustomer.TabIndex = 9;
            this.searchCustomer.TextChanged += new System.EventHandler(this.searchCustomer_TextChanged_1);
            this.searchCustomer.GotFocus += new System.EventHandler(this.searchCustomer_GotFocus_1);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.Text = "Sök kund:";
            // 
            // serviceLog
            // 
            this.serviceLog.Location = new System.Drawing.Point(0, 38);
            this.serviceLog.Name = "serviceLog";
            this.serviceLog.Size = new System.Drawing.Size(240, 170);
            this.serviceLog.TabIndex = 11;
            this.serviceLog.Visible = false;
            // 
            // CustomerList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.serviceLog);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.customerGrid);
            this.Controls.Add(this.searchCustomer);
            this.Controls.Add(this.label1);
            this.Menu = this.mainMenu1;
            this.Name = "CustomerList";
            this.Text = "Kundlista";
            this.Load += new System.EventHandler(this.CustomerList_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.CustomerList_Closing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGrid customerGrid;
        private System.Windows.Forms.DataGridTableStyle customerTable;
        private System.Windows.Forms.TextBox searchCustomer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox serviceLog;
        private Microsoft.WindowsCE.Forms.InputPanel inputPanel1;
        private System.Windows.Forms.DataGridTextBoxColumn nameCol;
        private System.Windows.Forms.DataGridTextBoxColumn addressCol;
        private System.Windows.Forms.DataGridTextBoxColumn customerNoCol;
    }
}