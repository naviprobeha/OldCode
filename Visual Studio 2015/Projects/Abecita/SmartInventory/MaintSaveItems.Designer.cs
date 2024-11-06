namespace SmartInventory
{
    partial class MaintSaveItems
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
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.panel3 = new System.Windows.Forms.Panel();
            this.scanHEidBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.jobGrid = new System.Windows.Forms.DataGrid();
            this.jobLines = new System.Windows.Forms.DataGridTableStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.binCodeCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.handleUnitCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.itemNoCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.quantityCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.locationCodeCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.menuItem2);
            this.menuItem1.Text = "Editera";
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "Ta bort";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.scanHEidBox);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Location = new System.Drawing.Point(0, 166);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(240, 56);
            // 
            // scanHEidBox
            // 
            this.scanHEidBox.Location = new System.Drawing.Point(16, 29);
            this.scanHEidBox.Name = "scanHEidBox";
            this.scanHEidBox.Size = new System.Drawing.Size(208, 21);
            this.scanHEidBox.TabIndex = 0;
            this.scanHEidBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.scanHEidBox_KeyPress);
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
            this.label2.Location = new System.Drawing.Point(8, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(208, 20);
            this.label2.Text = "Inlagring";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular);
            this.button2.Location = new System.Drawing.Point(8, 231);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(224, 32);
            this.button2.TabIndex = 7;
            this.button2.Text = "Tillbaka";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // jobGrid
            // 
            this.jobGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.jobGrid.Location = new System.Drawing.Point(0, 45);
            this.jobGrid.Name = "jobGrid";
            this.jobGrid.Size = new System.Drawing.Size(240, 120);
            this.jobGrid.TabIndex = 8;
            this.jobGrid.TableStyles.Add(this.jobLines);
            this.jobGrid.Click += new System.EventHandler(this.jobGrid_Click);
            // 
            // jobLines
            // 
            this.jobLines.GridColumnStyles.Add(this.locationCodeCol);
            this.jobLines.GridColumnStyles.Add(this.binCodeCol);
            this.jobLines.GridColumnStyles.Add(this.handleUnitCol);
            this.jobLines.GridColumnStyles.Add(this.itemNoCol);
            this.jobLines.GridColumnStyles.Add(this.quantityCol);
            this.jobLines.MappingName = "whseItemStore";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(8, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(208, 20);
            this.label1.Text = "Lagervård - Omflyttning";
            // 
            // binCodeCol
            // 
            this.binCodeCol.Format = "";
            this.binCodeCol.FormatInfo = null;
            this.binCodeCol.HeaderText = "Lagerplats";
            this.binCodeCol.MappingName = "binCode";
            this.binCodeCol.NullText = "";
            this.binCodeCol.Width = 70;
            // 
            // handleUnitCol
            // 
            this.handleUnitCol.Format = "";
            this.handleUnitCol.FormatInfo = null;
            this.handleUnitCol.HeaderText = "Hanteringsenhet";
            this.handleUnitCol.MappingName = "handleUnitId";
            this.handleUnitCol.NullText = "";
            this.handleUnitCol.Width = 80;
            // 
            // itemNoCol
            // 
            this.itemNoCol.Format = "";
            this.itemNoCol.FormatInfo = null;
            this.itemNoCol.HeaderText = "Artikelnr";
            this.itemNoCol.MappingName = "itemNo";
            this.itemNoCol.NullText = "";
            // 
            // quantityCol
            // 
            this.quantityCol.Format = "";
            this.quantityCol.FormatInfo = null;
            this.quantityCol.HeaderText = "Antal";
            this.quantityCol.MappingName = "quantity";
            this.quantityCol.NullText = "";
            // 
            // locationCodeCol
            // 
            this.locationCodeCol.Format = "";
            this.locationCodeCol.FormatInfo = null;
            this.locationCodeCol.HeaderText = "Lagerställe";
            this.locationCodeCol.MappingName = "locationCode";
            this.locationCodeCol.NullText = "";
            this.locationCodeCol.Width = 30;
            // 
            // MaintSaveItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.jobGrid);
            this.Controls.Add(this.label1);
            this.Menu = this.mainMenu1;
            this.Name = "MaintSaveItems";
            this.Text = "Lagervård - Omflyttning";
            this.Load += new System.EventHandler(this.MaintSaveItems_Load);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox scanHEidBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGrid jobGrid;
        private System.Windows.Forms.DataGridTableStyle jobLines;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.DataGridTextBoxColumn binCodeCol;
        private System.Windows.Forms.DataGridTextBoxColumn handleUnitCol;
        private System.Windows.Forms.DataGridTextBoxColumn itemNoCol;
        private System.Windows.Forms.DataGridTextBoxColumn locationCodeCol;
        private System.Windows.Forms.DataGridTextBoxColumn quantityCol;
    }
}