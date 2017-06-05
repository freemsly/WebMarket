// <copyright company="Recorded Books, Inc" file="ElasticSearchConfiguration.cs">
// Copyright © 2017 All Right Reserved
// </copyright>


using Elasticsearch.Net;

namespace WebMarket.Api.Search.Elastic
{
    using Nest;
    using System;
    using System.Linq;
	

    public static class ElasticsearchConfiguration
    {
        public static readonly string DefaultIndexSuffix = "-products";
        public static readonly string Host = "54.208.112.49";
        public static readonly int MaxConnections = 5000;

        public static readonly int NumberOfShards = 6;
        public static readonly int NumberOfReplicas = 1;
        
        public static readonly string DefaultIndex = "na" + DefaultIndexSuffix;

        private static Version _currentVersion;
        public static Version CurrentVersion
        {
            get
            {
                if (_currentVersion == null)
                    _currentVersion =  GetCurrentVersion();

                return _currentVersion;
            }
        }

        public static Uri[] CreateBaseUri(int? port = null)
        {
			//var host1 = ElasticSection.Node1;
			//var host2 = ElasticSection.Node2;
			//var host3 = ElasticSection.Node3;

			var host1 = Host;
	        var host2 = Host;
	        var host3 = Host;

			var uri = new []
            {
                new UriBuilder("http", host1, port.GetValueOrDefault(9200)).Uri,
                new UriBuilder("http", host2, port.GetValueOrDefault(9200)).Uri,
                new UriBuilder("http", host3, port.GetValueOrDefault(9200)).Uri,
            };
            return uri;
        }

        

        public static ConnectionSettings Settings(int? port = null, Uri hostOverride = null)
        {

            return new ConnectionSettings(new StaticConnectionPool(CreateBaseUri()))
                .ConnectionLimit(MaxConnections)
                .DisableAutomaticProxyDetection(false)
                .PrettyJson();
        }

	    public static readonly Lazy<ElasticClient> Client = new Lazy<ElasticClient>(() => new ElasticClient(Settings()));
        //public static readonly Lazy<ElasticClient> ClientNoRawResponse = new Lazy<ElasticClient>(() => new ElasticClient(Settings().ExposeRawResponse(false)));
        //public static readonly Lazy<ElasticClient> ClientThatThrows = new Lazy<ElasticClient>(() => new ElasticClient(Settings().ThrowOnElasticsearchServerExceptions()));
        
        //public static string NewUniqueIndexName()
        //{
        //    return DefaultIndex + "_" + Guid.NewGuid().ToString();
        //}

        public static Version GetCurrentVersion()
        {
            //dynamic info = Client.Value.ConnectionSettings.Raw.Info().Response;
            //var versionString = (string)info.version.number;
            //if (versionString.Contains("Beta"))
            //    versionString = string.Join(".", versionString.Split('.').Where(s => !s.StartsWith("Beta", StringComparison.OrdinalIgnoreCase)));
            //var version = Version.Parse(versionString);
            //return version;
	        return null;
        }
    }
}
