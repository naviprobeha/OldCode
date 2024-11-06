using System;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for Map.
	/// </summary>
	public class Map : System.Windows.Forms.Form
	{
	

		private SmartDatabase smartDatabase;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.PictureBox mapImage;
		private Status status;
		private DateTime mapUpdated;

		private Thread thread;
		private bool running;
		private GpsComm gpsComm;


		public Map(SmartDatabase smartDatabase, Status status, GpsComm gpsComm)
		{
			this.smartDatabase = smartDatabase;
			this.status = status;
			this.gpsComm = gpsComm;

			gpsComm.onPositionUpdate +=new Navipro.SantaMonica.Felicia.GpsComm.positionUpdateEventHandler(gpsComm_onPositionUpdate);
			gpsComm.onHeadingUpdate +=new Navipro.SantaMonica.Felicia.GpsComm.headingUpdateEventHandler(gpsComm_onHeadingUpdate);
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			status.heading = 45;

			DataMaps dataMaps = new DataMaps(smartDatabase);
			DataSet maps = dataMaps.getMapsInRange(status.rt90x, status.rt90y);

			if (maps.Tables[0].Rows.Count > 0)
			{
				string mapCode = maps.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

				DataMap dataMap = new DataMap(smartDatabase, mapCode);
				setMap(dataMap);

				mapUpdated = DateTime.Now;

				thread = new Thread(new ThreadStart(run));
				thread.Start();
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
			this.mapImage = new System.Windows.Forms.PictureBox();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(322, 214);
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.mapImage);
			this.tabPage1.Location = new System.Drawing.Point(4, 4);
			this.tabPage1.Size = new System.Drawing.Size(314, 188);
			this.tabPage1.Text = "Karta";
			// 
			// mapImage
			// 
			this.mapImage.Location = new System.Drawing.Point(-8, -24);
			this.mapImage.Size = new System.Drawing.Size(208, 104);
			// 
			// Map
			// 
			this.ClientSize = new System.Drawing.Size(322, 216);
			this.Controls.Add(this.tabControl1);
			this.Text = "Karta";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.Map_Closing);

		}
		#endregion

		private void setMap(DataMap dataMap)
		{
			Bitmap mapBmp = new Bitmap("\\program files\\felicia\\"+dataMap.code+".jpg");
			Bitmap screenBmp = new Bitmap(mapBmp.Width, mapBmp.Height);

			System.Drawing.Graphics graphix = Graphics.FromImage(screenBmp);	
			graphix.DrawImage(mapBmp, 0, 0, new Rectangle(0, 0, mapBmp.Width, mapBmp.Height), System.Drawing.GraphicsUnit.Pixel);
			
			int agentPositionX = getPositionOnMapX(mapBmp, status.rt90x, dataMap);
			int agentPositionY = getPositionOnMapY(mapBmp, status.rt90y, dataMap);

			//int screenOffsetY = mapBmp.Height - (agentPositionY - (96/2));
			int screenOffsetY = agentPositionY - 96;
			//if (screenOffsetY < 0) screenOffsetY = 0;
			//if ((screenOffsetY + 96) > 192) screenOffsetY = mapBmp.Height - 192;

			//int screenOffsetX = mapBmp.Width - (agentPositionX - (156/2));
			int screenOffsetX = agentPositionX - 156;
			//if (screenOffsetX < 0) screenOffsetX = 0;
			//if ((screenOffsetX + 156) > 312) screenOffsetX = mapBmp.Width - 312;

			try
			{
				mapImage.Size = new Size(mapBmp.Width, mapBmp.Height);
				mapImage.Location = new Point(0-screenOffsetX, 0-screenOffsetY);
			
				//System.Windows.Forms.MessageBox.Show(agentPositionX.ToString()+":"+agentPositionY.ToString()+" - "+screenOffsetX.ToString()+":"+screenOffsetY.ToString());

				plotAgent(ref graphix, agentPositionX, agentPositionY);

				mapImage.Image = screenBmp;

			}
			catch(Exception e)
			{
				if (e.Message != "") {}

			}

		}

		private void mapBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{

		}

		public void run()
		{
			while(running)
			{
				DataMaps dataMaps = new DataMaps(smartDatabase);
				DataSet maps = dataMaps.getMapsInRange(status.rt90x, status.rt90y);

				if (maps.Tables[0].Rows.Count > 0)
				{
					string mapCode = maps.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

					DataMap dataMap = new DataMap(smartDatabase, mapCode);
					setMap(dataMap);

					mapUpdated = DateTime.Now;
				}

				System.Windows.Forms.Application.DoEvents();

				Thread.Sleep(5000);
			}
		}

		private int getPositionOnMapX(Bitmap bitmap, int mapPointX, DataMap dataMap)
		{
			
			if ((mapPointX >= dataMap.positionLeft) && (mapPointX <= dataMap.positionRight))
			{
				float unit = (dataMap.positionRight - dataMap.positionLeft) / bitmap.Width;
				int vector = mapPointX - dataMap.positionLeft;
				
				return (int)(vector / unit);
			}

			return 0;

		}

		private int getPositionOnMapY(Bitmap bitmap, int mapPointY, DataMap dataMap)
		{
			if ((mapPointY >= dataMap.positionBottom) && (mapPointY <= dataMap.positionTop))
			{
				float unit = (dataMap.positionTop - dataMap.positionBottom) / bitmap.Height;
				int vector = mapPointY - dataMap.positionBottom;
				
				return (bitmap.Size.Height - ((int)(vector / unit)));
			}

			return 0;

		}

		private void plotAgent(ref Graphics graphix, int agentPositionX, int agentPositionY)
		{

			Brush redBrush = new SolidBrush(Color.Red);
			Brush greenBrush = new SolidBrush(Color.Green);
			Brush yellowBrush = new SolidBrush(Color.Yellow);

			Point[] points = new Point[3];

			if ((status.heading > 22.5) && (status.heading < 67.5))
			{
				points[0] = new Point(agentPositionX+3, agentPositionY-3);
				points[1] = new Point(agentPositionX-5, agentPositionY);
				points[2] = new Point(agentPositionX, agentPositionY+5);
			}

			if ((status.heading > 67.5) && (status.heading < 112.5))
			{
				points[0] = new Point(agentPositionX+5, agentPositionY);
				points[1] = new Point(agentPositionX-5, agentPositionY-3);
				points[2] = new Point(agentPositionX-5, agentPositionY+3);
			}

			if ((status.heading > 112.5) && (status.heading < 157.5))
			{
				points[0] = new Point(agentPositionX+3, agentPositionY+3);
				points[1] = new Point(agentPositionX, agentPositionY-5);
				points[2] = new Point(agentPositionX-5, agentPositionY);
			}

			if ((status.heading > 157.5) && (status.heading < 202.5))
			{
				points[0] = new Point(agentPositionX, agentPositionY+5);
				points[1] = new Point(agentPositionX+3, agentPositionY-5);
				points[2] = new Point(agentPositionX-3, agentPositionY-5);
			}

			if ((status.heading > 202.5) && (status.heading < 247.5))
			{
				points[0] = new Point(agentPositionX-3, agentPositionY+3);
				points[1] = new Point(agentPositionX, agentPositionY-5);
				points[2] = new Point(agentPositionX+5, agentPositionY);
			}

			if ((status.heading > 247.5) && (status.heading < 292.5))
			{
				points[0] = new Point(agentPositionX-5, agentPositionY);
				points[1] = new Point(agentPositionX+5, agentPositionY-3);
				points[2] = new Point(agentPositionX+5, agentPositionY+3);
			}

			if ((status.heading > 292.5) && (status.heading < 337.5))
			{
				points[0] = new Point(agentPositionX-3, agentPositionY-3);
				points[1] = new Point(agentPositionX, agentPositionY+5);
				points[2] = new Point(agentPositionX+5, agentPositionY);
			}

			if ((status.heading > 337.5) || (status.heading < 22.5))
			{
				points[0] = new Point(agentPositionX, agentPositionY-5);
				points[1] = new Point(agentPositionX-3, agentPositionY+5);
				points[2] = new Point(agentPositionX+3, agentPositionY+5);
			}

			graphix.FillPolygon(yellowBrush, points);
			graphix.DrawPolygon(new Pen(Color.Black), points);
			graphix.DrawEllipse(new Pen(Color.Black), new Rectangle(agentPositionX-10, agentPositionY-10, 20, 20));
			graphix.DrawEllipse(new Pen(Color.Black), new Rectangle(agentPositionX-9, agentPositionY-9, 18, 18));

			
		}


		private void Map_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			running = false;

			gpsComm.onHeadingUpdate -=new Navipro.SantaMonica.Felicia.GpsComm.headingUpdateEventHandler(gpsComm_onHeadingUpdate);
			gpsComm.onPositionUpdate -=new Navipro.SantaMonica.Felicia.GpsComm.positionUpdateEventHandler(gpsComm_onPositionUpdate);

		}

		private void gpsComm_onPositionUpdate(object sender, EventArgs e, int x, int y, int height)
		{
			status.rt90x = x;
			status.rt90y = y;
			status.height = height;
			status.lastUpdated = DateTime.Now;

		}

		private void gpsComm_onHeadingUpdate(object sender, EventArgs e, int heading, int speed)
		{
			status.heading = heading;
			status.speed = speed;
			status.lastUpdated = DateTime.Now;
		}
	}


}
