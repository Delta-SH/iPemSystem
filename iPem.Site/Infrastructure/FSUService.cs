using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.18020")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Web.Services.WebServiceBindingAttribute(Name="FSUServiceSoapBinding", Namespace="http://FSUService.chinamobile.com")]
public partial class FSUServiceService : System.Web.Services.Protocols.SoapHttpClientProtocol {
    
    private System.Threading.SendOrPostCallback invokeOperationCompleted;
    
    /// <remarks/>
    public FSUServiceService() {
        this.Url = "http://127.0.0.1:8080/services/FSUService";
    }
    
    /// <remarks/>
    public event invokeCompletedEventHandler invokeCompleted;
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace="http://FSUService.chinamobile.com", ResponseNamespace="http://FSUService.chinamobile.com")]
    [return: System.Xml.Serialization.SoapElementAttribute("invokeReturn")]
    public string invoke(string xmlData) {
        object[] results = this.Invoke("invoke", new object[] {
                    xmlData});
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult Begininvoke(string xmlData, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("invoke", new object[] {
                    xmlData}, callback, asyncState);
    }
    
    /// <remarks/>
    public string Endinvoke(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public void invokeAsync(string xmlData) {
        this.invokeAsync(xmlData, null);
    }
    
    /// <remarks/>
    public void invokeAsync(string xmlData, object userState) {
        if ((this.invokeOperationCompleted == null)) {
            this.invokeOperationCompleted = new System.Threading.SendOrPostCallback(this.OninvokeOperationCompleted);
        }
        this.InvokeAsync("invoke", new object[] {
                    xmlData}, this.invokeOperationCompleted, userState);
    }
    
    private void OninvokeOperationCompleted(object arg) {
        if ((this.invokeCompleted != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.invokeCompleted(this, new invokeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    /// <remarks/>
    public new void CancelAsync(object userState) {
        base.CancelAsync(userState);
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.18020")]
public delegate void invokeCompletedEventHandler(object sender, invokeCompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.18020")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class invokeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    
    private object[] results;
    
    internal invokeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
            base(exception, cancelled, userState) {
        this.results = results;
    }
    
    /// <remarks/>
    public string Result {
        get {
            this.RaiseExceptionIfNecessary();
            return ((string)(this.results[0]));
        }
    }
}
