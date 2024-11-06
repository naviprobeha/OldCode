namespace Navipro.SmartInventory
{
    partial class PickItemLines
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
            this.pickListNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.noOfLinesBox = new System.Windows.Forms.TextBox();
            this.pickLinesGrid = new System.Windows.Forms.DataGrid();
            this.pickLinesTable = new System.Windows.Forms.DataGridTableStyle();
            this.itemNoCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.variantCodeCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.descriptionCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.description2Col = new System.Windows.Forms.DataGridTextBoxColumn();
            this.SuspendLayout();
            // 
            // pickListNo
            // 
            this.pickListNo.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.pickListNo.Location = new System.Drawing.Point(4, 21);
            this.pickListNo.Name = "pickListNo";
            this.pickListNo.ReadOnly = true;
            this.pickListNo.Size = new System.Drawing.Size(112, 23);
            this.pickListNo.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(4, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 20);
            this.label4.Text = "Plocklista";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(4, 263);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 49);
            this.button1.TabIndex = 22;
            this.button1.Text = "Återgå";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(124, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.Text = "Antal rader";
            this.label2.ParentChanged += new System.EventHandler(this.label2_ParentChanged);
            // 
            // noOfLinesBox
            // 
            this.noOfLinesBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.noOfLinesBox.Location = new System.Drawing.Point(124, 21);
            this.noOfLinesBox.Name = "noOfLinesBox";
            this.noOfLinesBox.ReadOnly = true;
            this.noOfLinesBox.Size = new System.Drawing.Size(71, 23);
            this.noOfLinesBox.TabIndex = 29;
            // 
            // pickLinesGrid
            // 
            this.pickLinesGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.pickLinesGrid.Location = new System.Drawing.Point(2, 50);
            this.pickLinesGrid.Name = "pickLinesGrid";
            this.pickLinesGrid.PreferredRowHeight = 32;
            this.pickLinesGrid.Size = new System.Drawing.Size(235, 210);
            this.pickLinesGrid.TabIndex = 30;
            this.pickLinesGrid.TableStyles.Add(this.pickLinesTable);
            // 
            // pickLinesTable
            // 
            this.pickLinesTable.GridColumnStyles.Add(this.itemNoCol);
            this.pickLinesTable.GridColumnStyles.Add(this.variantCodeCol);
            this.pickLinesTable.GridColumnStyles.Add(this.descriptionCol);
            this.pickLinesTable.GridColumnStyles.Add(this.description2Col);
            this.pickLinesTable.MappingName = "pickItemLine";
            // 
            // itemNoCol
            // 
            this.itemNoCol.Format = "";
            this.itemNoCol.FormatInfo = null;
            this.itemNoCol.HeaderText = "Artikelnr";
            this.itemNoCol.MappingName = "itemNo";
            this.itemNoCol.NullText = "";
            this.itemNoCol.Width = 75;
            // 
            // variantCodeCol
            // 
            this.variantCodeCol.Format = "";
            this.variantCodeCol.FormatInfo = null;
            this.variantCodeCol.HeaderText = "Variant";
            this.variantCodeCol.MappingName = "variantCode";
            this.variantCodeCol.NullText = "";
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
            // PickItemLines
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(638, 455);
            this.Controls.Add(this.pickLinesGrid);
            this.Controls.Add(this.noOfLinesBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pickListNo);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PickItemLines";
            this.Text = "PickList";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox pickListNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox noOfLinesBox;
        private System.Windows.Forms.DataGrid pickLinesGrid;
        private System.Windows.Forms.DataGridTableStyle pickLinesTable;
        private System.Windows.Forms.DataGridTextBoxColumn itemNoCol;
        private System.Windows.Forms.DataGridTextBoxColumn variantCodeCol;
        private System.Windows.Forms.DataGridTextBoxColumn descriptionCol;
        private System.Windows.Forms.DataGridTextBoxColumn description2Col;
    }
}