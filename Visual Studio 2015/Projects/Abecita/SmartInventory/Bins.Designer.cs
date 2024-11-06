namespace SmartInventory
{
    partial class Bins
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
            this.binGrid = new System.Windows.Forms.DataGrid();
            this.binTable = new System.Windows.Forms.DataGridTableStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.locationCodeCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.codeCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.zoneCodeCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.blockingCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.emptyCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.SuspendLayout();
            // 
            // binGrid
            // 
            this.binGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.binGrid.Location = new System.Drawing.Point(0, 30);
            this.binGrid.Name = "binGrid";
            this.binGrid.Size = new System.Drawing.Size(240, 208);
            this.binGrid.TabIndex = 2;
            this.binGrid.TableStyles.Add(this.binTable);
            // 
            // binTable
            // 
            this.binTable.GridColumnStyles.Add(this.locationCodeCol);
            this.binTable.GridColumnStyles.Add(this.codeCol);
            this.binTable.GridColumnStyles.Add(this.zoneCodeCol);
            this.binTable.GridColumnStyles.Add(this.blockingCol);
            this.binTable.GridColumnStyles.Add(this.emptyCol);
            this.binTable.MappingName = "bin";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(8, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 20);
            this.label1.Text = "Lagerplatser";
            // 
            // locationCodeCol
            // 
            this.locationCodeCol.Format = "";
            this.locationCodeCol.FormatInfo = null;
            this.locationCodeCol.HeaderText = "Lagerplats";
            this.locationCodeCol.MappingName = "locationCode";
            this.locationCodeCol.NullText = "";
            // 
            // codeCol
            // 
            this.codeCol.Format = "";
            this.codeCol.FormatInfo = null;
            this.codeCol.HeaderText = "Kod";
            this.codeCol.MappingName = "code";
            this.codeCol.NullText = "";
            // 
            // zoneCodeCol
            // 
            this.zoneCodeCol.Format = "";
            this.zoneCodeCol.FormatInfo = null;
            this.zoneCodeCol.HeaderText = "Zon";
            this.zoneCodeCol.MappingName = "zoneCode";
            this.zoneCodeCol.NullText = "";
            // 
            // blockingCol
            // 
            this.blockingCol.Format = "";
            this.blockingCol.FormatInfo = null;
            this.blockingCol.HeaderText = "Låst";
            this.blockingCol.MappingName = "blocking";
            this.blockingCol.NullText = "";
            // 
            // emptyCol
            // 
            this.emptyCol.Format = "";
            this.emptyCol.FormatInfo = null;
            this.emptyCol.HeaderText = "Tom";
            this.emptyCol.MappingName = "empty";
            this.emptyCol.NullText = "";
            // 
            // Bins
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.binGrid);
            this.Controls.Add(this.label1);
            this.Menu = this.mainMenu1;
            this.Name = "Bins";
            this.Text = "Lagerplatser";
            this.Load += new System.EventHandler(this.Bins_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGrid binGrid;
        private System.Windows.Forms.DataGridTableStyle binTable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridTextBoxColumn locationCodeCol;
        private System.Windows.Forms.DataGridTextBoxColumn codeCol;
        private System.Windows.Forms.DataGridTextBoxColumn zoneCodeCol;
        private System.Windows.Forms.DataGridTextBoxColumn blockingCol;
        private System.Windows.Forms.DataGridTextBoxColumn emptyCol;
    }
}