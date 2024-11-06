namespace SmartInventory
{
    partial class StoreJobs
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
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.jobGrid = new System.Windows.Forms.DataGrid();
            this.jobTable = new System.Windows.Forms.DataGridTableStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.noCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.noOfLinesCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.menuItem2);
            this.menuItem1.Text = "Visa";
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "Rader";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // jobGrid
            // 
            this.jobGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.jobGrid.Location = new System.Drawing.Point(-5, 33);
            this.jobGrid.Name = "jobGrid";
            this.jobGrid.Size = new System.Drawing.Size(240, 208);
            this.jobGrid.TabIndex = 2;
            this.jobGrid.TableStyles.Add(this.jobTable);
            this.jobGrid.Click += new System.EventHandler(this.jobGrid_Click);
            // 
            // jobTable
            // 
            this.jobTable.GridColumnStyles.Add(this.noCol);
            this.jobTable.GridColumnStyles.Add(this.noOfLinesCol);
            this.jobTable.MappingName = "whseActivity";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 20);
            this.label1.Text = "Inlagringsuppdrag";
            // 
            // noCol
            // 
            this.noCol.Format = "";
            this.noCol.FormatInfo = null;
            this.noCol.HeaderText = "Batch-ID";
            this.noCol.MappingName = "no";
            this.noCol.NullText = "";
            this.noCol.Width = 100;
            // 
            // noOfLinesCol
            // 
            this.noOfLinesCol.Format = "";
            this.noOfLinesCol.FormatInfo = null;
            this.noOfLinesCol.HeaderText = "Antal rader";
            this.noOfLinesCol.MappingName = "noOfLines";
            this.noOfLinesCol.NullText = "";
            this.noOfLinesCol.Width = 100;
            // 
            // StoreJobs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.jobGrid);
            this.Controls.Add(this.label1);
            this.Menu = this.mainMenu1;
            this.Name = "StoreJobs";
            this.Text = "Inlagringsuppdrag";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.DataGrid jobGrid;
        private System.Windows.Forms.DataGridTableStyle jobTable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridTextBoxColumn noCol;
        private System.Windows.Forms.DataGridTextBoxColumn noOfLinesCol;
    }
}