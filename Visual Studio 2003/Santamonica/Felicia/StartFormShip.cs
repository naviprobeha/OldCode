namespace Navipro.SantaMonica.Felicia
{
	using System;
	using System.Drawing;
	using System.Collections;
	using System.Windows.Forms;
	using System.Data;
	using Navipro.SantaMonica.Goldfinger;
	using System.Threading;


	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class StartFormShip : System.Windows.Forms.Form, GpsListener, NotifyForm
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;

		private const string dbFileName = "Felicia.sdf";

		private SmartDatabase smartDatabase;
		private GpsComm gpsComm;
		private CommHandler commHandler;
		private System.Windows.Forms.Label statusLabel;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.DataGridTextBoxColumn nameCol;
		private System.Windows.Forms.DataGridTextBoxColumn cityCol;
		private System.Windows.Forms.DataGridTextBoxColumn statusCol;
		private System.Windows.Forms.DataGrid shipOrderGrid;
		private System.Windows.Forms.DataGridTableStyle shipOrderTable;
		private Status status;
		private System.Windows.Forms.TextBox statusBox;

		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.DataGridTextBoxColumn priorityCol;

		private DataSet shipOrderDataSet;
		private string statusText;
		public delegate void setStatusTextDelegate();

		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.MenuItem menuItem10;
		private System.Windows.Forms.MenuItem menuItem11;
		private System.Windows.Forms.MenuItem menuItem12;
		private System.Windows.Forms.MenuItem menuItem13;
		private System.Windows.Forms.MenuItem menuItem14;
		private System.Windows.Forms.DataGridTextBoxColumn commentCol;

		private System.Windows.Forms.Timer timer;
		private DateTime notifyDateTime;

		private bool messagesIsShown;
		private System.Windows.Forms.MenuItem menuItem15;
		private System.Windows.Forms.MenuItem menuItem16;
		private int currentPriorityView;

		public StartFormShip()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			shipOrderGrid.Font = new Font(shipOrderGrid.Font.Name, 9, FontStyle.Regular);
			shipOrderGrid.RowHeadersVisible = false;		
		
			this.currentPriorityView = 0;

			smartDatabase = new SmartDatabase(dbFileName);
			if (!smartDatabase.init())
			{
				smartDatabase.createDatabase();
				System.Windows.Forms.MessageBox.Show("Databas skapad.");

				Application.Exit();
				return;
			}


			smartDatabase.debug = false;

			if (smartDatabase.debug) System.Windows.Forms.MessageBox.Show("Starting GPS");
			gpsComm = new GpsComm(smartDatabase);

			//gpsComm.addGpsListener(this);
			gpsComm.onHeadingUpdate +=new Navipro.SantaMonica.Felicia.GpsComm.headingUpdateEventHandler(gpsComm_onHeadingUpdate);
			gpsComm.onPositionUpdate +=new Navipro.SantaMonica.Felicia.GpsComm.positionUpdateEventHandler(gpsComm_onPositionUpdate);
			gpsComm.onDataUpdate +=new Navipro.SantaMonica.Felicia.GpsComm.dataUpdateEventHandler(gpsComm_onDataUpdate);


			status = new Status(smartDatabase);

			if (smartDatabase.debug) System.Windows.Forms.MessageBox.Show("Starting Comm");
			commHandler = new CommHandler(smartDatabase, status, this);

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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(StartFormShip));
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.menuItem10 = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.menuItem11 = new System.Windows.Forms.MenuItem();
			this.menuItem12 = new System.Windows.Forms.MenuItem();
			this.menuItem13 = new System.Windows.Forms.MenuItem();
			this.menuItem14 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.statusLabel = new System.Windows.Forms.Label();
			this.shipOrderGrid = new System.Windows.Forms.DataGrid();
			this.shipOrderTable = new System.Windows.Forms.DataGridTableStyle();
			this.nameCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.cityCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.statusCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.commentCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.priorityCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.statusBox = new System.Windows.Forms.TextBox();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.button4 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.menuItem15 = new System.Windows.Forms.MenuItem();
			this.menuItem16 = new System.Windows.Forms.MenuItem();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.Add(this.menuItem1);
			this.mainMenu1.MenuItems.Add(this.menuItem2);
			this.mainMenu1.MenuItems.Add(this.menuItem11);
			this.mainMenu1.MenuItems.Add(this.menuItem3);
			// 
			// menuItem1
			// 
			this.menuItem1.MenuItems.Add(this.menuItem4);
			this.menuItem1.Text = "Arkiv";
			// 
			// menuItem4
			// 
			this.menuItem4.Text = "Avsluta";
			this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.MenuItems.Add(this.menuItem5);
			this.menuItem2.MenuItems.Add(this.menuItem8);
			this.menuItem2.MenuItems.Add(this.menuItem10);
			this.menuItem2.MenuItems.Add(this.menuItem9);
			this.menuItem2.Text = "Visa";
			// 
			// menuItem5
			// 
			this.menuItem5.Text = "GPS Info";
			this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
			// 
			// menuItem8
			// 
			this.menuItem8.Text = "Registrerade följesedlar";
			this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
			// 
			// menuItem10
			// 
			this.menuItem10.Text = "Registrerade körorder";
			this.menuItem10.Click += new System.EventHandler(this.menuItem10_Click);
			// 
			// menuItem9
			// 
			this.menuItem9.Text = "Lastade körorder";
			this.menuItem9.Click += new System.EventHandler(this.menuItem9_Click);
			// 
			// menuItem11
			// 
			this.menuItem11.MenuItems.Add(this.menuItem12);
			this.menuItem11.MenuItems.Add(this.menuItem13);
			this.menuItem11.MenuItems.Add(this.menuItem14);
			this.menuItem11.MenuItems.Add(this.menuItem15);
			this.menuItem11.MenuItems.Add(this.menuItem16);
			this.menuItem11.Text = "Verktyg";
			// 
			// menuItem12
			// 
			this.menuItem12.Text = "Bekräfta alla körorder";
			this.menuItem12.Click += new System.EventHandler(this.menuItem12_Click);
			// 
			// menuItem13
			// 
			this.menuItem13.Text = "-";
			// 
			// menuItem14
			// 
			this.menuItem14.Text = "Navigera till position";
			this.menuItem14.Click += new System.EventHandler(this.menuItem14_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.MenuItems.Add(this.menuItem7);
			this.menuItem3.Text = "Inställningar";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// menuItem7
			// 
			this.menuItem7.Text = "Allmänt";
			this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.button1.Location = new System.Drawing.Point(104, 152);
			this.button1.Size = new System.Drawing.Size(64, 32);
			this.button1.Text = "Karta";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.button2.Location = new System.Drawing.Point(176, 152);
			this.button2.Size = new System.Drawing.Size(64, 32);
			this.button2.Text = "Order";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.button3.Location = new System.Drawing.Point(248, 152);
			this.button3.Size = new System.Drawing.Size(64, 32);
			this.button3.Text = "Status";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// statusLabel
			// 
			this.statusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.statusLabel.Location = new System.Drawing.Point(48, 6);
			this.statusLabel.Size = new System.Drawing.Size(176, 20);
			this.statusLabel.Text = "Utstämplad";
			// 
			// shipOrderGrid
			// 
			this.shipOrderGrid.Location = new System.Drawing.Point(0, 32);
			this.shipOrderGrid.Size = new System.Drawing.Size(312, 112);
			this.shipOrderGrid.TableStyles.Add(this.shipOrderTable);
			this.shipOrderGrid.Text = "shipOrderGrid";
			this.shipOrderGrid.Click += new System.EventHandler(this.shippingOrderGrid_Click);
			// 
			// shipOrderTable
			// 
			this.shipOrderTable.GridColumnStyles.Add(this.nameCol);
			this.shipOrderTable.GridColumnStyles.Add(this.cityCol);
			this.shipOrderTable.GridColumnStyles.Add(this.statusCol);
			this.shipOrderTable.GridColumnStyles.Add(this.commentCol);
			this.shipOrderTable.GridColumnStyles.Add(this.priorityCol);
			this.shipOrderTable.MappingName = "shipOrder";
			// 
			// nameCol
			// 
			this.nameCol.HeaderText = "Namn";
			this.nameCol.MappingName = "shipName";
			this.nameCol.NullText = "";
			this.nameCol.Width = 150;
			// 
			// cityCol
			// 
			this.cityCol.HeaderText = "Ort";
			this.cityCol.MappingName = "shipCity";
			this.cityCol.NullText = "";
			this.cityCol.Width = 95;
			// 
			// statusCol
			// 
			this.statusCol.HeaderText = "Status";
			this.statusCol.MappingName = "statusText";
			this.statusCol.NullText = "";
			this.statusCol.Width = 30;
			// 
			// commentCol
			// 
			this.commentCol.HeaderText = "Kommentar";
			this.commentCol.MappingName = "comments";
			this.commentCol.NullText = "";
			this.commentCol.Width = 10;
			// 
			// priorityCol
			// 
			this.priorityCol.HeaderText = "Prio";
			this.priorityCol.MappingName = "priority";
			this.priorityCol.NullText = "";
			this.priorityCol.Width = 30;
			// 
			// menuItem6
			// 
			this.menuItem6.Text = "Allmänt";
			this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
			// 
			// statusBox
			// 
			this.statusBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular);
			this.statusBox.Location = new System.Drawing.Point(240, 8);
			this.statusBox.ReadOnly = true;
			this.statusBox.Size = new System.Drawing.Size(72, 18);
			this.statusBox.Text = "";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Size = new System.Drawing.Size(40, 56);
			// 
			// button4
			// 
			this.button4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
			this.button4.Location = new System.Drawing.Point(24, 152);
			this.button4.Size = new System.Drawing.Size(32, 32);
			this.button4.Text = "<";
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// button5
			// 
			this.button5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
			this.button5.Location = new System.Drawing.Point(64, 152);
			this.button5.Size = new System.Drawing.Size(32, 32);
			this.button5.Text = ">";
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// menuItem15
			// 
			this.menuItem15.Text = "-";
			// 
			// menuItem16
			// 
			this.menuItem16.Text = "Prioritetsvy";
			this.menuItem16.Click += new System.EventHandler(this.menuItem16_Click);
			// 
			// StartFormShip
			// 
			this.ClientSize = new System.Drawing.Size(322, 193);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.statusBox);
			this.Controls.Add(this.shipOrderGrid);
			this.Controls.Add(this.statusLabel);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.pictureBox1);
			this.Menu = this.mainMenu1;
			this.MinimizeBox = false;
			this.Text = "SmartOrder Uppsamling";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.StartForm_Closing);
			this.Load += new System.EventHandler(this.StartForm_Load);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>


		private void button3_Click(object sender, System.EventArgs e)
		{
			changeStatus();
		}

		private void menuItem4_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void StartForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			Cursor.Show();

			commHandler.stop();			
			gpsComm.close();
			this.timer.Enabled = false;


			commHandler.waitForTermination();
		
			//Monitor.Enter(lockingObject);
			//Monitor.Exit(lockingObject);

			Cursor.Current = Cursors.Default;
			Cursor.Hide();
			
			Application.Exit();
			
		}

		private void menuItem5_Click(object sender, System.EventArgs e)
		{
			GpsInfo gpsInfo = new GpsInfo(status, gpsComm);
			//gpsComm.addGpsListener(gpsInfo);
			gpsInfo.ShowDialog();
			//gpsComm.removeGpsListener(gpsInfo);
			gpsInfo.Dispose();
		}

		private void menuItem6_Click(object sender, System.EventArgs e)
		{
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
		
		}

		private void menuItem7_Click(object sender, System.EventArgs e)
		{
			GeneralSettings settings = new GeneralSettings(smartDatabase);
			settings.ShowDialog();
			smartDatabase.getSetup().refresh();		
			settings.Dispose();
		}

		public void updateGrid(object sender, EventArgs e)
		{

			if (status.status > 0)
			{
				int currentPosition = shipOrderGrid.CurrentRowIndex;

				DataShipOrders dataShipOrders = new DataShipOrders(smartDatabase);
				if (currentPriorityView == 0)
				{
					shipOrderDataSet = dataShipOrders.getDataSet();
				}
				else
				{
					shipOrderDataSet = dataShipOrders.getDataSet(currentPriorityView);
				}

				int i = 0;
				while(i < shipOrderDataSet.Tables[0].Rows.Count)
				{
					if (shipOrderDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() == "0")
					{
						DataRow row = shipOrderDataSet.Tables[0].Rows[i];
						row["statusText"] = "Otilldelad";
					}
					if (shipOrderDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() == "1")
					{
						DataRow row = shipOrderDataSet.Tables[0].Rows[i];
						row["statusText"] = "Nej tack";
					}
					if (shipOrderDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() == "2")
					{
						DataRow row = shipOrderDataSet.Tables[0].Rows[i];
						row["statusText"] = "Kanske";
					}
					if (shipOrderDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() == "3")
					{
						DataRow row = shipOrderDataSet.Tables[0].Rows[i];
						row["statusText"] = "Uppköad";
					}
					if (shipOrderDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() == "4")
					{
						DataRow row = shipOrderDataSet.Tables[0].Rows[i];
						row["statusText"] = "Ny";
					}
					if (shipOrderDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() == "5")
					{
						DataRow row = shipOrderDataSet.Tables[0].Rows[i];
						row["statusText"] = "Ja tack";
					}
					if (shipOrderDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() == "6")
					{
						DataRow row = shipOrderDataSet.Tables[0].Rows[i];
						row["statusText"] = "Lastad";
					}
					if (shipOrderDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() == "7")
					{
						DataRow row = shipOrderDataSet.Tables[0].Rows[i];
						row["statusText"] = "Kanske";
					}
					if (shipOrderDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() == "8")
					{
						DataRow row = shipOrderDataSet.Tables[0].Rows[i];
						row["statusText"] = "Nej tack";
					}
					if (shipOrderDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() == "20")
					{
						DataRow row = shipOrderDataSet.Tables[0].Rows[i];
						row["statusText"] = "Omtilldelad";
					}
					shipOrderDataSet.Tables[0].Rows[i].AcceptChanges();
					i++;
				}


				shipOrderGrid.DataSource = shipOrderDataSet.Tables[0];

				//try
				//{
				//	if (shipOrderDataSet.Tables[0].Rows.Count >= currentPosition) shipOrderGrid.CurrentRowIndex = currentPosition;
				//}
				//catch(Exception ex)
				//{}
			}
			else
			{
				shipOrderGrid.DataSource = null;
			}

		}

		private void StartForm_Load(object sender, System.EventArgs e)
		{

			timer = new System.Windows.Forms.Timer();
			timer.Tick +=new EventHandler(timer_Tick);
			timer.Interval = 1000;
			timer.Enabled = true;

			

			updateGrid(this, null);

			if (smartDatabase.getSetup().applicationMode == 2)
			{
				if (MessageBox.Show("Du är utloggad. Logga in?", "Logga in", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
				{
					changeStatus();
				}
			}
		}

		public void changeStatus()
		{

			if ((status.mobileUserName == "") || (status.mobileUserName == null))
			{
				Login login = new Login(smartDatabase);
				login.ShowDialog();
				if (login.getStatus() == 1)
				{
					status.mobileUserName = login.getMobileUserName();
					status.status = 1;
					login.Dispose();

				}
				else
				{
					status.mobileUserName = "";
					status.status = 0;
				}
			}

			if ((status.mobileUserName != "") && (status.mobileUserName != null))
			{

				StatusChangeShip statusChange = new StatusChangeShip(status);
				statusChange.ShowDialog();		
				statusChange.Dispose();


				if ((status.mobileUserName != "") && (status.mobileUserName != null))
				{
					statusLabel.Text = status.mobileUserName + " ("+status.getStatusText()+")";
				}
				else
				{
					statusLabel.Text = status.getStatusText();
				}

			}

			updateGrid(this, null);

		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			if (status.status > 0)
			{
				if (status.containerNo == "")
				{
					MessageBox.Show("Det finns ingen container på flaket. Klicka på Status, och lasta en container.", "Container", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
					return;
				}

				int orderType = 0;

				if (smartDatabase.getSetup().createShipOrder)
				{
					NewOrder newOrder = new NewOrder();
					newOrder.ShowDialog();
					orderType = newOrder.getStatus();
				}
				else
				{
					orderType = 2;
				}

				if (orderType > 0)
				{
					CustomerList customerList = new CustomerList(smartDatabase);

					customerList.ShowDialog();
					int customerStatus = customerList.getStatus();
					string customerNo = customerList.getCustomerNo();
					customerList.Dispose();

					if (customerStatus == 1)
					{
						DataCustomer dataCustomer = new DataCustomer(smartDatabase, customerNo);
						string customerShipAddressNo = "";

						if (dataCustomer.hasShipAddresses())
						{
							CustomerShipAddresses customerShipAddresses = new CustomerShipAddresses(smartDatabase, dataCustomer);
							customerShipAddresses.ShowDialog();
							int shipAddressStatus = customerShipAddresses.getStatus();
							customerShipAddressNo = customerShipAddresses.getCustomerShipAddressNo();
							customerShipAddresses.Dispose();

							if (shipAddressStatus == 0) return;
							if (shipAddressStatus == 2) customerShipAddressNo = "";
						}

						if (orderType == 1)
						{
							DataOrderHeader dataOrderHeader = new DataOrderHeader(smartDatabase, dataCustomer, customerShipAddressNo);
							dataOrderHeader.mobileUserName = status.mobileUserName;
							dataOrderHeader.containerNo = status.containerNo;
							dataOrderHeader.commit();

							Order order = new Order(smartDatabase, dataOrderHeader);
							order.ShowDialog();
							order.Dispose();
						}

						if (orderType == 2)
						{
							DataShipmentHeader dataShipmentHeader = new DataShipmentHeader(smartDatabase, dataCustomer, customerShipAddressNo);
							dataShipmentHeader.mobileUserName = status.mobileUserName;
							dataShipmentHeader.containerNo = status.containerNo;
							dataShipmentHeader.commit();

							Shipment shipment = new Shipment(smartDatabase, dataShipmentHeader, status);
							shipment.ShowDialog();
							shipment.Dispose();
						}

					}
				}
			}
			else
			{
				if (System.Windows.Forms.MessageBox.Show("Du är inte inloggad. Logga in?", "Logga in", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
				{
					button3_Click(sender, e);

				}
			}

		}

		private void shippingOrderGrid_Click(object sender, System.EventArgs e)
		{
			if (status.status > 0)
			{

				if (shipOrderDataSet.Tables[0].Rows.Count > 0)
				{
					if ((shipOrderGrid.CurrentRowIndex > -1) && (shipOrderGrid.DataSource != null))
					{
						Cursor.Current = Cursors.WaitCursor;
						Cursor.Show();

						DataShipOrder dataShipOrder = new DataShipOrder(smartDatabase, int.Parse(shipOrderDataSet.Tables[0].Rows[shipOrderGrid.CurrentRowIndex].ItemArray.GetValue(0).ToString()));
						ShipOrder shipOrder = new ShipOrder(smartDatabase, dataShipOrder, status);

						Cursor.Current = Cursors.Default;
						Cursor.Hide();

						shipOrder.ShowDialog();
						shipOrder.Dispose();

						updateGrid(this, null);
					}
				}
			}
		}

		public void invokeSetStatusText(string text)
		{
			statusText = text;
		}

		public void invokeUpdateGrid()
		{
			Invoke(new EventHandler(updateGrid));
		}

		public void setStatusTextBox(object sender, EventArgs e)
		{
			statusBox.Text = statusText;
			statusBox.Update();
		}



		private void button1_Click(object sender, System.EventArgs e)
		{
			Navigator navigator = new Navigator(smartDatabase);
			navigator.start();

		}
		#region GpsListener Members

		public void onRawDataReceive(string gpsString)
		{
			// TODO:  Add StartForm.onRawDataReceive implementation

			status.lastUpdated = DateTime.Now;
		}

		public void onPositionReceive(int x, int y, int height)
		{
			// TODO:  Add StartForm.onPositionReceive implementation
			status.rt90x = x;
			status.rt90y = y;
			status.height = height;

			status.lastUpdated = DateTime.Now;

		}

		public void onHeadingReceive(int heading, int speed)
		{
			// TODO:  Add StartForm.onHeadingReceive implementation
			status.heading = heading;
			status.speed = speed;
		}

		#endregion

		public Status getCurrentStatus()
		{
			return status;
		}

		private void menuItem8_Click(object sender, System.EventArgs e)
		{
			Shipments shipments = new Shipments(smartDatabase, status);
			shipments.ShowDialog();

			shipments.Dispose();
		}

		private void menuItem9_Click(object sender, System.EventArgs e)
		{

			ShippedOrders shippedOrders = new ShippedOrders(smartDatabase, status);
			shippedOrders.ShowDialog();

			shippedOrders.Dispose();
		}

		private void button4_Click(object sender, System.EventArgs e)
		{
			if ((shipOrderDataSet != null) && (shipOrderDataSet.Tables[0].Rows.Count > 0))
			{
				if (this.shipOrderGrid.CurrentRowIndex > 0)
				{
					this.shipOrderGrid.CurrentRowIndex = this.shipOrderGrid.CurrentRowIndex - 1;
					this.shipOrderGrid.Select(this.shipOrderGrid.CurrentRowIndex);
				}
			}
		}

		private void button5_Click(object sender, System.EventArgs e)
		{
			if ((shipOrderDataSet != null) && (shipOrderDataSet.Tables[0].Rows.Count > 0))
			{
				if (this.shipOrderGrid.CurrentRowIndex < (shipOrderDataSet.Tables[0].Rows.Count -1))
				{
					this.shipOrderGrid.CurrentRowIndex = this.shipOrderGrid.CurrentRowIndex + 1;
					this.shipOrderGrid.Select(this.shipOrderGrid.CurrentRowIndex);
				}
			}
		}

		private void menuItem10_Click(object sender, System.EventArgs e)
		{
			Orders orders = new Orders(smartDatabase);
			orders.ShowDialog();

			orders.Dispose();

		}

		private void menuItem12_Click(object sender, System.EventArgs e)
		{
			if (MessageBox.Show("Vill du bekräfta alla nya körorder?", "Bekräfta", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
			{
				DataShipOrders dataShipOrders = new DataShipOrders(smartDatabase);
				shipOrderDataSet = dataShipOrders.getDataSet();

				int i = 0;
				while(i < shipOrderDataSet.Tables[0].Rows.Count)
				{
					if (shipOrderDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() == "4")
					{
						DataShipOrder dataShipOrder = new DataShipOrder(smartDatabase, int.Parse(shipOrderDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString())); 
						dataShipOrder.status = 5;
						dataShipOrder.commit();

						DataSyncActions dataSyncActions = new DataSyncActions(smartDatabase);
						dataSyncActions.addSyncAction(1, 0, dataShipOrder.entryNo.ToString());
					}

					i++;
				}

				updateGrid(sender, e);

			}

		}

		private void gpsComm_onHeadingUpdate(object sender, EventArgs e, int heading, int speed)
		{
			status.updateSpeed(speed, heading);
		}

		private void gpsComm_onPositionUpdate(object sender, EventArgs e, int x, int y, int height)
		{
			status.updatePosition(x, y, height);
		}

		private void gpsComm_onDataUpdate(object sender, EventArgs e, string data)
		{

		}

		private void menuItem14_Click(object sender, System.EventArgs e)
		{
			NavigatePosition navigatePosition = new NavigatePosition(smartDatabase);
			navigatePosition.ShowDialog();
			navigatePosition.Dispose();
		}

		private void timer_Tick(object sender, EventArgs e)
		{

			if (notifyDateTime < DateTime.Now)
			{

				DataShipOrders dataShipOrders = new DataShipOrders(smartDatabase);
				if (dataShipOrders.checkIfNewShipOrders())
				{
					Sound sound = new Sound(0);
					sound.Play();

				}

				if (status.status > 0)
				{
					if (!this.messagesIsShown)
					{
						DataMessages dataMessages = new DataMessages(smartDatabase);
						DataMessage dataMessage = dataMessages.getFirstMessage();
						if (dataMessage != null)
						{
							this.messagesIsShown = true;
							MessageHandler messageHandler = new MessageHandler(smartDatabase);
							messageHandler.handleMessages(this, null);
							this.messagesIsShown = false;
						}
					}
				}

				if (gridIsUpdated()) updateGrid(this, null);

				notifyDateTime = DateTime.Now.AddSeconds(15);

			}

			setStatusTextBox(this, null);

			if (!commHandler.checkConnection())
			{
				//	if (!this.messagesIsShown)
				//	{
				//		this.messagesIsShown = true;
				//		System.Windows.Forms.MessageBox.Show("Du har blivit frånkopplad från Internet. Anslutningen kunde inte återupprättas. Prova att trycka på Reset-knappen.", "Anslutning", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
				//	}
				commHandler.stop();
				commHandler = new CommHandler(smartDatabase, status, this);
			}
			

		}

		private bool gridIsUpdated()
		{
			if (status.status > 0)
			{
				DataShipOrders dataShipOrders = new DataShipOrders(smartDatabase);
				shipOrderDataSet = dataShipOrders.getDataSet();

				if (this.shipOrderGrid.DataSource != null)
				{
					if (((DataTable)(this.shipOrderGrid.DataSource)).Rows.Count != shipOrderDataSet.Tables[0].Rows.Count) return true;
				}

			}
			return false;

		}

		private void menuItem16_Click(object sender, System.EventArgs e)
		{
			PriorityView priorityView = new PriorityView(smartDatabase);
			priorityView.ShowDialog();
			
			this.currentPriorityView = priorityView.getPriorityView();

			priorityView.Dispose();

			updateGrid(this, null);
		}
	}
}
