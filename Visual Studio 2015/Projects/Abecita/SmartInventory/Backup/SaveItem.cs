using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SmartInventory
{
	/// <summary>
	/// Summary description for SaveItem.
	/// </summary>
	public class SaveItem : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox scanLocationBox;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
	
		private SmartDatabase smartDatabase;
		private int freq;
		private System.Windows.Forms.TextBox sumQuantityBox;
		private System.Windows.Forms.TextBox itemNoBox;
		private System.Windows.Forms.TextBox heIdBox;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox statusBox;
		private DataWhseActivityLine dataWhseActivityLine;
		private DataSetup dataSetup;

		public SaveItem(SmartDatabase smartDatabase, int freq, DataWhseActivityLine dataWhseActivityLine)
		{
			this.smartDatabase = smartDatabase;
			this.freq = freq;
			this.dataSetup = new DataSetup(smartDatabase);

			this.dataWhseActivityLine = dataWhseActivityLine;
	
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			switch (freq)
			{
				case 1: 
				{
					label4.Text = "Frekvensklass: Högfrekvent";
					break;
				}
				case 2: 
				{
					label4.Text = "Frekvensklass: Mellanfrekvent";
					break;
				}
				case 3: 
				{
					label4.Text = "Frekvensklass: Lågfrekvent";
					break;
				}
				case 4: 
				{
					label4.Text = "Frekvensklass: Mkt lågfrekvent";
					break;
				}
				case 5: 
				{
					label4.Text = "Frekvensklass: Storvolymigt";
					break;
				}
			}


			this.heIdBox.Text = dataWhseActivityLine.handleUnitId;
			this.itemNoBox.Text = dataWhseActivityLine.itemNo;
			this.sumQuantityBox.Text = dataWhseActivityLine.quantity.ToString();

			this.scanLocationBox.Focus();
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
			this.button2 = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.panel3 = new System.Windows.Forms.Panel();
			this.label7 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.scanLocationBox = new System.Windows.Forms.TextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.sumQuantityBox = new System.Windows.Forms.TextBox();
			this.itemNoBox = new System.Windows.Forms.TextBox();
			this.heIdBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.statusBox = new System.Windows.Forms.TextBox();
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular);
			this.button2.Location = new System.Drawing.Point(8, 231);
			this.button2.Size = new System.Drawing.Size(224, 32);
			this.button2.Text = "Tillbaka";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular);
			this.label4.Location = new System.Drawing.Point(8, 23);
			this.label4.Size = new System.Drawing.Size(208, 20);
			this.label4.Text = "Frekvensklass:";
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label6.Location = new System.Drawing.Point(8, 7);
			this.label6.Size = new System.Drawing.Size(200, 20);
			this.label6.Text = "Inlagring - Batch:";
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.label7);
			this.panel3.Controls.Add(this.label5);
			this.panel3.Controls.Add(this.scanLocationBox);
			this.panel3.Location = new System.Drawing.Point(0, 47);
			this.panel3.Size = new System.Drawing.Size(240, 88);
			// 
			// label7
			// 
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label7.Location = new System.Drawing.Point(16, 32);
			this.label7.Size = new System.Drawing.Size(112, 20);
			this.label7.Text = "Scanna lagerplats:";
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
			this.label5.Location = new System.Drawing.Point(16, 8);
			this.label5.Size = new System.Drawing.Size(208, 20);
			this.label5.Text = "Lämnas till";
			// 
			// scanLocationBox
			// 
			this.scanLocationBox.Location = new System.Drawing.Point(16, 56);
			this.scanLocationBox.Size = new System.Drawing.Size(208, 20);
			this.scanLocationBox.Text = "";
			this.scanLocationBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.scanLocationBox_KeyPress);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.statusBox);
			this.panel1.Controls.Add(this.label8);
			this.panel1.Controls.Add(this.sumQuantityBox);
			this.panel1.Controls.Add(this.itemNoBox);
			this.panel1.Controls.Add(this.heIdBox);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Location = new System.Drawing.Point(0, 135);
			this.panel1.Size = new System.Drawing.Size(240, 88);
			// 
			// sumQuantityBox
			// 
			this.sumQuantityBox.Location = new System.Drawing.Point(16, 56);
			this.sumQuantityBox.ReadOnly = true;
			this.sumQuantityBox.Size = new System.Drawing.Size(96, 20);
			this.sumQuantityBox.Text = "";
			// 
			// itemNoBox
			// 
			this.itemNoBox.Location = new System.Drawing.Point(128, 16);
			this.itemNoBox.ReadOnly = true;
			this.itemNoBox.Size = new System.Drawing.Size(96, 20);
			this.itemNoBox.Text = "";
			// 
			// heIdBox
			// 
			this.heIdBox.Location = new System.Drawing.Point(16, 16);
			this.heIdBox.ReadOnly = true;
			this.heIdBox.Size = new System.Drawing.Size(96, 20);
			this.heIdBox.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 40);
			this.label3.Size = new System.Drawing.Size(72, 20);
			this.label3.Text = "Antal:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(128, 0);
			this.label2.Size = new System.Drawing.Size(72, 20);
			this.label2.Text = "Artikel:";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 0);
			this.label1.Size = new System.Drawing.Size(72, 20);
			this.label1.Text = "HE ID:";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(128, 40);
			this.label8.Text = "Status lpl:";
			// 
			// statusBox
			// 
			this.statusBox.Location = new System.Drawing.Point(128, 56);
			this.statusBox.ReadOnly = true;
			this.statusBox.Size = new System.Drawing.Size(96, 20);
			this.statusBox.Text = "";
			// 
			// SaveItem
			// 
			this.Controls.Add(this.button2);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.panel1);
			this.Text = "Påfyllning - Inlagring";

		}
		#endregion

		private void button2_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void scanLocationBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == '>')
			{
				e.Handled = true;

				DataBin dataBin = new DataBin(dataSetup.locationCode, scanLocationBox.Text, smartDatabase);
				if (dataBin.exists)
				{
					if (dataBin.blocking == 1)
					{
						//this.binCodeBox.Text = dataBin.code;
						this.statusBox.Text = "Spärrad";
						Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);
						this.scanLocationBox.Text = "";
					}
					else
					{
						//this.binCodeBox.Text = dataBin.code;
						this.scanLocationBox.Text = "";

						if (!dataBin.empty)
						{
							this.statusBox.Text = "Upptagen";
							Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);
						}					
						else
						{
							this.dataWhseActivityLine.binCode = dataBin.code;
							this.dataWhseActivityLine.commit();
							Sound sound = new Sound(Sound.SOUND_TYPE_SUCCESS);
							this.Close();
						}
					}

				}
				else
				{
					Sound sound = new Sound(Sound.SOUND_TYPE_ERROR);
					this.scanLocationBox.Text = "";
				}

			}

		}
	}
}
