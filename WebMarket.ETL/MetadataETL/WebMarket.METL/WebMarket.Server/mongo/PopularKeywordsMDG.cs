// <copyright company="Recorded Books Inc" file="PopularKeywordsMDG.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>

using System.Linq;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WebMarket.Model;
using WebMarket.Model.model;

namespace WebMarket.Server
{
    using MongoDB.Bson;
    using System;

    public class PopularKeywordsMDG
    {
        public readonly string _mongoUrl;
        public readonly string _mongoDb;
        public PopularKeywordsMDG(string mongoUrl, string mongoDb)
        {
            _mongoUrl = mongoUrl;
            _mongoDb = mongoDb;
        }
        public PopularKeywords Get()
        {
            var keywords = new PopularKeywords();

            var db = new MongoClient(new MongoUrl(_mongoUrl)).GetDatabase(_mongoDb);

            var collection = db.GetCollection<BsonDocument>("profiledata");
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("Year", DateTime.Now.Year) & builder.Eq("Month", DateTime.Now.Month) &
                         builder.Gt("ResultCount", 0) & builder.Regex("RequestPath", new BsonRegularExpression("/search/"))
                         & builder.Ne("Key", BsonNull.Value);

            var projection = Builders<BsonDocument>.Projection.Include("Key");
            var result = collection.Find(filter).Project(projection).ToListAsync();
            keywords.Text = result.Result.Select(item => item[1].AsBsonValue.ToString()
            .Replace("keywords:", string.Empty)).Take(1000).Distinct(StringComparer.CurrentCultureIgnoreCase).ToList();
            return keywords;
        }


        
    }
}
