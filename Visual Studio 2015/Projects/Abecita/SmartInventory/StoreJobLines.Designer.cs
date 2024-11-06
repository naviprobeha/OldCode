namespace SmartInventory
{
    partial class StoreJobLines
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
            this.jobLineGrid = new System.Windows.Forms.DataGrid();
            this.jobLineTable = new System.Windows.Forms.DataGridTableStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.itemNoCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.quantityCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.handleUnitIdCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.locationCodeCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.zoneCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.freqCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.binCodeCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.SuspendLayout();
            // 
            // jobLineGrid
            // 
            this.jobLineGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.jobLineGrid.Location = new System.Drawing.Point(0, 34);
            this.jobLineGrid.Name = "jobLineGrid";
            this.jobLineGrid.Size = new System.Drawing.Size(240, 208);
            this.jobLineGrid.TabIndex = 2;
            this.jobLineGrid.TableStyles.Add(this.jobLineTable);
            // 
            // jobLineTable
            // 
            this.jobLineTable.GridColumnStyles.Add(this.itemNoCol);
            this.jobLineTable.GridColumnStyles.Add(this.quantityCol);
            this.jobLineTable.GridColumnStyles.Add(this.handleUnitIdCol);
            this.jobLineTable.GridColumnStyles.Add(this.locationCodeCol);
            this.jobLineTable.GridColumnStyles.Add(this.zoneCol);
            this.jobLineTable.GridColumnStyles.Add(this.freqCol);
            this.jobLineTable.GridColumnStyles.Add(this.binCodeCol);
            this.jobLineTable.MappingName = "whseActivityLine";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(8, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 20);
            this.label1.Text = "Uppdrag:";
            // 
            // itemNoCol
            // 
            this.itemNoCol.Format = "";
            this.itemNoCol.FormatInfo = null;
            this.itemNoCol.HeaderText = "Artikelnr";
            this.itemNoCol.MappingName = "itemNo";
            this.itemNoCol.NullText = "";
            // 
            // quantityCol
            // 
            this.quantityCol.Format = "";
            this.quantityCol.FormatInfo = null;
            this.quantityCol.HeaderText = "Antal";
            this.quantityCol.MappingName = "quantity";
            this.quantityCol.NullText = "";
            // 
            // handleUnitIdCol
            // 
            this.handleUnitIdCol.Format = "";
            this.handleUnitIdCol.FormatInfo = null;
            this.handleUnitIdCol.HeaderText = "Hanteringsenhet";
            this.handleUnitIdCol.MappingName = "handleUnitId";
            this.handleUnitIdCol.NullText = "";
            // 
            // locationCodeCol
            // 
            this.locationCodeCol.Format = "";
            this.locationCodeCol.FormatInfo = null;
            this.locationCodeCol.HeaderText = "Lagerställe";
            this.locationCodeCol.MappingName = "locationCode";
            this.locationCodeCol.NullText = "";
            // 
            // zoneCol
            // 
            this.zoneCol.Format = "";
            this.zoneCol.FormatInfo = null;
            this.zoneCol.HeaderText = "Zon";
            this.zoneCol.MappingName = "zone";
            this.zoneCol.NullText = "";
            // 
            // freqCol
            // 
            this.freqCol.Format = "";
            this.freqCol.FormatInfo = null;
            this.freqCol.HeaderText = "Frekvens";
            this.freqCol.MappingName = "freq";
            this.freqCol.NullText = "";
            // 
            // binCodeCol
            // 
            this.binCodeCol.Format = "";
            this.binCodeCol.FormatInfo = null;
            this.binCodeCol.HeaderText = "Lagerplats";
            this.binCodeCol.MappingName = "binCode";
            this.binCodeCol.NullText = "";
            // 
            // StoreJobLines
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.jobLineGrid);
            this.Controls.Add(this.label1);
            this.Menu = this.mainMenu1;
            this.Name = "StoreJobLines";
            this.Text = "Inlagringsuppdrag";
            this.Load += new System.EventHandler(this.StoreJobLines_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGrid jobLineGrid;
        private System.Windows.Forms.DataGridTableStyle jobLineTable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridTextBoxColumn itemNoCol;
        private System.Windows.Forms.DataGridTextBoxColumn quantityCol;
        private System.Windows.Forms.DataGridTextBoxColumn handleUnitIdCol;
        private System.Windows.Forms.DataGridTextBoxColumn locationCodeCol;
        private System.Windows.Forms.DataGridTextBoxColumn zoneCol;
        private System.Windows.Forms.DataGridTextBoxColumn freqCol;
        private System.Windows.Forms.DataGridTextBoxColumn binCodeCol;
    }
}