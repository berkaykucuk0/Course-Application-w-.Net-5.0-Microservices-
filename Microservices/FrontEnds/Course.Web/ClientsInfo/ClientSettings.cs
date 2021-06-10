using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Web.ClientsInfo
{
    //This class for in appsettings.json 
    public class ClientSettings
    {
        public ClientInfo WebClient { get; set; }
        public ClientInfo WebClientForUser { get; set; }
    }

    public class ClientInfo
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
