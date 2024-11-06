using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace SmartOrder
{
	/// <summary>
	/// Summary description for Order.
	/// </summary>
	public class Order : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox orderNo;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox customerNo;
		private System.Windows.Forms.TextBox name;
		private System.Windows.Forms.TextBox address;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.TextBox zipCode;
		private System.Windows.Forms.TextBox city;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox address2;
		private System.Windows.Forms.Label label7;
		private Microsoft.WindowsCE.Forms.InputPanel inputPanel1;
		private System.Windows.Forms.Panel panel1;
	
		private DataSalesHeader dataSalesHeader;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox deliveryAddress2;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.TextBox deliveryCity;
		private System.Windows.Forms.TextBox deliveryZipCode;
		private System.Windows.Forms.TextBox deliveryAddress;
		private System.Windows.Forms.TextBox deliveryName;
		private System.Windows.Forms.ComboBox deliveryCode;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.TextBox deliveryContact;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.DataGridTableStyle salesLineTable;
		private System.Windows.Forms.DataGridTextBoxColumn itemNoCol;
		private System.Windows.Forms.DataGridTextBoxColumn descriptionCol;
		private System.Windows.Forms.DataGridTextBoxColumn quantityCol;
		private System.Windows.Forms.DataGridTextBoxColumn baseUnitCol;


		private SmartDatabase smartDatabase;
		private System.Windows.Forms.DataGrid salesLineGrid;
		private DataSalesLines dataSalesLines;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.Button button3;
		private DataSet salesLineDataSet;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.TextBox phone;
		private System.Windows.Forms.TextBox contact;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.DataGridTextBoxColumn colorCol;

		public Order(DataSalesHeader dataSalesHeader, SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
			this.dataSalesLines = new DataSalesLines(smartDatabase);

			this.dataSalesHeader = dataSalesHeader;
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.button3 = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.label7 = new System.Windows.Forms.Label();
			this.address2 = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.city = new System.Windows.Forms.TextBox();
			this.zipCode = new System.Windows.Forms.TextBox();
			this.customerNo = new System.Windows.Forms.TextBox();
			this.address = new System.Windows.Forms.TextBox();
			this.name = new System.Windows.Forms.TextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label2 = new System.Windows.Forms.Label();
			this.orderNo = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.panel3 = new System.Windows.Forms.Panel();
			this.label15 = new System.Windows.Forms.Label();
			this.deliveryContact = new System.Windows.Forms.TextBox();
			this.deliveryCode = new System.Windows.Forms.ComboBox();
			this.label9 = new System.Windows.Forms.Label();
			this.deliveryAddress2 = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.deliveryCity = new System.Windows.Forms.TextBox();
			this.deliveryZipCode = new System.Windows.Forms.TextBox();
			this.deliveryAddress = new System.Windows.Forms.TextBox();
			this.deliveryName = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.button4 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.salesLineGrid = new System.Windows.Forms.DataGrid();
			this.salesLineTable = new System.Windows.Forms.DataGridTableStyle();
			this.itemNoCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.descriptionCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.colorCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.quantityCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.baseUnitCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.label14 = new System.Windows.Forms.Label();
			this.inputPanel1 = new Microsoft.WindowsCE.Forms.InputPanel();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.label20 = new System.Windows.Forms.Label();
			this.panel4 = new System.Windows.Forms.Panel();
			this.phone = new System.Windows.Forms.TextBox();
			this.contact = new System.Windows.Forms.TextBox();
			this.label16 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(250, 272);
			this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.button3);
			this.tabPage1.Controls.Add(this.panel2);
			this.tabPage1.Controls.Add(this.panel1);
			this.tabPage1.Controls.Add(this.label1);
			this.tabPage1.Location = new System.Drawing.Point(4, 4);
			this.tabPage1.Size = new System.Drawing.Size(242, 246);
			this.tabPage1.Text = "Kund";
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(152, 216);
			this.button3.Size = new System.Drawing.Size(80, 20);
			this.button3.Text = "Klarmarkera";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.label7);
			this.panel2.Controls.Add(this.address2);
			this.panel2.Controls.Add(this.label6);
			this.panel2.Controls.Add(this.label5);
			this.panel2.Controls.Add(this.label4);
			this.panel2.Controls.Add(this.label3);
			this.panel2.Controls.Add(this.city);
			this.panel2.Controls.Add(this.zipCode);
			this.panel2.Controls.Add(this.customerNo);
			this.panel2.Controls.Add(this.address);
			this.panel2.Controls.Add(this.name);
			this.panel2.Location = new System.Drawing.Point(0, 64);
			this.panel2.Size = new System.Drawing.Size(232, 136);
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(8, 83);
			this.label7.Size = new System.Drawing.Size(72, 20);
			this.label7.Text = "Adress 2:";
			// 
			// address2
			// 
			this.address2.Location = new System.Drawing.Point(104, 80);
			this.address2.Size = new System.Drawing.Size(112, 20);
			this.address2.Text = "";
			this.address2.GotFocus += new System.EventHandler(this.address2_GotFocus);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 107);
			this.label6.Size = new System.Drawing.Size(64, 20);
			this.label6.Text = "Postadress:";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 59);
			this.label5.Size = new System.Drawing.Size(72, 20);
			this.label5.Text = "Adress:";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 35);
			this.label4.Size = new System.Drawing.Size(64, 20);
			this.label4.Text = "Namn:";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 11);
			this.label3.Size = new System.Drawing.Size(72, 20);
			this.label3.Text = "Kundnr:";
			// 
			// city
			// 
			this.city.Location = new System.Drawing.Point(144, 104);
			this.city.Size = new System.Drawing.Size(72, 20);
			this.city.Text = "";
			this.city.GotFocus += new System.EventHandler(this.city_GotFocus);
			// 
			// zipCode
			// 
			this.zipCode.Location = new System.Drawing.Point(104, 104);
			this.zipCode.Size = new System.Drawing.Size(32, 20);
			this.zipCode.Text = "";
			this.zipCode.GotFocus += new System.EventHandler(this.zipCode_GotFocus);
			// 
			// customerNo
			// 
			this.customerNo.Location = new System.Drawing.Point(104, 8);
			this.customerNo.ReadOnly = true;
			this.customerNo.Size = new System.Drawing.Size(72, 20);
			this.customerNo.Text = "";
			// 
			// address
			// 
			this.address.Location = new System.Drawing.Point(104, 56);
			this.address.Size = new System.Drawing.Size(112, 20);
			this.address.Text = "";
			this.address.GotFocus += new System.EventHandler(this.address_GotFocus);
			// 
			// name
			// 
			this.name.Location = new System.Drawing.Point(104, 32);
			this.name.Size = new System.Drawing.Size(112, 20);
			this.name.Text = "";
			this.name.GotFocus += new System.EventHandler(this.name_GotFocus);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.orderNo);
			this.panel1.Location = new System.Drawing.Point(0, 32);
			this.panel1.Size = new System.Drawing.Size(232, 32);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 11);
			this.label2.Size = new System.Drawing.Size(72, 20);
			this.label2.Text = "Ordernr:";
			// 
			// orderNo
			// 
			this.orderNo.Location = new System.Drawing.Point(104, 8);
			this.orderNo.ReadOnly = true;
			this.orderNo.Size = new System.Drawing.Size(72, 20);
			this.orderNo.Text = "";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Size = new System.Drawing.Size(120, 16);
			this.label1.Text = "Order: Kunduppgifter";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.panel3);
			this.tabPage2.Controls.Add(this.label8);
			this.tabPage2.Location = new System.Drawing.Point(4, 4);
			this.tabPage2.Size = new System.Drawing.Size(242, 246);
			this.tabPage2.Text = "Leverans";
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.label15);
			this.panel3.Controls.Add(this.deliveryContact);
			this.panel3.Controls.Add(this.deliveryCode);
			this.panel3.Controls.Add(this.label9);
			this.panel3.Controls.Add(this.deliveryAddress2);
			this.panel3.Controls.Add(this.label10);
			this.panel3.Controls.Add(this.label11);
			this.panel3.Controls.Add(this.label12);
			this.panel3.Controls.Add(this.label13);
			this.panel3.Controls.Add(this.deliveryCity);
			this.panel3.Controls.Add(this.deliveryZipCode);
			this.panel3.Controls.Add(this.deliveryAddress);
			this.panel3.Controls.Add(this.deliveryName);
			this.panel3.Location = new System.Drawing.Point(0, 32);
			this.panel3.Size = new System.Drawing.Size(232, 168);
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(8, 139);
			this.label15.Size = new System.Drawing.Size(88, 20);
			this.label15.Text = "Kontaktperson:";
			// 
			// deliveryContact
			// 
			this.deliveryContact.Location = new System.Drawing.Point(104, 136);
			this.deliveryContact.Size = new System.Drawing.Size(112, 20);
			this.deliveryContact.Text = "";
			this.deliveryContact.GotFocus += new System.EventHandler(this.deliveryContact_GotFocus);
			// 
			// deliveryCode
			// 
			this.deliveryCode.DisplayMember = "code";
			this.deliveryCode.Location = new System.Drawing.Point(104, 8);
			this.deliveryCode.Size = new System.Drawing.Size(112, 21);
			this.deliveryCode.SelectedIndexChanged += new System.EventHandler(this.deliveryCode_SelectedIndexChanged);
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(8, 83);
			this.label9.Size = new System.Drawing.Size(72, 20);
			this.label9.Text = "Adress 2:";
			// 
			// deliveryAddress2
			// 
			this.deliveryAddress2.Location = new System.Drawing.Point(104, 80);
			this.deliveryAddress2.Size = new System.Drawing.Size(112, 20);
			this.deliveryAddress2.Text = "";
			this.deliveryAddress2.GotFocus += new System.EventHandler(this.deliveryAddress2_GotFocus);
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(8, 107);
			this.label10.Size = new System.Drawing.Size(64, 20);
			this.label10.Text = "Postadress:";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(8, 59);
			this.label11.Size = new System.Drawing.Size(72, 20);
			this.label11.Text = "Adress:";
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(8, 35);
			this.label12.Size = new System.Drawing.Size(64, 20);
			this.label12.Text = "Namn:";
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(8, 11);
			this.label13.Size = new System.Drawing.Size(72, 20);
			this.label13.Text = "Leveranskod:";
			// 
			// deliveryCity
			// 
			this.deliveryCity.Location = new System.Drawing.Point(144, 104);
			this.deliveryCity.Size = new System.Drawing.Size(72, 20);
			this.deliveryCity.Text = "";
			this.deliveryCity.GotFocus += new System.EventHandler(this.deliveryCity_GotFocus);
			// 
			// deliveryZipCode
			// 
			this.deliveryZipCode.Location = new System.Drawing.Point(104, 104);
			this.deliveryZipCode.Size = new System.Drawing.Size(32, 20);
			this.deliveryZipCode.Text = "";
			this.deliveryZipCode.GotFocus += new System.EventHandler(this.deliveryZipCode_GotFocus);
			// 
			// deliveryAddress
			// 
			this.deliveryAddress.Location = new System.Drawing.Point(104, 56);
			this.deliveryAddress.Size = new System.Drawing.Size(112, 20);
			this.deliveryAddress.Text = "";
			this.deliveryAddress.GotFocus += new System.EventHandler(this.deliveryAddress_GotFocus);
			// 
			// deliveryName
			// 
			this.deliveryName.Location = new System.Drawing.Point(104, 32);
			this.deliveryName.Size = new System.Drawing.Size(112, 20);
			this.deliveryName.Text = "";
			this.deliveryName.GotFocus += new System.EventHandler(this.deliveryName_GotFocus);
			// 
			// label8
			// 
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label8.Location = new System.Drawing.Point(8, 8);
			this.label8.Size = new System.Drawing.Size(128, 20);
			this.label8.Text = "Order: Leverans";
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.button4);
			this.tabPage3.Controls.Add(this.button2);
			this.tabPage3.Controls.Add(this.button1);
			this.tabPage3.Controls.Add(this.salesLineGrid);
			this.tabPage3.Controls.Add(this.label14);
			this.tabPage3.Location = new System.Drawing.Point(4, 4);
			this.tabPage3.Size = new System.Drawing.Size(242, 246);
			this.tabPage3.Text = "Artiklar";
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(24, 216);
			this.button4.Size = new System.Drawing.Size(64, 20);
			this.button4.Text = "Visa";
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(168, 216);
			this.button2.Size = new System.Drawing.Size(64, 20);
			this.button2.Text = "Ta bort";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(96, 216);
			this.button1.Size = new System.Drawing.Size(64, 20);
			this.button1.Text = "Lägg till";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// salesLineGrid
			// 
			this.salesLineGrid.Location = new System.Drawing.Point(0, 32);
			this.salesLineGrid.Size = new System.Drawing.Size(240, 176);
			this.salesLineGrid.TableStyles.Add(this.salesLineTable);
			this.salesLineGrid.Text = "salesLineGrid";
			// 
			// salesLineTable
			// 
			this.salesLineTable.GridColumnStyles.Add(this.itemNoCol);
			this.salesLineTable.GridColumnStyles.Add(this.descriptionCol);
			this.salesLineTable.GridColumnStyles.Add(this.colorCol);
			this.salesLineTable.GridColumnStyles.Add(this.quantityCol);
			this.salesLineTable.GridColumnStyles.Add(this.baseUnitCol);
			this.salesLineTable.MappingName = "salesLine";
			// 
			// itemNoCol
			// 
			this.itemNoCol.HeaderText = "Artikelnr";
			this.itemNoCol.MappingName = "itemNo";
			this.itemNoCol.NullText = "";
			// 
			// descriptionCol
			// 
			this.descriptionCol.HeaderText = "Beskrivning";
			this.descriptionCol.MappingName = "description";
			this.descriptionCol.NullText = "";
			this.descriptionCol.Width = 100;
			// 
			// colorCol
			// 
			this.colorCol.HeaderText = "Färg";
			this.colorCol.MappingName = "colorCode";
			this.colorCol.NullText = "";
			// 
			// quantityCol
			// 
			this.quantityCol.HeaderText = "Antal";
			this.quantityCol.MappingName = "sumQuantity";
			this.quantityCol.NullText = "";
			// 
			// baseUnitCol
			// 
			this.baseUnitCol.HeaderText = "Basenhet";
			this.baseUnitCol.MappingName = "baseUnit";
			this.baseUnitCol.NullText = "";
			// 
			// label14
			// 
			this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label14.Location = new System.Drawing.Point(8, 8);
			this.label14.Text = "Order: Artiklar";
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.Add(this.panel4);
			this.tabPage4.Controls.Add(this.label20);
			this.tabPage4.Location = new System.Drawing.Point(4, 4);
			this.tabPage4.Size = new System.Drawing.Size(242, 246);
			this.tabPage4.Text = "Kontakt";
			// 
			// label20
			// 
			this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label20.Location = new System.Drawing.Point(8, 8);
			this.label20.Size = new System.Drawing.Size(216, 20);
			this.label20.Text = "Order: Kontaktuppgifter";
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.label16);
			this.panel4.Controls.Add(this.label17);
			this.panel4.Controls.Add(this.phone);
			this.panel4.Controls.Add(this.contact);
			this.panel4.Location = new System.Drawing.Point(0, 32);
			this.panel4.Size = new System.Drawing.Size(240, 104);
			// 
			// phone
			// 
			this.phone.Location = new System.Drawing.Point(104, 32);
			this.phone.Size = new System.Drawing.Size(112, 20);
			this.phone.Text = "";
			this.phone.GotFocus += new System.EventHandler(this.phone_GotFocus);
			// 
			// contact
			// 
			this.contact.Location = new System.Drawing.Point(104, 8);
			this.contact.Size = new System.Drawing.Size(112, 20);
			this.contact.Text = "";
			this.contact.GotFocus += new System.EventHandler(this.contact_GotFocus);
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(8, 35);
			this.label16.Size = new System.Drawing.Size(88, 20);
			this.label16.Text = "Telefonnr:";
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(8, 11);
			this.label17.Size = new System.Drawing.Size(80, 20);
			this.label17.Text = "Kontaktperson:";
			// 
			// Order
			// 
			this.ClientSize = new System.Drawing.Size(250, 270);
			this.Controls.Add(this.tabControl1);
			this.Menu = this.mainMenu1;
			this.Text = "Order";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.Order_Closing);
			this.Load += new System.EventHandler(this.Order_Load);

		}
		#endregion

		private void Order_Load(object sender, System.EventArgs e)
		{
			Agent agent = new Agent(smartDatabase);

			DataDeliveryAddresses deliveryAddresses = new DataDeliveryAddresses(smartDatabase);
			DataSet deliveryAddressesDataSet = deliveryAddresses.getDataSet(new DataCustomer(dataSalesHeader.customerNo));

			deliveryCode.Items.Add("Standard");
			
			int i = 0;
			while (i < deliveryAddressesDataSet.Tables[0].Rows.Count)
			{
				deliveryCode.Items.Add((string)deliveryAddressesDataSet.Tables[0].Rows[i].ItemArray.GetValue(2));
				i++;
			}
			
            
			orderNo.Text = agent.agentId+""+dataSalesHeader.no;
			customerNo.Text = dataSalesHeader.customerNo;
			name.Text = dataSalesHeader.name;
			address.Text = dataSalesHeader.address;
			address2.Text = dataSalesHeader.address2;
			zipCode.Text = dataSalesHeader.zipCode;
			city.Text = dataSalesHeader.city;

			deliveryCode.Text = dataSalesHeader.deliveryCode;
			deliveryName.Text = dataSalesHeader.deliveryName;
			deliveryAddress.Text = dataSalesHeader.deliveryAddress;
			deliveryAddress2.Text = dataSalesHeader.deliveryAddress2;
			deliveryZipCode.Text = dataSalesHeader.deliveryZipCode;
			deliveryCity.Text = dataSalesHeader.deliveryCity;
			deliveryContact.Text = dataSalesHeader.deliveryContact;

			contact.Text = dataSalesHeader.contact;
			phone.Text = dataSalesHeader.phoneNo;

			if (deliveryCode.Text == "")
			{
				deliveryCode.Text = "Standard";
				deliveryName.Text = name.Text;
				deliveryAddress.Text = address.Text;
				deliveryAddress2.Text = address2.Text;
				deliveryZipCode.Text = zipCode.Text;
				deliveryCity.Text = city.Text;
			}

			if (dataSalesHeader.ready)
			{
				button3.Text = "Öppna";
			}
			else
			{
				button3.Text = "Klarmarkera";
			}

			updateGrid();
			checkReadyFlag();
		}

		private void name_GotFocus(object sender, System.EventArgs e)
		{
			inputPanel1.Enabled = true;		
		}

		private void address_GotFocus(object sender, System.EventArgs e)
		{
			inputPanel1.Enabled = true;		
		}

		private void address2_GotFocus(object sender, System.EventArgs e)
		{
			inputPanel1.Enabled = true;
		}

		private void zipCode_GotFocus(object sender, System.EventArgs e)
		{
			inputPanel1.Enabled = true;
		}

		private void city_GotFocus(object sender, System.EventArgs e)
		{
			inputPanel1.Enabled = true;
		}

		private void tabControl1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			inputPanel1.Enabled = false;

			save();
		}

		private void save()
		{
			dataSalesHeader.customerNo = customerNo.Text;
			dataSalesHeader.name = name.Text;
			dataSalesHeader.address = address.Text;
			dataSalesHeader.address2 = address2.Text;
			dataSalesHeader.zipCode = zipCode.Text;
			dataSalesHeader.city = city.Text;
			dataSalesHeader.phoneNo = phone.Text;
			dataSalesHeader.contact = contact.Text;
			
			dataSalesHeader.deliveryCode = deliveryCode.Text;
			dataSalesHeader.deliveryName = deliveryName.Text;
			dataSalesHeader.deliveryAddress = deliveryAddress.Text;
			dataSalesHeader.deliveryAddress2 = deliveryAddress2.Text;
			dataSalesHeader.deliveryZipCode = deliveryZipCode.Text;
			dataSalesHeader.deliveryCity = deliveryCity.Text;
			dataSalesHeader.deliveryContact = deliveryContact.Text;

			dataSalesHeader.save();
		}

		private void deliveryName_GotFocus(object sender, System.EventArgs e)
		{
			inputPanel1.Enabled = true;
		}

		private void deliveryAddress_GotFocus(object sender, System.EventArgs e)
		{
			inputPanel1.Enabled = true;
		}

		private void deliveryAddress2_GotFocus(object sender, System.EventArgs e)
		{
			inputPanel1.Enabled = true;
		}

		private void deliveryZipCode_GotFocus(object sender, System.EventArgs e)
		{
			inputPanel1.Enabled = true;
		}

		private void deliveryCity_GotFocus(object sender, System.EventArgs e)
		{
			inputPanel1.Enabled = true;
		}

		private void deliveryContact_GotFocus(object sender, System.EventArgs e)
		{
			inputPanel1.Enabled = true;
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			int wizardLevel = 1;
			DataItem selectedItem = null;
			DataColor selectedColor = null;

			while((wizardLevel != 5) && (wizardLevel != 0))
			{
				if (wizardLevel == 1)
				{
					ItemList itemList = new ItemList(smartDatabase);
					itemList.setSelected(selectedItem);
					itemList.ShowDialog();
					selectedItem = itemList.getSelected();
					if (itemList.getStatus() == 1) 
					{
						if (selectedItem.hasColors())
							wizardLevel = 2;
						else
							wizardLevel = 4;
					}
					else
						wizardLevel = 0;
				}

				if (wizardLevel == 2)
				{
					ColorList colorList = new ColorList(smartDatabase, selectedItem);
					colorList.setSelected(selectedColor);
					colorList.ShowDialog();
					selectedColor = colorList.getSelected();
					if (colorList.getStatus() == 1)
						wizardLevel = 3;
					else
						wizardLevel = 1;
				}

				if (wizardLevel == 3)
				{
					if (selectedItem.hasSize2())
					{
						SizeList2 sizeList = new SizeList2(smartDatabase, dataSalesHeader, selectedItem, selectedColor);
						sizeList.ShowDialog();

						if (sizeList.getStatus() == 1)
							wizardLevel = 5;
						else
							wizardLevel = 2;

					}
					else
					{
						SizeList sizeList = new SizeList(smartDatabase, dataSalesHeader, selectedItem, selectedColor);
						sizeList.ShowDialog();

						if (sizeList.getStatus() == 1)
							wizardLevel = 5;
						else
							wizardLevel = 2;
					}
				}

				if (wizardLevel == 4)
				{
					QuantityForm quantityForm = new QuantityForm(selectedItem);
					quantityForm.ShowDialog();
					if (quantityForm.getStatus() == 1)
					{
						DataSalesLine dataSalesLine = new DataSalesLine(dataSalesHeader, selectedItem, null, null, null, float.Parse(quantityForm.getValue()), smartDatabase);
						dataSalesLine.save();
						wizardLevel = 5;
					}
					else
						wizardLevel = 1;

					updateGrid();
				}

				if (wizardLevel == 5)
				{
					selectedColor = null;

					updateGrid();
				}

			}

		}

		private void updateGrid()
		{
			salesLineDataSet = dataSalesLines.getDataSet(dataSalesHeader);

			DataColumn descriptionCol = salesLineDataSet.Tables[0].Columns.Add("description");
			DataColumn baseUnitCol = salesLineDataSet.Tables[0].Columns.Add("baseUnit");

			salesLineGrid.DataSource = salesLineDataSet.Tables[0];
			
			int i = 0;
			while (i < salesLineDataSet.Tables[0].Rows.Count)
			{
				DataItem dataItem = new DataItem((string)salesLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(0), smartDatabase);
				salesLineGrid[i, 1] = dataItem.description;
				salesLineGrid[i, 4] = dataItem.baseUnit;
				i++;
			}

		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			if (salesLineDataSet.Tables[0].Rows.Count > 0)
			{
				DataItem selectedItem = new DataItem(salesLineGrid[salesLineGrid.CurrentRowIndex, 0].ToString(), smartDatabase);
				DataColor selectedColor = new DataColor(salesLineGrid[salesLineGrid.CurrentRowIndex, 2].ToString(), smartDatabase);
				DataSalesLine.deleteAll(smartDatabase, dataSalesHeader, selectedItem, selectedColor);
				updateGrid();
			}
		}

		private void deliveryCode_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (deliveryCode.Text == "Standard")
			{
				deliveryName.Text = name.Text;
				deliveryAddress.Text = address.Text;
				deliveryAddress2.Text = address2.Text;
				deliveryZipCode.Text = zipCode.Text;
				deliveryCity.Text = city.Text;
			}
			else
			{
				DataDeliveryAddress dataDeliveryAddress = new DataDeliveryAddress(customerNo.Text, deliveryCode.Text, smartDatabase);

				deliveryName.Text = dataDeliveryAddress.name;
				deliveryAddress.Text = dataDeliveryAddress.address;
				deliveryAddress2.Text = dataDeliveryAddress.address2;
				deliveryZipCode.Text = dataDeliveryAddress.zipCode;
				deliveryCity.Text = dataDeliveryAddress.city;
				deliveryContact.Text = dataDeliveryAddress.contact;
			}
		}
	
		private void checkReadyFlag()
		{
			if (dataSalesHeader.ready)
			{
				name.ReadOnly = true;
				address.ReadOnly = true;
				address2.ReadOnly = true;
				zipCode.ReadOnly = true;
				city.ReadOnly = true;
				
				deliveryCode.Enabled = false;
				deliveryName.ReadOnly = true;
				deliveryAddress.ReadOnly = true;
				deliveryAddress2.ReadOnly = true;
				deliveryZipCode.ReadOnly = true;
				deliveryCity.ReadOnly = true;
				deliveryContact.ReadOnly = true;

				button1.Enabled = false;
				button2.Enabled = false;

			}
			else
			{
				name.ReadOnly = false;
				address.ReadOnly = false;
				address2.ReadOnly = false;
				zipCode.ReadOnly = false;
				city.ReadOnly = false;
				
				deliveryCode.Enabled = true;
				deliveryName.ReadOnly = false;
				deliveryAddress.ReadOnly = false;
				deliveryAddress2.ReadOnly = false;
				deliveryZipCode.ReadOnly = false;
				deliveryCity.ReadOnly = false;
				deliveryContact.ReadOnly = false;

				button1.Enabled = true;
				button2.Enabled = true;
			}

		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			if (dataSalesHeader.ready)
			{
				dataSalesHeader.ready = false;
				button3.Text = "Klarmarkera";
			}
			else
			{
				dataSalesHeader.ready = true;
				button3.Text = "Öppna";
			}
			checkReadyFlag();

		}

		private void Order_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			save();		
		}

		private void button4_Click(object sender, System.EventArgs e)
		{
			if (salesLineGrid.BindingContext[salesLineGrid.DataSource, ""].Count > 0)
			{
			
				DataItem selectedItem = new DataItem(salesLineGrid[salesLineGrid.CurrentRowIndex, 0].ToString(), smartDatabase);

				if (selectedItem.hasColors())
				{
					DataColor selectedColor = new DataColor(salesLineGrid[salesLineGrid.CurrentRowIndex, 2].ToString(), smartDatabase);
					if (selectedItem.hasSize2())
					{
						SizeList2 sizeList = new SizeList2(smartDatabase, dataSalesHeader, selectedItem, selectedColor);
						sizeList.hideButtons();
						if (dataSalesHeader.ready) sizeList.setReadOnly();
						sizeList.ShowDialog();

					}
					else
					{
						SizeList sizeList = new SizeList(smartDatabase, dataSalesHeader, selectedItem, selectedColor);
						sizeList.hideButtons();
						if (dataSalesHeader.ready) sizeList.setReadOnly();
						sizeList.ShowDialog();
					}
				}
				else
				{
					if (dataSalesHeader.ready)
					{
						System.Windows.Forms.MessageBox.Show("Antalet går inte att ändra när ordern är klarmarkerad.");
					}
					else
					{
						QuantityForm quantityForm = new QuantityForm(selectedItem);
						quantityForm.setValue(salesLineGrid[salesLineGrid.CurrentRowIndex, 3].ToString());
						quantityForm.ShowDialog();
						if (quantityForm.getStatus() == 1)
						{
							DataSalesLine dataSalesLine = new DataSalesLine(dataSalesHeader, selectedItem, null, null, null, float.Parse(quantityForm.getValue()), smartDatabase);
							dataSalesLine.save();
						}
					}
				}
			}
			updateGrid();
		}


		private void phone_GotFocus(object sender, System.EventArgs e)
		{
			inputPanel1.Enabled = true;				
		}

		private void contact_GotFocus(object sender, System.EventArgs e)
		{
			inputPanel1.Enabled = true;		
		}



	}
}
