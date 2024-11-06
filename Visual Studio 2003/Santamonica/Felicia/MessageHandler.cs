using System;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for MessageHandler.
	/// </summary>
	public class MessageHandler
	{
		private SmartDatabase smartDatabase;

		public MessageHandler(SmartDatabase smartDatabase)
		{
			//
			// TODO: Add constructor logic here
			//
			this.smartDatabase = smartDatabase;
		}

		public void handleMessages(object sender, EventArgs e)
		{

			DataMessages dataMessages = new DataMessages(smartDatabase);
			DataMessage dataMessage = dataMessages.getFirstMessage();
			if (dataMessage != null)
			{
				Sound sound = new Sound(0);
				sound.Play();

				dataMessage.delete();

				Message message = new Message(smartDatabase, dataMessage);
				message.ShowDialog();
				message.Dispose();
			}

		}
	}
}
