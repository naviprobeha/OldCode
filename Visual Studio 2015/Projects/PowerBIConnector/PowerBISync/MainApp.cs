using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerBISync
{
    public class MainApp : Logger
    {
        private Configuration configuration;
        private Database database;

        public MainApp()
        {
            configuration = new Configuration();
            database = new Database(this, configuration);

        }

        public void run()
        {
            write("Configures modules: " + configuration.module, 0);

            PowerBIModule module = null;
            if (configuration.module.IndexOf("StandardSalesDashboard") >= 0)
            {
                module = new SalesDashboardModule(configuration, database, this);
                if (module != null) module.run();
            }
            if (configuration.module.IndexOf("BOKSalesDashboard") >= 0)
            {
                module = new BOKSalesDashboardModule(configuration, database, this);
                if (module != null) module.run();
            }
            if (configuration.module.IndexOf("BOKWMSDashboard") >= 0)
            {
                module = new BOKWMSDashboardModule(configuration, database, this);
                if (module != null) module.run();
            }


            database.close();
        }

        public void write(string message, int type)
        {
            Console.WriteLine("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] " + message);
        }
    }
}
