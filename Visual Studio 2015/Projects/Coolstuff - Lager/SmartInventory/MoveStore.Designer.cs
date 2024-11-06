namespace Navipro.SmartInventory
{
    partial class MoveStore
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
            this.storeLinesGrid = new System.Windows.Forms.DataGrid();
            this.storeLineTable = new System.Windows.Forms.DataGridTableStyle();
            this.descriptionCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.description2Col = new System.Windows.Forms.DataGridTextBoxColumn();
            this.scanBin = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.noOfLinesBox = new System.Windows.Forms.TextBox();
            this.wagonBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
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
            // storeLinesGrid
            // 
            this.storeLinesGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.storeLinesGrid.Location = new System.Drawing.Point(1, 112);
            this.storeLinesGrid.Name = "storeLinesGrid";
            this.storeLinesGrid.PreferredRowHeight = 22;
            this.storeLinesGrid.Size = new System.Drawing.Size(237, 147);
            this.storeLinesGrid.TabIndex = 30;
            this.storeLinesGrid.TableStyles.Add(this.storeLineTable);
            // 
            // storeLineTable
            // 
            this.storeLineTable.GridColumnStyles.Add(this.descriptionCol);
            this.storeLineTable.GridColumnStyles.Add(this.description2Col);
            this.storeLineTable.GridColumnStyles.Add(this.quantityCol);
            this.storeLineTable.MappingName = "storeLine";
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
            // description2Col
            // 
            this.description2Col.Format = "";
            this.description2Col.FormatInfo = null;
            this.description2Col.HeaderText = "Beskrivning 2";
            this.description2Col.MappingName = "description2";
            this.description2Col.NullText = "";
            this.description2Col.Width = 100;
            // 
            // scanBin
            // 
            this.scanBin.Location = new System.Drawing.Point(4, 83);
            this.scanBin.Name = "scanBin";
            this.scanBin.Size = new System.Drawing.Size(232, 23);
            this.scanBin.TabIndex = 29;
            this.scanBin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.scanEanBox_KeyPress);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(4, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(142, 20);
            this.label3.Text = "Scanna lagerplats";
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
            // wagonBox
            // 
            this.wagonBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.wagonBox.Location = new System.Drawing.Point(4, 42);
            this.wagonBox.Name = "wagonBox";
            this.wagonBox.ReadOnly = true;
            this.wagonBox.Size = new System.Drawing.Size(112, 23);
            this.wagonBox.TabIndex = 27;
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
            this.label4.Text = "Vagn";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(207, 21);
            this.label1.Text = "Lagerflytt - Inlagring";
            // 
            // quantityCol
            // 
            this.quantityCol.Format = "";
            this.quantityCol.FormatInfo = null;
            this.quantityCol.HeaderText = "Antal";
            this.quantityCol.MappingName = "quantity";
            this.quantityCol.NullText = "";
            // 
            // MoveStore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(638, 455);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.storeLinesGrid);
            this.Controls.Add(this.scanBin);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.noOfLinesBox);
            this.Controls.Add(this.wagonBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MoveStore";
            this.Text = "MoveStore";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGrid storeLinesGrid;
        private System.Windows.Forms.DataGridTableStyle storeLineTable;
        private System.Windows.Forms.TextBox scanBin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox noOfLinesBox;
        private System.Windows.Forms.TextBox wagonBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridTextBoxColumn descriptionCol;
        private System.Windows.Forms.DataGridTextBoxColumn description2Col;
        private System.Windows.Forms.DataGridTextBoxColumn quantityCol;
    }
}