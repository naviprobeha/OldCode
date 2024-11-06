using System;
using System.Xml;
using System.Collections;

namespace SmartInventory
{
	/// <summary>
	/// Summary description for DataStatus.
	/// </summary>
	public class Status
	{
		private Logger logger;
		private SmartDatabase smartDatabase;
		private ArrayList binContentArray;
		private XmlElement xmlStatusElement;

		public Status(SmartDatabase smartDatabase, Logger logger)
		{
			this.smartDatabase = smartDatabase;
			this.logger = logger;
			this.binContentArray = new ArrayList();
			//
			// TODO: Add constructor logic here
			//
		}

		public Status(XmlElement statusElement, SmartDatabase smartDatabase, Logger logger)
		{
			this.logger = logger;
			this.smartDatabase = smartDatabase;
			this.binContentArray = new ArrayList();
			fromDOM(statusElement, smartDatabase);
		}

		public ArrayList binContentCollection
		{
			get
			{
				return binContentArray;
			}
		}

		public XmlElement xmlStatus
		{
			get
			{
				return xmlStatusElement;
			}
		}


		public void fromDOM(XmlElement element, SmartDatabase smartDatabase)
		{
			this.xmlStatusElement = element;

			XmlNodeList binContents = element.GetElementsByTagName("BIN_CONTENT");
			if (binContents.Count > 0)
			{
				int i = 0;
				while (i < binContents.Count)
				{
					DataBinContent binContent = new DataBinContent((XmlElement)binContents.Item(i), smartDatabase);
					this.binContentArray.Add(binContent);
					i++;

				}
			}

		}


	}
}
