namespace Company.Web.ViewModel
{
    public class ClientAppSettings
    {
        public string  stsServer { get; set; }
        public string redirecturl { get; set; }
        public string clientid { get; set; }
        public string responsetype { get; set; }
        public string scope { get; set; }
        public string postlogoutredirecturi { get; set; }
        public bool startchecksession { get; set; }
        public bool silentrenew { get; set; }
        public string startuproute { get; set; }
        public string forbiddenroute { get; set; }
        public string unauthorizedroute { get; set; }
        public bool logconsolewarningactive { get; set; }
        public bool logconsoledebugactive { get; set; }
        public string maxidtokeniatoffsetallowedinseconds { get; set; }
        public string apiServer { get; set; }
        public string apiFileServer { get; set; }
        public bool overridewellknownconfiguration { get; set; }
        public string overridewellknownconfigurationurl { get; set; } 
    }
}
