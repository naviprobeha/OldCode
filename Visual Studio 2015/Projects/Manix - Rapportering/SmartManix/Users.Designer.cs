namespace Navipro.SmartInventory
{
    partial class Users
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
            this.userGrid = new System.Windows.Forms.DataGrid();
            this.userTable = new System.Windows.Forms.DataGridTableStyle();
            this.codeCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.nameCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // userGrid
            // 
            this.userGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.userGrid.Location = new System.Drawing.Point(1, 28);
            this.userGrid.Name = "userGrid";
            this.userGrid.PreferredRowHeight = 22;
            this.userGrid.Size = new System.Drawing.Size(237, 229);
            this.userGrid.TabIndex = 0;
            this.userGrid.TableStyles.Add(this.userTable);
            // 
            // userTable
            // 
            this.userTable.GridColumnStyles.Add(this.codeCol);
            this.userTable.GridColumnStyles.Add(this.nameCol);
            this.userTable.MappingName = "user";
            // 
            // codeCol
            // 
            this.codeCol.Format = "";
            this.codeCol.FormatInfo = null;
            this.codeCol.HeaderText = "Nr";
            this.codeCol.MappingName = "code";
            this.codeCol.NullText = "";
            this.codeCol.Width = 70;
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
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 21);
            this.label1.Text = "Välj användare";
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
            // Users
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(638, 455);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.userGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Users";
            this.Text = "Plocklistor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Users_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGrid userGrid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridTextBoxColumn codeCol;
        private System.Windows.Forms.DataGridTextBoxColumn nameCol;
        private System.Windows.Forms.DataGridTableStyle userTable;
    }
}