namespace SmartInventory
{
    partial class SaveItems
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.heIdBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.lineGrid = new System.Windows.Forms.DataGrid();
            this.jobLineTable = new System.Windows.Forms.DataGridTableStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.itemNoCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.quantityCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.handleUnitIdCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.heIdBox);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Location = new System.Drawing.Point(0, 166);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(240, 56);
            // 
            // heIdBox
            // 
            this.heIdBox.Location = new System.Drawing.Point(16, 29);
            this.heIdBox.Name = "heIdBox";
            this.heIdBox.Size = new System.Drawing.Size(208, 21);
            this.heIdBox.TabIndex = 0;
            this.heIdBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.heIdBox_KeyPress);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(16, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(208, 20);
            this.label5.Text = "Scanna HE ID:";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(8, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(208, 20);
            this.label2.Text = "Frekvensklass:";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular);
            this.button2.Location = new System.Drawing.Point(8, 230);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(224, 32);
            this.button2.TabIndex = 7;
            this.button2.Text = "Tillbaka";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // lineGrid
            // 
            this.lineGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lineGrid.Location = new System.Drawing.Point(0, 46);
            this.lineGrid.Name = "lineGrid";
            this.lineGrid.Size = new System.Drawing.Size(240, 120);
            this.lineGrid.TabIndex = 8;
            this.lineGrid.TableStyles.Add(this.jobLineTable);
            // 
            // jobLineTable
            // 
            this.jobLineTable.GridColumnStyles.Add(this.handleUnitIdCol);
            this.jobLineTable.GridColumnStyles.Add(this.itemNoCol);
            this.jobLineTable.GridColumnStyles.Add(this.quantityCol);
            this.jobLineTable.MappingName = "whseActivityLine";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(8, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(208, 20);
            this.label1.Text = "Inlagring - Batch:";
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
            // quantityCol
            // 
            this.quantityCol.Format = "";
            this.quantityCol.FormatInfo = null;
            this.quantityCol.HeaderText = "Antal";
            this.quantityCol.MappingName = "quantity";
            this.quantityCol.NullText = "";
            this.quantityCol.Width = 75;
            // 
            // handleUnitIdCol
            // 
            this.handleUnitIdCol.Format = "";
            this.handleUnitIdCol.FormatInfo = null;
            this.handleUnitIdCol.HeaderText = "Hanteringsenhet";
            this.handleUnitIdCol.MappingName = "handleUnitId";
            this.handleUnitIdCol.NullText = "";
            this.handleUnitIdCol.Width = 100;
            // 
            // SaveItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.lineGrid);
            this.Controls.Add(this.label1);
            this.Menu = this.mainMenu1;
            this.Name = "SaveItems";
            this.Text = "Påfyllning - Inlagring";
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox heIdBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGrid lineGrid;
        private System.Windows.Forms.DataGridTableStyle jobLineTable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridTextBoxColumn itemNoCol;
        private System.Windows.Forms.DataGridTextBoxColumn quantityCol;
        private System.Windows.Forms.DataGridTextBoxColumn handleUnitIdCol;
    }
}