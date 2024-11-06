using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SmartInventory
{
	/// <summary>
	/// Summary description for MaintMoveItem.
	/// </summary>
	public class MaintMoveItem : System.Windows.Forms.Form, Logger
	{
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TextBox sumQuantityBox;
		private System.Windows.Forms.TextBox descriptionBox;
		private System.Windows.Forms.TextBox itemNoBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox statusBox;
		private System.Windows.Forms.TextBox heIdBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.ComboBox binDepartment;
		private System.Windows.Forms.TextBox scanBinBox;
	
		private SmartDatabase smartDatabase;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox binCodeBox;
		private DataSetup dataSetup;

		private Status status;

		public MaintMoveItem(SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			dataSetup = new DataSetup(smartDatabase);
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
			this.button2 = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.panel3 = new System.Windows.Forms.Panel();
			this.label7 = new System.Windows.Forms.Label();
			this.binDepartment = new System.Windows.Forms.ComboBox();
			this.scanBinBox = new System.Windows.Forms.TextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.binCodeBox = new System.Windows.Forms.TextBox();
			this.statusBox = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.heIdBox = new System.Windows.Forms.TextBox();
			this.sumQuantityBox = new System.Windows.Forms.TextBox();
			this.descriptionBox = new System.Windows.Forms.TextBox();
			this.itemNoBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular);
			this.button2.Location = new System.Drawing.Point(128, 231);
			this.button2.Size = new System.Drawing.Size(104, 32);
			this.button2.Text = "Tillbaka";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular);
			this.label4.Location = new System.Drawing.Point(8, 23);
			this.label4.Size = new System.Drawing.Size(208, 20);
			this.label4.Text = "Uttag";
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label6.Location = new System.Drawing.Point(8, 7);
			this.label6.Size = new System.Drawing.Size(200, 20);
			this.label6.Text = "Lagervård - Omflyttning";
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.label7);
			this.panel3.Controls.Add(this.binDepartment);
			this.panel3.Controls.Add(this.scanBinBox);
			this.panel3.Location = new System.Drawing.Point(0, 40);
			this.panel3.Size = new System.Drawing.Size(240, 56);
			// 
			// label7
			// 
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label7.Location = new System.Drawing.Point(8, 8);
			this.label7.Size = new System.Drawing.Size(112, 20);
			this.label7.Text = "Scanna lagerplats:";
			// 
			// binDepartment
			// 
			this.binDepartment.Location = new System.Drawing.Point(168, 32);
			this.binDepartment.Size = new System.Drawing.Size(64, 21);
			this.binDepartment.SelectedValueChanged += new System.EventHandler(this.binDepartment_SelectedValueChanged);
			// 
			// scanBinBox
			// 
			this.scanBinBox.Location = new System.Drawing.Point(8, 32);
			this.scanBinBox.Size = new System.Drawing.Size(152, 20);
			this.scanBinBox.Text = "";
			this.scanBinBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.scanBinBox_KeyPress);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.listBox1);
			this.panel1.Controls.Add(this.binCodeBox);
			this.panel1.Controls.Add(this.statusBox);
			this.panel1.Controls.Add(this.label8);
			this.panel1.Controls.Add(this.heIdBox);
			this.panel1.Controls.Add(this.sumQuantityBox);
			this.panel1.Controls.Add(this.descriptionBox);
			this.panel1.Controls.Add(this.itemNoBox);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.label5);
			this.panel1.Controls.Add(this.label9);
			this.panel1.Location = new System.Drawing.Point(0, 96);
			this.panel1.Size = new System.Drawing.Size(240, 128);
			// 
			// listBox1
			// 
			this.listBox1.Location = new System.Drawing.Point(8, 24);
			this.listBox1.Size = new System.Drawing.Size(224, 119);
			this.listBox1.Visible = false;
			// 
			// binCodeBox
			// 
			this.binCodeBox.Location = new System.Drawing.Point(8, 24);
			this.binCodeBox.ReadOnly = true;
			this.binCodeBox.Size = new System.Drawing.Size(104, 20);
			this.binCodeBox.Text = "";
			// 
			// statusBox
			// 
			this.statusBox.Location = new System.Drawing.Point(128, 104);
			this.statusBox.ReadOnly = true;
			this.statusBox.Size = new System.Drawing.Size(104, 20);
			this.statusBox.Text = "";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(128, 88);
			this.label8.Size = new System.Drawing.Size(64, 20);
			this.label8.Text = "Status:";
			// 
			// heIdBox
			// 
			this.heIdBox.Location = new System.Drawing.Point(8, 104);
			this.heIdBox.ReadOnly = true;
			this.heIdBox.Size = new System.Drawing.Size(104, 20);
			this.heIdBox.Text = "";
			// 
			// sumQuantityBox
			// 
			this.sumQuantityBox.Location = new System.Drawing.Point(128, 64);
			this.sumQuantityBox.ReadOnly = true;
			this.sumQuantityBox.Size = new System.Drawing.Size(104, 20);
			this.sumQuantityBox.Text = "";
			// 
			// descriptionBox
			// 
			this.descriptionBox.Location = new System.Drawing.Point(8, 64);
			this.descriptionBox.ReadOnly = true;
			this.descriptionBox.Size = new System.Drawing.Size(104, 20);
			this.descriptionBox.Text = "";
			this.descriptionBox.TextChanged += new System.EventHandler(this.descriptionBox_TextChanged);
			// 
			// itemNoBox
			// 
			this.itemNoBox.Location = new System.Drawing.Point(128, 24);
			this.itemNoBox.ReadOnly = true;
			this.itemNoBox.Size = new System.Drawing.Size(104, 20);
			this.itemNoBox.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(128, 48);
			this.label3.Size = new System.Drawing.Size(72, 20);
			this.label3.Text = "Saldo:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(128, 8);
			this.label2.Size = new System.Drawing.Size(72, 20);
			this.label2.Text = "Artikel:";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 88);
			this.label1.Size = new System.Drawing.Size(64, 20);
			this.label1.Text = "HE ID:";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 48);
			this.label5.Size = new System.Drawing.Size(64, 20);
			this.label5.Text = "Beskrivning:";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(8, 8);
			this.label9.Text = "Lagerplats:";
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular);
			this.button1.Location = new System.Drawing.Point(8, 232);
			this.button1.Size = new System.Drawing.Size(104, 32);
			this.button1.Text = "Tag ut";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// MaintMoveItem
			// 
			this.Controls.Add(this.button1);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.panel1);
			this.Text = "Lagervård - Omflyttning";
			this.Load += new System.EventHandler(this.MaintMoveItem_Load);

		}
		#endregion

		private void button2_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void descriptionBox_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private void MaintMoveItem_Load(object sender, System.EventArgs e)
		{
			scanBinBox.Focus();
		}

		private void scanBinBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == '>')
			{
				e.Handled = true;

				clearAllFields();

				DataBin dataBin = new DataBin(dataSetup.locationCode, scanBinBox.Text, smartDatabase);
				if (dataBin.exists)
				{
					if (dataBin.blocking == 1)
					{
						this.binCodeBox.Text = dataBin.code;
						this.statusBox.Text = "Spärrad";
						Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);
					}
					else
					{
						this.binCodeBox.Text = dataBin.code;

						requestStatus(dataBin);

					}

				}
				else
				{
					Sound sound = new Sound(Sound.SOUND_TYPE_ERROR);
				}
				this.scanBinBox.Text = "";
			}
		
		}

		private void requestStatus(DataBin dataBin)
		{
			listBox1.Visible = true;

			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
			System.Windows.Forms.Cursor.Show();

			button1.Enabled = false;
			button2.Enabled = false;
			Service synchService = new Service("statusRequest", smartDatabase);
			synchService.setLogger(this);

			synchService.serviceRequest.setServiceArgument(dataBin);

			ServiceResponse serviceResponse = synchService.performService();

			if (serviceResponse != null)
			{
				if (serviceResponse.hasErrors)
				{
					System.Windows.Forms.MessageBox.Show(serviceResponse.error.status+": "+serviceResponse.error.description, "Fel", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Hand, System.Windows.Forms.MessageBoxDefaultButton.Button1);
					write("Förfrågan misslyckades.");
				}
				else
				{
					write("Förfrågan klar.");
					listBox1.Visible = false;
					listBox1.Items.Clear();

					status = serviceResponse.status;

					if (status.binContentCollection.Count == 0)
					{
						System.Windows.Forms.MessageBox.Show("Lagerplats "+dataBin.code+" finns inte.");
					}
					else
					{

						if (status.binContentCollection.Count > 1)
						{
							int i = 0;
							while (i < status.binContentCollection.Count)
							{
								binDepartment.Items.Add(""+(i+1));
								i++;
							}
							binDepartment.SelectedIndex = 0;
						}
						else
						{
							DataBinContent binContent = (DataBinContent)status.binContentCollection[0];
							binCodeBox.Text = binContent.binCode;
							itemNoBox.Text = binContent.itemNo;
							descriptionBox.Text = binContent.description;
							sumQuantityBox.Text = binContent.quantity.ToString();
							heIdBox.Text = binContent.handleUnit;

							DataWhseItemStore itemStore = new DataWhseItemStore(dataSetup.locationCode, binCodeBox.Text, smartDatabase);
							if (itemStore.exists)
							{
								statusBox.Text = "Uttagen - Flytt";
								button1.Enabled = false;
							}
							else
							{
								if (binContent.status != "")
								{
									statusBox.Text = binContent.status;
									button1.Enabled = false;
								}
								else
								{
									statusBox.Text = "Upptagen";
									button1.Enabled = true;
								}
							}
						}
					}
				}
			}
			else
			{
				write("Förfrågan misslyckades.");
			}
			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
			System.Windows.Forms.Cursor.Hide();

			button2.Enabled = true;

			this.scanBinBox.Focus();
	
		}

		private void binDepartment_SelectedValueChanged(object sender, System.EventArgs e)
		{
			DataBinContent currentBinContent = (DataBinContent)status.binContentCollection[binDepartment.SelectedIndex];
			
			this.binCodeBox.Text = currentBinContent.binCode;
			this.itemNoBox.Text = currentBinContent.itemNo;
			this.descriptionBox.Text = currentBinContent.description;
			this.sumQuantityBox.Text = currentBinContent.quantity.ToString();
			this.heIdBox.Text = currentBinContent.handleUnit;

			DataWhseItemStore itemStore = new DataWhseItemStore(dataSetup.locationCode, binCodeBox.Text, smartDatabase);
			if (itemStore.exists)
			{
				statusBox.Text = "Uttagen - Flytt";
				button1.Enabled = false;
			}
			else
			{
				if (currentBinContent.status != "")
				{
					statusBox.Text = currentBinContent.status;
					button1.Enabled = false;
				}
				else
				{
					statusBox.Text = "Upptagen";
					button1.Enabled = true;
				}
			}
		
		}
		#region Logger Members

		public void write(string message)
		{
			// TODO:  Add MaintMoveItem.write implementation
			listBox1.Items.Add(message);
			Application.DoEvents();
		}

		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			DataWhseItemStore itemStore = new DataWhseItemStore(dataSetup.locationCode, binCodeBox.Text, smartDatabase);
			itemStore.itemNo = itemNoBox.Text;
			itemStore.handleUnitId = heIdBox.Text;
			itemStore.quantity = float.Parse(sumQuantityBox.Text);
			itemStore.commit();

			Sound sound = new Sound(Sound.SOUND_TYPE_SUCCESS);

			clearAllFields();
			scanBinBox.Focus();

		}


		private void clearAllFields()
		{
			this.statusBox.Text = "";
			this.itemNoBox.Text = "";
			this.sumQuantityBox.Text = "";
			this.binCodeBox.Text = "";
			this.heIdBox.Text = "";
			this.descriptionBox.Text = "";
			this.binCodeBox.Text = "";

		}
	}
}
