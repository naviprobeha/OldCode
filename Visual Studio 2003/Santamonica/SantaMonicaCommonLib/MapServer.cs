using System;
using System.Xml;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for MapServer.
	/// </summary>
	public class MapServer
	{
		private int x;
		private int y;
		private string pointText;
		private string mode;
		private string mapServerUrl;
		private string mapClientUrl;
		private string mapAccountNo;
		private string mapPassword;
		private string sessionId;

		public MapServer(Configuration configuration)
		{
			//
			// TODO: Add constructor logic here
			//
			this.mapServerUrl = configuration.mapServerUrl;
			this.mapClientUrl = configuration.mapClientUrl;
			this.mapAccountNo = configuration.mapAccount;
			this.mapPassword = configuration.mapPassword;
		}

		public void setSession(string sessionId, string operatorNo, string organizationNo)
		{
			this.sessionId = sessionId;
			SantaMonicaCommonLib.se.workanywhere.maps.MapServer mapServer = new SantaMonicaCommonLib.se.workanywhere.maps.MapServer();
			mapServer.Url = mapServerUrl;
			mapServer.setUserSession(mapAccountNo, mapPassword, sessionId, operatorNo, organizationNo);

		}

		public void setPosition(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		public void setPointMode(string text)
		{
			this.pointText = text;
			this.mode = "point";

		}


		public string getUrl()
		{
			return mapClientUrl+"?x="+x+"&y="+y+"&mode="+mode+"&pointText="+pointText+"&sessionId="+sessionId+"&accountNo="+mapAccountNo;
		}
	}
}
