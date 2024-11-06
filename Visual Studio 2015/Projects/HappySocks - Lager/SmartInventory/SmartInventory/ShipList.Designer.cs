namespace Navipro.SmartInventory
{
    partial class ShipList
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
            this.countryCodeCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.totalQtyCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.button11 = new System.Windows.Forms.Button();
            this.cityCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.salesHeaderGrid = new System.Windows.Forms.DataGrid();
            this.salesHeaderTable = new System.Windows.Forms.DataGridTableStyle();
            this.noCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.customerNoCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.customerNameCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.addressCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.mainMenu2 = new System.Windows.Forms.MainMenu();
            this.orderDateCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.SuspendLayout();
            // 
            // countryCodeCol
            // 
            this.countryCodeCol.Format = "";
            this.countryCodeCol.FormatInfo = null;
            this.countryCodeCol.HeaderText = "Land";
            this.countryCodeCol.MappingName = "countryCode";
            this.countryCodeCol.NullText = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 220);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 45);
            this.button1.TabIndex = 30;
            this.button1.Text = "Avbryt";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // totalQtyCol
            // 
            this.totalQtyCol.Format = "";
            this.totalQtyCol.FormatInfo = null;
            this.totalQtyCol.HeaderText = "Totalt antal";
            this.totalQtyCol.MappingName = "totalQty";
            this.totalQtyCol.NullText = "";
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(126, 220);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(111, 45);
            this.button11.TabIndex = 29;
            this.button11.Text = "Välj";
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // cityCol
            // 
            this.cityCol.Format = "";
            this.cityCol.FormatInfo = null;
            this.cityCol.HeaderText = "Ort";
            this.cityCol.MappingName = "city";
            this.cityCol.NullText = "";
            this.cityCol.Width = 75;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(4, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(233, 20);
            this.label2.Text = "Välj order.";
            // 
            // salesHeaderGrid
            // 
            this.salesHeaderGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.salesHeaderGrid.Location = new System.Drawing.Point(0, 29);
            this.salesHeaderGrid.Name = "salesHeaderGrid";
            this.salesHeaderGrid.Size = new System.Drawing.Size(240, 186);
            this.salesHeaderGrid.TabIndex = 27;
            this.salesHeaderGrid.TableStyles.Add(this.salesHeaderTable);
            // 
            // salesHeaderTable
            // 
            this.salesHeaderTable.GridColumnStyles.Add(this.noCol);
            this.salesHeaderTable.GridColumnStyles.Add(this.customerNoCol);
            this.salesHeaderTable.GridColumnStyles.Add(this.customerNameCol);
            this.salesHeaderTable.GridColumnStyles.Add(this.addressCol);
            this.salesHeaderTable.GridColumnStyles.Add(this.cityCol);
            this.salesHeaderTable.GridColumnStyles.Add(this.countryCodeCol);
            this.salesHeaderTable.GridColumnStyles.Add(this.totalQtyCol);
            this.salesHeaderTable.GridColumnStyles.Add(this.orderDateCol);
            this.salesHeaderTable.MappingName = "salesHeader";
            // 
            // noCol
            // 
            this.noCol.Format = "";
            this.noCol.FormatInfo = null;
            this.noCol.HeaderText = "Nr";
            this.noCol.MappingName = "no";
            this.noCol.NullText = "";
            this.noCol.Width = 75;
            // 
            // customerNoCol
            // 
            this.customerNoCol.Format = "";
            this.customerNoCol.FormatInfo = null;
            this.customerNoCol.HeaderText = "customerNo";
            this.customerNoCol.MappingName = "customerNo";
            this.customerNoCol.NullText = "";
            // 
            // customerNameCol
            // 
            this.customerNameCol.Format = "";
            this.customerNameCol.FormatInfo = null;
            this.customerNameCol.HeaderText = "Namn";
            this.customerNameCol.MappingName = "customerName";
            this.customerNameCol.NullText = "";
            this.customerNameCol.Width = 120;
            // 
            // addressCol
            // 
            this.addressCol.Format = "";
            this.addressCol.FormatInfo = null;
            this.addressCol.HeaderText = "Adress";
            this.addressCol.MappingName = "address";
            this.addressCol.NullText = "";
            this.addressCol.Width = 75;
            // 
            // orderDateCol
            // 
            this.orderDateCol.Format = "";
            this.orderDateCol.FormatInfo = null;
            this.orderDateCol.HeaderText = "Orderdatum";
            this.orderDateCol.MappingName = "orderDate";
            this.orderDateCol.NullText = "";
            this.orderDateCol.Width = 75;
            // 
            // ShipList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.salesHeaderGrid);
            this.Menu = this.mainMenu1;
            this.Name = "ShipList";
            this.Text = "SmartInventory";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridTextBoxColumn countryCodeCol;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridTextBoxColumn totalQtyCol;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.DataGridTextBoxColumn cityCol;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGrid salesHeaderGrid;
        private System.Windows.Forms.DataGridTableStyle salesHeaderTable;
        private System.Windows.Forms.DataGridTextBoxColumn noCol;
        private System.Windows.Forms.DataGridTextBoxColumn customerNoCol;
        private System.Windows.Forms.DataGridTextBoxColumn customerNameCol;
        private System.Windows.Forms.DataGridTextBoxColumn addressCol;
        private System.Windows.Forms.MainMenu mainMenu2;
        private System.Windows.Forms.DataGridTextBoxColumn orderDateCol;
    }
}