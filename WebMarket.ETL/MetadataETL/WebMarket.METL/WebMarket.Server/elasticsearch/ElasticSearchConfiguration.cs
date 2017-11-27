// <copyright company="Recorded Books, Inc" file="ElasticSearchConfiguration.cs">
// Copyright © 2017 All Right Reserved
// </copyright>

using System;
using Elasticsearch.Net;
using Nest;

namespace WebMarket.Server.elasticsearch
{
    public static class ElasticSearchConfiguration
    {
        public static readonly int MaxConnections = 50000;

        public static readonly int NumberOfShards = 2;
        public static readonly int NumberOfReplicas = 1;

        public static string LiveIndexAlias => CreateLiveIndexAlias();
        public static string OldIndexAlias => CreateOldIndexAlias();
        public static string IndexName { get; set; }
        public static string Host1 { get; set; }
        public static string Host2 { get; set; }
        public static string Host3 { get; set; }

        public static ElasticClient GetClient()
		{
            IndexName = CreateIndexName();
            return new ElasticClient(Settings(IndexName));
		}

        public static Uri[] CreateBaseUri(int? port = null)
        {
            var host1 = Host1;
            var host2 = Host2;
            var host3 = Host3;

            var uri = new[]
            {
                new UriBuilder("http", host1).Uri,
                new UriBuilder("http", host2).Uri,
                new UriBuilder("http", host3).Uri,
            };
            return uri;
        }

        public static ConnectionSettings Settings(string indexName, int? port = null, Uri hostOverride = null)
        {
            return new ConnectionSettings(CreateSniffingConnectionPool(CreateBaseUri()))
                .DisableDirectStreaming()
                .DefaultIndex(IndexName)
                .ConnectionLimit(1000)
                //.EnableHttpCompression()
                .RequestTimeout(TimeSpan.FromMinutes(120))
                .DisableAutomaticProxyDetection(false);
        }
        private static SniffingConnectionPool CreateSniffingConnectionPool(Uri[] nodes)
        {
            return new SniffingConnectionPool(nodes);
        }


        private static string CreateIndexName()
        {
            return string.Format("{0}-{1:dd-MM-yyyy-HH-mm-ss}", LiveIndexAlias, DateTime.Now);
        }

        private static string CreateLiveIndexAlias()
        {
            return "na-products";
        }

        private static string CreateOldIndexAlias()
        {
            return "na-products" + "-old";
        }
    }
}
