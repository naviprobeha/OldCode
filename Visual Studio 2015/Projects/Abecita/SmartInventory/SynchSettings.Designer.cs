namespace SmartInventory
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.synchMethod = new System.Windows.Forms.ComboBox();
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
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.allowDecimal = new System.Windows.Forms.CheckBox();
            this.locationCodeBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.inputPanel1 = new Microsoft.WindowsCE.Forms.InputPanel();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(240, 268);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Location = new System.Drawing.Point(0, 0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(240, 245);
            this.tabPage1.Text = "Synkronisering";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label6.Location = new System.Drawing.Point(8, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(168, 20);
            this.label6.Text = "Synkroniseringsinställningar";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.synchMethod);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.receiverBox);
            this.panel1.Controls.Add(this.hostBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(232, 88);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(8, 11);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 20);
            this.label7.Text = "Metod:";
            // 
            // synchMethod
            // 
            this.synchMethod.Items.Add("HTTP");
            this.synchMethod.Items.Add("TCP/IP");
            this.synchMethod.Location = new System.Drawing.Point(88, 8);
            this.synchMethod.Name = "synchMethod";
            this.synchMethod.Size = new System.Drawing.Size(128, 22);
            this.synchMethod.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 20);
            this.label2.Text = "Mottagare ID:";
            // 
            // receiverBox
            // 
            this.receiverBox.Location = new System.Drawing.Point(88, 56);
            this.receiverBox.Name = "receiverBox";
            this.receiverBox.Size = new System.Drawing.Size(128, 21);
            this.receiverBox.TabIndex = 3;
            this.receiverBox.GotFocus += new System.EventHandler(this.receiverBox_GotFocus);
            // 
            // hostBox
            // 
            this.hostBox.Location = new System.Drawing.Point(88, 32);
            this.hostBox.Name = "hostBox";
            this.hostBox.Size = new System.Drawing.Size(128, 21);
            this.hostBox.TabIndex = 4;
            this.hostBox.GotFocus += new System.EventHandler(this.hostBox_GotFocus);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 35);
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
            this.panel2.Location = new System.Drawing.Point(0, 120);
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
            this.passwordBox.GotFocus += new System.EventHandler(this.passwordBox_GotFocus);
            // 
            // userIdBox
            // 
            this.userIdBox.Location = new System.Drawing.Point(88, 32);
            this.userIdBox.Name = "userIdBox";
            this.userIdBox.Size = new System.Drawing.Size(128, 21);
            this.userIdBox.TabIndex = 1;
            this.userIdBox.GotFocus += new System.EventHandler(this.userIdBox_GotFocus);
            // 
            // agentIdBox
            // 
            this.agentIdBox.Location = new System.Drawing.Point(88, 8);
            this.agentIdBox.Name = "agentIdBox";
            this.agentIdBox.Size = new System.Drawing.Size(128, 21);
            this.agentIdBox.TabIndex = 2;
            this.agentIdBox.GotFocus += new System.EventHandler(this.agentIdBox_GotFocus);
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
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel3);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Location = new System.Drawing.Point(0, 0);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(240, 245);
            this.tabPage2.Text = "Lager";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.allowDecimal);
            this.panel3.Controls.Add(this.locationCodeBox);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Location = new System.Drawing.Point(0, 32);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(232, 88);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(8, 43);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 20);
            this.label9.Text = "Decimaltal:";
            // 
            // allowDecimal
            // 
            this.allowDecimal.Location = new System.Drawing.Point(88, 40);
            this.allowDecimal.Name = "allowDecimal";
            this.allowDecimal.Size = new System.Drawing.Size(100, 20);
            this.allowDecimal.TabIndex = 1;
            this.allowDecimal.Text = "Ja";
            // 
            // locationCodeBox
            // 
            this.locationCodeBox.Location = new System.Drawing.Point(88, 8);
            this.locationCodeBox.Name = "locationCodeBox";
            this.locationCodeBox.Size = new System.Drawing.Size(128, 21);
            this.locationCodeBox.TabIndex = 2;
            this.locationCodeBox.GotFocus += new System.EventHandler(this.locationCodeBox_GotFocus);
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(8, 11);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(80, 13);
            this.label11.Text = "Lagerställekod:";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label8.Location = new System.Drawing.Point(8, 8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(168, 20);
            this.label8.Text = "Lagerinställningar";
            // 
            // SynchSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.tabControl1);
            this.Menu = this.mainMenu1;
            this.Name = "SynchSettings";
            this.Text = "Inställningar";
            this.Load += new System.EventHandler(this.SynchSettings_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.SynchSettings_Closing);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox synchMethod;
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
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox allowDecimal;
        private System.Windows.Forms.TextBox locationCodeBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label8;
        private Microsoft.WindowsCE.Forms.InputPanel inputPanel1;
    }
}