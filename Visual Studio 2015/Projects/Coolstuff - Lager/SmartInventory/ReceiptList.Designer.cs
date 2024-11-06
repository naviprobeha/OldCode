namespace Navipro.SmartInventory
{
    partial class ReceiptList
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
            this.userBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.receiptListGrid = new System.Windows.Forms.DataGrid();
            this.receiptListTable = new System.Windows.Forms.DataGridTableStyle();
            this.no1Col = new System.Windows.Forms.DataGridTextBoxColumn();
            this.userIdCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.noOfRowsCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // userBox
            // 
            this.userBox.Location = new System.Drawing.Point(3, 41);
            this.userBox.Name = "userBox";
            this.userBox.Size = new System.Drawing.Size(232, 23);
            this.userBox.TabIndex = 22;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 20);
            this.label4.Text = "Välj användare";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 20);
            this.label1.Text = "Inleveransuppdrag";
            // 
            // receiptListGrid
            // 
            this.receiptListGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.receiptListGrid.Location = new System.Drawing.Point(1, 70);
            this.receiptListGrid.Name = "receiptListGrid";
            this.receiptListGrid.PreferredRowHeight = 22;
            this.receiptListGrid.Size = new System.Drawing.Size(237, 187);
            this.receiptListGrid.TabIndex = 25;
            this.receiptListGrid.TableStyles.Add(this.receiptListTable);
            // 
            // receiptListTable
            // 
            this.receiptListTable.GridColumnStyles.Add(this.no1Col);
            this.receiptListTable.GridColumnStyles.Add(this.userIdCol);
            this.receiptListTable.GridColumnStyles.Add(this.noOfRowsCol);
            this.receiptListTable.MappingName = "receiptHeader";
            // 
            // no1Col
            // 
            this.no1Col.Format = "";
            this.no1Col.FormatInfo = null;
            this.no1Col.HeaderText = "Nr";
            this.no1Col.MappingName = "no";
            this.no1Col.NullText = "";
            this.no1Col.Width = 75;
            // 
            // userIdCol
            // 
            this.userIdCol.Format = "";
            this.userIdCol.FormatInfo = null;
            this.userIdCol.HeaderText = "Användare";
            this.userIdCol.MappingName = "assignedTo";
            this.userIdCol.NullText = "";
            this.userIdCol.Width = 100;
            // 
            // noOfRowsCol
            // 
            this.noOfRowsCol.Format = "";
            this.noOfRowsCol.FormatInfo = null;
            this.noOfRowsCol.HeaderText = "Antal rader";
            this.noOfRowsCol.MappingName = "noOfLines";
            this.noOfRowsCol.NullText = "";
            this.noOfRowsCol.Width = 40;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.button2.Location = new System.Drawing.Point(148, 263);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(87, 49);
            this.button2.TabIndex = 27;
            this.button2.Text = "Välj";
            this.button2.Click += new System.EventHandler(this.button2_Click_2);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(4, 263);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 49);
            this.button1.TabIndex = 26;
            this.button1.Text = "Avbryt";
            this.button1.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.button3.Location = new System.Drawing.Point(97, 263);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(47, 49);
            this.button3.TabIndex = 28;
            this.button3.Text = "Ny";
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // ReceiptList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(638, 455);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.receiptListGrid);
            this.Controls.Add(this.userBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReceiptList";
            this.Text = "ReceiptList";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox userBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGrid receiptListGrid;
        private System.Windows.Forms.DataGridTableStyle receiptListTable;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridTextBoxColumn userIdCol;
        private System.Windows.Forms.DataGridTextBoxColumn noOfRowsCol;
        private System.Windows.Forms.DataGridTextBoxColumn no1Col;
    }
}