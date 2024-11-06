using System;
using System.Data;
using System.Data.SqlClient;
using Navipro.SantaMonica.Common;


namespace Navipro.SantaMonica.DataConversion
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class DataConversion : Logger
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		/// 

		private Configuration configuration1;
		private Configuration configuration2;

		private Database database1;
		private Database database2;

		[STAThread]
		static void Main(string[] args)
		{
			//
			// TODO: Add code to start application here
			//
			DataConversion dataConv = new DataConversion();
		}

		public DataConversion()
		{

			write("Starting...", 0);

			configuration1 = new Configuration();
			if (!configuration1.init())
			{
				write("Configuration1 failed to load...", 2);
				return;
			}

			configuration1.dataSource = "MobiLast";


			configuration2 = new Configuration();
			if (!configuration2.init())
			{
				write("Configuration2 failed to load...", 2);
				return;
			}

			database1 = new Database(this, configuration1);
			database2 = new Database(this, configuration2);

			try
			{
				transferCustomers();
			}
			catch(Exception e)
			{
				write(e.Message+", "+e.StackTrace, 0);
			}

			Console.In.ReadLine();
		}

		public void transferCustomers()
		{
			write("Reading customers...", 0);

			SqlDataReader dataReader = database1.query("SELECT Kundnamn, JurKundnamn, Organisationsnummer, Adress, Adress2, Postnr, Ort, Kundreferens, Telefon, Telefax, Mobiltelefon, Email, Anmarkning, CategotyFieldCust1, FakturaAdress, FakturaAdress2, FakturaPostnr, FakturaOrt, CategotyFieldCust3 FROM stdCustomer");

			int extNo = 990001;

			while(dataReader.Read())
			{
				string shipName = dataReader.GetValue(0).ToString();
				string customerName = dataReader.GetValue(1).ToString();
				string orgNo = dataReader.GetValue(2).ToString();
				string shipAddress = dataReader.GetValue(3).ToString();
				string shipAddress2 = dataReader.GetValue(4).ToString();
				string shipPostCode = dataReader.GetValue(5).ToString();
				string shipCity = dataReader.GetValue(6).ToString();
				string contact = dataReader.GetValue(7).ToString();
				string phoneNo = dataReader.GetValue(8).ToString();
				string faxNo = dataReader.GetValue(9).ToString();
				string cellPhoneNo = dataReader.GetValue(10).ToString();
				string email = dataReader.GetValue(11).ToString();
				string comments = dataReader.GetValue(12).ToString();
				string custNo = dataReader.GetValue(13).ToString();
				string invoiceAddress = dataReader.GetValue(14).ToString();
				string invoiceAddress2 = dataReader.GetValue(15).ToString();
				string invoicePostCode = dataReader.GetValue(16).ToString();
				string invoiceCity = dataReader.GetValue(17).ToString();
				string dairyNo = dataReader.GetValue(18).ToString();

				if (shipName.Length > 50) shipName = shipName.Substring(0,49);
				if (customerName.Length > 50) customerName = customerName.Substring(0,49);

				string comment2 = "";
				if (comments.Length > 200) 
				{
					comment2 = comments.Substring(200);
					comments = comments.Substring(0, 200);
				}

				if ((custNo.Length != 6) || (!custNo.StartsWith("10")))
				{
					custNo = extNo.ToString();
					extNo++;
				}

				SqlDataReader dataReader2 = database2.query("SELECT No FROM Customer WHERE No = '"+dataReader.GetValue(13).ToString()+"'");
				if (!dataReader2.Read())
				{
					dataReader2.Close();
					string sql = "INSERT INTO Customer ([Organization No], [No], [Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Contact Name], [Phone No], [Fax No], [Cell Phone No], [E-mail], [Price Group Code], [Bill-to Customer No], [Production Site], [Person No], [Registration No], [Dairy No], [Dairy Code], [Hide], [Blocked], [Force Cash Payment], [Direction Comment], [Direction Comment 2], [Position X], [Position Y]) VALUES ('KDT', '"+custNo+"', '"+customerName+"', '"+invoiceAddress+"', '"+invoiceAddress2+"', '"+invoicePostCode+"', '"+invoiceCity+"', '', '"+contact+"', '"+phoneNo+"', '"+faxNo+"', '"+cellPhoneNo+"', '"+email+"', '','','','','"+orgNo+"', '"+dairyNo+"', '', 0,0,0,'"+comments+"', '"+comment2+"', 0,0)";
					write(sql, 0);
					database2.nonQuery(sql); 
					sql = "INSERT INTO [Customer Ship Address] ([Organization No], [Customer No], [Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Contact Name], [Direction Comment], [Direction Comment 2], [Position X], [Position Y]) VALUES ('KDT', '"+custNo+"', '"+shipName+"', '"+shipAddress+"', '"+shipAddress2+"', '"+shipPostCode+"', '"+shipCity+"', '', '"+contact+"', '"+comments+"', '"+comment2+"', 0, 0)";
					write(sql, 0);
					database2.nonQuery(sql); 
				}
				else
				{
					string customerNo = dataReader2.GetValue(0).ToString();

					dataReader2.Close();

					string sql = "INSERT INTO [Customer Ship Address] ([Organization No], [Customer No], [Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Contact Name], [Direction Comment], [Direction Comment 2], [Position X], [Position Y]) VALUES ('KDT', '"+customerNo+"', '"+shipName+"', '"+shipAddress+"', '"+shipAddress2+"', '"+shipPostCode+"', '"+shipCity+"', '', '"+contact+"', '"+comments+"', '"+comment2+"', 0, 0)";
					write(sql, 0);
					database2.nonQuery(sql); 

				}

			}
			

			dataReader.Close();
		}

		public void write(string message, int status)
		{
			Console.Out.WriteLine(message);

		}
	}
}
