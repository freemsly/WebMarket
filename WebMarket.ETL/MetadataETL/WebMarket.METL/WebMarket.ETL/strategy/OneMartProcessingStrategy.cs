// <copyright company="Recorded Books Inc" file="OneMartProcessingStrategy.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>


using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using WebMarket.Contracts;
using WebMarket.Model;
using WebMarket.Model.model;

namespace WebMarket.ETL
{
 
    using System;
    using System.Diagnostics;

    public sealed class OneMartProcessingStrategy : ProcessingStrategy<MediaTitle>
    {
        public Stopwatch Timer { get; set; }
        public OneMartProcessingStrategy()
        {
            Timer = new Stopwatch();
            Timer.Start();
            //Loaders = _processLoaderDictionary[_onemarttype.ToLower()];
            Loaders = OneMartProcessingFactory.GetProcessorLoaders().Get();
        }

        protected override bool InitializeLoaders()
        {
            bool b = true;
            foreach (var item in Loaders)
            {
                b = b && item.Initialize();
                item.Write(String.Format("{0} {1}", item.IsInitialized, Timer.Elapsed.Seconds));
                Timer.Restart();
            }
            return b;
        }
    }

}
