using System;
using System.Collections.Generic;
using System.Text;

namespace WebMarket.Model.model
{
    public class ConnectionString
    {
        public SqlServer SqlServer { get; set; }
        public MySql MySql { get; set; }

        public ElasticSearch ElasticSearch { get; set; }

        public Mongo Mongo { get; set; }
    }
    public class SqlServer
    {
        public string dpcore { get; set; }
        public string dpmetadata { get; set; }
        public string oneclick { get; set; }
        public string trilogy { get; set; }

        public string ingestion { get; set; }
    }
    public class MySql
    {
        public string zinio { get; set; }
    }

    public class ElasticSearch
    {
        public string[] Node { get; set; }
        public string Index { get; set; }
    }

    public class Mongo
    {
        public string Node { get; set; }
        public string Index { get; set; }
    }

}
