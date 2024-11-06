using System;
using System.IO;
using System.Xml;
using System.Data;
using System.Data.SqlServerCe;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for DataItems.
	/// </summary>
	public class DataMaps
	{
		private SmartDatabase smartDatabase;

		public DataMaps(SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
		}

		public DataSet getDataSet()
		{
			SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT * FROM map");
			
			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "map");
			adapter.Dispose();

			return dataSet;
		}

		public DataSet getMapsInRange(int positionX, int positionY)
		{
			SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT code, description, positionTop, positionLeft, positionBottom, positionRight FROM map WHERE positionTop > "+positionY+" AND positionBottom < "+positionY+" AND positionLeft < "+positionX+" AND positionRight > "+positionX);
			
			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "map");
			adapter.Dispose();

			return dataSet;
		}

		public void downloadMap(Status status)
		{
			try
			{
				int left = 0;
				int top = 0;
				int right = 0;
				int bottom = 0;


				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(smartDatabase.getSetup().mapServerUrl+"?x="+status.rt90y+"&y="+status.rt90x+"&level=14&width=800&height=600&command=getBoundaries");

				string mapName = status.rt90x+"_"+status.rt90y;

				XmlElement docElement = xmlDocument.DocumentElement;
				if (docElement != null)
				{
					if (docElement.GetAttribute("left") != null) left = int.Parse(docElement.GetAttribute("left"));
					if (docElement.GetAttribute("top") != null) top = int.Parse(docElement.GetAttribute("top"));
					if (docElement.GetAttribute("right") != null) right = int.Parse(docElement.GetAttribute("right"));
					if (docElement.GetAttribute("bottom") != null) bottom = int.Parse(docElement.GetAttribute("bottom"));
			
				}

	

				System.Net.WebRequest webRequest = System.Net.HttpWebRequest.Create(smartDatabase.getSetup().mapServerUrl+"?x="+status.rt90y+"&y="+status.rt90x+"&level=8&width=800&height=600");

				System.Net.WebResponse webResponse = webRequest.GetResponse();
				System.IO.BinaryReader binReader = new System.IO.BinaryReader(webResponse.GetResponseStream());

				FileStream fileStream = new FileStream("\\program files\\felicia\\"+mapName+".jpg", System.IO.FileMode.Create);

				byte[] buffer = new byte[10000];
				int bytes = binReader.Read(buffer, 0, 2000);
				int i = 0;
				while (bytes > 0)
				{
					fileStream.Write(buffer, i, bytes);
					i = bytes;
					bytes = binReader.Read(buffer, i, 2000);
				}
				fileStream.Close();
				webResponse.Close();

				DataMap dataMap = new DataMap(smartDatabase, mapName);
				dataMap.positionBottom = bottom;
				dataMap.positionLeft = left;
				dataMap.positionRight = right;
				dataMap.positionTop = top;
				dataMap.commit();

			}
			catch(Exception e)
			{
				System.Windows.Forms.MessageBox.Show("Map Exception: "+e.Message);
			}
			
		}
	}
}
