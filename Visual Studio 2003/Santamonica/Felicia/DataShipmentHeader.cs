using System;
using System.Data;
using System.Data.SqlServerCe;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for DataOrder.
	/// </summary>
	public class DataShipmentHeader
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
		public string productionSite;
		public string dairyCode;
		public string dairyNo;
		public int payment;
		public int status;
		public string reference;
		public string mobileUserName;
		public string containerNo;
		public int shipOrderEntryNo;

		public string customerShipAddressNo;
		public string shipName;
		public string shipAddress;
		public string shipAddress2;
		public string shipPostCode;
		public string shipCity;

		public string invoiceNo;

		public int positionX;
		public int positionY;

		public DataCustomer dataCustomer;

		private string updateMethod;
		private SmartDatabase smartDatabase;
		

		public DataShipmentHeader(SmartDatabase smartDatabase, int no)
		{
			//
			// TODO: Add constructor logic here
			//

			this.smartDatabase = smartDatabase;
			this.entryNo = no;
			getFromDb();
		}


		public DataShipmentHeader(SmartDatabase smartDatabase, DataCustomer dataCustomer, string customerShipAddressNo)
		{
			this.smartDatabase = smartDatabase;

			fromCustomer(dataCustomer);
			this.shipOrderEntryNo = 0;

			applyCustomerShipAddress(customerShipAddressNo);

			this.invoiceNo = "";

			commit();

		}

		public DataShipmentHeader(SmartDatabase smartDatabase, DataShipOrder dataShipOrder)
		{
			this.smartDatabase = smartDatabase;

			DataCustomer dataCustomer = new DataCustomer(smartDatabase, dataShipOrder.billToCustomerNo);
			fromCustomer(dataCustomer);

			this.payment = dataShipOrder.paymentType;
			this.shipOrderEntryNo = dataShipOrder.entryNo;

			this.customerName = dataShipOrder.customerName;
			this.address = dataShipOrder.address;
			this.address2 = dataShipOrder.address2;
			this.postCode = dataShipOrder.postCode;
			this.city = dataShipOrder.city;

			this.customerShipAddressNo = dataShipOrder.customerShipAddressNo;
			this.shipName = dataShipOrder.shipName;
			this.shipAddress = dataShipOrder.shipAddress;
			this.shipAddress2 = dataShipOrder.shipAddress2;
			this.shipPostCode = dataShipOrder.shipPostCode;
			this.shipCity = dataShipOrder.shipCity;

		
			//if ((customerShipAddressNo != "") && (customerShipAddressNo != null))
			//{
			//	DataCustomerShipAddress dataCustomerShipAddress = new DataCustomerShipAddress(smartDatabase, int.Parse(customerShipAddressNo));
			//	this.productionSite = dataCustomerShipAddress.productionSite;
			//}
			this.productionSite = dataShipOrder.productionSite;

			this.phoneNo = dataShipOrder.phoneNo;
			this.cellPhoneNo = dataShipOrder.cellPhoneNo;
			if (cellPhoneNo.Length > 20) cellPhoneNo = cellPhoneNo.Substring(1, 20);

			this.invoiceNo = "";

			commit();

			
			DataShipOrderLines dataShipOrderLines = new DataShipOrderLines(smartDatabase);
			DataSet shipOrderLineDataSet = dataShipOrderLines.getDataSet(dataShipOrder.entryNo);
			int i = 0;
			while (i < shipOrderLineDataSet.Tables[0].Rows.Count)
			{

				DataItem dataItem = new DataItem(smartDatabase, shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString());


				DataShipmentLine dataShipmentLine = new DataShipmentLine(smartDatabase, this, dataItem);
				dataShipmentLine.quantity = int.Parse(shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(3).ToString());
				dataShipmentLine.unitPrice = float.Parse(shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(5).ToString());
				dataShipmentLine.amount = float.Parse(shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(6).ToString());

				dataShipmentLine.connectionItemNo = shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(10).ToString();
				dataShipmentLine.connectionQuantity = int.Parse(shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString());
				dataShipmentLine.connectionUnitPrice = float.Parse(shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(7).ToString());
				dataShipmentLine.connectionAmount = float.Parse(shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(8).ToString());

				dataShipmentLine.testQuantity = int.Parse(shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(11).ToString());

				dataShipmentLine.totalAmount = float.Parse(shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(9).ToString());

				dataShipmentLine.commit();


				DataShipOrderLineIds dataShipOrderLineIds = new DataShipOrderLineIds(smartDatabase);
				DataSet shipOrderLineIdDataSet = dataShipOrderLineIds.getDataSet(dataShipOrder.entryNo, int.Parse(shipOrderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()));
				int j = 0;
				while (j < shipOrderLineIdDataSet.Tables[0].Rows.Count)
				{
				
					DataShipmentLineId dataShipmentLineId = new DataShipmentLineId(smartDatabase, dataShipmentLine);
					dataShipmentLineId.unitId = shipOrderLineIdDataSet.Tables[0].Rows[j].ItemArray.GetValue(3).ToString();
					if (shipOrderLineIdDataSet.Tables[0].Rows[j].ItemArray.GetValue(4).ToString() == "1") dataShipmentLineId.bseTesting = true;
					if (shipOrderLineIdDataSet.Tables[0].Rows[j].ItemArray.GetValue(5).ToString() == "1") dataShipmentLineId.postMortem = true;

					dataShipmentLineId.commit();

					j++;
				}

				dataShipmentLine.updateTestings();

				i++;
			}

		}

		public void fromCustomer(DataCustomer dataCustomer)
		{
			this.organizationNo = dataCustomer.organizationNo;
			this.shipDate = DateTime.Now;
			this.customerNo = dataCustomer.no;
			this.customerName = dataCustomer.name;
			this.address = dataCustomer.address;
			this.address2 = dataCustomer.address2;
			this.postCode = dataCustomer.postCode;
			this.city = dataCustomer.city;
			this.productionSite = dataCustomer.productionSite;
			this.dairyCode = dataCustomer.dairyCode;
			this.dairyNo = dataCustomer.dairyNo;
			this.reference = "";
			this.mobileUserName = "";

			this.positionX = dataCustomer.positionX;
			this.positionY = dataCustomer.positionY;

			this.dataCustomer = dataCustomer;

		}

		public void commit()
		{
			
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM shipmentHeader WHERE entryNo = '"+entryNo+"'");

			if (dataReader.Read())
			{
				if ((updateMethod != null) && (updateMethod.Equals("D")))
				{
					smartDatabase.nonQuery("DELETE FROM shipmentHeader WHERE entryNo = '"+entryNo+"'");
				}

				else
				{

					try
					{
						smartDatabase.nonQuery("UPDATE shipmentHeader SET organizationNo = '"+organizationNo+"', shipDate = '"+shipDate.ToString("yyyy-MM-dd")+"', customerNo = '"+customerNo+"', customerName = '"+customerName+"', address = '"+address+"', address2 = '"+address2+"', postCode = '"+postCode+"', city = '"+city+"', countryCode = '"+countryCode+"', phoneNo = '"+phoneNo+"', cellPhoneNo = '"+cellPhoneNo+"', productionSite = '"+productionSite+"', status = '"+status+"', positionX = '"+positionX+"', positionY = '"+positionY+"', payment = '"+payment+"', dairyCode = '"+dairyCode+"', dairyNo = '"+dairyNo+"', reference = '"+reference+"', mobileUserName = '"+mobileUserName+"', containerNo = '"+containerNo+"', shipOrderEntryNo = '"+shipOrderEntryNo+"', customerShipAddressNo = '"+this.customerShipAddressNo+"', shipName = '"+this.shipName+"', shipAddress = '"+this.shipAddress+"', shipAddress2 = '"+shipAddress2+"', shipPostCode = '"+this.shipPostCode+"', shipCity = '"+this.city+"', invoiceNo = '"+invoiceNo+"' WHERE entryNo = '"+entryNo+"'");
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
					smartDatabase.nonQuery("INSERT INTO shipmentHeader (organizationNo, shipDate, customerNo, customerName, address, address2, postCode, city, countryCode, phoneNo, cellPhoneNo, productionSite, status, positionX, positionY, payment, dairyCode, dairyNo, reference, mobileUserName, containerNo, shipOrderEntryNo, customerShipAddressNo, shipName, shipAddress, shipAddress2, shipPostCode, shipCity, invoiceNo) VALUES ('"+organizationNo+"','"+shipDate.ToString("yyyy-MM-dd")+"','"+customerNo+"','"+customerName+"','"+address+"','"+address2+"','"+postCode+"','"+city+"','"+countryCode+"','"+phoneNo+"','"+cellPhoneNo+"','"+productionSite+"','"+status+"','"+positionX+"','"+positionY+"', '"+payment+"','"+dairyCode+"','"+dairyNo+"','"+reference+"','"+mobileUserName+"','"+containerNo+"','"+shipOrderEntryNo+"', '"+customerShipAddressNo+"', '"+shipName+"', '"+shipAddress+"', '"+shipAddress2+"', '"+shipPostCode+"', '"+shipCity+"', '"+invoiceNo+"')");
					dataReader = smartDatabase.query("SELECT entryNo FROM shipmentHeader WHERE entryNo = @@IDENTITY");
					if (dataReader.Read())
					{
						this.entryNo = dataReader.GetInt32(0);

					}
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
		
			SqlCeDataReader dataReader = smartDatabase.query("SELECT entryNo, organizationNo, shipDate, customerNo, customerName, address, address2, postCode, city, countryCode, phoneNo, cellPhoneNo, productionSite, status, positionX, positionY, payment, dairyCode, dairyNo, reference, mobileUserName, containerNo, shipOrderEntryNo, customerShipAddressNo, shipName, shipAddress, shipAddress2, shipPostCode, shipCity, invoiceNo FROM shipmentHeader WHERE entryNo = '"+entryNo+"'");

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
					this.productionSite = dataReader.GetValue(12).ToString();
					this.status = dataReader.GetInt32(13);
					this.positionX = dataReader.GetInt32(14);
					this.positionY = dataReader.GetInt32(15);
					this.payment = dataReader.GetInt32(16);
					this.dairyCode = dataReader.GetValue(17).ToString();
					this.dairyNo = dataReader.GetValue(18).ToString();
					this.reference = dataReader.GetValue(19).ToString();
					this.mobileUserName = dataReader.GetValue(20).ToString();
					this.containerNo = dataReader.GetValue(21).ToString();
					this.shipOrderEntryNo = dataReader.GetInt32(22);

					this.customerShipAddressNo = dataReader.GetValue(23).ToString();
					this.shipName = dataReader.GetValue(24).ToString();
					this.shipAddress = dataReader.GetValue(25).ToString();
					this.shipAddress2 = dataReader.GetValue(26).ToString();
					this.shipPostCode = dataReader.GetValue(27).ToString();
					this.shipCity = dataReader.GetValue(28).ToString();

					this.dataCustomer = new DataCustomer(smartDatabase, customerNo);

					this.invoiceNo = dataReader.GetValue(29).ToString();

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

		public string getTotalAmount()
		{
			SqlCeDataReader dataReader = smartDatabase.query("SELECT Str(SUM(totalAmount), 8, 2) as sumAmount FROM shipmentLine WHERE shipmentEntryNo = '"+this.entryNo+"'");

			string sumAmount = "0.00";
			if (dataReader.Read())
			{
				try
				{
					sumAmount = dataReader.GetValue(0).ToString();
					dataReader.Dispose();
					if (sumAmount == "") return "0.00";
					return sumAmount;
				}
				catch (SqlCeException e) 
				{
					smartDatabase.ShowErrors(e);
				}
			}
			dataReader.Dispose();
			return sumAmount;

		}


		public bool checkIds()
		{
			DataShipmentLines dataShipmentLines = new DataShipmentLines(smartDatabase);
			DataSet shipmentLinesDataSet = dataShipmentLines.getEntriesDataSet(this.entryNo);

			int i = 0;
			while (i < shipmentLinesDataSet.Tables[0].Rows.Count)
			{
				DataShipmentLine dataShipmentLine = new DataShipmentLine(smartDatabase, shipmentLinesDataSet.Tables[0].Rows[i]);
				
				DataItem item = new DataItem(smartDatabase, dataShipmentLine.itemNo);
				if (item.requireId)
				{
					DataShipmentLineIds dataShipmentLineIds = new DataShipmentLineIds(smartDatabase);
					if (!dataShipmentLineIds.checkIfIdsEntered(dataShipmentLine))
					{
						return false;
					}

				}

				i++;

			}

			return true;
		}

		public bool checkIdTypes()
		{
			DataShipmentLines dataShipmentLines = new DataShipmentLines(smartDatabase);
			DataSet shipmentLinesDataSet = dataShipmentLines.getEntriesDataSet(this.entryNo);

			int i = 0;
			while (i < shipmentLinesDataSet.Tables[0].Rows.Count)
			{
				DataShipmentLine dataShipmentLine = new DataShipmentLine(smartDatabase, shipmentLinesDataSet.Tables[0].Rows[i]);
				
				DataItem item = new DataItem(smartDatabase, dataShipmentLine.itemNo);
				if (item.requireId)
				{
					if (item.idGroupCode == "1")
					{
						DataShipmentLineIds dataShipmentLineIds = new DataShipmentLineIds(smartDatabase);
						DataSet idDataSet = dataShipmentLineIds.getShipmentLineDataSet(dataShipmentLine);
						int j = 0;
						while (j < idDataSet.Tables[0].Rows.Count)
						{
							string unitId = idDataSet.Tables[0].Rows[j].ItemArray.GetValue(3).ToString();
							IdType1Pad idPad = new IdType1Pad();

							string prodNo = "";
							string idNo = "";
							string controlNo = "";
							idPad.splitType1Id(unitId, out prodNo, out idNo, out controlNo);
							if (controlNo == "")
							{
								System.Windows.Forms.MessageBox.Show("Felaktigt ID-nr för "+item.description+": "+unitId, "Fel ID", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Hand, System.Windows.Forms.MessageBoxDefaultButton.Button1);
								return false;
							}

							j++;
						}
					}
				}

				i++;

			}

			return true;
		}


		public bool milkCowExists()
		{
			DataShipmentLines dataShipmentLines = new DataShipmentLines(smartDatabase);
			DataSet shipmentLinesDataSet = dataShipmentLines.getEntriesDataSet(this.entryNo);

			int i = 0;
			while (i < shipmentLinesDataSet.Tables[0].Rows.Count)
			{
				DataShipmentLine dataShipmentLine = new DataShipmentLine(smartDatabase, shipmentLinesDataSet.Tables[0].Rows[i]);
			
				DataItem item = new DataItem(smartDatabase, dataShipmentLine.itemNo);
				if (item.description.ToUpper().IndexOf("MJÖLKKO") > -1) return true;

				i++;

			}

			return false;
		}

		private void applyCustomerShipAddress(string customerShipAddressNo)
		{
			if (customerShipAddressNo != "")
			{
				DataCustomerShipAddress dataCustomerShipAddress = new DataCustomerShipAddress(smartDatabase, int.Parse(customerShipAddressNo));

				this.customerShipAddressNo = customerShipAddressNo;
				this.shipName = dataCustomerShipAddress.name;
				this.shipAddress = dataCustomerShipAddress.address;
				this.shipAddress2 = dataCustomerShipAddress.address2;
				this.shipPostCode = dataCustomerShipAddress.postCode;
				this.shipCity = dataCustomerShipAddress.city;
				this.productionSite = dataCustomerShipAddress.productionSite;

			}
			else
			{
				this.shipName = this.customerName;
				this.shipAddress = this.address;
				this.shipAddress2 = this.address2;
				this.shipPostCode = this.postCode;
				this.shipCity = this.city;

			}
		}
	}
}
