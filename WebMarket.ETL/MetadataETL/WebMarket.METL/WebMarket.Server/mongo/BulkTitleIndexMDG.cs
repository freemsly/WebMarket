// <copyright company="Recorded Books Inc" file="BulkTitleIndexMDG.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>


using System;
using WebMarket.Model;

namespace WebMarket.Server
{
    using MongoDB.Driver;

    public class BulkTitleIndexMDG
    {
        public readonly string MongoUrl;
        public readonly string Database;
        public BulkTitleIndexMDG(string mongoUrl, string database)
        {
            MongoUrl = mongoUrl;
            Database = database;
        }
        public BulkTitleIndex Index( BulkTitleIndex t)
        {
            var client = new MongoClient(new MongoUrl(MongoUrl));
            var collectionName = "titleindex_" + DateTime.Now.Day;
            var collection = client.GetDatabase(Database).GetCollection<TitleIndex>(collectionName);
            collection.Database.DropCollection(collectionName);
            

            foreach (var title in t.Titles.Batch(2000))
            {
                collection.InsertMany(title);
            }
            return t;
        }
    }

}
