// <copyright company="Recorded Books, Inc" file="PopularKeywords.cs">
// Copyright © 2017 All Right Reserved
// </copyright>


using System.Collections.Generic;

namespace WebMarket.Model
{
    using System;

    [Serializable]
    public  class PopularKeywords 
    {
        public MongoContext MongoContext { get; set; }
        public IEnumerable<string> Text { get; set; }
    }

    public class MongoContext
    {
        public readonly string _mongoConnection;
        public readonly string _mongoDb;
        public MongoContext(string mongoConnection, string mongoDb)
        {
            _mongoConnection = mongoConnection;
            _mongoDb = mongoDb;
        }

        public string GetConnection()
        {
            return _mongoConnection;
        }
    }
}
