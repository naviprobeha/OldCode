using System;
using System.Xml;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for Item.
	/// </summary>
	public class Contact : ServiceArgument
	{
		public string no;
		public string name;
		public string name2;
		public string address;
		public string address2;
		public string postCode;
		public string city;
		public string countryCode;
		public string phoneNo;
		public string email;
		public string companyNo;
		//public string languageCode;

		private Database database;

        public Contact()
        {
        }

		public Contact(Database database, string no)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
				 
			this.no = no;

			getFromDatabase();
		}


		private void getFromDatabase()
		{
            SqlDataReader dataReader = null;
            
            try
            {
                DatabaseQuery databaseQuery = database.prepare("SELECT [No_], [Name], [Name 2], [Address], [Address 2], [Post Code], [City], [Phone No_], [E-Mail], [Country_Region Code], [Company No_], [Language Code] FROM [" + database.getTableName("Contact") + "] WHERE [No_] = @no ");
                databaseQuery.addStringParameter("no", no, 20);
                dataReader = databaseQuery.executeQuery();
            }
            catch (Exception)
            {
                DatabaseQuery databaseQuery = database.prepare("SELECT [No_], [Name], [Name 2], [Address], [Address 2], [Post Code], [City], [Phone No_], [E-Mail], [Country Code], [Company No_], [Language Code] FROM [" + database.getTableName("Contact") + "] WHERE [No_] = @no ");
                databaseQuery.addStringParameter("no", no, 20);
                dataReader = databaseQuery.executeQuery();
            }



			if (dataReader.Read())
			{

				no = dataReader.GetValue(0).ToString();
				name = dataReader.GetValue(1).ToString();
				name2 = dataReader.GetValue(2).ToString();
				address = dataReader.GetValue(3).ToString();
				address2 = dataReader.GetValue(4).ToString();
				postCode = dataReader.GetValue(5).ToString();
				city = dataReader.GetValue(6).ToString();
				phoneNo = dataReader.GetValue(7).ToString();
				email = dataReader.GetValue(8).ToString();
				countryCode = dataReader.GetValue(9).ToString();
				companyNo = dataReader.GetValue(10).ToString();
				//languageCode = dataReader.GetValue(11).ToString();
			}

			dataReader.Close();


		}

		public void refresh()
		{
			getFromDatabase();
		}

		public Contact getCompany()
		{
			Contact contact = new Contact(database, this.companyNo);
			return contact;

		}

		#region ServiceArgument Members

		public System.Xml.XmlElement toDOM(System.Xml.XmlDocument xmlDoc)
		{
			// TODO:  Add Contact.toDOM implementation
			XmlElement xmlContactElement = xmlDoc.CreateElement("contact");
			XmlAttribute noAttribute = xmlDoc.CreateAttribute("no");
			noAttribute.Value = no;
			xmlContactElement.Attributes.Append(noAttribute);

			return xmlContactElement;
		}

		#endregion

	}
}
