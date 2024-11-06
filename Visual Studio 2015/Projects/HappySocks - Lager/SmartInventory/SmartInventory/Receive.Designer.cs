namespace Navipro.SmartInventory
{
    partial class Receive
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
            this.purchLineGrid = new System.Windows.Forms.DataGrid();
            this.purchaseLineTable = new System.Windows.Forms.DataGridTableStyle();
            this.itemNoCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.variantCodeCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.descriptionCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.unitOfMeasureCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.quantityCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.quantityReceivedCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.quantityToReceiveCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.eanBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button11 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.qtyBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // purchLineGrid
            // 
            this.purchLineGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.purchLineGrid.Location = new System.Drawing.Point(0, 55);
            this.purchLineGrid.Name = "purchLineGrid";
            this.purchLineGrid.Size = new System.Drawing.Size(240, 162);
            this.purchLineGrid.TabIndex = 0;
            this.purchLineGrid.TableStyles.Add(this.purchaseLineTable);
            // 
            // purchaseLineTable
            // 
            this.purchaseLineTable.GridColumnStyles.Add(this.itemNoCol);
            this.purchaseLineTable.GridColumnStyles.Add(this.variantCodeCol);
            this.purchaseLineTable.GridColumnStyles.Add(this.descriptionCol);
            this.purchaseLineTable.GridColumnStyles.Add(this.quantityCol);
            this.purchaseLineTable.GridColumnStyles.Add(this.quantityToReceiveCol);
            this.purchaseLineTable.GridColumnStyles.Add(this.quantityReceivedCol);
            this.purchaseLineTable.GridColumnStyles.Add(this.unitOfMeasureCol);
            this.purchaseLineTable.MappingName = "purchaseLine";
            // 
            // itemNoCol
            // 
            this.itemNoCol.Format = "";
            this.itemNoCol.FormatInfo = null;
            this.itemNoCol.HeaderText = "Artikelnr";
            this.itemNoCol.MappingName = "itemNo";
            this.itemNoCol.NullText = "";
            // 
            // variantCodeCol
            // 
            this.variantCodeCol.Format = "";
            this.variantCodeCol.FormatInfo = null;
            this.variantCodeCol.HeaderText = "Variantkod";
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
            this.descriptionCol.Width = 80;
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
            // quantityCol
            // 
            this.quantityCol.Format = "";
            this.quantityCol.FormatInfo = null;
            this.quantityCol.HeaderText = "Antal";
            this.quantityCol.MappingName = "quantity";
            this.quantityCol.NullText = "";
            // 
            // quantityReceivedCol
            // 
            this.quantityReceivedCol.Format = "";
            this.quantityReceivedCol.FormatInfo = null;
            this.quantityReceivedCol.HeaderText = "Inlevererat antal";
            this.quantityReceivedCol.MappingName = "quantityReceived";
            this.quantityReceivedCol.NullText = "";
            // 
            // quantityToReceiveCol
            // 
            this.quantityToReceiveCol.Format = "";
            this.quantityToReceiveCol.FormatInfo = null;
            this.quantityToReceiveCol.HeaderText = "Antal att inleverera";
            this.quantityToReceiveCol.MappingName = "quantityToReceive";
            this.quantityToReceiveCol.NullText = "";
            // 
            // eanBox
            // 
            this.eanBox.Location = new System.Drawing.Point(90, 28);
            this.eanBox.Name = "eanBox";
            this.eanBox.Size = new System.Drawing.Size(100, 21);
            this.eanBox.TabIndex = 1;
            this.eanBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 20);
            this.label1.Text = "EAN:";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(4, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(233, 20);
            this.label2.Text = "Scanna varje artikels EAN-kod.";
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(160, 220);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(80, 45);
            this.button11.TabIndex = 22;
            this.button11.Text = "Bokför";
            this.button11.Click += new System.EventHandler(this.button11_Click_1);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 220);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 45);
            this.button1.TabIndex = 23;
            this.button1.Text = "Avbryt";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(76, 220);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(88, 45);
            this.button2.TabIndex = 26;
            this.button2.Text = "Ändra";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // qtyBox
            // 
            this.qtyBox.Location = new System.Drawing.Point(196, 28);
            this.qtyBox.Name = "qtyBox";
            this.qtyBox.ReadOnly = true;
            this.qtyBox.Size = new System.Drawing.Size(41, 21);
            this.qtyBox.TabIndex = 29;
            // 
            // Receive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.qtyBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.eanBox);
            this.Controls.Add(this.purchLineGrid);
            this.Menu = this.mainMenu1;
            this.Name = "Receive";
            this.Text = "Receive";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGrid purchLineGrid;
        private System.Windows.Forms.TextBox eanBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridTableStyle purchaseLineTable;
        private System.Windows.Forms.DataGridTextBoxColumn itemNoCol;
        private System.Windows.Forms.DataGridTextBoxColumn variantCodeCol;
        private System.Windows.Forms.DataGridTextBoxColumn descriptionCol;
        private System.Windows.Forms.DataGridTextBoxColumn unitOfMeasureCol;
        private System.Windows.Forms.DataGridTextBoxColumn quantityCol;
        private System.Windows.Forms.DataGridTextBoxColumn quantityReceivedCol;
        private System.Windows.Forms.DataGridTextBoxColumn quantityToReceiveCol;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox qtyBox;
    }
}