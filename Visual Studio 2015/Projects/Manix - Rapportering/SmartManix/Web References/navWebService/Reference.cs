﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.8825
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.CompactFramework.Design.Data, Version 2.0.50727.8825.
// 
namespace Navipro.SmartInventory.navWebService {
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="InfojetServiceRequestSoap", Namespace="http://infojet.workanywhere.se/")]
    public partial class InfojetServiceRequest : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        /// <remarks/>
        public InfojetServiceRequest() {
            this.Url = "http://192.168.222.55/ManixWMSWebService/InfojetServiceRequest.asmx";
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://infojet.workanywhere.se/performservice", RequestNamespace="http://infojet.workanywhere.se/", ResponseNamespace="http://infojet.workanywhere.se/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string performservice(string xmlDoc) {
            object[] results = this.Invoke("performservice", new object[] {
                        xmlDoc});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult Beginperformservice(string xmlDoc, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("performservice", new object[] {
                        xmlDoc}, callback, asyncState);
        }
        
        /// <remarks/>
        public string Endperformservice(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
    }
}
