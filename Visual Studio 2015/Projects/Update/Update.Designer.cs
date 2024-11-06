namespace Update
{
    partial class Update
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
            this.button1 = new System.Windows.Forms.Button();
            this.caption = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.message = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(124, 206);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 40);
            this.button1.TabIndex = 5;
            this.button1.Text = "Uppdatera";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // caption
            // 
            this.caption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.caption.Location = new System.Drawing.Point(15, 11);
            this.caption.Name = "caption";
            this.caption.Size = new System.Drawing.Size(213, 20);
            this.caption.Text = "Uppdatering";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(15, 83);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(213, 20);
            // 
            // message
            // 
            this.message.Location = new System.Drawing.Point(15, 43);
            this.message.Name = "message";
            this.message.Size = new System.Drawing.Size(213, 21);
            this.message.TabIndex = 8;
            // 
            // Update
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.caption);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.message);
            this.Menu = this.mainMenu1;
            this.Name = "Update";
            this.Text = "Uppdatering";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label caption;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox message;
    }
}