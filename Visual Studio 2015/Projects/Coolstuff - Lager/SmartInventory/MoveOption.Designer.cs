namespace Navipro.SmartInventory
{
    partial class MoveOption
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
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.wagonBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.logViewList = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(128, 146);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 103);
            this.label3.Text = "Efter uttag, välj inlagring för att lagra in artiklarna på ny plats.";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(128, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 72);
            this.label2.Text = "Börja med att ta ut de artiklar som skall flyttas.";
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.button3.Location = new System.Drawing.Point(3, 227);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(118, 64);
            this.button3.TabIndex = 16;
            this.button3.Text = "Avbryt";
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.button2.Location = new System.Drawing.Point(3, 146);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(118, 64);
            this.button2.TabIndex = 15;
            this.button2.Text = "Inlagring";
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 20);
            this.label1.Text = "Lagerflytt";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(3, 66);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 64);
            this.button1.TabIndex = 14;
            this.button1.Text = "Uttag";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // wagonBox
            // 
            this.wagonBox.Location = new System.Drawing.Point(99, 31);
            this.wagonBox.Name = "wagonBox";
            this.wagonBox.Size = new System.Drawing.Size(113, 23);
            this.wagonBox.TabIndex = 74;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 20);
            this.label4.Text = "Scanna vagn:";
            // 
            // logViewList
            // 
            this.logViewList.Location = new System.Drawing.Point(387, 124);
            this.logViewList.Name = "logViewList";
            this.logViewList.Size = new System.Drawing.Size(100, 50);
            this.logViewList.TabIndex = 76;
            this.logViewList.Visible = false;
            // 
            // MoveOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(638, 455);
            this.Controls.Add(this.logViewList);
            this.Controls.Add(this.wagonBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MoveOption";
            this.Text = "MoveOption";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox wagonBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox logViewList;
    }
}