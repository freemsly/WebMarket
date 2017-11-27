using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebMarket.Model;
using WebMarket.Model.model;
using WebMarket.Server;

namespace WebMarket.METL
{
    public  class Startup
    {
        public Startup()
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }

        public  IConfigurationRoot Configuration { get; set; }

        public  void ConfigureServices(IServiceCollection services)
        {
            services.Add(new ServiceDescriptor(typeof(MongoContext), new MongoContext(Configuration["ConnectionString:Mongo:Index"], Configuration["ConnectionString:Mongo:Index"])));
            // add logging
            services.AddSingleton(new LoggerFactory().AddConsole());
            services.AddLogging();
            
            services.Configure<ConnectionString>(Configuration.GetSection("ConnectionString"));
            //services.Configure<ConnectionString.Mongo>(Configuration.GetSection("ConnectionString").GetSection("Mongo"));
            services.AddOptions();
            
            //services.AddSingleton(Configuration);
        }
    }
}
