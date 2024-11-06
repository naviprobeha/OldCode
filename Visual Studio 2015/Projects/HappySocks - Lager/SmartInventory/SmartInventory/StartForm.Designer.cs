namespace Navipro.SmartInventory
{
    partial class StartForm
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
            this.receiveBtn = new System.Windows.Forms.Button();
            this.physInvBtn = new System.Windows.Forms.Button();
            this.shipBtn = new System.Windows.Forms.Button();
            this.logViewList = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // receiveBtn
            // 
            this.receiveBtn.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.receiveBtn.Location = new System.Drawing.Point(14, 75);
            this.receiveBtn.Name = "receiveBtn";
            this.receiveBtn.Size = new System.Drawing.Size(214, 45);
            this.receiveBtn.TabIndex = 0;
            this.receiveBtn.Text = "Inleverans";
            this.receiveBtn.Click += new System.EventHandler(this.receiveBtn_Click);
            // 
            // physInvBtn
            // 
            this.physInvBtn.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.physInvBtn.Location = new System.Drawing.Point(14, 126);
            this.physInvBtn.Name = "physInvBtn";
            this.physInvBtn.Size = new System.Drawing.Size(214, 45);
            this.physInvBtn.TabIndex = 1;
            this.physInvBtn.Text = "Inventering";
            this.physInvBtn.Click += new System.EventHandler(this.physInvBtn_Click);
            // 
            // shipBtn
            // 
            this.shipBtn.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.shipBtn.Location = new System.Drawing.Point(14, 24);
            this.shipBtn.Name = "shipBtn";
            this.shipBtn.Size = new System.Drawing.Size(214, 45);
            this.shipBtn.TabIndex = 2;
            this.shipBtn.Text = "Utleverans";
            this.shipBtn.Click += new System.EventHandler(this.shipBtn_Click);
            // 
            // logViewList
            // 
            this.logViewList.Location = new System.Drawing.Point(14, 195);
            this.logViewList.Name = "logViewList";
            this.logViewList.Size = new System.Drawing.Size(77, 58);
            this.logViewList.TabIndex = 3;
            this.logViewList.Visible = false;
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.logViewList);
            this.Controls.Add(this.shipBtn);
            this.Controls.Add(this.physInvBtn);
            this.Controls.Add(this.receiveBtn);
            this.Menu = this.mainMenu1;
            this.Name = "StartForm";
            this.Text = "SmartInventory";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button receiveBtn;
        private System.Windows.Forms.Button physInvBtn;
        private System.Windows.Forms.Button shipBtn;
        private System.Windows.Forms.ListBox logViewList;
    }
}

