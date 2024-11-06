using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Xml;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for DataColor.
	/// </summary>
	public class DataShipOrder
	{
		public string organizationNo;
		public int entryNo;
		public DateTime shipDate;
		public string customerNo;
		public string customerName;
		public string address;
		public string address2;
		public string postCode;
		public string city;
		public string countryCode;
		public string phoneNo;
		public string cellPhoneNo;
		public string details;
		public string comments;
		public int priority;
		public DateTime receivedDate;
		public int status;
		public int positionX;
		public int positionY;

		public string customerShipAddressNo;
		public string shipName;
		public string shipAddress;
		public string shipAddress2;
		public string shipPostCode;
		public string shipCity;

		public string billToCustomerNo;
		public int paymentType;

		public string directionComment;
		public string directionComment2;

		public DateTime shipTime;

		public string agentCode;

		public DateTime creationDate;
		public string productionSite;

		private string updateMethod;
		private SmartDatabase smartDatabase;

		public DataShipOrder(SmartDatabase smartDatabase, int no)
		{
			//
			// TODO: Add constructor logic here
			//

			this.smartDatabase = smartDatabase;
			this.entryNo = no;
			if (!getFromDb()) this.entryNo = 0;
		}

		public DataShipOrder(DataSet dataset, SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
			fromDataSet(dataset);				
			commit();
		}



		public void fromDataSet(DataSet dataset)
		{
			organizationNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
			entryNo = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());
			shipDate = DateTime.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(2).ToString());
			customerNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
			customerName = dataset.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
			address = dataset.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
			address2 = dataset.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
			postCode = dataset.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
			city = dataset.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
			phoneNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
			cellPhoneNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
			details = dataset.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
			comments = dataset.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
			priority = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(13).ToString());
			status = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(14).ToString());
			positionX = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(15).ToString());
			positionY = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(16).ToString());
		
			this.billToCustomerNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(17).ToString();
			this.customerShipAddressNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(18).ToString();
			this.shipName = dataset.Tables[0].Rows[0].ItemArray.GetValue(19).ToString();
			this.shipAddress = dataset.Tables[0].Rows[0].ItemArray.GetValue(20).ToString();
			this.shipAddress2 = dataset.Tables[0].Rows[0].ItemArray.GetValue(21).ToString();
			this.shipPostCode = dataset.Tables[0].Rows[0].ItemArray.GetValue(22).ToString();
			this.shipCity = dataset.Tables[0].Rows[0].ItemArray.GetValue(23).ToString();
			
			this.directionComment = dataset.Tables[0].Rows[0].ItemArray.GetValue(24).ToString();
			this.directionComment2 = dataset.Tables[0].Rows[0].ItemArray.GetValue(25).ToString();

			this.paymentType = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(26).ToString());

			this.creationDate = DateTime.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(27).ToString());

			this.productionSite = dataset.Tables[0].Rows[0].ItemArray.GetValue(28).ToString();

			receivedDate = DateTime.Now;
			shipTime = DateTime.Today;

			//Fix för att rätta till datum...
			shipDate = shipDate.AddHours(9);

		}


		public void commit()
		{
			if (this.customerName != "")
			{

				SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM shipOrder WHERE entryNo = '"+entryNo+"'");

				if (dataReader.Read())
				{
					if ((updateMethod != null) && (updateMethod.Equals("D")))
					{
						smartDatabase.nonQuery("DELETE FROM shipOrder WHERE entryNo = '"+entryNo+"'");
					}
					else
					{

						try
						{
							smartDatabase.nonQuery("UPDATE shipOrder SET organizationNo = '"+organizationNo+"', shipDate = '"+shipDate.ToString("yyyy-MM-dd")+"', customerNo = '"+customerNo+"', customerName = '"+customerName+"', address = '"+address+"', address2 = '"+address2+"', postCode = '"+postCode+"', city = '"+city+"', phoneNo = '"+phoneNo+"', cellPhoneNo = '"+cellPhoneNo+"', details = '"+details+"', comments = '"+comments+"', priority = '"+priority+"', status = '"+status+"', positionX = '"+positionX+"', positionY = '"+positionY+"', billToCustomerNo = '"+billToCustomerNo+"', shipName = '"+shipName+"', shipAddress = '"+shipAddress+"', shipAddress2 = '"+shipAddress2+"', shipPostCode = '"+shipPostCode+"', shipCity = '"+shipCity+"', directionComment = '"+directionComment+"', directionComment2 = '"+directionComment2+"', paymentType = '"+paymentType+"', shipTime = '"+shipTime.ToString("yyyy-MM-dd HH:mm:ss")+"', agentCode = '"+agentCode+"', customerShipAddressNo = '"+this.customerShipAddressNo+"', creationDate = '"+this.creationDate.ToString("yyyy-MM-dd 00:00:00")+"', productionSite = '"+productionSite+"' WHERE entryNo = '"+entryNo+"'");
						}
						catch (SqlCeException e) 
						{
							smartDatabase.ShowErrors(e);
						}
					}
				}
				else
				{
					if ((updateMethod == null) || (!updateMethod.Equals("D")))
					{

						try
						{
							smartDatabase.nonQuery("INSERT INTO shipOrder (entryNo, organizationNo, shipDate, customerNo, customerName, address, address2, postCode, city, phoneNo, cellPhoneNo, details, comments, priority, status, positionX, positionY, receivedDate, billToCustomerNo, shipName, shipAddress, shipAddress2, shipPostCode, shipCity, directionComment, directionComment2, paymentType, shipTime, agentCode, customerShipAddressNo, creationDate, productionSite) VALUES ('"+entryNo+"','"+organizationNo+"','"+shipDate.ToString("yyyy-MM-dd")+"','"+customerNo+"','"+customerName+"','"+address+"','"+address2+"','"+postCode+"','"+city+"','"+phoneNo+"','"+cellPhoneNo+"','"+details+"','"+comments+"','"+priority+"','"+status+"','"+positionX+"','"+positionY+"', '"+receivedDate+"','"+billToCustomerNo+"','"+shipName+"','"+shipAddress+"','"+shipAddress2+"','"+shipPostCode+"','"+shipCity+"','"+directionComment+"','"+directionComment2+"','"+paymentType+"', '"+shipTime.ToString("yyyy-MM-dd HH:mm:ss")+"', '"+agentCode+"', '"+customerShipAddressNo+"', '"+creationDate.ToString("yyyy-MM-dd 00:00:00")+"', '"+productionSite+"')");
						
						}
						catch (SqlCeException e) 
						{
							smartDatabase.ShowErrors(e);
						}
					}
				}
				dataReader.Dispose();	
			}
		}

		public bool getFromDb()
		{
		
			SqlCeDataReader dataReader = smartDatabase.query("SELECT entryNo, organizationNo, shipDate, customerNo, customerName, address, address2, postCode, city, countryCode, phoneNo, cellPhoneNo, details, comments, priority, receivedDate, status, positionX, positionY, billToCustomerNo, shipName, shipAddress, shipAddress2, shipPostCode, shipCity, directionComment, directionComment2, paymentType, shipTime, agentCode, creationDate, customerShipAddressNo, productionSite FROM shipOrder WHERE entryNo = '"+entryNo+"'");

			if (dataReader.Read())
			{
				try
				{
					this.entryNo = dataReader.GetInt32(0);
					this.organizationNo = dataReader.GetValue(1).ToString();
					this.shipDate = dataReader.GetDateTime(2);
					this.customerNo = dataReader.GetValue(3).ToString();
					this.customerName = dataReader.GetValue(4).ToString();
					this.address = dataReader.GetValue(5).ToString();
					this.address2 = dataReader.GetValue(6).ToString();
					this.postCode = dataReader.GetValue(7).ToString();
					this.city = dataReader.GetValue(8).ToString();
					this.countryCode = dataReader.GetValue(9).ToString();
					this.phoneNo = dataReader.GetValue(10).ToString();
					this.cellPhoneNo = dataReader.GetValue(11).ToString();
					this.details = dataReader.GetValue(12).ToString();
					this.comments = dataReader.GetValue(13).ToString();
					this.priority = dataReader.GetInt32(14);
					
					this.receivedDate = dataReader.GetDateTime(15);

					this.status = dataReader.GetInt32(16);
					this.positionX = dataReader.GetInt32(17);
					this.positionY = dataReader.GetInt32(18);
					this.billToCustomerNo = dataReader.GetValue(19).ToString();
					this.shipName = dataReader.GetValue(20).ToString();
					this.shipAddress = dataReader.GetValue(21).ToString();
					this.shipAddress2 = dataReader.GetValue(22).ToString();
					this.shipPostCode = dataReader.GetValue(23).ToString();
					this.shipCity = dataReader.GetValue(24).ToString();

					this.directionComment = dataReader.GetValue(25).ToString();
					this.directionComment2 = dataReader.GetValue(26).ToString();

					this.paymentType = dataReader.GetInt32(27);
					
					this.shipTime = dataReader.GetDateTime(28);

					this.agentCode = dataReader.GetValue(29).ToString();

					if (!dataReader.IsDBNull(30))
					{
						this.creationDate = dataReader.GetDateTime(30);
					}
					else
					{
						this.creationDate = this.shipDate;
					}

					this.customerShipAddressNo = dataReader.GetValue(31).ToString();

					this.productionSite = dataReader.GetValue(32).ToString();

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

	}
}
