namespace Navipro.SmartInventory
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.logViewList = new System.Windows.Forms.ListBox();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(120, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 51);
            this.button1.TabIndex = 0;
            this.button1.Text = "Inleverans";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.button2.Location = new System.Drawing.Point(120, 111);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(115, 96);
            this.button2.TabIndex = 1;
            this.button2.Text = "Inventering";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.button3.Location = new System.Drawing.Point(3, 111);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(115, 96);
            this.button3.TabIndex = 2;
            this.button3.Text = "Skapa plock";
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.button4.Location = new System.Drawing.Point(3, 3);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(115, 102);
            this.button4.TabIndex = 3;
            this.button4.Text = "Plockning";
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.button5.Location = new System.Drawing.Point(120, 213);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(115, 101);
            this.button5.TabIndex = 4;
            this.button5.Text = "Avsluta";
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // logViewList
            // 
            this.logViewList.Location = new System.Drawing.Point(65, 183);
            this.logViewList.Name = "logViewList";
            this.logViewList.Size = new System.Drawing.Size(100, 50);
            this.logViewList.TabIndex = 5;
            this.logViewList.Visible = false;
            // 
            // button6
            // 
            this.button6.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.button6.Location = new System.Drawing.Point(3, 213);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(115, 101);
            this.button6.TabIndex = 6;
            this.button6.Text = "Lagerflytt";
            this.button6.Click += new System.EventHandler(this.button6_Click_1);
            // 
            // button7
            // 
            this.button7.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.button7.Location = new System.Drawing.Point(120, 54);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(115, 51);
            this.button7.TabIndex = 7;
            this.button7.Text = "Inlagring";
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(638, 455);
            this.Controls.Add(this.logViewList);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StartForm";
            this.Text = "SmartInventory";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ListBox logViewList;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
    }
}

