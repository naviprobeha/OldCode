using System;
using System.Windows.Forms;


namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for Main.
	/// </summary>
	public class MainClass
	{
		public MainClass()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		static void Main() 
		{
			DataSetup dataSetup = new DataSetup();
			dataSetup.refresh();

			
			int appMode = dataSetup.applicationMode;

			if (appMode == 2)
			{
				ApplicationMode applicationMode = new ApplicationMode();
				applicationMode.ShowDialog();

				appMode = applicationMode.getMode();

			}

			if (appMode == 0) Application.Run(new StartFormShip());
			if (appMode == 1) Application.Run(new StartFormLine());
			if (appMode == 3) Application.Run(new StartFormFactory());

		}
	}
}
