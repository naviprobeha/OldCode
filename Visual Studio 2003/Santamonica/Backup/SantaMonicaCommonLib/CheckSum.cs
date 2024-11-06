using System;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for CheckSum.
	/// </summary>
	public class CheckSum
	{

		public CheckSum()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static int calcCheckSum(string checkString)
		{
			int i = 0;
			int sum = 0;

			for (i=0;i<checkString.Length;i++)
			{
				sum = sum + (int)checkString[i];
			}
			
			return sum % 100;
		}
	}
}
