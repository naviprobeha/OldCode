using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Navipro.Infojet.Lib
{
    public class Captcha
    {

        public Captcha()
        {
            

        }

        public string generateCode()
        {
            string captchaImageText = GenerateRandomCode();
            System.Web.HttpContext.Current.Session["CaptchaImageText"] = captchaImageText;

            return captchaImageText;

        }

        public void generateImage()
        {
            string captchaImageText = System.Web.HttpContext.Current.Session["CaptchaImageText"].ToString();

            CaptchaImage ci = new CaptchaImage(captchaImageText, 300, 75);

            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.ContentType = "image/jpeg";

            ci.Image.Save(System.Web.HttpContext.Current.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            ci.Dispose();

        }

        private string GenerateRandomCode()
        {
            Random r = new Random();
            string s = "";
            for (int j = 0; j < 5; j++)
            {
                int i = r.Next(3);
                int ch;
                switch (i)
                {
                    case 1:
                        ch = r.Next(0, 9);
                        s = s + ch.ToString();
                        break;
                    case 2:
                        ch = r.Next(65, 90);
                        s = s + Convert.ToChar(ch).ToString();
                        break;
                    case 3:
                        ch = r.Next(97, 122);
                        s = s + Convert.ToChar(ch).ToString();
                        break;
                    default:
                        ch = r.Next(97, 122);
                        s = s + Convert.ToChar(ch).ToString();
                        break;
                }
                r.NextDouble();
                r.Next(100, 1999);
            }
            return s;
        }

    }
}
