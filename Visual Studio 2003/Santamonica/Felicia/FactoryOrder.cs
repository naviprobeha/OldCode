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
	public class FactoryOrder : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.Button button22;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.TextBox shipDateBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Label headerLabel;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button button24;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Label headerLabel2;
		private System.Windows.Forms.Button button23;
		private System.Windows.Forms.Button button9;
		private System.Windows.Forms.Label headerLabel3;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TabControl tabControl1;
	
		private SmartDatabase smartDatabase;
		private DataFactoryOrder dataFactoryOrder;
		private System.Windows.Forms.DataGridTableStyle lineOrderContainerTable;
		private System.Windows.Forms.DataGridTextBoxColumn containerNoCol;
		private System.Windows.Forms.DataGridTextBoxColumn weightCol;
		private System.Windows.Forms.DataGridTextBoxColumn containerTypeCol;
		private System.Windows.Forms.DataGridTextBoxColumn categoryCol;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox factoryNoBox;
		private System.Windows.Forms.TextBox factoryAddressBox;
		private System.Windows.Forms.TextBox factoryNameBox;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TextBox categoryCodeBox;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.TextBox categoryDescriptionBox;
		private System.Windows.Forms.TextBox quantityBox;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.TextBox realQuantityBox;
		private System.Windows.Forms.TextBox consumerPhoneNoBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox factoryPhoneNoBox;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox consumerNoBox;
		private System.Windows.Forms.TextBox consumerAddressBox;
		private System.Windows.Forms.TextBox consumerCityBox;
		private System.Windows.Forms.TextBox consumerNameBox;
		private System.Windows.Forms.TextBox factoryCityBox;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Button button7;
		private System.Windows.Forms.Button button8;
		private System.Windows.Forms.TextBox consumerLevelBox;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.TextBox loadDurationBox;
		private System.Windows.Forms.TextBox shipTimeBox;
		private System.Windows.Forms.TextBox loadWaitDurationBox;
		private System.Windows.Forms.TextBox arrivalTimeBox;
		private System.Windows.Forms.TextBox dropDurationBox;
		private System.Windows.Forms.TextBox dropWaitDurationBox;
		private System.Windows.Forms.Label headerLabel16;
		private System.Windows.Forms.TextBox currentLevelBox;
		private System.Windows.Forms.Label label16;
		private Status agentStatus;

		public FactoryOrder(SmartDatabase smartDatabase, DataFactoryOrder dataFactoryOrder, Status agentStatus)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			this.dataFactoryOrder = dataFactoryOrder;
			this.smartDatabase = smartDatabase;
			this.agentStatus = agentStatus;

			this.shipDateBox.Text = dataFactoryOrder.shipDate.ToString("yyyy-MM-dd")+" "+dataFactoryOrder.shipTime.ToString("HH:mm");
			this.factoryNoBox.Text = dataFactoryOrder.factoryNo;
			this.factoryNameBox.Text = dataFactoryOrder.factoryName;
			this.factoryAddressBox.Text = dataFactoryOrder.factoryAddress;
			this.factoryCityBox.Text = dataFactoryOrder.factoryCity;
			this.factoryPhoneNoBox.Text = dataFactoryOrder.factoryPhoneNo;
			this.consumerNoBox.Text = dataFactoryOrder.consumerNo;
			this.consumerNameBox.Text = dataFactoryOrder.consumerName;
			this.consumerAddressBox.Text = dataFactoryOrder.consumerAddress;
			this.consumerCityBox.Text = dataFactoryOrder.consumerCity;
			this.categoryCodeBox.Text = dataFactoryOrder.categoryCode;
			this.categoryDescriptionBox.Text = dataFactoryOrder.categoryDescription;
			this.quantityBox.Text = (dataFactoryOrder.quantity * 1000).ToString("0") + " kg";
			this.realQuantityBox.Text = (dataFactoryOrder.realQuantity * 1000).ToString("0") + " kg";
			this.consumerLevelBox.Text = (dataFactoryOrder.consumerLevel * 1000).ToString("0") + " kg";

			DataConsumerStatus dataConsumerStatus = new DataConsumerStatus(smartDatabase, dataFactoryOrder.consumerNo);
			currentLevelBox.Text = (dataConsumerStatus.inventoryLevel * 1000).ToString("0") + " kg";


			this.shipTimeBox.Text = dataFactoryOrder.shipDate.ToString("yyyy-MM-dd")+" "+dataFactoryOrder.shipTime.ToString("HH:mm");
			this.loadDurationBox.Text = dataFactoryOrder.loadDuration + " minuter";
			this.loadWaitDurationBox.Text = dataFactoryOrder.loadWaitDuration + " minuter";
			this.arrivalTimeBox.Text = dataFactoryOrder.arrivalDateTime.ToString("yyyy-MM-dd HH:mm");
			this.dropDurationBox.Text = dataFactoryOrder.dropDuration + " minuter";
			this.dropWaitDurationBox.Text = dataFactoryOrder.dropWaitDuration + " minuter";

			headerLabel.Text = "Biomalorder "+dataFactoryOrder.entryNo+" "+dataFactoryOrder.getStatus();
			headerLabel2.Text = "Biomalorder "+dataFactoryOrder.entryNo+" "+dataFactoryOrder.getStatus();
			headerLabel3.Text = "Biomalorder "+dataFactoryOrder.entryNo+" "+dataFactoryOrder.getStatus();			
			headerLabel16.Text = "Biomalorder "+dataFactoryOrder.entryNo+" "+dataFactoryOrder.getStatus();			

			if (dataFactoryOrder.status == 4)
			{
				button1.Visible = false;
				button4.Visible = false;
				button2.Visible = false;
				button5.Visible = false;
				button9.Visible = false;
				button3.Visible = false;
				
				button6.Visible = true;
				button7.Visible = true;
				button8.Visible = true;
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
			this.button8 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.factoryPhoneNoBox = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.factoryNoBox = new System.Windows.Forms.TextBox();
			this.button22 = new System.Windows.Forms.Button();
			this.label13 = new System.Windows.Forms.Label();
			this.shipDateBox = new System.Windows.Forms.TextBox();
			this.factoryAddressBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.button4 = new System.Windows.Forms.Button();
			this.headerLabel = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.factoryCityBox = new System.Windows.Forms.TextBox();
			this.factoryNameBox = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.button7 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.consumerPhoneNoBox = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.consumerNoBox = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.consumerAddressBox = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.consumerCityBox = new System.Windows.Forms.TextBox();
			this.consumerNameBox = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.button24 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.headerLabel2 = new System.Windows.Forms.Label();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.consumerLevelBox = new System.Windows.Forms.TextBox();
			this.button6 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.realQuantityBox = new System.Windows.Forms.TextBox();
			this.label18 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.quantityBox = new System.Windows.Forms.TextBox();
			this.categoryDescriptionBox = new System.Windows.Forms.TextBox();
			this.label17 = new System.Windows.Forms.Label();
			this.categoryCodeBox = new System.Windows.Forms.TextBox();
			this.button23 = new System.Windows.Forms.Button();
			this.button9 = new System.Windows.Forms.Button();
			this.headerLabel3 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.dropWaitDurationBox = new System.Windows.Forms.TextBox();
			this.dropDurationBox = new System.Windows.Forms.TextBox();
			this.label24 = new System.Windows.Forms.Label();
			this.arrivalTimeBox = new System.Windows.Forms.TextBox();
			this.label23 = new System.Windows.Forms.Label();
			this.loadWaitDurationBox = new System.Windows.Forms.TextBox();
			this.loadDurationBox = new System.Windows.Forms.TextBox();
			this.shipTimeBox = new System.Windows.Forms.TextBox();
			this.headerLabel16 = new System.Windows.Forms.Label();
			this.label21 = new System.Windows.Forms.Label();
			this.label22 = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.label25 = new System.Windows.Forms.Label();
			this.lineOrderContainerTable = new System.Windows.Forms.DataGridTableStyle();
			this.containerNoCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.containerTypeCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.categoryCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.weightCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.currentLevelBox = new System.Windows.Forms.TextBox();
			this.label16 = new System.Windows.Forms.Label();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(322, 216);
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.button8);
			this.tabPage1.Controls.Add(this.button1);
			this.tabPage1.Controls.Add(this.label6);
			this.tabPage1.Controls.Add(this.factoryPhoneNoBox);
			this.tabPage1.Controls.Add(this.label5);
			this.tabPage1.Controls.Add(this.factoryNoBox);
			this.tabPage1.Controls.Add(this.button22);
			this.tabPage1.Controls.Add(this.label13);
			this.tabPage1.Controls.Add(this.shipDateBox);
			this.tabPage1.Controls.Add(this.factoryAddressBox);
			this.tabPage1.Controls.Add(this.label1);
			this.tabPage1.Controls.Add(this.button4);
			this.tabPage1.Controls.Add(this.headerLabel);
			this.tabPage1.Controls.Add(this.label3);
			this.tabPage1.Controls.Add(this.factoryCityBox);
			this.tabPage1.Controls.Add(this.factoryNameBox);
			this.tabPage1.Controls.Add(this.label4);
			this.tabPage1.Location = new System.Drawing.Point(4, 4);
			this.tabPage1.Size = new System.Drawing.Size(314, 190);
			this.tabPage1.Text = "Från";
			// 
			// button8
			// 
			this.button8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button8.Location = new System.Drawing.Point(240, 24);
			this.button8.Size = new System.Drawing.Size(72, 32);
			this.button8.Text = "Skriv ut";
			this.button8.Visible = false;
			this.button8.Click += new System.EventHandler(this.button8_Click);
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button1.Location = new System.Drawing.Point(240, 64);
			this.button1.Size = new System.Drawing.Size(72, 32);
			this.button1.Text = "Lasta";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 147);
			this.label6.Size = new System.Drawing.Size(64, 20);
			this.label6.Text = "Telefonnr:";
			// 
			// factoryPhoneNoBox
			// 
			this.factoryPhoneNoBox.Location = new System.Drawing.Point(80, 144);
			this.factoryPhoneNoBox.ReadOnly = true;
			this.factoryPhoneNoBox.Size = new System.Drawing.Size(152, 20);
			this.factoryPhoneNoBox.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 51);
			this.label5.Size = new System.Drawing.Size(64, 20);
			this.label5.Text = "Nr:";
			// 
			// factoryNoBox
			// 
			this.factoryNoBox.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.factoryNoBox.Location = new System.Drawing.Point(80, 48);
			this.factoryNoBox.ReadOnly = true;
			this.factoryNoBox.Size = new System.Drawing.Size(152, 20);
			this.factoryNoBox.Text = "";
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
			this.label13.Location = new System.Drawing.Point(8, 26);
			this.label13.Size = new System.Drawing.Size(64, 20);
			this.label13.Text = "Lastning:";
			// 
			// shipDateBox
			// 
			this.shipDateBox.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.shipDateBox.Location = new System.Drawing.Point(79, 23);
			this.shipDateBox.ReadOnly = true;
			this.shipDateBox.Size = new System.Drawing.Size(152, 20);
			this.shipDateBox.Text = "";
			// 
			// factoryAddressBox
			// 
			this.factoryAddressBox.Location = new System.Drawing.Point(80, 96);
			this.factoryAddressBox.ReadOnly = true;
			this.factoryAddressBox.Size = new System.Drawing.Size(152, 20);
			this.factoryAddressBox.Text = "";
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
			this.button4.Location = new System.Drawing.Point(240, 104);
			this.button4.Size = new System.Drawing.Size(72, 32);
			this.button4.Text = "Lossa";
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// headerLabel
			// 
			this.headerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.headerLabel.Location = new System.Drawing.Point(4, 2);
			this.headerLabel.Size = new System.Drawing.Size(227, 20);
			this.headerLabel.Text = "Biomalorder";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 75);
			this.label3.Size = new System.Drawing.Size(64, 20);
			this.label3.Text = "Namn:";
			// 
			// factoryCityBox
			// 
			this.factoryCityBox.Location = new System.Drawing.Point(80, 120);
			this.factoryCityBox.ReadOnly = true;
			this.factoryCityBox.Size = new System.Drawing.Size(152, 20);
			this.factoryCityBox.Text = "";
			// 
			// factoryNameBox
			// 
			this.factoryNameBox.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.factoryNameBox.Location = new System.Drawing.Point(80, 72);
			this.factoryNameBox.ReadOnly = true;
			this.factoryNameBox.Size = new System.Drawing.Size(152, 20);
			this.factoryNameBox.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 99);
			this.label4.Size = new System.Drawing.Size(64, 20);
			this.label4.Text = "Adress:";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.button7);
			this.tabPage2.Controls.Add(this.button2);
			this.tabPage2.Controls.Add(this.label2);
			this.tabPage2.Controls.Add(this.consumerPhoneNoBox);
			this.tabPage2.Controls.Add(this.label7);
			this.tabPage2.Controls.Add(this.consumerNoBox);
			this.tabPage2.Controls.Add(this.label8);
			this.tabPage2.Controls.Add(this.textBox3);
			this.tabPage2.Controls.Add(this.consumerAddressBox);
			this.tabPage2.Controls.Add(this.label9);
			this.tabPage2.Controls.Add(this.label11);
			this.tabPage2.Controls.Add(this.consumerCityBox);
			this.tabPage2.Controls.Add(this.consumerNameBox);
			this.tabPage2.Controls.Add(this.label14);
			this.tabPage2.Controls.Add(this.button24);
			this.tabPage2.Controls.Add(this.button5);
			this.tabPage2.Controls.Add(this.headerLabel2);
			this.tabPage2.Location = new System.Drawing.Point(4, 4);
			this.tabPage2.Size = new System.Drawing.Size(314, 190);
			this.tabPage2.Text = "Till";
			// 
			// button7
			// 
			this.button7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button7.Location = new System.Drawing.Point(240, 24);
			this.button7.Size = new System.Drawing.Size(72, 32);
			this.button7.Text = "Skriv ut";
			this.button7.Visible = false;
			this.button7.Click += new System.EventHandler(this.button7_Click);
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button2.Location = new System.Drawing.Point(240, 104);
			this.button2.Size = new System.Drawing.Size(72, 32);
			this.button2.Text = "Lossa";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 147);
			this.label2.Size = new System.Drawing.Size(64, 20);
			this.label2.Text = "Telefonnr:";
			// 
			// consumerPhoneNoBox
			// 
			this.consumerPhoneNoBox.Location = new System.Drawing.Point(80, 144);
			this.consumerPhoneNoBox.ReadOnly = true;
			this.consumerPhoneNoBox.Size = new System.Drawing.Size(152, 20);
			this.consumerPhoneNoBox.Text = "";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(8, 51);
			this.label7.Size = new System.Drawing.Size(64, 20);
			this.label7.Text = "Nr:";
			// 
			// consumerNoBox
			// 
			this.consumerNoBox.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.consumerNoBox.Location = new System.Drawing.Point(80, 48);
			this.consumerNoBox.ReadOnly = true;
			this.consumerNoBox.Size = new System.Drawing.Size(152, 20);
			this.consumerNoBox.Text = "";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(8, 26);
			this.label8.Size = new System.Drawing.Size(64, 20);
			this.label8.Text = "Lossning:";
			// 
			// textBox3
			// 
			this.textBox3.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.textBox3.Location = new System.Drawing.Point(80, 24);
			this.textBox3.ReadOnly = true;
			this.textBox3.Size = new System.Drawing.Size(152, 20);
			this.textBox3.Text = "";
			// 
			// consumerAddressBox
			// 
			this.consumerAddressBox.Location = new System.Drawing.Point(80, 96);
			this.consumerAddressBox.ReadOnly = true;
			this.consumerAddressBox.Size = new System.Drawing.Size(152, 20);
			this.consumerAddressBox.Text = "";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(8, 123);
			this.label9.Size = new System.Drawing.Size(64, 20);
			this.label9.Text = "Ort:";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(8, 75);
			this.label11.Size = new System.Drawing.Size(64, 20);
			this.label11.Text = "Namn:";
			// 
			// consumerCityBox
			// 
			this.consumerCityBox.Location = new System.Drawing.Point(80, 120);
			this.consumerCityBox.ReadOnly = true;
			this.consumerCityBox.Size = new System.Drawing.Size(152, 20);
			this.consumerCityBox.Text = "";
			// 
			// consumerNameBox
			// 
			this.consumerNameBox.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.consumerNameBox.Location = new System.Drawing.Point(80, 72);
			this.consumerNameBox.ReadOnly = true;
			this.consumerNameBox.Size = new System.Drawing.Size(152, 20);
			this.consumerNameBox.Text = "";
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(8, 99);
			this.label14.Size = new System.Drawing.Size(64, 20);
			this.label14.Text = "Adress:";
			// 
			// button24
			// 
			this.button24.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button24.Location = new System.Drawing.Point(240, 144);
			this.button24.Size = new System.Drawing.Size(72, 32);
			this.button24.Text = "Tillbaka";
			this.button24.Click += new System.EventHandler(this.button24_Click);
			// 
			// button5
			// 
			this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button5.Location = new System.Drawing.Point(240, 64);
			this.button5.Size = new System.Drawing.Size(72, 32);
			this.button5.Text = "Lasta";
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// headerLabel2
			// 
			this.headerLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.headerLabel2.Location = new System.Drawing.Point(4, 2);
			this.headerLabel2.Size = new System.Drawing.Size(227, 20);
			this.headerLabel2.Text = "Biomalorder";
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.currentLevelBox);
			this.tabPage3.Controls.Add(this.label16);
			this.tabPage3.Controls.Add(this.consumerLevelBox);
			this.tabPage3.Controls.Add(this.button6);
			this.tabPage3.Controls.Add(this.button3);
			this.tabPage3.Controls.Add(this.realQuantityBox);
			this.tabPage3.Controls.Add(this.label18);
			this.tabPage3.Controls.Add(this.label10);
			this.tabPage3.Controls.Add(this.quantityBox);
			this.tabPage3.Controls.Add(this.categoryDescriptionBox);
			this.tabPage3.Controls.Add(this.label17);
			this.tabPage3.Controls.Add(this.categoryCodeBox);
			this.tabPage3.Controls.Add(this.button23);
			this.tabPage3.Controls.Add(this.button9);
			this.tabPage3.Controls.Add(this.headerLabel3);
			this.tabPage3.Controls.Add(this.label12);
			this.tabPage3.Controls.Add(this.label15);
			this.tabPage3.Location = new System.Drawing.Point(4, 4);
			this.tabPage3.Size = new System.Drawing.Size(314, 190);
			this.tabPage3.Text = "Innehåll";
			// 
			// consumerLevelBox
			// 
			this.consumerLevelBox.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.consumerLevelBox.Location = new System.Drawing.Point(80, 160);
			this.consumerLevelBox.ReadOnly = true;
			this.consumerLevelBox.Size = new System.Drawing.Size(152, 20);
			this.consumerLevelBox.Text = "";
			// 
			// button6
			// 
			this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button6.Location = new System.Drawing.Point(240, 24);
			this.button6.Size = new System.Drawing.Size(72, 32);
			this.button6.Text = "Skriv ut";
			this.button6.Visible = false;
			this.button6.Click += new System.EventHandler(this.button6_Click);
			// 
			// button3
			// 
			this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button3.Location = new System.Drawing.Point(240, 104);
			this.button3.Size = new System.Drawing.Size(72, 32);
			this.button3.Text = "Lossa";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// realQuantityBox
			// 
			this.realQuantityBox.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.realQuantityBox.Location = new System.Drawing.Point(80, 128);
			this.realQuantityBox.ReadOnly = true;
			this.realQuantityBox.Size = new System.Drawing.Size(152, 20);
			this.realQuantityBox.Text = "";
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(8, 131);
			this.label18.Size = new System.Drawing.Size(80, 20);
			this.label18.Text = "Lossat:";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(8, 107);
			this.label10.Size = new System.Drawing.Size(64, 20);
			this.label10.Text = "Lastat:";
			// 
			// quantityBox
			// 
			this.quantityBox.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.quantityBox.Location = new System.Drawing.Point(80, 104);
			this.quantityBox.ReadOnly = true;
			this.quantityBox.Size = new System.Drawing.Size(152, 20);
			this.quantityBox.Text = "";
			// 
			// categoryDescriptionBox
			// 
			this.categoryDescriptionBox.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.categoryDescriptionBox.Location = new System.Drawing.Point(80, 48);
			this.categoryDescriptionBox.ReadOnly = true;
			this.categoryDescriptionBox.Size = new System.Drawing.Size(152, 20);
			this.categoryDescriptionBox.Text = "";
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(8, 51);
			this.label17.Size = new System.Drawing.Size(64, 20);
			this.label17.Text = "Beskrivning:";
			// 
			// categoryCodeBox
			// 
			this.categoryCodeBox.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.categoryCodeBox.Location = new System.Drawing.Point(80, 24);
			this.categoryCodeBox.ReadOnly = true;
			this.categoryCodeBox.Size = new System.Drawing.Size(152, 20);
			this.categoryCodeBox.Text = "";
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
			this.button9.Location = new System.Drawing.Point(240, 64);
			this.button9.Size = new System.Drawing.Size(72, 32);
			this.button9.Text = "Lasta";
			this.button9.Click += new System.EventHandler(this.button9_Click);
			// 
			// headerLabel3
			// 
			this.headerLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.headerLabel3.Location = new System.Drawing.Point(4, 2);
			this.headerLabel3.Size = new System.Drawing.Size(227, 20);
			this.headerLabel3.Text = "Biomalorder";
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(8, 27);
			this.label12.Size = new System.Drawing.Size(64, 20);
			this.label12.Text = "Kategori:";
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(8, 152);
			this.label15.Size = new System.Drawing.Size(80, 29);
			this.label15.Text = "Nivå efter lossning:";
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.Add(this.dropWaitDurationBox);
			this.tabPage4.Controls.Add(this.dropDurationBox);
			this.tabPage4.Controls.Add(this.label24);
			this.tabPage4.Controls.Add(this.arrivalTimeBox);
			this.tabPage4.Controls.Add(this.label23);
			this.tabPage4.Controls.Add(this.loadWaitDurationBox);
			this.tabPage4.Controls.Add(this.loadDurationBox);
			this.tabPage4.Controls.Add(this.shipTimeBox);
			this.tabPage4.Controls.Add(this.headerLabel16);
			this.tabPage4.Controls.Add(this.label21);
			this.tabPage4.Controls.Add(this.label22);
			this.tabPage4.Controls.Add(this.label20);
			this.tabPage4.Controls.Add(this.label25);
			this.tabPage4.Location = new System.Drawing.Point(4, 4);
			this.tabPage4.Size = new System.Drawing.Size(314, 190);
			this.tabPage4.Text = "Tider";
			// 
			// dropWaitDurationBox
			// 
			this.dropWaitDurationBox.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.dropWaitDurationBox.Location = new System.Drawing.Point(160, 160);
			this.dropWaitDurationBox.ReadOnly = true;
			this.dropWaitDurationBox.Size = new System.Drawing.Size(144, 20);
			this.dropWaitDurationBox.Text = "";
			// 
			// dropDurationBox
			// 
			this.dropDurationBox.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.dropDurationBox.Location = new System.Drawing.Point(8, 160);
			this.dropDurationBox.ReadOnly = true;
			this.dropDurationBox.Size = new System.Drawing.Size(144, 20);
			this.dropDurationBox.Text = "";
			// 
			// label24
			// 
			this.label24.Location = new System.Drawing.Point(8, 144);
			this.label24.Size = new System.Drawing.Size(88, 20);
			this.label24.Text = "Lossningstid:";
			// 
			// arrivalTimeBox
			// 
			this.arrivalTimeBox.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.arrivalTimeBox.Location = new System.Drawing.Point(8, 120);
			this.arrivalTimeBox.ReadOnly = true;
			this.arrivalTimeBox.Size = new System.Drawing.Size(296, 20);
			this.arrivalTimeBox.Text = "";
			// 
			// label23
			// 
			this.label23.Location = new System.Drawing.Point(8, 104);
			this.label23.Size = new System.Drawing.Size(96, 20);
			this.label23.Text = "Lossat klockslag:";
			// 
			// loadWaitDurationBox
			// 
			this.loadWaitDurationBox.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.loadWaitDurationBox.Location = new System.Drawing.Point(160, 80);
			this.loadWaitDurationBox.ReadOnly = true;
			this.loadWaitDurationBox.Size = new System.Drawing.Size(144, 20);
			this.loadWaitDurationBox.Text = "";
			// 
			// loadDurationBox
			// 
			this.loadDurationBox.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.loadDurationBox.Location = new System.Drawing.Point(8, 80);
			this.loadDurationBox.ReadOnly = true;
			this.loadDurationBox.Size = new System.Drawing.Size(144, 20);
			this.loadDurationBox.Text = "";
			// 
			// shipTimeBox
			// 
			this.shipTimeBox.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.shipTimeBox.Location = new System.Drawing.Point(8, 40);
			this.shipTimeBox.ReadOnly = true;
			this.shipTimeBox.Size = new System.Drawing.Size(296, 20);
			this.shipTimeBox.Text = "";
			// 
			// headerLabel16
			// 
			this.headerLabel16.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.headerLabel16.Location = new System.Drawing.Point(4, 2);
			this.headerLabel16.Size = new System.Drawing.Size(227, 20);
			this.headerLabel16.Text = "Biomalorder";
			// 
			// label21
			// 
			this.label21.Location = new System.Drawing.Point(8, 24);
			this.label21.Size = new System.Drawing.Size(96, 20);
			this.label21.Text = "Lastat klockslag:";
			// 
			// label22
			// 
			this.label22.Location = new System.Drawing.Point(8, 64);
			this.label22.Size = new System.Drawing.Size(80, 20);
			this.label22.Text = "Lastningstid:";
			// 
			// label20
			// 
			this.label20.Location = new System.Drawing.Point(160, 64);
			this.label20.Size = new System.Drawing.Size(64, 20);
			this.label20.Text = "Väntetid:";
			// 
			// label25
			// 
			this.label25.Location = new System.Drawing.Point(160, 144);
			this.label25.Size = new System.Drawing.Size(64, 20);
			this.label25.Text = "Väntetid:";
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
			// currentLevelBox
			// 
			this.currentLevelBox.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.currentLevelBox.Location = new System.Drawing.Point(80, 80);
			this.currentLevelBox.ReadOnly = true;
			this.currentLevelBox.Size = new System.Drawing.Size(152, 20);
			this.currentLevelBox.Text = "";
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(8, 83);
			this.label16.Size = new System.Drawing.Size(80, 20);
			this.label16.Text = "Aktuell nivå:";
			// 
			// FactoryOrder
			// 
			this.ClientSize = new System.Drawing.Size(322, 216);
			this.Controls.Add(this.tabControl1);
			this.Text = "Linjeorder";

		}
		#endregion

		private void setOrderStatus(int status)
		{
			if ((status == 3) && (dataFactoryOrder.status < 3))
			{
				if (System.Windows.Forms.MessageBox.Show("Är du säker på att du vill lasta ordern?", "Lasta", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.Yes)				
				{
					return;
				}
			}

			if ((status == 4) && (dataFactoryOrder.status == 3))
			{
				if (System.Windows.Forms.MessageBox.Show("Är du säker på att du vill lossa ordern?", "Lossa", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.Yes)				
				{
					return;
				}
			}

			if (dataFactoryOrder.getFromDb())
			{
				FactoryOrderDetails factoryOrderDetails = new FactoryOrderDetails(smartDatabase, agentStatus, dataFactoryOrder, status);
				factoryOrderDetails.ShowDialog();
				if (factoryOrderDetails.getFormStatus() == 0) return;				

				FactoryOrderTime factoryOrderTime = new FactoryOrderTime(smartDatabase, agentStatus, dataFactoryOrder, status);
				factoryOrderTime.ShowDialog();
				if (factoryOrderTime.getFormStatus() == 0) return;				

				if (MessageBox.Show("Vill du skriva ut en fraktsedel?", "Skriv ut", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
				{
					this.printOrder();
				}

				dataFactoryOrder.status = status;
				dataFactoryOrder.commit();

				if (status == 3)
				{
					dataFactoryOrder.factoryPositionX = agentStatus.rt90x;
					dataFactoryOrder.factoryPositionY = agentStatus.rt90y;

					dataFactoryOrder.commit();
				}

				if (status == 4)
				{
					dataFactoryOrder.consumerPositionX = agentStatus.rt90x;
					dataFactoryOrder.consumerPositionY = agentStatus.rt90y;

					dataFactoryOrder.commit();
				}

				DataSyncActions dataSyncActions = new DataSyncActions(smartDatabase);
				dataSyncActions.addSyncAction(9, 0, dataFactoryOrder.entryNo.ToString());
			}



			this.Close();
		}

		private void printOrder()
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

				if (dataFactoryOrder.status == 4) printer.setCopy();

				printer.printFactoryOrder(dataFactoryOrder);
				printer.close();

				Cursor.Current = Cursors.Default;
				Cursor.Hide();
			}
		}

		private void button22_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void button31_Click(object sender, System.EventArgs e)
		{
			if (dataFactoryOrder.status < 3)
			{
				Navigator navigator = new Navigator(smartDatabase);
				navigator.navigate(dataFactoryOrder.factoryPositionY, dataFactoryOrder.factoryPositionX, dataFactoryOrder.factoryName);
			}
			else
			{
				Navigator navigator = new Navigator(smartDatabase);
				navigator.navigate(dataFactoryOrder.consumerPositionY, dataFactoryOrder.consumerPositionX, dataFactoryOrder.consumerName);

			}
		}


		private void button24_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}


		private void button23_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void button4_Click(object sender, System.EventArgs e)
		{
			if (dataFactoryOrder.status == 3)
			{
				this.setOrderStatus(4);
			}
			else
			{
				MessageBox.Show("Ordern är inte lastad.", "Fel", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
			}
		}

		private void button5_Click(object sender, System.EventArgs e)
		{
			this.setOrderStatus(3);
		}

		private void button9_Click(object sender, System.EventArgs e)
		{
			this.setOrderStatus(3);
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			this.setOrderStatus(3);

		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			if (dataFactoryOrder.status == 3)
			{
				this.setOrderStatus(4);
			}
			else
			{
				MessageBox.Show("Ordern är inte lastad.", "Fel", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
			}

		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			if (dataFactoryOrder.status == 3)
			{
				this.setOrderStatus(4);
			}
			else
			{
				MessageBox.Show("Ordern är inte lastad.", "Fel", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
			}

		}

		private void button6_Click(object sender, System.EventArgs e)
		{
			printOrder();
		}

		private void button7_Click(object sender, System.EventArgs e)
		{
			printOrder();
		}

		private void button8_Click(object sender, System.EventArgs e)
		{
			printOrder();
		}

	}
}
