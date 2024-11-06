namespace Navipro.SmartInventory
{
    partial class ItemBinContentList
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
            this.binContentListGrid = new System.Windows.Forms.DataGrid();
            this.itemBinContentTable = new System.Windows.Forms.DataGridTableStyle();
            this.binCodeCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.quantityCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // binContentListGrid
            // 
            this.binContentListGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.binContentListGrid.Location = new System.Drawing.Point(1, 28);
            this.binContentListGrid.Name = "binContentListGrid";
            this.binContentListGrid.PreferredRowHeight = 22;
            this.binContentListGrid.Size = new System.Drawing.Size(237, 229);
            this.binContentListGrid.TabIndex = 0;
            this.binContentListGrid.TableStyles.Add(this.itemBinContentTable);
            // 
            // itemBinContentTable
            // 
            this.itemBinContentTable.GridColumnStyles.Add(this.binCodeCol);
            this.itemBinContentTable.GridColumnStyles.Add(this.quantityCol);
            this.itemBinContentTable.MappingName = "itemBinContent";
            // 
            // binCodeCol
            // 
            this.binCodeCol.Format = "";
            this.binCodeCol.FormatInfo = null;
            this.binCodeCol.HeaderText = "Lagerplats";
            this.binCodeCol.MappingName = "binCode";
            this.binCodeCol.NullText = "";
            this.binCodeCol.Width = 80;
            // 
            // quantityCol
            // 
            this.quantityCol.Format = "";
            this.quantityCol.FormatInfo = null;
            this.quantityCol.HeaderText = "Antal";
            this.quantityCol.MappingName = "quantity";
            this.quantityCol.NullText = "";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 21);
            this.label1.Text = "Välj lagerplats:";
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
            // ItemBinContentList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(638, 455);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.binContentListGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ItemBinContentList";
            this.Text = "Byt lagerplats";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGrid binContentListGrid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridTableStyle itemBinContentTable;
        private System.Windows.Forms.DataGridTextBoxColumn binCodeCol;
        private System.Windows.Forms.DataGridTextBoxColumn quantityCol;
    }
}