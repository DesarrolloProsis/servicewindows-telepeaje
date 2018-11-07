﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WindowsServiceTelepeaje.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.genivia.com/mashup.wsdl", ConfigurationName="ServiceReference1.PortType")]
    public interface PortType {
        
        // CODEGEN: Generating message contract since the wrapper namespace (urn:transaction) of message MoveTransactionsUpRequest does not match the default value (http://www.genivia.com/mashup.wsdl)
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        WindowsServiceTelepeaje.ServiceReference1.MoveTransactionsUpResponse MoveTransactionsUp(WindowsServiceTelepeaje.ServiceReference1.MoveTransactionsUpRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        System.Threading.Tasks.Task<WindowsServiceTelepeaje.ServiceReference1.MoveTransactionsUpResponse> MoveTransactionsUpAsync(WindowsServiceTelepeaje.ServiceReference1.MoveTransactionsUpRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="InsertaTran", WrapperNamespace="urn:transaction", IsWrapped=true)]
    public partial class MoveTransactionsUpRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:transaction", Order=0)]
        public int secuencial;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:transaction", Order=1)]
        public int carril;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:transaction", Order=2)]
        public string Fecha;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:transaction", Order=3)]
        public string Hora;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:transaction", Order=4)]
        public string tarjeta;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:transaction", Order=5)]
        public byte status;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:transaction", Order=6)]
        public byte clase;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:transaction", Order=7)]
        public byte ejes;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:transaction", Order=8)]
        public byte rodada;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:transaction", Order=9)]
        public int sec_piso;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:transaction", Order=10)]
        public byte turno;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:transaction", Order=11)]
        public int secuencialTC;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:transaction", Order=12)]
        public int autorizacionTC;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:transaction", Order=13)]
        public string tarjetaC;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:transaction", Order=14)]
        public int medioTC;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:transaction", Order=15)]
        public int statusTC;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:transaction", Order=16)]
        public System.DateTime UtcTime;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:transaction", Order=17)]
        public System.DateTime LocalTime;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:transaction", Order=18)]
        public byte tipoVehiculo;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:transaction", Order=19)]
        public string Cuerpo;
        
        //[System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:transaction", Order=20)]
        //public string nombreLista;
        
        //[System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:transaction", Order=21)]
        //public System.DateTime fechaCargaLista;
        
        public MoveTransactionsUpRequest() {
        }
        
        public MoveTransactionsUpRequest(
                    int secuencial, 
                    int carril, 
                    string Fecha, 
                    string Hora, 
                    string tarjeta, 
                    byte status, 
                    byte clase, 
                    byte ejes, 
                    byte rodada, 
                    int sec_piso, 
                    byte turno, 
                    int secuencialTC, 
                    int autorizacionTC, 
                    string tarjetaC, 
                    int medioTC, 
                    int statusTC, 
                    System.DateTime UtcTime, 
                    System.DateTime LocalTime, 
                    byte tipoVehiculo, 
                    string Cuerpo) { 
                    //string nombreLista, 
                    //System.DateTime fechaCargaLista) {
            this.secuencial = secuencial;
            this.carril = carril;
            this.Fecha = Fecha;
            this.Hora = Hora;
            this.tarjeta = tarjeta;
            this.status = status;
            this.clase = clase;
            this.ejes = ejes;
            this.rodada = rodada;
            this.sec_piso = sec_piso;
            this.turno = turno;
            this.secuencialTC = secuencialTC;
            this.autorizacionTC = autorizacionTC;
            this.tarjetaC = tarjetaC;
            this.medioTC = medioTC;
            this.statusTC = statusTC;
            this.UtcTime = UtcTime;
            this.LocalTime = LocalTime;
            this.tipoVehiculo = tipoVehiculo;
            this.Cuerpo = Cuerpo;
            //this.nombreLista = nombreLista;
            //this.fechaCargaLista = fechaCargaLista;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="InsertaTranResponse", WrapperNamespace="urn:transaction", IsWrapped=true)]
    public partial class MoveTransactionsUpResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:transaction", Order=0)]
        public int InsertaTranResult;
        
        public MoveTransactionsUpResponse() {
        }
        
        public MoveTransactionsUpResponse(int InsertaTranResult) {
            this.InsertaTranResult = InsertaTranResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface PortTypeChannel : WindowsServiceTelepeaje.ServiceReference1.PortType, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class PortTypeClient : System.ServiceModel.ClientBase<WindowsServiceTelepeaje.ServiceReference1.PortType>, WindowsServiceTelepeaje.ServiceReference1.PortType {
        
        public PortTypeClient() {
        }
        
        public PortTypeClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public PortTypeClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PortTypeClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PortTypeClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        WindowsServiceTelepeaje.ServiceReference1.MoveTransactionsUpResponse WindowsServiceTelepeaje.ServiceReference1.PortType.MoveTransactionsUp(WindowsServiceTelepeaje.ServiceReference1.MoveTransactionsUpRequest request) {
            return base.Channel.MoveTransactionsUp(request);
        }
        
        public int MoveTransactionsUp(
                    int secuencial, 
                    int carril, 
                    string Fecha, 
                    string Hora, 
                    string tarjeta, 
                    byte status, 
                    byte clase, 
                    byte ejes, 
                    byte rodada, 
                    int sec_piso, 
                    byte turno, 
                    int secuencialTC, 
                    int autorizacionTC, 
                    string tarjetaC, 
                    int medioTC, 
                    int statusTC, 
                    System.DateTime UtcTime, 
                    System.DateTime LocalTime, 
                    byte tipoVehiculo, 
                    string Cuerpo) {  
                    //string nombreLista, 
                    //System.DateTime fechaCargaLista) {
            WindowsServiceTelepeaje.ServiceReference1.MoveTransactionsUpRequest inValue = new WindowsServiceTelepeaje.ServiceReference1.MoveTransactionsUpRequest();
            inValue.secuencial = secuencial;
            inValue.carril = carril;
            inValue.Fecha = Fecha;
            inValue.Hora = Hora;
            inValue.tarjeta = tarjeta;
            inValue.status = status;
            inValue.clase = clase;
            inValue.ejes = ejes;
            inValue.rodada = rodada;
            inValue.sec_piso = sec_piso;
            inValue.turno = turno;
            inValue.secuencialTC = secuencialTC;
            inValue.autorizacionTC = autorizacionTC;
            inValue.tarjetaC = tarjetaC;
            inValue.medioTC = medioTC;
            inValue.statusTC = statusTC;
            inValue.UtcTime = UtcTime;
            inValue.LocalTime = LocalTime;
            inValue.tipoVehiculo = tipoVehiculo;
            inValue.Cuerpo = Cuerpo;
            //inValue.nombreLista = nombreLista;
            //inValue.fechaCargaLista = fechaCargaLista;
            WindowsServiceTelepeaje.ServiceReference1.MoveTransactionsUpResponse retVal = ((WindowsServiceTelepeaje.ServiceReference1.PortType)(this)).MoveTransactionsUp(inValue);
            return retVal.InsertaTranResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<WindowsServiceTelepeaje.ServiceReference1.MoveTransactionsUpResponse> WindowsServiceTelepeaje.ServiceReference1.PortType.MoveTransactionsUpAsync(WindowsServiceTelepeaje.ServiceReference1.MoveTransactionsUpRequest request) {
            return base.Channel.MoveTransactionsUpAsync(request);
        }
        
        public System.Threading.Tasks.Task<WindowsServiceTelepeaje.ServiceReference1.MoveTransactionsUpResponse> MoveTransactionsUpAsync(
                    int secuencial, 
                    int carril, 
                    string Fecha, 
                    string Hora, 
                    string tarjeta, 
                    byte status, 
                    byte clase, 
                    byte ejes, 
                    byte rodada, 
                    int sec_piso, 
                    byte turno, 
                    int secuencialTC, 
                    int autorizacionTC, 
                    string tarjetaC, 
                    int medioTC, 
                    int statusTC, 
                    System.DateTime UtcTime, 
                    System.DateTime LocalTime, 
                    byte tipoVehiculo, 
                    string Cuerpo) { 
                    //string nombreLista, 
                    //System.DateTime fechaCargaLista) {
            WindowsServiceTelepeaje.ServiceReference1.MoveTransactionsUpRequest inValue = new WindowsServiceTelepeaje.ServiceReference1.MoveTransactionsUpRequest();
            inValue.secuencial = secuencial;
            inValue.carril = carril;
            inValue.Fecha = Fecha;
            inValue.Hora = Hora;
            inValue.tarjeta = tarjeta;
            inValue.status = status;
            inValue.clase = clase;
            inValue.ejes = ejes;
            inValue.rodada = rodada;
            inValue.sec_piso = sec_piso;
            inValue.turno = turno;
            inValue.secuencialTC = secuencialTC;
            inValue.autorizacionTC = autorizacionTC;
            inValue.tarjetaC = tarjetaC;
            inValue.medioTC = medioTC;
            inValue.statusTC = statusTC;
            inValue.UtcTime = UtcTime;
            inValue.LocalTime = LocalTime;
            inValue.tipoVehiculo = tipoVehiculo;
            inValue.Cuerpo = Cuerpo;
            //inValue.nombreLista = nombreLista;
            //inValue.fechaCargaLista = fechaCargaLista;
            return ((WindowsServiceTelepeaje.ServiceReference1.PortType)(this)).MoveTransactionsUpAsync(inValue);
        }
    }
}
