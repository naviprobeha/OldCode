namespace SmartOrder
{
    partial class StartForm
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
            this.searchBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.visitGrid = new System.Windows.Forms.DataGrid();
            this.activeCustomerTable = new System.Windows.Forms.DataGridTableStyle();
            this.noCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.nameCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.cityCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.inputPanel1 = new Microsoft.WindowsCE.Forms.InputPanel(this.components);
            this.button3 = new System.Windows.Forms.Button();
            this.blockedCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 20);
            this.label1.Text = "Aktiva kunder";
            // 
            // searchBox
            // 
            this.searchBox.Location = new System.Drawing.Point(46, 27);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(191, 23);
            this.searchBox.TabIndex = 1;
            this.searchBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.searchBox.GotFocus += new System.EventHandler(this.textBox1_GotFocus);
            this.searchBox.LostFocus += new System.EventHandler(this.textBox1_LostFocus);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(4, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 20);
            this.label2.Text = "Sök:";
            // 
            // visitGrid
            // 
            this.visitGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.visitGrid.Location = new System.Drawing.Point(4, 56);
            this.visitGrid.Name = "visitGrid";
            this.visitGrid.Size = new System.Drawing.Size(233, 206);
            this.visitGrid.TabIndex = 3;
            this.visitGrid.TableStyles.Add(this.activeCustomerTable);
            this.visitGrid.Click += new System.EventHandler(this.visitGrid_Click);
            // 
            // activeCustomerTable
            // 
            this.activeCustomerTable.GridColumnStyles.Add(this.noCol);
            this.activeCustomerTable.GridColumnStyles.Add(this.nameCol);
            this.activeCustomerTable.GridColumnStyles.Add(this.cityCol);
            this.activeCustomerTable.GridColumnStyles.Add(this.blockedCol);
            this.activeCustomerTable.MappingName = "activeCustomer";
            // 
            // noCol
            // 
            this.noCol.Format = "";
            this.noCol.FormatInfo = null;
            this.noCol.HeaderText = "Kundnr";
            this.noCol.MappingName = "no";
            this.noCol.NullText = "";
            // 
            // nameCol
            // 
            this.nameCol.Format = "";
            this.nameCol.FormatInfo = null;
            this.nameCol.HeaderText = "Namn";
            this.nameCol.MappingName = "name";
            this.nameCol.NullText = "";
            this.nameCol.Width = 150;
            // 
            // cityCol
            // 
            this.cityCol.Format = "";
            this.cityCol.FormatInfo = null;
            this.cityCol.HeaderText = "Ort";
            this.cityCol.MappingName = "city";
            this.cityCol.NullText = "";
            this.cityCol.Width = 100;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(141, 268);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 33);
            this.button1.TabIndex = 4;
            this.button1.Text = "Ny order";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(39, 268);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(96, 33);
            this.button2.TabIndex = 5;
            this.button2.Text = "Prisfråga";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(3, 268);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(30, 33);
            this.button3.TabIndex = 8;
            this.button3.Text = "M";
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // blockedCol
            // 
            this.blockedCol.Format = "";
            this.blockedCol.FormatInfo = null;
            this.blockedCol.HeaderText = "Spärrad";
            this.blockedCol.MappingName = "blocked";
            this.blockedCol.NullText = "";
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(247, 312);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.visitGrid);
            this.Controls.Add(this.searchBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "StartForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGrid visitGrid;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridTableStyle activeCustomerTable;
        private System.Windows.Forms.DataGridTextBoxColumn noCol;
        private System.Windows.Forms.DataGridTextBoxColumn nameCol;
        private System.Windows.Forms.DataGridTextBoxColumn cityCol;
        private System.Windows.Forms.Button button2;
        private Microsoft.WindowsCE.Forms.InputPanel inputPanel1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridTextBoxColumn blockedCol;
    }
}

