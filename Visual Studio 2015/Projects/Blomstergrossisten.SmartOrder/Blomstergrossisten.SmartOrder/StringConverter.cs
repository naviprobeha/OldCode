using System;

namespace SmartOrder
{
    /// <summary>
    /// Summary description for StringConverter.
    /// </summary>
    public class StringConverter
    {
        public StringConverter()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static string convertToAscii(string textString)
        {
            /*
                if (textString.Length > 5)
                {
                    if (textString.Substring(0, 3) == "All") System.Windows.Forms.MessageBox.Show(((int)textString[3]).ToString()+", "+textString);
				
				
                    textString = replace(233, 130, textString);

                    if (textString.Substring(0, 3) == "All") System.Windows.Forms.MessageBox.Show(((int)textString[3]).ToString()+", "+textString);
                }

                textString.Replace((char)233, (char)130);
    */
            return textString;
        }


        public static string replace(int oldChar, int newChar, string textString)
        {
            int i = 0;
            for (i = 0; i < textString.Length; i++)
            {
                if ((int)textString[i] == oldChar) textString = textString.Substring(0, i) + (char)newChar + textString.Substring(i + 1);
            }
            return textString;
        }
    }
}
