namespace SmartInventory
{
    partial class Jobs
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
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.jobGrid = new System.Windows.Forms.DataGrid();
            this.jobTable = new System.Windows.Forms.DataGridTableStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.noCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.noOfLinesCol = new System.Windows.Forms.DataGridTextBoxColumn();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular);
            this.button3.Location = new System.Drawing.Point(128, 190);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(104, 32);
            this.button3.TabIndex = 5;
            this.button3.Text = "Inlagring";
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular);
            this.button2.Location = new System.Drawing.Point(8, 230);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(224, 32);
            this.button2.TabIndex = 6;
            this.button2.Text = "Tillbaka";
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular);
            this.button1.Location = new System.Drawing.Point(8, 190);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 32);
            this.button1.TabIndex = 7;
            this.button1.Text = "Uttag";
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // jobGrid
            // 
            this.jobGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.jobGrid.Location = new System.Drawing.Point(0, 30);
            this.jobGrid.Name = "jobGrid";
            this.jobGrid.Size = new System.Drawing.Size(240, 152);
            this.jobGrid.TabIndex = 8;
            this.jobGrid.TableStyles.Add(this.jobTable);
            this.jobGrid.Click += new System.EventHandler(this.jobGrid_Click_1);
            // 
            // jobTable
            // 
            this.jobTable.GridColumnStyles.Add(this.noCol);
            this.jobTable.GridColumnStyles.Add(this.noOfLinesCol);
            this.jobTable.MappingName = "whseActivity";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(8, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 20);
            this.label1.Text = "Påfyllningsuppdrag";
            // 
            // noCol
            // 
            this.noCol.Format = "";
            this.noCol.FormatInfo = null;
            this.noCol.HeaderText = "Batch-ID";
            this.noCol.MappingName = "no";
            this.noCol.NullText = "";
            this.noCol.Width = 100;
            // 
            // noOfLinesCol
            // 
            this.noOfLinesCol.Format = "";
            this.noOfLinesCol.FormatInfo = null;
            this.noOfLinesCol.HeaderText = "Antal rader";
            this.noOfLinesCol.MappingName = "noOfLines";
            this.noOfLinesCol.NullText = "";
            this.noOfLinesCol.Width = 100;
            // 
            // Jobs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.jobGrid);
            this.Controls.Add(this.label1);
            this.Menu = this.mainMenu1;
            this.Name = "Jobs";
            this.Text = "Påfyllningsuppdrag";
            this.Load += new System.EventHandler(this.Jobs_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGrid jobGrid;
        private System.Windows.Forms.DataGridTableStyle jobTable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridTextBoxColumn noCol;
        private System.Windows.Forms.DataGridTextBoxColumn noOfLinesCol;
    }
}