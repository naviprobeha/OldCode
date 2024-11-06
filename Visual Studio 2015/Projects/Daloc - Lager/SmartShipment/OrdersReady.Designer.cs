namespace SmartShipment
{
    partial class OrdersReady
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
            this.button1 = new System.Windows.Forms.Button();
            this.orderGrid = new System.Windows.Forms.DataGrid();
            this.orderTable = new System.Windows.Forms.DataGridTableStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.orderNoCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.customerNoCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridTextBoxColumn();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular);
            this.button1.Location = new System.Drawing.Point(128, 222);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 40);
            this.button1.TabIndex = 6;
            this.button1.Text = "Visa";
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // orderGrid
            // 
            this.orderGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.orderGrid.Location = new System.Drawing.Point(0, 30);
            this.orderGrid.Name = "orderGrid";
            this.orderGrid.Size = new System.Drawing.Size(240, 184);
            this.orderGrid.TabIndex = 7;
            this.orderGrid.TableStyles.Add(this.orderTable);
            this.orderGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.orderGrid_KeyPress_1);
            // 
            // orderTable
            // 
            this.orderTable.GridColumnStyles.Add(this.orderNoCol);
            this.orderTable.GridColumnStyles.Add(this.customerNoCol);
            this.orderTable.GridColumnStyles.Add(this.name);
            this.orderTable.MappingName = "salesHeader";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(8, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 16);
            this.label1.Text = "Order klarmarkerade";
            // 
            // orderNoCol
            // 
            this.orderNoCol.Format = "";
            this.orderNoCol.FormatInfo = null;
            this.orderNoCol.HeaderText = "Ordernr";
            this.orderNoCol.MappingName = "orderNo";
            this.orderNoCol.NullText = "";
            // 
            // customerNoCol
            // 
            this.customerNoCol.Format = "";
            this.customerNoCol.FormatInfo = null;
            this.customerNoCol.HeaderText = "Kundnr";
            this.customerNoCol.MappingName = "customerNo";
            this.customerNoCol.NullText = "";
            // 
            // name
            // 
            this.name.Format = "";
            this.name.FormatInfo = null;
            this.name.HeaderText = "Namn";
            this.name.MappingName = "name";
            this.name.NullText = "";
            this.name.Width = 150;
            // 
            // OrdersReady
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.orderGrid);
            this.Controls.Add(this.label1);
            this.Menu = this.mainMenu1;
            this.Name = "OrdersReady";
            this.Text = "Order";
            this.Load += new System.EventHandler(this.OrdersReady_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGrid orderGrid;
        private System.Windows.Forms.DataGridTableStyle orderTable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridTextBoxColumn orderNoCol;
        private System.Windows.Forms.DataGridTextBoxColumn customerNoCol;
        private System.Windows.Forms.DataGridTextBoxColumn name;
    }
}