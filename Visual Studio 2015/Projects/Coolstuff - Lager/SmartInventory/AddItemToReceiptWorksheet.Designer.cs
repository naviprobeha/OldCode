namespace Navipro.SmartInventory
{
    partial class AddItemToReceiptWorksheet
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
            this.button1 = new System.Windows.Forms.Button();
            this.purchaseOrderGrid = new System.Windows.Forms.DataGrid();
            this.storeLineTable = new System.Windows.Forms.DataGridTableStyle();
            this.scanBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.descriptionBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.logViewList = new System.Windows.Forms.ListBox();
            this.itemNoCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.documentNo = new System.Windows.Forms.DataGridTextBoxColumn();
            this.descriptionCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.outstandingQtyCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.quantityCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(4, 263);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 49);
            this.button1.TabIndex = 31;
            this.button1.Text = "Avbryt";
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // purchaseOrderGrid
            // 
            this.purchaseOrderGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.purchaseOrderGrid.Location = new System.Drawing.Point(4, 113);
            this.purchaseOrderGrid.Name = "purchaseOrderGrid";
            this.purchaseOrderGrid.PreferredRowHeight = 22;
            this.purchaseOrderGrid.Size = new System.Drawing.Size(232, 144);
            this.purchaseOrderGrid.TabIndex = 30;
            this.purchaseOrderGrid.TableStyles.Add(this.storeLineTable);
            // 
            // storeLineTable
            // 
            this.storeLineTable.GridColumnStyles.Add(this.itemNoCol);
            this.storeLineTable.GridColumnStyles.Add(this.documentNo);
            this.storeLineTable.GridColumnStyles.Add(this.descriptionCol);
            this.storeLineTable.GridColumnStyles.Add(this.outstandingQtyCol);
            this.storeLineTable.GridColumnStyles.Add(this.quantityCol);
            this.storeLineTable.MappingName = "purchaseOrder";
            // 
            // scanBox
            // 
            this.scanBox.Location = new System.Drawing.Point(4, 42);
            this.scanBox.Name = "scanBox";
            this.scanBox.Size = new System.Drawing.Size(232, 23);
            this.scanBox.TabIndex = 29;
            this.scanBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.scanBox_KeyPress);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(4, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(142, 20);
            this.label3.Text = "Scanna artikel";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(237, 21);
            this.label1.Text = "Inleverans - Lägg till inköpsorder";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.button2.Location = new System.Drawing.Point(129, 263);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(107, 49);
            this.button2.TabIndex = 36;
            this.button2.Text = "OK";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // descriptionBox
            // 
            this.descriptionBox.Location = new System.Drawing.Point(4, 84);
            this.descriptionBox.Name = "descriptionBox";
            this.descriptionBox.ReadOnly = true;
            this.descriptionBox.Size = new System.Drawing.Size(232, 23);
            this.descriptionBox.TabIndex = 40;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(4, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 20);
            this.label2.Text = "Artikel";
            // 
            // logViewList
            // 
            this.logViewList.Location = new System.Drawing.Point(269, 178);
            this.logViewList.Name = "logViewList";
            this.logViewList.Size = new System.Drawing.Size(100, 98);
            this.logViewList.TabIndex = 69;
            this.logViewList.Visible = false;
            // 
            // itemNoCol
            // 
            this.itemNoCol.Format = "";
            this.itemNoCol.FormatInfo = null;
            this.itemNoCol.HeaderText = "Artikelnr";
            this.itemNoCol.MappingName = "itemNo";
            this.itemNoCol.NullText = "";
            // 
            // documentNo
            // 
            this.documentNo.Format = "";
            this.documentNo.FormatInfo = null;
            this.documentNo.HeaderText = "Ordernr";
            this.documentNo.MappingName = "documentNo";
            this.documentNo.NullText = "";
            // 
            // descriptionCol
            // 
            this.descriptionCol.Format = "";
            this.descriptionCol.FormatInfo = null;
            this.descriptionCol.HeaderText = "Beskrivning";
            this.descriptionCol.MappingName = "description";
            this.descriptionCol.NullText = "";
            // 
            // outstandingQtyCol
            // 
            this.outstandingQtyCol.Format = "";
            this.outstandingQtyCol.FormatInfo = null;
            this.outstandingQtyCol.HeaderText = "Återstående";
            this.outstandingQtyCol.MappingName = "outstandingQty";
            this.outstandingQtyCol.NullText = "";
            // 
            // quantityCol
            // 
            this.quantityCol.Format = "";
            this.quantityCol.FormatInfo = null;
            this.quantityCol.HeaderText = "Antal";
            this.quantityCol.MappingName = "quantity";
            this.quantityCol.NullText = "";
            // 
            // AddItemToReceiptWorksheet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(638, 455);
            this.Controls.Add(this.logViewList);
            this.Controls.Add(this.descriptionBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.purchaseOrderGrid);
            this.Controls.Add(this.scanBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddItemToReceiptWorksheet";
            this.Text = "AddItemToReceiptWorksheet";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGrid purchaseOrderGrid;
        private System.Windows.Forms.DataGridTableStyle storeLineTable;
        private System.Windows.Forms.TextBox scanBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox descriptionBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox logViewList;
        private System.Windows.Forms.DataGridTextBoxColumn itemNoCol;
        private System.Windows.Forms.DataGridTextBoxColumn documentNo;
        private System.Windows.Forms.DataGridTextBoxColumn descriptionCol;
        private System.Windows.Forms.DataGridTextBoxColumn outstandingQtyCol;
        private System.Windows.Forms.DataGridTextBoxColumn quantityCol;
    }
}