using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Organization.
	/// </summary>
	public class Organization
	{
		
		public string no;
		public string name;
		public string address;
		public string address2;
		public string postCode;
		public string city;
		public string countryCode;

		public string phoneNo;
		public string faxNo;
		public string email;

		public string contactName;

		public decimal stopFee;

		public bool enableSyncWithNavision;
		public string syncGroupCode;
		public string navisionUserId;
		public string navisionPassword;

		public string navisionVendorNo;
		public string stopItemNo;

		public string shippingCustomerNo;
		public int containerUsageLeadTimeDays;
		public int containerLoadTime;
		public string factoryCode;

		public bool allowLineOrderSupervision;
		public bool enableAutoPlan;
		public bool overwriteFromNavision;
		public bool autoAssignJournals;

		public bool callCenterMaster;
		public bool callCenterMember;

		private Database database;

		public Organization(Database database, SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//

			this.no = dataReader.GetValue(0).ToString();
			this.name = dataReader.GetValue(1).ToString();
			this.address = dataReader.GetValue(2).ToString();
			this.address2 = dataReader.GetValue(3).ToString();
			this.postCode = dataReader.GetValue(4).ToString();
			this.city = dataReader.GetValue(5).ToString();
			this.countryCode = dataReader.GetValue(6).ToString();
			this.phoneNo = dataReader.GetValue(7).ToString();
			this.faxNo = dataReader.GetValue(8).ToString();
			this.email = dataReader.GetValue(9).ToString();
			this.contactName = dataReader.GetValue(10).ToString();
			this.navisionUserId = dataReader.GetValue(11).ToString();
			this.navisionPassword = dataReader.GetValue(12).ToString();
			this.stopFee = dataReader.GetDecimal(13);
			this.navisionVendorNo = dataReader.GetValue(14).ToString();
			this.stopItemNo = dataReader.GetValue(15).ToString();
			
			this.allowLineOrderSupervision = false;
			if (dataReader.GetValue(16).ToString() == "1") this.allowLineOrderSupervision = true;

			this.enableSyncWithNavision = false;
			if (dataReader.GetValue(17).ToString() == "1") this.enableSyncWithNavision = true;

			this.syncGroupCode = dataReader.GetValue(18).ToString();
			this.shippingCustomerNo = dataReader.GetValue(19).ToString();
			this.containerUsageLeadTimeDays = int.Parse(dataReader.GetValue(20).ToString());
			this.factoryCode = dataReader.GetValue(21).ToString();

			this.enableAutoPlan = false;
			if (dataReader.GetValue(22).ToString() == "1") this.enableAutoPlan = true;

			this.containerLoadTime = dataReader.GetInt32(23);

			this.overwriteFromNavision = false;
			if (dataReader.GetValue(24).ToString() == "1") this.overwriteFromNavision = true;

			this.callCenterMaster = false;
			if (dataReader.GetValue(25).ToString() == "1") this.callCenterMaster = true;

			this.callCenterMember = false;
			if (dataReader.GetValue(26).ToString() == "1") this.callCenterMember = true;

			this.autoAssignJournals = false;
			if (dataReader.GetValue(27).ToString() == "1") this.autoAssignJournals = true;

			this.database = database;
		}

		public Organization(string no)
		{
			this.no = no;
		}

		public void updateStopItemPrice()
		{
			Items items = new Items();
			Item stopItem = items.getEntry(database, this.stopItemNo);

			PurchasePrice purchPrice = new PurchasePrice(database, stopItem, 1, this, DateTime.Now);

			this.stopFee = purchPrice.unitCost;
			save(database);


		}

		public void save(Database database)
		{
			int allowLineOrderSupervisionValue = 0;
			int enableSyncWithNavisionValue = 0;
			int enableAutoPlanValue = 0;
			int overwriteFromNavisionValue = 0;
			int callCenterMasterValue = 0;
			int callCenterMemberValue = 0;
			int autoAssignJournalsValue = 0;

			if (this.allowLineOrderSupervision) allowLineOrderSupervisionValue = 1;
			if (this.enableSyncWithNavision) enableSyncWithNavisionValue = 1;
			if (this.enableAutoPlan) enableAutoPlanValue = 1;
			if (this.overwriteFromNavision) overwriteFromNavisionValue = 1;
			if (this.callCenterMaster) callCenterMasterValue = 1;
			if (this.callCenterMember) callCenterMemberValue = 1;
			if (this.autoAssignJournals) autoAssignJournalsValue = 1;


			SynchronizationQueueEntries synchQueue = new SynchronizationQueueEntries();

			SqlDataReader dataReader = database.query("SELECT [No] FROM [Organization] WHERE [No] = '"+no+"'");

			if (dataReader.Read())
			{
				dataReader.Close();
				database.nonQuery("UPDATE [Organization] SET [Name] = '"+name+"', [Address] = '"+address+"', [Address 2] = '"+address2+"', [Post Code] = '"+postCode+"', [City] = '"+city+"', [Country Code] = '"+countryCode+"', [Phone No] = '"+phoneNo+"', [Fax No] = '"+faxNo+"', [E-mail] = '"+email+"', [Contact Name] = '"+contactName+"', [Stop Fee] = '"+stopFee.ToString().Replace(",", ".")+"', [Navision User ID] = '"+navisionUserId+"', [Navision Password] = '"+navisionPassword+"', [Navision Vendor No] = '"+navisionVendorNo+"', [Stop Item No] = '"+stopItemNo+"', [Allow Line Order Supervision] = '"+allowLineOrderSupervisionValue+"', [Enable Sync With Navision] = '"+enableSyncWithNavisionValue+"', [Sync Group Code] = '"+this.syncGroupCode+"', [Shipping Customer No] = '"+this.shippingCustomerNo+"', [Container Usage Lead Time Days] = '"+this.containerUsageLeadTimeDays+"', [Factory Code] = '"+this.factoryCode+"', [Enable Auto Plan] = '"+enableAutoPlanValue+"', [Container Load Time] = '"+this.containerLoadTime+"', [Overwrite From Navision] = '"+overwriteFromNavisionValue+"', [Call Center Master] = '"+callCenterMasterValue+"', [Call Center Member] = '"+callCenterMemberValue+"', [Auto Assign Journals] = '"+autoAssignJournalsValue+"' WHERE [No] = '"+no+"'");

			}
			else
			{
				dataReader.Close();
				database.nonQuery("INSERT INTO [Organization] ([No], [Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Fax No], [E-mail], [Contact Name], [Stop Fee], [Navision User ID], [Navision Password], [Navision Vendor No], [Stop Item No], [Allow Line Order Supervision], [Enable Sync With Navision], [Sync Group Code], [Shipping Customer No], [Container Usage Lead Time Days], [Factory Code], [Enable Auto Plan], [Container Load Time], [Overwrite From Navision], [Call Center Master], [Call Center Member], [Auto Assign Journals]) VALUES ('"+no+"','"+name+"','"+address+"','"+address2+"','"+postCode+"','"+city+"','"+countryCode+"','"+phoneNo+"','"+faxNo+"','"+email+"','"+contactName+"','"+stopFee.ToString().Replace(",", ".")+"','"+navisionUserId+"','"+navisionPassword+"','"+navisionVendorNo+"','"+stopItemNo+"', '"+allowLineOrderSupervisionValue+"', '"+enableSyncWithNavisionValue+"', '"+syncGroupCode+"', '"+this.shippingCustomerNo+"', '"+this.containerUsageLeadTimeDays+"','"+this.factoryCode+"', '"+enableAutoPlanValue+"', '"+this.containerLoadTime+"', '"+overwriteFromNavisionValue+"', '"+callCenterMasterValue+"', '"+callCenterMemberValue+"', '"+autoAssignJournalsValue+"')");
			}

			synchQueue.enqueueAgentsInOrganization(database, this.no, Agents.TYPE_SINGLE, SynchronizationQueueEntries.SYNC_ORGANIZATION, this.no, 0);
			synchQueue.enqueueAgentsInOrganization(database, this.no, Agents.TYPE_LINE, SynchronizationQueueEntries.SYNC_ORGANIZATION, this.no, 0);

			
		}

		public bool hasAvailableAgents(Database database, int type)
		{
			SqlDataReader dataReader = database.query("SELECT [Code] FROM [Agent] WHERE [Organization No] = '"+this.no+"' AND [Enabled] = '1' AND ([Type] = '"+type+"' OR [Type] = '2')");

			bool found = false;

			if (dataReader.Read())
			{
				found = true;
			}
			dataReader.Close();

			return found;

		}
	}
}
