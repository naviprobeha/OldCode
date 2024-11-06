using System;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Workflow.Runtime;
using Microsoft.Rtc.Collaboration;
using Microsoft.Rtc.Collaboration.AudioVideo;
using Microsoft.Rtc.Signaling;
using Microsoft.Rtc.Workflow.Activities;

namespace HelloWorld
{
    /// <summary>
    /// A sample UC application
    /// </summary>
    static class Program
    {
        private static CollaborationPlatform _collabPlatform;
        private static ApplicationEndpoint _endpoint;
        private static WorkflowRuntime _workflowRuntime;
        private static ReaderWriterLock _shutdownLock = new ReaderWriterLock();
        private static bool _shuttingDown;

        /// <summary>
        /// Main
        /// </summary>
        private static void Main()
        {
            Initialize();

            Console.WriteLine("Press Enter to stop.");
            Console.ReadLine();

            Cleanup();
        }

        /// <summary>
        /// Return the local certificate that establishes the trust relationship between the application machine and the OCS machine.
        /// </summary>
        /// <param name="issuedTo">Name of the machine the certificate was issued to.</param>
        /// <param name="issuerName">Name of certification authority that issued the certificate</param>
        /// <returns>A X509Certificate2 object matching the issuer name</returns>
        private static X509Certificate2 GetLocalCertificate(string issuedTo, string issuerName)
        {
            if (!string.IsNullOrEmpty(issuedTo) && !string.IsNullOrEmpty(issuerName))
            {
                X509Store store = new X509Store(StoreLocation.LocalMachine);
                store.Open(OpenFlags.ReadOnly);

                X509Certificate2Collection certificates = store.Certificates;
                foreach (X509Certificate2 certificate in certificates)
                {
                    string certIssuedTo = certificate.GetNameInfo(X509NameType.DnsName, false);
                    if (string.IsNullOrEmpty(certIssuedTo))
                    {
                        continue;
                    }

                    if (certificate.NotBefore > DateTime.Now || certificate.NotAfter < DateTime.Now)
                    {
                        continue;
                    }

                    if (certIssuedTo.Equals(issuedTo, StringComparison.OrdinalIgnoreCase) && 
                        certificate.IssuerName.Name.Contains(issuerName) && 
                        certificate.HasPrivateKey)
                    {
                        return certificate;
                    }
                }
            }
            
            return null;
        }

        /// <summary>
        /// Initialize static members.  
        /// </summary>
        private static void Initialize()
        {
            Console.Write("Step 1 of 3: Initializing the Workflow Runtime... ");
            InitializeWorkflow();
            Console.WriteLine("Complete");

            string gruu = string.Empty; /*Gruu of the trusted service added for this application on OCS server */
            X509Certificate2 cert = GetLocalCertificate(
                string.Empty, /* The FQDN of the machine the certificate was issued to. */
                string.Empty /* The name of the certificate issuer. */
            );
            ServerPlatformSettings platformSettings ;
            if (cert == null)
            {
                platformSettings = new ServerPlatformSettings("AppPlatform",
                    Dns.GetHostEntry("localhost").HostName, 5060 /*Change this to the port of choice.*/, null);
            }
            else
            {
                platformSettings = new ServerPlatformSettings("AppPlatform",
                    Dns.GetHostEntry("localhost").HostName, 
                    5060 /*Change the port to the value you register the Contact Object in OCS server.*/, 
                    gruu, cert);
            }

            _collabPlatform = new CollaborationPlatform(platformSettings);
            _collabPlatform.InstantMessagingSettings.SupportedFormats = InstantMessagingFormat.PlainText;

            Console.Write("Step 2 of 3: Starting CollaborationPlatform... ");
            _collabPlatform.EndStartup(_collabPlatform.BeginStartup(null, null));
            Console.WriteLine("Complete");

            //////////////////////////////////////////////////////////////////////////
            // Please specify the application uri and the ocsFqdn (aka proxyHost) 
            // if you want to use the endpoint with contact object. 
            // The default template uses empty values.
            //////////////////////////////////////////////////////////////////////////
            string applicationUri = String.Empty; /*SIP Uri used for contact object */
            string ocsFqdn = String.Empty; /*FQDN of the OCS Server or OCS Load Balancer*/
            int ocsTlsPort = 5061; /*Change the port to the value that OCS server listens for TLS protocol*/

            ApplicationEndpointSettings settings;
            if (string.IsNullOrEmpty(applicationUri) || string.IsNullOrEmpty(ocsFqdn))
            {
                settings = new ApplicationEndpointSettings("sip:server@" + Dns.GetHostEntry("localhost").HostName);
            }
            else
            {
                settings = new ApplicationEndpointSettings(applicationUri, ocsFqdn, ocsTlsPort);
                settings.UseRegistration = true;
            }

            _endpoint = new ApplicationEndpoint(_collabPlatform, settings);
            _endpoint.RegisterForIncomingCall<AudioVideoCall>(AudioVideoCallReceived);
            _endpoint.RegisterForIncomingCall<InstantMessagingCall>(InstantMessagingCallReceived);

            Console.Write("Step 3 of 3: Establishing Endpoint... ");
            _endpoint.EndEstablish(_endpoint.BeginEstablish(null, _endpoint));
            Console.WriteLine("Complete");
        }

