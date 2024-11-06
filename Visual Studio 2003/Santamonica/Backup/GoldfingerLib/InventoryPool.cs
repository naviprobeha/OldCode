using System;
using System.Data;
using Navipro.SantaMonica.Common;

namespace Navipro.SantaMonica.Goldfinger
{
	/// <summary>
	/// Summary description for InventoryPool.
	/// </summary>
	public class InventoryPool : Logger
	{
		private Configuration configuration;
		private Database database;


		public InventoryPool()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public bool init()
		{
			configuration = new Configuration();

			if (!configuration.initWeb())
			{
				return false;
			}

			database = new Database(this, configuration);
			 
			return true;
		}

		public void reportFactoryInventory(string factoryNo, float volume, float percent)
		{
			DateTime dateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 0, 0);
			if (DateTime.Now.Minute > 30) dateTime = dateTime.AddHours(1);

			FactoryInventories factoryInventories = new FactoryInventories();
			FactoryInventory factoryInventory = factoryInventories.getEntry(database, factoryNo, dateTime);		
			if (factoryInventory == null)
			{
				factoryInventory = new FactoryInventory();
				factoryInventory.factoryNo = factoryNo;
				factoryInventory.date = dateTime;
				factoryInventory.timeOfDay = dateTime;
			}

			factoryInventory.volume = volume;
			factoryInventory.percent = percent;
			//factoryInventory.inventory = (float)(volume / 0.8);
			factoryInventory.inventory = volume;
			factoryInventory.type = 0;
			factoryInventory.save(database);

		}

		public void dispose()
		{
			database.close();
		}


		#region Logger Members

		public void write(string message, int type)
		{
			// TODO:  Add InventoryPool.write implementation
		}

		#endregion
	}
}
