namespace Navipro.SmartInventory
{
    partial class StoreList
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
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.storeListGrid = new System.Windows.Forms.DataGrid();
            this.storeListTable = new System.Windows.Forms.DataGridTableStyle();
            this.noCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.assignedToCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.noOfLinesCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.button2.Location = new System.Drawing.Point(128, 263);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(107, 49);
            this.button2.TabIndex = 7;
            this.button2.Text = "Välj";
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(4, 263);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 49);
            this.button1.TabIndex = 6;
            this.button1.Text = "Avbryt";
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(198, 21);
            this.label1.Text = "Välj inlagringsuppdrag";
            // 
            // storeListGrid
            // 
            this.storeListGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.storeListGrid.Location = new System.Drawing.Point(1, 28);
            this.storeListGrid.Name = "storeListGrid";
            this.storeListGrid.PreferredRowHeight = 22;
            this.storeListGrid.Size = new System.Drawing.Size(237, 229);
            this.storeListGrid.TabIndex = 5;
            this.storeListGrid.TableStyles.Add(this.storeListTable);
            // 
            // storeListTable
            // 
            this.storeListTable.GridColumnStyles.Add(this.noCol);
            this.storeListTable.GridColumnStyles.Add(this.assignedToCol);
            this.storeListTable.GridColumnStyles.Add(this.noOfLinesCol);
            this.storeListTable.MappingName = "storeHeader";
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
            // assignedToCol
            // 
            this.assignedToCol.Format = "";
            this.assignedToCol.FormatInfo = null;
            this.assignedToCol.HeaderText = "Tilldelad";
            this.assignedToCol.MappingName = "assignedTo";
            this.assignedToCol.NullText = "";
            this.assignedToCol.Width = 100;
            // 
            // noOfLinesCol
            // 
            this.noOfLinesCol.Format = "";
            this.noOfLinesCol.FormatInfo = null;
            this.noOfLinesCol.HeaderText = "Antal rader";
            this.noOfLinesCol.MappingName = "noOfLines";
            this.noOfLinesCol.NullText = "";
            this.noOfLinesCol.Width = 75;
            // 
            // StoreList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(638, 455);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.storeListGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StoreList";
            this.Text = "StoreList";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGrid storeListGrid;
        private System.Windows.Forms.DataGridTableStyle storeListTable;
        private System.Windows.Forms.DataGridTextBoxColumn noCol;
        private System.Windows.Forms.DataGridTextBoxColumn assignedToCol;
        private System.Windows.Forms.DataGridTextBoxColumn noOfLinesCol;
    }
}