using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for GpsInfo.
	/// </summary>
	public class GpsInfo : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListBox gpsList;
		private System.Windows.Forms.Button pauseButton;
	
		private bool run;
		private System.Windows.Forms.TextBox lastUpdatedBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label statusLabel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;

		private Status status;
		private GpsComm gpsComm;
		private System.Windows.Forms.ListBox rt90yBox;
		private System.Windows.Forms.ListBox rt90xBox;
		private System.Windows.Forms.ListBox speedBox;

		public GpsInfo(Status status, GpsComm gpsComm)
		{
			this.status = status;
			this.gpsComm = gpsComm;
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			gpsComm.onDataUpdate +=new Navipro.SantaMonica.Felicia.GpsComm.dataUpdateEventHandler(gpsComm_onDataUpdate);
			gpsComm.onHeadingUpdate +=new Navipro.SantaMonica.Felicia.GpsComm.headingUpdateEventHandler(gpsComm_onHeadingUpdate);
			gpsComm.onPositionUpdate +=new Navipro.SantaMonica.Felicia.GpsComm.positionUpdateEventHandler(gpsComm_onPositionUpdate);

			run = true;

	
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
			this.gpsList = new System.Windows.Forms.ListBox();
			this.pauseButton = new System.Windows.Forms.Button();
			this.lastUpdatedBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.statusLabel = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.rt90yBox = new System.Windows.Forms.ListBox();
			this.rt90xBox = new System.Windows.Forms.ListBox();
			this.speedBox = new System.Windows.Forms.ListBox();
			// 
			// gpsList
			// 
			this.gpsList.Location = new System.Drawing.Point(128, 32);
			this.gpsList.Size = new System.Drawing.Size(184, 132);
			// 
			// pauseButton
			// 
			this.pauseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.pauseButton.Location = new System.Drawing.Point(240, 176);
			this.pauseButton.Size = new System.Drawing.Size(72, 32);
			this.pauseButton.Text = "Paus";
			this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
			// 
			// lastUpdatedBox
			// 
			this.lastUpdatedBox.Location = new System.Drawing.Point(8, 168);
			this.lastUpdatedBox.ReadOnly = true;
			this.lastUpdatedBox.Size = new System.Drawing.Size(112, 20);
			this.lastUpdatedBox.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 152);
			this.label2.Size = new System.Drawing.Size(80, 20);
			this.label2.Text = "Uppdaterad:";
			// 
			// statusLabel
			// 
			this.statusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.statusLabel.Location = new System.Drawing.Point(5, 3);
			this.statusLabel.Size = new System.Drawing.Size(208, 20);
			this.statusLabel.Text = "GPS Info";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 32);
			this.label1.Text = "X";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 72);
			this.label3.Text = "Y";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 112);
			this.label4.Text = "Hastighet";
			// 
			// rt90yBox
			// 
			this.rt90yBox.Location = new System.Drawing.Point(8, 48);
			this.rt90yBox.Size = new System.Drawing.Size(112, 28);
			// 
			// rt90xBox
			// 
			this.rt90xBox.Location = new System.Drawing.Point(8, 88);
			this.rt90xBox.Size = new System.Drawing.Size(112, 28);
			// 
			// speedBox
			// 
			this.speedBox.Location = new System.Drawing.Point(8, 128);
			this.speedBox.Size = new System.Drawing.Size(112, 28);
			// 
			// GpsInfo
			// 
			this.ClientSize = new System.Drawing.Size(322, 216);
			this.Controls.Add(this.speedBox);
			this.Controls.Add(this.rt90xBox);
			this.Controls.Add(this.rt90yBox);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.statusLabel);
			this.Controls.Add(this.lastUpdatedBox);
			this.Controls.Add(this.pauseButton);
			this.Controls.Add(this.gpsList);
			this.Controls.Add(this.label2);
			this.Text = "GpsInfo";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.GpsInfo_Closing);

		}
		#endregion


		private void pauseButton_Click(object sender, System.EventArgs e)
		{
			if (run)
			{
				run = false;
				pauseButton.Text = "Start";
			}
			else
			{
				run = true;
				pauseButton.Text = "Paus";
			}
		}


		private void gpsComm_onDataUpdate(object sender, EventArgs e, string data)
		{
			if (run)
			{
				
				gpsList.Items.Add(data);
				if (gpsList.Items.Count > 30) gpsList.Items.RemoveAt(0);

			}

		}

		private void gpsComm_onHeadingUpdate(object sender, EventArgs e, int heading, int speed)
		{
			if (run)
			{

				speedBox.Items.Clear();
				speedBox.Items.Add(status.speed.ToString());

			}

		}

		private void gpsComm_onPositionUpdate(object sender, EventArgs e, int x, int y, int height)
		{
			if (run)
			{
				rt90yBox.Items.Clear();
				rt90yBox.Items.Add(status.rt90y.ToString());

				rt90xBox.Items.Clear();
				rt90xBox.Items.Add(status.rt90x.ToString());

			}
		}


		private void GpsInfo_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			gpsComm.onDataUpdate -=new Navipro.SantaMonica.Felicia.GpsComm.dataUpdateEventHandler(gpsComm_onDataUpdate);
			gpsComm.onHeadingUpdate -=new Navipro.SantaMonica.Felicia.GpsComm.headingUpdateEventHandler(gpsComm_onHeadingUpdate);
			gpsComm.onPositionUpdate -=new Navipro.SantaMonica.Felicia.GpsComm.positionUpdateEventHandler(gpsComm_onPositionUpdate);
						
		}
	}
}
