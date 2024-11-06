using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for CustomerInfo.
	/// </summary>
	public class CustomerInfo : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox address2Box;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox postCodeBox;
		private System.Windows.Forms.TextBox cityBox;
		private System.Windows.Forms.TextBox addressBox;
		private System.Windows.Forms.TextBox nameBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox customerNoBox;
		private System.Windows.Forms.Label orderNoLabel;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TextBox productionSiteBox;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox dairyNoBox;
		private System.Windows.Forms.TextBox dairyCodeBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TabPage tabPage1;

		private DataCustomer dataCustomer;
		private System.Windows.Forms.Button button8;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TextBox phoneNoBox;
		private System.Windows.Forms.TextBox cellPhoneNoBox;
		private System.Windows.Forms.TextBox positionXBox;
		private System.Windows.Forms.TextBox positionYBox;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.TextBox latBox;
		private System.Windows.Forms.TextBox lonBox;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.DataGrid customerShipAddressGrid;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Button button7;
		private System.Windows.Forms.Button button9;
		private System.Windows.Forms.DataGridTableStyle customerShipAddressTable;
		private System.Windows.Forms.DataGridTextBoxColumn nameCol;
		private System.Windows.Forms.DataGridTextBoxColumn addressCol;
		private System.Windows.Forms.DataGridTextBoxColumn cityCol;
		private System.Windows.Forms.TabPage tabPage5;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.TextBox directionCommentBox;
		private System.Windows.Forms.Button button10;
		private System.Windows.Forms.Button button11;
		private SmartDatabase smartDatabase;
	
		public CustomerInfo(SmartDatabase smartDatabase, DataCustomer dataCustomer)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.dataCustomer = dataCustomer;
			this.smartDatabase = smartDatabase;

			this.customerNoBox.Text = dataCustomer.no;
			this.nameBox.Text = dataCustomer.name;
			this.addressBox.Text = dataCustomer.address;
			this.address2Box.Text = dataCustomer.address2;
			this.postCodeBox.Text = dataCustomer.postCode;
			this.cityBox.Text = dataCustomer.city;

			this.dairyCodeBox.Text = dataCustomer.dairyCode;
			this.dairyNoBox.Text = dataCustomer.dairyNo;
			this.productionSiteBox.Text = dataCustomer.productionSite;
			
			this.phoneNoBox.Text = dataCustomer.phoneNo;
			this.cellPhoneNoBox.Text = dataCustomer.cellPhoneNo;

			this.positionXBox.Text = dataCustomer.positionX.ToString();
			this.positionYBox.Text = dataCustomer.positionY.ToString();

			this.directionCommentBox.Text = dataCustomer.directionComment + dataCustomer.directionComment2;

			string lat = "";
			string lon = "";

			if ((dataCustomer.positionX > 0) && (dataCustomer.positionY > 0))
			{
				NavGaussKruger gaussKruger = new NavGaussKruger("rt90_2.5_gon_v");
				double[] latLon = gaussKruger.GetWGS84(dataCustomer.positionY, dataCustomer.positionX);

				double degreesLat = (int)latLon[0];
				double minutesLat = (latLon[0] - degreesLat)*60;

				double degreesLon = (int)latLon[1];
				double minutesLon = (latLon[1] - degreesLon)*60;

				lat = degreesLat+"° "+Math.Round(minutesLat, 4)+"'";
				lon = degreesLon+"° "+Math.Round(minutesLon, 4)+"'";
			}

			this.latBox.Text = lat;
			this.lonBox.Text = lon;

			DataCustomerShipAddresses dataCustomerShipAddresses = new DataCustomerShipAddresses(smartDatabase);
			DataSet customerShipAddressDataSet = dataCustomerShipAddresses.getDataSet(this.dataCustomer.no);
			customerShipAddressGrid.DataSource = customerShipAddressDataSet.Tables[0];

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
			this.button8 = new System.Windows.Forms.Button();
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
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.customerShipAddressGrid = new System.Windows.Forms.DataGrid();
			this.label17 = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.button4 = new System.Windows.Forms.Button();
			this.cellPhoneNoBox = new System.Windows.Forms.TextBox();
			this.phoneNoBox = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.productionSiteBox = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.dairyNoBox = new System.Windows.Forms.TextBox();
			this.dairyCodeBox = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.lonBox = new System.Windows.Forms.TextBox();
			this.latBox = new System.Windows.Forms.TextBox();
			this.label16 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.button5 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.label11 = new System.Windows.Forms.Label();
			this.positionXBox = new System.Windows.Forms.TextBox();
			this.positionYBox = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.button6 = new System.Windows.Forms.Button();
			this.button7 = new System.Windows.Forms.Button();
			this.button9 = new System.Windows.Forms.Button();
			this.customerShipAddressTable = new System.Windows.Forms.DataGridTableStyle();
			this.nameCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.addressCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.cityCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.tabPage5 = new System.Windows.Forms.TabPage();
			this.label18 = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.directionCommentBox = new System.Windows.Forms.TextBox();
			this.button10 = new System.Windows.Forms.Button();
			this.button11 = new System.Windows.Forms.Button();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage5);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(322, 216);
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.button3);
			this.tabPage1.Controls.Add(this.button8);
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
			this.tabPage1.Size = new System.Drawing.Size(314, 190);
			this.tabPage1.Text = "Allmänt";
			// 
			// button3
			// 
			this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button3.Location = new System.Drawing.Point(136, 152);
			this.button3.Size = new System.Drawing.Size(80, 32);
			this.button3.Text = "Navigera";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button8
			// 
			this.button8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button8.Location = new System.Drawing.Point(224, 152);
			this.button8.Size = new System.Drawing.Size(80, 32);
			this.button8.Text = "Tillbaka";
			this.button8.Click += new System.EventHandler(this.button8_Click);
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
			this.address2Box.Size = new System.Drawing.Size(224, 20);
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
			this.label2.Text = "Postadress:";
			// 
			// postCodeBox
			// 
			this.postCodeBox.Location = new System.Drawing.Point(80, 128);
			this.postCodeBox.ReadOnly = true;
			this.postCodeBox.Size = new System.Drawing.Size(48, 20);
			this.postCodeBox.Text = "";
			// 
			// cityBox
			// 
			this.cityBox.Location = new System.Drawing.Point(136, 128);
			this.cityBox.ReadOnly = true;
			this.cityBox.Size = new System.Drawing.Size(168, 20);
			this.cityBox.Text = "";
			// 
			// addressBox
			// 
			this.addressBox.Location = new System.Drawing.Point(80, 80);
			this.addressBox.ReadOnly = true;
			this.addressBox.Size = new System.Drawing.Size(224, 20);
			this.addressBox.Text = "";
			// 
			// nameBox
			// 
			this.nameBox.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.nameBox.Location = new System.Drawing.Point(80, 56);
			this.nameBox.ReadOnly = true;
			this.nameBox.Size = new System.Drawing.Size(224, 20);
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
			this.customerNoBox.Size = new System.Drawing.Size(224, 20);
			this.customerNoBox.Text = "";
			// 
			// orderNoLabel
			// 
			this.orderNoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.orderNoLabel.Location = new System.Drawing.Point(5, 3);
			this.orderNoLabel.Size = new System.Drawing.Size(219, 20);
			this.orderNoLabel.Text = "Kundinformation";
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.Add(this.button6);
			this.tabPage4.Controls.Add(this.button7);
			this.tabPage4.Controls.Add(this.button9);
			this.tabPage4.Controls.Add(this.customerShipAddressGrid);
			this.tabPage4.Controls.Add(this.label17);
			this.tabPage4.Location = new System.Drawing.Point(4, 4);
			this.tabPage4.Size = new System.Drawing.Size(314, 190);
			this.tabPage4.Text = "Gårdar";
			// 
			// customerShipAddressGrid
			// 
			this.customerShipAddressGrid.Location = new System.Drawing.Point(8, 32);
			this.customerShipAddressGrid.Size = new System.Drawing.Size(298, 112);
			this.customerShipAddressGrid.TableStyles.Add(this.customerShipAddressTable);
			// 
			// label17
			// 
			this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.label17.Location = new System.Drawing.Point(5, 3);
			this.label17.Size = new System.Drawing.Size(219, 20);
			this.label17.Text = "Kundinformation";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.button4);
			this.tabPage2.Controls.Add(this.cellPhoneNoBox);
			this.tabPage2.Controls.Add(this.phoneNoBox);
			this.tabPage2.Controls.Add(this.label14);
			this.tabPage2.Controls.Add(this.label13);
			this.tabPage2.Controls.Add(this.button2);
			this.tabPage2.Controls.Add(this.label6);
			this.tabPage2.Controls.Add(this.productionSiteBox);
			this.tabPage2.Controls.Add(this.label8);
			this.tabPage2.Controls.Add(this.dairyNoBox);
			this.tabPage2.Controls.Add(this.dairyCodeBox);
			this.tabPage2.Controls.Add(this.label5);
			this.tabPage2.Controls.Add(this.label7);
			this.tabPage2.Location = new System.Drawing.Point(4, 4);
			this.tabPage2.Size = new System.Drawing.Size(314, 190);
			this.tabPage2.Text = "Uppgifter";
			// 
			// button4
			// 
			this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button4.Location = new System.Drawing.Point(136, 152);
			this.button4.Size = new System.Drawing.Size(80, 32);
			this.button4.Text = "Navigera";
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// cellPhoneNoBox
			// 
			this.cellPhoneNoBox.Location = new System.Drawing.Point(80, 128);
			this.cellPhoneNoBox.ReadOnly = true;
			this.cellPhoneNoBox.Size = new System.Drawing.Size(224, 20);
			this.cellPhoneNoBox.Text = "";
			// 
			// phoneNoBox
			// 
			this.phoneNoBox.Location = new System.Drawing.Point(80, 104);
			this.phoneNoBox.ReadOnly = true;
			this.phoneNoBox.Size = new System.Drawing.Size(224, 20);
			this.phoneNoBox.Text = "";
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(8, 131);
			this.label14.Size = new System.Drawing.Size(64, 20);
			this.label14.Text = "Mobiltel. nr:";
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(8, 107);
			this.label13.Size = new System.Drawing.Size(64, 20);
			this.label13.Text = "Telefonnr:";
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button2.Location = new System.Drawing.Point(224, 152);
			this.button2.Size = new System.Drawing.Size(80, 32);
			this.button2.Text = "Tillbaka";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.label6.Location = new System.Drawing.Point(5, 3);
			this.label6.Size = new System.Drawing.Size(219, 20);
			this.label6.Text = "Kundinformation";
			// 
			// productionSiteBox
			// 
			this.productionSiteBox.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.productionSiteBox.Location = new System.Drawing.Point(80, 32);
			this.productionSiteBox.ReadOnly = true;
			this.productionSiteBox.Size = new System.Drawing.Size(224, 20);
			this.productionSiteBox.Text = "";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(8, 83);
			this.label8.Size = new System.Drawing.Size(64, 20);
			this.label8.Text = "Mejerinr:";
			// 
			// dairyNoBox
			// 
			this.dairyNoBox.Location = new System.Drawing.Point(80, 80);
			this.dairyNoBox.ReadOnly = true;
			this.dairyNoBox.Size = new System.Drawing.Size(224, 20);
			this.dairyNoBox.Text = "";
			// 
			// dairyCodeBox
			// 
			this.dairyCodeBox.Location = new System.Drawing.Point(80, 56);
			this.dairyCodeBox.ReadOnly = true;
			this.dairyCodeBox.Size = new System.Drawing.Size(224, 20);
			this.dairyCodeBox.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 59);
			this.label5.Size = new System.Drawing.Size(64, 20);
			this.label5.Text = "Mejerikod:";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(8, 35);
			this.label7.Size = new System.Drawing.Size(72, 20);
			this.label7.Text = "Prod. platsnr:";
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.lonBox);
			this.tabPage3.Controls.Add(this.latBox);
			this.tabPage3.Controls.Add(this.label16);
			this.tabPage3.Controls.Add(this.label15);
			this.tabPage3.Controls.Add(this.button5);
			this.tabPage3.Controls.Add(this.button1);
			this.tabPage3.Controls.Add(this.label11);
			this.tabPage3.Controls.Add(this.positionXBox);
			this.tabPage3.Controls.Add(this.positionYBox);
			this.tabPage3.Controls.Add(this.label12);
			this.tabPage3.Controls.Add(this.label9);
			this.tabPage3.Location = new System.Drawing.Point(4, 4);
			this.tabPage3.Size = new System.Drawing.Size(314, 190);
			this.tabPage3.Text = "Position";
			// 
			// lonBox
			// 
			this.lonBox.Location = new System.Drawing.Point(80, 104);
			this.lonBox.ReadOnly = true;
			this.lonBox.Size = new System.Drawing.Size(224, 20);
			this.lonBox.Text = "";
			// 
			// latBox
			// 
			this.latBox.Location = new System.Drawing.Point(80, 80);
			this.latBox.ReadOnly = true;
			this.latBox.Size = new System.Drawing.Size(224, 20);
			this.latBox.Text = "";
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(8, 107);
			this.label16.Size = new System.Drawing.Size(64, 20);
			this.label16.Text = "Lon:";
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(8, 83);
			this.label15.Size = new System.Drawing.Size(64, 20);
			this.label15.Text = "Lat:";
			// 
			// button5
			// 
			this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button5.Location = new System.Drawing.Point(136, 152);
			this.button5.Size = new System.Drawing.Size(80, 32);
			this.button5.Text = "Navigera";
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button1.Location = new System.Drawing.Point(224, 152);
			this.button1.Size = new System.Drawing.Size(80, 32);
			this.button1.Text = "Tillbaka";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(8, 59);
			this.label11.Size = new System.Drawing.Size(64, 20);
			this.label11.Text = "Y:";
			// 
			// positionXBox
			// 
			this.positionXBox.Location = new System.Drawing.Point(80, 56);
			this.positionXBox.ReadOnly = true;
			this.positionXBox.Size = new System.Drawing.Size(224, 20);
			this.positionXBox.Text = "";
			// 
			// positionYBox
			// 
			this.positionYBox.Location = new System.Drawing.Point(80, 32);
			this.positionYBox.ReadOnly = true;
			this.positionYBox.Size = new System.Drawing.Size(224, 20);
			this.positionYBox.Text = "";
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(8, 35);
			this.label12.Size = new System.Drawing.Size(64, 20);
			this.label12.Text = "X:";
			// 
			// label9
			// 
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.label9.Location = new System.Drawing.Point(5, 3);
			this.label9.Size = new System.Drawing.Size(219, 20);
			this.label9.Text = "Kundinformation";
			// 
			// button6
			// 
			this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button6.Location = new System.Drawing.Point(232, 152);
			this.button6.Size = new System.Drawing.Size(72, 32);
			this.button6.Text = "Info";
			this.button6.Click += new System.EventHandler(this.button6_Click);
			// 
			// button7
			// 
			this.button7.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
			this.button7.Location = new System.Drawing.Point(48, 152);
			this.button7.Size = new System.Drawing.Size(32, 32);
			this.button7.Text = ">";
			this.button7.Click += new System.EventHandler(this.button7_Click);
			// 
			// button9
			// 
			this.button9.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
			this.button9.Location = new System.Drawing.Point(8, 152);
			this.button9.Size = new System.Drawing.Size(32, 32);
			this.button9.Text = "<";
			this.button9.Click += new System.EventHandler(this.button9_Click);
			// 
			// customerShipAddressTable
			// 
			this.customerShipAddressTable.GridColumnStyles.Add(this.nameCol);
			this.customerShipAddressTable.GridColumnStyles.Add(this.addressCol);
			this.customerShipAddressTable.GridColumnStyles.Add(this.cityCol);
			this.customerShipAddressTable.MappingName = "customerShipAddress";
			// 
			// nameCol
			// 
			this.nameCol.HeaderText = "Namn";
			this.nameCol.MappingName = "name";
			this.nameCol.NullText = "";
			this.nameCol.Width = 100;
			// 
			// addressCol
			// 
			this.addressCol.HeaderText = "Adress";
			this.addressCol.MappingName = "address";
			this.addressCol.NullText = "";
			this.addressCol.Width = 100;
			// 
			// cityCol
			// 
			this.cityCol.HeaderText = "Ort";
			this.cityCol.MappingName = "city";
			this.cityCol.NullText = "";
			this.cityCol.Width = 100;
			// 
			// tabPage5
			// 
			this.tabPage5.Controls.Add(this.button10);
			this.tabPage5.Controls.Add(this.button11);
			this.tabPage5.Controls.Add(this.directionCommentBox);
			this.tabPage5.Controls.Add(this.label19);
			this.tabPage5.Controls.Add(this.label18);
			this.tabPage5.Location = new System.Drawing.Point(4, 4);
			this.tabPage5.Size = new System.Drawing.Size(314, 190);
			this.tabPage5.Text = "Vägbeskrivning";
			// 
			// label18
			// 
			this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.label18.Location = new System.Drawing.Point(5, 3);
			this.label18.Size = new System.Drawing.Size(219, 20);
			this.label18.Text = "Kundinformation";
			// 
			// label19
			// 
			this.label19.Location = new System.Drawing.Point(8, 35);
			this.label19.Size = new System.Drawing.Size(120, 20);
			this.label19.Text = "Vägbeskrivning:";
			// 
			// directionCommentBox
			// 
			this.directionCommentBox.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.directionCommentBox.Location = new System.Drawing.Point(8, 56);
			this.directionCommentBox.Multiline = true;
			this.directionCommentBox.ReadOnly = true;
			this.directionCommentBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.directionCommentBox.Size = new System.Drawing.Size(296, 80);
			this.directionCommentBox.Text = "";
			// 
			// button10
			// 
			this.button10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button10.Location = new System.Drawing.Point(136, 152);
			this.button10.Size = new System.Drawing.Size(80, 32);
			this.button10.Text = "Navigera";
			this.button10.Click += new System.EventHandler(this.button10_Click);
			// 
			// button11
			// 
			this.button11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button11.Location = new System.Drawing.Point(224, 152);
			this.button11.Size = new System.Drawing.Size(80, 32);
			this.button11.Text = "Tillbaka";
			this.button11.Click += new System.EventHandler(this.button11_Click);
			// 
			// CustomerInfo
			// 
			this.ClientSize = new System.Drawing.Size(322, 216);
			this.Controls.Add(this.tabControl1);
			this.Text = "Kundinformation";

		}
		#endregion

		private void button8_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			Navigator navigator = new Navigator(smartDatabase);
			navigator.navigate(dataCustomer.positionY, dataCustomer.positionX, dataCustomer.name);
		}

		private void button4_Click(object sender, System.EventArgs e)
		{
			Navigator navigator = new Navigator(smartDatabase);
			navigator.navigate(dataCustomer.positionY, dataCustomer.positionX, dataCustomer.name);
		}

		private void button5_Click(object sender, System.EventArgs e)
		{
			Navigator navigator = new Navigator(smartDatabase);
			navigator.navigate(dataCustomer.positionY, dataCustomer.positionX, dataCustomer.name);

		}

		private void button9_Click(object sender, System.EventArgs e)
		{
			if ((this.customerShipAddressGrid.DataSource != null) && (((DataTable)this.customerShipAddressGrid.DataSource).Rows.Count > 0))
			{
				if (this.customerShipAddressGrid.CurrentRowIndex > 0)
				{
					this.customerShipAddressGrid.CurrentRowIndex = this.customerShipAddressGrid.CurrentRowIndex - 1;
					this.customerShipAddressGrid.Select(this.customerShipAddressGrid.CurrentRowIndex);
				}
			}
		
		}

		private void button7_Click(object sender, System.EventArgs e)
		{
			if ((this.customerShipAddressGrid.DataSource != null) && (((DataTable)this.customerShipAddressGrid.DataSource).Rows.Count > 0))
			{
				if (this.customerShipAddressGrid.CurrentRowIndex < (((DataTable)this.customerShipAddressGrid.DataSource).Rows.Count -1))
				{
					this.customerShipAddressGrid.CurrentRowIndex = this.customerShipAddressGrid.CurrentRowIndex + 1;
					this.customerShipAddressGrid.Select(this.customerShipAddressGrid.CurrentRowIndex);
				}
			}

		}

		private void button10_Click(object sender, System.EventArgs e)
		{
			Navigator navigator = new Navigator(smartDatabase);
			navigator.navigate(dataCustomer.positionY, dataCustomer.positionX, dataCustomer.name);

		}

		private void button11_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void button6_Click(object sender, System.EventArgs e)
		{
			if (customerShipAddressGrid.DataSource != null)
			{
				DataTable customerShipAddressTable = (DataTable)customerShipAddressGrid.DataSource;
				if (customerShipAddressTable.Rows.Count > 0)
				{
					DataCustomerShipAddress dataCustomerShipAddress = new DataCustomerShipAddress(smartDatabase, int.Parse(customerShipAddressTable.Rows[customerShipAddressGrid.CurrentRowIndex].ItemArray.GetValue(0).ToString()));
					CustomerShipAddressInfo customerShipAddressInfo = new CustomerShipAddressInfo(smartDatabase, dataCustomerShipAddress);
					customerShipAddressInfo.ShowDialog();
					customerShipAddressInfo.Dispose();
				}
			}

		}
	}
}
