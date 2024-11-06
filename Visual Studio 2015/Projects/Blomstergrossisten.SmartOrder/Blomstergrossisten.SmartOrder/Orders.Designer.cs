namespace SmartOrder
{
    partial class Orders
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.orderGrid1 = new System.Windows.Forms.DataGrid();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.orderGrid2 = new System.Windows.Forms.DataGrid();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.orderTable1 = new System.Windows.Forms.DataGridTableStyle();
            this.orderNoCol1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.customerNoCol1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.customerNameCol1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.orderTable2 = new System.Windows.Forms.DataGridTableStyle();
            this.orderNoCol2 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.customerNoCol2 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.customerNameCol2 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(233, 263);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.orderGrid1);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(225, 234);
            this.tabPage1.Text = "Under bearbetning";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button3);
            this.tabPage2.Controls.Add(this.orderGrid2);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(225, 234);
            this.tabPage2.Text = "Klara";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(191, 20);
            this.label1.Text = "Order under bearbetning";
            // 
            // orderGrid1
            // 
            this.orderGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.orderGrid1.Location = new System.Drawing.Point(3, 28);
            this.orderGrid1.Name = "orderGrid1";
            this.orderGrid1.Size = new System.Drawing.Size(219, 160);
            this.orderGrid1.TabIndex = 1;
            this.orderGrid1.TableStyles.Add(this.orderTable1);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(140, 272);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 33);
            this.button1.TabIndex = 9;
            this.button1.Text = "Stäng";
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(4, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(191, 20);
            this.label2.Text = "Klarmarkerade order";
            // 
            // orderGrid2
            // 
            this.orderGrid2.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.orderGrid2.Location = new System.Drawing.Point(4, 28);
            this.orderGrid2.Name = "orderGrid2";
            this.orderGrid2.Size = new System.Drawing.Size(218, 160);
            this.orderGrid2.TabIndex = 2;
            this.orderGrid2.TableStyles.Add(this.orderTable2);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(126, 194);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(96, 33);
            this.button2.TabIndex = 10;
            this.button2.Text = "Visa";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(126, 194);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(96, 33);
            this.button3.TabIndex = 11;
            this.button3.Text = "Visa";
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // orderTable1
            // 
            this.orderTable1.GridColumnStyles.Add(this.orderNoCol1);
            this.orderTable1.GridColumnStyles.Add(this.customerNoCol1);
            this.orderTable1.GridColumnStyles.Add(this.customerNameCol1);
            this.orderTable1.MappingName = "salesHeader";
            // 
            // orderNoCol1
            // 
            this.orderNoCol1.Format = "";
            this.orderNoCol1.FormatInfo = null;
            this.orderNoCol1.HeaderText = "Ordernr";
            this.orderNoCol1.MappingName = "orderNo";
            this.orderNoCol1.NullText = "";
            // 
            // customerNoCol1
            // 
            this.customerNoCol1.Format = "";
            this.customerNoCol1.FormatInfo = null;
            this.customerNoCol1.HeaderText = "Kundnr";
            this.customerNoCol1.MappingName = "customerNo";
            this.customerNoCol1.NullText = "";
            // 
            // customerNameCol1
            // 
            this.customerNameCol1.Format = "";
            this.customerNameCol1.FormatInfo = null;
            this.customerNameCol1.HeaderText = "Namn";
            this.customerNameCol1.MappingName = "name";
            this.customerNameCol1.NullText = "";
            this.customerNameCol1.Width = 150;
            // 
            // orderTable2
            // 
            this.orderTable2.GridColumnStyles.Add(this.orderNoCol2);
            this.orderTable2.GridColumnStyles.Add(this.customerNoCol2);
            this.orderTable2.GridColumnStyles.Add(this.customerNameCol2);
            this.orderTable2.MappingName = "salesHeader";
            // 
            // orderNoCol2
            // 
            this.orderNoCol2.Format = "";
            this.orderNoCol2.FormatInfo = null;
            this.orderNoCol2.HeaderText = "Ordernr";
            this.orderNoCol2.MappingName = "orderNo";
            this.orderNoCol2.NullText = "";
            // 
            // customerNoCol2
            // 
            this.customerNoCol2.Format = "";
            this.customerNoCol2.FormatInfo = null;
            this.customerNoCol2.HeaderText = "Kundnr";
            this.customerNoCol2.MappingName = "customerNo";
            this.customerNoCol2.NullText = "";
            // 
            // customerNameCol2
            // 
            this.customerNameCol2.Format = "";
            this.customerNameCol2.FormatInfo = null;
            this.customerNameCol2.HeaderText = "Namn";
            this.customerNameCol2.MappingName = "name";
            this.customerNameCol2.NullText = "";
            this.customerNameCol2.Width = 150;
            // 
            // Orders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(239, 313);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Orders";
            this.Text = "Orders";
            this.Load += new System.EventHandler(this.Orders_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGrid orderGrid1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGrid orderGrid2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridTableStyle orderTable1;
        private System.Windows.Forms.DataGridTextBoxColumn orderNoCol1;
        private System.Windows.Forms.DataGridTextBoxColumn customerNoCol1;
        private System.Windows.Forms.DataGridTextBoxColumn customerNameCol1;
        private System.Windows.Forms.DataGridTableStyle orderTable2;
        private System.Windows.Forms.DataGridTextBoxColumn orderNoCol2;
        private System.Windows.Forms.DataGridTextBoxColumn customerNoCol2;
        private System.Windows.Forms.DataGridTextBoxColumn customerNameCol2;
    }
}