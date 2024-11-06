using System;


namespace Navipro.Base.Common
{
    interface AssemblyModule
    {
        bool init(Logger logger, Configuration configuration);
        bool start();
        bool stop();
        void sendCommand(string command);
    }
}
