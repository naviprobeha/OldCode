﻿//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4952
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------

namespace HelloWorld 
{   
    public partial class Settings : System.Configuration.ApplicationSettingsBase 
    {
        private static Settings defaultInstance;
        private static object syncObject = new object();
        public static Settings DefaultInstance 
        {
            get 
            {
                if ((Settings.defaultInstance == null)) 
                {
                    System.Threading.Monitor.Enter(Settings.syncObject);
                    try 
                    {
                        Settings.defaultInstance = new Settings();
                    }
                    finally 
                    {
                        System.Threading.Monitor.Exit(Settings.syncObject);
                    }
                }
                return Settings.defaultInstance;
            }
        }       
    }
}
