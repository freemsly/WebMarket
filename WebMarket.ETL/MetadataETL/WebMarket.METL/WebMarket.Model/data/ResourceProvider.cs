// <copyright company="Recorded Books, Inc" file="ResourceProvider.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

using System.Configuration;
using System.Resources;

namespace WebMarket.Model
{
    public static class ResourceProvider
    {
        public static ResourceManager Get()
        {
            //var environment = ConfigurationBuider.AppSettings["environment"];
            var environment = "na";
            switch (environment)
            {
                case "na":
                    return new ResourceManager("WebMarket.Model.na.Resources", typeof (na_Resources).Assembly);
                case "wfh":
                    return new ResourceManager("WebMarket.Model.wfh.Resources", typeof (wfh_Resources).Assembly);
                case "ws":
                    return new ResourceManager("WebMarket.Model.ws.Resources", typeof(wfh_Resources).Assembly);
                default:
                    return new ResourceManager("WebMarket.Model.na.Resources", typeof (na_Resources).Assembly);
            }
        }
    }
}
