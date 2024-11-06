﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.6421
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 2.0.50727.6421.
// 
#pragma warning disable 1591

namespace Navipro.Infojet.WebService.net.workanywhere.infojet {
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.6387")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="webRequestService_Binding", Namespace="urn:microsoft-dynamics-schemas/codeunit/webRequestService")]
    public partial class webRequestService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback ProcessRequestOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public webRequestService() {
            this.Url = global::Navipro.Infojet.WebService.Properties.Settings.Default.Navipro_Infojet_WebService_net_workanywhere_infojet_webRequestService;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event ProcessRequestCompletedEventHandler ProcessRequestCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/codeunit/webRequestService:ProcessRequest", RequestNamespace="urn:microsoft-dynamics-schemas/codeunit/webRequestService", ResponseElementName="ProcessRequest_Result", ResponseNamespace="urn:microsoft-dynamics-schemas/codeunit/webRequestService", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void ProcessRequest(string p_strGuid) {
            this.Invoke("ProcessRequest", new object[] {
                        p_strGuid});
        }
        
        /// <remarks/>
        public void ProcessRequestAsync(string p_strGuid) {
            this.ProcessRequestAsync(p_strGuid, null);
        }
        
        /// <remarks/>
        public void ProcessRequestAsync(string p_strGuid, object userState) {
            if ((this.ProcessRequestOperationCompleted == null)) {
                this.ProcessRequestOperationCompleted = new System.Threading.SendOrPostCallback(this.OnProcessRequestOperationCompleted);
            }
            this.InvokeAsync("ProcessRequest", new object[] {
                        p_strGuid}, this.ProcessRequestOperationCompleted, userState);
        }
        
        private void OnProcessRequestOperationCompleted(object arg) {
            if ((this.ProcessRequestCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ProcessRequestCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.6387")]
    public delegate void ProcessRequestCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
}

#pragma warning restore 1591