using System;
using System.Data;
using System.Threading;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for StartFormLine.
	/// </summary>
	public class StartFormLine : System.Windows.Forms.Form, NotifyForm
	{
		private System.Windows.Forms.TextBox statusBox;
		private System.Windows.Forms.DataGrid lineOrderGrid;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Label statusLabel;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.PictureBox pictureBox1;
	

		private const string dbFileName = "Felicia.sdf";

		private SmartDatabase smartDatabase;
		private GpsComm gpsComm;
		private CommHandler commHandler;
		private Status status;
		private DataSet lineOrderDataSet;
		private System.Windows.Forms.Button button2;
		private string statusText;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.DataGridTableStyle lineOrderTable;
		private System.Windows.Forms.DataGridTextBoxColumn nameCol;
		private System.Windows.Forms.DataGridTextBoxColumn cityCol;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem8;
		private MenuItem newOrderItem = new MenuItem();
		private MenuItem listOrderItem = new MenuItem();
		private System.Windows.Forms.DataGridTextBoxColumn qtyContainersCol;
	
		public delegate void setStatusTextDelegate();
		private DataLineJournal currentLineJournal;
		private System.Windows.Forms.DataGridTextBoxColumn detailsCol;

		private System.Windows.Forms.Timer timer;
		private DateTime notifyDateTime;


		private int myPosX = 0;
		private int myPosY = 0;
		private System.Windows.Forms.Button button1;

		private bool messagesIsShown;

		public StartFormLine()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			lineOrderGrid.Font = new Font(lineOrderGrid.Font.Name, 9, FontStyle.Regular);
			lineOrderGrid.RowHeadersVisible = false;		
		

			smartDatabase = new SmartDatabase(dbFileName);
			if (!smartDatabase.init())
			{
				smartDatabase.createDatabase();
				System.Windows.Forms.MessageBox.Show("Databas skapad.");

				Application.Exit();
				return;
			}


			if (smartDatabase.getSetup().createShipOrder == true)
			{
				newOrderItem = new MenuItem();
				newOrderItem.Text = "Ny uppsamlingsorder";
				newOrderItem.Click +=new EventHandler(newOrderItem_Click);

				listOrderItem = new MenuItem();
				listOrderItem.Text = "Registrerade uppsamlingsorder";
				listOrderItem.Click +=new EventHandler(listOrderItem_Click);

				menuItem3.MenuItems.Add(newOrderItem);
				menuItem2.MenuItems.Add(listOrderItem);
			}

			smartDatabase.debug = false;

			if (smartDatabase.debug) System.Windows.Forms.MessageBox.Show("Starting GPS");
			gpsComm = new GpsComm(smartDatabase);

			gpsComm.onHeadingUpdate += new Navipro.SantaMonica.Felicia.GpsComm.headingUpdateEventHandler(gpsComm_onHeadingUpdate);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(StartFormLine));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.statusBox = new System.Windows.Forms.TextBox();
			this.lineOrderGrid = new System.Windows.Forms.DataGrid();
			this.lineOrderTable = new System.Windows.Forms.DataGridTableStyle();
			this.nameCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.cityCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.detailsCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.qtyContainersCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.button5 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.statusLabel = new System.Windows.Forms.Label();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Size = new System.Drawing.Size(40, 56);
			// 
			// statusBox
			// 
			this.statusBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular);
			this.statusBox.Location = new System.Drawing.Point(240, 8);
			this.statusBox.ReadOnly = true;
			this.statusBox.Size = new System.Drawing.Size(72, 18);
			this.statusBox.Text = "";
			// 
			// lineOrderGrid
			// 
			this.lineOrderGrid.Location = new System.Drawing.Point(0, 32);
			this.lineOrderGrid.Size = new System.Drawing.Size(312, 112);
			this.lineOrderGrid.TableStyles.Add(this.lineOrderTable);
			this.lineOrderGrid.Text = "lineOrderGrid";
			this.lineOrderGrid.Click += new System.EventHandler(this.lineOrderGrid_Click);
			// 
			// lineOrderTable
			// 
			this.lineOrderTable.GridColumnStyles.Add(this.nameCol);
			this.lineOrderTable.GridColumnStyles.Add(this.cityCol);
			this.lineOrderTable.GridColumnStyles.Add(this.detailsCol);
			this.lineOrderTable.GridColumnStyles.Add(this.qtyContainersCol);
			this.lineOrderTable.MappingName = "lineOrder";
			// 
			// nameCol
			// 
			this.nameCol.HeaderText = "Namn";
			this.nameCol.MappingName = "shippingCustomerName";
			this.nameCol.NullText = "";
			this.nameCol.Width = 100;
			// 
			// cityCol
			// 
			this.cityCol.HeaderText = "Ort";
			this.cityCol.MappingName = "city";
			this.cityCol.NullText = "";
			this.cityCol.Width = 75;
			// 
			// detailsCol
			// 
			this.detailsCol.HeaderText = "Containers";
			this.detailsCol.MappingName = "details";
			this.detailsCol.NullText = "";
			this.detailsCol.Width = 75;
			// 
			// qtyContainersCol
			// 
			this.qtyContainersCol.HeaderText = "Antal";
			this.qtyContainersCol.MappingName = "qtyContainers";
			this.qtyContainersCol.NullText = "";
			this.qtyContainersCol.Width = 55;
			// 
			// button5
			// 
			this.button5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
			this.button5.Location = new System.Drawing.Point(48, 152);
			this.button5.Size = new System.Drawing.Size(32, 32);
			this.button5.Text = ">";
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// button4
			// 
			this.button4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
			this.button4.Location = new System.Drawing.Point(8, 152);
			this.button4.Size = new System.Drawing.Size(32, 32);
			this.button4.Text = "<";
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// button3
			// 
			this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
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
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.Add(this.menuItem1);
			this.mainMenu1.MenuItems.Add(this.menuItem2);
			this.mainMenu1.MenuItems.Add(this.menuItem3);
			this.mainMenu1.MenuItems.Add(this.menuItem4);
			// 
			// menuItem1
			// 
			this.menuItem1.MenuItems.Add(this.menuItem5);
			this.menuItem1.Text = "Arkiv";
			// 
			// menuItem5
			// 
			this.menuItem5.Text = "Avsluta";
			this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.MenuItems.Add(this.menuItem8);
			this.menuItem2.Text = "Visa";
			// 
			// menuItem8
			// 
			this.menuItem8.Text = "Lastade linjeorder";
			this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Text = "Verktyg";
			// 
			// menuItem4
			// 
			this.menuItem4.MenuItems.Add(this.menuItem6);
			this.menuItem4.MenuItems.Add(this.menuItem7);
			this.menuItem4.Text = "Inställningar";
			// 
			// menuItem6
			// 
			this.menuItem6.Text = "Allmänt";
			this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
			// 
			// menuItem7
			// 
			this.menuItem7.Text = "Containers";
			this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button2.Location = new System.Drawing.Point(192, 152);
			this.button2.Size = new System.Drawing.Size(48, 32);
			this.button2.Text = "Rutt";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button1.Location = new System.Drawing.Point(88, 152);
			this.button1.Size = new System.Drawing.Size(96, 32);
			this.button1.Text = "Rapportera";
			this.button1.Click += new System.EventHandler(this.button1_Click_1);
			// 
			// StartFormLine
			// 
			this.ClientSize = new System.Drawing.Size(322, 193);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.statusLabel);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.lineOrderGrid);
			this.Controls.Add(this.statusBox);
			this.Controls.Add(this.pictureBox1);
			this.Menu = this.mainMenu1;
			this.MinimizeBox = false;
			this.Text = "SmartOrder Linjetrafik";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.StartFormLine_Closing);
			this.Load += new System.EventHandler(this.StartFormLine_Load);

		}
		#endregion

		public void updateGrid(object sender, EventArgs e)
		{
			if (status.status > 0)
			{
				if (currentLineJournal != null)
				{
					try
					{

						DataLineOrders dataLineOrders = new DataLineOrders(smartDatabase);
						lineOrderDataSet = dataLineOrders.getActiveDataSet(currentLineJournal);

						int i = 0;
						while(i < lineOrderDataSet.Tables[0].Rows.Count)
						{
							DataRow row = lineOrderDataSet.Tables[0].Rows[i];
							row["qtyContainers"] = dataLineOrders.countContainers(int.Parse(lineOrderDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString())).ToString();

							lineOrderDataSet.Tables[0].Rows[i].AcceptChanges();
							i++;
						}


						lineOrderGrid.DataSource = lineOrderDataSet.Tables[0];

					}
					catch(Exception)
					{
						button2_Click(null, null);
					}
				}
				else
				{
					lineOrderGrid.DataSource = null;
				}
			}
			else
			{
				lineOrderGrid.DataSource = null;
			}

		}

		private void StartFormLine_Load(object sender, System.EventArgs e)
		{
			timer = new System.Windows.Forms.Timer();
			timer.Tick +=new EventHandler(timer_Tick);
			timer.Interval = 1000;
			timer.Enabled = true;

			if (currentLineJournal == null)
			{
				DataLineJournals dataLineJournals = new DataLineJournals(smartDatabase);
				currentLineJournal = dataLineJournals.getFirstLineJournal();
			}

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
			}
			else
			{

				StatusChangeLine statusChange = new StatusChangeLine(status);
				statusChange.ShowDialog();		
				statusChange.Dispose();

			}

			if ((status.mobileUserName != "") && (status.mobileUserName != null))
			{
				statusLabel.Text = status.mobileUserName + " ("+status.getStatusText()+")";
			}
			else
			{
				statusLabel.Text = status.getStatusText();
			}

			updateGrid(this, null);

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

		public void invokeSetStatusText(string text)
		{
			statusText = text;
		}

		public void setStatusTextBox(object sender, EventArgs e)
		{
			statusBox.Text = statusText;
			statusBox.Update();
		}

		private void button2_Click(object sender, System.EventArgs e)
		{

			if (status.status > 0)
			{
				LineJournalList lineJournalList = new LineJournalList(smartDatabase, status);
				lineJournalList.ShowDialog();
				if (lineJournalList.getLineJournal() != null)
				{
					this.currentLineJournal = lineJournalList.getLineJournal();			
				}
				lineJournalList.Dispose();

				updateGrid(this, null);

			}
			else
			{
				if (System.Windows.Forms.MessageBox.Show("Du är inte inloggad. Logga in?", "Logga in", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
				{
					changeStatus();

				}
			}

		}

		private void menuItem5_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			changeStatus();		

		}

		private void StartFormLine_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			Cursor.Show();


			commHandler.stop();			
			gpsComm.close();
			this.timer.Enabled = false;

			commHandler.waitForTermination();
		
			Cursor.Current = Cursors.Default;
			Cursor.Hide();
			
			Application.Exit();

		}

		private void menuItem6_Click(object sender, System.EventArgs e)
		{
			
			GeneralSettings settings = new GeneralSettings(smartDatabase);
			settings.ShowDialog();
			smartDatabase.getSetup().refresh();		
			settings.Dispose();

		}

		private void lineOrderGrid_Click(object sender, System.EventArgs e)
		{
			
			if (status.status > 0)
			{

				if (lineOrderDataSet.Tables[0].Rows.Count > 0)
				{
					if ((lineOrderGrid.CurrentRowIndex > -1) && (lineOrderGrid.DataSource != null))
					{
						Cursor.Current = Cursors.WaitCursor;
						Cursor.Show();

						DataLineOrder dataLineOrder = new DataLineOrder(smartDatabase, int.Parse(lineOrderDataSet.Tables[0].Rows[lineOrderGrid.CurrentRowIndex].ItemArray.GetValue(0).ToString()));
						LineOrder lineOrder = new LineOrder(smartDatabase, dataLineOrder, status);

						Cursor.Current = Cursors.Default;
						Cursor.Hide();

						lineOrder.ShowDialog();
						lineOrder.Dispose();

						updateGrid(this, null);
					}
				}
			}

		}

		private void menuItem7_Click(object sender, System.EventArgs e)
		{

			if (status.status > 0)
			{				
				LoadedContainers loadedContainers = new LoadedContainers(smartDatabase, status);
				loadedContainers.ShowDialog();
				loadedContainers.Dispose();

			}
			else
			{
				if (System.Windows.Forms.MessageBox.Show("Du är inte inloggad. Logga in?", "Logga in", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
				{
					changeStatus();

				}
			}

		}

		private void menuItem8_Click(object sender, System.EventArgs e)
		{
			
			if (status.status > 0)
			{
				ShippedLineOrders shippedLineOrders = new ShippedLineOrders(smartDatabase, status);
				shippedLineOrders.ShowDialog();
				shippedLineOrders.Dispose();

			}
			else
			{
				if (System.Windows.Forms.MessageBox.Show("Du är inte inloggad. Logga in?", "Logga in", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
				{
					changeStatus();

				}
			}

		}

		private void button4_Click(object sender, System.EventArgs e)
		{
			if ((this.lineOrderGrid.DataSource != null) && (((DataTable)this.lineOrderGrid.DataSource).Rows.Count > 0))
			{
				if (this.lineOrderGrid.CurrentRowIndex > 0)
				{
					this.lineOrderGrid.CurrentRowIndex = this.lineOrderGrid.CurrentRowIndex - 1;
					this.lineOrderGrid.Select(this.lineOrderGrid.CurrentRowIndex);
				}
			}

		}

		private void button5_Click(object sender, System.EventArgs e)
		{
			if ((this.lineOrderGrid.DataSource != null) && (((DataTable)this.lineOrderGrid.DataSource).Rows.Count > 0))
			{
				if (this.lineOrderGrid.CurrentRowIndex < (((DataTable)this.lineOrderGrid.DataSource).Rows.Count -1))
				{
					this.lineOrderGrid.CurrentRowIndex = this.lineOrderGrid.CurrentRowIndex + 1;
					this.lineOrderGrid.Select(this.lineOrderGrid.CurrentRowIndex);
				}
			}

		}

		private void menuItem9_Click(object sender, System.EventArgs e)
		{
			WindowsCE.hangUpConnections();
		}

		private void newOrderItem_Click(object sender, EventArgs e)
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

				DataOrderHeader dataOrderHeader = new DataOrderHeader(smartDatabase, dataCustomer, customerShipAddressNo);
				dataOrderHeader.mobileUserName = status.mobileUserName;
				dataOrderHeader.containerNo = status.containerNo;
				dataOrderHeader.commit();

				Order order = new Order(smartDatabase, dataOrderHeader);
				order.ShowDialog();
				order.Dispose();
			
			}

		}

		private void listOrderItem_Click(object sender, EventArgs e)
		{

			Orders orders = new Orders(smartDatabase);
			orders.ShowDialog();

			orders.Dispose();

		}

		private void menuItem9_Click_1(object sender, System.EventArgs e)
		{
			myPosX = myPosX + 1000;
			myPosY = myPosY + 1000;

			this.gpsComm_onPositionUpdate(this, null, myPosX, myPosY, 30);
			this.gpsComm_onHeadingUpdate(this, null, 90, 82);

		}

		private void timer_Tick(object sender, EventArgs e)
		{
			try
			{
				if (notifyDateTime < DateTime.Now)
				{

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

					updateGrid(this, null);

					notifyDateTime = DateTime.Now.AddSeconds(15);

				}

				setStatusTextBox(this, null);
				//commHandler.checkConnectionPerformHangup();

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
			catch(Exception)
			{

			}

		}

		#region NotifyForm Members

		public void invokeUpdateGrid()
		{
			// TODO:  Add StartFormLine.invokeUpdateGrid implementation
		}

		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			Navigator navigator = new Navigator(smartDatabase);
			navigator.start();

		}

		private void menuItem9_Click_2(object sender, System.EventArgs e)
		{
			commHandler.stop();
			commHandler = new CommHandler(smartDatabase, status, this);
		}

		private void button1_Click_1(object sender, System.EventArgs e)
		{
			if (currentLineJournal != null)
			{
				LineJournalReport lineJournalReport = new LineJournalReport(smartDatabase, status, currentLineJournal);
				lineJournalReport.ShowDialog();

				lineJournalReport.Dispose();

				updateGrid(null, null);

				currentLineJournal.getFromDb();

				if (currentLineJournal.status == 8)
				{
					DataLineJournals dataLineJournals = new DataLineJournals(smartDatabase);
					currentLineJournal = dataLineJournals.getFirstLineJournal();
				}
			}
		}
	}
}

