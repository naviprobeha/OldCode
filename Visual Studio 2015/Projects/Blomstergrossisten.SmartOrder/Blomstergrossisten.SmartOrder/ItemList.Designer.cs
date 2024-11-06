namespace SmartOrder
{
    partial class ItemList
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.prodGroupBox = new System.Windows.Forms.ComboBox();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.itemGrid = new System.Windows.Forms.DataGrid();
            this.itemTable = new System.Windows.Forms.DataGridTableStyle();
            this.itemNoCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.descriptionCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.priceCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.productGroupCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.inputPanel1 = new Microsoft.WindowsCE.Forms.InputPanel(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.Text = "Produktgrupp:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(124, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.Text = "Beskrivning:";
            // 
            // prodGroupBox
            // 
            this.prodGroupBox.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.prodGroupBox.Location = new System.Drawing.Point(4, 21);
            this.prodGroupBox.Name = "prodGroupBox";
            this.prodGroupBox.Size = new System.Drawing.Size(114, 29);
            this.prodGroupBox.TabIndex = 2;
            this.prodGroupBox.SelectedIndexChanged += new System.EventHandler(this.prodGroupBox_SelectedIndexChanged);
            // 
            // searchBox
            // 
            this.searchBox.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.searchBox.Location = new System.Drawing.Point(125, 21);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(100, 29);
            this.searchBox.TabIndex = 3;
            this.searchBox.TextChanged += new System.EventHandler(this.searchBox_TextChanged);
            this.searchBox.GotFocus += new System.EventHandler(this.searchBox_GotFocus);
            this.searchBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.searchBox_KeyPress);
            this.searchBox.LostFocus += new System.EventHandler(this.searchBox_LostFocus);
            // 
            // itemGrid
            // 
            this.itemGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.itemGrid.Location = new System.Drawing.Point(0, 57);
            this.itemGrid.Name = "itemGrid";
            this.itemGrid.Size = new System.Drawing.Size(239, 200);
            this.itemGrid.TabIndex = 4;
            this.itemGrid.TableStyles.Add(this.itemTable);
            this.itemGrid.LostFocus += new System.EventHandler(this.itemGrid_LostFocus);
            this.itemGrid.Click += new System.EventHandler(this.itemGrid_Click);
            // 
            // itemTable
            // 
            this.itemTable.GridColumnStyles.Add(this.itemNoCol);
            this.itemTable.GridColumnStyles.Add(this.descriptionCol);
            this.itemTable.GridColumnStyles.Add(this.priceCol);
            this.itemTable.GridColumnStyles.Add(this.productGroupCol);
            this.itemTable.MappingName = "item";
            // 
            // itemNoCol
            // 
            this.itemNoCol.Format = "";
            this.itemNoCol.FormatInfo = null;
            this.itemNoCol.HeaderText = "Artikelnr";
            this.itemNoCol.MappingName = "no";
            this.itemNoCol.NullText = "";
            this.itemNoCol.Width = 5;
            // 
            // descriptionCol
            // 
            this.descriptionCol.Format = "";
            this.descriptionCol.FormatInfo = null;
            this.descriptionCol.HeaderText = "Beskrivning";
            this.descriptionCol.MappingName = "description";
            this.descriptionCol.NullText = "";
            this.descriptionCol.Width = 170;
            // 
            // priceCol
            // 
            this.priceCol.Format = "";
            this.priceCol.FormatInfo = null;
            this.priceCol.HeaderText = "Pris";
            this.priceCol.MappingName = "unitPrice";
            this.priceCol.NullText = "";
            // 
            // productGroupCol
            // 
            this.productGroupCol.Format = "";
            this.productGroupCol.FormatInfo = null;
            this.productGroupCol.HeaderText = "Produktgrupp";
            this.productGroupCol.MappingName = "productGroupCode";
            this.productGroupCol.NullText = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(140, 263);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 33);
            this.button1.TabIndex = 9;
            this.button1.Text = "Nästa";
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(38, 263);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(96, 33);
            this.button2.TabIndex = 10;
            this.button2.Text = "Avbryt";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ItemList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(239, 313);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.itemGrid);
            this.Controls.Add(this.searchBox);
            this.Controls.Add(this.prodGroupBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ItemList";
            this.Text = "ItemList";
            this.Load += new System.EventHandler(this.ItemList_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.ItemList_Closing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox prodGroupBox;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.DataGrid itemGrid;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private Microsoft.WindowsCE.Forms.InputPanel inputPanel1;
        private System.Windows.Forms.DataGridTableStyle itemTable;
        private System.Windows.Forms.DataGridTextBoxColumn itemNoCol;
        private System.Windows.Forms.DataGridTextBoxColumn descriptionCol;
        private System.Windows.Forms.DataGridTextBoxColumn priceCol;
        private System.Windows.Forms.DataGridTextBoxColumn productGroupCol;
    }
}