namespace Navipro.SmartInventory
{
    partial class PickRetailCreate
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
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.shipmentListGrid = new System.Windows.Forms.DataGrid();
            this.shipmentListTable = new System.Windows.Forms.DataGridTableStyle();
            this.noCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.nameCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.noOfLinesCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.logViewList = new System.Windows.Forms.ListBox();
            this.userBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.wagonBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.button2.Location = new System.Drawing.Point(128, 263);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(107, 49);
            this.button2.TabIndex = 7;
            this.button2.Text = "Skapa";
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(4, 263);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 49);
            this.button1.TabIndex = 6;
            this.button1.Text = "Avbryt";
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 21);
            this.label1.Text = "Välj butiksuppdrag";
            // 
            // shipmentListGrid
            // 
            this.shipmentListGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.shipmentListGrid.Location = new System.Drawing.Point(1, 28);
            this.shipmentListGrid.Name = "shipmentListGrid";
            this.shipmentListGrid.PreferredRowHeight = 22;
            this.shipmentListGrid.Size = new System.Drawing.Size(237, 141);
            this.shipmentListGrid.TabIndex = 5;
            this.shipmentListGrid.TableStyles.Add(this.shipmentListTable);
            // 
            // shipmentListTable
            // 
            this.shipmentListTable.GridColumnStyles.Add(this.noCol);
            this.shipmentListTable.GridColumnStyles.Add(this.nameCol);
            this.shipmentListTable.GridColumnStyles.Add(this.noOfLinesCol);
            this.shipmentListTable.MappingName = "shipmentHeader";
            // 
            // noCol
            // 
            this.noCol.Format = "";
            this.noCol.FormatInfo = null;
            this.noCol.HeaderText = "Nr";
            this.noCol.MappingName = "no";
            this.noCol.NullText = "";
            this.noCol.Width = 75;
            // 
            // nameCol
            // 
            this.nameCol.Format = "";
            this.nameCol.FormatInfo = null;
            this.nameCol.HeaderText = "Namn";
            this.nameCol.MappingName = "name";
            this.nameCol.NullText = "";
            this.nameCol.Width = 125;
            // 
            // noOfLinesCol
            // 
            this.noOfLinesCol.Format = "";
            this.noOfLinesCol.FormatInfo = null;
            this.noOfLinesCol.HeaderText = "Antal rader";
            this.noOfLinesCol.MappingName = "noOfLines";
            this.noOfLinesCol.NullText = "";
            // 
            // logViewList
            // 
            this.logViewList.Location = new System.Drawing.Point(308, 107);
            this.logViewList.Name = "logViewList";
            this.logViewList.Size = new System.Drawing.Size(100, 98);
            this.logViewList.TabIndex = 47;
            this.logViewList.Visible = false;
            // 
            // userBox
            // 
            this.userBox.Location = new System.Drawing.Point(4, 189);
            this.userBox.Name = "userBox";
            this.userBox.Size = new System.Drawing.Size(232, 23);
            this.userBox.TabIndex = 49;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(4, 172);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 20);
            this.label4.Text = "Användare";
            // 
            // wagonBox
            // 
            this.wagonBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.wagonBox.Location = new System.Drawing.Point(4, 233);
            this.wagonBox.Name = "wagonBox";
            this.wagonBox.Size = new System.Drawing.Size(232, 23);
            this.wagonBox.TabIndex = 52;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(4, 216);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 20);
            this.label5.Text = "Plockvagn";
            // 
            // PickRetailCreate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(638, 455);
            this.Controls.Add(this.wagonBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.userBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.logViewList);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.shipmentListGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PickRetailCreate";
            this.Text = "PickRetailCreate";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGrid shipmentListGrid;
        private System.Windows.Forms.DataGridTableStyle shipmentListTable;
        private System.Windows.Forms.DataGridTextBoxColumn noCol;
        private System.Windows.Forms.DataGridTextBoxColumn nameCol;
        private System.Windows.Forms.DataGridTextBoxColumn noOfLinesCol;
        private System.Windows.Forms.ListBox logViewList;
        private System.Windows.Forms.ComboBox userBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox wagonBox;
        private System.Windows.Forms.Label label5;
    }
}