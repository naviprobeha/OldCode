using System;
using System.Threading;
using System.Windows.Forms;
using System.Net.Sockets;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for Navigator.
	/// </summary>
	public class Navigator
	{
		private SmartDatabase smartDatabase;

		public Navigator(SmartDatabase smartDatabase)
		{
			//
			// TODO: Add constructor logic here
			//
			this.smartDatabase = smartDatabase;
		}

		public void start()
		{
			WindowsCE.StartProcess(smartDatabase.getSetup().navigatorPath, "-tcpserver=127.0.0.1:4242");

		}

		public void navigate(int x, int y, string name)
		{
			if ((x == 0) || (y == 0))
			{
				MessageBox.Show("Kunden saknar position.");
				return;
			}

			double[] latLon = getLatLon(x, y);

			Cursor.Current = Cursors.WaitCursor;
			Cursor.Show();

			if (!WindowsCE.isStarted("Navigator"))
			{
				start();
				Thread.Sleep(15000);
			}

			Cursor.Current = Cursors.Default;
			Cursor.Hide();

			bool tryAgain = true;

			while(tryAgain)
			{
				try
				{
					//sendCommand("$window=0,60,645,400,noborder");
					sendCommand("$destination="+latLon[0].ToString()+","+latLon[1]+";\"Starta navigering till "+name+"?\";ask;navigate");
					tryAgain = false;
				}
				catch(Exception e)
				{

					if (MessageBox.Show("Ingen kontakt med navigatorn. Försöka igen?", "Fel", MessageBoxButtons.YesNo, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
					{
						tryAgain = false;
					}

					if (e.Message != "") {}

				}
			}
		}

		private double[] getLatLon(int x, int y)
		{
			NavGaussKruger gaussKruger = new NavGaussKruger("rt90_2.5_gon_v");
			return gaussKruger.GetWGS84(x, y);

		}

		private void sendCommand(string command)
		{
			TcpClient tcpClient = new TcpClient();
			tcpClient.Connect("localhost", 4242);
			
			NetworkStream netStream = tcpClient.GetStream();
			System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(netStream);
			streamWriter.WriteLine(command);

			streamWriter.Flush();
			tcpClient.Close();
		}

	}
}
