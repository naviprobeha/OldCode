using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for AgentTransaction.
	/// </summary>
	public class ScheduledShipOrder
	{
		public string organizationNo;
		public int entryNo;
		public string customerNo;
		public string customerName;
		public string address;
		public string address2;
		public string postCode;
		public string city;
		public string countryCode;

		public string phoneNo;
		public string cellPhoneNo;

		public string comments;

		public string billToCustomerNo;
		public int paymentType;

		public string customerShipAddressNo;
		public string shipName;
		public string shipAddress;
		public string shipAddress2;
		public string shipPostCode;
		public string shipCity;

		public string directionComment;
		public string directionComment2;

		public int positionX;
		public int positionY;

		public bool monday;
		public bool tuesday;
		public bool wednesday;
		public bool thursday;
		public bool friday;
		public bool saturday;
		public bool sunday;

		public int weekType;

		public string updateMethod;

		public ScheduledShipOrder(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//
			this.organizationNo = dataReader.GetValue(0).ToString();
			this.entryNo = dataReader.GetInt32(1);
			this.customerNo = dataReader.GetValue(2).ToString();
			this.customerName = dataReader.GetValue(3).ToString();
			this.address = dataReader.GetValue(4).ToString();
			this.address2 = dataReader.GetValue(5).ToString();
			this.postCode = dataReader.GetValue(6).ToString();
			this.city = dataReader.GetValue(7).ToString();
			this.countryCode = dataReader.GetValue(8).ToString();
			this.phoneNo = dataReader.GetValue(9).ToString();
			this.cellPhoneNo = dataReader.GetValue(10).ToString();
			this.comments = dataReader.GetValue(11).ToString();
			this.positionX = dataReader.GetInt32(12);
			this.positionY = dataReader.GetInt32(13);
			this.billToCustomerNo = dataReader.GetValue(14).ToString();
			this.customerShipAddressNo = dataReader.GetValue(15).ToString();
			this.shipName = dataReader.GetValue(16).ToString();
			this.shipAddress = dataReader.GetValue(17).ToString();
			this.shipAddress2 = dataReader.GetValue(18).ToString();
			this.shipPostCode = dataReader.GetValue(19).ToString();
			this.shipCity = dataReader.GetValue(20).ToString();
			this.directionComment = dataReader.GetValue(21).ToString();
			this.directionComment2 = dataReader.GetValue(22).ToString();
			this.paymentType = dataReader.GetInt32(23);

			if (dataReader.GetValue(24).ToString() == "1") this.monday = true;
			if (dataReader.GetValue(25).ToString() == "1") this.tuesday = true;
			if (dataReader.GetValue(26).ToString() == "1") this.wednesday = true;
			if (dataReader.GetValue(27).ToString() == "1") this.thursday = true;
			if (dataReader.GetValue(28).ToString() == "1") this.friday = true;
			if (dataReader.GetValue(29).ToString() == "1") this.saturday = true;
			if (dataReader.GetValue(30).ToString() == "1") this.sunday = true;
			
			this.weekType = int.Parse(dataReader.GetValue(31).ToString());
		}


		public ScheduledShipOrder(string organizationNo)
		{
			this.organizationNo = organizationNo;
			this.billToCustomerNo = "";
			this.customerNo = "";
		}


		public void save(Database database)
		{		
			int mondayInt = 0;
			int tuesdayInt = 0;
			int wednesdayInt = 0;
			int thursdayInt = 0;
			int fridayInt = 0;
			int saturdayInt = 0;
			int sundayInt = 0;

			if (monday) mondayInt = 1;
			if (tuesday) tuesdayInt = 1;
			if (wednesday) wednesdayInt = 1;
			if (thursday) thursdayInt = 1;
			if (friday) fridayInt = 1;
			if (saturday) saturdayInt = 1;
			if (sunday) sundayInt = 1;

			SqlDataReader dataReader = database.query("SELECT * FROM [Scheduled Ship Order] WHERE [Entry No] = '"+entryNo+"'");

			if (dataReader.Read())
			{
				dataReader.Close();
				if ((updateMethod != null) && (updateMethod.Equals("D")))
				{
					database.nonQuery("DELETE FROM [Scheduled Ship Order] WHERE [Entry No] = '"+entryNo+"'");
				}

				else
				{

					database.nonQuery("UPDATE [Scheduled Ship Order] SET [Organization No] = '"+this.organizationNo+"', [Customer No] = '"+this.customerNo+"', [Customer Name] = '"+this.customerName+"', [Address] = '"+this.address+"', [Address 2] = '"+this.address2+"', [Post Code] = '"+this.postCode+"', [City] = '"+this.city+"', [Country Code] = '"+this.countryCode+"', [Phone No] = '"+this.phoneNo+"', [Cell Phone No] = '"+this.cellPhoneNo+"', [Comments] = '"+this.comments+"', [Bill-to Customer No] = '"+this.billToCustomerNo+"', [Payment Type] = '"+this.paymentType+"', [Customer Ship Address No] = '"+this.customerShipAddressNo+"', [Ship Name] = '"+this.shipName+"', [Ship Address] = '"+this.shipAddress+"', [Ship Address 2] = '"+this.shipAddress2+"', [Ship Post Code] = '"+this.shipPostCode+"', [Ship City] = '"+this.shipCity+"', [Direction Comment] = '"+this.directionComment+"', [Direction Comment 2] = '"+this.directionComment2+"', [Position X] = '"+this.positionX+"', [Position Y] = '"+this.positionY+"', [Monday] = '"+mondayInt+"', [Tuesday] = '"+tuesdayInt+"', [Wednesday] = '"+wednesdayInt+"', [Thursday] = '"+thursdayInt+"', [Friday] = '"+fridayInt+"', [Saturday] = '"+saturdayInt+"', [Sunday] = '"+sundayInt+"', [Week Type] = '"+weekType+"' WHERE [Entry No] = '"+this.entryNo+"'");

				}
			}
			else
			{
				dataReader.Close();
				database.nonQuery("INSERT INTO [Scheduled Ship Order] ([Organization No], [Customer No], [Customer Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Cell Phone No], [Comments], [Bill-to Customer No], [Payment Type], [Customer Ship Address No], [Ship Name], [Ship Address], [Ship Address 2], [Ship Post Code], [Ship City], [Direction Comment], [Direction Comment 2], [Position X], [Position Y], [Monday], [Tuesday], [Wednesday], [Thursday], [Friday], [Saturday], [Sunday], [Week Type]) VALUES ('"+this.organizationNo+"','"+this.customerNo+"','"+this.customerName+"','"+this.address+"','"+this.address2+"','"+this.postCode+"','"+this.city+"','"+this.countryCode+"','"+this.phoneNo+"','"+this.cellPhoneNo+"','"+this.comments+"','"+this.billToCustomerNo+"','"+this.paymentType.ToString()+"','"+this.customerShipAddressNo+"','"+this.shipName+"','"+this.shipAddress+"','"+this.shipAddress2+"','"+this.shipPostCode+"','"+this.shipCity+"','"+this.directionComment+"','"+this.directionComment2+"','"+this.positionX+"','"+this.positionY+"','"+mondayInt+"','"+tuesdayInt+"','"+wednesdayInt+"','"+thursdayInt+"','"+fridayInt+"','"+saturdayInt+"','"+sundayInt+"', '"+weekType+"')");
				entryNo = (int)database.getInsertedSeqNo();

			}


		}

		public void delete(Database database)
		{
			updateMethod = "D";
			save(database);
		}

		public void applySellToCustomer(Customer customer)
		{

			customerNo = customer.no;

			phoneNo = customer.phoneNo;
			cellPhoneNo = customer.cellPhoneNo;
			
			directionComment = customer.directionComment;
			directionComment2 = customer.directionComment2;

			shipName = customer.name;
			shipAddress = customer.address;
			shipAddress2 = customer.address2;
			shipPostCode = customer.postCode;
			shipCity = customer.city;

			positionX = customer.positionX;
			positionY = customer.positionY;


			if (customer.forceCashPayment) paymentType = 1;

			if (billToCustomerNo == "")
			{
				applyBillToCustomer(customer);
			}


		}

		public void applyBillToCustomer(Customer customer)
		{

			billToCustomerNo = customer.no;

			customerName = customer.name;
			address = customer.address;
			address2 = customer.address2;
			postCode = customer.postCode;
			city = customer.city;

		}

		public void applyShipToAddress(CustomerShipAddress customerShipAddress)
		{

			customerShipAddressNo = customerShipAddress.entryNo;
			shipName = customerShipAddress.name;
			shipAddress = customerShipAddress.address;
			shipAddress2 = customerShipAddress.address2;
			shipPostCode = customerShipAddress.postCode;
			shipCity = customerShipAddress.city;

			directionComment = customerShipAddress.directionComment;
			directionComment2 = customerShipAddress.directionComment2;

			positionX = customerShipAddress.positionX;
			positionY = customerShipAddress.positionY;

		}
	}
}
