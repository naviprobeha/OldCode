namespace Navipro.SmartInventory
{
    partial class ShipCartonList
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
            this.button11 = new System.Windows.Forms.Button();
            this.cartonGrid = new System.Windows.Forms.DataGrid();
            this.cartonTable = new System.Windows.Forms.DataGridTableStyle();
            this.cartonCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.splitOnQtyCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.variantCodeBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.itemNoBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(160, 219);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(80, 45);
            this.button11.TabIndex = 33;
            this.button11.Text = "Stäng";
            this.button11.Click += new System.EventHandler(this.button11_Click_1);
            // 
            // cartonGrid
            // 
            this.cartonGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.cartonGrid.Location = new System.Drawing.Point(0, 73);
            this.cartonGrid.Name = "cartonGrid";
            this.cartonGrid.Size = new System.Drawing.Size(240, 141);
            this.cartonGrid.TabIndex = 32;
            this.cartonGrid.TableStyles.Add(this.cartonTable);
            // 
            // cartonTable
            // 
            this.cartonTable.GridColumnStyles.Add(this.cartonCol);
            this.cartonTable.GridColumnStyles.Add(this.splitOnQtyCol);
            this.cartonTable.MappingName = "salesLineCarton";
            // 
            // cartonCol
            // 
            this.cartonCol.Format = "";
            this.cartonCol.FormatInfo = null;
            this.cartonCol.HeaderText = "Kartong";
            this.cartonCol.MappingName = "cartonNo";
            this.cartonCol.Width = 150;
            // 
            // splitOnQtyCol
            // 
            this.splitOnQtyCol.Format = "";
            this.splitOnQtyCol.FormatInfo = null;
            this.splitOnQtyCol.HeaderText = "Dela vid antal";
            this.splitOnQtyCol.MappingName = "splitOnQuantity";
            this.splitOnQtyCol.Width = 80;
            // 
            // variantCodeBox
            // 
            this.variantCodeBox.Location = new System.Drawing.Point(136, 46);
            this.variantCodeBox.Name = "variantCodeBox";
            this.variantCodeBox.ReadOnly = true;
            this.variantCodeBox.Size = new System.Drawing.Size(100, 21);
            this.variantCodeBox.TabIndex = 39;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(136, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 20);
            this.label3.Text = "Variantkod";
            // 
            // itemNoBox
            // 
            this.itemNoBox.Location = new System.Drawing.Point(4, 46);
            this.itemNoBox.Name = "itemNoBox";
            this.itemNoBox.ReadOnly = true;
            this.itemNoBox.Size = new System.Drawing.Size(127, 21);
            this.itemNoBox.TabIndex = 38;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(4, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.Text = "Artikelnr";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(4, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(233, 20);
            this.label2.Text = "Kartonger";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 219);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 45);
            this.button1.TabIndex = 43;
            this.button1.Text = "Radera";
            this.button1.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(76, 219);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(88, 45);
            this.button2.TabIndex = 44;
            this.button2.Text = "Ändra";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ShipCartonList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.variantCodeBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.itemNoBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.cartonGrid);
            this.Menu = this.mainMenu1;
            this.Name = "ShipCartonList";
            this.Text = "Kartonger";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.DataGrid cartonGrid;
        private System.Windows.Forms.DataGridTableStyle cartonTable;
        private System.Windows.Forms.TextBox variantCodeBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox itemNoBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridTextBoxColumn cartonCol;
        private System.Windows.Forms.DataGridTextBoxColumn splitOnQtyCol;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}