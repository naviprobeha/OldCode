using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.ScaleControl
{
	/// <summary>
	/// Summary description for ViktoriaUppdrag.
	/// </summary>
	public class ViktoriaIdent
	{
		public string ident;
		public string bil;
		public string slapcont;
		public string uppdrag;
		public DateTime tidsmarkering;
		public string login;
		public int behorighet;
		public string transportor;
		public int sluppdrag;

		private Database database;
		private string updateMethod;
		public bool recordExists;

		public ViktoriaIdent(Database database, string ident)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
			this.ident = ident;
			
			this.behorighet = 5;
			this.tidsmarkering = DateTime.Now;
			this.login = "JOB";
			this.sluppdrag = 0;
			
			read();
		}


		private void fromReader(SqlDataReader dataReader)
		{
			ident = dataReader.GetValue(0).ToString();
			bil = dataReader.GetValue(1).ToString();
			slapcont = dataReader.GetValue(2).ToString();
			uppdrag = dataReader.GetValue(3).ToString();
			tidsmarkering = dataReader.GetDateTime(4);
			login = dataReader.GetValue(5).ToString();
		
			if (!dataReader.IsDBNull(6))
			{
				behorighet = int.Parse(dataReader.GetValue(6).ToString());
			}
			else
			{
				behorighet = 0;
			}
			if (!dataReader.IsDBNull(7))
			{
				sluppdrag = int.Parse(dataReader.GetValue(7).ToString());
			}
			else
			{
				sluppdrag = 0;
			}

			transportor = dataReader.GetValue(8).ToString();
		}

		private void read()
		{

		
			SqlDataReader dataReader = database.query("SELECT Ident, Bil, Slapcont, Uppdrag, Tidsmarkering, Login, Behorighet, Sluppdrag, Transportor FROM "+database.prefix+".IDENT WHERE Ident = '"+ident+"'");
			if (dataReader.Read())
			{
				fromReader(dataReader);
				this.recordExists = true;
			}
			
			dataReader.Close();
		}

		public void save()
		{

			SqlDataReader dataReader = database.query("SELECT Ident FROM "+database.prefix+".IDENT WHERE Ident = '"+ident+"'");

			if (dataReader.Read())
			{
				dataReader.Close();
				if ((updateMethod != null) && (updateMethod.Equals("D")))
				{
					database.nonQuery("DELETE FROM "+database.prefix+".IDENT WHERE Ident = '"+ident+"'");
				}

				else
				{
					database.nonQuery("UPDATE "+database.prefix+".IDENT SET Bil = '"+this.bil+"', Slapcont = '"+this.slapcont+"', Uppdrag = '"+this.uppdrag+"', Tidsmarkering = '"+this.tidsmarkering.ToString("yyyy-MM-dd HH:mm:ss")+"', Login = '"+this.login+"', Behorighet = '"+this.behorighet+"', Sluppdrag = '"+this.sluppdrag+"', Transportor = '"+transportor+"' WHERE Ident = '"+this.ident+"'");
				}
			}
			else
			{
				dataReader.Close();
				database.nonQuery("INSERT INTO "+database.prefix+".IDENT (Ident, Bil, Slapcont, Uppdrag, Tidsmarkering, Login, Behorighet, Sluppdrag, Transportor) VALUES ('"+this.ident+"','"+this.bil+"','"+this.slapcont+"','"+this.uppdrag+"', '"+this.tidsmarkering.ToString("yyyy-MM-dd HH:mm:ss")+"', '"+this.login+"', '"+this.behorighet+"', '"+this.sluppdrag+"', '"+this.transportor+"')");
			}

		}

		public void delete()
		{
			this.updateMethod = "D";
			save();
		}

	}
}
