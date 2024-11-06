using System;
using System.IO;
using System.Drawing;
using System.Data.SqlClient;


namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for Image.
	/// </summary>
	public class WebImage
	{
		public string code;
		public string description;
		public string fileName;
		public string publicDescription;
		public string imageType;
		public DateTime lastModified;
		public byte[] imageByteArray;
		public Image image;

        private string imageCachePath = "_assets/img_cache";
        private string imageUpdatePath = "_assets/img_update";
        private Database database;

		public WebImage(Database database, string code)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
				
			this.code = code;

			getFromDatabase(false);

		}

		public WebImage(Database database, SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
				
			readData(dataReader, false);
		}

		public WebImage(Database database, SqlDataReader dataReader, bool fromWebItemImage)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
				
			readData(dataReader, fromWebItemImage);
		}


		private void readData(SqlDataReader dataReader, bool fromWebItemImage)
		{
			try
			{
				if (fromWebItemImage)
				{
					code = dataReader.GetValue(5).ToString();
					description = dataReader.GetValue(6).ToString();
					fileName = dataReader.GetValue(7).ToString();
					imageType = dataReader.GetValue(8).ToString();
					DateTime lastModifiedDate = dataReader.GetDateTime(9);
					DateTime lastModifiedTime = dataReader.GetDateTime(10);
					lastModified = new DateTime(lastModifiedDate.Year, lastModifiedDate.Month, lastModifiedDate.Day, lastModifiedTime.Hour, lastModifiedTime.Minute, lastModifiedTime.Second, lastModifiedTime.Millisecond);

					publicDescription = dataReader.GetValue(11).ToString();
				}
				else
				{
					code = dataReader.GetValue(0).ToString();
					description = dataReader.GetValue(1).ToString();
					fileName = dataReader.GetValue(2).ToString();
					imageType = dataReader.GetValue(3).ToString();
					DateTime lastModifiedDate = dataReader.GetDateTime(4);
					DateTime lastModifiedTime = dataReader.GetDateTime(5);
					lastModified = new DateTime(lastModifiedDate.Year, lastModifiedDate.Month, lastModifiedDate.Day, lastModifiedTime.Hour, lastModifiedTime.Minute, lastModifiedTime.Second, lastModifiedTime.Millisecond);

					publicDescription = dataReader.GetValue(6).ToString();

				}
			}
			catch(Exception e)
			{
				throw new Exception("Image: "+this.code+", Exception: "+e.Message);
			}
			

		}

		public void uploadImage(string fileName)
		{
			imageByteArray = getBytesForImage(fileName);

			SqlConnection sqlConnection = database.getConnection();

			SqlCommand sqlCommand = sqlConnection.CreateCommand();
			sqlCommand.CommandText = "UPDATE ["+database.getTableName("Web Image")+"] SET [Image] = @image WHERE [Code] = '"+this.code+"'";
			sqlCommand.Parameters.Add("@image", System.Data.SqlDbType.Image, imageByteArray.Length).Value = imageByteArray;

			sqlCommand.ExecuteNonQuery();

			this.lastModified = DateTime.Now;
			save();

			getFromDatabase(true);
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

		private void getFromDatabase(bool loadImage)
		{
			if ((this.code != null) && (this.code != ""))
			{
				try
				{
					SqlDataReader dataReader = database.query("SELECT [Code], [Description], [Filename], [Image Type], [Last Modified Date], [Last Modified Time], [Public Description] FROM ["+database.getTableName("Web Image")+"] WHERE [Code] = '"+this.code+"'");
					if (dataReader.Read())
					{
						readData(dataReader, false);
					}

					dataReader.Close();
				}
				catch(Exception)
				{
					//throw new Exception("Image: "+this.code+", Exception: "+e.Message);
				}

				if (loadImage)
				{
					try
					{
				
						imageByteArray = (byte[])database.scalarQuery("SELECT [Image] FROM ["+database.getTableName("Web Image")+"] WHERE [Code] = '"+this.code+"'");
						if (imageByteArray != null)
						{
							try
							{
								image = Image.FromStream(new MemoryStream(imageByteArray));
							}
							catch(Exception)
							{}
						}
						else
						{
							//throw new Exception("ImageNotFoundException: "+this.code);
						}
					}
					catch(Exception)
					{
						//throw new Exception("Image: "+this.code+", Exception: "+e.Message);
					}
				}
			}
		}

		public void writeImage(StreamWriter writer)
		{
			if (image != null)
			{
				image.Save(writer.BaseStream, System.Drawing.Imaging.ImageFormat.Jpeg);
			}
		}


		public void setSize(float width, float height)
		{
			if (image != null)
			{
				if (width > image.Width) width = image.Width;
                
				float realWidth = width;
				float realHeight = ((width / image.Width) * image.Height);

				if (realHeight > height)
				{
					realHeight = height;
					realWidth = (height / image.Height) * image.Width;
				}

				Bitmap bmpSource = new Bitmap(image);
				
				Bitmap bmpTarget = new Bitmap((int)realWidth-1, (int)realHeight-1, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
				Graphics grfxThumb = Graphics.FromImage(bmpTarget);
				
				grfxThumb.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
				
				grfxThumb.DrawImage(bmpSource, new Rectangle(-1, -1, (int)realWidth, (int)realHeight));
				
				image = bmpTarget;

			}
		}

		public void save()
		{
			SqlDataReader dataReader = database.query("SELECT  [Code] FROM ["+database.getTableName("Web Image")+"] WHERE [Code] = '"+this.code+"'");
			bool exists = dataReader.Read();
			dataReader.Close();

			if (exists)
			{
				database.nonQuery("UPDATE ["+database.getTableName("Web Image")+"] SET [Description] = '"+this.description+"', [Filename] = '"+this.fileName+"', [Image Type] = '"+this.imageType+"', [Last Modified Date] = '"+this.lastModified.ToString("yyyy-MM-dd")+"', [Last Modified Time] = '1754-01-01 "+this.lastModified.ToString("HH:mm:ss")+"', [Public Description] = '"+this.publicDescription+"' WHERE [Code] = '"+this.code+"'");
			}
			else
			{
				database.nonQuery("INSERT INTO ["+database.getTableName("Web Image")+"] ([Code], [Description], [Filename], [Image Type], [Last Modified Date], [Last Modified Time], [Public Description]) VALUES ('"+this.code+"','"+this.description+"', '"+this.fileName+"', '"+this.imageType+"', '"+this.lastModified.ToString("yyyy-MM-dd")+"', '"+this.lastModified.ToString("HH:mm:ss")+"', '"+this.publicDescription+"')");
			}
		}

		public string getUrl()
		{
			//getFromDatabase();
			checkImageUpdate();

            string fileName = System.Web.HttpContext.Current.Server.MapPath(imageCachePath) + "\\" + this.code.Replace(" ", "_") + ".jpg";

			FileInfo cacheImageFile = new FileInfo(fileName);

			if (!cacheImageFile.Exists)
			{
				this.getFromDatabase(true);
				if (image != null) image.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
			}
			else
			{
				if (cacheImageFile.LastWriteTime.Ticks < this.lastModified.Ticks)
				{
					this.getFromDatabase(true);
					if (image != null) image.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
				}
			}

			return this.getVirtualDirectory()+"/"+imageCachePath+"/"+this.code.Replace(" ", "_")+".jpg";


		}

		public string getUrl(int width, int height)
		{			
			//getFromDatabase();
			checkImageUpdate();

            string fileName = System.Web.HttpContext.Current.Server.MapPath(imageCachePath) + "\\" + this.code.Replace(" ", "_") + "_" + width.ToString() + "_" + height.ToString() + ".jpg";

			checkImageUpdate();

			FileInfo cacheImageFile = new FileInfo(fileName);

			if (!cacheImageFile.Exists)
			{
				this.getFromDatabase(true);

				if (image != null)
				{
					this.setSize(width, height);
					try
					{
						image.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
					}
					catch(Exception e)
					{
						throw new Exception("WebImageException(1) in getUrl(width, height): Filename: "+fileName+", Exception: "+e.Message);
					}
				}
			}
			else
			{
				if (cacheImageFile.LastWriteTime < this.lastModified)
				{
					if (cacheImageFile.LastWriteTime < this.lastModified)
					{
						this.getFromDatabase(true);

						this.setSize(width, height);
						try
						{
							image.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
						}
						catch(Exception e)
						{
							throw new Exception("WebImageException(2) in getUrl(width, height): Filename: "+fileName+", Exception: "+e.Message);
						}
					}
				}
			}

			return this.getVirtualDirectory()+"/"+imageCachePath+"/"+this.code.Replace(" ", "_")+"_"+width.ToString()+"_"+height.ToString()+".jpg";

		}

		public void renderHtml(StringWriter stringWriter, Infojet infojetContext, WebPageLine webPageLine)
		{
			stringWriter.WriteLine("<img src=\""+getUrl()+"\" alt=\""+this.description+"\"/>");
		}

		private void checkImageUpdate()
		{
			if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(imageUpdatePath)+"\\"+this.fileName))
			{
                uploadImage(System.Web.HttpContext.Current.Server.MapPath(imageUpdatePath) + "\\" + this.fileName);
                System.IO.File.Delete(System.Web.HttpContext.Current.Server.MapPath(imageUpdatePath) + "\\" + this.fileName);

			}

			//getFromDatabase();
		}

		private string getVirtualDirectory()
		{
			if (System.Web.HttpContext.Current.Request.ApplicationPath == "/") return "";
			return System.Web.HttpContext.Current.Request.ApplicationPath;

		}
	}
}
