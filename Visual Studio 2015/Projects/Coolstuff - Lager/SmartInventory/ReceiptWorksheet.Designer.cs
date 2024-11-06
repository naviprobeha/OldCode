namespace Navipro.SmartInventory
{
    partial class ReceiptWorksheet
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
            this.receiptLinesGrid = new System.Windows.Forms.DataGrid();
            this.receiptLinesTable = new System.Windows.Forms.DataGridTableStyle();
            this.itemNoCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.descriptionCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.remainingQtyCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.qtyToReceiveCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.scanBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.noOfLinesBox = new System.Windows.Forms.TextBox();
            this.receiptNoBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(129, 263);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 49);
            this.button1.TabIndex = 31;
            this.button1.Text = "OK / Stäng";
            this.button1.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // receiptLinesGrid
            // 
            this.receiptLinesGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.receiptLinesGrid.Location = new System.Drawing.Point(1, 112);
            this.receiptLinesGrid.Name = "receiptLinesGrid";
            this.receiptLinesGrid.PreferredRowHeight = 22;
            this.receiptLinesGrid.Size = new System.Drawing.Size(237, 147);
            this.receiptLinesGrid.TabIndex = 30;
            this.receiptLinesGrid.TableStyles.Add(this.receiptLinesTable);
            // 
            // receiptLinesTable
            // 
            this.receiptLinesTable.GridColumnStyles.Add(this.itemNoCol);
            this.receiptLinesTable.GridColumnStyles.Add(this.descriptionCol);
            this.receiptLinesTable.GridColumnStyles.Add(this.remainingQtyCol);
            this.receiptLinesTable.GridColumnStyles.Add(this.qtyToReceiveCol);
            this.receiptLinesTable.MappingName = "receiptLine";
            // 
            // itemNoCol
            // 
            this.itemNoCol.Format = "";
            this.itemNoCol.FormatInfo = null;
            this.itemNoCol.HeaderText = "Artikelnr";
            this.itemNoCol.MappingName = "itemNo";
            this.itemNoCol.NullText = "";
            // 
            // descriptionCol
            // 
            this.descriptionCol.Format = "";
            this.descriptionCol.FormatInfo = null;
            this.descriptionCol.HeaderText = "Beskrivning";
            this.descriptionCol.MappingName = "description";
            this.descriptionCol.NullText = "";
            // 
            // remainingQtyCol
            // 
            this.remainingQtyCol.Format = "";
            this.remainingQtyCol.FormatInfo = null;
            this.remainingQtyCol.HeaderText = "Återstående antal";
            this.remainingQtyCol.MappingName = "quantity";
            this.remainingQtyCol.NullText = "";
            // 
            // qtyToReceiveCol
            // 
            this.qtyToReceiveCol.Format = "";
            this.qtyToReceiveCol.FormatInfo = null;
            this.qtyToReceiveCol.HeaderText = "Antal att inlev.";
            this.qtyToReceiveCol.MappingName = "qtyToReceive";
            this.qtyToReceiveCol.NullText = "";
            this.qtyToReceiveCol.Width = 40;
            // 
            // scanBox
            // 
            this.scanBox.Location = new System.Drawing.Point(4, 83);
            this.scanBox.Name = "scanBox";
            this.scanBox.Size = new System.Drawing.Size(232, 23);
            this.scanBox.TabIndex = 29;
            this.scanBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.scanBox_KeyPress);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(4, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(142, 20);
            this.label3.Text = "Scanna artikel";
            // 
            // noOfLinesBox
            // 
            this.noOfLinesBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.noOfLinesBox.Location = new System.Drawing.Point(122, 42);
            this.noOfLinesBox.Name = "noOfLinesBox";
            this.noOfLinesBox.ReadOnly = true;
            this.noOfLinesBox.Size = new System.Drawing.Size(114, 23);
            this.noOfLinesBox.TabIndex = 28;
            // 
            // receiptNoBox
            // 
            this.receiptNoBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.receiptNoBox.Location = new System.Drawing.Point(4, 42);
            this.receiptNoBox.Name = "receiptNoBox";
            this.receiptNoBox.ReadOnly = true;
            this.receiptNoBox.Size = new System.Drawing.Size(112, 23);
            this.receiptNoBox.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(122, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.Text = "Antal rader";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(4, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 20);
            this.label4.Text = "Uppdrag";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 21);
            this.label1.Text = "Inleveransuppdrag";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.button2.Location = new System.Drawing.Point(4, 263);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(107, 49);
            this.button2.TabIndex = 36;
            this.button2.Text = "Artikelinfo";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ReceiptWorksheet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(638, 455);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.receiptLinesGrid);
            this.Controls.Add(this.scanBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.noOfLinesBox);
            this.Controls.Add(this.receiptNoBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReceiptWorksheet";
            this.Text = "ReceiptWorksheet";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGrid receiptLinesGrid;
        private System.Windows.Forms.DataGridTableStyle receiptLinesTable;
        private System.Windows.Forms.TextBox scanBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox noOfLinesBox;
        private System.Windows.Forms.TextBox receiptNoBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridTextBoxColumn itemNoCol;
        private System.Windows.Forms.DataGridTextBoxColumn descriptionCol;
        private System.Windows.Forms.DataGridTextBoxColumn remainingQtyCol;
        private System.Windows.Forms.DataGridTextBoxColumn qtyToReceiveCol;
    }
}