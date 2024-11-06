namespace Navipro.SmartInventory
{
    partial class Storing
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
            this.label1 = new System.Windows.Forms.Label();
            this.scanBinBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.noOfLinesBox = new System.Windows.Forms.TextBox();
            this.putAwayNoBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.storeLinesGrid = new System.Windows.Forms.DataGrid();
            this.storeLineTable = new System.Windows.Forms.DataGridTableStyle();
            this.binCodeCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.descriptionCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.description2Col = new System.Windows.Forms.DataGridTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 21);
            this.label1.Text = "Inlagring";
            // 
            // scanBinBox
            // 
            this.scanBinBox.Location = new System.Drawing.Point(4, 83);
            this.scanBinBox.Name = "scanBinBox";
            this.scanBinBox.Size = new System.Drawing.Size(232, 23);
            this.scanBinBox.TabIndex = 17;
            this.scanBinBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.scanBinBox_KeyPress);
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
            this.noOfLinesBox.TabIndex = 16;
            // 
            // putAwayNoBox
            // 
            this.putAwayNoBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.putAwayNoBox.Location = new System.Drawing.Point(4, 42);
            this.putAwayNoBox.Name = "putAwayNoBox";
            this.putAwayNoBox.ReadOnly = true;
            this.putAwayNoBox.Size = new System.Drawing.Size(112, 23);
            this.putAwayNoBox.TabIndex = 15;
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
            // storeLinesGrid
            // 
            this.storeLinesGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.storeLinesGrid.Location = new System.Drawing.Point(1, 112);
            this.storeLinesGrid.Name = "storeLinesGrid";
            this.storeLinesGrid.PreferredRowHeight = 22;
            this.storeLinesGrid.Size = new System.Drawing.Size(237, 147);
            this.storeLinesGrid.TabIndex = 21;
            this.storeLinesGrid.TableStyles.Add(this.storeLineTable);
            // 
            // storeLineTable
            // 
            this.storeLineTable.GridColumnStyles.Add(this.binCodeCol);
            this.storeLineTable.GridColumnStyles.Add(this.descriptionCol);
            this.storeLineTable.GridColumnStyles.Add(this.description2Col);
            this.storeLineTable.MappingName = "storeLine";
            // 
            // binCodeCol
            // 
            this.binCodeCol.Format = "";
            this.binCodeCol.FormatInfo = null;
            this.binCodeCol.HeaderText = "Lagerplats";
            this.binCodeCol.MappingName = "binCode";
            this.binCodeCol.NullText = "";
            this.binCodeCol.Width = 75;
            // 
            // descriptionCol
            // 
            this.descriptionCol.Format = "";
            this.descriptionCol.FormatInfo = null;
            this.descriptionCol.HeaderText = "Beskrivning";
            this.descriptionCol.MappingName = "description";
            this.descriptionCol.NullText = "";
            this.descriptionCol.Width = 75;
            // 
            // description2Col
            // 
            this.description2Col.Format = "";
            this.description2Col.FormatInfo = null;
            this.description2Col.HeaderText = "Beskrivning 2";
            this.description2Col.MappingName = "description2";
            this.description2Col.NullText = "";
            this.description2Col.Width = 75;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(4, 263);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 49);
            this.button1.TabIndex = 22;
            this.button1.Text = "Avbryt";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Storing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(638, 455);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.storeLinesGrid);
            this.Controls.Add(this.scanBinBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.noOfLinesBox);
            this.Controls.Add(this.putAwayNoBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Storing";
            this.Text = "Storing";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox scanBinBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox noOfLinesBox;
        private System.Windows.Forms.TextBox putAwayNoBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGrid storeLinesGrid;
        private System.Windows.Forms.DataGridTableStyle storeLineTable;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridTextBoxColumn binCodeCol;
        private System.Windows.Forms.DataGridTextBoxColumn descriptionCol;
        private System.Windows.Forms.DataGridTextBoxColumn description2Col;
    }
}