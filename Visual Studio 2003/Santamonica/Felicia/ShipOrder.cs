using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for ShipmentOrder.
	/// </summary>
	public class ShipOrder : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label headerLabel;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox cityBox;
		private System.Windows.Forms.TextBox addressBox;
		private System.Windows.Forms.TextBox nameBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox phoneNoBox;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox cellPhoneNoBox;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox detailsBox;
		private System.Windows.Forms.TextBox commentBox;
		private System.Windows.Forms.Label headerLabel2;
		private System.Windows.Forms.Label headerLabel3;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Button button7;
		private System.Windows.Forms.Button button8;
		private System.Windows.Forms.Button button9;
		private System.Windows.Forms.Button button10;
		private System.Windows.Forms.Button button11;
		private System.Windows.Forms.Button button12;
	
		private DataShipOrder dataShipOrder;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox details2Box;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox shipNameBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TabPage tabPage5;
		private System.Windows.Forms.Button button13;
		private System.Windows.Forms.Button button14;
		private System.Windows.Forms.Button button15;
		private System.Windows.Forms.Button button16;
		private System.Windows.Forms.Button button17;
		private System.Windows.Forms.Button button18;
		private System.Windows.Forms.Label headerLabel4;
		private System.Windows.Forms.TextBox priorityBox;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Button button19;
		private System.Windows.Forms.Button button20;
		private System.Windows.Forms.Button button21;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox directionBox;
		private SmartDatabase smartDatabase;
		private System.Windows.Forms.TextBox comments2Box;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Button button22;
		private System.Windows.Forms.Button button23;
		private System.Windows.Forms.Button button24;
		private System.Windows.Forms.TabPage tabPage6;
		private System.Windows.Forms.Label headerLabel5;
		private System.Windows.Forms.Button button25;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TextBox agentBox;
		private System.Windows.Forms.Button button26;
		private System.Windows.Forms.Button button27;
		private System.Windows.Forms.Button button28;
		private System.Windows.Forms.Button button29;
		private System.Windows.Forms.Button button30;
		private System.Windows.Forms.Label headerLabel6;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Button button31;
		private System.Windows.Forms.TextBox dateBox;
		private Status agentStatus;


		public ShipOrder(SmartDatabase smartDatabase, DataShipOrder dataShipOrder, Status agentStatus)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.dataShipOrder = dataShipOrder;
			this.smartDatabase = smartDatabase;
			this.agentStatus = agentStatus;

			this.dateBox.Text = dataShipOrder.creationDate.ToString("yyyy-MM-dd");
			this.nameBox.Text = dataShipOrder.customerName;
			this.shipNameBox.Text = dataShipOrder.shipName;
			this.addressBox.Text = dataShipOrder.shipAddress;
			this.cityBox.Text = dataShipOrder.shipCity;
			this.phoneNoBox.Text = dataShipOrder.phoneNo;
			this.cellPhoneNoBox.Text = dataShipOrder.cellPhoneNo;
			this.detailsBox.Text = dataShipOrder.details;
			this.details2Box.Text = dataShipOrder.details;
			this.commentBox.Text = dataShipOrder.comments;
			this.directionBox.Text = dataShipOrder.directionComment+dataShipOrder.directionComment2;
			this.comments2Box.Text = dataShipOrder.comments;

			this.agentBox.Text = dataShipOrder.agentCode;			

			headerLabel.Text = "Hämtorder "+dataShipOrder.organizationNo+""+dataShipOrder.entryNo;
			headerLabel2.Text = "Hämtorder "+dataShipOrder.organizationNo+""+dataShipOrder.entryNo;
			headerLabel3.Text = "Hämtorder "+dataShipOrder.organizationNo+""+dataShipOrder.entryNo;
			headerLabel4.Text = "Hämtorder "+dataShipOrder.organizationNo+""+dataShipOrder.entryNo;
			headerLabel5.Text = "Hämtorder "+dataShipOrder.organizationNo+""+dataShipOrder.entryNo;
			headerLabel6.Text = "Hämtorder "+dataShipOrder.organizationNo+""+dataShipOrder.entryNo;

			setPriority(dataShipOrder.priority);
		

			if (dataShipOrder.status == 6)
			{
				button1.Visible = false;
				button2.Visible = false;
				button3.Visible = false;
				//button4.Visible = false;

				//button9.Visible = false;
				button10.Visible = false;
				button11.Visible = false;
				button12.Visible = false;

				//button5.Visible = false;
				button6.Visible = false;
				button7.Visible = false;
				button8.Visible = false;

				button25.Visible = false;
				button27.Visible = false;

			}

			if (dataShipOrder.status == 20)
			{
				button1.Visible = false;
				button2.Visible = false;
				button3.Visible = false;
				button4.Visible = false;

				button9.Visible = false;
				button10.Visible = false;
				button11.Visible = false;
				button12.Visible = false;

				button5.Visible = false;
				button6.Visible = false;
				button7.Visible = false;
				button8.Visible = false;

				button25.Visible = false;
				button27.Visible = false;

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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.button22 = new System.Windows.Forms.Button();
			this.label13 = new System.Windows.Forms.Label();
			this.dateBox = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.comments2Box = new System.Windows.Forms.TextBox();
			this.button19 = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.shipNameBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.details2Box = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.button4 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.headerLabel = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.cityBox = new System.Windows.Forms.TextBox();
			this.addressBox = new System.Windows.Forms.TextBox();
			this.nameBox = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.button24 = new System.Windows.Forms.Button();
			this.directionBox = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.button21 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.button6 = new System.Windows.Forms.Button();
			this.button7 = new System.Windows.Forms.Button();
			this.button8 = new System.Windows.Forms.Button();
			this.label7 = new System.Windows.Forms.Label();
			this.phoneNoBox = new System.Windows.Forms.TextBox();
			this.headerLabel2 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.cellPhoneNoBox = new System.Windows.Forms.TextBox();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.button23 = new System.Windows.Forms.Button();
			this.button20 = new System.Windows.Forms.Button();
			this.button9 = new System.Windows.Forms.Button();
			this.button10 = new System.Windows.Forms.Button();
			this.button11 = new System.Windows.Forms.Button();
			this.button12 = new System.Windows.Forms.Button();
			this.label10 = new System.Windows.Forms.Label();
			this.detailsBox = new System.Windows.Forms.TextBox();
			this.headerLabel3 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.commentBox = new System.Windows.Forms.TextBox();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.button31 = new System.Windows.Forms.Button();
			this.label16 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.headerLabel6 = new System.Windows.Forms.Label();
			this.tabPage5 = new System.Windows.Forms.TabPage();
			this.button30 = new System.Windows.Forms.Button();
			this.button29 = new System.Windows.Forms.Button();
			this.button28 = new System.Windows.Forms.Button();
			this.label8 = new System.Windows.Forms.Label();
			this.priorityBox = new System.Windows.Forms.TextBox();
			this.button18 = new System.Windows.Forms.Button();
			this.button17 = new System.Windows.Forms.Button();
			this.button16 = new System.Windows.Forms.Button();
			this.button15 = new System.Windows.Forms.Button();
			this.button14 = new System.Windows.Forms.Button();
			this.button13 = new System.Windows.Forms.Button();
			this.headerLabel4 = new System.Windows.Forms.Label();
			this.tabPage6 = new System.Windows.Forms.TabPage();
			this.button27 = new System.Windows.Forms.Button();
			this.button26 = new System.Windows.Forms.Button();
			this.button25 = new System.Windows.Forms.Button();
			this.label14 = new System.Windows.Forms.Label();
			this.agentBox = new System.Windows.Forms.TextBox();
			this.headerLabel5 = new System.Windows.Forms.Label();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Controls.Add(this.tabPage5);
			this.tabControl1.Controls.Add(this.tabPage6);
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(322, 214);
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.button22);
			this.tabPage1.Controls.Add(this.dateBox);
			this.tabPage1.Controls.Add(this.label6);
			this.tabPage1.Controls.Add(this.comments2Box);
			this.tabPage1.Controls.Add(this.button19);
			this.tabPage1.Controls.Add(this.label5);
			this.tabPage1.Controls.Add(this.shipNameBox);
			this.tabPage1.Controls.Add(this.label2);
			this.tabPage1.Controls.Add(this.details2Box);
			this.tabPage1.Controls.Add(this.label1);
			this.tabPage1.Controls.Add(this.button4);
			this.tabPage1.Controls.Add(this.button3);
			this.tabPage1.Controls.Add(this.button2);
			this.tabPage1.Controls.Add(this.button1);
			this.tabPage1.Controls.Add(this.headerLabel);
			this.tabPage1.Controls.Add(this.label3);
			this.tabPage1.Controls.Add(this.cityBox);
			this.tabPage1.Controls.Add(this.addressBox);
			this.tabPage1.Controls.Add(this.nameBox);
			this.tabPage1.Controls.Add(this.label4);
			this.tabPage1.Controls.Add(this.label13);
			this.tabPage1.Location = new System.Drawing.Point(4, 4);
			this.tabPage1.Size = new System.Drawing.Size(314, 188);
			this.tabPage1.Text = "Kund";
			// 
			// button22
			// 
			this.button22.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button22.Location = new System.Drawing.Point(240, 160);
			this.button22.Size = new System.Drawing.Size(72, 24);
			this.button22.Text = "Tillbaka";
			this.button22.Click += new System.EventHandler(this.button22_Click);
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(8, 27);
			this.label13.Size = new System.Drawing.Size(80, 20);
			this.label13.Text = "Anm. datum:";
			// 
			// dateBox
			// 
			this.dateBox.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.dateBox.Location = new System.Drawing.Point(80, 24);
			this.dateBox.ReadOnly = true;
			this.dateBox.Size = new System.Drawing.Size(152, 20);
			this.dateBox.Text = "";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 171);
			this.label6.Size = new System.Drawing.Size(64, 20);
			this.label6.Text = "Kommentar:";
			// 
			// comments2Box
			// 
			this.comments2Box.Location = new System.Drawing.Point(80, 168);
			this.comments2Box.ReadOnly = true;
			this.comments2Box.Size = new System.Drawing.Size(152, 20);
			this.comments2Box.Text = "";
			// 
			// button19
			// 
			this.button19.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button19.Location = new System.Drawing.Point(240, 2);
			this.button19.Size = new System.Drawing.Size(72, 24);
			this.button19.Text = "Skriv ut";
			this.button19.Click += new System.EventHandler(this.button19_Click);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 75);
			this.label5.Size = new System.Drawing.Size(72, 20);
			this.label5.Text = "Gårdsnamn:";
			// 
			// shipNameBox
			// 
			this.shipNameBox.Location = new System.Drawing.Point(80, 72);
			this.shipNameBox.ReadOnly = true;
			this.shipNameBox.Size = new System.Drawing.Size(152, 20);
			this.shipNameBox.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 147);
			this.label2.Size = new System.Drawing.Size(64, 20);
			this.label2.Text = "Innehåll:";
			// 
			// details2Box
			// 
			this.details2Box.Location = new System.Drawing.Point(80, 144);
			this.details2Box.ReadOnly = true;
			this.details2Box.Size = new System.Drawing.Size(152, 20);
			this.details2Box.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 123);
			this.label1.Size = new System.Drawing.Size(64, 20);
			this.label1.Text = "Ort:";
			// 
			// button4
			// 
			this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button4.Location = new System.Drawing.Point(240, 128);
			this.button4.Size = new System.Drawing.Size(72, 24);
			this.button4.Text = "Lastat";
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// button3
			// 
			this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button3.Location = new System.Drawing.Point(240, 64);
			this.button3.Size = new System.Drawing.Size(72, 24);
			this.button3.Text = "Nej tack";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button2.Location = new System.Drawing.Point(240, 96);
			this.button2.Size = new System.Drawing.Size(72, 24);
			this.button2.Text = "Kanske";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button1.Location = new System.Drawing.Point(240, 32);
			this.button1.Size = new System.Drawing.Size(72, 24);
			this.button1.Text = "Ja tack";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// headerLabel
			// 
			this.headerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.headerLabel.Location = new System.Drawing.Point(5, 3);
			this.headerLabel.Size = new System.Drawing.Size(227, 20);
			this.headerLabel.Text = "Hämtorder";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 51);
			this.label3.Size = new System.Drawing.Size(64, 20);
			this.label3.Text = "Namn:";
			// 
			// cityBox
			// 
			this.cityBox.Location = new System.Drawing.Point(80, 120);
			this.cityBox.ReadOnly = true;
			this.cityBox.Size = new System.Drawing.Size(152, 20);
			this.cityBox.Text = "";
			// 
			// addressBox
			// 
			this.addressBox.Location = new System.Drawing.Point(80, 96);
			this.addressBox.ReadOnly = true;
			this.addressBox.Size = new System.Drawing.Size(152, 20);
			this.addressBox.Text = "";
			// 
			// nameBox
			// 
			this.nameBox.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.nameBox.Location = new System.Drawing.Point(80, 48);
			this.nameBox.ReadOnly = true;
			this.nameBox.Size = new System.Drawing.Size(152, 20);
			this.nameBox.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 99);
			this.label4.Size = new System.Drawing.Size(64, 20);
			this.label4.Text = "Adress:";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.button24);
			this.tabPage2.Controls.Add(this.directionBox);
			this.tabPage2.Controls.Add(this.label11);
			this.tabPage2.Controls.Add(this.button21);
			this.tabPage2.Controls.Add(this.button5);
			this.tabPage2.Controls.Add(this.button6);
			this.tabPage2.Controls.Add(this.button7);
			this.tabPage2.Controls.Add(this.button8);
			this.tabPage2.Controls.Add(this.label7);
			this.tabPage2.Controls.Add(this.phoneNoBox);
			this.tabPage2.Controls.Add(this.headerLabel2);
			this.tabPage2.Controls.Add(this.label9);
			this.tabPage2.Controls.Add(this.cellPhoneNoBox);
			this.tabPage2.Location = new System.Drawing.Point(4, 4);
			this.tabPage2.Size = new System.Drawing.Size(314, 188);
			this.tabPage2.Text = "Kontakt";
			// 
			// button24
			// 
			this.button24.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button24.Location = new System.Drawing.Point(240, 160);
			this.button24.Size = new System.Drawing.Size(72, 24);
			this.button24.Text = "Tillbaka";
			this.button24.Click += new System.EventHandler(this.button24_Click);
			// 
			// directionBox
			// 
			this.directionBox.Location = new System.Drawing.Point(8, 96);
			this.directionBox.Multiline = true;
			this.directionBox.ReadOnly = true;
			this.directionBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.directionBox.Size = new System.Drawing.Size(224, 80);
			this.directionBox.Text = "";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(8, 80);
			this.label11.Size = new System.Drawing.Size(128, 20);
			this.label11.Text = "Vägbeskrivning:";
			// 
			// button21
			// 
			this.button21.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button21.Location = new System.Drawing.Point(240, 2);
			this.button21.Size = new System.Drawing.Size(72, 24);
			this.button21.Text = "Skriv ut";
			this.button21.Click += new System.EventHandler(this.button21_Click);
			// 
			// button5
			// 
			this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button5.Location = new System.Drawing.Point(240, 128);
			this.button5.Size = new System.Drawing.Size(72, 24);
			this.button5.Text = "Lastat";
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// button6
			// 
			this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button6.Location = new System.Drawing.Point(240, 64);
			this.button6.Size = new System.Drawing.Size(72, 24);
			this.button6.Text = "Nej tack";
			this.button6.Click += new System.EventHandler(this.button6_Click);
			// 
			// button7
			// 
			this.button7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button7.Location = new System.Drawing.Point(240, 96);
			this.button7.Size = new System.Drawing.Size(72, 24);
			this.button7.Text = "Kanske";
			this.button7.Click += new System.EventHandler(this.button7_Click);
			// 
			// button8
			// 
			this.button8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button8.Location = new System.Drawing.Point(240, 32);
			this.button8.Size = new System.Drawing.Size(72, 24);
			this.button8.Text = "Ja tack";
			this.button8.Click += new System.EventHandler(this.button8_Click);
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(8, 35);
			this.label7.Size = new System.Drawing.Size(56, 20);
			this.label7.Text = "Telefonnr:";
			// 
			// phoneNoBox
			// 
			this.phoneNoBox.Location = new System.Drawing.Point(80, 32);
			this.phoneNoBox.ReadOnly = true;
			this.phoneNoBox.Size = new System.Drawing.Size(152, 20);
			this.phoneNoBox.Text = "";
			// 
			// headerLabel2
			// 
			this.headerLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.headerLabel2.Location = new System.Drawing.Point(5, 3);
			this.headerLabel2.Size = new System.Drawing.Size(227, 20);
			this.headerLabel2.Text = "Hämtorder";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(8, 59);
			this.label9.Size = new System.Drawing.Size(56, 20);
			this.label9.Text = "Mobiltelnr:";
			// 
			// cellPhoneNoBox
			// 
			this.cellPhoneNoBox.Location = new System.Drawing.Point(80, 56);
			this.cellPhoneNoBox.ReadOnly = true;
			this.cellPhoneNoBox.Size = new System.Drawing.Size(152, 20);
			this.cellPhoneNoBox.Text = "";
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.button23);
			this.tabPage3.Controls.Add(this.button20);
			this.tabPage3.Controls.Add(this.button9);
			this.tabPage3.Controls.Add(this.button10);
			this.tabPage3.Controls.Add(this.button11);
			this.tabPage3.Controls.Add(this.button12);
			this.tabPage3.Controls.Add(this.label10);
			this.tabPage3.Controls.Add(this.detailsBox);
			this.tabPage3.Controls.Add(this.headerLabel3);
			this.tabPage3.Controls.Add(this.label12);
			this.tabPage3.Controls.Add(this.commentBox);
			this.tabPage3.Location = new System.Drawing.Point(4, 4);
			this.tabPage3.Size = new System.Drawing.Size(314, 188);
			this.tabPage3.Text = "Innehåll";
			// 
			// button23
			// 
			this.button23.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button23.Location = new System.Drawing.Point(240, 160);
			this.button23.Size = new System.Drawing.Size(72, 24);
			this.button23.Text = "Tillbaka";
			this.button23.Click += new System.EventHandler(this.button23_Click);
			// 
			// button20
			// 
			this.button20.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button20.Location = new System.Drawing.Point(240, 2);
			this.button20.Size = new System.Drawing.Size(72, 24);
			this.button20.Text = "Skriv ut";
			this.button20.Click += new System.EventHandler(this.button20_Click);
			// 
			// button9
			// 
			this.button9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button9.Location = new System.Drawing.Point(240, 128);
			this.button9.Size = new System.Drawing.Size(72, 24);
			this.button9.Text = "Lastat";
			this.button9.Click += new System.EventHandler(this.button9_Click);
			// 
			// button10
			// 
			this.button10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button10.Location = new System.Drawing.Point(240, 64);
			this.button10.Size = new System.Drawing.Size(72, 24);
			this.button10.Text = "Nej tack";
			this.button10.Click += new System.EventHandler(this.button10_Click);
			// 
			// button11
			// 
			this.button11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button11.Location = new System.Drawing.Point(240, 96);
			this.button11.Size = new System.Drawing.Size(72, 24);
			this.button11.Text = "Kanske";
			this.button11.Click += new System.EventHandler(this.button11_Click);
			// 
			// button12
			// 
			this.button12.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button12.Location = new System.Drawing.Point(240, 32);
			this.button12.Size = new System.Drawing.Size(72, 24);
			this.button12.Text = "Ja tack";
			this.button12.Click += new System.EventHandler(this.button12_Click);
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(8, 35);
			this.label10.Size = new System.Drawing.Size(56, 20);
			this.label10.Text = "Innehåll:";
			// 
			// detailsBox
			// 
			this.detailsBox.Location = new System.Drawing.Point(80, 32);
			this.detailsBox.Multiline = true;
			this.detailsBox.ReadOnly = true;
			this.detailsBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.detailsBox.Size = new System.Drawing.Size(152, 64);
			this.detailsBox.Text = "";
			// 
			// headerLabel3
			// 
			this.headerLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.headerLabel3.Location = new System.Drawing.Point(5, 3);
			this.headerLabel3.Size = new System.Drawing.Size(227, 20);
			this.headerLabel3.Text = "Hämtorder";
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(8, 104);
			this.label12.Size = new System.Drawing.Size(72, 20);
			this.label12.Text = "Kommentar:";
			// 
			// commentBox
			// 
			this.commentBox.Location = new System.Drawing.Point(80, 104);
			this.commentBox.Multiline = true;
			this.commentBox.ReadOnly = true;
			this.commentBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.commentBox.Size = new System.Drawing.Size(152, 72);
			this.commentBox.Text = "";
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.Add(this.button31);
			this.tabPage4.Controls.Add(this.label16);
			this.tabPage4.Controls.Add(this.label15);
			this.tabPage4.Controls.Add(this.headerLabel6);
			this.tabPage4.Location = new System.Drawing.Point(4, 4);
			this.tabPage4.Size = new System.Drawing.Size(314, 188);
			this.tabPage4.Text = "Navigera";
			// 
			// button31
			// 
			this.button31.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button31.Location = new System.Drawing.Point(224, 144);
			this.button31.Size = new System.Drawing.Size(80, 32);
			this.button31.Text = "Navigera";
			this.button31.Click += new System.EventHandler(this.button31_Click);
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(8, 96);
			this.label16.Size = new System.Drawing.Size(184, 80);
			this.label16.Text = "En förutsättning är dock att programmet Pocket Navigator är installerat.";
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(8, 32);
			this.label15.Size = new System.Drawing.Size(184, 56);
			this.label15.Text = "Genom ett klick på Navigera-knappen nedan startas en vägledning genom navigations" +
				"-programmet mot kunden.";
			// 
			// headerLabel6
			// 
			this.headerLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.headerLabel6.Location = new System.Drawing.Point(5, 3);
			this.headerLabel6.Size = new System.Drawing.Size(299, 20);
			this.headerLabel6.Text = "Hämtorder";
			// 
			// tabPage5
			// 
			this.tabPage5.Controls.Add(this.button30);
			this.tabPage5.Controls.Add(this.button29);
			this.tabPage5.Controls.Add(this.button28);
			this.tabPage5.Controls.Add(this.label8);
			this.tabPage5.Controls.Add(this.priorityBox);
			this.tabPage5.Controls.Add(this.button18);
			this.tabPage5.Controls.Add(this.button17);
			this.tabPage5.Controls.Add(this.button16);
			this.tabPage5.Controls.Add(this.button15);
			this.tabPage5.Controls.Add(this.button14);
			this.tabPage5.Controls.Add(this.button13);
			this.tabPage5.Controls.Add(this.headerLabel4);
			this.tabPage5.Location = new System.Drawing.Point(4, 4);
			this.tabPage5.Size = new System.Drawing.Size(314, 188);
			this.tabPage5.Text = "Prioritet";
			// 
			// button30
			// 
			this.button30.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button30.Location = new System.Drawing.Point(216, 144);
			this.button30.Size = new System.Drawing.Size(88, 32);
			this.button30.Text = "Prioritet 9";
			this.button30.Click += new System.EventHandler(this.button30_Click);
			// 
			// button29
			// 
			this.button29.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button29.Location = new System.Drawing.Point(216, 104);
			this.button29.Size = new System.Drawing.Size(88, 32);
			this.button29.Text = "Prioritet 8";
			this.button29.Click += new System.EventHandler(this.button29_Click);
			// 
			// button28
			// 
			this.button28.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button28.Location = new System.Drawing.Point(216, 64);
			this.button28.Size = new System.Drawing.Size(88, 32);
			this.button28.Text = "Prioritet 7";
			this.button28.Click += new System.EventHandler(this.button28_Click);
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(8, 35);
			this.label8.Size = new System.Drawing.Size(56, 20);
			this.label8.Text = "Datum:";
			// 
			// priorityBox
			// 
			this.priorityBox.Location = new System.Drawing.Point(80, 32);
			this.priorityBox.ReadOnly = true;
			this.priorityBox.Size = new System.Drawing.Size(208, 20);
			this.priorityBox.Text = "";
			// 
			// button18
			// 
			this.button18.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button18.Location = new System.Drawing.Point(112, 144);
			this.button18.Size = new System.Drawing.Size(88, 32);
			this.button18.Text = "Prioritet 6";
			this.button18.Click += new System.EventHandler(this.button18_Click);
			// 
			// button17
			// 
			this.button17.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button17.Location = new System.Drawing.Point(112, 104);
			this.button17.Size = new System.Drawing.Size(88, 32);
			this.button17.Text = "Prioritet 5";
			this.button17.Click += new System.EventHandler(this.button17_Click);
			// 
			// button16
			// 
			this.button16.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button16.Location = new System.Drawing.Point(112, 64);
			this.button16.Size = new System.Drawing.Size(88, 32);
			this.button16.Text = "Prioritet 4";
			this.button16.Click += new System.EventHandler(this.button16_Click);
			// 
			// button15
			// 
			this.button15.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button15.Location = new System.Drawing.Point(8, 144);
			this.button15.Size = new System.Drawing.Size(88, 32);
			this.button15.Text = "Prioritet 3";
			this.button15.Click += new System.EventHandler(this.button15_Click);
			// 
			// button14
			// 
			this.button14.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button14.Location = new System.Drawing.Point(8, 104);
			this.button14.Size = new System.Drawing.Size(88, 32);
			this.button14.Text = "Prioritet 2";
			this.button14.Click += new System.EventHandler(this.button14_Click);
			// 
			// button13
			// 
			this.button13.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button13.Location = new System.Drawing.Point(8, 64);
			this.button13.Size = new System.Drawing.Size(88, 32);
			this.button13.Text = "Prioritet 1";
			this.button13.Click += new System.EventHandler(this.button13_Click);
			// 
			// headerLabel4
			// 
			this.headerLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.headerLabel4.Location = new System.Drawing.Point(5, 3);
			this.headerLabel4.Size = new System.Drawing.Size(299, 20);
			this.headerLabel4.Text = "Hämtorder";
			// 
			// tabPage6
			// 
			this.tabPage6.Controls.Add(this.button27);
			this.tabPage6.Controls.Add(this.button26);
			this.tabPage6.Controls.Add(this.button25);
			this.tabPage6.Controls.Add(this.label14);
			this.tabPage6.Controls.Add(this.agentBox);
			this.tabPage6.Controls.Add(this.headerLabel5);
			this.tabPage6.Location = new System.Drawing.Point(4, 4);
			this.tabPage6.Size = new System.Drawing.Size(314, 188);
			this.tabPage6.Text = "Bil";
			// 
			// button27
			// 
			this.button27.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button27.Location = new System.Drawing.Point(120, 152);
			this.button27.Size = new System.Drawing.Size(104, 32);
			this.button27.Text = "Tilldela om";
			this.button27.Click += new System.EventHandler(this.button27_Click);
			// 
			// button26
			// 
			this.button26.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button26.Location = new System.Drawing.Point(232, 152);
			this.button26.Size = new System.Drawing.Size(72, 32);
			this.button26.Text = "Tillbaka";
			this.button26.Click += new System.EventHandler(this.button26_Click);
			// 
			// button25
			// 
			this.button25.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button25.Location = new System.Drawing.Point(240, 32);
			this.button25.Size = new System.Drawing.Size(64, 40);
			this.button25.Text = "Ändra";
			this.button25.Click += new System.EventHandler(this.button25_Click);
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(8, 35);
			this.label14.Size = new System.Drawing.Size(63, 20);
			this.label14.Text = "Bil:";
			// 
			// agentBox
			// 
			this.agentBox.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.agentBox.Location = new System.Drawing.Point(80, 32);
			this.agentBox.ReadOnly = true;
			this.agentBox.Size = new System.Drawing.Size(151, 20);
			this.agentBox.Text = "";
			// 
			// headerLabel5
			// 
			this.headerLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.headerLabel5.Location = new System.Drawing.Point(5, 3);
			this.headerLabel5.Size = new System.Drawing.Size(227, 20);
			this.headerLabel5.Text = "Hämtorder";
			// 
			// ShipOrder
			// 
			this.ClientSize = new System.Drawing.Size(322, 216);
			this.Controls.Add(this.tabControl1);
			this.Text = "Hämtorder";

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			setShipOrderStatus(5);
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			setShipOrderStatus(2);
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			setShipOrderStatus(1);
		}

		private void button4_Click(object sender, System.EventArgs e)
		{
			setShipOrderStatus(6);
		}


		private void setShipOrderStatus(int status)
		{
			if (status == 6) 
			{
				if (System.Windows.Forms.MessageBox.Show("Är du säker på att du vill lasta körordern?", "Körorder", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.Yes)				
				{
					return;
				}

				if (agentStatus.containerNo == "")
				{
					MessageBox.Show("Det finns ingen container på flaket. Klicka på Status, och lasta en container.", "Container", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
					return;
				}

			}

			if (dataShipOrder.getFromDb())
			{
				if (status == 1) status = 8; // Nej tack
				if (status == 2) status = 7; // Kanske
				dataShipOrder.status = status;

				if (status == 6)
				{
					dataShipOrder.positionX = agentStatus.rt90x;
					dataShipOrder.positionY = agentStatus.rt90y;
					dataShipOrder.shipTime = DateTime.Now;
				}

				dataShipOrder.commit();

				DataSyncActions dataSyncActions = new DataSyncActions(smartDatabase);
				dataSyncActions.addSyncAction(1, 0, dataShipOrder.entryNo.ToString());
			}

			if (status == 6)
			{

				if (dataShipOrder.customerNo != "")
				{
					if (System.Windows.Forms.MessageBox.Show("Vill du skapa en följesedel?", "Följesedel", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
					{
						//DataCustomer dataCustomer = new DataCustomer(smartDatabase, dataShipOrder.billToCustomerNo);
						//DataShipmentHeader dataShipmentHeader = new DataShipmentHeader(smartDatabase, dataCustomer);

						DataShipmentHeader dataShipmentHeader = new DataShipmentHeader(smartDatabase, dataShipOrder);

						dataShipmentHeader.mobileUserName = agentStatus.mobileUserName;
						dataShipmentHeader.containerNo = agentStatus.containerNo;
						dataShipmentHeader.commit();

						Shipment shipment = new Shipment(smartDatabase, dataShipmentHeader, agentStatus);
						shipment.ShowDialog();
						shipment.Dispose();
					}
				}
			}


			this.Close();
		}

		private void button8_Click(object sender, System.EventArgs e)
		{
			setShipOrderStatus(5);		
		}

		private void button6_Click(object sender, System.EventArgs e)
		{
			setShipOrderStatus(1);		
		}

		private void button7_Click(object sender, System.EventArgs e)
		{
			setShipOrderStatus(2);		
		}

		private void button5_Click(object sender, System.EventArgs e)
		{
			setShipOrderStatus(6);		
		}

		private void button12_Click(object sender, System.EventArgs e)
		{
			setShipOrderStatus(5);
		}

		private void button10_Click(object sender, System.EventArgs e)
		{
			setShipOrderStatus(1);
		}

		private void button11_Click(object sender, System.EventArgs e)
		{
			setShipOrderStatus(2);
		}

		private void button9_Click(object sender, System.EventArgs e)
		{
			setShipOrderStatus(6);
		}


		private void button13_Click(object sender, System.EventArgs e)
		{
			setPriority(1);
		}

		private void setPriority(int priority)
		{
			dataShipOrder.priority = priority;
			priorityBox.Text = "Prioritet "+dataShipOrder.priority;
			dataShipOrder.commit();
		}

		private void button14_Click(object sender, System.EventArgs e)
		{
			setPriority(2);
		}

		private void button15_Click(object sender, System.EventArgs e)
		{
			setPriority(3);
		
		}

		private void button16_Click(object sender, System.EventArgs e)
		{
			setPriority(4);		
		}

		private void button17_Click(object sender, System.EventArgs e)
		{
			setPriority(5);		
		}

		private void button18_Click(object sender, System.EventArgs e)
		{
			setPriority(6);		
		}

		private void button19_Click(object sender, System.EventArgs e)
		{
			printShipOrder();
		}

		private void button20_Click(object sender, System.EventArgs e)
		{
			printShipOrder();
		}

		private void button21_Click(object sender, System.EventArgs e)
		{
			printShipOrder();		
		}

		private void button22_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void button23_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void button24_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void button25_Click(object sender, System.EventArgs e)
		{
			AgentList agentList = new AgentList(smartDatabase);
			agentList.ShowDialog();

			if (agentList.getStatus() == 1)
			{
				this.agentBox.Text = agentList.getSelectedAgent();
				this.dataShipOrder.agentCode = this.agentBox.Text;
			}

			agentList.Dispose();

		}

		private void button26_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void button27_Click(object sender, System.EventArgs e)
		{
			if (dataShipOrder.agentCode == "")
			{
				MessageBox.Show("Du måste välja en bil först.", "Fel", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
			}
			else
			{
				if (MessageBox.Show("Du kommer att tilldela om körordern till "+dataShipOrder.agentCode+". Är du säker?", "Tilldela till bil", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
				{
					dataShipOrder.status = 20;
					dataShipOrder.commit();

					DataSyncActions dataSyncActions = new DataSyncActions(smartDatabase);
					dataSyncActions.addSyncAction(5, 0, dataShipOrder.entryNo.ToString());
					this.Close();
				}
			}
		}

		private void button28_Click(object sender, System.EventArgs e)
		{
			setPriority(7);
		}

		private void button29_Click(object sender, System.EventArgs e)
		{
			setPriority(8);
		}

		private void button30_Click(object sender, System.EventArgs e)
		{
			setPriority(9);
		}

		private void printShipOrder()
		{
			Printer printer = new Printer(smartDatabase);

			if (!printer.init())
			{
				MessageBox.Show("Kan ej få kontakt med skrivaren. Kontrollera skrivaren.", "Fel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
			}
			else
			{
			
				Cursor.Current = Cursors.WaitCursor;
				Cursor.Show();

				printer.printShipOrder(dataShipOrder);
				printer.close();

				Cursor.Current = Cursors.Default;
				Cursor.Hide();
			}
		}

		private void button31_Click(object sender, System.EventArgs e)
		{
			Navigator navigator = new Navigator(smartDatabase);
			navigator.navigate(dataShipOrder.positionY, dataShipOrder.positionX, dataShipOrder.shipName);

		}
	}
}
