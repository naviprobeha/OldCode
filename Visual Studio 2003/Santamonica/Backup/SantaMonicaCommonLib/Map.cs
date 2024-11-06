using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Map.
	/// </summary>
	public class Map
	{
		public string code;
		public string description;
		public int positionTop;
		public int positionLeft;
		public int positionRight;
		public int positionBottom;

		public Image mapImage;

		public Map(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//

			if (dataReader != null)
			{
				this.code = dataReader.GetValue(0).ToString();
				this.description = dataReader.GetValue(1).ToString();
				this.positionTop = dataReader.GetInt32(2);
				this.positionLeft = dataReader.GetInt32(3);
				this.positionBottom = dataReader.GetInt32(4);
				this.positionRight = dataReader.GetInt32(5);
			}
		}

		public void loadImage(Database database)
		{
			byte[] imageData = (byte[])database.scalarQuery("SELECT [Mapfile] FROM [Map] WHERE [Code] = '"+code+"'");

			if (imageData != null && imageData.Length != 0 )
			{       
     
				mapImage = Image.FromStream(new MemoryStream(imageData));
				
			}



		}

		public void writeImage(Database database, StreamWriter writer)
		{
			byte[] imageData = (byte[])database.scalarQuery("SELECT [Mapfile] FROM [Map] WHERE [Code] = '"+code+"'");

			if (imageData != null && imageData.Length != 0 )
			{       
     
				using(MemoryStream stream = new MemoryStream( imageData ))
				{
					stream.WriteTo(writer.BaseStream);
				}
			
			}

		}
	}
}
