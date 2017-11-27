using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebMarket.Model;
using WebMarket.Model.model;
using WebMarket.Server;

namespace WebMarket.ETL.configuration
{
    public static class EtlServiceProvider
    {
        static EtlServiceProvider()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
            ConfigureServices(new ServiceCollection());
            MapConnectionString();
        }

        public static IConfigurationRoot Configuration { get; set; }

        public static ConnectionString ConnectionStrings { get; set; }
        public static ServiceProvider ServiceProvider { get; set; }


        public static void ConfigureServices(IServiceCollection services)
        {
            services.Add(new ServiceDescriptor(typeof(PopularKeywordsMDG),
                new PopularKeywordsMDG(Configuration["ConnectionString:Mongo:Node"],
                    Configuration["ConnectionString:Mongo:Index"])));
            services.AddSingleton<IConnectionProvider, ConnectionProvider>();
            services.Configure<ConnectionString>(Configuration.GetSection("ConnectionString"));
            services.AddOptions();
            ServiceProvider = services.BuildServiceProvider();

            //ConnectionStrings = ServiceProvider.GetServices<ConnectionString>().First();
            //ConnectionStrings = configuration;
        }

        public static void MapConnectionString()
        {

            ConnectionStrings = new ConnectionString()
            {
                SqlServer = new SqlServer()
                {
                    trilogy = Configuration["ConnectionString:SqlServer:trilogy"],
                    dpcore = Configuration["ConnectionString:SqlServer:dpcore"],
                    dpmetadata = Configuration["ConnectionString:SqlServer:dpmetadata"],
                    ingestion = Configuration["ConnectionString:SqlServer:ingestion"],
                    oneclick = Configuration["ConnectionString:SqlServer:oneclick"]
                },
                MySql = new Model.model.MySql()
                {
                    zinio = Configuration["ConnectionString:MySql:zinio"]
                },
                Mongo = new Model.model.Mongo()
                {
                    Node = Configuration["ConnectionString:Mongo:Node"],
                    Index = Configuration["ConnectionString:Mongo:Index"],
                },
                ElasticSearch = new Model.model.ElasticSearch()
                {
                    Node =  Configuration.GetSection($"ConnectionString:ElasticSearch:Node").Get<string[]>(),
                    Index = Configuration["ConnectionString:ElasticSearch:Index"],
                
                }
                
            };
        }
    }
}
