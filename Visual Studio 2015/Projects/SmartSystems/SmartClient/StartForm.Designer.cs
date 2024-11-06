namespace Navipro.SmartSystems.SmartClient
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.logLabel1 = new System.Windows.Forms.Label();
            this.logLabel2 = new System.Windows.Forms.Label();
            this.logLabel3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(240, 169);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(-3, 235);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(243, 56);
            // 
            // logLabel1
            // 
            this.logLabel1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.logLabel1.Location = new System.Drawing.Point(3, 172);
            this.logLabel1.Name = "logLabel1";
            this.logLabel1.Size = new System.Drawing.Size(234, 17);
            // 
            // logLabel2
            // 
            this.logLabel2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.logLabel2.Location = new System.Drawing.Point(3, 189);
            this.logLabel2.Name = "logLabel2";
            this.logLabel2.Size = new System.Drawing.Size(234, 17);
            // 
            // logLabel3
            // 
            this.logLabel3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.logLabel3.Location = new System.Drawing.Point(3, 206);
            this.logLabel3.Name = "logLabel3";
            this.logLabel3.Size = new System.Drawing.Size(234, 17);
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.logLabel3);
            this.Controls.Add(this.logLabel2);
            this.Controls.Add(this.logLabel1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Name = "StartForm";
            this.Text = "SmartClient";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label logLabel1;
        private System.Windows.Forms.Label logLabel2;
        private System.Windows.Forms.Label logLabel3;

    }
}

