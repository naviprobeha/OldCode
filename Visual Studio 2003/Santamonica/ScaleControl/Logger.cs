using System;

namespace Navipro.SantaMonica.ScaleControl
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public interface Logger
	{
		void write(string message, int type);
	}
}