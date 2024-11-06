using System;

namespace Navipro.SantaMonica.JobEngine
{
	/// <summary>
	/// Summary description for FileUtility.
	/// </summary>
	public class FileUtility
	{
		public FileUtility()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static string getNextField(ref string textLine)
		{
			string returnText = "";

			if (textLine != "")
			{
				if (textLine.IndexOf(",") > 0)
				{
					returnText = textLine.Substring(0, textLine.IndexOf(","));
					textLine = textLine.Substring(textLine.IndexOf(",")+1);
				}
				else
				{
					returnText = textLine;
					textLine = "";
				}

				if (returnText[0] == '\"') returnText = returnText.Substring(1, returnText.Length-2);

			}

			return returnText;
		}
	}
}
