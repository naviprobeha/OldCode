namespace Navipro.SmartInventory
{
    partial class Ship
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
            this.quantityShippedCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.quantityToShipCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.button11 = new System.Windows.Forms.Button();
            this.quantityCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.salesLineGrid = new System.Windows.Forms.DataGrid();
            this.salesLineTable = new System.Windows.Forms.DataGridTableStyle();
            this.itemNoCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.variantCodeCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.descriptionCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.unitOfMeasureCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.mainMenu2 = new System.Windows.Forms.MainMenu();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // quantityShippedCol
            // 
            this.quantityShippedCol.Format = "";
            this.quantityShippedCol.FormatInfo = null;
            this.quantityShippedCol.HeaderText = "Levererat antal";
            this.quantityShippedCol.MappingName = "quantityShipped";
            this.quantityShippedCol.NullText = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 218);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 45);
            this.button1.TabIndex = 30;
            this.button1.Text = "Avbryt";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // quantityToShipCol
            // 
            this.quantityToShipCol.Format = "";
            this.quantityToShipCol.FormatInfo = null;
            this.quantityToShipCol.HeaderText = "Antal att leverera";
            this.quantityToShipCol.MappingName = "quantityToShip";
            this.quantityToShipCol.NullText = "";
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(160, 218);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(80, 45);
            this.button11.TabIndex = 29;
            this.button11.Text = "Bokför";
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // quantityCol
            // 
            this.quantityCol.Format = "";
            this.quantityCol.FormatInfo = null;
            this.quantityCol.HeaderText = "Antal";
            this.quantityCol.MappingName = "quantity";
            this.quantityCol.NullText = "";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(4, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(233, 20);
            this.label2.Text = "Order:";
            // 
            // salesLineGrid
            // 
            this.salesLineGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.salesLineGrid.Location = new System.Drawing.Point(0, 29);
            this.salesLineGrid.Name = "salesLineGrid";
            this.salesLineGrid.Size = new System.Drawing.Size(240, 186);
            this.salesLineGrid.TabIndex = 27;
            this.salesLineGrid.TableStyles.Add(this.salesLineTable);
            // 
            // salesLineTable
            // 
            this.salesLineTable.GridColumnStyles.Add(this.itemNoCol);
            this.salesLineTable.GridColumnStyles.Add(this.variantCodeCol);
            this.salesLineTable.GridColumnStyles.Add(this.descriptionCol);
            this.salesLineTable.GridColumnStyles.Add(this.unitOfMeasureCol);
            this.salesLineTable.GridColumnStyles.Add(this.quantityCol);
            this.salesLineTable.GridColumnStyles.Add(this.quantityShippedCol);
            this.salesLineTable.GridColumnStyles.Add(this.quantityToShipCol);
            this.salesLineTable.MappingName = "salesLine";
            // 
            // itemNoCol
            // 
            this.itemNoCol.Format = "";
            this.itemNoCol.FormatInfo = null;
            this.itemNoCol.HeaderText = "Artikelnr";
            this.itemNoCol.MappingName = "itemNo";
            this.itemNoCol.NullText = "";
            this.itemNoCol.Width = 100;
            // 
            // variantCodeCol
            // 
            this.variantCodeCol.Format = "";
            this.variantCodeCol.FormatInfo = null;
            this.variantCodeCol.HeaderText = "Variantkod";
            this.variantCodeCol.MappingName = "variantCode";
            this.variantCodeCol.NullText = "";
            this.variantCodeCol.Width = 75;
            // 
            // descriptionCol
            // 
            this.descriptionCol.Format = "";
            this.descriptionCol.FormatInfo = null;
            this.descriptionCol.HeaderText = "Beskrivning";
            this.descriptionCol.MappingName = "description";
            this.descriptionCol.NullText = "";
            this.descriptionCol.Width = 120;
            // 
            // unitOfMeasureCol
            // 
            this.unitOfMeasureCol.Format = "";
            this.unitOfMeasureCol.FormatInfo = null;
            this.unitOfMeasureCol.HeaderText = "Enhet";
            this.unitOfMeasureCol.MappingName = "unitOfMeasure";
            this.unitOfMeasureCol.NullText = "";
            this.unitOfMeasureCol.Width = 75;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(76, 218);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(88, 45);
            this.button2.TabIndex = 33;
            this.button2.Text = "Plocka";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Ship
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.salesLineGrid);
            this.Controls.Add(this.button2);
            this.Menu = this.mainMenu1;
            this.Name = "Ship";
            this.Text = "Ship";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridTextBoxColumn quantityShippedCol;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridTextBoxColumn quantityToShipCol;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.DataGridTextBoxColumn quantityCol;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGrid salesLineGrid;
        private System.Windows.Forms.DataGridTableStyle salesLineTable;
        private System.Windows.Forms.DataGridTextBoxColumn itemNoCol;
        private System.Windows.Forms.DataGridTextBoxColumn variantCodeCol;
        private System.Windows.Forms.DataGridTextBoxColumn descriptionCol;
        private System.Windows.Forms.DataGridTextBoxColumn unitOfMeasureCol;
        private System.Windows.Forms.MainMenu mainMenu2;
        private System.Windows.Forms.Button button2;
    }
}