using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for CustomerShipAddressInfo1.
	/// </summary>
	public class CustomerShipAddressInfo : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button8;
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
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.TextBox phoneNoBox;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox productionSiteBox;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TabPage tabPage1;

		private SmartDatabase smartDatabase;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.TextBox lonBox;
		private System.Windows.Forms.TextBox latBox;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox positionXBox;
		private System.Windows.Forms.TextBox positionYBox;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox directionCommentBox;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Button button7;
		private DataCustomerShipAddress dataCustomerShipAddress;
	
		public CustomerShipAddressInfo(SmartDatabase smartDatabase, DataCustomerShipAddress dataCustomerShipAddress)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			this.dataCustomerShipAddress = dataCustomerShipAddress;
			this.smartDatabase = smartDatabase;

			this.customerNoBox.Text = dataCustomerShipAddress.customerNo;
			this.nameBox.Text = dataCustomerShipAddress.name;
			this.addressBox.Text = dataCustomerShipAddress.address;
			this.address2Box.Text = dataCustomerShipAddress.address2;
			this.postCodeBox.Text = dataCustomerShipAddress.postCode;
			this.cityBox.Text = dataCustomerShipAddress.city;

			this.productionSiteBox.Text = dataCustomerShipAddress.productionSite;
			
			this.phoneNoBox.Text = dataCustomerShipAddress.phoneNo;

			this.positionXBox.Text = dataCustomerShipAddress.positionX.ToString();
			this.positionYBox.Text = dataCustomerShipAddress.positionY.ToString();

			this.directionCommentBox.Text = dataCustomerShipAddress.directionComment + dataCustomerShipAddress.directionComment2;

			string lat = "";
			string lon = "";

			if ((dataCustomerShipAddress.positionX > 0) && (dataCustomerShipAddress.positionY > 0))
			{
				NavGaussKruger gaussKruger = new NavGaussKruger("rt90_2.5_gon_v");
				double[] latLon = gaussKruger.GetWGS84(dataCustomerShipAddress.positionY, dataCustomerShipAddress.positionX);

				double degreesLat = (int)latLon[0];
				double minutesLat = (latLon[0] - degreesLat)*60;

				double degreesLon = (int)latLon[1];
				double minutesLon = (latLon[1] - degreesLon)*60;

				lat = degreesLat+"° "+Math.Round(minutesLat, 4)+"'";
				lon = degreesLon+"° "+Math.Round(minutesLon, 4)+"'";
			}

			this.latBox.Text = lat;
			this.lonBox.Text = lon;

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
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.tabPage3 = new System.Windows.Forms.TabPage();
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
			this.button4 = new System.Windows.Forms.Button();
			this.phoneNoBox = new System.Windows.Forms.TextBox();
			this.label13 = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.productionSiteBox = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.tabPage4 = new System.Windows.Forms.TabPage();
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
			this.label8 = new System.Windows.Forms.Label();
			this.directionCommentBox = new System.Windows.Forms.TextBox();
			this.button6 = new System.Windows.Forms.Button();
			this.button7 = new System.Windows.Forms.Button();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage4);
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
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.button6);
			this.tabPage2.Controls.Add(this.button7);
			this.tabPage2.Controls.Add(this.directionCommentBox);
			this.tabPage2.Controls.Add(this.label8);
			this.tabPage2.Controls.Add(this.label5);
			this.tabPage2.Location = new System.Drawing.Point(4, 4);
			this.tabPage2.Size = new System.Drawing.Size(314, 190);
			this.tabPage2.Text = "Vägbeskrivning";
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.button4);
			this.tabPage3.Controls.Add(this.phoneNoBox);
			this.tabPage3.Controls.Add(this.label13);
			this.tabPage3.Controls.Add(this.button2);
			this.tabPage3.Controls.Add(this.label6);
			this.tabPage3.Controls.Add(this.productionSiteBox);
			this.tabPage3.Controls.Add(this.label7);
			this.tabPage3.Location = new System.Drawing.Point(4, 4);
			this.tabPage3.Size = new System.Drawing.Size(314, 190);
			this.tabPage3.Text = "Uppgifter";
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
			this.orderNoLabel.Text = "Gårdsinformation";
			// 
			// button4
			// 
			this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button4.Location = new System.Drawing.Point(136, 152);
			this.button4.Size = new System.Drawing.Size(80, 32);
			this.button4.Text = "Navigera";
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// phoneNoBox
			// 
			this.phoneNoBox.Location = new System.Drawing.Point(80, 104);
			this.phoneNoBox.ReadOnly = true;
			this.phoneNoBox.Size = new System.Drawing.Size(224, 20);
			this.phoneNoBox.Text = "";
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
			this.label6.Text = "Gårdsinformation";
			// 
			// productionSiteBox
			// 
			this.productionSiteBox.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.productionSiteBox.Location = new System.Drawing.Point(80, 32);
			this.productionSiteBox.ReadOnly = true;
			this.productionSiteBox.Size = new System.Drawing.Size(224, 20);
			this.productionSiteBox.Text = "";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(8, 35);
			this.label7.Size = new System.Drawing.Size(72, 20);
			this.label7.Text = "Prod. platsnr:";
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.label5.Location = new System.Drawing.Point(5, 3);
			this.label5.Size = new System.Drawing.Size(219, 20);
			this.label5.Text = "Gårdsinformation";
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.Add(this.lonBox);
			this.tabPage4.Controls.Add(this.latBox);
			this.tabPage4.Controls.Add(this.label16);
			this.tabPage4.Controls.Add(this.label15);
			this.tabPage4.Controls.Add(this.button5);
			this.tabPage4.Controls.Add(this.button1);
			this.tabPage4.Controls.Add(this.label11);
			this.tabPage4.Controls.Add(this.positionXBox);
			this.tabPage4.Controls.Add(this.positionYBox);
			this.tabPage4.Controls.Add(this.label12);
			this.tabPage4.Controls.Add(this.label9);
			this.tabPage4.Location = new System.Drawing.Point(4, 4);
			this.tabPage4.Size = new System.Drawing.Size(314, 190);
			this.tabPage4.Text = "Position";
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
			this.label9.Text = "Gårdsinformation";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(8, 35);
			this.label8.Size = new System.Drawing.Size(88, 20);
			this.label8.Text = "Vägbeskrivning:";
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
			// button6
			// 
			this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button6.Location = new System.Drawing.Point(136, 152);
			this.button6.Size = new System.Drawing.Size(80, 32);
			this.button6.Text = "Navigera";
			this.button6.Click += new System.EventHandler(this.button6_Click);
			// 
			// button7
			// 
			this.button7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button7.Location = new System.Drawing.Point(224, 152);
			this.button7.Size = new System.Drawing.Size(80, 32);
			this.button7.Text = "Tillbaka";
			this.button7.Click += new System.EventHandler(this.button7_Click);
			// 
			// CustomerShipAddressInfo
			// 
			this.ClientSize = new System.Drawing.Size(322, 216);
			this.Controls.Add(this.tabControl1);
			this.Text = "Gårdsinformation";

		}
		#endregion

		private void button3_Click(object sender, System.EventArgs e)
		{
			Navigator navigator = new Navigator(smartDatabase);
			navigator.navigate(dataCustomerShipAddress.positionY, dataCustomerShipAddress.positionX, dataCustomerShipAddress.name);

		}

		private void button4_Click(object sender, System.EventArgs e)
		{
			Navigator navigator = new Navigator(smartDatabase);
			navigator.navigate(dataCustomerShipAddress.positionY, dataCustomerShipAddress.positionX, dataCustomerShipAddress.name);

		}

		private void button6_Click(object sender, System.EventArgs e)
		{
			Navigator navigator = new Navigator(smartDatabase);
			navigator.navigate(dataCustomerShipAddress.positionY, dataCustomerShipAddress.positionX, dataCustomerShipAddress.name);

		}

		private void button5_Click(object sender, System.EventArgs e)
		{
			Navigator navigator = new Navigator(smartDatabase);
			navigator.navigate(dataCustomerShipAddress.positionY, dataCustomerShipAddress.positionX, dataCustomerShipAddress.name);

		}

		private void button8_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void button7_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
