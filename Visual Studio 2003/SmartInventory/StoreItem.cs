using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SmartInventory
{
	/// <summary>
	/// Summary description for StoreForm.
	/// </summary>
	public class StoreForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox itemNoBox;
		private System.Windows.Forms.TextBox sumQuantityBox;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.TextBox packageScanBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label3;
	
		private SmartDatabase smartDatabase;
		private System.Windows.Forms.TextBox statusBox;
		private int status;
		private System.Windows.Forms.TextBox binCodeBox;
		private System.Windows.Forms.Label label7;
		private DataBin selectedBin;
		private System.Windows.Forms.TextBox scanBinBox;
		private System.Windows.Forms.Button button3;
		private DataSetup dataSetup;


		public StoreForm(SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
			this.dataSetup = new DataSetup(smartDatabase);
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//


			this.scanBinBox.Focus();
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.binCodeBox = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.statusBox = new System.Windows.Forms.TextBox();
			this.sumQuantityBox = new System.Windows.Forms.TextBox();
			this.itemNoBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.button3 = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.packageScanBox = new System.Windows.Forms.TextBox();
			this.panel3 = new System.Windows.Forms.Panel();
			this.label5 = new System.Windows.Forms.Label();
			this.scanBinBox = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.binCodeBox);
			this.panel1.Controls.Add(this.label7);
			this.panel1.Controls.Add(this.statusBox);
			this.panel1.Controls.Add(this.sumQuantityBox);
			this.panel1.Controls.Add(this.itemNoBox);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Location = new System.Drawing.Point(0, 88);
			this.panel1.Size = new System.Drawing.Size(240, 80);
			// 
			// binCodeBox
			// 
			this.binCodeBox.Location = new System.Drawing.Point(16, 16);
			this.binCodeBox.ReadOnly = true;
			this.binCodeBox.Size = new System.Drawing.Size(96, 20);
			this.binCodeBox.Text = "";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(16, 0);
			this.label7.Text = "Lagerplats:";
			// 
			// statusBox
			// 
			this.statusBox.Location = new System.Drawing.Point(128, 56);
			this.statusBox.ReadOnly = true;
			this.statusBox.Size = new System.Drawing.Size(96, 20);
			this.statusBox.Text = "";
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
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(128, 40);
			this.label3.Size = new System.Drawing.Size(72, 20);
			this.label3.Text = "Status lpl:";
			this.label3.ParentChanged += new System.EventHandler(this.label3_ParentChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 40);
			this.label2.Size = new System.Drawing.Size(72, 20);
			this.label2.Text = "Saldo:";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(128, 0);
			this.label1.Size = new System.Drawing.Size(72, 20);
			this.label1.Text = "Artikelnr:";
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.button3);
			this.panel2.Controls.Add(this.label4);
			this.panel2.Controls.Add(this.packageScanBox);
			this.panel2.Location = new System.Drawing.Point(0, 168);
			this.panel2.Size = new System.Drawing.Size(240, 56);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(160, 17);
			this.button3.Size = new System.Drawing.Size(64, 32);
			this.button3.Text = "Ändra";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label4.Location = new System.Drawing.Point(16, 8);
			this.label4.Size = new System.Drawing.Size(128, 20);
			this.label4.Text = "Scanna kolli ID:";
			// 
			// packageScanBox
			// 
			this.packageScanBox.Location = new System.Drawing.Point(16, 29);
			this.packageScanBox.Size = new System.Drawing.Size(136, 20);
			this.packageScanBox.Text = "";
			this.packageScanBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.packageScanBox_KeyPress);
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.label5);
			this.panel3.Controls.Add(this.scanBinBox);
			this.panel3.Location = new System.Drawing.Point(0, 32);
			this.panel3.Size = new System.Drawing.Size(240, 56);
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label5.Location = new System.Drawing.Point(16, 8);
			this.label5.Size = new System.Drawing.Size(208, 20);
			this.label5.Text = "Scanna lagerplats:";
			// 
			// scanBinBox
			// 
			this.scanBinBox.Location = new System.Drawing.Point(16, 29);
			this.scanBinBox.Size = new System.Drawing.Size(208, 20);
			this.scanBinBox.Text = "";
			this.scanBinBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.scanLocationBox_KeyPress);
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label6.Location = new System.Drawing.Point(8, 8);
			this.label6.Size = new System.Drawing.Size(200, 20);
			this.label6.Text = "Inlagring";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(16, 224);
			this.button1.Size = new System.Drawing.Size(96, 32);
			this.button1.Text = "Tillbaka";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(128, 224);
			this.button2.Size = new System.Drawing.Size(96, 32);
			this.button2.Text = "Huvudmeny";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// StoreForm
			// 
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.panel2);
			this.Text = "Inlagring";

		}
		#endregion

		private void label3_ParentChanged(object sender, System.EventArgs e)
		{
		
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			status = 1;
			this.Close();
		}

		public int getStatus()
		{
			return status;
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			status = 2;
			this.Close();
		}


		private void scanLocationBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == '>')
			{
				e.Handled = true;

				this.statusBox.Text = "";
				this.itemNoBox.Text = "";
				this.sumQuantityBox.Text = "";
				this.binCodeBox.Text = "";

				DataBin dataBin = new DataBin(dataSetup.locationCode, scanBinBox.Text, smartDatabase);
				if (dataBin.exists)
				{
					if (dataBin.blocking == 1)
					{
						this.binCodeBox.Text = dataBin.code;
						this.statusBox.Text = "Spärrad";
						Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);
						this.scanBinBox.Text = "";
					}
					else
					{
						this.binCodeBox.Text = dataBin.code;
						this.scanBinBox.Text = "";

						DataWhseActivityLine dataWhseActivityLine = new DataWhseActivityLine(dataBin, smartDatabase);
						if ((!dataBin.inComplete) && (dataWhseActivityLine.exists))
						{
							this.itemNoBox.Text = dataWhseActivityLine.itemNo;
							this.sumQuantityBox.Text = dataWhseActivityLine.quantity.ToString();
							this.statusBox.Text = "Upptagen";
							Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);
						}
						else
						{
							if (!dataBin.empty)
							{
								this.statusBox.Text = "Upptagen";
								Sound sound = new Sound(Sound.SOUND_TYPE_ERROR);
							}					
							else
							{
								selectedBin = dataBin;
								this.packageScanBox.Focus();
								Sound sound = new Sound(Sound.SOUND_TYPE_OK);

							}
						}
					}

				}
				else
				{
					Sound sound = new Sound(Sound.SOUND_TYPE_ERROR);
					this.scanBinBox.Text = "";
				}
			}
		
		}

		private void packageScanBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == '>')
			{
				e.Handled = true;

				if (selectedBin != null)
				{
					DataWhseActivityLine dataWhseActivityLine = new DataWhseActivityLine(packageScanBox.Text, smartDatabase);
                    if (dataWhseActivityLine.exists)
					{
						dataWhseActivityLine.binCode = selectedBin.code;
						dataWhseActivityLine.locationCode = selectedBin.locationCode;
						dataWhseActivityLine.commit();

						//selectedBin.empty = 0;
						//selectedBin.commit();

						Sound sound = new Sound(Sound.SOUND_TYPE_SUCCESS);

						this.binCodeBox.Text = selectedBin.code;
						this.itemNoBox.Text = dataWhseActivityLine.itemNo;
						this.sumQuantityBox.Text = dataWhseActivityLine.quantity.ToString();
						this.statusBox.Text = "Registrerad";

						this.scanBinBox.Text = "";
						this.packageScanBox.Text = "";
						this.scanBinBox.Focus();
					}
					else
					{
						this.packageScanBox.Text = "";
						Sound sound = new Sound(Sound.SOUND_TYPE_ERROR);
						this.scanBinBox.Focus();
					}

				}
				else
				{
					Sound sound = new Sound(Sound.SOUND_TYPE_ERROR);
					this.scanBinBox.Text = "";
					this.packageScanBox.Text = "";
					this.scanBinBox.Focus();
				}
			}
		
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			this.packageScanBox.Focus();
		}
	}
}