        private static void InitializeWorkflow()
        {
            _workflowRuntime = new WorkflowRuntime();
            _workflowRuntime.AddService(new CommunicationsWorkflowRuntimeService());
            _workflowRuntime.AddService(new TrackingDataWorkflowRuntimeService());

            _workflowRuntime.StartRuntime();
        }

        private static void Cleanup()
        {
            _shuttingDown = true;
            // The purpose of acquiring the writer lock is to ensure that AudioVideoCallReceived and InstantMessagingCallReceived
            // have incremented if they are going to.  We don't actually need to put anything inside the lock.
            _shutdownLock.AcquireWriterLock(Timeout.Infinite);
            _shutdownLock.ReleaseWriterLock();

            if (_workflowRuntime != null)
            {
                ManualResetEvent workflowRuntimeStopped = new ManualResetEvent(false);
                _workflowRuntime.Stopped += delegate(object sender, WorkflowRuntimeEventArgs e)
                {
                    workflowRuntimeStopped.Set();
                };
                
                _workflowRuntime.StopRuntime();

                int timeoutSecs = 45;
                bool signaled = workflowRuntimeStopped.WaitOne(TimeSpan.FromSeconds(timeoutSecs), false);
                Debug.Assert(signaled, "Workflow runtime still in progress after " + timeoutSecs + " secs, shutting down anyway.");

                _workflowRuntime.Dispose();
                _workflowRuntime = null;
            }

            // Terminate the endpoint once the workflow runtime is finished.
            if (_endpoint != null)
            {
                _endpoint.EndTerminate(_endpoint.BeginTerminate(null, null));
                _endpoint = null;
            }
        }

        /// <summary>
        /// EventHandler raised when an incoming invite arrives.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void AudioVideoCallReceived(object sender, CallReceivedEventArgs<AudioVideoCall> e)
        {
            Debug.Assert(e != null, "e != null");
            Debug.Assert(e.RequestData != null, "e.RequestData != null");
            StartWorkflow(e.Call, e.RequestData);
        }

        /// <summary>
        /// EventHandler raised when an incoming instant message arrives.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void InstantMessagingCallReceived(object sender, CallReceivedEventArgs<InstantMessagingCall> e)
        {
            Debug.Assert(e != null, "e != null");
            Debug.Assert(e.RequestData != null, "e.RequestData != null"); 
            StartWorkflow(e.Call, e.RequestData);
        }

        private static void StartWorkflow(Call call, SipRequestData requestData)
        {
            Debug.Assert(call != null, "call != null");
            Debug.Assert(call is AudioVideoCall || call is InstantMessagingCall,
                "Only AudioVideoCall and InstantMessagingCall are subscribed to above.");

            WorkflowInstance workflowInstance = _workflowRuntime.CreateWorkflow(typeof(Workflow1));
            Debug.Assert(workflowInstance != null, "workflowInstance != null");

            CommunicationsWorkflowRuntimeService communicationsWorkflowRuntimeService = (CommunicationsWorkflowRuntimeService)
                _workflowRuntime.GetService(typeof(CommunicationsWorkflowRuntimeService));
            Debug.Assert(communicationsWorkflowRuntimeService != null, "communicationsWorkflowRuntimeService != null");

            try
            {
                communicationsWorkflowRuntimeService.EnqueueCall(workflowInstance.InstanceId, call);
                communicationsWorkflowRuntimeService.SetEndpoint(workflowInstance.InstanceId, _endpoint);
                communicationsWorkflowRuntimeService.SetWorkflowCulture(workflowInstance.InstanceId, new CultureInfo("sv-SE"));
            }
            catch (InvalidOperationException ex)
            {
                call.EndTerminate(call.BeginTerminate(null, null));
                Debug.Fail("Unable to configure the CommunicationsWorkflowRuntimeService: " + ex.ToString());
            }
            
            _shutdownLock.AcquireReaderLock(Timeout.Infinite);

            try
            {
                if (_shuttingDown)
                {
                    call.EndTerminate(call.BeginTerminate(null, null));
                    return;
                }
            }
            finally
            {
                _shutdownLock.ReleaseReaderLock();
            }

            workflowInstance.Start();
        }
    }
}