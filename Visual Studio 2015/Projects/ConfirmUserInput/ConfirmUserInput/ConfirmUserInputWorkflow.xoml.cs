/*=====================================================================
  File:      ConfirmUserInputWorkflow.xoml.cs

  Summary:   Code-behind for the workflow.

---------------------------------------------------------------------
  This file is part of the Microsoft Live Communications Code Samples.

  Copyright (C) 2008 Microsoft Corporation.  All rights reserved.

This source code is intended only as a supplement to Microsoft
Development Tools and/or on-line documentation.  See these other
materials for detailed information regarding Microsoft code samples.

THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
PARTICULAR PURPOSE.
=====================================================================*/

using System;
using System.Diagnostics;
using System.Globalization;
using System.Workflow.Activities;
using Microsoft.Rtc.Workflow.Activities;
using Microsoft.Speech.Recognition;

namespace ConfirmUserInput
{
    /// <summary> 
    /// This example demonstrates how to ask for user input, and then repeat it back to the user.
    /// It also demonstrates how to use speech grammars and DTMF grammars in parallel when asking
    /// for numeric input.
    /// </summary>
    public partial class ConfirmUserInputWorkflow : SequentialWorkflowActivity
    {
        //Store the workflow culture so we can format our strings properly.
        private CultureInfo workflowCulture;

        /// <summary>
        /// Override this to get the workflow culture from the CommunicationsWorkflowRuntimeService.
        /// </summary>
        /// <param name="executionContext"></param>
        /// <returns></returns>
        protected override System.Workflow.ComponentModel.ActivityExecutionStatus Execute(System.Workflow.ComponentModel.ActivityExecutionContext executionContext)
        {
            CommunicationsWorkflowRuntimeService service =
                executionContext.GetService<CommunicationsWorkflowRuntimeService>();

            if (service == null)
            {
                throw new InvalidOperationException("CommunicationsWorkflowRuntimeService does not exist.");
            }

            this.workflowCulture = service.GetWorkflowCulture(this.WorkflowInstanceId);

            return base.Execute(executionContext);
        }

        /// <summary>
        /// This method is called when a call is Disconnected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleCallDisconnectedEvent(object sender, EventArgs e)
        {
            // Add your logic here.
        }

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
        /// set the grammar and DTMF grammar for the getPin QuestionAnswerActivity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetPin_TurnStarting(object sender, Microsoft.Rtc.Workflow.Activities.SpeechTurnStartingEventArgs e)
        {
            this.getPin.Grammars.Clear();
            this.getPin.Grammars.Add(
                new Grammar(
                    @"GrammarDefinitions\Digits.grxml",
                    "fourdigits"
                )
            );

            this.getPin.DtmfGrammars.Clear();
            this.getPin.DtmfGrammars.Add(
                new Grammar(
                    @"GrammarDefinitions\DigitsDtmf.grxml",
                    "fourdigits"
                )
            );
        }

        /// <summary>
        /// set the grammar and dynamic prompts for the confirmPin QuestionAnswerActivity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmPin_TurnStarting(object sender, Microsoft.Rtc.Workflow.Activities.SpeechTurnStartingEventArgs e)
        {
            this.confirmPin.Grammars.Clear();
            this.confirmPin.Grammars.Add(
                new Grammar(
                    @"GrammarDefinitions\Confirmation.grxml",
                    "Confirmation"
                )
            );

            this.confirmPin.MainPrompt.SetText(
                string.Format(
                    this.workflowCulture,
                    Properties.Resources.confirmPin_Main,
                    this.getPin.RecognitionResult.Text
                )
            );

            this.confirmPin.Prompts.NoRecognitionPrompt.SetText(
                string.Format(
                    this.workflowCulture,
                    Properties.Resources.confirmPin_NoRecognition,
                    this.getPin.RecognitionResult.Text
                )
            );

            this.confirmPin.Prompts.SilencePrompt.SetText(
                string.Format(
                    this.workflowCulture,
                    Properties.Resources.confirmPin_Silence,
                    this.getPin.RecognitionResult.Text
                )
            );
        }

        /// <summary>
        /// set the grammar and DTMF grammar for the getTrackingNumber QuestionAnswerActivity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetTrackingNumber_TurnStarting(object sender, Microsoft.Rtc.Workflow.Activities.SpeechTurnStartingEventArgs e)
        {
            this.getTrackingNumber.Grammars.Clear();
            this.getTrackingNumber.Grammars.Add(
                new Grammar(
                    @"GrammarDefinitions\Digits.grxml",
                    "sixdigits"
                )
            );

            this.getTrackingNumber.DtmfGrammars.Clear();
            this.getTrackingNumber.DtmfGrammars.Add(
                new Grammar(
                    @"GrammarDefinitions\DigitsDtmf.grxml",
                    "sixdigits"
                )
            );
        }

        /// <summary>
        /// set the grammar and dynamic prompts for the confirmTrackingNumber QuestionAnswerActivity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmTrackingNumber_TurnStarting(object sender, Microsoft.Rtc.Workflow.Activities.SpeechTurnStartingEventArgs e)
        {
            this.confirmTrackingNumber.Grammars.Clear();
            this.confirmTrackingNumber.Grammars.Add(
                new Grammar(
                    @"GrammarDefinitions\Confirmation.grxml",
                    "Confirmation"
                )
            );

            this.confirmTrackingNumber.MainPrompt.SetText(
                string.Format(
                    this.workflowCulture,
                    Properties.Resources.confirmTrackingNumber_Main,
                    this.getTrackingNumber.RecognitionResult.Text
                )
            );

            this.confirmTrackingNumber.Prompts.NoRecognitionPrompt.SetText(
                string.Format(
                    this.workflowCulture,
                    Properties.Resources.confirmTrackingNumber_NoRecognition,
                    this.getTrackingNumber.RecognitionResult.Text
                )
            );

            this.confirmTrackingNumber.Prompts.SilencePrompt.SetText(
                string.Format(
                    this.workflowCulture,
                    Properties.Resources.confirmTrackingNumber_Silence,
                    this.getTrackingNumber.RecognitionResult.Text
                )
            );
        }
    }
}
