namespace SmartShipment
{
    partial class OrderLines
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
            this.button5 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.serviceLog = new System.Windows.Forms.ListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.scanBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.salesLineGrid = new System.Windows.Forms.DataGrid();
            this.salesLineTable = new System.Windows.Forms.DataGridTableStyle();
            this.itemNoCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.descriptionCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.hangingCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.quantityCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.unitPriceCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.discountCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.amountCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.SuspendLayout();
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(168, 198);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(64, 20);
            this.button5.TabIndex = 9;
            this.button5.Text = "Ändra";
            this.button5.Click += new System.EventHandler(this.button5_Click_1);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(8, 198);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(72, 20);
            this.button3.TabIndex = 10;
            this.button3.Text = "Lägg till";
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(88, 198);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(72, 20);
            this.button4.TabIndex = 11;
            this.button4.Text = "Ta bort";
            this.button4.Click += new System.EventHandler(this.button4_Click_1);
            // 
            // serviceLog
            // 
            this.serviceLog.Location = new System.Drawing.Point(0, 38);
            this.serviceLog.Name = "serviceLog";
            this.serviceLog.Size = new System.Drawing.Size(200, 114);
            this.serviceLog.TabIndex = 12;
            this.serviceLog.Visible = false;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular);
            this.button2.Location = new System.Drawing.Point(8, 222);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(104, 40);
            this.button2.TabIndex = 13;
            this.button2.Text = "Föreg.";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular);
            this.button1.Location = new System.Drawing.Point(128, 222);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 40);
            this.button1.TabIndex = 14;
            this.button1.Text = "Slutför";
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // scanBox
            // 
            this.scanBox.Location = new System.Drawing.Point(72, 6);
            this.scanBox.Name = "scanBox";
            this.scanBox.Size = new System.Drawing.Size(160, 21);
            this.scanBox.TabIndex = 15;
            this.scanBox.TextChanged += new System.EventHandler(this.scanBox_TextChanged);
            this.scanBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.scanBox_KeyPress_1);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 20);
            this.label1.Text = "Scanna:";
            // 
            // salesLineGrid
            // 
            this.salesLineGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.salesLineGrid.Location = new System.Drawing.Point(0, 38);
            this.salesLineGrid.Name = "salesLineGrid";
            this.salesLineGrid.Size = new System.Drawing.Size(240, 152);
            this.salesLineGrid.TabIndex = 17;
            this.salesLineGrid.TableStyles.Add(this.salesLineTable);
            // 
            // salesLineTable
            // 
            this.salesLineTable.GridColumnStyles.Add(this.itemNoCol);
            this.salesLineTable.GridColumnStyles.Add(this.descriptionCol);
            this.salesLineTable.GridColumnStyles.Add(this.hangingCol);
            this.salesLineTable.GridColumnStyles.Add(this.quantityCol);
            this.salesLineTable.GridColumnStyles.Add(this.unitPriceCol);
            this.salesLineTable.GridColumnStyles.Add(this.discountCol);
            this.salesLineTable.GridColumnStyles.Add(this.amountCol);
            this.salesLineTable.MappingName = "salesLine";
            // 
            // itemNoCol
            // 
            this.itemNoCol.Format = "";
            this.itemNoCol.FormatInfo = null;
            this.itemNoCol.HeaderText = "Artikelnr";
            this.itemNoCol.MappingName = "itemNo";
            this.itemNoCol.NullText = "";
            this.itemNoCol.Width = 80;
            // 
            // descriptionCol
            // 
            this.descriptionCol.Format = "";
            this.descriptionCol.FormatInfo = null;
            this.descriptionCol.HeaderText = "Beskrivning";
            this.descriptionCol.MappingName = "description";
            this.descriptionCol.NullText = "";
            this.descriptionCol.Width = 100;
            // 
            // hangingCol
            // 
            this.hangingCol.Format = "";
            this.hangingCol.FormatInfo = null;
            this.hangingCol.HeaderText = "Hängning";
            this.hangingCol.MappingName = "hanging";
            this.hangingCol.NullText = "";
            // 
            // quantityCol
            // 
            this.quantityCol.Format = "";
            this.quantityCol.FormatInfo = null;
            this.quantityCol.HeaderText = "Antal";
            this.quantityCol.MappingName = "quantity";
            this.quantityCol.NullText = "";
            // 
            // unitPriceCol
            // 
            this.unitPriceCol.Format = "";
            this.unitPriceCol.FormatInfo = null;
            this.unitPriceCol.HeaderText = "A-pris";
            this.unitPriceCol.MappingName = "lineUnitPrice";
            this.unitPriceCol.NullText = "";
            // 
            // discountCol
            // 
            this.discountCol.Format = "";
            this.discountCol.FormatInfo = null;
            this.discountCol.HeaderText = "Rabatt";
            this.discountCol.MappingName = "discount";
            this.discountCol.NullText = "";
            // 
            // amountCol
            // 
            this.amountCol.Format = "";
            this.amountCol.FormatInfo = null;
            this.amountCol.HeaderText = "Belopp";
            this.amountCol.MappingName = "lineAmount";
            this.amountCol.NullText = "";
            // 
            // OrderLines
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.serviceLog);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.scanBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.salesLineGrid);
            this.Menu = this.mainMenu1;
            this.Name = "OrderLines";
            this.Text = "Orderrader";
            this.Load += new System.EventHandler(this.OrderLines_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ListBox serviceLog;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox scanBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGrid salesLineGrid;
        private System.Windows.Forms.DataGridTableStyle salesLineTable;
        private System.Windows.Forms.DataGridTextBoxColumn itemNoCol;
        private System.Windows.Forms.DataGridTextBoxColumn descriptionCol;
        private System.Windows.Forms.DataGridTextBoxColumn hangingCol;
        private System.Windows.Forms.DataGridTextBoxColumn quantityCol;
        private System.Windows.Forms.DataGridTextBoxColumn unitPriceCol;
        private System.Windows.Forms.DataGridTextBoxColumn discountCol;
        private System.Windows.Forms.DataGridTextBoxColumn amountCol;
    }
}