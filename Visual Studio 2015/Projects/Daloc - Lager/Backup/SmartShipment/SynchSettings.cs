using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data.SqlServerCe;

namespace SmartShipment
{
	/// <summary>
	/// Summary description for SynchSettings.
	/// </summary>
	public class SynchSettings : System.Windows.Forms.Form
	{
		private Microsoft.WindowsCE.Forms.InputPanel inputPanel1;
		private System.Windows.Forms.MainMenu mainMenu1;

        private SmartDatabase smartDatabase;
		private DataSetup dataSetup;
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
		private Agent agent;
	
		public SynchSettings(SmartDatabase smartDatabase)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.smartDatabase = smartDatabase;
			this.dataSetup = smartDatabase.getSetup();
			this.agent = dataSetup.getAgent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.inputPanel1 = new Microsoft.WindowsCE.Forms.InputPanel();
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
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label6.ForeColor = System.Drawing.SystemColors.HotTrack;
			this.label6.Location = new System.Drawing.Point(8, 8);
			this.label6.Size = new System.Drawing.Size(168, 20);
			this.label6.Text = "Synkroniseringsinställningar";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.receiverBox);
			this.panel1.Controls.Add(this.hostBox);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Location = new System.Drawing.Point(0, 32);
			this.panel1.Size = new System.Drawing.Size(232, 64);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 35);
			this.label2.Size = new System.Drawing.Size(80, 20);
			this.label2.Text = "Mottagare ID:";
			// 
			// receiverBox
			// 
			this.receiverBox.Location = new System.Drawing.Point(88, 32);
			this.receiverBox.Size = new System.Drawing.Size(128, 20);
			this.receiverBox.Text = "";
			// 
			// hostBox
			// 
			this.hostBox.Location = new System.Drawing.Point(88, 8);
			this.hostBox.Size = new System.Drawing.Size(128, 20);
			this.hostBox.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 11);
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
			this.panel2.Location = new System.Drawing.Point(0, 96);
			this.panel2.Size = new System.Drawing.Size(232, 88);
			// 
			// passwordBox
			// 
			this.passwordBox.Location = new System.Drawing.Point(88, 56);
			this.passwordBox.PasswordChar = '*';
			this.passwordBox.Size = new System.Drawing.Size(128, 20);
			this.passwordBox.Text = "";
			// 
			// userIdBox
			// 
			this.userIdBox.Location = new System.Drawing.Point(88, 32);
			this.userIdBox.Size = new System.Drawing.Size(128, 20);
			this.userIdBox.Text = "";
			// 
			// agentIdBox
			// 
			this.agentIdBox.Location = new System.Drawing.Point(88, 8);
			this.agentIdBox.Size = new System.Drawing.Size(128, 20);
			this.agentIdBox.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 59);
			this.label5.Size = new System.Drawing.Size(64, 16);
			this.label5.Text = "Lösenord:";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 35);
			this.label4.Size = new System.Drawing.Size(88, 16);
			this.label4.Text = "Användar-ID:";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 11);
			this.label3.Size = new System.Drawing.Size(56, 16);
			this.label3.Text = "Agent ID:";
			// 
			// SynchSettings
			// 
			this.Controls.Add(this.label6);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.panel2);
			this.Menu = this.mainMenu1;
			this.Text = "Inställningar";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.SynchSettings_Closing);
			this.Load += new System.EventHandler(this.SynchSettings_Load);

		}
		#endregion

		private void SynchSettings_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			dataSetup.host = this.hostBox.Text;
			dataSetup.receiver = this.receiverBox.Text;

			agent.agentId = this.agentIdBox.Text;
			agent.userName = this.userIdBox.Text;
			agent.password = this.passwordBox.Text;


			dataSetup.save();
			agent.save();
		}

		private void SynchSettings_Load(object sender, System.EventArgs e)
		{
			dataSetup.refresh();
			agent.refresh();

			this.hostBox.Text = dataSetup.host;
			this.receiverBox.Text = dataSetup.receiver;

			this.agentIdBox.Text = agent.agentId;
			this.userIdBox.Text = agent.userName;
			this.passwordBox.Text = agent.password;

		}

		private void hostBox_GotFocus(object sender, System.EventArgs e)
		{
			this.inputPanel1.Enabled = true;
		}

		private void portBox_GotFocus(object sender, System.EventArgs e)
		{
			this.inputPanel1.Enabled = true;

		}

		private void agentIdBox_GotFocus(object sender, System.EventArgs e)
		{
			this.inputPanel1.Enabled = true;

		}

		private void userIdBox_GotFocus(object sender, System.EventArgs e)
		{
			this.inputPanel1.Enabled = true;

		}

		private void passwordBox_GotFocus(object sender, System.EventArgs e)
		{
			this.inputPanel1.Enabled = true;

		}

		private void textBox2_GotFocus(object sender, System.EventArgs e)
		{
			this.inputPanel1.Enabled = true;		
		}

		private void checkBox1_CheckStateChanged(object sender, System.EventArgs e)
		{
		
		}



	}
}
