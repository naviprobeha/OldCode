using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for NavigatePosition.
	/// </summary>
	public class NavigatePosition : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.Label headerLabel;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox positionXBox;
		private System.Windows.Forms.TextBox positionYBox;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.TabPage tabPage1;

		private SmartDatabase smartDatabase;
	
		public NavigatePosition(SmartDatabase smartDatabase)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			this.smartDatabase = smartDatabase;
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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.headerLabel = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.positionXBox = new System.Windows.Forms.TextBox();
			this.positionYBox = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(322, 216);
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.button3);
			this.tabPage1.Controls.Add(this.button5);
			this.tabPage1.Controls.Add(this.button2);
			this.tabPage1.Controls.Add(this.button1);
			this.tabPage1.Controls.Add(this.label11);
			this.tabPage1.Controls.Add(this.positionXBox);
			this.tabPage1.Controls.Add(this.positionYBox);
			this.tabPage1.Controls.Add(this.label12);
			this.tabPage1.Controls.Add(this.headerLabel);
			this.tabPage1.Location = new System.Drawing.Point(4, 4);
			this.tabPage1.Size = new System.Drawing.Size(314, 190);
			this.tabPage1.Text = "Position";
			// 
			// headerLabel
			// 
			this.headerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.headerLabel.Location = new System.Drawing.Point(5, 3);
			this.headerLabel.Size = new System.Drawing.Size(219, 20);
			this.headerLabel.Text = "Navigera till position";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(9, 83);
			this.label11.Size = new System.Drawing.Size(64, 20);
			this.label11.Text = "Y:";
			// 
			// positionXBox
			// 
			this.positionXBox.Location = new System.Drawing.Point(81, 80);
			this.positionXBox.ReadOnly = true;
			this.positionXBox.Size = new System.Drawing.Size(151, 20);
			this.positionXBox.Text = "";
			// 
			// positionYBox
			// 
			this.positionYBox.Location = new System.Drawing.Point(81, 32);
			this.positionYBox.ReadOnly = true;
			this.positionYBox.Size = new System.Drawing.Size(151, 20);
			this.positionYBox.Text = "";
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(9, 35);
			this.label12.Size = new System.Drawing.Size(64, 20);
			this.label12.Text = "X:";
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button1.Location = new System.Drawing.Point(240, 32);
			this.button1.Size = new System.Drawing.Size(64, 32);
			this.button1.Text = "Ändra";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button2.Location = new System.Drawing.Point(240, 79);
			this.button2.Size = new System.Drawing.Size(64, 32);
			this.button2.Text = "Ändra";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button5
			// 
			this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button5.Location = new System.Drawing.Point(136, 152);
			this.button5.Size = new System.Drawing.Size(80, 32);
			this.button5.Text = "Navigera";
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// button3
			// 
			this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button3.Location = new System.Drawing.Point(224, 152);
			this.button3.Size = new System.Drawing.Size(80, 32);
			this.button3.Text = "Tillbaka";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// NavigatePosition
			// 
			this.ClientSize = new System.Drawing.Size(322, 216);
			this.Controls.Add(this.tabControl1);
			this.Text = "Navigera till position";

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			NumPad numPad = new NumPad();
			numPad.setInputString(positionYBox.Text);
			numPad.ShowDialog();
			
			if (numPad.getInputString() != "")
			{
				positionYBox.Text = numPad.getInputString();
				try
				{
					int.Parse(positionYBox.Text);
				}
				catch(Exception ex)
				{
					if (ex.Message != "") {}
					positionYBox.Text = "";
				}
			}
			
			numPad.Dispose();
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			NumPad numPad = new NumPad();
			numPad.setInputString(positionXBox.Text);
			numPad.ShowDialog();
			
			if (numPad.getInputString() != "")
			{
				positionXBox.Text = numPad.getInputString();
				try
				{
					int.Parse(positionXBox.Text);
				}
				catch(Exception ex)
				{
					if (ex.Message != "") {}
					positionXBox.Text = "";
				}
			}
			
			numPad.Dispose();

		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void button5_Click(object sender, System.EventArgs e)
		{
			Navigator navigator = new Navigator(smartDatabase);
			navigator.navigate(int.Parse(positionYBox.Text), int.Parse(positionXBox.Text), "vald XY-koordinat");

		}
	}
}
