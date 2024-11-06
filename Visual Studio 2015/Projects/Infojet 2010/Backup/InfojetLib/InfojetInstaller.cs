using System;
using System.DirectoryServices;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for InfojetSetup.
	/// </summary>
	/// 
	[System.ComponentModel.RunInstallerAttribute(true)]
	public class InfojetInstaller : System.Configuration.Install.Installer
	{
		public InfojetInstaller()
		{
			//
			// TODO: Add constructor logic here
			//
		}




		public int CreateWebSite(string webSiteName, string pathToRoot, bool createDir)
		{
			DirectoryEntry root = new DirectoryEntry("IIS://localhost/W3SVC");
			// Find unused ID value for new web site
			
			int siteID = 1;
			foreach(DirectoryEntry e in root.Children)
			{
				if (e.SchemaClassName == "IIsWebServer")
				{
					int ID = Convert.ToInt32(e.Name);
					if (ID >= siteID)
					{
						siteID = ID+1;
					}
				}
			}
			
			// Create web site
			DirectoryEntry site = (DirectoryEntry)root.Invoke("Create", "IIsWebServer", siteID);
			site.Invoke("Put", "ServerComment", webSiteName);
			site.Invoke("Put", "KeyType", "IIsWebServer");
			site.Invoke("Put", "ServerBindings", ":80:");
			site.Invoke("Put", "ServerState", 2);
			site.Invoke("Put", "FrontPageWeb", 1);
			site.Invoke("Put", "DefaultDoc", "Default.aspx");
			site.Invoke("Put", "SecureBindings", ":443:");
			site.Invoke("Put", "ServerAutoStart", 1);
			site.Invoke("Put", "ServerSize", 1);
			site.Invoke("SetInfo");
			
			// Create application virtual directory
			
			DirectoryEntry siteVDir = site.Children.Add("Root", "IISWebVirtualDir");
			siteVDir.Properties["AppIsolated"][0] = 2;
			siteVDir.Properties["Path"][0] = pathToRoot;
			siteVDir.Properties["AccessFlags"][0] = 513;
			siteVDir.Properties["FrontPageWeb"][0] = 1;
			siteVDir.Properties["AppRoot"][0] = "LM/W3SVC/"+siteID+"/Root";
			siteVDir.Properties["AppFriendlyName"][0] = "Root";
			siteVDir.CommitChanges();
			site.CommitChanges();
			return siteID;
		}


	}
}
