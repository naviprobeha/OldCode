namespace SmartShipment
{
    partial class SynchSettings
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
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.receiverBox = new System.Windows.Forms.TextBox();
            this.hostBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.userIdBox = new System.Windows.Forms.TextBox();
            this.agentIdBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label6.Location = new System.Drawing.Point(12, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(168, 20);
            this.label6.Text = "Synkroniseringsinställningar";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.receiverBox);
            this.panel1.Controls.Add(this.hostBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(4, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(232, 64);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 20);
            this.label2.Text = "Mottagare ID:";
            // 
            // receiverBox
            // 
            this.receiverBox.Location = new System.Drawing.Point(88, 32);
            this.receiverBox.Name = "receiverBox";
            this.receiverBox.Size = new System.Drawing.Size(128, 21);
            this.receiverBox.TabIndex = 1;
            // 
            // hostBox
            // 
            this.hostBox.Location = new System.Drawing.Point(88, 8);
            this.hostBox.Name = "hostBox";
            this.hostBox.Size = new System.Drawing.Size(128, 21);
            this.hostBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.Text = "Värd:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.passwordBox);
            this.panel2.Controls.Add(this.userIdBox);
            this.panel2.Controls.Add(this.agentIdBox);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(4, 99);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(232, 88);
            // 
            // passwordBox
            // 
            this.passwordBox.Location = new System.Drawing.Point(88, 56);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.PasswordChar = '*';
            this.passwordBox.Size = new System.Drawing.Size(128, 21);
            this.passwordBox.TabIndex = 0;
            // 
            // userIdBox
            // 
            this.userIdBox.Location = new System.Drawing.Point(88, 32);
            this.userIdBox.Name = "userIdBox";
            this.userIdBox.Size = new System.Drawing.Size(128, 21);
            this.userIdBox.TabIndex = 1;
            // 
            // agentIdBox
            // 
            this.agentIdBox.Location = new System.Drawing.Point(88, 8);
            this.agentIdBox.Name = "agentIdBox";
            this.agentIdBox.Size = new System.Drawing.Size(128, 21);
            this.agentIdBox.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(8, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 16);
            this.label5.Text = "Lösenord:";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 16);
            this.label4.Text = "Användar-ID:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.Text = "Agent ID:";
            // 
            // SynchSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Menu = this.mainMenu1;
            this.Name = "SynchSettings";
            this.Text = "Synkroniseringsinställningar";
            this.Load += new System.EventHandler(this.SynchSettings_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.SynchSettings_Closing);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox receiverBox;
        private System.Windows.Forms.TextBox hostBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox passwordBox;
        private System.Windows.Forms.TextBox userIdBox;
        private System.Windows.Forms.TextBox agentIdBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
    }
}