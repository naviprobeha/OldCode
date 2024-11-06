using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for Order.
	/// </summary>
	public class Shipment : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.TextBox customerNoBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox postCodeBox;
		private System.Windows.Forms.TextBox cityBox;
		private System.Windows.Forms.TextBox addressBox;
		private System.Windows.Forms.TextBox nameBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Button button8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox paymentBox;
		private System.Windows.Forms.Button button9;
	
		private SmartDatabase smartDatabase;
		private System.Windows.Forms.Label orderNoLabel;
		private System.Windows.Forms.Label orderNo2Label;
		private System.Windows.Forms.DataGridTableStyle itemTable;
		private System.Windows.Forms.DataGridTextBoxColumn descriptionCol;
		private System.Windows.Forms.DataGridTextBoxColumn quantityCol;
		private System.Windows.Forms.DataGridTextBoxColumn totalAmountCol;
		private System.Windows.Forms.DataGrid itemGrid;
		private System.Windows.Forms.Label orderNo3Label;
		private DataShipmentHeader dataShipmentHeader;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox totalAmountBox;
		private DataSet shipmentLineDataSet;
		private bool saveAndClose;
		private System.Windows.Forms.Button button10;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.Button button12;
		private System.Windows.Forms.Button button11;
		private System.Windows.Forms.TextBox dairyNoBox;
		private System.Windows.Forms.TextBox dairyCodeBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox productionSiteBox;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Button button13;
		private System.Windows.Forms.TextBox address2Box;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TabControl orderNo4Label;
		private System.Windows.Forms.Label orderNo5Label;
		private System.Windows.Forms.DataGridTextBoxColumn connectionQuantityCol;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox referenceBox;
		private System.Windows.Forms.Button button14;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox vatAmountBox;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.TextBox amountInclVatBox;
		private System.Windows.Forms.Button button15;
		private System.Windows.Forms.Button button16;
		private System.Windows.Forms.Button button17;
		private System.Windows.Forms.Button button18;
		private System.Windows.Forms.Button button19;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.TextBox phoneNoBox;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.TextBox cellPhoneNoBox;
		private bool allowDelete;
		private System.Windows.Forms.DataGridTextBoxColumn testQuantityCol;
		private System.Windows.Forms.TabPage tabPage5;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Label orderNo6Label;
		private System.Windows.Forms.Button button7;
		private System.Windows.Forms.Button button20;
		private System.Windows.Forms.Button button21;
		private System.Windows.Forms.Button button22;
		private System.Windows.Forms.Button button23;
		private System.Windows.Forms.TextBox shipAddress2Box;
		private System.Windows.Forms.TextBox shipCityBox;
		private System.Windows.Forms.TextBox shipAddressBox;
		private System.Windows.Forms.TextBox shipNameBox;
		private System.Windows.Forms.TextBox shipPostCodeBox;
		private System.Windows.Forms.Button button24;
		private System.Windows.Forms.Button button25;

		private Status agentStatus;

		public Shipment(SmartDatabase smartDatabase, DataShipmentHeader dataShipmentHeader, Status agentStatus)
		{
			this.smartDatabase = smartDatabase;
			this.dataShipmentHeader = dataShipmentHeader;

			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			this.allowDelete = false;
			button10.Visible = false;
			this.agentStatus = agentStatus;
			
			init();
			
		}

		public Shipment(SmartDatabase smartDatabase, DataShipmentHeader dataShipmentHeader, Status agentStatus, bool allowDelete)
		{
			this.smartDatabase = smartDatabase;
			this.dataShipmentHeader = dataShipmentHeader;
			this.agentStatus = agentStatus;

			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			this.allowDelete = allowDelete;
			button10.Visible = allowDelete;

			init();
		}

		private void init()
		{
			DataSetup dataSetup = new DataSetup(smartDatabase);

			this.customerNoBox.Text = dataShipmentHeader.customerNo;
			this.nameBox.Text = dataShipmentHeader.customerName;
			this.addressBox.Text = dataShipmentHeader.address;
			this.address2Box.Text = dataShipmentHeader.address2;
			this.postCodeBox.Text = dataShipmentHeader.postCode;
			this.cityBox.Text = dataShipmentHeader.city;
			this.productionSiteBox.Text = dataShipmentHeader.productionSite;
			this.dairyCodeBox.Text = dataShipmentHeader.dairyCode;
			this.dairyNoBox.Text = dataShipmentHeader.dairyNo;
			this.referenceBox.Text = dataShipmentHeader.reference;

			this.shipNameBox.Text = dataShipmentHeader.shipName;
			this.shipAddressBox.Text = dataShipmentHeader.shipAddress;
			this.shipAddress2Box.Text = dataShipmentHeader.shipAddress2;
			this.shipPostCodeBox.Text = dataShipmentHeader.shipPostCode;
			this.shipCityBox.Text = dataShipmentHeader.shipCity;


			this.phoneNoBox.Text = dataShipmentHeader.phoneNo;
			this.cellPhoneNoBox.Text = dataShipmentHeader.cellPhoneNo;

			updatePaymentText();
			updateGrid();

			this.orderNoLabel.Text = "Följesedel: "+smartDatabase.getSetup().agentId+"-"+dataShipmentHeader.entryNo.ToString();
			this.orderNo2Label.Text = "Följesedel: "+smartDatabase.getSetup().agentId+"-"+dataShipmentHeader.entryNo.ToString();
			this.orderNo3Label.Text = "Följesedel: "+smartDatabase.getSetup().agentId+"-"+dataShipmentHeader.entryNo.ToString();
			this.orderNo5Label.Text = "Följesedel: "+smartDatabase.getSetup().agentId+"-"+dataShipmentHeader.entryNo.ToString();
			this.orderNo6Label.Text = "Följesedel: "+smartDatabase.getSetup().agentId+"-"+dataShipmentHeader.entryNo.ToString();

			setModifyButtons(false);
			if (dataShipmentHeader.customerNo != "")
			{
				DataCustomer dataCustomer = new DataCustomer(smartDatabase, dataShipmentHeader.customerNo);
				if (dataCustomer.modifyable) setModifyButtons(true);
			}

			if (dataShipmentHeader.status != 0)
			{
				button3.Visible = false;
				button4.Visible = false;
				button5.Visible = false;
				button6.Visible = false;

				button11.Visible = false;
				button12.Visible = false;
				button13.Visible = false;
				button14.Visible = false;

				if (dataShipmentHeader.status > 0) button10.Visible = false;

				setModifyButtons(false);
			}

			if (dataShipmentHeader.dataCustomer.forceCashPayment)
			{
				MessageBox.Show("Kunden är tvingad till kontant betalning.", "Betalsätt", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
			}
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
			this.orderNo4Label = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.label14 = new System.Windows.Forms.Label();
			this.button19 = new System.Windows.Forms.Button();
			this.button18 = new System.Windows.Forms.Button();
			this.button17 = new System.Windows.Forms.Button();
			this.button16 = new System.Windows.Forms.Button();
			this.button15 = new System.Windows.Forms.Button();
			this.label10 = new System.Windows.Forms.Label();
			this.address2Box = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.postCodeBox = new System.Windows.Forms.TextBox();
			this.cityBox = new System.Windows.Forms.TextBox();
			this.addressBox = new System.Windows.Forms.TextBox();
			this.nameBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.customerNoBox = new System.Windows.Forms.TextBox();
			this.orderNoLabel = new System.Windows.Forms.Label();
			this.tabPage5 = new System.Windows.Forms.TabPage();
			this.button7 = new System.Windows.Forms.Button();
			this.button20 = new System.Windows.Forms.Button();
			this.button21 = new System.Windows.Forms.Button();
			this.button22 = new System.Windows.Forms.Button();
			this.button23 = new System.Windows.Forms.Button();
			this.label17 = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.shipAddress2Box = new System.Windows.Forms.TextBox();
			this.label19 = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.label21 = new System.Windows.Forms.Label();
			this.shipPostCodeBox = new System.Windows.Forms.TextBox();
			this.shipCityBox = new System.Windows.Forms.TextBox();
			this.shipAddressBox = new System.Windows.Forms.TextBox();
			this.shipNameBox = new System.Windows.Forms.TextBox();
			this.orderNo6Label = new System.Windows.Forms.Label();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.cellPhoneNoBox = new System.Windows.Forms.TextBox();
			this.label16 = new System.Windows.Forms.Label();
			this.phoneNoBox = new System.Windows.Forms.TextBox();
			this.label15 = new System.Windows.Forms.Label();
			this.productionSiteBox = new System.Windows.Forms.TextBox();
			this.orderNo5Label = new System.Windows.Forms.Label();
			this.button13 = new System.Windows.Forms.Button();
			this.label8 = new System.Windows.Forms.Label();
			this.button12 = new System.Windows.Forms.Button();
			this.button11 = new System.Windows.Forms.Button();
			this.dairyNoBox = new System.Windows.Forms.TextBox();
			this.dairyCodeBox = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.button5 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.itemGrid = new System.Windows.Forms.DataGrid();
			this.itemTable = new System.Windows.Forms.DataGridTableStyle();
			this.descriptionCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.quantityCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.connectionQuantityCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.testQuantityCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.totalAmountCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.orderNo2Label = new System.Windows.Forms.Label();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.amountInclVatBox = new System.Windows.Forms.TextBox();
			this.label13 = new System.Windows.Forms.Label();
			this.vatAmountBox = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.button14 = new System.Windows.Forms.Button();
			this.referenceBox = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.button10 = new System.Windows.Forms.Button();
			this.totalAmountBox = new System.Windows.Forms.TextBox();
			this.button9 = new System.Windows.Forms.Button();
			this.paymentBox = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.button8 = new System.Windows.Forms.Button();
			this.button6 = new System.Windows.Forms.Button();
			this.orderNo3Label = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.button24 = new System.Windows.Forms.Button();
			this.button25 = new System.Windows.Forms.Button();
			// 
			// orderNo4Label
			// 
			this.orderNo4Label.Controls.Add(this.tabPage1);
			this.orderNo4Label.Controls.Add(this.tabPage5);
			this.orderNo4Label.Controls.Add(this.tabPage4);
			this.orderNo4Label.Controls.Add(this.tabPage2);
			this.orderNo4Label.Controls.Add(this.tabPage3);
			this.orderNo4Label.SelectedIndex = 0;
			this.orderNo4Label.Size = new System.Drawing.Size(322, 214);
			this.orderNo4Label.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.label14);
			this.tabPage1.Controls.Add(this.button19);
			this.tabPage1.Controls.Add(this.button18);
			this.tabPage1.Controls.Add(this.button17);
			this.tabPage1.Controls.Add(this.button16);
			this.tabPage1.Controls.Add(this.button15);
			this.tabPage1.Controls.Add(this.label10);
			this.tabPage1.Controls.Add(this.address2Box);
			this.tabPage1.Controls.Add(this.label3);
			this.tabPage1.Controls.Add(this.label4);
			this.tabPage1.Controls.Add(this.label2);
			this.tabPage1.Controls.Add(this.postCodeBox);
			this.tabPage1.Controls.Add(this.cityBox);
			this.tabPage1.Controls.Add(this.addressBox);
			this.tabPage1.Controls.Add(this.nameBox);
			this.tabPage1.Controls.Add(this.label1);
			this.tabPage1.Controls.Add(this.customerNoBox);
			this.tabPage1.Controls.Add(this.orderNoLabel);
			this.tabPage1.Location = new System.Drawing.Point(4, 4);
			this.tabPage1.Size = new System.Drawing.Size(314, 188);
			this.tabPage1.Text = "Kund";
			this.tabPage1.EnabledChanged += new System.EventHandler(this.tabPage1_EnabledChanged);
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(8, 155);
			this.label14.Size = new System.Drawing.Size(64, 20);
			this.label14.Text = "Ort:";
			// 
			// button19
			// 
			this.button19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.button19.Location = new System.Drawing.Point(248, 128);
			this.button19.Size = new System.Drawing.Size(56, 20);
			this.button19.Text = "Ändra";
			this.button19.Click += new System.EventHandler(this.button19_Click);
			// 
			// button18
			// 
			this.button18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.button18.Location = new System.Drawing.Point(248, 152);
			this.button18.Size = new System.Drawing.Size(56, 20);
			this.button18.Text = "Ändra";
			this.button18.Click += new System.EventHandler(this.button18_Click);
			// 
			// button17
			// 
			this.button17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.button17.Location = new System.Drawing.Point(248, 104);
			this.button17.Size = new System.Drawing.Size(56, 20);
			this.button17.Text = "Ändra";
			this.button17.Click += new System.EventHandler(this.button17_Click_1);
			// 
			// button16
			// 
			this.button16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.button16.Location = new System.Drawing.Point(248, 80);
			this.button16.Size = new System.Drawing.Size(56, 20);
			this.button16.Text = "Ändra";
			this.button16.Click += new System.EventHandler(this.button16_Click_1);
			// 
			// button15
			// 
			this.button15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.button15.Location = new System.Drawing.Point(248, 56);
			this.button15.Size = new System.Drawing.Size(56, 20);
			this.button15.Text = "Ändra";
			this.button15.Click += new System.EventHandler(this.button15_Click);
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(8, 107);
			this.label10.Size = new System.Drawing.Size(64, 20);
			this.label10.Text = "Adress 2:";
			// 
			// address2Box
			// 
			this.address2Box.Location = new System.Drawing.Point(80, 104);
			this.address2Box.ReadOnly = true;
			this.address2Box.Size = new System.Drawing.Size(160, 20);
			this.address2Box.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 59);
			this.label3.Size = new System.Drawing.Size(64, 20);
			this.label3.Text = "Namn:";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 83);
			this.label4.Size = new System.Drawing.Size(64, 20);
			this.label4.Text = "Adress:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 131);
			this.label2.Size = new System.Drawing.Size(64, 20);
			this.label2.Text = "Postnr:";
			// 
			// postCodeBox
			// 
			this.postCodeBox.Location = new System.Drawing.Point(80, 128);
			this.postCodeBox.ReadOnly = true;
			this.postCodeBox.Size = new System.Drawing.Size(80, 20);
			this.postCodeBox.Text = "";
			// 
			// cityBox
			// 
			this.cityBox.Location = new System.Drawing.Point(80, 152);
			this.cityBox.ReadOnly = true;
			this.cityBox.Size = new System.Drawing.Size(160, 20);
			this.cityBox.Text = "";
			// 
			// addressBox
			// 
			this.addressBox.Location = new System.Drawing.Point(80, 80);
			this.addressBox.ReadOnly = true;
			this.addressBox.Size = new System.Drawing.Size(160, 20);
			this.addressBox.Text = "";
			// 
			// nameBox
			// 
			this.nameBox.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.nameBox.Location = new System.Drawing.Point(80, 56);
			this.nameBox.ReadOnly = true;
			this.nameBox.Size = new System.Drawing.Size(160, 20);
			this.nameBox.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 35);
			this.label1.Size = new System.Drawing.Size(56, 20);
			this.label1.Text = "Kundnr:";
			// 
			// customerNoBox
			// 
			this.customerNoBox.Location = new System.Drawing.Point(80, 32);
			this.customerNoBox.ReadOnly = true;
			this.customerNoBox.Size = new System.Drawing.Size(160, 20);
			this.customerNoBox.Text = "";
			// 
			// orderNoLabel
			// 
			this.orderNoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.orderNoLabel.Location = new System.Drawing.Point(5, 3);
			this.orderNoLabel.Size = new System.Drawing.Size(219, 20);
			this.orderNoLabel.Text = "Följesedel:";
			// 
			// tabPage5
			// 
			this.tabPage5.Controls.Add(this.button7);
			this.tabPage5.Controls.Add(this.button20);
			this.tabPage5.Controls.Add(this.button21);
			this.tabPage5.Controls.Add(this.button22);
			this.tabPage5.Controls.Add(this.button23);
			this.tabPage5.Controls.Add(this.label17);
			this.tabPage5.Controls.Add(this.label18);
			this.tabPage5.Controls.Add(this.shipAddress2Box);
			this.tabPage5.Controls.Add(this.label19);
			this.tabPage5.Controls.Add(this.label20);
			this.tabPage5.Controls.Add(this.label21);
			this.tabPage5.Controls.Add(this.shipPostCodeBox);
			this.tabPage5.Controls.Add(this.shipCityBox);
			this.tabPage5.Controls.Add(this.shipAddressBox);
			this.tabPage5.Controls.Add(this.shipNameBox);
			this.tabPage5.Controls.Add(this.orderNo6Label);
			this.tabPage5.Location = new System.Drawing.Point(4, 4);
			this.tabPage5.Size = new System.Drawing.Size(314, 188);
			this.tabPage5.Text = "Hämtadress";
			// 
			// button7
			// 
			this.button7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.button7.Location = new System.Drawing.Point(248, 104);
			this.button7.Size = new System.Drawing.Size(56, 20);
			this.button7.Text = "Ändra";
			this.button7.Click += new System.EventHandler(this.button7_Click);
			// 
			// button20
			// 
			this.button20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.button20.Location = new System.Drawing.Point(248, 128);
			this.button20.Size = new System.Drawing.Size(56, 20);
			this.button20.Text = "Ändra";
			this.button20.Click += new System.EventHandler(this.button20_Click);
			// 
			// button21
			// 
			this.button21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.button21.Location = new System.Drawing.Point(248, 80);
			this.button21.Size = new System.Drawing.Size(56, 20);
			this.button21.Text = "Ändra";
			this.button21.Click += new System.EventHandler(this.button21_Click);
			// 
			// button22
			// 
			this.button22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.button22.Location = new System.Drawing.Point(248, 56);
			this.button22.Size = new System.Drawing.Size(56, 20);
			this.button22.Text = "Ändra";
			this.button22.Click += new System.EventHandler(this.button22_Click);
			// 
			// button23
			// 
			this.button23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.button23.Location = new System.Drawing.Point(248, 32);
			this.button23.Size = new System.Drawing.Size(56, 20);
			this.button23.Text = "Ändra";
			this.button23.Click += new System.EventHandler(this.button23_Click);
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(8, 131);
			this.label17.Size = new System.Drawing.Size(64, 20);
			this.label17.Text = "Ort:";
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(8, 83);
			this.label18.Size = new System.Drawing.Size(64, 20);
			this.label18.Text = "Adress 2:";
			// 
			// shipAddress2Box
			// 
			this.shipAddress2Box.Location = new System.Drawing.Point(80, 80);
			this.shipAddress2Box.ReadOnly = true;
			this.shipAddress2Box.Size = new System.Drawing.Size(160, 20);
			this.shipAddress2Box.Text = "";
			// 
			// label19
			// 
			this.label19.Location = new System.Drawing.Point(8, 35);
			this.label19.Size = new System.Drawing.Size(64, 20);
			this.label19.Text = "Namn:";
			// 
			// label20
			// 
			this.label20.Location = new System.Drawing.Point(8, 59);
			this.label20.Size = new System.Drawing.Size(64, 20);
			this.label20.Text = "Adress:";
			// 
			// label21
			// 
			this.label21.Location = new System.Drawing.Point(8, 107);
			this.label21.Size = new System.Drawing.Size(64, 20);
			this.label21.Text = "Postnr:";
			// 
			// shipPostCodeBox
			// 
			this.shipPostCodeBox.Location = new System.Drawing.Point(80, 104);
			this.shipPostCodeBox.ReadOnly = true;
			this.shipPostCodeBox.Size = new System.Drawing.Size(80, 20);
			this.shipPostCodeBox.Text = "";
			// 
			// shipCityBox
			// 
			this.shipCityBox.Location = new System.Drawing.Point(80, 128);
			this.shipCityBox.ReadOnly = true;
			this.shipCityBox.Size = new System.Drawing.Size(160, 20);
			this.shipCityBox.Text = "";
			// 
			// shipAddressBox
			// 
			this.shipAddressBox.Location = new System.Drawing.Point(80, 56);
			this.shipAddressBox.ReadOnly = true;
			this.shipAddressBox.Size = new System.Drawing.Size(160, 20);
			this.shipAddressBox.Text = "";
			// 
			// shipNameBox
			// 
			this.shipNameBox.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.shipNameBox.Location = new System.Drawing.Point(80, 32);
			this.shipNameBox.ReadOnly = true;
			this.shipNameBox.Size = new System.Drawing.Size(160, 20);
			this.shipNameBox.Text = "";
			// 
			// orderNo6Label
			// 
			this.orderNo6Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.orderNo6Label.Location = new System.Drawing.Point(5, 3);
			this.orderNo6Label.Size = new System.Drawing.Size(219, 20);
			this.orderNo6Label.Text = "Följesedel:";
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.Add(this.button25);
			this.tabPage4.Controls.Add(this.button24);
			this.tabPage4.Controls.Add(this.cellPhoneNoBox);
			this.tabPage4.Controls.Add(this.label16);
			this.tabPage4.Controls.Add(this.phoneNoBox);
			this.tabPage4.Controls.Add(this.label15);
			this.tabPage4.Controls.Add(this.productionSiteBox);
			this.tabPage4.Controls.Add(this.orderNo5Label);
			this.tabPage4.Controls.Add(this.button13);
			this.tabPage4.Controls.Add(this.label8);
			this.tabPage4.Controls.Add(this.button12);
			this.tabPage4.Controls.Add(this.button11);
			this.tabPage4.Controls.Add(this.dairyNoBox);
			this.tabPage4.Controls.Add(this.dairyCodeBox);
			this.tabPage4.Controls.Add(this.label5);
			this.tabPage4.Controls.Add(this.label7);
			this.tabPage4.Location = new System.Drawing.Point(4, 4);
			this.tabPage4.Size = new System.Drawing.Size(314, 188);
			this.tabPage4.Text = "Uppgifter";
			// 
			// cellPhoneNoBox
			// 
			this.cellPhoneNoBox.Location = new System.Drawing.Point(80, 160);
			this.cellPhoneNoBox.ReadOnly = true;
			this.cellPhoneNoBox.Size = new System.Drawing.Size(152, 20);
			this.cellPhoneNoBox.Text = "";
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(8, 163);
			this.label16.Size = new System.Drawing.Size(64, 20);
			this.label16.Text = "Mobiltel. nr:";
			// 
			// phoneNoBox
			// 
			this.phoneNoBox.Location = new System.Drawing.Point(80, 128);
			this.phoneNoBox.ReadOnly = true;
			this.phoneNoBox.Size = new System.Drawing.Size(152, 20);
			this.phoneNoBox.Text = "";
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(8, 131);
			this.label15.Size = new System.Drawing.Size(64, 20);
			this.label15.Text = "Telefonnr:";
			// 
			// productionSiteBox
			// 
			this.productionSiteBox.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.productionSiteBox.Location = new System.Drawing.Point(81, 32);
			this.productionSiteBox.ReadOnly = true;
			this.productionSiteBox.Size = new System.Drawing.Size(151, 20);
			this.productionSiteBox.Text = "";
			// 
			// orderNo5Label
			// 
			this.orderNo5Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.orderNo5Label.Location = new System.Drawing.Point(5, 3);
			this.orderNo5Label.Size = new System.Drawing.Size(243, 20);
			this.orderNo5Label.Text = "Följesedel:";
			// 
			// button13
			// 
			this.button13.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button13.Location = new System.Drawing.Point(240, 96);
			this.button13.Size = new System.Drawing.Size(64, 24);
			this.button13.Text = "Ändra";
			this.button13.Click += new System.EventHandler(this.button13_Click);
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(8, 99);
			this.label8.Size = new System.Drawing.Size(64, 20);
			this.label8.Text = "Mejerinr:";
			// 
			// button12
			// 
			this.button12.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button12.Location = new System.Drawing.Point(240, 64);
			this.button12.Size = new System.Drawing.Size(64, 24);
			this.button12.Text = "Ändra";
			this.button12.Click += new System.EventHandler(this.button12_Click);
			// 
			// button11
			// 
			this.button11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button11.Location = new System.Drawing.Point(240, 32);
			this.button11.Size = new System.Drawing.Size(64, 24);
			this.button11.Text = "Ändra";
			this.button11.Click += new System.EventHandler(this.button11_Click);
			// 
			// dairyNoBox
			// 
			this.dairyNoBox.Location = new System.Drawing.Point(80, 96);
			this.dairyNoBox.ReadOnly = true;
			this.dairyNoBox.Size = new System.Drawing.Size(152, 20);
			this.dairyNoBox.Text = "";
			// 
			// dairyCodeBox
			// 
			this.dairyCodeBox.Location = new System.Drawing.Point(80, 64);
			this.dairyCodeBox.ReadOnly = true;
			this.dairyCodeBox.Size = new System.Drawing.Size(152, 20);
			this.dairyCodeBox.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 67);
			this.label5.Size = new System.Drawing.Size(64, 20);
			this.label5.Text = "Mejerikod:";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(9, 35);
			this.label7.Size = new System.Drawing.Size(72, 20);
			this.label7.Text = "Prod. platsnr:";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.button5);
			this.tabPage2.Controls.Add(this.button4);
			this.tabPage2.Controls.Add(this.button3);
			this.tabPage2.Controls.Add(this.button2);
			this.tabPage2.Controls.Add(this.button1);
			this.tabPage2.Controls.Add(this.itemGrid);
			this.tabPage2.Controls.Add(this.orderNo2Label);
			this.tabPage2.Location = new System.Drawing.Point(4, 4);
			this.tabPage2.Size = new System.Drawing.Size(314, 188);
			this.tabPage2.Text = "Artiklar";
			// 
			// button5
			// 
			this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button5.Location = new System.Drawing.Point(80, 152);
			this.button5.Size = new System.Drawing.Size(72, 32);
			this.button5.Text = "Ändra";
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// button4
			// 
			this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button4.Location = new System.Drawing.Point(160, 152);
			this.button4.Size = new System.Drawing.Size(72, 32);
			this.button4.Text = "Ta bort";
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// button3
			// 
			this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button3.Location = new System.Drawing.Point(240, 152);
			this.button3.Size = new System.Drawing.Size(72, 32);
			this.button3.Text = "Lägg till";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.button2.Location = new System.Drawing.Point(56, 200);
			this.button2.Size = new System.Drawing.Size(80, 40);
			this.button2.Text = "Lägg till";
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.button1.Location = new System.Drawing.Point(144, 200);
			this.button1.Size = new System.Drawing.Size(80, 40);
			this.button1.Text = "Ta bort";
			// 
			// itemGrid
			// 
			this.itemGrid.Location = new System.Drawing.Point(0, 24);
			this.itemGrid.Size = new System.Drawing.Size(320, 120);
			this.itemGrid.TableStyles.Add(this.itemTable);
			this.itemGrid.Click += new System.EventHandler(this.itemGrid_Click);
			// 
			// itemTable
			// 
			this.itemTable.GridColumnStyles.Add(this.descriptionCol);
			this.itemTable.GridColumnStyles.Add(this.quantityCol);
			this.itemTable.GridColumnStyles.Add(this.connectionQuantityCol);
			this.itemTable.GridColumnStyles.Add(this.testQuantityCol);
			this.itemTable.GridColumnStyles.Add(this.totalAmountCol);
			this.itemTable.MappingName = "shipmentLine";
			// 
			// descriptionCol
			// 
			this.descriptionCol.HeaderText = "Beskrivning";
			this.descriptionCol.MappingName = "description";
			this.descriptionCol.NullText = "";
			this.descriptionCol.Width = 100;
			// 
			// quantityCol
			// 
			this.quantityCol.HeaderText = "Ant./KG";
			this.quantityCol.MappingName = "quantity";
			this.quantityCol.NullText = "";
			// 
			// connectionQuantityCol
			// 
			this.connectionQuantityCol.HeaderText = "Avl.";
			this.connectionQuantityCol.MappingName = "connectionQuantity";
			this.connectionQuantityCol.NullText = "";
			this.connectionQuantityCol.Width = 40;
			// 
			// testQuantityCol
			// 
			this.testQuantityCol.HeaderText = "Provt.";
			this.testQuantityCol.MappingName = "testQuantity";
			this.testQuantityCol.NullText = "";
			this.testQuantityCol.Width = 40;
			// 
			// totalAmountCol
			// 
			this.totalAmountCol.HeaderText = "Belopp";
			this.totalAmountCol.MappingName = "formatedAmount";
			this.totalAmountCol.NullText = "";
			this.totalAmountCol.Width = 60;
			// 
			// orderNo2Label
			// 
			this.orderNo2Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.orderNo2Label.Location = new System.Drawing.Point(5, 3);
			this.orderNo2Label.Size = new System.Drawing.Size(203, 20);
			this.orderNo2Label.Text = "Följesedel:";
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.amountInclVatBox);
			this.tabPage3.Controls.Add(this.label13);
			this.tabPage3.Controls.Add(this.vatAmountBox);
			this.tabPage3.Controls.Add(this.label12);
			this.tabPage3.Controls.Add(this.button14);
			this.tabPage3.Controls.Add(this.referenceBox);
			this.tabPage3.Controls.Add(this.label11);
			this.tabPage3.Controls.Add(this.button10);
			this.tabPage3.Controls.Add(this.totalAmountBox);
			this.tabPage3.Controls.Add(this.button9);
			this.tabPage3.Controls.Add(this.paymentBox);
			this.tabPage3.Controls.Add(this.label9);
			this.tabPage3.Controls.Add(this.button8);
			this.tabPage3.Controls.Add(this.button6);
			this.tabPage3.Controls.Add(this.orderNo3Label);
			this.tabPage3.Controls.Add(this.label6);
			this.tabPage3.Location = new System.Drawing.Point(4, 4);
			this.tabPage3.Size = new System.Drawing.Size(314, 188);
			this.tabPage3.Text = "Kvittens / Betalning";
			// 
			// amountInclVatBox
			// 
			this.amountInclVatBox.Location = new System.Drawing.Point(208, 40);
			this.amountInclVatBox.ReadOnly = true;
			this.amountInclVatBox.Size = new System.Drawing.Size(96, 20);
			this.amountInclVatBox.Text = "";
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(208, 24);
			this.label13.Size = new System.Drawing.Size(104, 20);
			this.label13.Text = "Belopp inkl moms:";
			// 
			// vatAmountBox
			// 
			this.vatAmountBox.Location = new System.Drawing.Point(112, 40);
			this.vatAmountBox.ReadOnly = true;
			this.vatAmountBox.Size = new System.Drawing.Size(88, 20);
			this.vatAmountBox.Text = "";
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(112, 24);
			this.label12.Size = new System.Drawing.Size(72, 20);
			this.label12.Text = "Momsbelopp:";
			// 
			// button14
			// 
			this.button14.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button14.Location = new System.Drawing.Point(224, 68);
			this.button14.Size = new System.Drawing.Size(80, 32);
			this.button14.Text = "Ändra";
			this.button14.Click += new System.EventHandler(this.button14_Click);
			// 
			// referenceBox
			// 
			this.referenceBox.Location = new System.Drawing.Point(8, 80);
			this.referenceBox.ReadOnly = true;
			this.referenceBox.Size = new System.Drawing.Size(208, 20);
			this.referenceBox.Text = "";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(8, 64);
			this.label11.Size = new System.Drawing.Size(72, 20);
			this.label11.Text = "Referensinfo:";
			// 
			// button10
			// 
			this.button10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button10.Location = new System.Drawing.Point(48, 152);
			this.button10.Size = new System.Drawing.Size(80, 32);
			this.button10.Text = "Ta bort";
			this.button10.Click += new System.EventHandler(this.button10_Click);
			// 
			// totalAmountBox
			// 
			this.totalAmountBox.Location = new System.Drawing.Point(8, 40);
			this.totalAmountBox.ReadOnly = true;
			this.totalAmountBox.Size = new System.Drawing.Size(96, 20);
			this.totalAmountBox.Text = "";
			// 
			// button9
			// 
			this.button9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button9.Location = new System.Drawing.Point(136, 152);
			this.button9.Size = new System.Drawing.Size(80, 32);
			this.button9.Text = "Avbryt";
			this.button9.Click += new System.EventHandler(this.button9_Click);
			// 
			// paymentBox
			// 
			this.paymentBox.Location = new System.Drawing.Point(8, 120);
			this.paymentBox.ReadOnly = true;
			this.paymentBox.Size = new System.Drawing.Size(208, 20);
			this.paymentBox.Text = "";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(8, 104);
			this.label9.Size = new System.Drawing.Size(56, 20);
			this.label9.Text = "Betalning:";
			// 
			// button8
			// 
			this.button8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button8.Location = new System.Drawing.Point(224, 152);
			this.button8.Size = new System.Drawing.Size(80, 32);
			this.button8.Text = "Klar";
			this.button8.Click += new System.EventHandler(this.button8_Click);
			// 
			// button6
			// 
			this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button6.Location = new System.Drawing.Point(224, 108);
			this.button6.Size = new System.Drawing.Size(80, 32);
			this.button6.Text = "Ändra";
			this.button6.Click += new System.EventHandler(this.button6_Click);
			// 
			// orderNo3Label
			// 
			this.orderNo3Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.orderNo3Label.Location = new System.Drawing.Point(5, 3);
			this.orderNo3Label.Size = new System.Drawing.Size(251, 20);
			this.orderNo3Label.Text = "Följesedel:";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 24);
			this.label6.Size = new System.Drawing.Size(56, 20);
			this.label6.Text = "Belopp:";
			// 
			// button24
			// 
			this.button24.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button24.Location = new System.Drawing.Point(240, 128);
			this.button24.Size = new System.Drawing.Size(64, 24);
			this.button24.Text = "Ändra";
			this.button24.Click += new System.EventHandler(this.button24_Click);
			// 
			// button25
			// 
			this.button25.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button25.Location = new System.Drawing.Point(240, 160);
			this.button25.Size = new System.Drawing.Size(64, 24);
			this.button25.Text = "Ändra";
			this.button25.Click += new System.EventHandler(this.button25_Click);
			// 
			// Shipment
			// 
			this.ClientSize = new System.Drawing.Size(322, 216);
			this.Controls.Add(this.orderNo4Label);
			this.Text = "Order";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.Order_Closing);

		}
		#endregion

		private void tabControl1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		private void tabPage1_EnabledChanged(object sender, System.EventArgs e)
		{
		
		}

		private void updatePaymentText()
		{
			if (dataShipmentHeader.dataCustomer.forceCashPayment)
			{
				if (dataShipmentHeader.payment == 0) dataShipmentHeader.payment = 1;
				if ((dataShipmentHeader.milkCowExists()) && (dataShipmentHeader.dataCustomer.priceGroupCode != "")) dataShipmentHeader.payment = 0;
			}

			if (dataShipmentHeader.payment == 0)
			{
				paymentBox.Text = "Faktura";
			}
			if (dataShipmentHeader.payment == 1)
			{
				paymentBox.Text = "Kontant";
			}
			if (dataShipmentHeader.payment == 2)
			{
				paymentBox.Text = "Betalkort";
			}
		}

		private void button6_Click(object sender, System.EventArgs e)
		{
			Payment payment = new Payment();
			payment.setPaymentMethod(dataShipmentHeader.payment);
			if ((dataShipmentHeader.dataCustomer.forceCashPayment) && (!dataShipmentHeader.milkCowExists())) payment.setCashOnly();
			payment.ShowDialog();
			dataShipmentHeader.payment = payment.getPaymentMethod();
			payment.Dispose();

			updatePaymentText();
		}

		private void button8_Click(object sender, System.EventArgs e)
		{
			saveAndClose = true;
			this.Close();
		}

		private void button9_Click(object sender, System.EventArgs e)
		{
			saveAndClose = false;
			this.Close();
		}

		private void updateGrid()
		{
			DataShipmentLines dataShipmentLines = new DataShipmentLines(smartDatabase);
			shipmentLineDataSet = dataShipmentLines.getShipmentDataSet(dataShipmentHeader);
			itemGrid.DataSource = shipmentLineDataSet.Tables[0];

			totalAmountBox.Text = decimal.Parse(this.dataShipmentHeader.getTotalAmount()).ToString("N");
			vatAmountBox.Text = decimal.Round(decimal.Multiply(decimal.Parse(totalAmountBox.Text), new Decimal(0.25)), 2).ToString("N");
			amountInclVatBox.Text = decimal.Round(decimal.Multiply(decimal.Parse(totalAmountBox.Text), new Decimal(1.25)), 2).ToString("N");
			
			dataShipmentHeader.getFromDb();

			updatePaymentText();

		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			Cursor.Show();

			dataShipmentHeader.commit();

			ItemList itemList = new ItemList(smartDatabase);

			Cursor.Current = Cursors.Default;
			Cursor.Hide();

			itemList.ShowDialog();
			DataItem dataItem = itemList.getItem();
			itemList.Dispose();

			if (dataItem != null)
			{

				Cursor.Current = Cursors.WaitCursor;
				Cursor.Show();

				DataShipmentLine dataShipmentLine = new DataShipmentLine(smartDatabase, dataShipmentHeader, dataItem);		
				ShipmentItem shipmentItem = new ShipmentItem(smartDatabase, dataShipmentHeader, dataShipmentLine);

				Cursor.Current = Cursors.Default;
				Cursor.Hide();

				shipmentItem.ShowDialog();
				shipmentItem.Dispose();

				Cursor.Current = Cursors.WaitCursor;
				Cursor.Show();

				updatePaymentText();

				Cursor.Current = Cursors.Default;
				Cursor.Hide();

			}

			updateGrid();
		}

		private void button4_Click(object sender, System.EventArgs e)
		{
			dataShipmentHeader.commit();
			
			if (((DataTable)(itemGrid.DataSource)).Rows.Count > 0)
			{
				DataShipmentLine dataShipmentLine = new DataShipmentLine(smartDatabase, int.Parse(shipmentLineDataSet.Tables[0].Rows[itemGrid.CurrentRowIndex].ItemArray.GetValue(0).ToString()));
				dataShipmentLine.delete();
				updateGrid();
			}
		}

		private void itemGrid_Click(object sender, System.EventArgs e)
		{
		}

		private void button5_Click(object sender, System.EventArgs e)
		{
			dataShipmentHeader.commit();

			if (((DataTable)itemGrid.DataSource).Rows.Count > 0)
			{
				if (itemGrid.CurrentRowIndex >= 0)
				{
					DataShipmentLine dataShipmentLine = new DataShipmentLine(smartDatabase, int.Parse(shipmentLineDataSet.Tables[0].Rows[itemGrid.CurrentRowIndex].ItemArray.GetValue(0).ToString()));
					ShipmentItem shipmentItem = new ShipmentItem(smartDatabase, dataShipmentHeader, dataShipmentLine);
					shipmentItem.ShowDialog();
					shipmentItem.Dispose();
					updateGrid();
				}
			}
		}

		private void Order_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{

			if (saveAndClose)
			{

				dataShipmentHeader.commit();


				if (!dataShipmentHeader.checkIds())
				{
					MessageBox.Show("Det finns rader som saknar ID-nr.");
					e.Cancel = true;
					return;
				}


				if (!dataShipmentHeader.checkIdTypes())
				{
					e.Cancel = true;
					return;
				}


				Printer printer = new Printer(smartDatabase);

				if (!printer.init())
				{
					if (MessageBox.Show("Kan ej få kontakt med skrivaren. Kontrollera skrivaren. Skicka ändå?", "Fel", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) == DialogResult.No)
					{
						e.Cancel = true;
						return;
					}
				}
				else
				{
					Cursor.Current = Cursors.WaitCursor;
					Cursor.Show();

					if (dataShipmentHeader.status == 0)
					{
						DialogResult result = MessageBox.Show("Vill du skriva ut följesedeln?", "Utskrift", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
						if (result == DialogResult.Cancel)
						{
							Cursor.Current = Cursors.Default;
							Cursor.Hide();

							e.Cancel = true;
							return;
						}

						if (result == DialogResult.Yes)
						{
							printer.printShipment(dataShipmentHeader);

							if (MessageBox.Show("Vill du skriva ut en kopia av följesedel?", "Utskrift", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
							{
								printer.setCopy();				
								printer.printShipment(dataShipmentHeader);
							}

						}
					}
					else
					{
						if (MessageBox.Show("Vill du skriva ut en kopia av följesedeln?", "Utskrift", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
						{
							printer.setCopy();				
							printer.printShipment(dataShipmentHeader);
						}

						if (dataShipmentHeader.payment > 0)
						{
							if (MessageBox.Show("Vill du skriva ut en kopia av kontantnotan?", "Utskrift", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
							{
								printer.setCopy();				
								printer.printShipmentNote(dataShipmentHeader);
							}
						}
					}
					printer.close();

					Cursor.Current = Cursors.Default;
					Cursor.Hide();

				}


				if (dataShipmentHeader.status == 0)
				{
					bool sendToBackOffice = false;

					DialogResult result = MessageBox.Show("Är följesedeln klar för sändning?", "Sänd order", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

					if (result == DialogResult.No)
					{
						DialogResult result2 = MessageBox.Show("Du valde att inte skicka följesedeln. Rätt? Tryck NO för att skicka följesedeln.", "Sänd order", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
						if (result2 == DialogResult.No) sendToBackOffice = true;
					}

					if (result == DialogResult.Yes) sendToBackOffice = true;

					if (sendToBackOffice)
					{
						if (dataShipmentHeader.payment > 0)
						{
							DataShipmentInvoice dataShipmentInvoice = new DataShipmentInvoice(smartDatabase);
							dataShipmentHeader.invoiceNo = dataShipmentInvoice.getInvoiceNo(smartDatabase.getSetup().agentId);
							dataShipmentHeader.commit();
						}

						dataShipmentHeader.status = 1;
						dataShipmentHeader.positionX = agentStatus.rt90x;
						dataShipmentHeader.positionY = agentStatus.rt90y;
						dataShipmentHeader.commit();
						DataSyncActions syncAction = new DataSyncActions(smartDatabase);
						syncAction.addSyncAction(2, 0, dataShipmentHeader.entryNo.ToString());

						if (dataShipmentHeader.payment > 0)
						{
							if (MessageBox.Show("Vill du skriva ut kontantnota?", "Utskrift", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
							{
								printer.init();
								printer.printShipmentNote(dataShipmentHeader);
								printer.close();
							}
						}
					}

					if (result == DialogResult.Cancel)
					{
						e.Cancel = true;
						return;
					}
				}
				

			}
			else
			{
				if (!allowDelete)
				{
					if (MessageBox.Show("Du kommer att radera följesedeln. Ok?", "Avbryt", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
					{
						dataShipmentHeader.delete();
					
					}
					else
					{
						e.Cancel = true;
					}
					
				}
			}


		}

		private void button10_Click(object sender, System.EventArgs e)
		{
			if (MessageBox.Show("Du kommer att makulera följesedeln. Ok?", "Avbryt", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
			{
				dataShipmentHeader.delete();
				this.Close();							
			}
		}

		private void button11_Click(object sender, System.EventArgs e)
		{
			Keyboard keyb = new Keyboard(30);
			keyb.setInputString(dataShipmentHeader.productionSite);
			keyb.ShowDialog();
			dataShipmentHeader.productionSite = keyb.getInputString();
			productionSiteBox.Text = keyb.getInputString();
			keyb.Dispose();

		}

		private void button12_Click(object sender, System.EventArgs e)
		{
			Keyboard keyb = new Keyboard(20);
			keyb.setInputString(dataShipmentHeader.dairyCode);
			keyb.ShowDialog();
			dataShipmentHeader.dairyCode = keyb.getInputString();
			dairyCodeBox.Text = keyb.getInputString();
			keyb.Dispose();		
		}

		private void button13_Click(object sender, System.EventArgs e)
		{
			Keyboard keyb = new Keyboard(30);
			keyb.setInputString(dataShipmentHeader.dairyNo);
			keyb.ShowDialog();
			dataShipmentHeader.dairyNo = keyb.getInputString();
			dairyNoBox.Text = keyb.getInputString();
			keyb.Dispose();		
		
		}

		private void button14_Click(object sender, System.EventArgs e)
		{
			Keyboard keyboard = new Keyboard(100);
			keyboard.setInputString(referenceBox.Text);
			keyboard.ShowDialog();
			
			referenceBox.Text = keyboard.getInputString();

			keyboard.Dispose();

			dataShipmentHeader.reference = referenceBox.Text;

		}

		private void button17_Click(object sender, System.EventArgs e)
		{
			saveAndClose = true;
			this.Close();
		}

		private void button16_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void button15_Click(object sender, System.EventArgs e)
		{
			Keyboard keyb = new Keyboard(30);
			keyb.setInputString(dataShipmentHeader.customerName);
			keyb.ShowDialog();
			dataShipmentHeader.customerName = keyb.getInputString();
			if (dataShipmentHeader.shipName == "") dataShipmentHeader.shipName = dataShipmentHeader.customerName;
			this.nameBox.Text = keyb.getInputString();
			keyb.Dispose();

		}

		private void button16_Click_1(object sender, System.EventArgs e)
		{
			Keyboard keyb = new Keyboard(30);
			keyb.setInputString(dataShipmentHeader.address);
			keyb.ShowDialog();
			dataShipmentHeader.address = keyb.getInputString();
			if (dataShipmentHeader.shipAddress == "") dataShipmentHeader.shipAddress = dataShipmentHeader.address;
			this.addressBox.Text = keyb.getInputString();
			keyb.Dispose();

		}

		private void button17_Click_1(object sender, System.EventArgs e)
		{
			Keyboard keyb = new Keyboard(30);
			keyb.setInputString(dataShipmentHeader.address2);
			keyb.ShowDialog();
			dataShipmentHeader.address2 = keyb.getInputString();
			if (dataShipmentHeader.shipAddress2 == "") dataShipmentHeader.shipAddress2 = dataShipmentHeader.address2;
			this.address2Box.Text = keyb.getInputString();
			keyb.Dispose();

		}

		private void button18_Click(object sender, System.EventArgs e)
		{
			Keyboard keyb = new Keyboard(30);
			keyb.setInputString(dataShipmentHeader.city);
			keyb.ShowDialog();
			dataShipmentHeader.city = keyb.getInputString();
			if (dataShipmentHeader.shipCity == "") dataShipmentHeader.shipCity = dataShipmentHeader.city;
			this.cityBox.Text = keyb.getInputString();
			keyb.Dispose();

		}

		private void button19_Click(object sender, System.EventArgs e)
		{
			Keyboard keyb = new Keyboard(30);
			keyb.setInputString(dataShipmentHeader.postCode);
			keyb.ShowDialog();
			dataShipmentHeader.postCode = keyb.getInputString();
			if (dataShipmentHeader.shipPostCode == "") dataShipmentHeader.shipPostCode = dataShipmentHeader.postCode;
			this.postCodeBox.Text = keyb.getInputString();
			keyb.Dispose();
		
		}

		private void setModifyButtons(bool modifyable)
		{
			if (modifyable)
			{
				button15.Visible = true;
				button16.Visible = true;
				button17.Visible = true;
				button18.Visible = true;
				button19.Visible = true;

				customerNoBox.Width = 160;
				nameBox.Width = 160;
				addressBox.Width = 160;
				address2Box.Width = 160;
				cityBox.Width = 160;
			}
			else
			{
				button15.Visible = false;
				button16.Visible = false;
				button17.Visible = false;
				button18.Visible = false;
				button19.Visible = false;

				customerNoBox.Width = 224;
				nameBox.Width = 224;
				addressBox.Width = 224;
				address2Box.Width = 224;
				cityBox.Width = 224;

			}

		}

		private void button23_Click(object sender, System.EventArgs e)
		{
			Keyboard keyb = new Keyboard(30);
			keyb.setInputString(dataShipmentHeader.shipName);
			keyb.ShowDialog();
			dataShipmentHeader.shipName = keyb.getInputString();
			this.shipNameBox.Text = keyb.getInputString();
			keyb.Dispose();

			dataShipmentHeader.customerShipAddressNo = "NEW";

		}

		private void button22_Click(object sender, System.EventArgs e)
		{
			Keyboard keyb = new Keyboard(30);
			keyb.setInputString(dataShipmentHeader.shipAddress);
			keyb.ShowDialog();
			dataShipmentHeader.shipAddress = keyb.getInputString();
			this.shipAddressBox.Text = keyb.getInputString();
			keyb.Dispose();

			dataShipmentHeader.customerShipAddressNo = "NEW";

		}

		private void button21_Click(object sender, System.EventArgs e)
		{
			Keyboard keyb = new Keyboard(30);
			keyb.setInputString(dataShipmentHeader.shipAddress2);
			keyb.ShowDialog();
			dataShipmentHeader.shipAddress2 = keyb.getInputString();
			this.shipAddress2Box.Text = keyb.getInputString();
			keyb.Dispose();

			dataShipmentHeader.customerShipAddressNo = "NEW";

		}

		private void button7_Click(object sender, System.EventArgs e)
		{
			Keyboard keyb = new Keyboard(30);
			keyb.setInputString(dataShipmentHeader.shipPostCode);
			keyb.ShowDialog();
			dataShipmentHeader.shipPostCode = keyb.getInputString();
			this.shipPostCodeBox.Text = keyb.getInputString();
			keyb.Dispose();

			dataShipmentHeader.customerShipAddressNo = "NEW";

		}

		private void button20_Click(object sender, System.EventArgs e)
		{
			Keyboard keyb = new Keyboard(30);
			keyb.setInputString(dataShipmentHeader.shipCity);
			keyb.ShowDialog();
			dataShipmentHeader.shipCity = keyb.getInputString();
			this.shipCityBox.Text = keyb.getInputString();
			keyb.Dispose();

			dataShipmentHeader.customerShipAddressNo = "NEW";

		}

		private void button24_Click(object sender, System.EventArgs e)
		{
			Keyboard keyb = new Keyboard(30);
			keyb.setInputString(dataShipmentHeader.phoneNo);
			keyb.ShowDialog();
			dataShipmentHeader.phoneNo = keyb.getInputString();
			phoneNoBox.Text = keyb.getInputString();
			keyb.Dispose();		

		}

		private void button25_Click(object sender, System.EventArgs e)
		{
			Keyboard keyb = new Keyboard(30);
			keyb.setInputString(dataShipmentHeader.cellPhoneNo);
			keyb.ShowDialog();
			dataShipmentHeader.cellPhoneNo = keyb.getInputString();
			cellPhoneNoBox.Text = keyb.getInputString();
			keyb.Dispose();		

		}
	}
}
