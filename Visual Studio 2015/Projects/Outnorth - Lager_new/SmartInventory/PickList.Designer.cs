namespace Navipro.SmartInventory
{
    partial class PickList
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
            this.pickListGrid = new System.Windows.Forms.DataGrid();
            this.pickListTable = new System.Windows.Forms.DataGridTableStyle();
            this.noCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.noOfLinesCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.assignedToCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pickListGrid
            // 
            this.pickListGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.pickListGrid.Location = new System.Drawing.Point(1, 28);
            this.pickListGrid.Name = "pickListGrid";
            this.pickListGrid.PreferredRowHeight = 22;
            this.pickListGrid.Size = new System.Drawing.Size(237, 229);
            this.pickListGrid.TabIndex = 0;
            this.pickListGrid.TableStyles.Add(this.pickListTable);
            // 
            // pickListTable
            // 
            this.pickListTable.GridColumnStyles.Add(this.noCol);
            this.pickListTable.GridColumnStyles.Add(this.noOfLinesCol);
            this.pickListTable.GridColumnStyles.Add(this.assignedToCol);
            this.pickListTable.MappingName = "pickHeader";
            // 
            // noCol
            // 
            this.noCol.Format = "";
            this.noCol.FormatInfo = null;
            this.noCol.HeaderText = "Nr.";
            this.noCol.MappingName = "no";
            this.noCol.NullText = "";
            this.noCol.Width = 75;
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
            // assignedToCol
            // 
            this.assignedToCol.Format = "";
            this.assignedToCol.FormatInfo = null;
            this.assignedToCol.HeaderText = "Tilldelad";
            this.assignedToCol.MappingName = "assignedTo";
            this.assignedToCol.NullText = "";
            this.assignedToCol.Width = 75;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 21);
            this.label1.Text = "Välj plocklista.";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(4, 263);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 49);
            this.button1.TabIndex = 2;
            this.button1.Text = "Avbryt";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.button2.Location = new System.Drawing.Point(128, 263);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(107, 49);
            this.button2.TabIndex = 3;
            this.button2.Text = "Välj";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // PickList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(638, 455);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pickListGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PickList";
            this.Text = "Plocklistor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.PickList_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGrid pickListGrid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridTableStyle pickListTable;
        private System.Windows.Forms.DataGridTextBoxColumn noCol;
        private System.Windows.Forms.DataGridTextBoxColumn noOfLinesCol;
        private System.Windows.Forms.DataGridTextBoxColumn assignedToCol;
    }
}