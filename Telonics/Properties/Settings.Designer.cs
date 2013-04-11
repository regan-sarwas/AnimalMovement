﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Telonics.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\Program Files (x86)\\Telonics\\Data Converter\\TDC.exe")]
        public string TdcPathToExecutable {
            get {
                return ((string)(this["TdcPathToExecutable"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("20000")]
        public int TdcMillisecondTimeout {
            get {
                return ((int)(this["TdcMillisecondTimeout"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<BatchSettings>\r\n <ArgosFile>{0}</ArgosFile>\r\n <ParameterFile>{1}</ParameterFile>" +
            "\r\n <OutputFolder>{2}</OutputFolder>\r\n <BatchLog>{3}</BatchLog>\r\n <MoveFiles>fals" +
            "e</MoveFiles>\r\n <GoogleEarth>false</GoogleEarth>\r\n</BatchSettings>")]
        public string TdcArgosBatchFileFormat {
            get {
                return ((string)(this["TdcArgosBatchFileFormat"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<BatchSettings>\r\n <DatalogFile>{0}</DatalogFile>\r\n <OutputFolder>{1}</OutputFolde" +
            "r>\r\n <BatchLog>{2}</BatchLog>\r\n <MoveFiles>false</MoveFiles>\r\n <GoogleEarth>fals" +
            "e</GoogleEarth>\r\n</BatchSettings>")]
        public string TdcDatalogBatchFileFormat {
            get {
                return ((string)(this["TdcDatalogBatchFileFormat"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public int ArgosServerMinDownloadDays {
            get {
                return ((int)(this["ArgosServerMinDownloadDays"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("10")]
        public int ArgosServerMaxDownloadDays {
            get {
                return ((int)(this["ArgosServerMaxDownloadDays"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://ws-argos.clsamerica.com/argosDws/services/DixService")]
        public string ArgosUrl {
            get {
                return ((string)(this["ArgosUrl"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<soap:Envelope xmlns:soap=""""http://www.w3.org/2003/05/soap-envelope"""" xmlns:argos=""""http://service.dataxmldistribution.argos.cls.fr/types"""">
<soap:Header/>
<soap:Body>
<argos:csvRequest>
<argos:username>{0}</argos:username>
<argos:password>{1}</argos:password>
<argos:platformId>{2}</argos:platformId>
<argos:nbDaysFromNow>{3}</argos:nbDaysFromNow>
<argos:displayLocation>true</argos:displayLocation>
<argos:displayDiagnostic>true</argos:displayDiagnostic>
<argos:displayRawData>true</argos:displayRawData>
<argos:displayImageLocation>true</argos:displayImageLocation>
<argos:displayHexId>true</argos:displayHexId>
<argos:showHeader>true</argos:showHeader>
</argos:csvRequest>
</soap:Body>
</soap:Envelope>")]
        public string ArgosPlatformSoapRequest {
            get {
                return ((string)(this["ArgosPlatformSoapRequest"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<soap:Envelope xmlns:soap=""""http://www.w3.org/2003/05/soap-envelope"""" xmlns:argos=""""http://service.dataxmldistribution.argos.cls.fr/types"""">
<soap:Header/>
<soap:Body>
<argos:csvRequest>
<argos:username>{0}</argos:username>
<argos:password>{1}</argos:password>
<argos:programNumber>{2}</argos:programNumber>
<argos:nbDaysFromNow>{3}</argos:nbDaysFromNow>
<argos:displayLocation>true</argos:displayLocation>
<argos:displayDiagnostic>true</argos:displayDiagnostic>
<argos:displayRawData>true</argos:displayRawData>
<argos:displayImageLocation>true</argos:displayImageLocation>
<argos:displayHexId>true</argos:displayHexId>
<argos:showHeader>true</argos:showHeader>
</argos:csvRequest>
</soap:Body>
</soap:Envelope>")]
        public string ArgosProgramSoapRequest {
            get {
                return ((string)(this["ArgosProgramSoapRequest"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<soap:Envelope xmlns:soap=""""http://www.w3.org/2003/05/soap-envelope"""" xmlns:argos=""""http://service.dataxmldistribution.argos.cls.fr/types"""">
<soap:Header/>
<soap:Body>
<argos:platformListRequest>
<argos:username>{0}</argos:username>
<argos:password>{1}</argos:password>
</argos:platformListRequest>
</soap:Body>
</soap:Envelope>")]
        public string ArgosPlatformListSoapRequest {
            get {
                return ((string)(this["ArgosPlatformListSoapRequest"]));
            }
        }
    }
}
