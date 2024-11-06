using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for ApplicationMode.
	/// </summary>
	public class ApplicationMode : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label orderNo3Label;
	
		private int mode;

		public ApplicationMode()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			mode = 0;
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
			this.orderNo3Label = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			// 
			// orderNo3Label
			// 
			this.orderNo3Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.orderNo3Label.Location = new System.Drawing.Point(5, 3);
			this.orderNo3Label.Size = new System.Drawing.Size(251, 20);
			this.orderNo3Label.Text = "Välj arbetssätt";
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular);
			this.button1.Location = new System.Drawing.Point(184, 32);
			this.button1.Size = new System.Drawing.Size(128, 80);
			this.button1.Text = "Uppsamling";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular);
			this.button2.Location = new System.Drawing.Point(184, 120);
			this.button2.Size = new System.Drawing.Size(128, 80);
			this.button2.Text = "Linjetrafik";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 32);
			this.label1.Size = new System.Drawing.Size(168, 56);
			this.label1.Text = "Välj arbetssätt genom att klicka på någon av knapparna till höger.";
			// 
			// ApplicationMode
			// 
			this.ClientSize = new System.Drawing.Size(322, 216);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.orderNo3Label);
			this.MinimizeBox = false;
			this.Text = "SmartOrder";

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			mode = 0;
			
			//startApplication();

			this.Close();
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			mode = 1;
			
			//startApplication();

			this.Close();
		}

		public int getMode()
		{
			return mode;
		}

		private void startApplication()
		{
			if (mode == 0)
			{
				Application.Run(new StartFormShip());
				
				//StartFormShip startForm = new StartFormShip();
				//startForm.ShowDialog();
				//startForm.Dispose();
				
			}

			if (mode == 1)
			{
				Application.Run(new StartFormLine());

				//StartFormLine startForm = new StartFormLine();
				//startForm.ShowDialog();
				//startForm.Dispose();
			}

		}
	}
}
