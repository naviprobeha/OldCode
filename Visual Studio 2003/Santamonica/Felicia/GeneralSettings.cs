using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for GeneralSettings.
	/// </summary>
	public class GeneralSettings : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox agentIdBox;
		private System.Windows.Forms.TextBox serverNameBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.MainMenu mainMenu1;
		private Microsoft.WindowsCE.Forms.InputPanel inputPanel1;
	
		private SmartDatabase smartDatabase;
		private System.Windows.Forms.Label statusLabel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox synchIntervalBox;
		private System.Windows.Forms.Label label4;
		private DataSetup dataSetup;

		public GeneralSettings(SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
			this.dataSetup = smartDatabase.getSetup();

			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			this.smartDatabase = smartDatabase;
			this.dataSetup = smartDatabase.getSetup();
			this.serverNameBox.Text = dataSetup.host;
			this.agentIdBox.Text = dataSetup.agentId;
			this.synchIntervalBox.Text = dataSetup.synchInterval.ToString();

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
			this.agentIdBox = new System.Windows.Forms.TextBox();
			this.serverNameBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.inputPanel1 = new Microsoft.WindowsCE.Forms.InputPanel();
			this.statusLabel = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.synchIntervalBox = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			// 
			// agentIdBox
			// 
			this.agentIdBox.Location = new System.Drawing.Point(8, 96);
			this.agentIdBox.ReadOnly = true;
			this.agentIdBox.Size = new System.Drawing.Size(224, 20);
			this.agentIdBox.Text = "";
			this.agentIdBox.LostFocus += new System.EventHandler(this.agentIdBox_LostFocus);
			this.agentIdBox.GotFocus += new System.EventHandler(this.agentIdBox_GotFocus);
			// 
			// serverNameBox
			// 
			this.serverNameBox.Location = new System.Drawing.Point(8, 48);
			this.serverNameBox.ReadOnly = true;
			this.serverNameBox.Size = new System.Drawing.Size(224, 20);
			this.serverNameBox.Text = "";
			this.serverNameBox.LostFocus += new System.EventHandler(this.serverNameBox_LostFocus);
			this.serverNameBox.GotFocus += new System.EventHandler(this.serverNameBox_GotFocus);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 32);
			this.label2.Size = new System.Drawing.Size(72, 20);
			this.label2.Text = "Servernamn:";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 80);
			this.label3.Size = new System.Drawing.Size(56, 20);
			this.label3.Text = "Agent-ID:";
			// 
			// statusLabel
			// 
			this.statusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.statusLabel.Location = new System.Drawing.Point(5, 3);
			this.statusLabel.Size = new System.Drawing.Size(208, 20);
			this.statusLabel.Text = "Allmäna inställningar";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 128);
			this.label1.Size = new System.Drawing.Size(184, 20);
			this.label1.Text = "Synkroniseringsintervall:";
			// 
			// synchIntervalBox
			// 
			this.synchIntervalBox.Location = new System.Drawing.Point(8, 144);
			this.synchIntervalBox.ReadOnly = true;
			this.synchIntervalBox.Size = new System.Drawing.Size(120, 20);
			this.synchIntervalBox.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(136, 148);
			this.label4.Size = new System.Drawing.Size(56, 20);
			this.label4.Text = "minuter";
			// 
			// GeneralSettings
			// 
			this.ClientSize = new System.Drawing.Size(322, 216);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.synchIntervalBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.statusLabel);
			this.Controls.Add(this.agentIdBox);
			this.Controls.Add(this.serverNameBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label3);
			this.Menu = this.mainMenu1;
			this.Text = "Allmänna inställningar";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.GeneralSettings_Closing);

		}
		#endregion

		private void serverNameBox_GotFocus(object sender, System.EventArgs e)
		{
			inputPanel1.Enabled = true;
		}

		private void agentIdBox_GotFocus(object sender, System.EventArgs e)
		{
			inputPanel1.Enabled = true;
		}

		private void serverNameBox_LostFocus(object sender, System.EventArgs e)
		{
			inputPanel1.Enabled = false;
		}

		private void agentIdBox_LostFocus(object sender, System.EventArgs e)
		{
			inputPanel1.Enabled = false;
		}

		private void GeneralSettings_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			dataSetup.host = this.serverNameBox.Text;
			dataSetup.agentId = this.agentIdBox.Text;
			dataSetup.synchInterval = int.Parse(this.synchIntervalBox.Text);
			dataSetup.save();
		}
	}
}
