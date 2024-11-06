using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using Navipro.SantaMonica.ScaleControl;

namespace Navipro.SantaMonica.ScaleControl.Service
{
	public class ScaleControlService : System.ServiceProcess.ServiceBase, Logger
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private ScaleControl scaleControl;

		public ScaleControlService()
		{
			// This call is required by the Windows.Forms Component Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitComponent call
		}

		// The main entry point for the process
		static void Main()
		{
			System.ServiceProcess.ServiceBase[] ServicesToRun;
	
			// More than one user Service may run within the same process. To add
			// another service to this process, change the following line to
			// create a second service object. For example,
			//
			//   ServicesToRun = new System.ServiceProcess.ServiceBase[] {new Service1(), new MySecondUserService()};
			//
			ServicesToRun = new System.ServiceProcess.ServiceBase[] { new ScaleControlService() };

			System.ServiceProcess.ServiceBase.Run(ServicesToRun);
		}

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			this.ServiceName = "ScaleControlService";
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		/// <summary>
		/// Set things in motion so your service can do its work.
		/// </summary>
		protected override void OnStart(string[] args)
		{
			// TODO: Add code here to start your service.
			scaleControl = new ScaleControl(this);
			scaleControl.start();
		}
 
		/// <summary>
		/// Stop this service.
		/// </summary>
		protected override void OnStop()
		{
			// TODO: Add code here to perform any tear-down necessary to stop your service.
			scaleControl.stop();
		}
		#region Logger Members

		public void write(string message, int type)
		{
			// TODO:  Add ScaleControlService.write implementation
			if (type == 0) this.EventLog.WriteEntry(message, System.Diagnostics.EventLogEntryType.Information);
			if (type == 1) this.EventLog.WriteEntry(message, System.Diagnostics.EventLogEntryType.Warning);
			if (type == 2) this.EventLog.WriteEntry(message, System.Diagnostics.EventLogEntryType.Error);

		}

		#endregion
	}
}
