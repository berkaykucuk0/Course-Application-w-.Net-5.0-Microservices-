using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Web.Settings
{
    public class ServiceApiSettings
    {
        public string IdentityBaseUri { get; set; }
        public string GatewayBaseUri { get; set; }
        public string PhotoStockUri { get; set; }
        public ApiPath Catalog { get; set; }
        public ApiPath PhotoStock { get; set; }
    }
    public class ApiPath
    {
        public string Path { get; set; }
    }

}
