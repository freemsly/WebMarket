// <copyright company="Recorded Books Inc" file="OneMartProcessingFactory.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>


using System.Configuration;

namespace WebMarket.ETL
{

    public static class OneMartProcessingFactory
    {
        //private readonly static string Onemarttype = ConfigurationManager.AppSettings["onemarttype"] ?? OneMartType.Full.ToString().ToLower();
        private readonly static string Onemarttype =  WebMarketType.Full.ToString().ToLower();
        public static IOneMartProcessing GetProcessorLoaders()
        {
            switch (Onemarttype.ToLower())
            {
                case "full":
                    return new FullOneMartProcessing();
                case "delta":  
                    return new DeltaOneMartProcessing();
                default:
                    return new FullOneMartProcessing();
            }
        }
    }

}
