using System;
using System.Messaging;
using System.IO;
using System.Text;

namespace NavisionMobile
{
	/// <summary>
	/// Summary description for StringFormatter.
	/// </summary>
	public class StringFormatter : IMessageFormatter
	{
		public StringFormatter()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public void Write (System.Messaging.Message msg, object obj)
		{
			//Declare a buffer.
			byte[] buff;

			//Place the string into the buffer using UTF8 encoding.
			buff = Encoding.UTF8.GetBytes (obj.ToString());

			//Create a new MemoryStream object passing the buffer.
			Stream stm = new MemoryStream(buff);

			//Assign the stream to the message's BodyStream property.
			msg.BodyStream = stm;
		} 

		public object Read (System.Messaging.Message msg)
		{
			//Obtain the BodyStream for the message.
			Stream stm = msg.BodyStream;

			//Create a StreamReader object used for reading from the stream.
			StreamReader reader = new StreamReader (stm);

			//Return the string read from the stream.
			//StreamReader.ReadToEnd returns a string.
			return reader.ReadToEnd();
		}

		public bool CanRead (System.Messaging.Message msg)
		{
			return true;
		}
	
		public object Clone()
		{
			return new StringFormatter();
		}

	}
}
