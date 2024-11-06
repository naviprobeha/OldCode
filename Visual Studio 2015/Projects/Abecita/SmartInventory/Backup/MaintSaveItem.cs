using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SmartInventory
{
	/// <summary>
	/// Summary description for MaintSaveItem.
	/// </summary>
	public class MaintSaveItem : System.Windows.Forms.Form, Logger
	{
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox sumQuantityBox;
		private System.Windows.Forms.TextBox itemNoBox;
		private System.Windows.Forms.TextBox handleUnitIdBox;
	
		private SmartDatabase smartDatabase;
		private System.Windows.Forms.TextBox scanBinBox;
		private System.Windows.Forms.ListBox listBox1;
		private DataWhseItemStore dataWhseItemStore;

		public MaintSaveItem(DataWhseItemStore dataWhseItemStore, SmartDatabase smartDatabase)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.dataWhseItemStore = dataWhseItemStore;
			this.smartDatabase = smartDatabase;
			this.handleUnitIdBox.Text = dataWhseItemStore.handleUnitId;
			this.itemNoBox.Text = dataWhseItemStore.itemNo;
			this.sumQuantityBox.Text = dataWhseItemStore.quantity.ToString();
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			listBox1.Visible = false;
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
			this.scanBinBox = new System.Windows.Forms.TextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.sumQuantityBox = new System.Windows.Forms.TextBox();
			this.itemNoBox = new System.Windows.Forms.TextBox();
			this.handleUnitIdBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.listBox1 = new System.Windows.Forms.ListBox();
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
			this.label4.Text = "Inlagring";
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
			this.panel3.Controls.Add(this.label5);
			this.panel3.Controls.Add(this.scanBinBox);
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
			// scanBinBox
			// 
			this.scanBinBox.Location = new System.Drawing.Point(16, 56);
			this.scanBinBox.Size = new System.Drawing.Size(208, 20);
			this.scanBinBox.Text = "";
			this.scanBinBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.scanBinBox_KeyPress);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.listBox1);
			this.panel1.Controls.Add(this.sumQuantityBox);
			this.panel1.Controls.Add(this.itemNoBox);
			this.panel1.Controls.Add(this.handleUnitIdBox);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Location = new System.Drawing.Point(0, 135);
			this.panel1.Size = new System.Drawing.Size(240, 88);
			// 
			// sumQuantityBox
			// 
			this.sumQuantityBox.Location = new System.Drawing.Point(88, 56);
			this.sumQuantityBox.ReadOnly = true;
			this.sumQuantityBox.Size = new System.Drawing.Size(136, 20);
			this.sumQuantityBox.Text = "";
			// 
			// itemNoBox
			// 
			this.itemNoBox.Location = new System.Drawing.Point(88, 32);
			this.itemNoBox.ReadOnly = true;
			this.itemNoBox.Size = new System.Drawing.Size(136, 20);
			this.itemNoBox.Text = "";
			// 
			// handleUnitIdBox
			// 
			this.handleUnitIdBox.Location = new System.Drawing.Point(88, 8);
			this.handleUnitIdBox.ReadOnly = true;
			this.handleUnitIdBox.Size = new System.Drawing.Size(136, 20);
			this.handleUnitIdBox.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 59);
			this.label3.Size = new System.Drawing.Size(72, 20);
			this.label3.Text = "Antal:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 35);
			this.label2.Size = new System.Drawing.Size(72, 20);
			this.label2.Text = "Artikel:";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 11);
			this.label1.Size = new System.Drawing.Size(72, 20);
			this.label1.Text = "HE ID:";
			// 
			// listBox1
			// 
			this.listBox1.Location = new System.Drawing.Point(16, 8);
			this.listBox1.Size = new System.Drawing.Size(208, 80);
			// 
			// MaintSaveItem
			// 
			this.Controls.Add(this.button2);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.panel1);
			this.Text = "Lagervård - Omflyttning";
			this.Load += new System.EventHandler(this.MaintSaveItem_Load);

		}
		#endregion

		private void button2_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void MaintSaveItem_Load(object sender, System.EventArgs e)
		{
			this.scanBinBox.Focus();
		}

		private void scanBinBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == '>')
			{
				e.Handled = true;

				dataWhseItemStore.toBinCode = scanBinBox.Text;
				dataWhseItemStore.commit();

				
				if (performTransfer(dataWhseItemStore))
				{
					Sound sound = new Sound(Sound.SOUND_TYPE_SUCCESS);

					dataWhseItemStore.delete();
					this.Close();
						
				}
				else
				{
					dataWhseItemStore.toBinCode = "";
					dataWhseItemStore.commit();

					Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);

					this.scanBinBox.Text = "";
					this.scanBinBox.Focus();
				}
			}
		
		}
		#region Logger Members

		public void write(string message)
		{
			// TODO:  Add MaintSaveItem.write implementation
			listBox1.Items.Add(message);
			Application.DoEvents();
		}

		#endregion

		private bool performTransfer(DataWhseItemStore dataWhseItemStore)
		{
			listBox1.Visible = true;
			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
			System.Windows.Forms.Cursor.Show();

			Service synchService = new Service("moveHandleUnit", smartDatabase);
			synchService.setLogger(this);

			synchService.serviceRequest.setServiceArgument(dataWhseItemStore);

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
					System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
					System.Windows.Forms.Cursor.Hide();
					return true;
				}
			}
			else
			{
				write("Förfrågan misslyckades.");
			}

			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
			System.Windows.Forms.Cursor.Hide();
			return false;

		}
	}
}
