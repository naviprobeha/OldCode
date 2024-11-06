using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Xml;
using System.IO;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for DataColor.
	/// </summary>
	public class DataMap
	{
		public string code;
		public string description;
		public int positionTop;
		public int positionLeft;
		public int positionRight;
		public int positionBottom;

		private string updateMethod;
		private SmartDatabase smartDatabase;

		public DataMap(SmartDatabase smartDatabase, string code)
		{
			//
			// TODO: Add constructor logic here
			//

			this.smartDatabase = smartDatabase;
			this.code = code;
			getFromDb();
		}

		public DataMap(DataSet dataset, SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
			fromDataSet(dataset);				
			commit();
		}



		public void fromDataSet(DataSet dataset)
		{

			code = dataset.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
			description = dataset.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
			positionTop = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(2).ToString());
			positionLeft = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(3).ToString());
			positionRight = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(4).ToString());
			positionBottom = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(5).ToString());

		}


		public void commit()
		{
			
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM map WHERE code = '"+code+"'");

			if (dataReader.Read())
			{
				if ((updateMethod != null) && (updateMethod.Equals("D")))
				{
					smartDatabase.nonQuery("DELETE FROM map WHERE code = '"+code+"'");
				}
				else
				{

					try
					{
						smartDatabase.nonQuery("UPDATE map SET description = '"+description+"', positionTop = '"+positionTop+"', positionLeft = '"+positionLeft+"', positionBottom = '"+positionBottom+"', positionRight = '"+positionRight+"' WHERE code = '"+code+"'");
					}
					catch (SqlCeException e) 
					{
						smartDatabase.ShowErrors(e);
					}
				}
			}
			else
			{
				try
				{
					smartDatabase.nonQuery("INSERT INTO map (code, description, positionTop, positionLeft, positionBottom, positionRight) VALUES ('"+code+"','"+description+"','"+positionTop+"','"+positionLeft+"','"+positionBottom+"','"+positionRight+"')");
				}
				catch (SqlCeException e) 
				{
					smartDatabase.ShowErrors(e);
				}
			}
			dataReader.Dispose();	
		}

		public bool getFromDb()
		{
		
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM map WHERE code = '"+code+"'");

			if (dataReader.Read())
			{
				try
				{
					this.code = dataReader.GetValue(0).ToString();
					this.description = dataReader.GetValue(1).ToString();
					this.positionTop = dataReader.GetInt32(2);
					this.positionLeft = dataReader.GetInt32(3);
					this.positionBottom = dataReader.GetInt32(4);
					this.positionRight = dataReader.GetInt32(5);

					dataReader.Dispose();
					return true;
				}
				catch (SqlCeException e) 
				{
					smartDatabase.ShowErrors(e);
				}
			}
			dataReader.Dispose();
			return false;
			
		}

		public void delete()
		{
			this.updateMethod = "D";
			commit();
		}


		public void getMapFromServer()
		{
			try
			{
				DataSetup dataSetup = smartDatabase.getSetup();
			
				System.Net.WebRequest webRequest = System.Net.HttpWebRequest.Create("http://dev1.navipro.se/WebAdmin/showMap.aspx?organizationNo=KDT&mapCode="+code);

				System.Net.WebResponse webResponse = webRequest.GetResponse();
				System.IO.BinaryReader binReader = new System.IO.BinaryReader(webResponse.GetResponseStream());

				FileStream fileStream = new FileStream("\\program files\\felicia\\"+code+".jpg", System.IO.FileMode.Create);

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
			}
			catch(Exception e)
			{
				System.Windows.Forms.MessageBox.Show("Map Exception: "+e.Message);
			}
		}	
	}
}
