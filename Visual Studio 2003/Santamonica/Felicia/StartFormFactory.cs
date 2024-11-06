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
	public class StartFormFactory : System.Windows.Forms.Form, NotifyForm
	{
		private System.Windows.Forms.TextBox statusBox;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Label statusLabel;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
	

		private const string dbFileName = "Felicia.sdf";

		private SmartDatabase smartDatabase;
		private GpsComm gpsComm;
		private CommHandler commHandler;
		private Status status;
		private DataSet factoryOrderDataSet;
		private string statusText;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem8;
		private MenuItem newOrderItem = new MenuItem();
		private MenuItem listOrderItem = new MenuItem();
	
		public delegate void setStatusTextDelegate();

		private System.Windows.Forms.Timer timer;
		private DateTime notifyDateTime;


		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.DataGridTableStyle factoryOrderTable;
		private System.Windows.Forms.DataGridTextBoxColumn dateCol;
		private System.Windows.Forms.DataGridTextBoxColumn factoryNameCol;
		private System.Windows.Forms.DataGridTextBoxColumn consumerName;
		private System.Windows.Forms.DataGrid factoryOrderGrid;
		private System.Windows.Forms.DataGridTextBoxColumn statusCol;

		private bool messagesIsShown;

		public StartFormFactory()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			factoryOrderGrid.Font = new Font(factoryOrderGrid.Font.Name, 9, FontStyle.Regular);
			factoryOrderGrid.RowHeadersVisible = false;		
		

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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(StartFormFactory));
			this.statusBox = new System.Windows.Forms.TextBox();
			this.factoryOrderGrid = new System.Windows.Forms.DataGrid();
			this.factoryOrderTable = new System.Windows.Forms.DataGridTableStyle();
			this.dateCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.factoryNameCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.consumerName = new System.Windows.Forms.DataGridTextBoxColumn();
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
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.statusCol = new System.Windows.Forms.DataGridTextBoxColumn();
			// 
			// statusBox
			// 
			this.statusBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular);
			this.statusBox.Location = new System.Drawing.Point(240, 8);
			this.statusBox.ReadOnly = true;
			this.statusBox.Size = new System.Drawing.Size(72, 18);
			this.statusBox.Text = "";
			// 
			// factoryOrderGrid
			// 
			this.factoryOrderGrid.Location = new System.Drawing.Point(0, 32);
			this.factoryOrderGrid.Size = new System.Drawing.Size(312, 112);
			this.factoryOrderGrid.TableStyles.Add(this.factoryOrderTable);
			this.factoryOrderGrid.Text = "factoryOrderGrid";
			this.factoryOrderGrid.Click += new System.EventHandler(this.lineOrderGrid_Click);
			// 
			// factoryOrderTable
			// 
			this.factoryOrderTable.GridColumnStyles.Add(this.dateCol);
			this.factoryOrderTable.GridColumnStyles.Add(this.factoryNameCol);
			this.factoryOrderTable.GridColumnStyles.Add(this.consumerName);
			this.factoryOrderTable.GridColumnStyles.Add(this.statusCol);
			this.factoryOrderTable.MappingName = "factoryOrder";
			// 
			// dateCol
			// 
			this.dateCol.HeaderText = "Datum";
			this.dateCol.MappingName = "shipDate";
			this.dateCol.NullText = "";
			this.dateCol.Width = 70;
			// 
			// factoryNameCol
			// 
			this.factoryNameCol.HeaderText = "Från";
			this.factoryNameCol.MappingName = "factoryName";
			this.factoryNameCol.NullText = "";
			this.factoryNameCol.Width = 100;
			// 
			// consumerName
			// 
			this.consumerName.HeaderText = "Till";
			this.consumerName.MappingName = "consumerName";
			this.consumerName.NullText = "";
			this.consumerName.Width = 100;
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
			this.menuItem8.Text = "Körda biomalorder";
			this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Text = "Verktyg";
			// 
			// menuItem4
			// 
			this.menuItem4.MenuItems.Add(this.menuItem6);
			this.menuItem4.Text = "Inställningar";
			// 
			// menuItem6
			// 
			this.menuItem6.Text = "Allmänt";
			this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Size = new System.Drawing.Size(40, 56);
			// 
			// statusCol
			// 
			this.statusCol.HeaderText = "Status";
			this.statusCol.MappingName = "statusText";
			this.statusCol.NullText = "";
			this.statusCol.Width = 35;
			// 
			// StartFormFactory
			// 
			this.ClientSize = new System.Drawing.Size(322, 193);
			this.Controls.Add(this.statusLabel);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.factoryOrderGrid);
			this.Controls.Add(this.statusBox);
			this.Controls.Add(this.pictureBox1);
			this.Menu = this.mainMenu1;
			this.MinimizeBox = false;
			this.Text = "SmartOrder Biomal";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.StartFormLine_Closing);
			this.Load += new System.EventHandler(this.StartFormLine_Load);

		}
		#endregion

		public void updateGrid(object sender, EventArgs e)
		{

			if (status.status > 0)
			{

				DataFactoryOrders dataFactoryOrders = new DataFactoryOrders(smartDatabase);
				this.factoryOrderDataSet = dataFactoryOrders.getActiveDataSet();

				int i = 0;
				while(i < factoryOrderDataSet.Tables[0].Rows.Count)
				{
					DataRow row = factoryOrderDataSet.Tables[0].Rows[i];
					row["statusText"] = "";

					if (factoryOrderDataSet.Tables[0].Rows[i].ItemArray.GetValue(6).ToString() == "3")
					{
						row["statusText"] = "Lastad";
					}
					if (factoryOrderDataSet.Tables[0].Rows[i].ItemArray.GetValue(6).ToString() == "4")
					{
						row["statusText"] = "Lossad";
					}

					factoryOrderDataSet.Tables[0].Rows[i].AcceptChanges();
					i++;
				}


				factoryOrderGrid.DataSource = factoryOrderDataSet.Tables[0];
		
				

			}
			else
			{
				factoryOrderGrid.DataSource = null;
			}

		}

		private void StartFormLine_Load(object sender, System.EventArgs e)
		{
			timer = new System.Windows.Forms.Timer();
			timer.Tick +=new EventHandler(timer_Tick);
			timer.Interval = 1000;
			timer.Enabled = true;

			updateGrid(this, null);

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

				StatusChangeFactory statusChange = new StatusChangeFactory(status);
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

				if (factoryOrderDataSet.Tables[0].Rows.Count > 0)
				{
					if ((factoryOrderGrid.CurrentRowIndex > -1) && (factoryOrderGrid.DataSource != null))
					{
						Cursor.Current = Cursors.WaitCursor;
						Cursor.Show();

						DataFactoryOrder dataFactoryOrder = new DataFactoryOrder(smartDatabase, int.Parse(factoryOrderDataSet.Tables[0].Rows[factoryOrderGrid.CurrentRowIndex].ItemArray.GetValue(0).ToString()));
						FactoryOrder factoryOrder = new FactoryOrder(smartDatabase, dataFactoryOrder, status);

						Cursor.Current = Cursors.Default;
						Cursor.Hide();

						factoryOrder.ShowDialog();
						factoryOrder.Dispose();

						updateGrid(this, null);
					}
				}
			}

		}

		private void menuItem8_Click(object sender, System.EventArgs e)
		{
			
			if (status.status > 0)
			{
				ShippedFactoryOrders shippedFactoryOrders = new ShippedFactoryOrders(smartDatabase, status);
				shippedFactoryOrders.ShowDialog();
				shippedFactoryOrders.Dispose();

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
			if ((this.factoryOrderGrid.DataSource != null) && (((DataTable)this.factoryOrderGrid.DataSource).Rows.Count > 0))
			{
				if (this.factoryOrderGrid.CurrentRowIndex > 0)
				{
					this.factoryOrderGrid.CurrentRowIndex = this.factoryOrderGrid.CurrentRowIndex - 1;
					this.factoryOrderGrid.Select(this.factoryOrderGrid.CurrentRowIndex);
				}
			}

		}

		private void button5_Click(object sender, System.EventArgs e)
		{
			if ((this.factoryOrderGrid.DataSource != null) && (((DataTable)this.factoryOrderGrid.DataSource).Rows.Count > 0))
			{
				if (this.factoryOrderGrid.CurrentRowIndex < (((DataTable)this.factoryOrderGrid.DataSource).Rows.Count -1))
				{
					this.factoryOrderGrid.CurrentRowIndex = this.factoryOrderGrid.CurrentRowIndex + 1;
					this.factoryOrderGrid.Select(this.factoryOrderGrid.CurrentRowIndex);
				}
			}

		}

		private void menuItem9_Click(object sender, System.EventArgs e)
		{
			WindowsCE.hangUpConnections();
		}


		private void timer_Tick(object sender, EventArgs e)
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

				if (this.gridIsUpdated())
				{
					Sound sound = new Sound(0);
					sound.Play();
				}

				updateGrid(this, null);

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
				DataFactoryOrders dataFactoryOrders = new DataFactoryOrders(smartDatabase);
				DataSet factoryOrderDataSet = dataFactoryOrders.getActiveDataSet();

				if (this.factoryOrderGrid.DataSource != null)
				{
					if (((DataTable)(this.factoryOrderGrid.DataSource)).Rows.Count != factoryOrderDataSet.Tables[0].Rows.Count) return true;
				}

			}
			return false;

		}

		#region NotifyForm Members

		public void invokeUpdateGrid()
		{
			// TODO:  Add StartFormLine.invokeUpdateGrid implementation
		}

		#endregion
	}
}

