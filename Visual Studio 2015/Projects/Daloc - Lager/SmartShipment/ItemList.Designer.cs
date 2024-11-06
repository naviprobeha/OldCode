namespace SmartShipment
{
    partial class ItemList
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
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.itemGrid = new System.Windows.Forms.DataGrid();
            this.itemTable = new System.Windows.Forms.DataGridTableStyle();
            this.itemNoCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.descriptionCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular);
            this.button2.Location = new System.Drawing.Point(8, 226);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(104, 40);
            this.button2.TabIndex = 3;
            this.button2.Text = "Avbryt";
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular);
            this.button1.Location = new System.Drawing.Point(128, 226);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 40);
            this.button1.TabIndex = 4;
            this.button1.Text = "Ok";
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // itemGrid
            // 
            this.itemGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.itemGrid.Location = new System.Drawing.Point(0, 2);
            this.itemGrid.Name = "itemGrid";
            this.itemGrid.Size = new System.Drawing.Size(240, 216);
            this.itemGrid.TabIndex = 5;
            this.itemGrid.TableStyles.Add(this.itemTable);
            this.itemGrid.CurrentCellChanged += new System.EventHandler(this.itemGrid_CurrentCellChanged);
            this.itemGrid.Click += new System.EventHandler(this.itemGrid_Click_1);
            // 
            // itemTable
            // 
            this.itemTable.GridColumnStyles.Add(this.itemNoCol);
            this.itemTable.GridColumnStyles.Add(this.descriptionCol);
            this.itemTable.MappingName = "item";
            // 
            // itemNoCol
            // 
            this.itemNoCol.Format = "";
            this.itemNoCol.FormatInfo = null;
            this.itemNoCol.HeaderText = "Artikelnr";
            this.itemNoCol.MappingName = "no";
            this.itemNoCol.NullText = "";
            this.itemNoCol.Width = 100;
            // 
            // descriptionCol
            // 
            this.descriptionCol.Format = "";
            this.descriptionCol.FormatInfo = null;
            this.descriptionCol.HeaderText = "Beskrivning";
            this.descriptionCol.MappingName = "description";
            this.descriptionCol.NullText = "";
            this.descriptionCol.Width = 150;
            // 
            // ItemList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.itemGrid);
            this.Menu = this.mainMenu1;
            this.Name = "ItemList";
            this.Text = "Artikellista";
            this.Load += new System.EventHandler(this.ItemList_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGrid itemGrid;
        private System.Windows.Forms.DataGridTableStyle itemTable;
        private System.Windows.Forms.DataGridTextBoxColumn itemNoCol;
        private System.Windows.Forms.DataGridTextBoxColumn descriptionCol;
    }
}