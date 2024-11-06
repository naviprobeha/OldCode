using System;
using System.Diagnostics;
using System.Workflow.Activities;
using Microsoft.Speech.Recognition;

namespace HelloWorld
{
    /// <summary>
    /// Represents the control flow for the application
    /// </summary>
    public partial class Workflow1 : SequentialWorkflowActivity
    {
        /// <summary>
        /// This method is called when any exception occurs within the workflow.  It does some 
        /// generic exception logging, and is provided for convenience during debugging; you 
        /// should replace or augment this with your own error handling code.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleGeneralFault(object sender, EventArgs e)
        {
            // When an exception is thrown the actual exception is stored in the Fault property,
            // which is read-only.  Check this value for error information; if it is an exception, 
            // ToString() will include a full stack trace of all inner exceptions.
            string errorMessage = generalFaultHandler.Fault.ToString();
            Trace.Write(errorMessage);

            if (Debugger.IsAttached)
            {
                // If the debugger is attached, break here so that you can see the error that occurred.
                // (Check the errorMessage variable above.)
                Debugger.Break();
            }
        }

        /// <summary>
        /// This method is called when a call is Disconnected while the workflow is still executing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleCallDisconnectedEvent(object sender, EventArgs e)
        {
            // Add your logic here.
        }

    }
}
