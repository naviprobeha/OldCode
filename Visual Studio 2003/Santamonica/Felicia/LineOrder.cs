using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for LineOrder.
	/// </summary>
	public class LineOrder : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.Button button22;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.TextBox shipDateBox;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox comments2Box;
		private System.Windows.Forms.TextBox addressBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Label headerLabel;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox cityBox;
		private System.Windows.Forms.TextBox nameBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox details2Box;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button24;
		private System.Windows.Forms.TextBox directionBox;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox phoneNoBox;
		private System.Windows.Forms.Label headerLabel2;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox cellPhoneNoBox;
		private System.Windows.Forms.Button button31;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label headerLabel6;
		private System.Windows.Forms.TabPage tabPage5;
		private System.Windows.Forms.Button button23;
		private System.Windows.Forms.Button button9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label headerLabel3;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TabControl tabControl1;
	
		private SmartDatabase smartDatabase;
		private DataLineOrder dataLineOrder;
		private DataSet lineOrderContainerDataSet;
		private System.Windows.Forms.Label headerLabel8;
		private System.Windows.Forms.DataGrid lineOrderContainerGrid;
		private System.Windows.Forms.DataGridTableStyle lineOrderContainerTable;
		private System.Windows.Forms.DataGridTextBoxColumn containerNoCol;
		private System.Windows.Forms.DataGridTextBoxColumn weightCol;
		private System.Windows.Forms.DataGridTextBoxColumn containerTypeCol;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.DataGridTextBoxColumn categoryCol;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.TextBox commentBox;
		private System.Windows.Forms.TextBox detailsBox;
		private System.Windows.Forms.TextBox customerNoBox;
		private System.Windows.Forms.Label label5;
		private Status agentStatus;

		public LineOrder(SmartDatabase smartDatabase, DataLineOrder dataLineOrder, Status agentStatus)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			this.dataLineOrder = dataLineOrder;
			this.smartDatabase = smartDatabase;
			this.agentStatus = agentStatus;

			this.shipDateBox.Text = dataLineOrder.shipDate.ToString("yyyy-MM-dd")+" "+dataLineOrder.shipTime.ToString("HH:mm");
			this.nameBox.Text = dataLineOrder.shippingCustomerName;
			this.addressBox.Text = dataLineOrder.address;
			//this.address2Box.Text = dataLineOrder.address2;
			this.cityBox.Text = dataLineOrder.city;
			this.phoneNoBox.Text = dataLineOrder.phoneNo;
			this.cellPhoneNoBox.Text = dataLineOrder.cellPhoneNo;
			this.detailsBox.Text = dataLineOrder.details;
			this.details2Box.Text = dataLineOrder.details;
			this.commentBox.Text = dataLineOrder.comments;
			this.directionBox.Text = dataLineOrder.directionComment1+dataLineOrder.directionComment2;
			this.comments2Box.Text = dataLineOrder.comments;
			this.customerNoBox.Text = dataLineOrder.shippingCustomerNo;

			headerLabel.Text = "Linjeorder "+dataLineOrder.entryNo;
			headerLabel2.Text = "Linjeorder "+dataLineOrder.entryNo;
			headerLabel3.Text = "Linjeorder "+dataLineOrder.entryNo;			
			headerLabel6.Text = "Linjeorder "+dataLineOrder.entryNo;
			headerLabel8.Text = "Linjeorder "+dataLineOrder.entryNo;


			DataLineOrderContainers dataLineOrderContainers = new DataLineOrderContainers(smartDatabase);
			this.lineOrderContainerDataSet = dataLineOrderContainers.getDataSet(dataLineOrder.entryNo);
			lineOrderContainerGrid.DataSource = this.lineOrderContainerDataSet.Tables[0];

			if (dataLineOrder.status == 7)
			{
				button4.Text = "Skriv ut";
				button5.Text = "Skriv ut";
				button9.Text = "Skriv ut";
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
			this.shipDateBox = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.comments2Box = new System.Windows.Forms.TextBox();
			this.addressBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.details2Box = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.button4 = new System.Windows.Forms.Button();
			this.headerLabel = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.cityBox = new System.Windows.Forms.TextBox();
			this.nameBox = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.button24 = new System.Windows.Forms.Button();
			this.directionBox = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.button5 = new System.Windows.Forms.Button();
			this.label7 = new System.Windows.Forms.Label();
			this.phoneNoBox = new System.Windows.Forms.TextBox();
			this.headerLabel2 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.cellPhoneNoBox = new System.Windows.Forms.TextBox();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.button23 = new System.Windows.Forms.Button();
			this.button9 = new System.Windows.Forms.Button();
			this.label10 = new System.Windows.Forms.Label();
			this.commentBox = new System.Windows.Forms.TextBox();
			this.headerLabel3 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.detailsBox = new System.Windows.Forms.TextBox();
			this.tabPage5 = new System.Windows.Forms.TabPage();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.lineOrderContainerGrid = new System.Windows.Forms.DataGrid();
			this.lineOrderContainerTable = new System.Windows.Forms.DataGridTableStyle();
			this.containerNoCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.containerTypeCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.categoryCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.weightCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.headerLabel8 = new System.Windows.Forms.Label();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.button31 = new System.Windows.Forms.Button();
			this.label16 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.headerLabel6 = new System.Windows.Forms.Label();
			this.customerNoBox = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage5);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(322, 216);
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.label5);
			this.tabPage1.Controls.Add(this.customerNoBox);
			this.tabPage1.Controls.Add(this.button22);
			this.tabPage1.Controls.Add(this.label13);
			this.tabPage1.Controls.Add(this.shipDateBox);
			this.tabPage1.Controls.Add(this.label6);
			this.tabPage1.Controls.Add(this.comments2Box);
			this.tabPage1.Controls.Add(this.addressBox);
			this.tabPage1.Controls.Add(this.label2);
			this.tabPage1.Controls.Add(this.details2Box);
			this.tabPage1.Controls.Add(this.label1);
			this.tabPage1.Controls.Add(this.button4);
			this.tabPage1.Controls.Add(this.headerLabel);
			this.tabPage1.Controls.Add(this.label3);
			this.tabPage1.Controls.Add(this.cityBox);
			this.tabPage1.Controls.Add(this.nameBox);
			this.tabPage1.Controls.Add(this.label4);
			this.tabPage1.Location = new System.Drawing.Point(4, 4);
			this.tabPage1.Size = new System.Drawing.Size(314, 190);
			this.tabPage1.Text = "Kund";
			// 
			// button22
			// 
			this.button22.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button22.Location = new System.Drawing.Point(240, 144);
			this.button22.Size = new System.Drawing.Size(72, 32);
			this.button22.Text = "Tillbaka";
			this.button22.Click += new System.EventHandler(this.button22_Click);
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(7, 26);
			this.label13.Size = new System.Drawing.Size(64, 20);
			this.label13.Text = "Datum/Tid:";
			// 
			// shipDateBox
			// 
			this.shipDateBox.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.shipDateBox.Location = new System.Drawing.Point(79, 23);
			this.shipDateBox.ReadOnly = true;
			this.shipDateBox.Size = new System.Drawing.Size(152, 20);
			this.shipDateBox.Text = "";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(7, 170);
			this.label6.Size = new System.Drawing.Size(64, 20);
			this.label6.Text = "Kommentar:";
			// 
			// comments2Box
			// 
			this.comments2Box.Location = new System.Drawing.Point(79, 167);
			this.comments2Box.ReadOnly = true;
			this.comments2Box.Size = new System.Drawing.Size(152, 20);
			this.comments2Box.Text = "";
			// 
			// addressBox
			// 
			this.addressBox.Location = new System.Drawing.Point(80, 96);
			this.addressBox.ReadOnly = true;
			this.addressBox.Size = new System.Drawing.Size(152, 20);
			this.addressBox.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(7, 146);
			this.label2.Size = new System.Drawing.Size(64, 20);
			this.label2.Text = "Innehåll:";
			// 
			// details2Box
			// 
			this.details2Box.Location = new System.Drawing.Point(79, 143);
			this.details2Box.ReadOnly = true;
			this.details2Box.Size = new System.Drawing.Size(152, 20);
			this.details2Box.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(7, 122);
			this.label1.Size = new System.Drawing.Size(64, 20);
			this.label1.Text = "Ort:";
			// 
			// button4
			// 
			this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button4.Location = new System.Drawing.Point(240, 104);
			this.button4.Size = new System.Drawing.Size(72, 32);
			this.button4.Text = "Lastat";
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// headerLabel
			// 
			this.headerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.headerLabel.Location = new System.Drawing.Point(4, 2);
			this.headerLabel.Size = new System.Drawing.Size(227, 20);
			this.headerLabel.Text = "Linjeorder";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(7, 75);
			this.label3.Size = new System.Drawing.Size(64, 20);
			this.label3.Text = "Namn:";
			// 
			// cityBox
			// 
			this.cityBox.Location = new System.Drawing.Point(79, 119);
			this.cityBox.ReadOnly = true;
			this.cityBox.Size = new System.Drawing.Size(152, 20);
			this.cityBox.Text = "";
			// 
			// nameBox
			// 
			this.nameBox.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.nameBox.Location = new System.Drawing.Point(80, 72);
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
			this.tabPage2.Controls.Add(this.button5);
			this.tabPage2.Controls.Add(this.label7);
			this.tabPage2.Controls.Add(this.phoneNoBox);
			this.tabPage2.Controls.Add(this.headerLabel2);
			this.tabPage2.Controls.Add(this.label9);
			this.tabPage2.Controls.Add(this.cellPhoneNoBox);
			this.tabPage2.Location = new System.Drawing.Point(4, 4);
			this.tabPage2.Size = new System.Drawing.Size(314, 190);
			this.tabPage2.Text = "Kontakt";
			// 
			// button24
			// 
			this.button24.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button24.Location = new System.Drawing.Point(240, 144);
			this.button24.Size = new System.Drawing.Size(72, 32);
			this.button24.Text = "Tillbaka";
			this.button24.Click += new System.EventHandler(this.button24_Click);
			// 
			// directionBox
			// 
			this.directionBox.Location = new System.Drawing.Point(7, 98);
			this.directionBox.Multiline = true;
			this.directionBox.ReadOnly = true;
			this.directionBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.directionBox.Size = new System.Drawing.Size(224, 80);
			this.directionBox.Text = "";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(7, 82);
			this.label11.Size = new System.Drawing.Size(128, 20);
			this.label11.Text = "Vägbeskrivning:";
			// 
			// button5
			// 
			this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button5.Location = new System.Drawing.Point(240, 104);
			this.button5.Size = new System.Drawing.Size(72, 32);
			this.button5.Text = "Lastat";
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(7, 37);
			this.label7.Size = new System.Drawing.Size(56, 20);
			this.label7.Text = "Telefonnr:";
			// 
			// phoneNoBox
			// 
			this.phoneNoBox.Location = new System.Drawing.Point(79, 34);
			this.phoneNoBox.ReadOnly = true;
			this.phoneNoBox.Size = new System.Drawing.Size(152, 20);
			this.phoneNoBox.Text = "";
			// 
			// headerLabel2
			// 
			this.headerLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.headerLabel2.Location = new System.Drawing.Point(4, 2);
			this.headerLabel2.Size = new System.Drawing.Size(227, 20);
			this.headerLabel2.Text = "Linjeorder";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(7, 61);
			this.label9.Size = new System.Drawing.Size(56, 20);
			this.label9.Text = "Mobiltelnr:";
			// 
			// cellPhoneNoBox
			// 
			this.cellPhoneNoBox.Location = new System.Drawing.Point(79, 58);
			this.cellPhoneNoBox.ReadOnly = true;
			this.cellPhoneNoBox.Size = new System.Drawing.Size(152, 20);
			this.cellPhoneNoBox.Text = "";
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.button23);
			this.tabPage3.Controls.Add(this.button9);
			this.tabPage3.Controls.Add(this.label10);
			this.tabPage3.Controls.Add(this.commentBox);
			this.tabPage3.Controls.Add(this.headerLabel3);
			this.tabPage3.Controls.Add(this.label12);
			this.tabPage3.Controls.Add(this.detailsBox);
			this.tabPage3.Location = new System.Drawing.Point(4, 4);
			this.tabPage3.Size = new System.Drawing.Size(314, 190);
			this.tabPage3.Text = "Kommentar";
			// 
			// button23
			// 
			this.button23.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button23.Location = new System.Drawing.Point(240, 144);
			this.button23.Size = new System.Drawing.Size(72, 32);
			this.button23.Text = "Tillbaka";
			this.button23.Click += new System.EventHandler(this.button23_Click);
			// 
			// button9
			// 
			this.button9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button9.Location = new System.Drawing.Point(240, 104);
			this.button9.Size = new System.Drawing.Size(72, 32);
			this.button9.Text = "Lastat";
			this.button9.Click += new System.EventHandler(this.button9_Click);
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(8, 123);
			this.label10.Size = new System.Drawing.Size(56, 20);
			this.label10.Text = "Innehåll:";
			// 
			// commentBox
			// 
			this.commentBox.Location = new System.Drawing.Point(79, 34);
			this.commentBox.Multiline = true;
			this.commentBox.ReadOnly = true;
			this.commentBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.commentBox.Size = new System.Drawing.Size(152, 78);
			this.commentBox.Text = "";
			// 
			// headerLabel3
			// 
			this.headerLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.headerLabel3.Location = new System.Drawing.Point(4, 2);
			this.headerLabel3.Size = new System.Drawing.Size(227, 20);
			this.headerLabel3.Text = "Linjeorder";
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(8, 37);
			this.label12.Size = new System.Drawing.Size(64, 20);
			this.label12.Text = "Kommentar:";
			// 
			// detailsBox
			// 
			this.detailsBox.Location = new System.Drawing.Point(79, 120);
			this.detailsBox.Multiline = true;
			this.detailsBox.ReadOnly = true;
			this.detailsBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.detailsBox.Size = new System.Drawing.Size(152, 56);
			this.detailsBox.Text = "";
			// 
			// tabPage5
			// 
			this.tabPage5.Controls.Add(this.button2);
			this.tabPage5.Controls.Add(this.button1);
			this.tabPage5.Controls.Add(this.lineOrderContainerGrid);
			this.tabPage5.Controls.Add(this.headerLabel8);
			this.tabPage5.Location = new System.Drawing.Point(4, 4);
			this.tabPage5.Size = new System.Drawing.Size(314, 190);
			this.tabPage5.Text = "Containers";
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button2.Location = new System.Drawing.Point(160, 152);
			this.button2.Size = new System.Drawing.Size(72, 32);
			this.button2.Text = "Lastat";
			this.button2.Click += new System.EventHandler(this.button2_Click_1);
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button1.Location = new System.Drawing.Point(240, 152);
			this.button1.Size = new System.Drawing.Size(72, 32);
			this.button1.Text = "Service";
			this.button1.Click += new System.EventHandler(this.button1_Click_1);
			// 
			// lineOrderContainerGrid
			// 
			this.lineOrderContainerGrid.Location = new System.Drawing.Point(8, 24);
			this.lineOrderContainerGrid.Size = new System.Drawing.Size(304, 120);
			this.lineOrderContainerGrid.TableStyles.Add(this.lineOrderContainerTable);
			this.lineOrderContainerGrid.Text = "lineOrderContainerGrid";
			// 
			// lineOrderContainerTable
			// 
			this.lineOrderContainerTable.GridColumnStyles.Add(this.containerNoCol);
			this.lineOrderContainerTable.GridColumnStyles.Add(this.containerTypeCol);
			this.lineOrderContainerTable.GridColumnStyles.Add(this.categoryCol);
			this.lineOrderContainerTable.GridColumnStyles.Add(this.weightCol);
			this.lineOrderContainerTable.MappingName = "lineOrderContainer";
			// 
			// containerNoCol
			// 
			this.containerNoCol.HeaderText = "Container";
			this.containerNoCol.MappingName = "containerNo";
			this.containerNoCol.NullText = "";
			this.containerNoCol.Width = 60;
			// 
			// containerTypeCol
			// 
			this.containerTypeCol.HeaderText = "Typ";
			this.containerTypeCol.MappingName = "containerTypeCode";
			this.containerTypeCol.NullText = "";
			// 
			// categoryCol
			// 
			this.categoryCol.HeaderText = "Kategori";
			this.categoryCol.MappingName = "description";
			this.categoryCol.NullText = "";
			this.categoryCol.Width = 95;
			// 
			// weightCol
			// 
			this.weightCol.HeaderText = "Vikt (Kg)";
			this.weightCol.MappingName = "weight";
			this.weightCol.NullText = "";
			this.weightCol.Width = 70;
			// 
			// headerLabel8
			// 
			this.headerLabel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.headerLabel8.Location = new System.Drawing.Point(4, 2);
			this.headerLabel8.Size = new System.Drawing.Size(299, 20);
			this.headerLabel8.Text = "Hämtorder";
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.Add(this.button31);
			this.tabPage4.Controls.Add(this.label16);
			this.tabPage4.Controls.Add(this.label15);
			this.tabPage4.Controls.Add(this.headerLabel6);
			this.tabPage4.Location = new System.Drawing.Point(4, 4);
			this.tabPage4.Size = new System.Drawing.Size(314, 190);
			this.tabPage4.Text = "Navigera";
			// 
			// button31
			// 
			this.button31.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button31.Location = new System.Drawing.Point(227, 150);
			this.button31.Size = new System.Drawing.Size(80, 32);
			this.button31.Text = "Navigera";
			this.button31.Click += new System.EventHandler(this.button31_Click);
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(4, 102);
			this.label16.Size = new System.Drawing.Size(184, 80);
			this.label16.Text = "En förutsättning är dock att programmet Pocket Navigator är installerat.";
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(4, 38);
			this.label15.Size = new System.Drawing.Size(184, 56);
			this.label15.Text = "Genom ett klick på Navigera-knappen nedan startas en vägledning genom navigations" +
				"-programmet mot kunden.";
			// 
			// headerLabel6
			// 
			this.headerLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.headerLabel6.Location = new System.Drawing.Point(4, 2);
			this.headerLabel6.Size = new System.Drawing.Size(299, 20);
			this.headerLabel6.Text = "Hämtorder";
			// 
			// customerNoBox
			// 
			this.customerNoBox.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.customerNoBox.Location = new System.Drawing.Point(80, 48);
			this.customerNoBox.ReadOnly = true;
			this.customerNoBox.Size = new System.Drawing.Size(152, 20);
			this.customerNoBox.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 51);
			this.label5.Size = new System.Drawing.Size(64, 20);
			this.label5.Text = "Kundnr:";
			// 
			// LineOrder
			// 
			this.ClientSize = new System.Drawing.Size(322, 216);
			this.Controls.Add(this.tabControl1);
			this.Text = "Linjeorder";

		}
		#endregion

		private void setLineOrderStatus(int status)
		{
			if ((status == 7) && (dataLineOrder.status < 7))
			{
				if (System.Windows.Forms.MessageBox.Show("Är du säker på att du vill lasta linjeordern?", "Linjeorder", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.Yes)				
				{
					return;
				}

			}

			if (dataLineOrder.getFromDb())
			{
				if ((status == 7) && (dataLineOrder.status < 7))
				{
					DataLineOrderContainers dataLineOrderContainers = new DataLineOrderContainers(smartDatabase);
					ArrayList lineOrderContainerList = dataLineOrderContainers.getContainerList(dataLineOrder.entryNo);

					LoadedContainers loadedContainers = new LoadedContainers(smartDatabase, this.agentStatus, 1, this.dataLineOrder.shippingCustomerNo, 1, this.dataLineOrder.entryNo.ToString());
					loadedContainers.hideLoadContainerButton();
					loadedContainers.setLineOrderContainers(lineOrderContainerList);
					loadedContainers.ShowDialog();

					int formStatus = loadedContainers.getFormStatus();

					loadedContainers.Dispose();

                    if (formStatus == 0) return;

				
					if (MessageBox.Show("Rapportera väntetid?", "Väntetid", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
					{
						ReportWaitTime lineOrderWaitTime = new ReportWaitTime("Väntetid lastning");
						lineOrderWaitTime.ShowDialog();
						dataLineOrder.loadWaitTime = lineOrderWaitTime.getValue();
						lineOrderWaitTime.Dispose();
					}
				}

				if (MessageBox.Show("Vill du skriva ut en fraktsedel?", "Skriv ut", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
				{
					this.printLineOrder();
				}

				if (status == 1) status = 9; // Nej tack
				if (status == 2) status = 8; // Kanske
				dataLineOrder.status = status;
				dataLineOrder.commit();

				if (status == 7)
				{
					dataLineOrder.positionX = agentStatus.rt90x;
					dataLineOrder.positionY = agentStatus.rt90y;

					dataLineOrder.shipTime = DateTime.Now;
					dataLineOrder.commit();

				}

				DataSyncActions dataSyncActions = new DataSyncActions(smartDatabase);
				dataSyncActions.addSyncAction(7, 0, dataLineOrder.entryNo.ToString());
			}



			this.Close();
		}

		private void printLineOrder()
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

				if (dataLineOrder.status == 7) printer.setCopy();

				printer.printLineOrder(dataLineOrder);
				printer.close();

				Cursor.Current = Cursors.Default;
				Cursor.Hide();
			}
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			this.setLineOrderStatus(6);

		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			this.setLineOrderStatus(1);
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			this.setLineOrderStatus(2);

		}

		private void button4_Click(object sender, System.EventArgs e)
		{
			this.setLineOrderStatus(7);
		}

		private void button22_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void button31_Click(object sender, System.EventArgs e)
		{
			Navigator navigator = new Navigator(smartDatabase);
			navigator.navigate(dataLineOrder.positionY, dataLineOrder.positionX, dataLineOrder.shippingCustomerName);

		}

		private void button9_Click(object sender, System.EventArgs e)
		{
			this.setLineOrderStatus(7);

		}

		private void button8_Click(object sender, System.EventArgs e)
		{
			this.setLineOrderStatus(6);

		}

		private void button6_Click(object sender, System.EventArgs e)
		{
			this.setLineOrderStatus(1);

		}

		private void button5_Click(object sender, System.EventArgs e)
		{
			this.setLineOrderStatus(7);

		}

		private void button24_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void button12_Click(object sender, System.EventArgs e)
		{
			this.setLineOrderStatus(6);

		}

		private void button10_Click(object sender, System.EventArgs e)
		{
			this.setLineOrderStatus(1);

		}

		private void button23_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void button1_Click_1(object sender, System.EventArgs e)
		{
			if (lineOrderContainerDataSet.Tables[0].Rows.Count > 0)
			{
				string containerNo = lineOrderContainerDataSet.Tables[0].Rows[lineOrderContainerGrid.CurrentRowIndex].ItemArray.GetValue(2).ToString();
				if (MessageBox.Show("Vill du service-rapportera container "+containerNo+"?", "Service", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
				{
					DataContainerEntry dataContainerEntry = new DataContainerEntry(smartDatabase);
					dataContainerEntry.containerNo = containerNo;
					dataContainerEntry.entryDateTime = DateTime.Now;
					dataContainerEntry.type = 3;  // Service
					dataContainerEntry.positionX = agentStatus.rt90x;
					dataContainerEntry.positionY = agentStatus.rt90y;
					dataContainerEntry.locationType = 1;
					dataContainerEntry.locationCode = dataLineOrder.shippingCustomerNo;
					dataContainerEntry.commit();

				}
			}
			else
			{
				MessageBox.Show("Det finns inga containers på ordern.", "Fel", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
			}
		}

		private void button2_Click_1(object sender, System.EventArgs e)
		{
			this.setLineOrderStatus(7);

		}
	}
}
