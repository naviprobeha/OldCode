using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Navipro.SmartSystems.SmartLibrary;
using SmartClient.Registration;

namespace Navipro.SmartSystems.SmartClient
{
    class SmartRegistration
    {
        private Configuration configuration;
        private Logger logger;

        public SmartRegistration(Configuration configuration, Logger logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }

        public bool performRegistration()
        {

            if (configuration.getConfigValue("serialNo") == "")
            {
                logger.write("", 0, "Serienr saknas.");
                inputSerialNo();
            }

            logger.write("", 0, "Serienr: " + configuration.getConfigValue("serialNo"));

            logger.write("", 0, "Registrerar mot servern...");

            if (register())
            {
                logger.write("", 1, "Klar.");
                return true;
            }

            logger.write("", 1, "Misslyckades.");

            return false;
        }

        public bool register()
        {
            
            Registration registration = new Registration();

            string serialNo = configuration.getConfigValue("serialNo");
            string password = configuration.getConfigValue("password");
            string url = configuration.getConfigValue("serverUrl");

            registration.Url = url + "/Agent/Registration.asmx";

            int result = registration.register(serialNo, ref password);

            if (result == 200) return true;
            if (result == 201)
            {
                configuration.setConfigValue("password", password);
                return true;
            }

            return false;
        }

 
        private void inputSerialNo()
        {
            SerialInput serialInput = new SerialInput();
            serialInput.ShowDialog();

            configuration.setConfigValue("serialNo", serialInput.getSerialNo());

        }


    }
}
