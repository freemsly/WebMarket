// <copyright company="Recorded Books Inc" file="PopularKeywordsProcessorLoader.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>

using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using WebMarket.ETL.configuration;
using WebMarket.Model.model;
using WebMarket.Server;

namespace WebMarket.ETL
{
    using WebMarket.Contracts;
    using WebMarket.Model;
    using System;

    public class PopularKeywordsProcessorLoader : ProcessorLoader<MediaTitle>
    {
        public HashSet<string> Data { get; set; }
        public override string FacetToken => "PopularKeywords";

        public override bool Initialize()
        {
            Data = new HashSet<string>();
            //var result  = service.Get<PopularKeywords>(null);
            var keywordServer = EtlServiceProvider.ServiceProvider.GetServices<PopularKeywordsMDG>();

            //var keywordServer = new PopularKeywordsMDG();
            var result = keywordServer.First().Get();
            if (result.Text != null)
            {
                foreach (var text in result.Text)
                {
                    Data.Add(text);
                }
                IsInitialized = true;
            }
            Console.WriteLine("Keywords Count" + Data.Count);
            return IsInitialized;
        }
       

        public override IProcessor<MediaTitle> Load()
        {
            var processor = new PopularKeywordsProcessor();
            processor.SourceData = Data;
            return processor;
        }
    }
}

