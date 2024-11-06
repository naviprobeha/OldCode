using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Item.
	/// </summary>
	public class PhoneLogEntry
	{
		public string userId;
		public string callId;
		public string remoteNumber;
		public DateTime receivedDateTime;
		public DateTime pickedUpDateTime;
		public DateTime finishedDateTime;
		public int status;
		public string transferedToNumber;
		public int originType;
		public string originNo;
		public string name;
		public string city;
		public string organizationNo;
		public int direction;

		public string updateMethod;

		public PhoneLogEntry()
		{
			this.receivedDateTime = new DateTime(1753, 1, 1, 0, 0, 0);
			this.pickedUpDateTime = new DateTime(1753, 1, 1, 0, 0, 0);
			this.finishedDateTime = new DateTime(1753, 1, 1, 0, 0, 0);

		}

		public PhoneLogEntry(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//
			this.userId = dataReader.GetValue(0).ToString();
			this.callId = dataReader.GetValue(1).ToString();
			this.remoteNumber = dataReader.GetValue(2).ToString();

			DateTime receivedDate = dataReader.GetDateTime(3);
			DateTime receivedTime = dataReader.GetDateTime(4);
			receivedDateTime = DateTime.Parse(receivedDate.ToString("yyyy-MM-dd")+" "+receivedTime.ToString("HH:mm:ss"));

			DateTime pickedUpDate = dataReader.GetDateTime(5);
			DateTime pickedUpTime = dataReader.GetDateTime(6);
			pickedUpDateTime = DateTime.Parse(pickedUpDate.ToString("yyyy-MM-dd")+" "+pickedUpTime.ToString("HH:mm:ss"));

			DateTime finishedDate = dataReader.GetDateTime(7);
			DateTime finishedTime = dataReader.GetDateTime(8);
			finishedDateTime = DateTime.Parse(finishedDate.ToString("yyyy-MM-dd")+" "+finishedTime.ToString("HH:mm:ss"));

			this.status = dataReader.GetInt32(9);

			this.transferedToNumber = dataReader.GetValue(10).ToString();
			this.originType = dataReader.GetInt32(11);
			this.originNo = dataReader.GetValue(12).ToString();
			this.name = dataReader.GetValue(13).ToString();
			this.city = dataReader.GetValue(14).ToString();
			this.organizationNo = dataReader.GetValue(15).ToString();

			this.direction = dataReader.GetInt32(16);
		}

		public PhoneLogEntry(DataRow dataRow)
		{
			//
			// TODO: Add constructor logic here
			//
			this.userId = dataRow.ItemArray.GetValue(0).ToString();
			this.callId = dataRow.ItemArray.GetValue(1).ToString();
			this.remoteNumber = dataRow.ItemArray.GetValue(2).ToString();

			DateTime receivedDate = DateTime.Parse(dataRow.ItemArray.GetValue(3).ToString());
			DateTime receivedTime = DateTime.Parse(dataRow.ItemArray.GetValue(4).ToString());
			receivedDateTime = DateTime.Parse(receivedDate.ToString("yyyy-MM-dd")+" "+receivedTime.ToString("HH:mm:ss"));

			DateTime pickedUpDate = DateTime.Parse(dataRow.ItemArray.GetValue(5).ToString());
			DateTime pickedUpTime = DateTime.Parse(dataRow.ItemArray.GetValue(6).ToString());
			pickedUpDateTime = DateTime.Parse(pickedUpDate.ToString("yyyy-MM-dd")+" "+pickedUpTime.ToString("HH:mm:ss"));

			DateTime finishedDate = DateTime.Parse(dataRow.ItemArray.GetValue(7).ToString());
			DateTime finishedTime = DateTime.Parse(dataRow.ItemArray.GetValue(8).ToString());
			finishedDateTime = DateTime.Parse(finishedDate.ToString("yyyy-MM-dd")+" "+finishedTime.ToString("HH:mm:ss"));

			this.status = int.Parse(dataRow.ItemArray.GetValue(9).ToString());

			this.transferedToNumber = dataRow.ItemArray.GetValue(10).ToString();
			this.originType = int.Parse(dataRow.ItemArray.GetValue(11).ToString());
			this.originNo = dataRow.ItemArray.GetValue(12).ToString();
			this.name = dataRow.ItemArray.GetValue(13).ToString();
			this.city = dataRow.ItemArray.GetValue(14).ToString();
			this.organizationNo = dataRow.ItemArray.GetValue(15).ToString();

			this.direction = int.Parse(dataRow.ItemArray.GetValue(16).ToString());
		}

		public void delete(Database database)
		{

			updateMethod = "D";
			save(database);
		}

		public void save(Database database)
		{
			applyCustomerInformation(database);


			if (updateMethod == "D")
			{
				database.nonQuery("DELETE FROM [Phone Log Entry] WHERE [User ID] = '"+this.userId+"' AND [Call ID] = '"+this.callId+"'");

			}
			else
			{
				SqlDataReader dataReader = database.query("SELECT [Call ID] FROM [Phone Log Entry] WHERE [User ID] = '"+this.userId+"' AND [Call ID] = '"+this.callId+"'");

				if (dataReader.Read())
				{
					dataReader.Close();
					database.nonQuery("UPDATE [Phone Log Entry] SET [Remote Number] = '"+remoteNumber+"', [Received Date] = '"+receivedDateTime.ToString("yyyy-MM-dd")+"', [Received Time] = '"+receivedDateTime.ToString("1754-01-01 HH:mm:ss")+"', [Picked Up Date] = '"+pickedUpDateTime.ToString("yyyy-MM-dd")+"', [Picked Up Time] = '"+pickedUpDateTime.ToString("1754-01-01 HH:mm:ss")+"', [Finished Date] = '"+finishedDateTime.ToString("yyyy-MM-dd")+"', [Finished Time] = '"+finishedDateTime.ToString("1754-01-01 HH:mm:ss")+"', [Status] = '"+status+"', [Transfered To Number] = '"+this.transferedToNumber+"', [Origin Type] = '"+originType+"', [Origin No] = '"+originNo+"', [Name] = '"+name+"', [City] = '"+city+"', [Organization No] = '"+organizationNo+"', [Direction] = '"+direction+"' WHERE [User ID] = '"+userId+"' AND [Call ID] = '"+this.callId+"'");
				}
				else
				{
					dataReader.Close();
					database.nonQuery("INSERT INTO [Phone Log Entry] ([User ID], [Call ID], [Remote Number], [Received Date], [Received Time], [Picked Up Date], [Picked Up Time], [Finished Date], [Finished Time], [Status], [Transfered To Number], [Origin Type], [Origin No], [Name], [City], [Organization No], [Direction]) VALUES ('"+userId+"', '"+callId+"', '"+remoteNumber+"', '"+receivedDateTime.ToString("yyyy-MM-dd")+"', '"+receivedDateTime.ToString("1754-01-01 HH:mm:ss")+"', '"+pickedUpDateTime.ToString("yyyy-MM-dd")+"', '"+pickedUpDateTime.ToString("1754-01-01 HH:mm:ss")+"', '"+finishedDateTime.ToString("yyyy-MM-dd")+"', '"+finishedDateTime.ToString("1754-01-01 HH:mm:ss")+"', '"+status+"', '"+transferedToNumber+"', '"+originType+"', '"+originNo+"', '"+name+"', '"+city+"', '"+organizationNo+"', '"+direction+"')");
				}


			}
		}

		public string getDirection()
		{
			if (direction == 0) return "Inkommande";
			if (direction == 1) return "Utgående";
			return "";
		}

		public string getStatusText()
		{
			if (status == 0) 
			{
				return "";
			}
			if (status == 1)
			{
				return "Ringer";
			}
			if (status == 2)
			{
				return "Pågår";
			}
			if (status == 3) 
			{
				return "Vidarekopplad";
			}
			if (status == 4) 
			{
				return "Missat";
			}
			if (status == 5) 
			{
				return "Avslutat";
			}

			return "";


		}


		public string getStatusIcon()
		{
			if (status == 0) 
			{
				return "ind_white.gif";
			}
			if (status == 1)
			{
				return "ind_yellow.gif";
			}
			if (status == 2) 
			{
				return "ind_green.gif";
			}
			if (status == 3) 
			{
				return "ind_black.gif";
			}
			if (status == 4) 
			{
				return "ind_red.gif";
			}
			if (status == 5) 
			{
				return "ind_black.gif";
			}

			return "ind_white.gif";
		}

		public string getType()
		{
			if (originType == 0) return "";
			if (originType == 1) return "Kund";
			if (originType == 2) return "Kund";
			if (originType == 3) return "Fabrik";
			if (originType == 4) return "Värmeverk";

			return "";
		}

		public string getViewUrl()
		{
			if (originType == 1)
			{
				return "authorize.aspx?organizationNo="+organizationNo+"&customerNo="+originNo+"&command=viewCustomer";
			}
			if (originType == 2)
			{
				return "shippingCustomers_view.aspx?shippingCustomerNo="+originNo;
			}
			if (originType == 3)
			{
				return "factories_view.aspx?factoryNo="+originNo;
			}
			if (originType == 4)
			{
				return "consumers_view.aspx?consumerNo="+originNo;
			}
			return "";
		}

		public string getCreateOrderUrl()
		{
			return getCreateOrderUrl("");
		}

		public string getCreateOrderUrl(string organizationNo)
		{
			if (originType == 1)
			{
				if (organizationNo != "")
				{
					return "authorize.aspx?organizationNo="+organizationNo+"&customerNo="+originNo+"&command=createOrder";
				}
				else
				{
					return "authorize.aspx?organizationNo="+this.organizationNo+"&customerNo="+originNo+"&command=createOrder";
				}
			}
			if (originType == 2)
			{
				return "shippingCustomers.aspx?command=createOrder&shippingCustomerNo="+originNo;
			}
			if (originType == 3)
			{
				return "factories_view.aspx?factoryNo="+originNo;
			}
			if (originType == 4)
			{
				return "consumers_view.aspx?consumerNo="+originNo;
			}
			return "";
		}

		public DataSet getCustomerOrganizations(Database database)
		{
			if (this.originType == 1)
			{
				Customers customers = new Customers();
				return customers.getCallCenterCustomerOrganization(database, originNo);
			}
			return null;

		}

		private void applyCustomerInformation(Database database)
		{
			if (this.remoteNumber.Length <= 4) return;

			Customers customers = new Customers();
			Customer customer = customers.findCallCenterPhoneNo(database, this.remoteNumber);
			if (customer != null)
			{
				this.originType = 1;
				this.originNo = customer.no;
				this.name = customer.name;
				this.city = customer.city;
				this.organizationNo = customer.organizationNo;
			}
			else
			{
				ShippingCustomers shippingCustomers = new ShippingCustomers();
				ShippingCustomer shippingCustomer = shippingCustomers.findPhoneNo(database, this.remoteNumber);
				if (shippingCustomer != null)
				{
					this.originType = 2;
					this.originNo = shippingCustomer.no;
					this.name = shippingCustomer.name;
					this.city = shippingCustomer.city;
					this.organizationNo = "";
				}
				else
				{
					Consumers consumers = new Consumers();
					Consumer consumer = consumers.findPhoneNo(database, this.remoteNumber);
					if (consumer != null)
					{
						this.originType = 4;
						this.originNo = consumer.no;
						this.name = consumer.name;
						this.city = consumer.city;
						this.organizationNo = "";
					}
					else
					{
						Factories factories = new Factories();
						Factory factory = factories.findPhoneNo(database, this.remoteNumber);
						if (factory != null)
						{
							this.originType = 3;
							this.originNo = factory.no;
							this.name = factory.name;
							this.city = factory.city;
							this.organizationNo = "";
						}
					}
				}
			}


		}

		public PhoneLogEntry getAlternativeLogEntry(Database database)
		{
			PhoneLogEntries phoneLogEntries = new PhoneLogEntries();
			return phoneLogEntries.getEntry(database, this.remoteNumber, this.receivedDateTime, this.userId);
		}
	}
}
