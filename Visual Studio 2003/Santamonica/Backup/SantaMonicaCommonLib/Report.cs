using System;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public interface Report
	{
		void setDatabase(Database database);
		void setParameter(string parameterCode, string parameterValue);
		string getParameter(string parameterCode);
		string renderReport();
		string getName();
	}
}
