﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.6407
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Navipro.Infojet.WebService.net.workanywhere.outnorth {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="urn:microsoft-dynamics-schemas/codeunit/ServiceRequestWebService", ConfigurationName="net.workanywhere.outnorth.ServiceRequestWebService_Port")]
    public interface ServiceRequestWebService_Port {
        
        // CODEGEN: Generating message contract since the wrapper name (ProcessRequest_Result) of message ProcessRequest_Result does not match the default value (ProcessRequest)
        [System.ServiceModel.OperationContractAttribute(Action="urn:microsoft-dynamics-schemas/codeunit/ServiceRequestWebService:ProcessRequest", ReplyAction="*")]
        Navipro.Infojet.WebService.net.workanywhere.outnorth.ProcessRequest_Result ProcessRequest(Navipro.Infojet.WebService.net.workanywhere.outnorth.ProcessRequest request);
        
        // CODEGEN: Generating message contract since the wrapper name (Hepp_Result) of message Hepp_Result does not match the default value (Hepp)
        [System.ServiceModel.OperationContractAttribute(Action="urn:microsoft-dynamics-schemas/codeunit/ServiceRequestWebService:Hepp", ReplyAction="*")]
        Navipro.Infojet.WebService.net.workanywhere.outnorth.Hepp_Result Hepp(Navipro.Infojet.WebService.net.workanywhere.outnorth.Hepp request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="ProcessRequest", WrapperNamespace="urn:microsoft-dynamics-schemas/codeunit/ServiceRequestWebService", IsWrapped=true)]
    public partial class ProcessRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:microsoft-dynamics-schemas/codeunit/ServiceRequestWebService", Order=0)]
        public string p_strGuid;
        
        public ProcessRequest() {
        }
        
        public ProcessRequest(string p_strGuid) {
            this.p_strGuid = p_strGuid;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="ProcessRequest_Result", WrapperNamespace="urn:microsoft-dynamics-schemas/codeunit/ServiceRequestWebService", IsWrapped=true)]
    public partial class ProcessRequest_Result {
        
        public ProcessRequest_Result() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Hepp", WrapperNamespace="urn:microsoft-dynamics-schemas/codeunit/ServiceRequestWebService", IsWrapped=true)]
    public partial class Hepp {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:microsoft-dynamics-schemas/codeunit/ServiceRequestWebService", Order=0)]
        public string p_strGuid;
        
        public Hepp() {
        }
        
        public Hepp(string p_strGuid) {
            this.p_strGuid = p_strGuid;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Hepp_Result", WrapperNamespace="urn:microsoft-dynamics-schemas/codeunit/ServiceRequestWebService", IsWrapped=true)]
    public partial class Hepp_Result {
        
        public Hepp_Result() {
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface ServiceRequestWebService_PortChannel : Navipro.Infojet.WebService.net.workanywhere.outnorth.ServiceRequestWebService_Port, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class ServiceRequestWebService_PortClient : System.ServiceModel.ClientBase<Navipro.Infojet.WebService.net.workanywhere.outnorth.ServiceRequestWebService_Port>, Navipro.Infojet.WebService.net.workanywhere.outnorth.ServiceRequestWebService_Port {
        
        public ServiceRequestWebService_PortClient() {
        }
        
        public ServiceRequestWebService_PortClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceRequestWebService_PortClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceRequestWebService_PortClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceRequestWebService_PortClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Navipro.Infojet.WebService.net.workanywhere.outnorth.ProcessRequest_Result Navipro.Infojet.WebService.net.workanywhere.outnorth.ServiceRequestWebService_Port.ProcessRequest(Navipro.Infojet.WebService.net.workanywhere.outnorth.ProcessRequest request) {
            return base.Channel.ProcessRequest(request);
        }
        
        public void ProcessRequest(string p_strGuid) {
            Navipro.Infojet.WebService.net.workanywhere.outnorth.ProcessRequest inValue = new Navipro.Infojet.WebService.net.workanywhere.outnorth.ProcessRequest();
            inValue.p_strGuid = p_strGuid;
            Navipro.Infojet.WebService.net.workanywhere.outnorth.ProcessRequest_Result retVal = ((Navipro.Infojet.WebService.net.workanywhere.outnorth.ServiceRequestWebService_Port)(this)).ProcessRequest(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Navipro.Infojet.WebService.net.workanywhere.outnorth.Hepp_Result Navipro.Infojet.WebService.net.workanywhere.outnorth.ServiceRequestWebService_Port.Hepp(Navipro.Infojet.WebService.net.workanywhere.outnorth.Hepp request) {
            return base.Channel.Hepp(request);
        }
        
        public void Hepp(string p_strGuid) {
            Navipro.Infojet.WebService.net.workanywhere.outnorth.Hepp inValue = new Navipro.Infojet.WebService.net.workanywhere.outnorth.Hepp();
            inValue.p_strGuid = p_strGuid;
            Navipro.Infojet.WebService.net.workanywhere.outnorth.Hepp_Result retVal = ((Navipro.Infojet.WebService.net.workanywhere.outnorth.ServiceRequestWebService_Port)(this)).Hepp(inValue);
        }
    }
}
