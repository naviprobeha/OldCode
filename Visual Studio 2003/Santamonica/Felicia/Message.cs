using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for Message.
	/// </summary>
	public class Message : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label headerLabel3;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox nameBox;
		private System.Windows.Forms.Button button9;
		private System.Windows.Forms.TextBox messageBox;

		private SmartDatabase smartDatabase;
		private DataMessage dataMessage;
	
		public Message(SmartDatabase smartDatabase, DataMessage dataMessage)
		{
			this.smartDatabase = smartDatabase;
			this.dataMessage = dataMessage;
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			messageBox.Text = dataMessage.message;
			nameBox.Text = dataMessage.fromName;
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
			this.label12 = new System.Windows.Forms.Label();
			this.messageBox = new System.Windows.Forms.TextBox();
			this.headerLabel3 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.nameBox = new System.Windows.Forms.TextBox();
			this.button9 = new System.Windows.Forms.Button();
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(8, 72);
			this.label12.Size = new System.Drawing.Size(72, 20);
			this.label12.Text = "Meddelande:";
			// 
			// messageBox
			// 
			this.messageBox.Location = new System.Drawing.Point(8, 88);
			this.messageBox.Multiline = true;
			this.messageBox.ReadOnly = true;
			this.messageBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.messageBox.Size = new System.Drawing.Size(304, 80);
			this.messageBox.Text = "";
			// 
			// headerLabel3
			// 
			this.headerLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.headerLabel3.Location = new System.Drawing.Point(5, 3);
			this.headerLabel3.Size = new System.Drawing.Size(219, 20);
			this.headerLabel3.Text = "Meddelande";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 24);
			this.label3.Size = new System.Drawing.Size(64, 20);
			this.label3.Text = "Från:";
			// 
			// nameBox
			// 
			this.nameBox.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.nameBox.Location = new System.Drawing.Point(8, 40);
			this.nameBox.ReadOnly = true;
			this.nameBox.Size = new System.Drawing.Size(288, 20);
			this.nameBox.Text = "";
			// 
			// button9
			// 
			this.button9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button9.Location = new System.Drawing.Point(232, 176);
			this.button9.Size = new System.Drawing.Size(80, 32);
			this.button9.Text = "OK";
			this.button9.Click += new System.EventHandler(this.button9_Click);
			// 
			// Message
			// 
			this.ClientSize = new System.Drawing.Size(322, 216);
			this.Controls.Add(this.button9);
			this.Controls.Add(this.nameBox);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.headerLabel3);
			this.Controls.Add(this.messageBox);
			this.Controls.Add(this.label12);
			this.Text = "Message";

		}
		#endregion

		private void button9_Click(object sender, System.EventArgs e)
		{
			DataSyncActions dataSyncActions = new DataSyncActions(smartDatabase);
			dataSyncActions.addSyncAction(3, 0, dataMessage.entryNo.ToString());

			this.Close();
		}
	}
}
