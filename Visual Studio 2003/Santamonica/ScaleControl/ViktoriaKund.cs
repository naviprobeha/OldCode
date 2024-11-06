using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.ScaleControl
{
	/// <summary>
	/// Summary description for ViktoriaUppdrag.
	/// </summary>
	public class ViktoriaKund
	{
		public string kundlev;
		public string namn;
		public string adress1;
		public string adress2;
		public string postnr;
		public string ort;
		public string typ;
		public string login;
		public int behorighet;
		public DateTime tidsmarkering;

		private Database database;
		private string updateMethod;
		public bool recordExists;

		public ViktoriaKund(Database database, string kundlev)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
			this.kundlev = kundlev;
			
			this.behorighet = 3;
			this.typ = "X";
			this.tidsmarkering = DateTime.Now;
			this.login = "JOB";
			
			updateMethod = "";
			read();
		}


		private void fromReader(SqlDataReader dataReader)
		{
			kundlev = dataReader.GetValue(0).ToString();
			namn = dataReader.GetValue(1).ToString();
			adress1 = dataReader.GetValue(2).ToString();
			adress2 = dataReader.GetValue(3).ToString();
			postnr = dataReader.GetValue(4).ToString();
			ort = dataReader.GetValue(5).ToString();
			typ = dataReader.GetValue(6).ToString();
			tidsmarkering = dataReader.GetDateTime(7);
			login = dataReader.GetValue(8).ToString();
			behorighet = int.Parse(dataReader.GetValue(9).ToString());
		}

		private void read()
		{

		
			SqlDataReader dataReader = database.query("SELECT Kundlev, Namn, Adress1, Adress2, Postnr, Ort, Typ, Tidsmarkering, Login, Behorighet FROM "+database.prefix+".KUNDLEV WHERE Kundlev = '"+kundlev+"'");
			if (dataReader.Read())
			{
				fromReader(dataReader);
				this.recordExists = true;
			}
			
			dataReader.Close();
		}

		public void save()
		{

			SqlDataReader dataReader = database.query("SELECT Kundlev FROM "+database.prefix+".KUNDLEV WHERE Kundlev = '"+kundlev+"'");

			if (dataReader.Read())
			{
				dataReader.Close();
				if ((updateMethod != null) && (updateMethod.Equals("D")))
				{
					database.nonQuery("DELETE FROM "+database.prefix+".KUNDLEV WHERE Kundlev = '"+kundlev+"'");
				}

				else
				{
					database.nonQuery("UPDATE "+database.prefix+".KUNDLEV SET Namn = '"+this.namn+"', Adress1 = '"+this.adress1+"', Adress2 = '"+this.adress2+"', Postnr = '"+this.postnr+"', Ort = '"+this.ort+"', Typ = '"+this.typ+"', Tidsmarkering = '"+this.tidsmarkering.ToString("yyyy-MM-dd HH:mm:ss")+"', Login = '"+this.login+"', Behorighet = '"+this.behorighet+"' WHERE Kundlev = '"+this.kundlev+"'");
				}
			}
			else
			{
				dataReader.Close();
				database.nonQuery("INSERT INTO "+database.prefix+".KUNDLEV (Kundlev, Namn, Adress1, Adress2, Postnr, Ort, Typ, Tidsmarkering, Login, Behorighet) VALUES ('"+this.kundlev+"','"+this.namn+"','"+this.adress1+"','"+this.adress2+"', '"+this.postnr+"', '"+this.ort+"', '"+this.typ+"', '"+this.tidsmarkering.ToString("yyyy-MM-dd HH:mm:ss")+"', '"+this.login+"', '"+this.behorighet+"')");
			}

		}

        public string getQuery()
        {
            return "UPDATE "+database.prefix+".KUNDLEV SET Namn = '"+this.namn+"', Adress1 = '"+this.adress1+"', Adress2 = '"+this.adress2+"', Postnr = '"+this.postnr+"', Ort = '"+this.ort+"', Typ = '"+this.typ+"', Tidsmarkering = '"+this.tidsmarkering.ToString("yyyy-MM-dd HH:mm:ss")+"', Login = '"+this.login+"', Behorighet = '"+this.behorighet+"' WHERE Kundlev = '"+this.kundlev+"'";
        }
	}
}
