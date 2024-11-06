using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SmartShipment
{
	/// <summary>
	/// Summary description for OrderHeader.
	/// </summary>
	public class OrderHeader : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox customerNoBox;
		private System.Windows.Forms.TextBox customerNameBox;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox contactNameBox;
		private System.Windows.Forms.TextBox phoneNoBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label mark;
		private System.Windows.Forms.TextBox goodsmarkBox;
		private System.Windows.Forms.TextBox referenceNoBox;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.MainMenu mainMenu1;
		private Microsoft.WindowsCE.Forms.InputPanel inputPanel1;
		private System.Windows.Forms.Button button2;

		private SmartDatabase smartDatabase;
		private DataSetup dataSetup;
		private System.Windows.Forms.TextBox scanBox;
		private System.Windows.Forms.Label label7;
		private DataSalesHeader dataSalesHeader;
	
		public OrderHeader(SmartDatabase smartDatabase)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			this.smartDatabase = smartDatabase;
			this.dataSetup = smartDatabase.getSetup();
			this.dataSalesHeader = new DataSalesHeader(smartDatabase);
			dataSalesHeader.save();

			update();
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.scanBox.Focus();
		}

		public OrderHeader(SmartDatabase smartDatabase, DataSalesHeader dataSalesHeader)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			this.smartDatabase = smartDatabase;
			this.dataSetup = smartDatabase.getSetup();
			this.dataSalesHeader = dataSalesHeader;

			update();
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.customerNoBox = new System.Windows.Forms.TextBox();
			this.customerNameBox = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.contactNameBox = new System.Windows.Forms.TextBox();
			this.phoneNoBox = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.mark = new System.Windows.Forms.Label();
			this.goodsmarkBox = new System.Windows.Forms.TextBox();
			this.referenceNoBox = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.inputPanel1 = new Microsoft.WindowsCE.Forms.InputPanel();
			this.scanBox = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
			this.label1.Location = new System.Drawing.Point(8, 0);
			this.label1.Size = new System.Drawing.Size(192, 24);
			this.label1.Text = "Order";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 64);
			this.label2.Size = new System.Drawing.Size(48, 20);
			this.label2.Text = "Kundnr";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(88, 64);
			this.label3.Text = "Kundnamn";
			// 
			// customerNoBox
			// 
			this.customerNoBox.Location = new System.Drawing.Point(8, 80);
			this.customerNoBox.ReadOnly = true;
			this.customerNoBox.Size = new System.Drawing.Size(72, 20);
			this.customerNoBox.Text = "";
			this.customerNoBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.customerNoBox_KeyPress);
			// 
			// customerNameBox
			// 
			this.customerNameBox.Location = new System.Drawing.Point(88, 80);
			this.customerNameBox.ReadOnly = true;
			this.customerNameBox.Size = new System.Drawing.Size(144, 20);
			this.customerNameBox.Text = "";
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular);
			this.button1.Location = new System.Drawing.Point(8, 104);
			this.button1.Size = new System.Drawing.Size(224, 32);
			this.button1.Text = "Välj kund";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 144);
			this.label4.Text = "Kontaktperson";
			// 
			// contactNameBox
			// 
			this.contactNameBox.Location = new System.Drawing.Point(8, 160);
			this.contactNameBox.Size = new System.Drawing.Size(120, 20);
			this.contactNameBox.Text = "";
			this.contactNameBox.LostFocus += new System.EventHandler(this.contactNameBox_LostFocus);
			this.contactNameBox.GotFocus += new System.EventHandler(this.contactNameBox_GotFocus);
			// 
			// phoneNoBox
			// 
			this.phoneNoBox.Location = new System.Drawing.Point(136, 160);
			this.phoneNoBox.Size = new System.Drawing.Size(96, 20);
			this.phoneNoBox.Text = "";
			this.phoneNoBox.LostFocus += new System.EventHandler(this.phoneNoBox_LostFocus);
			this.phoneNoBox.GotFocus += new System.EventHandler(this.phoneNoBox_GotFocus);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(136, 144);
			this.label5.Size = new System.Drawing.Size(64, 20);
			this.label5.Text = "Telefonnr";
			// 
			// mark
			// 
			this.mark.Location = new System.Drawing.Point(8, 184);
			this.mark.Text = "Godsmärke";
			// 
			// goodsmarkBox
			// 
			this.goodsmarkBox.Location = new System.Drawing.Point(8, 200);
			this.goodsmarkBox.Size = new System.Drawing.Size(120, 20);
			this.goodsmarkBox.Text = "";
			this.goodsmarkBox.LostFocus += new System.EventHandler(this.goodsmarkBox_LostFocus);
			this.goodsmarkBox.GotFocus += new System.EventHandler(this.goodsmarkBox_GotFocus);
			this.goodsmarkBox.TextChanged += new System.EventHandler(this.goodsmarkBox_TextChanged);
			// 
			// referenceNoBox
			// 
			this.referenceNoBox.Location = new System.Drawing.Point(136, 200);
			this.referenceNoBox.Size = new System.Drawing.Size(96, 20);
			this.referenceNoBox.Text = "";
			this.referenceNoBox.LostFocus += new System.EventHandler(this.referenceNoBox_LostFocus);
			this.referenceNoBox.GotFocus += new System.EventHandler(this.referenceNoBox_GotFocus);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(136, 184);
			this.label6.Text = "Referensnr";
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular);
			this.button2.Location = new System.Drawing.Point(128, 224);
			this.button2.Size = new System.Drawing.Size(104, 40);
			this.button2.Text = "Nästa";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular);
			this.button3.Location = new System.Drawing.Point(8, 224);
			this.button3.Size = new System.Drawing.Size(104, 40);
			this.button3.Text = "Avbryt";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// scanBox
			// 
			this.scanBox.Location = new System.Drawing.Point(8, 40);
			this.scanBox.Size = new System.Drawing.Size(224, 20);
			this.scanBox.Text = "";
			this.scanBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.scanBox_KeyPress);
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(8, 24);
			this.label7.Size = new System.Drawing.Size(48, 20);
			this.label7.Text = "Scanna";
			// 
			// OrderHeader
			// 
			this.Controls.Add(this.scanBox);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.referenceNoBox);
			this.Controls.Add(this.goodsmarkBox);
			this.Controls.Add(this.mark);
			this.Controls.Add(this.phoneNoBox);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.contactNameBox);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.customerNameBox);
			this.Controls.Add(this.customerNoBox);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label6);
			this.Menu = this.mainMenu1;
			this.Text = "Orderhuvud";

		}
		#endregion

		private void button2_Click(object sender, System.EventArgs e)
		{
			save();

			if ((dataSalesHeader.customerNo == "") || (dataSalesHeader.customerNo ==  null))
			{
				System.Windows.Forms.MessageBox.Show("Du måste välja kund.");
			}
			else
			{
				OrderLines orderLines = new OrderLines(dataSalesHeader, smartDatabase, dataSetup);
				orderLines.ShowDialog();

				if (orderLines.getStatus() == 1)
				{
					if (MessageBox.Show("Vill du skicka ordern?", "Klar?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
					{
						dataSalesHeader.ready = true;
						dataSalesHeader.save();

						Synchronize synch = new Synchronize(smartDatabase, dataSetup, "sendOrders");
						synch.ShowDialog();	
	
						synch.Dispose();
						orderLines.Dispose();
						this.Close();
					}
				}
			}
		}

		private void goodsmarkBox_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private void goodsmarkBox_GotFocus(object sender, System.EventArgs e)
		{
			inputPanel1.Enabled = true;
		}

		private void referenceNoBox_GotFocus(object sender, System.EventArgs e)
		{
			inputPanel1.Enabled = true;
		}

		private void goodsmarkBox_LostFocus(object sender, System.EventArgs e)
		{
			inputPanel1.Enabled = false;
		}

		private void referenceNoBox_LostFocus(object sender, System.EventArgs e)
		{
			inputPanel1.Enabled = false;
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			save();

			if (System.Windows.Forms.MessageBox.Show("Du har valt att avbryta. Vill du radera ordern?", "Avbryta", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
			{
				dataSalesHeader.delete();
			}
			this.Close();
		}

		private void contactNameBox_GotFocus(object sender, System.EventArgs e)
		{
			inputPanel1.Enabled = true;
		}

		private void phoneNoBox_GotFocus(object sender, System.EventArgs e)
		{
			inputPanel1.Enabled = true;
		}

		private void contactNameBox_LostFocus(object sender, System.EventArgs e)
		{
			inputPanel1.Enabled = false;
		}

		private void phoneNoBox_LostFocus(object sender, System.EventArgs e)
		{
			inputPanel1.Enabled = false;

		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			CustomerList customerList = new CustomerList(smartDatabase);
			customerList.ShowDialog();
			DataCustomer dataCustomer = customerList.getCustomer();
			if (dataCustomer != null)
			{
				dataSalesHeader.customerNo = dataCustomer.no;
				dataSalesHeader.name = dataCustomer.name;
				dataSalesHeader.address = dataCustomer.address;
				dataSalesHeader.address2 = dataCustomer.address2;
				dataSalesHeader.city = dataCustomer.city;
				dataSalesHeader.zipCode = dataCustomer.zipCode;
				dataSalesHeader.contact = dataCustomer.contact;
				dataSalesHeader.phoneNo = dataCustomer.phoneNo;

				dataSalesHeader.deliveryName = dataCustomer.name;
				dataSalesHeader.deliveryAddress = dataCustomer.address;
				dataSalesHeader.deliveryAddress2 = dataCustomer.address2;
				dataSalesHeader.deliveryCity = dataCustomer.city;
				dataSalesHeader.deliveryZipCode = dataCustomer.zipCode;

				dataSalesHeader.save();
				update();
			}

			customerList.Dispose();
		}

		private void update()
		{
			Agent agent = dataSetup.getAgent();
			this.label1.Text = "Order "+agent.agentId+dataSalesHeader.no.ToString();
			this.Text = label1.Text;
			this.customerNoBox.Text = dataSalesHeader.customerNo;
			this.customerNameBox.Text = dataSalesHeader.name;
			this.contactNameBox.Text = dataSalesHeader.contact;
			this.phoneNoBox.Text = dataSalesHeader.phoneNo;

		}

		private void save()
		{
			dataSalesHeader.contact = this.contactNameBox.Text;
			dataSalesHeader.phoneNo = this.phoneNoBox.Text;
			dataSalesHeader.noteOfGoods = this.goodsmarkBox.Text;
			dataSalesHeader.customerReferenceNo = this.referenceNoBox.Text;
			dataSalesHeader.save();
		}

		private void customerNoBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{


		}

		private void scanBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if ((e.KeyChar == 13) || (e.KeyChar == '>'))
			{
				e.Handled = true;
				DataCustomer dataCustomer = new DataCustomer(scanBox.Text, smartDatabase);

				if (dataCustomer.name != "")
				{
					Sound sound = new Sound(Sound.SOUND_TYPE_SUCCESS);

					dataSalesHeader.customerNo = dataCustomer.no;
					dataSalesHeader.name = dataCustomer.name;
					dataSalesHeader.address = dataCustomer.address;
					dataSalesHeader.address2 = dataCustomer.address2;
					dataSalesHeader.city = dataCustomer.city;
					dataSalesHeader.zipCode = dataCustomer.zipCode;
					dataSalesHeader.contact = dataCustomer.contact;
					dataSalesHeader.phoneNo = dataCustomer.phoneNo;

					dataSalesHeader.deliveryName = dataCustomer.name;
					dataSalesHeader.deliveryAddress = dataCustomer.address;
					dataSalesHeader.deliveryAddress2 = dataCustomer.address2;
					dataSalesHeader.deliveryCity = dataCustomer.city;
					dataSalesHeader.deliveryZipCode = dataCustomer.zipCode;

					dataSalesHeader.save();
					update();

				}
				else
				{
					Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);

				}

				e.Handled = true;
				this.scanBox.Text = "";
				this.scanBox.Focus();
			}
		}
	}
}
