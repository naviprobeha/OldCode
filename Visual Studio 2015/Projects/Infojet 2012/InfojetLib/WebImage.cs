using System;
using System.IO;
using System.Drawing;
using System.Data.SqlClient;


namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for Image.
	/// </summary>
    /// 

    [Serializable]
	public class WebImage
	{
		public string code;
		public string description;
		public string fileName;
		public string publicDescription;
		public string imageType;
		public DateTime lastModified;
		private byte[] imageByteArray;
		private Image image;
        public bool doNotAllowResize;


        private string imageCachePath = "/_assets/img_cache";
        private string imageUpdatePath = "/_assets/img_update";
        private Infojet infojetContext;

        public WebImage()
        {

        }

		public WebImage(Infojet infojetContext, string code)
		{
			//
			// TODO: Add constructor logic here
			//
            imageCachePath = getVirtualDirectory() + imageCachePath;
            imageUpdatePath = getVirtualDirectory() + imageUpdatePath;

            this.infojetContext = infojetContext;
				
			this.code = code;

			getFromDatabase(false);

		}

        public WebImage(Infojet infojetContext, SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//
            imageCachePath = getVirtualDirectory() + imageCachePath;
            imageUpdatePath = getVirtualDirectory() + imageUpdatePath;

            this.infojetContext = infojetContext;
				
			readData(dataReader, false);
		}

        public WebImage(Infojet infojetContext, SqlDataReader dataReader, bool fromWebItemImage)
		{
			//
			// TODO: Add constructor logic here
			//
            imageCachePath = getVirtualDirectory() + imageCachePath;
            imageUpdatePath = getVirtualDirectory() + imageUpdatePath;

            this.infojetContext = infojetContext;
				
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

                    doNotAllowResize = false;
                    if (dataReader.GetValue(7).ToString() == "1") doNotAllowResize = true;

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
            /*
			imageByteArray = getBytesForImage(fileName);

            SqlConnection sqlConnection = infojetContext.systemDatabase.getConnection();

			SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = "UPDATE [" + infojetContext.systemDatabase.getTableName("Web Image") + "] SET [Image] = @image WHERE [Code] = @code";
			sqlCommand.Parameters.Add("@image", System.Data.SqlDbType.Image, imageByteArray.Length).Value = imageByteArray;
            sqlCommand.Parameters.Add("@code", System.Data.SqlDbType.NVarChar, 20).Value = code;

			sqlCommand.ExecuteNonQuery();

			this.lastModified = DateTime.Now;
			save();

			getFromDatabase(true);
             * */
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
                    DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Code], [Description], [Filename], [Image Type], [Last Modified Date], [Last Modified Time], [Public Description], [Do Not Allow Re-Size] FROM [" + infojetContext.systemDatabase.getTableName("Web Image") + "] WHERE [Code] = @code");
                    databaseQuery.addStringParameter("code", code, 20);


                    SqlDataReader dataReader = databaseQuery.executeQuery();
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
                        DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Image] FROM [" + infojetContext.systemDatabase.getTableName("Web Image") + "] WHERE [Code] = @code");
                        databaseQuery.addStringParameter("code", code, 20);


                        imageByteArray = (byte[])databaseQuery.executeScalar();
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
            /*
            SqlDataReader dataReader = infojetContext.systemDatabase.query("SELECT [Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Image") + "] WHERE [Code] = '" + this.code + "'");
			bool exists = dataReader.Read();
			dataReader.Close();

			if (exists)
			{
                infojetContext.systemDatabase.nonQuery("UPDATE [" + infojetContext.systemDatabase.getTableName("Web Image") + "] SET [Description] = '" + this.description + "', [Filename] = '" + this.fileName + "', [Image Type] = '" + this.imageType + "', [Last Modified Date] = '" + this.lastModified.ToString("yyyy-MM-dd") + "', [Last Modified Time] = '1754-01-01 " + this.lastModified.ToString("HH:mm:ss") + "', [Public Description] = '" + this.publicDescription + "' WHERE [Code] = '" + this.code + "'");
			}
			else
			{
                infojetContext.systemDatabase.nonQuery("INSERT INTO [" + infojetContext.systemDatabase.getTableName("Web Image") + "] ([Code], [Description], [Filename], [Image Type], [Last Modified Date], [Last Modified Time], [Public Description]) VALUES ('" + this.code + "','" + this.description + "', '" + this.fileName + "', '" + this.imageType + "', '" + this.lastModified.ToString("yyyy-MM-dd") + "', '" + this.lastModified.ToString("HH:mm:ss") + "', '" + this.publicDescription + "')");
			}
             * */
		}

        private System.Drawing.Imaging.ImageFormat getFormat()
        {
            if (this.imageType.ToUpper() == "GIF") return System.Drawing.Imaging.ImageFormat.Gif;
            if (this.imageType.ToUpper() == "JPG") return System.Drawing.Imaging.ImageFormat.Jpeg;
            if (this.imageType.ToUpper() == "PNG") return System.Drawing.Imaging.ImageFormat.Png;
            return System.Drawing.Imaging.ImageFormat.Jpeg;
        }

        private string getFormatExt()
        {
            if (imageType != null)
            {
                if (this.imageType.ToUpper() == "GIF") return this.imageType;
                if (this.imageType.ToUpper() == "JPG") return this.imageType;
                if (this.imageType.ToUpper() == "PNG") return this.imageType;
            }
            return "jpg";
        }

		public string getUrl()
		{
            if (this.code == null) return "";
            if (this.code == "") return "";

             //getFromDatabase();
			checkImageUpdate();

            string fileName = System.Web.HttpContext.Current.Server.MapPath(imageCachePath) + "\\" + this.code.Replace(" ", "_") + "."+getFormatExt();

			FileInfo cacheImageFile = new FileInfo(fileName);

			if (!cacheImageFile.Exists)
			{
				this.getFromDatabase(true);
				if (image != null) image.Save(fileName, getFormat());
			}
			else
			{
				if (cacheImageFile.LastWriteTime.Ticks < this.lastModified.Ticks)
				{
					this.getFromDatabase(true);
					if (image != null) image.Save(fileName, getFormat());
				}
			}

            //throw new Exception("Hupp: " + infojetContext.webSite.code + ", " + infojetContext.webSite.location);
			return imageCachePath+"/"+this.code.Replace(" ", "_")+"."+getFormatExt();


		}

		public string getUrl(int width, int height)
		{
            if (this.code == null) return "";
            if (this.code == "") return "";
            if (this.doNotAllowResize) return getUrl();

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
                    catch (Exception e)
                    {
                        throw new Exception("WebImageException(1) in getUrl(width, height): Filename: " + fileName + ", Exception: " + e.Message);
                    }
                }
            }
            else
            {
                if (cacheImageFile.LastWriteTime < this.lastModified.AddHours(1))
                {
                    this.getFromDatabase(true);

                    this.setSize(width, height);
                    try
                    {
                        if (image != null) image.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    catch (Exception e)
                    {
                        throw new Exception("WebImageException(2) in getUrl(width, height): Filename: " + fileName + ", Exception: " + e.Message);
                    }
                }
            }

			return imageCachePath+"/"+this.code.Replace(" ", "_")+"_"+width.ToString()+"_"+height.ToString()+".jpg";

		}

        public byte[] getByteArray(int width, int height)
        {
            if (this.code == null) return null;
            if (this.code == "") return null;


            this.getFromDatabase(true);

            if (image != null)
            {
                if ((!this.doNotAllowResize) && ((width > 0) || (height > 0))) this.setSize(width, height);

                
                try
                {
                    MemoryStream ms = new MemoryStream();
                    image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    return ms.ToArray();
                    
                }
                catch (Exception e)
                {
                    throw new Exception("WebImageException(1) in getUrl(width, height): Filename: " + fileName + ", Exception: " + e.Message);
                }
            }

            return null;
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
