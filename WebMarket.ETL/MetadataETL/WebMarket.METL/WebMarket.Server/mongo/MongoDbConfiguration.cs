// <copyright company="Recorded Books, Inc" file="MongoDbConfiguration.cs">
// Copyright © 2015 All Right Reserved
// </copyright>


using MongoDB.Driver;

namespace RB.OneMart
{
    using System.Configuration;

    public static class MongoDbConfiguration
    {
        //static readonly MongoDbSectionHandler MongoConfig = ConfigurationManager.GetSection("mongosection") as MongoDbSectionHandler;

        //public static string Connection()
        //{
        //    return MongoConfig.Url;
        //}

        //public static MongoClient Client()
        //{
        //    return new MongoClient(new MongoUrl(Connection())); 
        //}

        //public static IMongoDatabase Database()
        //{
        //    return Client().GetDatabase(MongoConfig.Database);
        //}

        //public static string Environment()
        //{
        //    var environment = ConfigurationManager.AppSettings["environment"];
        //    return !string.IsNullOrEmpty(environment) ? environment : "na";
        //}
    }
}
