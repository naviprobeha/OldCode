using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.ScaleControl
{
	/// <summary>
	/// Summary description for ViktoriaUppdrag.
	/// </summary>
	public class ViktoriaUppdrag
	{
		public string uppdrag;
		public string benamning;
		public string container;
		public string leverantor;
		public string hamtstalle;
		public string kund;
		public string levplats;
		public string artikel;
		public string referens;
		public string login;
		public int behorighet;
		public DateTime tidsmarkering;
		public string underleverantor;
		public string ravaruleverantor;
		public string lastplats;
		public int vaglangd;
		public string typ;
		public int slleverantor;
		public int slkund;
		public int slartikel;
		public int slunderleverantor;
		public int slravaruleverantor;
		public int sllastplats;
		public string anmarkning;
        public string anlaggning;

		private Database database;
		private string updateMethod;
		public bool recordExists;

		public ViktoriaUppdrag(Database database, string uppdrag)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
			this.uppdrag = uppdrag;
			
			this.behorighet = 5;
			this.login = "JOB";
			this.hamtstalle = "0";
			this.levplats = "0";
			this.underleverantor = "-";
			this.ravaruleverantor = "-";
			this.lastplats = "-";
			this.typ = "F";
			this.tidsmarkering = DateTime.Now;
			
			this.slleverantor = 0;
			this.slkund = 0;
			this.slartikel = 0;
			this.slunderleverantor = 0;
			this.slravaruleverantor = 0;
			this.sllastplats = 0;



			read();
		}


		private void fromReader(SqlDataReader dataReader)
		{
			uppdrag = dataReader.GetValue(0).ToString();
			benamning = dataReader.GetValue(1).ToString();
			container = dataReader.GetValue(2).ToString();
			leverantor = dataReader.GetValue(3).ToString();
			hamtstalle = dataReader.GetValue(4).ToString();
			kund = dataReader.GetValue(5).ToString();
			levplats = dataReader.GetValue(6).ToString();
			artikel = dataReader.GetValue(7).ToString();
			referens = dataReader.GetValue(8).ToString();
			login = dataReader.GetValue(9).ToString();
			behorighet = int.Parse(dataReader.GetValue(10).ToString());
			tidsmarkering = dataReader.GetDateTime(11);
			underleverantor = dataReader.GetValue(12).ToString();
			ravaruleverantor = dataReader.GetValue(13).ToString();
			lastplats = dataReader.GetValue(14).ToString();
			vaglangd = int.Parse(dataReader.GetValue(15).ToString());
			typ = dataReader.GetValue(16).ToString();
			slleverantor = int.Parse(dataReader.GetValue(17).ToString());
			slkund = int.Parse(dataReader.GetValue(18).ToString());
			slartikel = int.Parse(dataReader.GetValue(19).ToString());
			slunderleverantor = int.Parse(dataReader.GetValue(20).ToString());
			slravaruleverantor = int.Parse(dataReader.GetValue(21).ToString());
			sllastplats = int.Parse(dataReader.GetValue(22).ToString());
			anmarkning = dataReader.GetValue(23).ToString();
		}

		private void read()
		{
		
			SqlDataReader dataReader = database.query("SELECT Uppdrag, Benamning, Container, Leverantor, Hamtstalle, Kund, Levplats, Artikel, Referens, Login, Behorighet, Tidsmarkering, Underleverantor, Ravaruleverantor, Lastplats, Vaglangd, Typ, Slleverantor, Slkund, Slartikel, Slunderleverantor, Slravaruleverantor, Sllastplats, Anmarkning FROM "+database.prefix+".UPPDRAG WHERE Uppdrag = '"+uppdrag+"'");
			if (dataReader.Read())
			{
				fromReader(dataReader);
				this.recordExists = true;
			}
			
			dataReader.Close();
		}

		public void save()
		{

			SqlDataReader dataReader = database.query("SELECT Uppdrag FROM "+database.prefix+".UPPDRAG WHERE Uppdrag = '"+uppdrag+"'");

			if (dataReader.Read())
			{
				dataReader.Close();
				if ((updateMethod != null) && (updateMethod.Equals("D")))
				{
					database.nonQuery("DELETE FROM "+database.prefix+".UPPDRAG WHERE Uppdrag = '"+uppdrag+"'");
				}

				else
				{
					database.nonQuery("UPDATE "+database.prefix+".UPPDRAG SET Benamning = '"+this.benamning+"', Container = '"+this.container+"', Leverantor = '"+this.leverantor+"', Hamtstalle = '"+this.hamtstalle+"', Kund = '"+this.kund+"', Levplats = '"+this.levplats+"', Artikel = '"+this.artikel+"', Referens = '"+this.referens+"', Login = '"+this.login+"', Behorighet = '"+this.behorighet+"', Tidsmarkering = '"+this.tidsmarkering.ToString("yyyy-MM-dd HH:mm:ss")+"', Underleverantor = '"+this.underleverantor+"', Ravaruleverantor = '"+this.ravaruleverantor+"', Lastplats = '"+this.lastplats+"', Vaglangd = '"+this.vaglangd+"', Typ = '"+this.typ+"', Slleverantor = '"+this.slleverantor+"', Slkund = '"+this.slkund+"', Slartikel = '"+this.slartikel+"', Slunderleverantor = '"+this.slunderleverantor+"', Slravaruleverantor = '"+this.slravaruleverantor+"', Sllastplats = '"+this.sllastplats+"', Anmarkning = '"+anmarkning+"' WHERE Uppdrag = '"+this.uppdrag+"'");
				}
			}
			else
			{
				dataReader.Close();
				database.nonQuery("INSERT INTO "+database.prefix+".UPPDRAG (Uppdrag, Benamning, Container, Leverantor, Hamtstalle, Kund, Levplats, Artikel, Referens, Login, Behorighet, Tidsmarkering, Underleverantor, Ravaruleverantor, Lastplats, Vaglangd, Typ, Slleverantor, Slkund, Slartikel, Slunderleverantor, Slravaruleverantor, Sllastplats, Anmarkning, Anlaggning) VALUES ('"+this.uppdrag+"','"+this.benamning+"','"+this.container+"','"+this.leverantor+"','"+this.hamtstalle+"','"+this.kund+"', '"+this.levplats+"', '"+this.artikel+"', '"+this.referens+"', '"+this.login+"', '"+this.behorighet+"', '"+this.tidsmarkering.ToString("yyyy-MM-dd HH:mm:ss")+"', '"+this.underleverantor+"', '"+this.ravaruleverantor+"', '"+this.lastplats+"', '"+this.vaglangd+"', '"+this.typ+"', '"+this.slleverantor+"', '"+this.slkund+"', '"+this.slartikel+"', '"+this.slunderleverantor+"', '"+this.slravaruleverantor+"', '"+this.sllastplats+"', '"+anmarkning+"', '"+anlaggning+"')");
			}

		}

		public void delete()
		{
			this.updateMethod = "D";
			save();
		}

	}
}
