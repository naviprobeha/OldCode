using System;
using System.IO;
using System.Drawing;
using System.Data.SqlClient;


namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for Image.
	/// </summary>
	public class WebDocument
	{
		public string code;
		public string description;
		public string fileName;
		public string imageType;
		public DateTime lastModified;
		public byte[] imageByteArray;

		private Database database;

		public WebDocument(Database database, string code)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
				
			this.code = code;

			getFromDatabase();

		}

		public WebDocument(Database database, SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
				
			readData(dataReader);
		}

		private void readData(SqlDataReader dataReader)
		{
			code = dataReader.GetValue(0).ToString();
			description = dataReader.GetValue(1).ToString();
			fileName = dataReader.GetValue(2).ToString();
			imageType = dataReader.GetValue(3).ToString();
			DateTime lastModifiedDate = dataReader.GetDateTime(4);
			DateTime lastModifiedTime = dataReader.GetDateTime(5);
			lastModified = new DateTime(lastModifiedDate.Year, lastModifiedDate.Month, lastModifiedDate.Day, lastModifiedTime.Hour, lastModifiedTime.Minute, lastModifiedTime.Second, lastModifiedTime.Millisecond);
			

		}

		public void uploadDocument(string fileName)
		{
			imageByteArray = getBytesForImage(fileName);

			SqlConnection sqlConnection = database.getConnection();

			SqlCommand sqlCommand = sqlConnection.CreateCommand();
			sqlCommand.CommandText = "UPDATE ["+database.getTableName("Web Image")+"] SET [Image] = @image WHERE [Code] = '"+this.code+"'";
			sqlCommand.Parameters.Add("@image", System.Data.SqlDbType.Image, imageByteArray.Length).Value = imageByteArray;

			sqlCommand.ExecuteNonQuery();

			this.lastModified = DateTime.Now;
			save();

			getFromDatabase();
		}

		private byte[] getBytesForImage(string fileName)
		{
			FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
			BinaryReader binaryReader = new BinaryReader(fileStream);

			byte[] imageBytes = binaryReader.ReadBytes((int)fileStream.Length);

			binaryReader.Close();
			fileStream.Close();

			return imageBytes;

		}

		private void getFromDatabase()
		{
			if ((this.code != null) && (this.code != ""))
			{
				try
				{
					SqlDataReader dataReader = database.query("SELECT [Code], [Description], [Filename], [Image Type], [Last Modified Date], [Last Modified Time] FROM ["+database.getTableName("Web Image")+"] WHERE [Code] = '"+this.code+"'");
					if (dataReader.Read())
					{
						readData(dataReader);
					}

					dataReader.Close();
				}
				catch(Exception e)
				{
					throw new Exception("Document: "+this.code+", Exception: "+e.Message);
				}

				try
				{			
					imageByteArray = (byte[])database.scalarQuery("SELECT [Image] FROM ["+database.getTableName("Web Image")+"] WHERE [Code] = '"+this.code+"'");
				}
				catch(Exception e)
				{
					throw new Exception("Document: "+this.code+", Exception: "+e.Message);
				}
			}
		}

		public void writeDocument(StreamWriter writer)
		{		
			writer.Write(imageByteArray);
		}


		public void save()
		{
			SqlDataReader dataReader = database.query("SELECT [Code] FROM ["+database.getTableName("Web Image")+"] WHERE [Code] = '"+this.code+"'");
			bool exists = dataReader.Read();
			dataReader.Close();

			if (exists)
			{
				database.nonQuery("UPDATE ["+database.getTableName("Web Image")+"] SET [Description] = '"+this.description+"', [Filename] = '"+this.fileName+"', [Image Type] = '"+this.imageType+"', [Last Modified Date] = '"+this.lastModified.ToString("yyyy-MM-dd")+"', [Last Modified Time] = '1754-01-01 "+this.lastModified.ToString("HH:mm:ss")+"' WHERE [Code] = '"+this.code+"'");
			}
			else
			{
				database.nonQuery("INSERT INTO ["+database.getTableName("Web Image")+"] ([Code], [Description], [Filename], [Image Type], [Last Modified Date], [Last Modified Time]) VALUES ('"+this.code+"','"+this.description+"', '"+this.fileName+"', '"+this.imageType+"', '"+this.lastModified.ToString("yyyy-MM-dd")+"', '"+this.lastModified.ToString("HH:mm:ss")+"')");
			}
		}

		public string getUrl()
		{
			if (this.code != "")
			{
				checkDocumentUpdate();

				string fileName = System.Web.HttpContext.Current.Server.MapPath("images/caching")+"\\"+this.code.Replace(" ", "_")+"."+this.imageType;

				FileInfo cacheImageFile = new FileInfo(fileName);

				if (!cacheImageFile.Exists)
				{
					getFromDatabase();
				
					StreamWriter streamWriter = new StreamWriter(fileName);
					streamWriter.BaseStream.Write(imageByteArray, 0, imageByteArray.Length);
					streamWriter.Flush();
					streamWriter.Close();
				}
				else
				{
					if (cacheImageFile.LastWriteTime.Ticks < this.lastModified.Ticks)
					{
						getFromDatabase();

						StreamWriter streamWriter = new StreamWriter(fileName);
						streamWriter.BaseStream.Write(imageByteArray, 0, imageByteArray.Length);
						streamWriter.Flush();
						streamWriter.Close();
					}
				}

				return this.getVirtualDirectory()+"/images/caching/"+this.code.Replace(" ", "_")+"."+this.imageType;
			}

			return "";
		}


		public void renderHtml(StringWriter stringWriter, Infojet infojetContext, WebPageLine webPageLine)
		{
			stringWriter.WriteLine("<a href=\""+getUrl()+"\">"+this.description+"</a>");
		}

		private void checkDocumentUpdate()
		{
			if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath("images/update")+"\\"+this.fileName))
			{
				uploadDocument(System.Web.HttpContext.Current.Server.MapPath("images/update")+"\\"+this.fileName);
				System.IO.File.Delete(System.Web.HttpContext.Current.Server.MapPath("images/update")+"\\"+this.fileName);

			}

		}

		private string getVirtualDirectory()
		{
			if (System.Web.HttpContext.Current.Request.ApplicationPath == "/") return "";
			return System.Web.HttpContext.Current.Request.ApplicationPath;

		}
	}
}
