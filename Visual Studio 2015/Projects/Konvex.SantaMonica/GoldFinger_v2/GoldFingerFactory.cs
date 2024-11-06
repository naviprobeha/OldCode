using Navipro.SantaMonica.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Konvex.SmartShipping.Goldfinger
{
    public class GoldFingerFactory
    {
        public static IGoldfinger Create(string agentCode)
        {
            Configuration configuration = new Configuration();
            if (!configuration.initWeb())
            {
                return null;
            }
            Database database = new Database(null, configuration);

            IGoldfinger goldfinger = null;

            goldfinger = new Goldfinger2022();
            goldfinger.init(configuration, database);
            return goldfinger;

            /*
            Agents agents = new Agents();
            Agent agent = agents.getAgent(database, agentCode);
            if (agent == null)
            {
                goldfinger = new Goldfinger();
                goldfinger.init(configuration, database);
                return goldfinger;
            }

            if (agent.type == 1)
            {
                goldfinger = new Goldfinger();
                goldfinger.init(configuration, database);
                return goldfinger;
            }

            Organizations organizations = new Organizations();
            Organization organization = organizations.getOrganization(database, agent.organizationNo);

            if (organization.platform == 1)
            {
                goldfinger = new Goldfinger2022();
                goldfinger.init(configuration, database);
                return goldfinger;
            }

            goldfinger = new Goldfinger();
            goldfinger.init(configuration, database);
            return goldfinger;
            */
        }
    }
}
