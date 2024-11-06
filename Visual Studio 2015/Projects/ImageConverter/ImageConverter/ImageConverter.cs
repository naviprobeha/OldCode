using System;
using System.Runtime.InteropServices;
using System.Runtime;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

namespace Navipro.ImageConverter
{
    /// <summary>
    /// Summary description for Class1.
    /// </summary>


    class Guids
    {
        public const string coclsguid = "E03D715B-A13F-4cff-92F1-0319ADB3DE5F";
        public const string intfguid = "D030D214-C984-496a-87E7-31732C113E1E";
        public const string eventguid = "D030D214-C984-496a-87E7-31732C113E1F";

        public static readonly System.Guid idcoclass;
        public static readonly System.Guid idintf;
        public static readonly System.Guid idevent;

        static Guids()
        {
            idcoclass = new System.Guid(coclsguid);
            idintf = new System.Guid(intfguid);
            idevent = new System.Guid(eventguid);
        }
    }

    [Guid(Guids.intfguid), InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IImageConverter
    {
        [DispId(1)]
        void setSourceImage(string inFilename);
        [DispId(2)]
        bool convertImageToBmpFile(string outFilename);
        [DispId(3)]
        bool createBmpThumbnail(int width, int height, string outFilename);
        [DispId(4)]
        string getVersion();
        [DispId(5)]
        bool getSize(out int width, out int height);

    }


    [Guid(Guids.coclsguid), ProgId("Navipro.ImageConverter"), ClassInterface(ClassInterfaceType.None)]
    public class ImageConverter : IImageConverter
    {
        private Image image;

        public ImageConverter()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public void setSourceImage(string inFilename)
        {
            /*
            string extension = "";
            if (inFilename.IndexOf(".") > 0)
            {
                extension = inFilename.Substring(inFilename.IndexOf(".")+1);
                extension = "." + extension;
            }
            string tempFileName = "IC"+DateTime.Now.Ticks+extension;
            System.IO.File.Copy(inFilename, tempFileName, true);
            */

            image = Image.FromFile(inFilename);
        }

        public bool convertImageToBmpFile(string outFilename)
        {
            if (image != null)
            {
                image.Save(outFilename, System.Drawing.Imaging.ImageFormat.Bmp);
                image.Dispose();
                return true;
            }
            return false;
        }

        public bool createBmpThumbnail(int width, int height, string outFilename)
        {
            if (image != null)
            {
                Bitmap bmpSource = new Bitmap(image);


                // If the height is a 0 value, then make it the right ratio for the entered width
                if (height == 0)
                {
                    height = Convert.ToInt32(width * bmpSource.Height / bmpSource.Width);
                }
                else
                {
                    if (width == 0)
                    {
                        width = Convert.ToInt32(height * bmpSource.Width / bmpSource.Height);
                    }
                }
                Bitmap bmpTarget = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                Graphics grfxThumb = Graphics.FromImage(bmpTarget);

                // Set the interpolation mode
                // Bicubic				: Specifies bicubic interpolation
                // Bilinear				: Specifies bilinear interpolation
                // Default				: Specifies default mode
                // High					: Specifies high quality interpolation
                // HighQualityBicubic	: Specifies high quality bicubic interpolation ****************** THIS IS THE HIGHEST QUALITY
                // HighQualityBilinear	: Specifies high quality bilinear interpolation 
                // Invalid				: Equivalent to the Invalid element of the QualityMode enumeration
                // Low					: Specifies low quality interpolation
                // NearestNeighbor		: Specifies nearest-neighbor interpolation
                grfxThumb.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;

                grfxThumb.DrawImage(bmpSource, new Rectangle(0, 0, width, height));

                bmpTarget.Save(outFilename, ImageFormat.Bmp);

                image.Dispose();

            }
            return false;

        }

        public bool getSize(out int width, out int height)
        {
            width = 0;
            height = 0;

            if (image != null)
            {
                width = image.Size.Width;
                height = image.Size.Height;
                return true;
            }

            return false;
        }

        public string getVersion()
        {
            return "SE2.1";
        }
    }

}